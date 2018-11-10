using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System;

[Serializable]
public class PlayerProfile
{
    public string Name;
    public int TotalScore;
    public int TotalFrags;
    public int TotalDeaths;

    public PlayerProfile(string name)
    {
        Name = name;
        TotalScore = 0;
        TotalFrags = 0;
        TotalDeaths = 0;
    }
}
