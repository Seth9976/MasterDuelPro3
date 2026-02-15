using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000778 RID: 1912
	[Serializable]
	internal class EnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x06003CC1 RID: 15553 RVA: 0x000EA544 File Offset: 0x000E8744
		public override bool Equals(T x, T y)
		{
			int num = JitHelpers.UnsafeEnumCast<T>(x);
			int num2 = JitHelpers.UnsafeEnumCast<T>(y);
			return num == num2;
		}

		// Token: 0x06003CC2 RID: 15554 RVA: 0x000EA564 File Offset: 0x000E8764
		public override int GetHashCode(T obj)
		{
			return JitHelpers.UnsafeEnumCast<T>(obj).GetHashCode();
		}

		// Token: 0x06003CC3 RID: 15555 RVA: 0x000EA1E9 File Offset: 0x000E83E9
		public EnumEqualityComparer()
		{
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x000EA1E9 File Offset: 0x000E83E9
		protected EnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x06003CC5 RID: 15557 RVA: 0x000EA57F File Offset: 0x000E877F
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (Type.GetTypeCode(Enum.GetUnderlyingType(typeof(T))) != TypeCode.Int32)
			{
				info.SetType(typeof(ObjectEqualityComparer<T>));
			}
		}

		// Token: 0x06003CC6 RID: 15558 RVA: 0x000EA5A9 File Offset: 0x000E87A9
		public override bool Equals(object obj)
		{
			return obj is EnumEqualityComparer<T>;
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x000E9DC2 File Offset: 0x000E7FC2
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
