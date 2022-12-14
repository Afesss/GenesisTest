using DG.Tweening;
using UnityEngine;

namespace Infrastructure.Services
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;

        public void Construct(int volume)
        {
            _audio.volume = volume;
        }

        public void SetVolume(float volume)
        {
            DOTween.To(()=> _audio.volume, x => _audio.volume = x, volume, 1);
        }

        public void Play(AudioClip clip)
        {
            _audio.clip = clip;
            _audio.Play();
        }
    }
}
