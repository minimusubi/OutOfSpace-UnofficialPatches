using HarmonyLib;

namespace UnofficialPatches.patches {
    /**
     * Allows you to pause and unpause the game with the same button (menu/start button).
     */
    [HarmonyPatch(typeof(PauseUI))]
    internal class ControllerUnpausePatch {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        private static void Update(PauseUI __instance, DialogWindow ___dialogWindowOpen) {
            if ((bool)___dialogWindowOpen) {
                // There's a dialog open (such as Are you sure you want to restart the match?)
                return;
            }

            LoopManager manager = Singleton<LoopManager>.Instance;

            if (manager == null) {
                // LoopManager doesn't exist, so we're not in-game yet
                return;
            }

            // Attempt to find out if the game was already unpaused
            // Get private bool LoopManager.gamePaused
            bool gamePaused = Traverse.Create(manager).Field<bool>("gamePaused").Value;

            if (!gamePaused) {
                // Game is already unpaused
                return;
            }

            // Use the same input logic used to open the pause menu
            // Either the ESC key or Rewired Button 7 (The menu button on Xbox One/start button on Xbox 360)
            // Call private bool LoopManager.StartButtonPressed()
            bool startButtonPressed = Traverse.Create(manager)
                  .Method("StartButtonPressed")
                  .GetValue<bool>();

            if (startButtonPressed) {
                // Call public PauseUI.Resume()
                Traverse.Create(__instance).Method("Resume").GetValue();
            }
        }
    }
}
