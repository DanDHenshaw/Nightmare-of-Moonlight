using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    void Start()
    {
        _text.SetText("You Reached Floor " + GameManager.Instance.floorNumber.ToString());
    }

    public void Play()
    {
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadSceneAsync("Tutorial_Room");
    }

    public void Quit()
    {
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadSceneAsync("MenuScene");
    }
}
