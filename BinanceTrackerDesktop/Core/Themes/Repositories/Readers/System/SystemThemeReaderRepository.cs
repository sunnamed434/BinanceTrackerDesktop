﻿using BinanceTrackerDesktop.Core.Themes.Models;
using BinanceTrackerDesktop.Core.Themes.Recognizers;
using BinanceTrackerDesktop.Core.Themes.Recognizers.Windows;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Dark;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Exceptions;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Light;

namespace BinanceTrackerDesktop.Core.Themes.Repositories.Readers.System
{
    public sealed class SystemThemeDataRepository : IThemeDataRepository
    {
        public SystemThemeDataRepository(ISystemThemeRecognizer themeRecognizer)
        {
            ThemeRecognizer = themeRecognizer ?? throw new ArgumentNullException(nameof(themeRecognizer));
        }



        public ISystemThemeRecognizer ThemeRecognizer { get; }



        public IEnumerable<ThemeData> GetThemeData()
        {
            ISystemThemeRecognizer themeRecognizer = new WindowsSystemThemeRecognizer();
            Theme theme = themeRecognizer.RecognizeTheme();

            return theme switch
            {
                Theme.Light => new LightThemeReaderRepository().GetThemeData(),
                Theme.Dark  => new DarkThemeReaderRepository().GetThemeData(),
                _ => throw new ThemeCannotBeRecognizedException(),
            };
        }
    }
}