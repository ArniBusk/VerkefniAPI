using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Annarverkefni.Models.DTO;
using Annarverkefni.Models.Entity;

namespace Annarverkefni.Queries
{
    public class AvatarQuery
    {
        CardsDBEntities _db;

        public AvatarQuery()
        {
            _db = new CardsDBEntities();
        }
        // Get All Avatars
        public List<AvatarDTO> GetAllAvatars()
        {
            var a = (from x in _db.Avatars
                     select new AvatarDTO() {
                         Id = x.Id,
                         Cost = x.Cost,
                         Effect = x.Effect,
                         Hp = x.Hp,
                         Lore = x.Lore,
                         Name = x.Name,
                         Phrase = x.Phrase
                     }).ToList();

           
            return a;
        }
        // Get Avatar By ID
        public AvatarDTO GetAvatarById(int id)
        {
            var a = (from x in _db.Avatars
                     where x.Id == id
                     select new AvatarDTO() {
                         Id = x.Id,
                         Cost = x.Cost,
                         Effect = x.Effect,
                         Hp = x.Hp,
                         Lore = x.Lore,
                         Name = x.Name,
                         Phrase = x.Phrase
                     }).FirstOrDefault();
            return a;
        }
        // Edit Existing Avatar
        public AvatarDTO EditAvatar(int id, Avatar av)
        {
            var a = (from x in _db.Avatars
                     where x.Id == id
                     select x).FirstOrDefault();
            a.Hp = av.Hp;
            a.Name = av.Name;
            a.Phrase = av.Phrase;
            a.Lore = av.Lore;
            a.Cost = av.Cost;
            a.Effect = av.Effect;
            _db.SaveChanges();
            return GetAvatarById(id);

        }
        // Delete Existing Avatar
        public void DeleteAvatar(int id)
        {
            var a = (from x in _db.Avatars
                     where x.Id == id
                     select x).FirstOrDefault();
            _db.Avatars.Remove(a);
            _db.SaveChanges();
            
        }
        // Add New Avatar (Moderator Func)
        public AvatarDTO AddAvatar(Avatar a)
        {
            var av = new AvatarDTO()
            {
                Id = a.Id,
                Cost = a.Cost,
                Effect = a.Effect,
                Hp = a.Hp,
                Lore = a.Lore,
                Name = a.Name,
                Phrase = a.Phrase
            };

            _db.Avatars.Add(a);
            _db.SaveChanges();
            return av;
        }
        
        // Get All Avatars Owned by logged in user
        public List<AvatarDTO> GetOwnedAvatars(string s)
        {
            var a = (from x in _db.PlayersAvatars
                     where x.PlayerId == s
                     select (from z in _db.Avatars
                             where x.AvatarId == z.Id
                             select new AvatarDTO()
                             {
                                 Id = z.Id,
                                 Cost = z.Cost,
                                 Effect = z.Effect,
                                 Hp = z.Hp,
                                 Lore = z.Lore,
                                 Name = z.Name,
                                 Phrase = z.Phrase
                             }).FirstOrDefault()).ToList();
            return a;
        }
        
        public AvatarDTO AddToOwnedAvatar(int aid, string pid)
        {
            var temp = (from x in _db.Avatars
                        where x.Id == aid
                        select new AvatarDTO() {
                            Id = x.Id,
                            Cost = x.Cost,
                            Effect = x.Effect,
                            Hp = x.Hp,
                            Lore = x.Lore,
                            Name = x.Name,
                            Phrase = x.Phrase
                        }).FirstOrDefault();

            var av = new PlayersAvatar()
            {
                AvatarId = temp.Id,
                PlayerId = pid,
            };

            _db.PlayersAvatars.Add(av);
            _db.SaveChanges();
            return temp;
        } 

        public void DeleteFromOwnedAvatar(int id, string pid)
        {
            var a = (from x in _db.PlayersAvatars
                     where x.PlayerId == pid && x.AvatarId == id
                     select x).FirstOrDefault();
            

            _db.PlayersAvatars.Remove(a);
            _db.SaveChanges();
        }

        // get card Reveiws by AvatarId
        public List<ReviewDTO> GetReviewsById(int id)
        {
            //var a = db.Reveiw.Where(x => x.CardId == id).ToList();
            var a = (from x in _db.Reviews
                     where x.AvatarId == id
                     select new ReviewDTO()
                     {
                         UserId = (from u in _db.AspNetUsers
                                   where x.UserId == u.Id
                                   select u.UserName).FirstOrDefault(),
                         Asessment = x.Asessment,
                         AvatarId = x.AvatarId,
                         Id = x.Id,
                         Time = x.Time
                     }).ToList();
            return a;
        }
        // add Reveiw
        public Review AddNewReview(int id, string r,  string userid)
        {
            Review Rev = new Review()
            {
                AvatarId = id,
                Time = DateTime.Now,
                Asessment = r,
                UserId = userid
            };

            _db.Reviews.Add(Rev);
            _db.SaveChanges();

            return Rev;

        }
        // Delete Review (For mother russia)(Soviet Function)
        public void DeleteReview(int i)
        {
            var a = (from r in _db.Reviews
                     where r.Id == i
                     select r).FirstOrDefault();
           _db.Reviews.Remove(a);
            _db.SaveChanges();
        }

        public List<ReviewDTO> GetReviewByUserId(string uid)
        {
            var a = (from x in _db.Reviews
                     where x.UserId == uid
                     select new ReviewDTO()
                     {
                         Id = x.Id,
                         Asessment = x.Asessment,
                         AvatarId = x.AvatarId,
                         Time = x.Time,
                         UserId = (from i in _db.AspNetUsers
                                   where i.Id == uid
                                   select i.UserName).FirstOrDefault()
                     }).ToList();

            return a;
        }

        public string GetUsernameOfReviewPoster(string uid)
        {
            var a = (from x in _db.AspNetUsers
                     where x.Id == uid
                     select x.UserName).FirstOrDefault();
            return a;
        }

        public List<ReviewDTO> GetAllReviews()
        {
            var a = (from x in _db.Reviews
                     select new ReviewDTO
                     {
                         Asessment = x.Asessment,
                         AvatarId = x.AvatarId,
                         Id = x.Id,
                         Time = x.Time,
                         UserId = (from y in _db.AspNetUsers
                                   where x.UserId == y.Id
                                   select y.UserName).FirstOrDefault()
                     }).ToList();

            return a;
        }

        public ReviewDTO PutReview(string ass, int id)
        {
            var a = (from x in _db.Reviews
                     where x.Id == id
                     select x).FirstOrDefault();

            a.Asessment = ass;
            _db.SaveChanges();

            var b = new ReviewDTO()
            {
                Asessment = a.Asessment,
                AvatarId = a.AvatarId,
                Id = a.Id,
                Time = a.Time,
                UserId = a.UserId
            };

            return b;
        }

        public void GiveAvatars(List<int> idlist, string username)
        {
            var id = (from x in _db.AspNetUsers
                      where x.UserName == username
                      select x.Id).FirstOrDefault();

            foreach (int i in idlist)
            {
                var a = new PlayersAvatar()
                {
                    AvatarId = i,
                    PlayerId = id
                };
                _db.PlayersAvatars.Add(a);
            }

            _db.SaveChanges();
        }
       
    }


}