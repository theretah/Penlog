using Microsoft.AspNetCore.Mvc;
using Penlog.Data.Repository.IRepository;
using Penlog.Model.Entities;
using Penlog.ViewModels;

namespace Penlog.Pages.Shared.Components
{
    public class ProfileImageViewComponent : ViewComponent
    {
        private readonly IUnitOfWork unit;

        public ProfileImageViewComponent(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<IViewComponentResult> InvokeAsync(AppUser profileUser, int widthAndHeight)
        {
            var profileImageDataUrl = "default-profile.jpg";
            var profileImage = unit.Images.Find(i => i.Id == profileUser.ProfileImageId).FirstOrDefault();
            if (profileImage != null)
            {
                string imageBase64Data = Convert.ToBase64String(profileImage.Bytes);
                profileImageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            }

            return View(new ProfileImageVM
            {
                ProfileImageDataUrl = profileImageDataUrl,
                ImageWidth = widthAndHeight,
                ImageHeight = widthAndHeight
            });
        }
    }
}
