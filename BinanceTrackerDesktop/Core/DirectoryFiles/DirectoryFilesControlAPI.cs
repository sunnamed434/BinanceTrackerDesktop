using BinanceTrackerDesktop.Core.DirectoryFiles.Exception;
using BinanceTrackerDesktop.Core.DirectoryFiles.Extension;
using BinanceTrackerDesktop.Core.DirectoryFiles.Format;
using BinanceTrackerDesktop.Core.DirectoryFiles.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using static BinanceTrackerDesktop.Core.DirectoryFiles.DirectoryFilesControl;
using static BinanceTrackerDesktop.Core.DirectoryFiles.DirectoryImagesControl;

namespace BinanceTrackerDesktop.Core.DirectoryFiles
{
    public interface IDirectoryFilesControl<TFileItem> where TFileItem : IDirectoryFileItem
    {
        string FolderPath { get; }

        IEnumerable<TFileItem> Files { get; }

        IEnumerable<string> FileExtensions { get; }



        TFileItem GetDirectoryFileAt(string name);

        IEnumerable<string> GetAllFilePathFromDirectory();
    }

    public interface IDirectoryFileItem
    {
        object Result { get; }

        string FilePath { get; }

        string FileName { get; }
    }

    public sealed class ApplicationDirectoriesControl
    {
        public readonly Directories Folders;



        public ApplicationDirectoriesControl()
        {
            Folders = new Directories();
        }



        public sealed class Directories
        {
            public readonly DirectoryOfResources Resources;



            public Directories()
            {
                Resources = new DirectoryOfResources();
            }



            public sealed class DirectoryOfResources
            {
                public readonly DirectoryImagesControl Images;

                public readonly DirectoryFilesControl User;



                public DirectoryOfResources()
                {
                    Images = new DirectoryImagesControl();
                    User = new DirectoryFilesControl();
                }
            }
        }
    }

    public abstract class DirectoryFilesControlBase<TDirectoryFileItem> : IDirectoryFilesControl<TDirectoryFileItem> where TDirectoryFileItem : IDirectoryFileItem
    {
        public abstract string FolderPath { get; }

        public abstract IEnumerable<TDirectoryFileItem> Files { get; }

        public virtual IEnumerable<string> FileExtensions => new List<string>();



        public virtual TDirectoryFileItem GetDirectoryFileAt(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            return Files.FirstOrDefault(i => i.FileName.Contains(name));
        }

        public virtual IEnumerable<string> GetAllFilePathFromDirectory()
        {
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            string[] filesPaths = Directory.GetFiles(FolderPath);
            for (int i = 0; i < filesPaths.Length; i++)
                if (FilePathUtility.TryGetExtensionOf(filesPaths[i], out string extension) && FileExtensions.Contains(extension))
                    yield return filesPaths[i];
        }
    }

    public sealed class DirectoryImagesControl : DirectoryFilesControlBase<DirectoryImageItem>
    {
        public override string FolderPath => ApplicationDirectoryPaths.Images;

        public override IEnumerable<string> FileExtensions => new List<string>
        {
            FilesFormatExtensions.Icon,
            FilesFormatExtensions.JPG,
            FilesFormatExtensions.PNG
        };

        public override IEnumerable<DirectoryImageItem> Files { get; }



        public DirectoryImagesControl()
        {
            if (!Directory.Exists(FolderPath))
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

                if (!File.Exists(filePath))
                    throw new FileNotFoundException(nameof(filePath));

                Result = filePath.IsIcon() ? new Icon(filePath) : Image.FromFile(filePath);
                FileName = Path.GetFileName(filePath);
                FilePath = filePath;
            }
        }

        public sealed class RegisteredImages
        {
            public static readonly string ApplicationIcon = "app";
        }
    }

    public sealed class DirectoryFilesControl : DirectoryFilesControlBase<DirectoryDataItem>
    {
        public override string FolderPath => ApplicationDirectoryPaths.User;

        public override IEnumerable<string> FileExtensions => new List<string> 
        { 
            FilesFormatExtensions.Dat,
            FilesFormatExtensions.TXT
        };

        public override IEnumerable<DirectoryDataItem> Files { get; }



        public DirectoryFilesControl()
        {
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            List<DirectoryDataItem> items = new List<DirectoryDataItem>();
            foreach (string filePath in base.GetAllFilePathFromDirectory())
                items.Add(new DirectoryDataItem(filePath));

            Files = items;
        }



        public sealed class DirectoryDataItem : IDirectoryFileItem
        {
            public object Result => default;

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
        }

        public sealed class RegisteredData
        {
            public const string UserFile = "userdata";
        }
    }

    public sealed class ApplicationDirectoryPaths
    {
        public static readonly string Resources = nameof(Resources);

        public static readonly string User = Path.Combine(Resources, nameof(User));

        public static readonly string Images = Path.Combine(Resources, nameof(Images));
    }
}
