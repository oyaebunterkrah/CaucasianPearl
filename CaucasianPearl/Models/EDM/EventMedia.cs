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
    
    public partial class EventMedia
    {
        public int ID { get; set; }
        public Nullable<int> EventId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public Nullable<byte> MediaType { get; set; }
    }
}
