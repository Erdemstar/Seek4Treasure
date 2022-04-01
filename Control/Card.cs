using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Seek4Treasure.Control
{
    class Card
    {
        // Regex list
        public List<string> regexPattern = new List<string>()
        {
            @"\b4[0-9]{12}(?:[0-9]{3})?\b|\b5[1-5][0-9]{14}\b|\b3[47][0-9]{13}\b|\b3(?:0[0-5]|[68][0-9])[0-9]{11}\b|\b6(?:011|5[0-9]{2})[0-9]{12}\b|\b(?:2131|1800|35\d{ 3})\d{11}\b"
        };

        public List<Tuple<string, string>> Control(string codeline)
        {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();

            //regex loop
            foreach (var regex in regexPattern)
            {
                //find regex in file loop
                foreach (Match match in Regex.Matches(codeline, regex))
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
