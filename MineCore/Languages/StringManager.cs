using System.Globalization;
using System.Resources;

namespace MineCore.Languages
{
    public static class StringManager
    {
        public static CultureInfo SelectedCulture { get; private set; }
        public static ResourceManager ResourceManager;

        public static void Init(CultureInfo cultureInfo = null)
        {
            if (cultureInfo == null)
                SelectedCulture = CultureInfo.CurrentCulture;

            ResourceManager = new ResourceManager("MineCore.Languages.Strings_" + cultureInfo.Name.Replace('/', '-'),
                typeof(StringManager).Assembly);

            if (ResourceManager == null)
                ResourceManager =
                    new ResourceManager("MineCore.Languages.Strings_ja-JP", typeof(StringManager).Assembly);
        }
    }
}