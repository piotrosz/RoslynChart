using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynChart.Models
{
    [Serializable]
    public class CodeSampleSubSection
    {
        public CodeSampleSubSection(string name)
        {
            this.Name = name;
            CodeSamples = new List<CodeSample>();
        }

        public string Name { get; set; }
        public List<CodeSample> CodeSamples { get; set; }
    }
}
