/*
 * WearNTear.cs
 * Created by Ryan Broman on 2021-03-13
 * ryan@broman.dev
 */

using HarmonyLib;
using JetBrains.Annotations;

namespace ValheimRP {
    [UsedImplicitly]
    [HarmonyPatch(typeof(WearNTear))]
    internal class VRPWearNTear {
        [HarmonyPatch("RPC_Damage")]
        private static bool Prefix(WearNTear __instance, long sender, HitData hit) {
            return !PrivateArea.CheckInPrivateArea(__instance.transform.position, true);
        }
    }
}