using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RuToEnToRu
{
    internal static class Translator
    {
        private static string EN = "QWERTYUIOP{}ASDFGHJKL:\"ZXCVBNM<>qwertyuiop[]asdfghjkl;'zxcvbnm,.";
        private static string RU = "ЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮйцукенгшщзхъфывапролджэячсмитьбю";
        

        public static string TranslateText(string text)
        {
            string translatetext = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (EN.Contains(text[0]))
                {
                    if (EN.Contains(text[i]))
                        translatetext += RU[EN.IndexOf(text[i])];
                    InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("ru-RU"));
                }
                else
                {
                    if (RU.Contains(text[i]))
                        translatetext += EN[RU.IndexOf(text[i])];
                    InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("en-US"));
                }
                if (!RU.Contains(text[i]) && !EN.Contains(text[i]))
                {
                    translatetext += text[i];
                }
            }
            return translatetext;
        }
    }
}
