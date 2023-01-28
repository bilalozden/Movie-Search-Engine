using com.sun.org.apache.xalan.@internal.xsltc.cmdline;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Engine
{
    public class Input
    {
        public string input { get; set; }
        public string[] inputKeywords { get; set; } 

        //constructor -> check crazy input> transform the input to keywords
        public Input(string x)
        {
            List<string> keywords = new List<string>();

            input = x;            

            if (!CheckCrazyInput())
            {
            input = TransformInput();

                if (x.Contains("/"))
                {
                    
                    keywords = input.Split("/").ToList();
                }
                else
                {
                keywords = input.Split(" ").ToList();

                    if (keywords.Count > 3)
                    {
                        keywords.AddRange(AddWordCouples(keywords));

                    }
                    else Console.WriteLine("Few words for searching... Recommandation quality will be lower...\n");
                }
            }
            inputKeywords = keywords.ToArray();
        }
        //constructor -> check crazy input> transform the input to keywords


        //functions needed:
        public string TransformInput() //remove speaial characters
        {
            string newInput = input.ToLower();

            Regex pattern = new Regex("[()|^><`~:'@#$&*%_+=;,\t\r!.?-]|");
            newInput = pattern.Replace(newInput, "");
            newInput = newInput.Replace("\\","");

            return newInput;
        }

        public bool CheckCrazyInput() //If input is null or consists of only digits
        {
            if (string.IsNullOrEmpty(input)) return true;
            if (input.Length< 3)
            {
                return true;
            }
            if (input.All(c => char.IsDigit(c)))
            {
                return true;
            }

            string newInput = TransformInput().Replace(" ", "");

            if (newInput.Length<2)
            {
                return true;
            }

            if (input.Contains("/") && input.Replace("/","").Replace(" ","").Length < 2)
            {
                return true;
            }

            return false;
        }
        public List<string> AddWordCouples(List<string> x) //Create word couples for enhancing keywords: prison, cell -> prison cell // creating singular keywords of men-women

        {
            List<string> words = new List<string>();

            words.Add(x.ElementAt(1).Remove((x.ElementAt(1).Length-1),1));

            foreach (var word in x)
            {
                if (word == "men") words.Add("man");
                if (word == "women") words.Add("woman");
            }

            if (x.Count > 3)
            {
                words.Add(x.ElementAt(2) + " " + x.ElementAt(3));
            }
            if (x.Count > 4)
            {
                words.Add(x.ElementAt(2) + " " + x.ElementAt(4));
            }
            return words;
        }
        //functions needed
    }
}
