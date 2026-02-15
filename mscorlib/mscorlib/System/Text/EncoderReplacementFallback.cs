using System;
using System.Runtime.Serialization;

namespace System.Text
{
	/// <summary>Provides a failure handling mechanism, called a fallback, for an input character that cannot be converted to an output byte sequence. The fallback uses a user-specified replacement string instead of the original input character. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002F0 RID: 752
	[Serializable]
	public sealed class EncoderReplacementFallback : EncoderFallback, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderReplacementFallback" /> class.</summary>
		// Token: 0x06001A98 RID: 6808 RVA: 0x00064943 File Offset: 0x00062B43
		public EncoderReplacementFallback()
			: this("?")
		{
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x00064950 File Offset: 0x00062B50
		internal EncoderReplacementFallback(SerializationInfo info, StreamingContext context)
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

		// Token: 0x06001A9A RID: 6810 RVA: 0x0006499C File Offset: 0x00062B9C
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("strDefault", this._strDefault);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderReplacementFallback" /> class using a specified replacement string.</summary>
		/// <param name="replacement">A string that is converted in an encoding operation in place of an input character that cannot be encoded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="replacement" /> contains an invalid surrogate pair. In other words, the surrogate does not consist of one high surrogate component followed by one low surrogate component.</exception>
		// Token: 0x06001A9B RID: 6811 RVA: 0x000649B0 File Offset: 0x00062BB0
		public EncoderReplacementFallback(string replacement)
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

		/// <summary>Gets the replacement string that is the value of the <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>A substitute string that is used in place of an input character that cannot be encoded.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x00064A2A File Offset: 0x00062C2A
		public string DefaultString
		{
			get
			{
				return this._strDefault;
			}
		}

		/// <summary>Creates a <see cref="T:System.Text.EncoderFallbackBuffer" /> object that is initialized with the replacement string of this <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>A <see cref="T:System.Text.EncoderFallbackBuffer" /> object equal to this <see cref="T:System.Text.EncoderReplacementFallback" /> object. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A9D RID: 6813 RVA: 0x00064A32 File Offset: 0x00062C32
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderReplacementFallbackBuffer(this);
		}

		/// <summary>Gets the number of characters in the replacement string for the <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>The number of characters in the string used in place of an input character that cannot be encoded.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x00064A3A File Offset: 0x00062C3A
		public override int MaxCharCount
		{
			get
			{
				return this._strDefault.Length;
			}
		}

		/// <summary>Indicates whether the value of a specified object is equal to the <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>true if the <paramref name="value" /> parameter specifies an <see cref="T:System.Text.EncoderReplacementFallback" /> object and the replacement string of that object is equal to the replacement string of this <see cref="T:System.Text.EncoderReplacementFallback" /> object; otherwise, false. </returns>
		/// <param name="value">A <see cref="T:System.Text.EncoderReplacementFallback" /> object.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A9F RID: 6815 RVA: 0x00064A48 File Offset: 0x00062C48
		public override bool Equals(object value)
		{
			EncoderReplacementFallback encoderReplacementFallback = value as EncoderReplacementFallback;
			return encoderReplacementFallback != null && this._strDefault == encoderReplacementFallback._strDefault;
		}

		/// <summary>Retrieves the hash code for the value of the <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>The hash code of the value of the object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AA0 RID: 6816 RVA: 0x00064A72 File Offset: 0x00062C72
		public override int GetHashCode()
		{
			return this._strDefault.GetHashCode();
		}

		// Token: 0x04000C8B RID: 3211
		private string _strDefault;
	}
}
