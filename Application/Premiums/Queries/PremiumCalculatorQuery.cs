using Application.Covers.Dto;
using Domain.Enums;
using MediatR;

namespace Application.Premiums.Queries;
public record PremiumCalculatorQuery(DateOnly StartDate, DateOnly EndDate, CoverTypeDto CoverType) : IRequest<decimal>;

public class PremiumCalculatorQueryHandler : IRequestHandler<PremiumCalculatorQuery, decimal>
{
    private readonly PremiumCalculator _premiumCalculator;

    public PremiumCalculatorQueryHandler(PremiumCalculator premiumCalculator)
    {
        _premiumCalculator = premiumCalculator;
    }

    public async Task<decimal> Handle(PremiumCalculatorQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_premiumCalculator.ComputePremium(request.StartDate, request.EndDate, (CoverType)request.CoverType));
    }
}