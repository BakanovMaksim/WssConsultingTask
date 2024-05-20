using System.ComponentModel;

namespace WssConsultingTask.Application.Extensions
{
    public static class EnumExtensions
    {
        public static string ToDescription<TEnum>(this TEnum enumValue)
            where TEnum : struct
        {
            return GetEnumDescription((Enum)(object)enumValue);
        }

        private static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
