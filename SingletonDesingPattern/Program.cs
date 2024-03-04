public class Logger
{
    private static readonly Logger instance = new Logger();
    private Logger()
    {
        Console.WriteLine($"{nameof(Logger)} nesnesi oluşturuldu.");
    }

    // static constructor kullanarak Thread-safe işlemi sağlandı
    // farklı Thread kullanımında yalnızca bir adet nesne üretilmesi sağlandı.
    public static Logger GetInstance
    {
        get { return instance; }
    }

    public void AddLog(string taskName)
    {
        Console.WriteLine($"Task '{taskName}' içinde {nameof(Logger)} örneği kullanılıyor: {GetHashCode()}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Task.Run(() =>
        {
            Logger instance1 = Logger.GetInstance;
            instance1.AddLog("Task 1");
        });

        Task.Run(() =>
        {
            Task.Delay(1000).Wait();
            Logger instance2 = Logger.GetInstance;
            instance2.AddLog("Task 2");
        });

        Task.Run(() =>
        {
            Logger instance3 = Logger.GetInstance;
            instance3.AddLog("Task 3");
        });
        Console.ReadLine();
    }
}
