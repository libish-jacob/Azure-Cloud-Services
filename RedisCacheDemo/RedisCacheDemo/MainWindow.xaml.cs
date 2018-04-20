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
        class Test
        {
            public Test(int id, string name)
            {

            }

            public int Id { get; set; }

            public int Name { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect("libish-cache.redis.cache.windows.net,abortConnect=false,ssl=true,password=eoc0Jb9txoM+E0KCwMOYAcxNEvMt0ZKEsDxrr7x2bVI=");
            var cache = connection.GetDatabase();

            var isConnected = cache.IsConnected("eoc0Jb9txoM+E0KCwMOYAcxNEvMt0ZKEsDxrr7x2bVI=");
            // Store to cache
            cache.StringSet("e25", JsonConvert.SerializeObject(new Test(25, "Clayton Gragg")));

            // Retrieve from cache
            Test e25 = JsonConvert.DeserializeObject<Test>(cache.StringGet("e25"));
        }
    }
}
