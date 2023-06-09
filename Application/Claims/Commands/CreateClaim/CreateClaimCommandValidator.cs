using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Claims.Commands.CreateClaim;
public class CreateClaimCommandValidator : AbstractValidator<CreateClaimCommand>
{
    private readonly IClaimsDbContext _dbContext;

    public CreateClaimCommandValidator(IClaimsDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(c => c.DamageCost)
            .LessThanOrEqualTo(100_000m).WithMessage("DamageCost cannot exceed 100.000.");

        RuleFor(claim => claim)
            .Must(CreatedDateBeInTheRangeOFStartAndEndDates)
            .WithMessage("The claim's CreatedDate is outside the valid range of the associated Cover's StartDate and EndDate.");
    }

    private bool CreatedDateBeInTheRangeOFStartAndEndDates(CreateClaimCommand command)
    {
        var cover = _dbContext.Covers.First(c => c.Id == command.CoverId);

        DateOnly claimCreatedDate = DateOnly.FromDateTime(command.Created);

        return claimCreatedDate >= cover.StartDate && claimCreatedDate <= cover.EndDate;
    }
}
