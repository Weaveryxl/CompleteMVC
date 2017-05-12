using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CompleteMVC.DAL;
using CompleteMVC.ViewModels;
using Newtonsoft.Json;

namespace CompleteMVC.Controllers
{
    public class AccountController : BaseController
    {
        SchoolContext db = new SchoolContext();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }


        // POST: Login/Create
        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var user = db.User.FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);
                    //this part is what the GetUserDetails() did in Authentication2
                    //But why don't we make a user object like in Authentication2? Such as: 
                    //User user = new User() { Email = model.Email, Password = model.Password }; //从model中取email和密码，建立user

                    if (user != null)   //if user is valid:
                    {
                        var userRoles = user.Roles.Select(r => r.RoleName).ToArray();   //take all the role name of this one user
                        var serializeModel = new SchoolPrincipalSerializeModel
                        {
                            UserId = user.UserId,
                            FirstName = user.FirstMidName,
                            LastName = user.LastName,
                            Roles = userRoles
                        };
                        //why do we serialize it? is it nessesary? in authentication2, we didn't, why?
                        var userData = JsonConvert.SerializeObject(serializeModel);
                        // why not use FormsAuthentication.SetAuthCookie(model.Email, false); like in authentication2?
                        //THen the following lines are the same with authentication2
                        var authTicket = new FormsAuthenticationTicket(1, user.Username, DateTime.Now,
                            DateTime.Now.AddMinutes(15), false, userData);
                        var encTicket = FormsAuthentication.Encrypt(authTicket);
                        var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        Response.Cookies.Add(faCookie); //why it's the same with HttpContext.Response.Cookies.Add(authCookie)?

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username and/or password");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}