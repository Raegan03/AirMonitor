using System.IO;
using System.Reflection;
using AirMonitor.Data;
using AirMonitor.Helpers;
using AirMonitor.Services;
using AirMonitor.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace AirMonitor
{
    public partial class App : Application
    {
        public static DatabaseHelper Database;

        public App()
        {
            Database = new DatabaseHelper();
            Database.InitDatabase();

            InitializeComponent();
            MainPage = new RootTabbedPage();

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "AirMonitor.config.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                AirlyService.AirlyConfig = JsonConvert.DeserializeObject<AirlyConfig>(result);
            }
        }

        protected override void OnStart()
        {
            if(Database == null)
            {
                Database = new DatabaseHelper();
                Database.InitDatabase();
            }
        }

        protected override void OnSleep()
        {
            Database.Dispose();
            Database = null;
        }

        protected override void OnResume()
        {
            if (Database == null)
            {
                Database = new DatabaseHelper();
                Database.InitDatabase();
            }
        }
    }
}
