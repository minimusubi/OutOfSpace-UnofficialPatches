using HarmonyLib;

namespace UnofficialPatches.patches {
	/**
	 * Prevents the main menu music from restarting unnecessarily. For example, when:
	 * * Clicking "Back to main menu" from the match screen
	 * * Exiting the Compendium
	 * * Closing the Settings
	 */
	[HarmonyPatch(typeof(AudioManager))]
	internal class NoRestartMenuMusicPatch {
		const string MAIN_MENU_EVENT_REF = "event:/01_Music/Main Menu";
		static string currentMusic = null;

		[HarmonyPatch("StartMusic")]
		[HarmonyPrefix]
		private static bool StartMusicPrefix(string eventRef) {
			if (currentMusic == MAIN_MENU_EVENT_REF && eventRef == MAIN_MENU_EVENT_REF) {
				// Main menu music is already playing, skip
				return false;
			}

			// Let the original method continue
			return true;
		}

		[HarmonyPatch("StartMusic")]
		[HarmonyPostfix]
		private static void StartMusicPostfix(string eventRef) {
			currentMusic = eventRef;
		}

		[HarmonyPatch("StopMusic")]
		[HarmonyPostfix]
		private static void StopMusic() {
			currentMusic = null;
		}
	}
}
