using System.Reflection;

namespace BinanceTrackerDesktop.Core.Themes.Attributes.Utilities
{
    public sealed class CustomAttributesUtility
    {
        public static IEnumerable<TAttribute> GetCustomAttributesFromTypeMembers<TAttribute>(BindingFlags? flags)
            where TAttribute : Attribute
        {
            if (flags.HasValue == false)
            {
                throw new ArgumentNullException(nameof(flags));
            }

            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                foreach (MemberInfo memberInfo in type.GetMembers(flags.Value))
                {
                    TAttribute attribute = null;
                    if ((attribute = memberInfo.GetCustomAttribute<TAttribute>(false)) != null)
                    {
                        yield return attribute;
                    }
                }
            }
        }
    }
}
