namespace felonder;

class Program
{
    static void Main(string[] args)
    {
        Directory.CreateDirectory("agents_sessions");
        Console.WriteLine("Felonder Agents Contoller");
        AgentPool.InitPool();
        Console.ReadKey();
    }
}
