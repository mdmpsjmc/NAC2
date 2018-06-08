﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspergillosisEPR.Lib
{
    public class RegularExpressions
    {
        public static Regex FindWordInList(List<string> array)
        {
            return new Regex(@"\b(" + string.Join("|", array.Select(Regex.Escape).ToArray()) + @"\b)", RegexOptions.IgnoreCase);
        }

        public static Regex ValidYear()
        {
            return new Regex(@"\d{4}");
        }
    }
}