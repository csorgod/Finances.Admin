using Hanssens.Net;

namespace Finances.Common.Helpers
{
    public class LocalStorageSingleton
    {
        private static LocalStorage instance = null;

        public static LocalStorage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LocalStorage();
                }
                return instance;
            }
        }
    }
}
