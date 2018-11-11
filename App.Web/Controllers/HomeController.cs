using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using App.Web.Models;
using App.Web.Models.Common;
using Microsoft.AspNetCore.Diagnostics;
using App.Web.Models.Home;
using App.Contract.DTOs;
using App.Contract.Constants;
using System;

namespace App.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly int _minimumAdultRequiredPerRoom = 1;
        private readonly int _maximumNumberOfAdultAndChildren = 7;
        private readonly int _maximumNumberOfRoomPerBooking = 3;
        private readonly int _maximumNumberOfAdultPerRoom = 3;
        private readonly int _maximumNumberOfChildrenPerRoom = 3;
        private readonly int _maximumNumberOfInfantPerRoom = 3;

        public IActionResult Index()
        {
            var model = new HomeViewModel();

            model.MinimumAdultRequiredPerRoom = _minimumAdultRequiredPerRoom;
            model.MaximumNumberOfAdultAndChildren = _maximumNumberOfAdultAndChildren;
            model.MaximumNumberOfRoomPerBooking = _maximumNumberOfRoomPerBooking;
            model.MaximumNumberOfAdultPerRoom = _maximumNumberOfAdultPerRoom;
            model.MaximumNumberOfChildrenPerRoom = _maximumNumberOfChildrenPerRoom;
            model.MaximumNumberOfInfantPerRoom = _maximumNumberOfInfantPerRoom;

            return View(model);
        }

        public IActionResult CheckOccupancy(HomeViewModel model)
        {
            List<ErrorDTO> errors = new List<ErrorDTO>();
            if (!ModelState.IsValid)
            {
                var modelErrors = new List<string>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        modelErrors.Add(modelError.ErrorMessage);
                    }
                }

                return Json(new BaseViewModel(1, modelErrors));
            }

            #region checking
            if (model.AdultCount < _minimumAdultRequiredPerRoom)
            {
                var error = ErrorCode.MinimumNumberAdultRequired;
                error.Description = error.Description.Replace("{amount}", _minimumAdultRequiredPerRoom.ToString());
                errors.Add(error);
            }
            if ((model.AdultCount + model.ChildrenCount) > _maximumNumberOfAdultAndChildren)
            {
                var error = ErrorCode.NumberOfGuestExceedLimit;
                error.Description = error.Description.Replace("{amount}", _maximumNumberOfAdultAndChildren.ToString());
                errors.Add(error);
            }
            if ((model.ChildrenCount / _maximumNumberOfChildrenPerRoom) > model.AdultCount || (model.InfantCount / _maximumNumberOfInfantPerRoom) > model.AdultCount)
            {
                var error = ErrorCode.AdultSupervisionIsRequired;
                error.Description = error.Description.Replace("{childrenAmount}", _maximumNumberOfChildrenPerRoom.ToString());
                error.Description = error.Description.Replace("{infantAmount}", _maximumNumberOfInfantPerRoom.ToString());
                errors.Add(error);
            }
            if (model.AdultCount > (_maximumNumberOfRoomPerBooking * _maximumNumberOfAdultPerRoom)
                || model.ChildrenCount > (_maximumNumberOfRoomPerBooking * _maximumNumberOfChildrenPerRoom)
                || model.InfantCount > (_maximumNumberOfRoomPerBooking * _maximumNumberOfInfantPerRoom))
            {
                var error = ErrorCode.NumberOfRoomExceedLimit;
                error.Description = error.Description.Replace("{amount}", _maximumNumberOfRoomPerBooking.ToString());
                errors.Add(error);
            }
            #endregion

            if (errors.Count > 0)
            {
                return Json(new BaseViewModel(1, errors.Select(x => x.Description).ToList()));
            }
            else
            {
                #region find number of room needed
                var roomNeeded = 0;
                var adultRoomCount = Convert.ToInt32(Math.Ceiling((decimal)model.AdultCount / _maximumNumberOfAdultPerRoom));
                var childrenRoomCount = Convert.ToInt32(Math.Ceiling((decimal)model.ChildrenCount / _maximumNumberOfChildrenPerRoom));
                var infantRoomCount = Convert.ToInt32(Math.Ceiling((decimal)model.InfantCount / _maximumNumberOfInfantPerRoom));

                roomNeeded = adultRoomCount;
                if (roomNeeded < childrenRoomCount)
                {
                    roomNeeded = childrenRoomCount;
                }
                if (roomNeeded < infantRoomCount)
                {
                    roomNeeded = infantRoomCount;
                }
                #endregion

                #region distribute
                model.RoomNeeded = new List<RoomInfo>();
                for (int i = 0; i < roomNeeded; i++)
                {
                    var roomInfo = new RoomInfo();
                    roomInfo.AdultCount = model.AdultCount / roomNeeded;
                    roomInfo.ChildrenCount = model.ChildrenCount / roomNeeded;
                    roomInfo.InfantCount = model.InfantCount / roomNeeded;
                    if (i == 0)
                    {
                        roomInfo.AdultCount += model.AdultCount % roomNeeded;
                        roomInfo.ChildrenCount += model.ChildrenCount % roomNeeded;
                        roomInfo.InfantCount += model.InfantCount % roomNeeded;
                    }
                    model.RoomNeeded.Add(roomInfo);

                }
                #endregion

                return Json(new BaseViewModel(0, model));
            }
        }

        public IActionResult Error()
        {
            var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (Request.Method == "GET")
            {
                return View(new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Error = exceptionHandler.Error
                });
            }
            else
            {
                return Json(new BaseViewModel(-500, exceptionHandler.Error));
            }
        }
    }
}
