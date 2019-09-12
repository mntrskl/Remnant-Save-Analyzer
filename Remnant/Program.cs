using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Text;
using System.Runtime.Caching;

namespace Remnant
{
    #region Extensions & Helpers
    public static class Extensions
    {
        public static void AddIf(this TreeNodeCollection collection, TreeNode node)
        {
            if (!collection.ContainsKey(node.Name))
            {
                collection.Add(node);
            }
        }
    }
    #endregion

    #region Event Data
    public class RemnantEvent
    {
        public string zone;
        public string mainLocation;
        public string subLocation;
        public string eventType;
        public string eventName;
        public RemnantEvent(string zone, string mainLocation, string subLocation, string eventType, string eventName)
        {
            this.zone = zone;
            this.mainLocation = mainLocation;
            this.subLocation = subLocation;
            this.eventType = eventType;
            this.eventName = eventName;
        }
    }
    #endregion

    static class Program
    {
        #region Mapping
        static readonly Dictionary<string, string> mainLocations = new Dictionary<string, string>{
            {"City Overworld Zone1", "Fairview" },
            {"City Overworld Zone2", "Westcourt" },
            {"Wasteland Overworld Zone1", "TheEasternWind" },
            {"Wasteland Overworld Zone2", "TheScouringWaste" },
            {"Jungle Overworld Zone1", "TheVerdantStrand" },
            {"Jungle Overworld Zone2", "TheScaldingGlade" },
            {"Swamp Overworld Zone1", "TheFetidGlade" },
            {"Swamp Overworld Zone2", "TheMistFen" }
        };
        static readonly Dictionary<string, string> sublocations = new Dictionary<string, string>
        {
            {"RootCultist", "MarrowPass" },
            {"RootWraith", "TheHiddenSanctum" },
            {"RootBrute", "SunkenPassage" },
            {"Brabus", "CutthroatChannel" },
            {"RootTumbleweed", "TheTangledPass" },
            {"RootEnt", "TheChokingHollow" },
            {"RootDragon", "TheAshYard" },
            {"HuntersHideout", "HiddenGrotto" },
            {"MadMerchant", "Junktown" },
            {"LizAndLiz", "TheWarren" },
            {"LastWill", "FindMonkeyKey" },
            {"RootShrine", "TheGallows" },
            {"SwarmMaster", "TheIronRift" },
            {"HoundMaster","TheBurrows" },
            {"Sentinel", "ShackledCanyon" },
            {"Vyr", "TheArdentTemple" },
            {"WastelandGuardian", "LoomOfTheBlackSun" },
            {"TheHarrow", "TheBunker" },
            {"TheLostGantry", "ConcourseOfTheSun" },
            {"ArmorVault", "VaultOfTheHeralds" },
            {"TheCleanRoom", "ThePurgeHall" },
            {"SlimeHulk", "TheDrownedTrench" },
            {"Fatty", "TheFetidGlade" },
            {"Tyrant", "TheCapillary" },
            {"SwampGuardian", "The Grotto" },
            {"KinCaller", "TheHallOfJudgement" },
            {"BlinkFiend", "Widow'sPass" },
            {"StuckMerchant", "MerchantDungeon" },
            {"BlinkThief", "ForgottenUndercroft" },
            {"StormCaller", "Heretic'sNest" },
            {"ImmolatorAndZephyr", "WitheringVillage" },
            {"Wolf", "TheScaldingGlade" },
            {"TotemFather", "TheScaldingGlade" },
            {"TheRisen", "Ahanae'sLament" },
            {"DoeShrine", "Widow'sVestry" },
            {"WolfShrine", "Martyr'sSanctuary" },
            {"UndyingKing", "Pussy'sPyramid" }
        };
        static readonly Dictionary<string, string> eventNames = new Dictionary<string, string>
        {
            {"TheRisen", "Reanimators" },
            {"LizAndLiz", "LizChicagoTypewriter" },
            {"Fatty", "TheUncleanOne" },
            {"WastelandGuardian", "Claviger" },
            {"RootEnt", "EntBoss" },
            {"Wolf", "TheRavager" },
            {"RootDragon", "Singe" },
            {"SwarmMaster", "Scourge" },
            {"RootWraith", "Shroud" },
            {"RootTumbleweed", "TheMangler" },
            {"Kincaller", "Warden" },
            {"Tyrant", "Thrall" },
            {"Vyr", "ShadeAndShatter" },
            {"ImmolatorAndZephyr", "ScaldAndSear" },
            {"RootBrute", "Gorefist" },
            {"SlimeHulk", "Canker" },
            {"BlinkFiend", "Onslaught" },
            {"Sentinel", "Raze" },
            {"Penitent", "Letos Amulet" },
            {"LastWill", "SupplyRunAssaultRifle" },
            {"SwampGuardian", "Ixillis" }
        };
        #endregion

