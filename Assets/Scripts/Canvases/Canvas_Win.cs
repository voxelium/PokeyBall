using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Canvas_Win : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _winVolume;
    [SerializeField] PlayerController _playerController;

    private Canvas _canvasWin;

    private void OnEnable()
    {
      _playerController.EventGameWin += PrintWinVolume;
    }

    private void Awake()
    {
       _canvasWin = GetComponent<Canvas>();
       _canvasWin.enabled = false;
    }

    private void OnDisable()
    {
       _playerController.EventGameWin -= PrintWinVolume;
    }

    private void PrintWinVolume(int volume)
    {
        _canvasWin.enabled = true;
        _winVolume.text = volume.ToString();
    }

    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("GameScene");
        Debug.Log("кнопка нажата");
    }

}
