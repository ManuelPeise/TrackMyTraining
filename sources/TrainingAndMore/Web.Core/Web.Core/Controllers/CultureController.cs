using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Web.Core.Controllers
{
    [Route("[controller]/[action]")]
    public class CultureController : Controller
    {
        public IActionResult Set(string selectedCulture, string redirectUri)
        {
            
            Debug.WriteLine(selectedCulture);

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(
                    new RequestCulture(selectedCulture, selectedCulture)));

            return Redirect(redirectUri);
        }
    }
}
