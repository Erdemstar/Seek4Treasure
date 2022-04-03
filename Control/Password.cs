using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Seek4Treasure.Control
{
    class Password
    {
        // Regex list
        public List<string> regexPattern = new List<string>()
        {
            "(pword|pwd|pass|password)+\\s*[=|:]\\s*[\"|'|\\s]?[a-z0-9]+[\"|'|\\s]?"
        };

        public List<Tuple<string,string>> Control(string codeline)
        {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();

            //regex loop
            foreach (var regex in regexPattern)
            {
                //find regex in file loop
                foreach (Match match in Regex.Matches(codeline, regex, RegexOptions.IgnoreCase))
                {
                    if (match.Success && match.Groups.Count > 0)
                    {
                        result.Add(Tuple.Create(codeline, regex));
                    }
                }

            }

            return result;
        }
    }
}
