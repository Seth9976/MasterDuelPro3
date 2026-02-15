using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000F RID: 15
	[UsedByNativeCode]
	public readonly struct ContactPairHeader
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002EAC File Offset: 0x000010AC
		public Component body
		{
			get
			{
				return Physics.GetBodyByInstanceID(this.m_BodyID);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002EB9 File Offset: 0x000010B9
		public Component otherBody
		{
			get
			{
				return Physics.GetBodyByInstanceID(this.m_OtherBodyID);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002EC6 File Offset: 0x000010C6
		internal bool hasRemovedBody
		{
			get
			{
				return (this.m_Flags & CollisionPairHeaderFlags.RemovedActor) != (CollisionPairHeaderFlags)0 || (this.m_Flags & CollisionPairHeaderFlags.RemovedOtherActor) > (CollisionPairHeaderFlags)0;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002EE0 File Offset: 0x000010E0
		public readonly ref ContactPair GetContactPair(int index)
		{
			return this.GetContactPair_Internal(index);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002EFC File Offset: 0x000010FC
		internal unsafe ContactPair* GetContactPair_Internal(int index)
		{
			bool flag = (long)index >= (long)((ulong)this.m_NbPairs);
			if (flag)
			{
				throw new IndexOutOfRangeException("Invalid ContactPair index. Index should be greater than 0 and less than ContactPairHeader.PairCount");
			}
			return this.m_StartPtr.ToInt64() / (long)sizeof(ContactPair) + index * sizeof(ContactPair);
		}

		// Token: 0x0400002C RID: 44
		internal readonly int m_BodyID;

		// Token: 0x0400002D RID: 45
		internal readonly int m_OtherBodyID;

		// Token: 0x0400002E RID: 46
		internal readonly IntPtr m_StartPtr;

		// Token: 0x0400002F RID: 47
		internal readonly uint m_NbPairs;

		// Token: 0x04000030 RID: 48
		internal readonly CollisionPairHeaderFlags m_Flags;

		// Token: 0x04000031 RID: 49
		internal readonly Vector3 m_RelativeVelocity;
	}
}
