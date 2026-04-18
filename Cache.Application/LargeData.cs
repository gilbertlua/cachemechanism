using System.Data.Common;

public class LargeData
{
    public User GetLargeData(string id)
    {
        Thread.Sleep(3000);
        Console.WriteLine("Load from data source 3000ms");
        return new User();
    }
    private List<User>? _users = new List<User>
    {
        new User { Id = 1, Name = "A" },
        new User { Id = 2, Name = "B" },
        new User { Id = 3, Name = "C" },
        new User { Id = 4, Name = "D" },
    };
}

public class User
{
    public int Id{get;set;}
    public string? Name{get;set;}
}