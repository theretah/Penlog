using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Penlog.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace Penlog.Pages
{
    [Authorize]
    public class EditProfileModel : PageModel
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IUserStore<AppUser> userStore;
        private readonly IEmailSender emailSender;

        public EditProfileModel(UserManager<AppUser> userManager, IUserStore<AppUser> userStore, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.userStore = userStore;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public string Biography { get; set; }

        [Required]
        [RegularExpression(@"^([^0-9]*)$", ErrorMessage = "Username must NOT contain any digit.")]
        [BindProperty]
        public string Username { get; set; }

        [Phone]
        [BindProperty]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [BindProperty]
        public string Email { get; set; }

        public async Task<IActionResult> OnGet(string authorId)
        {
            var user = await userManager.FindByIdAsync(authorId);
            Biography = user.Biography;
            Username = user.UserName;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var user = userManager.GetUserAsync(User).Result;

            await userStore.SetUserNameAsync(user, Username, CancellationToken.None);

            var phoneNumber = await userManager.GetPhoneNumberAsync(user);
            if (PhoneNumber != phoneNumber)
                await userManager.SetPhoneNumberAsync(user, PhoneNumber);

            //var email = await userManager.GetEmailAsync(user);
            //if (Email != email)
            //{
            //    var userId = await userManager.GetUserIdAsync(user);
            //    var code = await userManager.GenerateChangeEmailTokenAsync(user, Email);
            //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //    var callbackUrl = Url.Page(
            //        "/Account/ConfirmEmailChange",
            //        pageHandler: null,
            //        values: new { area = "Identity", userId = userId, email = Email, code = code },
            //        protocol: Request.Scheme);
            //    await emailSender.SendEmailAsync(
            //        Email,
            //        "Confirm your email",
            //        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            //}

            user.Biography = Biography;

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
                return RedirectToPage("Profile", new { id = user.Id });

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return await OnGet(user.Id);
        }
    }
}
