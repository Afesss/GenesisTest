using System;
using System.Collections;
using UnityEngine;

namespace Particle
{
    public class CardParticle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;


        public void Activate(Sprite sprite)
        {
            _particleSystem.textureSheetAnimation.SetSprite(0, sprite);
            _particleSystem.Play();
            StartCoroutine(WaitDestroy());
        }

        private IEnumerator WaitDestroy()
        {
            while (_particleSystem.isPlaying)
            {
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}
