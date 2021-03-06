using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspergillosisEPR.Lib
{
    public class RegularExpressions
    {
        public static string UK_POST_CODE = @"^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9]?[A-Za-z])))) [0-9][A-Za-z]{2})$";

        public static Regex FindWordInList(List<string> array)
        {
            return new Regex(@"\b(" + string.Join("|", array.Select(Regex.Escape).ToArray()) + @"\b)", RegexOptions.IgnoreCase);
        }

        public static Regex ValidYear()
        {
            return new Regex(@"\d{4}");
        }

        public static Regex RM2Number()
        {
            return new Regex(@"RM2\s{0,}-{0,}\s{0,}\d{1,}");
        }

        public static Regex ClinicLetterRM2Number()
        {
            return new Regex(@"RM2\d{1,}|RM2-\d{1,}");
        }

        public static Regex JustDigits()
        {
            return new Regex(@"\d{1,}");
        }

        public static Regex TextBetweenParentheses()
        {
            return new Regex(@"\(([^)]*)\)");
        }

        public static Regex ClinicLetterDate()
        {
            return new Regex(@"\bClinic date.*|Date of clinic.*|Date of Dictation.*", RegexOptions.IgnoreCase);
        }

        public static Regex UkPostCode()
        {
            return new Regex(UK_POST_CODE);
        }

       
    }
}