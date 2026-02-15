using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000F3 RID: 243
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct StructMultiKey<[Nullable(2)] T1, [Nullable(2)] T2> : IEquatable<StructMultiKey<T1, T2>>
	{
		// Token: 0x0600071A RID: 1818 RVA: 0x00024021 File Offset: 0x00022221
		public StructMultiKey(T1 v1, T2 v2)
		{
			this.Value1 = v1;
			this.Value2 = v2;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00024034 File Offset: 0x00022234
		public override int GetHashCode()
		{
			T1 value = this.Value1;
			int num = ((value != null) ? value.GetHashCode() : 0);
			T2 value2 = this.Value2;
			return num ^ ((value2 != null) ? value2.GetHashCode() : 0);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0002408C File Offset: 0x0002228C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is StructMultiKey<T1, T2>)
			{
				StructMultiKey<T1, T2> structMultiKey = (StructMultiKey<T1, T2>)obj;
				return this.Equals(structMultiKey);
			}
			return false;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000240B3 File Offset: 0x000222B3
		public bool Equals([Nullable(new byte[] { 0, 1, 1 })] StructMultiKey<T1, T2> other)
		{
			return object.Equals(this.Value1, other.Value1) && object.Equals(this.Value2, other.Value2);
		}

		// Token: 0x040004D6 RID: 1238
		public readonly T1 Value1;

		// Token: 0x040004D7 RID: 1239
		public readonly T2 Value2;
	}
}
