using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x020001FB RID: 507
	public static class PhraseRecognitionSystem
	{
		// Token: 0x060013BF RID: 5055 RVA: 0x000298B0 File Offset: 0x00027AB0
		[RequiredByNativeCode]
		private static void PhraseRecognitionSystem_InvokeErrorEvent(SpeechError errorCode)
		{
			PhraseRecognitionSystem.ErrorDelegate onError = PhraseRecognitionSystem.OnError;
			bool flag = onError != null;
			if (flag)
			{
				onError(errorCode);
			}
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x000298D4 File Offset: 0x00027AD4
		[RequiredByNativeCode]
		private static void PhraseRecognitionSystem_InvokeStatusChangedEvent(SpeechSystemStatus status)
		{
			PhraseRecognitionSystem.StatusDelegate onStatusChanged = PhraseRecognitionSystem.OnStatusChanged;
			bool flag = onStatusChanged != null;
			if (flag)
			{
				onStatusChanged(status);
			}
		}

		// Token: 0x04000727 RID: 1831
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static PhraseRecognitionSystem.ErrorDelegate OnError;

		// Token: 0x04000728 RID: 1832
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static PhraseRecognitionSystem.StatusDelegate OnStatusChanged;

		// Token: 0x020001FC RID: 508
		// (Invoke) Token: 0x060013C2 RID: 5058
		public delegate void ErrorDelegate(SpeechError errorCode);

		// Token: 0x020001FD RID: 509
		// (Invoke) Token: 0x060013C4 RID: 5060
		public delegate void StatusDelegate(SpeechSystemStatus status);
	}
}
