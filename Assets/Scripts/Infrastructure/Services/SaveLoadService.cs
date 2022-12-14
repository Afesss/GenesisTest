using System;
using System.Collections.Generic;
using Infrastructure.Data;
using UI;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.Services
{
    public class SaveLoadService
    {
        private const string GameDataTitle = "GameData";
        public GameData Data { get; private set; }

        public SaveLoadService()
        {
            Load();
        }

        public void SaveMusicVolume(int volume)
        {
            Data.MusicVolume = volume;
            Save();
        }

        public void SaveSoundVolume(int volume)
        {
            Data.SondVolume = volume;
            Save();
        }

        public void AddColumnObject(int objIndex, ColumnType columnType)
        {
            switch (columnType)
            {
                case ColumnType.Left:
                    Data.LeftColumnHideObjects.Add(objIndex);
                    break;
                case ColumnType.Middle:
                    Data.MiddleColumnHideObjects.Add(objIndex);
                    break;
                case ColumnType.Right:
                    Data.RightColumnHideObjects.Add(objIndex);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(columnType), columnType, null);
            }
            Save();
        }

        public void RemoveColumnObjects(ColumnType columnType)
        {
            switch (columnType)
            {
                case ColumnType.Left:
                    Data.LeftColumnHideObjects.Clear();
                    break;
                case ColumnType.Middle:
                    Data.MiddleColumnHideObjects.Clear();
                    break;
                case ColumnType.Right:
                    Data.RightColumnHideObjects.Clear();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(columnType), columnType, null);
            }

            Save();
        }

        private void Save()
        {
            PlayerPrefs.SetString(GameDataTitle, JsonUtility.ToJson(Data));
        }

        private void Load()
        {
            string text = PlayerPrefs.GetString(GameDataTitle, null);
            if (string.IsNullOrEmpty(text))
            {
                Data = new GameData
                {
                    SondVolume = 1,
                    MusicVolume = 1,
                    LeftColumnHideObjects = new List<int>(),
                    MiddleColumnHideObjects = new List<int>(),
                    RightColumnHideObjects = new List<int>()
                };
                return;
            }
            Data = JsonUtility.FromJson<GameData>(text);
        }
    }
}