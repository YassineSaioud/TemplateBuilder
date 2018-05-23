using System;

namespace TemplateBuilder.Core.Models
{
    public class FeedbackEventArgs : EventArgs
    {
        public string Data { get; }
        public FeedbackEventArgs(string data)
        {
            Data = data;
        }
    }
}
