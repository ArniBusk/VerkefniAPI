using Annarverkefni.Models.Entity;
using Annarverkefni.Queries;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Annarverkefni.Controllers
{
    [RoutePrefix("api/review")]
    public class ReviewController : ApiController
    {
        AvatarQuery q;
        public ReviewController()
        {
            q = new AvatarQuery();
        }

        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage GetReviews(HttpRequestMessage req, int id)
        {
            var a = q.GetReviewsById(id);
            return req.CreateResponse(HttpStatusCode.OK, a);
        }

        [Route("{id}/add")]
        [HttpPost]
        public HttpResponseMessage AddReview(HttpRequestMessage req, Review r, int id)
        {
            var user = User.Identity.GetUserId();
            var a = q.AddNewReview(id, r.Asessment, user);
            return req.CreateResponse(HttpStatusCode.OK, a);

        }
        [Route("{id}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteReview(HttpRequestMessage req, int id)
        {
            q.DeleteReview(id);
            return req.CreateResponse(HttpStatusCode.OK);
        }

        [Route("user")]
        [HttpGet]
        public HttpResponseMessage GetAllOfUsersReview(HttpRequestMessage req)
        {
            var a = User.Identity.GetUserId();
            var list = q.GetReviewByUserId(a);

            return req.CreateResponse(HttpStatusCode.OK, list);
        }

        [Route("all")]
        [HttpGet]
        public HttpResponseMessage GetAllReviews(HttpRequestMessage req)
        {
            var a = q.GetAllReviews();
            return req.CreateResponse(HttpStatusCode.OK, a);
        }

        [Route("{id}/edit")]
        [HttpPut]
        public HttpResponseMessage EditReview(HttpRequestMessage req, String ass , int id)
        {
            var a = q.PutReview(ass, id);
            return req.CreateResponse(HttpStatusCode.OK, a);
        }
    }
}
