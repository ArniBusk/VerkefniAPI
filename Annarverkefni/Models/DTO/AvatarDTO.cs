using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Annarverkefni.Models.DTO
{
    public class AvatarDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Effect { get; set; }
        public int Hp { get; set; }
        public string Lore { get; set; }
        public string Phrase { get; set; }
        public int Cost { get; set; }
    }
}