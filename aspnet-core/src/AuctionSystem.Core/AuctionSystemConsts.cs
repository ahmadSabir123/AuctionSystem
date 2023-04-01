using AuctionSystem.Debugging;

namespace AuctionSystem
{
    public class AuctionSystemConsts
    {
        public const string LocalizationSourceName = "AuctionSystem";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "0565d7354d3743a7b583f2d7986f3115";
    }
}
