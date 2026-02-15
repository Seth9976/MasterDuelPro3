using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000F6 RID: 246
	internal static class ValidationUtils
	{
		// Token: 0x06000732 RID: 1842 RVA: 0x0002429C File Offset: 0x0002249C
		[NullableContext(1)]
		public static void ArgumentNotNull([Nullable(2)] [global::System.Diagnostics.CodeAnalysis.NotNull] object value, string parameterName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
		}
	}
}
