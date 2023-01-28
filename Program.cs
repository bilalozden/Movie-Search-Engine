using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Formats.Asn1;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;


namespace Movie_Engine
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine("Welcome to movie recommendation engine." +                
                "\nAvailable platforms: Netflix, Amazon Prime, Disney Plus, HBO Max" + "\n\tNote: some of the movies are not available on any platform..." +
                "\n\nYou can start the engine by writing a sentence to describe the movie." +
                "\n\nTip: to get the best recommendation, enter the keywords of a movie and split them by '/' " +
                "\n\tmore keywords -> better recommendation..." +
                "\n\tExample: 'plot twist/escape from prison/prison guard/violence'");

            Console.Write("\nEnter keywords or a sentence: ");

            string input = Console.ReadLine();

            Console.WriteLine("______________________\n");

            Input inputClass = new Input(input);

            //Check for crazy input, give a second chance, if again crazy input -> exit
            if (inputClass.CheckCrazyInput())
            {
                Console.WriteLine("Please enter input in the correct format:");
                input = Console.ReadLine();
                inputClass = new Input(input); 
                
                    if (inputClass.CheckCrazyInput())
                    {
                        Console.WriteLine("Exiting the program...");
                    }
                    else
                    {
                        //initialize the engine
                        string path = "movieDetails.csv";
                        MovieLibrary movieLibrary = new MovieLibrary(path);
                        SearchEngine searchEngine = new SearchEngine(movieLibrary, inputClass);                       
                        searchEngine.Find(); //enter a number into function if you see more recommandation-> searchEngine.Find(int n) -> n recommandation, default 1                        
                }
            }
            else
            {
                //initialize the engine
                string path = "movieDetails.csv";
                MovieLibrary movieLibrary = new MovieLibrary(path);
                SearchEngine searchEngine = new SearchEngine(movieLibrary, inputClass);
                searchEngine.Find(); //enter a number into function if you see more recommandation-> searchEngine.Find(int n) -> n recommandation, default 1: " + movieLibrary.keywordsAll["home"]);
            }

        }
    }
}