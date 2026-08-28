using HarmonyLib;

namespace UnofficialPatches.patches {
	/*
	 * Fix occasional invisible aliens when clients disagree on alien position.
	 * Keeps non-authoritative alien copies alive so network transform updates can
	 * correct temporary room disagreements near closing doors.
	 */
	[HarmonyPatch(typeof(EnemyAI), "Update")]
	internal static class FixInvisibleAlienPatch {
		[HarmonyPrefix]
		private static void PreventRemoteAlienDeactivation(EnemyAI __instance) {
			if (!__instance.inactive || __instance.photonView == null || __instance.photonView.IsMine) {
				return;
			}

			__instance.inactive = false;
		}
	}
}
