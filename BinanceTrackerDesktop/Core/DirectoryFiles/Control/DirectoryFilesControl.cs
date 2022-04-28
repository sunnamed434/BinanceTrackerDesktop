using BinanceTrackerDesktop.Core.DirectoryFiles.Base;
using BinanceTrackerDesktop.Core.DirectoryFiles.Exception;
using BinanceTrackerDesktop.Core.DirectoryFiles.Extension;
using BinanceTrackerDesktop.Core.DirectoryFiles.Format;
using BinanceTrackerDesktop.Core.DirectoryFiles.Item;
using BinanceTrackerDesktop.Core.DirectoryFiles.Paths;
using System;
using System.Collections.Generic;
using System.IO;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Control.DirectoryFilesControl;

namespace BinanceTrackerDesktop.Core.DirectoryFiles.Control
{
    public sealed class DirectoryFilesControl : DirectoryFilesControlBase<DirectoryDataItem>
    {
        public override string FolderPath => ApplicationDirectoryPaths.User;

        public override IEnumerable<string> FileExtensions => new List<string>
        {
            FilesFormatExtensions.DAT,
            FilesFormatExtensions.TXT
        };

        public override IEnumerable<DirectoryDataItem> Files { get; }



        public DirectoryFilesControl()
        {
            Directory.CreateDirectory(FolderPath);

            List<DirectoryDataItem> items = new List<DirectoryDataItem>();
            foreach (string filePath in base.GetAllFilePathFromDirectory())
                items.Add(new DirectoryDataItem(filePath));

            Files = items;
        }



        public sealed class DirectoryDataItem : IDirectoryFileItem
        {
            object IDirectoryFileItem.Result => throw new NotImplementedException();

            public string FilePath { get; }

            public string FileName { get; }



            public DirectoryDataItem(string filePath)
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException(nameof(filePath));

                if (!File.Exists(filePath))
                    throw new FileNotFoundException(nameof(filePath));

                if (!filePath.IsDat())
                    throw new FileExtensionDoesNotMatchWithDesiredException(nameof(filePath));

                FileName = Path.GetFileName(filePath);
                FilePath = filePath;
            }



            TResult IDirectoryFileItem.GetResult<TResult>()
            {
                throw new NotImplementedException();
            }
        }

        public sealed class RegisteredData
        {
            public const string UserFile = "userdata";
        }
    }
}
