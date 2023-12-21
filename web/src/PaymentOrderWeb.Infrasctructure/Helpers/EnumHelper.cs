using PaymentOrderWeb.Infrasctructure.Enums;
using PaymentOrderWeb.Infrasctructure.Extensions;

namespace PaymentOrderWeb.Infrasctructure.Helpers
{
    public static class EnumHelper
    {
        public static MonthEnum ToMonthEnum(string enumText)
        {
            enumText = enumText.ToUpper();

            if (MonthEnum.January.GetEnumDescription().Equals(enumText, StringComparison.CurrentCultureIgnoreCase)) return MonthEnum.January;
            if (MonthEnum.February.GetEnumDescription().Equals(enumText, StringComparison.CurrentCultureIgnoreCase)) return MonthEnum.February;
            if (MonthEnum.March.GetEnumDescription().Equals(enumText, StringComparison.CurrentCultureIgnoreCase)) return MonthEnum.March;
            if (MonthEnum.April.GetEnumDescription().Equals(enumText, StringComparison.CurrentCultureIgnoreCase)) return MonthEnum.April;
            if (MonthEnum.May.GetEnumDescription().Equals(enumText, StringComparison.CurrentCultureIgnoreCase)) return MonthEnum.May;
            if (MonthEnum.June.GetEnumDescription().Equals(enumText, StringComparison.CurrentCultureIgnoreCase)) return MonthEnum.June;
            if (MonthEnum.July.GetEnumDescription().Equals(enumText, StringComparison.CurrentCultureIgnoreCase)) return MonthEnum.July;
            if (MonthEnum.August.GetEnumDescription().Equals(enumText, StringComparison.CurrentCultureIgnoreCase)) return MonthEnum.August;
            if (MonthEnum.September.GetEnumDescription().Equals(enumText, StringComparison.CurrentCultureIgnoreCase)) return MonthEnum.September;
            if (MonthEnum.October.GetEnumDescription().Equals(enumText, StringComparison.CurrentCultureIgnoreCase)) return MonthEnum.October;
            if (MonthEnum.November.GetEnumDescription().Equals(enumText, StringComparison.CurrentCultureIgnoreCase)) return MonthEnum.November;
            if (MonthEnum.December.GetEnumDescription().Equals(enumText, StringComparison.CurrentCultureIgnoreCase)) return MonthEnum.December;

            throw new ArgumentException("Item not found.", enumText);
        }
    }
}
