using System;
using System.Runtime.Serialization;

namespace System.Text
{
	/// <summary>The exception that is thrown when a decoder fallback operation fails. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002E1 RID: 737
	[Serializable]
	public sealed class DecoderFallbackException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. </summary>
		// Token: 0x06001A25 RID: 6693 RVA: 0x000632E7 File Offset: 0x000614E7
		public DecoderFallbackException()
			: base("Value does not fall within the expected range.")
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. Parameters specify the error message, the array of bytes being decoded, and the index of the byte that cannot be decoded.</summary>
		/// <param name="message">An error message.</param>
		/// <param name="bytesUnknown">The input byte array.</param>
		/// <param name="index">The index position in <paramref name="bytesUnknown" /> of the byte that cannot be decoded.</param>
		// Token: 0x06001A26 RID: 6694 RVA: 0x000632FF File Offset: 0x000614FF
		public DecoderFallbackException(string message, byte[] bytesUnknown, int index)
			: base(message)
		{
			this._bytesUnknown = bytesUnknown;
			this._index = index;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x000188B2 File Offset: 0x00016AB2
		private DecoderFallbackException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x04000C62 RID: 3170
		private byte[] _bytesUnknown;

		// Token: 0x04000C63 RID: 3171
		private int _index;
	}
}
