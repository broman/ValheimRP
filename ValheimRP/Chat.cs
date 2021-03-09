using System;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace ValheimRP {
    [HarmonyPatch(typeof(Chat), "OnNewChatMessage")]
    internal class VRPChat {
        [UsedImplicitly]
        private static bool Prefix(Chat __instance, ref long senderID, ref Vector3 pos, ref Talker.Type type, ref string user, ref string text) {
            Log.LogInfo($"[Chat] [{Enum.GetName(typeof(Talker.Type), type)}] {user} ({senderID}) {pos}: {text}");
            return true;
        }

        [UsedImplicitly]
        private static void Postfix(Chat __instance) {
        }
    }
}