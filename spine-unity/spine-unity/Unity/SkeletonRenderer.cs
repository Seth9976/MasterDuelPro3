using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace Spine.Unity
{
	// Token: 0x0200003C RID: 60
	[ExecuteAlways]
	[RequireComponent(typeof(MeshRenderer))]
	[DisallowMultipleComponent]
	[HelpURL("http://esotericsoftware.com/spine-unity#SkeletonRenderer-Component")]
	public class SkeletonRenderer : MonoBehaviour, ISkeletonComponent, ISpineComponent, IHasSkeletonDataAsset
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000C3FC File Offset: 0x0000A5FC
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000C404 File Offset: 0x0000A604
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

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06000238 RID: 568 RVA: 0x0000C410 File Offset: 0x0000A610
		// (remove) Token: 0x06000239 RID: 569 RVA: 0x0000C448 File Offset: 0x0000A648
		private event SkeletonRenderer.InstructionDelegate generateMeshOverride;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x0600023A RID: 570 RVA: 0x0000C480 File Offset: 0x0000A680
		// (remove) Token: 0x0600023B RID: 571 RVA: 0x0000C4CC File Offset: 0x0000A6CC
		public event SkeletonRenderer.InstructionDelegate GenerateMeshOverride
		{
			add
			{
				this.generateMeshOverride += value;
				if (this.disableRenderingOnOverride && this.generateMeshOverride != null)
				{
					this.Initialize(false, false);
					if (this.meshRenderer)
					{
						this.meshRenderer.enabled = false;
					}
					this.updateMode = UpdateMode.FullUpdate;
				}
			}
			remove
			{
				this.generateMeshOverride -= value;
				if (this.disableRenderingOnOverride && this.generateMeshOverride == null)
				{
					this.Initialize(false, false);
					if (this.meshRenderer)
					{
						this.meshRenderer.enabled = true;
					}
				}
			}
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x0600023C RID: 572 RVA: 0x0000C508 File Offset: 0x0000A708
		// (remove) Token: 0x0600023D RID: 573 RVA: 0x0000C540 File Offset: 0x0000A740
		public event MeshGeneratorDelegate OnPostProcessVertices;

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000C575 File Offset: 0x0000A775
		public Dictionary<Material, Material> CustomMaterialOverride
		{
			get
			{
				return this.customMaterialOverride;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000C57D File Offset: 0x0000A77D
		public Dictionary<Slot, Material> CustomSlotMaterials
		{
			get
			{
				return this.customSlotMaterials;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000C585 File Offset: 0x0000A785
		public SkeletonClipping SkeletonClipping
		{
			get
			{
				return this.meshGenerator.SkeletonClipping;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000C592 File Offset: 0x0000A792
		public Skeleton Skeleton
		{
			get
			{
				this.Initialize(false, false);
				return this.skeleton;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000C5A2 File Offset: 0x0000A7A2
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000C5AA File Offset: 0x0000A7AA
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

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
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

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000C604 File Offset: 0x0000A804
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000C60C File Offset: 0x0000A80C
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

		// Token: 0x06000248 RID: 584 RVA: 0x0000C640 File Offset: 0x0000A840
		public void ResetLastPosition()
		{
			this.lastPosition = this.GetPhysicsTransformPosition();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000C64E File Offset: 0x0000A84E
		public void ResetLastRotation()
		{
			this.lastRotation = this.GetPhysicsTransformRotation();
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000C65C File Offset: 0x0000A85C
		public void ResetLastPositionAndRotation()
		{
			this.lastPosition = this.GetPhysicsTransformPosition();
			this.lastRotation = this.GetPhysicsTransformRotation();
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x0600024B RID: 587 RVA: 0x0000C678 File Offset: 0x0000A878
		// (remove) Token: 0x0600024C RID: 588 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
		public event SkeletonRenderer.SkeletonRendererDelegate OnRebuild;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x0600024D RID: 589 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
		// (remove) Token: 0x0600024E RID: 590 RVA: 0x0000C720 File Offset: 0x0000A920
		public event SkeletonRenderer.SkeletonRendererDelegate OnMeshAndMaterialsUpdated;

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000C755 File Offset: 0x0000A955
		public SkeletonDataAsset SkeletonDataAsset
		{
			get
			{
				return this.skeletonDataAsset;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000C75D File Offset: 0x0000A95D
		public static T NewSpineGameObject<T>(SkeletonDataAsset skeletonDataAsset, bool quiet = false) where T : SkeletonRenderer
		{
			return SkeletonRenderer.AddSpineComponent<T>(new GameObject("New Spine GameObject"), skeletonDataAsset, quiet);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000C770 File Offset: 0x0000A970
		public static T AddSpineComponent<T>(GameObject gameObject, SkeletonDataAsset skeletonDataAsset, bool quiet = false) where T : SkeletonRenderer
		{
			T c = gameObject.AddComponent<T>();
			if (skeletonDataAsset != null)
			{
				c.skeletonDataAsset = skeletonDataAsset;
				c.Initialize(false, quiet);
			}
			return c;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		public void SetMeshSettings(MeshGenerator.Settings settings)
		{
			this.calculateTangents = settings.calculateTangents;
			this.immutableTriangles = settings.immutableTriangles;
			this.pmaVertexColors = settings.pmaVertexColors;
			this.tintBlack = settings.tintBlack;
			this.useClipping = settings.useClipping;
			this.zSpacing = settings.zSpacing;
			this.meshGenerator.settings = settings;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000C809 File Offset: 0x0000AA09
		public virtual void Awake()
		{
			this.Initialize(false, false);
			if (this.generateMeshOverride == null || !this.disableRenderingOnOverride)
			{
				this.updateMode = this.updateWhenInvisible;
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000C82F File Offset: 0x0000AA2F
		private void OnDisable()
		{
			if (this.clearStateOnDisable && this.valid)
			{
				this.ClearState();
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000C847 File Offset: 0x0000AA47
		private void OnDestroy()
		{
			this.rendererBuffers.Dispose();
			this.valid = false;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000C85C File Offset: 0x0000AA5C
		public virtual void ClearState()
		{
			MeshFilter meshFilter = base.GetComponent<MeshFilter>();
			if (meshFilter != null)
			{
				meshFilter.sharedMesh = null;
			}
			this.currentInstructions.Clear();
			if (this.skeleton != null)
			{
				this.skeleton.SetToSetupPose();
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000C89E File Offset: 0x0000AA9E
		public void EnsureMeshGeneratorCapacity(int minimumVertexCount)
		{
			this.meshGenerator.EnsureVertexCapacity(minimumVertexCount, false, false, false);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000C8B0 File Offset: 0x0000AAB0
		public virtual void Initialize(bool overwrite, bool quiet = false)
		{
			if (this.valid && !overwrite)
			{
				return;
			}
			this.currentInstructions.Clear();
			this.rendererBuffers.Clear();
			this.meshGenerator.Begin();
			this.skeleton = null;
			this.valid = false;
			if (this.skeletonDataAsset == null)
			{
				return;
			}
			SkeletonData skeletonData = this.skeletonDataAsset.GetSkeletonData(quiet);
			if (skeletonData == null)
			{
				return;
			}
			this.valid = true;
			this.meshFilter = base.GetComponent<MeshFilter>();
			if (this.meshFilter == null)
			{
				this.meshFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			this.meshRenderer = base.GetComponent<MeshRenderer>();
			this.rendererBuffers.Initialize();
			this.skeleton = new Skeleton(skeletonData)
			{
				ScaleX = (float)(this.initialFlipX ? (-1) : 1),
				ScaleY = (float)(this.initialFlipY ? (-1) : 1)
			};
			this.ResetLastPositionAndRotation();
			if (!string.IsNullOrEmpty(this.initialSkinName) && !string.Equals(this.initialSkinName, "default", StringComparison.Ordinal))
			{
				this.skeleton.SetSkin(this.initialSkinName);
			}
			this.separatorSlots.Clear();
			for (int i = 0; i < this.separatorSlotNames.Length; i++)
			{
				this.separatorSlots.Add(this.skeleton.FindSlot(this.separatorSlotNames[i]));
			}
			UpdateMode updateModeSaved = this.updateMode;
			this.updateMode = UpdateMode.FullUpdate;
			this.UpdateWorldTransform(Skeleton.Physics.Update);
			this.LateUpdate();
			this.updateMode = updateModeSaved;
			if (this.OnRebuild != null)
			{
				this.OnRebuild(this);
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000CA3C File Offset: 0x0000AC3C
		public virtual void ApplyTransformMovementToPhysics()
		{
			if (Application.isPlaying)
			{
				if (this.physicsPositionInheritanceFactor != Vector2.zero)
				{
					Vector3 position = this.GetPhysicsTransformPosition();
					Vector3 positionDelta = position - this.lastPosition;
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

		// Token: 0x0600025A RID: 602 RVA: 0x0000CB30 File Offset: 0x0000AD30
		protected Vector3 GetPhysicsTransformPosition()
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

		// Token: 0x0600025B RID: 603 RVA: 0x0000CB94 File Offset: 0x0000AD94
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

		// Token: 0x0600025C RID: 604 RVA: 0x0000CC26 File Offset: 0x0000AE26
		protected virtual void UpdateWorldTransform(Skeleton.Physics physics)
		{
			this.skeleton.UpdateWorldTransform(physics);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000CC34 File Offset: 0x0000AE34
		public virtual void LateUpdate()
		{
			if (!this.valid)
			{
				return;
			}
			if (this.updateMode != UpdateMode.FullUpdate)
			{
				return;
			}
			this.LateUpdateMesh();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000CC50 File Offset: 0x0000AE50
		public virtual void LateUpdateMesh()
		{
			bool doMeshOverride = this.generateMeshOverride != null;
			if ((!this.meshRenderer || !this.meshRenderer.enabled) && !doMeshOverride)
			{
				return;
			}
			SkeletonRendererInstruction currentInstructions = this.currentInstructions;
			ExposedList<SubmeshInstruction> workingSubmeshInstructions = currentInstructions.submeshInstructions;
			MeshRendererBuffers.SmartMesh currentSmartMesh = this.rendererBuffers.GetNextMesh();
			bool updateTriangles;
			if (this.singleSubmesh)
			{
				MeshGenerator.GenerateSingleSubmeshInstruction(currentInstructions, this.skeleton, this.skeletonDataAsset.atlasAssets[0].PrimaryMaterial);
				if (this.customMaterialOverride.Count > 0)
				{
					MeshGenerator.TryReplaceMaterials(workingSubmeshInstructions, this.customMaterialOverride);
				}
				this.meshGenerator.settings = new MeshGenerator.Settings
				{
					pmaVertexColors = this.pmaVertexColors,
					zSpacing = this.zSpacing,
					useClipping = this.useClipping,
					tintBlack = this.tintBlack,
					calculateTangents = this.calculateTangents,
					addNormals = this.addNormals
				};
				this.meshGenerator.Begin();
				updateTriangles = SkeletonRendererInstruction.GeometryNotEqual(currentInstructions, currentSmartMesh.instructionUsed);
				if (currentInstructions.hasActiveClipping)
				{
					this.meshGenerator.AddSubmesh(workingSubmeshInstructions.Items[0], updateTriangles);
				}
				else
				{
					this.meshGenerator.BuildMeshWithArrays(currentInstructions, updateTriangles);
				}
			}
			else
			{
				MeshGenerator.GenerateSkeletonRendererInstruction(currentInstructions, this.skeleton, this.customSlotMaterials, this.separatorSlots, doMeshOverride, this.immutableTriangles);
				if (this.customMaterialOverride.Count > 0)
				{
					MeshGenerator.TryReplaceMaterials(workingSubmeshInstructions, this.customMaterialOverride);
				}
				if (doMeshOverride)
				{
					this.generateMeshOverride(currentInstructions);
					if (this.disableRenderingOnOverride)
					{
						return;
					}
				}
				updateTriangles = SkeletonRendererInstruction.GeometryNotEqual(currentInstructions, currentSmartMesh.instructionUsed);
				this.meshGenerator.settings = new MeshGenerator.Settings
				{
					pmaVertexColors = this.pmaVertexColors,
					zSpacing = this.zSpacing,
					useClipping = this.useClipping,
					tintBlack = this.tintBlack,
					calculateTangents = this.calculateTangents,
					addNormals = this.addNormals
				};
				this.meshGenerator.Begin();
				if (currentInstructions.hasActiveClipping)
				{
					this.meshGenerator.BuildMesh(currentInstructions, updateTriangles);
				}
				else
				{
					this.meshGenerator.BuildMeshWithArrays(currentInstructions, updateTriangles);
				}
			}
			if (this.OnPostProcessVertices != null)
			{
				this.OnPostProcessVertices(this.meshGenerator.Buffers);
			}
			Mesh currentMesh = currentSmartMesh.mesh;
			this.meshGenerator.FillVertexData(currentMesh);
			this.rendererBuffers.UpdateSharedMaterials(workingSubmeshInstructions);
			bool materialsChanged = this.rendererBuffers.MaterialsChangedInLastUpdate();
			if (updateTriangles)
			{
				this.meshGenerator.FillTriangles(currentMesh);
				this.meshRenderer.sharedMaterials = this.rendererBuffers.GetUpdatedSharedMaterialsArray();
			}
			else if (materialsChanged)
			{
				this.meshRenderer.sharedMaterials = this.rendererBuffers.GetUpdatedSharedMaterialsArray();
			}
			if (materialsChanged && this.maskMaterials.AnyMaterialCreated)
			{
				this.maskMaterials = new SkeletonRenderer.SpriteMaskInteractionMaterials();
			}
			this.meshGenerator.FillLateVertexData(currentMesh);
			if (this.meshFilter)
			{
				this.meshFilter.sharedMesh = currentMesh;
			}
			currentSmartMesh.instructionUsed.Set(currentInstructions);
			if (this.meshRenderer != null)
			{
				this.AssignSpriteMaskMaterials();
			}
			if (Application.isPlaying)
			{
				this.HandleOnDemandLoading();
			}
			if (this.fixDrawOrder && this.meshRenderer.sharedMaterials.Length > 2)
			{
				this.SetMaterialSettingsToFixDrawOrder();
			}
			if (this.OnMeshAndMaterialsUpdated != null)
			{
				this.OnMeshAndMaterialsUpdated(this);
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000CFBB File Offset: 0x0000B1BB
		public virtual void OnBecameVisible()
		{
			int num = (int)this.updateMode;
			this.updateMode = UpdateMode.FullUpdate;
			if (num != 3)
			{
				this.LateUpdate();
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000CFD3 File Offset: 0x0000B1D3
		public void OnBecameInvisible()
		{
			this.updateMode = this.updateWhenInvisible;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000CFE4 File Offset: 0x0000B1E4
		public void FindAndApplySeparatorSlots(string startsWith, bool clearExistingSeparators = true, bool updateStringArray = false)
		{
			if (string.IsNullOrEmpty(startsWith))
			{
				return;
			}
			this.FindAndApplySeparatorSlots((string slotName) => slotName.StartsWith(startsWith), clearExistingSeparators, updateStringArray);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000D020 File Offset: 0x0000B220
		public void FindAndApplySeparatorSlots(Func<string, bool> slotNamePredicate, bool clearExistingSeparators = true, bool updateStringArray = false)
		{
			if (slotNamePredicate == null)
			{
				return;
			}
			if (!this.valid)
			{
				return;
			}
			if (clearExistingSeparators)
			{
				this.separatorSlots.Clear();
			}
			foreach (Slot slot in this.skeleton.Slots)
			{
				if (slotNamePredicate(slot.Data.Name))
				{
					this.separatorSlots.Add(slot);
				}
			}
			if (updateStringArray)
			{
				List<string> detectedSeparatorNames = new List<string>();
				foreach (Slot slot2 in this.skeleton.Slots)
				{
					string slotName = slot2.Data.Name;
					if (slotNamePredicate(slotName))
					{
						detectedSeparatorNames.Add(slotName);
					}
				}
				if (!clearExistingSeparators)
				{
					foreach (string originalName in this.separatorSlotNames)
					{
						detectedSeparatorNames.Add(originalName);
					}
				}
				this.separatorSlotNames = detectedSeparatorNames.ToArray();
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000D14C File Offset: 0x0000B34C
		public void ReapplySeparatorSlotNames()
		{
			if (!this.valid)
			{
				return;
			}
			this.separatorSlots.Clear();
			int i = 0;
			int j = this.separatorSlotNames.Length;
			while (i < j)
			{
				Slot slot = this.skeleton.FindSlot(this.separatorSlotNames[i]);
				if (slot != null)
				{
					this.separatorSlots.Add(slot);
				}
				i++;
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		private void AssignSpriteMaskMaterials()
		{
			if (Application.isPlaying && this.maskInteraction != SpriteMaskInteraction.None && this.maskMaterials.materialsMaskDisabled.Length == 0)
			{
				this.maskMaterials.materialsMaskDisabled = this.meshRenderer.sharedMaterials;
			}
			if (this.maskMaterials.materialsMaskDisabled.Length != 0 && this.maskMaterials.materialsMaskDisabled[0] != null && this.maskInteraction == SpriteMaskInteraction.None)
			{
				this.meshRenderer.materials = this.maskMaterials.materialsMaskDisabled;
				return;
			}
			if (this.maskInteraction != SpriteMaskInteraction.VisibleInsideMask)
			{
				if (this.maskInteraction == SpriteMaskInteraction.VisibleOutsideMask)
				{
					if ((this.maskMaterials.materialsOutsideMask.Length == 0 || this.maskMaterials.materialsOutsideMask[0] == null) && !this.InitSpriteMaskMaterialsOutsideMask())
					{
						return;
					}
					this.meshRenderer.materials = this.maskMaterials.materialsOutsideMask;
				}
				return;
			}
			if ((this.maskMaterials.materialsInsideMask.Length == 0 || this.maskMaterials.materialsInsideMask[0] == null) && !this.InitSpriteMaskMaterialsInsideMask())
			{
				return;
			}
			this.meshRenderer.materials = this.maskMaterials.materialsInsideMask;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000D2C1 File Offset: 0x0000B4C1
		private bool InitSpriteMaskMaterialsInsideMask()
		{
			return this.InitSpriteMaskMaterialsForMaskType(CompareFunction.LessEqual, ref this.maskMaterials.materialsInsideMask);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000D2D5 File Offset: 0x0000B4D5
		private bool InitSpriteMaskMaterialsOutsideMask()
		{
			return this.InitSpriteMaskMaterialsForMaskType(CompareFunction.Greater, ref this.maskMaterials.materialsOutsideMask);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000D2EC File Offset: 0x0000B4EC
		private bool InitSpriteMaskMaterialsForMaskType(CompareFunction maskFunction, ref Material[] materialsToFill)
		{
			Material[] originalMaterials = this.maskMaterials.materialsMaskDisabled;
			materialsToFill = new Material[originalMaterials.Length];
			for (int i = 0; i < originalMaterials.Length; i++)
			{
				Material originalMaterial = originalMaterials[i];
				if (originalMaterial == null)
				{
					materialsToFill[i] = null;
				}
				else
				{
					Material newMaterial = new Material(originalMaterial);
					newMaterial.SetFloat(SkeletonRenderer.STENCIL_COMP_PARAM_ID, (float)maskFunction);
					materialsToFill[i] = newMaterial;
				}
			}
			return true;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000D34C File Offset: 0x0000B54C
		private void HandleOnDemandLoading()
		{
			foreach (AtlasAssetBase atlasAsset in this.skeletonDataAsset.atlasAssets)
			{
				if (atlasAsset.TextureLoadingMode != AtlasAssetBase.LoadingMode.Normal)
				{
					atlasAsset.BeginCustomTextureLoading();
					int i = 0;
					int count = this.meshRenderer.sharedMaterials.Length;
					while (i < count)
					{
						Material overrideMaterial = null;
						atlasAsset.RequireTexturesLoaded(this.meshRenderer.sharedMaterials[i], ref overrideMaterial);
						if (overrideMaterial != null)
						{
							this.meshRenderer.sharedMaterials[i] = overrideMaterial;
						}
						i++;
					}
					atlasAsset.EndCustomTextureLoading();
				}
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000D3D8 File Offset: 0x0000B5D8
		private void SetMaterialSettingsToFixDrawOrder()
		{
			if (this.reusedPropertyBlock == null)
			{
				this.reusedPropertyBlock = new MaterialPropertyBlock();
			}
			bool hasPerRendererBlock = this.meshRenderer.HasPropertyBlock();
			if (hasPerRendererBlock)
			{
				this.meshRenderer.GetPropertyBlock(this.reusedPropertyBlock);
			}
			for (int i = 0; i < this.meshRenderer.sharedMaterials.Length; i++)
			{
				if (this.meshRenderer.sharedMaterials[i])
				{
					if (!hasPerRendererBlock)
					{
						this.meshRenderer.GetPropertyBlock(this.reusedPropertyBlock, i);
					}
					this.reusedPropertyBlock.SetFloat(SkeletonRenderer.SUBMESH_DUMMY_PARAM_ID, (float)i);
					this.meshRenderer.SetPropertyBlock(this.reusedPropertyBlock, i);
					this.meshRenderer.sharedMaterials[i].enableInstancing = false;
				}
			}
		}

		// Token: 0x0400014E RID: 334
		public SkeletonDataAsset skeletonDataAsset;

		// Token: 0x0400014F RID: 335
		[SpineSkin("", "", false, false, true, false)]
		public string initialSkinName;

		// Token: 0x04000150 RID: 336
		public bool initialFlipX;

		// Token: 0x04000151 RID: 337
		public bool initialFlipY;

		// Token: 0x04000152 RID: 338
		protected UpdateMode updateMode = UpdateMode.FullUpdate;

		// Token: 0x04000153 RID: 339
		public UpdateMode updateWhenInvisible = UpdateMode.FullUpdate;

		// Token: 0x04000154 RID: 340
		[FormerlySerializedAs("submeshSeparators")]
		[SerializeField]
		[SpineSlot("", "", false, true, false)]
		protected string[] separatorSlotNames = new string[0];

		// Token: 0x04000155 RID: 341
		[NonSerialized]
		public readonly List<Slot> separatorSlots = new List<Slot>();

		// Token: 0x04000156 RID: 342
		[Range(-0.1f, 0f)]
		public float zSpacing;

		// Token: 0x04000157 RID: 343
		public bool useClipping = true;

		// Token: 0x04000158 RID: 344
		public bool immutableTriangles;

		// Token: 0x04000159 RID: 345
		public bool pmaVertexColors = true;

		// Token: 0x0400015A RID: 346
		public bool clearStateOnDisable;

		// Token: 0x0400015B RID: 347
		public bool tintBlack;

		// Token: 0x0400015C RID: 348
		public bool singleSubmesh;

		// Token: 0x0400015D RID: 349
		public bool fixDrawOrder;

		// Token: 0x0400015E RID: 350
		[FormerlySerializedAs("calculateNormals")]
		public bool addNormals;

		// Token: 0x0400015F RID: 351
		public bool calculateTangents;

		// Token: 0x04000160 RID: 352
		public SpriteMaskInteraction maskInteraction;

		// Token: 0x04000161 RID: 353
		public SkeletonRenderer.SpriteMaskInteractionMaterials maskMaterials = new SkeletonRenderer.SpriteMaskInteractionMaterials();

		// Token: 0x04000162 RID: 354
		public static readonly int STENCIL_COMP_PARAM_ID = Shader.PropertyToID("_StencilComp");

		// Token: 0x04000163 RID: 355
		public const CompareFunction STENCIL_COMP_MASKINTERACTION_NONE = CompareFunction.Always;

		// Token: 0x04000164 RID: 356
		public const CompareFunction STENCIL_COMP_MASKINTERACTION_VISIBLE_INSIDE = CompareFunction.LessEqual;

		// Token: 0x04000165 RID: 357
		public const CompareFunction STENCIL_COMP_MASKINTERACTION_VISIBLE_OUTSIDE = CompareFunction.Greater;

		// Token: 0x04000166 RID: 358
		public bool disableRenderingOnOverride = true;

		// Token: 0x04000169 RID: 361
		[NonSerialized]
		private readonly Dictionary<Material, Material> customMaterialOverride = new Dictionary<Material, Material>();

		// Token: 0x0400016A RID: 362
		[NonSerialized]
		private readonly Dictionary<Slot, Material> customSlotMaterials = new Dictionary<Slot, Material>();

		// Token: 0x0400016B RID: 363
		[NonSerialized]
		private readonly SkeletonRendererInstruction currentInstructions = new SkeletonRendererInstruction();

		// Token: 0x0400016C RID: 364
		private readonly MeshGenerator meshGenerator = new MeshGenerator();

		// Token: 0x0400016D RID: 365
		[NonSerialized]
		private readonly MeshRendererBuffers rendererBuffers = new MeshRendererBuffers();

		// Token: 0x0400016E RID: 366
		private MeshRenderer meshRenderer;

		// Token: 0x0400016F RID: 367
		private MeshFilter meshFilter;

		// Token: 0x04000170 RID: 368
		[NonSerialized]
		public bool valid;

		// Token: 0x04000171 RID: 369
		[NonSerialized]
		public Skeleton skeleton;

		// Token: 0x04000172 RID: 370
		[SerializeField]
		protected Vector2 physicsPositionInheritanceFactor = Vector2.one;

		// Token: 0x04000173 RID: 371
		[SerializeField]
		protected float physicsRotationInheritanceFactor = 1f;

		// Token: 0x04000174 RID: 372
		[SerializeField]
		protected Transform physicsMovementRelativeTo;

		// Token: 0x04000175 RID: 373
		protected Vector3 lastPosition;

		// Token: 0x04000176 RID: 374
		protected float lastRotation;

		// Token: 0x04000179 RID: 377
		private MaterialPropertyBlock reusedPropertyBlock;

		// Token: 0x0400017A RID: 378
		public static readonly int SUBMESH_DUMMY_PARAM_ID = Shader.PropertyToID("_Submesh");

		// Token: 0x0200003D RID: 61
		[Serializable]
		public class SpriteMaskInteractionMaterials
		{
			// Token: 0x1700005D RID: 93
			// (get) Token: 0x0600026C RID: 620 RVA: 0x0000D555 File Offset: 0x0000B755
			public bool AnyMaterialCreated
			{
				get
				{
					return this.materialsMaskDisabled.Length != 0 || this.materialsInsideMask.Length != 0 || this.materialsOutsideMask.Length != 0;
				}
			}

			// Token: 0x0400017B RID: 379
			public Material[] materialsMaskDisabled = new Material[0];

			// Token: 0x0400017C RID: 380
			public Material[] materialsInsideMask = new Material[0];

			// Token: 0x0400017D RID: 381
			public Material[] materialsOutsideMask = new Material[0];
		}

		// Token: 0x0200003E RID: 62
		// (Invoke) Token: 0x0600026F RID: 623
		public delegate void InstructionDelegate(SkeletonRendererInstruction instruction);

		// Token: 0x0200003F RID: 63
		// (Invoke) Token: 0x06000273 RID: 627
		public delegate void SkeletonRendererDelegate(SkeletonRenderer skeletonRenderer);
	}
}
