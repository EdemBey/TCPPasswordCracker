using System.Net;
using System.Net.Sockets;
using BrutServer;
using BrutServer.Models;

var listener = new TcpListener(IPAddress.Any, 4646);
listener.Start();
Console.WriteLine("Server ready");
var userInfos =
    PasswordFileHandler.ReadPasswordFile("passwords.txt");
using var fs = new FileStream("webster-dictionary.txt", FileMode.Open, FileAccess.Read);
using var dictionary = new StreamReader(fs).ReadToEndAsync();

Console.WriteLine("How many clients do you want to connect?");
var num = int.Parse(Console.ReadLine() ?? string.Empty);
var splitDictionary = SplitDictionary(num);

for(var i=0; i<num; i++)
{
    var socket = listener.AcceptTcpClient();//Until client comes in, the code doesn't continue
    Console.WriteLine("Incoming client");
    Task.Run(() =>
    {
        Client(socket, userInfos, splitDictionary[i]);
    });
}
static void Client(TcpClient socket, object data, string[] shortDictionary)
{
    var d = string.Join("\n ", shortDictionary);
    var send = DataHelp.ObjectToByteArray((data, shortDictionary));
    var ns = socket.GetStream();
    ns.Write(send, 0, send.Length);
}

string[][] SplitDictionary(int num)
{
    return dictionary.Result.Split("\n").Split(num);
}

