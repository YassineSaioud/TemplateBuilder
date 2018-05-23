using TemplateBuilder.Core;
using static TemplateBuilder.Console.Ioc.DependencyResolver;

namespace TemplateBuilder.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            Resolve<TemplateBuilderFactory>().Execute(args[0], args[1]);
        }
    }
}
