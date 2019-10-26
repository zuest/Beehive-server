using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCamp1.Models
{
    public class Trip
    {
        public int PassengerId;
        public int DriverId;
        public int PickupGate;
        public int CompanyId;
        public String DriverName;
        public String DriverQID;
        public string PlateNum;

        public Trip()
        {
            PassengerId = DriverId = PickupGate = CompanyId = 0;
            DriverName = DriverQID = PlateNum = "";
        }

    }
}