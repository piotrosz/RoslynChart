using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using RoslynChart.Models;

namespace RoslynChart.Core
{
    public class CodeSamplesImporter
    {
        public List<CodeSampleSection> codeSamples;
        private string rootDir;

        public CodeSamplesImporter(string rootDir)
        {
            codeSamples = new List<CodeSampleSection>();
            this.rootDir = rootDir;
        }

        public List<CodeSampleSection> Import()
        {
            foreach(string section1Dir in Directory.GetDirectories(rootDir))
            {
                codeSamples.Add(new CodeSampleSection(Path.GetFileName(section1Dir)));

                foreach(string section2Dir in Directory.GetDirectories(section1Dir))
                {
                    codeSamples.Last().Sections.Add(new CodeSampleSubSection(Path.GetFileName(section2Dir)));

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