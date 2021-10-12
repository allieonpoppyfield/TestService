using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestService.Models
{
    public class Request
    {
        [Required]
        public long AccountId { get; set; }

        [Required]
        public PeriodType PeriodType { get; set; }


        public List<SelectListItem> GetExistsPeriodTypes()
        {
            var result = new List<SelectListItem>()
            {
                new SelectListItem { Value = "1", Text = "Год" },
                new SelectListItem { Value = "2", Text = "Месяц" },
                new SelectListItem { Value = "3", Text = "Квартал" },
            };

            return result;
        }
    }
}
