using hmfDoNet.Filter;
using hmfDoNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Documents;
using LeanCloud.Storage;
using System.Collections.ObjectModel;
using LeanCloud;
namespace hmfDoNet.Controllers
{
    [checkUser]
    public class HomeController : Controller
    {
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            List<Cards> cardslist = new List<Cards>();


            LCQuery<LCObject> query = new LCQuery<LCObject>("Cards");

            // students 是包含满足条件的 Student 对象的数组
            try
            {
                ReadOnlyCollection<LCObject> Cards = await query.Find();
                foreach(LCObject card in Cards)
                {

                    Cards cards = new Cards();
                    cards.cardClassification = card["cardClassification"] as String;
                    cards.cardContent = card["cardContent"] as String;
                    cards.cardTitle = card["cardTitle"] as String;
                    cards.cardObjectId = card.ObjectId as String;
                    LCQuery<LCObject> Coverquery = new LCQuery<LCObject>("_File");
                    Coverquery.WhereEqualTo("objectId", card["cover"]);

                    LCObject todo = await Coverquery.First();

                    cards.cardImageData = (string)todo["url"];

                    cardslist.Add(cards);

                }
                ViewBag.selectList = new List<SelectListItem>() {
            
             new SelectListItem(){Value="体育",Text="体育"},
             new SelectListItem(){Value="饮食",Text="饮食"},
             new SelectListItem(){Value="穿搭",Text="穿搭"},
             new SelectListItem(){Value="新闻",Text="新闻"}};


                ViewBag.cardsList = cardslist;

                 return View();

            }
            catch (LCException e)
            {
                System.Diagnostics.Debug.WriteLine("读取cards失败"+ e);
                return null;

            }
            


        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Search(String search)
        {
            LCQuery<LCObject> Userquery = new LCQuery<LCObject>("_user");
            // 相当于 SQL 中的 title LIKE '%lunch%'


            Userquery.WhereContains("username", search);
            ReadOnlyCollection<LCObject> Users = await Userquery.Find();

            List<User> userLists = new List<User>();
            foreach (LCObject User in Users)
            {
                User u = new User(); 
                u.Username = User["username"] as String;
                u.ObjectId = User.ObjectId as String;
                userLists.Add(u);
            }
            ViewBag.userLists = userLists;

                LCQuery<LCObject> query = new LCQuery<LCObject>("Cards");
            // 相当于 SQL 中的 title LIKE '%lunch%'
            query.WhereContains("cardTitle", search);
            List<Cards> cardslist = new List<Cards>();

            try
            {
                ReadOnlyCollection<LCObject> Cards = await query.Find();
                foreach (LCObject card in Cards)
                {

                    Cards cards = new Cards();
                    cards.cardClassification = card["cardClassification"] as String;
                    cards.cardContent = card["cardContent"] as String;
                    cards.cardTitle = card["cardTitle"] as String;

                    LCQuery<LCObject> Coverquery = new LCQuery<LCObject>("_File");
                    Coverquery.WhereEqualTo("objectId", card["cover"]);

                    LCObject todo = await Coverquery.First();

                    cards.cardImageData = (string)todo["url"];
                    cards.cardObjectId = card.ObjectId as String;
                    cardslist.Add(cards);

                }
                ViewBag.cardsList = cardslist;

                return View("search");
            }
            catch (LCException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Content("搜索错误" + e);
            }
          
        }


        public async System.Threading.Tasks.Task<ActionResult> AddFlowee (String Username,String objectid)
        {


            try
            {
                LCUser user = await LCUser.GetCurrent();
                await user.Follow(objectid);
                ViewBag.result = "关注成功";
                return View("FollowResult");
                //return Content("关注成功");
            }
            catch (Exception e)
            {
                ViewBag.result = "关注失败"+e;
                return View("FollowResult");
            }
        }
    }
}