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
    [SerializeField] private TextMeshProUGUI _DestroyText;
    [SerializeField] private GameObject _TextEndLevel;
    [SerializeField] private GameObject _RetryLevel;
    [SerializeField] private Image _progressBar;
    [SerializeField] private Image _nextLvl;
    [SerializeField, Range(0, 1)] private float _winDelta;
    [SerializeField, Range(0, 5)] private float _speedProgressbar;
    [SerializeField] private List<Gradient> _gradients;
    [SerializeField] private List<GameObject> _towersCube;
    private int _destroyBlock;
    public float _win; // change from public to private
    public float _percent; // change from public to private
    public float _score = 0; // change from public to private
    public float _blockNum = 0; // change from public to private
    public int _currentLevel; // change from public to private
    public bool _isLeveldone; // change from public to private
    public bool _isDoneBall;
    private bool _isRetry;
    private float _oldTime;
    [SerializeField]private float _waitTime;
    
    public static GameManager Gm;
    
    
    void Awake()
    {
        if (Gm == null)
        {
            Gm = this;
        }
    }

    void Start()
    {
        _oldTime = Time.time;
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

    void Update()
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
        
        if (_isLeveldone && Input.GetMouseButtonDown(0))
        {
            NextLevel();
        }
        if (_isRetry && Input.GetMouseButtonDown(0))
        {
            RestartLevel();
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

    public Gradient GetGradientForCurrentLevel()
    {
        if (_currentLevel >= _towersCube.Count)
        {
            return _gradients[_currentLevel % _towersCube.Count];
        }
        return _gradients[_currentLevel];
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

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("level", _currentLevel+1);
        RestartLevel();
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
            _TextEndLevel.SetActive(true);
        }
    }
    public void RetryLevel()
    {
        _destroyBlock = (int)_progressBar.fillAmount * 100;
        _DestroyText.text = _destroyBlock + "%";
        _RetryLevel.SetActive(true);
        _isRetry = true;
    }
    
    /*
     * For testing
    */
    public void DeleteProgressLevel()
    {
        PlayerPrefs.SetInt("level", 0);
        RestartLevel();
    }

    public IEnumerator BallDone()
    {
        yield return new WaitForSeconds(_waitTime);
        _isDoneBall = true;
    }
}
