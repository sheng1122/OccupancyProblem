using App.Contract.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Contract.Constants
{
    public class ErrorCode
    {
        public static readonly ErrorDTO MinimumNumberAdultRequired = new ErrorDTO
        {
            Code = "E01",
            Description = "Minimum {amount} of adult is required per room."
        };

        public static readonly ErrorDTO NumberOfGuestExceedLimit = new ErrorDTO
        {
            Code = "E01",
            Description = "Maximum number of guest allowed for a booking is {amount}."
        };

        public static readonly ErrorDTO AdultSupervisionIsRequired = new ErrorDTO
        {
            Code = "E02",
            Description = "Every {childrenAmount} children or {infantAmount} infants required 1 adult supervision."
        };

        public static readonly ErrorDTO NumberOfRoomExceedLimit = new ErrorDTO
        {
            Code = "E03",
            Description = "Maximum number of room allowed per booking is {amount}."
        };
    }
}
