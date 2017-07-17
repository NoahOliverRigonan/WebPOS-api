using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace WebPOS_api.ApiControllers
{
    [RoutePrefix("api/userform")]
    public class ApiMstUserFormController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();

        [HttpGet, Route("list/{userId}")]
        public List<Entities.MstUserForm> getMstUserForm(String userId)
        {

            var userForm = from d in db.MstUserForms
                           join a in db.SysForms on
                           d.FormId equals a.Id
                           where d.UserId == Convert.ToInt32(userId)
                           select new Entities.MstUserForm
                           {
                               Id = d.Id,
                               FormId = d.FormId,
                               FormDescription = a.FormDescription,
                               UserId = d.UserId,
                               CanDelete = d.CanDelete,
                               CanAdd = d.CanAdd,
                               CanLock = d.CanLock,
                               CanUnlock = d.CanUnlock,
                               CanPrint = d.CanPrint,
                               CanPreview = d.CanPreview,
                               CanEdit = d.CanEdit,
                               CanTender = d.CanTender,
                               CanDiscount = d.CanDiscount,
                               CanView = d.CanView,
                               CanSplit = d.CanSplit,
                               CanCancel = d.CanCancel,
                               CanReturn = d.CanReturn
                           };
            return userForm.ToList();
        }

        [HttpGet, Route("get/{id}")]
        public List<Entities.MstUserForm> getMstUserFormId(String id)
        {

            var userForm = from d in db.MstUserForms
                           where d.Id == Convert.ToInt32(id)
                           select new Entities.MstUserForm
                           {
                               Id = d.Id,
                               FormId = d.FormId,
                               //FormDescription = a.FormDescription,
                               UserId = d.UserId,
                               CanDelete = d.CanDelete,
                               CanAdd = d.CanAdd,
                               CanLock = d.CanLock,
                               CanUnlock = d.CanUnlock,
                               CanPrint = d.CanPrint,
                               CanPreview = d.CanPreview,
                               CanEdit = d.CanEdit,
                               CanTender = d.CanTender,
                               CanDiscount = d.CanDiscount,
                               CanView = d.CanView,
                               CanSplit = d.CanSplit,
                               CanCancel = d.CanCancel,
                               CanReturn = d.CanReturn
                           };
            return userForm.ToList();
        }

        [HttpPost, Route("post")]
        public HttpResponseMessage postMstUserForm(Entities.MstUserForm userForm)
        {
            try
            {
                Data.MstUserForm newUserForm = new Data.MstUserForm();
                newUserForm.FormId = userForm.FormId;
                newUserForm.UserId = userForm.UserId;
                newUserForm.CanDelete = userForm.CanDelete;
                newUserForm.CanAdd = userForm.CanAdd;
                newUserForm.CanLock = userForm.CanLock;
                newUserForm.CanUnlock = userForm.CanUnlock;
                newUserForm.CanPrint = userForm.CanPrint;
                newUserForm.CanPreview = userForm.CanPreview;
                newUserForm.CanEdit = userForm.CanEdit;
                newUserForm.CanTender = userForm.CanTender;
                newUserForm.CanDiscount = userForm.CanDiscount;
                newUserForm.CanView = userForm.CanView;
                newUserForm.CanSplit = userForm.CanSplit;
                newUserForm.CanCancel = userForm.CanCancel;
                newUserForm.CanReturn = userForm.CanReturn;
                db.MstUserForms.InsertOnSubmit(newUserForm);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Something's went wrong from the server.");
            }

        }

        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putUser(String id, Entities.MstUserForm userForm)
        {
            try
            {
                var usersForm = from d in db.MstUserForms where d.Id == Convert.ToInt32(id) select d;
                if (usersForm.Any())
                {
                    //var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;

                    var updateUserForm = usersForm.FirstOrDefault();
                    updateUserForm.FormId = userForm.FormId;
                    updateUserForm.UserId = userForm.UserId;
                    updateUserForm.CanDelete = userForm.CanDelete;
                    updateUserForm.CanAdd = userForm.CanAdd;
                    updateUserForm.CanLock = userForm.CanLock;
                    updateUserForm.CanUnlock = userForm.CanUnlock;
                    updateUserForm.CanPrint = userForm.CanPrint;
                    updateUserForm.CanPreview = userForm.CanPreview;
                    updateUserForm.CanEdit = userForm.CanEdit;
                    updateUserForm.CanTender = userForm.CanTender;
                    updateUserForm.CanDiscount = userForm.CanDiscount;
                    updateUserForm.CanView = userForm.CanView;
                    updateUserForm.CanSplit = userForm.CanSplit;
                    updateUserForm.CanCancel = userForm.CanCancel;
                    updateUserForm.CanReturn = userForm.CanReturn;
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


        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteUserForm(String id)
        {
            try
            {
                var activities = from d in db.MstUserForms where d.Id == Convert.ToInt32(id) select d;
                if (activities.Any())
                {
                    db.MstUserForms.DeleteOnSubmit(activities.First());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Something went Wrong");
            }
        }
    }
}
