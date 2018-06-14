using System;
using UnityEngine;

public class DatabaseController : MonoBehaviour {

    [Header("Login")]
    public string userName = "jelle@email.com";
    public string password = "password";
    public bool isLoggedIn;

    [Header("Highscores")]
    public int gameID = 1;
    public int amount = 5;
    public ScoreData[] highscores;

    [Header("Submit score")]
    [Range(1, 5)]
    public int gameIDToSubmit = 1;
    public int scoreToSubmit = 20;
    public string sessionID;

    private void AttemptLogin() {
        Action<string> failure = delegate(string result) { Debug.LogError("Login failed " + result); };
        StartCoroutine(DatabaseManager.Login(userName, password, LoginSuccess, failure));
    }

    private void LoginSuccess(string result) {
        Debug.Log("succesfully logged into account, session id: " + result);
        sessionID = result;
        isLoggedIn = true;
    }

    public void GetScores() {
        Action<string> failure = delegate(string result) { Debug.LogError("Failure retrieving scores " + result); };
        StartCoroutine(DatabaseManager.GetHighscore(gameID, amount, HighscoreRecieved, failure));
    }	

    public void HighscoreRecieved(ScoreData[] highscores) {
        Debug.Log("succesfully retrieved highscores");
        this.highscores = highscores;
    }

    public void SubmitNewScore() {
        Action<string> failure = delegate (string result) { Debug.LogError("Failure submitting score " + result); };
        StartCoroutine(DatabaseManager.SubmitScore(sessionID, gameIDToSubmit, scoreToSubmit, SuccessfullySubmittedScore, failure));
    }

    public void SuccessfullySubmittedScore() {
        Debug.Log("Successfully submitted " + scoreToSubmit + " to game id " + gameIDToSubmit);
    }

    private void OnGUI() {
        GUILayout.BeginVertical("box", GUILayout.Width(200));
        GUILayout.Label("Login");
        userName = GUILayout.TextField(userName);
        password = GUILayout.TextField(password);
        if (GUILayout.Button("Login"))
            AttemptLogin();
        GUILayout.EndVertical();

        if (!isLoggedIn)
            return;

        GUILayout.BeginVertical("box", GUILayout.Width(200));
        GUILayout.Label("Highscores");
        GUILayout.Label("Amount:");
        int.TryParse(GUILayout.TextField(amount.ToString()), out amount);
        GUILayout.Label("Game ID:");
        int.TryParse(GUILayout.TextField(gameID.ToString()), out gameID);
        if (GUILayout.Button("Get Highscores"))
            GetScores();
        GUILayout.EndVertical();

        GUILayout.BeginVertical("box", GUILayout.Width(200));
        GUILayout.Label("Submit Score");
        GUILayout.Label("Game ID:");
        int.TryParse(GUILayout.TextField(gameIDToSubmit.ToString()), out gameIDToSubmit);
        GUILayout.Label("Score:");
        int.TryParse(GUILayout.TextField(scoreToSubmit.ToString()), out scoreToSubmit);
        if (GUILayout.Button("Submit"))
            SubmitNewScore();
        GUILayout.EndVertical();
    }
}
