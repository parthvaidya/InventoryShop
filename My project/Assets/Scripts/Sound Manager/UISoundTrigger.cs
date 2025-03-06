using UnityEngine;
using UnityEngine.UI;

public class UISoundTrigger : MonoBehaviour
{
    [SerializeField] private Sounds soundToPlay = Sounds.ButtonClick;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlaySound);
        }
    }

    private void PlaySound()
    {
        SoundManager.Instance.Play(soundToPlay);
    }
}
