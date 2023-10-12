using Microsoft.Extensions.Options;

namespace Nexusat.AspNetCore.Configuration;

/// <summary>
/// The system.
/// </summary>
public sealed class NexusatAspNetCoreSystem
{
    private IOptions<NexusatAspNetCoreOptions> IOptionOptions { get; }

    public NexusatAspNetCoreOptions Settings { get => IOptionOptions.Value; }

    internal NexusatAspNetCoreSystem(IOptions<NexusatAspNetCoreOptions> options)
    {
        IOptionOptions = options;
    }
}