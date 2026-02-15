using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000200 RID: 512
	public sealed class DictationRecognizer
	{
		// Token: 0x060013C9 RID: 5065 RVA: 0x00029A04 File Offset: 0x00027C04
		[RequiredByNativeCode]
		private unsafe void DictationRecognizer_InvokeHypothesisGeneratedEvent(IntPtr keyword, int keywordLength)
		{
			DictationRecognizer.DictationHypothesisDelegate handler = this.DictationHypothesis;
			bool flag = handler != null;
			if (flag)
			{
				handler(new string((char*)(void*)keyword, 0, keywordLength));
			}
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00029A38 File Offset: 0x00027C38
		[RequiredByNativeCode]
		private unsafe void DictationRecognizer_InvokeResultGeneratedEvent(IntPtr keyword, int keywordLength, ConfidenceLevel minimumConfidence)
		{
			DictationRecognizer.DictationResultDelegate handler = this.DictationResult;
			bool flag = handler != null;
			if (flag)
			{
				handler(new string((char*)(void*)keyword, 0, keywordLength), minimumConfidence);
			}
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00029A6C File Offset: 0x00027C6C
		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeCompletedEvent(DictationCompletionCause cause)
		{
			DictationRecognizer.DictationCompletedDelegate handler = this.DictationComplete;
			bool flag = handler != null;
			if (flag)
			{
				handler(cause);
			}
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00029A94 File Offset: 0x00027C94
		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeErrorEvent(string error, int hresult)
		{
			DictationRecognizer.DictationErrorHandler handler = this.DictationError;
			bool flag = handler != null;
			if (flag)
			{
				handler(error, hresult);
			}
		}

		// Token: 0x0400072B RID: 1835
		private IntPtr m_Recognizer;

		// Token: 0x0400072C RID: 1836
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private DictationRecognizer.DictationHypothesisDelegate DictationHypothesis;

		// Token: 0x0400072D RID: 1837
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private DictationRecognizer.DictationResultDelegate DictationResult;

		// Token: 0x0400072E RID: 1838
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private DictationRecognizer.DictationCompletedDelegate DictationComplete;

		// Token: 0x0400072F RID: 1839
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private DictationRecognizer.DictationErrorHandler DictationError;

		// Token: 0x02000201 RID: 513
		// (Invoke) Token: 0x060013CE RID: 5070
		public delegate void DictationHypothesisDelegate(string text);

		// Token: 0x02000202 RID: 514
		// (Invoke) Token: 0x060013D0 RID: 5072
		public delegate void DictationResultDelegate(string text, ConfidenceLevel confidence);

		// Token: 0x02000203 RID: 515
		// (Invoke) Token: 0x060013D2 RID: 5074
		public delegate void DictationCompletedDelegate(DictationCompletionCause cause);

		// Token: 0x02000204 RID: 516
		// (Invoke) Token: 0x060013D4 RID: 5076
		public delegate void DictationErrorHandler(string error, int hresult);
	}
}
