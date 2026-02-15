using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000268 RID: 616
	internal class ImmediateModeException : Exception
	{
		// Token: 0x060010C6 RID: 4294 RVA: 0x000487BA File Offset: 0x000469BA
		public ImmediateModeException(Exception inner)
			: base("", inner)
		{
		}
	}
}
