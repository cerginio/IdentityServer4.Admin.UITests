
### Running tests in Visual Studio

Choose target environment by going to Test->Test Settings->Select Test Settings File.
Select one of Environments\\*.runsettings files included in the project.

Run tests from Test->Windows->Test Explorer.

Keep all *.runsettings files optimized for execution on developer machine.
VSTS execution overrides settings such as -SeleniumDriver ChromeHeadless -InternalAdfs_ExpectIntegratedAuth False.
