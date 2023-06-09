using FluentValidation;

namespace Application.Covers.Commands.CreateCover;
public class CreatedCoverValidator : AbstractValidator<CreateCoverCommand>
{
    public CreatedCoverValidator()
    {
        RuleFor(c => c.StartDate)
            .LessThan(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Start date can not be in the past.");

        RuleFor(c => c)
            .Must(InsurancePeriodIsLessThenOneYear)
            .WithMessage("Total insurance period cannot exceed 1 year (365 days).");
    }

    private bool InsurancePeriodIsLessThenOneYear(CreateCoverCommand command)
    {
        return command.EndDate.DayNumber - command.StartDate.DayNumber <= 365;
    }
}
