using ApartmentWebsite.Helper;
using ApartmentWebsite.Models;
using ApartmentWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ApartmentWebsite.Pages.User
{
    public class VerifyPassOtpModel : PageModel
    {
        private readonly PRN221_PRJContext _context;

        public VerifyPassOtpModel(PRN221_PRJContext context)
        {

            _context = context;
        }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string OTP { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string confirmPassword { get; set; }
        public string ErrorMessage { get; set; }
        public void OnGet()
        {
            Email = TempData["Email"] as string;
            ErrorMessage = TempData["ErrorMessage"] as string;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _context.UserInfs.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == Email);
            var otp = HttpContext.Session.GetObjectFromJson<string>("Otp");
            if (otp == null)
            {
                TempData["ErrorMessage"] = "Otp not exsit.";
                return RedirectToPage("/User/Forgot-password");
            }
            if (!Password.Equals(confirmPassword))
            {
                TempData["ErrorMessage"] = "Password are not match.";
                return Page();
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(Password);
            _context.UserInfs.Update(user);
            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("Otp");
            return RedirectToPage("/User/Sign-in");
        }

    }
}
