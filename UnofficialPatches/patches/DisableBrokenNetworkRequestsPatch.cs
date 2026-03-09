using HarmonyLib;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UnofficialPatches.patches {
	/**
	 * Disables broken network requests.
	 */
	[HarmonyPatch]
	internal class DisableBrokenNetworkRequestsPatch {

		/**
		 * This makes three network requests for patch notes and one to check
		 * if BeholdStudios is live on Twitch. All four network requests fail.
		 * The patch note endpoints hang and time out, and the Twitch API
		 * endpoint is deprecated.
		 *
		 * The three update notes endpoints were:
		 * * http://beholdstudios.web1111.kinghost.net/oos/update_title.txt
		 * * http://beholdstudios.web1111.kinghost.net/oos/update_text.txt
		 * * http://beholdstudios.web1111.kinghost.net/oos/update_next.txt
		 * These network requests may be harmless, but since it seems that
		 * Behold Studios currently does not currently control them, I believe
		 * it's safer to simply remove these requests.
		 *
		 * The Twitch API used was the now deprecated Kraken API. See:
		 * * https://discuss.dev.twitch.com/t/kraken-api-going-away/36674
		 */
		[HarmonyPatch(typeof(CommunityUI), "OnEnable")]
		[HarmonyPrefix]
		private static bool CommunityUI_OnEnable(TextMeshProUGUI ___versionText) {
			// Taken from the original method
			BuildVersion buildVersion = Resources.Load<BuildVersion>("BuildVersion");
			___versionText.text = "Version " + buildVersion.GetVersion();

			// Skip the original method entirely
			return false;
		}

		/**
		 * This was used to notify the server that a match started. It sent a
		 * request to the following endpoint:
		 * * http://beholdstudios.web36f37.kinghost.net/oos/serverstatus.php?function=newgame
		 * This endpoint now returns 404 Not Found.
		 */
		[HarmonyPatch(typeof(LoopManager), "SendStartedMatchToServer")]
		[HarmonyPrefix]
		private static bool LoopManager_SendStartedMatchToServer(ref IEnumerator __result) {
			// Skip the original method entirely
			return false;
		}

		/**
		 * This was used to update the server with the number of players
		 * connected to the Photon network. It sent a request to the following
		 * endpoint:
		 * * http://beholdstudios.web36f37.kinghost.net/oos/serverstatus.php?function=setplayersconnected&number={count}
		 * This endpoint now returns 404 Not Found.
		 */
		[HarmonyPatch(typeof(PlayerSelectUI), "SetPlayersConnected")]
		[HarmonyPrefix]
		private static bool PlayerSelectUI_SetPlayersConnected(int value, ref int ___playersConnected, ref IEnumerator __result) {
			// Taken from the original method
			___playersConnected = value;
			// This coroutine should do nothing
			__result = GetEmptyEnumerator();

			// Skip the original method entirely
			return false;
		}

		/**
		 * This was used to update the number of connected players shown in the
		 * lobby screen. It sent a request to the following endpoint:
		 * * http://beholdstudios.web36f37.kinghost.net/oos/serverstatus.php?function=getplayersconnected
		 * This endpoint now returns 404 Not Found.
		 */
		[HarmonyPatch(typeof(PlayerSelectUI), "UpdatePlayersConnectedRoutine")]
		[HarmonyPrefix]
		private static bool PlayerSelectUI_UpdatePlayersConnectedRoutine(ref IEnumerator __result) {
			// This coroutine should do nothing
			__result = GetEmptyEnumerator();

			// Skip the original method entirely
			return false;
		}

		/**
		 * This was used update the average waiting time shown in the lobby
		 * screen. It sent a request to the following endpoint:
		 * * http://beholdstudios.web36f37.kinghost.net/oos/serverstatus.php?function=gameslasthour
		 * This endpoint now returns 404 Not Found.
		 */
		[HarmonyPatch(typeof(PlayerSelectUI), "UpdateWaitingTimeRoutine")]
		[HarmonyPrefix]
		private static bool PlayerSelectUI_UpdateWaitingTimeRoutine(ref IEnumerator __result) {
			// This coroutine should do nothing
			__result = GetEmptyEnumerator();

			// Skip the original method entirely
			return false;
		}

		private static IEnumerator GetEmptyEnumerator() {
			yield break;
		}
	}
}
