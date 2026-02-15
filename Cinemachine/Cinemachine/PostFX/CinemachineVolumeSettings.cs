using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Cinemachine.PostFX
{
	// Token: 0x020000E2 RID: 226
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[ExecuteAlways]
	[AddComponentMenu("")]
	[SaveDuringPlay]
	[DisallowMultipleComponent]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineVolumeSettings.html")]
	public class CinemachineVolumeSettings : CinemachineExtension
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0002179B File Offset: 0x0001F99B
		public bool IsValid
		{
			get
			{
				return this.m_Profile != null && this.m_Profile.components.Count > 0;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000217C0 File Offset: 0x0001F9C0
		public void InvalidateCachedProfile()
		{
			List<CinemachineVolumeSettings.VcamExtraState> list = base.GetAllExtraStates<CinemachineVolumeSettings.VcamExtraState>();
			for (int i = 0; i < list.Count; i++)
			{
				list[i].DestroyProfileCopy();
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000217F1 File Offset: 0x0001F9F1
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_FocusTracksTarget)
			{
				this.m_FocusTracking = ((base.VirtualCamera.LookAt != null) ? CinemachineVolumeSettings.FocusTrackingMode.LookAtTarget : CinemachineVolumeSettings.FocusTrackingMode.Camera);
			}
			this.m_FocusTracksTarget = false;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00021825 File Offset: 0x0001FA25
		protected override void OnDestroy()
		{
			this.InvalidateCachedProfile();
			base.OnDestroy();
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00021834 File Offset: 0x0001FA34
		protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
		{
			if (stage == CinemachineCore.Stage.Finalize)
			{
				CinemachineVolumeSettings.VcamExtraState extra = base.GetExtraState<CinemachineVolumeSettings.VcamExtraState>(vcam);
				if (!this.IsValid)
				{
					extra.DestroyProfileCopy();
					return;
				}
				VolumeProfile profile = this.m_Profile;
				if (this.m_FocusTracking == CinemachineVolumeSettings.FocusTrackingMode.None)
				{
					extra.DestroyProfileCopy();
				}
				else
				{
					if (extra.mProfileCopy == null)
					{
						extra.CreateProfileCopy(this.m_Profile);
					}
					profile = extra.mProfileCopy;
					DepthOfField dof;
					if (profile.TryGet<DepthOfField>(out dof))
					{
						float focusDistance = this.m_FocusOffset;
						if (this.m_FocusTracking == CinemachineVolumeSettings.FocusTrackingMode.LookAtTarget)
						{
							focusDistance += (state.FinalPosition - state.ReferenceLookAt).magnitude;
						}
						else
						{
							Transform focusTarget = null;
							CinemachineVolumeSettings.FocusTrackingMode focusTracking = this.m_FocusTracking;
							if (focusTracking != CinemachineVolumeSettings.FocusTrackingMode.FollowTarget)
							{
								if (focusTracking == CinemachineVolumeSettings.FocusTrackingMode.CustomTarget)
								{
									focusTarget = this.m_FocusTarget;
								}
							}
							else
							{
								focusTarget = base.VirtualCamera.Follow;
							}
							if (focusTarget != null)
							{
								focusDistance += (state.FinalPosition - focusTarget.position).magnitude;
							}
						}
						state.Lens.FocusDistance = (dof.focusDistance.value = Mathf.Max(0f, focusDistance));
						profile.isDirty = true;
					}
				}
				state.AddCustomBlendable(new CameraState.CustomBlendable(profile, 1f));
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000429A File Offset: 0x0000249A
		private static void OnCameraCut(CinemachineBrain brain)
		{
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0002196C File Offset: 0x0001FB6C
		private static void ApplyPostFX(CinemachineBrain brain)
		{
			CameraState state = brain.CurrentCameraState;
			int numBlendables = state.NumCustomBlendables;
			List<Volume> volumes = CinemachineVolumeSettings.GetDynamicBrainVolumes(brain, numBlendables);
			for (int i = 0; i < volumes.Count; i++)
			{
				volumes[i].weight = 0f;
				volumes[i].sharedProfile = null;
				volumes[i].profile = null;
			}
			Volume firstVolume = null;
			int numPPblendables = 0;
			for (int j = 0; j < numBlendables; j++)
			{
				CameraState.CustomBlendable b = state.GetCustomBlendable(j);
				VolumeProfile profile = b.m_Custom as VolumeProfile;
				if (!(profile == null))
				{
					Volume v = volumes[j];
					if (firstVolume == null)
					{
						firstVolume = v;
					}
					v.sharedProfile = profile;
					v.isGlobal = true;
					v.priority = CinemachineVolumeSettings.s_VolumePriority - (float)(numBlendables - j) - 1f;
					v.weight = b.m_Weight;
					numPPblendables++;
				}
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00021A64 File Offset: 0x0001FC64
		private static List<Volume> GetDynamicBrainVolumes(CinemachineBrain brain, int minVolumes)
		{
			GameObject volumeOwner = null;
			Transform t = brain.transform;
			int numChildren = t.childCount;
			CinemachineVolumeSettings.sVolumes.Clear();
			int i = 0;
			while (volumeOwner == null && i < numChildren)
			{
				GameObject child = t.GetChild(i).gameObject;
				if (child.hideFlags == HideFlags.HideAndDontSave)
				{
					child.GetComponents<Volume>(CinemachineVolumeSettings.sVolumes);
					if (CinemachineVolumeSettings.sVolumes.Count > 0)
					{
						volumeOwner = child;
					}
				}
				i++;
			}
			if (minVolumes > 0)
			{
				if (volumeOwner == null)
				{
					volumeOwner = new GameObject(CinemachineVolumeSettings.sVolumeOwnerName);
					volumeOwner.hideFlags = HideFlags.HideAndDontSave;
					volumeOwner.transform.parent = t;
				}
				UniversalAdditionalCameraData data = brain.gameObject.GetComponent<UniversalAdditionalCameraData>();
				if (data != null)
				{
					int mask = data.volumeLayerMask;
					for (int j = 0; j < 32; j++)
					{
						if ((mask & (1 << j)) != 0)
						{
							volumeOwner.layer = j;
							break;
						}
					}
				}
				while (CinemachineVolumeSettings.sVolumes.Count < minVolumes)
				{
					CinemachineVolumeSettings.sVolumes.Add(volumeOwner.gameObject.AddComponent<Volume>());
				}
			}
			return CinemachineVolumeSettings.sVolumes;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00021B7C File Offset: 0x0001FD7C
		[RuntimeInitializeOnLoadMethod]
		private static void InitializeModule()
		{
			CinemachineCore.CameraUpdatedEvent.RemoveListener(new UnityAction<CinemachineBrain>(CinemachineVolumeSettings.ApplyPostFX));
			CinemachineCore.CameraUpdatedEvent.AddListener(new UnityAction<CinemachineBrain>(CinemachineVolumeSettings.ApplyPostFX));
			CinemachineCore.CameraCutEvent.RemoveListener(new UnityAction<CinemachineBrain>(CinemachineVolumeSettings.OnCameraCut));
			CinemachineCore.CameraCutEvent.AddListener(new UnityAction<CinemachineBrain>(CinemachineVolumeSettings.OnCameraCut));
		}

		// Token: 0x04000496 RID: 1174
		public static float s_VolumePriority = 1000f;

		// Token: 0x04000497 RID: 1175
		[HideInInspector]
		public bool m_FocusTracksTarget;

		// Token: 0x04000498 RID: 1176
		[Tooltip("If the profile has the appropriate overrides, will set the base focus distance to be the distance from the selected target to the camera.The Focus Offset field will then modify that distance.")]
		public CinemachineVolumeSettings.FocusTrackingMode m_FocusTracking;

		// Token: 0x04000499 RID: 1177
		[Tooltip("The target to use if Focus Tracks Target is set to Custom Target")]
		public Transform m_FocusTarget;

		// Token: 0x0400049A RID: 1178
		[Tooltip("Offset from target distance, to be used with Focus Tracks Target.  Offsets the sharpest point away from the focus target.")]
		public float m_FocusOffset;

		// Token: 0x0400049B RID: 1179
		[Tooltip("This profile will be applied whenever this virtual camera is live")]
		public VolumeProfile m_Profile;

		// Token: 0x0400049C RID: 1180
		private static string sVolumeOwnerName = "__CMVolumes";

		// Token: 0x0400049D RID: 1181
		private static List<Volume> sVolumes = new List<Volume>();

		// Token: 0x020000E3 RID: 227
		public enum FocusTrackingMode
		{
			// Token: 0x0400049F RID: 1183
			None,
			// Token: 0x040004A0 RID: 1184
			LookAtTarget,
			// Token: 0x040004A1 RID: 1185
			FollowTarget,
			// Token: 0x040004A2 RID: 1186
			CustomTarget,
			// Token: 0x040004A3 RID: 1187
			Camera
		}

		// Token: 0x020000E4 RID: 228
		private class VcamExtraState
		{
			// Token: 0x0600052D RID: 1325 RVA: 0x00021C04 File Offset: 0x0001FE04
			public void CreateProfileCopy(VolumeProfile source)
			{
				this.DestroyProfileCopy();
				VolumeProfile profile = ScriptableObject.CreateInstance<VolumeProfile>();
				if (source != null)
				{
					foreach (VolumeComponent volumeComponent in source.components)
					{
						VolumeComponent itemCopy = global::UnityEngine.Object.Instantiate<VolumeComponent>(volumeComponent);
						profile.components.Add(itemCopy);
						profile.isDirty = true;
					}
				}
				this.mProfileCopy = profile;
			}

			// Token: 0x0600052E RID: 1326 RVA: 0x00021C84 File Offset: 0x0001FE84
			public void DestroyProfileCopy()
			{
				if (this.mProfileCopy != null)
				{
					RuntimeUtility.DestroyObject(this.mProfileCopy);
				}
				this.mProfileCopy = null;
			}

			// Token: 0x040004A4 RID: 1188
			public VolumeProfile mProfileCopy;
		}
	}
}
