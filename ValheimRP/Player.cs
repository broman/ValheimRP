using HarmonyLib;
using JetBrains.Annotations;

namespace ValheimRP {
    [HarmonyPatch(typeof(Player), "OnDamaged")]
    internal class VRPPlayer {
        /// <summary>
        /// Logs any damage done to the player.
        /// </summary>
        /// <param name="__instance">Player instance being damaged.</param>
        /// <param name="hit">Context of the hit being performed.</param>
        /// <returns>true always</returns>
        [UsedImplicitly]
        private static bool Prefix(Player __instance, HitData hit) {
            var attacker = GetPlayerInfo(hit.GetAttacker());

            var damage = hit.GetTotalDamage();
            
            Log.LogInfo($"{GetPlayerInfo(__instance)} hit by {attacker} for {damage}dmg");
            return true;
        }

        private static string GetPlayerInfo(Character character) {
            return !character ? "environment" : $"{character.m_name}{character.GetCenterPoint()}";
        }

        private static string GetPlayerInfo(Player player) {
            return $"{player.GetPlayerName()}{player.GetHeadPoint()}";
        }
    }
}