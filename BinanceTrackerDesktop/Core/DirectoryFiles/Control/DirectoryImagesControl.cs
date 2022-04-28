using BinanceTrackerDesktop.Core.DirectoryFiles.Base;
using BinanceTrackerDesktop.Core.DirectoryFiles.Format;
using BinanceTrackerDesktop.Core.DirectoryFiles.Item;
using BinanceTrackerDesktop.Core.DirectoryFiles.Paths;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Control.DirectoryImagesControl;

namespace BinanceTrackerDesktop.Core.DirectoryFiles.Control
{
    public sealed class DirectoryImagesControl : DirectoryFilesControlBase<DirectoryImageItem>
    {
        public override string FolderPath => ApplicationDirectoryPaths.Images;

        public override IEnumerable<string> FileExtensions => new List<string>
        {
            FilesFormatExtensions.ICO,
            FilesFormatExtensions.JPG,
            FilesFormatExtensions.PNG
        };

        public override IEnumerable<DirectoryImageItem> Files { get; }



        public DirectoryImagesControl()
        {
            Directory.CreateDirectory(FolderPath);

            List<DirectoryImageItem> items = new List<DirectoryImageItem>();
            foreach (string filePath in base.GetAllFilePathFromDirectory())
                items.Add(new DirectoryImageItem(filePath));

            Files = items;
        }



        public sealed class DirectoryImageItem : IDirectoryFileItem
        {
            public object Result { get; }

            public string FileName { get; }

            public string FilePath { get; }



            public DirectoryImageItem(string filePath)
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException(nameof(filePath));

                if (File.Exists(filePath) == false)
                    throw new FileNotFoundException(nameof(filePath));

                Result = Image.FromFile(filePath);
                FileName = Path.GetFileName(filePath);
                FilePath = filePath;
            }



            public TResult GetResult<TResult>()
            {
                return (TResult)Result;
            }

            public Image GetImage()
            {
                return GetResult<Image>();
            }

            public Icon GetIcon()
            {
                return Icon.FromHandle(GetResult<Bitmap>().GetHicon());
            }
        }

        public sealed class RegisteredImages
        {
            public static readonly string ApplicationIcon = "app";
        }
    }
}
