using System.IO;
using System.Reflection;
using System.Threading.Tasks;
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
        public static DataHelper DataHelper;
        public static DatabaseHelper DatabaseHelper;

        public App()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "AirMonitor.config.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                AirlyService.AirlyConfig = JsonConvert.DeserializeObject<AirlyConfig>(result);
            }

            InitializeComponent();
            Task.Run(async () => 
                { 
                    await InitDatabase();
                    await InitData();

                }).Wait();

            MainPage = new RootTabbedPage();
        }

        protected override void OnStart()
        {
            if(DatabaseHelper == null)
            {
                Task.Run(async () => { await InitDatabase(); }).Wait();
            }

            if (DataHelper == null)
            {
                Task.Run(async () => { await InitData(); }).Wait();
            }
        }

        protected override void OnSleep()
        {
            DatabaseHelper.Dispose();
            DatabaseHelper = null;

            DataHelper.Dispose();
            DataHelper = null;
        }

        protected override void OnResume()
        {
            if (DatabaseHelper == null)
            {
                Task.Run(async () => { await InitDatabase(); }).Wait();
            }

            if (DataHelper == null)
            {
                Task.Run(async () => { await InitData(); }).Wait();
            }
        }

        private async Task InitData()
        {
            DataHelper = new DataHelper();
            await DataHelper.InitData();
        }

        private async Task InitDatabase()
        {
            DatabaseHelper = new DatabaseHelper();
            await DatabaseHelper.InitDatabase();
        }
    }
}
