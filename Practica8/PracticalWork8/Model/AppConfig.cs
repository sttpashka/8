using PracticalWork8.ViewModel.Helpers;
using System;

namespace PracticalWork8.Model
{
    internal class AppConfig : BindingHelper
    {
        private static AppConfig Instance = new AppConfig();

        public static event EventHandler ThemeChanged;
        public static event EventHandler LanguageChanged;

        private string _theme;
        public string Theme
        {
            get { return _theme; }
            set
            {
                _theme = value;
                OnPropertyChanged();
                ThemeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private string _language;
        public string Language
        {
            get { return _language; }
            set
            {
                _language = value;
                OnPropertyChanged();
                LanguageChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public static void SaveTheme(string theme)
        {
            Instance.Theme = theme;
        }

        public static void SaveLanguage(string language)
        {
            Instance.Language = language;
        }

        public static AppConfig LoadInstance()
        {
            return Instance;
        }
    }
}
