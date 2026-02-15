using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x0200004D RID: 77
	[ExecuteAlways]
	[AddComponentMenu("Spine/SkeletonUtilityBone")]
	[HelpURL("http://esotericsoftware.com/spine-unity#SkeletonUtilityBone")]
	public class SkeletonUtilityBone : MonoBehaviour
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000EE07 File Offset: 0x0000D007
		public bool IncompatibleTransformMode
		{
			get
			{
				return this.incompatibleTransformMode;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000EE10 File Offset: 0x0000D010
		public void Reset()
		{
			this.bone = null;
			this.cachedTransform = base.transform;
			this.valid = this.hierarchy != null && this.hierarchy.IsValid;
			if (!this.valid)
			{
				return;
			}
			this.skeletonTransform = this.hierarchy.transform;
			this.hierarchy.OnReset -= this.HandleOnReset;
			this.hierarchy.OnReset += this.HandleOnReset;
			this.DoUpdate(SkeletonUtilityBone.UpdatePhase.Local);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000EEA4 File Offset: 0x0000D0A4
		private void OnEnable()
		{
			if (this.hierarchy == null)
			{
				this.hierarchy = base.transform.GetComponentInParent<SkeletonUtility>();
			}
			if (this.hierarchy == null)
			{
				return;
			}
			this.hierarchy.RegisterBone(this);
			this.hierarchy.OnReset += this.HandleOnReset;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000EF02 File Offset: 0x0000D102
		private void HandleOnReset()
		{
			this.Reset();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000EF0A File Offset: 0x0000D10A
		private void OnDisable()
		{
			if (this.hierarchy != null)
			{
				this.hierarchy.OnReset -= this.HandleOnReset;
				this.hierarchy.UnregisterBone(this);
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000EF40 File Offset: 0x0000D140
		public void DoUpdate(SkeletonUtilityBone.UpdatePhase phase)
		{
			if (!this.valid)
			{
				this.Reset();
				return;
			}
			Skeleton skeleton = this.hierarchy.Skeleton;
			if (this.bone == null)
			{
				if (string.IsNullOrEmpty(this.boneName))
				{
					return;
				}
				this.bone = skeleton.FindBone(this.boneName);
				if (this.bone == null)
				{
					Debug.LogError("Bone not found: " + this.boneName, this);
					return;
				}
			}
			if (!this.bone.Active)
			{
				return;
			}
			float positionScale = this.hierarchy.PositionScale;
			Transform thisTransform = this.cachedTransform;
			float skeletonFlipRotation = Mathf.Sign(skeleton.ScaleX * skeleton.ScaleY);
			if (this.mode == SkeletonUtilityBone.Mode.Follow)
			{
				if (phase != SkeletonUtilityBone.UpdatePhase.Local)
				{
					if (phase - SkeletonUtilityBone.UpdatePhase.World > 1)
					{
						return;
					}
					if (this.position)
					{
						thisTransform.localPosition = new Vector3(this.bone.AX * positionScale, this.bone.AY * positionScale, this.zPosition ? 0f : thisTransform.localPosition.z);
					}
					if (this.rotation)
					{
						if (this.bone.Data.Inherit.InheritsRotation())
						{
							thisTransform.localRotation = Quaternion.Euler(0f, 0f, this.bone.AppliedRotation);
						}
						else
						{
							Vector3 euler = this.skeletonTransform.rotation.eulerAngles;
							thisTransform.rotation = Quaternion.Euler(euler.x, euler.y, euler.z + this.bone.WorldRotationX * skeletonFlipRotation);
						}
					}
					if (this.scale)
					{
						thisTransform.localScale = new Vector3(this.bone.AScaleX, this.bone.AScaleY, 1f);
						this.incompatibleTransformMode = SkeletonUtilityBone.BoneTransformModeIncompatible(this.bone);
						return;
					}
				}
				else
				{
					if (this.position)
					{
						thisTransform.localPosition = new Vector3(this.bone.X * positionScale, this.bone.Y * positionScale, this.zPosition ? 0f : thisTransform.localPosition.z);
					}
					if (this.rotation)
					{
						if (this.bone.Data.Inherit.InheritsRotation())
						{
							thisTransform.localRotation = Quaternion.Euler(0f, 0f, this.bone.Rotation);
						}
						else
						{
							Vector3 euler2 = this.skeletonTransform.rotation.eulerAngles;
							thisTransform.rotation = Quaternion.Euler(euler2.x, euler2.y, euler2.z + this.bone.WorldRotationX * skeletonFlipRotation);
						}
					}
					if (this.scale)
					{
						thisTransform.localScale = new Vector3(this.bone.ScaleX, this.bone.ScaleY, 1f);
						this.incompatibleTransformMode = SkeletonUtilityBone.BoneTransformModeIncompatible(this.bone);
						return;
					}
				}
			}
			else if (this.mode == SkeletonUtilityBone.Mode.Override)
			{
				if (this.transformLerpComplete)
				{
					return;
				}
				if (this.parentReference == null)
				{
					if (this.position)
					{
						Vector3 clp = thisTransform.localPosition / positionScale;
						this.bone.X = Mathf.Lerp(this.bone.X, clp.x, this.overrideAlpha);
						this.bone.Y = Mathf.Lerp(this.bone.Y, clp.y, this.overrideAlpha);
					}
					if (this.rotation)
					{
						float angle = Mathf.LerpAngle(this.bone.Rotation, thisTransform.localRotation.eulerAngles.z, this.overrideAlpha);
						this.bone.Rotation = angle;
						this.bone.AppliedRotation = angle;
					}
					if (this.scale)
					{
						Vector3 cls = thisTransform.localScale;
						this.bone.ScaleX = Mathf.Lerp(this.bone.ScaleX, cls.x, this.overrideAlpha);
						this.bone.ScaleY = Mathf.Lerp(this.bone.ScaleY, cls.y, this.overrideAlpha);
					}
				}
				else
				{
					if (this.transformLerpComplete)
					{
						return;
					}
					if (this.position)
					{
						Vector3 pos = this.parentReference.InverseTransformPoint(thisTransform.position) / positionScale;
						this.bone.X = Mathf.Lerp(this.bone.X, pos.x, this.overrideAlpha);
						this.bone.Y = Mathf.Lerp(this.bone.Y, pos.y, this.overrideAlpha);
					}
					if (this.rotation)
					{
						float angle2 = Mathf.LerpAngle(this.bone.Rotation, Quaternion.LookRotation(Vector3.forward, this.parentReference.InverseTransformDirection(thisTransform.up)).eulerAngles.z, this.overrideAlpha);
						this.bone.Rotation = angle2;
						this.bone.AppliedRotation = angle2;
					}
					if (this.scale)
					{
						Vector3 cls2 = thisTransform.localScale;
						this.bone.ScaleX = Mathf.Lerp(this.bone.ScaleX, cls2.x, this.overrideAlpha);
						this.bone.ScaleY = Mathf.Lerp(this.bone.ScaleY, cls2.y, this.overrideAlpha);
					}
					this.incompatibleTransformMode = SkeletonUtilityBone.BoneTransformModeIncompatible(this.bone);
				}
				this.transformLerpComplete = true;
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000F4B0 File Offset: 0x0000D6B0
		public static bool BoneTransformModeIncompatible(Bone bone)
		{
			return !bone.Data.Inherit.InheritsScale();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000F4C5 File Offset: 0x0000D6C5
		public void AddBoundingBox(string skinName, string slotName, string attachmentName)
		{
			SkeletonUtility.AddBoneRigidbody2D(base.transform.gameObject, true, 0f);
			SkeletonUtility.AddBoundingBoxGameObject(this.bone.Skeleton, skinName, slotName, attachmentName, base.transform, true);
		}

		// Token: 0x040001B0 RID: 432
		public string boneName;

		// Token: 0x040001B1 RID: 433
		public Transform parentReference;

		// Token: 0x040001B2 RID: 434
		public SkeletonUtilityBone.Mode mode;

		// Token: 0x040001B3 RID: 435
		public bool position;

		// Token: 0x040001B4 RID: 436
		public bool rotation;

		// Token: 0x040001B5 RID: 437
		public bool scale;

		// Token: 0x040001B6 RID: 438
		public bool zPosition = true;

		// Token: 0x040001B7 RID: 439
		[Range(0f, 1f)]
		public float overrideAlpha = 1f;

		// Token: 0x040001B8 RID: 440
		public SkeletonUtility hierarchy;

		// Token: 0x040001B9 RID: 441
		[NonSerialized]
		public Bone bone;

		// Token: 0x040001BA RID: 442
		[NonSerialized]
		public bool transformLerpComplete;

		// Token: 0x040001BB RID: 443
		[NonSerialized]
		public bool valid;

		// Token: 0x040001BC RID: 444
		private Transform cachedTransform;

		// Token: 0x040001BD RID: 445
		private Transform skeletonTransform;

		// Token: 0x040001BE RID: 446
		private bool incompatibleTransformMode;

		// Token: 0x0200004E RID: 78
		public enum Mode
		{
			// Token: 0x040001C0 RID: 448
			Follow,
			// Token: 0x040001C1 RID: 449
			Override
		}

		// Token: 0x0200004F RID: 79
		public enum UpdatePhase
		{
			// Token: 0x040001C3 RID: 451
			Local,
			// Token: 0x040001C4 RID: 452
			World,
			// Token: 0x040001C5 RID: 453
			Complete
		}
	}
}
