using System;

namespace K4os.Compression.LZ4.Encoders
{
	// Token: 0x02000028 RID: 40
	public static class LZ4EncoderExtensions
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00005CEC File Offset: 0x00003EEC
		public unsafe static bool Topup(this ILZ4Encoder encoder, ref byte* source, int length)
		{
			int num = encoder.Topup(source, length);
			source += (IntPtr)num;
			return num != 0;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005D10 File Offset: 0x00003F10
		public unsafe static int Topup(this ILZ4Encoder encoder, byte[] source, int offset, int length)
		{
			byte* ptr;
			if (source == null || source.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &source[0];
			}
			return encoder.Topup(ptr + offset, length);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005D40 File Offset: 0x00003F40
		public static bool Topup(this ILZ4Encoder encoder, byte[] source, ref int offset, int length)
		{
			int num = encoder.Topup(source, offset, length);
			offset += num;
			return num != 0;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005D64 File Offset: 0x00003F64
		public unsafe static int Encode(this ILZ4Encoder encoder, byte[] target, int offset, int length, bool allowCopy)
		{
			byte* ptr;
			if (target == null || target.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &target[0];
			}
			return encoder.Encode(ptr + offset, length, allowCopy);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005D98 File Offset: 0x00003F98
		public static EncoderAction Encode(this ILZ4Encoder encoder, byte[] target, ref int offset, int length, bool allowCopy)
		{
			int num = encoder.Encode(target, offset, length, allowCopy);
			offset += Math.Abs(num);
			if (num == 0)
			{
				return EncoderAction.None;
			}
			if (num >= 0)
			{
				return EncoderAction.Encoded;
			}
			return EncoderAction.Copied;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005DCC File Offset: 0x00003FCC
		public unsafe static EncoderAction Encode(this ILZ4Encoder encoder, ref byte* target, int length, bool allowCopy)
		{
			int num = encoder.Encode(target, length, allowCopy);
			target += (IntPtr)Math.Abs(num);
			if (num == 0)
			{
				return EncoderAction.None;
			}
			if (num >= 0)
			{
				return EncoderAction.Encoded;
			}
			return EncoderAction.Copied;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005DFB File Offset: 0x00003FFB
		public unsafe static EncoderAction TopupAndEncode(this ILZ4Encoder encoder, byte* source, int sourceLength, byte* target, int targetLength, bool forceEncode, bool allowCopy, out int loaded, out int encoded)
		{
			loaded = 0;
			encoded = 0;
			if (sourceLength > 0)
			{
				loaded = encoder.Topup(source, sourceLength);
			}
			return encoder.FlushAndEncode(target, targetLength, forceEncode, allowCopy, loaded, out encoded);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005E28 File Offset: 0x00004028
		public unsafe static EncoderAction TopupAndEncode(this ILZ4Encoder encoder, byte[] source, int sourceOffset, int sourceLength, byte[] target, int targetOffset, int targetLength, bool forceEncode, bool allowCopy, out int loaded, out int encoded)
		{
			byte* ptr;
			if (source == null || source.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &source[0];
			}
			byte* ptr2;
			if (target == null || target.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &target[0];
			}
			return encoder.TopupAndEncode(ptr + sourceOffset, sourceLength, ptr2 + targetOffset, targetLength, forceEncode, allowCopy, out loaded, out encoded);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005E80 File Offset: 0x00004080
		private unsafe static EncoderAction FlushAndEncode(this ILZ4Encoder encoder, byte* target, int targetLength, bool forceEncode, bool allowCopy, int loaded, out int encoded)
		{
			encoded = 0;
			int blockSize = encoder.BlockSize;
			if (encoder.BytesReady < (forceEncode ? 1 : blockSize))
			{
				if (loaded <= 0)
				{
					return EncoderAction.None;
				}
				return EncoderAction.Loaded;
			}
			else
			{
				encoded = encoder.Encode(target, targetLength, allowCopy);
				if (allowCopy && encoded < 0)
				{
					encoded = -encoded;
					return EncoderAction.Copied;
				}
				return EncoderAction.Encoded;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005ED1 File Offset: 0x000040D1
		public unsafe static EncoderAction FlushAndEncode(this ILZ4Encoder encoder, byte* target, int targetLength, bool allowCopy, out int encoded)
		{
			return encoder.FlushAndEncode(target, targetLength, true, allowCopy, 0, out encoded);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005EE0 File Offset: 0x000040E0
		public unsafe static EncoderAction FlushAndEncode(this ILZ4Encoder encoder, byte[] target, int targetOffset, int targetLength, bool allowCopy, out int encoded)
		{
			byte* ptr;
			if (target == null || target.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &target[0];
			}
			return encoder.FlushAndEncode(ptr + targetOffset, targetLength, true, allowCopy, 0, out encoded);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005F18 File Offset: 0x00004118
		public unsafe static void Drain(this ILZ4Decoder decoder, byte[] target, int targetOffset, int offset, int length)
		{
			fixed (byte[] array = target)
			{
				byte* ptr;
				if (target == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				decoder.Drain(ptr + targetOffset, offset, length);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005F4B File Offset: 0x0000414B
		public unsafe static bool DecodeAndDrain(this ILZ4Decoder decoder, byte* source, int sourceLength, byte* target, int targetLength, out int decoded)
		{
			decoded = 0;
			if (sourceLength <= 0)
			{
				return false;
			}
			decoded = decoder.Decode(source, sourceLength, 0);
			if (decoded <= 0 || targetLength < decoded)
			{
				return false;
			}
			decoder.Drain(target, -decoded, decoded);
			return true;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005F84 File Offset: 0x00004184
		public unsafe static bool DecodeAndDrain(this ILZ4Decoder decoder, byte[] source, int sourceOffset, int sourceLength, byte[] target, int targetOffset, int targetLength, out int decoded)
		{
			byte* ptr;
			if (source == null || source.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &source[0];
			}
			byte* ptr2;
			if (target == null || target.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &target[0];
			}
			return decoder.DecodeAndDrain(ptr + sourceOffset, sourceLength, ptr2 + targetOffset, targetLength, out decoded);
		}
	}
}
