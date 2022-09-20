using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using Serilog;
using System.Linq;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.PushNuget);

    private const string CoreProjectName = "DarkHelpers";
    private const string CoreTestProjectName = "DarkHelpers.Tests";
    private const string WpfProjectName = "DarkHelpers.WPF";
    private const string WpfTestProjectName = "DarkHelpers.WPF.Tests";
    private const string XfProjectName = "DarkHelpers.XF";
    private const string XfTestProjectName = "DarkHelpers.XF.Tests";
    private const string MauiProjectName = "DarkHelpers.Maui";
    private const string MauiTestProjectName = "DarkHelpers.Maui.Tests";

    [Solution] readonly Solution Solution;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    readonly Configuration Configuration = Configuration.Release;

    [Parameter("Push the resulting packages to nuget.org", Name = "push")] readonly bool ShouldPushNuget;
    [Parameter] readonly string NugetApiUrl = "https://api.nuget.org/v3/index.json";
    [Parameter] readonly string NugetApiKey;

    Target LogSettings => _ => _
        .Executes(() =>
        {
            Log.Information("ShouldPushNuget: {ShouldPushNuget}", ShouldPushNuget);
            Log.Information("NugetApiUrl: {NugetApiUrl}", NugetApiUrl);
        });

    Target Clean => _ => _
        .DependsOn(LogSettings)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    #region Core

    Target RestoreCore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution.GetProject(CoreProjectName)));
        });

    Target CompileCore => _ => _
        .DependsOn(RestoreCore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution.GetProject(CoreProjectName))
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target CompileCoreTests => _ => _
        .DependsOn(CompileCore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution.GetProject(CoreTestProjectName))
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target RunCoreTests => _ => _
        .DependsOn(CompileCoreTests)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution.GetProject(CoreTestProjectName))
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild());
        });

    Target PackCore => _ => _
        .DependsOn(RunCoreTests)
        .Executes(() =>
        {
            DotNetPack(s => s
              .SetProject(Solution.GetProject(CoreProjectName))
              .SetConfiguration(Configuration)
              .EnableNoBuild()
              .EnableNoRestore()
              .SetOutputDirectory(ArtifactsDirectory));
        });

    #endregion

    #region WPF

    Target RestoreWpf => _ => _
        .DependsOn(PackCore)
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution.GetProject(WpfProjectName)));
        });

    Target CompileWpf => _ => _
        .DependsOn(RestoreWpf)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution.GetProject(WpfProjectName))
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target CompileWpfTests => _ => _
        .DependsOn(CompileWpf)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution.GetProject(WpfTestProjectName))
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target RunWpfTests => _ => _
        .DependsOn(CompileWpfTests)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution.GetProject(WpfTestProjectName))
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild());
        });

    Target PackWpf => _ => _
        .DependsOn(RunWpfTests)
        .Executes(() =>
        {
            DotNetPack(s => s
                .SetProject(Solution.GetProject(WpfProjectName))
                .SetConfiguration(Configuration)
                .EnableNoBuild()
                .EnableNoRestore()
                .SetOutputDirectory(ArtifactsDirectory));
        });

    #endregion

    #region XF

    Target RestoreXf => _ => _
        .DependsOn(PackCore)
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution.GetProject(XfProjectName)));
        });

    Target CompileXf => _ => _
        .DependsOn(RestoreXf)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution.GetProject(XfProjectName))
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target CompileXfTests => _ => _
        .DependsOn(CompileXf)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution.GetProject(XfTestProjectName))
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target RunXfTests => _ => _
        .DependsOn(CompileXfTests)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution.GetProject(XfTestProjectName))
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild());
        });

    Target PackXf => _ => _
        .DependsOn(RunXfTests)
        .Executes(() =>
        {
            DotNetPack(s => s
                .SetProject(Solution.GetProject(XfProjectName))
                .SetConfiguration(Configuration)
                .EnableNoBuild()
                .EnableNoRestore()
                .SetOutputDirectory(ArtifactsDirectory));
        });

    #endregion

    #region Maui

    Target RestoreMaui => _ => _
        .DependsOn(PackCore)
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution.GetProject(MauiProjectName)));
        });

    Target CompileMaui => _ => _
        .DependsOn(RestoreMaui)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution.GetProject(MauiProjectName))
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target CompileMauiTests => _ => _
        .DependsOn(CompileMaui)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution.GetProject(MauiTestProjectName))
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target RunMauiTests => _ => _
        .DependsOn(CompileMauiTests)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution.GetProject(MauiTestProjectName))
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild());
        });

    Target PackMaui => _ => _
        .DependsOn(RunMauiTests)
        .Executes(() =>
        {
            DotNetPack(s => s
                .SetProject(Solution.GetProject(MauiProjectName))
                .SetConfiguration(Configuration)
                .EnableNoBuild()
                .EnableNoRestore()
                .SetOutputDirectory(ArtifactsDirectory));
        });

    #endregion

    Target Pack => _ => _
        .DependsOn(PackCore)
        .DependsOn(PackWpf)
        .DependsOn(PackXf)
        .DependsOn(PackMaui);

    Target PushNuget => _ => _
        .DependsOn(Pack)
        .Requires(() => NugetApiUrl)
        .Requires(() => NugetApiKey)
        .Executes(() =>
        {
            if (!ShouldPushNuget)
            {
                Log.Information("Skipping push because it is disabled (the --push parameter wasn't used)");
                return;
            }

            GlobFiles(ArtifactsDirectory, "*.nupkg")
               .Where(x => !string.IsNullOrEmpty(x) && !x.EndsWith("symbols.nupkg"))
               .ForEach(x =>
               {
                   DotNetNuGetPush(s => s
                       .SetTargetPath(x)
                       .SetSource(NugetApiUrl)
                       .SetApiKey(NugetApiKey)
                   );
               });
        });
}
