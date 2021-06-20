//This is a modified version of https://gist.github.com/eppz/1ebbc1cf6a77741f56d63d3803e57ba3
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

public class BuildPostProcessor
{
    [PostProcessBuildAttribute(1)]
    public static void OnPostProcessBuild(BuildTarget target, string path)
    {
#if UNITY_iOS
        if (target == BuildTarget.iOS)
        {
            // Read.
            string projectPath = PBXProject.GetPBXProjectPath(path);
            PBXProject project = new PBXProject();
            project.ReadFromString(File.ReadAllText(projectPath));
#if UNITY_2019_3_OR_NEWER
            string targetGUID = project.GetUnityMainTargetGuid();
#else
            string targetName = PBXProject.GetUnityTargetName();
            string targetGUID = project.TargetGuidByName(targetName);
#endif
            AddFrameworks(project, targetGUID);
            
            var plistPath = Path.Combine(path, "Info.plist");
            var plist = new PlistDocument();
            plist.ReadFromFile(plistPath);
            plist.root.SetString("NSSpeechRecognitionUsageDescription", "This app needs access to Speech Recognition");
            plist.WriteToFile(plistPath);

            // Write.
            File.WriteAllText(projectPath, project.WriteToString());
        }
#endif
    }
}