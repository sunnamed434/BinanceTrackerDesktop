using System.Reflection;

namespace BinanceTrackerDesktop.Core.Themes.Attributes.Utilities
{
    public sealed class CustomAttributesUtility
    {
        public static IEnumerable<(TAttribute, TFieldCastType)> GetCustomAttributesFromFields<TAttribute, TFieldCastType>(BindingFlags? flags)
            where TAttribute : Attribute
            where TFieldCastType : class
        {
            if (flags.HasValue == false)
            {
                throw new ArgumentNullException(nameof(flags));
            }

            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                foreach (FieldInfo fieldInfo in type.GetFields(flags.Value))
                {
                    TAttribute attribute = null;
                    if ((attribute = fieldInfo.GetCustomAttribute<TAttribute>(false)) != null)
                    {
                        yield return (null, null);
                    }
                }
            }
        }
    }
}
