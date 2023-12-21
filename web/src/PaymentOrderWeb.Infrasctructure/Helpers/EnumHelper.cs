using PaymentOrderWeb.Infrasctructure.Enums;
using PaymentOrderWeb.Infrasctructure.Extensions;

namespace PaymentOrderWeb.Infrasctructure.Helpers
{
    public static class EnumHelper
    {
        public static MonthEnum ToMonthEnum(string enumText)
        {
            enumText = enumText.ToUpper();

            if (MonthEnum.January.GetEnumDescription().ToUpper() == enumText) return MonthEnum.January;
            if (MonthEnum.February.GetEnumDescription().ToUpper() == enumText) return MonthEnum.February;
            if (MonthEnum.March.GetEnumDescription().ToUpper() == enumText) return MonthEnum.March;
            if (MonthEnum.April.GetEnumDescription().ToUpper() == enumText) return MonthEnum.April;
            if (MonthEnum.May.GetEnumDescription().ToUpper() == enumText) return MonthEnum.May;
            if (MonthEnum.June.GetEnumDescription().ToUpper() == enumText) return MonthEnum.June;
            if (MonthEnum.July.GetEnumDescription().ToUpper() == enumText) return MonthEnum.July;
            if (MonthEnum.August.GetEnumDescription().ToUpper() == enumText) return MonthEnum.August;
            if (MonthEnum.September.GetEnumDescription().ToUpper() == enumText) return MonthEnum.September;
            if (MonthEnum.October.GetEnumDescription().ToUpper() == enumText) return MonthEnum.October;
            if (MonthEnum.November.GetEnumDescription().ToUpper() == enumText) return MonthEnum.November;
            if (MonthEnum.December.GetEnumDescription().ToUpper() == enumText) return MonthEnum.December;

            throw new ArgumentException("Item not found.", enumText);
        }
    }
}
