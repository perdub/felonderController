static class AgentPool{
    static List<Agent> AgentsPool = new ();
    static Queue<Agent> freeAgents;
    public static void InitPool()
    {
        var sessions = Directory.GetFiles("agents_sessions","*.sess");
        foreach(var session in sessions){
            AgentsPool.Add(new Agent(session.Split('\\')[1].Split('.')[0]));
        }
        freeAgents = new Queue<Agent>(AgentsPool);
    }
    public static async Task<Agent> AddAgent()
    {
        var n = new Agent(generatePhoneNumber());

        AgentsPool.Add(n);

        await n.EnterToGroup("https://t.me/kessokuband");
        await n.SetPassword(n.Client);
        await n.SetPhonePrivaty();

        return n;
    }
    private static string generatePhoneNumber(){
        byte d = (byte)((Random.Shared.NextInt64() % 3) + 1);
        short num = (short)Random.Shared.Next(10000);

        //add zeros behind number
        short zeros = (short)(4-Convert.ToInt16(Math.Ceiling(Math.Log10(num))));
        string z = "";
        for(int i = 0; i<zeros;i++){
            z+='0';
        }

        return "99966"+d+z+num;
    }
    public static async Task<Agent> GetAgentAsync(string first_name, string last_name){
        Agent agent;
        bool suss = freeAgents.TryDequeue(out agent);
        if(!suss){
            agent = await AddAgent();
        }

        await agent.ChangeProfil(first_name, last_name, "felonder agent lol");

        return agent;
    }
}