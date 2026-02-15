using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x0200077B RID: 1915
	[Serializable]
	internal sealed class LongEnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x06003CCE RID: 15566 RVA: 0x000EA5F4 File Offset: 0x000E87F4
		public override bool Equals(T x, T y)
		{
			long num = JitHelpers.UnsafeEnumCastLong<T>(x);
			long num2 = JitHelpers.UnsafeEnumCastLong<T>(y);
			return num == num2;
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x000EA614 File Offset: 0x000E8814
		public override int GetHashCode(T obj)
		{
			return JitHelpers.UnsafeEnumCastLong<T>(obj).GetHashCode();
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x000EA62F File Offset: 0x000E882F
		public override bool Equals(object obj)
		{
			return obj is LongEnumEqualityComparer<T>;
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x000E9DC2 File Offset: 0x000E7FC2
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x000EA1E9 File Offset: 0x000E83E9
		public LongEnumEqualityComparer()
		{
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x000EA1E9 File Offset: 0x000E83E9
		public LongEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x000EA63A File Offset: 0x000E883A
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(ObjectEqualityComparer<T>));
		}
	}
}
