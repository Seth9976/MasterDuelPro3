using System;

namespace Unity.Burst
{
	// Token: 0x02000028 RID: 40
	public interface IFunctionPointer
	{
		// Token: 0x060000D2 RID: 210
		[Obsolete("This method will be removed in a future version of Burst")]
		IFunctionPointer FromIntPtr(IntPtr ptr);
	}
}
