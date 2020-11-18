# 電子書線上閱讀系統
> _大四上_友鋒課期末作業_   

* 功能說明
  1. 電子書線上閱讀。
  2. 會員系統。
  3. 電子書圖片CRUD。
  4. 電子書圖片順序更動管理。
 
* 使用技術
  1. ASP.NET MVC
  2. C#
  3. js
  4. Entity Framework
  5. LINQ
  
* 程式架構
  |        | 說明 |程式 |
  |------- |:-----|:------:|
  | **前端**   |  1. 介面主要使用`bootstrap`進行設計，電子書閱讀頁面則是老師提供之網頁樣板。</br>2. 使用`js`讓在管理電子書圖片時，可以一直新增圖片，並且頁數即時更新。</br>3. 以`Razor`語法將後端傳回之資訊，動態呈現於網頁。 |  [程式碼](https://github.com/hank444tw/1119Work/tree/master/1119Work/Views) |
  | **後端**   |  1. 會員系統(註冊、登入、登出、會員管理)。 </br> 2. 電子書CRUD。</br> 3. 電子書圖片管理(移動順序、新增、刪除)。</br> 4. 以`LINQ`語法透過`model`，對資料庫進行存取。 |  [程式碼](https://github.com/hank444tw/1119Work/blob/master/1119Work/Controllers/HomeController.cs) |
  | **資料庫** |  1. 使用`ASP.NET MVC`的`Entity Framework`進行資料庫設計。</br> 2. 建置`Model`來對資料庫進行存取。 |   [程式碼](https://github.com/hank444tw/1119Work/tree/master/1119Work/Models) |     

* 網站截圖
<img src="https://github.com/hank444tw/1119Work/blob/master/banner1.JPG" stryle="float:right" />  

<img src="https://github.com/hank444tw/1119Work/blob/master/banner.JPG" stryle="float:right" />    

<img src="https://github.com/hank444tw/1119Work/blob/master/banner2.JPG" stryle="float:right" />
