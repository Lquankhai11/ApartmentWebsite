using ApartmentWebsite.Helper;
using ApartmentWebsite.Models;
using ApartmentWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ApartmentWebsite.Pages.User
{
    public class Forgot_passwordModel : PageModel
    {
        private readonly UserService _userService;
        private readonly IEmailService _emailService;
        private readonly PRN221_PRJContext _context;

        public Forgot_passwordModel(UserService userService, IEmailService emailService, PRN221_PRJContext context)
        {
            _userService = userService;
            _emailService = emailService;
            _context = context;
        }
        [BindProperty]
        public string Email { get; set; }
        public string ErrorMessage { get; set; }
        public void OnGet()
        {
            ErrorMessage = TempData["ErrorMessage"] as string;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _context.UserInfs.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == Email);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Account not exsit.";
            }

            var otp = _userService.GenerateRandomNumber();

            // Lưu OTP vào Session
            HttpContext.Session.SetObjectAsJson("Otp", otp, TimeSpan.FromMinutes(5));
            TempData["Email"] = Email;
            // Gửi OTP qua email
            await _emailService.SendOtpAsync(Email, otp);

            return RedirectToPage("/User/VerifyPassOtp");
        }
    }
}
