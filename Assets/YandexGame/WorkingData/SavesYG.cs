
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public bool[] OpenLevels;
        public int[] NumberOfStarsLevels;

        public void Init(int numberOfLevels)
        {
            if (OpenLevels == null || OpenLevels.Length != numberOfLevels)
                OpenLevels = new bool[numberOfLevels];

            if (NumberOfStarsLevels == null || NumberOfStarsLevels.Length != numberOfLevels)
                NumberOfStarsLevels = new int[numberOfLevels];
        }

        public void SaveLevels(List<Level> levels)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                OpenLevels[i] = levels[i].IsLevelOpen;
                NumberOfStarsLevels[i] = levels[i].SelectedCoins;
            }
        }
    }
}
