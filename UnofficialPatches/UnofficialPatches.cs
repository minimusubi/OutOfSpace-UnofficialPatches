using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;

namespace UnofficialPatches;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class UnofficialPatchesPlugin : BaseUnityPlugin {
	internal static new ManualLogSource Logger;
	private readonly Harmony harmony = new("musubi.outofspace.UnofficialPatches");

	private void Awake() {
		// Plugin startup logic
		Logger = base.Logger;
		Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

		harmony.PatchAll(Assembly.GetExecutingAssembly());
	}
}
