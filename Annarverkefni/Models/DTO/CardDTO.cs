using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Annarverkefni.Models.DTO
{
    public class CardDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Dps { get; set; }
        public int Mana { get; set; }
        public string Info { get; set; }
        public int CardType { get; set; }
    }
}