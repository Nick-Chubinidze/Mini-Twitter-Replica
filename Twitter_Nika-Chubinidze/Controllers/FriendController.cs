using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Web.Mvc;
using System.Web.Routing;
using Twitter_Nika_Chubinidze.Models;

namespace Twitter_Nika_Chubinidze.Controllers
{
    public class FriendController : Controller
    {
        private readonly BaseRepository<TwitterUser> _twitterUserRepository;
        private readonly BaseRepository<FollowerUser> _followerUserRepository;

        public FriendController(BaseRepository<TwitterUser> twitterUserRepository, BaseRepository<FollowerUser> followerUserRepository)
        {
            _twitterUserRepository = twitterUserRepository;
            _followerUserRepository = followerUserRepository;
        }

        public ActionResult Index(string search)
        {
            FriendModel fm = new FriendModel();
            IEnumerable<TwitterUser> result;
            IEnumerable<TwitterUser> users;

            if (!string.IsNullOrEmpty(search))
            {
                var words = search.Split(' ').Where(x => x.Length > 0).ToEnumerable();

                result  =
                    _twitterUserRepository.Set()
                        .Where(p =>
                        {
                            var enumerable = words as string[] ?? words.ToArray();
                            return enumerable.Any(w => p.Email.ToLowerInvariant().Contains(w.ToLowerInvariant()))
                            ||
                            p.Email.Split(' ', ',', '.', '-', '(', ')')
                                .Where(x => x.Length > 1)
                                .ToEnumerable()
                                .Any(w => enumerable.Any(c => c.ToLowerInvariant().Contains(w.ToLowerInvariant())));
                        });

                var friend = _twitterUserRepository.Set().Where(x => x.Followers.Any(y => y.Email == User.Identity.Name));

                if (friend != null)
                    users = result.Where(p => !friend.Any(p2 => p2.Email == p.Email) && p.Email != User.Identity.Name);
                else
                    users = result.Where(p => p.Email != User.Identity.Name);

                fm.Users = users;
                fm.Friends = friend;
                return View(fm);
            }

            var friends = _twitterUserRepository.Set().Where(x => x.Followers.Any(y => y.Email == User.Identity.Name));
            fm.Friends = friends;
            return View(fm);
        }

        public ActionResult AddToFriends(string follow)
        {
            var allUsers = _twitterUserRepository.Set(); 

            var friend = allUsers.FirstOrDefault(x => x.Email == follow);

            if (friend == null)
            {
                ViewBag.Error = "No such person Found";
                return View("Index");
            }

            var user = allUsers.FirstOrDefault(x => x.Email == User.Identity.Name);

            friend.Followers.Add(user);
            user.Followers.Add(friend);

            _twitterUserRepository.Save(user);
            _twitterUserRepository.Save(friend); 

            return RedirectToAction("Index", new RouteValueDictionary( new { controller = "Home", action = "Index", friend = follow }));
        }
    }
}