static class AgentPool{
    static List<Agent> AgentsPool = new ();
    public static void InitPool()
    {
        var sessions = Directory.GetFiles("agents_sessions","*.sess");
        foreach(var session in sessions){
            AgentsPool.Add(new Agent(session.Split('/')[1].Split('.')[0]));
        }
    }
    public static async Task<Agent> AddAgent()
    {
        var n = new Agent(generatePhoneNumber());

        AgentsPool.Add(n);

        return n;
    }
    private static string generatePhoneNumber(){
        byte d = (byte)(Random.Shared.NextInt64() % 3);
        short num = (short)Random.Shared.Next(10000);
        return "99966"+d+num;
    }
}