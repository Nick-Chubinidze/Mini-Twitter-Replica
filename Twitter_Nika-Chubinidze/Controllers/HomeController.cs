using Domain.Abstract;
using Domain.Entities;
using System.Drawing; 
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Twitter_Nika_Chubinidze.Helpers;
using Twitter_Nika_Chubinidze.Models;
using System;

namespace Twitter_Nika_Chubinidze.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly BaseRepository<Photo> _photoRepository;
        private readonly BaseRepository<TwitterUser> _twitterUserRepository;
        private readonly BaseRepository<Tweet> _tweetRepository;

        public HomeController(BaseRepository<Photo> photoRepository, BaseRepository<TwitterUser> twitterUserRepository, BaseRepository<Tweet> tweetRepository)
        {
            _twitterUserRepository = twitterUserRepository;
            _photoRepository = photoRepository;
            _tweetRepository = tweetRepository;
        }

        public ActionResult Index(string friend)
        {
            var allUsers = _twitterUserRepository.Set();
            TwitterUser user;

            if (!string.IsNullOrEmpty(friend))
                user = allUsers.FirstOrDefault(x => x.Email == friend);
            else 
                user = allUsers.FirstOrDefault(x => x.Email == User.Identity.Name);

            var followers = allUsers.Where(id => id.Followers.Any(x => x.Email == user.Email));

            var tweets = _tweetRepository.Set().Where(x => followers.Any(t => t.Email == x.User.Email) || x.User.UserName == user.UserName);

            var profilePic = user.Photos.LastOrDefault(x => !x.IsCover);
            var CoverPic = user.Photos.LastOrDefault(x => x.IsCover);

            string photoPathCover = null;
            if (CoverPic != null) 
                photoPathCover = string.Format("../../Content/img/Uploads/CoverBigSize/{0}/{1}", user.Email, CoverPic.PhotoPath); 

            string photoPathProfile = null;

            if (profilePic != null)
            {
                if (System.IO.File.Exists(Server.MapPath(string.Format("~/Content/img/Uploads/ProfileSmallSize/{0}/{1}", user.Email, profilePic.PhotoPath))))
                    photoPathProfile = string.Format("/Content/img/Uploads/ProfileSmallSize/{0}/{1}", user.Email, profilePic.PhotoPath);
                else if (System.IO.File.Exists(Server.MapPath(string.Format("/Content/img/Uploads/ProfileOriginalSize/{0}/{1}", user.Email, profilePic.PhotoPath))))
                    photoPathProfile = string.Format("/Content/img/Uploads/ProfileOriginalSize/{0}/{1}", user.Email, profilePic.PhotoPath);
            }

            var model = new MainModel { ProfilePhoto = photoPathProfile, CoverPhoto = photoPathCover, FirstName = user.Name, LastName = user.Surname, User = user.UserName, Tweets = tweets.Reverse() };
            return View(model);
        }

        public ActionResult SaveImage(HttpPostedFileBase img)
        {
            if (ImgRestr(img))
                SaveImg(img); 

            return RedirectToAction("Index");
        }

        public ActionResult SaveCoverImage(HttpPostedFileBase img)  
        {
            if (ImgRestr(img))
                SaveImg(img, true); 

            return RedirectToAction("Index");
        }

        private void SaveImg(HttpPostedFileBase img, bool isCover=false)
        {
            var user = _twitterUserRepository.Set().FirstOrDefault(x => x.Email == User.Identity.Name);

            try
            {
                Bitmap image = new Bitmap(img.InputStream); 

                if (!isCover)
                {
                    image.Save(HostingEnvironment.MapPath(
                                        string.Format("~/Content/img/Uploads/{0}/{1}/{2}", "ProfileOriginalSize", user.Email, img.FileName)),
                                        System.Drawing.Imaging.ImageFormat.Jpeg);

                    if (image.Width > 800)
                    {
                        ImageHelper.ResizeAndSaveImage(new ImageAndPath { folder = "ProfileMediumSize", user = user.Email, img = image, pic = img.FileName });
                        ImageHelper.ResizeAndSaveImage(new ImageAndPath { folder = "ProfileSmallSize", user = user.Email, img = image, pic = img.FileName }, 130);
                    }
                    else if (image.Width > 120)
                        ImageHelper.ResizeAndSaveImage(new ImageAndPath { folder = "ProfileSmallSize", user = user.Email, img = image, pic = img.FileName }, 130);
                }
                else 
                    ImageHelper.ResizeAndSaveImage(new ImageAndPath { folder = "CoverBigSize", user = user.Email, img = image, pic = img.FileName },1500,300);  

                _photoRepository.Save(new Photo { PhotoPath = img.FileName, TwitterUser = user, IsCover = isCover });
            }
            catch (System.Exception e)
            {
                //Log in real application
            } 
        }

        private bool ImgRestr(HttpPostedFileBase img)
        {
            if (img != null && (img.ContentType == "image/jpg" || img.ContentType == "image/jpeg" || img.ContentType == "image/png"))
                return true;

            TempData["message"] = "Provided file is Empty or wrong format";
            return false;
        }


        [HttpGet]
        public PartialViewResult Tweets()
        {
            var allUsers = _twitterUserRepository.Set();
            var user = allUsers.FirstOrDefault(x => x.Email == User.Identity.Name);

            var followers = allUsers.Where(id => id.Followers.Any(x => x.Email == user.Email));

            var tweets = _tweetRepository.Set().Where(x => followers.Any(t => t.Email == x.User.Email) || x.User.UserName == user.UserName);

            return PartialView(tweets.Reverse());
        }

        [HttpPost]
        public PartialViewResult Tweets(string post)
        { 
            var allUsers = _twitterUserRepository.Set();
            var user = allUsers.FirstOrDefault(x => x.Email == User.Identity.Name);

            if (post.Length > 0) 
                _tweetRepository.Save(new Tweet { TweetText = post, User = user, TweetDate = DateTime.Now });
            else 
                TempData["message"] = "Provided post should not be";

            var followers = allUsers.Where(id => id.Followers.Any(x => x.Email == user.Email));

            var tweets = _tweetRepository.Set().Where(x => followers.Any(t => t.Email == x.User.Email) || x.User.UserName == user.UserName);

            return PartialView(tweets.Reverse());
        }

        [HttpPost]
        public ActionResult DeleteTweet (int post)
        {
            var tweet = _tweetRepository.Set().FirstOrDefault(x => x.Id == post);

            _tweetRepository.Delete(tweet);

            return RedirectToAction("Tweets");
        }

        [HttpPost]
        public ActionResult ReTweets(int tweetId,string post) 
        {  
            var user = _twitterUserRepository.Set().FirstOrDefault(x => x.Email == User.Identity.Name);
            var tweet = _tweetRepository.Set().FirstOrDefault(x=>x.Id==tweetId);
            tweet.ReTweets.Add(new Tweet { TweetText = post, User = user, TweetDate = DateTime.Now, IsComment = true });

            _tweetRepository.Save(tweet);  

            return RedirectToAction("Tweets");
        }
    }
}