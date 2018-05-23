using TemplateBuilder.Core.Models;
using static System.Console;
using System;

namespace TemplateBuilder.Core.Helpers
{
    public static class ConsoleHelper
    {
        public static void WriteMessage(object sender, FeedbackEventArgs data)
        {
            Write("{0:yyyy-MM-d HH:mm:ss} - {1}\n", DateTime.Now, data.Data);
            ReadKey(true);
        }
    }
}
