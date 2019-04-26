using System.Globalization;
using System.Resources;
using MineCore.Utils;

namespace MineCore.Languages
{
    public static class StringManager
    {
        public static CultureInfo SelectedCulture { get; private set; }
        public static ResourceManager ResourceManager;

        public static void Init(CultureInfo cultureInfo = null)
        {
            if (cultureInfo.IsNull())
                SelectedCulture = CultureInfo.CurrentCulture;

            ResourceManager = new ResourceManager(
                "MineCore.Languages.Strings_" + SelectedCulture.Name.Replace('/', '-'),
                typeof(StringManager).Assembly);

            if (ResourceManager.IsNull())
                ResourceManager =
                    new ResourceManager("MineCore.Languages.Strings_ja-JP", typeof(StringManager).Assembly);
        }

        public static string GetString(string key)
        {
            key.ThrownOnArgNull(nameof(key));

            return ResourceManager.GetString(key);
        }

        public static string GetString(string key, params object[] args)
        {
            key.ThrownOnArgNull(nameof(key));

            return string.Format(GetString(key), args);
        }
    }
}