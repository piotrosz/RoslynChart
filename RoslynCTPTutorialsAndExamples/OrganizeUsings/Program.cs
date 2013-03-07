using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.CSharp;

namespace OrganizeUsings
{
    class Program
    {
        static void Main(string[] args)
        {
            var workspace = Workspace.LoadSolution(@"..\..\..\code\SampleCode1\SampleCode1.sln");

            // Take a snapshot of the original solution.
            var originalSolution = workspace.CurrentSolution;

            // Declare a variable to store the intermediate solution snapshot at each step.
            ISolution newSolution = originalSolution;

            foreach (var project in originalSolution.Projects)
            {
                // Note how we can't simply iterate over project.Documents because it will return
                // IDocument objects from the originalSolution, not from the newSolution. We need to
                // use the DocumentId (that doesn't change) to look up the corresponding snapshot of
                // the document in the newSolution.
                foreach (var documentId in project.DocumentIds)
                {
                    // Look up the snapshot for the original document in the latest forked solution.
                    var document = newSolution.GetDocument(documentId);
                    // Get a transformed version of the document (a new solution snapshot is created
                    // under the covers to contain it - none of the existing objects are modified).
                    var newDocument = document.OrganizeImports();

                    // Store the solution implicitly constructed in the previous step as the latest
                    // one so we can continue building it up in the next iteration.
                    newSolution = newDocument.Project.Solution;
                }
            }

            // Actually apply the accumulated changes and save them to disk. At this point
            // workspace.CurrentSolution is updated to point to the new solution.
            workspace.ApplyChanges(originalSolution, newSolution);

        }
    }
}
