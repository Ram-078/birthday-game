using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static AudioSource audioSource;
    public AudioClip bgm;

    [SerializeField] private Slider musicSlider;

    private float musicVolume = 1f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (bgm != null)
        {
            PlayBGM(bgm);
        }

        musicSlider.value = musicVolume;
        musicSlider.onValueChanged.AddListener(SetVolume);

        SetVolume(musicVolume);
    }

    public void PlayBGM(AudioClip audioClip = null)
    {
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
        }

        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        musicVolume = volume;
        audioSource.volume = volume;
    }

    public static void PauseBGM()
    {
        audioSource.volume = 0f;
    }
    
    public static void ResumwBGM()
    {
        audioSource.volume = 1f;
    }
    
    
}