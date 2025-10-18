using iRacingSdkWrapper;

namespace Tests;

[TestFixture]
public class TelemetryReadTests
{
    private SdkWrapper _wrapper;

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        string filePath = "../../../../../porsche992rgt3_roadatlanta full 2025-10-03 17-59-42.ibt";
        _wrapper = new SdkWrapper(filePath);

        Thread.Sleep(5000);

        Assert.Pass();
    }
}
