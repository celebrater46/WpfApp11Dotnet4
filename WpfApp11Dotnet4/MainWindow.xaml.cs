using System;
using System.Windows;

namespace WpfApp11Dotnet4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadFromConfig()
        {
            var cfg = Config.ReadConfig();
            if (cfg == null)
            {
                return; 
            }

            nameText.Text = cfg.Person.Name;
            ageText.Text = cfg.Person.Age.ToString();
            cal.SelectedDate = cfg.Person.Birthday.ToLocalTime();
            memoText.Text = cfg.Memo;
        }

        private void SaveToConfig()
        {
            var cfg = new Config();

            cfg.Person.Name = nameText.Text;
            int age;
            if (int.TryParse(ageText.Text, out age) == false)
            {
                age = 0;
            }
            cfg.Person.Age = age;
            cfg.Person.Birthday = (DateTime)cal.SelectedDate;
            cfg.Memo = memoText.Text;

            Config.WriteConfig(cfg);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            LoadFromConfig();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveToConfig();
        }
    }
}
