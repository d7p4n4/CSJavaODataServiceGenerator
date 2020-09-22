using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSJavaODataServiceGenerator
{
    class JavaPersistenceXMLGenerator
    {


        #region members

        public string TemplatePath { get; set; }
        public string TemplateSubPath { get; set; }
        public string OutputPath { get; set; }
        public string PersistenceName { get; set; }
        public string IP { get; set; }
        public string Port { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public List<Type> Types { get; set; }

        private const string TemplateExtension = ".javaT";

        private const string Suffix = "EFCap";

        private const string PersistenceNameMask = "#persistenceName#";
        private const string ClassNameMask = "#className#";
        private const string IPMask = "#ip#";
        private const string PortMask = "#port#";
        private const string DatabaseNameMask = "#databaseName#";
        private const string UserNameMask = "#userName#";
        private const string PasswordMask = "#password#";

        private const string ClassCodeAsVariableMask = "#classCodeAsVariable#";

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
                        .Replace(PersistenceNameMask, PersistenceName)
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
                        .Replace(IPMask, IP)
                        .Replace(PortMask, Port)
                        .Replace(DatabaseNameMask, DatabaseName)
                        .Replace(UserNameMask, UserName)
                        .Replace(PasswordMask, Password)
                ;
        }

        public string GetClasses()
        {
            string text = ReadIntoString("class");
            string returnText = "";

            foreach (Type type in Types)
            {
                returnText += text.Replace(ClassNameMask, type.Name);
            }

            return returnText;

        }

        public JavaPersistenceXMLGenerator Generate()
        {

            string result = null;

            result += GetHead();

            result += GetClasses();

            result += GetMethods();

            result += GetFoot();

            WriteOut(result, "persistence", OutputPath);

            return this;

        } // Generate

        public JavaPersistenceXMLGenerator Generate(List<Type> types)
        {

            Types = types;

            return Generate();

        } // Generate

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

    }
}
