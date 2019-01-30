using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _1119Work
{
    public class RouteConfig
    {
        public static string GetRandomStringByGuid()  //使用Guid產生亂碼
        {
            var str = Guid.NewGuid().ToString().Replace("-", ""); //將"-"字號去掉
            return str;
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}"); //不要透過Route處理的網址

            /*---------萬用Route----------*/
            /*routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional } //如果網址為'/',便連到defaults設定的連結
            );*/
            /*----------------------------*/

            //最初執行時
            routes.MapRoute(
                name:"/",
                url:"",
                defaults:new { Controller = "Home", action = "Index" });

            //首頁
            routes.MapRoute(
                name:"HomePage",
                url:"HomePage/{Page}",
                defaults:new { Controller = "Home", action = "Index", Page = 1}, //若省略Page，則以預設值1查詢
                constraints:new {Page = @"\d+"}); //正規表示式，代表一個或多個的數字

            //會員名單
            routes.MapRoute(
                name:"ListMember",
                url:"ListMember",
                defaults:new {Controller = "Home",action = "ListMember"});

            //登入紀錄
            routes.MapRoute(
                name: "SigninRecord",
                url: "SigninRecord",
                defaults: new { Controller = "Home", action = "SigninRecord" });

            //新增會員(註冊)
            routes.MapRoute(
                name: "CreateMember",
                url: "CreateMember",
                defaults: new { controller = "Home", action = "CreateMember" });

            //修改會員
            routes.MapRoute(
                name:"EditMember",
                url:"ListMember/EditMember/{Rid}",
                defaults:new { controller = "Home", action = "Edit" }); //參數Rid必帶

            //刪除會員
            routes.MapRoute(
                name: "DeleteMember",
                url: "ListMember/DeleteMember/{Rid}",
                defaults: new { Controller = "Home", action = "Delete" });

            //登入
            routes.MapRoute(
                name: "Signin",
                url: "Signin",
                defaults: new { Controller = "Home", action = "Signin" });

            //登出
            routes.MapRoute(
                name: "SignOut",
                url: "SignOut",
                defaults: new { Controller = "Home",action = "SignOut"});

            //電子書閱讀
            routes.MapRoute(
                name: "Book",
                url: "Book/{BookName}",
                defaults: new { Controller = "Home", action = "Book" });

            //繪本清單
            routes.MapRoute(
                name: "ListBook",
                url: "ListBook",
                defaults: new { Controller = "Home", action = "ListBook" });

            //新增繪本
            routes.MapRoute(
                name: "CreateBook",
                url: "CreateBook",
                defaults: new {Controller = "Home",action = "CreateBook"});

            //繪本修改
            routes.MapRoute(
                name: "EditBook",
                url: "ListBook/EditBook/{Rid}",
                defaults: new { Controller = "Home", action = "EditBook" });

            //圖片左移
            routes.MapRoute(
                name: "DownInnerPage",
                url: "ListBook/EditBook/DownInnerPage/{Page}",
                defaults: new { Controller = "Home", action = "DownInnerPage" });

            //圖片右移
            routes.MapRoute(
                name: "UpInnerPage",
                url: "ListBook/EditBook/UpInnerPage/{Page}",
                defaults: new { Controller = "Home", action = "UpInnerPage" });

            //刪除內頁圖片
            routes.MapRoute(
                name: "DeleteInnerPage",
                url: "ListBook/EditBook/DeleteInnerPage/{Page}",
                defaults: new { Controller = "Home", action = "DeleteInnerPage" });

            //繪本刪除
            routes.MapRoute(
                name: "DeleteBook",
                url: "ListBook/DeleteBook/{Rid}",
                defaults: new { Controller = "Home", action = "DeleteBook" });
        }
    }
}
