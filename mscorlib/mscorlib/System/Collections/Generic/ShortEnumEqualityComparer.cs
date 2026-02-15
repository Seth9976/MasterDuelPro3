using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x0200077A RID: 1914
	[Serializable]
	internal sealed class ShortEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x06003CCB RID: 15563 RVA: 0x000EA5B4 File Offset: 0x000E87B4
		public ShortEnumEqualityComparer()
		{
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x000EA5B4 File Offset: 0x000E87B4
		public ShortEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x000EA5D8 File Offset: 0x000E87D8
		public override int GetHashCode(T obj)
		{
			return ((short)JitHelpers.UnsafeEnumCast<T>(obj)).GetHashCode();
		}
	}
}
