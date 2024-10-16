using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Live_Server.SocketServer
{
    public class SockerServer
    {
        private static List<WebSocket> _clients = new List<WebSocket>();



        public static async Task StartServer()
        {
            string pathToWatch = @"C:\Users\himanshu.y.sharma\Desktop\"; // Change this to your directory

            var httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:5000/");
            httpListener.Start();
            Console.WriteLine("WebSocket server started on ws://localhost:5000");

            _ = Task.Run(async () => await AcceptWebSocketConnections(httpListener));

            FileSystemWatcher watcher = new FileSystemWatcher();
            
                watcher.Path = pathToWatch;
                watcher.Filter = "*.txt";

                watcher.NotifyFilter = NotifyFilters.LastWrite;

                watcher.Changed += OnChanged;
                watcher.EnableRaisingEvents = true;

               
            
        }
        private static async Task AcceptWebSocketConnections(HttpListener httpListener)
        {
            while (true)
            {
                var httpContext = await httpListener.GetContextAsync();
                if (httpContext.Request.IsWebSocketRequest)
                {
                    WebSocketContext webSocketContext = await httpContext.AcceptWebSocketAsync(null);
                    WebSocket webSocket = webSocketContext.WebSocket;
                    _clients.Add(webSocket);
                    Console.WriteLine("Client connected!");

                    // Handle incoming messages (optional)
                    _ = Task.Run(async () => await ReceiveMessages(webSocket));
                }
            }
        }

        private static async Task ReceiveMessages(WebSocket webSocket)
        {
            var buffer = new byte[1024];

            while (webSocket.State == WebSocketState.Open)
            {
                try
                {
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    // Handle incoming message if needed
                }
                catch (Exception)
                {
                    // Remove client on error
                    _clients.Remove(webSocket);
                    break;
                }
            }
        }

        private static async void OnChanged(object source, FileSystemEventArgs e)
        {
            // Wait for the file to be fully written
            await Task.Delay(500);

            // Read the contents of the file
            string content = File.ReadAllText(e.FullPath);
            Console.WriteLine($"File: {e.FullPath} changed. New content:");

            // Send content to all connected clients
            await SendToClients(content);
        }

        private static async Task SendToClients(string content)
        {
            var data = Encoding.UTF8.GetBytes(content);

            foreach (var client in _clients)
            {
                try
                {
                    await client.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                catch (Exception)
                {
                    // Remove client on error
                    _clients.Remove(client);
                }
            }
        

            Console.WriteLine(content);

            MyToolWindow.Instance?.UpdateText(content);
            
        }

    }
}
