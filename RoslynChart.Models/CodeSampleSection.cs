using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoslynChart.Models
{
    [Serializable]
    public class CodeSampleSection
    {
        public CodeSampleSection(string name)
        {
            this.Name = name;
            Sections = new List<CodeSampleSubSection>();
        }

        public string Name { get; set; }
        public List<CodeSampleSubSection> Sections { get; set; }
    }
}