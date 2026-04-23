using System;
using UnityEngine;

namespace Snog.TimerManager.Utils
{
    public class GameTimer
    {
        public string ID { get; }
        public float Duration { get; private set; }
        public float Elapsed { get; private set; }
        public bool IsLooping { get; private set; }
        public bool IsRunning { get; private set; }

        // runtime delegate-based callback
        public Action OnComplete;

        public GameTimer(string id, float duration, bool isLooping, Action onComplete)
        {
            ID = id;
            Duration = Mathf.Max(0.0001f, duration);
            IsLooping = isLooping;
            OnComplete = onComplete;
            Elapsed = 0f;
            IsRunning = true;
        }

        public void Update(float delta)
        {
            if (!IsRunning) return;

            Elapsed += delta;
            if (Elapsed >= Duration)
            {
                try { OnComplete?.Invoke(); }
                catch (Exception ex) { Debug.LogException(ex); }

                if (IsLooping)
                {
                    Elapsed -= Duration;
                }
                else
                {
                    IsRunning = false;
                }
            }
        }

        public void Pause() => IsRunning = false;
        public void Resume() => IsRunning = true;
        public void Stop() { IsRunning = false; Elapsed = 0f; }
        public void SetDuration(float d) => Duration = Mathf.Max(0.0001f, d);
    }
}
