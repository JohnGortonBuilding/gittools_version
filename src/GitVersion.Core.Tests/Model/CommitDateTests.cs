using GitVersion.Core.Tests.Helpers;
using GitVersion.Extensions;
using GitVersion.Model.Configuration;
using GitVersion.VersionCalculation;
using NUnit.Framework;

namespace GitVersion.Core.Tests;

[TestFixture]
public class CommitDateTests : TestBase
{
    [Test]
    [TestCase("yyyy-MM-dd", "2017-10-06")]
    [TestCase("dd.MM.yyyy", "06.10.2017")]
    [TestCase("yyyyMMdd", "20171006")]
    [TestCase("yyyy-MM", "2017-10")]
    public void CommitDateFormatTest(string format, string expectedOutcome)
    {
        var date = new DateTime(2017, 10, 6);

        var formatValues = new SemanticVersionFormatValues(
            new SemanticVersion
            {
                BuildMetaData = new SemanticVersionBuildMetaData("950d2f830f5a2af12a6779a48d20dcbb02351f25", 0, MainBranch, "3139d4eeb044f46057693473eacc2655b3b27e7d", "3139d4eeb", new DateTimeOffset(date, TimeSpan.Zero), 0) // assume time zone is UTC

            },
            new EffectiveConfiguration(
                AssemblyVersioningScheme.MajorMinorPatch, AssemblyFileVersioningScheme.MajorMinorPatch, "", "", "", VersioningMode.ContinuousDelivery, "", "", "", IncrementStrategy.Inherit,
                "", true, "", "", false, "", "", "", "", CommitMessageIncrementMode.Enabled, 4, 4, 4, Enumerable.Empty<IVersionFilter>(), false, true, format, false, 0, 0)
        );

        Assert.That(formatValues.CommitDate, Is.EqualTo(expectedOutcome));
    }
}
