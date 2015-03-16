using SStest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SStest.Controllers
{
    public class MapController : Controller
    {

        private SmartShoppingEntities db = new SmartShoppingEntities();
        int M_id;
        //private IQueryable<ShoppingList> ShoppingLists;

        // GET: Map
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Map()
        {
            if (User.Identity.IsAuthenticated)
            {
                string aspid = User.Identity.GetUserId();
                M_id = db.Members.Where(m => m.Id == aspid).Select(m => m.Member_ID).First();
            }

            var MemberLists = from a in db.ShoppingList
                              where a.PList.Member_ID == M_id && a.Address != null
                              select a;

            return View(MemberLists);
        }

        [Authorize]
        public ActionResult AddrListsByM_ID()
        {
            if (User.Identity.IsAuthenticated)
            {
                string aspid = User.Identity.GetUserId();
                M_id = db.Members.Where(m => m.Id == aspid).Select(m => m.Member_ID).First();
            }

            var Lists = from a in db.ShoppingList
                       where a.PList.Member_ID == M_id && a.Address != null
                       select new { addr = a.Address, text = a.ListName, id = a.ListName };

            return Json(Lists.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddrListByL_ID(int L_ID)
        {
            var theList = from a in db.ShoppingList
                       where a.List_ID == L_ID && a.Address != null
                       select new { addr = a.Address, text = a.ListName, id = a.ListName };

            ViewBag.addr =theList.First().addr.ToString();
            //return Json(List.ToList(), JsonRequestBehavior.AllowGet);
            return View(theList);
        }
    }
}