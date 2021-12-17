using System.IO;
using System.Text;
using WpfApp11Dotnet4;

namespace WpfApp11Dotnet4
{
    public class Config
    {
        public Person Person { get; set; }
        public string Memo { get; set; }

        public Config()
        {
            this.Person = new Person();
        }

        public static string GetConfigFilePath()
        {
            string appFilePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            return System.Text.RegularExpressions.Regex.Replace(
                appFilePath,
                ".exe",
                ".json",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        public static Config ReadConfig()
        {
            string configFile = GetConfigFilePath();

            if (File.Exists(configFile) == false)
            {
                return null;
            }

            using (var reader = new StreamReader(configFile, Encoding.UTF8))
            {
                string buf = reader.ReadToEnd();
                var js = new System.Web.Script.Serialization.JavaScriptSerializer();
                return js.Deserialize<Config>(buf);
            }
        }

        public static void WriteConfig(Config cfg)
        {
            var js = new System.Web.Script.Serialization.JavaScriptSerializer();
            string buf = js.Serialize(cfg);

            string configFile = GetConfigFilePath();

            using (var writer = new StreamWriter(configFile, false, Encoding.UTF8))
            {
                writer.Write(buf);
            }
        }

    }
}