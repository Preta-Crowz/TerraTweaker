using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace zzzTerraTweaker {
    public class SyncSystem : ModSystem {
        public override void OnWorldLoad() {
            if (Main.netMode != NetmodeID.MultiplayerClient) return;
            ModPacket packet = ModLoader.GetMod("zzzTerraTweaker").GetPacket();
            packet.Write((byte)0); // Start sync
            packet.Send();
        }
    }
}