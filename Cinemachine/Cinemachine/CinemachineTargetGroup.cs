using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200003E RID: 62
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("Cinemachine/CinemachineTargetGroup")]
	[SaveDuringPlay]
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineTargetGroup.html")]
	public class CinemachineTargetGroup : MonoBehaviour, ICinemachineTargetGroup
	{
		// Token: 0x0600016F RID: 367 RVA: 0x0000AA28 File Offset: 0x00008C28
		private void OnValidate()
		{
			int count = ((this.m_Targets == null) ? 0 : this.m_Targets.Length);
			for (int i = 0; i < count; i++)
			{
				this.m_Targets[i].weight = Mathf.Max(0f, this.m_Targets[i].weight);
				this.m_Targets[i].radius = Mathf.Max(0f, this.m_Targets[i].radius);
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000AAAD File Offset: 0x00008CAD
		private void Reset()
		{
			this.m_PositionMode = CinemachineTargetGroup.PositionMode.GroupCenter;
			this.m_RotationMode = CinemachineTargetGroup.RotationMode.Manual;
			this.m_UpdateMethod = CinemachineTargetGroup.UpdateMethod.LateUpdate;
			this.m_Targets = Array.Empty<CinemachineTargetGroup.Target>();
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000AACF File Offset: 0x00008CCF
		public Transform Transform
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000AAD7 File Offset: 0x00008CD7
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000AAF2 File Offset: 0x00008CF2
		public Bounds BoundingBox
		{
			get
			{
				if (this.m_LastUpdateFrame != Time.frameCount)
				{
					this.DoUpdate();
				}
				return this.m_BoundingBox;
			}
			private set
			{
				this.m_BoundingBox = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000AAFB File Offset: 0x00008CFB
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000AB16 File Offset: 0x00008D16
		public BoundingSphere Sphere
		{
			get
			{
				if (this.m_LastUpdateFrame != Time.frameCount)
				{
					this.DoUpdate();
				}
				return this.m_BoundingSphere;
			}
			private set
			{
				this.m_BoundingSphere = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000AB1F File Offset: 0x00008D1F
		public bool IsEmpty
		{
			get
			{
				if (this.m_LastUpdateFrame != Time.frameCount)
				{
					this.DoUpdate();
				}
				return this.m_ValidMembers.Count == 0;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000AB44 File Offset: 0x00008D44
		public void AddMember(Transform t, float weight, float radius)
		{
			int index = 0;
			if (this.m_Targets == null)
			{
				this.m_Targets = new CinemachineTargetGroup.Target[1];
			}
			else
			{
				index = this.m_Targets.Length;
				Array targets = this.m_Targets;
				this.m_Targets = new CinemachineTargetGroup.Target[index + 1];
				Array.Copy(targets, this.m_Targets, index);
			}
			this.m_Targets[index].target = t;
			this.m_Targets[index].weight = weight;
			this.m_Targets[index].radius = radius;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		public void RemoveMember(Transform t)
		{
			int index = this.FindMember(t);
			if (index >= 0)
			{
				CinemachineTargetGroup.Target[] oldTargets = this.m_Targets;
				this.m_Targets = new CinemachineTargetGroup.Target[this.m_Targets.Length - 1];
				if (index > 0)
				{
					Array.Copy(oldTargets, this.m_Targets, index);
				}
				if (index < oldTargets.Length - 1)
				{
					Array.Copy(oldTargets, index + 1, this.m_Targets, index, oldTargets.Length - index - 1);
				}
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000AC30 File Offset: 0x00008E30
		public int FindMember(Transform t)
		{
			if (this.m_Targets != null)
			{
				for (int i = this.m_Targets.Length - 1; i >= 0; i--)
				{
					if (this.m_Targets[i].target == t)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000AC78 File Offset: 0x00008E78
		public BoundingSphere GetWeightedBoundsForMember(int index)
		{
			if (this.m_LastUpdateFrame != Time.frameCount)
			{
				this.DoUpdate();
			}
			if (!this.IndexIsValid(index) || !this.m_MemberValidity[index])
			{
				return this.Sphere;
			}
			return CinemachineTargetGroup.WeightedMemberBoundsForValidMember(ref this.m_Targets[index], this.m_AveragePos, this.m_MaxWeight);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000ACD4 File Offset: 0x00008ED4
		public Bounds GetViewSpaceBoundingBox(Matrix4x4 observer)
		{
			if (this.m_LastUpdateFrame != Time.frameCount)
			{
				this.DoUpdate();
			}
			Matrix4x4 inverseView = observer;
			if (!Matrix4x4.Inverse3DAffine(observer, ref inverseView))
			{
				inverseView = observer.inverse;
			}
			Bounds b = new Bounds(inverseView.MultiplyPoint3x4(this.m_AveragePos), Vector3.zero);
			if (this.CachedCountIsValid)
			{
				bool gotOne = false;
				Vector3 unit = 2f * Vector3.one;
				int count = this.m_ValidMembers.Count;
				for (int i = 0; i < count; i++)
				{
					BoundingSphere s = CinemachineTargetGroup.WeightedMemberBoundsForValidMember(ref this.m_Targets[this.m_ValidMembers[i]], this.m_AveragePos, this.m_MaxWeight);
					s.position = inverseView.MultiplyPoint3x4(s.position);
					if (gotOne)
					{
						b.Encapsulate(new Bounds(s.position, s.radius * unit));
					}
					else
					{
						b = new Bounds(s.position, s.radius * unit);
					}
					gotOne = true;
				}
			}
			return b;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000ADE5 File Offset: 0x00008FE5
		private bool CachedCountIsValid
		{
			get
			{
				return this.m_MemberValidity.Count == ((this.m_Targets == null) ? 0 : this.m_Targets.Length);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000AE07 File Offset: 0x00009007
		private bool IndexIsValid(int index)
		{
			return index >= 0 && this.m_Targets != null && index < this.m_Targets.Length && this.CachedCountIsValid;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000AE28 File Offset: 0x00009028
		private static BoundingSphere WeightedMemberBoundsForValidMember(ref CinemachineTargetGroup.Target t, Vector3 avgPos, float maxWeight)
		{
			Vector3 pos = ((t.target == null) ? avgPos : TargetPositionCache.GetTargetPosition(t.target));
			float w = Mathf.Max(0f, t.weight);
			if (maxWeight > 0.0001f && w < maxWeight)
			{
				w /= maxWeight;
			}
			else
			{
				w = 1f;
			}
			return new BoundingSphere(Vector3.Lerp(avgPos, pos, w), t.radius * w);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000AE90 File Offset: 0x00009090
		public void DoUpdate()
		{
			this.m_LastUpdateFrame = Time.frameCount;
			this.UpdateMemberValidity();
			this.m_AveragePos = this.CalculateAveragePosition();
			this.BoundingBox = this.CalculateBoundingBox();
			this.m_BoundingSphere = this.CalculateBoundingSphere();
			CinemachineTargetGroup.PositionMode positionMode = this.m_PositionMode;
			if (positionMode != CinemachineTargetGroup.PositionMode.GroupCenter)
			{
				if (positionMode == CinemachineTargetGroup.PositionMode.GroupAverage)
				{
					base.transform.position = this.m_AveragePos;
				}
			}
			else
			{
				base.transform.position = this.Sphere.position;
			}
			CinemachineTargetGroup.RotationMode rotationMode = this.m_RotationMode;
			if (rotationMode != CinemachineTargetGroup.RotationMode.Manual && rotationMode == CinemachineTargetGroup.RotationMode.GroupAverage)
			{
				base.transform.rotation = this.CalculateAverageOrientation();
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000AF2C File Offset: 0x0000912C
		private void UpdateMemberValidity()
		{
			int count = ((this.m_Targets == null) ? 0 : this.m_Targets.Length);
			this.m_ValidMembers.Clear();
			this.m_ValidMembers.Capacity = Mathf.Max(this.m_ValidMembers.Capacity, count);
			this.m_MemberValidity.Clear();
			this.m_MemberValidity.Capacity = Mathf.Max(this.m_MemberValidity.Capacity, count);
			this.m_WeightSum = (this.m_MaxWeight = 0f);
			for (int i = 0; i < count; i++)
			{
				this.m_MemberValidity.Add(this.m_Targets[i].target != null && this.m_Targets[i].weight > 0.0001f && this.m_Targets[i].target.gameObject.activeInHierarchy);
				if (this.m_MemberValidity[i])
				{
					this.m_ValidMembers.Add(i);
					this.m_MaxWeight = Mathf.Max(this.m_MaxWeight, this.m_Targets[i].weight);
					this.m_WeightSum += this.m_Targets[i].weight;
				}
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000B078 File Offset: 0x00009278
		private Vector3 CalculateAveragePosition()
		{
			if (this.m_WeightSum < 0.0001f)
			{
				return base.transform.position;
			}
			Vector3 pos = Vector3.zero;
			int count = this.m_ValidMembers.Count;
			for (int i = 0; i < count; i++)
			{
				int targetIndex = this.m_ValidMembers[i];
				float weight = this.m_Targets[targetIndex].weight;
				pos += TargetPositionCache.GetTargetPosition(this.m_Targets[targetIndex].target) * weight;
			}
			return pos / this.m_WeightSum;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000B10C File Offset: 0x0000930C
		private Bounds CalculateBoundingBox()
		{
			if (this.m_MaxWeight < 0.0001f)
			{
				return this.BoundingBox;
			}
			Bounds b = new Bounds(this.m_AveragePos, Vector3.zero);
			int count = this.m_ValidMembers.Count;
			for (int i = 0; i < count; i++)
			{
				BoundingSphere s = CinemachineTargetGroup.WeightedMemberBoundsForValidMember(ref this.m_Targets[this.m_ValidMembers[i]], this.m_AveragePos, this.m_MaxWeight);
				b.Encapsulate(new Bounds(s.position, s.radius * 2f * Vector3.one));
			}
			return b;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000B1AC File Offset: 0x000093AC
		private BoundingSphere CalculateBoundingSphere()
		{
			int count = this.m_ValidMembers.Count;
			if (count == 0 || this.m_MaxWeight < 0.0001f)
			{
				return this.m_BoundingSphere;
			}
			BoundingSphere sphere = CinemachineTargetGroup.WeightedMemberBoundsForValidMember(ref this.m_Targets[this.m_ValidMembers[0]], this.m_AveragePos, this.m_MaxWeight);
			for (int i = 1; i < count; i++)
			{
				BoundingSphere s = CinemachineTargetGroup.WeightedMemberBoundsForValidMember(ref this.m_Targets[this.m_ValidMembers[i]], this.m_AveragePos, this.m_MaxWeight);
				float distance = (s.position - sphere.position).magnitude + s.radius;
				if (distance > sphere.radius)
				{
					sphere.radius = (sphere.radius + distance) * 0.5f;
					sphere.position = (sphere.radius * sphere.position + (distance - sphere.radius) * s.position) / distance;
				}
			}
			return sphere;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000B2BC File Offset: 0x000094BC
		private Quaternion CalculateAverageOrientation()
		{
			if (this.m_WeightSum > 0.001f)
			{
				Vector3 averageForward = Vector3.zero;
				Vector3 averageUp = Vector3.zero;
				int count = this.m_ValidMembers.Count;
				for (int i = 0; i < count; i++)
				{
					int targetIndex = this.m_ValidMembers[i];
					float scaledWeight = this.m_Targets[targetIndex].weight / this.m_WeightSum;
					Quaternion rot = TargetPositionCache.GetTargetRotation(this.m_Targets[targetIndex].target);
					averageForward += rot * Vector3.forward * scaledWeight;
					averageUp += rot * Vector3.up * scaledWeight;
				}
				if (averageForward.sqrMagnitude > 0.0001f && averageUp.sqrMagnitude > 0.0001f)
				{
					return Quaternion.LookRotation(averageForward, averageUp);
				}
			}
			return base.transform.rotation;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000B3A2 File Offset: 0x000095A2
		private void FixedUpdate()
		{
			if (this.m_UpdateMethod == CinemachineTargetGroup.UpdateMethod.FixedUpdate)
			{
				this.DoUpdate();
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000B3B3 File Offset: 0x000095B3
		private void Update()
		{
			if (!Application.isPlaying || this.m_UpdateMethod == CinemachineTargetGroup.UpdateMethod.Update)
			{
				this.DoUpdate();
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000B3CA File Offset: 0x000095CA
		private void LateUpdate()
		{
			if (this.m_UpdateMethod == CinemachineTargetGroup.UpdateMethod.LateUpdate)
			{
				this.DoUpdate();
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000B3DC File Offset: 0x000095DC
		public void GetViewSpaceAngularBounds(Matrix4x4 observer, out Vector2 minAngles, out Vector2 maxAngles, out Vector2 zRange)
		{
			if (this.m_LastUpdateFrame != Time.frameCount)
			{
				this.DoUpdate();
			}
			Matrix4x4 world2local = observer;
			if (!Matrix4x4.Inverse3DAffine(observer, ref world2local))
			{
				world2local = observer.inverse;
			}
			float r = this.m_BoundingSphere.radius;
			Bounds b = new Bounds
			{
				center = world2local.MultiplyPoint3x4(this.m_AveragePos),
				extents = new Vector3(r, r, r)
			};
			zRange = new Vector2(b.center.z - r, b.center.z + r);
			if (this.CachedCountIsValid)
			{
				bool haveOne = false;
				int count = this.m_ValidMembers.Count;
				for (int i = 0; i < count; i++)
				{
					BoundingSphere s = CinemachineTargetGroup.WeightedMemberBoundsForValidMember(ref this.m_Targets[this.m_ValidMembers[i]], this.m_AveragePos, this.m_MaxWeight);
					Vector3 p = world2local.MultiplyPoint3x4(s.position);
					if (p.z >= 0.0001f)
					{
						float rN = s.radius / p.z;
						Vector3 rN2 = new Vector3(rN, rN, 0f);
						Vector3 pN = p / p.z;
						if (!haveOne)
						{
							b.center = pN;
							b.extents = rN2;
							zRange = new Vector2(p.z, p.z);
							haveOne = true;
						}
						else
						{
							b.Encapsulate(pN + rN2);
							b.Encapsulate(pN - rN2);
							zRange.x = Mathf.Min(zRange.x, p.z);
							zRange.y = Mathf.Max(zRange.y, p.z);
						}
					}
				}
			}
			Vector3 pMin = b.min;
			Vector3 pMax = b.max;
			minAngles = new Vector2(Vector3.SignedAngle(Vector3.forward, new Vector3(0f, pMin.y, 1f), Vector3.left), Vector3.SignedAngle(Vector3.forward, new Vector3(pMin.x, 0f, 1f), Vector3.up));
			maxAngles = new Vector2(Vector3.SignedAngle(Vector3.forward, new Vector3(0f, pMax.y, 1f), Vector3.left), Vector3.SignedAngle(Vector3.forward, new Vector3(pMax.x, 0f, 1f), Vector3.up));
		}

		// Token: 0x0400013A RID: 314
		[Tooltip("How the group's position is calculated.  Select GroupCenter for the center of the bounding box, and GroupAverage for a weighted average of the positions of the members.")]
		public CinemachineTargetGroup.PositionMode m_PositionMode;

		// Token: 0x0400013B RID: 315
		[Tooltip("How the group's rotation is calculated.  Select Manual to use the value in the group's transform, and GroupAverage for a weighted average of the orientations of the members.")]
		public CinemachineTargetGroup.RotationMode m_RotationMode;

		// Token: 0x0400013C RID: 316
		[Tooltip("When to update the group's transform based on the position of the group members")]
		public CinemachineTargetGroup.UpdateMethod m_UpdateMethod = CinemachineTargetGroup.UpdateMethod.LateUpdate;

		// Token: 0x0400013D RID: 317
		[NoSaveDuringPlay]
		[Tooltip("The target objects, together with their weights and radii, that will contribute to the group's average position, orientation, and size.")]
		public CinemachineTargetGroup.Target[] m_Targets = Array.Empty<CinemachineTargetGroup.Target>();

		// Token: 0x0400013E RID: 318
		private float m_MaxWeight;

		// Token: 0x0400013F RID: 319
		private float m_WeightSum;

		// Token: 0x04000140 RID: 320
		private Vector3 m_AveragePos;

		// Token: 0x04000141 RID: 321
		private Bounds m_BoundingBox;

		// Token: 0x04000142 RID: 322
		private BoundingSphere m_BoundingSphere;

		// Token: 0x04000143 RID: 323
		private int m_LastUpdateFrame = -1;

		// Token: 0x04000144 RID: 324
		private List<int> m_ValidMembers = new List<int>();

		// Token: 0x04000145 RID: 325
		private List<bool> m_MemberValidity = new List<bool>();

		// Token: 0x0200003F RID: 63
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		[Serializable]
		public struct Target
		{
			// Token: 0x04000146 RID: 326
			[Tooltip("The target objects.  This object's position and orientation will contribute to the group's average position and orientation, in accordance with its weight")]
			public Transform target;

			// Token: 0x04000147 RID: 327
			[Tooltip("How much weight to give the target when averaging.  Cannot be negative")]
			public float weight;

			// Token: 0x04000148 RID: 328
			[Tooltip("The radius of the target, used for calculating the bounding box.  Cannot be negative")]
			public float radius;
		}

		// Token: 0x02000040 RID: 64
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		public enum PositionMode
		{
			// Token: 0x0400014A RID: 330
			GroupCenter,
			// Token: 0x0400014B RID: 331
			GroupAverage
		}

		// Token: 0x02000041 RID: 65
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		public enum RotationMode
		{
			// Token: 0x0400014D RID: 333
			Manual,
			// Token: 0x0400014E RID: 334
			GroupAverage
		}

		// Token: 0x02000042 RID: 66
		public enum UpdateMethod
		{
			// Token: 0x04000150 RID: 336
			Update,
			// Token: 0x04000151 RID: 337
			FixedUpdate,
			// Token: 0x04000152 RID: 338
			LateUpdate
		}
	}
}
