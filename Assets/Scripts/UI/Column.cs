using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace UI
{
    public class Column : MonoBehaviour
    {
        [SerializeField] private ColumnType _type;
        [SerializeField] private Card[] _cards;

        public event Action ColumnSizeChanged;

        public float ContentSize
        {
            get
            {
                if (_activeCards.Count == 0)
                    return 0;
                return _localPositions[0].y - _localPositions[_activeCards.Count - 1].y + CardHeight;
            }
        }
            

        private const int CardHeight = 400;
        
        private Vector3[] _localPositions;
        private List<Card> _activeCards;
        private List<int> _disabledCards;

        private SaveLoadService _saveLoadService;

        [Inject]
        private void Construct(ServiceRegistrator serviceRegistrator)
        {
            _saveLoadService = serviceRegistrator.SaveLoadService;
        }

        public void Activate()
        {
            _localPositions = new Vector3[_cards.Length];
            _activeCards = _cards.ToList();
            for (int i = 0; i < _cards.Length; i++)
            {
                _localPositions[i] = _cards[i].GetLocalPosition();
                _cards[i].Activate(i);
                _cards[i].Disabled += CardOnDisabled;
            }

            LoadState();
        }

        public void RestartCards()
        {
            _activeCards = _cards.ToList();
            for (int i = 0; i < _cards.Length; i++)
            {
                _cards[i].RestartCard(_localPositions[i]);
            }
            _saveLoadService.RemoveColumnObjects(_type);
        }

        private void LoadState()
        {
            switch (_type)
            {
                case ColumnType.Left:
                    _disabledCards = _saveLoadService.Data.LeftColumnHideObjects;
                    break;
                case ColumnType.Middle:
                    _disabledCards = _saveLoadService.Data.MiddleColumnHideObjects;
                    break;
                case ColumnType.Right:
                    _disabledCards = _saveLoadService.Data.RightColumnHideObjects;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            for (int i = 0; i < _disabledCards.Count; i++)
            {
                _activeCards.Remove(_cards[_disabledCards[i]]);
                _cards[_disabledCards[i]].Disable();
            }
            UpdateCardPositions();
            ColumnSizeChanged?.Invoke();
        }

        private void CardOnDisabled(Card card)
        {
            _saveLoadService.AddColumnObject(card.Index, _type);
            _activeCards.Remove(card);
            SmoothUpdateCardPositions();
            ColumnSizeChanged?.Invoke();
        }

        private void UpdateCardPositions()
        {
            for (int i = 0; i < _activeCards.Count; i++)
            {
                _activeCards[i].SetLocalPosition(_localPositions[i]);
            }
        }

        private void SmoothUpdateCardPositions()
        {
            for (int i = 0; i < _activeCards.Count; i++)
            {
                _activeCards[i].SmoothLocalMove(_localPositions[i]);
            }
        }
    }
}
