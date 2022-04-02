﻿using GitVersion.Infrastructure;

namespace GitVersion.Show;

[Command("show", typeof(ConfigSettings), "Shows the effective configuration.")]
public class ConfigShowCommand : Command<ConfigShowSettings>
{
    private readonly ILogger logger;
    private readonly IService service;

    public ConfigShowCommand(ILogger logger, IService service)
    {
        this.logger = logger;
        this.service = service;
    }

    public override Task<int> InvokeAsync(ConfigShowSettings settings)
    {
        var value = service.Call();
        logger.LogInformation($"Command : 'config show', LogFile : '{settings.LogFile}', WorkDir : '{settings.WorkDir}' ");
        return Task.FromResult(value);
    }
}