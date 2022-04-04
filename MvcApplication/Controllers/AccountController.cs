using System.Configuration;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using MvcApplication.Model;
using Newtonsoft.Json;
using RestSharp;

namespace MvcApplication.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string returnUrl)
        {
            HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties
                {
                    RedirectUri = returnUrl ?? Url.Action("Index", "Home")
                },
                "Auth0");
            return new HttpUnauthorizedResult();
        }

        public ActionResult Authrization(string code, string state)
        {
            string auth0ClientId = ConfigurationManager.AppSettings["auth0:ClientId"];
            string auth0ClientSecret = ConfigurationManager.AppSettings["auth0:ClientSecret"];
            string auth0RedirectUri = "http://localhost:3000/account/authrizationtoken";

            var client = new RestClient("https://dev-0ztxkmi4.us.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=authorization_code&client_id="+ auth0ClientId + "&client_secret="+ auth0ClientSecret + "&code="+ code + "&redirect_uri="+ auth0RedirectUri + "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            string res = response.Content;
            AuthResponse authRes = JsonConvert.DeserializeObject<AuthResponse>(res);
            return View(authRes);
        }

        public ActionResult AuthrizationToken(string refresh_token)
        {
            string auth0ClientId = ConfigurationManager.AppSettings["auth0:ClientId"];

            var client = new RestClient("https://dev-0ztxkmi4.us.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");            
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=refresh_token&client_id=" + auth0ClientId + "&refresh_token=" + refresh_token + "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public void Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            HttpContext.GetOwinContext().Authentication.SignOut("Auth0");
        }

        [Authorize]
        public ActionResult Tokens()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            ViewBag.IdToken = claimsIdentity?.FindFirst(c => c.Type == "id_token")?.Value;

            return View();
        }

        [Authorize]
        public ActionResult Claims()
        {
            return View();
        }
    }
}