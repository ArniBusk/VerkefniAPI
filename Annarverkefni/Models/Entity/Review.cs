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
    
    public partial class Review
    {
        public int Id { get; set; }
        public int AvatarId { get; set; }
        public string Asessment { get; set; }
        public System.DateTime Time { get; set; }
        public string UserId { get; set; }
    
        public virtual Avatar Avatar { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
