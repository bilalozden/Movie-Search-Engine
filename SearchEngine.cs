using opennlp.tools.parser;
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
        public string[] searchKeywords { get; set; }
        public int inputKeywordsAmount { get; set; }

        //Constructor
        public SearchEngine(MovieLibrary movieLibrary, Input inputClass)
        {        
            this.movieLibrary = movieLibrary;
            this.searchKeywords = inputClass.inputKeywords;
            inputKeywordsAmount = searchKeywords.Length;
        }
        //Constructor


        //Get a match score from the AI Model for each movie -> save them into a dictionary -> Print the best match (default) 
        public void Find() 
        {
            recommendationList = new Dictionary<Movie, double>();

            foreach (var item in movieLibrary.movieList)
            {
                recommendationList.Add(item,Score(item));
            }

            recommendationList = recommendationList.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine("\nRecommended movie: \n____________________\n" 
                + recommendationList.ElementAt(0).Key.ToString() +
                "\n\t\tWatch it on: " + recommendationList.ElementAt(0).Key.platforms);  

            if (recommendationList.ElementAt(0).Key.platforms == "nowhere available") //if the recommended movie not on any platform -> ask for offering the next available movie
            {
                Console.WriteLine("\nWould you like to see the next recommanded movie which you can watch one of the platforms on? " +
                    "\n\tenter: Y/N");

                string pref = Console.ReadLine().ToLower();

                switch (pref)
                {
                    case "y":
                    {
                        bool flag = true;
                        while (flag)
                        {
                            for (int i = 0; i < recommendationList.Count; i++)
                                if (recommendationList.ElementAt(i).Key.platforms != "nowhere available")
                                {
                                        flag = false;
                                    Console.WriteLine("\nRecommended movie: \n____________________\n"
                                                        + recommendationList.ElementAt(i).Key.ToString() +
                                                        "\n\t\tWatch it on: " + recommendationList.ElementAt(i).Key.platforms);
                                    break;
                                }
                    }

                    break;
                    }
                    case "n":
                    {
                        Console.WriteLine("Exiting the program...");

                        break;
                    }
                    default:
                        Console.WriteLine("Wrong input... Exiting the program...");
                        break;  
                }
            }
        }
        //Get a match score from the AI model for each movie, save them into a dictionary -> print the best match (default)


        //Get a match score from the AI Model for each movie -> save them into a dictionary -> Prints the best n matches 
        public void Find(int recAmount) //Prints the best n matches
        {
            recommendationList = new Dictionary<Movie, double>();

            foreach (var item in movieLibrary.movieList)
            {
                recommendationList.Add(item, Score(item));
            }

            recommendationList = recommendationList.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine("Top " + recAmount +  " matches: ");
            for (int i = 0; i < recAmount; i++)
                Console.WriteLine("\t" + (i + 1) + ". " + recommendationList.ElementAt(i).Key.ToString() + "\n\t\tPlatform(s): " + recommendationList.ElementAt(i).Key.platforms +
                            "\n\t\tsearch score: " + recommendationList.ElementAt(i).Value);
        }
        //Get a match score from the AI Model for each movie -> save them into a dictionary -> Prints the best n matches


        //AI MODEL
        public double Score(Movie movie) //return a score for the movie
        {
            double[] pScoreKeywords = new double[inputKeywordsAmount];
            double pScoreKeywordsMultiply = 1;
            for (int i = 0; i < inputKeywordsAmount; i++)
            {
                pScoreKeywords[i] = pScoreKeyword(searchKeywords[i]);
                pScoreKeywordsMultiply *= pScoreKeywords[i];
                //Console.WriteLine(pScoreKeywordsMultiply);
            }


            double[] pScoreKeywordsMovie = new double[inputKeywordsAmount];
            double pScoreKeywordsMovieMultiply = 1;
            for (int i = 0; i < inputKeywordsAmount; i++)
            {
                pScoreKeywordsMovie[i] = pScoreKeywordMovie(movie, searchKeywords[i]);
                pScoreKeywordsMovieMultiply *= pScoreKeywordsMovie[i];
                //Console.WriteLine(pScoreKeywordsMovieMultiply);
            }

            //double pMovie = Math.Log10(1 /movieLibrary.movieList.Length);
            //pMovie = Math.Abs(pMovie);;
            double pMovie = 1;

            //bcs the values are so small, we need Log function
            double score = Math.Log10((pMovie * pScoreKeywordsMultiply)/(pScoreKeywordsMovieMultiply));
            
            score = Math.Abs(score);

            return score;
        }
        //AI MODEL


        //functions needed for AI model 
        public double pScoreKeywordMovie(Movie movie, string keyword)
        {          
            foreach (var item in movie.keywords)
            {
                if (item == keyword)
                {
                    return 2;
                }
            }
            return 1; //as default can't return 0, it would fail the math calculation
        }
        public double pScoreKeyword(string keyword)
        {
            double tempScore=0;

            if (movieLibrary.keywordsAll.ContainsKey(keyword))
            {
                //tempScore += movieLibrary.keywordsAll[keyword] / movieLibrary.totalAmountOfKeywords;

                tempScore +=  1 / (movieLibrary.keywordsAll[keyword] / Math.Log10(movieLibrary.totalAmountOfKeywords));

                //Console.WriteLine(movieLibrary.totalAmountOfKeywords);
                //Console.WriteLine(keyword + ": " + (1+tempScore));

            }
            return 1 + tempScore; //as default can't return 0, it would fail the math calculation
        }
        //functions needed for AI model



        // other functions:
        public void PrintKeywords() //to print the keywords created from the input (just in case)
        {
            foreach (string keyword in searchKeywords)
            {
                Console.WriteLine(keyword);
            }
        }
        // other functions:
    }
}

