using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _score = 0;
    private int _ballNum = 0;

    public static GameManager Gm;
    private bool isReadyFlip;
    
    void Awake()
    {
        if (Gm == null)
        {
            Gm = this;
        }
    }
    
    public void AddScore()
    {
        _score += 1;
    }

    public int Score
    {
        get => _score;
    }

    public bool IsReadyFlip
    {
        get => isReadyFlip;
        set => isReadyFlip = value;
    }
}
