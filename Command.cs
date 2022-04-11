using System;

using Terraria.ModLoader;
using Terraria;

using Moonlight.Vanilla;

namespace zzzTerraTweaker{
    class TTCommand : ModCommand{
        public TTCommand(){}

        public override string Description => "TerraTweaker Command, requires subcommand for use";
        public override string Usage => "/tt help";

        override public string Command{
            get{return "tt";}
        }
        override public CommandType Type {
            get{return CommandType.Chat;}
        }

        public override void Action(CommandCaller caller, string input, string[] args){
            if(args.Length < 1){
                caller.Reply("You must use subcommand, use /tt help for more information.");
                return;
            }
            switch(args[0]){
                case "help": Help(caller);
                    break;
                case "hand": HandInfo(caller);
                    break;
                case "syntax": Syntax(caller);
                    break;
                default:
                    caller.Reply("Unknown subcommand, use /tt help for more information.");
                    break;
            }
            return;
        }

        void Help(CommandCaller caller){
            caller.Reply("/tt help");
            caller.Reply("  Display this information.");
            caller.Reply("/tt hand");
            caller.Reply("  Get information about item that you hold.");
            caller.Reply("/tt syntax");
            caller.Reply("  Check syntax for scripts without reload.");
        }

        void HandInfo(CommandCaller caller){
            Item item = caller.Player.HeldItem;
            if(item == null){
                caller.Reply("You don\'t hold any item.");
                return;
            }
            caller.Reply(Lang.GetItemName(item.type).Key.Split('.')[1]);
            caller.Reply("Code : " + item.type);
            if(item.ModItem != null) {
                caller.Reply("From : " + item.ModItem.Mod.Name);
                caller.Reply("Name : " + item.ModItem.Name);
            }
            caller.Reply("Max Stack : " + item.maxStack);
        }

        void Syntax(CommandCaller caller){
            zzzTerraTweaker TT = (zzzTerraTweaker)Mod;
            caller.Reply("Start syntax checking, script count : " + TT.ScriptFiles.Count.ToString());
            TT.SetupScripts(true);
            caller.Reply("Done.");
            caller.Reply("Successfuly compiled : " + TT.Compiled.Count.ToString());
            if (TT.Failed.Count == 0) caller.Reply("You should reload to apply script to game.");
            else {
                caller.Reply("Failed compiled : " + TT.Failed.Count.ToString());
                caller.Reply("Failed scripts :");
                foreach (string name in TT.Failed) {
                    caller.Reply("  " + name);
                }
                caller.Reply("Check log file for more information.");
            }
        }
    }
}