using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PaymentOrderWeb.Infrasctructure.Extensions
{
    public static class DatetimeExtension
    {
        public static int DaysInMonth(this DateTime source)
        {
            return DateTime.DaysInMonth(source.Year, source.Month);
        }
        public static bool IsBusinessDay(this DateTime source)
        {
            return !(source.Date.DayOfWeek == DayOfWeek.Saturday || source.Date.DayOfWeek == DayOfWeek.Sunday);
        }

        public static bool IsBusinessDay(this DateOnly source)
        {
            return !(source.DayOfWeek == DayOfWeek.Saturday || source.DayOfWeek == DayOfWeek.Sunday);
        }

        public static int TotalBusinessDaysInMonth(this DateTime source)
        {
            var PrimeiroDiadoMes = new DateTime(source.Year, source.Month, 1);
            var UltimoDiadoMes = new DateTime(source.Year, source.Month, source.DaysInMonth());

            int days = 0;

            while (PrimeiroDiadoMes.Date <= UltimoDiadoMes.Date)
            {
                if (PrimeiroDiadoMes.DayOfWeek != DayOfWeek.Saturday
                   && PrimeiroDiadoMes.DayOfWeek != DayOfWeek.Sunday)
                    days++;

                PrimeiroDiadoMes = PrimeiroDiadoMes.AddDays(1);
            }

            return days;
        }
    }
}
