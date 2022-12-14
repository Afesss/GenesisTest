using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BottomPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform _contentRect;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Column[] _columns;

        private void Start()
        {
            _restartButton.onClick.AddListener(RestartColumns);
            foreach (Column column in _columns)
            {
                column.Activate();
                column.ColumnSizeChanged += OnColumnSizeChanged;
            }

            _contentRect.sizeDelta = new Vector2(0, _columns[0].ContentSize);
        }

        private void OnColumnSizeChanged()
        {
            float maxSize = 0;
            foreach (Column column in _columns)
            {
                if (maxSize < column.ContentSize)
                {
                    maxSize = column.ContentSize;
                }
            }
            _contentRect.sizeDelta = new Vector2(0, maxSize);
        }

        private void RestartColumns()
        {
            foreach (Column column in _columns)
            {
                column.RestartCards();
            }

            OnColumnSizeChanged();
        }
    }
}
