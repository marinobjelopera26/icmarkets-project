using ICM.Crypto.Domain.ValueObjects;

namespace ICM.Crypto.UnitTests.Domain;

[TestFixture, TestOf(typeof(SnapshotId))]
internal sealed class SnapshotIdTests
{
    [Test]
    public void CreateNew_AssignsValidGuidValue()
    {
        // Act
        var actual = SnapshotId.CreateNew();
        
        // Assert
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.Value, Is.Not.EqualTo(Guid.Empty));
    }

    [TestCase("24820851-4AA5-4ECC-9979-EB363FBEA8D5")]
    [TestCase("8BEDCA39-67FD-40B0-8999-92D915890A7E")]
    [TestCase("0164E478-8E6D-49DA-8BD4-06F1D51594C2")]
    public void FromExisting_CorrectlyAssignsProvidedValue(string value)
    {
        var guid = Guid.Parse(value);
        
        // Act
        var actual = SnapshotId.FromExisting(guid);
        
        // Assert
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.Value, Is.EqualTo(guid));
    }

    [Test]
    public void ToString_ReturnsExpectedFormat()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var expectedValue = guid.ToString("D");
        var snapshotId = SnapshotId.FromExisting(guid);
        
        // Act
        var actual = snapshotId.ToString();
        
        // Assert 
        Assert.That(actual, Is.EqualTo(expectedValue));
    }
}