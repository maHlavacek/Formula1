using Formula1.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Utils;

namespace Formula1.Core
{
    /// <summary>
    /// Daten sind in XML-Dateien gespeichert und werden per Linq2XML
    /// in die Collections geladen.
    /// </summary>
    public static class ImportController
    {
        public static List<Race> Races { get; set; }
        public static List<Result> Results { get; set; }
        /// <summary>
        /// Daten der Rennen werden per Linq2XML aus der
        /// XML-Datei ausgelesen und in die Races-Collection gespeichert.
        /// Races werden nicht aus den Results geladen, weil sonst die
        /// Rennen in der Zukunft fehlen
        /// </summary>
        public static IEnumerable<Race> LoadRacesFromRacesXml()
        {
            Races = new List<Race>();
            string racesPath = MyFile.GetFullNameInApplicationTree("Races.xml");
            var xElement = XDocument.Load(racesPath).Root;
            if (xElement != null)
            {
                Races = (from race in xElement.Elements("Race")
                         select new Race
                         {
                             Number = (int)race.Attribute("round"),
                             Date = (DateTime)race.Element("Date"),
                             Country = race.Element("Circuit")?.Element("Location")?.Element("Country")?.Value,
                             City = race.Element("Circuit")?.Element("Location")?.Element("Locality")?.Value
                         }).ToList();
            }
            return Races;
        }

        /// <summary>
        /// Aus den Results werden alle Collections, außer Races gefüllt.
        /// Races wird extra behandelt, um auch Rennen ohne Results zu verwalten
        /// </summary>
        public static IEnumerable<Result> LoadResultsFromXmlIntoCollections()
        {
            LoadRacesFromRacesXml();
            Results = new List<Result>();
            string resultPath = MyFile.GetFullNameInApplicationTree("Results.xml");
            var xElement = XDocument.Load(resultPath).Root;
            if (xElement != null)
            {
                Results = xElement.Elements("Race")?.Elements("ResultsList")?.Elements("Result")
                    .Select(result => new Result
                    {
                        Race = GetRace(result),
                        Driver = GetDriver(result),
                        Team = GetTeam(result),
                        Position = (int)result.Attribute("position"),
                        Points = (int)result.Attribute("points")
                    }).ToList();
            }
            return Results;
        }

        public static Race GetRace(XElement xElement)
        {
            int raceNumber = (int)xElement.Parent?.Parent?.Attribute("round");
            return Races.Single(race => race.Number == raceNumber);
        }

        public static Driver GetDriver(XElement xElement)
        {
            var result = xElement.Elements("Driver")
                .Select(driver => new Driver
                {
                    FirstName = driver.Element("GivenName").Value,
                    LastName = driver.Element("FamilyName").Value,
                    Nationality = driver.Element("Nationality").Value
                });
            return result.Single();
        }

        public static Team GetTeam(XElement xElement)
        {
            var result = xElement.Elements("Constructor")
                .Select(team => new Team
                {
                    Nationality = (string)team.Element("Nationality"),
                    Name = (string)team.Element("Name")
                });
            return result.Single();
        }
    }
}