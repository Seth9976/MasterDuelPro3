using System;
using System.Threading;

namespace System.Text
{
	/// <summary>Provides a failure-handling mechanism, called a fallback, for an encoded input byte sequence that cannot be converted to an output character. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002E2 RID: 738
	[Serializable]
	public abstract class DecoderFallback
	{
		/// <summary>Gets an object that outputs a substitute string in place of an input byte sequence that cannot be decoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.DecoderFallback" /> class. The default value is a <see cref="T:System.Text.DecoderReplacementFallback" /> object that emits the QUESTION MARK character ("?", U+003F) in place of unknown byte sequences. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x00063316 File Offset: 0x00061516
		public static DecoderFallback ReplacementFallback
		{
			get
			{
				DecoderFallback decoderFallback;
				if ((decoderFallback = DecoderFallback.s_replacementFallback) == null)
				{
					decoderFallback = Interlocked.CompareExchange<DecoderFallback>(ref DecoderFallback.s_replacementFallback, new DecoderReplacementFallback(), null) ?? DecoderFallback.s_replacementFallback;
				}
				return decoderFallback;
			}
		}

		/// <summary>Gets an object that throws an exception when an input byte sequence cannot be decoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.DecoderFallback" /> class. The default value is a <see cref="T:System.Text.DecoderExceptionFallback" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x0006333A File Offset: 0x0006153A
		public static DecoderFallback ExceptionFallback
		{
			get
			{
				DecoderFallback decoderFallback;
				if ((decoderFallback = DecoderFallback.s_exceptionFallback) == null)
				{
					decoderFallback = Interlocked.CompareExchange<DecoderFallback>(ref DecoderFallback.s_exceptionFallback, new DecoderExceptionFallback(), null) ?? DecoderFallback.s_exceptionFallback;
				}
				return decoderFallback;
			}
		}

		/// <summary>When overridden in a derived class, initializes a new instance of the <see cref="T:System.Text.DecoderFallbackBuffer" /> class. </summary>
		/// <returns>An object that provides a fallback buffer for a decoder.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A2A RID: 6698
		public abstract DecoderFallbackBuffer CreateFallbackBuffer();

		/// <summary>When overridden in a derived class, gets the maximum number of characters the current <see cref="T:System.Text.DecoderFallback" /> object can return.</summary>
		/// <returns>The maximum number of characters the current <see cref="T:System.Text.DecoderFallback" /> object can return.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001A2B RID: 6699
		public abstract int MaxCharCount { get; }

		// Token: 0x04000C64 RID: 3172
		private static DecoderFallback s_replacementFallback;

		// Token: 0x04000C65 RID: 3173
		private static DecoderFallback s_exceptionFallback;
	}
}
