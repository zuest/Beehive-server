using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCamp1.Models
{
    public class TaxiProvider
    {
        public int Id;
        public String Name;
        public int Quote;

        public TaxiProvider()
        {
            Id = Quote = 0;
            Name = "";
        }
    }
}