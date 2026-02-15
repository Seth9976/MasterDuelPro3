using System;
using System.Security;
using System.Threading;

namespace System.Drawing
{
	// Token: 0x0200000E RID: 14
	internal static class ClientUtils
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000035EC File Offset: 0x000017EC
		public static bool IsCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException || ex is ExecutionEngineException || ex is IndexOutOfRangeException || ex is AccessViolationException;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003629 File Offset: 0x00001829
		public static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is SecurityException || ClientUtils.IsCriticalException(ex);
		}
	}
}
