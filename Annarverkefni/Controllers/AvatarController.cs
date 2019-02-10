using Annarverkefni.Models.Entity;
using Annarverkefni.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace Annarverkefni.Controllers
{
    [RoutePrefix("api/avatar")]
    public class AvatarController : ApiController
    {
        AvatarQuery q;
        public AvatarController()
        {
            q = new AvatarQuery();
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetAllAvatars(HttpRequestMessage req)
        {
            var a = q.GetAllAvatars();
            
            return req.CreateResponse(HttpStatusCode.OK, a);
        }

        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage GetAvatarById(HttpRequestMessage req, int id)
        {
            var a = q.GetAvatarById(id);
            return req.CreateResponse(HttpStatusCode.OK, a);
        }

        [Route("{id}")]
        [HttpPut]
        public HttpResponseMessage EditAvatar(HttpRequestMessage req, Avatar av , int id)
        {
            var a = q.EditAvatar(id, av);
            return req.CreateResponse(HttpStatusCode.OK, a);
        }

        [Route("{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteAvatar(HttpRequestMessage req, int id)
        {
            q.DeleteAvatar(id);
            return req.CreateResponse(HttpStatusCode.OK);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage AddAvatar(HttpRequestMessage req, Avatar av)
        {
            var a = q.AddAvatar(av);
            return req.CreateResponse(HttpStatusCode.OK, a);
        }

        
        [Route("owned")]
        [HttpGet]
        public HttpResponseMessage GetOwnedAvatars(HttpRequestMessage req)
        {
            var guid = User.Identity.GetUserId();
            var a = q.GetOwnedAvatars(guid);
            return req.CreateResponse(HttpStatusCode.OK, a);

        }
        
        [Route("buy/{aid}")]
        [HttpPost]
        public HttpResponseMessage BuyAvatar(HttpRequestMessage req, int aid)
        {
            var uid = User.Identity.GetUserId();
            var stoff = q.AddToOwnedAvatar(aid, uid);
            return req.CreateResponse(HttpStatusCode.OK, stoff);
        } 

        [Route("owned/{id}")]
        [HttpDelete]
        public HttpResponseMessage SellAvatar(HttpRequestMessage req, int id)
        {
            var a = User.Identity.GetUserId();
            q.DeleteFromOwnedAvatar(id, a);

            return req.CreateResponse(HttpStatusCode.OK);
        }

        [Route("getusername/{id}")]
        [HttpGet]
        public HttpResponseMessage GetUsername(HttpRequestMessage req, string id)
        {
            var a = q.GetUsernameOfReviewPoster(id);

            return req.CreateResponse(HttpStatusCode.OK, a);
        }

        [Route("give")]
        [HttpPost]
        public HttpResponseMessage GivePlayerAvatars(HttpRequestMessage req, List<int> idlist, string username)
        {
            q.GiveAvatars(idlist, username);
            return req.CreateResponse(HttpStatusCode.OK);
        }


    } 
}
