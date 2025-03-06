using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //create sound manager instance
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }
    public SoundType[] Sounds;
    public AudioSource soundEffect;
    public AudioSource soundMusic;

    private void Awake()
    {
        //check for duplicate instance
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //play the bachground music
        PlayMusic(global::Sounds.Music);
    }

    public void PlayMusic(Sounds sound)
    {
        //find the audio clip
        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            soundMusic.clip = clip;
            soundMusic.Play();
        } else { 
            Debug.Log("Clip not found"); 
        }
    }

    public void Play(Sounds sound)
    {
        //Play the sound
        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            soundEffect.PlayOneShot(clip);
        }
        else { 
            Debug.Log("Clip not found"); 
        }
    }

    private AudioClip getSoundClip(Sounds sound)
    {
        //create array for sound
        SoundType item = Array.Find(Sounds, i => i.soundType == sound);
        if (item != null)
        {
            return item.soundClip;
        }
        return null;
    }

    //make sound type
    [Serializable]
    public class SoundType
    {
        public Sounds soundType;
        public AudioClip soundClip;
    }
}

//enum for sound types
public enum Sounds
{
    ButtonClick,
    Music,
    PopupMusic,
    MoneyAdded,
    ShopItems,
    ClickItem,
    Warning

    
}