using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000023 RID: 35
	[ExecuteAlways]
	[AddComponentMenu("Spine/Point Follower")]
	[HelpURL("http://esotericsoftware.com/spine-unity#PointFollower")]
	public class PointFollower : MonoBehaviour, IHasSkeletonRenderer, ISpineComponent, IHasSkeletonComponent
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005B91 File Offset: 0x00003D91
		public SkeletonRenderer SkeletonRenderer
		{
			get
			{
				return this.skeletonRenderer;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00005B91 File Offset: 0x00003D91
		public ISkeletonComponent SkeletonComponent
		{
			get
			{
				return this.skeletonRenderer;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00005B99 File Offset: 0x00003D99
		public bool IsValid
		{
			get
			{
				return this.valid;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005BA1 File Offset: 0x00003DA1
		public void Initialize()
		{
			this.valid = this.skeletonRenderer != null && this.skeletonRenderer.valid;
			if (!this.valid)
			{
				return;
			}
			this.UpdateReferences();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005BD4 File Offset: 0x00003DD4
		private void HandleRebuildRenderer(SkeletonRenderer skeletonRenderer)
		{
			this.Initialize();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005BDC File Offset: 0x00003DDC
		private void UpdateReferences()
		{
			this.skeletonTransform = this.skeletonRenderer.transform;
			this.skeletonRenderer.OnRebuild -= this.HandleRebuildRenderer;
			this.skeletonRenderer.OnRebuild += this.HandleRebuildRenderer;
			this.skeletonTransformIsParent = this.skeletonTransform == base.transform.parent;
			this.bone = null;
			this.point = null;
			if (!string.IsNullOrEmpty(this.pointAttachmentName))
			{
				Skeleton skeleton = this.skeletonRenderer.Skeleton;
				Slot slot = skeleton.FindSlot(this.slotName);
				if (slot != null)
				{
					int slotIndex = slot.Data.Index;
					this.bone = slot.Bone;
					this.point = skeleton.GetAttachment(slotIndex, this.pointAttachmentName) as PointAttachment;
				}
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005CA8 File Offset: 0x00003EA8
		private void OnDestroy()
		{
			if (this.skeletonRenderer != null)
			{
				this.skeletonRenderer.OnRebuild -= this.HandleRebuildRenderer;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005CD0 File Offset: 0x00003ED0
		public void LateUpdate()
		{
			if (this.point == null)
			{
				if (string.IsNullOrEmpty(this.pointAttachmentName))
				{
					return;
				}
				this.UpdateReferences();
				if (this.point == null)
				{
					return;
				}
			}
			Vector2 worldPos;
			this.point.ComputeWorldPosition(this.bone, out worldPos.x, out worldPos.y);
			float rotation = this.point.ComputeWorldRotation(this.bone);
			Transform thisTransform = base.transform;
			if (this.skeletonTransformIsParent)
			{
				thisTransform.localPosition = new Vector3(worldPos.x, worldPos.y, this.followSkeletonZPosition ? 0f : thisTransform.localPosition.z);
				if (this.followRotation)
				{
					float halfRotation = rotation * 0.5f * 0.017453292f;
					thisTransform.localRotation = new Quaternion
					{
						z = Mathf.Sin(halfRotation),
						w = Mathf.Cos(halfRotation)
					};
				}
			}
			else
			{
				Vector3 targetWorldPosition = this.skeletonTransform.TransformPoint(new Vector3(worldPos.x, worldPos.y, 0f));
				if (!this.followSkeletonZPosition)
				{
					targetWorldPosition.z = thisTransform.position.z;
				}
				Transform transformParent = thisTransform.parent;
				if (transformParent != null)
				{
					Matrix4x4 i = transformParent.localToWorldMatrix;
					if (i.m00 * i.m11 - i.m01 * i.m10 < 0f)
					{
						rotation = -rotation;
					}
				}
				if (this.followRotation)
				{
					Vector3 transformWorldRotation = this.skeletonTransform.rotation.eulerAngles;
					thisTransform.SetPositionAndRotation(targetWorldPosition, Quaternion.Euler(transformWorldRotation.x, transformWorldRotation.y, transformWorldRotation.z + rotation));
				}
				else
				{
					thisTransform.position = targetWorldPosition;
				}
			}
			if (this.followSkeletonFlip)
			{
				Vector3 localScale = thisTransform.localScale;
				localScale.y = Mathf.Abs(localScale.y) * Mathf.Sign(this.bone.Skeleton.ScaleX * this.bone.Skeleton.ScaleY);
				thisTransform.localScale = localScale;
			}
		}

		// Token: 0x0400008D RID: 141
		public SkeletonRenderer skeletonRenderer;

		// Token: 0x0400008E RID: 142
		[SpineSlot("", "skeletonRenderer", false, true, false)]
		public string slotName;

		// Token: 0x0400008F RID: 143
		[SpineAttachment(true, false, false, "slotName", "skeletonRenderer", "", true, true)]
		public string pointAttachmentName;

		// Token: 0x04000090 RID: 144
		public bool followRotation = true;

		// Token: 0x04000091 RID: 145
		public bool followSkeletonFlip = true;

		// Token: 0x04000092 RID: 146
		public bool followSkeletonZPosition;

		// Token: 0x04000093 RID: 147
		private Transform skeletonTransform;

		// Token: 0x04000094 RID: 148
		private bool skeletonTransformIsParent;

		// Token: 0x04000095 RID: 149
		private PointAttachment point;

		// Token: 0x04000096 RID: 150
		private Bone bone;

		// Token: 0x04000097 RID: 151
		private bool valid;
	}
}
