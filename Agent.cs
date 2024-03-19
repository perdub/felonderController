using TL;
using WTelegram;

class Agent{
    private WTelegram.Client _client;

    //in 999 66 x yyyy format(without + sign!)
    public string PhoneNumber{get;private set;}

    private string? config(string par){
        switch(par){
            case "session_pathname":
                return $"agents_sessions/{PhoneNumber}.sess";
            case "api_id":
                return "5";
            case "api_hash":
                return "1c5c96d5edd401b1ed40db3fb5633e2d";
            case "server_address":
                return "2>149.154.167.40:443";
            case "phone_number":
                return '+'+PhoneNumber;
            case "verification_code":
                var d = PhoneNumber[5];
                string code = "";
                for(int i = 0; i<5;i++){
                    code+=d;
                }
                return code;
            default:
                return null;
        }
    }

    private void buildClient(){
        _client = new Client(config);
        _client.LoginUserIfNeeded().Wait();
        Console.WriteLine($"Agent with {PhoneNumber} created!");
    }

    public Agent(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
        buildClient();
    }

    public async Task EnterToGroup(){
        
    }
}