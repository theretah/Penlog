using Microsoft.AspNetCore.Mvc;
using Penlog.Model.Entities;

namespace Penlog.Pages.Shared.Components
{
    public class ProfileImageViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(AppUser profileUser)
        {
            var profileImageDataUrl = "default-profile.jpg";
            if (profileUser.ProfileImage != null)
            {
                string imageBase64Data = Convert.ToBase64String(profileUser.ProfileImage.Bytes);
                profileImageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            }

            return View(model:profileImageDataUrl);
        }
    }
}
