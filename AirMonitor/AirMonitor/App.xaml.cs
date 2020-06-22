using System.IO;
using System.Reflection;
using AirMonitor.Data;
using AirMonitor.Services;
using AirMonitor.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace AirMonitor
{
    public partial class App : Application
    {
        public App()
        {
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
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
