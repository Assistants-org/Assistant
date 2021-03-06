﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rovecode.Assistant.Application.Helpers
{
    public static class KeyHelper
    {
        public static IEnumerable<string> ConvertToKey(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new string[] { };
            }

            // format text and
            text = text.ToLower();

            // remove not valid characters
            text = Regex.Replace(text, @"[^a-zа-я0-9 ]", "", RegexOptions.Multiline);

            // add spaces near numbers
            text = Regex.Replace(text, @"(?<=[a-zа-я])(?=[0-9])|(?<=[0-9])(?=[a-zа-я])", " ");

            // remove multy spaces
            text = Regex.Replace(text, @"\s+", " ", RegexOptions.Multiline);

            // to key (array)
            var key = text.Split(" ");

            // remove blanks from array
            key = key.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            return key;
        }
    }
}
