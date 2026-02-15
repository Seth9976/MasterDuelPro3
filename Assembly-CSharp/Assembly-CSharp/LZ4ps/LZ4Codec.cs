using System;

namespace LZ4ps
{
	// Token: 0x0200119A RID: 4506
	public static class LZ4Codec
	{
		// Token: 0x060086DC RID: 34524 RVA: 0x0000216D File Offset: 0x0000036D
		private static void Assert(bool condition, string errorMessage)
		{
		}

		// Token: 0x060086DD RID: 34525 RVA: 0x0000216D File Offset: 0x0000036D
		internal static void Poke2(byte[] buffer, int offset, ushort value)
		{
		}

		// Token: 0x060086DE RID: 34526 RVA: 0x000029CC File Offset: 0x00000BCC
		internal static ushort Peek2(byte[] buffer, int offset)
		{
			return 0;
		}

		// Token: 0x060086DF RID: 34527 RVA: 0x000029CC File Offset: 0x00000BCC
		internal static uint Peek4(byte[] buffer, int offset)
		{
			return 0U;
		}

		// Token: 0x060086E0 RID: 34528 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint Xor4(byte[] buffer, int offset1, int offset2)
		{
			return 0U;
		}

		// Token: 0x060086E1 RID: 34529 RVA: 0x000F1669 File Offset: 0x000EF869
		private static ulong Xor8(byte[] buffer, int offset1, int offset2)
		{
			return 0UL;
		}

		// Token: 0x060086E2 RID: 34530 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool Equal2(byte[] buffer, int offset1, int offset2)
		{
			return false;
		}

		// Token: 0x060086E3 RID: 34531 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool Equal4(byte[] buffer, int offset1, int offset2)
		{
			return false;
		}

		// Token: 0x060086E4 RID: 34532 RVA: 0x0000216D File Offset: 0x0000036D
		private static void Copy4(byte[] buf, int src, int dst)
		{
		}

		// Token: 0x060086E5 RID: 34533 RVA: 0x0000216D File Offset: 0x0000036D
		private static void Copy8(byte[] buf, int src, int dst)
		{
		}

		// Token: 0x060086E6 RID: 34534 RVA: 0x0000216D File Offset: 0x0000036D
		private static void BlockCopy(byte[] src, int src_0, byte[] dst, int dst_0, int len)
		{
		}

		// Token: 0x060086E7 RID: 34535 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int WildCopy(byte[] src, int src_0, byte[] dst, int dst_0, int dst_end)
		{
			return 0;
		}

		// Token: 0x060086E8 RID: 34536 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int SecureCopy(byte[] buffer, int src, int dst, int dst_end)
		{
			return 0;
		}

		// Token: 0x060086E9 RID: 34537 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Encode32(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			return 0;
		}

		// Token: 0x060086EA RID: 34538 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] Encode32(byte[] input, int inputOffset, int inputLength)
		{
			return null;
		}

