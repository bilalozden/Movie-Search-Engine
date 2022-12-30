using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Engine
{
    public class Movie
    {
        public string imdb_id { get; set; }
        public string title { get; set; }
        public string year { get; set; }
        public string runtime { get; set; }
        public string genres { get; set; }
        public string score { get; set; }
        public string[] keywords { get; set; }
        public Dictionary<string, int> keywordPScore { get; set; }
        public int keywordsLength { get; set; }

    /* movie details
           IMDB number at index: 1
           original title at index: 3
           year at index: 4
           runtime minutes: 5
           genres at index: 6
           IMDB rating at index: 7
           keywords at index: 8
           */
    public Movie(string imdb_id1, string title1, string year1, string runtime1, string genres1, string score1, string keywords1)
    {
            imdb_id = imdb_id1;
            title = title1;
            year = year1;
            runtime = runtime1;
            genres = genres1;
            score = score1;
            keywords = keywordsBuilder(keywords1);
            keywordsLength = keywords.Length;           
        }

    public string[] keywordsBuilder(string keywordsInput)
    {
            string[] tempKeywords = keywordsInput.Replace("-", " ").Split(",").ToArray();  
            return tempKeywords;
    }

     public string ToString()
        {
            string movieWithDetails = title + " (" + year + ") - IMDB Score: " + score;
            return movieWithDetails;
        }
    }
}
