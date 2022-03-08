using System.Net;
using System.Net.Sockets;
using BrutServer;
using BrutServer.Models;

var listener = new TcpListener(IPAddress.Any, 4646);
listener.Start();
Console.WriteLine("Server ready");
var userInfos =
    PasswordFileHandler.ReadPasswordFile("passwords.txt");
while (true)
{
    var socket = listener.AcceptTcpClient();//Until client comes in, the code doesn't continue
    Console.WriteLine("Incoming client");
    Task.Run(() =>
    {
        Client(socket, userInfos);
    });
}
static void Client(TcpClient socket, object data)
{
    var ns = socket.GetStream();
    var send = DataHelp.ObjectToByteArray(data);
    ns.Write(send, 0, send.Length);
}

