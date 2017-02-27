﻿// © Xavalon. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xavalon.XamlStyler.Core.DocumentManipulation;

namespace Xavalon.XamlStyler.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToXmlEncodedString(this string input, bool ignoreCarrier = false)
        {
            var buffer = new StringBuilder(input);

            buffer.Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;");

            if (!ignoreCarrier)
            {
                buffer.Replace("\n", "&#10;");
            }

            return buffer.ToString();
        }

        public static IEnumerable<string> GetLines(this string source)
        {
            using (var reader = new StringReader(source))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        public static IList<string> ToList(this string source)
        {
            return !string.IsNullOrEmpty(source)
                ? source.Split(',')
                    .Where(_ => !String.IsNullOrWhiteSpace(_))
                    .Select(_ => _.Trim())
                    .ToList()
                : new List<string>();
        }

        public static IList<NameSelector> ToNameSelectorList(this string source)
        {
            return !string.IsNullOrEmpty(source)
                ? source.Split(',')
                    .Where(_ => !string.IsNullOrWhiteSpace(_))
                    .Select(_ => new NameSelector(_.Trim()))
                    .ToList()
                : new List<NameSelector>();
        }
    }
}