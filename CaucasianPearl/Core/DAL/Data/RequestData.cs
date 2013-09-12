using System;
using System.Collections.Generic;
using System.Linq;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Helpers.EntityHelpers;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.DAL.Data
{
    public class RequestItem
    {
        public RequestItem()
        {

        }

        public RequestItem(Request request)
        {
            ID = request.ID;
        }

        public int ID { get; set; }
    }
}