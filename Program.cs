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
            string input = "stars/war/jedi/dark side/force/emperor";

            Console.WriteLine("keywords: " + input + "\n");

            string path = "movies_keywords.csv";
            MovieLibrary movieLibrary = new MovieLibrary(path);
            SearchEngine searchEngine = new SearchEngine(movieLibrary, input);

            searchEngine.Find();

            Console.WriteLine("\nTotal amount of movies: " + movieLibrary.movieList.Length);
            Console.WriteLine("Total amount of keywords: " + movieLibrary.totalAmountOfKeywords);
        }
    }
}