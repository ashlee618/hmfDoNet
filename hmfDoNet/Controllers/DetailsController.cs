using hmfDoNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeanCloud.Storage;
using LeanCloud;
using System.Collections.ObjectModel;

namespace hmfDoNet.Controllers
{
    public class DetailsController : Controller
    {
        // GET: Details
        public async System.Threading.Tasks.Task<ActionResult> Index(String id)
        {
            System.Diagnostics.Debug.WriteLine("imte"+ id);
            try
            {
                LCQuery<LCObject> query = new LCQuery<LCObject>("Cards");
                query.WhereEqualTo("objectId", id);
                // students 是包含满足条件的 Student 对象的数组
                LCObject obejct = await query.First();

                LCQuery<LCObject> Coverquery = new LCQuery<LCObject>("_File");
                Coverquery.WhereEqualTo("objectId", obejct["cover"]);

                LCObject todo = await Coverquery.First();

                Cards card = new Cards();

                card.cardImageData = (string)todo["url"];
                card.cardClassification = obejct["cardClassification"] as String;
                card.cardContent = obejct["cardContent"] as String;
                card.cardTitle = obejct["cardTitle"] as String;
                card.cardObjectId = obejct.ObjectId as String;

                ViewBag.card = card;
                return View();
            }
            catch(LCException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
           

            return View();
        }
    }
}