using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000020 RID: 32
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	[AddComponentMenu("Spine/UI/BoneFollowerGraphic")]
	[HelpURL("http://esotericsoftware.com/spine-unity#BoneFollowerGraphic")]
	public class BoneFollowerGraphic : MonoBehaviour
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004ADE File Offset: 0x00002CDE
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00004AE6 File Offset: 0x00002CE6
		public SkeletonGraphic SkeletonGraphic
		{
			get
			{
				return this.skeletonGraphic;
			}
			set
			{
				this.skeletonGraphic = value;
				this.Initialize();
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004AF5 File Offset: 0x00002CF5
		public bool SetBone(string name)
		{
			this.bone = this.skeletonGraphic.Skeleton.FindBone(name);
			if (this.bone == null)
			{
				Debug.LogError("Bone not found: " + name, this);
				return false;
			}
			this.boneName = name;
			return true;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004B31 File Offset: 0x00002D31
		public void Awake()
		{
			if (this.initializeOnAwake)
			{
				this.Initialize();
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004B44 File Offset: 0x00002D44
		public virtual void Initialize()
		{
			this.bone = null;
			this.valid = this.skeletonGraphic != null && this.skeletonGraphic.IsValid;
			if (!this.valid)
			{
				return;
			}
			this.skeletonTransform = this.skeletonGraphic.transform;
			this.skeletonTransformIsParent = this.skeletonTransform == base.transform.parent;
			if (!string.IsNullOrEmpty(this.boneName))
			{
				this.bone = this.skeletonGraphic.Skeleton.FindBone(this.boneName);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004BD8 File Offset: 0x00002DD8
		public virtual void LateUpdate()
		{
			if (!this.valid)
			{
				this.Initialize();
				return;
			}
			if (this.bone == null)
			{
				if (string.IsNullOrEmpty(this.boneName))
				{
					return;
				}
				this.bone = this.skeletonGraphic.Skeleton.FindBone(this.boneName);
				if (!this.SetBone(this.boneName))
				{
					return;
				}
			}
			RectTransform thisTransform = base.transform as RectTransform;
			if (thisTransform == null)
			{
				return;
			}
			float scale = this.skeletonGraphic.MeshScale;
			Vector2 offset = this.skeletonGraphic.MeshOffset;
			float additionalFlipScale = 1f;
			if (this.skeletonTransformIsParent)
			{
				thisTransform.localPosition = new Vector3(this.followXYPosition ? (this.bone.WorldX * scale + offset.x) : thisTransform.localPosition.x, this.followXYPosition ? (this.bone.WorldY * scale + offset.y) : thisTransform.localPosition.y, this.followZPosition ? 0f : thisTransform.localPosition.z);
				if (this.followBoneRotation)
				{
					thisTransform.localRotation = this.bone.GetQuaternion();
				}
			}
			else
			{
				Vector3 targetWorldPosition = this.skeletonTransform.TransformPoint(new Vector3(this.bone.WorldX * scale + offset.x, this.bone.WorldY * scale + offset.y, 0f));
				if (!this.followZPosition)
				{
					targetWorldPosition.z = thisTransform.position.z;
				}
				if (!this.followXYPosition)
				{
					targetWorldPosition.x = thisTransform.position.x;
					targetWorldPosition.y = thisTransform.position.y;
				}
				Vector3 skeletonLossyScale = this.skeletonTransform.lossyScale;
				Transform transformParent = thisTransform.parent;
				Vector3 parentLossyScale = ((transformParent != null) ? transformParent.lossyScale : Vector3.one);
				if (this.followBoneRotation)
				{
					float boneWorldRotation = this.bone.WorldRotationX;
					if (skeletonLossyScale.x * skeletonLossyScale.y < 0f)
					{
						boneWorldRotation = -boneWorldRotation;
					}
					if (this.followSkeletonFlip || this.maintainedAxisOrientation == BoneFollower.AxisOrientation.XAxis)
					{
						if (skeletonLossyScale.x * parentLossyScale.x < 0f)
						{
							boneWorldRotation += 180f;
						}
					}
					else if (skeletonLossyScale.y * parentLossyScale.y < 0f)
					{
						boneWorldRotation += 180f;
					}
					Vector3 worldRotation = this.skeletonTransform.rotation.eulerAngles;
					if (this.followLocalScale && this.bone.ScaleX < 0f)
					{
						boneWorldRotation += 180f;
					}
					thisTransform.SetPositionAndRotation(targetWorldPosition, Quaternion.Euler(worldRotation.x, worldRotation.y, worldRotation.z + boneWorldRotation));
				}
				else
				{
					thisTransform.position = targetWorldPosition;
				}
				additionalFlipScale = Mathf.Sign(skeletonLossyScale.x * parentLossyScale.x * skeletonLossyScale.y * parentLossyScale.y);
			}
			Bone parentBone = this.bone.Parent;
			if (this.followParentWorldScale || this.followLocalScale || this.followSkeletonFlip)
			{
				Vector3 localScale = new Vector3(1f, 1f, 1f);
				if (this.followParentWorldScale && parentBone != null)
				{
					localScale = new Vector3(parentBone.WorldScaleX, parentBone.WorldScaleY, 1f);
				}
				if (this.followLocalScale)
				{
					localScale.Scale(new Vector3(this.bone.ScaleX, this.bone.ScaleY, 1f));
				}
				if (this.followSkeletonFlip)
				{
					localScale.y *= Mathf.Sign(this.bone.Skeleton.ScaleX * this.bone.Skeleton.ScaleY) * additionalFlipScale;
				}
				thisTransform.localScale = localScale;
			}
		}

		// Token: 0x04000065 RID: 101
		public SkeletonGraphic skeletonGraphic;

		// Token: 0x04000066 RID: 102
		public bool initializeOnAwake = true;

		// Token: 0x04000067 RID: 103
		[SpineBone("", "skeletonGraphic", true, false)]
		public string boneName;

		// Token: 0x04000068 RID: 104
		public bool followBoneRotation = true;

		// Token: 0x04000069 RID: 105
		[Tooltip("Follows the skeleton's flip state by controlling this Transform's local scale.")]
		public bool followSkeletonFlip = true;

		// Token: 0x0400006A RID: 106
		[Tooltip("Follows the target bone's local scale.")]
		public bool followLocalScale;

		// Token: 0x0400006B RID: 107
		[Tooltip("Includes the parent bone's lossy world scale. BoneFollower cannot inherit rotated/skewed scale because of UnityEngine.Transform property limitations.")]
		public bool followParentWorldScale;

		// Token: 0x0400006C RID: 108
		public bool followXYPosition = true;

		// Token: 0x0400006D RID: 109
		public bool followZPosition = true;

		// Token: 0x0400006E RID: 110
		[Tooltip("Applies when 'Follow Skeleton Flip' is disabled but 'Follow Bone Rotation' is enabled. When flipping the skeleton by scaling its Transform, this follower's rotation is adjusted instead of its scale to follow the bone orientation. When one of the axes is flipped,  only one axis can be followed, either the X or the Y axis, which is selected here.")]
		public BoneFollower.AxisOrientation maintainedAxisOrientation = BoneFollower.AxisOrientation.XAxis;

		// Token: 0x0400006F RID: 111
		[NonSerialized]
		public Bone bone;

		// Token: 0x04000070 RID: 112
		private Transform skeletonTransform;

		// Token: 0x04000071 RID: 113
		private bool skeletonTransformIsParent;

		// Token: 0x04000072 RID: 114
		[NonSerialized]
		public bool valid;
	}
}
