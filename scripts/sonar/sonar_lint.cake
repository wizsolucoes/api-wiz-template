#tool nuget:?package=MSBuild.SonarQube.Runner.Tool&version=4.8.0
#addin nuget:?package=Cake.Sonar&version=1.1.25

var target = Argument("target", "Default");

var solutionDirectory = "../../Wiz.Template.sln";
var sonarProjectKey = "{PROJECT_KEY}";
var sonarKey = "{SONAR_KEY}";
var sonarOrganization = "wizdevops";

Task("Build")
    .Does(() =>
{
    var buildSettings = new DotNetCoreBuildSettings
    {
        Configuration = "Release",
        NoIncremental = true
    };

    DotNetCoreBuild(solutionDirectory, buildSettings);
});

Task("SonarLint")
    .Does(() => 
{
    var sonarSettings = new SonarBeginSettings
    {
        Key = sonarProjectKey,
        Name = sonarProjectKey,
        Url = "https://sonarcloud.io",
        Login = sonarKey,   
        Organization = sonarOrganization,
        Exclusions = "**/obj/**,**/*.dll"
    };

    SonarBegin(sonarSettings);
});

Task("Default")
    .IsDependentOn("SonarLint")
    .IsDependentOn("Build")
	.Does(()=>
{ 
    Information("Sonar lint done!");
});

RunTarget(target);