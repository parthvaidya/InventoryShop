using UnityEngine;
using UnityEngine.UI;

public class UISoundTrigger : MonoBehaviour
{
    //Sounds
    [SerializeField] private Sounds soundToPlay = Sounds.ButtonClick;

    private void Awake()
    {
        //Get button components to trigger the listener on sounds
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlaySound);
        }
    }

    //Play the sound
    private void PlaySound()
    {
        SoundManager.Instance.Play(soundToPlay);
    }
}
