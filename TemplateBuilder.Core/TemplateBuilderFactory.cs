using System;
using System.Collections.Generic;
using System.Linq;
using TemplateBuilder.Core.Extentions;
using TemplateBuilder.Core.Interfaces;
using TemplateBuilder.Core.Models;

namespace TemplateBuilder.Core
{
    public class TemplateBuilderFactory
    {
        event EventHandler<FeedbackEventArgs> _console;
        readonly IEnumerable<IFileOperations> _fileOperations;

        public TemplateBuilderFactory(EventHandler<FeedbackEventArgs> console, IEnumerable<IFileOperations> fileOperations)
        {
            _console = console;
            _fileOperations = fileOperations;
        }

        public void Execute(string operation, string jsonConfigPath)
        {
            try
            {
                _fileOperations.FirstOrDefault(o => o.IsMatch(operation))?.Execute(jsonConfigPath);
            }
            catch (Exception ex)
            {
                _console.RaiseFeedBack($"{ex.Message}{Environment.NewLine}{ex.StackTrace}");
            }
        }

    }
}

