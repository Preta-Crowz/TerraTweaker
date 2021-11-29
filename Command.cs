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
                case "hand": HandInfo(caller);
                    break;
                case "help": Help(caller);
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
        }

        void HandInfo(CommandCaller caller){
            ModItem item = caller.Player.HeldItem.modItem;
            if(item == null || item.mod == null){
                Item vitem = caller.Player.HeldItem;
                if(vitem == null){
                    caller.Reply("You don\'t hold any item.");
                    return;
                }
                caller.Reply(Lang.GetItemName(vitem.type).Key.Split('.')[1]);
                caller.Reply("Code : " + vitem.type);
                caller.Reply("Max Stack : " + vitem.maxStack);
                return;
            }
            caller.Reply("From : " + item.mod.Name);
            caller.Reply("Name : " + item.Name);
            caller.Reply("Max Stack : " + caller.Player.HeldItem.maxStack);
        }
    }
}