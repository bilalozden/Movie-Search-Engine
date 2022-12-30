using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Engine
{
    public class SearchEngine
    {
        public MovieLibrary movieLibrary { get; set; }
        public Dictionary<Movie, double> recommendationList { get; set; }
        public string[] inputKeywords { get; set; }
        public int inputKeywordsAmount { get; set; }
        public SearchEngine(MovieLibrary movieLibrary, string input)
        {        
            this.movieLibrary = movieLibrary;
            inputKeywords = input.Split('/').ToArray();
            inputKeywordsAmount = inputKeywords.Length;
        }

        public void Find()
        {
            recommendationList = new Dictionary<Movie, double>();

            foreach (var item in movieLibrary.movieList)
            {
                recommendationList.Add(item,Score(item));
            }

            recommendationList = recommendationList.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine("Top 3 Matches: ");
            for (int i = 0; i < 3; i++)
                Console.WriteLine("\t" +  (i+1) + ". " +  recommendationList.ElementAt(i).Key.ToString());
                //Console.WriteLine(recommendationList.ElementAt(i).Key.ToString() + " : "
                //    + recommendationList.ElementAt(i).Value);
        }

        public double Score(Movie movie) //return a score for the movie
        {
            double[] pScoreKeywords = new double[inputKeywordsAmount];
            double pScoreKeywordsMultiply = 1;
            for (int i = 0; i < inputKeywordsAmount; i++)
            {
                pScoreKeywords[i] = pScoreKeyword(inputKeywords[i]);
                pScoreKeywordsMultiply *= pScoreKeywords[i];
                //Console.WriteLine(pScoreKeywordsMultiply);
            }


            double[] pScoreKeywordsMovie = new double[inputKeywordsAmount];
            double pScoreKeywordsMovieMultiply = 1;
            for (int i = 0; i < inputKeywordsAmount; i++)
            {
                pScoreKeywordsMovie[i] = pScoreKeywordMovie(movie, inputKeywords[i]);
                pScoreKeywordsMovieMultiply *= pScoreKeywordsMovie[i];
                //Console.WriteLine(pScoreKeywordsMovieMultiply);
            }

            //double pMovie = 1 / movieLibrary.movieList.Length;
            double pMovie = 1;

            //bcs the values are so small, we need Log function
            double score = Math.Log10((pMovie * pScoreKeywordsMultiply)/(pScoreKeywordsMovieMultiply));

            return score;
        }

        public double pScoreKeywordMovie(Movie movie, string keyword)
        {          
            foreach (var item in movie.keywords)
            {
                if (item == keyword)
                {
                    return 2;
                }
            }

            return 1;
        }

        public double pScoreKeyword(string keyword)
        {
            double tempScore=0;

            if (movieLibrary.keywordsAll.ContainsKey(keyword))
                tempScore += movieLibrary.keywordsAll[keyword]/ movieLibrary.totalAmountOfKeywords;

            return 1 + tempScore;
        }
    }
}

