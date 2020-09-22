using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSJavaODataServiceGenerator
{
    class JavaWebXMLGenerator
    {


        #region members

        public string TemplatePath { get; set; }
        public string TemplateSubPath { get; set; }
        public string OutputPath { get; set; }
        public string PackageName { get; set; }
        public string ClassName { get; set; }
        public string UrlPattern { get; set; }
        public string DisplayName { get; set; }

        public Type Type { get; set; }

        private const string TemplateExtension = ".javaT";


        private const string ClassNameMask = "#className#";
        private const string PackageNameMask = "#packageName#";
        private const string UrlPatternMask = "#urlPattern#";
        private const string DisplayNameMask = "#displayName#";

        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = TemplatePath + TemplateSubPath + fileName + TemplateExtension;

            return File.ReadAllText(textFile);

        } // ReadIntoString

        public void WriteOut(string text, string fileName, string outputPath)
        {
            File.WriteAllText(outputPath + fileName + ".xml", text);

        }

        public string GetNameWithLowerFirstLetter(String Code)
        {
            return
                char.ToLower(Code[0])
                + Code.Substring(1)
                ;

        } // GetNameWithLowerFirstLetter

        public string GetHead()
        {

            return ReadIntoString("Head")
                        .Replace(DisplayNameMask, DisplayName)
                        ;

        }

        public string GetFoot()
        {
            return
                ReadIntoString("Foot")
                        ;

        }

        public string GetMethods()
        {
            return
                ReadIntoString("Methods")
                        .Replace(UrlPatternMask, UrlPattern)
                        .Replace(ClassNameMask, ClassName)
                        .Replace(PackageNameMask, PackageName);
        }

        public JavaWebXMLGenerator Generate()
        {

            string result = null;

            result += GetHead();

            result += GetMethods();

            result += GetFoot();

            WriteOut(result, "web", OutputPath);

            return this;

        } // Generate

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

    }
}
