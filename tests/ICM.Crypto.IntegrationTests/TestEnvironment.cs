using DotNet.Testcontainers.Containers;

namespace ICM.Crypto.IntegrationTests;

internal static class TestEnvironment
{
    public static string ApiBaseUrl { get; set; } = string.Empty;
    public static IContainer Api { get; set; } = null!;
    public static IContainer Database { get; set; } = null!;
}