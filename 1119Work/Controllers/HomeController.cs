using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _1119Work.Models;
using PagedList;
using System.Net; //取得IP要用到的
using System.IO;  //儲存、刪除本機資料要用到

namespace _1119Work.Controllers
{
    public class HomeController : Controller
    {
        DB40441124Entities4 db = new DB40441124Entities4();
        public int pagesize = 6; //要顯示的資料數量
        string fileName;

        public ActionResult Index(int Page = 1)
        {
            int CurrentPage = Page < 1 ? 1 : Page;
            var book = db.Book.ToList();
            var Result = book.ToPagedList(CurrentPage, pagesize);
            return View(Result);
        }

        public ActionResult Book(int Id)
        {
            var ChooseBook = db.Book.Where(m => m.Id == Id).FirstOrDefault();
            return View(ChooseBook);
        }

        public ActionResult SigninRecord(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var logdata = (from d1 in db.MemLog
                           join d2 in db.Member
                           on d1.Mem_id equals d2.Mem_id
                           select new
                           {
                               d1.Mem_id,
                               d1.IPAdress,
                               d1.Log_date,
                               d1.Log_id,
                               d2.Mem_name,
                               d2.Mem_password
                           }).OrderByDescending(m => m.Log_date).ToList();
            List<JoinLog> joinlog = new List<JoinLog>();
            foreach(var item in logdata)
            {
                joinlog.Add(new JoinLog
                {
                    Mem_id = item.Mem_id,
                    Mem_name = item.Mem_name,
                    IPAdress = item.IPAdress,
                    Log_date = item.Log_date
                });
            }
            var Result = joinlog.ToPagedList(currentPage, pagesize);
            return View("SigninRecord",Result);
        }

        public ActionResult ListMember()
        {
            var member = db.Member.ToList();
            return View(member);
        }

        public ActionResult ListBook()
        {
            var book = db.Book.ToList();
            return View(book);
        }

        public ActionResult Edit(int id) //修改會員資料
        {
            var todo = db.Member.Where(m => m.Id == id).FirstOrDefault();
            return View(todo);
        }

        [HttpPost]
        public ActionResult Edit(int id,string Mem_password, string Mem_name)
        {
            var todo = db.Member.Where(m => m.Id == id).FirstOrDefault();
            todo.Mem_password = Mem_password;
            todo.Mem_name = Mem_name;
            db.SaveChanges();
            return RedirectToAction("ListMember");
        }

        public ActionResult Delete(int id) //刪除會員資料
        {
            var todo = db.Member.Where(m => m.Id == id).FirstOrDefault();
            db.Member.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("ListMember");
        }

        public ActionResult DeleteBook(int id)
        {
            var book = db.Book.Where(m => m.Id == id).FirstOrDefault();

            String BookID = id.ToString();
            var FolderPath = Server.MapPath("~/Image/" + BookID); //圖片資料夾實體位置
            var path = Path.Combine(FolderPath, "0 " + book.DeputyFileName); //圖片檔案位置
            System.IO.File.Delete(path); //刪除圖片檔案
            Directory.Delete(FolderPath); //刪除圖片資料夾

            db.Book.Remove(book);
            db.SaveChanges();
            return RedirectToAction("ListBook");
        }

        public ActionResult EditBook(int id)
        {
            var todo = db.Book.Where(m => m.Id == id).FirstOrDefault();
            return View(todo);
        }

        [HttpPost]
        public ActionResult EditBook(int id,string BookName,string Author,string Introdution, HttpPostedFileBase file, FormCollection form)
        {
            var book = db.Book.Where(m => m.Id == id).FirstOrDefault();

            //if (file.ContentLength > 0) //判斷檔案大小
            if (file != null) //判斷有無再上傳圖片
            {
                String BookID = id.ToString();
                var FolderPath = Server.MapPath("~/Image/" + BookID); //圖片資料夾實體位置
                var path = Path.Combine(FolderPath, "0 " + book.DeputyFileName); //原圖片檔案位置
                System.IO.File.Delete(path); //刪除原圖

                fileName = Path.GetExtension(file.FileName); //取得上傳圖片副檔名
                var path2 = Path.Combine(FolderPath, "0 " + fileName);
                file.SaveAs(path2);
                book.DeputyFileName = fileName; //資料表副檔名欄位更改
            }

            book.BookName = BookName;  
            book.Author = Author;
            book.Introdution = Introdution;
            db.SaveChanges();

            foreach (string item in Request.Files)
            {
                HttpPostedFileBase file5 = Request.Files[item] as HttpPostedFileBase;
                if (file5 == null || file5.ContentLength == 0)
                    continue;
                //判断Upload文件夹是否存在，不存在就创建
                string path5 = Server.MapPath("~/Upload");
                if (!System.IO.Directory.Exists(path5))
                {
                    System.IO.Directory.CreateDirectory(path5);
                }
                path5 = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                //获取上传的文件名   
                string fileName = file5.FileName;
                //上传      
                file5.SaveAs(Path.Combine(path5, fileName));
            }
            //return Content("<script>alert('上传文件成功');window.history.back();</script>");

            return RedirectToAction("ListBook");
        }

