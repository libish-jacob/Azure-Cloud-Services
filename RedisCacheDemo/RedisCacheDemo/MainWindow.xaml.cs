using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RedisCacheDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // cache key is the primary access key
        static string cacheKey = "eoc0Jb9txoM+E0KCwMOYAcxNEvMt0ZKEsDxrr7x2bVI=";
        static string hostName = "libish-cache.redis.cache.windows.net";

        public class Test
        {
            public Test(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public int Id { get; set; }

            public string Name { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect($"{hostName},abortConnect=false,ssl=true,password={cacheKey}");
        });

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // here we are using an ssl connection. make sure the ssl port used by cache 6380 is open or not.

            ConnectionMultiplexer connection = lazyConnection.Value;
            var cache = connection.GetDatabase();

            var isConnected = cache.IsConnected(cacheKey);
            if (isConnected)
            {
                var ser = JsonConvert.SerializeObject(new Test(25, "test"));

                // Store to cache
                if (!cache.KeyExists("e25"))
                {
                    var r = cache.StringSet("e25", ser);
                }

                // Retrieve from cache
                var retr = cache.StringGet("e25");
                Test e25 = JsonConvert.DeserializeObject<Test>(retr);
            }
        }
    }
}
