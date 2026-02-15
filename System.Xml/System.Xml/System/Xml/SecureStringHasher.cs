using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Xml
{
	// Token: 0x02000040 RID: 64
	internal class SecureStringHasher : IEqualityComparer<string>
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x0000CF43 File Offset: 0x0000B143
		public SecureStringHasher()
		{
			this.hashCodeRandomizer = Environment.TickCount;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000CF56 File Offset: 0x0000B156
		public bool Equals(string x, string y)
		{
			return string.Equals(x, y, StringComparison.Ordinal);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000CF60 File Offset: 0x0000B160
		public int GetHashCode(string key)
		{
			if (SecureStringHasher.hashCodeDelegate == null)
			{
				SecureStringHasher.hashCodeDelegate = SecureStringHasher.GetHashCodeDelegate();
			}
			return SecureStringHasher.hashCodeDelegate(key, key.Length, (long)this.hashCodeRandomizer);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000CF8C File Offset: 0x0000B18C
		private static int GetHashCodeOfString(string key, int sLen, long additionalEntropy)
		{
			int num = (int)additionalEntropy;
			for (int i = 0; i < key.Length; i++)
			{
				num += (num << 7) ^ (int)key[i];
			}
			num -= num >> 17;
			num -= num >> 11;
			return num - (num >> 5);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000CFD0 File Offset: 0x0000B1D0
		private static SecureStringHasher.HashCodeOfStringDelegate GetHashCodeDelegate()
		{
			MethodInfo method = typeof(string).GetMethod("InternalMarvin32HashString", BindingFlags.Static | BindingFlags.NonPublic);
			if (method != null)
			{
				return (SecureStringHasher.HashCodeOfStringDelegate)Delegate.CreateDelegate(typeof(SecureStringHasher.HashCodeOfStringDelegate), method);
			}
			return new SecureStringHasher.HashCodeOfStringDelegate(SecureStringHasher.GetHashCodeOfString);
		}

		// Token: 0x0400015B RID: 347
		private static SecureStringHasher.HashCodeOfStringDelegate hashCodeDelegate;

		// Token: 0x0400015C RID: 348
		private int hashCodeRandomizer;

		// Token: 0x02000041 RID: 65
		// (Invoke) Token: 0x060001FA RID: 506
		private delegate int HashCodeOfStringDelegate(string s, int sLen, long additionalEntropy);
	}
}
