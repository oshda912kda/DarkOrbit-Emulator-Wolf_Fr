using MySQLManager.Database.Session_Details.Interfaces;
using Newtonsoft.Json;
using Ow.Chat;
using Ow.Game;
using Ow.Game.Ticks;
using Ow.Managers;
using Ow.Managers.MySQLManager;
using Ow.Net;
using Ow.Net.netty;
using Ow.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ow
{
    class Program
    {
        public static TickManager TickManager = new TickManager();

        public static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                CheckMySQLConnection();
                LoadDatabase();
                InitiateServer();
                KeepAlive();
            } 
            catch(Exception e)
            {
                Out.WriteLine("Main void exception: " + e, "Program.cs");
                Logger.Log("error_log", $"- [Program.cs] Main void exception: {e}");
            }
        }

        private static void KeepAlive()
        {
            while (true)
            {
                var l = Console.ReadLine();
                if (l != "" && l.StartsWith("/"))
                    ExecuteCommand(l);
            }
        }

        public static bool CheckMySQLConnection()
        {
            if (!SqlDatabaseManager.Initialized)
            {
                int tries = 0;
                TRY:
                try
                {
                    SqlDatabaseManager.Initialize();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Out.WriteLine("\x1b[34mWelcome on Wolf_Fr Emulator\x1b[0m");
                    Out.WriteLine("\x1b[34m", "EMU");
                    Out.WriteLine("\x1b[34mWW      WW  OOOOO  LL      FFFFFFF         FFFFFFF RRRRRR       \x1b[0m", "EMU");
                    Out.WriteLine("\x1b[34mWW      WW OO   OO LL      FF              FF      RR   RR      \x1b[0m", "EMU");
                    Out.WriteLine("\x1b[34mWW   W  WW OO   OO LL      FFFF            FFFF    RRRRRR       \x1b[0m", "EMU");
                    Out.WriteLine("\x1b[34m WW WWW WW OO   OO LL      FF              FF      RR  RR       \x1b[0m", "EMU");
                    Out.WriteLine("\x1b[34m  WW   WW   OOOO0  LLLLLLL FF      _______ FF      RR   RR      \x1b[0m", "EMU");
                    Out.WriteLine("\x1b[34m \x1b[0m", "EMU");
                    Out.WriteLine("\x1b[34mEEEEEEE MM    MM UU   UU LL        AAA   TTTTTTT  OOOOO  RRRRRR \x1b[0m", "EMU");
                    Out.WriteLine("\x1b[34mEE      MMM  MMM UU   UU LL       AAAAA    TTT   OO   OO RR   RR\x1b[0m", "EMU");
                    Out.WriteLine("\x1b[34mEEEEE   MM MM MM UU   UU LL      AA   AA   TTT   OO   OO RRRRRR \x1b[0m", "EMU");
                    Out.WriteLine("\x1b[34mEE      MM    MM UU   UU LL      AAAAAAA   TTT   OO   OO RR  RR \x1b[0m", "EMU");
                    Out.WriteLine("\x1b[34mEEEEEEE MM    MM  UUUUU  LLLLLLL AA   AA   TTT    OOOO0  RR   RR\x1b[0m", "EMU");
                    Out.WriteLine("\x1b[34m", "EMU");
                    Out.WriteLine("\x1b[34m▓▓▓▓▓▓▓▓▓▓\x1b[0m \x1b[37m░░░░░░░░\x1b[0m \x1b[31m▓▓▓▓▓▓▓▓▓▓\x1b[0m", "FRA"); // Blue
                    Out.WriteLine("\x1b[34m▓▓▓▓▓▓▓▓▓▓\x1b[0m \x1b[37m░░░░░░░░\x1b[0m \x1b[31m▓▓▓▓▓▓▓▓▓▓\x1b[0m", "FRA"); // Blue
                    Out.WriteLine("\x1b[34m▓▓▓▓▓▓▓▓▓▓\x1b[0m \x1b[37m░░░░░░░░\x1b[0m \x1b[31m▓▓▓▓▓▓▓▓▓▓\x1b[0m", "FRA"); // Blue
                    Out.WriteLine("\x1b[34m▓▓▓▓▓▓▓▓▓▓\x1b[0m \x1b[37m░░░░░░░░\x1b[0m \x1b[31m▓▓▓▓▓▓▓▓▓▓\x1b[0m", "FRA"); // Blue
                    Out.WriteLine("\x1b[34m▓▓▓▓▓▓▓▓▓▓\x1b[0m \x1b[37m░░░░░░░░\x1b[0m \x1b[31m▓▓▓▓▓▓▓▓▓▓\x1b[0m", "FRA"); // Blue
                    Out.WriteLine("\x1b[34m▓▓▓▓▓▓▓▓▓▓\x1b[0m \x1b[37m░░░░░░░░\x1b[0m \x1b[31m▓▓▓▓▓▓▓▓▓▓\x1b[0m", "FRA"); // Blue
                    Out.WriteLine("\x1b[34m▓▓▓▓▓▓▓▓▓▓\x1b[0m \x1b[37m░░░░░░░░\x1b[0m \x1b[31m▓▓▓▓▓▓▓▓▓▓\x1b[0m", "FRA"); // Blue
                    Out.WriteLine("\x1b[34m▓▓▓▓▓▓▓▓▓▓\x1b[0m \x1b[37m░░░░░░░░\x1b[0m \x1b[31m▓▓▓▓▓▓▓▓▓▓\x1b[0m", "FRA"); // Blue
                    

                    
                    
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("MYSQL Connection failed: " + e);
                    Out.WriteLine("MYSQL Connection failed!");
                    if (tries < 6)
                    {
                        Out.WriteLine("Trying to reconnect in .. " + tries + " seconds.");
                        Thread.Sleep(tries * 1000);
                        tries++;
                        goto TRY;
                    } else Environment.Exit(0);
                }
            }
            return false;
        }

        public static void LoadDatabase()
        {
            QueryManager.LoadClans();
            QueryManager.LoadShips();
            QueryManager.LoadNpcs();
            QueryManager.LoadMaps();
        }

        public static void InitiateServer()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            Handler.AddCommands();
            Room.AddRooms();
            EventManager.InitiateEvents();
            StartListening();
        }

        public static void StartListening()
        {
            Task.Factory.StartNew(GameServer.StartListening);
            Out.WriteLine("\x1b[32mListening on port " + GameServer.Port + ".\x1b[0m", "GameServer"); // Green
            Task.Factory.StartNew(ChatServer.StartListening);
            Out.WriteLine("\x1b[33mListening on port " + ChatServer.Port + ".\x1b[0m", "ChatServer"); // Yellow
            Task.Factory.StartNew(SocketServer.StartListening);
            Out.WriteLine("\x1b[31mListening on port " + SocketServer.Port + ".\x1b[0m", "SocketServer"); // Red
            Out.WriteLine("\x1b[34mwolffr.ddns.net\x1b[0m");

            Task.Factory.StartNew(TickManager.Tick);
        }

        public static void ExecuteCommand(string txt)
        {
            var packet = txt.Replace("/", "");
            var splitted = packet.Split(' ');
        
            switch (splitted[0])
            {
                case "restart":
                    GameManager.Restart(Convert.ToInt32(splitted[1]));
                    break;
                case "list_players":
                    foreach (var gameSession in GameManager.GameSessions.Values)
                    {
                        if (gameSession != null)
                            Out.WriteLine($"{gameSession.Player.Name} ({gameSession.Player.Id})");
                    }
                    break;
            }        
        }
    }
}