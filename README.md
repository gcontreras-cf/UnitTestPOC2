# Project Setup & Testing Guide

## 1. Update the Local Database

**Question:**  
What command should I use to update the database on the server?

**Instructions:**  
Ensure your `appsettings.json` points to your local database.  
Use NuGet Package Manager or CLI:

```bash
dotnet ef database update --project Infrastructure --startup-project Api
```

---

## 2. Run and Validate Methods and Database

**Question:**  
Which framework should be used for unit testing, which classes should be tested, how should the project be organized, and which dependencies should be used?

**Instructions:**  
- Use **xUnit** as the unit testing framework.
- Prioritize testing controllers and services.
- Organize test projects by feature or layer:
  - Example: `Tests/Controllers`, `Tests/Services`
- Use **Moq** for mocking dependencies.
- Add references to relevant projects and NuGet packages.

---

## 3. Create a Unit Test Project

**Instructions:**  
Generate a new test project:

```bash
dotnet new xunit -n ProjectName.Tests
dotnet add ProjectName.Tests reference Api
dotnet add ProjectName.Tests reference Infrastructure
dotnet add package Moq
```

---

## 4. Generate Test Class for a Controller

**Prompt:**  
`/test #ClientController`

**Instructions:**  
- Use `[Theory]`, `[InlineData]`, and `[ClassData]` for parameterized tests.
- Mock dependencies and utilities as needed.

**Example Test Skeleton:**
```csharp
using Xunit;
using Moq;

namespace ProjectName.Tests.Controllers
{
    public class ClientControllerTests
    {
        // Arrange dependencies with Moq

        [Theory]
        [InlineData(1)]
        public void GetClient_ReturnsExpectedResult(int clientId)
        {
            // Test implementation
        }
    }
}
```

---

## 5. Run Unit Tests & Validate

**Instructions:**  
Run tests and validate results:

```bash
dotnet test ProjectName.Tests
```

- Investigate and repair any failing tests.

---

## 6. Example Test Class for a Service

**Prompt:**  
`/test #ClientService`

**Instructions:**  
- Follow the same structure as controller tests.
- Place service test classes in `Tests/Services`.

**Example Test Skeleton:**
```csharp
using Xunit;
using Moq;

namespace ProjectName.Tests.Services
{
    public class ClientServiceTests
    {
        // Arrange dependencies with Moq

        [Theory]
        [InlineData("validInput")]
        public void ProcessClient_ReturnsExpectedResult(string input)
        {
            // Test implementation
        }
    }
}
```

---

## 7. Generate Coverage Report

**Question:**  
What steps and tools can I use to generate a coverage report?

**Instructions:**  
- Add **coverlet** for coverage:
  ```bash
  dotnet add ProjectName.Tests package coverlet.collector
  ```
- Run tests with coverage collection:
  ```bash
  dotnet test --collect:"XPlat Code Coverage"
  ```

---

## 8. Validate Coverage with HTML Report

**Instructions:**  
- Use **ReportGenerator** to create HTML reports:
  ```bash
  dotnet tool install -g dotnet-reportgenerator-globaltool
  reportgenerator -reports:"TestResults/*/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
  ```
- Open `coveragereport/index.html` for results.
