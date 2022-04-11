using System;
using System.IO;
using System.Collections.Generic;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

using Moonlight;
using Moonlight.Variable;

namespace zzzTerraTweaker{
    class zzzTerraTweaker : Mod{
        public zzzTerraTweaker(){}

        private string DataPath, ScriptPath;
        private List<string> ScriptFiles = new List<string>();
        private List<string> Errors = new List<string>();
        private Dictionary<string,byte[]> HashDict = new Dictionary<string,byte[]>();
        private Dictionary<string,byte[]> ServerHash;

        private List<MLRecipe> recipes = new List<MLRecipe>();
        private List<MLItem> removes = new List<MLItem>();

        public override void Load(){
            this.ScriptPath = Path.Combine(Main.SavePath, "Scripts");
            this.Logger.Debug("Script Path : " + this.ScriptPath);
            Directory.CreateDirectory(this.ScriptPath);
            string[] allFiles = Directory.GetFiles(this.ScriptPath);
            foreach(string file in allFiles){
                if(Path.GetExtension(file) == ".moon"){
                    string fileName = Path.GetFileName(file);
                    this.ScriptFiles.Add(fileName);
            }}
            this.Logger.Debug("Script List : " + string.Join(",", this.ScriptFiles));
            SetupScripts();
        }

        public void SetupScripts(){
            foreach(string name in this.ScriptFiles){
                Script script = new Script(Path.Combine(this.ScriptPath, name));
                script.Load();
                HashDict.Add(name, script.hash);
                this.Logger.Info("Loaded " + name + ", MD5 : " + script.hexHash);
                this.Logger.Info("Start Compile " + name);
                script.Compile();
                this.Logger.Info("Compiled " + name);
                int i = 0;
                foreach(MLRecipe recipe in script.GetRecipes())
                    recipes.Add((MLRecipe)recipe);
                foreach(MLItem item in script.GetRemoves())
                    removes.Add(item);
            }
        }

        public override void PostAddRecipes(){
            RemoveRecipe(removes);
            foreach(MLRecipe recipe in recipes)
                RegisterRecipe(recipe);
        }

        public void RegisterRecipe(MLRecipe data){
            int res = data.GetValue().GetValue().Type;
            this.Logger.Debug("Creating new recipe for : " + data.GetValue().ToString() + "(" + res + ")");
            Recipe r = CreateRecipe(res, data.GetValue().Count);

            foreach(MLItem item in data.Ingredient){
                this.Logger.Debug("Adding Ingredient : " + item.ToString());
                this.Logger.Debug("Raw Data : " + item.GetValue());
                if(item.isVanilla) r = r.AddIngredient(item.GetValue(), item.Count);
                else r = r.AddIngredient(item.GetValue().Type, item.Count);
            }

            foreach(MLTile tile in data.Requirement){
                this.Logger.Debug("Adding Requirement : " + tile.ToString());
                this.Logger.Debug("Raw Data : " + tile.GetValue());
                if(tile.isVanilla) r = r.AddTile(tile.GetValue());
                else r = r.AddTile(tile.GetValue().Type);
            }
            this.Logger.Debug("Registering recipe for : " + data.GetValue().ToString() + "(" + res + ")");
            r.Register();
            this.Logger.Debug("Registered");
        }

        public void RemoveRecipe(List<MLItem> data){
            List<int> targets = new List<int>();
            foreach(MLItem item in data){
                if(item.isVanilla) targets.Add(item.GetValue());
                else targets.Add(item.GetValue().type);
            }
            for (int i = 0; i < Recipe.numRecipes; i++) {
                Recipe R = Main.recipe[i];
                if(targets.Exists(id => id == R.createItem.type))
                    R.RemoveRecipe();
            }
        }

        public override void HandlePacket(BinaryReader reader, int fromWho) {
            // Start(C)   : 0
            // Init(S)    : 0
            // ScriptData : 1 hash{16} name{}
            // DataDone(S): 2
            // Result(C)  : 2 result{1}
            byte type = reader.ReadByte();
            if (type == 0) {
                this.Logger.Debug("Got Start/Init Packet.");
                if (Main.netMode != NetmodeID.Server) ServerHash = new Dictionary<string,byte[]>();
                else SendHash(fromWho);
            } else if (type == 1) {
                byte[] hash = reader.ReadBytes(16);
                string name = reader.ReadString();
                ServerHash.Add(name, hash);
            } else if (type == 2) {
                if (Main.netMode != NetmodeID.Server) {
                    bool synced = CheckHash();
                    if (!synced) {
                        this.Logger.Error("Script is not same from server! Please contact to admin.");
                    }
                    ModPacket packet = this.GetPacket();
                    packet.Write((byte)2);
                    packet.Write(synced);
                    packet.Send(fromWho);
                } else {
                    bool result = reader.ReadBoolean();
                    if (result) this.Logger.Info("Verified script.");
                    else {
                        this.Logger.Info("Script verify failed. Kick player.");
                        NetMessage.SendData(2, fromWho, -1, NetworkText.FromKey("CLI.KickMessage", new object[0]), 0, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
        }

        public bool CheckHash() {
            this.Logger.Info("Start check hash");
            this.Logger.Debug("Script count : " + HashDict.Count.ToString() + "/" + ServerHash.Count.ToString());
            if (ServerHash.Count != HashDict.Count) return false;
            foreach( KeyValuePair<string, byte[]> HashData in ServerHash ) {
                if (!HashDict.ContainsKey(HashData.Key)) return false;
                this.Logger.Debug("Hash for " + HashData.Key + ":" + Convert.ToHexString(HashData.Value) + "/" + Convert.ToHexString(HashDict[HashData.Key]));
                if (Convert.ToHexString(HashData.Value) != Convert.ToHexString(HashDict[HashData.Key])) return false;
            }
            this.Logger.Debug("Hash Check OK");
            return true;
        }

        public void SendHash(int syncTarget) {
            this.Logger.Debug("Start send hash");
            ModPacket packet = this.GetPacket();
            packet.Write((byte)0); // Init
            packet.Send(syncTarget);
            foreach( KeyValuePair<string, byte[]> HashData in HashDict ) {
                packet = this.GetPacket();
                packet.Write((byte)1); // ScriptData
                packet.Write(HashData.Value);
                packet.Write(HashData.Key);
                packet.Send(syncTarget);
            }
            packet = this.GetPacket();
            packet.Write((byte)2); // DataDone
            packet.Send(syncTarget);
            this.Logger.Debug("Hash send OK");
        }
    }
}