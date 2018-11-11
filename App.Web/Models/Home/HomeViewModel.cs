using App.Web.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models.Home
{
    public class HomeViewModel
    {
        [Required(ErrorMessage = "Please enter number of adults.")]
        [Display(Name = "Adults")]
        [Range(1, Int32.MaxValue)]
        public int AdultCount { get; set; } = 1;

        [Required(ErrorMessage = "Please enter number of children.")]
        [Display(Name = "Children")]
        [Range(0, Int32.MaxValue)]
        public int ChildrenCount { get; set; }

        [Required(ErrorMessage = "Please enter number of infants.")]
        [Display(Name = "Infants")]
        [Range(0, Int32.MaxValue)]
        public int InfantCount { get; set; }

        public int MinimumAdultRequiredPerRoom { get; set; }
        public int MaximumNumberOfAdultAndChildren { get; set; }
        public int MaximumNumberOfRoomPerBooking { get; set; }
        public int MaximumNumberOfAdultPerRoom { get; set; }
        public int MaximumNumberOfChildrenPerRoom { get; set; }
        public int MaximumNumberOfInfantPerRoom { get; set; }

        public List<RoomInfo> RoomNeeded { get; set; }
    }

    public class RoomInfo
    {
        public int AdultCount { get; set; }
        public int ChildrenCount { get; set; }
        public int InfantCount { get; set; }
    }
}
