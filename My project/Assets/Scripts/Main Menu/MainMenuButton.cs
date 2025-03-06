using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    //add buttons fields
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    private void Start()
    {
        startButton.onClick.AddListener(startGame);
        quitButton.onClick.AddListener(Quit);
    }

    //start the game
    private void startGame()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(1);
    }

    //quit the game
    private void Quit()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(2);
    }
}
