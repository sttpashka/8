using PracticalWork8.Model;
using System;
using System.IO;
using System.Windows;

namespace PracticalWork8
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private AppConfig AppConfig;
        private string themeFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\pr8theme.txt";
        private string languageFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\pr8language.txt";

        public App()
        {
            InitializeComponent();

            AppConfig = new AppConfig();
            AppConfig.ThemeChanged += AppConfig_ThemeChanged;
            AppConfig.LanguageChanged += AppConfig_LanguageChanged;

            if (File.Exists(themeFile))
            {
                AppConfig.SaveTheme(File.ReadAllText(themeFile));
            }
            if (File.Exists(languageFile))
            {
                AppConfig.SaveLanguage(File.ReadAllText(languageFile));
            }
        }

        private void AppConfig_ThemeChanged(object sender, EventArgs e)
        {
            if (AppConfig.LoadInstance().Theme != null)
            {
                AppConfig = AppConfig.LoadInstance();

                var dictionary = new ResourceDictionary { Source = new Uri($"/KoshenskiyTheme;component/Themes/{AppConfig.Theme}.xaml", UriKind.RelativeOrAbsolute) };

                Current.Resources.MergedDictionaries.RemoveAt(0);
                Current.Resources.MergedDictionaries.Insert(0, dictionary);

                File.WriteAllText(themeFile, AppConfig.Theme);
            }
        }

        private void AppConfig_LanguageChanged(object sender, EventArgs e)
        {
            if (AppConfig.LoadInstance().Language != null)
            {
                AppConfig = AppConfig.LoadInstance();

                var dictionary = new ResourceDictionary { Source = new Uri($"/KoshenskiyLanguage;component/Languages/{AppConfig.Language}.xaml", UriKind.RelativeOrAbsolute) };

                Current.Resources.MergedDictionaries.RemoveAt(1);
                Current.Resources.MergedDictionaries.Insert(1, dictionary);

                File.WriteAllText(languageFile, AppConfig.Language);
            }
        }
    }
}
