using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000779 RID: 1913
	[Serializable]
	internal sealed class SByteEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x06003CC8 RID: 15560 RVA: 0x000EA5B4 File Offset: 0x000E87B4
		public SByteEnumEqualityComparer()
		{
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x000EA5B4 File Offset: 0x000E87B4
		public SByteEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x000EA5BC File Offset: 0x000E87BC
		public override int GetHashCode(T obj)
		{
			return ((sbyte)JitHelpers.UnsafeEnumCast<T>(obj)).GetHashCode();
		}
	}
}
