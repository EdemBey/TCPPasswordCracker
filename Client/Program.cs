// See https://aka.ms/new-console-template for more information

using System.Net.Sockets;
using BrutServer.Models;
using Client;

Console.WriteLine("Hello, World!");

var clientSocket = new TcpClient("127.0.0.1", 4646);
var ns = clientSocket.GetStream();
Console.WriteLine("OK");

var receiveBuffer = new byte[1024];
ns.Read(receiveBuffer, 0, receiveBuffer.Length);
var userInfos = DataHelp.ByteArrayToObject(receiveBuffer);

var cracker = new Cracking();
cracker.RunCracking((IEnumerable<UserInfo>) userInfos);
