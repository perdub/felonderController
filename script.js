var nijika = getFromPool("Нидзика", "Идзити");
var ryo = getFromPool("Рё", "Ямада");
var kita = getFromPool("Кита", "Икуё");
var bocchi = getFromPool("Хитори", "Гото");

var seika = getFromPool("Сейка", "Идзити");
var pa = getFromPool("Па-сан");

var futari = getFromPool("Футари", "Гото");

var group = nijika.CreateGroup("сука блять").Result;
var kita_nijika_contact = nijika.GetUser(kita.PhoneNumber).Result;
nijika.AddUser(group, kita_nijika_contact).Result;


var group = "https://t.me/+U0g3vb4LUy9kMjBj";

nijika.EnterToGroup(group);
ryo.EnterToGroup(group);
kita.EnterToGroup(group);
bocchi.EnterToGroup(group);