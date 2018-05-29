using System.Collections.Generic;
using System.IO;

namespace TemplateBuilder.Core.Models
{
    public class Template
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Root { get; set; }
        public string Project { get; set; }
        public IEnumerable<Content> Contents { get; set; }
        public VersioningInfo VersioningInfo { get; set; }
        public string RootFullPath => Path.Combine(Root, $"{Name}-{Version}");
    }

    public class Content
    {
        public string Parent { get; set; }
        public IEnumerable<ContentFile> Files { get; set; }
        public IEnumerable<Content> Children { get; set; }
    }

    public class ContentFile
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string FullPath => System.IO.Path.Combine(Path, Name);
    }

    public class VersioningInfo
    {
        public string TableName { get; set; }
        public IEnumerable<string> Fields { get; set; }
    }

}


