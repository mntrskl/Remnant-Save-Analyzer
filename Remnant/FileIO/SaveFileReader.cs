using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Remnant.Models;
using System.ComponentModel;

namespace Remnant.FileIO
{
    public class SaveFileReader
    {
        private readonly string _filePath;

        public SaveFileReader(string filePath)
        {
            _filePath = filePath;
            namesEncountered = new HashSet<string>();
        }
        
        private string _subZone;
        private string _location;
        private HashSet<string> namesEncountered;

        public List<EventModel> ParseCampaign()
        {
            while (!WaitForFile())
            {
                Task.Delay(1000);
            }

            string[] textArray = ReadCampaign().Split('\n');
            List<EventModel> detailList = new List<EventModel>();
            namesEncountered.Clear();
            _subZone = "Fairview";
            _location = string.Empty;

            for (int i = 0; i < textArray.Length; i++)
            {
                EventModel eventDetails = ParseLine(textArray[i]);
                if (eventDetails.eventType != EventType.Invalid)
                {
                    detailList.Add(eventDetails);
                }
            }

            return detailList;
        }

        public List<EventModel> ParseAdventure()
        {
            while (!WaitForFile())
            {
                Task.Delay(1000);
            }

            string[] textArray = ReadAdventure().Split('\n');
            List<EventModel> detailList = new List<EventModel>();
            namesEncountered.Clear();
            _subZone = "N/A";
            _location = string.Empty;

            for (int i = 1; i < textArray.Length; i++)
            {
                EventModel eventDetails = ParseLine(textArray[i]);
                if (eventDetails.eventType != EventType.Invalid)
                {
                    detailList.Add(eventDetails);
                }
            }

            return detailList;
        }

        private string ReadCampaign()
        {
            string fileText = File.ReadAllText(_filePath);
            fileText = fileText
                .Split(new string[] { "/Game/Campaign_Main/Quest_Campaign_Ward13.Quest_Campaign_Ward13" }, StringSplitOptions.None)[0];
            fileText = fileText
                .Split(new string[] { "/Game/Campaign_Main/Quest_Campaign_City.Quest_Campaign_City" }, StringSplitOptions.None)[1];
            fileText = fileText.Trim().Replace("/Game/", "\n");
            return fileText;
        }

        private string ReadAdventure()
        {
            string fileText = File.ReadAllText(_filePath);

            if(!fileText.Contains("Quests/Quest_AdventureMode"))
            {
                return string.Empty;
            }

            fileText = fileText.Split(new string[] { "Quests/Quest_AdventureMode" }, StringSplitOptions.None).Length > 2 
                ? fileText.Split(new string[] { "Quests/Quest_AdventureMode" }, StringSplitOptions.None)[2]
                : string.Empty;

            fileText = fileText.Trim().Replace("/Game/", "\n");
            return fileText;
        }

        private EventModel ParseLine(string line)
        {
            line = line.Substring(0, line.Length - 9); //trim bogus characters ending each line
            if(line.Equals(string.Empty))
            {
                return new EventModel();
            }

            string[] lineList = line.Split('/');
            if (!EventTranslationModel.IsValidZone(lineList[0]))
            {
                return new EventModel();
            }

            if (lineList[1].CompareTo("Templates") == 0)
            {
                string subZoneText = lineList[2].Split('.')[0];
                _subZone = EventTranslationModel.TranslateEventSubZone(subZoneText.Substring(0, subZoneText.Length - 3));
                return new EventModel();
            }

            return BuildEvent(lineList);
        }

        private EventModel BuildEvent(string[] lineList)
        {
            EventModel @event = new EventModel();
            @event.zone = EventTranslationModel.TranslateEventZone(lineList[0]);
            @event.subZone = _subZone;
            @event.eventType = ReadEventType(lineList[2]);

            @event.location = @event.eventType == EventType.ItemDrop 
                ? _location 
                : @event.eventType == EventType.PointOfInterest
                    ? "Overworld"
                    : ReadEventLocation(lineList[2]);
            @event.name = ReadEventName(lineList[2]);
            if (namesEncountered.Contains(@event.name))
            {
                @event.eventType = EventType.Invalid;
            }
            else
            {
                namesEncountered.Add(@event.name);
            }
            return @event;
        }

        private EventType ReadEventType(string eventString)
        {
            string typeString = eventString.Split('_')[1];
            if(typeString.Equals("UndyingKing") || typeString.Equals("Boss"))
            {
                return EventType.WorldBoss;
            }
            else if(typeString.ToLower().Equals("miniboss"))
            {
                return EventType.MiniBoss;
            }
            else if (typeString.Equals("Event"))
            {
                return EventType.ItemDrop;
            }
            else if (typeString.Equals("SmallD"))
            {
                return EventType.SideDungeon;
            }
            else if (typeString.Equals("Siege"))
            {
                return EventType.Siege;
            }
            else if(typeString.Equals("OverworldPOI"))
            {
                return EventType.PointOfInterest;
            }
            else
            {
                return EventType.Invalid;
            }
        }

        private string ReadEventLocation(string eventString)
        {
            string locationString = eventString.Contains("UndyingKing")
                ? "UndyingKing"
                : eventString.Split('_')[2];

            _location = EventTranslationModel.IsValidLocation(locationString)
                ? EventTranslationModel.TranslateEventLocation(locationString)
                : locationString;

            return _location;
        }

        private string ReadEventName(string eventString)
        {
            string nameString = eventString.Split('_')[2];

            return EventTranslationModel.IsValidName(nameString)
                ? EventTranslationModel.TranslateEventName(nameString)
                : nameString; //eventually translate to readable names
        }

        bool WaitForFile()
        {
            int numTries = 0;
            while (true)
            {
                ++numTries;
                try
                {
                    // Attempt to open the file exclusively.
                    using (FileStream fs = new FileStream(_filePath,
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
}
