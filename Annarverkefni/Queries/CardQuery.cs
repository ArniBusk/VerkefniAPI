using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Annarverkefni.Models.Entity;
using Annarverkefni.Models.DTO;

namespace Annarverkefni.Queries
{

    public class CardQuery
    {
        CardsDBEntities db;

        public CardQuery()
        {
            db = new CardsDBEntities();
        }

        // get list of All cards Active and Inactive
        public List<CardDTO> GetAllCards()
        {
            var a = (from x in db.Cards
                     select new CardDTO() {
                         Id = x.Id,
                         Info = x.Info,
                         CardType = x.CardType,
                         Dps = x.Dps,
                         Hp = x.Hp,
                         Mana = x.Mana,
                         Name = x.Name
                     }).ToList();
            return a;
        }
        //get list of active cards
        public List<CardDTO> GetActiveCards()
        {
            var a = (from x in db.Cards
                     where x.Active == true
                     select new CardDTO()
                     {
                         Id = x.Id,
                         Hp = x.Hp,
                         Info = x.Info,
                         Dps = x.Dps,
                         Mana = x.Mana,
                         Name = x.Name,
                         CardType = x.CardType 
                     }).ToList();
            return a;
        }
        // get card by ID
        public CardDTO GetCardById(int id)
        {
            var a = (from x in db.Cards
                     where x.Id == id
                     select new CardDTO()
                     {
                         Id = x.Id,
                         Hp = x.Hp,
                         Info = x.Info,
                         Dps = x.Dps,
                         Mana = x.Mana,
                         Name = x.Name
                     }).FirstOrDefault();
            return a;
        }
       
        // Add New Card (Moderator Function)
        public CardDTO AddNewCard(Card c)
        {
            var a = new CardDTO()
            {
                Id = c.Id,
                Info = c.Info,
                CardType = c.CardType,
                Dps = c.Dps,
                Hp = c.Hp,
                Mana = c.Mana,
                Name = c.Name
            };

            db.Cards.Add(c);
            db.SaveChanges();
            return a;
        }
        // Edit Card (Moderator Function)
        public CardDTO EditCard(int id, Card c)
        {
            var a = (from car in db.Cards
                     where car.Id == id
                     select car).FirstOrDefault();
            a.Id = id;
            a.Active = c.Active;
            a.CardType = c.CardType;
            a.Dps = c.Dps;
            a.Hp = c.Hp;
            a.Info = c.Info;
            a.Mana = c.Mana;
            a.Name = c.Name;
            db.SaveChanges();
            return GetCardById(id);
        }
        // Delete Card (Moderator Function)
        public void DeleteCard(int i)
        {
            var a = (from c in db.Cards
                     where c.Id == i
                     select c).FirstOrDefault();
            db.Cards.Remove(a);
            db.SaveChanges();
        }
        

        public  List<PlayersDeck> AddCardToDeck(List<Card> c, string s, string id)
        {
            var list = new List<PlayersDeck>();
            foreach (var card in c)
            {
                var a = new PlayersDeck() { PlayerId = id, CardId = card.Id, DeckName = s };
                list.Add(a);
                db.PlayersDecks.Add(a);
            }

            db.SaveChanges();
            return list;
        }

        public List<CardDTO> GetAllCardsInDeck(string name, string id)
        {
            var a = (from x in db.PlayersDecks
                     where x.DeckName == name && x.PlayerId == id
                     select (from c in db.Cards
                             where c.Id == x.CardId
                             select new CardDTO()
                             {
                                 Id = c.Id,
                                 Info = c.Info,
                                 CardType = c.CardType,
                                 Dps = c.Dps,
                                 Hp = c.Hp,
                                 Mana = c.Mana,
                                 Name = c.Name
                             }).FirstOrDefault()).ToList();

            return a;
        }

         public List<string> GetAllUserDecks(string id)
        {
            var decklist = (from x in db.PlayersDecks
                            where x.PlayerId == id
                            select x.DeckName).ToList();
            var uniques = decklist.Distinct().ToList();
            return uniques;
        }

        
        

    }
}