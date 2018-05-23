using TemplateBuilder.Core.Helpers;
using TemplateBuilder.Core.Models;
using System;
using System.IO;
using System.Text;

namespace TemplateBuilder.Core.Extentions
{
    public static class Extentions
    {
        public static void RaiseFeedBack(this EventHandler<FeedbackEventArgs> obj, string data)
        {
            if (obj != null && !string.IsNullOrEmpty(data))
                obj(obj, new FeedbackEventArgs(data));
        }

        public static void NewLine(this FileStream fileStream)
        {
            var newLine = Encoding.ASCII.GetBytes(Environment.NewLine);
            fileStream.Write(newLine, 0, newLine.Length);
        }

        public static void WriteVersionScript(this FileStream fileStream, Template templateConfig)
        {
            var query = $"INSERT INTO {templateConfig.VersioningInfo.TableName}({string.Join(",", templateConfig.VersioningInfo.Fields)}) VALUES ('{templateConfig.Version}',getdate(),'Traitement Auto.')";
            var quaryBytes = Encoding.ASCII.GetBytes(query);
            fileStream.NewLine();
            fileStream.Write(quaryBytes, 0, quaryBytes.Length);
        }
    }
}
