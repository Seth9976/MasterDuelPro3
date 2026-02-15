using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Determines the set of valid key sizes for the symmetric cryptographic algorithms.</summary>
	// Token: 0x02000384 RID: 900
	[ComVisible(true)]
	public sealed class KeySizes
	{
		/// <summary>Specifies the minimum key size in bits.</summary>
		/// <returns>The minimum key size in bits.</returns>
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x0007C12A File Offset: 0x0007A32A
		public int MinSize
		{
			get
			{
				return this.m_minSize;
			}
		}

		/// <summary>Specifies the maximum key size in bits.</summary>
		/// <returns>The maximum key size in bits.</returns>
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0007C132 File Offset: 0x0007A332
		public int MaxSize
		{
			get
			{
				return this.m_maxSize;
			}
		}

		/// <summary>Specifies the interval between valid key sizes in bits.</summary>
		/// <returns>The interval between valid key sizes in bits.</returns>
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x0007C13A File Offset: 0x0007A33A
		public int SkipSize
		{
			get
			{
				return this.m_skipSize;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.KeySizes" /> class with the specified key values.</summary>
		/// <param name="minSize">The minimum valid key size. </param>
		/// <param name="maxSize">The maximum valid key size. </param>
		/// <param name="skipSize">The interval between valid key sizes. </param>
		// Token: 0x06001F40 RID: 8000 RVA: 0x0007C142 File Offset: 0x0007A342
		public KeySizes(int minSize, int maxSize, int skipSize)
		{
			this.m_minSize = minSize;
			this.m_maxSize = maxSize;
			this.m_skipSize = skipSize;
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x0007C160 File Offset: 0x0007A360
		internal bool IsLegal(int keySize)
		{
			int num = keySize - this.MinSize;
			bool flag = num >= 0 && keySize <= this.MaxSize;
			if (this.SkipSize != 0)
			{
				return flag && num % this.SkipSize == 0;
			}
			return flag;
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x0007C1A4 File Offset: 0x0007A3A4
		internal static bool IsLegalKeySize(KeySizes[] legalKeys, int size)
		{
			for (int i = 0; i < legalKeys.Length; i++)
			{
				if (legalKeys[i].IsLegal(size))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000EA8 RID: 3752
		private int m_minSize;

		// Token: 0x04000EA9 RID: 3753
		private int m_maxSize;

		// Token: 0x04000EAA RID: 3754
		private int m_skipSize;
	}
}
