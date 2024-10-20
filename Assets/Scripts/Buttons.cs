using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    private const string SceneToLoad = "2D_Platformer";

    [SerializeField] private Text _text;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    public void AboutAutors()
    {
        _text.enabled = _text.enabled ? false : true;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
