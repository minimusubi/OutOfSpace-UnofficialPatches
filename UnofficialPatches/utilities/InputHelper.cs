using HarmonyLib;
using Rewired;
using UnityEngine;
using static Rewired.Controller;

namespace UnofficialPatches.utilities {
	[HarmonyPatch]
	internal class InputHelper {
		static bool wasLeftTriggerPressed = false;
		static bool isLeftTriggerDown = false;

		[HarmonyPatch(typeof(CameraManager), "Update")]
		[HarmonyPostfix]
		private static void Update() {
			bool isLeftTriggerPressed = false;
			isLeftTriggerDown = false;

			foreach (Joystick joystick in ReInput.controllers.Joysticks) {
				foreach (Axis axis in joystick.Axes) {
					if (axis.elementIdentifier.name != "Left Trigger") {
						continue;
					}

					if (axis.value > 0.6f) {
						isLeftTriggerPressed = true;
						break;
					}
				}
			}

			if (isLeftTriggerPressed && !wasLeftTriggerPressed) {
				isLeftTriggerDown = true;
			}

			wasLeftTriggerPressed = isLeftTriggerPressed;
		}

		internal static bool GetLeftTriggerDown() {
			return isLeftTriggerDown;
		}

		internal static bool GetLeftCtrlDown() {
			return Input.GetKeyDown(KeyCode.LeftControl);
		}
	}
}
