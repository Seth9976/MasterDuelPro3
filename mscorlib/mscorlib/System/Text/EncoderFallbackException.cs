using System;
using System.Runtime.Serialization;

namespace System.Text
{
	/// <summary>The exception that is thrown when an encoder fallback operation fails. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002EC RID: 748
	[Serializable]
	public sealed class EncoderFallbackException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderFallbackException" /> class.</summary>
		// Token: 0x06001A77 RID: 6775 RVA: 0x000632E7 File Offset: 0x000614E7
		public EncoderFallbackException()
			: base("Value does not fall within the expected range.")
		{
			base.HResult = -2147024809;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x000642A5 File Offset: 0x000624A5
		internal EncoderFallbackException(string message, char charUnknown, int index)
			: base(message)
		{
			this._charUnknown = charUnknown;
			this._index = index;
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x000642BC File Offset: 0x000624BC
		internal EncoderFallbackException(string message, char charUnknownHigh, char charUnknownLow, int index)
			: base(message)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", SR.Format("Valid values are between {0} and {1}, inclusive.", 55296, 56319));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("CharUnknownLow", SR.Format("Valid values are between {0} and {1}, inclusive.", 56320, 57343));
			}
			this._charUnknownHigh = charUnknownHigh;
			this._charUnknownLow = charUnknownLow;
			this._index = index;
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000188B2 File Offset: 0x00016AB2
		private EncoderFallbackException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x04000C79 RID: 3193
		private char _charUnknown;

		// Token: 0x04000C7A RID: 3194
		private char _charUnknownHigh;

		// Token: 0x04000C7B RID: 3195
		private char _charUnknownLow;

		// Token: 0x04000C7C RID: 3196
		private int _index;
	}
}
