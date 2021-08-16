using System.IO;
using System.Reflection;

namespace GrossumRedTest
{
    static class Utils
    {
        const string ResourceKey = "GrossumRedTest.Source.";

        public static string LoadText(string name)
        {
            using (Stream stream = Assembly.GetExecutingAssembly()
                   .GetManifestResourceStream(ResourceKey + name))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
