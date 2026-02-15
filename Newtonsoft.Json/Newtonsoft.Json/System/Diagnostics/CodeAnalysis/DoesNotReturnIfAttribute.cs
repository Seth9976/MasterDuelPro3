using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020A5 File Offset: 0x000002A5
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020B4 File Offset: 0x000002B4
		public bool ParameterValue { get; }
	}
}
