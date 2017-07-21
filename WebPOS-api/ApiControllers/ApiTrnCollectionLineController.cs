using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebPOS_api.ApiControllers
{
    [RoutePrefix("api/CollectionLine")]
    public class ApiTrnCollectionLineController : ApiController
    {
        private Data.posdbDataContext db = new Data.posdbDataContext();


        [HttpGet, Route("get/{collectionLineId}")]
        public List<Entities.TrnCollectionLine> listCollectionLine(String collectionLineId)
        {
            var collectionLine = from d in db.TrnCollectionLines
                                    where d.CollectionId == Convert.ToInt32(collectionLineId)
                                    select new Entities.TrnCollectionLine
                                    {
                                        Id = d.Id,
                                        CollectionId = d.CollectionId,
                                        Amount = d.Amount,
                                        PayTypeId = d.PayTypeId,
                                        PayType = d.MstPayType.PayType,
                                        CheckNumber = d.CheckNumber,
                                        CheckDate = d.CheckDate,
                                        CheckBank = d.CheckBank,
                                        CreditCardVerificationCode = d.CreditCardVerificationCode,
                                        CreditCardNumber = d.CreditCardNumber,
                                        CreditCardType = d.CreditCardType,
                                        CreditCardBank = d.CreditCardBank,
                                        GiftCertificateNumber = d.GiftCertificateNumber,
                                        OtherInformation = d.OtherInformation,
                                        StockInId = d.StockInId,
                                        StockIn = d.TrnStockIn.StockInNumber,
                                        AccountId = d.AccountId,
                                        Account = d.MstAccount.Account
                                    };

            return collectionLine.ToList();
        }

        [HttpPost, Route("post")]
        public HttpResponseMessage postCollectionLine(Entities.TrnCollectionLine collectionLine)
        {
            try
            {
                Data.TrnCollectionLine newCollectionLine = new Data.TrnCollectionLine();
                newCollectionLine.CollectionId = collectionLine.CollectionId;
                newCollectionLine.Amount = collectionLine.Amount;
                newCollectionLine.PayTypeId = collectionLine.PayTypeId;
                newCollectionLine.CheckNumber = collectionLine.CheckNumber;
                newCollectionLine.CheckDate = collectionLine.CheckDate;
                newCollectionLine.CheckBank = collectionLine.CheckBank;
                newCollectionLine.CreditCardVerificationCode = collectionLine.CreditCardVerificationCode;
                newCollectionLine.CreditCardNumber = collectionLine.CreditCardNumber;
                newCollectionLine.CreditCardType = collectionLine.CreditCardType;
                newCollectionLine.CreditCardBank = collectionLine.CreditCardBank;
                newCollectionLine.GiftCertificateNumber = collectionLine.GiftCertificateNumber;
                newCollectionLine.OtherInformation = collectionLine.OtherInformation;
                newCollectionLine.StockInId = collectionLine.StockInId;
                newCollectionLine.AccountId = collectionLine.AccountId;
                db.TrnCollectionLines.InsertOnSubmit(newCollectionLine);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Something's wrong from the server.");
            }

        }

        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putCollectionLine(String id, Entities.TrnCollectionLine newCollectionLine)
        {
            try
            {
                var collectionLine = from d in db.TrnCollectionLines where d.Id == Convert.ToInt32(id) select d;
                if (collectionLine.Any())
                {
                    //var userId = (from d in db.MstUsers where d.AspNetUserId == User.Identity.GetUserId() select d).FirstOrDefault().Id;

                    var updateCollectionLine = collectionLine.FirstOrDefault();
                    updateCollectionLine.CollectionId = newCollectionLine.CollectionId;
                    updateCollectionLine.Amount = newCollectionLine.Amount;
                    updateCollectionLine.PayTypeId = newCollectionLine.PayTypeId;
                    updateCollectionLine.CheckNumber = newCollectionLine.CheckNumber;
                    updateCollectionLine.CheckDate = newCollectionLine.CheckDate;
                    updateCollectionLine.CheckBank = newCollectionLine.CheckBank;
                    updateCollectionLine.CreditCardVerificationCode = newCollectionLine.CreditCardVerificationCode;
                    updateCollectionLine.CreditCardNumber = newCollectionLine.CreditCardNumber;
                    updateCollectionLine.CreditCardType = newCollectionLine.CreditCardType;
                    updateCollectionLine.CreditCardBank = newCollectionLine.CreditCardBank;
                    updateCollectionLine.GiftCertificateNumber = newCollectionLine.GiftCertificateNumber;
                    updateCollectionLine.OtherInformation = newCollectionLine.OtherInformation;
                    updateCollectionLine.StockInId = newCollectionLine.StockInId;
                    updateCollectionLine.AccountId = newCollectionLine.AccountId;
                    db.TrnCollectionLines.InsertOnSubmit(updateCollectionLine);
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
        public HttpResponseMessage deleteCollectionLine(String id)
        {
            try
            {
                var activities = from d in db.TrnCollectionLines where d.Id == Convert.ToInt32(id) select d;
                if (activities.Any())
                {
                    db.TrnCollectionLines.DeleteOnSubmit(activities.First());
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
