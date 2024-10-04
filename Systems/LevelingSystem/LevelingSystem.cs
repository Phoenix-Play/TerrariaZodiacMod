using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ZodiacMod.Systems.LevelingSystem
{
    public class LevelingSystem : ModSystem
    {
        public int Level { get; set; }

        public int Experience { get; set; }

        private int GetExperienceToNextLevel(int level)
        {
            int difficultyMultiplier = Main.expertMode ? (Main.masterMode ? 5 : 2) : 1;
            int baseExperience = 100 * difficultyMultiplier;

            return (int)(baseExperience * Math.Pow(level, 2));
        }

        public void AddExperience(int amount)
        {
            CombatText.NewText(Main.LocalPlayer.getRect(), new Microsoft.Xna.Framework.Color(255, 215, 0), $"+{amount} EXP");
            Experience += amount;
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            int experienceToNextLevel = GetExperienceToNextLevel(Level);
            if (Experience >= experienceToNextLevel)
            {
                Level++;
                Experience = 0;
            }
        }

        public List<int> GetDataToSave()
        {
            return new List<int>
            {
               Level,
               Experience
            };
        }

        public void LoadData(int level, int experience)
        {
            Level = level;
            Experience = experience;
            CheckLevelUp();

            Main.NewText($"[Zodiac Mod]: LOADDATA Level: {Level}");
            Main.NewText($"[Zodiac Mod]: LOADDATA Experience: {Experience}");
        }
    }
}
