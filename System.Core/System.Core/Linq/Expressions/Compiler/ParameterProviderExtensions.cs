using System;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020000E9 RID: 233
	internal static class ParameterProviderExtensions
	{
		// Token: 0x060007AD RID: 1965 RVA: 0x000193A4 File Offset: 0x000175A4
		public static int IndexOf(this IParameterProvider provider, ParameterExpression parameter)
		{
			int i = 0;
			int parameterCount = provider.ParameterCount;
			while (i < parameterCount)
			{
				if (provider.GetParameter(i) == parameter)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000193D1 File Offset: 0x000175D1
		public static bool Contains(this IParameterProvider provider, ParameterExpression parameter)
		{
			return provider.IndexOf(parameter) >= 0;
		}
	}
}
