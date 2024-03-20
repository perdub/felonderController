using Jint;

namespace felonder;

class Program
{
    static async Task Main(string[] args)
    {
        Directory.CreateDirectory("agents_sessions");
        Console.WriteLine("Felonder Agents Contoller");
        AgentPool.InitPool();
        var jsEngine = new Engine(cfg => cfg.AllowClr(typeof(Agent).Assembly));
        jsEngine.SetValue("getFromPool", (Func<string, string, Agent>)((string firstName, string lastName="") => {
            return AgentPool.GetAgentAsync(firstName, lastName).Result;
        }));
        jsEngine.Execute(File.ReadAllText("script.js"));
        Console.ReadKey();
    }
}
