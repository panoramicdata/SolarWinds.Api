namespace SolarWinds.Api.Test.Orion;

/// <summary>
/// Orion credential integration tests.
/// </summary>
[Trait("Category", "Integration")]
public class CredentialTests(ITestOutputHelper iTestOutputHelper) : NodeQueryTestsBase(iTestOutputHelper);
