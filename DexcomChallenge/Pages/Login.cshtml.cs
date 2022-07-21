using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DexcomChallenge.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
            string returnUrl = "/";
            Challenge(new AuthenticationProperties() { RedirectUri = returnUrl });
        }
    }
}
