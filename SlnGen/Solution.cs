using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen
{
    public sealed class Solution
    {
        public string SolutionName => mSolutionName;
        readonly string mSolutionName;

        public List<CsProj> Projects => mProjects;
        private List<CsProj> mProjects { get; set; }

        public Guid SolutionGuid => mSolutionGuid;
        readonly Guid mSolutionGuid;
        public Solution(string solutionName)
        {
            mSolutionName = solutionName;
            mSolutionGuid = Guid.NewGuid();

            mProjects = new List<CsProj>();
        }

        public void AddProject(CsProj project) => mProjects.Add(project);

        public string GenerateSolutionFiles(string solutionPath)
        {
            string slnDirectoryPath = Path.Combine(solutionPath, SolutionName);
            DirectoryInfo slnDirectory = Directory.CreateDirectory(slnDirectoryPath);
            Directory.CreateDirectory(Path.Combine(slnDirectoryPath, "packages"));

            Dictionary<CsProj, string> projectWithCsProjFilePath = new Dictionary<CsProj, string>();
            foreach (CsProj csproj in Projects)
            {
                string csProjFilePath = csproj.GenerateProjectFiles(slnDirectoryPath);
                projectWithCsProjFilePath.Add(csproj, csProjFilePath);
            }

            StringBuilder slnTextBuilder = new StringBuilder();

            slnTextBuilder.AppendLine();
            slnTextBuilder.AppendLine("Microsoft Visual Studio Solution File, Format Version 12.00");
            slnTextBuilder.AppendLine("# Visual Studio 14");
            slnTextBuilder.AppendLine("VisualStudioVersion = 14.0.25420.1");
            slnTextBuilder.AppendLine("MinimumVisualStudioVersion = 10.0.40219.1");
            
            foreach (KeyValuePair<CsProj, string> projectAndCsProjFilePath in projectWithCsProjFilePath)
            {
                CsProj project = projectAndCsProjFilePath.Key;
                string relativeDirectory = projectAndCsProjFilePath.Value.Replace(String.Concat(@"\", slnDirectoryPath), String.Empty);

                slnTextBuilder.AppendLine($"Project(\"{{{SolutionGuid.ToString()}}}\") = \"{project.AssemblyName}\", \"{relativeDirectory}\", \"{{{project.AssemblyGuid.ToString()}}}");
                slnTextBuilder.AppendLine("EndProject");
            }

            slnTextBuilder.AppendLine("Global");
            slnTextBuilder.AppendLine("\tGlobalSection(SolutionConfigurationPlatforms) = preSolution");
            slnTextBuilder.AppendLine("\t\tDebug|Any CPU = Debug|Any CPU");
            slnTextBuilder.AppendLine("\t\tRelease|Any CPU = Release|Any CPU");
            slnTextBuilder.AppendLine("\tEndGlobalSection");
            slnTextBuilder.AppendLine("\tGlobalSection(ProjectConfigurationPlatforms) = postSolution");
            foreach (CsProj csproj in Projects)
            {
                slnTextBuilder.AppendLine($"\t\t{{{csproj.AssemblyGuid.ToString()}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU");
                slnTextBuilder.AppendLine($"\t\t{{{csproj.AssemblyGuid.ToString()}}}.Debug|Any CPU.Build.0 = Debug|Any CPU");
                slnTextBuilder.AppendLine($"\t\t{{{csproj.AssemblyGuid.ToString()}}}.Release|Any CPU.ActiveCfg = Release|Any CPU");
                slnTextBuilder.AppendLine($"\t\t{{{csproj.AssemblyGuid.ToString()}}}.Release|Any CPU.Build.0 = Release|Any CPU");
            }
            slnTextBuilder.AppendLine("\tEndGlobalSection");
            slnTextBuilder.AppendLine("\tGlobalSection(SolutionProperties) = preSolution");
            slnTextBuilder.AppendLine("\t\tHideSolutionNode = FALSE");
            slnTextBuilder.AppendLine("\tEndGlobalSection");
            slnTextBuilder.AppendLine("EndGlobal");

            File.WriteAllText(Path.Combine(slnDirectoryPath, String.Concat(SolutionName, ".sln")), slnTextBuilder.ToString());

            return slnDirectoryPath;
        }
    }
}
