using MvcApplication.Model;
using MvcApplication.Support;
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

        // [HttpPost]
        //[Route("role")]
        //public IHttpActionResult Post1()
        // {
        //     var client = new RestClient("https://dev-0ztxkmi4.us.auth0.com/oauth/token");
        //     var request = new RestRequest(Method.POST);
        //     request.AddHeader("content-type", "application/json");
        //     request.AddHeader("authorization", "Bearer MGMT_API_ACCESS_TOKEN");
        //     request.AddHeader("cache-control", "no-cache");
        //     request.AddParameter("application/json", "{ \"roles\":\"rol_YhIgCKtt78su8kiZ\"}", ParameterType.RequestBody);
        //     IRestResponse response = client.Execute(request);
        //     return Json(response.Content);
        // }
        [HttpPost]
        [Route("role")]
        public IHttpActionResult Post1()
        {
            var client = new RestClient("https://dev-0ztxkmi4.us.auth0.com/api/v2/roles/rol_z4jCkxK0LOBqNxkN/permissions");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IndjVEVvSGpGU3ByS3BVMzM2QURmYyJ9.eyJpc3MiOiJodHRwczovL2Rldi0wenR4a21pNC51cy5hdXRoMC5jb20vIiwic3ViIjoiYXV0aDB8NjI0MmM1MGFiMDM0NDAwMDY5MmY1MTFlIiwiYXVkIjoiaHR0cHM6Ly9kZXYtMHp0eGttaTQudXMuYXV0aDAuY29tL2FwaS92Mi8iLCJpYXQiOjE2NDkzMzY1OTgsImV4cCI6MTY0OTQyMjk5OCwiYXpwIjoicTkyZFVtckdyWFpsUHVyb1N3YjRPWlpGWGxMM2lVbW8iLCJzY29wZSI6Im9mZmxpbmVfYWNjZXNzIn0.Jgoe9U318JEQsCaMBuwqW7vAzR5l9V_vEmJ7RFADiO8ltgmvOgtwt_CK_xwsDipHyM1SiMik4QxTIdW0Tsu4tKtfV_e_UI0E598CWx545_wuNv5433-MD71DvjNC_zmWvFMn-6e8-mKxqjBWIjn5JG5iNvti8mCw_gPt3zZOk35EQuH73CWZcyA3yV3bxLioKmW3X79NNsVzpCPVWosegWGlFoEqdPKZaAiVwkai3rZl-QaTRVD2-L11J55a-5M_nhZ4YqBa9VvLzBGTOx8m0-98MLXDdywzXuEC5RKTuJRmwoYFfUev6jtC-bP37wxG4lz0chRZ9dkNEmdIBsfZoA.eyJpc3MiOiJodHRwczovL2Rldi0wenR4a21pNC51cy5hdXRoMC5jb20vIiwic3ViIjoiYXV0aDB8NjI0MmM1MGFiMDM0NDAwMDY5MmY1MTFlIiwiYXVkIjoiaHR0cHM6Ly9kZXYtMHp0eGttaTQudXMuYXV0aDAuY29tL2FwaS92Mi8iLCJpYXQiOjE2NDkzMzE3MTksImV4cCI6MTY0OTQxODExOSwiYXpwIjoicTkyZFVtckdyWFpsUHVyb1N3YjRPWlpGWGxMM2lVbW8iLCJzY29wZSI6Im9mZmxpbmVfYWNjZXNzIn0.YRqbDqKLFvIAilU8iz2aNRUwmcBSWZxjaubPO3nSTUiJ5DIFdsI7Y_UcYKzf-WRXW6AqN5AcZfmjqQwWZgTP8UivbK1bACw6vWIKkK_W8LeVPaKLYnq56YrNVrozYSOCIJdJtxTSMb9yVbEeI1B_-8SjA8wfGH0E2kUpRomFyrGCtQlaCRliNfu32JShRLlyPajZYHMdQfhGYaVPWNQuqGKtuRSEwgk1pT5RjVaq33yAx_tOVQR1joRrwmK1TY7_rOTHqL7M8mYc-8lvC1FcxOXL6VTndSP-R-exKtZ-MHNak0jqqonVToDnZ3NA0x5BEQ5sGRFORIixx_IH591uDw");
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter("application/json", "{ \"permissions\": [ { \"resource_server_identifier\": \"https://api.surepayroll.com/api/v1/\", \"permission_name\": \"read:hello-resource\" } ] }", ParameterType.RequestBody);
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
        //[MyCustomAuthorization]
        [Authorize]
        public IHttpActionResult Get()
        {
            return Ok(Item);
        }

        [HttpGet]
        [Route("Get/Term")]
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

        [HttpGet]
        [Route("Unauthorized")]
        public IHttpActionResult Unauthorized(string term)
        {
            return Unauthorized();
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
