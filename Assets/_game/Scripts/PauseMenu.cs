using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private Controls _controls;

    [SerializeField] private GameObject _menu;

    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        _menu.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        _menu.SetActive(false);
    }

    public void Quit()
    {
        Destroy(GameManager.Instance.gameObject);
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("MenuScene");
    }
}
