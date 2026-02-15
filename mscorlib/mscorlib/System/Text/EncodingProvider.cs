using System;

namespace System.Text
{
	// Token: 0x020002F3 RID: 755
	public abstract class EncodingProvider
	{
		// Token: 0x06001AB6 RID: 6838
		public abstract Encoding GetEncoding(string name);

		// Token: 0x06001AB7 RID: 6839
		public abstract Encoding GetEncoding(int codepage);

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000651AC File Offset: 0x000633AC
		public virtual Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = this.GetEncoding(codepage);
			if (encoding != null)
			{
				encoding = (Encoding)this.GetEncoding(codepage).Clone();
				encoding.EncoderFallback = encoderFallback;
				encoding.DecoderFallback = decoderFallback;
			}
			return encoding;
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000651E8 File Offset: 0x000633E8
		internal static Encoding GetEncodingFromProvider(int codepage)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			for (int i = 0; i < array.Length; i++)
			{
				Encoding encoding = array[i].GetEncoding(codepage);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x00065228 File Offset: 0x00063428
		internal static Encoding GetEncodingFromProvider(string encodingName)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			for (int i = 0; i < array.Length; i++)
			{
				Encoding encoding = array[i].GetEncoding(encodingName);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00065268 File Offset: 0x00063468
		internal static Encoding GetEncodingFromProvider(int codepage, EncoderFallback enc, DecoderFallback dec)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			for (int i = 0; i < array.Length; i++)
			{
				Encoding encoding = array[i].GetEncoding(codepage, enc, dec);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x04000C8F RID: 3215
		private static object s_InternalSyncObject = new object();

		// Token: 0x04000C90 RID: 3216
		private static volatile EncodingProvider[] s_providers;
	}
}
