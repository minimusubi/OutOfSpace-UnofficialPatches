using Cinemachine;
using UnofficialPatches.utilities;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineTargetGroup;

namespace UnofficialPatches.patches {
	/**
	 * Adds a camera mode focused only on local players, toggleable with Left Control or Left Trigger.
	 */
	[HarmonyPatch(typeof(CameraManager))]
	internal class CameraZoomPatch {
		static Target[] originalTargets = null;

		[HarmonyPatch("Update")]
		[HarmonyPostfix]
		static private void Update(CinemachineTargetGroup ___targetGroup) {
			if (InputHelper.GetLeftTriggerDown() || InputHelper.GetLeftCtrlDown()) {
				SwapCamera(___targetGroup);
			}
		}

		static private void SwapCamera(CinemachineTargetGroup targetGroup) {
			if (originalTargets == null) {
				ActivateSoloCamera(targetGroup);
			} else {
				RestoreGroupCamera(targetGroup);
			}
		}

		static private void ActivateSoloCamera(CinemachineTargetGroup targetGroup) {
			MultiplayerManager manager = Singleton<MultiplayerManager>.Instance;
			originalTargets = targetGroup.m_Targets;
			List<Target> targets = [];

			foreach (PlayerCharacter character in manager.localPlayerCharacters) {
				targets.Add(new Target {
					target = character.gameObject.GetComponent<Transform>(),
					weight = 1,
					radius = 0
				});
			}

			targetGroup.m_Targets = [.. targets];
		}

		static private void RestoreGroupCamera(CinemachineTargetGroup targetGroup) {
			targetGroup.m_Targets = originalTargets;
			originalTargets = null;

		}
	}
}
