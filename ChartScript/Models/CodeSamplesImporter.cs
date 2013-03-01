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
        public List<CodeSampleSection1> codeSamples;

        public CodeSamplesImporter()
        {
            codeSamples = new List<CodeSampleSection1>();
        }

        public List<CodeSampleSection1> Import()
        {
            string rootDir = HttpContext.Current.Request.MapPath("~/SampleCode");

            foreach(string section1Dir in Directory.GetDirectories(rootDir))
            {
                codeSamples.Add(new CodeSampleSection1(Path.GetFileName(section1Dir)));

                foreach(string section2Dir in Directory.GetDirectories(section1Dir))
                {
                    codeSamples.Last().Sections.Add(new CodeSampleSection2(Path.GetFileName(section2Dir)));

                    foreach (string file in Directory.GetFiles(section2Dir, "*.cs", SearchOption.AllDirectories))
                    {
                        string fileContent = File.ReadAllText(file);
                        Match match = Regex.Match(fileContent, @"// \[([\w\s]{1,})\]([^**]{0,})// End");

                        if (match.Success)
                        {
                            codeSamples.Last().Sections.Last().CodeSamples.Add(new CodeSample
                            {
                                Name = match.Groups[1].ToString(),
                                Code = match.Groups[2].ToString()
                            });
                        }
                    }
                }
            }

            return codeSamples;
        }
    }
}