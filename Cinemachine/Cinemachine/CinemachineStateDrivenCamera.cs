using System;
using System.Collections.Generic;
using System.Text;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000035 RID: 53
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[DisallowMultipleComponent]
	[ExecuteAlways]
	[ExcludeFromPreset]
	[AddComponentMenu("Cinemachine/CinemachineStateDrivenCamera")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineStateDrivenCamera.html")]
	public class CinemachineStateDrivenCamera : CinemachineVirtualCameraBase
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00009550 File Offset: 0x00007750
		public override string Description
		{
			get
			{
				if (this.mActiveBlend != null)
				{
					return this.mActiveBlend.Description;
				}
				ICinemachineCamera vcam = this.LiveChild;
				if (vcam == null)
				{
					return "(none)";
				}
				StringBuilder stringBuilder = CinemachineDebug.SBFromPool();
				stringBuilder.Append("[");
				stringBuilder.Append(vcam.Name);
				stringBuilder.Append("]");
				string text = stringBuilder.ToString();
				CinemachineDebug.ReturnToPool(stringBuilder);
				return text;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000095B8 File Offset: 0x000077B8
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000095C0 File Offset: 0x000077C0
		public ICinemachineCamera LiveChild { get; set; }

		// Token: 0x06000141 RID: 321 RVA: 0x000095C9 File Offset: 0x000077C9
		public override bool IsLiveChild(ICinemachineCamera vcam, bool dominantChildOnly = false)
		{
			return vcam == this.LiveChild || (this.mActiveBlend != null && this.mActiveBlend.Uses(vcam));
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000095EC File Offset: 0x000077EC
		public override CameraState State
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000095F4 File Offset: 0x000077F4
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00009602 File Offset: 0x00007802
		public override Transform LookAt
		{
			get
			{
				return base.ResolveLookAt(this.m_LookAt);
			}
			set
			{
				this.m_LookAt = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000960B File Offset: 0x0000780B
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00009619 File Offset: 0x00007819
		public override Transform Follow
		{
			get
			{
				return base.ResolveFollow(this.m_Follow);
			}
			set
			{
				this.m_Follow = value;
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00009624 File Offset: 0x00007824
		public override void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
			this.UpdateListOfChildren();
			CinemachineVirtualCameraBase[] childCameras = this.m_ChildCameras;
			for (int i = 0; i < childCameras.Length; i++)
			{
				childCameras[i].OnTargetObjectWarped(target, positionDelta);
			}
			base.OnTargetObjectWarped(target, positionDelta);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00009660 File Offset: 0x00007860
		public override void ForceCameraPosition(Vector3 pos, Quaternion rot)
		{
			this.UpdateListOfChildren();
			CinemachineVirtualCameraBase[] childCameras = this.m_ChildCameras;
			for (int i = 0; i < childCameras.Length; i++)
			{
				childCameras[i].ForceCameraPosition(pos, rot);
			}
			base.ForceCameraPosition(pos, rot);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000969A File Offset: 0x0000789A
		public override void OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime)
		{
			base.OnTransitionFromCamera(fromCam, worldUp, deltaTime);
			base.InvokeOnTransitionInExtensions(fromCam, worldUp, deltaTime);
			this.m_TransitioningFrom = fromCam;
			this.InternalUpdateCameraState(worldUp, deltaTime);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000096C0 File Offset: 0x000078C0
		public override void InternalUpdateCameraState(Vector3 worldUp, float deltaTime)
		{
			this.UpdateListOfChildren();
			CinemachineVirtualCameraBase best = this.ChooseCurrentCamera();
			if (best != null && !best.gameObject.activeInHierarchy)
			{
				best.gameObject.SetActive(true);
				best.UpdateCameraState(worldUp, deltaTime);
			}
			ICinemachineCamera previousCam = this.LiveChild;
			this.LiveChild = best;
			if (previousCam != this.LiveChild && this.LiveChild != null)
			{
				this.LiveChild.OnTransitionFromCamera(previousCam, worldUp, deltaTime);
				CinemachineCore.Instance.GenerateCameraActivationEvent(this.LiveChild, previousCam);
				if (previousCam != null)
				{
					this.mActiveBlend = base.CreateBlend(previousCam, this.LiveChild, this.LookupBlend(previousCam, this.LiveChild), this.mActiveBlend);
					if (this.mActiveBlend == null || !this.mActiveBlend.Uses(previousCam))
					{
						CinemachineCore.Instance.GenerateCameraCutEvent(this.LiveChild);
					}
				}
			}
			if (this.mActiveBlend != null)
			{
				this.mActiveBlend.TimeInBlend += ((deltaTime >= 0f) ? deltaTime : this.mActiveBlend.Duration);
				if (this.mActiveBlend.IsComplete)
				{
					this.mActiveBlend = null;
				}
			}
			if (this.mActiveBlend != null)
			{
				this.mActiveBlend.UpdateCameraState(worldUp, deltaTime);
				this.m_State = this.mActiveBlend.State;
			}
			else if (this.LiveChild != null)
			{
				if (this.m_TransitioningFrom != null)
				{
					this.LiveChild.OnTransitionFromCamera(this.m_TransitioningFrom, worldUp, deltaTime);
				}
				this.m_State = this.LiveChild.State;
			}
			this.m_TransitioningFrom = null;
			base.InvokePostPipelineStageCallback(this, CinemachineCore.Stage.Finalize, ref this.m_State, deltaTime);
			this.PreviousStateIsValid = true;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00009850 File Offset: 0x00007A50
		protected override void OnEnable()
		{
			base.OnEnable();
			this.InvalidateListOfChildren();
			this.mActiveBlend = null;
			CinemachineDebug.OnGUIHandlers = (CinemachineDebug.OnGUIDelegate)Delegate.Remove(CinemachineDebug.OnGUIHandlers, new CinemachineDebug.OnGUIDelegate(this.OnGuiHandler));
			CinemachineDebug.OnGUIHandlers = (CinemachineDebug.OnGUIDelegate)Delegate.Combine(CinemachineDebug.OnGUIHandlers, new CinemachineDebug.OnGUIDelegate(this.OnGuiHandler));
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000098B0 File Offset: 0x00007AB0
		protected override void OnDisable()
		{
			base.OnDisable();
			CinemachineDebug.OnGUIHandlers = (CinemachineDebug.OnGUIDelegate)Delegate.Remove(CinemachineDebug.OnGUIHandlers, new CinemachineDebug.OnGUIDelegate(this.OnGuiHandler));
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000098D8 File Offset: 0x00007AD8
		public void OnTransformChildrenChanged()
		{
			this.InvalidateListOfChildren();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000098E0 File Offset: 0x00007AE0
		private void OnGuiHandler()
		{
			if (!this.m_ShowDebugText)
			{
				CinemachineDebug.ReleaseScreenPos(this);
				return;
			}
			StringBuilder stringBuilder = CinemachineDebug.SBFromPool();
			stringBuilder.Append(base.Name);
			stringBuilder.Append(": ");
			stringBuilder.Append(this.Description);
			string text = stringBuilder.ToString();
			GUI.Label(CinemachineDebug.GetScreenPos(this, text, GUI.skin.box), text, GUI.skin.box);
			CinemachineDebug.ReturnToPool(stringBuilder);
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00009954 File Offset: 0x00007B54
		public CinemachineVirtualCameraBase[] ChildCameras
		{
			get
			{
				this.UpdateListOfChildren();
				return this.m_ChildCameras;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00009962 File Offset: 0x00007B62
		public bool IsBlending
		{
			get
			{
				return this.mActiveBlend != null;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000996D File Offset: 0x00007B6D
		public CinemachineBlend ActiveBlend
		{
			get
			{
				return this.mActiveBlend;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00009975 File Offset: 0x00007B75
		public static int CreateFakeHash(int parentHash, AnimationClip clip)
		{
			return Animator.StringToHash(parentHash.ToString() + "_" + clip.name);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00009994 File Offset: 0x00007B94
		private int LookupFakeHash(int parentHash, AnimationClip clip)
		{
			if (this.mHashCache == null)
			{
				this.mHashCache = new Dictionary<AnimationClip, List<CinemachineStateDrivenCamera.HashPair>>();
			}
			List<CinemachineStateDrivenCamera.HashPair> list = null;
			if (!this.mHashCache.TryGetValue(clip, out list))
			{
				list = new List<CinemachineStateDrivenCamera.HashPair>();
				this.mHashCache[clip] = list;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].parentHash == parentHash)
				{
					return list[i].hash;
				}
			}
			int newHash = CinemachineStateDrivenCamera.CreateFakeHash(parentHash, clip);
			list.Add(new CinemachineStateDrivenCamera.HashPair
			{
				parentHash = parentHash,
				hash = newHash
			});
			this.mStateParentLookup[newHash] = parentHash;
			return newHash;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00009A3B File Offset: 0x00007C3B
		private void InvalidateListOfChildren()
		{
			this.m_ChildCameras = null;
			this.LiveChild = null;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00009A4C File Offset: 0x00007C4C
		private void UpdateListOfChildren()
		{
			if (this.m_ChildCameras != null && this.mInstructionDictionary != null && this.mStateParentLookup != null)
			{
				return;
			}
			List<CinemachineVirtualCameraBase> list = new List<CinemachineVirtualCameraBase>();
			foreach (CinemachineVirtualCameraBase i in base.GetComponentsInChildren<CinemachineVirtualCameraBase>(true))
			{
				if (i.transform.parent == base.transform)
				{
					list.Add(i);
				}
			}
			this.m_ChildCameras = list.ToArray();
			this.ValidateInstructions();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00009AC4 File Offset: 0x00007CC4
		internal void ValidateInstructions()
		{
			if (this.m_Instructions == null)
			{
				this.m_Instructions = Array.Empty<CinemachineStateDrivenCamera.Instruction>();
			}
			this.mInstructionDictionary = new Dictionary<int, int>();
			for (int i = 0; i < this.m_Instructions.Length; i++)
			{
				if (this.m_Instructions[i].m_VirtualCamera != null && this.m_Instructions[i].m_VirtualCamera.transform.parent != base.transform)
				{
					this.m_Instructions[i].m_VirtualCamera = null;
				}
				this.mInstructionDictionary[this.m_Instructions[i].m_FullHash] = i;
			}
			this.mStateParentLookup = new Dictionary<int, int>();
			if (this.m_ParentHash != null)
			{
				foreach (CinemachineStateDrivenCamera.ParentHash j in this.m_ParentHash)
				{
					this.mStateParentLookup[j.m_Hash] = j.m_ParentHash;
				}
			}
			this.mHashCache = null;
			this.mActivationTime = (this.mPendingActivationTime = 0f);
			this.mActiveBlend = null;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00009BE0 File Offset: 0x00007DE0
		private CinemachineVirtualCameraBase ChooseCurrentCamera()
		{
			if (this.m_ChildCameras == null || this.m_ChildCameras.Length == 0)
			{
				this.mActivationTime = 0f;
				return null;
			}
			CinemachineVirtualCameraBase defaultCam = this.m_ChildCameras[0];
			if (this.m_AnimatedTarget == null || !this.m_AnimatedTarget.gameObject.activeSelf || this.m_AnimatedTarget.runtimeAnimatorController == null || this.m_LayerIndex < 0 || !this.m_AnimatedTarget.hasBoundPlayables || this.m_LayerIndex >= this.m_AnimatedTarget.layerCount)
			{
				this.mActivationTime = 0f;
				return defaultCam;
			}
			int hash;
			if (this.m_AnimatedTarget.IsInTransition(this.m_LayerIndex))
			{
				AnimatorStateInfo info = this.m_AnimatedTarget.GetNextAnimatorStateInfo(this.m_LayerIndex);
				this.m_AnimatedTarget.GetNextAnimatorClipInfo(this.m_LayerIndex, this.m_clipInfoList);
				hash = this.GetClipHash(info.fullPathHash, this.m_clipInfoList);
			}
			else
			{
				AnimatorStateInfo info2 = this.m_AnimatedTarget.GetCurrentAnimatorStateInfo(this.m_LayerIndex);
				this.m_AnimatedTarget.GetCurrentAnimatorClipInfo(this.m_LayerIndex, this.m_clipInfoList);
				hash = this.GetClipHash(info2.fullPathHash, this.m_clipInfoList);
			}
			while (hash != 0 && !this.mInstructionDictionary.ContainsKey(hash))
			{
				hash = (this.mStateParentLookup.ContainsKey(hash) ? this.mStateParentLookup[hash] : 0);
			}
			float now = CinemachineCore.CurrentTime;
			if (this.mActivationTime != 0f)
			{
				if (this.mActiveInstruction.m_FullHash == hash)
				{
					this.mPendingActivationTime = 0f;
					return this.mActiveInstruction.m_VirtualCamera;
				}
				if (this.PreviousStateIsValid && this.mPendingActivationTime != 0f && this.mPendingInstruction.m_FullHash == hash)
				{
					if (now - this.mPendingActivationTime > this.mPendingInstruction.m_ActivateAfter && (now - this.mActivationTime > this.mActiveInstruction.m_MinDuration || this.mPendingInstruction.m_VirtualCamera.Priority > this.mActiveInstruction.m_VirtualCamera.Priority))
					{
						this.mActiveInstruction = this.mPendingInstruction;
						this.mActivationTime = now;
						this.mPendingActivationTime = 0f;
					}
					return this.mActiveInstruction.m_VirtualCamera;
				}
			}
			this.mPendingActivationTime = 0f;
			if (!this.mInstructionDictionary.ContainsKey(hash))
			{
				if (this.mActivationTime != 0f)
				{
					return this.mActiveInstruction.m_VirtualCamera;
				}
				return defaultCam;
			}
			else
			{
				CinemachineStateDrivenCamera.Instruction newInstr = this.m_Instructions[this.mInstructionDictionary[hash]];
				if (newInstr.m_VirtualCamera == null)
				{
					newInstr.m_VirtualCamera = defaultCam;
				}
				if (!this.PreviousStateIsValid || this.mActivationTime <= 0f || (newInstr.m_ActivateAfter <= 0f && (now - this.mActivationTime >= this.mActiveInstruction.m_MinDuration || newInstr.m_VirtualCamera.Priority > this.mActiveInstruction.m_VirtualCamera.Priority)))
				{
					this.mActiveInstruction = newInstr;
					this.mActivationTime = now;
					return this.mActiveInstruction.m_VirtualCamera;
				}
				this.mPendingInstruction = newInstr;
				this.mPendingActivationTime = now;
				if (this.mActivationTime != 0f)
				{
					return this.mActiveInstruction.m_VirtualCamera;
				}
				return defaultCam;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00009F14 File Offset: 0x00008114
		private int GetClipHash(int hash, List<AnimatorClipInfo> clips)
		{
			int bestClip = -1;
			for (int i = 0; i < clips.Count; i++)
			{
				if (bestClip < 0 || clips[i].weight > clips[bestClip].weight)
				{
					bestClip = i;
				}
			}
			if (bestClip >= 0 && clips[bestClip].weight > 0f)
			{
				hash = this.LookupFakeHash(hash, clips[bestClip].clip);
			}
			return hash;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00009F90 File Offset: 0x00008190
		private CinemachineBlendDefinition LookupBlend(ICinemachineCamera fromKey, ICinemachineCamera toKey)
		{
			CinemachineBlendDefinition blend = this.m_DefaultBlend;
			if (this.m_CustomBlends != null)
			{
				string fromCameraName = ((fromKey != null) ? fromKey.Name : string.Empty);
				string toCameraName = ((toKey != null) ? toKey.Name : string.Empty);
				blend = this.m_CustomBlends.GetBlendForVirtualCameras(fromCameraName, toCameraName, blend);
			}
			if (CinemachineCore.GetBlendOverride != null)
			{
				blend = CinemachineCore.GetBlendOverride(fromKey, toKey, blend, this);
			}
			return blend;
		}

		// Token: 0x04000101 RID: 257
		[Tooltip("Default object for the camera children to look at (the aim target), if not specified in a child camera.  May be empty if all of the children define targets of their own.")]
		[NoSaveDuringPlay]
		[VcamTargetProperty]
		public Transform m_LookAt;

		// Token: 0x04000102 RID: 258
		[Tooltip("Default object for the camera children wants to move with (the body target), if not specified in a child camera.  May be empty if all of the children define targets of their own.")]
		[NoSaveDuringPlay]
		[VcamTargetProperty]
		public Transform m_Follow;

		// Token: 0x04000103 RID: 259
		[Space]
		[Tooltip("The state machine whose state changes will drive this camera's choice of active child")]
		[NoSaveDuringPlay]
		public Animator m_AnimatedTarget;

		// Token: 0x04000104 RID: 260
		[Tooltip("Which layer in the target state machine to observe")]
		[NoSaveDuringPlay]
		public int m_LayerIndex;

		// Token: 0x04000105 RID: 261
		[Tooltip("When enabled, the current child camera and blend will be indicated in the game window, for debugging")]
		public bool m_ShowDebugText;

		// Token: 0x04000106 RID: 262
		[SerializeField]
		[HideInInspector]
		[NoSaveDuringPlay]
		internal CinemachineVirtualCameraBase[] m_ChildCameras;

		// Token: 0x04000107 RID: 263
		[Tooltip("The set of instructions associating virtual cameras with states.  These instructions are used to choose the live child at any given moment")]
		public CinemachineStateDrivenCamera.Instruction[] m_Instructions;

		// Token: 0x04000108 RID: 264
		[CinemachineBlendDefinitionProperty]
		[Tooltip("The blend which is used if you don't explicitly define a blend between two Virtual Camera children")]
		public CinemachineBlendDefinition m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 0.5f);

		// Token: 0x04000109 RID: 265
		[Tooltip("This is the asset which contains custom settings for specific child blends")]
		public CinemachineBlenderSettings m_CustomBlends;

		// Token: 0x0400010A RID: 266
		[HideInInspector]
		[SerializeField]
		internal CinemachineStateDrivenCamera.ParentHash[] m_ParentHash;

		// Token: 0x0400010C RID: 268
		private ICinemachineCamera m_TransitioningFrom;

		// Token: 0x0400010D RID: 269
		private CameraState m_State = CameraState.Default;

		// Token: 0x0400010E RID: 270
		private Dictionary<AnimationClip, List<CinemachineStateDrivenCamera.HashPair>> mHashCache;

		// Token: 0x0400010F RID: 271
		private float mActivationTime;

		// Token: 0x04000110 RID: 272
		private CinemachineStateDrivenCamera.Instruction mActiveInstruction;

		// Token: 0x04000111 RID: 273
		private float mPendingActivationTime;

		// Token: 0x04000112 RID: 274
		private CinemachineStateDrivenCamera.Instruction mPendingInstruction;

		// Token: 0x04000113 RID: 275
		private CinemachineBlend mActiveBlend;

		// Token: 0x04000114 RID: 276
		private Dictionary<int, int> mInstructionDictionary;

		// Token: 0x04000115 RID: 277
		private Dictionary<int, int> mStateParentLookup;

		// Token: 0x04000116 RID: 278
		private List<AnimatorClipInfo> m_clipInfoList = new List<AnimatorClipInfo>();

		// Token: 0x02000036 RID: 54
		[Serializable]
		public struct Instruction
		{
			// Token: 0x04000117 RID: 279
			[Tooltip("The full hash of the animation state")]
			public int m_FullHash;

			// Token: 0x04000118 RID: 280
			[Tooltip("The virtual camera to activate when the animation state becomes active")]
			public CinemachineVirtualCameraBase m_VirtualCamera;

			// Token: 0x04000119 RID: 281
			[Tooltip("How long to wait (in seconds) before activating the virtual camera. This filters out very short state durations")]
			public float m_ActivateAfter;

			// Token: 0x0400011A RID: 282
			[Tooltip("The minimum length of time (in seconds) to keep a virtual camera active")]
			public float m_MinDuration;
		}

		// Token: 0x02000037 RID: 55
		[DocumentationSorting(DocumentationSortingAttribute.Level.Undoc)]
		[Serializable]
		internal struct ParentHash
		{
			// Token: 0x0600015B RID: 347 RVA: 0x0000A029 File Offset: 0x00008229
			public ParentHash(int h, int p)
			{
				this.m_Hash = h;
				this.m_ParentHash = p;
			}

			// Token: 0x0400011B RID: 283
			public int m_Hash;

			// Token: 0x0400011C RID: 284
			public int m_ParentHash;
		}

		// Token: 0x02000038 RID: 56
		private struct HashPair
		{
			// Token: 0x0400011D RID: 285
			public int parentHash;

			// Token: 0x0400011E RID: 286
			public int hash;
		}
	}
}
