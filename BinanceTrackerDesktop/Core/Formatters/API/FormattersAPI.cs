﻿using BinanceTrackerDesktop.Core.Currencies;
using System;
using System.Drawing;
using System.Text;
using static BinanceTrackerDesktop.Core.Formatters.API.UserBalanceLosesFormatter;

namespace BinanceTrackerDesktop.Core.Formatters.API
{
    public interface IFormatter<TResult, TArgument>
    {
        TResult Format(TArgument value);
    }

    public class CurrencyFormatter : IFormatter<string, decimal>
    {
        private const string DefaultFormat = "0.00";



        public string Format(decimal value)
        {
            return new StringBuilder()
                .Append(CurrencySymbol.EUR)
                .Append(value.ToString(DefaultFormat))
                .ToString();
        }
    }

    public class CryptocurrencyFormatter : IFormatter<string, string>
    {
        public string Format(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            return new StringBuilder()
                .Append(content)
                .Append(CurrencyName.EUR)
                .ToString();
        }
    }

    public class UserBalanceLosesFormatter : IFormatter<Color, BinanceUserBalanceLossesOptions>
    {
        public Color Format(BinanceUserBalanceLossesOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (options.BestBalance == decimal.Zero)
                return Color.Gray;
            else if (options.BestBalance > options.TotalBalance)
                return Color.Red;
            else if (options.BestBalance < options.TotalBalance)
                return Color.Green;
            else 
                return Color.Gray;
        }



        public class BinanceUserBalanceLossesOptions
        {
            public readonly decimal TotalBalance;

            public readonly decimal BestBalance;



            public BinanceUserBalanceLossesOptions(decimal totalBalance, decimal bestBalance)
            {
                TotalBalance = totalBalance;
                BestBalance = bestBalance;
            }
        }
    }
}
