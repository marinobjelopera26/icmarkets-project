using FluentValidation;
using ICM.Crypto.Domain.Enums;

namespace ICM.Crypto.Application.Features.GetLatestSnapshot;

internal sealed class GetLatestSnapshotQueryValidator : AbstractValidator<GetLatestSnapshotQuery>
{
    private static readonly HashSet<string> SupportedCoins =
        Enum.GetValues<Coin>()
            .Select(c => c.ToString("G"))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

    private static readonly HashSet<string> SupportedChains =
        Enum.GetValues<Chain>()
            .Select(c => c.ToString("G"))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

    public GetLatestSnapshotQueryValidator()
    {
        RuleFor(query => query.Coin)
            .NotEmpty()
            .WithMessage("Coin identifier is required.")
            .Must(coin => SupportedCoins.Contains(coin))
            .WithMessage(r =>
                $"Coin identifier '{r.Coin}' is not supported. Supported coins are: {string.Join(", ", SupportedCoins)}");
        
        RuleFor(r => r.Chain)
            .NotEmpty()
            .WithMessage("Chain identifier is required.")
            .Must(chain => SupportedChains.Contains(chain))
            .WithMessage(r =>
                $"Chain identifier '{r.Chain}' is not supported. Supported chains are: {string.Join(", ", SupportedChains)}");
    }
}