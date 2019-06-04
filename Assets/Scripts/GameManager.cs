using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField, Range(0, 1)] private float _winDelta;
    [SerializeField, Range(0, 5)] private float _speedProgressbar;
    [SerializeField] private List<Gradient> _gradients;
    
    private float _win;
    private float _percent;
    private float _score = 0;
    private float _blockNum = 0;
    
    public static GameManager Gm;
    
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

    public Gradient GetGradientForCurrentLevel()
    {
        return _gradients[Random.Range(0, _gradients.Count)];
    }
    
    public void AddScore()
    {
        _score += 1;
    }

    public float Score
    {
        get => _score;
    }
    
    public void AddBlock()
    {
        _blockNum += 1;
        _win = _blockNum * _winDelta;
        _percent = _win / 10;
    }
    
    public void RefreshBlock()
    {
        _blockNum = 0;
    }
    
    void ChangeProgress()
    {
        _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, _score / _percent/10, _speedProgressbar);
    }
}
