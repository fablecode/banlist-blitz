using BanlistBlitz.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace BanlistBlitz.Integration.Tests
{
    [TestFixture]
    public class Class1
    {
        private BanlistBlitz _sut = null!;

        [SetUp]
        public void SetUp()
        {
            _sut = new BanlistBlitz(new []{new TcgFormatProcessor()});
        }

        [Test]
        public async Task Given_A_Format_Should_Return_A_Valid_Banlist_Name()
        {
            // Arrange
            var format = Format.Tcg;

            // Act
            var result = await _sut.LoadBanlist(format);

            // Assert
            result.Name.Should().NotBeNullOrWhiteSpace();
        }
    }
}