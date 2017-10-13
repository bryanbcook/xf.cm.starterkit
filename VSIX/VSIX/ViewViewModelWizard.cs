using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Xamarin.Forms.CaliburnMicro.StarterKit
{
    public class ViewViewModelWizard : IWizard
    {
        public void BeforeOpeningFile(ProjectItem projectItem) { }

        public void ProjectFinishedGenerating(Project project) { }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem) { }

        public void RunFinished()
        {
            // fix: specifying \ViewModels creates a temporary folder at the root of the drive
            var cleanupFolders = new[] { "\\ViewModels", "\\Views" };

            foreach (var folder in cleanupFolders)
            {
                if (Directory.Exists(folder))
                {
                    NukeTempFolder(folder);
                }
            }
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            // This Wizard installs the View and ViewModels in their correct folder.
            // Since we cannot change the path of the item after it's been created, 
            // we calculate the names of these folders and namespaces before the items are added to the project.

            // We can use the $rootnamespace$ variable to determine which folder the view/viewmodel is being inserted into
            // at runtime.

            //if (automationObject is DTE)
            //{
            //    var dte = (DTE)automationObject;
            //    var projects = (Array)dte.ActiveSolutionProjects;
            //    var project = (Project)projects.GetValue(0);

            //    replacementsDictionary.Add("$projectfolder$", System.IO.Path.GetDirectoryName(project.FileName));
            //}

            ApplyCustomParameters(replacementsDictionary);           
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        private void ApplyCustomParameters(Dictionary<string,string> replacementsDictionary)
        {
            string rootNamespace = replacementsDictionary["$rootnamespace$"];

            string[] namespaceParts = rootNamespace.Split('.');
            int index = namespaceParts.Length - 1;

            for(int i = namespaceParts.Length - 1; i > 0; i--)
            {
                string currentPart = namespaceParts[i];

                if (currentPart.Equals("ViewModels", StringComparison.OrdinalIgnoreCase) ||
                    currentPart.Equals("Views", StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    break;
                }
            }
            
            replacementsDictionary.Add("$viewmodelfolder$", ConstructFolderName("ViewModels", namespaceParts, index));
            replacementsDictionary.Add("$viewfolder$", ConstructFolderName("Views", namespaceParts, index));
            replacementsDictionary.Add("$viewmodelnamespace$", ConstructNamespace("ViewModels", namespaceParts, index));
            replacementsDictionary.Add("$viewnamespace$", ConstructNamespace("Views", namespaceParts, index));
        }

        private string ConstructFolderName(string rootFolderName, string[] namespaceParts, int namespaceIndex)
        {
            return ConstructNamespace(rootFolderName, namespaceParts, namespaceIndex, forNamespace:false);
        }

        private string ConstructNamespace(string targetNamespace, string[] namespaceParts, int namespaceIndex, bool forNamespace = true)
        {
            string delimiter = forNamespace ? "." : "\\";

            string subNamespace = String.Empty;
            if (IsItemTemplateInASubFolder(namespaceParts, namespaceIndex))
            {
                subNamespace = delimiter + string.Join(delimiter, namespaceParts.Skip(namespaceIndex + 1));
            }

            if (forNamespace)
            {
                string rootNamspace = namespaceIndex > 0 ? string.Join(delimiter, namespaceParts.Take(namespaceIndex)) : namespaceParts[0];
                targetNamespace = rootNamspace + delimiter + targetNamespace;
            }

            return string.Format("{0}{1}", targetNamespace, subNamespace);
        }

        private bool IsItemTemplateInASubFolder(string[] namespaceParts, int index)
        {
            // it's a subfolder if the index isn't the end
            return ((namespaceParts.Length - 1 != index) && (index + 1 < namespaceParts.Length));
        }

        private void NukeTempFolder(string folder)
        {
            var files = Directory.GetFiles(folder);
            foreach(var file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception)
                {
                }
            }

            // process subfolders recursively
            foreach (var dir in Directory.GetDirectories(folder))
            {
                NukeTempFolder(dir);
            }

            try
            {
                Directory.Delete(folder);
            }
            catch (Exception)
            {
            }
        }
    }
}
