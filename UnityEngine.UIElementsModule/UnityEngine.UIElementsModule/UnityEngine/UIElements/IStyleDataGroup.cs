using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003B2 RID: 946
	internal interface IStyleDataGroup<T>
	{
		// Token: 0x06001BF8 RID: 7160
		T Copy();

		// Token: 0x06001BF9 RID: 7161
		void CopyFrom(ref T other);
	}
}
