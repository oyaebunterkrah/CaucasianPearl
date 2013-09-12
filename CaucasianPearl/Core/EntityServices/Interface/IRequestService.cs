using System;
using System.Collections.Generic;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices.Interface
{
    public interface IRequestService<T> : IOrderedService<T> where T : class, IOrdered, new()
    {
    }
}