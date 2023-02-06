public static class NotificationApi
{
#if LOCAL_TESTING
    public static string ApiUrl => "http://localhost:5020";

#else
    public static string ApiUrl => "https://wirepusher.com";
#endif
    public static string SendApiName => "send";
    public static string ApiKey => "API_KEY_HERE";
    public static string AlarmUnsecuredType => "Alarm - Unsecured";
    public static string AlarmSecuredType => "Alarm - Secured";
}
