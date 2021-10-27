using BinanceTrackerDesktop.Core.Formatters.Models;
using System;
using System.Collections.Generic;

namespace BinanceTrackerDesktop.Core.Formatters.Utility
{
    public sealed class FormatterUtility<TArgument, TType> where TType : IFormatter<TArgument>
    {
        private static IList<IFormatter<TArgument>> formatters = new List<IFormatter<TArgument>>
        {

        };



        public static object Format(TArgument argument)
        {
            IFormatter<TArgument> instance = null;
            foreach (IFormatter<TArgument> formatter in formatters)
            {
                if (formatter.GetType().Equals(typeof(TType)))
                {
                    instance = formatter;
                    break;
                }
            }

            if (instance == null)
            {
                instance = (IFormatter<TArgument>)Activator.CreateInstance(typeof(TType));
                formatters.Add(instance);
            }

            return instance.Format(argument);
        }
    }
}
