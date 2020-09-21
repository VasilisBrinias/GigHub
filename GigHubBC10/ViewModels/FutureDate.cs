using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace GigHubBC10.ViewModels
{
    //CUSTOM VALIDATION
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            bool isValid = DateTime.TryParseExact(Convert.ToString(value),
                "d MMM yyyy", CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out dateTime);//-->elenxw an einai mellontiki i hmerominia

            return (isValid && dateTime > DateTime.Now);
        }
    }
}