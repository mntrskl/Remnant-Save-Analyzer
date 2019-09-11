using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Text;

namespace Remnant
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            rForm = new Form1();
            Application.Run(rForm);
        }
        public static Form rForm;
        public static FileSystemWatcher saveWatcher;
        public static string currentSave = "";
        public delegate void CampaignReroll(string saveText, List<RemnantEvent> events);
        public static CampaignReroll OnReroll;
        static Dictionary<string, string> sublocations = new Dictionary<string, string>
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
        static Dictionary<string, string> mainLocations = new Dictionary<string, string>{
            {"City Overworld Zone1", "Fairview" },
            {"City Overworld Zone2", "Westcourt" },
            {"Wasteland Overworld Zone1", "TheEasternWind" },
            {"Wasteland Overworld Zone2", "TheScouringWaste" },
            {"Jungle Overworld Zone1", "TheVerdantStrand" },
            {"Jungle Overworld Zone2", "TheScaldingGlade" },
            {"Swamp Overworld Zone1", "TheFetidGlade" },
            {"Swamp Overworld Zone2", "TheMistFen" }
        };
        public static void WatchForChanges(string savePath)
        {
            if (saveWatcher != null)
            {
                saveWatcher.Dispose();
            }

            saveWatcher = new FileSystemWatcher()
            {
                Path = Path.GetDirectoryName(savePath),
                Filter = Path.GetFileName(savePath)
            };
            saveWatcher.Changed += OnChangedAsync;
            saveWatcher.EnableRaisingEvents = true;

            ParseSave(savePath);
        }
        private static void ParseSave(string filePath)
        {
            try
            {
                //rForm.Controls[1].InvokeEx(x => x.Text = "Loading...");

                StringBuilder bob = new StringBuilder();
                List<RemnantEvent> campaignEvents = new List<RemnantEvent>();

                while (!WaitForFile(filePath))
                    Task.Delay(1000);

                string text = File.ReadAllText(filePath);
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
                        {
                            eventName = eventName
                                .Replace("TheRisen", "Reanimators")
                                .Replace("LizAndLiz", "LizChicagoTypewriter")
                                .Replace("Fatty", "TheUncleanOne")
                                .Replace("WastelandGuardian", "Claviger")
                                .Replace("RootEnt", "EntBoss")
                                .Replace("Wolf", "TheRavager")
                                .Replace("RootDragon", "Singe")
                                .Replace("SwarmMaster", "Scourge")
                                .Replace("RootWraith", "Shroud")
                                .Replace("RootTumbleweed", "TheMangler")
                                .Replace("Kincaller", "Warden")
                                .Replace("Tyrant", "Thrall")
                                .Replace("Vyr", "ShadeAndShatter")
                                .Replace("ImmolatorAndZephyr", "ScaldAndSear")
                                .Replace("RootBrute", "Gorefist")
                                .Replace("SlimeHulk", "Canker")
                                .Replace("BlinkFiend", "Onslaught")
                                .Replace("Sentinel", "Raze")
                                .Replace("Penitent", "Letos Amulet")
                                .Replace("LastWill", "SupplyRunAssaultRifle")
                                .Replace("SwampGuardian", "Ixillis");
                        }

                        if (zone != "" && eventType != "" && eventName != "")
                        {
                            string value;
                            if (zones[zone].TryGetValue(eventType, out value))
                            //if (zones[zone][eventType] != "")
                            {
                                if (zones[zone][eventType].IndexOf(eventName) == -1)
                                {
                                    zones[zone][eventType] += ", " + eventName;
                                    //Console.WriteLine($"{zone}, {currentMainLocation}, {currentSublocation}, {eventType}, {eventName}");
                                    bob.AppendLine($"{zone}, {currentMainLocation}, {currentSublocation}, {eventType}, {eventName}");
                                    campaignEvents.Add(new RemnantEvent(zone, currentMainLocation, currentSublocation, eventType, eventName));
                                }
                            }
                            else
                            {
                                zones[zone][eventType] = eventName;
                                //Console.WriteLine($"{zone}, {currentMainLocation}, {currentSublocation}, {eventType}, {eventName}");
                                bob.AppendLine($"{zone}, {currentMainLocation}, {currentSublocation}, {eventType}, {eventName}");
                                campaignEvents.Add(new RemnantEvent(zone, currentMainLocation, currentSublocation, eventType, eventName));
                            }
                        }
                    }
                }
                currentSave = bob.ToString();
                OnReroll?.Invoke(currentSave, campaignEvents);
                //rForm.Controls[1].InvokeEx(x => x.Text = currentSave);
            }
            catch (FileNotFoundException ex)
            {
                //rForm.Controls[1].InvokeEx(x => x.Text = ex.Message);
            }
        }
        public static void SaveCSV(string dirPath)
        {
            File.WriteAllText(dirPath, currentSave);
        }
        private static void OnChangedAsync(object source, FileSystemEventArgs e) =>
            ParseSave(e.FullPath);
        //public static void InvokeEx<T>(this T @this, Action<T> action) where T : ISynchronizeInvoke
        //{
        //    if (@this.InvokeRequired)
        //    {
        //        @this.Invoke(action, new object[] { @this });
        //    }
        //    else
        //    {
        //        action(@this);
        //    }
        //}
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
                    //Log.LogWarning(
                    //   "WaitForFile {0} failed to get an exclusive lock: {1}",
                    //    fullPath, ex.ToString());

                    if (numTries > 10)
                    {
                        //Log.LogWarning(
                        //    "WaitForFile {0} giving up after 10 tries",
                        //    fullPath);
                        return false;
                    }

                    // Wait for the lock to be released
                    System.Threading.Thread.Sleep(500);
                }
            }

            //Log.LogTrace("WaitForFile {0} returning true after {1} tries",
            //    fullPath, numTries);
            return true;
        }

    }
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
}
