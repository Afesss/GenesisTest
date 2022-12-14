using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utilities
{
    [RequireComponent(typeof(TMP_Text))]
    public class TMPHyperlink : MonoBehaviour, IPointerClickHandler
    {
        private const string URL = "https://www.instagram.com/afesdaller/";
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Application.OpenURL(URL);
        }
    }
}