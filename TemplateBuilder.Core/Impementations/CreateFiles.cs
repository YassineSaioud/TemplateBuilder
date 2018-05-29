using Microsoft.Build.Evaluation;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using TemplateBuilder.Core.Interfaces;
using TemplateBuilder.Core.Models;

namespace TemplateBuilder.Core.Impementations
{
    public class CreateFiles : IFileOperations
    {
        public bool IsMatch(string operation)
        {
            return operation.Equals("-create", StringComparison.CurrentCultureIgnoreCase);
        }

        public void Execute(string templatePath)
        {
            var template = JsonConvert.DeserializeObject<Template>(File.ReadAllText(templatePath));
            if (template?.Contents != null)
            {
                var project = new Project(template.Project);
                if (project != null)
                    foreach (var content in template.Contents)
                        BuildTree(content.Parent, content, project, template.RootFullPath);
            }
        }

        #region Private Methods

        void BuildTree(string parent, Content content, Project project, string rootPath)
        {
            parent = parent.Equals(content.Parent) ? content.Parent : parent;

            var destinationDirectory = Path.Combine(project.DirectoryPath, rootPath, parent);
            if (!Directory.Exists(destinationDirectory))
                Directory.CreateDirectory(destinationDirectory);

            if (!Directory.Exists(destinationDirectory) && content.Files == null && content.Children == null)
                project.AddItem("Folder", Path.Combine(rootPath, parent));
            else
            {
                if (content.Files != null && content.Files.Any(file => !string.IsNullOrEmpty(file?.Name)))
                    foreach (var file in content.Files)
                    {
                        var destinationFile = Path.Combine(project.DirectoryPath, rootPath, parent, file.Name);
                        // Moving existing file
                        if (File.Exists(file.FullPath))
                        {
                            new FileInfo(file.FullPath).MoveTo(destinationFile);
                            project.AddItem("Content", Path.Combine(rootPath, parent, file.Name));
                        }
                        // Create new file
                        else if (!File.Exists(destinationFile))
                        {
                            File.Create(destinationFile);
                            project.AddItem("Content", Path.Combine(rootPath, parent, file.Name));
                        }
                    }
                if (content.Children != null && content.Children.Any(child => child != null))
                    foreach (var child in content.Children)
                        BuildTree(Path.Combine(parent, child.Parent), child, project, rootPath);
            }

            project.Save();
        }

        #endregion


    }
}

