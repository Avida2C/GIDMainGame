using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HighScore
{
    public string PlayerName;

    public long Score;

    public HighScore(string playerName, long score)
    {
        this.PlayerName = playerName;
        this.Score = score;
    }
    
}
