﻿using System;
using System.Linq;
using GitVersion;
using GitVersion.Calculation;
using GitVersion.Configuration;
using GitVersion.Extensions;
using GitVersion.Infrastructure;
using GitVersion.Normalization;
using GitVersion.Output;

// TODO: load the list of assemblies from the app working directory, later we might load from nuget
var assemblies = new[]
{
    typeof(CommonModule).Assembly,
    typeof(NormalizeModule).Assembly,
    typeof(CalculateModule).Assembly,
    typeof(ConfigModule).Assembly,
    typeof(OutputModule).Assembly,
    typeof(CliModule).Assembly
};

var gitVersionModules = assemblies
    .SelectMany(a => a.GetTypes().Where(TypeIsGitVersionModule))
    .Select(t => (IGitVersionModule)Activator.CreateInstance(t)!)
    .ToList();

using var serviceProvider = new ContainerRegistrar()
    .RegisterModules(gitVersionModules)
    .AddLogging(args)
    .Build();

var app = serviceProvider.GetService<GitVersionApp>();

var result = await app!.RunAsync(args);

if (!Console.IsInputRedirected) Console.ReadKey();

return result;

static bool TypeIsGitVersionModule(Type type)
{
    return typeof(IGitVersionModule).IsAssignableFrom(type) &&
           !type.IsInterface &&
           !type.IsAbstract;
}