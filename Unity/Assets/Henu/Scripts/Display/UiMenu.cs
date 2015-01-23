﻿using Henu.Settings;
using Henu.State;
using UnityEngine;

namespace Henu.Display {

	/*================================================================================================*/
	public class UiMenu : MonoBehaviour {

		public static readonly float ScaleArcSize = 1.1f;

		private MenuState vMenuState;
		private ArcState vArcState;
		private ISettings vSettings;

		private UiPalm vUiPalm;
		private UiArc vUiArc;

		private Quaternion vLeftRot;
		private Quaternion vRightRot;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		internal void Build(MenuState pMenuState, ISettings pSettings) {
			vMenuState = pMenuState;
			vArcState = vMenuState.Arc;
			vSettings = pSettings;
			vLeftRot = Quaternion.identity;
			vRightRot = Quaternion.AngleAxis(180, Vector3.up);

			var palmObj = new GameObject("Palm");
			palmObj.transform.SetParent(gameObject.transform, false);
			vUiPalm = palmObj.AddComponent<UiPalm>();
			vUiPalm.Build(vArcState, vSettings);

			var arcObj = new GameObject("Arc");
			arcObj.transform.SetParent(gameObject.transform, false);
			vUiArc = arcObj.AddComponent<UiArc>();
			vUiArc.Build(vArcState, vSettings);
		}

		/*--------------------------------------------------------------------------------------------*/
		public void Update() {
			bool isLeft = vArcState.IsLeft;
			Vector3 scale = Vector3.one*(vArcState.Size*ScaleArcSize);

			if ( !isLeft ) {
				scale.z *= -1;
			}

			gameObject.transform.localPosition = vArcState.Center;
			gameObject.transform.localRotation = vArcState.Rotation;
			gameObject.transform.localScale = scale;

			vUiArc.gameObject.transform.localRotation = (isLeft ? vLeftRot : vRightRot);
			vUiPalm.gameObject.transform.localRotation = (isLeft ? vLeftRot : vRightRot);
		}

	}

}
