namespace TestNotifier
{
    internal class Program
    {
        internal static T GetService<T>() where T : class => SimpleServiceResolver.Instance.GetService<T>();

        static async Task Main(string[] args)
        {
            SimpleServiceResolver.CreateInstance((services) =>
            {
                //No custom services to register yet
            }, args);

            using var http = GetService<ISimpleHttpClientFactory>().CreateSimpleClient(nameof(Main), baseUrl: NotificationApi.ApiUrl);
            await http.GetNoResponseAsync(NotificationApi.SendApiName, queryParams: new
            {
                Id = NotificationApi.ApiKey,
                Title = "Basement Alarm Notification",
                Message = "Basement door is OPEN!",
                Type = NotificationApi.AlarmUnsecuredType
            });

            Console.WriteLine("Notification Sent!");
        }
    }
}