		// Token: 0x060086EB RID: 34539 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Encode64(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			return 0;
		}

		// Token: 0x060086EC RID: 34540 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] Encode64(byte[] input, int inputOffset, int inputLength)
		{
			return null;
		}

		// Token: 0x060086ED RID: 34541 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Decode32(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength, bool knownOutputLength)
		{
			return 0;
		}

		// Token: 0x060086EE RID: 34542 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] Decode32(byte[] input, int inputOffset, int inputLength, int outputLength)
		{
			return null;
		}

		// Token: 0x060086EF RID: 34543 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Decode64(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength, bool knownOutputLength)
		{
			return 0;
		}

		// Token: 0x060086F0 RID: 34544 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] Decode64(byte[] input, int inputOffset, int inputLength, int outputLength)
		{
			return null;
		}

		// Token: 0x060086F1 RID: 34545 RVA: 0x0000216A File Offset: 0x0000036A
		private static LZ4Codec.LZ4HC_Data_Structure LZ4HC_Create(byte[] src, int src_0, int src_len, byte[] dst, int dst_0, int dst_len)
		{
			return null;
		}

		// Token: 0x060086F2 RID: 34546 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4_compressHC_32(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			return 0;
		}

		// Token: 0x060086F3 RID: 34547 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Encode32HC(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			return 0;
		}

		// Token: 0x060086F4 RID: 34548 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] Encode32HC(byte[] input, int inputOffset, int inputLength)
		{
			return null;
		}

		// Token: 0x060086F5 RID: 34549 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4_compressHC_64(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			return 0;
		}

		// Token: 0x060086F6 RID: 34550 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Encode64HC(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			return 0;
		}

		// Token: 0x060086F7 RID: 34551 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] Encode64HC(byte[] input, int inputOffset, int inputLength)
		{
			return null;
		}

		// Token: 0x060086F8 RID: 34552 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4_compressCtx_safe32(int[] hash_table, byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			return 0;
		}

		// Token: 0x060086F9 RID: 34553 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4_compress64kCtx_safe32(ushort[] hash_table, byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			return 0;
		}

		// Token: 0x060086FA RID: 34554 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4_uncompress_safe32(byte[] src, byte[] dst, int src_0, int dst_0, int dst_len)
		{
			return 0;
		}

		// Token: 0x060086FB RID: 34555 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4_uncompress_unknownOutputSize_safe32(byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			return 0;
		}

		// Token: 0x060086FC RID: 34556 RVA: 0x0000216D File Offset: 0x0000036D
		private static void LZ4HC_Insert_32(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p)
		{
		}

		// Token: 0x060086FD RID: 34557 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4HC_CommonLength_32(LZ4Codec.LZ4HC_Data_Structure ctx, int p1, int p2)
		{
			return 0;
		}

		// Token: 0x060086FE RID: 34558 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4HC_InsertAndFindBestMatch_32(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p, ref int src_match)
		{
			return 0;
		}

		// Token: 0x060086FF RID: 34559 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4HC_InsertAndGetWiderMatch_32(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p, int startLimit, int longest, ref int matchpos, ref int startpos)
		{
			return 0;
		}

		// Token: 0x06008700 RID: 34560 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4_encodeSequence_32(LZ4Codec.LZ4HC_Data_Structure ctx, ref int src_p, ref int dst_p, ref int src_anchor, int matchLength, int src_ref, int dst_end)
		{
			return 0;
		}

		// Token: 0x06008701 RID: 34561 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4_compressHCCtx_32(LZ4Codec.LZ4HC_Data_Structure ctx)
		{
			return 0;
		}

		// Token: 0x06008702 RID: 34562 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4_compressCtx_safe64(int[] hash_table, byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			return 0;
		}

		// Token: 0x06008703 RID: 34563 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4_compress64kCtx_safe64(ushort[] hash_table, byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			return 0;
		}

		// Token: 0x06008704 RID: 34564 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4_uncompress_safe64(byte[] src, byte[] dst, int src_0, int dst_0, int dst_len)
		{
			return 0;
		}

		// Token: 0x06008705 RID: 34565 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4_uncompress_unknownOutputSize_safe64(byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			return 0;
		}

		// Token: 0x06008706 RID: 34566 RVA: 0x0000216D File Offset: 0x0000036D
		private static void LZ4HC_Insert_64(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p)
		{
		}

		// Token: 0x06008707 RID: 34567 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4HC_CommonLength_64(LZ4Codec.LZ4HC_Data_Structure ctx, int p1, int p2)
		{
			return 0;
		}

		// Token: 0x06008708 RID: 34568 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4HC_InsertAndFindBestMatch_64(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p, ref int matchpos)
		{
			return 0;
		}

		// Token: 0x06008709 RID: 34569 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4HC_InsertAndGetWiderMatch_64(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p, int startLimit, int longest, ref int matchpos, ref int startpos)
		{
			return 0;
		}

		// Token: 0x0600870A RID: 34570 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4_encodeSequence_64(LZ4Codec.LZ4HC_Data_Structure ctx, ref int src_p, ref int dst_p, ref int src_anchor, int matchLength, int src_ref)
		{
			return 0;
		}

		// Token: 0x0600870B RID: 34571 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int LZ4_compressHCCtx_64(LZ4Codec.LZ4HC_Data_Structure ctx)
		{
			return 0;
		}

		// Token: 0x0600870C RID: 34572 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int MaximumOutputLength(int inputLength)
		{
			return 0;
		}

		// Token: 0x0600870D RID: 34573 RVA: 0x0000216D File Offset: 0x0000036D
		internal static void CheckArguments(byte[] input, int inputOffset, ref int inputLength, byte[] output, int outputOffset, ref int outputLength)
		{
		}

		// Token: 0x0400C15D RID: 49501
		private const int MEMORY_USAGE = 14;

		// Token: 0x0400C15E RID: 49502
		private const int NOTCOMPRESSIBLE_DETECTIONLEVEL = 6;

		// Token: 0x0400C15F RID: 49503
		private const int BLOCK_COPY_LIMIT = 16;

		// Token: 0x0400C160 RID: 49504
		private const int MINMATCH = 4;

		// Token: 0x0400C161 RID: 49505
		private const int SKIPSTRENGTH = 6;

		// Token: 0x0400C162 RID: 49506
		private const int COPYLENGTH = 8;

		// Token: 0x0400C163 RID: 49507
		private const int LASTLITERALS = 5;

		// Token: 0x0400C164 RID: 49508
		private const int MFLIMIT = 12;

		// Token: 0x0400C165 RID: 49509
		private const int MINLENGTH = 13;

		// Token: 0x0400C166 RID: 49510
		private const int MAXD_LOG = 16;

		// Token: 0x0400C167 RID: 49511
		private const int MAXD = 65536;

		// Token: 0x0400C168 RID: 49512
		private const int MAXD_MASK = 65535;

		// Token: 0x0400C169 RID: 49513
		private const int MAX_DISTANCE = 65535;

		// Token: 0x0400C16A RID: 49514
		private const int ML_BITS = 4;

		// Token: 0x0400C16B RID: 49515
		private const int ML_MASK = 15;

		// Token: 0x0400C16C RID: 49516
		private const int RUN_BITS = 4;

		// Token: 0x0400C16D RID: 49517
		private const int RUN_MASK = 15;

		// Token: 0x0400C16E RID: 49518
		private const int STEPSIZE_64 = 8;

		// Token: 0x0400C16F RID: 49519
		private const int STEPSIZE_32 = 4;

		// Token: 0x0400C170 RID: 49520
		private const int LZ4_64KLIMIT = 65547;

		// Token: 0x0400C171 RID: 49521
		private const int HASH_LOG = 12;

		// Token: 0x0400C172 RID: 49522
		private const int HASH_TABLESIZE = 4096;

		// Token: 0x0400C173 RID: 49523
		private const int HASH_ADJUST = 20;

		// Token: 0x0400C174 RID: 49524
		private const int HASH64K_LOG = 13;

		// Token: 0x0400C175 RID: 49525
		private const int HASH64K_TABLESIZE = 8192;

		// Token: 0x0400C176 RID: 49526
		private const int HASH64K_ADJUST = 19;

		// Token: 0x0400C177 RID: 49527
		private const int HASHHC_LOG = 15;

		// Token: 0x0400C178 RID: 49528
		private const int HASHHC_TABLESIZE = 32768;

		// Token: 0x0400C179 RID: 49529
		private const int HASHHC_ADJUST = 17;

		// Token: 0x0400C17A RID: 49530
		private static readonly int[] DECODER_TABLE_32;

		// Token: 0x0400C17B RID: 49531
		private static readonly int[] DECODER_TABLE_64;

		// Token: 0x0400C17C RID: 49532
		private static readonly int[] DEBRUIJN_TABLE_32;

		// Token: 0x0400C17D RID: 49533
		private static readonly int[] DEBRUIJN_TABLE_64;

		// Token: 0x0400C17E RID: 49534
		private const int MAX_NB_ATTEMPTS = 256;

		// Token: 0x0400C17F RID: 49535
		private const int OPTIMAL_ML = 18;

		// Token: 0x0200119B RID: 4507
		private class LZ4HC_Data_Structure
		{
			// Token: 0x0400C180 RID: 49536
			public byte[] src;

			// Token: 0x0400C181 RID: 49537
			public int src_base;

			// Token: 0x0400C182 RID: 49538
			public int src_end;

			// Token: 0x0400C183 RID: 49539
			public int src_LASTLITERALS;

			// Token: 0x0400C184 RID: 49540
			public byte[] dst;

			// Token: 0x0400C185 RID: 49541
			public int dst_base;

			// Token: 0x0400C186 RID: 49542
			public int dst_len;

			// Token: 0x0400C187 RID: 49543
			public int dst_end;

			// Token: 0x0400C188 RID: 49544
			public int[] hashTable;

			// Token: 0x0400C189 RID: 49545
			public ushort[] chainTable;

			// Token: 0x0400C18A RID: 49546
			public int nextToUpdate;
		}
	}
}
