using MvcApplication.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApplication.Controllers
{
    [RoutePrefix("api/default")]
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
        public IHttpActionResult Post()
        {
            var client = new RestClient("https://dev-0ztxkmi4.us.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"bQd75pd6BE0CAsonrvR4K3iAqlVXmYjo\",\"client_secret\":\"0C7Ck0h2t5VniuCN9vrvPhZyATrYcwGW9bY3V0KEjOuTu6a8ugTKCJ1PPcR3q4uD\",\"audience\":\"https://dev-0ztxkmi4.us.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return Json(response.Content);
        }

        //[HttpGet]
        //[Route("private")]
        ////[Authorize]
        //public IHttpActionResult Get1()
        //{
        //    var client = new RestClient("http://path_to_your_api/");
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IndjVEVvSGpGU3ByS3BVMzM2QURmYyJ9.eyJpc3MiOiJodHRwczovL2Rldi0wenR4a21pNC51cy5hdXRoMC5jb20vIiwic3ViIjoiYlFkNzVwZDZCRTBDQXNvbnJ2UjRLM2lBcWxWWG1Zam9AY2xpZW50cyIsImF1ZCI6Imh0dHBzOi8vZGV2LTB6dHhrbWk0LnVzLmF1dGgwLmNvbS9hcGkvdjIvIiwiaWF0IjoxNjQ4NTcxNjQ4LCJleHAiOjE2NDg2NTgwNDgsImF6cCI6ImJRZDc1cGQ2QkUwQ0Fzb25ydlI0SzNpQXFsVlhtWWpvIiwiZ3R5IjoiY2xpZW50LWNyZWRlbnRpYWxzIn0.JN6zYbBTo8Rsp41KIuu4X04Scw9kY1ohWHD_C4SzaAejkE1x1EZ_Y_wBCZm2A7AA0AKA2SahQUGPLXZ77PqxBiIN-DqXKsIUMjo2qaiieGjHd452NLtyRrHBzd0ApV7j9EH5z5kimPkoEGF1kHq-NB4sFpyucVNPwy9W-IcodaAV1FBGVd_d20hxHAT2iN_EsN2rWpQ69xjp6XJjBeejaF47bruJ4cUgoY1bBMdRG6p7aOYnk7x30EIUClaRNqTVeKG2yDvZ8vCVDWY6jwq7_xS7eKAZa2dQfcAYCiQ-wEZnL9f2GlZJGi81WJpdUC_WZYl9CBv9Q1dO0f6-5C5VSw");
        //    IRestResponse response = client.Execute(request);
        //    return Json(response.Content);
        //}

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
        [Route("Get")]
        [Authorize]
        public IHttpActionResult Get(string term)
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

        [HttpPost]
        [Route("Item")]
        [Authorize]
        public IHttpActionResult Post(CustomListItem customListItem)
        {
            Item.Add(customListItem);
            return Ok("Record added to the list.");
        }


        [HttpPut]
        [Authorize]
        [Route("Item")]
        public IHttpActionResult Put(CustomListItem customListItem)
        {
            try
            {
                foreach (var item in Item)
                {
                    if(item.Term == customListItem.Term)
                    {
                        item.Definition = customListItem.Definition;
                        item.Term = customListItem.Term;
                    }
                }
                return Ok("Record updated.");
            }
            catch(Exception ex)
            {
                return Ok("Item not found in the list.");
            }
            
        }

       

    }
}
