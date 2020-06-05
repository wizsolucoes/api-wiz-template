#tool nuget:?package=ReportGenerator&version=4.6.1
#addin nuget:?package=Cake.Coverlet&version=2.4.2

var target = Argument("target", "Default");

var testProjectsRelativePaths = new string[]
{
    "../../test/Wiz.Template.Integration.Tests/Wiz.Template.Integration.Tests.csproj",
    "../../test/Wiz.Template.Unit.Tests/Wiz.Template.Unit.Tests.csproj"
};

var coverageDirectory = Directory("../../code_coverage");
var coverageFileName = "results";
var coverageFilePath = coverageDirectory + File(coverageFileName + ".cobertura.xml");
var jsonFilePath = coverageDirectory + File(coverageFileName + ".json");

Task("CoverageDirectory")
    .Does(() =>
{
    if (!DirectoryExists(coverageDirectory)) 
    {
        CreateDirectory(coverageDirectory);
        Information("Directory code_coverage create");
    }
    else 
    {
        CleanDirectory(coverageDirectory);
        Information("Directory code_coverage exists");
    }
});

Task("CoverageTest")
    .Does(() =>
{
    var testSettings = new DotNetCoreTestSettings
    {
        NoBuild = true,
        ArgumentCustomization = args => args.Append("--logger trx")
    };

    var coverletSettings = new CoverletSettings
    {
        CollectCoverage = true,
        CoverletOutputDirectory = coverageDirectory,
        CoverletOutputName = coverageFileName
    };

    if (testProjectsRelativePaths.Length == 1)
    {
        coverletSettings.CoverletOutputFormat  = CoverletOutputFormat.cobertura;

        DotNetCoreTest(testProjectsRelativePaths[0], testSettings, coverletSettings);
    }
    else
    {
        DotNetCoreTest(testProjectsRelativePaths[0], testSettings, coverletSettings);

        coverletSettings.MergeWithFile = jsonFilePath;

        for (int i = 1; i < testProjectsRelativePaths.Length; i++)
        {
            if (i == testProjectsRelativePaths.Length - 1)
            {
                coverletSettings.CoverletOutputFormat  = CoverletOutputFormat.cobertura;
            }

            DotNetCoreTest(testProjectsRelativePaths[i], testSettings, coverletSettings);
        }
    }
});

Task("Report")
    .Does(() =>
{
    var reportSettings = new ReportGeneratorSettings
    {
        ArgumentCustomization = args => args.Append($"-reportTypes:HtmlInline_AzurePipelines")
    };

    ReportGenerator(coverageFilePath, coverageDirectory, reportSettings);
});

Task("Default")
    .IsDependentOn("CoverageDirectory")
    .IsDependentOn("CoverageTest")
    .IsDependentOn("Report")
	.Does(()=>
{ 
    Information("Code coverage done!");
});

RunTarget(target);