        // Watcher
        public static FileSystemWatcher saveWatcher;
        public static string currentSaveFile = "";
        public static string currentSaveCSV = "";
        // Delegates
        public delegate void CampaignReroll(string saveText, List<RemnantEvent> events);
        public static CampaignReroll OnReroll;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void ParseSave()
        {
            try
            {
                StringBuilder bob = new StringBuilder();
                List<RemnantEvent> campaignEvents = new List<RemnantEvent>();

                while (!WaitForFile(currentSaveFile))
                    Task.Delay(1000);

                string text = File.ReadAllText(currentSaveFile);
                text = text
                    .Split(new string[] { "/Game/Campaign_Main/Quest_Campaign_Ward13.Quest_Campaign_Ward13" }, StringSplitOptions.None)[0];
                text = text
                    .Split(new string[] { "/Game/Campaign_Main/Quest_Campaign_City.Quest_Campaign_City" }, StringSplitOptions.None)[1];
                text = text.Replace("/Game", "\n");
                //text = System.Text.RegularExpressions.Regex.Replace(text, @"/Game/g", "\n", System.Text.RegularExpressions.RegexOptions);
                string[] textArray = text.Split('\n');

                Dictionary<string, Dictionary<string, string>> zones = new Dictionary<string, Dictionary<string, string>>() {
                    { "Earth", new Dictionary<string, string>() },
                    { "Rhom", new Dictionary<string, string>() },
                    { "Yaesha", new Dictionary<string, string>() },
                    { "Corsus", new Dictionary<string, string>() }
                };

                string currentMainLocation = "Fairview";
                string currentSublocation = "";

                for (int i = 0; i < textArray.Length; i++)
                {
                    string zone = "";
                    string lastEventName = "";
                    string eventType = "";
                    string eventName = "";
                    bool inSmallDungeon = true;

                    string textLine = textArray[i];

                    if (textLine.IndexOf("World_City") != -1)
                        zone = "Earth";

                    if (textLine.IndexOf("World_Wasteland") != -1)
                        zone = "Rhom";

                    if (textLine.IndexOf("World_Jungle") != -1)
                        zone = "Yaesha";

                    if (textLine.IndexOf("World_Swamp") != -1)
                        zone = "Corsus";

                    lastEventName = eventName;
                    if (textLine.IndexOf("SmallD") != -1)
                    {
                        eventType = "Side Dungeon";
                        eventName = textLine.Split('/')[3].Split('_')[2];
                        currentSublocation = sublocations[eventName];
                        inSmallDungeon = true;

                    }
                    if (textLine.IndexOf("Quest_Boss") != -1)
                    {
                        eventType = "World Boss";
                        eventName = textLine.Split('/')[3].Split('_')[(textLine.IndexOf("UndyingKing") != -1) ? 1 : 2];
                        currentSublocation = sublocations[eventName];
                    }
                    if (textLine.IndexOf("Siege") != -1)
                    {
                        eventType = "Siege";
                        eventName = textLine.Split('/')[3].Split('_')[2];
                        currentSublocation = sublocations[eventName];
                    }
                    if (textLine.IndexOf("Mini") != -1)
                    {
                        eventType = "Miniboss";
                        eventName = textLine.Split('/')[3].Split('_')[2];
                        currentSublocation = sublocations[eventName];
                    }
                    if (textLine.IndexOf("Quest_Event") != -1)
                    {
                        eventType = "Item Drop";
                        eventName = textLine.Split('/')[3].Split('_')[2];
                    }

                    if (textLine.IndexOf("Overworld_Zone") != -1)
                    {
                        currentMainLocation = textLine.Split('/')[3].Split('_')[1] + " " + textLine.Split('/')[3].Split('_')[2] + " " + textLine.Split('/')[3].Split('_')[3];
                        currentMainLocation = mainLocations[currentMainLocation];
                    }

                    // Show Data
                    if (eventName != lastEventName)
                    {
                        if (eventName != "")
                            if (eventNames.ContainsKey(eventName))
                                eventName = eventNames[eventName];

                        if (zone != "" && eventType != "" && eventName != "")
                        {
                            string value;
                            if (zones[zone].TryGetValue(eventType, out value))
                            {
                                if (zones[zone][eventType].IndexOf(eventName) == -1)
                                {
                                    zones[zone][eventType] += ", " + eventName;
                                    bob.AppendLine($"{zone}, {currentMainLocation}, {currentSublocation}, {eventType}, {eventName}");
                                    campaignEvents.Add(new RemnantEvent(zone, currentMainLocation, currentSublocation, eventType, eventName));
                                }
                            }
                            else
                            {
                                zones[zone][eventType] = eventName;
                                bob.AppendLine($"{zone}, {currentMainLocation}, {currentSublocation}, {eventType}, {eventName}");
                                campaignEvents.Add(new RemnantEvent(zone, currentMainLocation, currentSublocation, eventType, eventName));
                            }
                        }
                    }
                }
                currentSaveCSV = bob.ToString();
                OnReroll?.Invoke(bob.ToString(), campaignEvents);
            }
            catch (FileNotFoundException ex)
            {

            }
        }

