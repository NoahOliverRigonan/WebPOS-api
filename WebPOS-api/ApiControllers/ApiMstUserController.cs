using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace WebPOS_api.ApiControllers
{
    [RoutePrefix("api/user")]
    public class ApiMstUserController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();

        [HttpGet, Route("list")]
        public List<Entities.MstUser> listMstUser()
        {
            var user = from d in db.MstUsers
                       select new Entities.MstUser
                       {
                           Id = d.Id,
                           UserName = d.UserName,
                           Password = d.Password,
                           FullName = d.FullName,
                           UserCardNumber = d.UserCardNumber,
                           EntryUserId = d.EntryUserId,
                           EntryDateTime = d.EntryDateTime,
                           UpdateUserId = d.UpdateUserId,
                           UpdateDateTime = d.UpdateDateTime,
                           IsLocked = d.IsLocked,
                           AspNetUserId = d.AspNetUserId,
                       };
            return user.ToList();

        }

        [HttpGet, Route("list/{id}")]
        public List<Entities.MstUser> listMstUser(String id)
        {
            var userList = from d in db.MstUsers
                           select d;

            var sysFormList = from d in db.SysForms
                              select d;

            //var userForm = from d in db.MstUserForms select d;

            var user = from d in db.MstUsers
                       where d.Id == Convert.ToInt32(id)
                       select new Entities.MstUser
                       {
                           Id = d.Id,
                           UserName = d.UserName,
                           Password = d.Password,
                           FullName = d.FullName,
                           UserCardNumber = d.UserCardNumber,
                           EntryUserId = d.EntryUserId,
                           EntryDateTime = d.EntryDateTime,
                           UpdateUserId = d.UpdateUserId,
                           UpdateDateTime = d.UpdateDateTime,
                           IsLocked = d.IsLocked,
                           AspNetUserId = d.AspNetUserId,

                           listMstUser = userList.Select(m => new Entities.MstUser
                           {
                               Id = m.Id,
                               UserName = m.UserName
                           }).ToList(),

                           listSysForm = sysFormList.Select(m => new Entities.SysForm
                           {
                               Id = m.Id,
                               FormDescription = m.FormDescription
                           }).ToList(),

                           //listMstUserForm = userForm.Select(m => new Entities.MstUserForm
                           //{
                           //    Id = m.Id,
                           //    FormId = m.FormId,
                           //    UserId = m.UserId,
                           //    CanDelete = m.CanDelete,
                           //    CanAdd = m.CanAdd,
                           //    CanLock = m.CanLock,
                           //    CanUnlock = m.CanUnlock,
                           //    CanPrint = m.CanPrint,
                           //    CanPreview = m.CanPreview,
                           //    CanEdit = m.CanEdit,
                           //    CanTender = m.CanTender,
                           //    CanDiscount = m.CanDiscount,
                           //    CanView = m.CanView,
                           //    CanSplit = m.CanSplit,
                           //    CanCancel = m.CanCancel,
                           //    CanReturn = m.CanReturn
                           //}).ToList()
                       };
            return user.ToList();

        }

        [HttpPost, Route("post")]
        public Int32 postUser(Entities.MstUser user)
        {
            try
            {
                var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;

                var id = User.Identity.GetUserId();


                Data.MstUser newUser = new Data.MstUser();
                newUser.UserName = "n/a";
                newUser.Password = "n/a";
                newUser.FullName = "n/a";
                newUser.UserCardNumber = "n/a";
                newUser.EntryUserId = userId;
                newUser.EntryDateTime = DateTime.Today;
                newUser.UpdateUserId = userId;
                newUser.UpdateDateTime = DateTime.Today;
                newUser.IsLocked = false;
                newUser.AspNetUserId = id;
                db.MstUsers.InsertOnSubmit(newUser);
                db.SubmitChanges();

                return newUser.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }

        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putUser(String id, Entities.MstUser user)
        {
            try
            {
                var users = from d in db.MstUsers where d.Id == Convert.ToInt32(id) select d;
                if (users.Any())
                {
                    //var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;

                    var updateUser = users.FirstOrDefault();
                    updateUser.UserName = user.UserName;
                    updateUser.Password = user.Password;
                    updateUser.FullName = user.FullName;
                    updateUser.UserCardNumber = user.UserCardNumber;
                    updateUser.EntryUserId = user.EntryUserId;
                    updateUser.EntryDateTime = DateTime.Now;
                    updateUser.UpdateUserId = user.UpdateUserId;
                    updateUser.UpdateDateTime = DateTime.Now;
                    updateUser.IsLocked = user.IsLocked;
                    updateUser.AspNetUserId = user.AspNetUserId;
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        //DELETE USER
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteUser(String id)
        {
            try
            {
                var activities = from d in db.MstUsers where d.Id == Convert.ToInt32(id) select d;
                if (activities.Any())
                {
                    db.MstUsers.DeleteOnSubmit(activities.First());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

    }
}
