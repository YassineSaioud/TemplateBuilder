namespace TemplateBuilder.Core.Interfaces
{
    public interface IFileOperations
    {
        bool IsMatch(string operation);
        void Execute(string jsonConfigPath);
    }
}
