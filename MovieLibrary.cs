using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Engine
{
    public class MovieLibrary //keeps all movies
    {
        public Movie[] movieList { get; set; } 
        public Dictionary<string, int> keywordsAll { get; set; } //key: keyword, value:amount of mention in the library
        public int totalAmountOfKeywords { get; set; } //total amount of keywords in the library

        //Constructor -> initialize movies
        public MovieLibrary(string path)
        {         
            string[] lines = System.IO.File.ReadAllLines(path);
            
            movieList = new Movie[lines.Length];

            for (int i=0; i< lines.Length; i++)
            {
                ///string imdb_id1, string title1, string year1, string runtime1, string genres1, string score1, string keywords1, string platform(s)
                
                movieList[i] = new Movie(
                    lines[i].Split(";")[1], // string imdb_id1
                    lines[i].Split(";")[3], // string title1
                    lines[i].Split(";")[4], // string year1
                    lines[i].Split(";")[5], // string runtime1
                    lines[i].Split(";")[6], // string genres1
                    lines[i].Split(";")[7], // string score1
                    lines[i].Split(";")[8]); // string keywords1
                movieList[i].platforms = string.Empty;

               
                if (lines[i].Split(";")[9] == "1")
                    movieList[i].platforms += "Disney-Plus" + " ";
                if (lines[i].Split(";")[10] == "1")
                    movieList[i].platforms += "Amazon-Prime" + " ";
                if (lines[i].Split(";")[11] == "1")
                    movieList[i].platforms += "Netflix" + " ";
                if (lines[i].Split(";")[12] == "1")
                    movieList[i].platforms += "HBO-Max" + " ";
                if (movieList[i].platforms == string.Empty)
                    movieList[i].platforms = "nowhere available";
            }

            Dictionary<string, int> keywordTotal = new Dictionary<string, int>();
            keywordsAll = new Dictionary<string, int>();
            totalAmountOfKeywords = 0;

            foreach (var item in movieList)
            {
                foreach (var keyword in item.keywords)
                {
                    if (!keywordsAll.ContainsKey(keyword))
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
        //Constructor -> initialize movies

        //other functions
        public string findAMovie(string movieName) //search a movie by title (just in case)
        {
            foreach (var movie in movieList)
            {
                if (movie.title.ToLower() == movieName.ToLower())
                { 
                    return movie.ToString(); 
                }
            }
            return "nothing found";
        }
        //other functions
    }
}