        [HttpPost]
        public ActionResult Test(FormCollection form)
        {
            foreach (string item in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                if (file == null || file.ContentLength == 0)
                    continue;
                //判断Upload文件夹是否存在，不存在就创建
                string path = Server.MapPath("..//Upload");
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                path = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                //获取上传的文件名   
                string fileName = file.FileName;
                //上传      
                file.SaveAs(Path.Combine(path, fileName));
            }
            return Content("<script>alert('上传文件成功');window.history.back();</script>");
        }

        public ActionResult CreateMember()
        {
            return View();
        }

        /*[HttpPost] 
        public ActionResult CreateMember(Member member) 也行
        {
            db.Member.Add(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

        [HttpPost]
        public ActionResult CreateMember(string Mem_id,string Mem_password,string Mem_name,HttpPostedFileBase file)
        {
            var test_id = db.Member.Where(m => m.Mem_id == Mem_id).FirstOrDefault();
            if(test_id != null) //判斷帳號是否已使用過
            {
                ViewBag.Message = "帳號已有人使用!";
                return View();
            }

            Member member = new Member();

            if (file != null) //判斷有無上傳大頭照
            {
                fileName = Path.GetExtension(file.FileName); //取得副檔名
                member.DeputyFileName = fileName; //副檔名存進資料表
            }
            member.Mem_id = Mem_id;
            member.Mem_password = Mem_password;
            member.Mem_name = Mem_name;
            db.Member.Add(member);
            db.SaveChanges();

            var NowData = db.Member.Where(m => m.Mem_id == Mem_id).FirstOrDefault();
            String MemberID = NowData.Id.ToString();

            if (file != null)
            {
                var FolderPath = Server.MapPath("~/ImageMember/" + MemberID); //資料夾的路徑及檔名
                if (!Directory.Exists(FolderPath)) //判斷此資料夾是否存在
                {
                    Directory.CreateDirectory(FolderPath); //創建資料夾
                }
                var path = Path.Combine(FolderPath, "0 " + fileName); //要儲存圖片的路徑及檔名
                file.SaveAs(path);
            }
            return RedirectToAction("ListMember");
        }

        public ActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBook(string BookName, string Author, string Introdution, HttpPostedFileBase file)
        {
            var test_BookName = db.Book.Where(m => m.BookName == BookName).FirstOrDefault();
            if (test_BookName != null)
            {
                ViewBag.Message = "此書已有人上傳過!!";
                return View();
            }
            if (file.ContentLength > 0)
            {
                fileName = Path.GetExtension(file.FileName); //取得副檔名
            }
            Book book = new Book();
            book.BookName = BookName;
            book.Author = Author;
            book.Introdution = Introdution;
            book.DeputyFileName = fileName;
            db.Book.Add(book);
            db.SaveChanges();

            var NowData = db.Book.Where(m => m.BookName == BookName).FirstOrDefault();
            String BookID = NowData.Id.ToString();

            if (file.ContentLength > 0)
            {
                var FolderPath = Server.MapPath("~/Image/" + BookID);
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }
                //var path = Path.Combine(HttpContext.Server.MapPath(FolderPath), BookID + fileName);
                var path = Path.Combine(FolderPath, "0 " + fileName);
                file.SaveAs(path);
            }
            return RedirectToAction("ListBook");
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(HttpContext.Server.MapPath("~/Image"), fileName);
                file.SaveAs(path);
            }
            return RedirectToAction("Upload");
        }

        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin(string Mem_id,string Mem_password)
        {
            var member = db.Member.Where(m => m.Mem_id == Mem_id && m.Mem_password == Mem_password).FirstOrDefault();
            if(member == null)
            {
                ViewBag.Message = "帳號或密碼錯誤";
                return View();
            }
            else
            {
                Session["Welcome"] = member.Mem_name + " 你好!";
                Session["MemberID"] = member.Id; //紀錄該登入會員ID

                string ip = GetClientIP();

                string date = DateTime.Now.ToString();

                DateTime myDate = DateTime.Now;
                string myDateString = myDate.ToString("yyyy-MM-dd HH:mm:ss");

                MemLog memLog = new MemLog();
                memLog.Mem_id = member.Mem_id;
                memLog.IPAdress = ip;
                memLog.Log_date = myDateString;
                

                db.MemLog.Add(memLog);
                db.SaveChanges();
            }
            return RedirectToAction("SigninRecord");
        }

        public ActionResult SignOut()
        {
            Session["MemberID"] = null;
            return RedirectToAction("Index");
        }


        /// <summary>
        /// 取得正確的Client端IP
        /// </summary>
        /// <returns></returns>
        protected string GetClientIP()
        {
            //------取得ip------
            String strHostName = Dns.GetHostName();
            IPHostEntry iphostentry = Dns.GetHostByName(strHostName);   //取得本機的 IpHostEntry 類別實體
            string ip = iphostentry.AddressList[0].ToString();
            /* 網路找的 不知道他為啥要用foreach
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                Console.WriteLine(ipaddress.ToString());//使用了兩種方式都可以讀取出IP位置
                TempData["1"]= ipaddress.ToString();
                ip = ip + ipaddress.ToString();
            }
            */
            //------取得ip------
            return (ip);
        }

    }
}
