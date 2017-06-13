using System.Drawing;
using System.Drawing.Drawing2D; 
using System.Web.Hosting;

namespace Twitter_Nika_Chubinidze.Helpers
{
    public class ImageHelper
    {
        public static void ResizeAndSaveImage(ImageAndPath iap, int size = 800, int coverHeight = 0)
        {
            int newHeight = 0;
            Bitmap resizedImage;

            if (coverHeight == 0)
                newHeight = (int)(iap.img.Height * ((float)size / (float)iap.img.Width));
            else
                newHeight = coverHeight;

            resizedImage = new Bitmap(size, newHeight);
            Graphics gr = Graphics.FromImage(resizedImage);
            gr.SmoothingMode = SmoothingMode.HighQuality;
            gr.CompositingQuality = CompositingQuality.HighQuality;
            gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gr.DrawImage(iap.img, 0, 0, size, newHeight); 

            resizedImage.Save(HostingEnvironment.MapPath(string.Format("~/Content/img/Uploads/{0}/{1}/{2}",iap.folder,iap.user,iap.pic)), System.Drawing.Imaging.ImageFormat.Jpeg); 
        } 
    }
}