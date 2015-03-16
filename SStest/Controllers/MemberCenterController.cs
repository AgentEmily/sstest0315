using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SStest.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace SStest.Controllers
{
    public class MemberCenterController : Controller
    {
        SmartShoppingEntities db = new SmartShoppingEntities();
        int MemberID;
        // GET: MemberCenter
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CancelEmailAd(bool Confirmed)
        {
            ViewBag.Confirmed = Confirmed;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelAd(Members member)
        {
            //get member with the email, save adok to false
            if (ModelState.IsValid)
            {
                var user = db.Members.Where(c=>c.Email==member.Email).First();
                if (user!=null)
                {
                    user.AdOK = false;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
                
            }
            return RedirectToAction("CancelEmailAd", new { Confirmed=true});
        }

        [HttpGet]
        [Authorize]
        public ActionResult ModifyMemberData(int MemberId = 1)
        {
            if (User.Identity.IsAuthenticated)
            {
                string aspid = User.Identity.GetUserId();
                MemberID = db.Members.Where(m => m.Id == aspid).Select(m => m.Member_ID).First();
            }
            var q = db.Members.Where(m => m.Member_ID == MemberID).FirstOrDefault();
            return View("ModifyMemberData", q);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ModifyMemberData(Members _Members)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files[0];
               
                
                var imgSize = file.ContentLength;
                byte[] imgByte = new Byte[imgSize];
                file.InputStream.Read(imgByte, 0, imgSize);
                _Members.M_Picture = imgByte;
                if (_Members.Birthdate!=null)
                {
                    
                    DateTime d = new DateTime();
                    d=(DateTime)_Members.Birthdate;
                    d.ToShortDateString();
                    _Members.Birthdate = d;
                }
                
                
                db.Entry(_Members).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ModifyMemberData");

            }

            return View();
        }
        public ActionResult GetImagebytes(int id=1)
        {
            if (User.Identity.IsAuthenticated)
            {
                string aspid = User.Identity.GetUserId();
                MemberID = db.Members.Where(m => m.Id == aspid).Select(m => m.Member_ID).First();
            }
            var q = db.Members.Where(m => m.Member_ID == MemberID).Select(m => m.M_Picture).FirstOrDefault();
            return File(q, "image/jpeg");
        }
    }
}