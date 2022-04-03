using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Seek4Treasure.Control
{
    class Token
    {
        // Regex list
        public List<string> regexPattern = new List<string>()
        {
            //Key,
            "(key)\\s*(=|:)\\s*(\"|')?\\s*\\w*\\d*(\"|')?",
            //JWT Token
            "(^eyJ[\\w-]*\\.[\\w-]*\\.[\\w-]*$)",
        };

        public List<Tuple<string, string>> Control(string codeline)
        {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();

            foreach (var regex in regexPattern)
            {
                //find regex in file loop
                foreach (Match match in Regex.Matches(codeline, regex,RegexOptions.IgnoreCase))
                {
                    if (match.Success && match.Groups.Count > 0)
                    {
                        // in js file key value may be more than 100 and it false positive. So i write a little controll
                        if (codeline.Length < 300) { result.Add(Tuple.Create(codeline, regex)); }
                        
                    }
                }
            }

            return result;
        }
    }
}
