#addin "Cake.FileHelpers"
#addin "Cake.Xamarin"

// --------------------------------------------------------------------------------
// ARGUMENTS
// --------------------------------------------------------------------------------

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

// --------------------------------------------------------------------------------
// FILES & DIRECTORIES
// --------------------------------------------------------------------------------

var solutionFile = File("../Todo.sln");
var androidProject = File("../Droid/Todo.Droid.csproj");
var androidBin = Directory("../Droid/bin") + Directory(configuration);
var iOSProject = File("../iOS/ToDo.iOS.csproj");
var iOSBin = Directory("../iOS/bin/iPhone") + Directory(configuration);

// --------------------------------------------------------------------------------
// PREPARATION
// --------------------------------------------------------------------------------

Task("Clean")
    .Does(() =>
{
    CleanDirectory(androidBin);
    CleanDirectory(iOSBin);
});

Task("Restore-NuGet")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(solutionFile);
});

// --------------------------------------------------------------------------------
// ANDROID
// --------------------------------------------------------------------------------

Task("Build Android")
    .IsDependentOn("Restore-NuGet")
    .Does(() =>
{
 	MSBuild(androidProject, settings =>
        settings.SetConfiguration(configuration)
        .WithTarget("SignAndroidPackage"));
});

// --------------------------------------------------------------------------------
// IOS
// --------------------------------------------------------------------------------

Task("Build iOS")
    .WithCriteria(IsRunningOnUnix())
    .IsDependentOn("Build Android")
    .Does(() =>
{
    MSBuild (iOSProject, settings => 
	    settings.SetConfiguration(configuration)
    		.WithProperty("Platform", "iPhone")
    		.WithProperty("OutputPath", $"bin/iPhone/{configuration}/"));
});

// --------------------------------------------------------------------------------
// TESTS
// --------------------------------------------------------------------------------

Task("Build tests")
    .IsDependentOn("Build iOS")
    .Does(() =>
{
});

Task("Run unit tests")
    .IsDependentOn("Build tests")
    .Does(() =>
{
});

Task("Run UI tests")
    .IsDependentOn("Run unit tests")
    .Does(() =>
{
});

// --------------------------------------------------------------------------------
// TASK TARGETS
// --------------------------------------------------------------------------------

Task("Default")
    .IsDependentOn("Run UI tests");

// --------------------------------------------------------------------------------
// EXECUTION
// --------------------------------------------------------------------------------

RunTarget(target);