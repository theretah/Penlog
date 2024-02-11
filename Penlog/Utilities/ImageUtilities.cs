using Penlog.Entities;

namespace Penlog.Utilities
{
    public static class ImageUtilities
    {
        public static string GenerateImageDataUrl(byte[] bytes)
        {
            string imageBase64Data = Convert.ToBase64String(bytes);
            string imageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

            return imageDataUrl;
        }
        public static string GeneratePreviewImageDataUrl(Image? image)
        {
            return (image != null) ? GenerateImageDataUrl(image.Bytes) : "";
        }
    }
}