using PaymentOrderWeb.Domain.Entities;

namespace PaymentOrderWeb.Domain.Extensions
{
    public static class EmployeeDataExtension
    {
        public static TimeSpan TotalLunchHour(this EmployeeData source)
        {
            var teste = source.LunchTime.Split(" - ");
            var fa = teste[0].Split(":");

            var tsts = Convert.ToInt16(fa[0]);
            var tsts1 = Convert.ToInt16(fa[1]);
            var time = new TimeSpan(tsts, tsts1, 0);

            var fa3 = teste[1].Split(":");
            var tsts2 = Convert.ToInt16(fa3[0]);
            var tsts3 = Convert.ToInt16(fa3[1]);
            var time2 = new TimeSpan(tsts2, tsts3, 0);

            var total = time2 - time;

            return total;
        }

        public static TimeSpan TotalHoursWorked(this EmployeeData source)
        {
            var worked = source.OutputTime - source.EntryTime;
            return worked - source.TotalLunchHour();
        }

        public static double TotalDay(this EmployeeData source)
        {
            return source.TotalHoursWorked().TotalHours * source.HourlyRate;
        }

        public static TimeSpan TotalDiscountDay(this EmployeeData source)
        {
            var total = TimeSpan.Zero;
            if (source.IsBusinessDay())
            {
                var dailyWorkload = TimeSpan.FromHours(8);
                if (source.TotalHoursWorked() < dailyWorkload) total = dailyWorkload - source.TotalHoursWorked();
            }
            return total;
        }

        public static TimeSpan TotalExtraDay(this EmployeeData source)
        {
            var total = TimeSpan.Zero;
            if (source.IsBusinessDay())
            {
                var dailyWorkload = TimeSpan.FromHours(8);
                if (source.TotalHoursWorked() > dailyWorkload) total = source.TotalHoursWorked() - dailyWorkload;
            }
            return total;
        }

        public static bool IsBusinessDay(this EmployeeData source)
        {
            return !(source.Date.DayOfWeek == DayOfWeek.Saturday || source.Date.DayOfWeek == DayOfWeek.Sunday);
        }
    }
}
