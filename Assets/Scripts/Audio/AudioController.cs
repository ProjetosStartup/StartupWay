using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    
    private void Start()
    {
        Play("MainTheme");
        
    }
   

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
    public void StopLooping(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if(s.source.loop)
            s.source.Stop();
    }
    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return false;
        }
        return s.source.isPlaying;
    }

    public void MuteAudio(string name,bool mute)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.mute = mute;
    }
    public void SetVolume(bool isBMG,float percent)
    {
        Sound[] ss = Array.FindAll(sounds, sound => sound.isBGM == isBMG);
      
       
        

        if (ss.Length == 0)
        {
            Debug.LogWarning("Not Found Sound with isBMG = "+ isBMG);
        }
        
        foreach(Sound s in ss)
        {
            s.source.volume = s.volume * percent;
        }
    }

    public float GetVolume(bool isBMG)
    {
        Sound[] ss = Array.FindAll(sounds, sound => sound.isBGM == isBMG);

        return ss[0].volume;
    }
}
