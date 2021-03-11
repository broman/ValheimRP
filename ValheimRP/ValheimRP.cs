/*
 * ValheimRP.cs
 * Created by Ryan Broman on 2021-03-08
 * ryan@broman.dev
 */

using BepInEx;
using HarmonyLib;
using ValheimRP.Util;

namespace ValheimRP {
    [BepInPlugin(ModGuid, ModName, ModVer)]
    public class ValheimRP : BaseUnityPlugin {
        private const string ModGuid = AuthorName + "." + ModName;
        private const string AuthorName = "ValheimRP";
        private const string ModName = "ValheimRP";
        private const string ModVer = "0.0.1";

        private static Harmony _harmonyInstance;

        internal static ValheimRP Instance { get; private set; }

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake() {
            Instance = this;
            Log.Init(Logger);
            Init();
        }
        private void OnDestroy() {
            Disable();
        }

        private static void Init() {
            EnableMonoModHooks();

            EnableHarmonyPatches();
        }

        /// <summary>
        /// Disables the enabled hooks.
        /// Should be called when the game scene is destroyed, or mod is unloaded.
        /// <seealso cref="OnDestroy"/>
        /// </summary>
        private static void Disable() {
            DisableMonoModHooks();

            DisableHarmonyPatches();
        }

        private static void EnableMonoModHooks() {
            On.FejdStartup.Awake += OnFejdStartupAwakeMonoModHookShowcase;
        }

        private static void DisableMonoModHooks() {
            On.FejdStartup.Awake -= OnFejdStartupAwakeMonoModHookShowcase;
        }

        private static void EnableHarmonyPatches() {
            _harmonyInstance = new Harmony(ModGuid);
            _harmonyInstance.PatchAll();
        }

        private static void DisableHarmonyPatches() {
            _harmonyInstance.UnpatchSelf();
        }

        private static void OnFejdStartupAwakeMonoModHookShowcase(On.FejdStartup.orig_Awake orig, FejdStartup self) {
            // calling the original method
            orig(self);
        }
    }
}