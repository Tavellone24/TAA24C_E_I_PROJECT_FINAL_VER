using UnityEditor;
using UnityEngine;
using Snog.TimerManager;

[CustomEditor(typeof(TimerManager))]
public class TimerManagerEditor : Editor
{
    private string filterID = "";

    public override void OnInspectorGUI()
    {
        if (Application.isPlaying)
            Repaint();

        DrawDefaultInspector();
        TimerManager manager = (TimerManager)target;
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Active Timers", EditorStyles.boldLabel);
        if (Application.isPlaying)
        {
            filterID = EditorGUILayout.TextField("Filter by ID", filterID);

            foreach (var kvp in manager.GetAllTimers())
            {
                var timer = kvp.Value;

                if (!string.IsNullOrEmpty(filterID) && !timer.ID.Contains(filterID))
                    continue;

                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField("ID", timer.ID);
                EditorGUILayout.LabelField("Duration", timer.Duration.ToString("F2") + "s");
                EditorGUILayout.LabelField("Elapsed", timer.Elapsed.ToString("F2") + "s");
                EditorGUILayout.LabelField("Looping", timer.IsLooping ? "Yes" : "No");
                EditorGUILayout.LabelField("Running", timer.IsRunning ? "Yes" : "No");

                // safe progress calculation
                float progress;
                if (timer.Duration <= 0f || float.IsInfinity(timer.Elapsed) || float.IsNaN(timer.Elapsed))
                    progress = timer.IsLooping ? 0f : 1f; // fallback
                else
                    progress = Mathf.Clamp01(timer.Elapsed / timer.Duration);

                EditorGUI.ProgressBar(EditorGUILayout.GetControlRect(), progress, $"{(int)(progress * 100f)}%");

                EditorGUILayout.Space();

                // Controls (grouped)
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Resume")) manager.ResumeTimer(timer.ID);
                if (GUILayout.Button("Pause"))  manager.PauseTimer(timer.ID);
                if (GUILayout.Button("Stop"))   manager.StopTimer(timer.ID);
                if (GUILayout.Button("Remove")) manager.RemoveTimer(timer.ID);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical(); // close box for this timer
                EditorGUILayout.Space();
            }

            // global controls
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Resume All Timers")) manager.ResumeAllTimers();
            if (GUILayout.Button("Pause All Timers")) manager.PauseAllTimers();
            if (GUILayout.Button("Stop All Timers")) manager.StopAllTimers(); // see note below
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.HelpBox("Timers will be visible during Play Mode.", MessageType.Info);
        }
    }
}
