using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace BinanceTrackerDesktop.Core.Files.API
{
    public interface IDirectoryFile<T>
    {
        string FileExtension { get; }

        string SearchPattern { get; }

        IEnumerable<T> Files { get; }



        T GetDirectoryFileAt(string name);
    }

    public interface IDirectoryImage
    {
        Icon Icon { get; }

        string FileName { get; }

        string FilePath { get; }
    }

    public class ApplicationDirectoryControl
    {
        public DirectoriesControl Directories { get; }



        public ApplicationDirectoryControl()
        {
            Directories = new DirectoriesControl();
        }



        public class DirectoriesControl
        {
            public DirectoryFileImages Images { get; }



            public DirectoriesControl()
            {
                Images = new DirectoryFileImages();
            }
        }
    }

    public abstract class DirectoryFileBase<T> : IDirectoryFile<T>
    {
        public abstract string FileExtension { get; }

        public string SearchPattern => FileSearchPatternSymbol.Asterisk + FileExtension;

        public abstract IEnumerable<T> Files { get; }



        public abstract T GetDirectoryFileAt(string name);
    }

    public class DirectoryFileImages : DirectoryFileBase<IDirectoryImage>
    {
        public override string FileExtension => ".ico";

        public override IEnumerable<IDirectoryImage> Files { get; }



        public DirectoryFileImages()
        {
            List<IDirectoryImage> icons = new List<IDirectoryImage>();

            if (!Directory.Exists(ApplicationDirectory.Icons))
                throw new DirectoryNotFoundException(nameof(ApplicationDirectory.Icons));

            string[] filesPaths = Directory.GetFiles(ApplicationDirectory.Icons, base.SearchPattern);
            for (int i = 0; i < filesPaths.Length; i++)
                icons.Add(new DirectoryImage(filesPaths[i]));

            Files = icons;
        }



        public override IDirectoryImage GetDirectoryFileAt(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            return Files.FirstOrDefault(i => i.FileName.Contains(name));
        }



        public class DirectoryImage : IDirectoryImage
        {
            public Icon Icon { get; }

            public string FileName { get; }

            public string FilePath { get; }



            public DirectoryImage(string filePath, int width = ImageSize.Width, int height = ImageSize.Height)
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException(nameof(filePath));

                if (!File.Exists(filePath))
                    throw new FileNotFoundException(nameof(filePath));

                if (!filePath.IsIcon())
                    throw new ImageIsNotIconException(nameof(filePath));

                Icon = new Icon(filePath, width, height);
                FileName = Path.GetFileName(filePath);
                FilePath = filePath;
            }
        }
    }

    public class ApplicationDirectory
    {
        public static readonly string Data = nameof(Data).ToLower();

        public static readonly string Icons = Path.Combine(Data, nameof(Icons).ToLower());
    }

    public class DirectoryIcons
    {
        public static readonly string ApplicationIcon = "app";
    }

    public class FilesExtensions
    {
        public const string Icon = ".ico";
    }

    public class FileSearchPatternSymbol
    {
        public const string Asterisk = "*";
    }

    public static class FilePathExtension
    {
        public static bool IsIcon(this string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));

            if (!File.Exists(source))
                throw new FileNotFoundException(nameof(source));

            string fileExtension = Path.GetExtension(source);

            return !string.IsNullOrEmpty(fileExtension) && fileExtension == FilesExtensions.Icon;
        }
    }

    public class ImageSize
    {
        public const int Width = 32;

        public const int Height = 32;
    }

    public class ImageIsNotIconException : Exception
    {
        public ImageIsNotIconException()
        {

        }

        public ImageIsNotIconException(string message) : base(string.Format("Image is not icon: {0}", message))
        {

        }
    }
}
