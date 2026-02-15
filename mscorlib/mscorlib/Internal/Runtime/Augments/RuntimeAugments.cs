using System;
using System.Runtime.ExceptionServices;

namespace Internal.Runtime.Augments
{
	// Token: 0x02000094 RID: 148
	internal class RuntimeAugments
	{
		// Token: 0x060002BE RID: 702 RVA: 0x000108FB File Offset: 0x0000EAFB
		public static void ReportUnhandledException(Exception exception)
		{
			ExceptionDispatchInfo.Capture(exception).Throw();
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00010908 File Offset: 0x0000EB08
		internal static ReflectionExecutionDomainCallbacks Callbacks
		{
			get
			{
				return RuntimeAugments.s_reflectionExecutionDomainCallbacks;
			}
		}

		// Token: 0x04000277 RID: 631
		private static ReflectionExecutionDomainCallbacks s_reflectionExecutionDomainCallbacks = new ReflectionExecutionDomainCallbacks();
	}
}
