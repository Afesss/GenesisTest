using System;
using Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Utilities
{
    public class BundleSpriteSetter : MonoBehaviour
    {
        [SerializeField] private SpriteType _spriteType;
        
        private Image _image;

        [Inject]
        private void Construct(ServiceRegistrator serviceRegistrator)
        {
            _image = GetComponent<Image>();
            switch (_spriteType)
            {
                case SpriteType.Jack:
                    _image.sprite = serviceRegistrator.AssetBundleService.Jack;
                    break;
                case SpriteType.Lady:
                    _image.sprite = serviceRegistrator.AssetBundleService.Lady;
                    break;
                case SpriteType.King:
                    _image.sprite = serviceRegistrator.AssetBundleService.King;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public enum SpriteType
        {
            Jack,
            Lady,
            King
        }
    }
}
