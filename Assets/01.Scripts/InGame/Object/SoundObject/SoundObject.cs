using UnityEngine;
using UnityEngine.Audio;

namespace SoundManage
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundObject : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer _audioMixer;
        [SerializeField] private SoundPack _soundPack;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            //_audioSource.outputAudioMixerGroup = _audioMixer.outputAudioMixerGroup;
        }

        public void Play(int id)
        {
            if (_soundPack == null)
            {
                Debug.LogWarning("[SoundObject] soundPack is null");
                return;
            }
            
            SoundCell cell = _soundPack.FindSound(id);
            if (cell.id == -1)
            {
                Debug.LogWarning($"[SoundObject] soundCell ID is not exist (ID:{id})");
                return;
            }
            _audioSource.PlayOneShot(cell._AudioClip);
        }
        
        public void Play(string name)
        {
            if (_soundPack == null)
            {
                Debug.LogWarning("[SoundObject] soundPack is null");
                return;
            }
            
            SoundCell cell = _soundPack.FindSound(name);
            if (cell.id == -1)
            {
                Debug.LogWarning($"[SoundObject] soundCell NAME is not exist (NAME:{name})");
                return;
            }
            _audioSource.PlayOneShot(cell._AudioClip);
        }
    }

}