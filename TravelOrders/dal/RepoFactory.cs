using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOrders.dal
{
    public static class RepoFactory
    {
        public static IRepository GetRepository() => new SqlRepository();
    }
}