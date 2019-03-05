using Formula1.Core.Entities;
using System;
using System.Collections.Generic;
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
        public static List<Race> Races { get;private set; }
        /// <summary>
        /// Daten der Rennen werden per Linq2XML aus der
        /// XML-Datei ausgelesen und in die Races-Collection gespeichert.
        /// Races werden nicht aus den Results geladen, weil sonst die
        /// Rennen in der Zukunft fehlen
        /// </summary>
        public static IEnumerable<Race> LoadRacesFromRacesXml()
        {
            Races = new List<Race>();
            string path = MyFile.GetFullNameInApplicationTree("Races.xml");
            var xElement = XDocument.Load(path).Root;
            if(xElement != null)
            {
                Races = (from race in xElement.Elements("Race")
                        select new Race
                        {
                            Number = (int)race.Attribute("Round"),
                            Date = (DateTime)race.Element("Date"),
                            Country = race.Element("Circuit")?.Element("Location")?.Element("Country")?.Value,
                            City = race.Element("Circuit")?.Element("Location")?.Element("Locality")?.Value
                        }).ToList();
            }
        }

        /// <summary>
        /// Aus den Results werden alle Collections, außer Races gefüllt.
        /// Races wird extra behandelt, um auch Rennen ohne Results zu verwalten
        /// </summary>
        public static IEnumerable<Result> LoadResultsFromXmlIntoCollections()
        {
            throw new NotImplementedException();
        }


    }
}