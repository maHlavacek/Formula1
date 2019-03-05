using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formula1.Core.Entities;

namespace Formula1.Core
{
    public class ResultCalculator
    {
        /// <summary>
        /// Berechnet aus den Ergebnissen den aktuellen WM-Stand für die Fahrer
        /// </summary>
        /// <returns>DTO für die Fahrerergebnisse</returns>
        public static IEnumerable<TotalResultDto<Driver>> GetDriverWmTable()
        {
            List<Result> results = ImportController.LoadResultsFromXmlIntoCollections().ToList();
            var result = results
                .GroupBy(gb => gb.Driver)
                .Select(driver => new TotalResultDto<Driver>
                {
                    Competitor = driver.First().Driver,
                    Points = driver.Sum(s => s.Points)
                }
                ).ToList();
            result.Sort();
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Position = (i + 1);
            }
            return result;
        }

        /// <summary>
        /// Berechnet aus den Ergebnissen den aktuellen WM-Stand für die Teams
        /// </summary>
        /// <returns>DTO für die Teamergebnisse</returns>
        public static IEnumerable<TotalResultDto<Team>> GetTeamWmTable()
        {
            List<Result> results = ImportController.LoadResultsFromXmlIntoCollections().ToList();
            var result = results
                .GroupBy(gb => gb.Team)
                .Select(team => new TotalResultDto<Team>
                {
                    Competitor = team.First().Team,
                    Points = team.Sum(s => s.Points)
                }
                ).ToList();
            result.Sort();
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Position = (i + 1);
            }
            return result;
        }
    }
}



