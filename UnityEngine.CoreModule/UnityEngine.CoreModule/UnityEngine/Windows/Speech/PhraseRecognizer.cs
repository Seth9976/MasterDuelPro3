using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x020001FE RID: 510
	public abstract class PhraseRecognizer
	{
		// Token: 0x060013C5 RID: 5061 RVA: 0x000298F8 File Offset: 0x00027AF8
		[RequiredByNativeCode]
		private unsafe void InvokePhraseRecognizedEvent(IntPtr rawText, int rawTextLength, ConfidenceLevel confidence, SemanticMeaning[] semanticMeanings, long phraseStartFileTime, long phraseDurationTicks)
		{
			PhraseRecognizer.PhraseRecognizedDelegate onPhraseRecognized = this.OnPhraseRecognized;
			bool flag = onPhraseRecognized != null;
			if (flag)
			{
				onPhraseRecognized(new PhraseRecognizedEventArgs(new string((char*)(void*)rawText, 0, rawTextLength), confidence, semanticMeanings, DateTime.FromFileTime(phraseStartFileTime), TimeSpan.FromTicks(phraseDurationTicks)));
			}
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00029940 File Offset: 0x00027B40
		[RequiredByNativeCode]
		private unsafe static SemanticMeaning[] MarshalSemanticMeaning(IntPtr keys, IntPtr values, IntPtr valueSizes, int valueCount)
		{
			SemanticMeaning[] result = new SemanticMeaning[valueCount];
			int valueIndex = 0;
			for (int i = 0; i < valueCount; i++)
			{
				uint ithValueSize = *(uint*)((byte*)(void*)valueSizes + (IntPtr)i * 4);
				SemanticMeaning semanticMeaning = new SemanticMeaning
				{
					key = new string(*(IntPtr*)((byte*)(void*)keys + (IntPtr)i * (IntPtr)sizeof(char*))),
					values = new string[ithValueSize]
				};
				int j = 0;
				while ((long)j < (long)((ulong)ithValueSize))
				{
					semanticMeaning.values[j] = new string(*(IntPtr*)((byte*)(void*)values + (IntPtr)(valueIndex + j) * (IntPtr)sizeof(char*)));
					j++;
				}
				result[i] = semanticMeaning;
				valueIndex += (int)ithValueSize;
			}
			return result;
		}

		// Token: 0x04000729 RID: 1833
		protected IntPtr m_Recognizer;

		// Token: 0x0400072A RID: 1834
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private PhraseRecognizer.PhraseRecognizedDelegate OnPhraseRecognized;

		// Token: 0x020001FF RID: 511
		// (Invoke) Token: 0x060013C8 RID: 5064
		public delegate void PhraseRecognizedDelegate(PhraseRecognizedEventArgs args);
	}
}
