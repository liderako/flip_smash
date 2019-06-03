using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Image _progressBar;
    [SerializeField, Range(0, 1)]
    float _winDelta;
    [SerializeField, Range(0, 5)]
    float _speedProgressbar;
    private float _score = 0;
    private float _blockNum = 0;

    public static GameManager Gm;
    private bool isReadyFlip;
    
    void Awake()
    {
        if (Gm == null)
        {
            Gm = this;
        }
    }
    void Update()
    {
        ChangeProgress();
    }
    public void AddScore()
    {
        _score += 1;
    }

    public float Score
    {
        get => _score;
    }

    public bool IsReadyFlip
    {
        get => isReadyFlip;
        set => isReadyFlip = value;
    }
    public void AddBlock()
    {
        _blockNum += 1;
    }
    public void RefreshBlock()
    {
        _blockNum = 0;
    }
    void ChangeProgress()
    {
        Debug.Log("BlockNum" + _blockNum);
        float win = _blockNum * _winDelta;
        Debug.Log("win" + win);
        float percent = win / 10;
        Debug.Log("percent" + percent);
        _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, _score / percent / 10, _speedProgressbar);
    }
}