        private static MemoryCache _memCache;
        private static CacheItemPolicy _cacheItemPolicy;
        private const int CacheTimeMilliseconds = 1000;
        public static void StartWatching()
        {
            _memCache = MemoryCache.Default;

            if (saveWatcher != null)
            {
                saveWatcher.Changed -= OnSaveChanged;
                saveWatcher.Dispose();
                saveWatcher = null;
            }

            saveWatcher = new FileSystemWatcher()
            {
                Path = Path.GetDirectoryName(currentSaveFile),
                Filter = Path.GetFileName(currentSaveFile),
                NotifyFilter = NotifyFilters.LastWrite
            };

            _cacheItemPolicy = new CacheItemPolicy() { RemovedCallback = OnRemovedFromCache };

            saveWatcher.Changed += OnSaveChanged;
            saveWatcher.EnableRaisingEvents = true;
        }

        public static void StopWatching()
        {
            if (saveWatcher != null)
            {
                saveWatcher.Changed -= OnSaveChanged;
                saveWatcher.Dispose();
                saveWatcher = null;
            }
        }

        static bool WaitForFile(string fullPath)
        {
            int numTries = 0;
            while (true)
            {
                ++numTries;
                try
                {
                    // Attempt to open the file exclusively.
                    using (FileStream fs = new FileStream(fullPath,
                        FileMode.Open, FileAccess.Read,
                        FileShare.None, 100))
                    {
                        fs.ReadByte();

                        // If we got this far the file is ready
                        break;
                    }
                }
                catch (Exception ex)
                {
                    if (numTries > 10)
                        return false;

                    // Wait for the lock to be released
                    System.Threading.Thread.Sleep(500);
                }
            }
            return true;
        }

        public static void SaveCSV(string dirPath) => File.WriteAllText(dirPath, currentSaveCSV);

        private static void OnSaveChanged(object source, FileSystemEventArgs e) {
            _cacheItemPolicy.AbsoluteExpiration =
           DateTimeOffset.Now.AddMilliseconds(CacheTimeMilliseconds);

            // Only add if it is not there already (swallow others)
            _memCache.AddOrGetExisting(e.Name, e, _cacheItemPolicy);
        }
        private static void OnRemovedFromCache(CacheEntryRemovedArguments args)
        {
            if (args.RemovedReason != CacheEntryRemovedReason.Expired) return;
            // Now actually handle file event
            var e = (FileSystemEventArgs)args.CacheItem.Value;
            ParseSave();
        }
    }
}
