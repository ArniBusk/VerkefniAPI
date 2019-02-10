using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Annarverkefni.Models.DTO
{
    public class ReviewDTO
    {
        
        public int Id { get; set; }
        public int AvatarId { get; set; }
        public string Asessment { get; set; }
        public System.DateTime Time { get; set; }
        public string UserId { get; set; }
    }
}