using UnityEngine;

namespace Snog.TimerManager.Demo
{
    /// <summary>
    /// Usage:
    /// - Run scene and watch Console + target toggles.
    /// - Controls (while playing): P = Pause runtime timer, R = Resume, S = Stop, Space = Toggle creating a new runtime timer.
    /// </summary>
    public class DemoTimerExample : MonoBehaviour
    {
        [Header("Runtime (code) timer")]
        [SerializeField] private string runtimeTimerID = "runtime_demo";
        [SerializeField] private float runtimeDuration = 4f;
        [SerializeField] private bool runtimeIsLooping = false;

        [Header("Visual")]
        [SerializeField] private GameObject targetToToggle;

        private bool runtimeTimerCreated = false;

        private void Start()
        {
            CreateRuntimeTimer();
            TimerManager.Instance.RegisterOnComplete(runtimeTimerID, () =>
            {
                Debug.Log($"RegisterOnComplete callback called for '{runtimeTimerID}'");
            });
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                TimerManager.Instance.PauseTimer(runtimeTimerID);
                Debug.Log("Paused runtime timer.");
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                TimerManager.Instance.ResumeTimer(runtimeTimerID);
                Debug.Log("Resumed runtime timer.");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                TimerManager.Instance.StopTimer(runtimeTimerID);
                Debug.Log("Stopped runtime timer.");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TimerManager.Instance.CreateTimer(runtimeTimerID, runtimeDuration, runtimeIsLooping, () =>
                {
                    Debug.Log("Space-created runtime timer completed.");
                    ToggleTarget();
                }, overwrite: true);

                runtimeTimerCreated = true;
                Debug.Log("created/overwrote runtime timer.");
            }

            if (TimerManager.Instance.TimerExists(runtimeTimerID))
            {
                if (TimerManager.Instance.GetAllTimers().TryGetValue(runtimeTimerID, out var t))
                {
                    float progress = Mathf.Clamp01(t.Elapsed / Mathf.Max(0.0001f, t.Duration));
                    Debug.Log($"Timer '{runtimeTimerID}' progress: {progress:P0} Elapsed: {t.Elapsed:F2}s / {t.Duration:F2}s");
                }
            }
        }

        private void CreateRuntimeTimer()
        {
            if (runtimeTimerCreated) return;

            TimerManager.Instance.CreateTimer(runtimeTimerID, runtimeDuration, runtimeIsLooping, () =>
            {
                Debug.Log($"Runtime timer '{runtimeTimerID}' completed.");
                ToggleTarget();
            });
            Debug.Log($"Created runtime timer '{"runtime_timer"}' ({runtimeDuration}s).");

            TimerManager.Instance.CreateTimer("runtime_timer2", 2, runtimeIsLooping, () =>
            {
                Debug.Log($"Runtime timer '{"runtime_timer2"}' completed.");
                ToggleTarget();
            });
            Debug.Log($"Created runtime timer '{"runtime_timer"}' ({2}s).");

            runtimeTimerCreated = true;
        }

        private void ToggleTarget()
        {
            if (targetToToggle == null) return;
            if (!targetToToggle.TryGetComponent<MeshRenderer>(out var meshRenderer))
            {
                Debug.LogWarning($"'{targetToToggle.name}' has no MeshRenderer component.");
                return;
            }

            Color randomColor = new
            (
                Random.value, // Red channel
                Random.value, // Green channel
                Random.value  // Blue channel
            );

            meshRenderer.material.color = randomColor;

        }
    }
}