using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TranslatedHanselAndGretel.Extensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return null;
            
            return TranslatedHanselAndGretel.Resources.Resources.ResourceManager.GetString(Text, CultureInfo.CurrentCulture);
        }
    }
}