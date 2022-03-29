using MvcApplication.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApplication.Controllers
{
    [RoutePrefix("api")]
    public class DefultController : ApiController
    {
        [HttpGet]
        [Route("private")]
        //[Authorize]
        public IHttpActionResult Private()
        {
            return Json(new
            {
                Message = "Hello from a private endpoint! You need to be authenticated to see this."
            });
        }

        [HttpPost]
        [Route("private")]
        //[Authorize]
        public IHttpActionResult Post()
        {
            var client = new RestClient("https://dev-0ztxkmi4.us.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"bQd75pd6BE0CAsonrvR4K3iAqlVXmYjo\",\"client_secret\":\"0C7Ck0h2t5VniuCN9vrvPhZyATrYcwGW9bY3V0KEjOuTu6a8ugTKCJ1PPcR3q4uD\",\"audience\":\"https://dev-0ztxkmi4.us.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return Json(response.Content);
        }

        private static List<CustomListItem> Item = new List<CustomListItem> {
                new CustomListItem
                {
                    Term= "Access Token",
                    Definition = "A credential that can be used by an application to access an API. It informs the API that the bearer of the token has been authorized to access the API and perform specific actions specified by the scope that has been granted."
                },
                new CustomListItem
                {
                    Term= "JWT",
                    Definition = "An open, industry standard RFC 7519 method for representing claims securely between two parties. "
                },
                new CustomListItem
                {
                    Term= "OpenID",
                    Definition = "An open standard for authentication that allows applications to verify users are who they say they are without needing to collect, store, and therefore become liable for a user’s login information."
                }
        };

        [HttpGet]
        [Route("GetAll")]
        [Authorize]
        public IHttpActionResult Get()
        {
            return Ok(Item);
        }

        [HttpGet]
        [Route("private1")]
        public IHttpActionResult GetItem(string term = "")
        {
            var CustomListItem = Item.Find(item =>
                    item.Term.Equals(term, StringComparison.InvariantCultureIgnoreCase));

            if (CustomListItem == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(CustomListItem);
            }
        }
        
    }
}
