using BepInEx;
using HarmonyLib;

namespace ValheimRP {
    [BepInPlugin(ModGuid, ModName, ModVer)]
    public class ValheimRP : BaseUnityPlugin {
        private const string ModGuid = AuthorName + "." + ModName;
        private const string AuthorName = "ValheimRP";
        private const string ModName = "ValheimRP";
        private const string ModVer = "0.0.1";

        internal static Harmony HarmonyInstance;

        internal static ValheimRP Instance { get; private set; }

        /// <summary>
        ///     Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake() {
            Instance = this;

            Log.Init(Logger);
            
            Log.LogInfo("Testing 123!");
            Init();
        }

        /// <summary>
        ///     Destroying the attached Behaviour will result in the game or Scene receiving OnDestroy.
        ///     OnDestroy occurs when a Scene or game ends.
        ///     It is also called when your mod is unloaded, this is where you do clean up of hooks, harmony patches,
        ///     loose GameObjects and loose monobehaviours.
        ///     Loose here refers to gameobjects not attached
        ///     to the parent BepIn GameObject where your BaseUnityPlugin is attached
        /// </summary>
        private void OnDestroy() {
            Disable();
        }

        internal static void Init() {
            EnableMonoModHooks();

            EnableHarmonyPatches();
        }

        /// <summary>
        ///     Disable the enabled hooks.
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
            HarmonyInstance = new Harmony(ModGuid);
            HarmonyInstance.PatchAll();
        }

        private static void DisableHarmonyPatches() {
            HarmonyInstance.UnpatchSelf();
        }

        private static void OnFejdStartupAwakeMonoModHookShowcase(On.FejdStartup.orig_Awake orig, FejdStartup self) {
            // calling the original method
            orig(self);
        }
    }
}