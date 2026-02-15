using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000208 RID: 520
	public enum DictationCompletionCause
	{
		// Token: 0x04000745 RID: 1861
		Complete,
		// Token: 0x04000746 RID: 1862
		AudioQualityFailure,
		// Token: 0x04000747 RID: 1863
		Canceled,
		// Token: 0x04000748 RID: 1864
		TimeoutExceeded,
		// Token: 0x04000749 RID: 1865
		PauseLimitExceeded,
		// Token: 0x0400074A RID: 1866
		NetworkFailure,
		// Token: 0x0400074B RID: 1867
		MicrophoneUnavailable,
		// Token: 0x0400074C RID: 1868
		UnknownError
	}
}
