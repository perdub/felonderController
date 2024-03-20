using TL;
using WTelegram;

class Agent{
    private WTelegram.Client _client;
    public Client Client{get{return _client;}}

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
            case "password":
                return "felonderAgent";
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
        Console.WriteLine($"Target phone - {phoneNumber}");
        PhoneNumber = phoneNumber;
        buildClient();
    }

    public async Task SetPassword(Client c){
        const string old_password = "password";     // current password if any (unused otherwise)
        const string new_password = "felonderAgent"; // or null to disable 2FA
        var accountPwd = await c.Account_GetPassword();
        var password = accountPwd.current_algo == null ? null : await WTelegram.Client.InputCheckPassword(accountPwd, old_password);
        accountPwd.current_algo = null; // makes InputCheckPassword generate a new password
        var new_password_hash = new_password == null ? null : await WTelegram.Client.InputCheckPassword(accountPwd, new_password);
        await c.Account_UpdatePasswordSettings(password, new Account_PasswordInputSettings
        {
            flags = Account_PasswordInputSettings.Flags.has_new_algo,
            new_password_hash = new_password_hash?.A,
            new_algo = accountPwd.new_algo,
            hint = "project name please; ad:@NijikaIjichiBot",
        });
    }

    public async Task SetPhonePrivaty(){
        await _client.Account_SetPrivacy(InputPrivacyKey.PhoneNumber, new InputPrivacyValueAllowAll());
    }

    public async Task EnterToGroup(string link){
        if(link.Contains("joinchat")||link.Contains('+')){
            var invite = await _client.Messages_ImportChatInvite(link.Replace("https://t․me/+","").Replace("https://t․me/joinchat/", ""));
        }
        else{
            var resolved = await _client.Contacts_ResolveUsername(link.Replace("@", "").Replace("https://t.me/",""));
            if (resolved.Chat is Channel channel)
                await _client.Channels_JoinChannel(channel);
        }
    }

    public async Task ChangeProfil(string first_name, string last_name="", string bio=""){
        await _client.Account_UpdateProfile(first_name, last_name, bio);
    }

    public async Task<Chat> CreateGroup(string title){
        var group = await _client.Messages_CreateChat(Array.Empty<InputUserBase>(), title);
        foreach(var g in group.Chats){
            if(g.Value is Chat c){
                if(c.title==title){
                    return c;
                }
            }
        }
        return null;
    }
    public async Task<string> GetChatInviteLink(Chat c){
        var link = (ChatInviteExported)await _client.Messages_ExportChatInvite(c.ToInputPeer(), title:"felonder link");
        return link.link;
    }
    public async Task<User> GetMe(){
        return _client.User;
    }
    public async Task<User> GetUser(string phone){
        var u = await _client.Contacts_ResolvePhone('+'+phone);
        return u.User;
    }
    public async Task AddUser(Chat group, User user){
        await _client.AddChatUser(group.ToInputPeer(), new InputUser(user.id, user.access_hash));
    }
}