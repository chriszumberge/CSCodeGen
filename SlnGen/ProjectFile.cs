using CSCodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen
{
    public class ProjectFile
    {
        public string FileName => mFileName;
        readonly string mFileName;

        public string FileExtension => mFileExtension;
        readonly string mFileExtension;

        public bool ShouldCompile => mShouldCompile;
        readonly bool mShouldCompile;

        public bool IsContent => mIsContent;
        readonly bool mIsContent;

        public string FileContents => mFileContents;
        protected string mFileContents { get; set; }

        public ProjectFile(string fileName, string fileExtension, bool shouldCompile, bool isContent = false, string fileContents = null)
        {
            mFileName = fileName;
            mFileExtension = fileExtension;
            mShouldCompile = shouldCompile;
            mIsContent = isContent;

            if (String.IsNullOrEmpty(fileContents))
            {
                mFileContents = String.Empty;
            }
            else
            {
                mFileContents = fileContents;
            }
        }

        public ProjectFile(CGFile file)
        {
            mFileName = file.FileName;
            mFileExtension = file.FileExtension;
            mShouldCompile = true;
            mIsContent = false;
            mFileContents = file.ToString();
        }

        public ProjectFile(CGFile file, bool shouldCompile, bool isContent)
        {
            mFileName = file.FileName;
            mFileExtension = file.FileExtension;
            mShouldCompile = shouldCompile;
            mIsContent = isContent;
            mFileContents = file.ToString();
        }

        public string GetFileSystemName() => String.Concat(FileName, ".", FileExtension);
    }
}
