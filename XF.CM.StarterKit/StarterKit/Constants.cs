namespace XF.CM.StarterKit
{
    public static class Constants
    {
#if !DEBUG
        /* Replace these values on the build server using a special task */
        public const string ServiceUrl = "$serviceUrl$";
        public const string AppVersion = "$appVersion$";
#else
        public const string ServiceUrl = "http://localhost:57900";
        public const string AppVersion = "1.0.0";
#endif

    }
}
