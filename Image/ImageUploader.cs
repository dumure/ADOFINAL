using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace ADOFINAL.Image
{
    public class ImageUploader
    {
        private const string cloud_name = "dw0okfdb2";
        private const string api_key = "985281241538259";
        private const string api_secret = "kU5icFk5mUJ4I3-28NvAhuVZHN4";
        private Cloudinary cloudinary;
        public ImageUploader()
        {
            Account account = new Account(cloud_name, api_key, api_secret);
            cloudinary = new Cloudinary(account);
        }
        public string Upload(string filePath)
        {
            ImageUploadParams imageUploadParams = new ImageUploadParams()
            {
                File = new FileDescription(filePath)
            };
            var result = cloudinary.Upload(imageUploadParams);
            return result.SecureUrl.ToString();
        }
    }
}
