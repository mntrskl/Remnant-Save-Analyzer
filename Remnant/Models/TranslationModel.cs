using System;
using System.Collections.Generic;

namespace Remnant.Models
{
    public class EventTranslationModel
    {

        private static readonly Dictionary<string, string> zoneNameTranslation;

        private static readonly Dictionary<string, string> locationNameTranslation;

        private static readonly Dictionary<string, string> subZoneNameTranslation;

        private static readonly Dictionary<string, string> eventNameTranslation;

        static EventTranslationModel()
        {
            zoneNameTranslation = new Dictionary<string, string>{
                { "World_City", "Earth" },
                { "World_Wasteland", "Rhom" },
                { "World_Jungle", "Yaesha" },
                { "World_Swamp", "Corsus" }
            };

            locationNameTranslation = new Dictionary<string, string>{
                {"RootCultist", "Marrow Pass" },
                {"RootWraith", "The Hidden Sanctum" },
                {"Root Brute", "Sunken Passage" },
                {"Brabus", "Cutthroat Channel" },
                {"RootTumbleweed", "The Tangled Pass" },
                {"RootEnt", "The Choking Hollow" },
                {"RootDragon", "The Ash Yard" },
                {"HuntersHideout", "Hidden Grotto" },
                {"MadMerchant", "Junktown" },
                {"LizAndLiz", "The Warren" },
                {"LastWill", "Find Monkey Key" },
                {"RootShrine", "The Gallows" },
                {"SwarmMaster", "The IronRift" },
                {"HoundMaster","The Burrows" },
                {"Sentinel", "Shackled Canyon" },
                {"Vyr", "The Ardent Temple" },
                {"WastelandGuardian", "Loom Of The Black Sun" },
                {"TheHarrow", "The Bunker" },
                {"TheLostGantry", "Concourse Of The Sun" },
                {"ArmorVault", "Vault Of The Heralds" },
                {"TheCleanRoom", "The Purge Hall" },
                {"SlimeHulk", "The Drowned Trench" },
                {"Fatty", "The Fetid Glade" },
                {"Tyrant", "The Capillary" },
                {"SwampGuardian", "The Grotto" },
                {"KinCaller", "The Hall Of Judgement" },
                {"BlinkFiend", "Widow's Pass" },
                {"StuckMerchant", "Merchant Dungeon" },
                {"BlinkThief", "Forgotten Undercroft" },
                {"StormCaller", "Heretic's Nest" },
                {"ImmolatorAndZephyr", "Withering Village" },
                {"Wolf", "The Scalding Glade" },
                {"TotemFather", "The Scalding Glade" },
                {"TheRisen", "Ahanae's Lament" },
                {"DoeShrine", "Widow's Vestry" },
                {"WolfShrine", "Martyr's Sanctuary" },
                {"UndyingKing", "Hall of The Undying" }
            };

            subZoneNameTranslation = new Dictionary<string, string>{
                {"Template_City_Overworld_Zone1", "Fairview" },
                {"Template_City_Overworld_Zone2", "Westcourt" },
                {"Template_Wasteland_Overworld_Zone1", "The Eastern Wind" },
                {"Template_Wasteland_Overworld_Zone2", "The Scouring Waste" },
                {"Template_Jungle_Overworld_Zone1", "The Verdant Strand" },
                {"Template_Jungle_Overworld_Zone2", "The Scalding Glade" },
                {"Template_Swamp_Overworld_Zone1", "The Fetid Glade" },
                {"Template_Swamp_Overworld_Zone2", "The Mist Fen" }
            };

            eventNameTranslation = new Dictionary<string, string>{
                {"TheRisen", "Reanimators"},
                {"LizAndLiz", "Liz Chicago Typewriter"},
                {"Fatty", "The Unclean One"},
                {"WastelandGuardian", "Claviger"},
                {"RootEnt", "Ent Boss"},
                {"Wolf", "The Ravager"},
                {"RootDragon", "Singe"},
                {"SwarmMaster", "Scourge"},
                {"RootWraith", "Shroud"},
                {"RootTumbleweed", "The Mangler"},
                {"Kincaller", "Warden"},
                {"Tyrant", "Thrall"},
                {"Vyr", "Shade And Shatter"},
                {"ImmolatorAndZephyr", "Scald And Sear"},
                {"RootBrute", "Gorefist"},
                {"SlimeHulk", "Canker"},
                {"BlinkFiend", "Onslaught"},
                {"Sentinel", "Raze"},
                {"Penitent", "Letos Amulet"},
                {"LastWill", "Supply Run Assault Rifle"},
                {"SwampGuardian", "Ixillis"}
            };
        }

        public static string TranslateEventZone(string key)
        {
            return zoneNameTranslation[key];
        }

        public static string TranslateEventSubZone(string key)
        {
            return subZoneNameTranslation[key];
        }

        public static string TranslateEventLocation(string key)
        {
            return locationNameTranslation[key];
        }

        public static string TranslateEventName(string key)
        {
            return eventNameTranslation[key];
        }

        public static bool IsValidZone(string key)
        {
            return zoneNameTranslation.ContainsKey(key);
        }

        public static bool IsValidLocation(string key)
        {
            return locationNameTranslation.ContainsKey(key);
        }

        public static bool IsValidName(string key)
        {
            return eventNameTranslation.ContainsKey(key);
        }
    };        
}
