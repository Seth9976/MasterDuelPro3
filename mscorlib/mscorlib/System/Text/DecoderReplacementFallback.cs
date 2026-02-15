using System;
using System.Runtime.Serialization;

namespace System.Text
{
	/// <summary>Provides a failure-handling mechanism, called a fallback, for an encoded input byte sequence that cannot be converted to an output character. The fallback emits a user-specified replacement string instead of a decoded input byte sequence. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002E5 RID: 741
	[Serializable]
	public sealed class DecoderReplacementFallback : DecoderFallback, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderReplacementFallback" /> class. </summary>
		// Token: 0x06001A44 RID: 6724 RVA: 0x00063930 File Offset: 0x00061B30
		public DecoderReplacementFallback()
			: this("?")
		{
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00063940 File Offset: 0x00061B40
		internal DecoderReplacementFallback(SerializationInfo info, StreamingContext context)
		{
			try
			{
				this._strDefault = info.GetString("strDefault");
			}
			catch
			{
				this._strDefault = info.GetString("_strDefault");
			}
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x0006398C File Offset: 0x00061B8C
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("strDefault", this._strDefault);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderReplacementFallback" /> class using a specified replacement string.</summary>
		/// <param name="replacement">A string that is emitted in a decoding operation in place of an input byte sequence that cannot be decoded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="replacement" /> contains an invalid surrogate pair. In other words, the surrogate pair does not consist of one high surrogate component followed by one low surrogate component.</exception>
		// Token: 0x06001A47 RID: 6727 RVA: 0x000639A0 File Offset: 0x00061BA0
		public DecoderReplacementFallback(string replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			bool flag = false;
			for (int i = 0; i < replacement.Length; i++)
			{
				if (char.IsSurrogate(replacement, i))
				{
					if (char.IsHighSurrogate(replacement, i))
					{
						if (flag)
						{
							break;
						}
						flag = true;
					}
					else
					{
						if (!flag)
						{
							flag = true;
							break;
						}
						flag = false;
					}
				}
				else if (flag)
				{
					break;
				}
			}
			if (flag)
			{
				throw new ArgumentException(SR.Format("String contains invalid Unicode code points.", "replacement"));
			}
			this._strDefault = replacement;
		}

		/// <summary>Gets the replacement string that is the value of the <see cref="T:System.Text.DecoderReplacementFallback" /> object.</summary>
		/// <returns>A substitute string that is emitted in place of an input byte sequence that cannot be decoded.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x00063A1A File Offset: 0x00061C1A
		public string DefaultString
		{
			get
			{
				return this._strDefault;
			}
		}

		/// <summary>Creates a <see cref="T:System.Text.DecoderFallbackBuffer" /> object that is initialized with the replacement string of this <see cref="T:System.Text.DecoderReplacementFallback" /> object.</summary>
		/// <returns>A <see cref="T:System.Text.DecoderFallbackBuffer" /> object that specifies a string to use instead of the original decoding operation input.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A49 RID: 6729 RVA: 0x00063A22 File Offset: 0x00061C22
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderReplacementFallbackBuffer(this);
		}

		/// <summary>Gets the number of characters in the replacement string for the <see cref="T:System.Text.DecoderReplacementFallback" /> object.</summary>
		/// <returns>The number of characters in the string that is emitted in place of a byte sequence that cannot be decoded, that is, the length of the string returned by the <see cref="P:System.Text.DecoderReplacementFallback.DefaultString" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x00063A2A File Offset: 0x00061C2A
		public override int MaxCharCount
		{
			get
			{
				return this._strDefault.Length;
			}
		}

		/// <summary>Indicates whether the value of a specified object is equal to the <see cref="T:System.Text.DecoderReplacementFallback" /> object.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.Text.DecoderReplacementFallback" /> object having a <see cref="P:System.Text.DecoderReplacementFallback.DefaultString" /> property that is equal to the <see cref="P:System.Text.DecoderReplacementFallback.DefaultString" /> property of the current <see cref="T:System.Text.DecoderReplacementFallback" /> object; otherwise, false. </returns>
		/// <param name="value">A <see cref="T:System.Text.DecoderReplacementFallback" /> object.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A4B RID: 6731 RVA: 0x00063A38 File Offset: 0x00061C38
		public override bool Equals(object value)
		{
			DecoderReplacementFallback decoderReplacementFallback = value as DecoderReplacementFallback;
			return decoderReplacementFallback != null && this._strDefault == decoderReplacementFallback._strDefault;
		}

		/// <summary>Retrieves the hash code for the value of the <see cref="T:System.Text.DecoderReplacementFallback" /> object.</summary>
		/// <returns>The hash code of the value of the object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A4C RID: 6732 RVA: 0x00063A62 File Offset: 0x00061C62
		public override int GetHashCode()
		{
			return this._strDefault.GetHashCode();
		}

		// Token: 0x04000C6C RID: 3180
		private string _strDefault;
	}
}
