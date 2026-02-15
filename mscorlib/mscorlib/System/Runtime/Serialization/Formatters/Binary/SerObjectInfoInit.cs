using System;
using System.Collections;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004FD RID: 1277
	internal sealed class SerObjectInfoInit
	{
		// Token: 0x04001411 RID: 5137
		internal Hashtable seenBeforeTable = new Hashtable();

		// Token: 0x04001412 RID: 5138
		internal int objectInfoIdCount = 1;

		// Token: 0x04001413 RID: 5139
		internal SerStack oiPool = new SerStack("SerObjectInfo Pool");
	}
}
