using FluentValidation;
using ICM.Crypto.Domain.Enums;

namespace ICM.Crypto.Application.Features.BlockchainHistory;

internal sealed class GetBlockchainHistoryQueryValidator : AbstractValidator<GetBlockchainHistoryQuery>
{
    private const int MinPageSize = 1;
    private const int MaxPageSize = 50;

    private static readonly HashSet<string> SupportedCoins =
        Enum.GetValues<Coin>()
            .Select(c => c.ToString("G"))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

    private static readonly HashSet<string> SupportedChains =
        Enum.GetValues<Chain>()
            .Select(c => c.ToString("G"))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

    public GetBlockchainHistoryQueryValidator()
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
        
        RuleFor(r => r.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page number must be greater than or equal to 1.");

        RuleFor(r => r.PageSize)
            .InclusiveBetween(MinPageSize, MaxPageSize)
            .WithMessage("Page size must be between 1 and 30, inclusive.");
    }
}