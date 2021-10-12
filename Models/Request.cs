using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestService.Models
{
    public class Request
    {
        [AccountIdValidation]
        public long? AccountId { get; set; }

        [Required(ErrorMessage = "Нужно выбрать тип периода")]
        public PeriodType? PeriodType { get; set; }

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
    public class AccountIdValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null ||!int.TryParse(value.ToString(), out int _accountId))
            {
                return new ValidationResult("Введено некорректное значение ID");
            }

            var balanceFileParh = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "balance.json");
            using StreamReader r = new StreamReader(balanceFileParh);
            string json = r.ReadToEnd();
            var resultList = JsonConvert.DeserializeObject<BalanceList>(json).Balances;

            if (resultList.FirstOrDefault(x => x.AccountID == _accountId) == null)
            {
                return new ValidationResult("Аккаунта с таким ID не существует");
            }

            return ValidationResult.Success;
        }
    }

}
