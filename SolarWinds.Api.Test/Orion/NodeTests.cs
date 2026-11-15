namespace SolarWinds.Api.Test.Orion;

/// <summary>
/// Orion node integration tests.
/// </summary>
[Trait("Category", "Integration")]
public class NodeTests(ITestOutputHelper iTestOutputHelper) : NodeQueryTestsBase(iTestOutputHelper);
