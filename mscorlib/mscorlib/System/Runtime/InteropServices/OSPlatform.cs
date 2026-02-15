using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000516 RID: 1302
	public readonly struct OSPlatform : IEquatable<OSPlatform>
	{
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060028EA RID: 10474 RVA: 0x000A7C11 File Offset: 0x000A5E11
		public static OSPlatform Linux { get; } = new OSPlatform("LINUX");

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x000A7C18 File Offset: 0x000A5E18
		public static OSPlatform OSX { get; } = new OSPlatform("OSX");

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060028EC RID: 10476 RVA: 0x000A7C1F File Offset: 0x000A5E1F
		public static OSPlatform Windows { get; } = new OSPlatform("WINDOWS");

		// Token: 0x060028ED RID: 10477 RVA: 0x000A7C26 File Offset: 0x000A5E26
		private OSPlatform(string osPlatform)
		{
			if (osPlatform == null)
			{
				throw new ArgumentNullException("osPlatform");
			}
			if (osPlatform.Length == 0)
			{
				throw new ArgumentException("Value cannot be empty.", "osPlatform");
			}
			this._osPlatform = osPlatform;
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000A7C55 File Offset: 0x000A5E55
		public static OSPlatform Create(string osPlatform)
		{
			return new OSPlatform(osPlatform);
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000A7C5D File Offset: 0x000A5E5D
		public bool Equals(OSPlatform other)
		{
			return this.Equals(other._osPlatform);
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x000A7C6B File Offset: 0x000A5E6B
		internal bool Equals(string other)
		{
			return string.Equals(this._osPlatform, other, StringComparison.Ordinal);
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x000A7C7A File Offset: 0x000A5E7A
		public override bool Equals(object obj)
		{
			return obj is OSPlatform && this.Equals((OSPlatform)obj);
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x000A7C92 File Offset: 0x000A5E92
		public override int GetHashCode()
		{
			if (this._osPlatform != null)
			{
				return this._osPlatform.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000A7CA9 File Offset: 0x000A5EA9
		public override string ToString()
		{
			return this._osPlatform ?? string.Empty;
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000A7CBA File Offset: 0x000A5EBA
		public static bool operator ==(OSPlatform left, OSPlatform right)
		{
			return left.Equals(right);
		}

		// Token: 0x040014E2 RID: 5346
		private readonly string _osPlatform;
	}
}
