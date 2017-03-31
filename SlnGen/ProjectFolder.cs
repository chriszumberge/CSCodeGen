using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen
{
    public sealed class ProjectFolder : IFileContainer
    {
        public string FolderName => mFolderName;
        readonly string mFolderName;

        public List<ProjectFile> Files => mFiles;
        private List<ProjectFile> mFiles { get; set; }

        public List<ProjectFolder> Folders => mFolders;
        private List<ProjectFolder> mFolders { get; set; }

        public ProjectFolder(string folderName)
        {
            mFolderName = folderName;
            mFiles = new List<ProjectFile>();
            mFolders = new List<ProjectFolder>();
        }

        List<ProjectFile> IFileContainer.GetFiles() => mFiles;

        void IFileContainer.AddFile(ProjectFile file) => mFiles.Add(file);

        List<ProjectFolder> IFileContainer.GetFolders() => mFolders;

        void IFileContainer.AddFolder(ProjectFolder folder) => mFolders.Add(folder);
    }
}
