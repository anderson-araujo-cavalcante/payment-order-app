using PaymentOrderWeb.Domain.Entities;
using PaymentOrderWeb.Infrasctructure.Extensions;
using System;

namespace PaymentOrderWeb.Domain.Extensions
{
    public static class EmployeeDataExtension
    {
        public static TimeSpan TotalTimeLunchHour(this EmployeeData source)
        {
            var hours = source.LunchTime.Split(" - ");
            var outForLunch = FormatHour(hours[0].Split(":"));
            var returnFromLunch = FormatHour(hours[1].Split(":"));
            return returnFromLunch - outForLunch;
        }

        private static TimeSpan FormatHour(string[] hours)
        {
            var hour = Convert.ToInt16(hours[0]);
            var minute = Convert.ToInt16(hours[1]);
            return new TimeSpan(hour, minute, 0);
        }

        public static TimeSpan TotalHoursWorked(this EmployeeData source)
        {
            var worked = source.OutputTime - source.EntryTime;
            return worked - source.TotalTimeLunchHour();
        }

        public static double TotalValueDay(this EmployeeData source)
        {
            return source.TotalHoursWorked().TotalHours * source.HourlyRate;
        }

        public static TimeSpan TotalTimeDiscountDay(this EmployeeData source)
        {
            var total = TimeSpan.Zero;
            if (source.Date.IsBusinessDay())
            {
                var dailyWorkload = TimeSpan.FromHours(8);
                if (source.TotalHoursWorked() < dailyWorkload) total = dailyWorkload - source.TotalHoursWorked();
            }
            return total;
        }

        public static TimeSpan TotalTimeExtraDay(this EmployeeData source)
        {
            var total = TimeSpan.Zero;
            if (source.Date.IsBusinessDay())
            {
                var dailyWorkload = TimeSpan.FromHours(8);
                if (source.TotalHoursWorked() > dailyWorkload) total = source.TotalHoursWorked() - dailyWorkload;
            }
            return total;
        }
    }
}
