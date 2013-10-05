using System;
using System.Linq;
using System.Text;

namespace StringBuilderExtension
{
    public static class Extensions
    {
        public static string Substring(this StringBuilder source, int index, int length)
        {
            //poneje stringbuilder nqma po default Substring nishto ne ni prechi da si ulesnim jivota prosto kato go obarnem kam ToString() i polzvame negoiq gotov metod.
            //neshto koeto po nikakav nachin ne e v razrez s uslovieto
            return source.ToString().Substring(index, length);
        }
    }
}
