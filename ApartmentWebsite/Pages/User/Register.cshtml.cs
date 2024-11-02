using ApartmentWebsite.Models;
using ApartmentWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
namespace ApartmentWebsite.Pages.User
{
    public class RegisterModel : PageModel
    {
        private readonly PRN221_PRJContext _context;

        public RegisterModel(PRN221_PRJContext context)
        {
            _context = context;

        }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string confirmPassword { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingUser = await _context.UserInfs.FirstOrDefaultAsync(u => u.Email == Email);
            if (existingUser != null)
            {
                TempData["ErrorMessage"] = "Email already in use.";
                return Page();
            }

            if (Password != confirmPassword)
            {
                TempData["ErrorMessage"] = "Passwords do not match.";
                return Page();
            }
            var newUser = new UserInf
            {
                Email = Email,
                Password = BCrypt.Net.BCrypt.HashPassword(Password),
                Status = true,
                RoleId = 1
            };

            _context.UserInfs.Add(newUser);
            await _context.SaveChangesAsync();
            return RedirectToPage("/User/Sign-in");
        }
        public void OnGet()
        {
            ErrorMessage = TempData["ErrorMessage"] as string;
        }
    }
}
