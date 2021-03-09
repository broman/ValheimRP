using System;
using HarmonyLib;
using JetBrains.Annotations;

namespace ValheimRP {
    [HarmonyPatch(typeof(Player), "OnDamaged")]
    internal class VRPPlayer {
        [UsedImplicitly]
        private static bool Prefix(Player __instance, HitData hit) {
            var attacker = hit.GetAttacker();
            var attackerName = "enemy or environment";
            if (attacker)
                attackerName = attacker.m_name;
            
            var damage = hit.GetTotalDamage();
            
            Log.LogInfo($"[Hit] {__instance.GetPlayerName()} hit by {attackerName} for {damage}dmg");
            return true;
        }
    }
}