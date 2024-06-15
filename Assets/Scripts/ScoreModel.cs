using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModel
{
    public event Action<uint> ScoreUpdated;

    public uint Score { 
        get { return _score; }
        set
        {
            _score = value;
            ScoreUpdated?.Invoke(value);
        }
    }

    private uint _score;
}
