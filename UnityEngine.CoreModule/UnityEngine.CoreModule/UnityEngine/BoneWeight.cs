using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200012A RID: 298
	[UsedByNativeCode]
	[Serializable]
	public struct BoneWeight : IEquatable<BoneWeight>
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x000140C8 File Offset: 0x000122C8
		public float weight0
		{
			get
			{
				return this.m_Weight0;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x000140E0 File Offset: 0x000122E0
		public float weight1
		{
			get
			{
				return this.m_Weight1;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x000140F8 File Offset: 0x000122F8
		public float weight2
		{
			get
			{
				return this.m_Weight2;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00014110 File Offset: 0x00012310
		public float weight3
		{
			get
			{
				return this.m_Weight3;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00014128 File Offset: 0x00012328
		public int boneIndex0
		{
			get
			{
				return this.m_BoneIndex0;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00014140 File Offset: 0x00012340
		public int boneIndex1
		{
			get
			{
				return this.m_BoneIndex1;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00014158 File Offset: 0x00012358
		public int boneIndex2
		{
			get
			{
				return this.m_BoneIndex2;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00014170 File Offset: 0x00012370
		public int boneIndex3
		{
			get
			{
				return this.m_BoneIndex3;
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00014188 File Offset: 0x00012388
		public override int GetHashCode()
		{
			return this.boneIndex0.GetHashCode() ^ (this.boneIndex1.GetHashCode() << 2) ^ (this.boneIndex2.GetHashCode() >> 2) ^ (this.boneIndex3.GetHashCode() >> 1) ^ (this.weight0.GetHashCode() << 5) ^ (this.weight1.GetHashCode() << 4) ^ (this.weight2.GetHashCode() >> 4) ^ (this.weight3.GetHashCode() >> 3);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00014220 File Offset: 0x00012420
		public override bool Equals(object other)
		{
			return other is BoneWeight && this.Equals((BoneWeight)other);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001424C File Offset: 0x0001244C
		public bool Equals(BoneWeight other)
		{
			return this.boneIndex0.Equals(other.boneIndex0) && this.boneIndex1.Equals(other.boneIndex1) && this.boneIndex2.Equals(other.boneIndex2) && this.boneIndex3.Equals(other.boneIndex3) && new Vector4(this.weight0, this.weight1, this.weight2, this.weight3).Equals(new Vector4(other.weight0, other.weight1, other.weight2, other.weight3));
		}

		// Token: 0x040003F6 RID: 1014
		[SerializeField]
		private float m_Weight0;

		// Token: 0x040003F7 RID: 1015
		[SerializeField]
		private float m_Weight1;

		// Token: 0x040003F8 RID: 1016
		[SerializeField]
		private float m_Weight2;

		// Token: 0x040003F9 RID: 1017
		[SerializeField]
		private float m_Weight3;

		// Token: 0x040003FA RID: 1018
		[SerializeField]
		private int m_BoneIndex0;

		// Token: 0x040003FB RID: 1019
		[SerializeField]
		private int m_BoneIndex1;

		// Token: 0x040003FC RID: 1020
		[SerializeField]
		private int m_BoneIndex2;

		// Token: 0x040003FD RID: 1021
		[SerializeField]
		private int m_BoneIndex3;
	}
}
