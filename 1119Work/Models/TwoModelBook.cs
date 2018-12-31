using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1119Work.Models
{
    public class TwoModelBook
    {
        public Book Book { get; set; }
        public List<InnerPage> InnerPage { get; set; }
    }
}