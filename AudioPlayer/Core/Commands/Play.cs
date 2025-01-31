﻿using System;
using System.IO;
using System.Linq;
using AudioPlayer.API;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace AudioPlayer.Core.Commands
{
    public class Play : ICommand
    {
        public static Play Instance { get; } = new Play();
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("audioplayer.play"))
            {
                response = "You dont have perms to do that";
                return false;
            }

            if (arguments.Count == 0)
            {
                response = "Usage: audioplayer play path";
                return false;
            }

            var path = string.Join(" ", arguments.ToArray());

            if (!File.Exists(path))
            {
                response = $"The provided path doesn't exist.\nPath: {path}";
                return false;
            }
            
            AudioController.PlayFromFile(path);
            response = "Playing...";
            return true;
        }

        public string Command { get; } = "play";
        public string[] Aliases { get; } = {"p"};
        public string Description { get; } = "Plays audio.";
    }
}