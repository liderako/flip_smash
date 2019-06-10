using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lvlText;
    [SerializeField] private TextMeshProUGUI _percentText;
    [SerializeField] private GameObject _textEndLevel;
    [SerializeField] private GameObject _retryLevel;
    [SerializeField] private Image _progressBar;
    [SerializeField] private Image _nextLvl;
    [SerializeField, Range(0, 1)] private float _winDelta;
    [SerializeField, Range(0, 5)] private float _speedProgressbar;
    [SerializeField] private List<Gradient> _gradients;
    [SerializeField] private List<GameObject> _towersCube;
    private int _destroyBlock;
    
    
    private float _win;
    private float _percent;
    private float _score;
    private float _blockNum;
    private int _currentLevel;
    [SerializeField]private bool _isLeveldone;
    [SerializeField]private bool _isDoneBall;
    private bool _isRetry;
    
    [SerializeField]private float _oldTime;
    [SerializeField]private float _waitTime;
    
    public static GameManager Gm;
    public delegate void MethodContainer();
    public event MethodContainer LoadLevel;
    
    void Awake()
    {
        if (Gm == null)
        {
            Gm = this;
        }
    }

    void Start()
    {
        LoadLevel += RestartLevel;
        LoadLevel();
    }

    void Update()
    {
        TestInput();
        if (_isLeveldone && Input.GetMouseButtonDown(0))
        {
            NextLevel();
        }
        if (_isRetry && Input.GetMouseButtonDown(0))
        {
            LoadLevel();
        }
        if (_isDoneBall && Time.time - _oldTime > _waitTime)
        {
            RetryLevel();
        } 
    }
    
    void LateUpdate()
    {
        ChangeProgress();
    }
    
    public void RestartLevel()
    {
        _isLeveldone = false;
        _isDoneBall =false;
        _isRetry = false;
        //_oldTime = Time.time;
        if (PlayerPrefs.GetInt("level", 0) == 0)
        {
            PlayerPrefs.SetInt("level", 0);
        }
        _currentLevel = PlayerPrefs.GetInt("level");
        if (_currentLevel >= _towersCube.Count)
        {
            _towersCube[_currentLevel % _towersCube.Count].SetActive(true);
        }
        else
        {
            _towersCube[_currentLevel].SetActive(true);
        }
        _lvlText.text = "LVL: " + _currentLevel;
    }
    
    public void AddScore()
    {
        _score += 1;
        _oldTime = Time.time;
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

    public void NextLevel()
    {
        PlayerPrefs.SetInt("level", _currentLevel+1);
        LoadLevel();
    }
    
    public IEnumerator BallDone()
    {
        yield return new WaitForSeconds(_waitTime);
        _isDoneBall = true;
        _oldTime = Time.time;
    }

    public Gradient GetGradientForCurrentLevel()
    {
        if (_currentLevel >= _towersCube.Count)
        {
            return _gradients[_currentLevel % _towersCube.Count];
        }
        return _gradients[_currentLevel];
    }

    private void ChangeProgress()
    {
        _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, _score / _percent/10, _speedProgressbar);
        CheckWinStatus();
    }

    private void CheckWinStatus()
    {
        if (_score >= _win && _score != 0)
        {
            _nextLvl.fillAmount = Mathf.Lerp(_nextLvl.fillAmount, 1, _speedProgressbar / 10);
            _isLeveldone = true;
            _textEndLevel.SetActive(true);
        }
    }
    private void RetryLevel()
    {
        _destroyBlock = (int)(_progressBar.fillAmount * 100);
        _percentText.text = _destroyBlock + "%";
        _retryLevel.SetActive(true);
        _isRetry = true;
    }
    
    /*
     * * For testing
     * */
    private void DeleteProgressLevel()
    {
        PlayerPrefs.SetInt("level", 0);
        RestartLevel();
    }
    
    private void TestInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteProgressLevel();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }
    }
}
