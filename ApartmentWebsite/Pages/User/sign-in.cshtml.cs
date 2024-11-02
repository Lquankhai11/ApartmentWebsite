using ApartmentWebsite.Models;
using ApartmentWebsite.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApartmentWebsite.Pages.User
{
    public class Sign_inModel : PageModel
    {
        private readonly UserService _userService;
        private readonly PRN221_PRJContext _context;

        public Sign_inModel(UserService userService, PRN221_PRJContext context)
        {
            _userService = userService;
            _context = context;

        }
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public bool RememberMe { get; set; }
        public string ErrorMessage { get; set; }
        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                var roleId = User.FindFirst(ClaimTypes.Role)?.Value;
                if (roleId == "1")
                {
                    Response.Redirect("/User/Home");
                    return;
                }
                else if (roleId == "2")
                {
                    Response.Redirect("/User/Dashboard");
                    return;
                }
            }

            ErrorMessage = TempData["ErrorMessage"] as string;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _context.UserInfs.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == Username || u.PhoneNumber == Username);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Account not exits.";
                return Page();
            }

            if (!BCrypt.Net.BCrypt.Verify(Password, user.Password))
            {
                TempData["ErrorMessage"] = "Incorrect password.";
                return Page();
            }

            if ((bool)!user.Status)
            {
                TempData["ErrorMessage"] = "Your account had been baned.";
                return Page();
            }
            // Tạo claims cho người dùng
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Cài đặt thời gian hết hạn cookie
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = RememberMe, // Đặt IsPersistent dựa trên trạng thái của checkbox
                ExpiresUtc = RememberMe
                    ? DateTimeOffset.UtcNow.AddDays(7) // 7 ngày nếu được chọn
                    : DateTimeOffset.UtcNow.AddHours(1) // 1 giờ nếu không được chọn
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);


            if (user.RoleId == 1)
            {
                return RedirectToPage("/User/Home");
            }
            else if (user.RoleId == 2)
            {
                return RedirectToPage("/User/Dashboard");
            }

            return RedirectToPage("/User/Login");
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/User/Sign_in");
        }

    }
}
