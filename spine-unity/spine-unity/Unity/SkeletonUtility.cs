using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x0200004B RID: 75
	[ExecuteAlways]
	[RequireComponent(typeof(ISkeletonAnimation))]
	[HelpURL("http://esotericsoftware.com/spine-unity#SkeletonUtility")]
	public sealed class SkeletonUtility : MonoBehaviour
	{
		// Token: 0x0600029B RID: 667 RVA: 0x0000DFC8 File Offset: 0x0000C1C8
		public static PolygonCollider2D AddBoundingBoxGameObject(Skeleton skeleton, string skinName, string slotName, string attachmentName, Transform parent, bool isTrigger = true)
		{
			Skin skin = (string.IsNullOrEmpty(skinName) ? skeleton.Data.DefaultSkin : skeleton.Data.FindSkin(skinName));
			if (skin == null)
			{
				Debug.LogError("Skin " + skinName + " not found!");
				return null;
			}
			Slot slot = skeleton.FindSlot(slotName);
			Attachment attachment = ((slot != null) ? skin.GetAttachment(slot.Data.Index, attachmentName) : null);
			if (attachment == null)
			{
				Debug.LogFormat("Attachment in slot '{0}' named '{1}' not found in skin '{2}'.", new object[] { slotName, attachmentName, skin.Name });
				return null;
			}
			BoundingBoxAttachment box = attachment as BoundingBoxAttachment;
			if (box != null)
			{
				return SkeletonUtility.AddBoundingBoxGameObject(box.Name, box, slot, parent, isTrigger);
			}
			Debug.LogFormat("Attachment '{0}' was not a Bounding Box.", new object[] { attachmentName });
			return null;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000E08C File Offset: 0x0000C28C
		public static PolygonCollider2D AddBoundingBoxGameObject(string name, BoundingBoxAttachment box, Slot slot, Transform parent, bool isTrigger = true)
		{
			GameObject go = new GameObject("[BoundingBox]" + (string.IsNullOrEmpty(name) ? box.Name : name));
			Transform transform = go.transform;
			transform.parent = parent;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			return SkeletonUtility.AddBoundingBoxAsComponent(box, slot, go, isTrigger);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000E0F1 File Offset: 0x0000C2F1
		public static PolygonCollider2D AddBoundingBoxAsComponent(BoundingBoxAttachment box, Slot slot, GameObject gameObject, bool isTrigger = true)
		{
			if (box == null)
			{
				return null;
			}
			PolygonCollider2D polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
			polygonCollider2D.isTrigger = isTrigger;
			SkeletonUtility.SetColliderPointsLocal(polygonCollider2D, slot, box, 1f);
			return polygonCollider2D;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000E114 File Offset: 0x0000C314
		public static void SetColliderPointsLocal(PolygonCollider2D collider, Slot slot, BoundingBoxAttachment box, float scale = 1f)
		{
			if (box == null)
			{
				return;
			}
			if (box.IsWeighted())
			{
				Debug.LogWarning("UnityEngine.PolygonCollider2D does not support weighted or animated points. Collider points will not be animated and may have incorrect orientation. If you want to use it as a collider, please remove weights and animations from the bounding box in Spine editor.");
			}
			Vector2[] verts = box.GetLocalVertices(slot, null);
			if (scale != 1f)
			{
				int i = 0;
				int j = verts.Length;
				while (i < j)
				{
					verts[i] *= scale;
					i++;
				}
			}
			collider.SetPath(0, verts);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000E178 File Offset: 0x0000C378
		public static Bounds GetBoundingBoxBounds(BoundingBoxAttachment boundingBox, float depth = 0f)
		{
			float[] floats = boundingBox.Vertices;
			int floatCount = floats.Length;
			Bounds bounds = default(Bounds);
			bounds.center = new Vector3(floats[0], floats[1], 0f);
			for (int i = 2; i < floatCount; i += 2)
			{
				bounds.Encapsulate(new Vector3(floats[i], floats[i + 1], 0f));
			}
			Vector3 size = bounds.size;
			size.z = depth;
			bounds.size = size;
			return bounds;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
		public static Rigidbody2D AddBoneRigidbody2D(GameObject gameObject, bool isKinematic = true, float gravityScale = 0f)
		{
			Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
			if (rb == null)
			{
				rb = gameObject.AddComponent<Rigidbody2D>();
				rb.isKinematic = isKinematic;
				rb.gravityScale = gravityScale;
			}
			return rb;
		}

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060002A1 RID: 673 RVA: 0x0000E228 File Offset: 0x0000C428
		// (remove) Token: 0x060002A2 RID: 674 RVA: 0x0000E260 File Offset: 0x0000C460
		public event SkeletonUtility.SkeletonUtilityDelegate OnReset;

		// Token: 0x060002A3 RID: 675 RVA: 0x0000E298 File Offset: 0x0000C498
		private void Update()
		{
			Skeleton skeleton = this.skeletonComponent.Skeleton;
			if (skeleton != null && this.boneRoot != null)
			{
				if (this.flipBy180DegreeRotation)
				{
					this.boneRoot.localScale = new Vector3(Mathf.Abs(skeleton.ScaleX), Mathf.Abs(skeleton.ScaleY), 1f);
					this.boneRoot.eulerAngles = new Vector3((float)((skeleton.ScaleY > 0f) ? 0 : 180), (float)((skeleton.ScaleX > 0f) ? 0 : 180), 0f);
				}
				else
				{
					this.boneRoot.localScale = new Vector3(skeleton.ScaleX, skeleton.ScaleY, 1f);
				}
			}
			if (this.skeletonGraphic != null)
			{
				this.positionScale = this.skeletonGraphic.MeshScale;
				this.lastPositionScale = this.positionScale;
				if (this.boneRoot)
				{
					this.positionOffset = this.skeletonGraphic.MeshOffset;
					if (this.positionOffset != Vector2.zero)
					{
						this.boneRoot.localPosition = this.positionOffset;
					}
				}
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000E3D0 File Offset: 0x0000C5D0
		private void UpdateToMeshScaleAndOffset(MeshGeneratorBuffers ignoredParameter)
		{
			if (this.skeletonGraphic == null)
			{
				return;
			}
			this.positionScale = this.skeletonGraphic.MeshScale;
			if (this.boneRoot)
			{
				this.positionOffset = this.skeletonGraphic.MeshOffset;
				if (this.positionOffset != Vector2.zero)
				{
					this.boneRoot.localPosition = this.positionOffset;
				}
			}
			if (this.lastPositionScale != this.positionScale)
			{
				this.UpdateLocal(this.skeletonAnimation);
				this.UpdateWorld(this.skeletonAnimation);
				this.UpdateComplete(this.skeletonAnimation);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000E478 File Offset: 0x0000C678
		public ISkeletonComponent SkeletonComponent
		{
			get
			{
				if (this.skeletonComponent == null)
				{
					this.skeletonComponent = ((this.skeletonRenderer != null) ? this.skeletonRenderer.GetComponent<ISkeletonComponent>() : ((this.skeletonGraphic != null) ? this.skeletonGraphic.GetComponent<ISkeletonComponent>() : base.GetComponent<ISkeletonComponent>()));
				}
				return this.skeletonComponent;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000E4D5 File Offset: 0x0000C6D5
		public Skeleton Skeleton
		{
			get
			{
				if (this.SkeletonComponent == null)
				{
					return null;
				}
				return this.skeletonComponent.Skeleton;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		public bool IsValid
		{
			get
			{
				return (this.skeletonRenderer != null && this.skeletonRenderer.valid) || (this.skeletonGraphic != null && this.skeletonGraphic.IsValid);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000E526 File Offset: 0x0000C726
		public float PositionScale
		{
			get
			{
				return this.positionScale;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000E52E File Offset: 0x0000C72E
		public Vector2 PositionOffset
		{
			get
			{
				return this.positionOffset;
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000E536 File Offset: 0x0000C736
		public void ResubscribeEvents()
		{
			this.ResubscribeIndependentEvents();
			this.ResubscribeDependentEvents();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000E544 File Offset: 0x0000C744
		private void ResubscribeIndependentEvents()
		{
			if (this.skeletonRenderer != null)
			{
				this.skeletonRenderer.OnRebuild -= this.HandleRendererReset;
				this.skeletonRenderer.OnRebuild += this.HandleRendererReset;
			}
			else if (this.skeletonGraphic != null)
			{
				this.skeletonGraphic.OnRebuild -= this.HandleRendererReset;
				this.skeletonGraphic.OnRebuild += this.HandleRendererReset;
				this.skeletonGraphic.OnPostProcessVertices -= this.UpdateToMeshScaleAndOffset;
				this.skeletonGraphic.OnPostProcessVertices += this.UpdateToMeshScaleAndOffset;
			}
			if (this.skeletonAnimation != null)
			{
				this.skeletonAnimation.UpdateLocal -= this.UpdateLocal;
				this.skeletonAnimation.UpdateLocal += this.UpdateLocal;
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000E630 File Offset: 0x0000C830
		private void ResubscribeDependentEvents()
		{
			if (this.skeletonAnimation != null)
			{
				this.skeletonAnimation.UpdateWorld -= this.UpdateWorld;
				this.skeletonAnimation.UpdateComplete -= this.UpdateComplete;
				if (this.hasOverrideBones || this.hasConstraints)
				{
					this.skeletonAnimation.UpdateWorld += this.UpdateWorld;
				}
				if (this.hasConstraints)
				{
					this.skeletonAnimation.UpdateComplete += this.UpdateComplete;
				}
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000E6BC File Offset: 0x0000C8BC
		private void OnEnable()
		{
			if (this.skeletonRenderer == null)
			{
				this.skeletonRenderer = base.GetComponent<SkeletonRenderer>();
			}
			if (this.skeletonGraphic == null)
			{
				this.skeletonGraphic = base.GetComponent<SkeletonGraphic>();
			}
			if (this.skeletonAnimation == null)
			{
				this.skeletonAnimation = ((this.skeletonRenderer != null) ? this.skeletonRenderer.GetComponent<ISkeletonAnimation>() : ((this.skeletonGraphic != null) ? this.skeletonGraphic.GetComponent<ISkeletonAnimation>() : base.GetComponent<ISkeletonAnimation>()));
			}
			if (this.skeletonComponent == null)
			{
				this.skeletonComponent = ((this.skeletonRenderer != null) ? this.skeletonRenderer.GetComponent<ISkeletonComponent>() : ((this.skeletonGraphic != null) ? this.skeletonGraphic.GetComponent<ISkeletonComponent>() : base.GetComponent<ISkeletonComponent>()));
			}
			this.CollectBones();
			this.ResubscribeEvents();
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000E79D File Offset: 0x0000C99D
		private void Start()
		{
			this.CollectBones();
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000E7A8 File Offset: 0x0000C9A8
		private void OnDisable()
		{
			if (this.skeletonRenderer != null)
			{
				this.skeletonRenderer.OnRebuild -= this.HandleRendererReset;
			}
			if (this.skeletonGraphic != null)
			{
				this.skeletonGraphic.OnRebuild -= this.HandleRendererReset;
				this.skeletonGraphic.OnPostProcessVertices -= this.UpdateToMeshScaleAndOffset;
			}
			if (this.skeletonAnimation != null)
			{
				this.skeletonAnimation.UpdateLocal -= this.UpdateLocal;
				this.skeletonAnimation.UpdateWorld -= this.UpdateWorld;
				this.skeletonAnimation.UpdateComplete -= this.UpdateComplete;
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000E863 File Offset: 0x0000CA63
		private void HandleRendererReset(SkeletonRenderer r)
		{
			if (this.OnReset != null)
			{
				this.OnReset();
			}
			this.CollectBones();
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000E863 File Offset: 0x0000CA63
		private void HandleRendererReset(SkeletonGraphic g)
		{
			if (this.OnReset != null)
			{
				this.OnReset();
			}
			this.CollectBones();
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000E87E File Offset: 0x0000CA7E
		public void RegisterBone(SkeletonUtilityBone bone)
		{
			if (this.boneComponents.Contains(bone))
			{
				return;
			}
			this.boneComponents.Add(bone);
			this.needToReprocessBones = true;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000E8A2 File Offset: 0x0000CAA2
		public void UnregisterBone(SkeletonUtilityBone bone)
		{
			this.boneComponents.Remove(bone);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000E8B1 File Offset: 0x0000CAB1
		public void RegisterConstraint(SkeletonUtilityConstraint constraint)
		{
			if (this.constraintComponents.Contains(constraint))
			{
				return;
			}
			this.constraintComponents.Add(constraint);
			this.needToReprocessBones = true;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000E8D5 File Offset: 0x0000CAD5
		public void UnregisterConstraint(SkeletonUtilityConstraint constraint)
		{
			this.constraintComponents.Remove(constraint);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000E8E4 File Offset: 0x0000CAE4
		public void CollectBones()
		{
			Skeleton skeleton = this.skeletonComponent.Skeleton;
			if (skeleton == null)
			{
				return;
			}
			if (this.boneRoot != null)
			{
				List<object> constraintTargets = new List<object>();
				ExposedList<IkConstraint> ikConstraints = skeleton.IkConstraints;
				int i = 0;
				int j = ikConstraints.Count;
				while (i < j)
				{
					constraintTargets.Add(ikConstraints.Items[i].Target);
					i++;
				}
				ExposedList<TransformConstraint> transformConstraints = skeleton.TransformConstraints;
				int k = 0;
				int l = transformConstraints.Count;
				while (k < l)
				{
					constraintTargets.Add(transformConstraints.Items[k].Target);
					k++;
				}
				List<SkeletonUtilityBone> boneComponents = this.boneComponents;
				int m = 0;
				int n = boneComponents.Count;
				while (m < n)
				{
					SkeletonUtilityBone b = boneComponents[m];
					if (b.bone != null)
					{
						goto IL_00CA;
					}
					b.DoUpdate(SkeletonUtilityBone.UpdatePhase.Local);
					if (b.bone != null)
					{
						goto IL_00CA;
					}
					IL_00FB:
					m++;
					continue;
					IL_00CA:
					this.hasOverrideBones |= b.mode == SkeletonUtilityBone.Mode.Override;
					this.hasConstraints |= constraintTargets.Contains(b.bone);
					goto IL_00FB;
				}
				this.hasConstraints |= this.constraintComponents.Count > 0;
				this.needToReprocessBones = false;
			}
			else
			{
				this.boneComponents.Clear();
				this.constraintComponents.Clear();
			}
			this.ResubscribeDependentEvents();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000EA38 File Offset: 0x0000CC38
		private void UpdateLocal(ISkeletonAnimation anim)
		{
			if (this.needToReprocessBones)
			{
				this.CollectBones();
			}
			List<SkeletonUtilityBone> boneComponents = this.boneComponents;
			if (boneComponents == null)
			{
				return;
			}
			int i = 0;
			int j = boneComponents.Count;
			while (i < j)
			{
				boneComponents[i].transformLerpComplete = false;
				i++;
			}
			this.UpdateAllBones(SkeletonUtilityBone.UpdatePhase.Local);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000EA88 File Offset: 0x0000CC88
		private void UpdateWorld(ISkeletonAnimation anim)
		{
			this.UpdateAllBones(SkeletonUtilityBone.UpdatePhase.World);
			int i = 0;
			int j = this.constraintComponents.Count;
			while (i < j)
			{
				this.constraintComponents[i].DoUpdate();
				i++;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000EAC5 File Offset: 0x0000CCC5
		private void UpdateComplete(ISkeletonAnimation anim)
		{
			this.UpdateAllBones(SkeletonUtilityBone.UpdatePhase.Complete);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000EAD0 File Offset: 0x0000CCD0
		private void UpdateAllBones(SkeletonUtilityBone.UpdatePhase phase)
		{
			if (this.boneRoot == null)
			{
				this.CollectBones();
			}
			List<SkeletonUtilityBone> boneComponents = this.boneComponents;
			if (boneComponents == null)
			{
				return;
			}
			int i = 0;
			int j = boneComponents.Count;
			while (i < j)
			{
				boneComponents[i].DoUpdate(phase);
				i++;
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000EB1C File Offset: 0x0000CD1C
		public Transform GetBoneRoot()
		{
			if (this.boneRoot != null)
			{
				return this.boneRoot;
			}
			GameObject boneRootObject = new GameObject("SkeletonUtility-SkeletonRoot");
			if (this.skeletonGraphic != null)
			{
				boneRootObject.AddComponent<RectTransform>();
			}
			this.boneRoot = boneRootObject.transform;
			this.boneRoot.SetParent(base.transform);
			this.boneRoot.localPosition = Vector3.zero;
			this.boneRoot.localRotation = Quaternion.identity;
			this.boneRoot.localScale = Vector3.one;
			return this.boneRoot;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000EBB4 File Offset: 0x0000CDB4
		public GameObject SpawnRoot(SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca)
		{
			this.GetBoneRoot();
			Skeleton skeleton = this.skeletonComponent.Skeleton;
			GameObject gameObject = this.SpawnBone(skeleton.RootBone, this.boneRoot, mode, pos, rot, sca);
			this.CollectBones();
			return gameObject;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000EBF4 File Offset: 0x0000CDF4
		public GameObject SpawnHierarchy(SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca)
		{
			this.GetBoneRoot();
			Skeleton skeleton = this.skeletonComponent.Skeleton;
			GameObject gameObject = this.SpawnBoneRecursively(skeleton.RootBone, this.boneRoot, mode, pos, rot, sca);
			this.CollectBones();
			return gameObject;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000EC34 File Offset: 0x0000CE34
		public GameObject SpawnBoneRecursively(Bone bone, Transform parent, SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca)
		{
			GameObject go = this.SpawnBone(bone, parent, mode, pos, rot, sca);
			ExposedList<Bone> childrenBones = bone.Children;
			int i = 0;
			int j = childrenBones.Count;
			while (i < j)
			{
				Bone child = childrenBones.Items[i];
				this.SpawnBoneRecursively(child, go.transform, mode, pos, rot, sca);
				i++;
			}
			return go;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000EC8C File Offset: 0x0000CE8C
		public GameObject SpawnBone(Bone bone, Transform parent, SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca)
		{
			GameObject go = new GameObject(bone.Data.Name);
			if (this.skeletonGraphic != null)
			{
				go.AddComponent<RectTransform>();
			}
			Transform goTransform = go.transform;
			goTransform.SetParent(parent);
			SkeletonUtilityBone b = go.AddComponent<SkeletonUtilityBone>();
			b.hierarchy = this;
			b.position = pos;
			b.rotation = rot;
			b.scale = sca;
			b.mode = mode;
			b.zPosition = true;
			b.Reset();
			b.bone = bone;
			b.boneName = bone.Data.Name;
			b.valid = true;
			if (mode == SkeletonUtilityBone.Mode.Override)
			{
				if (rot)
				{
					goTransform.localRotation = Quaternion.Euler(0f, 0f, b.bone.AppliedRotation);
				}
				if (pos)
				{
					goTransform.localPosition = new Vector3(b.bone.X * this.positionScale + this.positionOffset.x, b.bone.Y * this.positionScale + this.positionOffset.y, 0f);
				}
				goTransform.localScale = new Vector3(b.bone.ScaleX, b.bone.ScaleY, 0f);
			}
			return go;
		}

		// Token: 0x040001A2 RID: 418
		public Transform boneRoot;

		// Token: 0x040001A3 RID: 419
		public bool flipBy180DegreeRotation;

		// Token: 0x040001A4 RID: 420
		[HideInInspector]
		public SkeletonRenderer skeletonRenderer;

		// Token: 0x040001A5 RID: 421
		[HideInInspector]
		public SkeletonGraphic skeletonGraphic;

		// Token: 0x040001A6 RID: 422
		[NonSerialized]
		public ISkeletonAnimation skeletonAnimation;

		// Token: 0x040001A7 RID: 423
		private ISkeletonComponent skeletonComponent;

		// Token: 0x040001A8 RID: 424
		[NonSerialized]
		public List<SkeletonUtilityBone> boneComponents = new List<SkeletonUtilityBone>();

		// Token: 0x040001A9 RID: 425
		[NonSerialized]
		public List<SkeletonUtilityConstraint> constraintComponents = new List<SkeletonUtilityConstraint>();

		// Token: 0x040001AA RID: 426
		private float positionScale = 1f;

		// Token: 0x040001AB RID: 427
		private float lastPositionScale = 1f;

		// Token: 0x040001AC RID: 428
		private Vector2 positionOffset = Vector2.zero;

		// Token: 0x040001AD RID: 429
		private bool hasOverrideBones;

		// Token: 0x040001AE RID: 430
		private bool hasConstraints;

		// Token: 0x040001AF RID: 431
		private bool needToReprocessBones;

		// Token: 0x0200004C RID: 76
		// (Invoke) Token: 0x060002C2 RID: 706
		public delegate void SkeletonUtilityDelegate();
	}
}
