using ICM.Crypto.Domain.ValueObjects;

namespace ICM.Crypto.UnitTests.Domain;

[TestFixture, TestOf(typeof(Source))]
internal sealed class SourceTests
{
    [TestCase("")]
    [TestCase(" ")]
    public void Create_ForEmptyOrWhiteSpaceString_ThrowsArgumentException(string input)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Source.Create(input));
    }
    
    [Test]
    public void Create_ForNullString_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => Source.Create(null!));
    }

    [TestCase("ExternalApi")]
    [TestCase("External_Api")]
    [TestCase("External API")]
    [TestCase("External^%API")]
    public void Create_ForValidString_CreatesSourceInstance(string input)
    {
        // Act
        var actual =  Source.Create(input);
        
        // Assert
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.Value, Is.EqualTo(input));
    }

    [Test]
    public void ToString_ReturnsTheStringValue()
    {
        // Arrange
        const string ExpectedValue = "External API Source";
        var source = Source.Create(ExpectedValue);
        
        // Act
        var actual = source.ToString(); 
        
        // Assert
        Assert.That(actual, Is.EqualTo(ExpectedValue));
    }
}