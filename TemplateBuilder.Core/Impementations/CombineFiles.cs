using Microsoft.Build.Evaluation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TemplateBuilder.Core.Extentions;
using TemplateBuilder.Core.Interfaces;
using TemplateBuilder.Core.Models;

namespace TemplateBuilder.Core.Impementations
{
    public class CombineFiles : IFileOperations
    {
        public bool IsMatch(string operation)
        {
            return operation.Equals("-combine", StringComparison.CurrentCultureIgnoreCase);
        }

        public void Execute(string templatePath)
        {
            var inputFilePaths = new List<string>();
            var template = JsonConvert.DeserializeObject<Template>(File.ReadAllText(templatePath));
            if (template?.Contents != null)
            {
                var project = new Project(template.Project);
                if (project != null)
                {
                    // Get files from tree
                    foreach (var content in template.Contents)
                        GetFilesFromTree(content, content.Parent, project.DirectoryPath, template.RootFullPath, inputFilePaths);

                    // Making output file
                    var outputFileName = $"{template.Name}-{template.Version}.sql";
                    var outputFilePath = Path.Combine(project.DirectoryPath, template.RootFullPath, outputFileName);
                    if (!File.Exists(outputFilePath))
                    {
                        // Combine files contents into output file
                        using (var outputStream = File.Create(outputFilePath))
                        {
                            foreach (var inputFilePath in inputFilePaths)
                                using (var inputStream = File.OpenRead(inputFilePath))
                                {
                                    outputStream.NewLine();
                                    inputStream.CopyTo(outputStream);
                                    outputStream.NewLine();
                                }
                            // Write versionning info
                            outputStream.WriteVersionScript(template);
                        }
                        // Add output file to current csproj
                        project.AddItem("Content", Path.Combine(template.RootFullPath, outputFileName));
                        project.Save();
                    }
                }
            }
        }

        #region Private Mrthods

        void GetFilesFromTree(Content content, string parent, string projetctPath, string rootPath, List<string> inputFilePaths)
        {
            parent = parent.Equals(content.Parent) ? content.Parent : parent;

            var files = Directory.GetFiles(Path.Combine(projetctPath, rootPath, parent));
            if (files != null)
                inputFilePaths.AddRange(files);

            if (content.Children != null && content.Children.Any(child => child != null))
                foreach (var child in content.Children)
                    GetFilesFromTree(child, Path.Combine(parent, child.Parent), projetctPath, rootPath, inputFilePaths);
        }

        #endregion

    }
}
