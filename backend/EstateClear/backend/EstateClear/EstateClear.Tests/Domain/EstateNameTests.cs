using EstateClear.Domain.Estates;
using Xunit;

namespace EstateClear.Tests.Domain;

public class EstateNameTests
{
    [Fact]
    public void EstateNamesWithEquivalentNormalizedValueShouldBeEqual()
    {
        var name1 = EstateName.From("  eSTaTe   ALpha ");
        var name2 = EstateName.From("ESTATE ALPHA");

        Assert.True(name1.Equals(name2));
        Assert.True(name1 == name2);
        Assert.False(name1 != name2);
    }

    [Fact]
    public void EstateNamesWithDifferentValueShouldNotBeEqual()
    {
        var name1 = EstateName.From("Estate Alpha");
        var name2 = EstateName.From("Estate Beta");

        Assert.False(name1.Equals(name2));
        Assert.False(name1 == name2);
        Assert.True(name1 != name2);
    }

    [Fact]
    public void EstateNameEqualityShouldBeReflexiveSymmetricTransitive()
    {
        var x = EstateName.From("estate alpha");
        var y = EstateName.From("Estate   Alpha");
        var z = EstateName.From("  ESTATE ALPHA  ");

        Assert.True(x.Equals(x));

        Assert.True(x.Equals(y));
        Assert.True(y.Equals(x));

        Assert.True(x.Equals(y));
        Assert.True(y.Equals(z));
        Assert.True(x.Equals(z));
    }

    [Fact]
    public void EstateNameShouldProvideAConsistentHashCode()
    {
        var name1 = EstateName.From("estate alpha");
        var name2 = EstateName.From("Estate   Alpha");
        var different = EstateName.From("Estate Beta");

        Assert.Equal(name1.GetHashCode(), name2.GetHashCode());
        Assert.NotEqual(name1.GetHashCode(), different.GetHashCode());
    }
}
