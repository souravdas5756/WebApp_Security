using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;

namespace WebApp_UnderTheHood.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; } = new Credential();
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Simulate a login process
            if (Credential.UserName == "admin" && Credential.Password == "password")
            {
                //create security claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Credential.UserName),
                    new Claim(ClaimTypes.Email, "admin@myweb.com")
                };
                //create identity
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                //create principal
                var principal = new ClaimsPrincipal(identity);
                //sign in the user
                await HttpContext.SignInAsync("MyCookieAuth", principal);
                //redirect to home page
                return RedirectToPage("/Index");

            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();

        }
    }

    public class Credential
    {
        [Required(ErrorMessage = "Name is required.")]
        [Display(Description ="User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

    }
}
