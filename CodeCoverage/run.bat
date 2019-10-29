@echo off

setlocal ENABLEDELAYEDEXPANSION

REM Set project name
set project=%1
if "%project%" == "" goto missing_project_name

set fileList =
	for %%y in (%*) do (
		set v1=%%y
		call set v2=x%%v1:UnitTests=%%%%
		if not !v2!==x%%y (set fileList=%%y\bin\Debug\%%y.dll !fileList!)
	)

REM create output folder
if not exist ..\cireport\testcoveragereport mkdir ..\cireport\testcoveragereport

REM Set working dir
pushd ..\

REM xUnit
REM OpenCover
set command=CodeCoverage\tools\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user -target:"CodeCoverage\tools\xunit.runner.console.2.3.0\tools\net452\xunit.console.exe" -targetargs:"%fileList% -nologo -noshadow -quiet -xml UnitTestResult_%project%.xml" -filter:"+[LuckyDraw*]* -[*]*Fanex.OIC.ExternalApi.Areas.HelpPage.* -[*]*Fanex.OIC.ExternalApi.WebApiApplication* -[*]*Fanex.OIC.ExternalApi.WebApiConfig* -[*]*Fanex.OIC.ExternalApi.DiConfig* -[*]*Fanex.OIC.ExternalApi.DependencyResolver.UnityResolver* -[*]*Fanex.OIC.ExternalApi.Resource.Labels*" -output:"UnitTestCoverage_%project%.xml"
call %command%

if %errorlevel% neq 0 goto error

REM REM ReportGenerator
set command=CodeCoverage\tools\ReportGenerator.3.0.2\tools\reportgenerator.exe -reports:"UnitTestCoverage_%project%.xml" -targetdir:"cireport\testcoveragereport\"
call %command%
if %errorlevel% neq 0 goto error

echo Success.
goto end

:missing_project_name
echo Please provide test project name.
goto end

:error
echo Failure.

:end
set /p temp="Hit enter to continue "

popd