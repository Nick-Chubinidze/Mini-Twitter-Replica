using System.IO;
using System.Web;

namespace Domain.Helpers
{
    class PhotoFolderHelper
    { 
        public static void CreatePhotoFolderIfNotExist(string Email)
        { 
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Format("~/Content/img/Uploads/ProfileOriginalSize/{0}", Email))))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Format("~/Content/img/Uploads/ProfileOriginalSize/{0}", Email)));
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Format("~/Content/img/Uploads/ProfileSmallSize/{0}", Email)));
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Format("~/Content/img/Uploads/ProfileMediumSize/{0}", Email))); 
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Format("~/Content/img/Uploads/CoverBigSize/{0}", Email)));
            }
        }
    }
}
