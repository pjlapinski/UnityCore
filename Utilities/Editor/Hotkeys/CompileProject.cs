// Source: https://github.com/adammyhre/Unity-Utils/blob/master/UnityUtils/Scripts/Hotkeys/Editor/CompileProject.cs

using UnityEditor;
using UnityEditor.Compilation;

public static class CompileProject {
    [MenuItem("File/Compile _F5")]
    private static void Compile() {
        CompilationPipeline.RequestScriptCompilation(RequestScriptCompilationOptions.CleanBuildCache);
    }
}
