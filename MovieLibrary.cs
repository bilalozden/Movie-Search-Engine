using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Engine
{
    public class MovieLibrary
    {
        public Movie[] movieList { get; set; } 
        public Dictionary<string, int> keywordsAll { get; set; }
        public int totalAmountOfKeywords { get; set; }
        public MovieLibrary(string path)
        {         
            movieList = new Movie[10000];
            
            string[] lines = System.IO.File.ReadAllLines(path);

            for (int i=0; i< movieList.Length; i++)
            {
                ///string imdb_id1, string title1, string year1, string runtime1, string genres1, string score1, string keywords1
                movieList[i] = new Movie(
                    lines[i].Split(";")[1], // string imdb_id1
                    lines[i].Split(";")[3], // string title1
                    lines[i].Split(";")[4], // string year1
                    lines[i].Split(";")[5], // string runtime1
                    lines[i].Split(";")[6], // string genres1
                    lines[i].Split(";")[7], // string score1
                    lines[i].Split(";")[8]); // string keywords1
            }

            Dictionary<string, int> keywordTotal = new Dictionary<string, int>();
            keywordsAll = new Dictionary<string, int>();
            totalAmountOfKeywords = 0;

            foreach (var item in movieList)
            {
                foreach (var keyword in item.keywords)
                {
                    if (!keywordTotal.ContainsKey(keyword))
                    {
                        keywordsAll[keyword] = 1;
                    }
                    else
                        keywordsAll[keyword]++;

                    totalAmountOfKeywords++;
                }
            }
            keywordsAll = keywordsAll.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
