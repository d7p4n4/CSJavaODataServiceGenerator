using Ac4yClassModule.Class;
using Ac4yClassModule.Service;
using CSClassLibForJavaOData;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CSJavaODataServiceGenerator
{
    class Program
    {

        #region members

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private const string APPSETTINGS_TEMPLATEPATH = "TEMPLATEPATH";

        // JAVA //

        private const string APPSETTINGS_JAVAODATASERVICESUBPATH = "JAVAODATASERVICESUBPATH";
        private const string APPSETTINGS_CLASSNAME = "CLASSNAME";
        private const string APPSETTINGS_PACKAGENAME = "PACKAGENAME";
        private const string APPSETTINGS_JPAPACKAGENAME = "JPAPACKAGENAME";
        private const string APPSETTINGS_PERSISTENCENAME = "PERSISTENCENAME";
        private const string APPSETTINGS_JAVAODATASERVICEOUTPUTPATH = "JAVAODATASERVICEOUTPUTPATH";

        private const string APPSETTINGS_IP = "IP";
        private const string APPSETTINGS_PORT = "PORT";
        private const string APPSETTINGS_USERNAME = "USERNAME";
        private const string APPSETTINGS_PASSWORD = "PASSWORD";
        private const string APPSETTINGS_DATABASENAME = "DATABASENAME";
        private const string APPSETTINGS_JAVAPERSISTENCESUBPATH = "JAVAPERSISTENCESUBPATH";
        private const string APPSETTINGS_JAVAPERSISTENCEOUTPUTPATH = "JAVAPERSISTENCEOUTPUTPATH";

        private const string APPSETTINGS_JAVAJPASUBPATH = "JAVAJPASUBPATH";
        private const string APPSETTINGS_JAVAJPAOUTPUTPATH = "JAVAJPAOUTPUTPATH";


        public IConfiguration Config { get; set; }

        #endregion members

        public Program(IConfiguration config)
        {

            Config = config;

        } // Program

        public void Run()
        {
            List<Type> persistenceLista = new List<Type>();
            persistenceLista.Add(typeof(Cars));
            persistenceLista.Add(typeof(Colors));

            new JavaJPAGenerator()
            {
                TemplatePath = Config[APPSETTINGS_TEMPLATEPATH]
                ,
                TemplateSubPath = Config[APPSETTINGS_JAVAJPASUBPATH]
                ,
                OutputPath = Config[APPSETTINGS_JAVAJPAOUTPUTPATH]
                ,
                PackageName = Config[APPSETTINGS_JPAPACKAGENAME]
            }
                .Generate(new Ac4yClassHandler().GetAc4yClassFromType(typeof(Cars)));


        } // run

        static void Main(string[] args)
        {

            try
            {
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

                IConfiguration config = null;

                config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", true, true)
                            .Build();

                new Program(config).Run();

            }
            catch (Exception exception)
            {

                log.Error(exception.Message);
                log.Error(exception.StackTrace);

                Console.ReadLine();

            }

        } // Main

    } // Program

} // JavaGeneralas