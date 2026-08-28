using HarmonyLib;
using UnityEngine;

namespace UnofficialPatches.patches {
	/**
	 * Prevents player input while the game is out of focus.
	 */
	[HarmonyPatch(typeof(PlayerCharacter))]
	internal class NoInputInBackground {
		[HarmonyPatch("GetInput")]
		[HarmonyPrefix]
		private static bool GetInput() {
			if (!Application.isFocused) {
				// The application is in the background; skip processing inputs
				return false;
			}

			return true;
		}
	}
}
