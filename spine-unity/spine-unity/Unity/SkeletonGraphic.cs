using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Spine.Unity
{
	// Token: 0x0200002C RID: 44
	[ExecuteAlways]
	[RequireComponent(typeof(CanvasRenderer), typeof(RectTransform))]
	[DisallowMultipleComponent]
	[AddComponentMenu("Spine/SkeletonGraphic (Unity UI Canvas)")]
	[HelpURL("http://esotericsoftware.com/spine-unity#SkeletonGraphic-Component")]
	public class SkeletonGraphic : MaskableGraphic, ISkeletonComponent, ISpineComponent, IAnimationStateComponent, ISkeletonAnimation, IHasSkeletonDataAsset
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000819D File Offset: 0x0000639D
		public SkeletonDataAsset SkeletonDataAsset
		{
			get
			{
				return this.skeletonDataAsset;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000081A5 File Offset: 0x000063A5
		// (set) Token: 0x06000152 RID: 338 RVA: 0x000081AD File Offset: 0x000063AD
		public override Color color
		{
			get
			{
				return this.m_SkeletonColor;
			}
			set
			{
				this.m_SkeletonColor = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000081B6 File Offset: 0x000063B6
		public float MeshScale
		{
			get
			{
				return this.meshScale;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000081BE File Offset: 0x000063BE
		public Vector2 MeshOffset
		{
			get
			{
				return this.meshOffset;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000081C6 File Offset: 0x000063C6
		// (set) Token: 0x06000156 RID: 342 RVA: 0x000081CE File Offset: 0x000063CE
		public UpdateMode UpdateMode
		{
			get
			{
				return this.updateMode;
			}
			set
			{
				this.updateMode = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000081D7 File Offset: 0x000063D7
		public List<Transform> SeparatorParts
		{
			get
			{
				return this.separatorParts;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000081E0 File Offset: 0x000063E0
		public static SkeletonGraphic NewSkeletonGraphicGameObject(SkeletonDataAsset skeletonDataAsset, Transform parent, Material material)
		{
			SkeletonGraphic sg = SkeletonGraphic.AddSkeletonGraphicComponent(new GameObject("New Spine GameObject"), skeletonDataAsset, material);
			if (parent != null)
			{
				sg.transform.SetParent(parent, false);
			}
			return sg;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008218 File Offset: 0x00006418
		public static SkeletonGraphic AddSkeletonGraphicComponent(GameObject gameObject, SkeletonDataAsset skeletonDataAsset, Material material)
		{
			SkeletonGraphic skeletonGraphic = gameObject.AddComponent<SkeletonGraphic>();
			if (skeletonDataAsset != null)
			{
				skeletonGraphic.material = material;
				skeletonGraphic.skeletonDataAsset = skeletonDataAsset;
				skeletonGraphic.Initialize(false);
			}
			CanvasRenderer canvasRenderer = gameObject.GetComponent<CanvasRenderer>();
			if (canvasRenderer)
			{
				canvasRenderer.cullTransparentMesh = false;
			}
			return skeletonGraphic;
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600015A RID: 346 RVA: 0x00008264 File Offset: 0x00006464
		// (remove) Token: 0x0600015B RID: 347 RVA: 0x0000829C File Offset: 0x0000649C
		private event SkeletonGraphic.MeshAssignmentDelegateSingle assignMeshOverrideSingle;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600015C RID: 348 RVA: 0x000082D4 File Offset: 0x000064D4
		// (remove) Token: 0x0600015D RID: 349 RVA: 0x0000830C File Offset: 0x0000650C
		private event SkeletonGraphic.MeshAssignmentDelegateMultiple assignMeshOverrideMultiple;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600015E RID: 350 RVA: 0x00008341 File Offset: 0x00006541
		// (remove) Token: 0x0600015F RID: 351 RVA: 0x00008361 File Offset: 0x00006561
		public event SkeletonGraphic.MeshAssignmentDelegateSingle AssignMeshOverrideSingleRenderer
		{
			add
			{
				this.assignMeshOverrideSingle += value;
				if (this.disableMeshAssignmentOnOverride && this.assignMeshOverrideSingle != null)
				{
					this.Initialize(false);
				}
			}
			remove
			{
				this.assignMeshOverrideSingle -= value;
				if (this.disableMeshAssignmentOnOverride && this.assignMeshOverrideSingle == null)
				{
					this.Initialize(false);
				}
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000160 RID: 352 RVA: 0x00008381 File Offset: 0x00006581
		// (remove) Token: 0x06000161 RID: 353 RVA: 0x000083A1 File Offset: 0x000065A1
		public event SkeletonGraphic.MeshAssignmentDelegateMultiple AssignMeshOverrideMultipleRenderers
		{
			add
			{
				this.assignMeshOverrideMultiple += value;
				if (this.disableMeshAssignmentOnOverride && this.assignMeshOverrideMultiple != null)
				{
					this.Initialize(false);
				}
			}
			remove
			{
				this.assignMeshOverrideMultiple -= value;
				if (this.disableMeshAssignmentOnOverride && this.assignMeshOverrideMultiple == null)
				{
					this.Initialize(false);
				}
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000083C1 File Offset: 0x000065C1
		public Dictionary<Texture, Texture> CustomTextureOverride
		{
			get
			{
				return this.customTextureOverride;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000083C9 File Offset: 0x000065C9
		public Dictionary<Texture, Material> CustomMaterialOverride
		{
			get
			{
				return this.customMaterialOverride;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000083D1 File Offset: 0x000065D1
		// (set) Token: 0x06000165 RID: 357 RVA: 0x000083D9 File Offset: 0x000065D9
		public Texture OverrideTexture
		{
			get
			{
				return this.overrideTexture;
			}
			set
			{
				this.overrideTexture = value;
				base.canvasRenderer.SetTexture(this.mainTexture);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000083F3 File Offset: 0x000065F3
		public override Texture mainTexture
		{
			get
			{
				if (this.overrideTexture != null)
				{
					return this.overrideTexture;
				}
				return this.baseTexture;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008410 File Offset: 0x00006610
		protected override void Awake()
		{
			base.Awake();
			base.onCullStateChanged.AddListener(new UnityAction<bool>(this.OnCullStateChanged));
			this.SyncSubmeshGraphicsWithCanvasRenderers();
			if (!this.IsValid)
			{
				this.Initialize(false);
				if (this.IsValid)
				{
					this.Rebuild(CanvasUpdate.PreRender);
				}
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000845E File Offset: 0x0000665E
		protected override void OnDestroy()
		{
			this.Clear();
			base.OnDestroy();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000846C File Offset: 0x0000666C
		public override void Rebuild(CanvasUpdate update)
		{
			base.Rebuild(update);
			if (!this.IsValid)
			{
				return;
			}
			if (base.canvasRenderer.cull)
			{
				return;
			}
			if (update == CanvasUpdate.PreRender)
			{
				this.PrepareInstructionsAndRenderers(true);
				this.UpdateMeshToInstructions();
			}
			if (this.allowMultipleCanvasRenderers)
			{
				base.canvasRenderer.Clear();
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000084BC File Offset: 0x000066BC
		protected override void OnDisable()
		{
			base.OnDisable();
			foreach (CanvasRenderer canvasRenderer in this.canvasRenderers)
			{
				canvasRenderer.Clear();
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00008514 File Offset: 0x00006714
		public virtual void Update()
		{
			if (this.freeze || this.updateTiming != UpdateTiming.InUpdate)
			{
				return;
			}
			this.Update(this.unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008542 File Offset: 0x00006742
		protected virtual void FixedUpdate()
		{
			if (this.freeze || this.updateTiming != UpdateTiming.InFixedUpdate)
			{
				return;
			}
			this.Update(this.unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008570 File Offset: 0x00006770
		public virtual void Update(float deltaTime)
		{
			if (!this.IsValid)
			{
				return;
			}
			this.wasUpdatedAfterInit = true;
			if (this.updateMode < UpdateMode.OnlyAnimationStatus)
			{
				return;
			}
			this.UpdateAnimationStatus(deltaTime);
			if (this.updateMode == UpdateMode.OnlyAnimationStatus)
			{
				return;
			}
			this.ApplyAnimation();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000085A4 File Offset: 0x000067A4
		protected void SyncSubmeshGraphicsWithCanvasRenderers()
		{
			this.submeshGraphics.Clear();
			foreach (CanvasRenderer canvasRenderer in this.canvasRenderers)
			{
				SkeletonSubmeshGraphic submeshGraphic = canvasRenderer.GetComponent<SkeletonSubmeshGraphic>();
				if (submeshGraphic == null)
				{
					submeshGraphic = canvasRenderer.gameObject.AddComponent<SkeletonSubmeshGraphic>();
					submeshGraphic.maskable = base.maskable;
					submeshGraphic.raycastTarget = false;
				}
				this.submeshGraphics.Add(submeshGraphic);
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008638 File Offset: 0x00006838
		protected void UpdateAnimationStatus(float deltaTime)
		{
			deltaTime *= this.timeScale;
			this.state.Update(deltaTime);
			this.skeleton.Update(deltaTime);
			this.ApplyTransformMovementToPhysics();
			if (this.updateMode == UpdateMode.OnlyAnimationStatus)
			{
				this.state.ApplyEventTimelinesOnly(this.skeleton, false);
				return;
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000868C File Offset: 0x0000688C
		public virtual void ApplyTransformMovementToPhysics()
		{
			if (Application.isPlaying)
			{
				if (this.physicsPositionInheritanceFactor != Vector2.zero)
				{
					Vector2 position = this.GetPhysicsTransformPosition();
					Vector2 positionDelta = (position - this.lastPosition) / this.meshScale;
					positionDelta = base.transform.InverseTransformVector(positionDelta);
					if (this.physicsMovementRelativeTo != null)
					{
						positionDelta = this.physicsMovementRelativeTo.TransformVector(positionDelta);
					}
					positionDelta.x *= this.physicsPositionInheritanceFactor.x;
					positionDelta.y *= this.physicsPositionInheritanceFactor.y;
					this.skeleton.PhysicsTranslate(positionDelta.x, positionDelta.y);
					this.lastPosition = position;
				}
				if (this.physicsRotationInheritanceFactor != 0f)
				{
					float rotation = this.GetPhysicsTransformRotation();
					this.skeleton.PhysicsRotate(0f, 0f, this.physicsRotationInheritanceFactor * (rotation - this.lastRotation));
					this.lastRotation = rotation;
				}
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000879C File Offset: 0x0000699C
		protected Vector2 GetPhysicsTransformPosition()
		{
			if (this.physicsMovementRelativeTo == null)
			{
				return base.transform.position;
			}
			if (this.physicsMovementRelativeTo == base.transform.parent)
			{
				return base.transform.localPosition;
			}
			return this.physicsMovementRelativeTo.InverseTransformPoint(base.transform.position);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000880C File Offset: 0x00006A0C
		protected float GetPhysicsTransformRotation()
		{
			if (this.physicsMovementRelativeTo == null)
			{
				return base.transform.rotation.eulerAngles.z;
			}
			if (this.physicsMovementRelativeTo == base.transform.parent)
			{
				return base.transform.localRotation.eulerAngles.z;
			}
			return (Quaternion.Inverse(this.physicsMovementRelativeTo.rotation) * base.transform.rotation).eulerAngles.z;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000088A0 File Offset: 0x00006AA0
		public virtual void ApplyAnimation()
		{
			if (this.BeforeApply != null)
			{
				this.BeforeApply(this);
			}
			if (this.updateMode != UpdateMode.OnlyEventTimelines)
			{
				this.state.Apply(this.skeleton);
			}
			else
			{
				this.state.ApplyEventTimelinesOnly(this.skeleton, true);
			}
			this.AfterAnimationApplied();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000088F8 File Offset: 0x00006AF8
		public virtual void AfterAnimationApplied()
		{
			if (this.UpdateLocal != null)
			{
				this.UpdateLocal(this);
			}
			if (this.UpdateWorld == null)
			{
				this.UpdateWorldTransform(Skeleton.Physics.Update);
			}
			else
			{
				this.UpdateWorldTransform(Skeleton.Physics.Pose);
				this.UpdateWorld(this);
				this.UpdateWorldTransform(Skeleton.Physics.Update);
			}
			if (this.UpdateComplete != null)
			{
				this.UpdateComplete(this);
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008958 File Offset: 0x00006B58
		protected void UpdateWorldTransform(Skeleton.Physics physics)
		{
			this.skeleton.UpdateWorldTransform(physics);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008968 File Offset: 0x00006B68
		public void LateUpdate()
		{
			if (!this.IsValid)
			{
				return;
			}
			if (!this.wasUpdatedAfterInit)
			{
				this.Update(0f);
			}
			if (this.freeze)
			{
				return;
			}
			if (this.updateMode != UpdateMode.FullUpdate)
			{
				return;
			}
			if (this.updateTiming == UpdateTiming.InLateUpdate)
			{
				this.Update(this.unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime);
			}
			this.UpdateMesh();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000089CD File Offset: 0x00006BCD
		protected void OnCullStateChanged(bool culled)
		{
			if (culled)
			{
				this.OnBecameInvisible();
				return;
			}
			this.OnBecameVisible();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000089DF File Offset: 0x00006BDF
		public void OnBecameVisible()
		{
			this.updateMode = UpdateMode.FullUpdate;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000089E8 File Offset: 0x00006BE8
		public void OnBecameInvisible()
		{
			this.updateMode = this.updateWhenInvisible;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000089F8 File Offset: 0x00006BF8
		public void ReapplySeparatorSlotNames()
		{
			if (!this.IsValid)
			{
				return;
			}
			this.separatorSlots.Clear();
			int i = 0;
			int j = this.separatorSlotNames.Length;
			while (i < j)
			{
				string slotName = this.separatorSlotNames[i];
				if (!(slotName == ""))
				{
					Slot slot = this.skeleton.FindSlot(slotName);
					if (slot != null)
					{
						this.separatorSlots.Add(slot);
					}
				}
				i++;
			}
			this.UpdateSeparatorPartParents();
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00008A66 File Offset: 0x00006C66
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00008A75 File Offset: 0x00006C75
		public Skeleton Skeleton
		{
			get
			{
				this.Initialize(false);
				return this.skeleton;
			}
			set
			{
				this.skeleton = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00008A7E File Offset: 0x00006C7E
		public SkeletonData SkeletonData
		{
			get
			{
				this.Initialize(false);
				if (this.skeleton != null)
				{
					return this.skeleton.Data;
				}
				return null;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00008A9C File Offset: 0x00006C9C
		public bool IsValid
		{
			get
			{
				return this.skeleton != null;
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x0600017F RID: 383 RVA: 0x00008AA8 File Offset: 0x00006CA8
		// (remove) Token: 0x06000180 RID: 384 RVA: 0x00008AE0 File Offset: 0x00006CE0
		public event SkeletonGraphic.SkeletonRendererDelegate OnRebuild;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000181 RID: 385 RVA: 0x00008B18 File Offset: 0x00006D18
		// (remove) Token: 0x06000182 RID: 386 RVA: 0x00008B50 File Offset: 0x00006D50
		public event SkeletonGraphic.InstructionDelegate OnInstructionsPrepared;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000183 RID: 387 RVA: 0x00008B88 File Offset: 0x00006D88
		// (remove) Token: 0x06000184 RID: 388 RVA: 0x00008BC0 File Offset: 0x00006DC0
		public event SkeletonGraphic.SkeletonRendererDelegate OnMeshAndMaterialsUpdated;

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00008BF5 File Offset: 0x00006DF5
		public AnimationState AnimationState
		{
			get
			{
				this.Initialize(false);
				return this.state;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00008C04 File Offset: 0x00006E04
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00008C0C File Offset: 0x00006E0C
		public Vector2 PhysicsPositionInheritanceFactor
		{
			get
			{
				return this.physicsPositionInheritanceFactor;
			}
			set
			{
				if (this.physicsPositionInheritanceFactor == Vector2.zero && value != Vector2.zero)
				{
					this.ResetLastPosition();
				}
				this.physicsPositionInheritanceFactor = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00008C3A File Offset: 0x00006E3A
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00008C42 File Offset: 0x00006E42
		public float PhysicsRotationInheritanceFactor
		{
			get
			{
				return this.physicsRotationInheritanceFactor;
			}
			set
			{
				if (this.physicsRotationInheritanceFactor == 0f && value != 0f)
				{
					this.ResetLastRotation();
				}
				this.physicsRotationInheritanceFactor = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00008C66 File Offset: 0x00006E66
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00008C6E File Offset: 0x00006E6E
		public Transform PhysicsMovementRelativeTo
		{
			get
			{
				return this.physicsMovementRelativeTo;
			}
			set
			{
				this.physicsMovementRelativeTo = value;
				if (this.physicsPositionInheritanceFactor != Vector2.zero)
				{
					this.ResetLastPosition();
				}
				if (this.physicsRotationInheritanceFactor != 0f)
				{
					this.ResetLastRotation();
				}
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008CA2 File Offset: 0x00006EA2
		public void ResetLastPosition()
		{
			this.lastPosition = this.GetPhysicsTransformPosition();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008CB0 File Offset: 0x00006EB0
		public void ResetLastRotation()
		{
			this.lastRotation = this.GetPhysicsTransformRotation();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008CBE File Offset: 0x00006EBE
		public void ResetLastPositionAndRotation()
		{
			this.lastPosition = this.GetPhysicsTransformPosition();
			this.lastRotation = this.GetPhysicsTransformRotation();
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00008CD8 File Offset: 0x00006ED8
		public MeshGenerator MeshGenerator
		{
			get
			{
				return this.meshGenerator;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00008CE0 File Offset: 0x00006EE0
		public SkeletonClipping SkeletonClipping
		{
			get
			{
				return this.meshGenerator.SkeletonClipping;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00008CED File Offset: 0x00006EED
		public ExposedList<Mesh> MeshesMultipleCanvasRenderers
		{
			get
			{
				return this.meshes;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00008CF5 File Offset: 0x00006EF5
		public ExposedList<Material> MaterialsMultipleCanvasRenderers
		{
			get
			{
				return this.usedMaterials;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00008CFD File Offset: 0x00006EFD
		public ExposedList<Texture> TexturesMultipleCanvasRenderers
		{
			get
			{
				return this.usedTextures;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00008D05 File Offset: 0x00006F05
		public Mesh GetLastMesh()
		{
			return this.meshBuffers.GetCurrent().mesh;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00008D17 File Offset: 0x00006F17
		public bool MatchRectTransformWithBounds()
		{
			if (!this.wasUpdatedAfterInit)
			{
				this.Update(0f);
			}
			this.UpdateMesh();
			if (!this.allowMultipleCanvasRenderers)
			{
				return this.MatchRectTransformSingleRenderer();
			}
			return this.MatchRectTransformMultipleRenderers();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00008D48 File Offset: 0x00006F48
		protected bool MatchRectTransformSingleRenderer()
		{
			Mesh mesh = this.GetLastMesh();
			if (mesh == null)
			{
				return false;
			}
			if (mesh.vertexCount == 0 || mesh.bounds.size == Vector3.zero)
			{
				base.rectTransform.sizeDelta = new Vector2(50f, 50f);
				base.rectTransform.pivot = new Vector2(0.5f, 0.5f);
				return false;
			}
			mesh.RecalculateBounds();
			this.SetRectTransformBounds(mesh.bounds);
			return true;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00008DD4 File Offset: 0x00006FD4
		protected bool MatchRectTransformMultipleRenderers()
		{
			bool anyBoundsAdded = false;
			Bounds combinedBounds = default(Bounds);
			for (int i = 0; i < this.canvasRenderers.Count; i++)
			{
				if (this.canvasRenderers[i].gameObject.activeSelf)
				{
					Mesh mesh = this.meshes.Items[i];
					if (!(mesh == null) && mesh.vertexCount != 0)
					{
						mesh.RecalculateBounds();
						Bounds bounds = mesh.bounds;
						if (anyBoundsAdded)
						{
							combinedBounds.Encapsulate(bounds);
						}
						else
						{
							anyBoundsAdded = true;
							combinedBounds = bounds;
						}
					}
				}
			}
			if (!anyBoundsAdded || combinedBounds.size == Vector3.zero)
			{
				base.rectTransform.sizeDelta = new Vector2(50f, 50f);
				base.rectTransform.pivot = new Vector2(0.5f, 0.5f);
				return false;
			}
			this.SetRectTransformBounds(combinedBounds);
			return true;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008EB0 File Offset: 0x000070B0
		private void SetRectTransformBounds(Bounds combinedBounds)
		{
			Vector3 size = combinedBounds.size;
			Vector3 center = combinedBounds.center;
			Vector2 p = new Vector2(0.5f - center.x / size.x, 0.5f - center.y / size.y);
			SkeletonGraphic.SetRectTransformSize(this, size);
			base.rectTransform.pivot = p;
			foreach (Transform transform in this.separatorParts)
			{
				RectTransform separatorTransform = transform.GetComponent<RectTransform>();
				if (separatorTransform)
				{
					SkeletonGraphic.SetRectTransformSize(separatorTransform, size);
					separatorTransform.pivot = p;
				}
			}
			foreach (SkeletonSubmeshGraphic skeletonSubmeshGraphic in this.submeshGraphics)
			{
				SkeletonGraphic.SetRectTransformSize(skeletonSubmeshGraphic, size);
				skeletonSubmeshGraphic.rectTransform.pivot = p;
			}
			this.referenceSize = size;
			this.referenceScale *= this.layoutScale;
			this.layoutScale = 1f;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008FF4 File Offset: 0x000071F4
		public static void SetRectTransformSize(Graphic target, Vector2 size)
		{
			SkeletonGraphic.SetRectTransformSize(target.rectTransform, size);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00009004 File Offset: 0x00007204
		public static void SetRectTransformSize(RectTransform targetRectTransform, Vector2 size)
		{
			Vector2 parentSize = Vector2.zero;
			if (targetRectTransform.parent != null)
			{
				RectTransform parentTransform = targetRectTransform.parent.GetComponent<RectTransform>();
				if (parentTransform)
				{
					parentSize = parentTransform.rect.size;
				}
			}
			Vector2 anchorAreaSize = Vector2.Scale(targetRectTransform.anchorMax - targetRectTransform.anchorMin, parentSize);
			targetRectTransform.sizeDelta = size - anchorAreaSize;
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x0600019B RID: 411 RVA: 0x00009070 File Offset: 0x00007270
		// (remove) Token: 0x0600019C RID: 412 RVA: 0x000090A8 File Offset: 0x000072A8
		public event ISkeletonAnimationDelegate OnAnimationRebuild;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600019D RID: 413 RVA: 0x000090E0 File Offset: 0x000072E0
		// (remove) Token: 0x0600019E RID: 414 RVA: 0x00009118 File Offset: 0x00007318
		public event UpdateBonesDelegate BeforeApply;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600019F RID: 415 RVA: 0x00009150 File Offset: 0x00007350
		// (remove) Token: 0x060001A0 RID: 416 RVA: 0x00009188 File Offset: 0x00007388
		public event UpdateBonesDelegate UpdateLocal;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060001A1 RID: 417 RVA: 0x000091C0 File Offset: 0x000073C0
		// (remove) Token: 0x060001A2 RID: 418 RVA: 0x000091F8 File Offset: 0x000073F8
		public event UpdateBonesDelegate UpdateWorld;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060001A3 RID: 419 RVA: 0x00009230 File Offset: 0x00007430
		// (remove) Token: 0x060001A4 RID: 420 RVA: 0x00009268 File Offset: 0x00007468
		public event UpdateBonesDelegate UpdateComplete;

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000929D File Offset: 0x0000749D
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x000092A5 File Offset: 0x000074A5
		public UpdateTiming UpdateTiming
		{
			get
			{
				return this.updateTiming;
			}
			set
			{
				this.updateTiming = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000092AE File Offset: 0x000074AE
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x000092B6 File Offset: 0x000074B6
		public bool UnscaledTime
		{
			get
			{
				return this.unscaledTime;
			}
			set
			{
				this.unscaledTime = value;
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060001A9 RID: 425 RVA: 0x000092C0 File Offset: 0x000074C0
		// (remove) Token: 0x060001AA RID: 426 RVA: 0x000092F8 File Offset: 0x000074F8
		public event MeshGeneratorDelegate OnPostProcessVertices;

		// Token: 0x060001AB RID: 427 RVA: 0x00009330 File Offset: 0x00007530
		public void Clear()
		{
			this.skeleton = null;
			base.canvasRenderer.Clear();
			for (int i = 0; i < this.canvasRenderers.Count; i++)
			{
				this.canvasRenderers[i].Clear();
			}
			this.DestroyMeshes();
			this.usedMaterials.Clear(true);
			this.usedTextures.Clear(true);
			this.DisposeMeshBuffers();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000939C File Offset: 0x0000759C
		public void TrimRenderers()
		{
			List<CanvasRenderer> newList = new List<CanvasRenderer>();
			foreach (CanvasRenderer canvasRenderer in this.canvasRenderers)
			{
				if (canvasRenderer.gameObject.activeSelf)
				{
					newList.Add(canvasRenderer);
				}
				else if (Application.isEditor && !Application.isPlaying)
				{
					global::UnityEngine.Object.DestroyImmediate(canvasRenderer.gameObject);
				}
				else
				{
					global::UnityEngine.Object.Destroy(canvasRenderer.gameObject);
				}
			}
			this.canvasRenderers = newList;
			this.SyncSubmeshGraphicsWithCanvasRenderers();
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00009438 File Offset: 0x00007638
		public void Initialize(bool overwrite)
		{
			if (this.IsValid && !overwrite)
			{
				return;
			}
			if (this.skeletonDataAsset == null)
			{
				return;
			}
			SkeletonData skeletonData = this.skeletonDataAsset.GetSkeletonData(false);
			if (skeletonData == null)
			{
				return;
			}
			if (this.skeletonDataAsset.atlasAssets.Length == 0 || this.skeletonDataAsset.atlasAssets[0].MaterialCount <= 0)
			{
				return;
			}
			this.skeleton = new Skeleton(skeletonData)
			{
				ScaleX = (float)(this.initialFlipX ? (-1) : 1),
				ScaleY = (float)(this.initialFlipY ? (-1) : 1)
			};
			this.InitMeshBuffers();
			this.baseTexture = this.skeletonDataAsset.atlasAssets[0].PrimaryMaterial.mainTexture;
			base.canvasRenderer.SetTexture(this.mainTexture);
			this.ResetLastPositionAndRotation();
			if (!string.IsNullOrEmpty(this.initialSkinName))
			{
				this.skeleton.SetSkin(this.initialSkinName);
			}
			this.separatorSlots.Clear();
			for (int i = 0; i < this.separatorSlotNames.Length; i++)
			{
				this.separatorSlots.Add(this.skeleton.FindSlot(this.separatorSlotNames[i]));
			}
			if (this.OnRebuild != null)
			{
				this.OnRebuild(this);
			}
			this.wasUpdatedAfterInit = false;
			this.state = new AnimationState(this.skeletonDataAsset.GetAnimationStateData());
			if (this.state == null)
			{
				this.Clear();
				return;
			}
			if (!string.IsNullOrEmpty(this.startingAnimation))
			{
				Animation animationObject = this.skeletonDataAsset.GetSkeletonData(false).FindAnimation(this.startingAnimation);
				if (animationObject != null)
				{
					this.state.SetAnimation(0, animationObject, this.startingLoop);
				}
			}
			if (this.OnAnimationRebuild != null)
			{
				this.OnAnimationRebuild(this);
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000095EC File Offset: 0x000077EC
		public void PrepareInstructionsAndRenderers(bool isInRebuild = false)
		{
			if (!this.allowMultipleCanvasRenderers)
			{
				MeshGenerator.GenerateSingleSubmeshInstruction(this.currentInstructions, this.skeleton, null);
				if (this.canvasRenderers.Count > 0)
				{
					this.DisableUnusedCanvasRenderers(0, isInRebuild);
				}
				this.usedRenderersCount = 0;
			}
			else
			{
				MeshGenerator.GenerateSkeletonRendererInstruction(this.currentInstructions, this.skeleton, null, this.enableSeparatorSlots ? this.separatorSlots : null, this.enableSeparatorSlots && this.separatorSlots.Count > 0, false);
				int submeshCount = this.currentInstructions.submeshInstructions.Count;
				this.EnsureCanvasRendererCount(submeshCount);
				this.EnsureMeshesCount(submeshCount);
				this.EnsureUsedTexturesAndMaterialsCount(submeshCount);
				this.EnsureSeparatorPartCount();
				this.PrepareRendererGameObjects(this.currentInstructions, isInRebuild);
			}
			if (this.OnInstructionsPrepared != null)
			{
				this.OnInstructionsPrepared(this.currentInstructions);
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000096C1 File Offset: 0x000078C1
		public void UpdateMesh()
		{
			this.PrepareInstructionsAndRenderers(false);
			this.UpdateMeshToInstructions();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000096D0 File Offset: 0x000078D0
		public void UpdateMeshToInstructions()
		{
			if (!this.IsValid || this.currentInstructions.rawVertexCount < 0)
			{
				return;
			}
			this.skeleton.SetColor(this.color);
			if (!this.allowMultipleCanvasRenderers)
			{
				this.UpdateMeshSingleCanvasRenderer(this.currentInstructions);
			}
			else
			{
				this.UpdateMaterialsMultipleCanvasRenderers(this.currentInstructions);
				this.UpdateMeshMultipleCanvasRenderers(this.currentInstructions);
			}
			if (this.OnMeshAndMaterialsUpdated != null)
			{
				this.OnMeshAndMaterialsUpdated(this);
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00009747 File Offset: 0x00007947
		public bool HasMultipleSubmeshInstructions()
		{
			return this.IsValid && MeshGenerator.RequiresMultipleSubmeshesByDrawOrder(this.skeleton);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000975E File Offset: 0x0000795E
		protected void InitMeshBuffers()
		{
			if (this.meshBuffers != null)
			{
				this.meshBuffers.GetNext().Clear();
				this.meshBuffers.GetNext().Clear();
				return;
			}
			this.meshBuffers = new DoubleBuffered<MeshRendererBuffers.SmartMesh>();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009794 File Offset: 0x00007994
		protected void DisposeMeshBuffers()
		{
			if (this.meshBuffers != null)
			{
				this.meshBuffers.GetNext().Dispose();
				this.meshBuffers.GetNext().Dispose();
				this.meshBuffers = null;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000097C8 File Offset: 0x000079C8
		protected void UpdateMeshSingleCanvasRenderer(SkeletonRendererInstruction currentInstructions)
		{
			MeshRendererBuffers.SmartMesh smartMesh = this.meshBuffers.GetNext();
			bool updateTriangles = SkeletonRendererInstruction.GeometryNotEqual(currentInstructions, smartMesh.instructionUsed);
			this.meshGenerator.Begin();
			if (currentInstructions.hasActiveClipping && currentInstructions.submeshInstructions.Count > 0)
			{
				this.meshGenerator.AddSubmesh(currentInstructions.submeshInstructions.Items[0], updateTriangles);
			}
			else
			{
				this.meshGenerator.BuildMeshWithArrays(currentInstructions, updateTriangles);
			}
			this.meshScale = ((base.canvas == null) ? 100f : base.canvas.referencePixelsPerUnit);
			if (this.layoutScaleMode != SkeletonGraphic.LayoutMode.None)
			{
				this.meshScale *= this.referenceScale;
				this.layoutScale = this.GetLayoutScale(this.layoutScaleMode);
				this.meshScale *= this.layoutScale;
				this.meshOffset = this.pivotOffset * this.layoutScale;
			}
			else
			{
				this.meshOffset = this.pivotOffset;
			}
			if (this.meshOffset == Vector2.zero)
			{
				this.meshGenerator.ScaleVertexData(this.meshScale);
			}
			else
			{
				this.meshGenerator.ScaleAndOffsetVertexData(this.meshScale, this.meshOffset);
			}
			if (this.OnPostProcessVertices != null)
			{
				this.OnPostProcessVertices(this.meshGenerator.Buffers);
			}
			Mesh mesh = smartMesh.mesh;
			this.meshGenerator.FillVertexData(mesh);
			if (updateTriangles)
			{
				this.meshGenerator.FillTriangles(mesh);
			}
			this.meshGenerator.FillLateVertexData(mesh);
			smartMesh.instructionUsed.Set(currentInstructions);
			if (this.assignMeshOverrideSingle != null)
			{
				this.assignMeshOverrideSingle(mesh, base.canvasRenderer.GetMaterial(), this.mainTexture);
			}
			bool assignAtCanvasRenderer = this.assignMeshOverrideSingle == null || !this.disableMeshAssignmentOnOverride;
			if (assignAtCanvasRenderer)
			{
				base.canvasRenderer.SetMesh(mesh);
			}
			else
			{
				base.canvasRenderer.SetMesh(null);
			}
			bool assignTexture = false;
			if (currentInstructions.submeshInstructions.Count > 0)
			{
				Material material = currentInstructions.submeshInstructions.Items[0].material;
				if (material != null && this.baseTexture != material.mainTexture)
				{
					this.baseTexture = material.mainTexture;
					if (this.overrideTexture == null && assignAtCanvasRenderer)
					{
						assignTexture = true;
					}
				}
			}
			if (Application.isPlaying)
			{
				this.HandleOnDemandLoading();
			}
			if (assignTexture)
			{
				base.canvasRenderer.SetTexture(this.mainTexture);
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00009A40 File Offset: 0x00007C40
		protected void UpdateMaterialsMultipleCanvasRenderers(SkeletonRendererInstruction currentInstructions)
		{
			int submeshCount = currentInstructions.submeshInstructions.Count;
			bool useOriginalTextureAndMaterial = this.customMaterialOverride.Count == 0 && this.customTextureOverride.Count == 0;
			BlendModeMaterials blendModeMaterials = this.skeletonDataAsset.blendModeMaterials;
			bool hasBlendModeMaterials = blendModeMaterials.RequiresBlendModeMaterials;
			bool pmaVertexColors = this.meshGenerator.settings.pmaVertexColors;
			Material[] usedMaterialItems = this.usedMaterials.Items;
			Texture[] usedTextureItems = this.usedTextures.Items;
			for (int i = 0; i < submeshCount; i++)
			{
				Material submeshMaterial = currentInstructions.submeshInstructions.Items[i].material;
				if (useOriginalTextureAndMaterial)
				{
					if (submeshMaterial == null)
					{
						usedMaterialItems[i] = null;
						usedTextureItems[i] = null;
					}
					else
					{
						usedTextureItems[i] = submeshMaterial.mainTexture;
						if (!hasBlendModeMaterials)
						{
							usedMaterialItems[i] = this.materialForRendering;
						}
						else
						{
							BlendMode blendMode = blendModeMaterials.BlendModeForMaterial(submeshMaterial);
							Material usedMaterial = this.materialForRendering;
							if (blendMode == BlendMode.Additive && !pmaVertexColors && this.additiveMaterial)
							{
								usedMaterial = this.additiveMaterial;
							}
							else if (blendMode == BlendMode.Multiply && this.multiplyMaterial)
							{
								usedMaterial = this.multiplyMaterial;
							}
							else if (blendMode == BlendMode.Screen && this.screenMaterial)
							{
								usedMaterial = this.screenMaterial;
							}
							usedMaterialItems[i] = this.submeshGraphics[i].GetModifiedMaterial(usedMaterial);
						}
					}
				}
				else
				{
					Texture originalTexture = submeshMaterial.mainTexture;
					Material usedMaterial2;
					if (!this.customMaterialOverride.TryGetValue(originalTexture, out usedMaterial2))
					{
						usedMaterial2 = this.material;
					}
					Texture usedTexture;
					if (!this.customTextureOverride.TryGetValue(originalTexture, out usedTexture))
					{
						usedTexture = originalTexture;
					}
					usedMaterialItems[i] = this.submeshGraphics[i].GetModifiedMaterial(usedMaterial2);
					usedTextureItems[i] = usedTexture;
				}
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00009C00 File Offset: 0x00007E00
		protected void UpdateMeshMultipleCanvasRenderers(SkeletonRendererInstruction currentInstructions)
		{
			this.meshScale = ((base.canvas == null) ? 100f : base.canvas.referencePixelsPerUnit);
			if (this.layoutScaleMode != SkeletonGraphic.LayoutMode.None)
			{
				this.meshScale *= this.referenceScale;
				this.layoutScale = this.GetLayoutScale(this.layoutScaleMode);
				this.meshScale *= this.layoutScale;
				this.meshOffset = this.pivotOffset * this.layoutScale;
			}
			else
			{
				this.meshOffset = this.pivotOffset;
			}
			int submeshCount = currentInstructions.submeshInstructions.Count;
			Mesh[] meshesItems = this.meshes.Items;
			bool useOriginalTextureAndMaterial = this.customMaterialOverride.Count == 0 && this.customTextureOverride.Count == 0;
			BlendModeMaterials blendModeMaterials = this.skeletonDataAsset.blendModeMaterials;
			bool hasBlendModeMaterials = blendModeMaterials.RequiresBlendModeMaterials;
			bool mainCullTransparentMesh = base.canvasRenderer.cullTransparentMesh;
			bool pmaVertexColors = this.meshGenerator.settings.pmaVertexColors;
			Material[] usedMaterialItems = this.usedMaterials.Items;
			Texture[] usedTextureItems = this.usedTextures.Items;
			for (int i = 0; i < submeshCount; i++)
			{
				SubmeshInstruction submeshInstructionItem = currentInstructions.submeshInstructions.Items[i];
				this.meshGenerator.Begin();
				this.meshGenerator.AddSubmesh(submeshInstructionItem, true);
				Mesh targetMesh = meshesItems[i];
				if (this.meshOffset == Vector2.zero)
				{
					this.meshGenerator.ScaleVertexData(this.meshScale);
				}
				else
				{
					this.meshGenerator.ScaleAndOffsetVertexData(this.meshScale, this.meshOffset);
				}
				if (this.OnPostProcessVertices != null)
				{
					this.OnPostProcessVertices(this.meshGenerator.Buffers);
				}
				this.meshGenerator.FillVertexData(targetMesh);
				this.meshGenerator.FillTriangles(targetMesh);
				this.meshGenerator.FillLateVertexData(targetMesh);
				CanvasRenderer canvasRenderer = this.canvasRenderers[i];
				if (this.assignMeshOverrideSingle == null || !this.disableMeshAssignmentOnOverride)
				{
					canvasRenderer.SetMesh(targetMesh);
				}
				else
				{
					canvasRenderer.SetMesh(null);
				}
				SkeletonSubmeshGraphic skeletonSubmeshGraphic = this.submeshGraphics[i];
				if (useOriginalTextureAndMaterial && hasBlendModeMaterials)
				{
					bool allowCullTransparentMesh = true;
					BlendMode materialBlendMode = blendModeMaterials.BlendModeForMaterial(usedMaterialItems[i]);
					if ((materialBlendMode == BlendMode.Normal && submeshInstructionItem.hasPMAAdditiveSlot) || (materialBlendMode == BlendMode.Additive && pmaVertexColors))
					{
						allowCullTransparentMesh = false;
					}
					canvasRenderer.cullTransparentMesh = allowCullTransparentMesh && mainCullTransparentMesh;
				}
				canvasRenderer.materialCount = 1;
			}
			if (Application.isPlaying)
			{
				this.HandleOnDemandLoading();
			}
			if (this.assignMeshOverrideSingle == null || !this.disableMeshAssignmentOnOverride)
			{
				for (int j = 0; j < submeshCount; j++)
				{
					this.canvasRenderers[j].SetMaterial(usedMaterialItems[j], usedTextureItems[j]);
				}
			}
			if (this.assignMeshOverrideMultiple != null)
			{
				this.assignMeshOverrideMultiple(submeshCount, meshesItems, usedMaterialItems, usedTextureItems);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009ED8 File Offset: 0x000080D8
		private void HandleOnDemandLoading()
		{
			foreach (AtlasAssetBase atlasAsset in this.skeletonDataAsset.atlasAssets)
			{
				if (atlasAsset.TextureLoadingMode != AtlasAssetBase.LoadingMode.Normal)
				{
					atlasAsset.BeginCustomTextureLoading();
					if (!this.allowMultipleCanvasRenderers)
					{
						Texture loadedTexture = null;
						atlasAsset.RequireTextureLoaded(this.mainTexture, ref loadedTexture, null);
						if (loadedTexture)
						{
							this.baseTexture = loadedTexture;
						}
					}
					else
					{
						Texture[] textureItems = this.usedTextures.Items;
						int i = 0;
						int count = this.usedTextures.Count;
						while (i < count)
						{
							Texture loadedTexture2 = null;
							atlasAsset.RequireTextureLoaded(textureItems[i], ref loadedTexture2, null);
							if (loadedTexture2)
							{
								this.usedTextures.Items[i] = loadedTexture2;
							}
							i++;
						}
					}
					atlasAsset.EndCustomTextureLoading();
				}
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00009FA0 File Offset: 0x000081A0
		protected void EnsureCanvasRendererCount(int targetCount)
		{
			for (int i = this.canvasRenderers.Count; i < targetCount; i++)
			{
				GameObject gameObject = new GameObject(string.Format("Renderer{0}", i), new Type[] { typeof(RectTransform) });
				gameObject.transform.SetParent(base.transform, false);
				gameObject.transform.localPosition = Vector3.zero;
				CanvasRenderer canvasRenderer = gameObject.AddComponent<CanvasRenderer>();
				this.canvasRenderers.Add(canvasRenderer);
				SkeletonSubmeshGraphic submeshGraphic = gameObject.AddComponent<SkeletonSubmeshGraphic>();
				submeshGraphic.maskable = base.maskable;
				submeshGraphic.raycastTarget = false;
				submeshGraphic.rectTransform.pivot = base.rectTransform.pivot;
				submeshGraphic.rectTransform.anchorMin = Vector2.zero;
				submeshGraphic.rectTransform.anchorMax = Vector2.one;
				submeshGraphic.rectTransform.sizeDelta = Vector2.zero;
				this.submeshGraphics.Add(submeshGraphic);
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000A094 File Offset: 0x00008294
		protected void PrepareRendererGameObjects(SkeletonRendererInstruction currentInstructions, bool isInRebuild = false)
		{
			int submeshCount = currentInstructions.submeshInstructions.Count;
			this.DisableUnusedCanvasRenderers(submeshCount, isInRebuild);
			Transform parent = ((this.separatorParts.Count == 0) ? base.transform : this.separatorParts[0]);
			if (this.updateSeparatorPartLocation)
			{
				for (int p = 0; p < this.separatorParts.Count; p++)
				{
					Transform separatorPart = this.separatorParts[p];
					if (!(separatorPart == null))
					{
						separatorPart.position = base.transform.position;
						separatorPart.rotation = base.transform.rotation;
					}
				}
			}
			if (this.updateSeparatorPartScale)
			{
				Vector3 targetScale = base.transform.lossyScale;
				for (int p2 = 0; p2 < this.separatorParts.Count; p2++)
				{
					Transform separatorPart2 = this.separatorParts[p2];
					if (!(separatorPart2 == null))
					{
						Transform partParent = separatorPart2.parent;
						Vector3 parentScale = ((partParent == null) ? Vector3.one : partParent.lossyScale);
						separatorPart2.localScale = new Vector3((parentScale.x == 0f) ? 1f : (targetScale.x / parentScale.x), (parentScale.y == 0f) ? 1f : (targetScale.y / parentScale.y), (parentScale.z == 0f) ? 1f : (targetScale.z / parentScale.z));
					}
				}
			}
			int separatorSlotGroupIndex = 0;
			int targetSiblingIndex = 0;
			for (int i = 0; i < submeshCount; i++)
			{
				CanvasRenderer canvasRenderer = this.canvasRenderers[i];
				if (canvasRenderer != null)
				{
					if (i >= this.usedRenderersCount)
					{
						canvasRenderer.gameObject.SetActive(true);
					}
					if (canvasRenderer.transform.parent != parent.transform && !isInRebuild)
					{
						canvasRenderer.transform.SetParent(parent.transform, false);
					}
					canvasRenderer.transform.SetSiblingIndex(targetSiblingIndex++);
				}
				SkeletonSubmeshGraphic submeshGraphic = this.submeshGraphics[i];
				if (submeshGraphic != null)
				{
					RectTransform rectTransform = submeshGraphic.rectTransform;
					rectTransform.localPosition = Vector3.zero;
					rectTransform.pivot = base.rectTransform.pivot;
					rectTransform.anchorMin = Vector2.zero;
					rectTransform.anchorMax = Vector2.one;
					rectTransform.sizeDelta = Vector2.zero;
				}
				if (currentInstructions.submeshInstructions.Items[i].forceSeparate)
				{
					targetSiblingIndex = 0;
					parent = this.separatorParts[++separatorSlotGroupIndex];
				}
			}
			this.usedRenderersCount = submeshCount;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000A340 File Offset: 0x00008540
		protected void DisableUnusedCanvasRenderers(int usedCount, bool isInRebuild = false)
		{
			for (int i = usedCount; i < this.canvasRenderers.Count; i++)
			{
				this.canvasRenderers[i].Clear();
				if (!isInRebuild)
				{
					this.canvasRenderers[i].gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000A390 File Offset: 0x00008590
		protected void EnsureMeshesCount(int targetCount)
		{
			int count = this.meshes.Count;
			this.meshes.EnsureCapacity(targetCount);
			for (int i = count; i < targetCount; i++)
			{
				this.meshes.Add(SpineMesh.NewSkeletonMesh());
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000A3D0 File Offset: 0x000085D0
		protected void EnsureUsedTexturesAndMaterialsCount(int targetCount)
		{
			int count = this.usedMaterials.Count;
			this.usedMaterials.EnsureCapacity(targetCount);
			this.usedTextures.EnsureCapacity(targetCount);
			for (int i = count; i < targetCount; i++)
			{
				this.usedMaterials.Add(null);
				this.usedTextures.Add(null);
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000A424 File Offset: 0x00008624
		protected void DestroyMeshes()
		{
			foreach (Mesh mesh in this.meshes)
			{
				global::UnityEngine.Object.Destroy(mesh);
			}
			this.meshes.Clear(true);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000A480 File Offset: 0x00008680
		protected void EnsureSeparatorPartCount()
		{
			int targetCount = this.separatorSlots.Count + 1;
			if (targetCount == 1)
			{
				return;
			}
			for (int i = this.separatorParts.Count; i < targetCount; i++)
			{
				GameObject go = new GameObject(string.Format("{0}[{1}]", "Part", i), new Type[] { typeof(RectTransform) });
				go.transform.SetParent(base.transform, false);
				RectTransform component = go.transform.GetComponent<RectTransform>();
				component.localPosition = Vector3.zero;
				component.pivot = base.rectTransform.pivot;
				component.anchorMin = Vector2.zero;
				component.anchorMax = Vector2.one;
				component.sizeDelta = Vector2.zero;
				this.separatorParts.Add(go.transform);
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000A554 File Offset: 0x00008754
		protected void UpdateSeparatorPartParents()
		{
			int usedCount = this.separatorSlots.Count + 1;
			if (usedCount == 1)
			{
				usedCount = 0;
				for (int i = 0; i < this.canvasRenderers.Count; i++)
				{
					CanvasRenderer canvasRenderer = this.canvasRenderers[i];
					if (canvasRenderer.transform.parent.name.Contains("Part"))
					{
						canvasRenderer.transform.SetParent(base.transform, false);
						canvasRenderer.transform.localPosition = Vector3.zero;
					}
				}
			}
			for (int j = 0; j < this.separatorParts.Count; j++)
			{
				bool isUsed = j < usedCount;
				this.separatorParts[j].gameObject.SetActive(isUsed);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000A60C File Offset: 0x0000880C
		protected float GetLayoutScale(SkeletonGraphic.LayoutMode mode)
		{
			Vector2 currentSize = this.GetCurrentRectSize();
			mode = this.GetEffectiveLayoutMode(mode);
			if (mode == SkeletonGraphic.LayoutMode.WidthControlsHeight)
			{
				return currentSize.x / this.referenceSize.x;
			}
			if (mode == SkeletonGraphic.LayoutMode.HeightControlsWidth)
			{
				return currentSize.y / this.referenceSize.y;
			}
			return 1f;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000A65C File Offset: 0x0000885C
		protected SkeletonGraphic.LayoutMode GetEffectiveLayoutMode(SkeletonGraphic.LayoutMode mode)
		{
			Vector2 currentSize = this.GetCurrentRectSize();
			float referenceAspect = this.referenceSize.x / this.referenceSize.y;
			float frameAspect = currentSize.x / currentSize.y;
			if (mode == SkeletonGraphic.LayoutMode.FitInParent)
			{
				mode = ((frameAspect > referenceAspect) ? SkeletonGraphic.LayoutMode.HeightControlsWidth : SkeletonGraphic.LayoutMode.WidthControlsHeight);
			}
			else if (mode == SkeletonGraphic.LayoutMode.EnvelopeParent)
			{
				mode = ((frameAspect > referenceAspect) ? SkeletonGraphic.LayoutMode.WidthControlsHeight : SkeletonGraphic.LayoutMode.HeightControlsWidth);
			}
			return mode;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000A6B8 File Offset: 0x000088B8
		private Vector2 GetCurrentRectSize()
		{
			return base.rectTransform.rect.size;
		}

		// Token: 0x040000D3 RID: 211
		public SkeletonDataAsset skeletonDataAsset;

		// Token: 0x040000D4 RID: 212
		public Material additiveMaterial;

		// Token: 0x040000D5 RID: 213
		public Material multiplyMaterial;

		// Token: 0x040000D6 RID: 214
		public Material screenMaterial;

		// Token: 0x040000D7 RID: 215
		[FormerlySerializedAs("m_Color")]
		[SerializeField]
		protected Color m_SkeletonColor = Color.white;

		// Token: 0x040000D8 RID: 216
		[SpineSkin("", "skeletonDataAsset", false, false, true, false)]
		public string initialSkinName;

		// Token: 0x040000D9 RID: 217
		public bool initialFlipX;

		// Token: 0x040000DA RID: 218
		public bool initialFlipY;

		// Token: 0x040000DB RID: 219
		[SpineAnimation("", "skeletonDataAsset", true, false, false)]
		public string startingAnimation;

		// Token: 0x040000DC RID: 220
		public bool startingLoop;

		// Token: 0x040000DD RID: 221
		public float timeScale = 1f;

		// Token: 0x040000DE RID: 222
		public bool freeze;

		// Token: 0x040000DF RID: 223
		protected float meshScale = 1f;

		// Token: 0x040000E0 RID: 224
		protected Vector2 meshOffset = Vector2.zero;

		// Token: 0x040000E1 RID: 225
		public SkeletonGraphic.LayoutMode layoutScaleMode;

		// Token: 0x040000E2 RID: 226
		[SerializeField]
		protected Vector2 referenceSize = Vector2.one;

		// Token: 0x040000E3 RID: 227
		[SerializeField]
		protected Vector2 pivotOffset = Vector2.zero;

		// Token: 0x040000E4 RID: 228
		[SerializeField]
		protected float referenceScale = 1f;

		// Token: 0x040000E5 RID: 229
		[SerializeField]
		protected float layoutScale = 1f;

		// Token: 0x040000E6 RID: 230
		protected const bool EditReferenceRect = false;

		// Token: 0x040000E7 RID: 231
		protected UpdateMode updateMode = UpdateMode.FullUpdate;

		// Token: 0x040000E8 RID: 232
		public UpdateMode updateWhenInvisible = UpdateMode.FullUpdate;

		// Token: 0x040000E9 RID: 233
		public bool allowMultipleCanvasRenderers;

		// Token: 0x040000EA RID: 234
		public List<CanvasRenderer> canvasRenderers = new List<CanvasRenderer>();

		// Token: 0x040000EB RID: 235
		protected List<SkeletonSubmeshGraphic> submeshGraphics = new List<SkeletonSubmeshGraphic>();

		// Token: 0x040000EC RID: 236
		protected int usedRenderersCount;

		// Token: 0x040000ED RID: 237
		public const string SeparatorPartGameObjectName = "Part";

		// Token: 0x040000EE RID: 238
		[SerializeField]
		[SpineSlot("", "", false, true, false)]
		protected string[] separatorSlotNames = new string[0];

		// Token: 0x040000EF RID: 239
		[NonSerialized]
		public readonly List<Slot> separatorSlots = new List<Slot>();

		// Token: 0x040000F0 RID: 240
		public bool enableSeparatorSlots;

		// Token: 0x040000F1 RID: 241
		[SerializeField]
		protected List<Transform> separatorParts = new List<Transform>();

		// Token: 0x040000F2 RID: 242
		public bool updateSeparatorPartLocation = true;

		// Token: 0x040000F3 RID: 243
		public bool updateSeparatorPartScale;

		// Token: 0x040000F4 RID: 244
		private bool wasUpdatedAfterInit = true;

		// Token: 0x040000F5 RID: 245
		private Texture baseTexture;

		// Token: 0x040000F6 RID: 246
		public bool disableMeshAssignmentOnOverride = true;

		// Token: 0x040000F9 RID: 249
		[NonSerialized]
		private readonly Dictionary<Texture, Texture> customTextureOverride = new Dictionary<Texture, Texture>();

		// Token: 0x040000FA RID: 250
		[NonSerialized]
		private readonly Dictionary<Texture, Material> customMaterialOverride = new Dictionary<Texture, Material>();

		// Token: 0x040000FB RID: 251
		private Texture overrideTexture;

		// Token: 0x040000FC RID: 252
		protected Skeleton skeleton;

		// Token: 0x04000100 RID: 256
		protected AnimationState state;

		// Token: 0x04000101 RID: 257
		[SerializeField]
		protected Vector2 physicsPositionInheritanceFactor = Vector2.one;

		// Token: 0x04000102 RID: 258
		[SerializeField]
		protected float physicsRotationInheritanceFactor = 1f;

		// Token: 0x04000103 RID: 259
		[SerializeField]
		protected Transform physicsMovementRelativeTo;

		// Token: 0x04000104 RID: 260
		protected Vector2 lastPosition;

		// Token: 0x04000105 RID: 261
		protected float lastRotation;

		// Token: 0x04000106 RID: 262
		[SerializeField]
		protected MeshGenerator meshGenerator = new MeshGenerator();

		// Token: 0x04000107 RID: 263
		private DoubleBuffered<MeshRendererBuffers.SmartMesh> meshBuffers;

		// Token: 0x04000108 RID: 264
		private SkeletonRendererInstruction currentInstructions = new SkeletonRendererInstruction();

		// Token: 0x04000109 RID: 265
		private readonly ExposedList<Mesh> meshes = new ExposedList<Mesh>();

		// Token: 0x0400010A RID: 266
		private readonly ExposedList<Material> usedMaterials = new ExposedList<Material>();

		// Token: 0x0400010B RID: 267
		private readonly ExposedList<Texture> usedTextures = new ExposedList<Texture>();

		// Token: 0x04000111 RID: 273
		[SerializeField]
		protected UpdateTiming updateTiming = UpdateTiming.InUpdate;

		// Token: 0x04000112 RID: 274
		[SerializeField]
		protected bool unscaledTime;

		// Token: 0x0200002D RID: 45
		public enum LayoutMode
		{
			// Token: 0x04000115 RID: 277
			None,
			// Token: 0x04000116 RID: 278
			WidthControlsHeight,
			// Token: 0x04000117 RID: 279
			HeightControlsWidth,
			// Token: 0x04000118 RID: 280
			FitInParent,
			// Token: 0x04000119 RID: 281
			EnvelopeParent
		}

		// Token: 0x0200002E RID: 46
		// (Invoke) Token: 0x060001C5 RID: 453
		public delegate void MeshAssignmentDelegateSingle(Mesh mesh, Material graphicMaterial, Texture texture);

		// Token: 0x0200002F RID: 47
		// (Invoke) Token: 0x060001C9 RID: 457
		public delegate void MeshAssignmentDelegateMultiple(int meshCount, Mesh[] meshes, Material[] graphicMaterials, Texture[] textures);

		// Token: 0x02000030 RID: 48
		// (Invoke) Token: 0x060001CD RID: 461
		public delegate void SkeletonRendererDelegate(SkeletonGraphic skeletonGraphic);

		// Token: 0x02000031 RID: 49
		// (Invoke) Token: 0x060001D1 RID: 465
		public delegate void InstructionDelegate(SkeletonRendererInstruction instruction);
	}
}
