using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ZodiacMod.Content.Items.Swords.TrainingSword;
using ZodiacMod.Systems.LevelingSystem;

namespace ZodiacMod
{
    public class ZodiacModPlayer : ModPlayer
    {
        // Monster Killing to Level Up
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // check if the npc is not a town npc
            if (!target.townNPC)
            {
                // Check if npc died
                if (target.life <= 0)
                {
                    // Add experience to the player's leveling system
                    ModContent.GetInstance<LevelingSystem>().AddExperience(target.lifeMax / 10);
                }
            }
        }

        // Saving and Loading Data
        public override void SaveData(TagCompound tag)
        {
            // Save the player's leveling system data
            List<int> LevelData = ModContent.GetInstance<LevelingSystem>().GetDataToSave();
            tag.Set("ZodiacMod:LevelingSystem:Level", LevelData[0], true);
            tag.Set("ZodiacMod:LevelingSystem:Experience", LevelData[1], true);
        }

        public override void LoadData(TagCompound tag)
        {
            // Load the player's leveling system data
            int level = tag.GetInt("ZodiacMod:LevelingSystem:Level");
            int experience = tag.GetInt("ZodiacMod:LevelingSystem:Experience");
            ModContent.GetInstance<LevelingSystem>().LoadData(level, experience);
        }

        // Starting Inventory
        public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath)
        {
            // Replace the Copper Shortsword with the Training Sword in the player's starting inventory
            itemsByMod["Terraria"].RemoveAll(item => item.type == ItemID.CopperShortsword);
            itemsByMod["Terraria"].Insert(0, new Item(ModContent.ItemType<TrainingSword>()));

            // Add a stack of 50 torches to the player's starting inventory
            itemsByMod["Terraria"].Add(new Item(ItemID.Torch, 50));
        }
    }
}
