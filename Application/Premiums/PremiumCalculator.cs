using Domain.Enums;

namespace Application.Premiums;

/// <summary>
/// Class for calculating premiums
/// </summary>
public class PremiumCalculator
{
    private const decimal BaseDayRate = 1250m;

    /// <summary>
    /// Calculates premiums for CoverType for given Start and End Dates
    /// </summary>
    /// <remarks>
    /// Premium depends on the type of the covered object and the length of the insurance period.
    /// Base day rate was set to be 1250.
    /// Yacht should be 10% more expensive, Passenger ship 20%, Tanker 50%, and other types 30%
    /// The length of the insurance period should influence the premium progressively:
    /// <list type="bullet">
    /// <item>
    /// <description>First 30 days are computed based on the logic above</description>
    /// </item>
    /// <item>
    /// <description>Following 150 days are discounted by 5% for Yacht and by 2% for other types</description>
    /// </item>
    /// <item>
    /// <description>The remaining days are discounted by additional 3% for Yacht and by 1% for other types</description>
    /// </item>
    /// </list>
    /// </remarks>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="coverType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///Thrown when the start date is bigger then end date.
    /// </exception>
    public decimal ComputePremium(DateOnly startDate, DateOnly endDate, CoverType coverType)
    {
        int lengthOfInsurancePeriod = endDate.DayNumber - startDate.DayNumber;

        if (lengthOfInsurancePeriod < 0)
            throw new ArgumentOutOfRangeException($"Start date ({startDate}) is bigger then End date ({endDate})");

        if (lengthOfInsurancePeriod == 0) return 0m;

        return CalculatePremiumDiscount(coverType, lengthOfInsurancePeriod);
    }

    private decimal CalculatePremiumDiscount(CoverType coverType, int insuranceDaysLength)
    {
        decimal baseRatePerDayForCoverType = CalculateBaseRateForCoverType(coverType);

        decimal totalPremium = 0m;

        var daysInPeriods = GetDaysFromInsurancePeriods(insuranceDaysLength);

        // First Period
        totalPremium += totalPremium + (daysInPeriods.daysInFirstPeriod * baseRatePerDayForCoverType);

        // Second Period
        decimal discountForSecondPeriod = coverType == CoverType.Yacht ? 0.05m : 0.02m;
        decimal premiumForSecondPeriod = daysInPeriods.daysInSecondPeriod * baseRatePerDayForCoverType;
        totalPremium += premiumForSecondPeriod - premiumForSecondPeriod * discountForSecondPeriod;


        // Second Period
        decimal discountForTheRestPeriod = coverType == CoverType.Yacht ? 0.03m : 0.01m;
        decimal premiumForTheRestPeriod = daysInPeriods.restOfDaysInPeriod * baseRatePerDayForCoverType;
        totalPremium += premiumForTheRestPeriod - premiumForTheRestPeriod * discountForTheRestPeriod;

        return totalPremium;
    }

    private decimal CalculateBaseRateForCoverType(CoverType coverType) => coverType switch
    {
        CoverType.Yacht => BaseDayRate * 1.1m,
        CoverType.PassengerShip => BaseDayRate * 1.2m,
        CoverType.Tanker => BaseDayRate * 1.5m,
        _ => BaseDayRate * 1.3m
    };

    private (int daysInFirstPeriod, int daysInSecondPeriod, int restOfDaysInPeriod) GetDaysFromInsurancePeriods(int days)
    {
        int daysInFirstRange = days > 30 ? 30 : days;

        int remainingDays = days - daysInFirstRange;

        int daysInSecondRange = remainingDays > 150 ? 150 : remainingDays;

        int restOfDaysInRange = days - 180 > 0 ? days - 180 : 0;

        return (daysInFirstRange, daysInSecondRange, restOfDaysInRange);
    }
}