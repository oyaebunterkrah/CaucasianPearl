//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CaucasianPearl.Models.EDM
{
    using System;
    using System.Collections.Generic;
    
    public partial class Feedback
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Comment { get; set; }
        public string Suggestion { get; set; }
        public System.DateTime FeedbackDateTime { get; set; }
        public bool IsApproved { get; set; }
    }
}