using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

namespace ChartScript.Models
{
    public class CodeSamplesImporter
    {
        private CodeSampleList samples;

        public CodeSamplesImporter()
        {
            samples = new CodeSampleList();
        }

        public CodeSampleList Import()
        {
            foreach (string file in Directory.GetFiles(HttpContext.Current.Request.MapPath("~/SampleCode"), "*.cs"))
            {
                string fileContent = File.ReadAllText(file);

                Match match = Regex.Match(fileContent, @"// Sample begin \[([\w\s]{1,})\]([^**]{0,})// Sample end");

                if (match.Success)
                {
                    samples.Add(new CodeSample
                    {
                        Name = match.Groups[1].ToString(),
                        Code = match.Groups[2].ToString()
                    });
                }
            }

            return samples;
        }
    }
}