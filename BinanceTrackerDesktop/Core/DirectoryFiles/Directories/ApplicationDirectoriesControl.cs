using BinanceTrackerDesktop.Core.DirectoryFiles.Control;

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
}
