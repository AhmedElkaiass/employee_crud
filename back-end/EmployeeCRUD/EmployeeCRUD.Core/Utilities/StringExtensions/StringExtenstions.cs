﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//\////////////////////////////////////////////////////////////////////////////////////////////////
namespace  EmployeeCRUD.Core.Utilities.StringExtensions
{
    //\////////////////////////////////////////////////////////////////////////////////////////////
    public static class StringExtenstions
    {

        //-----------------------------------------------------------------------------------------
        public static string ToPascalCase(this string original)
        {
            Regex invalidCharsRgx = new Regex("[^_a-zA-Z0-9]");
            Regex whiteSpace = new Regex(@"(?<=\s)");
            Regex startsWithLowerCaseChar = new Regex("^[a-z]");
            Regex firstCharFollowedByUpperCasesOnly = new Regex("(?<=[A-Z])[A-Z0-9]+$");
            Regex lowerCaseNextToNumber = new Regex("(?<=[0-9])[a-z]");
            Regex upperCaseInside = new Regex("(?<=[A-Z])[A-Z]+?((?=[A-Z][a-z])|(?=[0-9]))");

            // replace white spaces with undescore, then replace all invalid chars with empty string
            var pascalCase = invalidCharsRgx.Replace(whiteSpace.Replace(original, "_"), string.Empty)
                // split by underscores
                .Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
                // set first letter to uppercase
                .Select(w => startsWithLowerCaseChar.Replace(w, m => m.Value.ToUpper()))
                // replace second and all following upper case letters to lower if there is no next lower (ABC -> Abc)
                .Select(w => firstCharFollowedByUpperCasesOnly.Replace(w, m => m.Value.ToLower()))
                // set upper case the first lower case following a number (Ab9cd -> Ab9Cd)
                .Select(w => lowerCaseNextToNumber.Replace(w, m => m.Value.ToUpper()))
                // lower second and next upper case letters except the last if it follows by any lower (ABcDEf -> AbcDef)
                .Select(w => upperCaseInside.Replace(w, m => m.Value.ToLower()));

            return string.Concat(pascalCase);
        }
        //-----------------------------------------------------------------------------------------
        /// <summary>
        ///  Max lengigth Must be smaller than 8
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns> 
        public static string GetRondomStringCode(int Length)
        {
            char[] Chars = "qwertyuioplkjhgfdsazxcvbnm1234567890".ToCharArray();
            string Code = "";
            Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                Code += Chars[random.Next(0, Chars.Length)];
            }
            return Code;
        }
        //---------------------------------------------------------------------------------------------
        /// <summary>
        ///  Max lengigth Must be smaller than 8
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string GetRondomStringCodeWith2Char(int Length = 4)
        {
            char[] Chars = "qwertyuioplkjhgfdsazxcvbnm".ToCharArray();
            string Code = "";
            Random random = new Random();
            for (int i = 0; i < 2; i++)
            {
                Code += Chars[random.Next(0, Chars.Length)];
            }
            char[] numbers = "1234567890".ToCharArray();
            for (int i = 0; i < Length; i++)
            {
                Code += numbers[random.Next(0, numbers.Length)];
            }
            return Code;
        }
     
        //-----------------------------------------------------------------------------------------
    }
    //\////////////////////////////////////////////////////////////////////////////////////////////
}
//\////////////////////////////////////////////////////////////////////////////////////////////////