using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000010 RID: 16
	[UsedByNativeCode]
	public readonly struct ContactPair
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002F41 File Offset: 0x00001141
		public Collider collider
		{
			get
			{
				return (this.m_ColliderID == 0) ? null : Physics.GetColliderByInstanceID(this.m_ColliderID);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002F59 File Offset: 0x00001159
		public Collider otherCollider
		{
			get
			{
				return (this.m_OtherColliderID == 0) ? null : Physics.GetColliderByInstanceID(this.m_OtherColliderID);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002F71 File Offset: 0x00001171
		public bool isCollisionEnter
		{
			get
			{
				return (this.m_Events & CollisionPairEventFlags.NotifyTouchFound) > (CollisionPairEventFlags)0;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002F7E File Offset: 0x0000117E
		public bool isCollisionExit
		{
			get
			{
				return (this.m_Events & CollisionPairEventFlags.NotifyTouchLost) > (CollisionPairEventFlags)0;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002F8C File Offset: 0x0000118C
		public bool isCollisionStay
		{
			get
			{
				return (this.m_Events & CollisionPairEventFlags.NotifyTouchPersists) > (CollisionPairEventFlags)0;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002F99 File Offset: 0x00001199
		internal bool hasRemovedCollider
		{
			get
			{
				return (this.m_Flags & CollisionPairFlags.RemovedShape) != (CollisionPairFlags)0 || (this.m_Flags & CollisionPairFlags.RemovedOtherShape) > (CollisionPairFlags)0;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002FB4 File Offset: 0x000011B4
		internal int ExtractContactsArray(ContactPoint[] managedContainer, bool flipped)
		{
			int size = (int)Math.Min((long)managedContainer.Length, (long)((ulong)this.m_NbPoints));
			for (int i = 0; i < size; i++)
			{
				readonly ref ContactPairPoint nativePoint = ref this.GetContactPoint(i);
				ContactPoint contactPoint = new ContactPoint
				{
					m_Point = nativePoint.position,
					m_Impulse = nativePoint.impulse,
					m_Separation = nativePoint.separation
				};
				if (flipped)
				{
					contactPoint.m_Normal = -nativePoint.normal;
					contactPoint.m_ThisColliderInstanceID = this.m_OtherColliderID;
					contactPoint.m_OtherColliderInstanceID = this.m_ColliderID;
				}
				else
				{
					contactPoint.m_Normal = nativePoint.normal;
					contactPoint.m_ThisColliderInstanceID = this.m_ColliderID;
					contactPoint.m_OtherColliderInstanceID = this.m_OtherColliderID;
				}
				managedContainer[i] = contactPoint;
			}
			return size;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003098 File Offset: 0x00001298
		public readonly ref ContactPairPoint GetContactPoint(int index)
		{
			return this.GetContactPoint_Internal(index);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000030B4 File Offset: 0x000012B4
		internal unsafe ContactPairPoint* GetContactPoint_Internal(int index)
		{
			bool flag = (long)index >= (long)((ulong)this.m_NbPoints);
			if (flag)
			{
				throw new IndexOutOfRangeException("Invalid ContactPairPoint index. Index should be greater than 0 and less than ContactPair.ContactCount");
			}
			return this.m_StartPtr.ToInt64() / (long)sizeof(ContactPairPoint) + index * sizeof(ContactPairPoint);
		}

		// Token: 0x04000032 RID: 50
		internal readonly int m_ColliderID;

		// Token: 0x04000033 RID: 51
		internal readonly int m_OtherColliderID;

		// Token: 0x04000034 RID: 52
		internal readonly IntPtr m_StartPtr;

		// Token: 0x04000035 RID: 53
		internal readonly uint m_NbPoints;

		// Token: 0x04000036 RID: 54
		internal readonly CollisionPairFlags m_Flags;

		// Token: 0x04000037 RID: 55
		internal readonly CollisionPairEventFlags m_Events;

		// Token: 0x04000038 RID: 56
		internal readonly Vector3 m_ImpulseSum;
	}
}
