using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models.Common
{
    public class BaseViewModel
    {
        public StatusInfo Status { get; private set; }
        public object Data { get; private set; }

        public BaseViewModel()
        {
            Status = new StatusInfo();
        }

        public BaseViewModel(int statusCode, object data)
        {
            SetResponse(statusCode, null);
            Data = data;
        }

        public BaseViewModel(int statusCode, Exception ex) : this(statusCode, ex.ToString())
        {
        }

        public BaseViewModel(int statusCode, string statusMsg = null) : this()
        {
            SetResponse(statusCode, statusMsg);
        }

        public void SetResponse(int statusCode, string statusMsg = null)
        {
            StatusInfo status = new StatusInfo
            {
                Code = statusCode
            };

            switch (statusCode)
            {
                case 0:
                    status.Msg = "Success";
                    break;
                case 1:
                    status.Msg = statusMsg;
                    break;
                case -500:
                    status.Msg = string.IsNullOrWhiteSpace(statusMsg) ? statusMsg : "Unknown exception";
                    break;
                default:
                    status.Msg = string.IsNullOrWhiteSpace(statusMsg) ? statusMsg : "Unknown exception";
                    break;
            }

            Status = status;
        }
    }
}
