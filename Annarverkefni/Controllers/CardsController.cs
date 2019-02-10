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
    [RoutePrefix("api/cards")]
    public class CardsController : ApiController
    {
        CardQuery q;
        public CardsController()
        {
            q = new CardQuery();
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetActiveCards(HttpRequestMessage req)
        {
            var a = q.GetActiveCards();
            return req.CreateResponse(HttpStatusCode.OK, a);
        }

        [Route("all")]
        [HttpGet]
        public HttpResponseMessage GetAllCards(HttpRequestMessage req)
        {
            var a = q.GetAllCards();
            return req.CreateResponse(HttpStatusCode.OK, a);
        }

        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage GetCardById(HttpRequestMessage req, int id)
        {
            var a = q.GetCardById(id);
            return req.CreateResponse(HttpStatusCode.OK, a);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage AddNewCard(HttpRequestMessage req, Card c)
        {
            var a = q.AddNewCard(c);
            return req.CreateResponse(HttpStatusCode.OK, a);
        }
        [Route("{id}")]
        [HttpPut]
        public HttpResponseMessage EditCard(HttpRequestMessage req, Card c, int id)
        {
            var a = q.EditCard(id, c);
            return req.CreateResponse(HttpStatusCode.OK, a);
        }
        [Route("{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteCard(HttpRequestMessage req, int id)
        {
            q.DeleteCard(id);
            return req.CreateResponse(HttpStatusCode.OK);
        }

        [Route("deck/new/{name}")]
        [HttpPost]
        public HttpResponseMessage MakeNewDeck(HttpRequestMessage req, List<Card> cardlist, string name)
        {
            var id = User.Identity.GetUserId();
            var list = q.AddCardToDeck(cardlist, name, id);

            return req.CreateResponse(HttpStatusCode.OK, list);
        }

        [Route("deck/{deckname}")]
        [HttpGet]
        public HttpResponseMessage GetDeckByName(HttpRequestMessage req, string deckname)
        {
            var id = User.Identity.GetUserId();
            var deck = q.GetAllCardsInDeck(deckname, id);

            return req.CreateResponse(HttpStatusCode.OK, deck);
        }

        [Route("deck/all")]
        [HttpGet]
        public HttpResponseMessage GetListOfDecks(HttpRequestMessage req)
        {
            var id = User.Identity.GetUserId();
            List<string> list = q.GetAllUserDecks(id);

            return req.CreateResponse(HttpStatusCode.OK, list);
        }

    }
}
