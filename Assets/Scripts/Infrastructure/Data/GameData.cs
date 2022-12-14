using System;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    [Serializable]
    public class GameData
    {
        public int MusicVolume;
        public int SondVolume;
        public List<int> LeftColumnHideObjects;
        public List<int> MiddleColumnHideObjects;
        public List<int> RightColumnHideObjects;
    }
}