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
        private const string APPSETTINGS_PERSISTENCENAME = "PERSISTENCENAME";
        private const string APPSETTINGS_JAVAODATASERVICEOUTPUTPATH = "JAVAODATASERVICEOUTPUTPATH";

        private const string APPSETTINGS_IP = "IP";
        private const string APPSETTINGS_PORT = "PORT";
        private const string APPSETTINGS_USERNAME = "USERNAME";
        private const string APPSETTINGS_PASSWORD = "PASSWORD";
        private const string APPSETTINGS_DATABASENAME = "DATABASENAME";
        private const string APPSETTINGS_JAVAPERSISTENCESUBPATH = "JAVAPERSISTENCESUBPATH";
        private const string APPSETTINGS_JAVAPERSISTENCEOUTPUTPATH = "JAVAPERSISTENCEOUTPUTPATH";


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

            new JavaPersistenceXMLGenerator()
            {
                TemplatePath = Config[APPSETTINGS_TEMPLATEPATH]
                ,
                TemplateSubPath = Config[APPSETTINGS_JAVAPERSISTENCESUBPATH]
                ,
                OutputPath = Config[APPSETTINGS_JAVAPERSISTENCEOUTPUTPATH]
                ,
                IP = Config[APPSETTINGS_IP]
                ,
                Port = Config[APPSETTINGS_PORT]
                ,
                PersistenceName = Config[APPSETTINGS_PERSISTENCENAME]
                ,
                Password = Config[APPSETTINGS_PASSWORD]
                ,
                UserName = Config[APPSETTINGS_USERNAME]
                ,
                DatabaseName = Config[APPSETTINGS_DATABASENAME]
            }
                .Generate(persistenceLista);


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