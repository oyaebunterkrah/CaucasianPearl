﻿namespace CaucasianPearl.Core.DAL.Interface
{
    public interface IUrlFriendly : IOrdered
    {
        string ShortName { get; set; }
    }
}
