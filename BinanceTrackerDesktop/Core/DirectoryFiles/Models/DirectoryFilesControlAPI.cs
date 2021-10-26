﻿using BinanceTrackerDesktop.Core.DirectoryFiles.Exception;
using BinanceTrackerDesktop.Core.DirectoryFiles.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Models.DirectoryDataControl;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Models.DirectoryImagesControl;

namespace BinanceTrackerDesktop.Core.DirectoryFiles.Models
{
    public interface IDirectoryFilesControl<TFileItem> where TFileItem : IDirectoryFileItem
    {
        string FolderPath { get; }

        string FileExtension { get; }

        string SearchPattern { get; }

        IEnumerable<TFileItem> Files { get; }



        TFileItem GetDirectoryFileAt(string name);

        IEnumerable<string> GetAllFilePathFromDirectory();
    }

    public interface IDirectoryFileItem
    {
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

                public readonly DirectoryDataControl User;



                public DirectoryOfResources()
                {
                    Images = new DirectoryImagesControl();
                    User = new DirectoryDataControl();
                }
            }
        }
    }

    public abstract class DirectoryFilesControlBase<T> : IDirectoryFilesControl<T> where T : IDirectoryFileItem
    {
        public abstract string FolderPath { get; }

        public virtual string FileExtension { get; } = string.Empty;

        public abstract IEnumerable<T> Files { get; }

        public string SearchPattern { get; }



        public DirectoryFilesControlBase()
        {
            SearchPattern = new StringBuilder()
                .Append(FileSearchPatternSymbol.Asterisk)
                .Append(FileExtension)
                .ToString();
        }



        public virtual T GetDirectoryFileAt(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            return Files.FirstOrDefault(i => i.FileName.Contains(name));
        }

        public virtual IEnumerable<string> GetAllFilePathFromDirectory()
        {
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            string[] filesPaths = Directory.GetFiles(FolderPath, SearchPattern);
            for (int i = 0; i < filesPaths.Length; i++)
                yield return filesPaths[i];
        }
    }

    public sealed class DirectoryImagesControl : DirectoryFilesControlBase<DirectoryImageItem>
    {
        public override string FolderPath => ApplicationDirectoryPaths.Icons;

        public override string FileExtension => FileExtensions.Icon;

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
            public Icon Icon { get; }

            public string FileName { get; }

            public string FilePath { get; }



            public DirectoryImageItem(string filePath, int width = DefaultSizeOfImages.Width, int height = DefaultSizeOfImages.Height)
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException(nameof(filePath));

                if (!File.Exists(filePath))
                    throw new FileNotFoundException(nameof(filePath));

                if (!filePath.IsIcon())
                    throw new FileExtensionDoesNotMatchWithDesiredException(nameof(filePath));

                Icon = new Icon(filePath, width, height);
                FileName = Path.GetFileName(filePath);
                FilePath = filePath;
            }



            public sealed class DefaultSizeOfImages
            {
                public const int Width = 32;

                public const int Height = 32;
            }
        }

        public sealed class RegisteredImages
        {
            public static readonly string ApplicationIcon = "app";
        }
    }

    public sealed class DirectoryDataControl : DirectoryFilesControlBase<DirectoryDataItem>
    {
        public override string FolderPath => ApplicationDirectoryPaths.User;
        
        public override string FileExtension => FileExtensions.Dat;

        public override IEnumerable<DirectoryDataItem> Files { get; }



        public DirectoryDataControl()
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

        public static readonly string Icons = Path.Combine(Resources, nameof(Icons));
    }

    public sealed class FileExtensions
    {
        public const string Icon = ".ico";

        public const string Dat = ".dat";
    }

    public sealed class FileSearchPatternSymbol
    {
        public const string Asterisk = "*";
    }
}