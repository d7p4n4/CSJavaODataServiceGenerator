using Ac4yClassModule.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSJavaODataServiceGenerator
{
    class JavaJPAGenerator
    {


        #region members

        public string TemplatePath { get; set; }
        public string TemplateSubPath { get; set; }
        public string OutputPath { get; set; }
        public string PackageName { get; set; }

        public Ac4yClass Type { get; set; }

        private const string TemplateExtension = ".javaT";
    
        private const string PackageNameMask = "#packageName#";
        private const string TableNameMask = "#tableName#";
        private const string TypeMask = "#type#";
        private const string PropertyNameMask = "#propertyName#";

        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = TemplatePath + TemplateSubPath + fileName + TemplateExtension;

            return File.ReadAllText(textFile);

        } // ReadIntoString

        public void WriteOut(string text, string fileName, string outputPath)
        {
            File.WriteAllText(outputPath + fileName + ".java", text);

        }

        public string GetNameWithLowerFirstLetter(String Code)
        {
            return
                char.ToLower(Code[0])
                + Code.Substring(1)
                ;

        } // GetNameWithLowerFirstLetter
        public string GetNameWithUpperFirstLetter(String Code)
        {
            return
                char.ToUpper(Code[0])
                + Code.Substring(1)
                ;

        } // GetNameWithUpperFirstLetter

        public string GetHead()
        {

            return ReadIntoString("Head")
                        .Replace(PackageNameMask, PackageName)
                        .Replace(TableNameMask, Type.Name)
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
                ;
        }

        private string GetGetSet()
        {
            string text = ReadIntoString("getSet");
            string returnText = "";

            for (int i = 0; i <= Type.PropertyList.Count - 11; i++)
            {
                returnText += text.Replace(TypeMask, Type.PropertyList[i].TypeName)
                                  .Replace(PropertyNameMask, Type.PropertyList[i].Name);
            }

            return returnText;
        }

        public string GetProperty()
        {
            string text = ReadIntoString("property");
            string returnText = "";

            for (int i = 0; i <= Type.PropertyList.Count - 11; i++)
            {
                returnText += text.Replace(TypeMask, Type.PropertyList[i].TypeName)
                                  .Replace(PropertyNameMask, Type.PropertyList[i].Name);
            }

            return returnText;

        }

        public JavaJPAGenerator Generate()
        {

            string result = null;

            result += GetHead();

            result += GetMethods();

            result += GetProperty();

            result += GetGetSet();

            result += GetFoot();

            WriteOut(result, Type.Name, OutputPath);

            return this;

        } // Generate


        public JavaJPAGenerator Generate(Ac4yClass type)
        {

            Type = type;

            return Generate();

        } // Generate

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

    }
}
