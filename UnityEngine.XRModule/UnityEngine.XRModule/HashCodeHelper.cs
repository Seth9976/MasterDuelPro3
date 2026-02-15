using System;

namespace UnityEngine.XR
{
	// Token: 0x0200001D RID: 29
	internal static class HashCodeHelper
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002ADC File Offset: 0x00000CDC
		public static int Combine(int hash1, int hash2)
		{
			return hash1 * 486187739 + hash2;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public static int Combine(int hash1, int hash2, int hash3)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2), hash3);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B07 File Offset: 0x00000D07
		public static int Combine(int hash1, int hash2, int hash3, int hash4)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2, hash3), hash4);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002B17 File Offset: 0x00000D17
		public static int Combine(int hash1, int hash2, int hash3, int hash4, int hash5)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2, hash3, hash4), hash5);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002B29 File Offset: 0x00000D29
		public static int Combine(int hash1, int hash2, int hash3, int hash4, int hash5, int hash6)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2, hash3, hash4, hash5), hash6);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002B3D File Offset: 0x00000D3D
		public static int Combine(int hash1, int hash2, int hash3, int hash4, int hash5, int hash6, int hash7)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2, hash3, hash4, hash5, hash6), hash7);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002B53 File Offset: 0x00000D53
		public static int Combine(int hash1, int hash2, int hash3, int hash4, int hash5, int hash6, int hash7, int hash8)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2, hash3, hash4, hash5, hash6, hash7), hash8);
		}
	}
}
