using hmfDoNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeanCloud;
using LeanCloud.Storage;
using System.Collections.ObjectModel;
using Quobject.EngineIoClientDotNet.Modules;

namespace hmfDoNet.Controllers
{
    public class WorksCenterController : Controller
    {

        public ActionResult Create()
        {
            ViewBag.selectList = new List<SelectListItem>() {

             new SelectListItem(){Value="体育",Text="体育"},
             new SelectListItem(){Value="饮食",Text="饮食"},
             new SelectListItem(){Value="穿搭",Text="穿搭"},
             new SelectListItem(){Value="新闻",Text="新闻"}};


            return View();
        }
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            LCUser user = await LCUser.GetCurrent();

            //LCObject post = LCObject.CreateWithoutData("Post", user.SessionToken);
            LCQuery<LCObject> Cardsquery = new LCQuery<LCObject>("Cards");
            Cardsquery.WhereEqualTo("author", user);
            Cardsquery.WhereExists("cover");

            Cardsquery.Include("cover");

            // comments 包含与 post 相关联的评论
            try
            {

                ReadOnlyCollection<LCObject> Cardcomments = await Cardsquery.Find();
                List<Cards> cards = new List<Cards>();
                foreach (LCObject Cardcomment in Cardcomments)
                {
                    // 该操作无需网络连接

                    Cards card = new Cards();


                    card.cardClassification = Cardcomment["cardClassification"] as String;
                    card.cardContent = Cardcomment["cardContent"] as String;


                    //var covers = Cardcomment["cover"];


                    LCQuery<LCObject> query = new LCQuery<LCObject>("_File");
                    query.WhereEqualTo("objectId", Cardcomment["cover"]);

                    LCObject todo = await query.First();

                    System.Diagnostics.Debug.WriteLine(todo["url"]);
                    card.cardImageData = (string)todo["url"];

                    card.cardTitle = Cardcomment["cardTitle"] as String;
                    card.cardObjectId = Cardcomment.ObjectId as String;

                    cards.Add(card);

                }


                ViewBag.cardsList = cards;


                return View("index");


            }
            catch (LCException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return View("index");
            }

            return View("index");

        }

        /// <summary>
        /// 上传文件
        /// </summary>
        public async System.Threading.Tasks.Task<ActionResult> CreateWork(Cards card, HttpPostedFileBase file)
        {
            card.cardImageType = file.ContentType;//获取图片类型
            var img = new byte[file.ContentLength];//新建一个长度等于图片大小的二进制地址
            
   
            file.InputStream.Read(img, 0, file.ContentLength);//将image读取到ImageData中
            var fileName = file.FileName;
            //
            var filePath = Server.MapPath(string.Format("~/{0}", "File"));

            //file.SaveAs(Path.Combine(filePath, fileName));


            LCObject db_Card = new LCObject("Cards");
            LCFile LCfile = new LCFile(fileName, img);

            db_Card["cardClassification"] = card.cardClassification;
            db_Card["cardTitle"] = card.cardTitle;
            db_Card["cardContent"] = card.cardContent;

            //db_Card["cardImageData"] = LCfile;
            db_Card["cardImageType"] = card.cardImageType;

            await LCfile.Save();
             db_Card["cover"] = LCfile.ObjectId;
            //db_Card.Add("cover", LCfile);

            try
            {
                LCUser user = await LCUser.GetCurrent();
                db_Card["author"] = user;


                await db_Card.Save();


                System.Diagnostics.Debug.WriteLine("保存成功");
         
                return Redirect("Index");
            }
            catch (LCException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                System.Diagnostics.Debug.WriteLine("保存失败");

                return Json("保存失败");

            }




        }



        public async System.Threading.Tasks.Task<ActionResult> getCoverAsync(LCObject card)
        {
            LCQuery<LCObject> Filequery = new LCQuery<LCObject>("_File");
            Filequery.WhereEqualTo("card", card);

            try
            {
                ReadOnlyCollection<LCObject> Files = await Filequery.Find();
                foreach (var File in Files)
                {
                    ViewBag.url = File["url"];
                }
                return View("index");
            }
            catch (LCException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return View("index");

        }

    }
}