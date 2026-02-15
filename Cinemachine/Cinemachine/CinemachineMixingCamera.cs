using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200002E RID: 46
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[DisallowMultipleComponent]
	[ExecuteAlways]
	[ExcludeFromPreset]
	[AddComponentMenu("Cinemachine/CinemachineMixingCamera")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineMixingCamera.html")]
	public class CinemachineMixingCamera : CinemachineVirtualCameraBase
	{
		// Token: 0x06000107 RID: 263 RVA: 0x00008698 File Offset: 0x00006898
		public float GetWeight(int index)
		{
			switch (index)
			{
			case 0:
				return this.m_Weight0;
			case 1:
				return this.m_Weight1;
			case 2:
				return this.m_Weight2;
			case 3:
				return this.m_Weight3;
			case 4:
				return this.m_Weight4;
			case 5:
				return this.m_Weight5;
			case 6:
				return this.m_Weight6;
			case 7:
				return this.m_Weight7;
			default:
				Debug.LogError("CinemachineMixingCamera: Invalid index: " + index.ToString());
				return 0f;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00008720 File Offset: 0x00006920
		public void SetWeight(int index, float w)
		{
			switch (index)
			{
			case 0:
				this.m_Weight0 = w;
				return;
			case 1:
				this.m_Weight1 = w;
				return;
			case 2:
				this.m_Weight2 = w;
				return;
			case 3:
				this.m_Weight3 = w;
				return;
			case 4:
				this.m_Weight4 = w;
				return;
			case 5:
				this.m_Weight5 = w;
				return;
			case 6:
				this.m_Weight6 = w;
				return;
			case 7:
				this.m_Weight7 = w;
				return;
			default:
				Debug.LogError("CinemachineMixingCamera: Invalid index: " + index.ToString());
				return;
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000087AC File Offset: 0x000069AC
		public float GetWeight(CinemachineVirtualCameraBase vcam)
		{
			this.ValidateListOfChildren();
			int index;
			if (this.m_indexMap.TryGetValue(vcam, out index))
			{
				return this.GetWeight(index);
			}
			Debug.LogError("CinemachineMixingCamera: Invalid child: " + ((vcam != null) ? vcam.Name : "(null)"));
			return 0f;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00008804 File Offset: 0x00006A04
		public void SetWeight(CinemachineVirtualCameraBase vcam, float w)
		{
			this.ValidateListOfChildren();
			int index;
			if (this.m_indexMap.TryGetValue(vcam, out index))
			{
				this.SetWeight(index, w);
				return;
			}
			Debug.LogError("CinemachineMixingCamera: Invalid child: " + ((vcam != null) ? vcam.Name : "(null)"));
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00008855 File Offset: 0x00006A55
		// (set) Token: 0x0600010C RID: 268 RVA: 0x0000885D File Offset: 0x00006A5D
		private ICinemachineCamera LiveChild { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00008866 File Offset: 0x00006A66
		public override CameraState State
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000886E File Offset: 0x00006A6E
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00008876 File Offset: 0x00006A76
		public override Transform LookAt { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000887F File Offset: 0x00006A7F
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00008887 File Offset: 0x00006A87
		public override Transform Follow { get; set; }

		// Token: 0x06000112 RID: 274 RVA: 0x00008890 File Offset: 0x00006A90
		public override void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
			this.ValidateListOfChildren();
			CinemachineVirtualCameraBase[] childCameras = this.m_ChildCameras;
			for (int i = 0; i < childCameras.Length; i++)
			{
				childCameras[i].OnTargetObjectWarped(target, positionDelta);
			}
			base.OnTargetObjectWarped(target, positionDelta);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000088CC File Offset: 0x00006ACC
		public override void ForceCameraPosition(Vector3 pos, Quaternion rot)
		{
			this.ValidateListOfChildren();
			CinemachineVirtualCameraBase[] childCameras = this.m_ChildCameras;
			for (int i = 0; i < childCameras.Length; i++)
			{
				childCameras[i].ForceCameraPosition(pos, rot);
			}
			base.ForceCameraPosition(pos, rot);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00008906 File Offset: 0x00006B06
		protected override void OnEnable()
		{
			base.OnEnable();
			this.InvalidateListOfChildren();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00008914 File Offset: 0x00006B14
		public void OnTransformChildrenChanged()
		{
			this.InvalidateListOfChildren();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000891C File Offset: 0x00006B1C
		protected override void OnValidate()
		{
			base.OnValidate();
			for (int i = 0; i < 8; i++)
			{
				this.SetWeight(i, Mathf.Max(0f, this.GetWeight(i)));
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00008954 File Offset: 0x00006B54
		public override bool IsLiveChild(ICinemachineCamera vcam, bool dominantChildOnly = false)
		{
			CinemachineVirtualCameraBase[] children = this.ChildCameras;
			int i = 0;
			while (i < 8 && i < children.Length)
			{
				if (children[i] == vcam)
				{
					return this.GetWeight(i) > 0.0001f && children[i].isActiveAndEnabled;
				}
				i++;
			}
			return false;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000899A File Offset: 0x00006B9A
		public CinemachineVirtualCameraBase[] ChildCameras
		{
			get
			{
				this.ValidateListOfChildren();
				return this.m_ChildCameras;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000089A8 File Offset: 0x00006BA8
		protected void InvalidateListOfChildren()
		{
			this.m_ChildCameras = null;
			this.m_indexMap = null;
			this.LiveChild = null;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000089C0 File Offset: 0x00006BC0
		protected void ValidateListOfChildren()
		{
			if (this.m_ChildCameras != null)
			{
				return;
			}
			this.m_indexMap = new Dictionary<CinemachineVirtualCameraBase, int>();
			List<CinemachineVirtualCameraBase> list = new List<CinemachineVirtualCameraBase>();
			foreach (CinemachineVirtualCameraBase i in base.GetComponentsInChildren<CinemachineVirtualCameraBase>(true))
			{
				if (i.transform.parent == base.transform)
				{
					int index = list.Count;
					list.Add(i);
					if (index < 8)
					{
						this.m_indexMap.Add(i, index);
					}
				}
			}
			this.m_ChildCameras = list.ToArray();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00008A48 File Offset: 0x00006C48
		public override void OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime)
		{
			base.OnTransitionFromCamera(fromCam, worldUp, deltaTime);
			base.InvokeOnTransitionInExtensions(fromCam, worldUp, deltaTime);
			CinemachineVirtualCameraBase[] children = this.ChildCameras;
			int i = 0;
			while (i < 8 && i < children.Length)
			{
				children[i].OnTransitionFromCamera(fromCam, worldUp, deltaTime);
				i++;
			}
			this.InternalUpdateCameraState(worldUp, deltaTime);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00008A94 File Offset: 0x00006C94
		public override void InternalUpdateCameraState(Vector3 worldUp, float deltaTime)
		{
			CinemachineVirtualCameraBase[] children = this.ChildCameras;
			this.LiveChild = null;
			float highestWeight = 0f;
			float totalWeight = 0f;
			int i = 0;
			while (i < 8 && i < children.Length)
			{
				CinemachineVirtualCameraBase vcam = children[i];
				if (vcam.isActiveAndEnabled)
				{
					float weight = Mathf.Max(0f, this.GetWeight(i));
					if (weight > 0.0001f)
					{
						totalWeight += weight;
						if (totalWeight == weight)
						{
							this.m_State = vcam.State;
						}
						else
						{
							this.m_State = CameraState.Lerp(this.m_State, vcam.State, weight / totalWeight);
						}
						if (weight > highestWeight)
						{
							highestWeight = weight;
							this.LiveChild = vcam;
						}
					}
				}
				i++;
			}
			base.InvokePostPipelineStageCallback(this, CinemachineCore.Stage.Finalize, ref this.m_State, deltaTime);
		}

		// Token: 0x040000E6 RID: 230
		public const int MaxCameras = 8;

		// Token: 0x040000E7 RID: 231
		[Tooltip("The weight of the first tracked camera")]
		public float m_Weight0 = 0.5f;

		// Token: 0x040000E8 RID: 232
		[Tooltip("The weight of the second tracked camera")]
		public float m_Weight1 = 0.5f;

		// Token: 0x040000E9 RID: 233
		[Tooltip("The weight of the third tracked camera")]
		public float m_Weight2 = 0.5f;

		// Token: 0x040000EA RID: 234
		[Tooltip("The weight of the fourth tracked camera")]
		public float m_Weight3 = 0.5f;

		// Token: 0x040000EB RID: 235
		[Tooltip("The weight of the fifth tracked camera")]
		public float m_Weight4 = 0.5f;

		// Token: 0x040000EC RID: 236
		[Tooltip("The weight of the sixth tracked camera")]
		public float m_Weight5 = 0.5f;

		// Token: 0x040000ED RID: 237
		[Tooltip("The weight of the seventh tracked camera")]
		public float m_Weight6 = 0.5f;

		// Token: 0x040000EE RID: 238
		[Tooltip("The weight of the eighth tracked camera")]
		public float m_Weight7 = 0.5f;

		// Token: 0x040000EF RID: 239
		private CameraState m_State = CameraState.Default;

		// Token: 0x040000F3 RID: 243
		private CinemachineVirtualCameraBase[] m_ChildCameras;

		// Token: 0x040000F4 RID: 244
		private Dictionary<CinemachineVirtualCameraBase, int> m_indexMap;
	}
}
