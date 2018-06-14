using System;

[Serializable]
public class ScoreData {
    public int Score;
    public string Nickname;

    public ScoreData(int score, string nickname) {
        Nickname = nickname;
        Score = score;
    }
}

