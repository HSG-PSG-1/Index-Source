using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Data.Linq.SqlClient;
using HSG.DAL;
using HSG.Helper;
using HSG.Models;

namespace HSG.Services
{
    public class DefaultDBService : _ServiceBase
    {
        /*public static DefaultPO GetPO(int userID)
        {
            return new DefaultPO()
            {
                CustID = _Session.NewCustOrgId,
                AssignTo = Config.DefaultPOAssigneeId,
                ShipToLocID = 0,
                OrderStatusID = Config.DefaultPOStatusId,
                BrandID = 0
            };
        }*/
    }
}
