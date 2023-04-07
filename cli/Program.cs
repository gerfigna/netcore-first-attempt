using System;
using System.Reflection;
using System.Runtime.InteropServices;
using CommandLine;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Signaturit.Lawsuit.Domain.Service;
using Signaturit.Lawsuit.Application.Query.TrialWinner;

class Program
{

    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(opts => RunOptions(opts))
            .WithNotParsed<Options>((errs) => HandleParseError(errs));

    }

    static void RunOptions(Options opts)
    {
        if (opts.Verbose)
        {
            Console.WriteLine($"Verbose mode is {(opts.Verbose ? "on" : "off")}");
            Console.WriteLine($"Plaintiff signatures are {opts.Plaintiff}");
            Console.WriteLine($"Defendant signatures are {opts.Defendant}");
        }

        var serviceCollection = new ServiceCollection()
            //.AddMediatR(Assembly.GetExecutingAssembly())
            .AddMediatR(AppDomain.CurrentDomain.GetAssemblies())
            .AddScoped<ContractPartScorer>()
            .BuildServiceProvider()
        ;

        var mediator = serviceCollection.GetRequiredService<IMediator>();
        var query = new GetTrialWinnerQuery($"{opts.Plaintiff}", $"{opts.Defendant}");

        Task<GetTrialWinnerQueryResponse> task = mediator.Send(query);

        task.Wait(1000);

        Console.WriteLine(task.Result.Message);


    }

    static void HandleParseError(IEnumerable<Error> errs)
    {
        foreach (var err in errs)
        {
            Console.WriteLine(err.ToString());
        }
    }
}

class Options
{
    [Option('v', "verbose", Required = false, HelpText = "Prints verbose messages to console.")]
    public bool Verbose { get; set; }

    [Value(0, MetaName = "Plaintiff", HelpText = "Plaintiff signatures")]
    public string? Plaintiff { get; set; }

    [Value(1, MetaName = "Defendant", HelpText = "Defendant signatures")]
    public string? Defendant { get; set; }

}
