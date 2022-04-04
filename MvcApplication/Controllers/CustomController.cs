using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcApplication.Model;

namespace MvcApplication.Controllers
{

    //[Route("api/[controller]")]
    //[Authorize]
    //public class CustomController : ApiController
    //{
    //    private static List<CustomListItem> Item = new List<CustomListItem> {
    //        new CustomListItem
    //        {
    //            Term= "Access Token",
    //            Definition = "A credential that can be used by an application to access an API. It informs the API that the bearer of the token has been authorized to access the API and perform specific actions specified by the scope that has been granted."
    //        },
    //        new CustomListItem
    //        {
    //            Term= "JWT",
    //            Definition = "An open, industry standard RFC 7519 method for representing claims securely between two parties. "
    //        },
    //        new CustomListItem
    //        {
    //            Term= "OpenID",
    //            Definition = "An open standard for authentication that allows applications to verify users are who they say they are without needing to collect, store, and therefore become liable for a user’s login information."
    //        }
    //    };

    //    [HttpGet]
    //    [Authorize]
    //    public IHttpActionResult <List<CustomListItem>> Get()
    //    {
    //        return Ok(Item);
    //    }

    //    [HttpGet]
    //    [Route("{term}")]
    //    [Authorize]
    //    public IHttpActionResult  Get(string term)
    //    {
    //        var CustomListItem = Item.Find(item =>
    //                item.Term.Equals(term, StringComparison.InvariantCultureIgnoreCase));

    //        if (CustomListItem == null)
    //        {
    //            return NotFound();
    //        }
    //        else
    //        {
    //            return Ok(CustomListItem);
    //        }
    //    }

    //    [HttpPost]
    //    [Authorize]
    //    public IHttpActionResult Post(CustomListItem customListItem)
    //    {
    //        var existingCustomListItem = Item.Find(item =>
    //                item.Term.Equals(CustomListItem.Term, StringComparison.InvariantCultureIgnoreCase));

    //        if (existingCustomListItem != null)
    //        {
    //            return Conflict("Cannot create the term because it already exists.");
    //        }
    //        else
    //        {
    //            Item.Add(customListItem);
    //            var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeUriString(customListItem.Term));
    //            return Created(resourceUrl, customListItem);
    //        }
    //    }

    //    [HttpPut]
    //    [Authorize]
    //    public IHttpActionResult Put(CustomListItem customListItem)
    //    {
    //        var existingCustomListItem = Item.Find(item =>
    //        item.Term.Equals(CustomListItem.Term, StringComparison.InvariantCultureIgnoreCase));

    //        if (existingCustomListItem == null)
    //        {
    //            return NotFound("Cannot update a nont existing term.");
    //        }
    //        else
    //        {
    //            existingCustomListItem.Definition = CustomListItem.Definition;
    //            return Ok();
    //        }
    //    }

    //    [HttpDelete]
    //    [Route("{term}")]
    //    [Authorize]
    //    public IHttpActionResult Delete(string term)
    //    {
    //        var CustomListItem = Item.Find(item =>
    //               item.Term.Equals(term, StringComparison.InvariantCultureIgnoreCase));

    //        if (CustomListItem == null)
    //        {
    //            return NotFound();
    //        }
    //        else
    //        {
    //            Item.Remove(CustomListItem);
    //            return NoContent();
    //        }
    //    }

    //[HttpDelete]
    //[Route("{term}")]
    //[Authorize]
    //public IHttpActionResult Delete(string term)
    //{
    //    var CustomListItem = Item.Find(item =>
    //           item.Term.Equals(term, StringComparison.InvariantCultureIgnoreCase));

    //    if (CustomListItem == null)
    //    {
    //        return NotFound();
    //    }
    //    else
    //    {
    //        Item.Remove(CustomListItem);
    //        return Ok();
    //    }
    //}


    //}
}
