﻿using System.Linq;
using Formula1.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Formula1.CoreTest
{
    [TestClass()]
    public class ImportControllerTests
    {
        /// <summary>
        /// Als erste Übung die Rennen aus der XML-Datei parsen
        /// </summary>
        [TestMethod()]
        public void T01_LoadRacesFromRacesXmlTest()
        {
            var races = ImportController.LoadRacesFromRacesXml().ToList();
            Assert.AreEqual(20, races.Count);
            Assert.AreEqual("Melbourne", races.First().City);
            Assert.AreEqual(1, races.First().Number);
            Assert.AreEqual("Abu Dhabi", races.Last().City);
            Assert.AreEqual(20, races.Last().Number);
        }

        /// <summary>
        /// Alle Results in Collections laden.
        /// </summary>
        [TestMethod()]
        public void T02_LoadResultsFromResultsXmlTest()
        {
            var results = ImportController.LoadResultsFromXmlIntoCollections().ToList();
            Assert.AreEqual(10, results.GroupBy(res => res.Team).Count());
            Assert.AreEqual(24, results.GroupBy(res => res.Driver).Count());
            Assert.AreEqual(300, results.Count);
        }

        [TestMethod]
        public void T03_LoadVettelsResults()
        {
            // Lade Vettels Platzierungen in ein anonymes Objekt { City, Position}
            // Sortiert nach der Rennnummer
            // Assert.Fail("Not implemented!");
            var resultsList = ImportController.LoadResultsFromXmlIntoCollections().ToList();
            var results = resultsList.OrderBy(o => o.Race.Number)
                .Where(w => w.Driver.Name == "Vettel Sebastian")
                .Select(sV => new
                {
                    City = sV.Race.City,
                    Position = sV.Position,
                }).ToList();
            Assert.AreEqual(1, results[0].Position);
            Assert.AreEqual(4, results[7].Position);
            Assert.AreEqual(18, results[13].Position);
        }

    }

}