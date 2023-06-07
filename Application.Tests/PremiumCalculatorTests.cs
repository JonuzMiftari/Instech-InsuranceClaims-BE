using Application.Premiums;
using Domain.Enums;
using FluentAssertions;

namespace Application.Tests;

public class PremiumCalculatorTests
{
    [Theory]
    [MemberData(nameof(PremiumCalculatorData.Data), MemberType = typeof(PremiumCalculatorData))]
    public void TestPremiums(DateOnly startDate, DateOnly endDate, CoverType coverType, decimal expectedResult)
    {
        // Arrange
        var computePremium = new PremiumCalculator();

        // Act
        var result = computePremium.ComputePremium(startDate, endDate, coverType);

        // Assert
        result.Should().Be(expectedResult);
    }
}

public class PremiumCalculatorData
{
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            // Yacht
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 01, 11), CoverType.Yacht, 13750m},
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 01, 21),CoverType.Yacht, 27500m },
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 01, 31),CoverType.Yacht, 41250m },
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 03, 22), CoverType.Yacht, 106562.5m},
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 06, 30),CoverType.Yacht, 237187.5m },
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 07, 10),CoverType.Yacht, 250525m },
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 07, 20), CoverType.Yacht, 263862.5m},
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 08, 09),CoverType.Yacht, 290537.5m },

            // PassengerShip
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 01, 11), CoverType.PassengerShip, 15000m},
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 01, 21),CoverType.PassengerShip, 30000m },
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 01, 31),CoverType.PassengerShip, 45000m },
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 03, 22), CoverType.PassengerShip, 118500m},
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 06, 30),CoverType.PassengerShip, 265500m },
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 07, 10),CoverType.PassengerShip, 280350m },
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 07, 20), CoverType.PassengerShip, 295200m},
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 08, 09),CoverType.PassengerShip, 324900m },

            // Tanker
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 01, 11), CoverType.Tanker, 18750m},
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 01, 21),CoverType.Tanker, 37500m },
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 01, 31),CoverType.Tanker, 56250m },
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 03, 22), CoverType.Tanker, 148125m},
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 06, 30),CoverType.Tanker, 331875m },
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 07, 10),CoverType.Tanker, 350437.5m },
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 07, 20), CoverType.Tanker, 369000m},
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 08, 09),CoverType.Tanker, 406125m },

            // Other - Mixed BulkCarrier && ContainerShip
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 01, 11), CoverType.BulkCarrier, 16250m},
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 01, 21),CoverType.ContainerShip, 32500m },
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 01, 31),CoverType.BulkCarrier, 48750m },
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 03, 22), CoverType.BulkCarrier, 128375m},
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 06, 30),CoverType.ContainerShip, 287625m },
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 07, 10),CoverType.ContainerShip, 303712.5m },
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 07, 20), CoverType.ContainerShip, 319800m},
            new object[] { new DateOnly(2023, 01, 01), new DateOnly(2023, 08, 09),CoverType.BulkCarrier, 351975m },
        };
}