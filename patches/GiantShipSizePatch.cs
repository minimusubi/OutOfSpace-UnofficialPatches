using HarmonyLib;
using UnityEngine;

namespace UnofficialPatches.patches {
	/**
	 * Adds the unused GIANT ship size to the size selector. Does not add MINI, as this triggers the tutorial.
	 */
	[HarmonyPatch(typeof(PlayerSelectUI))]
	internal class GiantShipSizePatch {
		[HarmonyPatch("InitializeCarrousel")]
		[HarmonyPostfix]
		private static void InitializeCarrousel(CarouselUI ___shipSizeCarousel) {
			int sizeValueLength = ___shipSizeCarousel.values.Count;

			if (sizeValueLength == 4) {
				___shipSizeCarousel.Initialize("SMALL", ["SMALL", "MEDIUM", "LARGE", "GIANT", "ANY"]);
			}
			___shipSizeCarousel.SetValue(PlayerPrefs.GetString("shipSize", "SMALL"));
		}
	}
}
