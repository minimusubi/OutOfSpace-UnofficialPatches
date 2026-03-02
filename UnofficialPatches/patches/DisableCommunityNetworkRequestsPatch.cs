using HarmonyLib;
using TMPro;
using UnityEngine;

namespace UnofficialPatches.patches {
	/**
	 * Disables community-related network requests.
	 * There are three network requests made for update notes, and one to check
	 * if BeholdStudios is live on Twitch. All four of these network requests
	 * fail. The update notes endpoints do not work, and the Twitch API
	 * endpoint is outdated.
	 *
	 * The three update notes endpoints were:
	 * * http://beholdstudios.web1111.kinghost.net/oos/update_title.txt
	 * * http://beholdstudios.web1111.kinghost.net/oos/update_text.txt
	 * * http://beholdstudios.web1111.kinghost.net/oos/update_next.txt
	 * These network requests may be harmless, but since the endpoints are no
	 * longer maintained, and since it's not obvious that Behold Studios will
	 * always remain in control of these, I believe it's safer to simply remove
	 * these requests.
	 *
	 * The Twitch API used was the now deprecated Kraken API. See:
	 * https://discuss.dev.twitch.com/t/kraken-api-going-away/36674
	 */
	[HarmonyPatch(typeof(CommunityUI))]
	internal class DisableCommunityNetworkRequestsPatch {
		[HarmonyPatch("OnEnable")]
		[HarmonyPrefix]
		private static bool OnEnable(TextMeshProUGUI ___versionText) {
			// Taken from the original method
			BuildVersion buildVersion = Resources.Load<BuildVersion>("BuildVersion");
			___versionText.text = "Version " + buildVersion.GetVersion();

			// Skip the original method entirely.
			return false;
		}
	}
}
