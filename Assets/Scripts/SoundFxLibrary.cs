using System.Collections.Generic;
using UnityEngine;

public class SoundFxLibrary : MonoBehaviour
{
   
    [SerializeField] private SoundFxGroup[]  soundFxGroups;
    private Dictionary<string, List<AudioClip>> soundDictionary;

    private void Awake()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        soundDictionary = new Dictionary<string, List<AudioClip>>();
        foreach (SoundFxGroup sfxGroup in soundFxGroups)
        {
            soundDictionary[sfxGroup.name] = sfxGroup.audioClips;
        }
    }

    public AudioClip GetRandomSoundFx(string name)
    {
        if (soundDictionary.ContainsKey(name))
        {
            List<AudioClip> audioClips = soundDictionary[name];
            if (audioClips.Count > 0)
                {
                    return audioClips[Random.Range(0, audioClips.Count)];
                }
        }
        return null;
    }
    
     
}

[System.Serializable]

public class SoundFxGroup
{
    public string name;
    public  List<AudioClip> audioClips;
}