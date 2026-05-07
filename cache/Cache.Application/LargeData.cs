using System.Data.Common;
using Cache.Service;
public class LargeData
{
    public UserProfile GetLargeData(string id)
    {
        Thread.Sleep(3000);
        Console.WriteLine("Load from data source 3000ms");
        return new UserProfile()
        {
            Id = 2,
            Name = "John"
        };
    }
}
