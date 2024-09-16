namespace Application.FunctionalTests;

using static Testing;

[TestFixture]
public class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        await ResetState();
    }
}
