using System;
using System.Collections;
using UnityEngine;

public static class DatabaseManager {

    private const string HOME_URL = "http://studenthome.hku.nl/~jelle.dekkers/kernmodule4/Unity/";
    private const string LOGIN_URL = HOME_URL + "login.php?";
    private const string GET_SCORES_URL = HOME_URL + "getScores.php?";
    private const string SUBMIT_SCORE = HOME_URL + "insertScore.php?";

    public static IEnumerator Login(string userName, string password, Action<string> onSuccess, Action<string> onFailure) {
        string url = LOGIN_URL + "username=" + userName + "&" + "password=" + password;
        using (WWW login = new WWW(url)) {
            yield return login;
            if (login.text != String.Empty) {
                onSuccess(login.text);
            } else {
                onFailure.Invoke(login.text);
            }
        }
    }

    public static IEnumerator GetHighscore(int gameID, int amount, Action<ScoreData[]> onSuccess, Action<string> onFailure) {
        string url = GET_SCORES_URL + "id=" + gameID + "&" + "amount=" + amount;
        using (WWW request = new WWW(url)) {
            yield return request;
            if (request.error == null) {
                ScoreData[] scores = CreateArrayFromJson(JsonHelper.FixJsonString(request.text));
                onSuccess.Invoke(scores);
            } else {
                onFailure.Invoke(request.text);
            }
        }
    }

    public static IEnumerator SubmitScore(string sessionID, int gameID, int score, Action onSuccess, Action<string> onFailure) {
        string url = SUBMIT_SCORE + "sessionID=" + sessionID + "&" + "gameID=" + gameID + "&" + "score=" + score;
        using (WWW login = new WWW(url)) {
            yield return login;
            if (login.error == null && login.text == String.Empty) {
                onSuccess.Invoke();
            } else {
                onFailure.Invoke(login.text);
            }
        }
    }

    private static ScoreData[] CreateArrayFromJson(string jsonArray) {
        return JsonHelper.FromJson<ScoreData>(jsonArray);
    }
}
