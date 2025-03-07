using UnityEngine;
public class SoundHelper 
{
    //Play Sounds
   public static void PlaySound(Sounds sound)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play(sound);
        } else {
            Debug.Log("SoundManager instance is not available!");
        }
    }

    //Play background music
    public static void PlayMusic(Sounds sound)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayMusic(sound);
        } else {
            Debug.LogWarning("SoundManager instance is not available!");
        }
    }
}
