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

        [HttpGet, Route("get/{userId}")]
        public List<Entities.MstUserForm> getMstUserFormId(String userId)
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
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }
    }
}
