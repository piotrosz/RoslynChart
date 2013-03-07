using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartScript.Models
{
    [Serializable]
    public class CodeSampleSection1
    {
        public CodeSampleSection1(string name)
        {
            this.Name = name;
            Sections = new List<CodeSampleSection2>();
        }

        public string Name { get; set; }
        public List<CodeSampleSection2> Sections { get; set; }
    }

    [Serializable]
    public class CodeSampleSection2
    {
        public CodeSampleSection2(string name)
        {
            this.Name = name;
            CodeSamples = new List<CodeSample>();
        }

        public string Name { get; set; }
        public List<CodeSample> CodeSamples { get; set; }
    }
}