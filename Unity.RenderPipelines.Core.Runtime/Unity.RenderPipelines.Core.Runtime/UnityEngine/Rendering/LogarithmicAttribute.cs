using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000129 RID: 297
	internal class LogarithmicAttribute : PropertyAttribute
	{
		// Token: 0x06000992 RID: 2450 RVA: 0x0001E509 File Offset: 0x0001C709
		public LogarithmicAttribute(int min, int max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x04000555 RID: 1365
		public int min;

		// Token: 0x04000556 RID: 1366
		public int max;
	}
}
