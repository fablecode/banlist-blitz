using BanlistBlitz.Domain;
using BanlistBlitz.Processors;
using FluentAssertions;
using NUnit.Framework;

namespace BanlistBlitz.Integration.Tests
{
    [TestFixture]
    public class BanlistBlitzTests
    {
        private BanlistBlitz _sut = null!;

        [SetUp]
        public void SetUp()
        {
            _sut = new BanlistBlitz(new IFormatProcessor[]{ new TcgFormatProcessor(), new OcgFormatProcessor() });
        }

        [TestCaseSource(nameof(FormatCases))]
        public async Task Given_A_Format_Should_Return_A_Valid_Banlist_Name(Format format)
        {
            // Arrange

            // Act
            var result = await _sut.LoadBanlist(format);

            // Assert
            result.Name.Should().NotBeNullOrWhiteSpace();
        }

        public static object[] FormatCases =
        {
            //new object[] { Format.Tcg },
            new object[] { Format.Ocg },
        };
    }
}