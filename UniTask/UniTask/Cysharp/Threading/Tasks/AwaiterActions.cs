using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000113 RID: 275
	internal static class AwaiterActions
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x0001FB36 File Offset: 0x0001DD36
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Continuation(object state)
		{
			((Action)state)();
		}

		// Token: 0x0400042B RID: 1067
		internal static readonly Action<object> InvokeContinuationDelegate = new Action<object>(AwaiterActions.Continuation);
	}
}
