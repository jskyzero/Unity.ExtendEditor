// Jsky About Windows
// contains version and some other info


public class AboutWindow : UnityEditor.EditorWindow
{
    [UnityEditor.MenuItem("Jsky/About")]
    public static void ShowWindow()
    {
        GetWindow(typeof(AboutWindow));
    }

    private void OnGUI()
    {
        UnityEngine.GUILayout.Label("Version 0.0.1");
    }
}
