using System;

namespace System
{
	// Token: 0x020001AD RID: 429
	[Serializable]
	internal class ReflectionOnlyType : RuntimeType
	{
		// Token: 0x0600103C RID: 4156 RVA: 0x00044D4E File Offset: 0x00042F4E
		private ReflectionOnlyType()
		{
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x00044D56 File Offset: 0x00042F56
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("The requested operation is invalid in the ReflectionOnly context."));
			}
		}
	}
}
