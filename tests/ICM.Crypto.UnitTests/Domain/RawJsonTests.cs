using ICM.Crypto.Domain.ValueObjects;

namespace ICM.Crypto.UnitTests.Domain;

[TestFixture]
internal sealed class RawJsonTests
{
    [TestCase("")]
    [TestCase(" ")]
    public void Create_ForEmptyOrWhiteSpaceString_ThrowsArgumentException(string input)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => RawJson.Create(input));
    }
    
    [Test]
    public void Create_ForNullString_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => RawJson.Create(null!));
    }

    [TestCase("{ \"data\": { \"someKey\": \"someValue\", \"int\": 1, \"bool\": true } ")]
    public void Create_ForValidString_CreatesSourceInstance(string input)
    {
        // Act
        var actual =  RawJson.Create(input);
        
        // Assert
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.Value, Is.EqualTo(input));
    }
}