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
    }
}