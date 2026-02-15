using System;

namespace System.Text
{
	/// <summary>Throws a <see cref="T:System.Text.EncoderFallbackException" /> if an input character cannot be converted to an encoded output byte sequence. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002EA RID: 746
	[Serializable]
	public sealed class EncoderExceptionFallback : EncoderFallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderExceptionFallback" /> class.</summary>
		/// <returns>A <see cref="T:System.Text.EncoderFallbackBuffer" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A6D RID: 6765 RVA: 0x000641CB File Offset: 0x000623CB
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderExceptionFallbackBuffer();
		}

		/// <summary>Gets the maximum number of characters this instance can return.</summary>
		/// <returns>The return value is always zero.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x00033991 File Offset: 0x00031B91
		public override int MaxCharCount
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Indicates whether the current <see cref="T:System.Text.EncoderExceptionFallback" /> object and a specified object are equal.</summary>
		/// <returns>true if <paramref name="value" /> is not null (Nothing in Visual Basic .NET) and is a <see cref="T:System.Text.EncoderExceptionFallback" /> object; otherwise, false.</returns>
		/// <param name="value">An object that derives from the <see cref="T:System.Text.EncoderExceptionFallback" /> class.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A6F RID: 6767 RVA: 0x000641D2 File Offset: 0x000623D2
		public override bool Equals(object value)
		{
			return value is EncoderExceptionFallback;
		}

		/// <summary>Retrieves the hash code for this instance.</summary>
		/// <returns>The return value is always the same arbitrary value, and has no special significance. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A70 RID: 6768 RVA: 0x000641DF File Offset: 0x000623DF
		public override int GetHashCode()
		{
			return 654;
		}
	}
}
