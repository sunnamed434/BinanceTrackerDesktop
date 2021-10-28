using BinanceTrackerDesktop.Core.Formatters.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BinanceTrackerDesktop.Core.Formatters.Utility
{
    public sealed class FormatterUtility<TArgument, TFormatterType> where TFormatterType : IFormatter<TArgument>
    {
        private static IList<IFormatter<TArgument>> formatters = new List<IFormatter<TArgument>>
        {

        };


        
        public static object Format(TArgument argument)
        {
            IFormatter<TArgument> instance = formatters.FirstOrDefault(f => f.GetType().Equals(typeof(TFormatterType)));
            if (instance == null)
            {
                instance = (IFormatter<TArgument>)Activator.CreateInstance(typeof(TFormatterType));
                formatters.Add(instance);
            }

            return instance.Format(argument);
        }
    }
}
