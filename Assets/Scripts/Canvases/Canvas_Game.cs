using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Canvas_Game : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _currentVolume;
    [SerializeField] PlayerController _playerController;

    private Canvas _gameCanvas;

    private void Start()
    {
        _gameCanvas = GetComponent<Canvas>();
        _gameCanvas.enabled = true; 
    }

    private void OnEnable()
    {
      _playerController.EventCurrentVolumeUpdate += PrintVolume;
      _playerController.EventGameOver += HideGameCanvas;
      _playerController.EventGameWin += HideGameCanvas;
      
    }

    private void OnDisable()
    {
        _playerController.EventCurrentVolumeUpdate -= PrintVolume;
        _playerController.EventGameOver -= HideGameCanvas;
        _playerController.EventGameWin -= HideGameCanvas;
    }

    private void PrintVolume(int volume)
    {
        _currentVolume.text = volume.ToString();
    }

    private void HideGameCanvas(int volume)
    {
        _gameCanvas.enabled = false;
    }

}
