//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Annarverkefni.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlayersDeck
    {
        public int Id { get; set; }
        public string PlayerId { get; set; }
        public int CardId { get; set; }
        public string DeckName { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Card Card { get; set; }
    }
}
