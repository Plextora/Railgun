#region

using System.Collections.Generic;
using System.Linq;
using System.Windows;

#endregion

namespace Railgun
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ResourceDictionary = new Dictionary<string, ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Current.Resources.MergedDictionaries)
            {
                string key = dictionary.Source.OriginalString.Split(';').First().Split('/').Last().Split('.').First();
                ResourceDictionary.Add(key, dictionary);
            }
        }

        public static Dictionary<string, ResourceDictionary> ResourceDictionary;
    }
}
