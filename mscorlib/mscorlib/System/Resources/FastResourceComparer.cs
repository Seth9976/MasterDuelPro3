using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Resources
{
	// Token: 0x020005CC RID: 1484
	internal sealed class FastResourceComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
	{
		// Token: 0x06002BD6 RID: 11222 RVA: 0x000ACE71 File Offset: 0x000AB071
		public int GetHashCode(object key)
		{
			return FastResourceComparer.HashFunction((string)key);
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000ACE7E File Offset: 0x000AB07E
		public int GetHashCode(string key)
		{
			return FastResourceComparer.HashFunction(key);
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000ACE88 File Offset: 0x000AB088
		internal static int HashFunction(string key)
		{
			uint num = 5381U;
			for (int i = 0; i < key.Length; i++)
			{
				num = ((num << 5) + num) ^ (uint)key[i];
			}
			return (int)num;
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000ACEBC File Offset: 0x000AB0BC
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			string text = (string)a;
			string text2 = (string)b;
			return string.CompareOrdinal(text, text2);
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000321D5 File Offset: 0x000303D5
		public int Compare(string a, string b)
		{
			return string.CompareOrdinal(a, b);
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000321DE File Offset: 0x000303DE
		public bool Equals(string a, string b)
		{
			return string.Equals(a, b);
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000ACEE4 File Offset: 0x000AB0E4
		public bool Equals(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			string text = (string)a;
			string text2 = (string)b;
			return string.Equals(text, text2);
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x000ACF0C File Offset: 0x000AB10C
		public unsafe static int CompareOrdinal(string a, byte[] bytes, int bCharLength)
		{
			int num = 0;
			int num2 = 0;
			int num3 = a.Length;
			if (num3 > bCharLength)
			{
				num3 = bCharLength;
			}
			if (bCharLength == 0)
			{
				if (a.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				fixed (byte[] array = bytes)
				{
					byte* ptr;
					if (bytes == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					byte* ptr2 = ptr;
					while (num < num3 && num2 == 0)
					{
						int num4 = (int)(*ptr2) | ((int)ptr2[1] << 8);
						num2 = (int)a[num++] - num4;
						ptr2 += 2;
					}
				}
				if (num2 != 0)
				{
					return num2;
				}
				return a.Length - bCharLength;
			}
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x000ACF92 File Offset: 0x000AB192
		public static int CompareOrdinal(byte[] bytes, int aCharLength, string b)
		{
			return -FastResourceComparer.CompareOrdinal(b, bytes, aCharLength);
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x000ACFA0 File Offset: 0x000AB1A0
		internal unsafe static int CompareOrdinal(byte* a, int byteLen, string b)
		{
			int num = 0;
			int num2 = 0;
			int num3 = byteLen >> 1;
			if (num3 > b.Length)
			{
				num3 = b.Length;
			}
			while (num2 < num3 && num == 0)
			{
				num = (int)((char)((int)(*(a++)) | ((int)(*(a++)) << 8)) - b[num2++]);
			}
			if (num != 0)
			{
				return num;
			}
			return byteLen - b.Length * 2;
		}

		// Token: 0x0400164A RID: 5706
		internal static readonly FastResourceComparer Default = new FastResourceComparer();
	}
}
