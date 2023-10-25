using PracticalWork8.ViewModel.Helpers;
using PracticalWork8.Model;
using KoshenskiyJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Windows;

namespace PracticalWork8.ViewModel
{
    internal class MainViewModel : BindingHelper
    {
        readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\pr8json.json";

        #region Properties
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Commands
        public BindableCommand SerializeCommand { get; set; }
        public BindableCommand DeserializeCommand { get; set; }
        public BindableCommand ChangeThemeCommand { get; set; }
        public BindableCommand ChangeLanguageCommand { get; set; }
        #endregion

        public MainViewModel()
        {
            Text = Application.Current.FindResource("String1") as string;
            SerializeCommand = new BindableCommand(_ => Serialize());
            DeserializeCommand = new BindableCommand(_ => Deserialize());
            ChangeThemeCommand = new BindableCommand(ChangeTheme);
            ChangeLanguageCommand = new BindableCommand(ChangeLanguage);
        }

        private void Serialize()
        {
            List<string> list = new List<string>
            {
                Text
            };
            Json.Serialization(list, path);
            Text = Application.Current.FindResource("String2") as string;
        }

        private void Deserialize()
        {
            List<string> list = Json.Deserialization<string>(path);
            Text = list.FirstOrDefault();
        }

        private void ChangeTheme(object parameter)
        {
            AppConfig.SaveTheme(parameter?.ToString());
        }

        private void ChangeLanguage(object parameter)
        {
            AppConfig.SaveLanguage(parameter?.ToString());
            Text = Application.Current.FindResource("String1") as string;
        }
    }
}
