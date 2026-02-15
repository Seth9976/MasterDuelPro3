using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200037D RID: 893
	public readonly struct HashAlgorithmName : IEquatable<HashAlgorithmName>
	{
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x0007BF1A File Offset: 0x0007A11A
		public static HashAlgorithmName MD5
		{
			get
			{
				return new HashAlgorithmName("MD5");
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x0007BF26 File Offset: 0x0007A126
		public static HashAlgorithmName SHA1
		{
			get
			{
				return new HashAlgorithmName("SHA1");
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001F1F RID: 7967 RVA: 0x0007BF32 File Offset: 0x0007A132
		public static HashAlgorithmName SHA256
		{
			get
			{
				return new HashAlgorithmName("SHA256");
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x0007BF3E File Offset: 0x0007A13E
		public static HashAlgorithmName SHA384
		{
			get
			{
				return new HashAlgorithmName("SHA384");
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x0007BF4A File Offset: 0x0007A14A
		public static HashAlgorithmName SHA512
		{
			get
			{
				return new HashAlgorithmName("SHA512");
			}
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0007BF56 File Offset: 0x0007A156
		public HashAlgorithmName(string name)
		{
			this._name = name;
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001F23 RID: 7971 RVA: 0x0007BF5F File Offset: 0x0007A15F
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x0007BF67 File Offset: 0x0007A167
		public override string ToString()
		{
			return this._name ?? string.Empty;
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0007BF78 File Offset: 0x0007A178
		public override bool Equals(object obj)
		{
			return obj is HashAlgorithmName && this.Equals((HashAlgorithmName)obj);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0007BF90 File Offset: 0x0007A190
		public bool Equals(HashAlgorithmName other)
		{
			return this._name == other._name;
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0007BFA3 File Offset: 0x0007A1A3
		public override int GetHashCode()
		{
			if (this._name != null)
			{
				return this._name.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0007BFBA File Offset: 0x0007A1BA
		public static bool operator ==(HashAlgorithmName left, HashAlgorithmName right)
		{
			return left.Equals(right);
		}

		// Token: 0x04000E97 RID: 3735
		private readonly string _name;
	}
}
