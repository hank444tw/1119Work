using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1119Work.Models
{
    public class JoinBook
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Introdution { get; set; }
        public string DeputyFileName { get; set; }
        public Nullable<int> MemberID { get; set; }

        public Nullable<int> BookID { get; set; }
        public Nullable<int> Page { get; set; }
        public string ImageName { get; set; }
    }
}