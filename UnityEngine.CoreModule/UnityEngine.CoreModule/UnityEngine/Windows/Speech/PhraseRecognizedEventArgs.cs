using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200020A RID: 522
	public struct PhraseRecognizedEventArgs
	{
		// Token: 0x060013D5 RID: 5077 RVA: 0x00029ABA File Offset: 0x00027CBA
		internal PhraseRecognizedEventArgs(string text, ConfidenceLevel confidence, SemanticMeaning[] semanticMeanings, DateTime phraseStartTime, TimeSpan phraseDuration)
		{
			this.text = text;
			this.confidence = confidence;
			this.semanticMeanings = semanticMeanings;
			this.phraseStartTime = phraseStartTime;
			this.phraseDuration = phraseDuration;
		}

		// Token: 0x0400074F RID: 1871
		public readonly ConfidenceLevel confidence;

		// Token: 0x04000750 RID: 1872
		public readonly SemanticMeaning[] semanticMeanings;

		// Token: 0x04000751 RID: 1873
		public readonly string text;

		// Token: 0x04000752 RID: 1874
		public readonly DateTime phraseStartTime;

		// Token: 0x04000753 RID: 1875
		public readonly TimeSpan phraseDuration;
	}
}
