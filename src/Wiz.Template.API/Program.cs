using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using Wizco.Common.Web;

namespace Wiz.Template.API;

[ExcludeFromCodeCoverage]
public class Program
{
    /// Defines the entry point of the application.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public static void Main(string[] args)
    {
        WizcoProgramBase.CreateHostBuilder<Startup>(args).Build().Run();
    }
}
