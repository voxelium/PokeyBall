using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Canvas_GameOver : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;

    private Canvas _canvasGameOver;

    private void OnEnable()
    {
      _playerController.EventGameOver += ShowCanvasGameOver;
    }

    private void Awake()
    {
       _canvasGameOver = GetComponent<Canvas>();
       _canvasGameOver.enabled = false;
    }

    private void OnDisable()
    {
        _playerController.EventGameOver -= ShowCanvasGameOver;
    }

    private void ShowCanvasGameOver(int volume)
    {
        _canvasGameOver.enabled = true;
    }

    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("GameScene");
        Debug.Log("кнопка нажата");
    }

}
