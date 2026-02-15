using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000207 RID: 519
	public enum SpeechError
	{
		// Token: 0x0400073A RID: 1850
		NoError,
		// Token: 0x0400073B RID: 1851
		TopicLanguageNotSupported,
		// Token: 0x0400073C RID: 1852
		GrammarLanguageMismatch,
		// Token: 0x0400073D RID: 1853
		GrammarCompilationFailure,
		// Token: 0x0400073E RID: 1854
		AudioQualityFailure,
		// Token: 0x0400073F RID: 1855
		PauseLimitExceeded,
		// Token: 0x04000740 RID: 1856
		TimeoutExceeded,
		// Token: 0x04000741 RID: 1857
		NetworkFailure,
		// Token: 0x04000742 RID: 1858
		MicrophoneUnavailable,
		// Token: 0x04000743 RID: 1859
		UnknownError
	}
}
