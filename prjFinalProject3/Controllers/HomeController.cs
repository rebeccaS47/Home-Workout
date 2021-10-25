using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using prjFinalProject3.Models;
using PagedList;
using System.Web.Security;


namespace prjFinalProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        dbGymEntities db = new dbGymEntities();
        // GET: Home

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Users = "Jasper, anita, tom")]
        public ActionResult Classs(string claId = "A01")
        {
            CouClaRou vm = new CouClaRou()
            {
                courses = db.TableCourses1081747.ToList(),
                classs = db.TableClasss1081747.ToList(),
                routines = db.TableRoutines1081747.Where(m => m.ClaId == claId).ToList()
            };
            return View(vm);
        }

        int pageSize = 9;
        [AllowAnonymous]
        public ActionResult Locations(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var products = db.TableLocations1081747.OrderBy(m => m.LoId).ToList();
            var result = products.ToPagedList(currentPage, pageSize);
            return View(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SearchLo(string city)
        {
            var result = db.TableLocations1081747.Where(m => m.LoAddress.Contains(city));
            return View(result);
        }

        [Authorize(Users = "Jasper, anita, tom")]
        public ActionResult Members()
        {
            return View(db.TableMembers1081747.ToList());
        }

        [Authorize(Users = "Jasper, anita, tom")]
        public ActionResult EditMem(int MemId)
        {
            var mem = db.TableMembers1081747
                .Where(m => m.MemId == MemId).FirstOrDefault();   
            return View(mem);
        }

        [Authorize(Users = "Jasper, anita, tom")]
        [HttpPost]
        public ActionResult EditMem(TableMembers1081747 mem)
        {
            if (ModelState.IsValid)
            {
                var temp = (from m in db.TableMembers1081747        
                            where m.MemId == mem.MemId
                            select m)
                    .FirstOrDefault();

                temp.MemId = mem.MemId;                
                temp.MemName = mem.MemName;
                temp.MemGender = mem.MemGender;
                temp.MemPhone = mem.MemPhone;
                temp.MemEmail = mem.MemEmail;
                temp.MemJoinDate = mem.MemJoinDate;
                temp.MemBalence = mem.MemBalence;
                temp.MemWeight = mem.MemWeight;
                temp.MemHeight = mem.MemHeight;
                db.SaveChanges();                    
                return RedirectToAction("Members");   
            }
            return View(mem);
        }

        [Authorize(Users = "Jasper, anita, tom")]
        public ActionResult DeleteMem(int MemId)
        {
            var mem = db.TableMembers1081747
                .Where(m => m.MemId == MemId)
                .FirstOrDefault();
            db.TableMembers1081747.Remove(mem);
            db.SaveChanges();
            return RedirectToAction("Members");
        }

        [Authorize(Users = "Jasper, anita, tom")]
        public ActionResult CreateMem(TableMembers1081747 mem)
        {
            try
            {
                db.TableMembers1081747.Add(mem);
                db.SaveChanges();
                return RedirectToAction("Members", new { MemId = mem.MemId }); 
            }
            catch (Exception ex)
            { }
            return View(mem);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string txtUid, string txtPwd)
        {
            string[] uidAry = new string[] { "jasper", "anita", "tom" };
            string[] pwdAry = new string[] { "123", "456", "789" };

            int index = -1;
            for (int i = 0; i < uidAry.Length; i++)
            {
                if (uidAry[i] == txtUid && pwdAry[i] == txtPwd)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                ViewBag.Err = "帳密錯誤!";
            }
            else
            {
                FormsAuthentication.RedirectFromLoginPage(txtUid, true);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();  
            return RedirectToAction("Index");
        }

    }
}