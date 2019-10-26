using CodeCamp1.DB;
using CodeCamp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeCamp1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        //public JsonResult ReciveBeacon(postData pd)
        public JsonResult ReciveBeacon(string id)
        {
            return Json(new
            {
                Success = true,
                AddedCount = 1,
                Message = id
            });
        }

        public ActionResult GenerateTicket()
        {
            return View();
        }

        public ActionResult AssignPassengerToTrip()
        {
            return View();
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

        private int SendRequestToProvide(int providerId, int passengersCount, string ticketNum = "")
        {
            return passengersCount;
        }

        [HttpPost]
        public JsonResult AssignPassengerToTrip(string ticketNumber, int companyId)
        {
            Int32 message = -1;
            Trip tripInfo = new Trip();

            CrudDataService objCrd = new CrudDataService();

            tripInfo = objCrd.AssignPassengerToTrip(ticketNumber);

            var result = new { DriverName = tripInfo.DriverName, PickupGate = tripInfo.PickupGate, PlateNum = tripInfo.PlateNum };
            return Json(result);

        }

        [HttpPost]
        public JsonResult GenerateTicketRequest(int providerId, string ticketNum)
        {
            int availableCount = SendRequestToProvide(providerId, 1, ticketNum);

            if (availableCount > 0)
            {
                return Json(new
                {
                    Success = true,
                    AddedCount = 1,
                    Message = "Success"
                });
            }


            return Json(new
            {
                Success = false,
                AddedCount = -1,
                Message = "no cars"
            });

        }

        [HttpPost]
        public JsonResult GeneratFlightInfo()
        {
            try
            {
                Flight objFlight = new Flight();

                Int32 message = -1;
                executeReuslt r = new executeReuslt();

                CrudDataService objCrd = new CrudDataService();

                r = objCrd.GenerateFlightInfoDb(User.Identity.Name);
                message = r.result;

                if (r.result > 0)
                {
                    string subject = "Booking request via Book now in our website";

                    int needRideCount = objCrd.ReadPassengerCount();

                    List<TaxiProvider> tpList = new List<TaxiProvider>();

                    tpList = objCrd.ReadTaxiProvidersList();

                    foreach (var t in tpList)
                    {
                        int requiredCount = (int) Math.Ceiling((needRideCount * t.Quote) / 100.0);

                        int availableCount = SendRequestToProvide(t.Id, requiredCount);

                        if (availableCount < requiredCount)
                        {
                            needRideCount = needRideCount - availableCount;
                        }
                    }
                }

                else
                {
                    message = -1;
                }

                // Returns message that successfully uploaded  
                return Json(new
                {
                    Success = true,
                    AddedCount = r.addedCount,
                    Message = message
                });
            }

            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    AddedCount = "-1",
                    Message = -1
                });
            }
        }
    }
}