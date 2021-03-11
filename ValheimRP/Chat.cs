/*
 * Chat.cs
 * Created by Ryan Broman on 2021-03-09
 * ryan@broman.dev
 */

using System;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using ValheimRP.Util;

// ReSharper disable StringIndexOfIsCultureSpecific.1

namespace ValheimRP {
    [UsedImplicitly]
    [HarmonyPatch(typeof(Chat))]
    internal class VRPChat {
        /// <summary>
        /// Logs all chat messages and pings.
        /// </summary>
        /// <param name="__instance">Chat instance.</param>
        /// <param name="senderID">ID of the player sending that chat.</param>
        /// <param name="pos">Sender's current position, or ping position if ping.</param>
        /// <param name="type">Channel the chat is being sent through.</param>
        /// <param name="user">Sender's character name.</param>
        /// <param name="text">Content of the chat.</param>
        [HarmonyPatch("OnNewChatMessage")]
        private static bool Prefix(Chat __instance, ref long senderID, ref Vector3 pos, ref Talker.Type type,
            ref string user, ref string text) {
            Log.LogInfo($"[Chat] [{Enum.GetName(typeof(Talker.Type), type)}] {user} ({senderID}) {pos}: {text}");
            
            // Prevent OOC messages from appearing on the player's head.
            if(!IsOOC(text)) return true;
            text = text.Replace('<', ' ');
            text = text.Replace('>', ' ');
            __instance.AddString(user, text, type);
            return false;
        }

        /// <summary>
        /// Directs custom chats to pseudo-channels.
        /// </summary>
        /// <param name="__instance">Chat instance</param>
        /// <param name="user">Sender's character name</param>
        /// <param name="text">Actual content of the message</param>
        /// <param name="type">Channel the chat is being sent through.</param>
        [HarmonyPatch("AddString", typeof(string), typeof(string), typeof(Talker.Type))]
        private static bool Prefix(Chat __instance, ref string user, ref string text, ref Talker.Type type) {
            if (!IsOOC(text)) return true;
            text = text.Substring(text.IndexOf(" "));

            var message = $"<color={ColorUtility.ToHtmlStringRGBA(Color.blue)}>[OOC] {user}:{text}</color>";
            __instance.AddString(message);
            
            return false;
        }

        private static bool IsOOC(string input) {
            return input.ToLower().StartsWith("/ooc") || input.ToLower().StartsWith("//");
        }
    }
}