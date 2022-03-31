using UnityEditor;
using UnityEditor.Compilation;

public static class RecompileScripts
{
    [MenuItem("Tools/PJL/Recompile Scripts", false, 1)]
    public static void ForceRecompile()
    {
        CompilationPipeline.RequestScriptCompilation();
    }
}