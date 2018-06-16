using System;
using UnityEngine;

public class GameTimer : MonoBehaviour {

    public float maxTimeInSeconds = 120;
    public float time;

    public static Action onTimerReachedZero;

    private void Start() {
        TurnManager.onGameStarted += StartTimer;
        time = maxTimeInSeconds;
        enabled = false;
    }

    private void StartTimer() {
        enabled = true;
    }

    private void Update() {
        time -= Time.deltaTime;
        if (time < 0)
            OnTimeReachedZero();
    }

    private void OnTimeReachedZero() {
        enabled = false;
        if (onTimerReachedZero != null)
            onTimerReachedZero.Invoke();
    }

    private void OnGUI() {
        GUI.Label(new Rect(Screen.width - 100, 10, 100, 20), "Time: " + time.ToString("N0"));
    }
}
