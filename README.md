# Unit Testing Workflow Instructions

This document outlines the recommended workflow and prompts for implementing unit tests in your project.

## 1. Update the Local Database
**Prompt:**  
What command should I use to update the database on the server?

## 2. Run and Validate Methods and Database
**Prompt:**  
Based on the solution, which framework should be used for unit testing, which classes should be tested, how should the project be organized, and which dependencies should be used?

## 3. Create a Unit Test Project

## 4. Generate a Test Class for a Controller
**Prompt:**  
`/test controller`  
Consider using `Theory`, `InlineData`, and `ClassData` for different scenarios in each test. If necessary, generate mocks for dependencies and utilities.

## 5. Run Unit Tests and Validate Results
If the tests fail, repair them.

## 6. Explain How the Tests Work and Expected Data

## 7. Organize the Code for Refactoring
Explain how the code should be organized in case of a refactor.

## 8. Identify Classes Not Validated
**Prompt:**  
Can interfaces and/or DTO classes be tested, or which classes should be used for unit testing?

## 9. Example: Test Class for a Service
**Prompt:**  
`/test service`  
Maintain the structure of previous tests and place the classes in the correct folders.

## 10. Run Tests

## 11. Example: Test Class for a Repository
**Prompt:**  
`/test repository`  
Add utilities if necessary.

## 12. Run Tests

## 13. Coverage Report Generation
**Prompt:**  
What steps and tools can I use to generate a coverage report?

## 14. Run Coverage Commands

## 15. Validate Coverage with HTML Report****
