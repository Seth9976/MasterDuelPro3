using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Spine.Unity
{
	// Token: 0x0200001E RID: 30
	[ExecuteAlways]
	[AddComponentMenu("Spine/BoneFollower")]
	[HelpURL("http://esotericsoftware.com/spine-unity#BoneFollower")]
	public class BoneFollower : MonoBehaviour
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004579 File Offset: 0x00002779
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00004581 File Offset: 0x00002781
		public SkeletonRenderer SkeletonRenderer
		{
			get
			{
				return this.skeletonRenderer;
			}
			set
			{
				this.skeletonRenderer = value;
				this.Initialize();
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004590 File Offset: 0x00002790
		public bool SetBone(string name)
		{
			this.bone = this.skeletonRenderer.skeleton.FindBone(name);
			if (this.bone == null)
			{
				Debug.LogError("Bone not found: " + name, this);
				return false;
			}
			this.boneName = name;
			return true;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000045CC File Offset: 0x000027CC
		public void Awake()
		{
			if (this.initializeOnAwake)
			{
				this.Initialize();
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000045DC File Offset: 0x000027DC
		public void HandleRebuildRenderer(SkeletonRenderer skeletonRenderer)
		{
			this.Initialize();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000045E4 File Offset: 0x000027E4
		public virtual void Initialize()
		{
			this.bone = null;
			this.valid = this.skeletonRenderer != null && this.skeletonRenderer.valid;
			if (!this.valid)
			{
				return;
			}
			this.skeletonTransform = this.skeletonRenderer.transform;
			this.skeletonRenderer.OnRebuild -= this.HandleRebuildRenderer;
			this.skeletonRenderer.OnRebuild += this.HandleRebuildRenderer;
			this.skeletonTransformIsParent = this.skeletonTransform == base.transform.parent;
			if (!string.IsNullOrEmpty(this.boneName))
			{
				this.bone = this.skeletonRenderer.skeleton.FindBone(this.boneName);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000046A4 File Offset: 0x000028A4
		private void OnDestroy()
		{
			if (this.skeletonRenderer != null)
			{
				this.skeletonRenderer.OnRebuild -= this.HandleRebuildRenderer;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000046CC File Offset: 0x000028CC
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
				this.bone = this.skeletonRenderer.skeleton.FindBone(this.boneName);
				if (!this.SetBone(this.boneName))
				{
					return;
				}
			}
			Transform thisTransform = base.transform;
			float additionalFlipScale = 1f;
			if (this.skeletonTransformIsParent)
			{
				thisTransform.localPosition = new Vector3(this.followXYPosition ? this.bone.WorldX : thisTransform.localPosition.x, this.followXYPosition ? this.bone.WorldY : thisTransform.localPosition.y, this.followZPosition ? 0f : thisTransform.localPosition.z);
				if (this.followBoneRotation)
				{
					float halfRotation = Mathf.Atan2(this.bone.C, this.bone.A) * 0.5f;
					if (this.followLocalScale && this.bone.ScaleX < 0f)
					{
						halfRotation += 1.5707964f;
					}
					thisTransform.localRotation = new Quaternion
					{
						z = Mathf.Sin(halfRotation),
						w = Mathf.Cos(halfRotation)
					};
				}
			}
			else
			{
				Vector3 targetWorldPosition = this.skeletonTransform.TransformPoint(new Vector3(this.bone.WorldX, this.bone.WorldY, 0f));
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

		// Token: 0x04000054 RID: 84
		public SkeletonRenderer skeletonRenderer;

		// Token: 0x04000055 RID: 85
		[SpineBone("", "skeletonRenderer", true, false)]
		public string boneName;

		// Token: 0x04000056 RID: 86
		public bool followXYPosition = true;

		// Token: 0x04000057 RID: 87
		public bool followZPosition = true;

		// Token: 0x04000058 RID: 88
		public bool followBoneRotation = true;

		// Token: 0x04000059 RID: 89
		[Tooltip("Follows the skeleton's flip state by controlling this Transform's local scale.")]
		public bool followSkeletonFlip = true;

		// Token: 0x0400005A RID: 90
		[Tooltip("Follows the target bone's local scale.")]
		[FormerlySerializedAs("followScale")]
		public bool followLocalScale;

		// Token: 0x0400005B RID: 91
		[Tooltip("Includes the parent bone's lossy world scale. BoneFollower cannot inherit rotated/skewed scale because of UnityEngine.Transform property limitations.")]
		public bool followParentWorldScale;

		// Token: 0x0400005C RID: 92
		[Tooltip("Applies when 'Follow Skeleton Flip' is disabled but 'Follow Bone Rotation' is enabled. When flipping the skeleton by scaling its Transform, this follower's rotation is adjusted instead of its scale to follow the bone orientation. When one of the axes is flipped,  only one axis can be followed, either the X or the Y axis, which is selected here.")]
		public BoneFollower.AxisOrientation maintainedAxisOrientation = BoneFollower.AxisOrientation.XAxis;

		// Token: 0x0400005D RID: 93
		[FormerlySerializedAs("resetOnAwake")]
		public bool initializeOnAwake = true;

		// Token: 0x0400005E RID: 94
		[NonSerialized]
		public bool valid;

		// Token: 0x0400005F RID: 95
		[NonSerialized]
		public Bone bone;

		// Token: 0x04000060 RID: 96
		private Transform skeletonTransform;

		// Token: 0x04000061 RID: 97
		private bool skeletonTransformIsParent;

		// Token: 0x0200001F RID: 31
		public enum AxisOrientation
		{
			// Token: 0x04000063 RID: 99
			XAxis = 1,
			// Token: 0x04000064 RID: 100
			YAxis
		}
	}
}
