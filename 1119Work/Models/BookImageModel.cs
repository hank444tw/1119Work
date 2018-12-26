using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1119Work.Models
{
    public class BookImageModel
    {
        /// 加這個，當BookImageModel new出來時，存取PhotoFileNames才不會發生NULL例外
        private List<string> _PhotoFileNames = new List<string>();

        /// 多張大頭照的檔案名稱
        public List<string> PhotoFileNames { get { return this._PhotoFileNames; } set { this._PhotoFileNames = value; } }
    }
}