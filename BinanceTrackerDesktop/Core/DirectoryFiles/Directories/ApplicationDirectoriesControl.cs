using BinanceTrackerDesktop.Core.DirectoryFiles.Controls.UserData;
using BinanceTrackerDesktop.Core.DirectoryFiles.Controls.Images;
using BinanceTrackerDesktop.Core.DirectoryFiles.Controls.Themes;
using BinanceTrackerDesktop.Core.DirectoryFiles.Controls.Localizations;

namespace BinanceTrackerDesktop.Core.DirectoryFiles.Directories
{
    public sealed class ApplicationDirectoriesControl
    {
        public readonly Directories Folders;



        public ApplicationDirectoriesControl()
        {
            Folders = new Directories();
        }



        public sealed class Directories
        {
            public readonly DirectoryResources Resources;



            public Directories()
            {
                Resources = new DirectoryResources();
            }



            public sealed class DirectoryResources
            {
                public readonly ImagesDirectoryFilesControl Images;

                public readonly UserDataDirectoryFilesControl User;

                public readonly ThemesDirectoryFilesControl Themes;

                public readonly LocalizationsDirectoryFilesControl Localizations;



                public DirectoryResources()
                {
                    Images = new ImagesDirectoryFilesControl();
                    User = new UserDataDirectoryFilesControl();
                    Themes = new ThemesDirectoryFilesControl();
                    Localizations = new LocalizationsDirectoryFilesControl();
                }
            }
        }
    }
}
