using System;

namespace System.Runtime
{
	// Token: 0x0200002A RID: 42
	internal static class InternalSR
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00004569 File Offset: 0x00002769
		public static string AsyncEventArgsCompletedTwice(Type t)
		{
			return string.Format("AsyncEventArgs completed twice for {0}", t);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004576 File Offset: 0x00002776
		public static string AsyncEventArgsCompletionPending(Type t)
		{
			return string.Format("AsyncEventArgs completion pending for {0}", t);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004583 File Offset: 0x00002783
		public static string EtwRegistrationFailed(object arg)
		{
			return string.Format("ETW registration failed {0}", arg);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004590 File Offset: 0x00002790
		public static string InvalidAsyncResultImplementation(Type t)
		{
			return string.Format("Invalid AsyncResult implementation: {0}", t);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000459D File Offset: 0x0000279D
		public static string ShipAssertExceptionMessage(object description)
		{
			return string.Format("Ship assert exception {0}", description);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000045AA File Offset: 0x000027AA
		public static string AsyncResultCompletedTwice(Type t)
		{
			return string.Format("AsyncResult Completed Twice for {0}", t);
		}
	}
}
