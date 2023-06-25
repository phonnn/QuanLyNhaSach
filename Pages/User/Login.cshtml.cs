using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaSach.Processing;
using System.Security.Claims;

namespace QuanLyNhaSach.Pages.User
{
    public class LoginModel : PageModel
    {
        private readonly IUser _Processing = (IUser)Injector.Injector.GetProcessing<UserProcessing>();
        public string notify = string.Empty;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                Entities.User user = await _Processing.validateUser(Username, Password);
                if (user != null)
                {
                    List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, Username),
                };

                    foreach (Entities.UserRole role in user.UserRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.RoleNavigation.Name));
                    }

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
					var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToPage("/Index");
                }

                ModelState.AddModelError("", "Invalid username or password");
                return Page();
            } catch (Exception ex)
            {
                notify = ex.Message;
                return Page();
            }
        }
    }

}
