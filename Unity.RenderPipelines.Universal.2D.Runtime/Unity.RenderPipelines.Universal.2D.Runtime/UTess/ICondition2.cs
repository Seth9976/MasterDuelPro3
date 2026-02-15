using System;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x020000AC RID: 172
	internal interface ICondition2<in T, in U>
	{
		// Token: 0x060003DD RID: 989
		bool Test(T x, U y, ref float t);
	}
}
