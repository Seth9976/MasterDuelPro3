using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200023B RID: 571
	internal static class ExceptionHelpers
	{
		// Token: 0x060014D1 RID: 5329 RVA: 0x0005EE3C File Offset: 0x0005D03C
		public static bool IsExceptionIndicatingBugInCode(this Exception exception)
		{
			return exception is NullReferenceException || exception is IndexOutOfRangeException || exception is ArgumentException;
		}
	}
}
