using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChanceryStore
{
    public class WordFunc
    {
        public static string FirstLetterUpperToLower(string word)
       {
            string redoneWord;
            string firstLetterUpper = word.Substring(0, 1);
            string firstLetterLower = word.Substring(0, 1).ToLower();
            redoneWord = word.Replace(firstLetterUpper, firstLetterLower);
            return redoneWord;
        }
    }
}
