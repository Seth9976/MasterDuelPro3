using System;
using System.Runtime.CompilerServices;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000051 RID: 81
	public static class Constant
	{
		// Token: 0x06000E33 RID: 3635 RVA: 0x000024DA File Offset: 0x000006DA
		public static bool IsConstantExpression<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T t) where T : struct, ValueType
		{
			return false;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x000024DA File Offset: 0x000006DA
		public unsafe static bool IsConstantExpression(void* t)
		{
			return false;
		}
	}
}
