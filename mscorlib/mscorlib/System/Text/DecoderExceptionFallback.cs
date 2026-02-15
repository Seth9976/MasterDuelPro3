using System;

namespace System.Text
{
	/// <summary>Throws <see cref="T:System.Text.DecoderFallbackException" /> if an encoded input byte sequence cannot be converted to a decoded output character. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002DF RID: 735
	[Serializable]
	public sealed class DecoderExceptionFallback : DecoderFallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderExceptionFallback" /> class. </summary>
		/// <returns>A <see cref="T:System.Text.DecoderFallbackBuffer" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A1C RID: 6684 RVA: 0x00063233 File Offset: 0x00061433
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderExceptionFallbackBuffer();
		}

		/// <summary>Gets the maximum number of characters this instance can return.</summary>
		/// <returns>The return value is always zero.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x00033991 File Offset: 0x00031B91
		public override int MaxCharCount
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Indicates whether the current <see cref="T:System.Text.DecoderExceptionFallback" /> object and a specified object are equal.</summary>
		/// <returns>true if <paramref name="value" /> is not null and is a <see cref="T:System.Text.DecoderExceptionFallback" /> object; otherwise, false.</returns>
		/// <param name="value">An object that derives from the <see cref="T:System.Text.DecoderExceptionFallback" /> class.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A1E RID: 6686 RVA: 0x0006323A File Offset: 0x0006143A
		public override bool Equals(object value)
		{
			return value is DecoderExceptionFallback;
		}

		/// <summary>Retrieves the hash code for this instance.</summary>
		/// <returns>The return value is always the same arbitrary value, and has no special significance. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A1F RID: 6687 RVA: 0x00063247 File Offset: 0x00061447
		public override int GetHashCode()
		{
			return 879;
		}
	}
}
