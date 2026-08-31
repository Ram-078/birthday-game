using UnityEngine;
using UnityEngine.UI;

public class  SoundFxManager: MonoBehaviour
{
   private static SoundFxManager instance;
   
   private static AudioSource audioSource;
   private static AudioSource voiceAudioSource;
   private static SoundFxLibrary soundFxLibrary;
   [SerializeField] private Slider sfxslider;

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
         AudioSource[] audioSources = GetComponents<AudioSource>();
         audioSource = audioSources[0];
         voiceAudioSource = audioSources[1];
         soundFxLibrary = GetComponent<SoundFxLibrary>();
      }
      else
      {
         Destroy(gameObject);
      }
   }

   public static void Play(string soundName)
   {
      AudioClip clip = soundFxLibrary.GetRandomSoundFx(soundName);
      if (clip != null)
      {
         audioSource.PlayOneShot(clip);
      }
   }

   public static void PlayVoice(AudioClip audioClip, float pitch = 1f)
   {
      voiceAudioSource.pitch = pitch;
      voiceAudioSource.PlayOneShot(audioClip);
   }

   
      void Start()
      {
         if (sfxslider != null)
         {
            sfxslider.onValueChanged.AddListener(delegate { OnValueChanged(); });
         }
      }   
      
   public static void SetVolume(float volume)
   {
      audioSource.volume = volume;
      voiceAudioSource.volume = volume;
   }

   public void OnValueChanged()
   {
      SetVolume(sfxslider.value);
   }
}
