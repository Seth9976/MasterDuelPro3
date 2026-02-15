using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Unity.Burst.Intrinsics
{
	// Token: 0x02000039 RID: 57
	[BurstCompile]
	public static class X86
	{
		// Token: 0x06000AB1 RID: 2737 RVA: 0x00006534 File Offset: 0x00004734
		private unsafe static v128 GenericCSharpLoad(void* ptr)
		{
			return *(v128*)ptr;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00006545 File Offset: 0x00004745
		private unsafe static void GenericCSharpStore(void* ptr, v128 val)
		{
			*(v128*)ptr = val;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00008747 File Offset: 0x00006947
		private static sbyte Saturate_To_Int8(int val)
		{
			if (val > 127)
			{
				return sbyte.MaxValue;
			}
			if (val < -128)
			{
				return sbyte.MinValue;
			}
			return (sbyte)val;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0000875B File Offset: 0x0000695B
		private static byte Saturate_To_UnsignedInt8(int val)
		{
			if (val > 255)
			{
				return byte.MaxValue;
			}
			if (val < 0)
			{
				return 0;
			}
			return (byte)val;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00008773 File Offset: 0x00006973
		private static short Saturate_To_Int16(int val)
		{
			if (val > 32767)
			{
				return short.MaxValue;
			}
			if (val < -32768)
			{
				return short.MinValue;
			}
			return (short)val;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00008793 File Offset: 0x00006993
		private static ushort Saturate_To_UnsignedInt16(int val)
		{
			if (val > 65535)
			{
				return ushort.MaxValue;
			}
			if (val < 0)
			{
				return 0;
			}
			return (ushort)val;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000087AB File Offset: 0x000069AB
		private static bool IsNaN(uint v)
		{
			return (v & 2147483647U) > 2139095040U;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000087BB File Offset: 0x000069BB
		private static bool IsNaN(ulong v)
		{
			return (v & 9223372036854775807UL) > 9218868437227405312UL;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000024D5 File Offset: 0x000006D5
		private static void BurstIntrinsicSetCSRFromManaged(int _)
		{
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x000024DA File Offset: 0x000006DA
		private static int BurstIntrinsicGetCSRFromManaged()
		{
			return 0;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000087D3 File Offset: 0x000069D3
		internal static int getcsr_raw()
		{
			return X86.DoGetCSRTrampoline();
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000087DA File Offset: 0x000069DA
		internal static void setcsr_raw(int bits)
		{
			X86.DoSetCSRTrampoline(bits);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000087E2 File Offset: 0x000069E2
		[BurstCompile(CompileSynchronously = true)]
		private static void DoSetCSRTrampoline(int bits)
		{
			if (X86.Sse.IsSseSupported)
			{
				X86.BurstIntrinsicSetCSRFromManaged(bits);
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x000087F1 File Offset: 0x000069F1
		[BurstCompile(CompileSynchronously = true)]
		private static int DoGetCSRTrampoline()
		{
			if (X86.Sse.IsSseSupported)
			{
				return X86.BurstIntrinsicGetCSRFromManaged();
			}
			return 0;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00008801 File Offset: 0x00006A01
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x00008808 File Offset: 0x00006A08
		public static X86.MXCSRBits MXCSR
		{
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			get
			{
				return (X86.MXCSRBits)X86.getcsr_raw();
			}
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			set
			{
				X86.setcsr_raw((int)value);
			}
		}

		// Token: 0x0200003A RID: 58
		public static class Avx
		{
			// Token: 0x17000041 RID: 65
			// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x000024DA File Offset: 0x000006DA
			public static bool IsAvxSupported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000AC2 RID: 2754 RVA: 0x00008810 File Offset: 0x00006A10
			[DebuggerStepThrough]
			public static v256 mm256_add_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.add_pd(a.Lo128, b.Lo128), X86.Sse2.add_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AC3 RID: 2755 RVA: 0x00008839 File Offset: 0x00006A39
			[DebuggerStepThrough]
			public static v256 mm256_add_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.add_ps(a.Lo128, b.Lo128), X86.Sse.add_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AC4 RID: 2756 RVA: 0x00008862 File Offset: 0x00006A62
			[DebuggerStepThrough]
			public static v256 mm256_addsub_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse3.addsub_pd(a.Lo128, b.Lo128), X86.Sse3.addsub_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AC5 RID: 2757 RVA: 0x0000888B File Offset: 0x00006A8B
			[DebuggerStepThrough]
			public static v256 mm256_addsub_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse3.addsub_ps(a.Lo128, b.Lo128), X86.Sse3.addsub_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AC6 RID: 2758 RVA: 0x000088B4 File Offset: 0x00006AB4
			[DebuggerStepThrough]
			public static v256 mm256_and_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.and_pd(a.Lo128, b.Lo128), X86.Sse2.and_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AC7 RID: 2759 RVA: 0x000088DD File Offset: 0x00006ADD
			[DebuggerStepThrough]
			public static v256 mm256_and_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.and_ps(a.Lo128, b.Lo128), X86.Sse.and_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AC8 RID: 2760 RVA: 0x00008906 File Offset: 0x00006B06
			[DebuggerStepThrough]
			public static v256 mm256_andnot_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.andnot_pd(a.Lo128, b.Lo128), X86.Sse2.andnot_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AC9 RID: 2761 RVA: 0x0000892F File Offset: 0x00006B2F
			[DebuggerStepThrough]
			public static v256 mm256_andnot_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.andnot_ps(a.Lo128, b.Lo128), X86.Sse.andnot_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000ACA RID: 2762 RVA: 0x00008958 File Offset: 0x00006B58
			[DebuggerStepThrough]
			public static v256 mm256_blend_pd(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Sse4_1.blend_pd(a.Lo128, b.Lo128, imm8 & 3), X86.Sse4_1.blend_pd(a.Hi128, b.Hi128, imm8 >> 2));
			}

			// Token: 0x06000ACB RID: 2763 RVA: 0x00008987 File Offset: 0x00006B87
			[DebuggerStepThrough]
			public static v256 mm256_blend_ps(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Sse4_1.blend_ps(a.Lo128, b.Lo128, imm8 & 15), X86.Sse4_1.blend_ps(a.Hi128, b.Hi128, imm8 >> 4));
			}

			// Token: 0x06000ACC RID: 2764 RVA: 0x000089B7 File Offset: 0x00006BB7
			[DebuggerStepThrough]
			public static v256 mm256_blendv_pd(v256 a, v256 b, v256 mask)
			{
				return new v256(X86.Sse4_1.blendv_pd(a.Lo128, b.Lo128, mask.Lo128), X86.Sse4_1.blendv_pd(a.Hi128, b.Hi128, mask.Hi128));
			}

			// Token: 0x06000ACD RID: 2765 RVA: 0x000089EC File Offset: 0x00006BEC
			[DebuggerStepThrough]
			public static v256 mm256_blendv_ps(v256 a, v256 b, v256 mask)
			{
				return new v256(X86.Sse4_1.blendv_ps(a.Lo128, b.Lo128, mask.Lo128), X86.Sse4_1.blendv_ps(a.Hi128, b.Hi128, mask.Hi128));
			}

			// Token: 0x06000ACE RID: 2766 RVA: 0x00008A21 File Offset: 0x00006C21
			[DebuggerStepThrough]
			public static v256 mm256_div_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.div_pd(a.Lo128, b.Lo128), X86.Sse2.div_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000ACF RID: 2767 RVA: 0x00008A4A File Offset: 0x00006C4A
			[DebuggerStepThrough]
			public static v256 mm256_div_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.div_ps(a.Lo128, b.Lo128), X86.Sse.div_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AD0 RID: 2768 RVA: 0x00008A73 File Offset: 0x00006C73
			[DebuggerStepThrough]
			public static v256 mm256_dp_ps(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Sse4_1.dp_ps(a.Lo128, b.Lo128, imm8), X86.Sse4_1.dp_ps(a.Hi128, b.Hi128, imm8));
			}

			// Token: 0x06000AD1 RID: 2769 RVA: 0x00008A9E File Offset: 0x00006C9E
			[DebuggerStepThrough]
			public static v256 mm256_hadd_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse3.hadd_pd(a.Lo128, b.Lo128), X86.Sse3.hadd_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AD2 RID: 2770 RVA: 0x00008AC7 File Offset: 0x00006CC7
			[DebuggerStepThrough]
			public static v256 mm256_hadd_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse3.hadd_ps(a.Lo128, b.Lo128), X86.Sse3.hadd_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AD3 RID: 2771 RVA: 0x00008AF0 File Offset: 0x00006CF0
			[DebuggerStepThrough]
			public static v256 mm256_hsub_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse3.hsub_pd(a.Lo128, b.Lo128), X86.Sse3.hsub_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AD4 RID: 2772 RVA: 0x00008B19 File Offset: 0x00006D19
			[DebuggerStepThrough]
			public static v256 mm256_hsub_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse3.hsub_ps(a.Lo128, b.Lo128), X86.Sse3.hsub_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AD5 RID: 2773 RVA: 0x00008B42 File Offset: 0x00006D42
			[DebuggerStepThrough]
			public static v256 mm256_max_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.max_pd(a.Lo128, b.Lo128), X86.Sse2.max_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AD6 RID: 2774 RVA: 0x00008B6B File Offset: 0x00006D6B
			[DebuggerStepThrough]
			public static v256 mm256_max_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.max_ps(a.Lo128, b.Lo128), X86.Sse.max_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AD7 RID: 2775 RVA: 0x00008B94 File Offset: 0x00006D94
			[DebuggerStepThrough]
			public static v256 mm256_min_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.min_pd(a.Lo128, b.Lo128), X86.Sse2.min_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AD8 RID: 2776 RVA: 0x00008BBD File Offset: 0x00006DBD
			[DebuggerStepThrough]
			public static v256 mm256_min_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.min_ps(a.Lo128, b.Lo128), X86.Sse.min_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AD9 RID: 2777 RVA: 0x00008BE6 File Offset: 0x00006DE6
			[DebuggerStepThrough]
			public static v256 mm256_mul_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.mul_pd(a.Lo128, b.Lo128), X86.Sse2.mul_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000ADA RID: 2778 RVA: 0x00008C0F File Offset: 0x00006E0F
			[DebuggerStepThrough]
			public static v256 mm256_mul_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.mul_ps(a.Lo128, b.Lo128), X86.Sse.mul_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000ADB RID: 2779 RVA: 0x00008C38 File Offset: 0x00006E38
			[DebuggerStepThrough]
			public static v256 mm256_or_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.or_pd(a.Lo128, b.Lo128), X86.Sse2.or_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000ADC RID: 2780 RVA: 0x00008C61 File Offset: 0x00006E61
			[DebuggerStepThrough]
			public static v256 mm256_or_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.or_ps(a.Lo128, b.Lo128), X86.Sse.or_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000ADD RID: 2781 RVA: 0x00008C8A File Offset: 0x00006E8A
			[DebuggerStepThrough]
			public static v256 mm256_shuffle_pd(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Sse2.shuffle_pd(a.Lo128, b.Lo128, imm8 & 3), X86.Sse2.shuffle_pd(a.Hi128, b.Hi128, imm8 >> 2));
			}

			// Token: 0x06000ADE RID: 2782 RVA: 0x00008CB9 File Offset: 0x00006EB9
			[DebuggerStepThrough]
			public static v256 mm256_shuffle_ps(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Sse.shuffle_ps(a.Lo128, b.Lo128, imm8), X86.Sse.shuffle_ps(a.Hi128, b.Hi128, imm8));
			}

			// Token: 0x06000ADF RID: 2783 RVA: 0x00008CE4 File Offset: 0x00006EE4
			[DebuggerStepThrough]
			public static v256 mm256_sub_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.sub_pd(a.Lo128, b.Lo128), X86.Sse2.sub_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AE0 RID: 2784 RVA: 0x00008D0D File Offset: 0x00006F0D
			[DebuggerStepThrough]
			public static v256 mm256_sub_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.sub_ps(a.Lo128, b.Lo128), X86.Sse.sub_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AE1 RID: 2785 RVA: 0x00008D36 File Offset: 0x00006F36
			[DebuggerStepThrough]
			public static v256 mm256_xor_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.xor_pd(a.Lo128, b.Lo128), X86.Sse2.xor_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AE2 RID: 2786 RVA: 0x00008D5F File Offset: 0x00006F5F
			[DebuggerStepThrough]
			public static v256 mm256_xor_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.xor_ps(a.Lo128, b.Lo128), X86.Sse.xor_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AE3 RID: 2787 RVA: 0x00008D88 File Offset: 0x00006F88
			[DebuggerStepThrough]
			public static v128 cmp_pd(v128 a, v128 b, int imm8)
			{
				switch (imm8 & 31)
				{
				case 0:
					return X86.Sse2.cmpeq_pd(a, b);
				case 1:
					return X86.Sse2.cmplt_pd(a, b);
				case 2:
					return X86.Sse2.cmple_pd(a, b);
				case 3:
					return X86.Sse2.cmpunord_pd(a, b);
				case 4:
					return X86.Sse2.cmpneq_pd(a, b);
				case 5:
					return X86.Sse2.cmpnlt_pd(a, b);
				case 6:
					return X86.Sse2.cmpnle_pd(a, b);
				case 7:
					return X86.Sse2.cmpord_pd(a, b);
				case 8:
					return X86.Sse2.or_pd(X86.Sse2.cmpeq_pd(a, b), X86.Sse2.cmpunord_pd(a, b));
				case 9:
					return X86.Sse2.or_pd(X86.Sse2.cmpnge_pd(a, b), X86.Sse2.cmpunord_pd(a, b));
				case 10:
					return X86.Sse2.or_pd(X86.Sse2.cmpngt_pd(a, b), X86.Sse2.cmpunord_pd(a, b));
				case 11:
					return default(v128);
				case 12:
					return X86.Sse2.and_pd(X86.Sse2.cmpneq_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				case 13:
					return X86.Sse2.and_pd(X86.Sse2.cmpge_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				case 14:
					return X86.Sse2.and_pd(X86.Sse2.cmpgt_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				case 15:
					return new v128(-1);
				case 16:
					return X86.Sse2.and_pd(X86.Sse2.cmpeq_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				case 17:
					return X86.Sse2.and_pd(X86.Sse2.cmplt_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				case 18:
					return X86.Sse2.and_pd(X86.Sse2.cmple_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				case 19:
					return X86.Sse2.cmpunord_pd(a, b);
				case 20:
					return X86.Sse2.cmpneq_pd(a, b);
				case 21:
					return X86.Sse2.or_pd(X86.Sse2.cmpnlt_pd(a, b), X86.Sse2.cmpunord_pd(a, b));
				case 22:
					return X86.Sse2.or_pd(X86.Sse2.cmpnle_pd(a, b), X86.Sse2.cmpunord_pd(a, b));
				case 23:
					return X86.Sse2.cmpord_pd(a, b);
				case 24:
					return X86.Sse2.or_pd(X86.Sse2.cmpeq_pd(a, b), X86.Sse2.cmpunord_pd(a, b));
				case 25:
					return X86.Sse2.or_pd(X86.Sse2.cmpnge_pd(a, b), X86.Sse2.cmpunord_pd(a, b));
				case 26:
					return X86.Sse2.or_pd(X86.Sse2.cmpngt_pd(a, b), X86.Sse2.cmpunord_pd(a, b));
				case 27:
					return default(v128);
				case 28:
					return X86.Sse2.and_pd(X86.Sse2.cmpneq_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				case 29:
					return X86.Sse2.and_pd(X86.Sse2.cmpge_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				case 30:
					return X86.Sse2.and_pd(X86.Sse2.cmpgt_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				default:
					return new v128(-1);
				}
			}

			// Token: 0x06000AE4 RID: 2788 RVA: 0x00008FEE File Offset: 0x000071EE
			[DebuggerStepThrough]
			public static v256 mm256_cmp_pd(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Avx.cmp_pd(a.Lo128, b.Lo128, imm8), X86.Avx.cmp_pd(a.Hi128, b.Hi128, imm8));
			}

			// Token: 0x06000AE5 RID: 2789 RVA: 0x0000901C File Offset: 0x0000721C
			[DebuggerStepThrough]
			public static v128 cmp_ps(v128 a, v128 b, int imm8)
			{
				switch (imm8 & 31)
				{
				case 0:
					return X86.Sse.cmpeq_ps(a, b);
				case 1:
					return X86.Sse.cmplt_ps(a, b);
				case 2:
					return X86.Sse.cmple_ps(a, b);
				case 3:
					return X86.Sse.cmpunord_ps(a, b);
				case 4:
					return X86.Sse.cmpneq_ps(a, b);
				case 5:
					return X86.Sse.cmpnlt_ps(a, b);
				case 6:
					return X86.Sse.cmpnle_ps(a, b);
				case 7:
					return X86.Sse.cmpord_ps(a, b);
				case 8:
					return X86.Sse.or_ps(X86.Sse.cmpeq_ps(a, b), X86.Sse.cmpunord_ps(a, b));
				case 9:
					return X86.Sse.or_ps(X86.Sse.cmpnge_ps(a, b), X86.Sse.cmpunord_ps(a, b));
				case 10:
					return X86.Sse.or_ps(X86.Sse.cmpngt_ps(a, b), X86.Sse.cmpunord_ps(a, b));
				case 11:
					return default(v128);
				case 12:
					return X86.Sse.and_ps(X86.Sse.cmpneq_ps(a, b), X86.Sse.cmpord_ps(a, b));
				case 13:
					return X86.Sse.and_ps(X86.Sse.cmpge_ps(a, b), X86.Sse.cmpord_ps(a, b));
				case 14:
					return X86.Sse.and_ps(X86.Sse.cmpgt_ps(a, b), X86.Sse.cmpord_ps(a, b));
				case 15:
					return new v128(-1);
				case 16:
					return X86.Sse.and_ps(X86.Sse.cmpeq_ps(a, b), X86.Sse.cmpord_ps(a, b));
				case 17:
					return X86.Sse.and_ps(X86.Sse.cmplt_ps(a, b), X86.Sse.cmpord_ps(a, b));
				case 18:
					return X86.Sse.and_ps(X86.Sse.cmple_ps(a, b), X86.Sse.cmpord_ps(a, b));
				case 19:
					return X86.Sse.cmpunord_ps(a, b);
				case 20:
					return X86.Sse.cmpneq_ps(a, b);
				case 21:
					return X86.Sse.or_ps(X86.Sse.cmpnlt_ps(a, b), X86.Sse.cmpunord_ps(a, b));
				case 22:
					return X86.Sse.or_ps(X86.Sse.cmpnle_ps(a, b), X86.Sse.cmpunord_ps(a, b));
				case 23:
					return X86.Sse.cmpord_ps(a, b);
				case 24:
					return X86.Sse.or_ps(X86.Sse.cmpeq_ps(a, b), X86.Sse.cmpunord_ps(a, b));
				case 25:
					return X86.Sse.or_ps(X86.Sse.cmpnge_ps(a, b), X86.Sse.cmpunord_ps(a, b));
				case 26:
					return X86.Sse.or_ps(X86.Sse.cmpngt_ps(a, b), X86.Sse.cmpunord_ps(a, b));
				case 27:
					return default(v128);
				case 28:
					return X86.Sse.and_ps(X86.Sse.cmpneq_ps(a, b), X86.Sse.cmpord_ps(a, b));
				case 29:
					return X86.Sse.and_ps(X86.Sse.cmpge_ps(a, b), X86.Sse.cmpord_ps(a, b));
				case 30:
					return X86.Sse.and_ps(X86.Sse.cmpgt_ps(a, b), X86.Sse.cmpord_ps(a, b));
				default:
					return new v128(-1);
				}
			}

			// Token: 0x06000AE6 RID: 2790 RVA: 0x00009282 File Offset: 0x00007482
			[DebuggerStepThrough]
			public static v256 mm256_cmp_ps(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Avx.cmp_ps(a.Lo128, b.Lo128, imm8), X86.Avx.cmp_ps(a.Hi128, b.Hi128, imm8));
			}

			// Token: 0x06000AE7 RID: 2791 RVA: 0x000092AD File Offset: 0x000074AD
			[DebuggerStepThrough]
			public static v128 cmp_sd(v128 a, v128 b, int imm8)
			{
				return new v128(X86.Avx.cmp_pd(a, b, imm8).ULong0, a.ULong1);
			}

			// Token: 0x06000AE8 RID: 2792 RVA: 0x000092C7 File Offset: 0x000074C7
			[DebuggerStepThrough]
			public static v128 cmp_ss(v128 a, v128 b, int imm8)
			{
				return new v128(X86.Avx.cmp_ps(a, b, imm8).UInt0, a.UInt1, a.UInt2, a.UInt3);
			}

			// Token: 0x06000AE9 RID: 2793 RVA: 0x000092ED File Offset: 0x000074ED
			[DebuggerStepThrough]
			public static v256 mm256_cvtepi32_pd(v128 a)
			{
				return new v256((double)a.SInt0, (double)a.SInt1, (double)a.SInt2, (double)a.SInt3);
			}

			// Token: 0x06000AEA RID: 2794 RVA: 0x00009310 File Offset: 0x00007510
			[DebuggerStepThrough]
			public static v256 mm256_cvtepi32_ps(v256 a)
			{
				return new v256(X86.Sse2.cvtepi32_ps(a.Lo128), X86.Sse2.cvtepi32_ps(a.Hi128));
			}

			// Token: 0x06000AEB RID: 2795 RVA: 0x00009330 File Offset: 0x00007530
			[DebuggerStepThrough]
			public static v128 mm256_cvtpd_ps(v256 a)
			{
				v128 lo = X86.Sse2.cvtpd_ps(a.Lo128);
				v128 hi = X86.Sse2.cvtpd_ps(a.Hi128);
				return new v128(lo.Float0, lo.Float1, hi.Float0, hi.Float1);
			}

			// Token: 0x06000AEC RID: 2796 RVA: 0x00009372 File Offset: 0x00007572
			[DebuggerStepThrough]
			public static v256 mm256_cvtps_epi32(v256 a)
			{
				return new v256(X86.Sse2.cvtps_epi32(a.Lo128), X86.Sse2.cvtps_epi32(a.Hi128));
			}

			// Token: 0x06000AED RID: 2797 RVA: 0x0000938F File Offset: 0x0000758F
			[DebuggerStepThrough]
			public static v256 mm256_cvtps_pd(v128 a)
			{
				return new v256((double)a.Float0, (double)a.Float1, (double)a.Float2, (double)a.Float3);
			}

			// Token: 0x06000AEE RID: 2798 RVA: 0x000093B2 File Offset: 0x000075B2
			[DebuggerStepThrough]
			public static v128 mm256_cvttpd_epi32(v256 a)
			{
				return new v128((int)a.Double0, (int)a.Double1, (int)a.Double2, (int)a.Double3);
			}

			// Token: 0x06000AEF RID: 2799 RVA: 0x000093D8 File Offset: 0x000075D8
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v128 mm256_cvtpd_epi32(v256 a)
			{
				v128 q = X86.Sse2.cvtpd_epi32(new v128(a.Double0, a.Double1));
				v128 r = X86.Sse2.cvtpd_epi32(new v128(a.Double2, a.Double3));
				return new v128(q.SInt0, q.SInt1, r.SInt0, r.SInt1);
			}

			// Token: 0x06000AF0 RID: 2800 RVA: 0x00009430 File Offset: 0x00007630
			[DebuggerStepThrough]
			public static v256 mm256_cvttps_epi32(v256 a)
			{
				return new v256(X86.Sse2.cvttps_epi32(a.Lo128), X86.Sse2.cvttps_epi32(a.Hi128));
			}

			// Token: 0x06000AF1 RID: 2801 RVA: 0x0000944D File Offset: 0x0000764D
			[DebuggerStepThrough]
			public static float mm256_cvtss_f32(v256 a)
			{
				return a.Float0;
			}

			// Token: 0x06000AF2 RID: 2802 RVA: 0x00009455 File Offset: 0x00007655
			[DebuggerStepThrough]
			public static v128 mm256_extractf128_ps(v256 a, int imm8)
			{
				if (imm8 == 0)
				{
					return a.Lo128;
				}
				return a.Hi128;
			}

			// Token: 0x06000AF3 RID: 2803 RVA: 0x00009455 File Offset: 0x00007655
			[DebuggerStepThrough]
			public static v128 mm256_extractf128_pd(v256 a, int imm8)
			{
				if (imm8 == 0)
				{
					return a.Lo128;
				}
				return a.Hi128;
			}

			// Token: 0x06000AF4 RID: 2804 RVA: 0x00009455 File Offset: 0x00007655
			[DebuggerStepThrough]
			public static v128 mm256_extractf128_si256(v256 a, int imm8)
			{
				if (imm8 == 0)
				{
					return a.Lo128;
				}
				return a.Hi128;
			}

			// Token: 0x06000AF5 RID: 2805 RVA: 0x000024D5 File Offset: 0x000006D5
			[DebuggerStepThrough]
			public static void mm256_zeroall()
			{
			}

			// Token: 0x06000AF6 RID: 2806 RVA: 0x000024D5 File Offset: 0x000006D5
			[DebuggerStepThrough]
			public static void mm256_zeroupper()
			{
			}

			// Token: 0x06000AF7 RID: 2807 RVA: 0x00009468 File Offset: 0x00007668
			[DebuggerStepThrough]
			public unsafe static v128 permutevar_ps(v128 a, v128 b)
			{
				v128 dst = default(v128);
				uint* dptr = &dst.UInt0;
				uint* aptr = &a.UInt0;
				int* bptr = &b.SInt0;
				for (int i = 0; i < 4; i++)
				{
					int ndx = bptr[i] & 3;
					dptr[i] = aptr[ndx];
				}
				return dst;
			}

			// Token: 0x06000AF8 RID: 2808 RVA: 0x000094C5 File Offset: 0x000076C5
			[DebuggerStepThrough]
			public static v256 mm256_permutevar_ps(v256 a, v256 b)
			{
				return new v256(X86.Avx.permutevar_ps(a.Lo128, b.Lo128), X86.Avx.permutevar_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AF9 RID: 2809 RVA: 0x000094EE File Offset: 0x000076EE
			[DebuggerStepThrough]
			public static v128 permute_ps(v128 a, int imm8)
			{
				return X86.Sse2.shuffle_epi32(a, imm8);
			}

			// Token: 0x06000AFA RID: 2810 RVA: 0x000094F7 File Offset: 0x000076F7
			[DebuggerStepThrough]
			public static v256 mm256_permute_ps(v256 a, int imm8)
			{
				return new v256(X86.Avx.permute_ps(a.Lo128, imm8), X86.Avx.permute_ps(a.Hi128, imm8));
			}

			// Token: 0x06000AFB RID: 2811 RVA: 0x00009518 File Offset: 0x00007718
			[DebuggerStepThrough]
			public unsafe static v128 permutevar_pd(v128 a, v128 b)
			{
				v128 dst = default(v128);
				double* ptr = &dst.Double0;
				double* aptr = &a.Double0;
				*ptr = aptr[(int)(b.SLong0 & 2L) >> 1];
				ptr[1] = aptr[(int)(b.SLong1 & 2L) >> 1];
				return dst;
			}

			// Token: 0x06000AFC RID: 2812 RVA: 0x00009568 File Offset: 0x00007768
			[DebuggerStepThrough]
			public unsafe static v256 mm256_permutevar_pd(v256 a, v256 b)
			{
				v256 dst = default(v256);
				double* ptr = &dst.Double0;
				double* aptr = &a.Double0;
				*ptr = aptr[(int)(b.SLong0 & 2L) >> 1];
				ptr[1] = aptr[(int)(b.SLong1 & 2L) >> 1];
				ptr[2] = aptr[2 + ((int)(b.SLong2 & 2L) >> 1)];
				ptr[3] = aptr[2 + ((int)(b.SLong3 & 2L) >> 1)];
				return dst;
			}

			// Token: 0x06000AFD RID: 2813 RVA: 0x000095EE File Offset: 0x000077EE
			[DebuggerStepThrough]
			public static v256 mm256_permute_pd(v256 a, int imm8)
			{
				return new v256(X86.Avx.permute_pd(a.Lo128, imm8 & 3), X86.Avx.permute_pd(a.Hi128, imm8 >> 2));
			}

			// Token: 0x06000AFE RID: 2814 RVA: 0x00009614 File Offset: 0x00007814
			[DebuggerStepThrough]
			public unsafe static v128 permute_pd(v128 a, int imm8)
			{
				v128 dst = default(v128);
				double* ptr = &dst.Double0;
				double* aptr = &a.Double0;
				*ptr = aptr[imm8 & 1];
				ptr[1] = aptr[(imm8 >> 1) & 1];
				return dst;
			}

			// Token: 0x06000AFF RID: 2815 RVA: 0x00009654 File Offset: 0x00007854
			private static v128 Select4(v256 src1, v256 src2, int control)
			{
				switch (control & 3)
				{
				case 0:
					return src1.Lo128;
				case 1:
					return src1.Hi128;
				case 2:
					return src2.Lo128;
				default:
					return src2.Hi128;
				}
			}

			// Token: 0x06000B00 RID: 2816 RVA: 0x00009694 File Offset: 0x00007894
			[DebuggerStepThrough]
			public static v256 mm256_permute2f128_ps(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Avx.Select4(a, b, imm8), X86.Avx.Select4(a, b, imm8 >> 4));
			}

			// Token: 0x06000B01 RID: 2817 RVA: 0x000096AD File Offset: 0x000078AD
			[DebuggerStepThrough]
			public static v256 mm256_permute2f128_pd(v256 a, v256 b, int imm8)
			{
				return X86.Avx.mm256_permute2f128_ps(a, b, imm8);
			}

			// Token: 0x06000B02 RID: 2818 RVA: 0x000096AD File Offset: 0x000078AD
			[DebuggerStepThrough]
			public static v256 mm256_permute2f128_si256(v256 a, v256 b, int imm8)
			{
				return X86.Avx.mm256_permute2f128_ps(a, b, imm8);
			}

			// Token: 0x06000B03 RID: 2819 RVA: 0x000096B7 File Offset: 0x000078B7
			[DebuggerStepThrough]
			public unsafe static v256 mm256_broadcast_ss(void* ptr)
			{
				return new v256(*(uint*)ptr);
			}

			// Token: 0x06000B04 RID: 2820 RVA: 0x000096C0 File Offset: 0x000078C0
			[DebuggerStepThrough]
			public unsafe static v128 broadcast_ss(void* ptr)
			{
				return new v128(*(uint*)ptr);
			}

			// Token: 0x06000B05 RID: 2821 RVA: 0x000096C9 File Offset: 0x000078C9
			[DebuggerStepThrough]
			public unsafe static v256 mm256_broadcast_sd(void* ptr)
			{
				return new v256(*(double*)ptr);
			}

			// Token: 0x06000B06 RID: 2822 RVA: 0x000096D2 File Offset: 0x000078D2
			[DebuggerStepThrough]
			public unsafe static v256 mm256_broadcast_ps(void* ptr)
			{
				v128 v = X86.Sse.loadu_ps(ptr);
				return new v256(v, v);
			}

			// Token: 0x06000B07 RID: 2823 RVA: 0x000096E0 File Offset: 0x000078E0
			[DebuggerStepThrough]
			public unsafe static v256 mm256_broadcast_pd(void* ptr)
			{
				return X86.Avx.mm256_broadcast_ps(ptr);
			}

			// Token: 0x06000B08 RID: 2824 RVA: 0x000096E8 File Offset: 0x000078E8
			[DebuggerStepThrough]
			public static v256 mm256_insertf128_ps(v256 a, v128 b, int imm8)
			{
				if ((imm8 & 1) == 0)
				{
					return new v256(b, a.Hi128);
				}
				return new v256(a.Lo128, b);
			}

			// Token: 0x06000B09 RID: 2825 RVA: 0x00009708 File Offset: 0x00007908
			[DebuggerStepThrough]
			public static v256 mm256_insertf128_pd(v256 a, v128 b, int imm8)
			{
				return X86.Avx.mm256_insertf128_ps(a, b, imm8);
			}

			// Token: 0x06000B0A RID: 2826 RVA: 0x00009708 File Offset: 0x00007908
			[DebuggerStepThrough]
			public static v256 mm256_insertf128_si256(v256 a, v128 b, int imm8)
			{
				return X86.Avx.mm256_insertf128_ps(a, b, imm8);
			}

			// Token: 0x06000B0B RID: 2827 RVA: 0x00009712 File Offset: 0x00007912
			[DebuggerStepThrough]
			public unsafe static v256 mm256_load_ps(void* ptr)
			{
				return *(v256*)ptr;
			}

			// Token: 0x06000B0C RID: 2828 RVA: 0x0000971A File Offset: 0x0000791A
			[DebuggerStepThrough]
			public unsafe static void mm256_store_ps(void* ptr, v256 val)
			{
				*(v256*)ptr = val;
			}

			// Token: 0x06000B0D RID: 2829 RVA: 0x00009723 File Offset: 0x00007923
			[DebuggerStepThrough]
			public unsafe static v256 mm256_load_pd(void* ptr)
			{
				return X86.Avx.mm256_load_ps(ptr);
			}

			// Token: 0x06000B0E RID: 2830 RVA: 0x0000972B File Offset: 0x0000792B
			[DebuggerStepThrough]
			public unsafe static void mm256_store_pd(void* ptr, v256 a)
			{
				X86.Avx.mm256_store_ps(ptr, a);
			}

			// Token: 0x06000B0F RID: 2831 RVA: 0x00009723 File Offset: 0x00007923
			[DebuggerStepThrough]
			public unsafe static v256 mm256_loadu_pd(void* ptr)
			{
				return X86.Avx.mm256_load_ps(ptr);
			}

			// Token: 0x06000B10 RID: 2832 RVA: 0x0000972B File Offset: 0x0000792B
			[DebuggerStepThrough]
			public unsafe static void mm256_storeu_pd(void* ptr, v256 a)
			{
				X86.Avx.mm256_store_ps(ptr, a);
			}

			// Token: 0x06000B11 RID: 2833 RVA: 0x00009723 File Offset: 0x00007923
			[DebuggerStepThrough]
			public unsafe static v256 mm256_loadu_ps(void* ptr)
			{
				return X86.Avx.mm256_load_ps(ptr);
			}

			// Token: 0x06000B12 RID: 2834 RVA: 0x0000972B File Offset: 0x0000792B
			[DebuggerStepThrough]
			public unsafe static void mm256_storeu_ps(void* ptr, v256 a)
			{
				X86.Avx.mm256_store_ps(ptr, a);
			}

			// Token: 0x06000B13 RID: 2835 RVA: 0x00009723 File Offset: 0x00007923
			[DebuggerStepThrough]
			public unsafe static v256 mm256_load_si256(void* ptr)
			{
				return X86.Avx.mm256_load_ps(ptr);
			}

			// Token: 0x06000B14 RID: 2836 RVA: 0x0000972B File Offset: 0x0000792B
			[DebuggerStepThrough]
			public unsafe static void mm256_store_si256(void* ptr, v256 v)
			{
				X86.Avx.mm256_store_ps(ptr, v);
			}

			// Token: 0x06000B15 RID: 2837 RVA: 0x00009723 File Offset: 0x00007923
			[DebuggerStepThrough]
			public unsafe static v256 mm256_loadu_si256(void* ptr)
			{
				return X86.Avx.mm256_load_ps(ptr);
			}

			// Token: 0x06000B16 RID: 2838 RVA: 0x0000972B File Offset: 0x0000792B
			[DebuggerStepThrough]
			public unsafe static void mm256_storeu_si256(void* ptr, v256 v)
			{
				X86.Avx.mm256_store_ps(ptr, v);
			}

			// Token: 0x06000B17 RID: 2839 RVA: 0x00009734 File Offset: 0x00007934
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public unsafe static v256 mm256_loadu2_m128(void* hiaddr, void* loaddr)
			{
				return X86.Avx.mm256_set_m128(X86.Sse.loadu_ps(hiaddr), X86.Sse.loadu_ps(loaddr));
			}

			// Token: 0x06000B18 RID: 2840 RVA: 0x00009747 File Offset: 0x00007947
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public unsafe static v256 mm256_loadu2_m128d(void* hiaddr, void* loaddr)
			{
				return X86.Avx.mm256_loadu2_m128(hiaddr, loaddr);
			}

			// Token: 0x06000B19 RID: 2841 RVA: 0x00009747 File Offset: 0x00007947
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public unsafe static v256 mm256_loadu2_m128i(void* hiaddr, void* loaddr)
			{
				return X86.Avx.mm256_loadu2_m128(hiaddr, loaddr);
			}

			// Token: 0x06000B1A RID: 2842 RVA: 0x00009750 File Offset: 0x00007950
			[DebuggerStepThrough]
			public static v256 mm256_set_m128(v128 hi, v128 lo)
			{
				return new v256(lo, hi);
			}

			// Token: 0x06000B1B RID: 2843 RVA: 0x00009759 File Offset: 0x00007959
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public unsafe static void mm256_storeu2_m128(void* hiaddr, void* loaddr, v256 val)
			{
				X86.Sse.storeu_ps(hiaddr, val.Hi128);
				X86.Sse.storeu_ps(loaddr, val.Lo128);
			}

			// Token: 0x06000B1C RID: 2844 RVA: 0x00009759 File Offset: 0x00007959
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public unsafe static void mm256_storeu2_m128d(void* hiaddr, void* loaddr, v256 val)
			{
				X86.Sse.storeu_ps(hiaddr, val.Hi128);
				X86.Sse.storeu_ps(loaddr, val.Lo128);
			}

			// Token: 0x06000B1D RID: 2845 RVA: 0x00009759 File Offset: 0x00007959
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public unsafe static void mm256_storeu2_m128i(void* hiaddr, void* loaddr, v256 val)
			{
				X86.Sse.storeu_ps(hiaddr, val.Hi128);
				X86.Sse.storeu_ps(loaddr, val.Lo128);
			}

			// Token: 0x06000B1E RID: 2846 RVA: 0x00009774 File Offset: 0x00007974
			[DebuggerStepThrough]
			public unsafe static v128 maskload_pd(void* mem_addr, v128 mask)
			{
				v128 result = default(v128);
				if (mask.SLong0 < 0L)
				{
					result.ULong0 = (ulong)(*(long*)mem_addr);
				}
				if (mask.SLong1 < 0L)
				{
					result.ULong1 = (ulong)(*(long*)((byte*)mem_addr + 8));
				}
				return result;
			}

			// Token: 0x06000B1F RID: 2847 RVA: 0x000097B4 File Offset: 0x000079B4
			[DebuggerStepThrough]
			public unsafe static v256 mm256_maskload_pd(void* mem_addr, v256 mask)
			{
				return new v256(X86.Avx.maskload_pd(mem_addr, mask.Lo128), X86.Avx.maskload_pd((void*)((byte*)mem_addr + 16), mask.Hi128));
			}

			// Token: 0x06000B20 RID: 2848 RVA: 0x000097D8 File Offset: 0x000079D8
			[DebuggerStepThrough]
			public unsafe static void maskstore_pd(void* mem_addr, v128 mask, v128 a)
			{
				if (mask.SLong0 < 0L)
				{
					*(long*)mem_addr = (long)a.ULong0;
				}
				if (mask.SLong1 < 0L)
				{
					*(long*)((byte*)mem_addr + 8) = (long)a.ULong1;
				}
			}

			// Token: 0x06000B21 RID: 2849 RVA: 0x0000980D File Offset: 0x00007A0D
			[DebuggerStepThrough]
			public unsafe static void mm256_maskstore_pd(void* mem_addr, v256 mask, v256 a)
			{
				X86.Avx.maskstore_pd(mem_addr, mask.Lo128, a.Lo128);
				X86.Avx.maskstore_pd((void*)((byte*)mem_addr + 16), mask.Hi128, a.Hi128);
			}

			// Token: 0x06000B22 RID: 2850 RVA: 0x00009838 File Offset: 0x00007A38
			[DebuggerStepThrough]
			public unsafe static v128 maskload_ps(void* mem_addr, v128 mask)
			{
				v128 result = default(v128);
				if (mask.SInt0 < 0)
				{
					result.UInt0 = *(uint*)mem_addr;
				}
				if (mask.SInt1 < 0)
				{
					result.UInt1 = *(uint*)((byte*)mem_addr + 4);
				}
				if (mask.SInt2 < 0)
				{
					result.UInt2 = *(uint*)((byte*)mem_addr + (IntPtr)2 * 4);
				}
				if (mask.SInt3 < 0)
				{
					result.UInt3 = *(uint*)((byte*)mem_addr + (IntPtr)3 * 4);
				}
				return result;
			}

			// Token: 0x06000B23 RID: 2851 RVA: 0x000098A4 File Offset: 0x00007AA4
			[DebuggerStepThrough]
			public unsafe static v256 mm256_maskload_ps(void* mem_addr, v256 mask)
			{
				return new v256(X86.Avx.maskload_ps(mem_addr, mask.Lo128), X86.Avx.maskload_ps((void*)((byte*)mem_addr + 16), mask.Hi128));
			}

			// Token: 0x06000B24 RID: 2852 RVA: 0x000098C8 File Offset: 0x00007AC8
			[DebuggerStepThrough]
			public unsafe static void maskstore_ps(void* mem_addr, v128 mask, v128 a)
			{
				if (mask.SInt0 < 0)
				{
					*(int*)mem_addr = (int)a.UInt0;
				}
				if (mask.SInt1 < 0)
				{
					*(int*)((byte*)mem_addr + 4) = (int)a.UInt1;
				}
				if (mask.SInt2 < 0)
				{
					*(int*)((byte*)mem_addr + (IntPtr)2 * 4) = (int)a.UInt2;
				}
				if (mask.SInt3 < 0)
				{
					*(int*)((byte*)mem_addr + (IntPtr)3 * 4) = (int)a.UInt3;
				}
			}

			// Token: 0x06000B25 RID: 2853 RVA: 0x00009927 File Offset: 0x00007B27
			[DebuggerStepThrough]
			public unsafe static void mm256_maskstore_ps(void* mem_addr, v256 mask, v256 a)
			{
				X86.Avx.maskstore_ps(mem_addr, mask.Lo128, a.Lo128);
				X86.Avx.maskstore_ps((void*)((byte*)mem_addr + 16), mask.Hi128, a.Hi128);
			}

			// Token: 0x06000B26 RID: 2854 RVA: 0x00009950 File Offset: 0x00007B50
			[DebuggerStepThrough]
			public static v256 mm256_movehdup_ps(v256 a)
			{
				return new v256(a.UInt1, a.UInt1, a.UInt3, a.UInt3, a.UInt5, a.UInt5, a.UInt7, a.UInt7);
			}

			// Token: 0x06000B27 RID: 2855 RVA: 0x00009987 File Offset: 0x00007B87
			[DebuggerStepThrough]
			public static v256 mm256_moveldup_ps(v256 a)
			{
				return new v256(a.UInt0, a.UInt0, a.UInt2, a.UInt2, a.UInt4, a.UInt4, a.UInt6, a.UInt6);
			}

			// Token: 0x06000B28 RID: 2856 RVA: 0x000099BE File Offset: 0x00007BBE
			[DebuggerStepThrough]
			public static v256 mm256_movedup_pd(v256 a)
			{
				return new v256(a.Double0, a.Double0, a.Double2, a.Double2);
			}

			// Token: 0x06000B29 RID: 2857 RVA: 0x00009712 File Offset: 0x00007912
			[DebuggerStepThrough]
			public unsafe static v256 mm256_lddqu_si256(void* mem_addr)
			{
				return *(v256*)mem_addr;
			}

			// Token: 0x06000B2A RID: 2858 RVA: 0x0000971A File Offset: 0x0000791A
			[DebuggerStepThrough]
			public unsafe static void mm256_stream_si256(void* mem_addr, v256 a)
			{
				*(v256*)mem_addr = a;
			}

			// Token: 0x06000B2B RID: 2859 RVA: 0x0000971A File Offset: 0x0000791A
			[DebuggerStepThrough]
			public unsafe static void mm256_stream_pd(void* mem_addr, v256 a)
			{
				*(v256*)mem_addr = a;
			}

			// Token: 0x06000B2C RID: 2860 RVA: 0x0000971A File Offset: 0x0000791A
			[DebuggerStepThrough]
			public unsafe static void mm256_stream_ps(void* mem_addr, v256 a)
			{
				*(v256*)mem_addr = a;
			}

			// Token: 0x06000B2D RID: 2861 RVA: 0x000099DD File Offset: 0x00007BDD
			[DebuggerStepThrough]
			public static v256 mm256_rcp_ps(v256 a)
			{
				return new v256(X86.Sse.rcp_ps(a.Lo128), X86.Sse.rcp_ps(a.Hi128));
			}

			// Token: 0x06000B2E RID: 2862 RVA: 0x000099FA File Offset: 0x00007BFA
			[DebuggerStepThrough]
			public static v256 mm256_rsqrt_ps(v256 a)
			{
				return new v256(X86.Sse.rsqrt_ps(a.Lo128), X86.Sse.rsqrt_ps(a.Hi128));
			}

			// Token: 0x06000B2F RID: 2863 RVA: 0x00009A17 File Offset: 0x00007C17
			[DebuggerStepThrough]
			public static v256 mm256_sqrt_pd(v256 a)
			{
				return new v256(X86.Sse2.sqrt_pd(a.Lo128), X86.Sse2.sqrt_pd(a.Hi128));
			}

			// Token: 0x06000B30 RID: 2864 RVA: 0x00009A34 File Offset: 0x00007C34
			[DebuggerStepThrough]
			public static v256 mm256_sqrt_ps(v256 a)
			{
				return new v256(X86.Sse.sqrt_ps(a.Lo128), X86.Sse.sqrt_ps(a.Hi128));
			}

			// Token: 0x06000B31 RID: 2865 RVA: 0x00009A51 File Offset: 0x00007C51
			[DebuggerStepThrough]
			public static v256 mm256_round_pd(v256 a, int rounding)
			{
				return new v256(X86.Sse4_1.round_pd(a.Lo128, rounding), X86.Sse4_1.round_pd(a.Hi128, rounding));
			}

			// Token: 0x06000B32 RID: 2866 RVA: 0x00009A70 File Offset: 0x00007C70
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_ceil_pd(v256 val)
			{
				return X86.Avx.mm256_round_pd(val, 2);
			}

			// Token: 0x06000B33 RID: 2867 RVA: 0x00009A79 File Offset: 0x00007C79
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_floor_pd(v256 val)
			{
				return X86.Avx.mm256_round_pd(val, 1);
			}

			// Token: 0x06000B34 RID: 2868 RVA: 0x00009A82 File Offset: 0x00007C82
			[DebuggerStepThrough]
			public static v256 mm256_round_ps(v256 a, int rounding)
			{
				return new v256(X86.Sse4_1.round_ps(a.Lo128, rounding), X86.Sse4_1.round_ps(a.Hi128, rounding));
			}

			// Token: 0x06000B35 RID: 2869 RVA: 0x00009AA1 File Offset: 0x00007CA1
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_ceil_ps(v256 val)
			{
				return X86.Avx.mm256_round_ps(val, 2);
			}

			// Token: 0x06000B36 RID: 2870 RVA: 0x00009AAA File Offset: 0x00007CAA
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_floor_ps(v256 val)
			{
				return X86.Avx.mm256_round_ps(val, 1);
			}

			// Token: 0x06000B37 RID: 2871 RVA: 0x00009AB3 File Offset: 0x00007CB3
			[DebuggerStepThrough]
			public static v256 mm256_unpackhi_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpackhi_pd(a.Lo128, b.Lo128), X86.Sse2.unpackhi_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B38 RID: 2872 RVA: 0x00009ADC File Offset: 0x00007CDC
			[DebuggerStepThrough]
			public static v256 mm256_unpacklo_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpacklo_pd(a.Lo128, b.Lo128), X86.Sse2.unpacklo_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B39 RID: 2873 RVA: 0x00009B05 File Offset: 0x00007D05
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_unpackhi_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.unpackhi_ps(a.Lo128, b.Lo128), X86.Sse.unpackhi_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B3A RID: 2874 RVA: 0x00009B2E File Offset: 0x00007D2E
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_unpacklo_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.unpacklo_ps(a.Lo128, b.Lo128), X86.Sse.unpacklo_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B3B RID: 2875 RVA: 0x00009B57 File Offset: 0x00007D57
			[DebuggerStepThrough]
			public static int mm256_testz_si256(v256 a, v256 b)
			{
				return X86.Sse4_1.testz_si128(a.Lo128, b.Lo128) & X86.Sse4_1.testz_si128(a.Hi128, b.Hi128);
			}

			// Token: 0x06000B3C RID: 2876 RVA: 0x00009B7C File Offset: 0x00007D7C
			[DebuggerStepThrough]
			public static int mm256_testc_si256(v256 a, v256 b)
			{
				return X86.Sse4_1.testc_si128(a.Lo128, b.Lo128) & X86.Sse4_1.testc_si128(a.Hi128, b.Hi128);
			}

			// Token: 0x06000B3D RID: 2877 RVA: 0x00009BA4 File Offset: 0x00007DA4
			[DebuggerStepThrough]
			public static int mm256_testnzc_si256(v256 a, v256 b)
			{
				int zf = X86.Avx.mm256_testz_si256(a, b);
				int cf = X86.Avx.mm256_testc_si256(a, b);
				return 1 - (zf | cf);
			}

			// Token: 0x06000B3E RID: 2878 RVA: 0x00009BC8 File Offset: 0x00007DC8
			[DebuggerStepThrough]
			public unsafe static int mm256_testz_pd(v256 a, v256 b)
			{
				ulong* aptr = &a.ULong0;
				ulong* bptr = &b.ULong0;
				for (int i = 0; i < 4; i++)
				{
					if ((aptr[i] & bptr[i] & 9223372036854775808UL) != 0UL)
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06000B3F RID: 2879 RVA: 0x00009C14 File Offset: 0x00007E14
			[DebuggerStepThrough]
			public unsafe static int mm256_testc_pd(v256 a, v256 b)
			{
				ulong* aptr = &a.ULong0;
				ulong* bptr = &b.ULong0;
				for (int i = 0; i < 4; i++)
				{
					if ((~aptr[i] & bptr[i] & 9223372036854775808UL) != 0UL)
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06000B40 RID: 2880 RVA: 0x00009C5E File Offset: 0x00007E5E
			[DebuggerStepThrough]
			public static int mm256_testnzc_pd(v256 a, v256 b)
			{
				return 1 - (X86.Avx.mm256_testz_pd(a, b) | X86.Avx.mm256_testc_pd(a, b));
			}

			// Token: 0x06000B41 RID: 2881 RVA: 0x00009C74 File Offset: 0x00007E74
			[DebuggerStepThrough]
			public unsafe static int testz_pd(v128 a, v128 b)
			{
				ulong* aptr = &a.ULong0;
				ulong* bptr = &b.ULong0;
				for (int i = 0; i < 2; i++)
				{
					if ((aptr[i] & bptr[i] & 9223372036854775808UL) != 0UL)
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06000B42 RID: 2882 RVA: 0x00009CC0 File Offset: 0x00007EC0
			[DebuggerStepThrough]
			public unsafe static int testc_pd(v128 a, v128 b)
			{
				ulong* aptr = &a.ULong0;
				ulong* bptr = &b.ULong0;
				for (int i = 0; i < 2; i++)
				{
					if ((~aptr[i] & bptr[i] & 9223372036854775808UL) != 0UL)
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06000B43 RID: 2883 RVA: 0x00009D0A File Offset: 0x00007F0A
			[DebuggerStepThrough]
			public static int testnzc_pd(v128 a, v128 b)
			{
				return 1 - (X86.Avx.testz_pd(a, b) | X86.Avx.testc_pd(a, b));
			}

			// Token: 0x06000B44 RID: 2884 RVA: 0x00009D20 File Offset: 0x00007F20
			[DebuggerStepThrough]
			public unsafe static int mm256_testz_ps(v256 a, v256 b)
			{
				uint* aptr = &a.UInt0;
				uint* bptr = &b.UInt0;
				for (int i = 0; i < 8; i++)
				{
					if ((aptr[i] & bptr[i] & 2147483648U) != 0U)
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06000B45 RID: 2885 RVA: 0x00009D68 File Offset: 0x00007F68
			[DebuggerStepThrough]
			public unsafe static int mm256_testc_ps(v256 a, v256 b)
			{
				uint* aptr = &a.UInt0;
				uint* bptr = &b.UInt0;
				for (int i = 0; i < 8; i++)
				{
					if ((~aptr[i] & bptr[i] & 2147483648U) != 0U)
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06000B46 RID: 2886 RVA: 0x00009DAE File Offset: 0x00007FAE
			[DebuggerStepThrough]
			public static int mm256_testnzc_ps(v256 a, v256 b)
			{
				return 1 - (X86.Avx.mm256_testz_ps(a, b) | X86.Avx.mm256_testc_ps(a, b));
			}

			// Token: 0x06000B47 RID: 2887 RVA: 0x00009DC4 File Offset: 0x00007FC4
			[DebuggerStepThrough]
			public unsafe static int testz_ps(v128 a, v128 b)
			{
				uint* aptr = &a.UInt0;
				uint* bptr = &b.UInt0;
				for (int i = 0; i < 4; i++)
				{
					if ((aptr[i] & bptr[i] & 2147483648U) != 0U)
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06000B48 RID: 2888 RVA: 0x00009E0C File Offset: 0x0000800C
			[DebuggerStepThrough]
			public unsafe static int testc_ps(v128 a, v128 b)
			{
				uint* aptr = &a.UInt0;
				uint* bptr = &b.UInt0;
				for (int i = 0; i < 4; i++)
				{
					if ((~aptr[i] & bptr[i] & 2147483648U) != 0U)
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06000B49 RID: 2889 RVA: 0x00009E52 File Offset: 0x00008052
			[DebuggerStepThrough]
			public static int testnzc_ps(v128 a, v128 b)
			{
				return 1 - (X86.Avx.testz_ps(a, b) | X86.Avx.testc_ps(a, b));
			}

			// Token: 0x06000B4A RID: 2890 RVA: 0x00009E65 File Offset: 0x00008065
			[DebuggerStepThrough]
			public static int mm256_movemask_pd(v256 a)
			{
				return X86.Sse2.movemask_pd(a.Lo128) | (X86.Sse2.movemask_pd(a.Hi128) << 2);
			}

			// Token: 0x06000B4B RID: 2891 RVA: 0x00009E80 File Offset: 0x00008080
			[DebuggerStepThrough]
			public static int mm256_movemask_ps(v256 a)
			{
				return X86.Sse.movemask_ps(a.Lo128) | (X86.Sse.movemask_ps(a.Hi128) << 4);
			}

			// Token: 0x06000B4C RID: 2892 RVA: 0x00009E9C File Offset: 0x0000809C
			[DebuggerStepThrough]
			public static v256 mm256_setzero_pd()
			{
				return default(v256);
			}

			// Token: 0x06000B4D RID: 2893 RVA: 0x00009EB4 File Offset: 0x000080B4
			[DebuggerStepThrough]
			public static v256 mm256_setzero_ps()
			{
				return default(v256);
			}

			// Token: 0x06000B4E RID: 2894 RVA: 0x00009ECC File Offset: 0x000080CC
			[DebuggerStepThrough]
			public static v256 mm256_setzero_si256()
			{
				return default(v256);
			}

			// Token: 0x06000B4F RID: 2895 RVA: 0x00009EE2 File Offset: 0x000080E2
			[DebuggerStepThrough]
			public static v256 mm256_set_pd(double d, double c, double b, double a)
			{
				return new v256(a, b, c, d);
			}

			// Token: 0x06000B50 RID: 2896 RVA: 0x00009EED File Offset: 0x000080ED
			[DebuggerStepThrough]
			public static v256 mm256_set_ps(float e7, float e6, float e5, float e4, float e3, float e2, float e1, float e0)
			{
				return new v256(e0, e1, e2, e3, e4, e5, e6, e7);
			}

			// Token: 0x06000B51 RID: 2897 RVA: 0x00009F00 File Offset: 0x00008100
			[DebuggerStepThrough]
			public static v256 mm256_set_epi8(byte e31_, byte e30_, byte e29_, byte e28_, byte e27_, byte e26_, byte e25_, byte e24_, byte e23_, byte e22_, byte e21_, byte e20_, byte e19_, byte e18_, byte e17_, byte e16_, byte e15_, byte e14_, byte e13_, byte e12_, byte e11_, byte e10_, byte e9_, byte e8_, byte e7_, byte e6_, byte e5_, byte e4_, byte e3_, byte e2_, byte e1_, byte e0_)
			{
				return new v256(e0_, e1_, e2_, e3_, e4_, e5_, e6_, e7_, e8_, e9_, e10_, e11_, e12_, e13_, e14_, e15_, e16_, e17_, e18_, e19_, e20_, e21_, e22_, e23_, e24_, e25_, e26_, e27_, e28_, e29_, e30_, e31_);
			}

			// Token: 0x06000B52 RID: 2898 RVA: 0x00009F50 File Offset: 0x00008150
			[DebuggerStepThrough]
			public static v256 mm256_set_epi16(short e15_, short e14_, short e13_, short e12_, short e11_, short e10_, short e9_, short e8_, short e7_, short e6_, short e5_, short e4_, short e3_, short e2_, short e1_, short e0_)
			{
				return new v256(e0_, e1_, e2_, e3_, e4_, e5_, e6_, e7_, e8_, e9_, e10_, e11_, e12_, e13_, e14_, e15_);
			}

			// Token: 0x06000B53 RID: 2899 RVA: 0x00009F7E File Offset: 0x0000817E
			[DebuggerStepThrough]
			public static v256 mm256_set_epi32(int e7, int e6, int e5, int e4, int e3, int e2, int e1, int e0)
			{
				return new v256(e0, e1, e2, e3, e4, e5, e6, e7);
			}

			// Token: 0x06000B54 RID: 2900 RVA: 0x00009F91 File Offset: 0x00008191
			[DebuggerStepThrough]
			public static v256 mm256_set_epi64x(long e3, long e2, long e1, long e0)
			{
				return new v256(e0, e1, e2, e3);
			}

			// Token: 0x06000B55 RID: 2901 RVA: 0x00009750 File Offset: 0x00007950
			[DebuggerStepThrough]
			public static v256 mm256_set_m128d(v128 hi, v128 lo)
			{
				return new v256(lo, hi);
			}

			// Token: 0x06000B56 RID: 2902 RVA: 0x00009750 File Offset: 0x00007950
			[DebuggerStepThrough]
			public static v256 mm256_set_m128i(v128 hi, v128 lo)
			{
				return new v256(lo, hi);
			}

			// Token: 0x06000B57 RID: 2903 RVA: 0x00009F9C File Offset: 0x0000819C
			[DebuggerStepThrough]
			public static v256 mm256_setr_pd(double d, double c, double b, double a)
			{
				return new v256(d, c, b, a);
			}

			// Token: 0x06000B58 RID: 2904 RVA: 0x00009FA7 File Offset: 0x000081A7
			[DebuggerStepThrough]
			public static v256 mm256_setr_ps(float e7, float e6, float e5, float e4, float e3, float e2, float e1, float e0)
			{
				return new v256(e7, e6, e5, e4, e3, e2, e1, e0);
			}

			// Token: 0x06000B59 RID: 2905 RVA: 0x00009FBC File Offset: 0x000081BC
			[DebuggerStepThrough]
			public static v256 mm256_setr_epi8(byte e31_, byte e30_, byte e29_, byte e28_, byte e27_, byte e26_, byte e25_, byte e24_, byte e23_, byte e22_, byte e21_, byte e20_, byte e19_, byte e18_, byte e17_, byte e16_, byte e15_, byte e14_, byte e13_, byte e12_, byte e11_, byte e10_, byte e9_, byte e8_, byte e7_, byte e6_, byte e5_, byte e4_, byte e3_, byte e2_, byte e1_, byte e0_)
			{
				return new v256(e31_, e30_, e29_, e28_, e27_, e26_, e25_, e24_, e23_, e22_, e21_, e20_, e19_, e18_, e17_, e16_, e15_, e14_, e13_, e12_, e11_, e10_, e9_, e8_, e7_, e6_, e5_, e4_, e3_, e2_, e1_, e0_);
			}

			// Token: 0x06000B5A RID: 2906 RVA: 0x0000A00C File Offset: 0x0000820C
			[DebuggerStepThrough]
			public static v256 mm256_setr_epi16(short e15_, short e14_, short e13_, short e12_, short e11_, short e10_, short e9_, short e8_, short e7_, short e6_, short e5_, short e4_, short e3_, short e2_, short e1_, short e0_)
			{
				return new v256(e15_, e14_, e13_, e12_, e11_, e10_, e9_, e8_, e7_, e6_, e5_, e4_, e3_, e2_, e1_, e0_);
			}

			// Token: 0x06000B5B RID: 2907 RVA: 0x0000A03A File Offset: 0x0000823A
			[DebuggerStepThrough]
			public static v256 mm256_setr_epi32(int e7, int e6, int e5, int e4, int e3, int e2, int e1, int e0)
			{
				return new v256(e7, e6, e5, e4, e3, e2, e1, e0);
			}

			// Token: 0x06000B5C RID: 2908 RVA: 0x0000A04D File Offset: 0x0000824D
			[DebuggerStepThrough]
			public static v256 mm256_setr_epi64x(long e3, long e2, long e1, long e0)
			{
				return new v256(e3, e2, e1, e0);
			}

			// Token: 0x06000B5D RID: 2909 RVA: 0x0000A058 File Offset: 0x00008258
			[DebuggerStepThrough]
			public static v256 mm256_setr_m128(v128 hi, v128 lo)
			{
				return new v256(hi, lo);
			}

			// Token: 0x06000B5E RID: 2910 RVA: 0x0000A058 File Offset: 0x00008258
			[DebuggerStepThrough]
			public static v256 mm256_setr_m128d(v128 hi, v128 lo)
			{
				return new v256(hi, lo);
			}

			// Token: 0x06000B5F RID: 2911 RVA: 0x0000A058 File Offset: 0x00008258
			[DebuggerStepThrough]
			public static v256 mm256_setr_m128i(v128 hi, v128 lo)
			{
				return new v256(hi, lo);
			}

			// Token: 0x06000B60 RID: 2912 RVA: 0x0000A061 File Offset: 0x00008261
			[DebuggerStepThrough]
			public static v256 mm256_set1_pd(double a)
			{
				return new v256(a);
			}

			// Token: 0x06000B61 RID: 2913 RVA: 0x0000A069 File Offset: 0x00008269
			[DebuggerStepThrough]
			public static v256 mm256_set1_ps(float a)
			{
				return new v256(a);
			}

			// Token: 0x06000B62 RID: 2914 RVA: 0x0000A071 File Offset: 0x00008271
			[DebuggerStepThrough]
			public static v256 mm256_set1_epi8(byte a)
			{
				return new v256(a);
			}

			// Token: 0x06000B63 RID: 2915 RVA: 0x0000A079 File Offset: 0x00008279
			[DebuggerStepThrough]
			public static v256 mm256_set1_epi16(short a)
			{
				return new v256(a);
			}

			// Token: 0x06000B64 RID: 2916 RVA: 0x0000A081 File Offset: 0x00008281
			[DebuggerStepThrough]
			public static v256 mm256_set1_epi32(int a)
			{
				return new v256(a);
			}

			// Token: 0x06000B65 RID: 2917 RVA: 0x0000A089 File Offset: 0x00008289
			[DebuggerStepThrough]
			public static v256 mm256_set1_epi64x(long a)
			{
				return new v256(a);
			}

			// Token: 0x06000B66 RID: 2918 RVA: 0x00006072 File Offset: 0x00004272
			[DebuggerStepThrough]
			public static v256 mm256_castpd_ps(v256 a)
			{
				return a;
			}

			// Token: 0x06000B67 RID: 2919 RVA: 0x00006072 File Offset: 0x00004272
			[DebuggerStepThrough]
			public static v256 mm256_castps_pd(v256 a)
			{
				return a;
			}

			// Token: 0x06000B68 RID: 2920 RVA: 0x00006072 File Offset: 0x00004272
			[DebuggerStepThrough]
			public static v256 mm256_castps_si256(v256 a)
			{
				return a;
			}

			// Token: 0x06000B69 RID: 2921 RVA: 0x00006072 File Offset: 0x00004272
			[DebuggerStepThrough]
			public static v256 mm256_castpd_si256(v256 a)
			{
				return a;
			}

			// Token: 0x06000B6A RID: 2922 RVA: 0x00006072 File Offset: 0x00004272
			[DebuggerStepThrough]
			public static v256 mm256_castsi256_ps(v256 a)
			{
				return a;
			}

			// Token: 0x06000B6B RID: 2923 RVA: 0x00006072 File Offset: 0x00004272
			[DebuggerStepThrough]
			public static v256 mm256_castsi256_pd(v256 a)
			{
				return a;
			}

			// Token: 0x06000B6C RID: 2924 RVA: 0x0000A091 File Offset: 0x00008291
			[DebuggerStepThrough]
			public static v128 mm256_castps256_ps128(v256 a)
			{
				return a.Lo128;
			}

			// Token: 0x06000B6D RID: 2925 RVA: 0x0000A091 File Offset: 0x00008291
			[DebuggerStepThrough]
			public static v128 mm256_castpd256_pd128(v256 a)
			{
				return a.Lo128;
			}

			// Token: 0x06000B6E RID: 2926 RVA: 0x0000A091 File Offset: 0x00008291
			[DebuggerStepThrough]
			public static v128 mm256_castsi256_si128(v256 a)
			{
				return a.Lo128;
			}

			// Token: 0x06000B6F RID: 2927 RVA: 0x0000A099 File Offset: 0x00008299
			[DebuggerStepThrough]
			public static v256 mm256_castps128_ps256(v128 a)
			{
				return new v256(a, X86.Sse.setzero_ps());
			}

			// Token: 0x06000B70 RID: 2928 RVA: 0x0000A099 File Offset: 0x00008299
			[DebuggerStepThrough]
			public static v256 mm256_castpd128_pd256(v128 a)
			{
				return new v256(a, X86.Sse.setzero_ps());
			}

			// Token: 0x06000B71 RID: 2929 RVA: 0x0000A099 File Offset: 0x00008299
			[DebuggerStepThrough]
			public static v256 mm256_castsi128_si256(v128 a)
			{
				return new v256(a, X86.Sse.setzero_ps());
			}

			// Token: 0x06000B72 RID: 2930 RVA: 0x0000A0A8 File Offset: 0x000082A8
			[DebuggerStepThrough]
			public static v128 undefined_ps()
			{
				return default(v128);
			}

			// Token: 0x06000B73 RID: 2931 RVA: 0x0000A0BE File Offset: 0x000082BE
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v128 undefined_pd()
			{
				return X86.Avx.undefined_ps();
			}

			// Token: 0x06000B74 RID: 2932 RVA: 0x0000A0BE File Offset: 0x000082BE
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v128 undefined_si128()
			{
				return X86.Avx.undefined_ps();
			}

			// Token: 0x06000B75 RID: 2933 RVA: 0x0000A0C8 File Offset: 0x000082C8
			[DebuggerStepThrough]
			public static v256 mm256_undefined_ps()
			{
				return default(v256);
			}

			// Token: 0x06000B76 RID: 2934 RVA: 0x0000A0DE File Offset: 0x000082DE
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_undefined_pd()
			{
				return X86.Avx.mm256_undefined_ps();
			}

			// Token: 0x06000B77 RID: 2935 RVA: 0x0000A0DE File Offset: 0x000082DE
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_undefined_si256()
			{
				return X86.Avx.mm256_undefined_ps();
			}

			// Token: 0x06000B78 RID: 2936 RVA: 0x0000A099 File Offset: 0x00008299
			[DebuggerStepThrough]
			public static v256 mm256_zextps128_ps256(v128 a)
			{
				return new v256(a, X86.Sse.setzero_ps());
			}

			// Token: 0x06000B79 RID: 2937 RVA: 0x0000A0E5 File Offset: 0x000082E5
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_zextpd128_pd256(v128 a)
			{
				return X86.Avx.mm256_zextps128_ps256(a);
			}

			// Token: 0x06000B7A RID: 2938 RVA: 0x0000A0E5 File Offset: 0x000082E5
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_zextsi128_si256(v128 a)
			{
				return X86.Avx.mm256_zextps128_ps256(a);
			}

			// Token: 0x06000B7B RID: 2939 RVA: 0x0000A0F0 File Offset: 0x000082F0
			[DebuggerStepThrough]
			public unsafe static v256 mm256_insert_epi8(v256 a, int i, int index)
			{
				v256 dst = a;
				(&dst.Byte0)[index & 31] = (byte)i;
				return dst;
			}

			// Token: 0x06000B7C RID: 2940 RVA: 0x0000A110 File Offset: 0x00008310
			[DebuggerStepThrough]
			public unsafe static v256 mm256_insert_epi16(v256 a, int i, int index)
			{
				v256 dst = a;
				(&dst.SShort0)[index & 15] = (short)i;
				return dst;
			}

			// Token: 0x06000B7D RID: 2941 RVA: 0x0000A134 File Offset: 0x00008334
			[DebuggerStepThrough]
			public unsafe static v256 mm256_insert_epi32(v256 a, int i, int index)
			{
				v256 dst = a;
				(&dst.SInt0)[index & 7] = i;
				return dst;
			}

			// Token: 0x06000B7E RID: 2942 RVA: 0x0000A158 File Offset: 0x00008358
			[DebuggerStepThrough]
			public unsafe static v256 mm256_insert_epi64(v256 a, long i, int index)
			{
				v256 dst = a;
				(&dst.SLong0)[index & 3] = i;
				return dst;
			}

			// Token: 0x06000B7F RID: 2943 RVA: 0x0000A179 File Offset: 0x00008379
			[DebuggerStepThrough]
			public unsafe static int mm256_extract_epi32(v256 a, int index)
			{
				return (&a.SInt0)[index & 7];
			}

			// Token: 0x06000B80 RID: 2944 RVA: 0x0000A18B File Offset: 0x0000838B
			[DebuggerStepThrough]
			public unsafe static long mm256_extract_epi64(v256 a, int index)
			{
				return (&a.SLong0)[index & 3];
			}

			// Token: 0x0200003B RID: 59
			public enum CMP
			{
				// Token: 0x04000265 RID: 613
				EQ_OQ,
				// Token: 0x04000266 RID: 614
				LT_OS,
				// Token: 0x04000267 RID: 615
				LE_OS,
				// Token: 0x04000268 RID: 616
				UNORD_Q,
				// Token: 0x04000269 RID: 617
				NEQ_UQ,
				// Token: 0x0400026A RID: 618
				NLT_US,
				// Token: 0x0400026B RID: 619
				NLE_US,
				// Token: 0x0400026C RID: 620
				ORD_Q,
				// Token: 0x0400026D RID: 621
				EQ_UQ,
				// Token: 0x0400026E RID: 622
				NGE_US,
				// Token: 0x0400026F RID: 623
				NGT_US,
				// Token: 0x04000270 RID: 624
				FALSE_OQ,
				// Token: 0x04000271 RID: 625
				NEQ_OQ,
				// Token: 0x04000272 RID: 626
				GE_OS,
				// Token: 0x04000273 RID: 627
				GT_OS,
				// Token: 0x04000274 RID: 628
				TRUE_UQ,
				// Token: 0x04000275 RID: 629
				EQ_OS,
				// Token: 0x04000276 RID: 630
				LT_OQ,
				// Token: 0x04000277 RID: 631
				LE_OQ,
				// Token: 0x04000278 RID: 632
				UNORD_S,
				// Token: 0x04000279 RID: 633
				NEQ_US,
				// Token: 0x0400027A RID: 634
				NLT_UQ,
				// Token: 0x0400027B RID: 635
				NLE_UQ,
				// Token: 0x0400027C RID: 636
				ORD_S,
				// Token: 0x0400027D RID: 637
				EQ_US,
				// Token: 0x0400027E RID: 638
				NGE_UQ,
				// Token: 0x0400027F RID: 639
				NGT_UQ,
				// Token: 0x04000280 RID: 640
				FALSE_OS,
				// Token: 0x04000281 RID: 641
				NEQ_OS,
				// Token: 0x04000282 RID: 642
				GE_OQ,
				// Token: 0x04000283 RID: 643
				GT_OQ,
				// Token: 0x04000284 RID: 644
				TRUE_US
			}
		}

		// Token: 0x0200003C RID: 60
		public static class Avx2
		{
			// Token: 0x17000042 RID: 66
			// (get) Token: 0x06000B81 RID: 2945 RVA: 0x000024DA File Offset: 0x000006DA
			public static bool IsAvx2Supported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000B82 RID: 2946 RVA: 0x0000A1A0 File Offset: 0x000083A0
			[DebuggerStepThrough]
			public unsafe static int mm256_movemask_epi8(v256 a)
			{
				uint result = 0U;
				byte* ptr = &a.Byte0;
				uint bit = 1U;
				int i = 0;
				while (i < 32)
				{
					result |= ((uint)ptr[i] >> 7) * bit;
					i++;
					bit <<= 1;
				}
				return (int)result;
			}

			// Token: 0x06000B83 RID: 2947 RVA: 0x0000A1D7 File Offset: 0x000083D7
			[DebuggerStepThrough]
			public unsafe static int mm256_extract_epi8(v256 a, int index)
			{
				return (int)(&a.Byte0)[index & 31];
			}

			// Token: 0x06000B84 RID: 2948 RVA: 0x0000A1E7 File Offset: 0x000083E7
			[DebuggerStepThrough]
			public unsafe static int mm256_extract_epi16(v256 a, int index)
			{
				return (int)(&a.UShort0)[index & 15];
			}

			// Token: 0x06000B85 RID: 2949 RVA: 0x0000A1FA File Offset: 0x000083FA
			[DebuggerStepThrough]
			public static double mm256_cvtsd_f64(v256 a)
			{
				return a.Double0;
			}

			// Token: 0x06000B86 RID: 2950 RVA: 0x0000A202 File Offset: 0x00008402
			[DebuggerStepThrough]
			public static int mm256_cvtsi256_si32(v256 a)
			{
				return a.SInt0;
			}

			// Token: 0x06000B87 RID: 2951 RVA: 0x0000A20A File Offset: 0x0000840A
			[DebuggerStepThrough]
			public static long mm256_cvtsi256_si64(v256 a)
			{
				return a.SLong0;
			}

			// Token: 0x06000B88 RID: 2952 RVA: 0x0000A212 File Offset: 0x00008412
			[DebuggerStepThrough]
			public static v256 mm256_cmpeq_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.cmpeq_epi8(a.Lo128, b.Lo128), X86.Sse2.cmpeq_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B89 RID: 2953 RVA: 0x0000A23B File Offset: 0x0000843B
			[DebuggerStepThrough]
			public static v256 mm256_cmpeq_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.cmpeq_epi16(a.Lo128, b.Lo128), X86.Sse2.cmpeq_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B8A RID: 2954 RVA: 0x0000A264 File Offset: 0x00008464
			[DebuggerStepThrough]
			public static v256 mm256_cmpeq_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse2.cmpeq_epi32(a.Lo128, b.Lo128), X86.Sse2.cmpeq_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B8B RID: 2955 RVA: 0x0000A28D File Offset: 0x0000848D
			[DebuggerStepThrough]
			public static v256 mm256_cmpeq_epi64(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.cmpeq_epi64(a.Lo128, b.Lo128), X86.Sse4_1.cmpeq_epi64(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B8C RID: 2956 RVA: 0x0000A2B6 File Offset: 0x000084B6
			[DebuggerStepThrough]
			public static v256 mm256_cmpgt_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.cmpgt_epi8(a.Lo128, b.Lo128), X86.Sse2.cmpgt_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B8D RID: 2957 RVA: 0x0000A2DF File Offset: 0x000084DF
			[DebuggerStepThrough]
			public static v256 mm256_cmpgt_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.cmpgt_epi16(a.Lo128, b.Lo128), X86.Sse2.cmpgt_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B8E RID: 2958 RVA: 0x0000A308 File Offset: 0x00008508
			[DebuggerStepThrough]
			public static v256 mm256_cmpgt_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse2.cmpgt_epi32(a.Lo128, b.Lo128), X86.Sse2.cmpgt_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B8F RID: 2959 RVA: 0x0000A331 File Offset: 0x00008531
			[DebuggerStepThrough]
			public static v256 mm256_cmpgt_epi64(v256 a, v256 b)
			{
				return new v256(X86.Sse4_2.cmpgt_epi64(a.Lo128, b.Lo128), X86.Sse4_2.cmpgt_epi64(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B90 RID: 2960 RVA: 0x0000A35A File Offset: 0x0000855A
			[DebuggerStepThrough]
			public static v256 mm256_max_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.max_epi8(a.Lo128, b.Lo128), X86.Sse4_1.max_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B91 RID: 2961 RVA: 0x0000A383 File Offset: 0x00008583
			[DebuggerStepThrough]
			public static v256 mm256_max_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.max_epi16(a.Lo128, b.Lo128), X86.Sse2.max_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B92 RID: 2962 RVA: 0x0000A3AC File Offset: 0x000085AC
			[DebuggerStepThrough]
			public static v256 mm256_max_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.max_epi32(a.Lo128, b.Lo128), X86.Sse4_1.max_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B93 RID: 2963 RVA: 0x0000A3D5 File Offset: 0x000085D5
			[DebuggerStepThrough]
			public static v256 mm256_max_epu8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.max_epu8(a.Lo128, b.Lo128), X86.Sse2.max_epu8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B94 RID: 2964 RVA: 0x0000A3FE File Offset: 0x000085FE
			[DebuggerStepThrough]
			public static v256 mm256_max_epu16(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.max_epu16(a.Lo128, b.Lo128), X86.Sse4_1.max_epu16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B95 RID: 2965 RVA: 0x0000A427 File Offset: 0x00008627
			[DebuggerStepThrough]
			public static v256 mm256_max_epu32(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.max_epu32(a.Lo128, b.Lo128), X86.Sse4_1.max_epu32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B96 RID: 2966 RVA: 0x0000A450 File Offset: 0x00008650
			[DebuggerStepThrough]
			public static v256 mm256_min_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.min_epi8(a.Lo128, b.Lo128), X86.Sse4_1.min_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B97 RID: 2967 RVA: 0x0000A479 File Offset: 0x00008679
			[DebuggerStepThrough]
			public static v256 mm256_min_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.min_epi16(a.Lo128, b.Lo128), X86.Sse2.min_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B98 RID: 2968 RVA: 0x0000A4A2 File Offset: 0x000086A2
			[DebuggerStepThrough]
			public static v256 mm256_min_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.min_epi32(a.Lo128, b.Lo128), X86.Sse4_1.min_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B99 RID: 2969 RVA: 0x0000A4CB File Offset: 0x000086CB
			[DebuggerStepThrough]
			public static v256 mm256_min_epu8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.min_epu8(a.Lo128, b.Lo128), X86.Sse2.min_epu8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B9A RID: 2970 RVA: 0x0000A4F4 File Offset: 0x000086F4
			[DebuggerStepThrough]
			public static v256 mm256_min_epu16(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.min_epu16(a.Lo128, b.Lo128), X86.Sse4_1.min_epu16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B9B RID: 2971 RVA: 0x0000A51D File Offset: 0x0000871D
			[DebuggerStepThrough]
			public static v256 mm256_min_epu32(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.min_epu32(a.Lo128, b.Lo128), X86.Sse4_1.min_epu32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B9C RID: 2972 RVA: 0x0000A546 File Offset: 0x00008746
			[DebuggerStepThrough]
			public static v256 mm256_and_si256(v256 a, v256 b)
			{
				return new v256(X86.Sse2.and_si128(a.Lo128, b.Lo128), X86.Sse2.and_si128(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B9D RID: 2973 RVA: 0x0000A56F File Offset: 0x0000876F
			[DebuggerStepThrough]
			public static v256 mm256_andnot_si256(v256 a, v256 b)
			{
				return new v256(X86.Sse2.andnot_si128(a.Lo128, b.Lo128), X86.Sse2.andnot_si128(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B9E RID: 2974 RVA: 0x0000A598 File Offset: 0x00008798
			[DebuggerStepThrough]
			public static v256 mm256_or_si256(v256 a, v256 b)
			{
				return new v256(X86.Sse2.or_si128(a.Lo128, b.Lo128), X86.Sse2.or_si128(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B9F RID: 2975 RVA: 0x0000A5C1 File Offset: 0x000087C1
			[DebuggerStepThrough]
			public static v256 mm256_xor_si256(v256 a, v256 b)
			{
				return new v256(X86.Sse2.xor_si128(a.Lo128, b.Lo128), X86.Sse2.xor_si128(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA0 RID: 2976 RVA: 0x0000A5EA File Offset: 0x000087EA
			[DebuggerStepThrough]
			public static v256 mm256_abs_epi8(v256 a)
			{
				return new v256(X86.Ssse3.abs_epi8(a.Lo128), X86.Ssse3.abs_epi8(a.Hi128));
			}

			// Token: 0x06000BA1 RID: 2977 RVA: 0x0000A607 File Offset: 0x00008807
			[DebuggerStepThrough]
			public static v256 mm256_abs_epi16(v256 a)
			{
				return new v256(X86.Ssse3.abs_epi16(a.Lo128), X86.Ssse3.abs_epi16(a.Hi128));
			}

			// Token: 0x06000BA2 RID: 2978 RVA: 0x0000A624 File Offset: 0x00008824
			[DebuggerStepThrough]
			public static v256 mm256_abs_epi32(v256 a)
			{
				return new v256(X86.Ssse3.abs_epi32(a.Lo128), X86.Ssse3.abs_epi32(a.Hi128));
			}

			// Token: 0x06000BA3 RID: 2979 RVA: 0x0000A641 File Offset: 0x00008841
			[DebuggerStepThrough]
			public static v256 mm256_add_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.add_epi8(a.Lo128, b.Lo128), X86.Sse2.add_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA4 RID: 2980 RVA: 0x0000A66A File Offset: 0x0000886A
			[DebuggerStepThrough]
			public static v256 mm256_add_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.add_epi16(a.Lo128, b.Lo128), X86.Sse2.add_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA5 RID: 2981 RVA: 0x0000A693 File Offset: 0x00008893
			[DebuggerStepThrough]
			public static v256 mm256_add_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse2.add_epi32(a.Lo128, b.Lo128), X86.Sse2.add_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA6 RID: 2982 RVA: 0x0000A6BC File Offset: 0x000088BC
			[DebuggerStepThrough]
			public static v256 mm256_add_epi64(v256 a, v256 b)
			{
				return new v256(X86.Sse2.add_epi64(a.Lo128, b.Lo128), X86.Sse2.add_epi64(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA7 RID: 2983 RVA: 0x0000A6E5 File Offset: 0x000088E5
			[DebuggerStepThrough]
			public static v256 mm256_adds_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.adds_epi8(a.Lo128, b.Lo128), X86.Sse2.adds_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA8 RID: 2984 RVA: 0x0000A70E File Offset: 0x0000890E
			[DebuggerStepThrough]
			public static v256 mm256_adds_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.adds_epi16(a.Lo128, b.Lo128), X86.Sse2.adds_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA9 RID: 2985 RVA: 0x0000A737 File Offset: 0x00008937
			[DebuggerStepThrough]
			public static v256 mm256_adds_epu8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.adds_epu8(a.Lo128, b.Lo128), X86.Sse2.adds_epu8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BAA RID: 2986 RVA: 0x0000A760 File Offset: 0x00008960
			[DebuggerStepThrough]
			public static v256 mm256_adds_epu16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.adds_epu16(a.Lo128, b.Lo128), X86.Sse2.adds_epu16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BAB RID: 2987 RVA: 0x0000A789 File Offset: 0x00008989
			[DebuggerStepThrough]
			public static v256 mm256_sub_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.sub_epi8(a.Lo128, b.Lo128), X86.Sse2.sub_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BAC RID: 2988 RVA: 0x0000A7B2 File Offset: 0x000089B2
			[DebuggerStepThrough]
			public static v256 mm256_sub_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.sub_epi16(a.Lo128, b.Lo128), X86.Sse2.sub_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BAD RID: 2989 RVA: 0x0000A7DB File Offset: 0x000089DB
			[DebuggerStepThrough]
			public static v256 mm256_sub_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse2.sub_epi32(a.Lo128, b.Lo128), X86.Sse2.sub_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BAE RID: 2990 RVA: 0x0000A804 File Offset: 0x00008A04
			[DebuggerStepThrough]
			public static v256 mm256_sub_epi64(v256 a, v256 b)
			{
				return new v256(X86.Sse2.sub_epi64(a.Lo128, b.Lo128), X86.Sse2.sub_epi64(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BAF RID: 2991 RVA: 0x0000A82D File Offset: 0x00008A2D
			[DebuggerStepThrough]
			public static v256 mm256_subs_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.subs_epi8(a.Lo128, b.Lo128), X86.Sse2.subs_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB0 RID: 2992 RVA: 0x0000A856 File Offset: 0x00008A56
			[DebuggerStepThrough]
			public static v256 mm256_subs_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.subs_epi16(a.Lo128, b.Lo128), X86.Sse2.subs_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB1 RID: 2993 RVA: 0x0000A87F File Offset: 0x00008A7F
			[DebuggerStepThrough]
			public static v256 mm256_subs_epu8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.subs_epu8(a.Lo128, b.Lo128), X86.Sse2.subs_epu8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB2 RID: 2994 RVA: 0x0000A8A8 File Offset: 0x00008AA8
			[DebuggerStepThrough]
			public static v256 mm256_subs_epu16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.subs_epu16(a.Lo128, b.Lo128), X86.Sse2.subs_epu16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB3 RID: 2995 RVA: 0x0000A8D1 File Offset: 0x00008AD1
			[DebuggerStepThrough]
			public static v256 mm256_avg_epu8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.avg_epu8(a.Lo128, b.Lo128), X86.Sse2.avg_epu8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB4 RID: 2996 RVA: 0x0000A8FA File Offset: 0x00008AFA
			[DebuggerStepThrough]
			public static v256 mm256_avg_epu16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.avg_epu16(a.Lo128, b.Lo128), X86.Sse2.avg_epu16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB5 RID: 2997 RVA: 0x0000A923 File Offset: 0x00008B23
			[DebuggerStepThrough]
			public static v256 mm256_hadd_epi16(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.hadd_epi16(a.Lo128, b.Lo128), X86.Ssse3.hadd_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB6 RID: 2998 RVA: 0x0000A94C File Offset: 0x00008B4C
			[DebuggerStepThrough]
			public static v256 mm256_hadd_epi32(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.hadd_epi32(a.Lo128, b.Lo128), X86.Ssse3.hadd_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB7 RID: 2999 RVA: 0x0000A975 File Offset: 0x00008B75
			[DebuggerStepThrough]
			public static v256 mm256_hadds_epi16(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.hadds_epi16(a.Lo128, b.Lo128), X86.Ssse3.hadds_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB8 RID: 3000 RVA: 0x0000A99E File Offset: 0x00008B9E
			[DebuggerStepThrough]
			public static v256 mm256_hsub_epi16(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.hsub_epi16(a.Lo128, b.Lo128), X86.Ssse3.hsub_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB9 RID: 3001 RVA: 0x0000A9C7 File Offset: 0x00008BC7
			[DebuggerStepThrough]
			public static v256 mm256_hsub_epi32(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.hsub_epi32(a.Lo128, b.Lo128), X86.Ssse3.hsub_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BBA RID: 3002 RVA: 0x0000A9F0 File Offset: 0x00008BF0
			[DebuggerStepThrough]
			public static v256 mm256_hsubs_epi16(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.hsubs_epi16(a.Lo128, b.Lo128), X86.Ssse3.hsubs_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BBB RID: 3003 RVA: 0x0000AA19 File Offset: 0x00008C19
			[DebuggerStepThrough]
			public static v256 mm256_madd_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.madd_epi16(a.Lo128, b.Lo128), X86.Sse2.madd_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BBC RID: 3004 RVA: 0x0000AA42 File Offset: 0x00008C42
			[DebuggerStepThrough]
			public static v256 mm256_maddubs_epi16(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.maddubs_epi16(a.Lo128, b.Lo128), X86.Ssse3.maddubs_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BBD RID: 3005 RVA: 0x0000AA6B File Offset: 0x00008C6B
			[DebuggerStepThrough]
			public static v256 mm256_mulhi_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.mulhi_epi16(a.Lo128, b.Lo128), X86.Sse2.mulhi_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BBE RID: 3006 RVA: 0x0000AA94 File Offset: 0x00008C94
			[DebuggerStepThrough]
			public static v256 mm256_mulhi_epu16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.mulhi_epu16(a.Lo128, b.Lo128), X86.Sse2.mulhi_epu16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BBF RID: 3007 RVA: 0x0000AABD File Offset: 0x00008CBD
			[DebuggerStepThrough]
			public static v256 mm256_mullo_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.mullo_epi16(a.Lo128, b.Lo128), X86.Sse2.mullo_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC0 RID: 3008 RVA: 0x0000AAE6 File Offset: 0x00008CE6
			[DebuggerStepThrough]
			public static v256 mm256_mullo_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.mullo_epi32(a.Lo128, b.Lo128), X86.Sse4_1.mullo_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC1 RID: 3009 RVA: 0x0000AB0F File Offset: 0x00008D0F
			[DebuggerStepThrough]
			public static v256 mm256_mul_epu32(v256 a, v256 b)
			{
				return new v256(X86.Sse2.mul_epu32(a.Lo128, b.Lo128), X86.Sse2.mul_epu32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC2 RID: 3010 RVA: 0x0000AB38 File Offset: 0x00008D38
			[DebuggerStepThrough]
			public static v256 mm256_mul_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.mul_epi32(a.Lo128, b.Lo128), X86.Sse4_1.mul_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC3 RID: 3011 RVA: 0x0000AB61 File Offset: 0x00008D61
			[DebuggerStepThrough]
			public static v256 mm256_sign_epi8(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.sign_epi8(a.Lo128, b.Lo128), X86.Ssse3.sign_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC4 RID: 3012 RVA: 0x0000AB8A File Offset: 0x00008D8A
			[DebuggerStepThrough]
			public static v256 mm256_sign_epi16(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.sign_epi16(a.Lo128, b.Lo128), X86.Ssse3.sign_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC5 RID: 3013 RVA: 0x0000ABB3 File Offset: 0x00008DB3
			[DebuggerStepThrough]
			public static v256 mm256_sign_epi32(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.sign_epi32(a.Lo128, b.Lo128), X86.Ssse3.sign_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC6 RID: 3014 RVA: 0x0000ABDC File Offset: 0x00008DDC
			[DebuggerStepThrough]
			public static v256 mm256_mulhrs_epi16(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.mulhrs_epi16(a.Lo128, b.Lo128), X86.Ssse3.mulhrs_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC7 RID: 3015 RVA: 0x0000AC05 File Offset: 0x00008E05
			[DebuggerStepThrough]
			public static v256 mm256_sad_epu8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.sad_epu8(a.Lo128, b.Lo128), X86.Sse2.sad_epu8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC8 RID: 3016 RVA: 0x0000AC2E File Offset: 0x00008E2E
			[DebuggerStepThrough]
			public static v256 mm256_mpsadbw_epu8(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Sse4_1.mpsadbw_epu8(a.Lo128, b.Lo128, imm8 & 7), X86.Sse4_1.mpsadbw_epu8(a.Hi128, b.Hi128, (imm8 >> 3) & 7));
			}

			// Token: 0x06000BC9 RID: 3017 RVA: 0x0000AC5F File Offset: 0x00008E5F
			[DebuggerStepThrough]
			public static v256 mm256_slli_si256(v256 a, int imm8)
			{
				return new v256(X86.Sse2.slli_si128(a.Lo128, imm8), X86.Sse2.slli_si128(a.Hi128, imm8));
			}

			// Token: 0x06000BCA RID: 3018 RVA: 0x0000AC7E File Offset: 0x00008E7E
			[DebuggerStepThrough]
			public static v256 mm256_bslli_epi128(v256 a, int imm8)
			{
				return X86.Avx2.mm256_slli_si256(a, imm8);
			}

			// Token: 0x06000BCB RID: 3019 RVA: 0x0000AC87 File Offset: 0x00008E87
			[DebuggerStepThrough]
			public static v256 mm256_srli_si256(v256 a, int imm8)
			{
				return new v256(X86.Sse2.srli_si128(a.Lo128, imm8), X86.Sse2.srli_si128(a.Hi128, imm8));
			}

			// Token: 0x06000BCC RID: 3020 RVA: 0x0000ACA6 File Offset: 0x00008EA6
			[DebuggerStepThrough]
			public static v256 mm256_bsrli_epi128(v256 a, int imm8)
			{
				return X86.Avx2.mm256_srli_si256(a, imm8);
			}

			// Token: 0x06000BCD RID: 3021 RVA: 0x0000ACAF File Offset: 0x00008EAF
			[DebuggerStepThrough]
			public static v256 mm256_sll_epi16(v256 a, v128 count)
			{
				return new v256(X86.Sse2.sll_epi16(a.Lo128, count), X86.Sse2.sll_epi16(a.Hi128, count));
			}

			// Token: 0x06000BCE RID: 3022 RVA: 0x0000ACCE File Offset: 0x00008ECE
			[DebuggerStepThrough]
			public static v256 mm256_sll_epi32(v256 a, v128 count)
			{
				return new v256(X86.Sse2.sll_epi32(a.Lo128, count), X86.Sse2.sll_epi32(a.Hi128, count));
			}

			// Token: 0x06000BCF RID: 3023 RVA: 0x0000ACED File Offset: 0x00008EED
			[DebuggerStepThrough]
			public static v256 mm256_sll_epi64(v256 a, v128 count)
			{
				return new v256(X86.Sse2.sll_epi64(a.Lo128, count), X86.Sse2.sll_epi64(a.Hi128, count));
			}

			// Token: 0x06000BD0 RID: 3024 RVA: 0x0000AD0C File Offset: 0x00008F0C
			[DebuggerStepThrough]
			public static v256 mm256_slli_epi16(v256 a, int imm8)
			{
				return new v256(X86.Sse2.slli_epi16(a.Lo128, imm8), X86.Sse2.slli_epi16(a.Hi128, imm8));
			}

			// Token: 0x06000BD1 RID: 3025 RVA: 0x0000AD2B File Offset: 0x00008F2B
			[DebuggerStepThrough]
			public static v256 mm256_slli_epi32(v256 a, int imm8)
			{
				return new v256(X86.Sse2.slli_epi32(a.Lo128, imm8), X86.Sse2.slli_epi32(a.Hi128, imm8));
			}

			// Token: 0x06000BD2 RID: 3026 RVA: 0x0000AD4A File Offset: 0x00008F4A
			[DebuggerStepThrough]
			public static v256 mm256_slli_epi64(v256 a, int imm8)
			{
				return new v256(X86.Sse2.slli_epi64(a.Lo128, imm8), X86.Sse2.slli_epi64(a.Hi128, imm8));
			}

			// Token: 0x06000BD3 RID: 3027 RVA: 0x0000AD69 File Offset: 0x00008F69
			[DebuggerStepThrough]
			public static v256 mm256_sllv_epi32(v256 a, v256 count)
			{
				return new v256(X86.Avx2.sllv_epi32(a.Lo128, count.Lo128), X86.Avx2.sllv_epi32(a.Hi128, count.Hi128));
			}

			// Token: 0x06000BD4 RID: 3028 RVA: 0x0000AD92 File Offset: 0x00008F92
			[DebuggerStepThrough]
			public static v256 mm256_sllv_epi64(v256 a, v256 count)
			{
				return new v256(X86.Avx2.sllv_epi64(a.Lo128, count.Lo128), X86.Avx2.sllv_epi64(a.Hi128, count.Hi128));
			}

			// Token: 0x06000BD5 RID: 3029 RVA: 0x0000ADBC File Offset: 0x00008FBC
			[DebuggerStepThrough]
			public unsafe static v128 sllv_epi32(v128 a, v128 count)
			{
				v128 dst = default(v128);
				uint* aptr = &a.UInt0;
				uint* dptr = &dst.UInt0;
				int* sptr = &count.SInt0;
				for (int i = 0; i < 4; i++)
				{
					int shift = sptr[i];
					if (shift >= 0 && shift <= 31)
					{
						dptr[i] = aptr[i] << shift;
					}
					else
					{
						dptr[i] = 0U;
					}
				}
				return dst;
			}

			// Token: 0x06000BD6 RID: 3030 RVA: 0x0000AE34 File Offset: 0x00009034
			[DebuggerStepThrough]
			public unsafe static v128 sllv_epi64(v128 a, v128 count)
			{
				v128 dst = default(v128);
				ulong* aptr = &a.ULong0;
				ulong* dptr = &dst.ULong0;
				long* sptr = &count.SLong0;
				for (int i = 0; i < 2; i++)
				{
					int shift = (int)sptr[i];
					if (shift >= 0 && shift <= 63)
					{
						dptr[i] = aptr[i] << shift;
					}
					else
					{
						dptr[i] = 0UL;
					}
				}
				return dst;
			}

			// Token: 0x06000BD7 RID: 3031 RVA: 0x0000AEAD File Offset: 0x000090AD
			[DebuggerStepThrough]
			public static v256 mm256_sra_epi16(v256 a, v128 count)
			{
				return new v256(X86.Sse2.sra_epi16(a.Lo128, count), X86.Sse2.sra_epi16(a.Hi128, count));
			}

			// Token: 0x06000BD8 RID: 3032 RVA: 0x0000AECC File Offset: 0x000090CC
			[DebuggerStepThrough]
			public static v256 mm256_sra_epi32(v256 a, v128 count)
			{
				return new v256(X86.Sse2.sra_epi32(a.Lo128, count), X86.Sse2.sra_epi32(a.Hi128, count));
			}

			// Token: 0x06000BD9 RID: 3033 RVA: 0x0000AEEB File Offset: 0x000090EB
			[DebuggerStepThrough]
			public static v256 mm256_srai_epi16(v256 a, int imm8)
			{
				return new v256(X86.Sse2.srai_epi16(a.Lo128, imm8), X86.Sse2.srai_epi16(a.Hi128, imm8));
			}

			// Token: 0x06000BDA RID: 3034 RVA: 0x0000AF0A File Offset: 0x0000910A
			[DebuggerStepThrough]
			public static v256 mm256_srai_epi32(v256 a, int imm8)
			{
				return new v256(X86.Sse2.srai_epi32(a.Lo128, imm8), X86.Sse2.srai_epi32(a.Hi128, imm8));
			}

			// Token: 0x06000BDB RID: 3035 RVA: 0x0000AF29 File Offset: 0x00009129
			[DebuggerStepThrough]
			public static v256 mm256_srav_epi32(v256 a, v256 count)
			{
				return new v256(X86.Avx2.srav_epi32(a.Lo128, count.Lo128), X86.Avx2.srav_epi32(a.Hi128, count.Hi128));
			}

			// Token: 0x06000BDC RID: 3036 RVA: 0x0000AF54 File Offset: 0x00009154
			[DebuggerStepThrough]
			public unsafe static v128 srav_epi32(v128 a, v128 count)
			{
				v128 dst = default(v128);
				int* aptr = &a.SInt0;
				int* dptr = &dst.SInt0;
				int* sptr = &count.SInt0;
				for (int i = 0; i < 4; i++)
				{
					int shift = Math.Min(sptr[i] & 255, 32);
					int shift2 = 0;
					if (shift >= 16)
					{
						shift -= 16;
						shift2 += 16;
					}
					dptr[i] = aptr[i] >> shift >> shift2;
				}
				return dst;
			}

			// Token: 0x06000BDD RID: 3037 RVA: 0x0000AFDF File Offset: 0x000091DF
			[DebuggerStepThrough]
			public static v256 mm256_srl_epi16(v256 a, v128 count)
			{
				return new v256(X86.Sse2.srl_epi16(a.Lo128, count), X86.Sse2.srl_epi16(a.Hi128, count));
			}

			// Token: 0x06000BDE RID: 3038 RVA: 0x0000AFFE File Offset: 0x000091FE
			[DebuggerStepThrough]
			public static v256 mm256_srl_epi32(v256 a, v128 count)
			{
				return new v256(X86.Sse2.srl_epi32(a.Lo128, count), X86.Sse2.srl_epi32(a.Hi128, count));
			}

			// Token: 0x06000BDF RID: 3039 RVA: 0x0000B01D File Offset: 0x0000921D
			[DebuggerStepThrough]
			public static v256 mm256_srl_epi64(v256 a, v128 count)
			{
				return new v256(X86.Sse2.srl_epi64(a.Lo128, count), X86.Sse2.srl_epi64(a.Hi128, count));
			}

			// Token: 0x06000BE0 RID: 3040 RVA: 0x0000B03C File Offset: 0x0000923C
			[DebuggerStepThrough]
			public static v256 mm256_srli_epi16(v256 a, int imm8)
			{
				return new v256(X86.Sse2.srli_epi16(a.Lo128, imm8), X86.Sse2.srli_epi16(a.Hi128, imm8));
			}

			// Token: 0x06000BE1 RID: 3041 RVA: 0x0000B05B File Offset: 0x0000925B
			[DebuggerStepThrough]
			public static v256 mm256_srli_epi32(v256 a, int imm8)
			{
				return new v256(X86.Sse2.srli_epi32(a.Lo128, imm8), X86.Sse2.srli_epi32(a.Hi128, imm8));
			}

			// Token: 0x06000BE2 RID: 3042 RVA: 0x0000B07A File Offset: 0x0000927A
			[DebuggerStepThrough]
			public static v256 mm256_srli_epi64(v256 a, int imm8)
			{
				return new v256(X86.Sse2.srli_epi64(a.Lo128, imm8), X86.Sse2.srli_epi64(a.Hi128, imm8));
			}

			// Token: 0x06000BE3 RID: 3043 RVA: 0x0000B099 File Offset: 0x00009299
			[DebuggerStepThrough]
			public static v256 mm256_srlv_epi32(v256 a, v256 count)
			{
				return new v256(X86.Avx2.srlv_epi32(a.Lo128, count.Lo128), X86.Avx2.srlv_epi32(a.Hi128, count.Hi128));
			}

			// Token: 0x06000BE4 RID: 3044 RVA: 0x0000B0C2 File Offset: 0x000092C2
			[DebuggerStepThrough]
			public static v256 mm256_srlv_epi64(v256 a, v256 count)
			{
				return new v256(X86.Avx2.srlv_epi64(a.Lo128, count.Lo128), X86.Avx2.srlv_epi64(a.Hi128, count.Hi128));
			}

			// Token: 0x06000BE5 RID: 3045 RVA: 0x0000B0EC File Offset: 0x000092EC
			[DebuggerStepThrough]
			public unsafe static v128 srlv_epi32(v128 a, v128 count)
			{
				v128 dst = default(v128);
				uint* aptr = &a.UInt0;
				uint* dptr = &dst.UInt0;
				int* sptr = &count.SInt0;
				for (int i = 0; i < 4; i++)
				{
					int shift = sptr[i];
					if (shift >= 0 && shift <= 31)
					{
						dptr[i] = aptr[i] >> shift;
					}
					else
					{
						dptr[i] = 0U;
					}
				}
				return dst;
			}

			// Token: 0x06000BE6 RID: 3046 RVA: 0x0000B164 File Offset: 0x00009364
			[DebuggerStepThrough]
			public unsafe static v128 srlv_epi64(v128 a, v128 count)
			{
				v128 dst = default(v128);
				ulong* aptr = &a.ULong0;
				ulong* dptr = &dst.ULong0;
				long* sptr = &count.SLong0;
				for (int i = 0; i < 2; i++)
				{
					int shift = (int)sptr[i];
					if (shift >= 0 && shift <= 63)
					{
						dptr[i] = aptr[i] >> shift;
					}
					else
					{
						dptr[i] = 0UL;
					}
				}
				return dst;
			}

			// Token: 0x06000BE7 RID: 3047 RVA: 0x0000B1DD File Offset: 0x000093DD
			[DebuggerStepThrough]
			public static v128 blend_epi32(v128 a, v128 b, int imm8)
			{
				return X86.Sse4_1.blend_ps(a, b, imm8);
			}

			// Token: 0x06000BE8 RID: 3048 RVA: 0x0000B1E7 File Offset: 0x000093E7
			[DebuggerStepThrough]
			public static v256 mm256_blend_epi32(v256 a, v256 b, int imm8)
			{
				return X86.Avx.mm256_blend_ps(a, b, imm8);
			}

			// Token: 0x06000BE9 RID: 3049 RVA: 0x0000B1F1 File Offset: 0x000093F1
			[DebuggerStepThrough]
			public static v256 mm256_alignr_epi8(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Ssse3.alignr_epi8(a.Lo128, b.Lo128, imm8), X86.Ssse3.alignr_epi8(a.Hi128, b.Hi128, imm8));
			}

			// Token: 0x06000BEA RID: 3050 RVA: 0x0000B21C File Offset: 0x0000941C
			[DebuggerStepThrough]
			public static v256 mm256_blendv_epi8(v256 a, v256 b, v256 mask)
			{
				return new v256(X86.Sse4_1.blendv_epi8(a.Lo128, b.Lo128, mask.Lo128), X86.Sse4_1.blendv_epi8(a.Hi128, b.Hi128, mask.Hi128));
			}

			// Token: 0x06000BEB RID: 3051 RVA: 0x0000B251 File Offset: 0x00009451
			[DebuggerStepThrough]
			public static v256 mm256_blend_epi16(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Sse4_1.blend_epi16(a.Lo128, b.Lo128, imm8), X86.Sse4_1.blend_epi16(a.Hi128, b.Hi128, imm8));
			}

			// Token: 0x06000BEC RID: 3052 RVA: 0x0000B27C File Offset: 0x0000947C
			[DebuggerStepThrough]
			public static v256 mm256_packs_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.packs_epi16(a.Lo128, b.Lo128), X86.Sse2.packs_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BED RID: 3053 RVA: 0x0000B2A5 File Offset: 0x000094A5
			[DebuggerStepThrough]
			public static v256 mm256_packs_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse2.packs_epi32(a.Lo128, b.Lo128), X86.Sse2.packs_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BEE RID: 3054 RVA: 0x0000B2CE File Offset: 0x000094CE
			[DebuggerStepThrough]
			public static v256 mm256_packus_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.packus_epi16(a.Lo128, b.Lo128), X86.Sse2.packus_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BEF RID: 3055 RVA: 0x0000B2F7 File Offset: 0x000094F7
			[DebuggerStepThrough]
			public static v256 mm256_packus_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.packus_epi32(a.Lo128, b.Lo128), X86.Sse4_1.packus_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BF0 RID: 3056 RVA: 0x0000B320 File Offset: 0x00009520
			[DebuggerStepThrough]
			public static v256 mm256_unpackhi_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpackhi_epi8(a.Lo128, b.Lo128), X86.Sse2.unpackhi_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BF1 RID: 3057 RVA: 0x0000B349 File Offset: 0x00009549
			[DebuggerStepThrough]
			public static v256 mm256_unpackhi_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpackhi_epi16(a.Lo128, b.Lo128), X86.Sse2.unpackhi_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BF2 RID: 3058 RVA: 0x0000B372 File Offset: 0x00009572
			[DebuggerStepThrough]
			public static v256 mm256_unpackhi_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpackhi_epi32(a.Lo128, b.Lo128), X86.Sse2.unpackhi_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BF3 RID: 3059 RVA: 0x0000B39B File Offset: 0x0000959B
			[DebuggerStepThrough]
			public static v256 mm256_unpackhi_epi64(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpackhi_epi64(a.Lo128, b.Lo128), X86.Sse2.unpackhi_epi64(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BF4 RID: 3060 RVA: 0x0000B3C4 File Offset: 0x000095C4
			[DebuggerStepThrough]
			public static v256 mm256_unpacklo_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpacklo_epi8(a.Lo128, b.Lo128), X86.Sse2.unpacklo_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BF5 RID: 3061 RVA: 0x0000B3ED File Offset: 0x000095ED
			[DebuggerStepThrough]
			public static v256 mm256_unpacklo_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpacklo_epi16(a.Lo128, b.Lo128), X86.Sse2.unpacklo_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BF6 RID: 3062 RVA: 0x0000B416 File Offset: 0x00009616
			[DebuggerStepThrough]
			public static v256 mm256_unpacklo_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpacklo_epi32(a.Lo128, b.Lo128), X86.Sse2.unpacklo_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BF7 RID: 3063 RVA: 0x0000B43F File Offset: 0x0000963F
			[DebuggerStepThrough]
			public static v256 mm256_unpacklo_epi64(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpacklo_epi64(a.Lo128, b.Lo128), X86.Sse2.unpacklo_epi64(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BF8 RID: 3064 RVA: 0x0000B468 File Offset: 0x00009668
			[DebuggerStepThrough]
			public static v256 mm256_shuffle_epi8(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.shuffle_epi8(a.Lo128, b.Lo128), X86.Ssse3.shuffle_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BF9 RID: 3065 RVA: 0x0000B491 File Offset: 0x00009691
			[DebuggerStepThrough]
			public static v256 mm256_shuffle_epi32(v256 a, int imm8)
			{
				return new v256(X86.Sse2.shuffle_epi32(a.Lo128, imm8), X86.Sse2.shuffle_epi32(a.Hi128, imm8));
			}

			// Token: 0x06000BFA RID: 3066 RVA: 0x0000B4B0 File Offset: 0x000096B0
			[DebuggerStepThrough]
			public static v256 mm256_shufflehi_epi16(v256 a, int imm8)
			{
				return new v256(X86.Sse2.shufflehi_epi16(a.Lo128, imm8), X86.Sse2.shufflehi_epi16(a.Hi128, imm8));
			}

			// Token: 0x06000BFB RID: 3067 RVA: 0x0000B4CF File Offset: 0x000096CF
			[DebuggerStepThrough]
			public static v256 mm256_shufflelo_epi16(v256 a, int imm8)
			{
				return new v256(X86.Sse2.shufflelo_epi16(a.Lo128, imm8), X86.Sse2.shufflelo_epi16(a.Hi128, imm8));
			}

			// Token: 0x06000BFC RID: 3068 RVA: 0x0000B4EE File Offset: 0x000096EE
			[DebuggerStepThrough]
			public static v128 mm256_extracti128_si256(v256 a, int imm8)
			{
				return X86.Avx.mm256_extractf128_si256(a, imm8);
			}

			// Token: 0x06000BFD RID: 3069 RVA: 0x00009708 File Offset: 0x00007908
			[DebuggerStepThrough]
			public static v256 mm256_inserti128_si256(v256 a, v128 b, int imm8)
			{
				return X86.Avx.mm256_insertf128_ps(a, b, imm8);
			}

			// Token: 0x06000BFE RID: 3070 RVA: 0x0000B4F7 File Offset: 0x000096F7
			[DebuggerStepThrough]
			public static v128 broadcastss_ps(v128 a)
			{
				return new v128(a.Float0);
			}

			// Token: 0x06000BFF RID: 3071 RVA: 0x0000B504 File Offset: 0x00009704
			[DebuggerStepThrough]
			public static v256 mm256_broadcastss_ps(v128 a)
			{
				return new v256(a.Float0);
			}

			// Token: 0x06000C00 RID: 3072 RVA: 0x0000B511 File Offset: 0x00009711
			[DebuggerStepThrough]
			public static v128 broadcastsd_pd(v128 a)
			{
				return new v128(a.Double0);
			}

			// Token: 0x06000C01 RID: 3073 RVA: 0x0000B51E File Offset: 0x0000971E
			[DebuggerStepThrough]
			public static v256 mm256_broadcastsd_pd(v128 a)
			{
				return new v256(a.Double0);
			}

			// Token: 0x06000C02 RID: 3074 RVA: 0x0000B52B File Offset: 0x0000972B
			[DebuggerStepThrough]
			public static v128 broadcastb_epi8(v128 a)
			{
				return new v128(a.Byte0);
			}

			// Token: 0x06000C03 RID: 3075 RVA: 0x0000B538 File Offset: 0x00009738
			[DebuggerStepThrough]
			public static v128 broadcastw_epi16(v128 a)
			{
				return new v128(a.SShort0);
			}

			// Token: 0x06000C04 RID: 3076 RVA: 0x0000B545 File Offset: 0x00009745
			[DebuggerStepThrough]
			public static v128 broadcastd_epi32(v128 a)
			{
				return new v128(a.SInt0);
			}

			// Token: 0x06000C05 RID: 3077 RVA: 0x0000B552 File Offset: 0x00009752
			[DebuggerStepThrough]
			public static v128 broadcastq_epi64(v128 a)
			{
				return new v128(a.SLong0);
			}

			// Token: 0x06000C06 RID: 3078 RVA: 0x0000B55F File Offset: 0x0000975F
			[DebuggerStepThrough]
			public static v256 mm256_broadcastb_epi8(v128 a)
			{
				return new v256(a.Byte0);
			}

			// Token: 0x06000C07 RID: 3079 RVA: 0x0000B56C File Offset: 0x0000976C
			[DebuggerStepThrough]
			public static v256 mm256_broadcastw_epi16(v128 a)
			{
				return new v256(a.SShort0);
			}

			// Token: 0x06000C08 RID: 3080 RVA: 0x0000B579 File Offset: 0x00009779
			[DebuggerStepThrough]
			public static v256 mm256_broadcastd_epi32(v128 a)
			{
				return new v256(a.SInt0);
			}

			// Token: 0x06000C09 RID: 3081 RVA: 0x0000B586 File Offset: 0x00009786
			[DebuggerStepThrough]
			public static v256 mm256_broadcastq_epi64(v128 a)
			{
				return new v256(a.SLong0);
			}

			// Token: 0x06000C0A RID: 3082 RVA: 0x0000B593 File Offset: 0x00009793
			[DebuggerStepThrough]
			public static v256 mm256_broadcastsi128_si256(v128 a)
			{
				return new v256(a, a);
			}

			// Token: 0x06000C0B RID: 3083 RVA: 0x0000B59C File Offset: 0x0000979C
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepi8_epi16(v128 a)
			{
				v256 dst = default(v256);
				short* dptr = &dst.SShort0;
				sbyte* aptr = &a.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = (short)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000C0C RID: 3084 RVA: 0x0000B5DC File Offset: 0x000097DC
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepi8_epi32(v128 a)
			{
				v256 dst = default(v256);
				int* dptr = &dst.SInt0;
				sbyte* aptr = &a.SByte0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = (int)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000C0D RID: 3085 RVA: 0x0000B61C File Offset: 0x0000981C
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepi8_epi64(v128 a)
			{
				v256 dst = default(v256);
				long* dptr = &dst.SLong0;
				sbyte* aptr = &a.SByte0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = (long)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000C0E RID: 3086 RVA: 0x0000B65C File Offset: 0x0000985C
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepi16_epi32(v128 a)
			{
				v256 dst = default(v256);
				int* dptr = &dst.SInt0;
				short* aptr = &a.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = (int)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000C0F RID: 3087 RVA: 0x0000B6A0 File Offset: 0x000098A0
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepi16_epi64(v128 a)
			{
				v256 dst = default(v256);
				long* dptr = &dst.SLong0;
				short* aptr = &a.SShort0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = (long)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000C10 RID: 3088 RVA: 0x0000B6E4 File Offset: 0x000098E4
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepi32_epi64(v128 a)
			{
				v256 dst = default(v256);
				long* dptr = &dst.SLong0;
				int* aptr = &a.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = (long)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000C11 RID: 3089 RVA: 0x0000B728 File Offset: 0x00009928
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepu8_epi16(v128 a)
			{
				v256 dst = default(v256);
				short* dptr = &dst.SShort0;
				byte* aptr = &a.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = (short)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000C12 RID: 3090 RVA: 0x0000B768 File Offset: 0x00009968
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepu8_epi32(v128 a)
			{
				v256 dst = default(v256);
				int* dptr = &dst.SInt0;
				byte* aptr = &a.Byte0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = (int)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000C13 RID: 3091 RVA: 0x0000B7A8 File Offset: 0x000099A8
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepu8_epi64(v128 a)
			{
				v256 dst = default(v256);
				long* dptr = &dst.SLong0;
				byte* aptr = &a.Byte0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = (long)((ulong)aptr[i]);
				}
				return dst;
			}

			// Token: 0x06000C14 RID: 3092 RVA: 0x0000B7E8 File Offset: 0x000099E8
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepu16_epi32(v128 a)
			{
				v256 dst = default(v256);
				int* dptr = &dst.SInt0;
				ushort* aptr = &a.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = (int)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000C15 RID: 3093 RVA: 0x0000B82C File Offset: 0x00009A2C
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepu16_epi64(v128 a)
			{
				v256 dst = default(v256);
				long* dptr = &dst.SLong0;
				ushort* aptr = &a.UShort0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = (long)((ulong)aptr[i]);
				}
				return dst;
			}

			// Token: 0x06000C16 RID: 3094 RVA: 0x0000B870 File Offset: 0x00009A70
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepu32_epi64(v128 a)
			{
				v256 dst = default(v256);
				long* dptr = &dst.SLong0;
				uint* aptr = &a.UInt0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = (long)((ulong)aptr[i]);
				}
				return dst;
			}

			// Token: 0x06000C17 RID: 3095 RVA: 0x0000B8B4 File Offset: 0x00009AB4
			[DebuggerStepThrough]
			public unsafe static v128 maskload_epi32(void* mem_addr, v128 mask)
			{
				v128 dst = default(v128);
				int* mptr = &mask.SInt0;
				int* dptr = &dst.SInt0;
				for (int i = 0; i < 4; i++)
				{
					if (mptr[i] < 0)
					{
						dptr[i] = *(int*)((byte*)mem_addr + (IntPtr)i * 4);
					}
				}
				return dst;
			}

			// Token: 0x06000C18 RID: 3096 RVA: 0x0000B90C File Offset: 0x00009B0C
			[DebuggerStepThrough]
			public unsafe static v128 maskload_epi64(void* mem_addr, v128 mask)
			{
				v128 dst = default(v128);
				long* mptr = &mask.SLong0;
				long* dptr = &dst.SLong0;
				for (int i = 0; i < 2; i++)
				{
					if (mptr[i] < 0L)
					{
						dptr[i] = *(long*)((byte*)mem_addr + (IntPtr)i * 8);
					}
				}
				return dst;
			}

			// Token: 0x06000C19 RID: 3097 RVA: 0x0000B964 File Offset: 0x00009B64
			[DebuggerStepThrough]
			public unsafe static void maskstore_epi32(void* mem_addr, v128 mask, v128 a)
			{
				int* mptr = &mask.SInt0;
				int* sptr = &a.SInt0;
				for (int i = 0; i < 4; i++)
				{
					if (mptr[i] < 0)
					{
						*(int*)((byte*)mem_addr + (IntPtr)i * 4) = sptr[i];
					}
				}
			}

			// Token: 0x06000C1A RID: 3098 RVA: 0x0000B9AC File Offset: 0x00009BAC
			[DebuggerStepThrough]
			public unsafe static void maskstore_epi64(void* mem_addr, v128 mask, v128 a)
			{
				long* mptr = &mask.SLong0;
				long* sptr = &a.SLong0;
				for (int i = 0; i < 2; i++)
				{
					if (mptr[i] < 0L)
					{
						*(long*)((byte*)mem_addr + (IntPtr)i * 8) = sptr[i];
					}
				}
			}

			// Token: 0x06000C1B RID: 3099 RVA: 0x0000B9F4 File Offset: 0x00009BF4
			[DebuggerStepThrough]
			public unsafe static v256 mm256_maskload_epi32(void* mem_addr, v256 mask)
			{
				v256 dst = default(v256);
				int* mptr = &mask.SInt0;
				int* dptr = &dst.SInt0;
				for (int i = 0; i < 8; i++)
				{
					if (mptr[i] < 0)
					{
						dptr[i] = *(int*)((byte*)mem_addr + (IntPtr)i * 4);
					}
				}
				return dst;
			}

			// Token: 0x06000C1C RID: 3100 RVA: 0x0000BA4C File Offset: 0x00009C4C
			[DebuggerStepThrough]
			public unsafe static v256 mm256_maskload_epi64(void* mem_addr, v256 mask)
			{
				v256 dst = default(v256);
				long* mptr = &mask.SLong0;
				long* dptr = &dst.SLong0;
				for (int i = 0; i < 4; i++)
				{
					if (mptr[i] < 0L)
					{
						dptr[i] = *(long*)((byte*)mem_addr + (IntPtr)i * 8);
					}
				}
				return dst;
			}

			// Token: 0x06000C1D RID: 3101 RVA: 0x0000BAA4 File Offset: 0x00009CA4
			[DebuggerStepThrough]
			public unsafe static void mm256_maskstore_epi32(void* mem_addr, v256 mask, v256 a)
			{
				int* mptr = &mask.SInt0;
				int* sptr = &a.SInt0;
				for (int i = 0; i < 8; i++)
				{
					if (mptr[i] < 0)
					{
						*(int*)((byte*)mem_addr + (IntPtr)i * 4) = sptr[i];
					}
				}
			}

			// Token: 0x06000C1E RID: 3102 RVA: 0x0000BAEC File Offset: 0x00009CEC
			[DebuggerStepThrough]
			public unsafe static void mm256_maskstore_epi64(void* mem_addr, v256 mask, v256 a)
			{
				long* mptr = &mask.SLong0;
				long* sptr = &a.SLong0;
				for (int i = 0; i < 4; i++)
				{
					if (mptr[i] < 0L)
					{
						*(long*)((byte*)mem_addr + (IntPtr)i * 8) = sptr[i];
					}
				}
			}

			// Token: 0x06000C1F RID: 3103 RVA: 0x0000BB34 File Offset: 0x00009D34
			[DebuggerStepThrough]
			public unsafe static v256 mm256_permutevar8x32_epi32(v256 a, v256 idx)
			{
				v256 dst = default(v256);
				int* iptr = &idx.SInt0;
				int* aptr = &a.SInt0;
				int* dptr = &dst.SInt0;
				for (int i = 0; i < 8; i++)
				{
					int index = iptr[i] & 7;
					dptr[i] = aptr[index];
				}
				return dst;
			}

			// Token: 0x06000C20 RID: 3104 RVA: 0x0000BB91 File Offset: 0x00009D91
			[DebuggerStepThrough]
			public static v256 mm256_permutevar8x32_ps(v256 a, v256 idx)
			{
				return X86.Avx2.mm256_permutevar8x32_epi32(a, idx);
			}

			// Token: 0x06000C21 RID: 3105 RVA: 0x0000BB9C File Offset: 0x00009D9C
			[DebuggerStepThrough]
			public unsafe static v256 mm256_permute4x64_epi64(v256 a, int imm8)
			{
				v256 dst = default(v256);
				long* aptr = &a.SLong0;
				long* dptr = &dst.SLong0;
				int i = 0;
				while (i < 4)
				{
					dptr[i] = aptr[imm8 & 3];
					i++;
					imm8 >>= 2;
				}
				return dst;
			}

			// Token: 0x06000C22 RID: 3106 RVA: 0x0000BBE5 File Offset: 0x00009DE5
			[DebuggerStepThrough]
			public static v256 mm256_permute4x64_pd(v256 a, int imm8)
			{
				return X86.Avx2.mm256_permute4x64_epi64(a, imm8);
			}

			// Token: 0x06000C23 RID: 3107 RVA: 0x0000BBEE File Offset: 0x00009DEE
			[DebuggerStepThrough]
			public static v256 mm256_permute2x128_si256(v256 a, v256 b, int imm8)
			{
				return X86.Avx.mm256_permute2f128_si256(a, b, imm8);
			}

			// Token: 0x06000C24 RID: 3108 RVA: 0x00009712 File Offset: 0x00007912
			[DebuggerStepThrough]
			public unsafe static v256 mm256_stream_load_si256(void* mem_addr)
			{
				return *(v256*)mem_addr;
			}

			// Token: 0x06000C25 RID: 3109 RVA: 0x0000BBF8 File Offset: 0x00009DF8
			private unsafe static void EmulatedGather<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U>(T* dptr, void* base_addr, long* indexPtr, int scale, int n, U* mask) where T : struct, ValueType where U : struct, ValueType, IComparable<U>
			{
				U maskZero = default(U);
				for (int i = 0; i < n; i++)
				{
					long offset = indexPtr[i] * (long)scale;
					T* mem_addr = (T*)((byte*)base_addr + offset);
					if (mask == null || mask[(IntPtr)i * (IntPtr)sizeof(U) / (IntPtr)sizeof(U)].CompareTo(maskZero) < 0)
					{
						dptr[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = *mem_addr;
					}
				}
			}

			// Token: 0x06000C26 RID: 3110 RVA: 0x0000BC64 File Offset: 0x00009E64
			private unsafe static void EmulatedGather<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U>(T* dptr, void* base_addr, int* indexPtr, int scale, int n, U* mask) where T : struct, ValueType where U : struct, ValueType, IComparable<U>
			{
				U maskZero = default(U);
				for (int i = 0; i < n; i++)
				{
					long offset = (long)indexPtr[i] * (long)scale;
					T* mem_addr = (T*)((byte*)base_addr + offset);
					if (mask == null || mask[(IntPtr)i * (IntPtr)sizeof(U) / (IntPtr)sizeof(U)].CompareTo(maskZero) < 0)
					{
						dptr[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = *mem_addr;
					}
				}
			}

			// Token: 0x06000C27 RID: 3111 RVA: 0x0000BCD0 File Offset: 0x00009ED0
			[DebuggerStepThrough]
			public unsafe static v256 mm256_i32gather_epi32(void* base_addr, v256 vindex, int scale)
			{
				v256 dst = default(v256);
				X86.Avx2.EmulatedGather<int, int>(&dst.SInt0, base_addr, &vindex.SInt0, scale, sizeof(v256) / 4, null);
				return dst;
			}

			// Token: 0x06000C28 RID: 3112 RVA: 0x0000BD08 File Offset: 0x00009F08
			[DebuggerStepThrough]
			public unsafe static v256 mm256_i32gather_pd(void* base_addr, v128 vindex, int scale)
			{
				v256 dst = default(v256);
				X86.Avx2.EmulatedGather<double, long>(&dst.Double0, base_addr, &vindex.SInt0, scale, 4, null);
				return dst;
			}

			// Token: 0x06000C29 RID: 3113 RVA: 0x0000BD38 File Offset: 0x00009F38
			[DebuggerStepThrough]
			public unsafe static v256 mm256_i32gather_ps(void* base_addr, v256 vindex, int scale)
			{
				v256 dst = default(v256);
				X86.Avx2.EmulatedGather<float, int>(&dst.Float0, base_addr, &vindex.SInt0, scale, 8, null);
				return dst;
			}

			// Token: 0x06000C2A RID: 3114 RVA: 0x0000BD68 File Offset: 0x00009F68
			[DebuggerStepThrough]
			public unsafe static v256 mm256_i64gather_pd(void* base_addr, v256 vindex, int scale)
			{
				v256 dst = default(v256);
				X86.Avx2.EmulatedGather<double, long>(&dst.Double0, base_addr, &vindex.SLong0, scale, 4, null);
				return dst;
			}

			// Token: 0x06000C2B RID: 3115 RVA: 0x0000BD98 File Offset: 0x00009F98
			[DebuggerStepThrough]
			public unsafe static v128 mm256_i64gather_ps(void* base_addr, v256 vindex, int scale)
			{
				v128 dst = default(v128);
				X86.Avx2.EmulatedGather<float, int>(&dst.Float0, base_addr, &vindex.SLong0, scale, 4, null);
				return dst;
			}

			// Token: 0x06000C2C RID: 3116 RVA: 0x0000BDC8 File Offset: 0x00009FC8
			[DebuggerStepThrough]
			public unsafe static v128 i32gather_pd(void* base_addr, v128 vindex, int scale)
			{
				v128 dst = default(v128);
				X86.Avx2.EmulatedGather<double, long>(&dst.Double0, base_addr, &vindex.SInt0, scale, 2, null);
				return dst;
			}

			// Token: 0x06000C2D RID: 3117 RVA: 0x0000BDF8 File Offset: 0x00009FF8
			[DebuggerStepThrough]
			public unsafe static v128 i32gather_ps(void* base_addr, v128 vindex, int scale)
			{
				v128 dst = default(v128);
				X86.Avx2.EmulatedGather<float, int>(&dst.Float0, base_addr, &vindex.SInt0, scale, 4, null);
				return dst;
			}

			// Token: 0x06000C2E RID: 3118 RVA: 0x0000BE28 File Offset: 0x0000A028
			[DebuggerStepThrough]
			public unsafe static v128 i64gather_pd(void* base_addr, v128 vindex, int scale)
			{
				v128 dst = default(v128);
				X86.Avx2.EmulatedGather<double, long>(&dst.Double0, base_addr, &vindex.SLong0, scale, 2, null);
				return dst;
			}

			// Token: 0x06000C2F RID: 3119 RVA: 0x0000BE58 File Offset: 0x0000A058
			[DebuggerStepThrough]
			public unsafe static v128 i64gather_ps(void* base_addr, v128 vindex, int scale)
			{
				v128 dst = default(v128);
				X86.Avx2.EmulatedGather<float, int>(&dst.Float0, base_addr, &vindex.SLong0, scale, 2, null);
				return dst;
			}

			// Token: 0x06000C30 RID: 3120 RVA: 0x0000BE88 File Offset: 0x0000A088
			[DebuggerStepThrough]
			public unsafe static v256 mm256_i32gather_epi64(void* base_addr, v128 vindex, int scale)
			{
				v256 dst = default(v256);
				X86.Avx2.EmulatedGather<long, long>(&dst.SLong0, base_addr, &vindex.SInt0, scale, 4, null);
				return dst;
			}

			// Token: 0x06000C31 RID: 3121 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
			[DebuggerStepThrough]
			public unsafe static v128 mm256_i64gather_epi32(void* base_addr, v256 vindex, int scale)
			{
				v128 dst = default(v128);
				X86.Avx2.EmulatedGather<int, int>(&dst.SInt0, base_addr, &vindex.SLong0, scale, 4, null);
				return dst;
			}

			// Token: 0x06000C32 RID: 3122 RVA: 0x0000BEE8 File Offset: 0x0000A0E8
			[DebuggerStepThrough]
			public unsafe static v256 mm256_i64gather_epi64(void* base_addr, v256 vindex, int scale)
			{
				v256 dst = default(v256);
				X86.Avx2.EmulatedGather<long, long>(&dst.SLong0, base_addr, &vindex.SLong0, scale, 4, null);
				return dst;
			}

			// Token: 0x06000C33 RID: 3123 RVA: 0x0000BF18 File Offset: 0x0000A118
			[DebuggerStepThrough]
			public unsafe static v128 i32gather_epi32(void* base_addr, v128 vindex, int scale)
			{
				v128 dst = default(v128);
				X86.Avx2.EmulatedGather<int, int>(&dst.SInt0, base_addr, &vindex.SInt0, scale, 4, null);
				return dst;
			}

			// Token: 0x06000C34 RID: 3124 RVA: 0x0000BF48 File Offset: 0x0000A148
			[DebuggerStepThrough]
			public unsafe static v128 i32gather_epi64(void* base_addr, v128 vindex, int scale)
			{
				v128 dst = default(v128);
				X86.Avx2.EmulatedGather<long, long>(&dst.SLong0, base_addr, &vindex.SInt0, scale, 2, null);
				return dst;
			}

			// Token: 0x06000C35 RID: 3125 RVA: 0x0000BF78 File Offset: 0x0000A178
			[DebuggerStepThrough]
			public unsafe static v128 i64gather_epi32(void* base_addr, v128 vindex, int scale)
			{
				v128 dst = default(v128);
				X86.Avx2.EmulatedGather<int, int>(&dst.SInt0, base_addr, &vindex.SLong0, scale, 2, null);
				return dst;
			}

			// Token: 0x06000C36 RID: 3126 RVA: 0x0000BFA8 File Offset: 0x0000A1A8
			[DebuggerStepThrough]
			public unsafe static v128 i64gather_epi64(void* base_addr, v128 vindex, int scale)
			{
				v128 dst = default(v128);
				X86.Avx2.EmulatedGather<long, long>(&dst.SLong0, base_addr, &vindex.SLong0, scale, 2, null);
				return dst;
			}

			// Token: 0x06000C37 RID: 3127 RVA: 0x0000BFD8 File Offset: 0x0000A1D8
			[DebuggerStepThrough]
			public unsafe static v256 mm256_mask_i32gather_pd(v256 src, void* base_addr, v128 vindex, v256 mask, int scale)
			{
				v256 dst = src;
				X86.Avx2.EmulatedGather<double, long>(&dst.Double0, base_addr, &vindex.SInt0, scale, 4, &mask.SLong0);
				return dst;
			}

			// Token: 0x06000C38 RID: 3128 RVA: 0x0000C00C File Offset: 0x0000A20C
			[DebuggerStepThrough]
			public unsafe static v256 mm256_mask_i32gather_ps(v256 src, void* base_addr, v256 vindex, v256 mask, int scale)
			{
				v256 dst = src;
				X86.Avx2.EmulatedGather<float, int>(&dst.Float0, base_addr, &vindex.SInt0, scale, 8, &mask.SInt0);
				return dst;
			}

			// Token: 0x06000C39 RID: 3129 RVA: 0x0000C040 File Offset: 0x0000A240
			[DebuggerStepThrough]
			public unsafe static v256 mm256_mask_i64gather_pd(v256 src, void* base_addr, v256 vindex, v256 mask, int scale)
			{
				v256 dst = src;
				X86.Avx2.EmulatedGather<double, long>(&dst.Double0, base_addr, &vindex.SLong0, scale, 4, &mask.SLong0);
				return dst;
			}

			// Token: 0x06000C3A RID: 3130 RVA: 0x0000C074 File Offset: 0x0000A274
			[DebuggerStepThrough]
			public unsafe static v128 mm256_mask_i64gather_ps(v128 src, void* base_addr, v256 vindex, v128 mask, int scale)
			{
				v128 dst = src;
				X86.Avx2.EmulatedGather<float, int>(&dst.Float0, base_addr, &vindex.SLong0, scale, 4, &mask.SInt0);
				return dst;
			}

			// Token: 0x06000C3B RID: 3131 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
			[DebuggerStepThrough]
			public unsafe static v256 mm256_mask_i32gather_epi32(v256 src, void* base_addr, v256 vindex, v256 mask, int scale)
			{
				v256 dst = src;
				X86.Avx2.EmulatedGather<int, int>(&dst.SInt0, base_addr, &vindex.SInt0, scale, 8, &mask.SInt0);
				return dst;
			}

			// Token: 0x06000C3C RID: 3132 RVA: 0x0000C0DC File Offset: 0x0000A2DC
			[DebuggerStepThrough]
			public unsafe static v256 mm256_mask_i32gather_epi64(v256 src, void* base_addr, v128 vindex, v256 mask, int scale)
			{
				v256 dst = src;
				X86.Avx2.EmulatedGather<long, long>(&dst.SLong0, base_addr, &vindex.SInt0, scale, 4, &mask.SLong0);
				return dst;
			}

			// Token: 0x06000C3D RID: 3133 RVA: 0x0000C110 File Offset: 0x0000A310
			[DebuggerStepThrough]
			public unsafe static v256 mm256_mask_i64gather_epi64(v256 src, void* base_addr, v256 vindex, v256 mask, int scale)
			{
				v256 dst = src;
				X86.Avx2.EmulatedGather<long, long>(&dst.SLong0, base_addr, &vindex.SLong0, scale, 4, &mask.SLong0);
				return dst;
			}

			// Token: 0x06000C3E RID: 3134 RVA: 0x0000C144 File Offset: 0x0000A344
			[DebuggerStepThrough]
			public unsafe static v128 mm256_mask_i64gather_epi32(v128 src, void* base_addr, v256 vindex, v128 mask, int scale)
			{
				v128 dst = src;
				X86.Avx2.EmulatedGather<int, int>(&dst.SInt0, base_addr, &vindex.SLong0, scale, 4, &mask.SInt0);
				return dst;
			}

			// Token: 0x06000C3F RID: 3135 RVA: 0x0000C178 File Offset: 0x0000A378
			[DebuggerStepThrough]
			public unsafe static v128 mask_i32gather_pd(v128 src, void* base_addr, v128 vindex, v128 mask, int scale)
			{
				v128 dst = src;
				X86.Avx2.EmulatedGather<double, long>(&dst.Double0, base_addr, &vindex.SInt0, scale, 2, &mask.SLong0);
				return dst;
			}

			// Token: 0x06000C40 RID: 3136 RVA: 0x0000C1AC File Offset: 0x0000A3AC
			[DebuggerStepThrough]
			public unsafe static v128 mask_i32gather_ps(v128 src, void* base_addr, v128 vindex, v128 mask, int scale)
			{
				v128 dst = src;
				X86.Avx2.EmulatedGather<float, int>(&dst.Float0, base_addr, &vindex.SInt0, scale, 4, &mask.SInt0);
				return dst;
			}

			// Token: 0x06000C41 RID: 3137 RVA: 0x0000C1E0 File Offset: 0x0000A3E0
			[DebuggerStepThrough]
			public unsafe static v128 mask_i64gather_pd(v128 src, void* base_addr, v128 vindex, v128 mask, int scale)
			{
				v128 dst = src;
				X86.Avx2.EmulatedGather<double, long>(&dst.Double0, base_addr, &vindex.SLong0, scale, 2, &mask.SLong0);
				return dst;
			}

			// Token: 0x06000C42 RID: 3138 RVA: 0x0000C214 File Offset: 0x0000A414
			[DebuggerStepThrough]
			public unsafe static v128 mask_i64gather_ps(v128 src, void* base_addr, v128 vindex, v128 mask, int scale)
			{
				v128 dst = src;
				dst.UInt2 = (dst.UInt3 = 0U);
				X86.Avx2.EmulatedGather<float, int>(&dst.Float0, base_addr, &vindex.SLong0, scale, 2, &mask.SInt0);
				return dst;
			}

			// Token: 0x06000C43 RID: 3139 RVA: 0x0000C258 File Offset: 0x0000A458
			[DebuggerStepThrough]
			public unsafe static v128 mask_i32gather_epi32(v128 src, void* base_addr, v128 vindex, v128 mask, int scale)
			{
				v128 dst = src;
				X86.Avx2.EmulatedGather<int, int>(&dst.SInt0, base_addr, &vindex.SInt0, scale, 4, &mask.SInt0);
				return dst;
			}

			// Token: 0x06000C44 RID: 3140 RVA: 0x0000C28C File Offset: 0x0000A48C
			[DebuggerStepThrough]
			public unsafe static v128 mask_i32gather_epi64(v128 src, void* base_addr, v128 vindex, v128 mask, int scale)
			{
				v128 dst = src;
				X86.Avx2.EmulatedGather<long, long>(&dst.SLong0, base_addr, &vindex.SInt0, scale, 2, &mask.SLong0);
				return dst;
			}

			// Token: 0x06000C45 RID: 3141 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
			[DebuggerStepThrough]
			public unsafe static v128 mask_i64gather_epi32(v128 src, void* base_addr, v128 vindex, v128 mask, int scale)
			{
				v128 dst = src;
				dst.UInt2 = (dst.UInt3 = 0U);
				X86.Avx2.EmulatedGather<int, int>(&dst.SInt0, base_addr, &vindex.SLong0, scale, 2, &mask.SInt0);
				return dst;
			}

			// Token: 0x06000C46 RID: 3142 RVA: 0x0000C304 File Offset: 0x0000A504
			[DebuggerStepThrough]
			public unsafe static v128 mask_i64gather_epi64(v128 src, void* base_addr, v128 vindex, v128 mask, int scale)
			{
				v128 dst = src;
				X86.Avx2.EmulatedGather<long, long>(&dst.SLong0, base_addr, &vindex.SLong0, scale, 2, &mask.SLong0);
				return dst;
			}
		}

		// Token: 0x0200003D RID: 61
		public static class Bmi1
		{
			// Token: 0x17000043 RID: 67
			// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0000C335 File Offset: 0x0000A535
			public static bool IsBmi1Supported
			{
				get
				{
					return X86.Avx2.IsAvx2Supported;
				}
			}

			// Token: 0x06000C48 RID: 3144 RVA: 0x0000C33C File Offset: 0x0000A53C
			[DebuggerStepThrough]
			public static uint andn_u32(uint a, uint b)
			{
				return ~a & b;
			}

			// Token: 0x06000C49 RID: 3145 RVA: 0x0000C33C File Offset: 0x0000A53C
			[DebuggerStepThrough]
			public static ulong andn_u64(ulong a, ulong b)
			{
				return ~a & b;
			}

			// Token: 0x06000C4A RID: 3146 RVA: 0x0000C344 File Offset: 0x0000A544
			[DebuggerStepThrough]
			public static uint bextr_u32(uint a, uint start, uint len)
			{
				start &= 255U;
				if (start >= 32U)
				{
					return 0U;
				}
				uint aShifted = a >> (int)start;
				len &= 255U;
				if (len >= 32U)
				{
					return aShifted;
				}
				return aShifted & ((1U << (int)len) - 1U);
			}

			// Token: 0x06000C4B RID: 3147 RVA: 0x0000C384 File Offset: 0x0000A584
			[DebuggerStepThrough]
			public static ulong bextr_u64(ulong a, uint start, uint len)
			{
				start &= 255U;
				if (start >= 64U)
				{
					return 0UL;
				}
				ulong aShifted = a >> (int)start;
				len &= 255U;
				if (len >= 64U)
				{
					return aShifted;
				}
				return aShifted & ((1UL << (int)len) - 1UL);
			}

			// Token: 0x06000C4C RID: 3148 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
			[DebuggerStepThrough]
			public static uint bextr2_u32(uint a, uint control)
			{
				uint start = control & 255U;
				uint len = (control >> 8) & 255U;
				return X86.Bmi1.bextr_u32(a, start, len);
			}

			// Token: 0x06000C4D RID: 3149 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
			[DebuggerStepThrough]
			public static ulong bextr2_u64(ulong a, ulong control)
			{
				uint start = (uint)(control & 255UL);
				uint len = (uint)((control >> 8) & 255UL);
				return X86.Bmi1.bextr_u64(a, start, len);
			}

			// Token: 0x06000C4E RID: 3150 RVA: 0x0000C41B File Offset: 0x0000A61B
			[DebuggerStepThrough]
			public static uint blsi_u32(uint a)
			{
				return -a & a;
			}

			// Token: 0x06000C4F RID: 3151 RVA: 0x0000C41B File Offset: 0x0000A61B
			[DebuggerStepThrough]
			public static ulong blsi_u64(ulong a)
			{
				return -a & a;
			}

			// Token: 0x06000C50 RID: 3152 RVA: 0x0000C421 File Offset: 0x0000A621
			[DebuggerStepThrough]
			public static uint blsmsk_u32(uint a)
			{
				return (a - 1U) ^ a;
			}

			// Token: 0x06000C51 RID: 3153 RVA: 0x0000C428 File Offset: 0x0000A628
			[DebuggerStepThrough]
			public static ulong blsmsk_u64(ulong a)
			{
				return (a - 1UL) ^ a;
			}

			// Token: 0x06000C52 RID: 3154 RVA: 0x0000C430 File Offset: 0x0000A630
			[DebuggerStepThrough]
			public static uint blsr_u32(uint a)
			{
				return (a - 1U) & a;
			}

			// Token: 0x06000C53 RID: 3155 RVA: 0x0000C437 File Offset: 0x0000A637
			[DebuggerStepThrough]
			public static ulong blsr_u64(ulong a)
			{
				return (a - 1UL) & a;
			}

			// Token: 0x06000C54 RID: 3156 RVA: 0x0000C440 File Offset: 0x0000A640
			[DebuggerStepThrough]
			public static uint tzcnt_u32(uint a)
			{
				uint c = 32U;
				a &= -a;
				if (a != 0U)
				{
					c -= 1U;
				}
				if ((a & 65535U) != 0U)
				{
					c -= 16U;
				}
				if ((a & 16711935U) != 0U)
				{
					c -= 8U;
				}
				if ((a & 252645135U) != 0U)
				{
					c -= 4U;
				}
				if ((a & 858993459U) != 0U)
				{
					c -= 2U;
				}
				if ((a & 1431655765U) != 0U)
				{
					c -= 1U;
				}
				return c;
			}

			// Token: 0x06000C55 RID: 3157 RVA: 0x0000C4A0 File Offset: 0x0000A6A0
			[DebuggerStepThrough]
			public static ulong tzcnt_u64(ulong a)
			{
				ulong c = 64UL;
				a &= -a;
				if (a != 0UL)
				{
					c -= 1UL;
				}
				if ((a & (ulong)(-1)) != 0UL)
				{
					c -= 32UL;
				}
				if ((a & 281470681808895UL) != 0UL)
				{
					c -= 16UL;
				}
				if ((a & 71777214294589695UL) != 0UL)
				{
					c -= 8UL;
				}
				if ((a & 1085102592571150095UL) != 0UL)
				{
					c -= 4UL;
				}
				if ((a & 3689348814741910323UL) != 0UL)
				{
					c -= 2UL;
				}
				if ((a & 6148914691236517205UL) != 0UL)
				{
					c -= 1UL;
				}
				return c;
			}
		}

		// Token: 0x0200003E RID: 62
		public static class Bmi2
		{
			// Token: 0x17000044 RID: 68
			// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0000C335 File Offset: 0x0000A535
			public static bool IsBmi2Supported
			{
				get
				{
					return X86.Avx2.IsAvx2Supported;
				}
			}

			// Token: 0x06000C57 RID: 3159 RVA: 0x0000C527 File Offset: 0x0000A727
			[DebuggerStepThrough]
			public static uint bzhi_u32(uint a, uint index)
			{
				index &= 255U;
				if (index >= 32U)
				{
					return a;
				}
				return a & ((1U << (int)index) - 1U);
			}

			// Token: 0x06000C58 RID: 3160 RVA: 0x0000C543 File Offset: 0x0000A743
			[DebuggerStepThrough]
			public static ulong bzhi_u64(ulong a, ulong index)
			{
				index &= 255UL;
				if (index >= 64UL)
				{
					return a;
				}
				return a & ((1UL << (int)index) - 1UL);
			}

			// Token: 0x06000C59 RID: 3161 RVA: 0x0000C564 File Offset: 0x0000A764
			[DebuggerStepThrough]
			public static uint mulx_u32(uint a, uint b, out uint hi)
			{
				ulong num = (ulong)a;
				ulong bBig = (ulong)b;
				ulong result = num * bBig;
				hi = (uint)(result >> 32);
				return (uint)(result & (ulong)(-1));
			}

			// Token: 0x06000C5A RID: 3162 RVA: 0x0000C585 File Offset: 0x0000A785
			[DebuggerStepThrough]
			public static ulong mulx_u64(ulong a, ulong b, out ulong hi)
			{
				return Common.umul128(a, b, out hi);
			}

			// Token: 0x06000C5B RID: 3163 RVA: 0x0000C590 File Offset: 0x0000A790
			[DebuggerStepThrough]
			public static uint pdep_u32(uint a, uint mask)
			{
				uint result = 0U;
				int i = 0;
				for (int j = 0; j < 32; j++)
				{
					if ((mask & (1U << j)) != 0U)
					{
						result |= ((a >> i) & 1U) << j;
						i++;
					}
				}
				return result;
			}

			// Token: 0x06000C5C RID: 3164 RVA: 0x0000C5D0 File Offset: 0x0000A7D0
			[DebuggerStepThrough]
			public static ulong pdep_u64(ulong a, ulong mask)
			{
				ulong result = 0UL;
				int i = 0;
				for (int j = 0; j < 64; j++)
				{
					if ((mask & (1UL << j)) != 0UL)
					{
						result |= ((a >> i) & 1UL) << j;
						i++;
					}
				}
				return result;
			}

			// Token: 0x06000C5D RID: 3165 RVA: 0x0000C610 File Offset: 0x0000A810
			[DebuggerStepThrough]
			public static uint pext_u32(uint a, uint mask)
			{
				uint result = 0U;
				int i = 0;
				for (int j = 0; j < 32; j++)
				{
					if ((mask & (1U << j)) != 0U)
					{
						result |= ((a >> j) & 1U) << i;
						i++;
					}
				}
				return result;
			}

			// Token: 0x06000C5E RID: 3166 RVA: 0x0000C650 File Offset: 0x0000A850
			[DebuggerStepThrough]
			public static ulong pext_u64(ulong a, ulong mask)
			{
				ulong result = 0UL;
				int i = 0;
				for (int j = 0; j < 64; j++)
				{
					if ((mask & (1UL << j)) != 0UL)
					{
						result |= ((a >> j) & 1UL) << i;
						i++;
					}
				}
				return result;
			}
		}

		// Token: 0x0200003F RID: 63
		[Flags]
		public enum MXCSRBits
		{
			// Token: 0x04000286 RID: 646
			FlushToZero = 32768,
			// Token: 0x04000287 RID: 647
			RoundingControlMask = 24576,
			// Token: 0x04000288 RID: 648
			RoundToNearest = 0,
			// Token: 0x04000289 RID: 649
			RoundDown = 8192,
			// Token: 0x0400028A RID: 650
			RoundUp = 16384,
			// Token: 0x0400028B RID: 651
			RoundTowardZero = 24576,
			// Token: 0x0400028C RID: 652
			PrecisionMask = 4096,
			// Token: 0x0400028D RID: 653
			UnderflowMask = 2048,
			// Token: 0x0400028E RID: 654
			OverflowMask = 1024,
			// Token: 0x0400028F RID: 655
			DivideByZeroMask = 512,
			// Token: 0x04000290 RID: 656
			DenormalOperationMask = 256,
			// Token: 0x04000291 RID: 657
			InvalidOperationMask = 128,
			// Token: 0x04000292 RID: 658
			ExceptionMask = 8064,
			// Token: 0x04000293 RID: 659
			DenormalsAreZeroes = 64,
			// Token: 0x04000294 RID: 660
			PrecisionFlag = 32,
			// Token: 0x04000295 RID: 661
			UnderflowFlag = 16,
			// Token: 0x04000296 RID: 662
			OverflowFlag = 8,
			// Token: 0x04000297 RID: 663
			DivideByZeroFlag = 4,
			// Token: 0x04000298 RID: 664
			DenormalFlag = 2,
			// Token: 0x04000299 RID: 665
			InvalidOperationFlag = 1,
			// Token: 0x0400029A RID: 666
			FlagMask = 63
		}

		// Token: 0x02000040 RID: 64
		[Flags]
		public enum RoundingMode
		{
			// Token: 0x0400029C RID: 668
			FROUND_TO_NEAREST_INT = 0,
			// Token: 0x0400029D RID: 669
			FROUND_TO_NEG_INF = 1,
			// Token: 0x0400029E RID: 670
			FROUND_TO_POS_INF = 2,
			// Token: 0x0400029F RID: 671
			FROUND_TO_ZERO = 3,
			// Token: 0x040002A0 RID: 672
			FROUND_CUR_DIRECTION = 4,
			// Token: 0x040002A1 RID: 673
			FROUND_RAISE_EXC = 0,
			// Token: 0x040002A2 RID: 674
			FROUND_NO_EXC = 8,
			// Token: 0x040002A3 RID: 675
			FROUND_NINT = 0,
			// Token: 0x040002A4 RID: 676
			FROUND_FLOOR = 1,
			// Token: 0x040002A5 RID: 677
			FROUND_CEIL = 2,
			// Token: 0x040002A6 RID: 678
			FROUND_TRUNC = 3,
			// Token: 0x040002A7 RID: 679
			FROUND_RINT = 4,
			// Token: 0x040002A8 RID: 680
			FROUND_NEARBYINT = 12,
			// Token: 0x040002A9 RID: 681
			FROUND_NINT_NOEXC = 8,
			// Token: 0x040002AA RID: 682
			FROUND_FLOOR_NOEXC = 9,
			// Token: 0x040002AB RID: 683
			FROUND_CEIL_NOEXC = 10,
			// Token: 0x040002AC RID: 684
			FROUND_TRUNC_NOEXC = 11,
			// Token: 0x040002AD RID: 685
			FROUND_RINT_NOEXC = 12
		}

		// Token: 0x02000041 RID: 65
		internal struct RoundingScope : IDisposable
		{
			// Token: 0x06000C5F RID: 3167 RVA: 0x0000C690 File Offset: 0x0000A890
			public RoundingScope(X86.MXCSRBits roundingMode)
			{
				this.OldBits = X86.MXCSR;
				X86.MXCSR = (this.OldBits & ~X86.MXCSRBits.RoundingControlMask) | roundingMode;
			}

			// Token: 0x06000C60 RID: 3168 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
			public void Dispose()
			{
				X86.MXCSR = this.OldBits;
			}

			// Token: 0x040002AE RID: 686
			private X86.MXCSRBits OldBits;
		}

		// Token: 0x02000042 RID: 66
		public static class F16C
		{
			// Token: 0x17000045 RID: 69
			// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0000C335 File Offset: 0x0000A535
			public static bool IsF16CSupported
			{
				get
				{
					return X86.Avx2.IsAvx2Supported;
				}
			}

			// Token: 0x06000C62 RID: 3170 RVA: 0x0000C6C0 File Offset: 0x0000A8C0
			[DebuggerStepThrough]
			private static uint HalfToFloat(ushort h)
			{
				bool flag = (h & 32768) > 0;
				long exponent = (long)(h >> 10) & 31L;
				uint mantissa = (uint)(h & 1023);
				uint result = (flag ? 2147483648U : 0U);
				if (exponent != 0L || mantissa != 0U)
				{
					if (exponent == 0L)
					{
						exponent = -1L;
						do
						{
							exponent += 1L;
							mantissa <<= 1;
						}
						while ((mantissa & 1024U) == 0U);
						result |= (uint)((uint)(112L - exponent) << 23);
						result |= (mantissa & 1023U) << 13;
					}
					else
					{
						bool isInfOrNan = exponent == 31L;
						result |= (uint)(isInfOrNan ? 255L : ((uint)(112L + exponent) << 23));
						result |= mantissa << 13;
					}
				}
				return result;
			}

			// Token: 0x06000C63 RID: 3171 RVA: 0x0000C753 File Offset: 0x0000A953
			[DebuggerStepThrough]
			public static v128 cvtph_ps(v128 a)
			{
				return new v128(X86.F16C.HalfToFloat(a.UShort0), X86.F16C.HalfToFloat(a.UShort1), X86.F16C.HalfToFloat(a.UShort2), X86.F16C.HalfToFloat(a.UShort3));
			}

			// Token: 0x06000C64 RID: 3172 RVA: 0x0000C788 File Offset: 0x0000A988
			[DebuggerStepThrough]
			public static v256 mm256_cvtph_ps(v128 a)
			{
				return new v256(X86.F16C.HalfToFloat(a.UShort0), X86.F16C.HalfToFloat(a.UShort1), X86.F16C.HalfToFloat(a.UShort2), X86.F16C.HalfToFloat(a.UShort3), X86.F16C.HalfToFloat(a.UShort4), X86.F16C.HalfToFloat(a.UShort5), X86.F16C.HalfToFloat(a.UShort6), X86.F16C.HalfToFloat(a.UShort7));
			}

			// Token: 0x06000C65 RID: 3173 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
			[DebuggerStepThrough]
			private static ushort FloatToHalf(uint f, int rounding)
			{
				uint exponentAndSign = f >> 23;
				sbyte shift = X86.F16C.ShiftTable[(int)exponentAndSign];
				uint result = (uint)(X86.F16C.BaseTable[(int)exponentAndSign] + (ushort)((f & 8388607U) >> (int)shift));
				bool isFinite = (result & 31744U) != 31744U;
				bool isNegative = (result & 32768U) > 0U;
				if (rounding == 8)
				{
					uint fWithRoundingBitPreserved = (f & 8388607U) >> (int)(shift - 1);
					if ((exponentAndSign & 255U) == 102U)
					{
						result += 1U;
					}
					if (isFinite && (fWithRoundingBitPreserved & 1U) != 0U)
					{
						result += 1U;
					}
				}
				else if (rounding == 11)
				{
					if (!isFinite)
					{
						result -= (uint)(~shift & 1);
					}
				}
				else if (rounding == 10)
				{
					if (isFinite && !isNegative)
					{
						if (exponentAndSign <= 102U && exponentAndSign != 0U)
						{
							result += 1U;
						}
						else if ((f & 8388607U & ((1U << (int)shift) - 1U)) != 0U)
						{
							result += 1U;
						}
					}
					bool flag = result == 64512U;
					bool inputIsNotNegativeInfOrNan = exponentAndSign != 511U;
					if (flag && inputIsNotNegativeInfOrNan)
					{
						result -= 1U;
					}
				}
				else if (rounding == 9)
				{
					if (isFinite && isNegative)
					{
						if (exponentAndSign <= 358U && exponentAndSign != 256U)
						{
							result += 1U;
						}
						else if ((f & 8388607U & ((1U << (int)shift) - 1U)) != 0U)
						{
							result += 1U;
						}
					}
					bool flag2 = result == 31744U;
					bool inputIsNotPositiveInfOrNan = exponentAndSign != 255U;
					if (flag2 && inputIsNotPositiveInfOrNan)
					{
						result -= 1U;
					}
				}
				return (ushort)result;
			}

			// Token: 0x06000C66 RID: 3174 RVA: 0x0000C93C File Offset: 0x0000AB3C
			[DebuggerStepThrough]
			public static v128 cvtps_ph(v128 a, int rounding)
			{
				if (rounding == 12)
				{
					X86.MXCSRBits mxcsrbits = X86.MXCSR & X86.MXCSRBits.RoundingControlMask;
					if (mxcsrbits <= X86.MXCSRBits.RoundDown)
					{
						if (mxcsrbits != X86.MXCSRBits.RoundToNearest)
						{
							if (mxcsrbits == X86.MXCSRBits.RoundDown)
							{
								rounding = 9;
							}
						}
						else
						{
							rounding = 8;
						}
					}
					else if (mxcsrbits != X86.MXCSRBits.RoundUp)
					{
						if (mxcsrbits == X86.MXCSRBits.RoundingControlMask)
						{
							rounding = 11;
						}
					}
					else
					{
						rounding = 10;
					}
				}
				return new v128(X86.F16C.FloatToHalf(a.UInt0, rounding), X86.F16C.FloatToHalf(a.UInt1, rounding), X86.F16C.FloatToHalf(a.UInt2, rounding), X86.F16C.FloatToHalf(a.UInt3, rounding), 0, 0, 0, 0);
			}

			// Token: 0x06000C67 RID: 3175 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
			[DebuggerStepThrough]
			public static v128 mm256_cvtps_ph(v256 a, int rounding)
			{
				if (rounding == 12)
				{
					X86.MXCSRBits mxcsrbits = X86.MXCSR & X86.MXCSRBits.RoundingControlMask;
					if (mxcsrbits <= X86.MXCSRBits.RoundDown)
					{
						if (mxcsrbits != X86.MXCSRBits.RoundToNearest)
						{
							if (mxcsrbits == X86.MXCSRBits.RoundDown)
							{
								rounding = 9;
							}
						}
						else
						{
							rounding = 8;
						}
					}
					else if (mxcsrbits != X86.MXCSRBits.RoundUp)
					{
						if (mxcsrbits == X86.MXCSRBits.RoundingControlMask)
						{
							rounding = 11;
						}
					}
					else
					{
						rounding = 10;
					}
				}
				return new v128(X86.F16C.FloatToHalf(a.UInt0, rounding), X86.F16C.FloatToHalf(a.UInt1, rounding), X86.F16C.FloatToHalf(a.UInt2, rounding), X86.F16C.FloatToHalf(a.UInt3, rounding), X86.F16C.FloatToHalf(a.UInt4, rounding), X86.F16C.FloatToHalf(a.UInt5, rounding), X86.F16C.FloatToHalf(a.UInt6, rounding), X86.F16C.FloatToHalf(a.UInt7, rounding));
			}

			// Token: 0x040002AF RID: 687
			private static readonly ushort[] BaseTable = new ushort[]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 1, 2, 4, 8, 16, 32, 64,
				128, 256, 512, 1024, 2048, 3072, 4096, 5120, 6144, 7168,
				8192, 9216, 10240, 11264, 12288, 13312, 14336, 15360, 16384, 17408,
				18432, 19456, 20480, 21504, 22528, 23552, 24576, 25600, 26624, 27648,
				28672, 29696, 30720, 31744, 31744, 31744, 31744, 31744, 31744, 31744,
				31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744,
				31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744,
				31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744,
				31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744,
				31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744,
				31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744,
				31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744,
				31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744,
				31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744,
				31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744, 31744,
				31744, 31744, 31744, 31744, 31744, 31744, 32768, 32768, 32768, 32768,
				32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768,
				32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768,
				32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768,
				32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768,
				32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768,
				32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768,
				32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768,
				32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768,
				32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768,
				32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32768, 32769,
				32770, 32772, 32776, 32784, 32800, 32832, 32896, 33024, 33280, 33792,
				34816, 35840, 36864, 37888, 38912, 39936, 40960, 41984, 43008, 44032,
				45056, 46080, 47104, 48128, 49152, 50176, 51200, 52224, 53248, 54272,
				55296, 56320, 57344, 58368, 59392, 60416, 61440, 62464, 63488, 64512,
				64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512,
				64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512,
				64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512,
				64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512,
				64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512,
				64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512,
				64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512,
				64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512,
				64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512,
				64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512,
				64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512, 64512,
				64512, 64512
			};

			// Token: 0x040002B0 RID: 688
			private static readonly sbyte[] ShiftTable = new sbyte[]
			{
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 23, 22, 21, 20, 19, 18, 17,
				16, 15, 14, 13, 13, 13, 13, 13, 13, 13,
				13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
				13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
				13, 13, 13, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 13, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 23,
				22, 21, 20, 19, 18, 17, 16, 15, 14, 13,
				13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
				13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
				13, 13, 13, 13, 13, 13, 13, 13, 13, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 13
			};
		}

		// Token: 0x02000043 RID: 67
		public static class Fma
		{
			// Token: 0x17000046 RID: 70
			// (get) Token: 0x06000C69 RID: 3177 RVA: 0x0000C335 File Offset: 0x0000A535
			public static bool IsFmaSupported
			{
				get
				{
					return X86.Avx2.IsAvx2Supported;
				}
			}

			// Token: 0x06000C6A RID: 3178 RVA: 0x0000CAC5 File Offset: 0x0000ACC5
			[DebuggerStepThrough]
			private static float FmaHelper(float a, float b, float c)
			{
				return (float)((double)a * (double)b + (double)c);
			}

			// Token: 0x06000C6B RID: 3179 RVA: 0x0000CAD0 File Offset: 0x0000ACD0
			[DebuggerStepThrough]
			private static float FnmaHelper(float a, float b, float c)
			{
				return X86.Fma.FmaHelper(-a, b, c);
			}

			// Token: 0x06000C6C RID: 3180 RVA: 0x0000CADB File Offset: 0x0000ACDB
			[DebuggerStepThrough]
			public static v128 fmadd_pd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C6D RID: 3181 RVA: 0x0000CADB File Offset: 0x0000ACDB
			[DebuggerStepThrough]
			public static v256 mm256_fmadd_pd(v256 a, v256 b, v256 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C6E RID: 3182 RVA: 0x0000CAE8 File Offset: 0x0000ACE8
			[DebuggerStepThrough]
			public static v128 fmadd_ps(v128 a, v128 b, v128 c)
			{
				return new v128(X86.Fma.FmaHelper(a.Float0, b.Float0, c.Float0), X86.Fma.FmaHelper(a.Float1, b.Float1, c.Float1), X86.Fma.FmaHelper(a.Float2, b.Float2, c.Float2), X86.Fma.FmaHelper(a.Float3, b.Float3, c.Float3));
			}

			// Token: 0x06000C6F RID: 3183 RVA: 0x0000CB58 File Offset: 0x0000AD58
			[DebuggerStepThrough]
			public static v256 mm256_fmadd_ps(v256 a, v256 b, v256 c)
			{
				return new v256(X86.Fma.FmaHelper(a.Float0, b.Float0, c.Float0), X86.Fma.FmaHelper(a.Float1, b.Float1, c.Float1), X86.Fma.FmaHelper(a.Float2, b.Float2, c.Float2), X86.Fma.FmaHelper(a.Float3, b.Float3, c.Float3), X86.Fma.FmaHelper(a.Float4, b.Float4, c.Float4), X86.Fma.FmaHelper(a.Float5, b.Float5, c.Float5), X86.Fma.FmaHelper(a.Float6, b.Float6, c.Float6), X86.Fma.FmaHelper(a.Float7, b.Float7, c.Float7));
			}

			// Token: 0x06000C70 RID: 3184 RVA: 0x0000CADB File Offset: 0x0000ACDB
			[DebuggerStepThrough]
			public static v128 fmadd_sd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C71 RID: 3185 RVA: 0x0000CC24 File Offset: 0x0000AE24
			[DebuggerStepThrough]
			public static v128 fmadd_ss(v128 a, v128 b, v128 c)
			{
				v128 result = a;
				result.Float0 = X86.Fma.FmaHelper(a.Float0, b.Float0, c.Float0);
				return result;
			}

			// Token: 0x06000C72 RID: 3186 RVA: 0x0000CADB File Offset: 0x0000ACDB
			[DebuggerStepThrough]
			public static v128 fmaddsub_pd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C73 RID: 3187 RVA: 0x0000CADB File Offset: 0x0000ACDB
			[DebuggerStepThrough]
			public static v256 mm256_fmaddsub_pd(v256 a, v256 b, v256 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C74 RID: 3188 RVA: 0x0000CC54 File Offset: 0x0000AE54
			[DebuggerStepThrough]
			public static v128 fmaddsub_ps(v128 a, v128 b, v128 c)
			{
				return new v128(X86.Fma.FmaHelper(a.Float0, b.Float0, -c.Float0), X86.Fma.FmaHelper(a.Float1, b.Float1, c.Float1), X86.Fma.FmaHelper(a.Float2, b.Float2, -c.Float2), X86.Fma.FmaHelper(a.Float3, b.Float3, c.Float3));
			}

			// Token: 0x06000C75 RID: 3189 RVA: 0x0000CCC4 File Offset: 0x0000AEC4
			[DebuggerStepThrough]
			public static v256 mm256_fmaddsub_ps(v256 a, v256 b, v256 c)
			{
				return new v256(X86.Fma.FmaHelper(a.Float0, b.Float0, -c.Float0), X86.Fma.FmaHelper(a.Float1, b.Float1, c.Float1), X86.Fma.FmaHelper(a.Float2, b.Float2, -c.Float2), X86.Fma.FmaHelper(a.Float3, b.Float3, c.Float3), X86.Fma.FmaHelper(a.Float4, b.Float4, -c.Float4), X86.Fma.FmaHelper(a.Float5, b.Float5, c.Float5), X86.Fma.FmaHelper(a.Float6, b.Float6, -c.Float6), X86.Fma.FmaHelper(a.Float7, b.Float7, c.Float7));
			}

			// Token: 0x06000C76 RID: 3190 RVA: 0x0000CADB File Offset: 0x0000ACDB
			[DebuggerStepThrough]
			public static v128 fmsub_pd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C77 RID: 3191 RVA: 0x0000CADB File Offset: 0x0000ACDB
			[DebuggerStepThrough]
			public static v256 mm256_fmsub_pd(v256 a, v256 b, v256 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C78 RID: 3192 RVA: 0x0000CD94 File Offset: 0x0000AF94
			[DebuggerStepThrough]
			public static v128 fmsub_ps(v128 a, v128 b, v128 c)
			{
				return new v128(X86.Fma.FmaHelper(a.Float0, b.Float0, -c.Float0), X86.Fma.FmaHelper(a.Float1, b.Float1, -c.Float1), X86.Fma.FmaHelper(a.Float2, b.Float2, -c.Float2), X86.Fma.FmaHelper(a.Float3, b.Float3, -c.Float3));
			}

			// Token: 0x06000C79 RID: 3193 RVA: 0x0000CE08 File Offset: 0x0000B008
			[DebuggerStepThrough]
			public static v256 mm256_fmsub_ps(v256 a, v256 b, v256 c)
			{
				return new v256(X86.Fma.FmaHelper(a.Float0, b.Float0, -c.Float0), X86.Fma.FmaHelper(a.Float1, b.Float1, -c.Float1), X86.Fma.FmaHelper(a.Float2, b.Float2, -c.Float2), X86.Fma.FmaHelper(a.Float3, b.Float3, -c.Float3), X86.Fma.FmaHelper(a.Float4, b.Float4, -c.Float4), X86.Fma.FmaHelper(a.Float5, b.Float5, -c.Float5), X86.Fma.FmaHelper(a.Float6, b.Float6, -c.Float6), X86.Fma.FmaHelper(a.Float7, b.Float7, -c.Float7));
			}

			// Token: 0x06000C7A RID: 3194 RVA: 0x0000CADB File Offset: 0x0000ACDB
			[DebuggerStepThrough]
			public static v128 fmsub_sd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C7B RID: 3195 RVA: 0x0000CEDC File Offset: 0x0000B0DC
			[DebuggerStepThrough]
			public static v128 fmsub_ss(v128 a, v128 b, v128 c)
			{
				v128 result = a;
				result.Float0 = X86.Fma.FmaHelper(a.Float0, b.Float0, -c.Float0);
				return result;
			}

			// Token: 0x06000C7C RID: 3196 RVA: 0x0000CADB File Offset: 0x0000ACDB
			[DebuggerStepThrough]
			public static v128 fmsubadd_pd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C7D RID: 3197 RVA: 0x0000CADB File Offset: 0x0000ACDB
			[DebuggerStepThrough]
			public static v256 mm256_fmsubadd_pd(v256 a, v256 b, v256 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C7E RID: 3198 RVA: 0x0000CF0C File Offset: 0x0000B10C
			[DebuggerStepThrough]
			public static v128 fmsubadd_ps(v128 a, v128 b, v128 c)
			{
				return new v128(X86.Fma.FmaHelper(a.Float0, b.Float0, c.Float0), X86.Fma.FmaHelper(a.Float1, b.Float1, -c.Float1), X86.Fma.FmaHelper(a.Float2, b.Float2, c.Float2), X86.Fma.FmaHelper(a.Float3, b.Float3, -c.Float3));
			}

			// Token: 0x06000C7F RID: 3199 RVA: 0x0000CF7C File Offset: 0x0000B17C
			[DebuggerStepThrough]
			public static v256 mm256_fmsubadd_ps(v256 a, v256 b, v256 c)
			{
				return new v256(X86.Fma.FmaHelper(a.Float0, b.Float0, c.Float0), X86.Fma.FmaHelper(a.Float1, b.Float1, -c.Float1), X86.Fma.FmaHelper(a.Float2, b.Float2, c.Float2), X86.Fma.FmaHelper(a.Float3, b.Float3, -c.Float3), X86.Fma.FmaHelper(a.Float4, b.Float4, c.Float4), X86.Fma.FmaHelper(a.Float5, b.Float5, -c.Float5), X86.Fma.FmaHelper(a.Float6, b.Float6, c.Float6), X86.Fma.FmaHelper(a.Float7, b.Float7, -c.Float7));
			}

			// Token: 0x06000C80 RID: 3200 RVA: 0x0000CADB File Offset: 0x0000ACDB
			[DebuggerStepThrough]
			public static v128 fnmadd_pd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C81 RID: 3201 RVA: 0x0000CADB File Offset: 0x0000ACDB
			[DebuggerStepThrough]
			public static v256 mm256_fnmadd_pd(v256 a, v256 b, v256 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C82 RID: 3202 RVA: 0x0000D04C File Offset: 0x0000B24C
			[DebuggerStepThrough]
			public static v128 fnmadd_ps(v128 a, v128 b, v128 c)
			{
				return new v128(X86.Fma.FnmaHelper(a.Float0, b.Float0, c.Float0), X86.Fma.FnmaHelper(a.Float1, b.Float1, c.Float1), X86.Fma.FnmaHelper(a.Float2, b.Float2, c.Float2), X86.Fma.FnmaHelper(a.Float3, b.Float3, c.Float3));
			}

			// Token: 0x06000C83 RID: 3203 RVA: 0x0000D0BC File Offset: 0x0000B2BC
			[DebuggerStepThrough]
			public static v256 mm256_fnmadd_ps(v256 a, v256 b, v256 c)
			{
				return new v256(X86.Fma.FnmaHelper(a.Float0, b.Float0, c.Float0), X86.Fma.FnmaHelper(a.Float1, b.Float1, c.Float1), X86.Fma.FnmaHelper(a.Float2, b.Float2, c.Float2), X86.Fma.FnmaHelper(a.Float3, b.Float3, c.Float3), X86.Fma.FnmaHelper(a.Float4, b.Float4, c.Float4), X86.Fma.FnmaHelper(a.Float5, b.Float5, c.Float5), X86.Fma.FnmaHelper(a.Float6, b.Float6, c.Float6), X86.Fma.FnmaHelper(a.Float7, b.Float7, c.Float7));
			}

			// Token: 0x06000C84 RID: 3204 RVA: 0x0000CADB File Offset: 0x0000ACDB
			[DebuggerStepThrough]
			public static v128 fnmadd_sd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C85 RID: 3205 RVA: 0x0000D188 File Offset: 0x0000B388
			[DebuggerStepThrough]
			public static v128 fnmadd_ss(v128 a, v128 b, v128 c)
			{
				v128 result = a;
				result.Float0 = X86.Fma.FnmaHelper(a.Float0, b.Float0, c.Float0);
				return result;
			}

			// Token: 0x06000C86 RID: 3206 RVA: 0x0000CADB File Offset: 0x0000ACDB
			[DebuggerStepThrough]
			public static v128 fnmsub_pd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C87 RID: 3207 RVA: 0x0000CADB File Offset: 0x0000ACDB
			[DebuggerStepThrough]
			public static v256 mm256_fnmsub_pd(v256 a, v256 b, v256 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C88 RID: 3208 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
			[DebuggerStepThrough]
			public static v128 fnmsub_ps(v128 a, v128 b, v128 c)
			{
				return new v128(X86.Fma.FnmaHelper(a.Float0, b.Float0, -c.Float0), X86.Fma.FnmaHelper(a.Float1, b.Float1, -c.Float1), X86.Fma.FnmaHelper(a.Float2, b.Float2, -c.Float2), X86.Fma.FnmaHelper(a.Float3, b.Float3, -c.Float3));
			}

			// Token: 0x06000C89 RID: 3209 RVA: 0x0000D22C File Offset: 0x0000B42C
			[DebuggerStepThrough]
			public static v256 mm256_fnmsub_ps(v256 a, v256 b, v256 c)
			{
				return new v256(X86.Fma.FnmaHelper(a.Float0, b.Float0, -c.Float0), X86.Fma.FnmaHelper(a.Float1, b.Float1, -c.Float1), X86.Fma.FnmaHelper(a.Float2, b.Float2, -c.Float2), X86.Fma.FnmaHelper(a.Float3, b.Float3, -c.Float3), X86.Fma.FnmaHelper(a.Float4, b.Float4, -c.Float4), X86.Fma.FnmaHelper(a.Float5, b.Float5, -c.Float5), X86.Fma.FnmaHelper(a.Float6, b.Float6, -c.Float6), X86.Fma.FnmaHelper(a.Float7, b.Float7, -c.Float7));
			}

			// Token: 0x06000C8A RID: 3210 RVA: 0x0000CADB File Offset: 0x0000ACDB
			[DebuggerStepThrough]
			public static v128 fnmsub_sd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C8B RID: 3211 RVA: 0x0000D300 File Offset: 0x0000B500
			[DebuggerStepThrough]
			public static v128 fnmsub_ss(v128 a, v128 b, v128 c)
			{
				v128 result = a;
				result.Float0 = X86.Fma.FnmaHelper(a.Float0, b.Float0, -c.Float0);
				return result;
			}

			// Token: 0x02000044 RID: 68
			[StructLayout(LayoutKind.Explicit)]
			private struct Union
			{
				// Token: 0x040002B1 RID: 689
				[FieldOffset(0)]
				public float f;

				// Token: 0x040002B2 RID: 690
				[FieldOffset(0)]
				public uint u;
			}
		}

		// Token: 0x02000045 RID: 69
		public static class Popcnt
		{
			// Token: 0x17000047 RID: 71
			// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0000D32F File Offset: 0x0000B52F
			public static bool IsPopcntSupported
			{
				get
				{
					return X86.Sse4_2.IsSse42Supported;
				}
			}

			// Token: 0x06000C8D RID: 3213 RVA: 0x0000D338 File Offset: 0x0000B538
			[DebuggerStepThrough]
			public static int popcnt_u32(uint v)
			{
				int result = 0;
				for (uint mask = 2147483648U; mask != 0U; mask >>= 1)
				{
					result += (((v & mask) != 0U) ? 1 : 0);
				}
				return result;
			}

			// Token: 0x06000C8E RID: 3214 RVA: 0x0000D364 File Offset: 0x0000B564
			[DebuggerStepThrough]
			public static int popcnt_u64(ulong v)
			{
				int result = 0;
				for (ulong mask = 9223372036854775808UL; mask != 0UL; mask >>= 1)
				{
					result += (((v & mask) != 0UL) ? 1 : 0);
				}
				return result;
			}
		}

		// Token: 0x02000046 RID: 70
		public static class Sse
		{
			// Token: 0x17000048 RID: 72
			// (get) Token: 0x06000C8F RID: 3215 RVA: 0x000024DA File Offset: 0x000006DA
			public static bool IsSseSupported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000C90 RID: 3216 RVA: 0x0000D393 File Offset: 0x0000B593
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static v128 load_ps(void* ptr)
			{
				return X86.GenericCSharpLoad(ptr);
			}

			// Token: 0x06000C91 RID: 3217 RVA: 0x0000D393 File Offset: 0x0000B593
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static v128 loadu_ps(void* ptr)
			{
				return X86.GenericCSharpLoad(ptr);
			}

			// Token: 0x06000C92 RID: 3218 RVA: 0x0000D39B File Offset: 0x0000B59B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static void store_ps(void* ptr, v128 val)
			{
				X86.GenericCSharpStore(ptr, val);
			}

			// Token: 0x06000C93 RID: 3219 RVA: 0x0000D39B File Offset: 0x0000B59B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static void storeu_ps(void* ptr, v128 val)
			{
				X86.GenericCSharpStore(ptr, val);
			}

			// Token: 0x06000C94 RID: 3220 RVA: 0x0000D39B File Offset: 0x0000B59B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static void stream_ps(void* mem_addr, v128 a)
			{
				X86.GenericCSharpStore(mem_addr, a);
			}

			// Token: 0x06000C95 RID: 3221 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
			[DebuggerStepThrough]
			public static v128 cvtsi32_ss(v128 a, int b)
			{
				v128 dst = a;
				dst.Float0 = (float)b;
				return dst;
			}

			// Token: 0x06000C96 RID: 3222 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
			[DebuggerStepThrough]
			public static v128 cvtsi64_ss(v128 a, long b)
			{
				v128 dst = a;
				dst.Float0 = (float)b;
				return dst;
			}

			// Token: 0x06000C97 RID: 3223 RVA: 0x0000D3DC File Offset: 0x0000B5DC
			[DebuggerStepThrough]
			public static v128 add_ss(v128 a, v128 b)
			{
				v128 dst = a;
				dst.Float0 += b.Float0;
				return dst;
			}

			// Token: 0x06000C98 RID: 3224 RVA: 0x0000D400 File Offset: 0x0000B600
			[DebuggerStepThrough]
			public static v128 add_ps(v128 a, v128 b)
			{
				v128 dst = a;
				dst.Float0 += b.Float0;
				dst.Float1 += b.Float1;
				dst.Float2 += b.Float2;
				dst.Float3 += b.Float3;
				return dst;
			}

			// Token: 0x06000C99 RID: 3225 RVA: 0x0000D454 File Offset: 0x0000B654
			[DebuggerStepThrough]
			public static v128 sub_ss(v128 a, v128 b)
			{
				v128 dst = a;
				dst.Float0 = a.Float0 - b.Float0;
				return dst;
			}

			// Token: 0x06000C9A RID: 3226 RVA: 0x0000D478 File Offset: 0x0000B678
			[DebuggerStepThrough]
			public static v128 sub_ps(v128 a, v128 b)
			{
				v128 dst = a;
				dst.Float0 -= b.Float0;
				dst.Float1 -= b.Float1;
				dst.Float2 -= b.Float2;
				dst.Float3 -= b.Float3;
				return dst;
			}

			// Token: 0x06000C9B RID: 3227 RVA: 0x0000D4CC File Offset: 0x0000B6CC
			[DebuggerStepThrough]
			public static v128 mul_ss(v128 a, v128 b)
			{
				v128 dst = a;
				dst.Float0 = a.Float0 * b.Float0;
				return dst;
			}

			// Token: 0x06000C9C RID: 3228 RVA: 0x0000D4F0 File Offset: 0x0000B6F0
			[DebuggerStepThrough]
			public static v128 mul_ps(v128 a, v128 b)
			{
				v128 dst = a;
				dst.Float0 *= b.Float0;
				dst.Float1 *= b.Float1;
				dst.Float2 *= b.Float2;
				dst.Float3 *= b.Float3;
				return dst;
			}

			// Token: 0x06000C9D RID: 3229 RVA: 0x0000D544 File Offset: 0x0000B744
			[DebuggerStepThrough]
			public static v128 div_ss(v128 a, v128 b)
			{
				v128 dst = a;
				dst.Float0 = a.Float0 / b.Float0;
				return dst;
			}

			// Token: 0x06000C9E RID: 3230 RVA: 0x0000D568 File Offset: 0x0000B768
			[DebuggerStepThrough]
			public static v128 div_ps(v128 a, v128 b)
			{
				v128 dst = a;
				dst.Float0 /= b.Float0;
				dst.Float1 /= b.Float1;
				dst.Float2 /= b.Float2;
				dst.Float3 /= b.Float3;
				return dst;
			}

			// Token: 0x06000C9F RID: 3231 RVA: 0x0000D5BC File Offset: 0x0000B7BC
			[DebuggerStepThrough]
			public static v128 sqrt_ss(v128 a)
			{
				v128 dst = a;
				dst.Float0 = (float)Math.Sqrt((double)a.Float0);
				return dst;
			}

			// Token: 0x06000CA0 RID: 3232 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
			[DebuggerStepThrough]
			public static v128 sqrt_ps(v128 a)
			{
				return new v128
				{
					Float0 = (float)Math.Sqrt((double)a.Float0),
					Float1 = (float)Math.Sqrt((double)a.Float1),
					Float2 = (float)Math.Sqrt((double)a.Float2),
					Float3 = (float)Math.Sqrt((double)a.Float3)
				};
			}

			// Token: 0x06000CA1 RID: 3233 RVA: 0x0000D648 File Offset: 0x0000B848
			[DebuggerStepThrough]
			public static v128 rcp_ss(v128 a)
			{
				v128 dst = a;
				dst.Float0 = 1f / a.Float0;
				return dst;
			}

			// Token: 0x06000CA2 RID: 3234 RVA: 0x0000D66C File Offset: 0x0000B86C
			[DebuggerStepThrough]
			public static v128 rcp_ps(v128 a)
			{
				return new v128
				{
					Float0 = 1f / a.Float0,
					Float1 = 1f / a.Float1,
					Float2 = 1f / a.Float2,
					Float3 = 1f / a.Float3
				};
			}

			// Token: 0x06000CA3 RID: 3235 RVA: 0x0000D6D0 File Offset: 0x0000B8D0
			[DebuggerStepThrough]
			public static v128 rsqrt_ss(v128 a)
			{
				v128 dst = a;
				dst.Float0 = 1f / (float)Math.Sqrt((double)a.Float0);
				return dst;
			}

			// Token: 0x06000CA4 RID: 3236 RVA: 0x0000D6FC File Offset: 0x0000B8FC
			[DebuggerStepThrough]
			public static v128 rsqrt_ps(v128 a)
			{
				return new v128
				{
					Float0 = 1f / (float)Math.Sqrt((double)a.Float0),
					Float1 = 1f / (float)Math.Sqrt((double)a.Float1),
					Float2 = 1f / (float)Math.Sqrt((double)a.Float2),
					Float3 = 1f / (float)Math.Sqrt((double)a.Float3)
				};
			}

			// Token: 0x06000CA5 RID: 3237 RVA: 0x0000D77C File Offset: 0x0000B97C
			[DebuggerStepThrough]
			public static v128 min_ss(v128 a, v128 b)
			{
				v128 dst = a;
				dst.Float0 = Math.Min(a.Float0, b.Float0);
				return dst;
			}

			// Token: 0x06000CA6 RID: 3238 RVA: 0x0000D7A4 File Offset: 0x0000B9A4
			[DebuggerStepThrough]
			public static v128 min_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = Math.Min(a.Float0, b.Float0),
					Float1 = Math.Min(a.Float1, b.Float1),
					Float2 = Math.Min(a.Float2, b.Float2),
					Float3 = Math.Min(a.Float3, b.Float3)
				};
			}

			// Token: 0x06000CA7 RID: 3239 RVA: 0x0000D81C File Offset: 0x0000BA1C
			[DebuggerStepThrough]
			public static v128 max_ss(v128 a, v128 b)
			{
				v128 dst = a;
				dst.Float0 = Math.Max(a.Float0, b.Float0);
				return dst;
			}

			// Token: 0x06000CA8 RID: 3240 RVA: 0x0000D844 File Offset: 0x0000BA44
			[DebuggerStepThrough]
			public static v128 max_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = Math.Max(a.Float0, b.Float0),
					Float1 = Math.Max(a.Float1, b.Float1),
					Float2 = Math.Max(a.Float2, b.Float2),
					Float3 = Math.Max(a.Float3, b.Float3)
				};
			}

			// Token: 0x06000CA9 RID: 3241 RVA: 0x0000D8BC File Offset: 0x0000BABC
			[DebuggerStepThrough]
			public static v128 and_ps(v128 a, v128 b)
			{
				v128 dst = a;
				dst.UInt0 &= b.UInt0;
				dst.UInt1 &= b.UInt1;
				dst.UInt2 &= b.UInt2;
				dst.UInt3 &= b.UInt3;
				return dst;
			}

			// Token: 0x06000CAA RID: 3242 RVA: 0x0000D910 File Offset: 0x0000BB10
			[DebuggerStepThrough]
			public static v128 andnot_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = (~a.UInt0 & b.UInt0),
					UInt1 = (~a.UInt1 & b.UInt1),
					UInt2 = (~a.UInt2 & b.UInt2),
					UInt3 = (~a.UInt3 & b.UInt3)
				};
			}

			// Token: 0x06000CAB RID: 3243 RVA: 0x0000D97C File Offset: 0x0000BB7C
			[DebuggerStepThrough]
			public static v128 or_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = (a.UInt0 | b.UInt0),
					UInt1 = (a.UInt1 | b.UInt1),
					UInt2 = (a.UInt2 | b.UInt2),
					UInt3 = (a.UInt3 | b.UInt3)
				};
			}

			// Token: 0x06000CAC RID: 3244 RVA: 0x0000D9E4 File Offset: 0x0000BBE4
			[DebuggerStepThrough]
			public static v128 xor_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = (a.UInt0 ^ b.UInt0),
					UInt1 = (a.UInt1 ^ b.UInt1),
					UInt2 = (a.UInt2 ^ b.UInt2),
					UInt3 = (a.UInt3 ^ b.UInt3)
				};
			}

			// Token: 0x06000CAD RID: 3245 RVA: 0x0000DA4C File Offset: 0x0000BC4C
			[DebuggerStepThrough]
			public static v128 cmpeq_ss(v128 a, v128 b)
			{
				v128 dst = a;
				dst.UInt0 = ((a.Float0 == b.Float0) ? uint.MaxValue : 0U);
				return dst;
			}

			// Token: 0x06000CAE RID: 3246 RVA: 0x0000DA78 File Offset: 0x0000BC78
			[DebuggerStepThrough]
			public static v128 cmpeq_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = ((a.Float0 == b.Float0) ? uint.MaxValue : 0U),
					UInt1 = ((a.Float1 == b.Float1) ? uint.MaxValue : 0U),
					UInt2 = ((a.Float2 == b.Float2) ? uint.MaxValue : 0U),
					UInt3 = ((a.Float3 == b.Float3) ? uint.MaxValue : 0U)
				};
			}

			// Token: 0x06000CAF RID: 3247 RVA: 0x0000DAF4 File Offset: 0x0000BCF4
			[DebuggerStepThrough]
			public static v128 cmplt_ss(v128 a, v128 b)
			{
				v128 dst = a;
				dst.UInt0 = ((a.Float0 < b.Float0) ? uint.MaxValue : 0U);
				return dst;
			}

			// Token: 0x06000CB0 RID: 3248 RVA: 0x0000DB20 File Offset: 0x0000BD20
			[DebuggerStepThrough]
			public static v128 cmplt_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = ((a.Float0 < b.Float0) ? uint.MaxValue : 0U),
					UInt1 = ((a.Float1 < b.Float1) ? uint.MaxValue : 0U),
					UInt2 = ((a.Float2 < b.Float2) ? uint.MaxValue : 0U),
					UInt3 = ((a.Float3 < b.Float3) ? uint.MaxValue : 0U)
				};
			}

			// Token: 0x06000CB1 RID: 3249 RVA: 0x0000DB9C File Offset: 0x0000BD9C
			[DebuggerStepThrough]
			public static v128 cmple_ss(v128 a, v128 b)
			{
				v128 dst = a;
				dst.UInt0 = ((a.Float0 <= b.Float0) ? uint.MaxValue : 0U);
				return dst;
			}

			// Token: 0x06000CB2 RID: 3250 RVA: 0x0000DBC8 File Offset: 0x0000BDC8
			[DebuggerStepThrough]
			public static v128 cmple_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = ((a.Float0 <= b.Float0) ? uint.MaxValue : 0U),
					UInt1 = ((a.Float1 <= b.Float1) ? uint.MaxValue : 0U),
					UInt2 = ((a.Float2 <= b.Float2) ? uint.MaxValue : 0U),
					UInt3 = ((a.Float3 <= b.Float3) ? uint.MaxValue : 0U)
				};
			}

			// Token: 0x06000CB3 RID: 3251 RVA: 0x0000DC42 File Offset: 0x0000BE42
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpgt_ss(v128 a, v128 b)
			{
				return X86.Sse.cmplt_ss(b, a);
			}

			// Token: 0x06000CB4 RID: 3252 RVA: 0x0000DC4B File Offset: 0x0000BE4B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpgt_ps(v128 a, v128 b)
			{
				return X86.Sse.cmplt_ps(b, a);
			}

			// Token: 0x06000CB5 RID: 3253 RVA: 0x0000DC54 File Offset: 0x0000BE54
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpge_ss(v128 a, v128 b)
			{
				return X86.Sse.cmple_ss(b, a);
			}

			// Token: 0x06000CB6 RID: 3254 RVA: 0x0000DC5D File Offset: 0x0000BE5D
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpge_ps(v128 a, v128 b)
			{
				return X86.Sse.cmple_ps(b, a);
			}

			// Token: 0x06000CB7 RID: 3255 RVA: 0x0000DC68 File Offset: 0x0000BE68
			[DebuggerStepThrough]
			public static v128 cmpneq_ss(v128 a, v128 b)
			{
				v128 dst = a;
				dst.UInt0 = ((a.Float0 != b.Float0) ? uint.MaxValue : 0U);
				return dst;
			}

			// Token: 0x06000CB8 RID: 3256 RVA: 0x0000DC94 File Offset: 0x0000BE94
			[DebuggerStepThrough]
			public static v128 cmpneq_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = ((a.Float0 != b.Float0) ? uint.MaxValue : 0U),
					UInt1 = ((a.Float1 != b.Float1) ? uint.MaxValue : 0U),
					UInt2 = ((a.Float2 != b.Float2) ? uint.MaxValue : 0U),
					UInt3 = ((a.Float3 != b.Float3) ? uint.MaxValue : 0U)
				};
			}

			// Token: 0x06000CB9 RID: 3257 RVA: 0x0000DD10 File Offset: 0x0000BF10
			[DebuggerStepThrough]
			public static v128 cmpnlt_ss(v128 a, v128 b)
			{
				v128 dst = a;
				dst.UInt0 = ((a.Float0 >= b.Float0) ? uint.MaxValue : 0U);
				return dst;
			}

			// Token: 0x06000CBA RID: 3258 RVA: 0x0000DD3C File Offset: 0x0000BF3C
			[DebuggerStepThrough]
			public static v128 cmpnlt_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = ((a.Float0 >= b.Float0) ? uint.MaxValue : 0U),
					UInt1 = ((a.Float1 >= b.Float1) ? uint.MaxValue : 0U),
					UInt2 = ((a.Float2 >= b.Float2) ? uint.MaxValue : 0U),
					UInt3 = ((a.Float3 >= b.Float3) ? uint.MaxValue : 0U)
				};
			}

			// Token: 0x06000CBB RID: 3259 RVA: 0x0000DDB8 File Offset: 0x0000BFB8
			[DebuggerStepThrough]
			public static v128 cmpnle_ss(v128 a, v128 b)
			{
				v128 dst = a;
				dst.UInt0 = ((a.Float0 > b.Float0) ? uint.MaxValue : 0U);
				return dst;
			}

			// Token: 0x06000CBC RID: 3260 RVA: 0x0000DDE4 File Offset: 0x0000BFE4
			[DebuggerStepThrough]
			public static v128 cmpnle_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = ((a.Float0 > b.Float0) ? uint.MaxValue : 0U),
					UInt1 = ((a.Float1 > b.Float1) ? uint.MaxValue : 0U),
					UInt2 = ((a.Float2 > b.Float2) ? uint.MaxValue : 0U),
					UInt3 = ((a.Float3 > b.Float3) ? uint.MaxValue : 0U)
				};
			}

			// Token: 0x06000CBD RID: 3261 RVA: 0x0000DE5E File Offset: 0x0000C05E
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpngt_ss(v128 a, v128 b)
			{
				return X86.Sse.cmpnlt_ss(b, a);
			}

			// Token: 0x06000CBE RID: 3262 RVA: 0x0000DE67 File Offset: 0x0000C067
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpngt_ps(v128 a, v128 b)
			{
				return X86.Sse.cmpnlt_ps(b, a);
			}

			// Token: 0x06000CBF RID: 3263 RVA: 0x0000DE70 File Offset: 0x0000C070
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpnge_ss(v128 a, v128 b)
			{
				return X86.Sse.cmpnle_ss(b, a);
			}

			// Token: 0x06000CC0 RID: 3264 RVA: 0x0000DE79 File Offset: 0x0000C079
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpnge_ps(v128 a, v128 b)
			{
				return X86.Sse.cmpnle_ps(b, a);
			}

			// Token: 0x06000CC1 RID: 3265 RVA: 0x0000DE84 File Offset: 0x0000C084
			[DebuggerStepThrough]
			public static v128 cmpord_ss(v128 a, v128 b)
			{
				v128 dst = a;
				dst.UInt0 = ((X86.IsNaN(a.UInt0) || X86.IsNaN(b.UInt0)) ? 0U : uint.MaxValue);
				return dst;
			}

			// Token: 0x06000CC2 RID: 3266 RVA: 0x0000DEBC File Offset: 0x0000C0BC
			[DebuggerStepThrough]
			public static v128 cmpord_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = ((X86.IsNaN(a.UInt0) || X86.IsNaN(b.UInt0)) ? 0U : uint.MaxValue),
					UInt1 = ((X86.IsNaN(a.UInt1) || X86.IsNaN(b.UInt1)) ? 0U : uint.MaxValue),
					UInt2 = ((X86.IsNaN(a.UInt2) || X86.IsNaN(b.UInt2)) ? 0U : uint.MaxValue),
					UInt3 = ((X86.IsNaN(a.UInt3) || X86.IsNaN(b.UInt3)) ? 0U : uint.MaxValue)
				};
			}

			// Token: 0x06000CC3 RID: 3267 RVA: 0x0000DF68 File Offset: 0x0000C168
			[DebuggerStepThrough]
			public static v128 cmpunord_ss(v128 a, v128 b)
			{
				v128 dst = a;
				dst.UInt0 = ((X86.IsNaN(a.UInt0) || X86.IsNaN(b.UInt0)) ? uint.MaxValue : 0U);
				return dst;
			}

			// Token: 0x06000CC4 RID: 3268 RVA: 0x0000DFA0 File Offset: 0x0000C1A0
			[DebuggerStepThrough]
			public static v128 cmpunord_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = ((X86.IsNaN(a.UInt0) || X86.IsNaN(b.UInt0)) ? uint.MaxValue : 0U),
					UInt1 = ((X86.IsNaN(a.UInt1) || X86.IsNaN(b.UInt1)) ? uint.MaxValue : 0U),
					UInt2 = ((X86.IsNaN(a.UInt2) || X86.IsNaN(b.UInt2)) ? uint.MaxValue : 0U),
					UInt3 = ((X86.IsNaN(a.UInt3) || X86.IsNaN(b.UInt3)) ? uint.MaxValue : 0U)
				};
			}

			// Token: 0x06000CC5 RID: 3269 RVA: 0x0000E04A File Offset: 0x0000C24A
			[DebuggerStepThrough]
			public static int comieq_ss(v128 a, v128 b)
			{
				if (a.Float0 != b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CC6 RID: 3270 RVA: 0x0000E05D File Offset: 0x0000C25D
			[DebuggerStepThrough]
			public static int comilt_ss(v128 a, v128 b)
			{
				if (a.Float0 >= b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CC7 RID: 3271 RVA: 0x0000E070 File Offset: 0x0000C270
			[DebuggerStepThrough]
			public static int comile_ss(v128 a, v128 b)
			{
				if (a.Float0 > b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CC8 RID: 3272 RVA: 0x0000E083 File Offset: 0x0000C283
			[DebuggerStepThrough]
			public static int comigt_ss(v128 a, v128 b)
			{
				if (a.Float0 <= b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CC9 RID: 3273 RVA: 0x0000E096 File Offset: 0x0000C296
			[DebuggerStepThrough]
			public static int comige_ss(v128 a, v128 b)
			{
				if (a.Float0 < b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CCA RID: 3274 RVA: 0x0000E0A9 File Offset: 0x0000C2A9
			[DebuggerStepThrough]
			public static int comineq_ss(v128 a, v128 b)
			{
				if (a.Float0 == b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CCB RID: 3275 RVA: 0x0000E04A File Offset: 0x0000C24A
			[DebuggerStepThrough]
			public static int ucomieq_ss(v128 a, v128 b)
			{
				if (a.Float0 != b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CCC RID: 3276 RVA: 0x0000E05D File Offset: 0x0000C25D
			[DebuggerStepThrough]
			public static int ucomilt_ss(v128 a, v128 b)
			{
				if (a.Float0 >= b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CCD RID: 3277 RVA: 0x0000E070 File Offset: 0x0000C270
			[DebuggerStepThrough]
			public static int ucomile_ss(v128 a, v128 b)
			{
				if (a.Float0 > b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CCE RID: 3278 RVA: 0x0000E083 File Offset: 0x0000C283
			[DebuggerStepThrough]
			public static int ucomigt_ss(v128 a, v128 b)
			{
				if (a.Float0 <= b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CCF RID: 3279 RVA: 0x0000E096 File Offset: 0x0000C296
			[DebuggerStepThrough]
			public static int ucomige_ss(v128 a, v128 b)
			{
				if (a.Float0 < b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CD0 RID: 3280 RVA: 0x0000E0A9 File Offset: 0x0000C2A9
			[DebuggerStepThrough]
			public static int ucomineq_ss(v128 a, v128 b)
			{
				if (a.Float0 == b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CD1 RID: 3281 RVA: 0x0000E0BC File Offset: 0x0000C2BC
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static int cvtss_si32(v128 a)
			{
				return X86.Sse.cvt_ss2si(a);
			}

			// Token: 0x06000CD2 RID: 3282 RVA: 0x0000E0C4 File Offset: 0x0000C2C4
			[DebuggerStepThrough]
			public static int cvt_ss2si(v128 a)
			{
				return (int)Math.Round((double)a.Float0, MidpointRounding.ToEven);
			}

			// Token: 0x06000CD3 RID: 3283 RVA: 0x0000E0D4 File Offset: 0x0000C2D4
			[DebuggerStepThrough]
			public static long cvtss_si64(v128 a)
			{
				return (long)Math.Round((double)a.Float0, MidpointRounding.ToEven);
			}

			// Token: 0x06000CD4 RID: 3284 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
			[DebuggerStepThrough]
			public static float cvtss_f32(v128 a)
			{
				return a.Float0;
			}

			// Token: 0x06000CD5 RID: 3285 RVA: 0x0000E0EC File Offset: 0x0000C2EC
			[DebuggerStepThrough]
			public static int cvttss_si32(v128 a)
			{
				int num;
				using (new X86.RoundingScope(X86.MXCSRBits.RoundingControlMask))
				{
					num = (int)a.Float0;
				}
				return num;
			}

			// Token: 0x06000CD6 RID: 3286 RVA: 0x0000E130 File Offset: 0x0000C330
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static int cvtt_ss2si(v128 a)
			{
				return X86.Sse.cvttss_si32(a);
			}

			// Token: 0x06000CD7 RID: 3287 RVA: 0x0000E138 File Offset: 0x0000C338
			[DebuggerStepThrough]
			public static long cvttss_si64(v128 a)
			{
				long num;
				using (new X86.RoundingScope(X86.MXCSRBits.RoundingControlMask))
				{
					num = (long)a.Float0;
				}
				return num;
			}

			// Token: 0x06000CD8 RID: 3288 RVA: 0x0000E17C File Offset: 0x0000C37C
			[DebuggerStepThrough]
			public static v128 set_ss(float a)
			{
				return new v128(a, 0f, 0f, 0f);
			}

			// Token: 0x06000CD9 RID: 3289 RVA: 0x0000E193 File Offset: 0x0000C393
			[DebuggerStepThrough]
			public static v128 set1_ps(float a)
			{
				return new v128(a, a, a, a);
			}

			// Token: 0x06000CDA RID: 3290 RVA: 0x0000E19E File Offset: 0x0000C39E
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 set_ps1(float a)
			{
				return X86.Sse.set1_ps(a);
			}

			// Token: 0x06000CDB RID: 3291 RVA: 0x0000E1A6 File Offset: 0x0000C3A6
			[DebuggerStepThrough]
			public static v128 set_ps(float e3, float e2, float e1, float e0)
			{
				return new v128(e0, e1, e2, e3);
			}

			// Token: 0x06000CDC RID: 3292 RVA: 0x0000E1B1 File Offset: 0x0000C3B1
			[DebuggerStepThrough]
			public static v128 setr_ps(float e3, float e2, float e1, float e0)
			{
				return new v128(e3, e2, e1, e0);
			}

			// Token: 0x06000CDD RID: 3293 RVA: 0x0000E1BC File Offset: 0x0000C3BC
			[DebuggerStepThrough]
			public static v128 move_ss(v128 a, v128 b)
			{
				v128 dst = a;
				dst.Float0 = b.Float0;
				return dst;
			}

			// Token: 0x06000CDE RID: 3294 RVA: 0x0000E1D9 File Offset: 0x0000C3D9
			public static int SHUFFLE(int d, int c, int b, int a)
			{
				return (a & 3) | ((b & 3) << 2) | ((c & 3) << 4) | ((d & 3) << 6);
			}

			// Token: 0x06000CDF RID: 3295 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
			[DebuggerStepThrough]
			public unsafe static v128 shuffle_ps(v128 a, v128 b, int imm8)
			{
				v128 dst = default(v128);
				uint* aptr = &a.UInt0;
				uint* bptr = &b.UInt0;
				dst.UInt0 = aptr[imm8 & 3];
				dst.UInt1 = aptr[(imm8 >> 2) & 3];
				dst.UInt2 = bptr[(imm8 >> 4) & 3];
				dst.UInt3 = bptr[(imm8 >> 6) & 3];
				return dst;
			}

			// Token: 0x06000CE0 RID: 3296 RVA: 0x0000E260 File Offset: 0x0000C460
			[DebuggerStepThrough]
			public static v128 unpackhi_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = a.Float2,
					Float1 = b.Float2,
					Float2 = a.Float3,
					Float3 = b.Float3
				};
			}

			// Token: 0x06000CE1 RID: 3297 RVA: 0x0000E2AC File Offset: 0x0000C4AC
			[DebuggerStepThrough]
			public static v128 unpacklo_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = a.Float0,
					Float1 = b.Float0,
					Float2 = a.Float1,
					Float3 = b.Float1
				};
			}

			// Token: 0x06000CE2 RID: 3298 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
			[DebuggerStepThrough]
			public static v128 movehl_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = b.Float2,
					Float1 = b.Float3,
					Float2 = a.Float2,
					Float3 = a.Float3
				};
			}

			// Token: 0x06000CE3 RID: 3299 RVA: 0x0000E344 File Offset: 0x0000C544
			[DebuggerStepThrough]
			public static v128 movelh_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = a.Float0,
					Float1 = a.Float1,
					Float2 = b.Float0,
					Float3 = b.Float1
				};
			}

			// Token: 0x06000CE4 RID: 3300 RVA: 0x0000E390 File Offset: 0x0000C590
			[DebuggerStepThrough]
			public static int movemask_ps(v128 a)
			{
				int dst = 0;
				if ((a.UInt0 & 2147483648U) != 0U)
				{
					dst |= 1;
				}
				if ((a.UInt1 & 2147483648U) != 0U)
				{
					dst |= 2;
				}
				if ((a.UInt2 & 2147483648U) != 0U)
				{
					dst |= 4;
				}
				if ((a.UInt3 & 2147483648U) != 0U)
				{
					dst |= 8;
				}
				return dst;
			}

			// Token: 0x06000CE5 RID: 3301 RVA: 0x0000E3E8 File Offset: 0x0000C5E8
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static void TRANSPOSE4_PS(ref v128 row0, ref v128 row1, ref v128 row2, ref v128 row3)
			{
				v128 _Tmp0 = X86.Sse.shuffle_ps(row0, row1, 68);
				v128 _Tmp = X86.Sse.shuffle_ps(row0, row1, 238);
				v128 _Tmp2 = X86.Sse.shuffle_ps(row2, row3, 68);
				v128 _Tmp3 = X86.Sse.shuffle_ps(row2, row3, 238);
				row0 = X86.Sse.shuffle_ps(_Tmp0, _Tmp2, 136);
				row1 = X86.Sse.shuffle_ps(_Tmp0, _Tmp2, 221);
				row2 = X86.Sse.shuffle_ps(_Tmp, _Tmp3, 136);
				row3 = X86.Sse.shuffle_ps(_Tmp, _Tmp3, 221);
			}

			// Token: 0x06000CE6 RID: 3302 RVA: 0x0000E494 File Offset: 0x0000C694
			[DebuggerStepThrough]
			public static v128 setzero_ps()
			{
				return default(v128);
			}

			// Token: 0x06000CE7 RID: 3303 RVA: 0x0000E4AA File Offset: 0x0000C6AA
			[DebuggerStepThrough]
			public unsafe static v128 loadu_si16(void* mem_addr)
			{
				return new v128(*(short*)mem_addr, 0, 0, 0, 0, 0, 0, 0);
			}

			// Token: 0x06000CE8 RID: 3304 RVA: 0x0000E4BA File Offset: 0x0000C6BA
			public unsafe static void storeu_si16(void* mem_addr, v128 a)
			{
				*(short*)mem_addr = a.SShort0;
			}

			// Token: 0x06000CE9 RID: 3305 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
			[DebuggerStepThrough]
			public unsafe static v128 loadu_si64(void* mem_addr)
			{
				return new v128(*(long*)mem_addr, 0L);
			}

			// Token: 0x06000CEA RID: 3306 RVA: 0x0000E4CF File Offset: 0x0000C6CF
			[DebuggerStepThrough]
			public unsafe static void storeu_si64(void* mem_addr, v128 a)
			{
				*(long*)mem_addr = a.SLong0;
			}
		}

		// Token: 0x02000047 RID: 71
		public static class Sse2
		{
			// Token: 0x17000049 RID: 73
			// (get) Token: 0x06000CEB RID: 3307 RVA: 0x000024DA File Offset: 0x000006DA
			public static bool IsSse2Supported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000CEC RID: 3308 RVA: 0x0000E4D9 File Offset: 0x0000C6D9
			[DebuggerStepThrough]
			public static int SHUFFLE2(int x, int y)
			{
				return y | (x << 1);
			}

			// Token: 0x06000CED RID: 3309 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
			[DebuggerStepThrough]
			public unsafe static void stream_si32(int* mem_addr, int a)
			{
				*mem_addr = a;
			}

			// Token: 0x06000CEE RID: 3310 RVA: 0x0000E4E5 File Offset: 0x0000C6E5
			[DebuggerStepThrough]
			public unsafe static void stream_si64(long* mem_addr, long a)
			{
				*mem_addr = a;
			}

			// Token: 0x06000CEF RID: 3311 RVA: 0x0000D39B File Offset: 0x0000B59B
			[DebuggerStepThrough]
			public unsafe static void stream_pd(void* mem_addr, v128 a)
			{
				X86.GenericCSharpStore(mem_addr, a);
			}

			// Token: 0x06000CF0 RID: 3312 RVA: 0x0000D39B File Offset: 0x0000B59B
			[DebuggerStepThrough]
			public unsafe static void stream_si128(void* mem_addr, v128 a)
			{
				X86.GenericCSharpStore(mem_addr, a);
			}

			// Token: 0x06000CF1 RID: 3313 RVA: 0x0000E4EC File Offset: 0x0000C6EC
			[DebuggerStepThrough]
			public unsafe static v128 add_epi8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				sbyte* dptr = &dst.SByte0;
				sbyte* aptr = &a.SByte0;
				sbyte* bptr = &b.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = aptr[i] + bptr[i];
				}
				return dst;
			}

			// Token: 0x06000CF2 RID: 3314 RVA: 0x0000E540 File Offset: 0x0000C740
			[DebuggerStepThrough]
			public unsafe static v128 add_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = aptr[i] + bptr[i];
				}
				return dst;
			}

			// Token: 0x06000CF3 RID: 3315 RVA: 0x0000E59C File Offset: 0x0000C79C
			[DebuggerStepThrough]
			public static v128 add_epi32(v128 a, v128 b)
			{
				return new v128
				{
					SInt0 = a.SInt0 + b.SInt0,
					SInt1 = a.SInt1 + b.SInt1,
					SInt2 = a.SInt2 + b.SInt2,
					SInt3 = a.SInt3 + b.SInt3
				};
			}

			// Token: 0x06000CF4 RID: 3316 RVA: 0x0000E604 File Offset: 0x0000C804
			[DebuggerStepThrough]
			public static v128 add_epi64(v128 a, v128 b)
			{
				return new v128
				{
					SLong0 = a.SLong0 + b.SLong0,
					SLong1 = a.SLong1 + b.SLong1
				};
			}

			// Token: 0x06000CF5 RID: 3317 RVA: 0x0000E644 File Offset: 0x0000C844
			[DebuggerStepThrough]
			public unsafe static v128 adds_epi8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				sbyte* dptr = &dst.SByte0;
				sbyte* aptr = &a.SByte0;
				sbyte* bptr = &b.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = X86.Saturate_To_Int8((int)(aptr[i] + bptr[i]));
				}
				return dst;
			}

			// Token: 0x06000CF6 RID: 3318 RVA: 0x0000E69C File Offset: 0x0000C89C
			[DebuggerStepThrough]
			public unsafe static v128 adds_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = X86.Saturate_To_Int16((int)(aptr[i] + bptr[i]));
				}
				return dst;
			}

			// Token: 0x06000CF7 RID: 3319 RVA: 0x0000E6FC File Offset: 0x0000C8FC
			[DebuggerStepThrough]
			public unsafe static v128 adds_epu8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				byte* dptr = &dst.Byte0;
				byte* aptr = &a.Byte0;
				byte* bptr = &b.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = X86.Saturate_To_UnsignedInt8((int)(aptr[i] + bptr[i]));
				}
				return dst;
			}

			// Token: 0x06000CF8 RID: 3320 RVA: 0x0000E754 File Offset: 0x0000C954
			[DebuggerStepThrough]
			public unsafe static v128 adds_epu16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				ushort* dptr = &dst.UShort0;
				ushort* aptr = &a.UShort0;
				ushort* bptr = &b.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = X86.Saturate_To_UnsignedInt16((int)(aptr[i] + bptr[i]));
				}
				return dst;
			}

			// Token: 0x06000CF9 RID: 3321 RVA: 0x0000E7B4 File Offset: 0x0000C9B4
			[DebuggerStepThrough]
			public unsafe static v128 avg_epu8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				byte* dptr = &dst.Byte0;
				byte* aptr = &a.Byte0;
				byte* bptr = &b.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = (byte)(aptr[i] + bptr[i] + 1 >> 1);
				}
				return dst;
			}

			// Token: 0x06000CFA RID: 3322 RVA: 0x0000E80C File Offset: 0x0000CA0C
			[DebuggerStepThrough]
			public unsafe static v128 avg_epu16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				ushort* dptr = &dst.UShort0;
				ushort* aptr = &a.UShort0;
				ushort* bptr = &b.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = (ushort)(aptr[i] + bptr[i] + 1 >> 1);
				}
				return dst;
			}

			// Token: 0x06000CFB RID: 3323 RVA: 0x0000E86C File Offset: 0x0000CA6C
			[DebuggerStepThrough]
			public unsafe static v128 madd_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				int* dptr = &dst.SInt0;
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				for (int i = 0; i <= 3; i++)
				{
					int j = 2 * i;
					int r = (int)(aptr[j + 1] * bptr[j + 1]);
					int q = (int)(aptr[j] * bptr[j]);
					dptr[i] = r + q;
				}
				return dst;
			}

			// Token: 0x06000CFC RID: 3324 RVA: 0x0000E8EC File Offset: 0x0000CAEC
			[DebuggerStepThrough]
			public unsafe static v128 max_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = Math.Max(aptr[i], bptr[i]);
				}
				return dst;
			}

			// Token: 0x06000CFD RID: 3325 RVA: 0x0000E94C File Offset: 0x0000CB4C
			[DebuggerStepThrough]
			public unsafe static v128 max_epu8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				byte* dptr = &dst.Byte0;
				byte* aptr = &a.Byte0;
				byte* bptr = &b.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = Math.Max(aptr[i], bptr[i]);
				}
				return dst;
			}

			// Token: 0x06000CFE RID: 3326 RVA: 0x0000E9A4 File Offset: 0x0000CBA4
			[DebuggerStepThrough]
			public unsafe static v128 min_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = Math.Min(aptr[i], bptr[i]);
				}
				return dst;
			}

			// Token: 0x06000CFF RID: 3327 RVA: 0x0000EA04 File Offset: 0x0000CC04
			[DebuggerStepThrough]
			public unsafe static v128 min_epu8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				byte* dptr = &dst.Byte0;
				byte* aptr = &a.Byte0;
				byte* bptr = &b.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = Math.Min(aptr[i], bptr[i]);
				}
				return dst;
			}

			// Token: 0x06000D00 RID: 3328 RVA: 0x0000EA5C File Offset: 0x0000CC5C
			[DebuggerStepThrough]
			public unsafe static v128 mulhi_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					int tmp = (int)(aptr[i] * bptr[i]);
					dptr[i] = (short)(tmp >> 16);
				}
				return dst;
			}

			// Token: 0x06000D01 RID: 3329 RVA: 0x0000EAC0 File Offset: 0x0000CCC0
			[DebuggerStepThrough]
			public unsafe static v128 mulhi_epu16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				ushort* dptr = &dst.UShort0;
				ushort* aptr = &a.UShort0;
				ushort* bptr = &b.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					uint tmp = (uint)(aptr[i] * bptr[i]);
					dptr[i] = (ushort)(tmp >> 16);
				}
				return dst;
			}

			// Token: 0x06000D02 RID: 3330 RVA: 0x0000EB24 File Offset: 0x0000CD24
			[DebuggerStepThrough]
			public unsafe static v128 mullo_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					int tmp = (int)(aptr[i] * bptr[i]);
					dptr[i] = (short)tmp;
				}
				return dst;
			}

			// Token: 0x06000D03 RID: 3331 RVA: 0x0000EB84 File Offset: 0x0000CD84
			[DebuggerStepThrough]
			public static v128 mul_epu32(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (ulong)a.UInt0 * (ulong)b.UInt0,
					ULong1 = (ulong)a.UInt2 * (ulong)b.UInt2
				};
			}

			// Token: 0x06000D04 RID: 3332 RVA: 0x0000EBC8 File Offset: 0x0000CDC8
			[DebuggerStepThrough]
			public unsafe static v128 sad_epu8(v128 a, v128 b)
			{
				v128 tmp;
				byte* tptr = &tmp.Byte0;
				byte* aptr = &a.Byte0;
				byte* bptr = &b.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					tptr[i] = (byte)Math.Abs((int)(aptr[i] - bptr[i]));
				}
				v128 dst = default(v128);
				ushort* dptr = &dst.UShort0;
				for (int j = 0; j <= 1; j++)
				{
					int bo = j * 8;
					dptr[4 * j] = (ushort)(tptr[bo] + tptr[bo + 1] + tptr[bo + 2] + tptr[bo + 3] + tptr[bo + 4] + tptr[bo + 5] + tptr[bo + 6] + tptr[bo + 7]);
				}
				return dst;
			}

			// Token: 0x06000D05 RID: 3333 RVA: 0x0000EC8C File Offset: 0x0000CE8C
			[DebuggerStepThrough]
			public unsafe static v128 sub_epi8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				sbyte* dptr = &dst.SByte0;
				sbyte* aptr = &a.SByte0;
				sbyte* bptr = &b.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = aptr[i] - bptr[i];
				}
				return dst;
			}

			// Token: 0x06000D06 RID: 3334 RVA: 0x0000ECE0 File Offset: 0x0000CEE0
			[DebuggerStepThrough]
			public unsafe static v128 sub_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = aptr[i] - bptr[i];
				}
				return dst;
			}

			// Token: 0x06000D07 RID: 3335 RVA: 0x0000ED3C File Offset: 0x0000CF3C
			[DebuggerStepThrough]
			public unsafe static v128 sub_epi32(v128 a, v128 b)
			{
				v128 dst = default(v128);
				int* dptr = &dst.SInt0;
				int* aptr = &a.SInt0;
				int* bptr = &b.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = aptr[i] - bptr[i];
				}
				return dst;
			}

			// Token: 0x06000D08 RID: 3336 RVA: 0x0000ED98 File Offset: 0x0000CF98
			[DebuggerStepThrough]
			public unsafe static v128 sub_epi64(v128 a, v128 b)
			{
				v128 dst = default(v128);
				long* dptr = &dst.SLong0;
				long* aptr = &a.SLong0;
				long* bptr = &b.SLong0;
				for (int i = 0; i <= 1; i++)
				{
					dptr[i] = aptr[i] - bptr[i];
				}
				return dst;
			}

			// Token: 0x06000D09 RID: 3337 RVA: 0x0000EDF4 File Offset: 0x0000CFF4
			[DebuggerStepThrough]
			public unsafe static v128 subs_epi8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				sbyte* dptr = &dst.SByte0;
				sbyte* aptr = &a.SByte0;
				sbyte* bptr = &b.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = X86.Saturate_To_Int8((int)(aptr[i] - bptr[i]));
				}
				return dst;
			}

			// Token: 0x06000D0A RID: 3338 RVA: 0x0000EE4C File Offset: 0x0000D04C
			[DebuggerStepThrough]
			public unsafe static v128 subs_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = X86.Saturate_To_Int16((int)(aptr[i] - bptr[i]));
				}
				return dst;
			}

			// Token: 0x06000D0B RID: 3339 RVA: 0x0000EEAC File Offset: 0x0000D0AC
			[DebuggerStepThrough]
			public unsafe static v128 subs_epu8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				byte* dptr = &dst.Byte0;
				byte* aptr = &a.Byte0;
				byte* bptr = &b.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = X86.Saturate_To_UnsignedInt8((int)(aptr[i] - bptr[i]));
				}
				return dst;
			}

			// Token: 0x06000D0C RID: 3340 RVA: 0x0000EF04 File Offset: 0x0000D104
			[DebuggerStepThrough]
			public unsafe static v128 subs_epu16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				ushort* dptr = &dst.UShort0;
				ushort* aptr = &a.UShort0;
				ushort* bptr = &b.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = X86.Saturate_To_UnsignedInt16((int)(aptr[i] - bptr[i]));
				}
				return dst;
			}

			// Token: 0x06000D0D RID: 3341 RVA: 0x0000EF64 File Offset: 0x0000D164
			[DebuggerStepThrough]
			public unsafe static v128 slli_si128(v128 a, int imm8)
			{
				int dist = Math.Min(imm8 & 255, 16);
				v128 dst = default(v128);
				byte* dptr = &dst.Byte0;
				byte* aptr = &a.Byte0;
				for (int i = 0; i < dist; i++)
				{
					dptr[i] = 0;
				}
				for (int j = dist; j < 16; j++)
				{
					dptr[j] = aptr[j - dist];
				}
				return dst;
			}

			// Token: 0x06000D0E RID: 3342 RVA: 0x0000EFCE File Offset: 0x0000D1CE
			[DebuggerStepThrough]
			public static v128 bslli_si128(v128 a, int imm8)
			{
				return X86.Sse2.slli_si128(a, imm8);
			}

			// Token: 0x06000D0F RID: 3343 RVA: 0x0000EFD8 File Offset: 0x0000D1D8
			[DebuggerStepThrough]
			public unsafe static v128 bsrli_si128(v128 a, int imm8)
			{
				int dist = Math.Min(imm8 & 255, 16);
				v128 dst = default(v128);
				byte* dptr = &dst.Byte0;
				byte* aptr = &a.Byte0;
				for (int i = 0; i < 16 - dist; i++)
				{
					dptr[i] = aptr[dist + i];
				}
				for (int j = 16 - dist; j < 16; j++)
				{
					dptr[j] = 0;
				}
				return dst;
			}

			// Token: 0x06000D10 RID: 3344 RVA: 0x0000F048 File Offset: 0x0000D248
			[DebuggerStepThrough]
			public unsafe static v128 slli_epi16(v128 a, int imm8)
			{
				v128 dst = default(v128);
				int dist = imm8 & 255;
				ushort* dptr = &dst.UShort0;
				ushort* aptr = &a.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					if (dist > 15)
					{
						dptr[i] = 0;
					}
					else
					{
						dptr[i] = (ushort)(aptr[i] << dist);
					}
				}
				return dst;
			}

			// Token: 0x06000D11 RID: 3345 RVA: 0x0000F0B0 File Offset: 0x0000D2B0
			[DebuggerStepThrough]
			public unsafe static v128 sll_epi16(v128 a, v128 count)
			{
				v128 dst = default(v128);
				int dist = (int)Math.Min(count.ULong0, 16UL);
				ushort* dptr = &dst.UShort0;
				ushort* aptr = &a.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					if (dist > 15)
					{
						dptr[i] = 0;
					}
					else
					{
						dptr[i] = (ushort)(aptr[i] << dist);
					}
				}
				return dst;
			}

			// Token: 0x06000D12 RID: 3346 RVA: 0x0000F120 File Offset: 0x0000D320
			[DebuggerStepThrough]
			public unsafe static v128 slli_epi32(v128 a, int imm8)
			{
				v128 dst = default(v128);
				int dist = Math.Min(imm8 & 255, 32);
				uint* dptr = &dst.UInt0;
				uint* aptr = &a.UInt0;
				for (int i = 0; i <= 3; i++)
				{
					if (dist > 31)
					{
						dptr[i] = 0U;
					}
					else
					{
						dptr[i] = aptr[i] << dist;
					}
				}
				return dst;
			}

			// Token: 0x06000D13 RID: 3347 RVA: 0x0000F18C File Offset: 0x0000D38C
			[DebuggerStepThrough]
			public unsafe static v128 sll_epi32(v128 a, v128 count)
			{
				v128 dst = default(v128);
				int dist = (int)Math.Min(count.ULong0, 32UL);
				uint* dptr = &dst.UInt0;
				uint* aptr = &a.UInt0;
				for (int i = 0; i <= 3; i++)
				{
					if (dist > 31)
					{
						dptr[i] = 0U;
					}
					else
					{
						dptr[i] = aptr[i] << dist;
					}
				}
				return dst;
			}

			// Token: 0x06000D14 RID: 3348 RVA: 0x0000F1FC File Offset: 0x0000D3FC
			[DebuggerStepThrough]
			public unsafe static v128 slli_epi64(v128 a, int imm8)
			{
				v128 dst = default(v128);
				int dist = Math.Min(imm8 & 255, 64);
				ulong* dptr = &dst.ULong0;
				ulong* aptr = &a.ULong0;
				for (int i = 0; i <= 1; i++)
				{
					if (dist > 63)
					{
						dptr[i] = 0UL;
					}
					else
					{
						dptr[i] = aptr[i] << dist;
					}
				}
				return dst;
			}

			// Token: 0x06000D15 RID: 3349 RVA: 0x0000F26C File Offset: 0x0000D46C
			[DebuggerStepThrough]
			public unsafe static v128 sll_epi64(v128 a, v128 count)
			{
				v128 dst = default(v128);
				int dist = (int)Math.Min(count.ULong0, 64UL);
				ulong* dptr = &dst.ULong0;
				ulong* aptr = &a.ULong0;
				for (int i = 0; i <= 1; i++)
				{
					if (dist > 63)
					{
						dptr[i] = 0UL;
					}
					else
					{
						dptr[i] = aptr[i] << dist;
					}
				}
				return dst;
			}

			// Token: 0x06000D16 RID: 3350 RVA: 0x0000F2DC File Offset: 0x0000D4DC
			[DebuggerStepThrough]
			public unsafe static v128 srai_epi16(v128 a, int imm8)
			{
				int dist = Math.Min(imm8 & 255, 16);
				v128 dst = a;
				short* dptr = &dst.SShort0;
				if (dist > 0)
				{
					dist--;
					for (int i = 0; i <= 7; i++)
					{
						short* ptr = dptr + i;
						*ptr = (short)(*ptr >> 1);
						short* ptr2 = dptr + i;
						*ptr2 = (short)(*ptr2 >> dist);
					}
				}
				return dst;
			}

			// Token: 0x06000D17 RID: 3351 RVA: 0x0000F334 File Offset: 0x0000D534
			[DebuggerStepThrough]
			public unsafe static v128 sra_epi16(v128 a, v128 count)
			{
				int dist = (int)Math.Min(count.ULong0, 16UL);
				v128 dst = a;
				short* dptr = &dst.SShort0;
				if (dist > 0)
				{
					dist--;
					for (int i = 0; i <= 7; i++)
					{
						short* ptr = dptr + i;
						*ptr = (short)(*ptr >> 1);
						short* ptr2 = dptr + i;
						*ptr2 = (short)(*ptr2 >> dist);
					}
				}
				return dst;
			}

			// Token: 0x06000D18 RID: 3352 RVA: 0x0000F38C File Offset: 0x0000D58C
			[DebuggerStepThrough]
			public unsafe static v128 srai_epi32(v128 a, int imm8)
			{
				int dist = Math.Min(imm8 & 255, 32);
				v128 dst = a;
				int* dptr = &dst.SInt0;
				if (dist > 0)
				{
					dist--;
					for (int i = 0; i <= 3; i++)
					{
						dptr[i] >>= 1;
						dptr[i] >>= dist;
					}
				}
				return dst;
			}

			// Token: 0x06000D19 RID: 3353 RVA: 0x0000F3E4 File Offset: 0x0000D5E4
			[DebuggerStepThrough]
			public unsafe static v128 sra_epi32(v128 a, v128 count)
			{
				int dist = (int)Math.Min(count.ULong0, 32UL);
				v128 dst = a;
				int* dptr = &dst.SInt0;
				if (dist > 0)
				{
					dist--;
					for (int i = 0; i <= 3; i++)
					{
						dptr[i] >>= 1;
						dptr[i] >>= dist;
					}
				}
				return dst;
			}

			// Token: 0x06000D1A RID: 3354 RVA: 0x0000F43A File Offset: 0x0000D63A
			[DebuggerStepThrough]
			public static v128 srli_si128(v128 a, int imm8)
			{
				return X86.Sse2.bsrli_si128(a, imm8);
			}

			// Token: 0x06000D1B RID: 3355 RVA: 0x0000F444 File Offset: 0x0000D644
			[DebuggerStepThrough]
			public unsafe static v128 srli_epi16(v128 a, int imm8)
			{
				int dist = Math.Min(imm8 & 255, 16);
				v128 dst = a;
				ushort* dptr = &dst.UShort0;
				if (dist > 0)
				{
					dist--;
					for (int i = 0; i <= 7; i++)
					{
						ushort* ptr = dptr + i;
						*ptr = (ushort)(*ptr >> 1);
						ushort* ptr2 = dptr + i;
						*ptr2 = (ushort)(*ptr2 >> dist);
					}
				}
				return dst;
			}

			// Token: 0x06000D1C RID: 3356 RVA: 0x0000F49C File Offset: 0x0000D69C
			[DebuggerStepThrough]
			public unsafe static v128 srl_epi16(v128 a, v128 count)
			{
				int dist = (int)Math.Min(count.ULong0, 16UL);
				v128 dst = a;
				ushort* dptr = &dst.UShort0;
				if (dist > 0)
				{
					dist--;
					for (int i = 0; i <= 7; i++)
					{
						ushort* ptr = dptr + i;
						*ptr = (ushort)(*ptr >> 1);
						ushort* ptr2 = dptr + i;
						*ptr2 = (ushort)(*ptr2 >> dist);
					}
				}
				return dst;
			}

			// Token: 0x06000D1D RID: 3357 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
			[DebuggerStepThrough]
			public unsafe static v128 srli_epi32(v128 a, int imm8)
			{
				int dist = Math.Min(imm8 & 255, 32);
				v128 dst = a;
				uint* dptr = &dst.UInt0;
				if (dist > 0)
				{
					dist--;
					for (int i = 0; i <= 3; i++)
					{
						dptr[i] >>= 1;
						dptr[i] >>= dist;
					}
				}
				return dst;
			}

			// Token: 0x06000D1E RID: 3358 RVA: 0x0000F54C File Offset: 0x0000D74C
			[DebuggerStepThrough]
			public unsafe static v128 srl_epi32(v128 a, v128 count)
			{
				int dist = (int)Math.Min(count.ULong0, 32UL);
				v128 dst = a;
				uint* dptr = &dst.UInt0;
				if (dist > 0)
				{
					dist--;
					for (int i = 0; i <= 3; i++)
					{
						dptr[i] >>= 1;
						dptr[i] >>= dist;
					}
				}
				return dst;
			}

			// Token: 0x06000D1F RID: 3359 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
			[DebuggerStepThrough]
			public unsafe static v128 srli_epi64(v128 a, int imm8)
			{
				int dist = Math.Min(imm8 & 255, 64);
				v128 dst = a;
				ulong* dptr = &dst.ULong0;
				if (dist > 0)
				{
					dist--;
					for (int i = 0; i <= 1; i++)
					{
						dptr[i] >>= 1;
						dptr[i] >>= dist;
					}
				}
				return dst;
			}

			// Token: 0x06000D20 RID: 3360 RVA: 0x0000F5FC File Offset: 0x0000D7FC
			[DebuggerStepThrough]
			public unsafe static v128 srl_epi64(v128 a, v128 count)
			{
				int dist = (int)Math.Min(count.ULong0, 64UL);
				v128 dst = a;
				ulong* dptr = &dst.ULong0;
				if (dist > 0)
				{
					dist--;
					for (int i = 0; i <= 1; i++)
					{
						dptr[i] >>= 1;
						dptr[i] >>= dist;
					}
				}
				return dst;
			}

			// Token: 0x06000D21 RID: 3361 RVA: 0x0000F654 File Offset: 0x0000D854
			[DebuggerStepThrough]
			public static v128 and_si128(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (a.ULong0 & b.ULong0),
					ULong1 = (a.ULong1 & b.ULong1)
				};
			}

			// Token: 0x06000D22 RID: 3362 RVA: 0x0000F694 File Offset: 0x0000D894
			[DebuggerStepThrough]
			public static v128 andnot_si128(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (~a.ULong0 & b.ULong0),
					ULong1 = (~a.ULong1 & b.ULong1)
				};
			}

			// Token: 0x06000D23 RID: 3363 RVA: 0x0000F6D4 File Offset: 0x0000D8D4
			[DebuggerStepThrough]
			public static v128 or_si128(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (a.ULong0 | b.ULong0),
					ULong1 = (a.ULong1 | b.ULong1)
				};
			}

			// Token: 0x06000D24 RID: 3364 RVA: 0x0000F714 File Offset: 0x0000D914
			[DebuggerStepThrough]
			public static v128 xor_si128(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (a.ULong0 ^ b.ULong0),
					ULong1 = (a.ULong1 ^ b.ULong1)
				};
			}

			// Token: 0x06000D25 RID: 3365 RVA: 0x0000F754 File Offset: 0x0000D954
			[DebuggerStepThrough]
			public unsafe static v128 cmpeq_epi8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				byte* aptr = &a.Byte0;
				byte* bptr = &b.Byte0;
				byte* dptr = &dst.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = ((aptr[i] == bptr[i]) ? byte.MaxValue : 0);
				}
				return dst;
			}

			// Token: 0x06000D26 RID: 3366 RVA: 0x0000F7B0 File Offset: 0x0000D9B0
			[DebuggerStepThrough]
			public unsafe static v128 cmpeq_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				ushort* aptr = &a.UShort0;
				ushort* bptr = &b.UShort0;
				ushort* dptr = &dst.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = ((aptr[i] == bptr[i]) ? ushort.MaxValue : 0);
				}
				return dst;
			}

			// Token: 0x06000D27 RID: 3367 RVA: 0x0000F814 File Offset: 0x0000DA14
			[DebuggerStepThrough]
			public unsafe static v128 cmpeq_epi32(v128 a, v128 b)
			{
				v128 dst = default(v128);
				uint* aptr = &a.UInt0;
				uint* bptr = &b.UInt0;
				uint* dptr = &dst.UInt0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = ((aptr[i] == bptr[i]) ? uint.MaxValue : 0U);
				}
				return dst;
			}

			// Token: 0x06000D28 RID: 3368 RVA: 0x0000F874 File Offset: 0x0000DA74
			[DebuggerStepThrough]
			public unsafe static v128 cmpgt_epi8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				sbyte* aptr = &a.SByte0;
				sbyte* bptr = &b.SByte0;
				sbyte* dptr = &dst.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = ((aptr[i] > bptr[i]) ? -1 : 0);
				}
				return dst;
			}

			// Token: 0x06000D29 RID: 3369 RVA: 0x0000F8CC File Offset: 0x0000DACC
			[DebuggerStepThrough]
			public unsafe static v128 cmpgt_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				short* dptr = &dst.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = ((aptr[i] > bptr[i]) ? -1 : 0);
				}
				return dst;
			}

			// Token: 0x06000D2A RID: 3370 RVA: 0x0000F92C File Offset: 0x0000DB2C
			[DebuggerStepThrough]
			public unsafe static v128 cmpgt_epi32(v128 a, v128 b)
			{
				v128 dst = default(v128);
				int* aptr = &a.SInt0;
				int* bptr = &b.SInt0;
				int* dptr = &dst.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = ((aptr[i] > bptr[i]) ? (-1) : 0);
				}
				return dst;
			}

			// Token: 0x06000D2B RID: 3371 RVA: 0x0000F98B File Offset: 0x0000DB8B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmplt_epi8(v128 a, v128 b)
			{
				return X86.Sse2.cmpgt_epi8(b, a);
			}

			// Token: 0x06000D2C RID: 3372 RVA: 0x0000F994 File Offset: 0x0000DB94
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmplt_epi16(v128 a, v128 b)
			{
				return X86.Sse2.cmpgt_epi16(b, a);
			}

			// Token: 0x06000D2D RID: 3373 RVA: 0x0000F99D File Offset: 0x0000DB9D
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmplt_epi32(v128 a, v128 b)
			{
				return X86.Sse2.cmpgt_epi32(b, a);
			}

			// Token: 0x06000D2E RID: 3374 RVA: 0x0000F9A8 File Offset: 0x0000DBA8
			[DebuggerStepThrough]
			public static v128 cvtepi32_pd(v128 a)
			{
				return new v128
				{
					Double0 = (double)a.SInt0,
					Double1 = (double)a.SInt1
				};
			}

			// Token: 0x06000D2F RID: 3375 RVA: 0x0000F9DC File Offset: 0x0000DBDC
			[DebuggerStepThrough]
			public static v128 cvtsi32_sd(v128 a, int b)
			{
				v128 dst = a;
				dst.Double0 = (double)b;
				return dst;
			}

			// Token: 0x06000D30 RID: 3376 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
			[DebuggerStepThrough]
			public static v128 cvtsi64_sd(v128 a, long b)
			{
				v128 dst = a;
				dst.Double0 = (double)b;
				return dst;
			}

			// Token: 0x06000D31 RID: 3377 RVA: 0x0000FA11 File Offset: 0x0000DC11
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cvtsi64x_sd(v128 a, long b)
			{
				return X86.Sse2.cvtsi64_sd(a, b);
			}

			// Token: 0x06000D32 RID: 3378 RVA: 0x0000FA1C File Offset: 0x0000DC1C
			[DebuggerStepThrough]
			public static v128 cvtepi32_ps(v128 a)
			{
				return new v128
				{
					Float0 = (float)a.SInt0,
					Float1 = (float)a.SInt1,
					Float2 = (float)a.SInt2,
					Float3 = (float)a.SInt3
				};
			}

			// Token: 0x06000D33 RID: 3379 RVA: 0x0000FA6C File Offset: 0x0000DC6C
			[DebuggerStepThrough]
			public static v128 cvtsi32_si128(int a)
			{
				return new v128
				{
					SInt0 = a
				};
			}

			// Token: 0x06000D34 RID: 3380 RVA: 0x0000FA8C File Offset: 0x0000DC8C
			[DebuggerStepThrough]
			public static v128 cvtsi64_si128(long a)
			{
				return new v128
				{
					SLong0 = a
				};
			}

			// Token: 0x06000D35 RID: 3381 RVA: 0x0000FAAA File Offset: 0x0000DCAA
			[DebuggerStepThrough]
			public static v128 cvtsi64x_si128(long a)
			{
				return X86.Sse2.cvtsi64_si128(a);
			}

			// Token: 0x06000D36 RID: 3382 RVA: 0x0000FAB2 File Offset: 0x0000DCB2
			[DebuggerStepThrough]
			public static int cvtsi128_si32(v128 a)
			{
				return a.SInt0;
			}

			// Token: 0x06000D37 RID: 3383 RVA: 0x0000FABA File Offset: 0x0000DCBA
			[DebuggerStepThrough]
			public static long cvtsi128_si64(v128 a)
			{
				return a.SLong0;
			}

			// Token: 0x06000D38 RID: 3384 RVA: 0x0000FABA File Offset: 0x0000DCBA
			[DebuggerStepThrough]
			public static long cvtsi128_si64x(v128 a)
			{
				return a.SLong0;
			}

			// Token: 0x06000D39 RID: 3385 RVA: 0x0000FAC4 File Offset: 0x0000DCC4
			[DebuggerStepThrough]
			public static v128 set_epi64x(long e1, long e0)
			{
				return new v128
				{
					SLong0 = e0,
					SLong1 = e1
				};
			}

			// Token: 0x06000D3A RID: 3386 RVA: 0x0000FAEC File Offset: 0x0000DCEC
			[DebuggerStepThrough]
			public static v128 set_epi32(int e3, int e2, int e1, int e0)
			{
				return new v128
				{
					SInt0 = e0,
					SInt1 = e1,
					SInt2 = e2,
					SInt3 = e3
				};
			}

			// Token: 0x06000D3B RID: 3387 RVA: 0x0000FB24 File Offset: 0x0000DD24
			[DebuggerStepThrough]
			public static v128 set_epi16(short e7, short e6, short e5, short e4, short e3, short e2, short e1, short e0)
			{
				return new v128
				{
					SShort0 = e0,
					SShort1 = e1,
					SShort2 = e2,
					SShort3 = e3,
					SShort4 = e4,
					SShort5 = e5,
					SShort6 = e6,
					SShort7 = e7
				};
			}

			// Token: 0x06000D3C RID: 3388 RVA: 0x0000FB80 File Offset: 0x0000DD80
			[DebuggerStepThrough]
			public static v128 set_epi8(sbyte e15_, sbyte e14_, sbyte e13_, sbyte e12_, sbyte e11_, sbyte e10_, sbyte e9_, sbyte e8_, sbyte e7_, sbyte e6_, sbyte e5_, sbyte e4_, sbyte e3_, sbyte e2_, sbyte e1_, sbyte e0_)
			{
				return new v128
				{
					SByte0 = e0_,
					SByte1 = e1_,
					SByte2 = e2_,
					SByte3 = e3_,
					SByte4 = e4_,
					SByte5 = e5_,
					SByte6 = e6_,
					SByte7 = e7_,
					SByte8 = e8_,
					SByte9 = e9_,
					SByte10 = e10_,
					SByte11 = e11_,
					SByte12 = e12_,
					SByte13 = e13_,
					SByte14 = e14_,
					SByte15 = e15_
				};
			}

			// Token: 0x06000D3D RID: 3389 RVA: 0x0000FC24 File Offset: 0x0000DE24
			[DebuggerStepThrough]
			public static v128 set1_epi64x(long a)
			{
				return new v128
				{
					SLong0 = a,
					SLong1 = a
				};
			}

			// Token: 0x06000D3E RID: 3390 RVA: 0x0000FC4C File Offset: 0x0000DE4C
			[DebuggerStepThrough]
			public static v128 set1_epi32(int a)
			{
				return new v128
				{
					SInt0 = a,
					SInt1 = a,
					SInt2 = a,
					SInt3 = a
				};
			}

			// Token: 0x06000D3F RID: 3391 RVA: 0x0000FC84 File Offset: 0x0000DE84
			[DebuggerStepThrough]
			public unsafe static v128 set1_epi16(short a)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = a;
				}
				return dst;
			}

			// Token: 0x06000D40 RID: 3392 RVA: 0x0000FCB8 File Offset: 0x0000DEB8
			[DebuggerStepThrough]
			public unsafe static v128 set1_epi8(sbyte a)
			{
				v128 dst = default(v128);
				sbyte* dptr = &dst.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = a;
				}
				return dst;
			}

			// Token: 0x06000D41 RID: 3393 RVA: 0x0000FCEC File Offset: 0x0000DEEC
			[DebuggerStepThrough]
			public static v128 setr_epi32(int e3, int e2, int e1, int e0)
			{
				return new v128
				{
					SInt0 = e3,
					SInt1 = e2,
					SInt2 = e1,
					SInt3 = e0
				};
			}

			// Token: 0x06000D42 RID: 3394 RVA: 0x0000FD24 File Offset: 0x0000DF24
			[DebuggerStepThrough]
			public static v128 setr_epi16(short e7, short e6, short e5, short e4, short e3, short e2, short e1, short e0)
			{
				return new v128
				{
					SShort0 = e7,
					SShort1 = e6,
					SShort2 = e5,
					SShort3 = e4,
					SShort4 = e3,
					SShort5 = e2,
					SShort6 = e1,
					SShort7 = e0
				};
			}

			// Token: 0x06000D43 RID: 3395 RVA: 0x0000FD80 File Offset: 0x0000DF80
			[DebuggerStepThrough]
			public static v128 setr_epi8(sbyte e15_, sbyte e14_, sbyte e13_, sbyte e12_, sbyte e11_, sbyte e10_, sbyte e9_, sbyte e8_, sbyte e7_, sbyte e6_, sbyte e5_, sbyte e4_, sbyte e3_, sbyte e2_, sbyte e1_, sbyte e0_)
			{
				return new v128
				{
					SByte0 = e15_,
					SByte1 = e14_,
					SByte2 = e13_,
					SByte3 = e12_,
					SByte4 = e11_,
					SByte5 = e10_,
					SByte6 = e9_,
					SByte7 = e8_,
					SByte8 = e7_,
					SByte9 = e6_,
					SByte10 = e5_,
					SByte11 = e4_,
					SByte12 = e3_,
					SByte13 = e2_,
					SByte14 = e1_,
					SByte15 = e0_
				};
			}

			// Token: 0x06000D44 RID: 3396 RVA: 0x0000FE24 File Offset: 0x0000E024
			[DebuggerStepThrough]
			public static v128 setzero_si128()
			{
				return default(v128);
			}

			// Token: 0x06000D45 RID: 3397 RVA: 0x0000FE3C File Offset: 0x0000E03C
			[DebuggerStepThrough]
			public static v128 move_epi64(v128 a)
			{
				return new v128
				{
					ULong0 = a.ULong0,
					ULong1 = 0UL
				};
			}

			// Token: 0x06000D46 RID: 3398 RVA: 0x0000FE68 File Offset: 0x0000E068
			[DebuggerStepThrough]
			public unsafe static v128 packs_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				sbyte* dptr = &dst.SByte0;
				for (int i = 0; i < 8; i++)
				{
					dptr[i] = X86.Saturate_To_Int8((int)aptr[i]);
				}
				for (int j = 0; j < 8; j++)
				{
					dptr[j + 8] = X86.Saturate_To_Int8((int)bptr[j]);
				}
				return dst;
			}

			// Token: 0x06000D47 RID: 3399 RVA: 0x0000FEE0 File Offset: 0x0000E0E0
			[DebuggerStepThrough]
			public unsafe static v128 packs_epi32(v128 a, v128 b)
			{
				v128 dst = default(v128);
				int* aptr = &a.SInt0;
				int* bptr = &b.SInt0;
				short* dptr = &dst.SShort0;
				for (int i = 0; i < 4; i++)
				{
					dptr[i] = X86.Saturate_To_Int16(aptr[i]);
				}
				for (int j = 0; j < 4; j++)
				{
					dptr[j + 4] = X86.Saturate_To_Int16(bptr[j]);
				}
				return dst;
			}

			// Token: 0x06000D48 RID: 3400 RVA: 0x0000FF60 File Offset: 0x0000E160
			[DebuggerStepThrough]
			public unsafe static v128 packus_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				byte* dptr = &dst.Byte0;
				for (int i = 0; i < 8; i++)
				{
					dptr[i] = X86.Saturate_To_UnsignedInt8((int)aptr[i]);
				}
				for (int j = 0; j < 8; j++)
				{
					dptr[j + 8] = X86.Saturate_To_UnsignedInt8((int)bptr[j]);
				}
				return dst;
			}

			// Token: 0x06000D49 RID: 3401 RVA: 0x0000FFD7 File Offset: 0x0000E1D7
			[DebuggerStepThrough]
			public unsafe static ushort extract_epi16(v128 a, int imm8)
			{
				return (&a.UShort0)[imm8 & 7];
			}

			// Token: 0x06000D4A RID: 3402 RVA: 0x0000FFEC File Offset: 0x0000E1EC
			[DebuggerStepThrough]
			public unsafe static v128 insert_epi16(v128 a, int i, int imm8)
			{
				v128 dst = a;
				(&dst.SShort0)[imm8 & 7] = (short)i;
				return dst;
			}

			// Token: 0x06000D4B RID: 3403 RVA: 0x00010010 File Offset: 0x0000E210
			[DebuggerStepThrough]
			public unsafe static int movemask_epi8(v128 a)
			{
				int dst = 0;
				byte* aptr = &a.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					if ((aptr[i] & 128) != 0)
					{
						dst |= 1 << i;
					}
				}
				return dst;
			}

			// Token: 0x06000D4C RID: 3404 RVA: 0x0001004C File Offset: 0x0000E24C
			[DebuggerStepThrough]
			public unsafe static v128 shuffle_epi32(v128 a, int imm8)
			{
				v128 dst = default(v128);
				uint* ptr = &dst.UInt0;
				uint* aptr = &a.UInt0;
				*ptr = aptr[imm8 & 3];
				ptr[1] = aptr[(imm8 >> 2) & 3];
				ptr[2] = aptr[(imm8 >> 4) & 3];
				ptr[3] = aptr[(imm8 >> 6) & 3];
				return dst;
			}

			// Token: 0x06000D4D RID: 3405 RVA: 0x000100B0 File Offset: 0x0000E2B0
			[DebuggerStepThrough]
			public unsafe static v128 shufflehi_epi16(v128 a, int imm8)
			{
				v128 dst = a;
				short* ptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				ptr[4] = aptr[4 + (imm8 & 3)];
				ptr[5] = aptr[4 + ((imm8 >> 2) & 3)];
				ptr[6] = aptr[4 + ((imm8 >> 4) & 3)];
				ptr[7] = aptr[4 + ((imm8 >> 6) & 3)];
				return dst;
			}

			// Token: 0x06000D4E RID: 3406 RVA: 0x00010120 File Offset: 0x0000E320
			[DebuggerStepThrough]
			public unsafe static v128 shufflelo_epi16(v128 a, int imm8)
			{
				v128 dst = a;
				short* ptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				*ptr = aptr[imm8 & 3];
				ptr[1] = aptr[(imm8 >> 2) & 3];
				ptr[2] = aptr[(imm8 >> 4) & 3];
				ptr[3] = aptr[(imm8 >> 6) & 3];
				return dst;
			}

			// Token: 0x06000D4F RID: 3407 RVA: 0x00010180 File Offset: 0x0000E380
			[DebuggerStepThrough]
			public unsafe static v128 unpackhi_epi8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				byte* dptr = &dst.Byte0;
				byte* aptr = &a.Byte0;
				byte* bptr = &b.Byte0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[2 * i] = aptr[i + 8];
					dptr[2 * i + 1] = bptr[i + 8];
				}
				return dst;
			}

			// Token: 0x06000D50 RID: 3408 RVA: 0x000101E0 File Offset: 0x0000E3E0
			[DebuggerStepThrough]
			public unsafe static v128 unpackhi_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				ushort* dptr = &dst.UShort0;
				ushort* aptr = &a.UShort0;
				ushort* bptr = &b.UShort0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[2 * i] = aptr[i + 4];
					dptr[2 * i + 1] = bptr[i + 4];
				}
				return dst;
			}

			// Token: 0x06000D51 RID: 3409 RVA: 0x0001024C File Offset: 0x0000E44C
			[DebuggerStepThrough]
			public static v128 unpackhi_epi32(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = a.UInt2,
					UInt1 = b.UInt2,
					UInt2 = a.UInt3,
					UInt3 = b.UInt3
				};
			}

			// Token: 0x06000D52 RID: 3410 RVA: 0x00010298 File Offset: 0x0000E498
			[DebuggerStepThrough]
			public static v128 unpackhi_epi64(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = a.ULong1,
					ULong1 = b.ULong1
				};
			}

			// Token: 0x06000D53 RID: 3411 RVA: 0x000102C8 File Offset: 0x0000E4C8
			[DebuggerStepThrough]
			public unsafe static v128 unpacklo_epi8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				byte* dptr = &dst.Byte0;
				byte* aptr = &a.Byte0;
				byte* bptr = &b.Byte0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[2 * i] = aptr[i];
					dptr[2 * i + 1] = bptr[i];
				}
				return dst;
			}

			// Token: 0x06000D54 RID: 3412 RVA: 0x00010324 File Offset: 0x0000E524
			[DebuggerStepThrough]
			public unsafe static v128 unpacklo_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				ushort* dptr = &dst.UShort0;
				ushort* aptr = &a.UShort0;
				ushort* bptr = &b.UShort0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[2 * i] = aptr[i];
					dptr[2 * i + 1] = bptr[i];
				}
				return dst;
			}

			// Token: 0x06000D55 RID: 3413 RVA: 0x0001038C File Offset: 0x0000E58C
			[DebuggerStepThrough]
			public static v128 unpacklo_epi32(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = a.UInt0,
					UInt1 = b.UInt0,
					UInt2 = a.UInt1,
					UInt3 = b.UInt1
				};
			}

			// Token: 0x06000D56 RID: 3414 RVA: 0x000103D8 File Offset: 0x0000E5D8
			[DebuggerStepThrough]
			public static v128 unpacklo_epi64(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = a.ULong0,
					ULong1 = b.ULong0
				};
			}

			// Token: 0x06000D57 RID: 3415 RVA: 0x00010408 File Offset: 0x0000E608
			[DebuggerStepThrough]
			public static v128 add_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 + b.Double0,
					Double1 = a.Double1
				};
			}

			// Token: 0x06000D58 RID: 3416 RVA: 0x00010440 File Offset: 0x0000E640
			[DebuggerStepThrough]
			public static v128 add_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 + b.Double0,
					Double1 = a.Double1 + b.Double1
				};
			}

			// Token: 0x06000D59 RID: 3417 RVA: 0x00010480 File Offset: 0x0000E680
			[DebuggerStepThrough]
			public static v128 div_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 / b.Double0,
					Double1 = a.Double1
				};
			}

			// Token: 0x06000D5A RID: 3418 RVA: 0x000104B8 File Offset: 0x0000E6B8
			[DebuggerStepThrough]
			public static v128 div_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 / b.Double0,
					Double1 = a.Double1 / b.Double1
				};
			}

			// Token: 0x06000D5B RID: 3419 RVA: 0x000104F8 File Offset: 0x0000E6F8
			[DebuggerStepThrough]
			public static v128 max_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = Math.Max(a.Double0, b.Double0),
					Double1 = a.Double1
				};
			}

			// Token: 0x06000D5C RID: 3420 RVA: 0x00010534 File Offset: 0x0000E734
			[DebuggerStepThrough]
			public static v128 max_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = Math.Max(a.Double0, b.Double0),
					Double1 = Math.Max(a.Double1, b.Double1)
				};
			}

			// Token: 0x06000D5D RID: 3421 RVA: 0x0001057C File Offset: 0x0000E77C
			[DebuggerStepThrough]
			public static v128 min_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = Math.Min(a.Double0, b.Double0),
					Double1 = a.Double1
				};
			}

			// Token: 0x06000D5E RID: 3422 RVA: 0x000105B8 File Offset: 0x0000E7B8
			[DebuggerStepThrough]
			public static v128 min_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = Math.Min(a.Double0, b.Double0),
					Double1 = Math.Min(a.Double1, b.Double1)
				};
			}

			// Token: 0x06000D5F RID: 3423 RVA: 0x00010600 File Offset: 0x0000E800
			[DebuggerStepThrough]
			public static v128 mul_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 * b.Double0,
					Double1 = a.Double1
				};
			}

			// Token: 0x06000D60 RID: 3424 RVA: 0x00010638 File Offset: 0x0000E838
			[DebuggerStepThrough]
			public static v128 mul_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 * b.Double0,
					Double1 = a.Double1 * b.Double1
				};
			}

			// Token: 0x06000D61 RID: 3425 RVA: 0x00010678 File Offset: 0x0000E878
			[DebuggerStepThrough]
			public static v128 sqrt_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = Math.Sqrt(b.Double0),
					Double1 = a.Double1
				};
			}

			// Token: 0x06000D62 RID: 3426 RVA: 0x000106B0 File Offset: 0x0000E8B0
			[DebuggerStepThrough]
			public static v128 sqrt_pd(v128 a)
			{
				return new v128
				{
					Double0 = Math.Sqrt(a.Double0),
					Double1 = Math.Sqrt(a.Double1)
				};
			}

			// Token: 0x06000D63 RID: 3427 RVA: 0x000106EC File Offset: 0x0000E8EC
			[DebuggerStepThrough]
			public static v128 sub_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 - b.Double0,
					Double1 = a.Double1
				};
			}

			// Token: 0x06000D64 RID: 3428 RVA: 0x00010724 File Offset: 0x0000E924
			[DebuggerStepThrough]
			public static v128 sub_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 - b.Double0,
					Double1 = a.Double1 - b.Double1
				};
			}

			// Token: 0x06000D65 RID: 3429 RVA: 0x00010764 File Offset: 0x0000E964
			[DebuggerStepThrough]
			public static v128 and_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (a.ULong0 & b.ULong0),
					ULong1 = (a.ULong1 & b.ULong1)
				};
			}

			// Token: 0x06000D66 RID: 3430 RVA: 0x000107A4 File Offset: 0x0000E9A4
			[DebuggerStepThrough]
			public static v128 andnot_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (~a.ULong0 & b.ULong0),
					ULong1 = (~a.ULong1 & b.ULong1)
				};
			}

			// Token: 0x06000D67 RID: 3431 RVA: 0x000107E4 File Offset: 0x0000E9E4
			[DebuggerStepThrough]
			public static v128 or_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (a.ULong0 | b.ULong0),
					ULong1 = (a.ULong1 | b.ULong1)
				};
			}

			// Token: 0x06000D68 RID: 3432 RVA: 0x00010824 File Offset: 0x0000EA24
			[DebuggerStepThrough]
			public static v128 xor_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (a.ULong0 ^ b.ULong0),
					ULong1 = (a.ULong1 ^ b.ULong1)
				};
			}

			// Token: 0x06000D69 RID: 3433 RVA: 0x00010864 File Offset: 0x0000EA64
			[DebuggerStepThrough]
			public static v128 cmpeq_sd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 == b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = a.ULong1
				};
			}

			// Token: 0x06000D6A RID: 3434 RVA: 0x000108A4 File Offset: 0x0000EAA4
			[DebuggerStepThrough]
			public static v128 cmplt_sd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 < b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = a.ULong1
				};
			}

			// Token: 0x06000D6B RID: 3435 RVA: 0x000108E4 File Offset: 0x0000EAE4
			[DebuggerStepThrough]
			public static v128 cmple_sd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 <= b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = a.ULong1
				};
			}

			// Token: 0x06000D6C RID: 3436 RVA: 0x00010922 File Offset: 0x0000EB22
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpgt_sd(v128 a, v128 b)
			{
				return X86.Sse2.cmple_sd(b, a);
			}

			// Token: 0x06000D6D RID: 3437 RVA: 0x0001092B File Offset: 0x0000EB2B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpge_sd(v128 a, v128 b)
			{
				return X86.Sse2.cmplt_sd(b, a);
			}

			// Token: 0x06000D6E RID: 3438 RVA: 0x00010934 File Offset: 0x0000EB34
			[DebuggerStepThrough]
			public static v128 cmpord_sd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((X86.IsNaN(a.ULong0) || X86.IsNaN(b.ULong0)) ? 0UL : ulong.MaxValue),
					ULong1 = a.ULong1
				};
			}

			// Token: 0x06000D6F RID: 3439 RVA: 0x00010980 File Offset: 0x0000EB80
			[DebuggerStepThrough]
			public static v128 cmpunord_sd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((X86.IsNaN(a.ULong0) || X86.IsNaN(b.ULong0)) ? ulong.MaxValue : 0UL),
					ULong1 = a.ULong1
				};
			}

			// Token: 0x06000D70 RID: 3440 RVA: 0x000109CC File Offset: 0x0000EBCC
			[DebuggerStepThrough]
			public static v128 cmpneq_sd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 != b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = a.ULong1
				};
			}

			// Token: 0x06000D71 RID: 3441 RVA: 0x00010A0C File Offset: 0x0000EC0C
			[DebuggerStepThrough]
			public static v128 cmpnlt_sd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 >= b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = a.ULong1
				};
			}

			// Token: 0x06000D72 RID: 3442 RVA: 0x00010A4C File Offset: 0x0000EC4C
			[DebuggerStepThrough]
			public static v128 cmpnle_sd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 > b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = a.ULong1
				};
			}

			// Token: 0x06000D73 RID: 3443 RVA: 0x00010A8A File Offset: 0x0000EC8A
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpngt_sd(v128 a, v128 b)
			{
				return X86.Sse2.cmpnlt_sd(b, a);
			}

			// Token: 0x06000D74 RID: 3444 RVA: 0x00010A93 File Offset: 0x0000EC93
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpnge_sd(v128 a, v128 b)
			{
				return X86.Sse2.cmpnle_sd(b, a);
			}

			// Token: 0x06000D75 RID: 3445 RVA: 0x00010A9C File Offset: 0x0000EC9C
			[DebuggerStepThrough]
			public static v128 cmpeq_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 == b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 == b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D76 RID: 3446 RVA: 0x00010AE8 File Offset: 0x0000ECE8
			[DebuggerStepThrough]
			public static v128 cmplt_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 < b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 < b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D77 RID: 3447 RVA: 0x00010B34 File Offset: 0x0000ED34
			[DebuggerStepThrough]
			public static v128 cmple_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 <= b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 <= b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D78 RID: 3448 RVA: 0x00010B80 File Offset: 0x0000ED80
			[DebuggerStepThrough]
			public static v128 cmpgt_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 > b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 > b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D79 RID: 3449 RVA: 0x00010BCC File Offset: 0x0000EDCC
			[DebuggerStepThrough]
			public static v128 cmpge_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 >= b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 >= b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D7A RID: 3450 RVA: 0x00010C18 File Offset: 0x0000EE18
			[DebuggerStepThrough]
			public static v128 cmpord_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((X86.IsNaN(a.ULong0) || X86.IsNaN(b.ULong0)) ? 0UL : ulong.MaxValue),
					ULong1 = ((X86.IsNaN(a.ULong1) || X86.IsNaN(b.ULong1)) ? 0UL : ulong.MaxValue)
				};
			}

			// Token: 0x06000D7B RID: 3451 RVA: 0x00010C7C File Offset: 0x0000EE7C
			[DebuggerStepThrough]
			public static v128 cmpunord_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((X86.IsNaN(a.ULong0) || X86.IsNaN(b.ULong0)) ? ulong.MaxValue : 0UL),
					ULong1 = ((X86.IsNaN(a.ULong1) || X86.IsNaN(b.ULong1)) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D7C RID: 3452 RVA: 0x00010CE0 File Offset: 0x0000EEE0
			[DebuggerStepThrough]
			public static v128 cmpneq_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 != b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 != b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D7D RID: 3453 RVA: 0x00010D2C File Offset: 0x0000EF2C
			[DebuggerStepThrough]
			public static v128 cmpnlt_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 >= b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 >= b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D7E RID: 3454 RVA: 0x00010D78 File Offset: 0x0000EF78
			[DebuggerStepThrough]
			public static v128 cmpnle_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 > b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 > b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D7F RID: 3455 RVA: 0x00010DC4 File Offset: 0x0000EFC4
			[DebuggerStepThrough]
			public static v128 cmpngt_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 <= b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 <= b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D80 RID: 3456 RVA: 0x00010E10 File Offset: 0x0000F010
			[DebuggerStepThrough]
			public static v128 cmpnge_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 < b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 < b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D81 RID: 3457 RVA: 0x00010E5C File Offset: 0x0000F05C
			[DebuggerStepThrough]
			public static int comieq_sd(v128 a, v128 b)
			{
				if (a.Double0 != b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D82 RID: 3458 RVA: 0x00010E6F File Offset: 0x0000F06F
			[DebuggerStepThrough]
			public static int comilt_sd(v128 a, v128 b)
			{
				if (a.Double0 >= b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D83 RID: 3459 RVA: 0x00010E82 File Offset: 0x0000F082
			[DebuggerStepThrough]
			public static int comile_sd(v128 a, v128 b)
			{
				if (a.Double0 > b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D84 RID: 3460 RVA: 0x00010E95 File Offset: 0x0000F095
			[DebuggerStepThrough]
			public static int comigt_sd(v128 a, v128 b)
			{
				if (a.Double0 <= b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D85 RID: 3461 RVA: 0x00010EA8 File Offset: 0x0000F0A8
			[DebuggerStepThrough]
			public static int comige_sd(v128 a, v128 b)
			{
				if (a.Double0 < b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D86 RID: 3462 RVA: 0x00010EBB File Offset: 0x0000F0BB
			[DebuggerStepThrough]
			public static int comineq_sd(v128 a, v128 b)
			{
				if (a.Double0 == b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D87 RID: 3463 RVA: 0x00010E5C File Offset: 0x0000F05C
			[DebuggerStepThrough]
			public static int ucomieq_sd(v128 a, v128 b)
			{
				if (a.Double0 != b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D88 RID: 3464 RVA: 0x00010E6F File Offset: 0x0000F06F
			[DebuggerStepThrough]
			public static int ucomilt_sd(v128 a, v128 b)
			{
				if (a.Double0 >= b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D89 RID: 3465 RVA: 0x00010E82 File Offset: 0x0000F082
			[DebuggerStepThrough]
			public static int ucomile_sd(v128 a, v128 b)
			{
				if (a.Double0 > b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D8A RID: 3466 RVA: 0x00010E95 File Offset: 0x0000F095
			[DebuggerStepThrough]
			public static int ucomigt_sd(v128 a, v128 b)
			{
				if (a.Double0 <= b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D8B RID: 3467 RVA: 0x00010EA8 File Offset: 0x0000F0A8
			[DebuggerStepThrough]
			public static int ucomige_sd(v128 a, v128 b)
			{
				if (a.Double0 < b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D8C RID: 3468 RVA: 0x00010EBB File Offset: 0x0000F0BB
			[DebuggerStepThrough]
			public static int ucomineq_sd(v128 a, v128 b)
			{
				if (a.Double0 == b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D8D RID: 3469 RVA: 0x00010ED0 File Offset: 0x0000F0D0
			[DebuggerStepThrough]
			public static v128 cvtpd_ps(v128 a)
			{
				return new v128
				{
					Float0 = (float)a.Double0,
					Float1 = (float)a.Double1,
					Float2 = 0f,
					Float3 = 0f
				};
			}

			// Token: 0x06000D8E RID: 3470 RVA: 0x00010F1C File Offset: 0x0000F11C
			[DebuggerStepThrough]
			public static v128 cvtps_pd(v128 a)
			{
				return new v128
				{
					Double0 = (double)a.Float0,
					Double1 = (double)a.Float1
				};
			}

			// Token: 0x06000D8F RID: 3471 RVA: 0x00010F50 File Offset: 0x0000F150
			[DebuggerStepThrough]
			public static v128 cvtpd_epi32(v128 a)
			{
				return new v128
				{
					SInt0 = (int)Math.Round(a.Double0),
					SInt1 = (int)Math.Round(a.Double1)
				};
			}

			// Token: 0x06000D90 RID: 3472 RVA: 0x00010F8C File Offset: 0x0000F18C
			[DebuggerStepThrough]
			public static int cvtsd_si32(v128 a)
			{
				return (int)Math.Round(a.Double0);
			}

			// Token: 0x06000D91 RID: 3473 RVA: 0x00010F9A File Offset: 0x0000F19A
			[DebuggerStepThrough]
			public static long cvtsd_si64(v128 a)
			{
				return (long)Math.Round(a.Double0);
			}

			// Token: 0x06000D92 RID: 3474 RVA: 0x00010FA8 File Offset: 0x0000F1A8
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static long cvtsd_si64x(v128 a)
			{
				return X86.Sse2.cvtsd_si64(a);
			}

			// Token: 0x06000D93 RID: 3475 RVA: 0x00010FB0 File Offset: 0x0000F1B0
			[DebuggerStepThrough]
			public static v128 cvtsd_ss(v128 a, v128 b)
			{
				v128 dst = a;
				dst.Float0 = (float)b.Double0;
				return dst;
			}

			// Token: 0x06000D94 RID: 3476 RVA: 0x00010FCE File Offset: 0x0000F1CE
			[DebuggerStepThrough]
			public static double cvtsd_f64(v128 a)
			{
				return a.Double0;
			}

			// Token: 0x06000D95 RID: 3477 RVA: 0x00010FD8 File Offset: 0x0000F1D8
			[DebuggerStepThrough]
			public static v128 cvtss_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = (double)b.Float0,
					Double1 = (double)a.Float0
				};
			}

			// Token: 0x06000D96 RID: 3478 RVA: 0x0001100C File Offset: 0x0000F20C
			[DebuggerStepThrough]
			public static v128 cvttpd_epi32(v128 a)
			{
				return new v128
				{
					SInt0 = (int)a.Double0,
					SInt1 = (int)a.Double1
				};
			}

			// Token: 0x06000D97 RID: 3479 RVA: 0x0001103E File Offset: 0x0000F23E
			[DebuggerStepThrough]
			public static int cvttsd_si32(v128 a)
			{
				return (int)a.Double0;
			}

			// Token: 0x06000D98 RID: 3480 RVA: 0x00011047 File Offset: 0x0000F247
			[DebuggerStepThrough]
			public static long cvttsd_si64(v128 a)
			{
				return (long)a.Double0;
			}

			// Token: 0x06000D99 RID: 3481 RVA: 0x00011050 File Offset: 0x0000F250
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static long cvttsd_si64x(v128 a)
			{
				return X86.Sse2.cvttsd_si64(a);
			}

			// Token: 0x06000D9A RID: 3482 RVA: 0x00011058 File Offset: 0x0000F258
			[DebuggerStepThrough]
			public static v128 cvtps_epi32(v128 a)
			{
				return new v128
				{
					SInt0 = (int)Math.Round((double)a.Float0),
					SInt1 = (int)Math.Round((double)a.Float1),
					SInt2 = (int)Math.Round((double)a.Float2),
					SInt3 = (int)Math.Round((double)a.Float3)
				};
			}

			// Token: 0x06000D9B RID: 3483 RVA: 0x000110C0 File Offset: 0x0000F2C0
			[DebuggerStepThrough]
			public static v128 cvttps_epi32(v128 a)
			{
				return new v128
				{
					SInt0 = (int)a.Float0,
					SInt1 = (int)a.Float1,
					SInt2 = (int)a.Float2,
					SInt3 = (int)a.Float3
				};
			}

			// Token: 0x06000D9C RID: 3484 RVA: 0x00011110 File Offset: 0x0000F310
			[DebuggerStepThrough]
			public static v128 set_sd(double a)
			{
				return new v128
				{
					Double0 = a,
					Double1 = 0.0
				};
			}

			// Token: 0x06000D9D RID: 3485 RVA: 0x00011140 File Offset: 0x0000F340
			[DebuggerStepThrough]
			public static v128 set1_pd(double a)
			{
				return new v128
				{
					Double1 = a,
					Double0 = a
				};
			}

			// Token: 0x06000D9E RID: 3486 RVA: 0x00011168 File Offset: 0x0000F368
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 set_pd1(double a)
			{
				return X86.Sse2.set1_pd(a);
			}

			// Token: 0x06000D9F RID: 3487 RVA: 0x00011170 File Offset: 0x0000F370
			[DebuggerStepThrough]
			public static v128 set_pd(double e1, double e0)
			{
				return new v128
				{
					Double0 = e0,
					Double1 = e1
				};
			}

			// Token: 0x06000DA0 RID: 3488 RVA: 0x00011198 File Offset: 0x0000F398
			[DebuggerStepThrough]
			public static v128 setr_pd(double e1, double e0)
			{
				return new v128
				{
					Double0 = e1,
					Double1 = e0
				};
			}

			// Token: 0x06000DA1 RID: 3489 RVA: 0x000111C0 File Offset: 0x0000F3C0
			[DebuggerStepThrough]
			public static v128 unpackhi_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double1,
					Double1 = b.Double1
				};
			}

			// Token: 0x06000DA2 RID: 3490 RVA: 0x000111F0 File Offset: 0x0000F3F0
			[DebuggerStepThrough]
			public static v128 unpacklo_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0,
					Double1 = b.Double0
				};
			}

			// Token: 0x06000DA3 RID: 3491 RVA: 0x00011220 File Offset: 0x0000F420
			[DebuggerStepThrough]
			public static int movemask_pd(v128 a)
			{
				int dst = 0;
				if ((a.ULong0 & 9223372036854775808UL) != 0UL)
				{
					dst |= 1;
				}
				if ((a.ULong1 & 9223372036854775808UL) != 0UL)
				{
					dst |= 2;
				}
				return dst;
			}

			// Token: 0x06000DA4 RID: 3492 RVA: 0x0001125C File Offset: 0x0000F45C
			[DebuggerStepThrough]
			public unsafe static v128 shuffle_pd(v128 a, v128 b, int imm8)
			{
				v128 dst = default(v128);
				double* aptr = &a.Double0;
				double* bptr = &b.Double0;
				dst.Double0 = aptr[imm8 & 1];
				dst.Double1 = bptr[(imm8 >> 1) & 1];
				return dst;
			}

			// Token: 0x06000DA5 RID: 3493 RVA: 0x000112A8 File Offset: 0x0000F4A8
			[DebuggerStepThrough]
			public static v128 move_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = b.Double0,
					Double1 = a.Double1
				};
			}

			// Token: 0x06000DA6 RID: 3494 RVA: 0x000112D8 File Offset: 0x0000F4D8
			public unsafe static v128 loadu_si32(void* mem_addr)
			{
				return new v128(*(int*)mem_addr, 0, 0, 0);
			}

			// Token: 0x06000DA7 RID: 3495 RVA: 0x000112E4 File Offset: 0x0000F4E4
			public unsafe static void storeu_si32(void* mem_addr, v128 a)
			{
				*(int*)mem_addr = a.SInt0;
			}

			// Token: 0x06000DA8 RID: 3496 RVA: 0x0000D393 File Offset: 0x0000B593
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static v128 load_si128(void* ptr)
			{
				return X86.GenericCSharpLoad(ptr);
			}

			// Token: 0x06000DA9 RID: 3497 RVA: 0x0000D393 File Offset: 0x0000B593
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static v128 loadu_si128(void* ptr)
			{
				return X86.GenericCSharpLoad(ptr);
			}

			// Token: 0x06000DAA RID: 3498 RVA: 0x0000D39B File Offset: 0x0000B59B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static void store_si128(void* ptr, v128 val)
			{
				X86.GenericCSharpStore(ptr, val);
			}

			// Token: 0x06000DAB RID: 3499 RVA: 0x0000D39B File Offset: 0x0000B59B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static void storeu_si128(void* ptr, v128 val)
			{
				X86.GenericCSharpStore(ptr, val);
			}

			// Token: 0x06000DAC RID: 3500 RVA: 0x000024D5 File Offset: 0x000006D5
			[DebuggerStepThrough]
			public unsafe static void clflush(void* ptr)
			{
			}
		}

		// Token: 0x02000048 RID: 72
		public static class Sse3
		{
			// Token: 0x1700004A RID: 74
			// (get) Token: 0x06000DAD RID: 3501 RVA: 0x000024DA File Offset: 0x000006DA
			public static bool IsSse3Supported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000DAE RID: 3502 RVA: 0x000112F0 File Offset: 0x0000F4F0
			[DebuggerStepThrough]
			public static v128 addsub_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = a.Float0 - b.Float0,
					Float1 = a.Float1 + b.Float1,
					Float2 = a.Float2 - b.Float2,
					Float3 = a.Float3 + b.Float3
				};
			}

			// Token: 0x06000DAF RID: 3503 RVA: 0x00011358 File Offset: 0x0000F558
			[DebuggerStepThrough]
			public static v128 addsub_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 - b.Double0,
					Double1 = a.Double1 + b.Double1
				};
			}

			// Token: 0x06000DB0 RID: 3504 RVA: 0x00011398 File Offset: 0x0000F598
			[DebuggerStepThrough]
			public static v128 hadd_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 + a.Double1,
					Double1 = b.Double0 + b.Double1
				};
			}

			// Token: 0x06000DB1 RID: 3505 RVA: 0x000113D8 File Offset: 0x0000F5D8
			[DebuggerStepThrough]
			public static v128 hadd_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = a.Float0 + a.Float1,
					Float1 = a.Float2 + a.Float3,
					Float2 = b.Float0 + b.Float1,
					Float3 = b.Float2 + b.Float3
				};
			}

			// Token: 0x06000DB2 RID: 3506 RVA: 0x00011440 File Offset: 0x0000F640
			[DebuggerStepThrough]
			public static v128 hsub_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 - a.Double1,
					Double1 = b.Double0 - b.Double1
				};
			}

			// Token: 0x06000DB3 RID: 3507 RVA: 0x00011480 File Offset: 0x0000F680
			[DebuggerStepThrough]
			public static v128 hsub_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = a.Float0 - a.Float1,
					Float1 = a.Float2 - a.Float3,
					Float2 = b.Float0 - b.Float1,
					Float3 = b.Float2 - b.Float3
				};
			}

			// Token: 0x06000DB4 RID: 3508 RVA: 0x000114E8 File Offset: 0x0000F6E8
			[DebuggerStepThrough]
			public static v128 movedup_pd(v128 a)
			{
				return new v128
				{
					Double0 = a.Double0,
					Double1 = a.Double0
				};
			}

			// Token: 0x06000DB5 RID: 3509 RVA: 0x00011518 File Offset: 0x0000F718
			[DebuggerStepThrough]
			public static v128 movehdup_ps(v128 a)
			{
				return new v128
				{
					Float0 = a.Float1,
					Float1 = a.Float1,
					Float2 = a.Float3,
					Float3 = a.Float3
				};
			}

			// Token: 0x06000DB6 RID: 3510 RVA: 0x00011564 File Offset: 0x0000F764
			[DebuggerStepThrough]
			public static v128 moveldup_ps(v128 a)
			{
				return new v128
				{
					Float0 = a.Float0,
					Float1 = a.Float0,
					Float2 = a.Float2,
					Float3 = a.Float2
				};
			}
		}

		// Token: 0x02000049 RID: 73
		public static class Sse4_1
		{
			// Token: 0x1700004B RID: 75
			// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x000024DA File Offset: 0x000006DA
			public static bool IsSse41Supported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000DB8 RID: 3512 RVA: 0x0000D393 File Offset: 0x0000B593
			[DebuggerStepThrough]
			public unsafe static v128 stream_load_si128(void* mem_addr)
			{
				return X86.GenericCSharpLoad(mem_addr);
			}

			// Token: 0x06000DB9 RID: 3513 RVA: 0x000115B0 File Offset: 0x0000F7B0
			[DebuggerStepThrough]
			public unsafe static v128 blend_pd(v128 a, v128 b, int imm8)
			{
				v128 dst = default(v128);
				double* dptr = &dst.Double0;
				double* aptr = &a.Double0;
				double* bptr = &b.Double0;
				for (int i = 0; i <= 1; i++)
				{
					if ((imm8 & (1 << i)) != 0)
					{
						dptr[i] = bptr[i];
					}
					else
					{
						dptr[i] = aptr[i];
					}
				}
				return dst;
			}

			// Token: 0x06000DBA RID: 3514 RVA: 0x00011618 File Offset: 0x0000F818
			[DebuggerStepThrough]
			public unsafe static v128 blend_ps(v128 a, v128 b, int imm8)
			{
				v128 dst = default(v128);
				uint* dptr = &dst.UInt0;
				uint* aptr = &a.UInt0;
				uint* bptr = &b.UInt0;
				for (int i = 0; i <= 3; i++)
				{
					if ((imm8 & (1 << i)) != 0)
					{
						dptr[i] = bptr[i];
					}
					else
					{
						dptr[i] = aptr[i];
					}
				}
				return dst;
			}

			// Token: 0x06000DBB RID: 3515 RVA: 0x00011680 File Offset: 0x0000F880
			[DebuggerStepThrough]
			public unsafe static v128 blendv_pd(v128 a, v128 b, v128 mask)
			{
				v128 dst = default(v128);
				double* dptr = &dst.Double0;
				double* aptr = &a.Double0;
				double* bptr = &b.Double0;
				long* mptr = &mask.SLong0;
				for (int i = 0; i <= 1; i++)
				{
					if (mptr[i] < 0L)
					{
						dptr[i] = bptr[i];
					}
					else
					{
						dptr[i] = aptr[i];
					}
				}
				return dst;
			}

			// Token: 0x06000DBC RID: 3516 RVA: 0x000116F4 File Offset: 0x0000F8F4
			[DebuggerStepThrough]
			public unsafe static v128 blendv_ps(v128 a, v128 b, v128 mask)
			{
				v128 dst = default(v128);
				uint* dptr = &dst.UInt0;
				uint* aptr = &a.UInt0;
				uint* bptr = &b.UInt0;
				int* mptr = &mask.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					if (mptr[i] < 0)
					{
						dptr[i] = bptr[i];
					}
					else
					{
						dptr[i] = aptr[i];
					}
				}
				return dst;
			}

			// Token: 0x06000DBD RID: 3517 RVA: 0x00011768 File Offset: 0x0000F968
			[DebuggerStepThrough]
			public unsafe static v128 blendv_epi8(v128 a, v128 b, v128 mask)
			{
				v128 dst = default(v128);
				byte* dptr = &dst.Byte0;
				byte* aptr = &a.Byte0;
				byte* bptr = &b.Byte0;
				sbyte* mptr = &mask.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					if (mptr[i] < 0)
					{
						dptr[i] = bptr[i];
					}
					else
					{
						dptr[i] = aptr[i];
					}
				}
				return dst;
			}

			// Token: 0x06000DBE RID: 3518 RVA: 0x000117CC File Offset: 0x0000F9CC
			[DebuggerStepThrough]
			public unsafe static v128 blend_epi16(v128 a, v128 b, int imm8)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					if (((imm8 >> i) & 1) != 0)
					{
						dptr[i] = bptr[i];
					}
					else
					{
						dptr[i] = aptr[i];
					}
				}
				return dst;
			}

			// Token: 0x06000DBF RID: 3519 RVA: 0x00011834 File Offset: 0x0000FA34
			[DebuggerStepThrough]
			public static v128 dp_pd(v128 a, v128 b, int imm8)
			{
				double num = (((imm8 & 16) != 0) ? (a.Double0 * b.Double0) : 0.0);
				double t = (((imm8 & 32) != 0) ? (a.Double1 * b.Double1) : 0.0);
				double sum = num + t;
				return new v128
				{
					Double0 = (((imm8 & 1) != 0) ? sum : 0.0),
					Double1 = (((imm8 & 2) != 0) ? sum : 0.0)
				};
			}

			// Token: 0x06000DC0 RID: 3520 RVA: 0x000118BC File Offset: 0x0000FABC
			[DebuggerStepThrough]
			public static v128 dp_ps(v128 a, v128 b, int imm8)
			{
				float num = (((imm8 & 16) != 0) ? (a.Float0 * b.Float0) : 0f);
				float t = (((imm8 & 32) != 0) ? (a.Float1 * b.Float1) : 0f);
				float t2 = (((imm8 & 64) != 0) ? (a.Float2 * b.Float2) : 0f);
				float t3 = (((imm8 & 128) != 0) ? (a.Float3 * b.Float3) : 0f);
				float sum = num + t + t2 + t3;
				return new v128
				{
					Float0 = (((imm8 & 1) != 0) ? sum : 0f),
					Float1 = (((imm8 & 2) != 0) ? sum : 0f),
					Float2 = (((imm8 & 4) != 0) ? sum : 0f),
					Float3 = (((imm8 & 8) != 0) ? sum : 0f)
				};
			}

			// Token: 0x06000DC1 RID: 3521 RVA: 0x00011998 File Offset: 0x0000FB98
			[DebuggerStepThrough]
			public unsafe static int extract_ps(v128 a, int imm8)
			{
				return (&a.SInt0)[imm8 & 3];
			}

			// Token: 0x06000DC2 RID: 3522 RVA: 0x000119AA File Offset: 0x0000FBAA
			[DebuggerStepThrough]
			public unsafe static float extractf_ps(v128 a, int imm8)
			{
				return (&a.Float0)[imm8 & 3];
			}

			// Token: 0x06000DC3 RID: 3523 RVA: 0x000119BC File Offset: 0x0000FBBC
			[DebuggerStepThrough]
			public unsafe static byte extract_epi8(v128 a, int imm8)
			{
				return (&a.Byte0)[imm8 & 15];
			}

			// Token: 0x06000DC4 RID: 3524 RVA: 0x00011998 File Offset: 0x0000FB98
			[DebuggerStepThrough]
			public unsafe static int extract_epi32(v128 a, int imm8)
			{
				return (&a.SInt0)[imm8 & 3];
			}

			// Token: 0x06000DC5 RID: 3525 RVA: 0x000119CC File Offset: 0x0000FBCC
			[DebuggerStepThrough]
			public unsafe static long extract_epi64(v128 a, int imm8)
			{
				return (&a.SLong0)[imm8 & 1];
			}

			// Token: 0x06000DC6 RID: 3526 RVA: 0x000119E0 File Offset: 0x0000FBE0
			[DebuggerStepThrough]
			public unsafe static v128 insert_ps(v128 a, v128 b, int imm8)
			{
				v128 dst = a;
				(&dst.Float0)[(imm8 >> 4) & 3] = (&b.Float0)[(imm8 >> 6) & 3];
				for (int i = 0; i < 4; i++)
				{
					if ((imm8 & (1 << i)) != 0)
					{
						(&dst.Float0)[i] = 0f;
					}
				}
				return dst;
			}

			// Token: 0x06000DC7 RID: 3527 RVA: 0x00011A40 File Offset: 0x0000FC40
			[DebuggerStepThrough]
			public unsafe static v128 insert_epi8(v128 a, byte i, int imm8)
			{
				v128 dst = a;
				(&dst.Byte0)[imm8 & 15] = i;
				return dst;
			}

			// Token: 0x06000DC8 RID: 3528 RVA: 0x00011A60 File Offset: 0x0000FC60
			[DebuggerStepThrough]
			public unsafe static v128 insert_epi32(v128 a, int i, int imm8)
			{
				v128 dst = a;
				(&dst.SInt0)[imm8 & 3] = i;
				return dst;
			}

			// Token: 0x06000DC9 RID: 3529 RVA: 0x00011A84 File Offset: 0x0000FC84
			[DebuggerStepThrough]
			public unsafe static v128 insert_epi64(v128 a, long i, int imm8)
			{
				v128 dst = a;
				(&dst.SLong0)[imm8 & 1] = i;
				return dst;
			}

			// Token: 0x06000DCA RID: 3530 RVA: 0x00011AA8 File Offset: 0x0000FCA8
			[DebuggerStepThrough]
			public unsafe static v128 max_epi8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				sbyte* dptr = &dst.SByte0;
				sbyte* aptr = &a.SByte0;
				sbyte* bptr = &b.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = Math.Max(aptr[i], bptr[i]);
				}
				return dst;
			}

			// Token: 0x06000DCB RID: 3531 RVA: 0x00011B00 File Offset: 0x0000FD00
			[DebuggerStepThrough]
			public unsafe static v128 max_epi32(v128 a, v128 b)
			{
				v128 dst = default(v128);
				int* dptr = &dst.SInt0;
				int* aptr = &a.SInt0;
				int* bptr = &b.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = Math.Max(aptr[i], bptr[i]);
				}
				return dst;
			}

			// Token: 0x06000DCC RID: 3532 RVA: 0x00011B60 File Offset: 0x0000FD60
			[DebuggerStepThrough]
			public unsafe static v128 max_epu32(v128 a, v128 b)
			{
				v128 dst = default(v128);
				uint* dptr = &dst.UInt0;
				uint* aptr = &a.UInt0;
				uint* bptr = &b.UInt0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = Math.Max(aptr[i], bptr[i]);
				}
				return dst;
			}

			// Token: 0x06000DCD RID: 3533 RVA: 0x00011BC0 File Offset: 0x0000FDC0
			[DebuggerStepThrough]
			public unsafe static v128 max_epu16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				ushort* dptr = &dst.UShort0;
				ushort* aptr = &a.UShort0;
				ushort* bptr = &b.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = Math.Max(aptr[i], bptr[i]);
				}
				return dst;
			}

			// Token: 0x06000DCE RID: 3534 RVA: 0x00011C20 File Offset: 0x0000FE20
			[DebuggerStepThrough]
			public unsafe static v128 min_epi8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				sbyte* dptr = &dst.SByte0;
				sbyte* aptr = &a.SByte0;
				sbyte* bptr = &b.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = Math.Min(aptr[i], bptr[i]);
				}
				return dst;
			}

			// Token: 0x06000DCF RID: 3535 RVA: 0x00011C78 File Offset: 0x0000FE78
			[DebuggerStepThrough]
			public unsafe static v128 min_epi32(v128 a, v128 b)
			{
				v128 dst = default(v128);
				int* dptr = &dst.SInt0;
				int* aptr = &a.SInt0;
				int* bptr = &b.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = Math.Min(aptr[i], bptr[i]);
				}
				return dst;
			}

			// Token: 0x06000DD0 RID: 3536 RVA: 0x00011CD8 File Offset: 0x0000FED8
			[DebuggerStepThrough]
			public unsafe static v128 min_epu32(v128 a, v128 b)
			{
				v128 dst = default(v128);
				uint* dptr = &dst.UInt0;
				uint* aptr = &a.UInt0;
				uint* bptr = &b.UInt0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = Math.Min(aptr[i], bptr[i]);
				}
				return dst;
			}

			// Token: 0x06000DD1 RID: 3537 RVA: 0x00011D38 File Offset: 0x0000FF38
			[DebuggerStepThrough]
			public unsafe static v128 min_epu16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				ushort* dptr = &dst.UShort0;
				ushort* aptr = &a.UShort0;
				ushort* bptr = &b.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = Math.Min(aptr[i], bptr[i]);
				}
				return dst;
			}

			// Token: 0x06000DD2 RID: 3538 RVA: 0x00011D98 File Offset: 0x0000FF98
			[DebuggerStepThrough]
			public static v128 packus_epi32(v128 a, v128 b)
			{
				return new v128
				{
					UShort0 = X86.Saturate_To_UnsignedInt16(a.SInt0),
					UShort1 = X86.Saturate_To_UnsignedInt16(a.SInt1),
					UShort2 = X86.Saturate_To_UnsignedInt16(a.SInt2),
					UShort3 = X86.Saturate_To_UnsignedInt16(a.SInt3),
					UShort4 = X86.Saturate_To_UnsignedInt16(b.SInt0),
					UShort5 = X86.Saturate_To_UnsignedInt16(b.SInt1),
					UShort6 = X86.Saturate_To_UnsignedInt16(b.SInt2),
					UShort7 = X86.Saturate_To_UnsignedInt16(b.SInt3)
				};
			}

			// Token: 0x06000DD3 RID: 3539 RVA: 0x00011E40 File Offset: 0x00010040
			[DebuggerStepThrough]
			public static v128 cmpeq_epi64(v128 a, v128 b)
			{
				return new v128
				{
					SLong0 = ((a.SLong0 == b.SLong0) ? (-1L) : 0L),
					SLong1 = ((a.SLong1 == b.SLong1) ? (-1L) : 0L)
				};
			}

			// Token: 0x06000DD4 RID: 3540 RVA: 0x00011E8C File Offset: 0x0001008C
			[DebuggerStepThrough]
			public unsafe static v128 cvtepi8_epi16(v128 a)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				sbyte* aptr = &a.SByte0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = (short)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000DD5 RID: 3541 RVA: 0x00011ECC File Offset: 0x000100CC
			[DebuggerStepThrough]
			public unsafe static v128 cvtepi8_epi32(v128 a)
			{
				v128 dst = default(v128);
				int* dptr = &dst.SInt0;
				sbyte* aptr = &a.SByte0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = (int)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000DD6 RID: 3542 RVA: 0x00011F0C File Offset: 0x0001010C
			[DebuggerStepThrough]
			public unsafe static v128 cvtepi8_epi64(v128 a)
			{
				v128 dst = default(v128);
				long* dptr = &dst.SLong0;
				sbyte* aptr = &a.SByte0;
				for (int i = 0; i <= 1; i++)
				{
					dptr[i] = (long)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000DD7 RID: 3543 RVA: 0x00011F4C File Offset: 0x0001014C
			[DebuggerStepThrough]
			public unsafe static v128 cvtepi16_epi32(v128 a)
			{
				v128 dst = default(v128);
				int* dptr = &dst.SInt0;
				short* aptr = &a.SShort0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = (int)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000DD8 RID: 3544 RVA: 0x00011F90 File Offset: 0x00010190
			[DebuggerStepThrough]
			public unsafe static v128 cvtepi16_epi64(v128 a)
			{
				v128 dst = default(v128);
				long* dptr = &dst.SLong0;
				short* aptr = &a.SShort0;
				for (int i = 0; i <= 1; i++)
				{
					dptr[i] = (long)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000DD9 RID: 3545 RVA: 0x00011FD4 File Offset: 0x000101D4
			[DebuggerStepThrough]
			public unsafe static v128 cvtepi32_epi64(v128 a)
			{
				v128 dst = default(v128);
				long* dptr = &dst.SLong0;
				int* aptr = &a.SInt0;
				for (int i = 0; i <= 1; i++)
				{
					dptr[i] = (long)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000DDA RID: 3546 RVA: 0x00012018 File Offset: 0x00010218
			[DebuggerStepThrough]
			public unsafe static v128 cvtepu8_epi16(v128 a)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				byte* aptr = &a.Byte0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = (short)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000DDB RID: 3547 RVA: 0x00012058 File Offset: 0x00010258
			[DebuggerStepThrough]
			public unsafe static v128 cvtepu8_epi32(v128 a)
			{
				v128 dst = default(v128);
				int* dptr = &dst.SInt0;
				byte* aptr = &a.Byte0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = (int)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000DDC RID: 3548 RVA: 0x00012098 File Offset: 0x00010298
			[DebuggerStepThrough]
			public unsafe static v128 cvtepu8_epi64(v128 a)
			{
				v128 dst = default(v128);
				long* dptr = &dst.SLong0;
				byte* aptr = &a.Byte0;
				for (int i = 0; i <= 1; i++)
				{
					dptr[i] = (long)((ulong)aptr[i]);
				}
				return dst;
			}

			// Token: 0x06000DDD RID: 3549 RVA: 0x000120D8 File Offset: 0x000102D8
			[DebuggerStepThrough]
			public unsafe static v128 cvtepu16_epi32(v128 a)
			{
				v128 dst = default(v128);
				int* dptr = &dst.SInt0;
				ushort* aptr = &a.UShort0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = (int)aptr[i];
				}
				return dst;
			}

			// Token: 0x06000DDE RID: 3550 RVA: 0x0001211C File Offset: 0x0001031C
			[DebuggerStepThrough]
			public unsafe static v128 cvtepu16_epi64(v128 a)
			{
				v128 dst = default(v128);
				long* dptr = &dst.SLong0;
				ushort* aptr = &a.UShort0;
				for (int i = 0; i <= 1; i++)
				{
					dptr[i] = (long)((ulong)aptr[i]);
				}
				return dst;
			}

			// Token: 0x06000DDF RID: 3551 RVA: 0x00012160 File Offset: 0x00010360
			[DebuggerStepThrough]
			public unsafe static v128 cvtepu32_epi64(v128 a)
			{
				v128 dst = default(v128);
				long* dptr = &dst.SLong0;
				uint* aptr = &a.UInt0;
				for (int i = 0; i <= 1; i++)
				{
					dptr[i] = (long)((ulong)aptr[i]);
				}
				return dst;
			}

			// Token: 0x06000DE0 RID: 3552 RVA: 0x000121A4 File Offset: 0x000103A4
			[DebuggerStepThrough]
			public static v128 mul_epi32(v128 a, v128 b)
			{
				return new v128
				{
					SLong0 = (long)a.SInt0 * (long)b.SInt0,
					SLong1 = (long)a.SInt2 * (long)b.SInt2
				};
			}

			// Token: 0x06000DE1 RID: 3553 RVA: 0x000121E8 File Offset: 0x000103E8
			[DebuggerStepThrough]
			public unsafe static v128 mullo_epi32(v128 a, v128 b)
			{
				v128 dst = default(v128);
				int* dptr = &dst.SInt0;
				int* aptr = &a.SInt0;
				int* bptr = &b.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = aptr[i] * bptr[i];
				}
				return dst;
			}

			// Token: 0x06000DE2 RID: 3554 RVA: 0x00012242 File Offset: 0x00010442
			[DebuggerStepThrough]
			public static int testz_si128(v128 a, v128 b)
			{
				if ((a.SLong0 & b.SLong0) != 0L || (a.SLong1 & b.SLong1) != 0L)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000DE3 RID: 3555 RVA: 0x00012265 File Offset: 0x00010465
			[DebuggerStepThrough]
			public static int testc_si128(v128 a, v128 b)
			{
				if ((~(a.SLong0 != 0L) & b.SLong0) != 0L || (~(a.SLong1 != 0L) & b.SLong1) != 0L)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000DE4 RID: 3556 RVA: 0x0001228C File Offset: 0x0001048C
			[DebuggerStepThrough]
			public static int testnzc_si128(v128 a, v128 b)
			{
				int zf = (((a.SLong0 & b.SLong0) == 0L && (a.SLong1 & b.SLong1) == 0L) ? 1 : 0);
				int cf = (((~(a.SLong0 != 0L) & b.SLong0) == 0L && (~(a.SLong1 != 0L) & b.SLong1) == 0L) ? 1 : 0);
				return 1 - (zf | cf);
			}

			// Token: 0x06000DE5 RID: 3557 RVA: 0x000122E6 File Offset: 0x000104E6
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static int test_all_zeros(v128 a, v128 mask)
			{
				return X86.Sse4_1.testz_si128(a, mask);
			}

			// Token: 0x06000DE6 RID: 3558 RVA: 0x000122EF File Offset: 0x000104EF
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static int test_mix_ones_zeroes(v128 a, v128 mask)
			{
				return X86.Sse4_1.testnzc_si128(a, mask);
			}

			// Token: 0x06000DE7 RID: 3559 RVA: 0x000122F8 File Offset: 0x000104F8
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static int test_all_ones(v128 a)
			{
				return X86.Sse4_1.testc_si128(a, X86.Sse2.cmpeq_epi32(a, a));
			}

			// Token: 0x06000DE8 RID: 3560 RVA: 0x00012308 File Offset: 0x00010508
			private static double RoundDImpl(double d, int roundingMode)
			{
				switch (roundingMode & 7)
				{
				case 0:
					return Math.Round(d);
				case 1:
					return Math.Floor(d);
				case 2:
				{
					double r = Math.Ceiling(d);
					if (r == 0.0 && d < 0.0)
					{
						return new v128(9223372036854775808UL).Double0;
					}
					return r;
				}
				case 3:
					return Math.Truncate(d);
				default:
				{
					X86.MXCSRBits mxcsrbits = X86.MXCSR & X86.MXCSRBits.RoundingControlMask;
					if (mxcsrbits == X86.MXCSRBits.RoundToNearest)
					{
						return Math.Round(d);
					}
					if (mxcsrbits == X86.MXCSRBits.RoundDown)
					{
						return Math.Floor(d);
					}
					if (mxcsrbits != X86.MXCSRBits.RoundUp)
					{
						return Math.Truncate(d);
					}
					return Math.Ceiling(d);
				}
				}
			}

			// Token: 0x06000DE9 RID: 3561 RVA: 0x000123B8 File Offset: 0x000105B8
			[DebuggerStepThrough]
			public static v128 round_pd(v128 a, int rounding)
			{
				return new v128
				{
					Double0 = X86.Sse4_1.RoundDImpl(a.Double0, rounding),
					Double1 = X86.Sse4_1.RoundDImpl(a.Double1, rounding)
				};
			}

			// Token: 0x06000DEA RID: 3562 RVA: 0x000123F4 File Offset: 0x000105F4
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static v128 floor_pd(v128 a)
			{
				return X86.Sse4_1.round_pd(a, 1);
			}

			// Token: 0x06000DEB RID: 3563 RVA: 0x000123FD File Offset: 0x000105FD
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static v128 ceil_pd(v128 a)
			{
				return X86.Sse4_1.round_pd(a, 2);
			}

			// Token: 0x06000DEC RID: 3564 RVA: 0x00012408 File Offset: 0x00010608
			[DebuggerStepThrough]
			public static v128 round_ps(v128 a, int rounding)
			{
				return new v128
				{
					Float0 = (float)X86.Sse4_1.RoundDImpl((double)a.Float0, rounding),
					Float1 = (float)X86.Sse4_1.RoundDImpl((double)a.Float1, rounding),
					Float2 = (float)X86.Sse4_1.RoundDImpl((double)a.Float2, rounding),
					Float3 = (float)X86.Sse4_1.RoundDImpl((double)a.Float3, rounding)
				};
			}

			// Token: 0x06000DED RID: 3565 RVA: 0x00012472 File Offset: 0x00010672
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static v128 floor_ps(v128 a)
			{
				return X86.Sse4_1.round_ps(a, 1);
			}

			// Token: 0x06000DEE RID: 3566 RVA: 0x0001247B File Offset: 0x0001067B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static v128 ceil_ps(v128 a)
			{
				return X86.Sse4_1.round_ps(a, 2);
			}

			// Token: 0x06000DEF RID: 3567 RVA: 0x00012484 File Offset: 0x00010684
			[DebuggerStepThrough]
			public static v128 round_sd(v128 a, v128 b, int rounding)
			{
				return new v128
				{
					Double0 = X86.Sse4_1.RoundDImpl(b.Double0, rounding),
					Double1 = a.Double1
				};
			}

			// Token: 0x06000DF0 RID: 3568 RVA: 0x000124BA File Offset: 0x000106BA
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static v128 floor_sd(v128 a, v128 b)
			{
				return X86.Sse4_1.round_sd(a, b, 1);
			}

			// Token: 0x06000DF1 RID: 3569 RVA: 0x000124C4 File Offset: 0x000106C4
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static v128 ceil_sd(v128 a, v128 b)
			{
				return X86.Sse4_1.round_sd(a, b, 2);
			}

			// Token: 0x06000DF2 RID: 3570 RVA: 0x000124D0 File Offset: 0x000106D0
			[DebuggerStepThrough]
			public static v128 round_ss(v128 a, v128 b, int rounding)
			{
				v128 dst = a;
				dst.Float0 = (float)X86.Sse4_1.RoundDImpl((double)b.Float0, rounding);
				return dst;
			}

			// Token: 0x06000DF3 RID: 3571 RVA: 0x000124F5 File Offset: 0x000106F5
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static v128 floor_ss(v128 a, v128 b)
			{
				return X86.Sse4_1.round_ss(a, b, 1);
			}

			// Token: 0x06000DF4 RID: 3572 RVA: 0x000124FF File Offset: 0x000106FF
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static v128 ceil_ss(v128 a, v128 b)
			{
				return X86.Sse4_1.round_ss(a, b, 2);
			}

			// Token: 0x06000DF5 RID: 3573 RVA: 0x0001250C File Offset: 0x0001070C
			[DebuggerStepThrough]
			public unsafe static v128 minpos_epu16(v128 a)
			{
				int index = 0;
				ushort min = a.UShort0;
				ushort* aptr = &a.UShort0;
				for (int i = 1; i <= 7; i++)
				{
					if (aptr[i] < min)
					{
						index = i;
						min = aptr[i];
					}
				}
				return new v128
				{
					UShort0 = min,
					UShort1 = (ushort)index
				};
			}

			// Token: 0x06000DF6 RID: 3574 RVA: 0x0001256C File Offset: 0x0001076C
			[DebuggerStepThrough]
			public unsafe static v128 mpsadbw_epu8(v128 a, v128 b, int imm8)
			{
				v128 dst = default(v128);
				ushort* dptr = &dst.UShort0;
				byte* aptr = &a.Byte0 + ((imm8 >> 2) & 1) * 4;
				byte* ptr = &b.Byte0 + (imm8 & 3) * 4;
				byte b2 = *ptr;
				byte b3 = ptr[1];
				byte b4 = ptr[2];
				byte b5 = ptr[3];
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = (ushort)(Math.Abs((int)(aptr[i] - b2)) + Math.Abs((int)(aptr[i + 1] - b3)) + Math.Abs((int)(aptr[i + 2] - b4)) + Math.Abs((int)(aptr[i + 3] - b5)));
				}
				return dst;
			}

			// Token: 0x06000DF7 RID: 3575 RVA: 0x00012613 File Offset: 0x00010813
			[DebuggerStepThrough]
			public static int MK_INSERTPS_NDX(int srcField, int dstField, int zeroMask)
			{
				return (srcField << 6) | (dstField << 4) | zeroMask;
			}
		}

		// Token: 0x0200004A RID: 74
		public static class Sse4_2
		{
			// Token: 0x1700004C RID: 76
			// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x000024DA File Offset: 0x000006DA
			public static bool IsSse42Supported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000DF9 RID: 3577 RVA: 0x00012620 File Offset: 0x00010820
			private unsafe static v128 cmpistrm_emulation<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T* a, T* b, int len, int imm8, int allOnes, T allOnesT) where T : struct, ValueType, IComparable<T>, IEquatable<T>
			{
				int intRes2 = X86.Sse4_2.ComputeStrCmpIntRes2<T>(a, X86.Sse4_2.ComputeStringLength<T>(a, len), b, X86.Sse4_2.ComputeStringLength<T>(b, len), len, imm8, allOnes);
				return X86.Sse4_2.ComputeStrmOutput<T>(len, imm8, allOnesT, intRes2);
			}

			// Token: 0x06000DFA RID: 3578 RVA: 0x00012654 File Offset: 0x00010854
			private unsafe static v128 cmpestrm_emulation<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T* a, int alen, T* b, int blen, int len, int imm8, int allOnes, T allOnesT) where T : struct, ValueType, IComparable<T>, IEquatable<T>
			{
				int intRes2 = X86.Sse4_2.ComputeStrCmpIntRes2<T>(a, alen, b, blen, len, imm8, allOnes);
				return X86.Sse4_2.ComputeStrmOutput<T>(len, imm8, allOnesT, intRes2);
			}

			// Token: 0x06000DFB RID: 3579 RVA: 0x00012680 File Offset: 0x00010880
			private unsafe static v128 ComputeStrmOutput<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(int len, int imm8, T allOnesT, int intRes2) where T : struct, ValueType, IComparable<T>, IEquatable<T>
			{
				v128 result = default(v128);
				if ((imm8 & 64) != 0)
				{
					T* maskDst = (T*)(&result.Byte0);
					for (int i = 0; i < len; i++)
					{
						if ((intRes2 & (1 << i)) != 0)
						{
							maskDst[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = allOnesT;
						}
						else
						{
							maskDst[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = default(T);
						}
					}
				}
				else
				{
					result.SInt0 = intRes2;
				}
				return result;
			}

			// Token: 0x06000DFC RID: 3580 RVA: 0x000126EC File Offset: 0x000108EC
			private unsafe static int cmpistri_emulation<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T* a, T* b, int len, int imm8, int allOnes, T allOnesT) where T : struct, ValueType, IComparable<T>, IEquatable<T>
			{
				int intRes2 = X86.Sse4_2.ComputeStrCmpIntRes2<T>(a, X86.Sse4_2.ComputeStringLength<T>(a, len), b, X86.Sse4_2.ComputeStringLength<T>(b, len), len, imm8, allOnes);
				return X86.Sse4_2.ComputeStriOutput(len, imm8, intRes2);
			}

			// Token: 0x06000DFD RID: 3581 RVA: 0x0001271C File Offset: 0x0001091C
			private unsafe static int cmpestri_emulation<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T* a, int alen, T* b, int blen, int len, int imm8, int allOnes, T allOnesT) where T : struct, ValueType, IComparable<T>, IEquatable<T>
			{
				int intRes2 = X86.Sse4_2.ComputeStrCmpIntRes2<T>(a, alen, b, blen, len, imm8, allOnes);
				return X86.Sse4_2.ComputeStriOutput(len, imm8, intRes2);
			}

			// Token: 0x06000DFE RID: 3582 RVA: 0x00012744 File Offset: 0x00010944
			private static int ComputeStriOutput(int len, int imm8, int intRes2)
			{
				if ((imm8 & 64) == 0)
				{
					for (int bit = 0; bit < len; bit++)
					{
						if ((intRes2 & (1 << bit)) != 0)
						{
							return bit;
						}
					}
				}
				else
				{
					for (int bit2 = len - 1; bit2 >= 0; bit2--)
					{
						if ((intRes2 & (1 << bit2)) != 0)
						{
							return bit2;
						}
					}
				}
				return len;
			}

			// Token: 0x06000DFF RID: 3583 RVA: 0x0001278C File Offset: 0x0001098C
			private unsafe static int ComputeStringLength<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T* ptr, int max) where T : struct, ValueType, IEquatable<T>
			{
				for (int i = 0; i < max; i++)
				{
					if (EqualityComparer<T>.Default.Equals(ptr[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)], default(T)))
					{
						return i;
					}
				}
				return max;
			}

			// Token: 0x06000E00 RID: 3584 RVA: 0x000127D0 File Offset: 0x000109D0
			private unsafe static int ComputeStrCmpIntRes2<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T* a, int alen, T* b, int blen, int len, int imm8, int allOnes) where T : struct, ValueType, IComparable<T>, IEquatable<T>
			{
				bool aInvalid = false;
				X86.Sse4_2.StrBoolArray boolRes = default(X86.Sse4_2.StrBoolArray);
				bool bInvalid;
				for (int i = 0; i < len; i++)
				{
					T aCh = a[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
					if (i == alen)
					{
						aInvalid = true;
					}
					bInvalid = false;
					for (int j = 0; j < len; j++)
					{
						T bCh = b[(IntPtr)j * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
						if (j == blen)
						{
							bInvalid = true;
						}
						bool match;
						switch ((imm8 >> 2) & 3)
						{
						case 0:
							match = EqualityComparer<T>.Default.Equals(aCh, bCh);
							if (!aInvalid && bInvalid)
							{
								match = false;
							}
							else if (aInvalid && !bInvalid)
							{
								match = false;
							}
							else if (aInvalid && bInvalid)
							{
								match = false;
							}
							break;
						case 1:
							if ((i & 1) == 0)
							{
								match = Comparer<T>.Default.Compare(bCh, aCh) >= 0;
							}
							else
							{
								match = Comparer<T>.Default.Compare(bCh, aCh) <= 0;
							}
							if (!aInvalid && bInvalid)
							{
								match = false;
							}
							else if (aInvalid && !bInvalid)
							{
								match = false;
							}
							else if (aInvalid && bInvalid)
							{
								match = false;
							}
							break;
						case 2:
							match = EqualityComparer<T>.Default.Equals(aCh, bCh);
							if (!aInvalid && bInvalid)
							{
								match = false;
							}
							else if (aInvalid && !bInvalid)
							{
								match = false;
							}
							else if (aInvalid && bInvalid)
							{
								match = true;
							}
							break;
						default:
							match = EqualityComparer<T>.Default.Equals(aCh, bCh);
							if (!aInvalid && bInvalid)
							{
								match = false;
							}
							else if (aInvalid && !bInvalid)
							{
								match = true;
							}
							else if (aInvalid && bInvalid)
							{
								match = true;
							}
							break;
						}
						boolRes.SetBit(i, j, match);
					}
				}
				int intRes = 0;
				switch ((imm8 >> 2) & 3)
				{
				case 0:
				{
					for (int i = 0; i < len; i++)
					{
						for (int j = 0; j < len; j++)
						{
							intRes |= (boolRes.GetBit(j, i) ? 1 : 0) << i;
						}
					}
					break;
				}
				case 1:
				{
					for (int i = 0; i < len; i++)
					{
						for (int j = 0; j < len; j += 2)
						{
							intRes |= ((boolRes.GetBit(j, i) && boolRes.GetBit(j + 1, i)) ? 1 : 0) << i;
						}
					}
					break;
				}
				case 2:
				{
					for (int i = 0; i < len; i++)
					{
						intRes |= (boolRes.GetBit(i, i) ? 1 : 0) << i;
					}
					break;
				}
				case 3:
				{
					intRes = allOnes;
					for (int i = 0; i < len; i++)
					{
						int k = i;
						for (int j = 0; j < len - i; j++)
						{
							if (!boolRes.GetBit(j, k))
							{
								intRes &= ~(1 << i);
							}
							k++;
						}
					}
					break;
				}
				}
				int intRes2 = 0;
				bInvalid = false;
				for (int i = 0; i < len; i++)
				{
					if ((imm8 & 16) != 0)
					{
						if ((imm8 & 32) != 0)
						{
							if (EqualityComparer<T>.Default.Equals(b[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)], default(T)))
							{
								bInvalid = true;
							}
							if (bInvalid)
							{
								intRes2 |= intRes & (1 << i);
							}
							else
							{
								intRes2 |= ~intRes & (1 << i);
							}
						}
						else
						{
							intRes2 |= ~intRes & (1 << i);
						}
					}
					else
					{
						intRes2 |= intRes & (1 << i);
					}
				}
				return intRes2;
			}

			// Token: 0x06000E01 RID: 3585 RVA: 0x00012B14 File Offset: 0x00010D14
			[DebuggerStepThrough]
			public unsafe static v128 cmpistrm(v128 a, v128 b, int imm8)
			{
				v128 c;
				if ((imm8 & 1) == 0)
				{
					if ((imm8 & 2) == 0)
					{
						c = X86.Sse4_2.cmpistrm_emulation<byte>(&a.Byte0, &b.Byte0, 16, imm8, 65535, byte.MaxValue);
					}
					else
					{
						c = X86.Sse4_2.cmpistrm_emulation<sbyte>(&a.SByte0, &b.SByte0, 16, imm8, 65535, -1);
					}
				}
				else if ((imm8 & 2) == 0)
				{
					c = X86.Sse4_2.cmpistrm_emulation<ushort>(&a.UShort0, &b.UShort0, 8, imm8, 255, ushort.MaxValue);
				}
				else
				{
					c = X86.Sse4_2.cmpistrm_emulation<short>(&a.SShort0, &b.SShort0, 8, imm8, 255, -1);
				}
				return c;
			}

			// Token: 0x06000E02 RID: 3586 RVA: 0x00012BBC File Offset: 0x00010DBC
			[DebuggerStepThrough]
			public unsafe static int cmpistri(v128 a, v128 b, int imm8)
			{
				if ((imm8 & 1) == 0)
				{
					if ((imm8 & 2) == 0)
					{
						return X86.Sse4_2.cmpistri_emulation<byte>(&a.Byte0, &b.Byte0, 16, imm8, 65535, byte.MaxValue);
					}
					return X86.Sse4_2.cmpistri_emulation<sbyte>(&a.SByte0, &b.SByte0, 16, imm8, 65535, -1);
				}
				else
				{
					if ((imm8 & 2) == 0)
					{
						return X86.Sse4_2.cmpistri_emulation<ushort>(&a.UShort0, &b.UShort0, 8, imm8, 255, ushort.MaxValue);
					}
					return X86.Sse4_2.cmpistri_emulation<short>(&a.SShort0, &b.SShort0, 8, imm8, 255, -1);
				}
			}

			// Token: 0x06000E03 RID: 3587 RVA: 0x00012C5C File Offset: 0x00010E5C
			[DebuggerStepThrough]
			public unsafe static v128 cmpestrm(v128 a, int la, v128 b, int lb, int imm8)
			{
				v128 c;
				if ((imm8 & 1) == 0)
				{
					if ((imm8 & 2) == 0)
					{
						c = X86.Sse4_2.cmpestrm_emulation<byte>(&a.Byte0, la, &b.Byte0, lb, 16, imm8, 65535, byte.MaxValue);
					}
					else
					{
						c = X86.Sse4_2.cmpestrm_emulation<sbyte>(&a.SByte0, la, &b.SByte0, lb, 16, imm8, 65535, -1);
					}
				}
				else if ((imm8 & 2) == 0)
				{
					c = X86.Sse4_2.cmpestrm_emulation<ushort>(&a.UShort0, la, &b.UShort0, lb, 8, imm8, 255, ushort.MaxValue);
				}
				else
				{
					c = X86.Sse4_2.cmpestrm_emulation<short>(&a.SShort0, la, &b.SShort0, lb, 8, imm8, 255, -1);
				}
				return c;
			}

			// Token: 0x06000E04 RID: 3588 RVA: 0x00012D10 File Offset: 0x00010F10
			[DebuggerStepThrough]
			public unsafe static int cmpestri(v128 a, int la, v128 b, int lb, int imm8)
			{
				if ((imm8 & 1) == 0)
				{
					if ((imm8 & 2) == 0)
					{
						return X86.Sse4_2.cmpestri_emulation<byte>(&a.Byte0, la, &b.Byte0, lb, 16, imm8, 65535, byte.MaxValue);
					}
					return X86.Sse4_2.cmpestri_emulation<sbyte>(&a.SByte0, la, &b.SByte0, lb, 16, imm8, 65535, -1);
				}
				else
				{
					if ((imm8 & 2) == 0)
					{
						return X86.Sse4_2.cmpestri_emulation<ushort>(&a.UShort0, la, &b.UShort0, lb, 8, imm8, 255, ushort.MaxValue);
					}
					return X86.Sse4_2.cmpestri_emulation<short>(&a.SShort0, la, &b.SShort0, lb, 8, imm8, 255, -1);
				}
			}

			// Token: 0x06000E05 RID: 3589 RVA: 0x00012DBC File Offset: 0x00010FBC
			[DebuggerStepThrough]
			public unsafe static int cmpistrz(v128 a, v128 b, int imm8)
			{
				if ((imm8 & 1) == 0)
				{
					if (X86.Sse4_2.ComputeStringLength<byte>(&b.Byte0, 16) >= 16)
					{
						return 0;
					}
					return 1;
				}
				else
				{
					if (X86.Sse4_2.ComputeStringLength<ushort>(&b.UShort0, 8) >= 8)
					{
						return 0;
					}
					return 1;
				}
			}

			// Token: 0x06000E06 RID: 3590 RVA: 0x00012DF0 File Offset: 0x00010FF0
			[DebuggerStepThrough]
			public static int cmpistrc(v128 a, v128 b, int imm8)
			{
				v128 q = X86.Sse4_2.cmpistrm(a, b, imm8);
				if (q.SInt0 != 0 || q.SInt1 != 0 || q.SInt2 != 0 || q.SInt3 != 0)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x06000E07 RID: 3591 RVA: 0x00012E29 File Offset: 0x00011029
			[DebuggerStepThrough]
			public unsafe static int cmpistrs(v128 a, v128 b, int imm8)
			{
				if ((imm8 & 1) == 0)
				{
					if (X86.Sse4_2.ComputeStringLength<byte>(&a.Byte0, 16) >= 16)
					{
						return 0;
					}
					return 1;
				}
				else
				{
					if (X86.Sse4_2.ComputeStringLength<ushort>(&a.UShort0, 8) >= 8)
					{
						return 0;
					}
					return 1;
				}
			}

			// Token: 0x06000E08 RID: 3592 RVA: 0x00012E5C File Offset: 0x0001105C
			[DebuggerStepThrough]
			public unsafe static int cmpistro(v128 a, v128 b, int imm8)
			{
				int intRes2;
				if ((imm8 & 1) == 0)
				{
					int al = X86.Sse4_2.ComputeStringLength<byte>(&a.Byte0, 16);
					int bl = X86.Sse4_2.ComputeStringLength<byte>(&b.Byte0, 16);
					if ((imm8 & 2) == 0)
					{
						intRes2 = X86.Sse4_2.ComputeStrCmpIntRes2<byte>(&a.Byte0, al, &b.Byte0, bl, 16, imm8, 65535);
					}
					else
					{
						intRes2 = X86.Sse4_2.ComputeStrCmpIntRes2<sbyte>(&a.SByte0, al, &b.SByte0, bl, 16, imm8, 65535);
					}
				}
				else
				{
					int al2 = X86.Sse4_2.ComputeStringLength<ushort>(&a.UShort0, 8);
					int bl2 = X86.Sse4_2.ComputeStringLength<ushort>(&b.UShort0, 8);
					if ((imm8 & 2) == 0)
					{
						intRes2 = X86.Sse4_2.ComputeStrCmpIntRes2<ushort>(&a.UShort0, al2, &b.UShort0, bl2, 8, imm8, 255);
					}
					else
					{
						intRes2 = X86.Sse4_2.ComputeStrCmpIntRes2<short>(&a.SShort0, al2, &b.SShort0, bl2, 8, imm8, 255);
					}
				}
				return intRes2 & 1;
			}

			// Token: 0x06000E09 RID: 3593 RVA: 0x00012F43 File Offset: 0x00011143
			[DebuggerStepThrough]
			public static int cmpistra(v128 a, v128 b, int imm8)
			{
				return ~X86.Sse4_2.cmpistrc(a, b, imm8) & ~X86.Sse4_2.cmpistrz(a, b, imm8) & 1;
			}

			// Token: 0x06000E0A RID: 3594 RVA: 0x00012F5C File Offset: 0x0001115C
			[DebuggerStepThrough]
			public static int cmpestrz(v128 a, int la, v128 b, int lb, int imm8)
			{
				int size = (((imm8 & 1) == 1) ? 16 : 8);
				int upperBound = 128 / size - 1;
				if (lb > upperBound)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000E0B RID: 3595 RVA: 0x00012F88 File Offset: 0x00011188
			[DebuggerStepThrough]
			public unsafe static int cmpestrc(v128 a, int la, v128 b, int lb, int imm8)
			{
				int intRes2;
				if ((imm8 & 1) == 0)
				{
					if ((imm8 & 2) == 0)
					{
						intRes2 = X86.Sse4_2.ComputeStrCmpIntRes2<byte>(&a.Byte0, la, &b.Byte0, lb, 16, imm8, 65535);
					}
					else
					{
						intRes2 = X86.Sse4_2.ComputeStrCmpIntRes2<sbyte>(&a.SByte0, la, &b.SByte0, lb, 16, imm8, 65535);
					}
				}
				else if ((imm8 & 2) == 0)
				{
					intRes2 = X86.Sse4_2.ComputeStrCmpIntRes2<ushort>(&a.UShort0, la, &b.UShort0, lb, 8, imm8, 255);
				}
				else
				{
					intRes2 = X86.Sse4_2.ComputeStrCmpIntRes2<short>(&a.SShort0, la, &b.SShort0, lb, 8, imm8, 255);
				}
				if (intRes2 == 0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000E0C RID: 3596 RVA: 0x00013038 File Offset: 0x00011238
			[DebuggerStepThrough]
			public static int cmpestrs(v128 a, int la, v128 b, int lb, int imm8)
			{
				int size = (((imm8 & 1) == 1) ? 16 : 8);
				int upperBound = 128 / size - 1;
				if (la > upperBound)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000E0D RID: 3597 RVA: 0x00013064 File Offset: 0x00011264
			[DebuggerStepThrough]
			public unsafe static int cmpestro(v128 a, int la, v128 b, int lb, int imm8)
			{
				int intRes2;
				if ((imm8 & 1) == 0)
				{
					if ((imm8 & 2) == 0)
					{
						intRes2 = X86.Sse4_2.ComputeStrCmpIntRes2<byte>(&a.Byte0, la, &b.Byte0, lb, 16, imm8, 65535);
					}
					else
					{
						intRes2 = X86.Sse4_2.ComputeStrCmpIntRes2<sbyte>(&a.SByte0, la, &b.SByte0, lb, 16, imm8, 65535);
					}
				}
				else if ((imm8 & 2) == 0)
				{
					intRes2 = X86.Sse4_2.ComputeStrCmpIntRes2<ushort>(&a.UShort0, la, &b.UShort0, lb, 8, imm8, 255);
				}
				else
				{
					intRes2 = X86.Sse4_2.ComputeStrCmpIntRes2<short>(&a.SShort0, la, &b.SShort0, lb, 8, imm8, 255);
				}
				return intRes2 & 1;
			}

			// Token: 0x06000E0E RID: 3598 RVA: 0x0001310E File Offset: 0x0001130E
			[DebuggerStepThrough]
			public static int cmpestra(v128 a, int la, v128 b, int lb, int imm8)
			{
				return ~X86.Sse4_2.cmpestrc(a, la, b, lb, imm8) & ~X86.Sse4_2.cmpestrz(a, la, b, lb, imm8) & 1;
			}

			// Token: 0x06000E0F RID: 3599 RVA: 0x0001312C File Offset: 0x0001132C
			[DebuggerStepThrough]
			public static v128 cmpgt_epi64(v128 val1, v128 val2)
			{
				return new v128
				{
					SLong0 = ((val1.SLong0 > val2.SLong0) ? (-1L) : 0L),
					SLong1 = ((val1.SLong1 > val2.SLong1) ? (-1L) : 0L)
				};
			}

			// Token: 0x06000E10 RID: 3600 RVA: 0x00013176 File Offset: 0x00011376
			[DebuggerStepThrough]
			public static uint crc32_u32(uint crc, uint v)
			{
				crc = X86.Sse4_2.crc32_u8(crc, (byte)v);
				v >>= 8;
				crc = X86.Sse4_2.crc32_u8(crc, (byte)v);
				v >>= 8;
				crc = X86.Sse4_2.crc32_u8(crc, (byte)v);
				v >>= 8;
				crc = X86.Sse4_2.crc32_u8(crc, (byte)v);
				return crc;
			}

			// Token: 0x06000E11 RID: 3601 RVA: 0x000131B0 File Offset: 0x000113B0
			[DebuggerStepThrough]
			public static uint crc32_u8(uint crc, byte v)
			{
				crc = (crc >> 8) ^ X86.Sse4_2.crctab[(int)((crc ^ (uint)v) & 255U)];
				return crc;
			}

			// Token: 0x06000E12 RID: 3602 RVA: 0x000131C8 File Offset: 0x000113C8
			[DebuggerStepThrough]
			public static uint crc32_u16(uint crc, ushort v)
			{
				crc = X86.Sse4_2.crc32_u8(crc, (byte)v);
				v = (ushort)(v >> 8);
				crc = X86.Sse4_2.crc32_u8(crc, (byte)v);
				return crc;
			}

			// Token: 0x06000E13 RID: 3603 RVA: 0x000131E5 File Offset: 0x000113E5
			[DebuggerStepThrough]
			[Obsolete("Use the ulong version of this intrinsic instead.")]
			public static ulong crc32_u64(ulong crc_ul, long v)
			{
				return X86.Sse4_2.crc32_u64(crc_ul, (ulong)v);
			}

			// Token: 0x06000E14 RID: 3604 RVA: 0x000131F0 File Offset: 0x000113F0
			[DebuggerStepThrough]
			public static ulong crc32_u64(ulong crc_ul, ulong v)
			{
				uint num = X86.Sse4_2.crc32_u8((uint)crc_ul, (byte)v);
				v >>= 8;
				uint num2 = X86.Sse4_2.crc32_u8(num, (byte)v);
				v >>= 8;
				uint num3 = X86.Sse4_2.crc32_u8(num2, (byte)v);
				v >>= 8;
				uint num4 = X86.Sse4_2.crc32_u8(num3, (byte)v);
				v >>= 8;
				uint num5 = X86.Sse4_2.crc32_u8(num4, (byte)v);
				v >>= 8;
				uint num6 = X86.Sse4_2.crc32_u8(num5, (byte)v);
				v >>= 8;
				uint num7 = X86.Sse4_2.crc32_u8(num6, (byte)v);
				v >>= 8;
				return (ulong)X86.Sse4_2.crc32_u8(num7, (byte)v);
			}

			// Token: 0x040002B3 RID: 691
			private static readonly uint[] crctab = new uint[]
			{
				0U, 4067132163U, 3778769143U, 324072436U, 3348797215U, 904991772U, 648144872U, 3570033899U, 2329499855U, 2024987596U,
				1809983544U, 2575936315U, 1296289744U, 3207089363U, 2893594407U, 1578318884U, 274646895U, 3795141740U, 4049975192U, 51262619U,
				3619967088U, 632279923U, 922689671U, 3298075524U, 2592579488U, 1760304291U, 2075979607U, 2312596564U, 1562183871U, 2943781820U,
				3156637768U, 1313733451U, 549293790U, 3537243613U, 3246849577U, 871202090U, 3878099393U, 357341890U, 102525238U, 4101499445U,
				2858735121U, 1477399826U, 1264559846U, 3107202533U, 1845379342U, 2677391885U, 2361733625U, 2125378298U, 820201905U, 3263744690U,
				3520608582U, 598981189U, 4151959214U, 85089709U, 373468761U, 3827903834U, 3124367742U, 1213305469U, 1526817161U, 2842354314U,
				2107672161U, 2412447074U, 2627466902U, 1861252501U, 1098587580U, 3004210879U, 2688576843U, 1378610760U, 2262928035U, 1955203488U,
				1742404180U, 2511436119U, 3416409459U, 969524848U, 714683780U, 3639785095U, 205050476U, 4266873199U, 3976438427U, 526918040U,
				1361435347U, 2739821008U, 2954799652U, 1114974503U, 2529119692U, 1691668175U, 2005155131U, 2247081528U, 3690758684U, 697762079U,
				986182379U, 3366744552U, 476452099U, 3993867776U, 4250756596U, 255256311U, 1640403810U, 2477592673U, 2164122517U, 1922457750U,
				2791048317U, 1412925310U, 1197962378U, 3037525897U, 3944729517U, 427051182U, 170179418U, 4165941337U, 746937522U, 3740196785U,
				3451792453U, 1070968646U, 1905808397U, 2213795598U, 2426610938U, 1657317369U, 3053634322U, 1147748369U, 1463399397U, 2773627110U,
				4215344322U, 153784257U, 444234805U, 3893493558U, 1021025245U, 3467647198U, 3722505002U, 797665321U, 2197175160U, 1889384571U,
				1674398607U, 2443626636U, 1164749927U, 3070701412U, 2757221520U, 1446797203U, 137323447U, 4198817972U, 3910406976U, 461344835U,
				3484808360U, 1037989803U, 781091935U, 3705997148U, 2460548119U, 1623424788U, 1939049696U, 2180517859U, 1429367560U, 2807687179U,
				3020495871U, 1180866812U, 410100952U, 3927582683U, 4182430767U, 186734380U, 3756733383U, 763408580U, 1053836080U, 3434856499U,
				2722870694U, 1344288421U, 1131464017U, 2971354706U, 1708204729U, 2545590714U, 2229949006U, 1988219213U, 680717673U, 3673779818U,
				3383336350U, 1002577565U, 4010310262U, 493091189U, 238226049U, 4233660802U, 2987750089U, 1082061258U, 1395524158U, 2705686845U,
				1972364758U, 2279892693U, 2494862625U, 1725896226U, 952904198U, 3399985413U, 3656866545U, 731699698U, 4283874585U, 222117402U,
				510512622U, 3959836397U, 3280807620U, 837199303U, 582374963U, 3504198960U, 68661723U, 4135334616U, 3844915500U, 390545967U,
				1230274059U, 3141532936U, 2825850620U, 1510247935U, 2395924756U, 2091215383U, 1878366691U, 2644384480U, 3553878443U, 565732008U,
				854102364U, 3229815391U, 340358836U, 3861050807U, 4117890627U, 119113024U, 1493875044U, 2875275879U, 3090270611U, 1247431312U,
				2660249211U, 1828433272U, 2141937292U, 2378227087U, 3811616794U, 291187481U, 34330861U, 4032846830U, 615137029U, 3603020806U,
				3314634738U, 939183345U, 1776939221U, 2609017814U, 2295496738U, 2058945313U, 2926798794U, 1545135305U, 1330124605U, 3173225534U,
				4084100981U, 17165430U, 307568514U, 3762199681U, 888469610U, 3332340585U, 3587147933U, 665062302U, 2042050490U, 2346497209U,
				2559330125U, 1793573966U, 3190661285U, 1279665062U, 1595330642U, 2910671697U
			};

			// Token: 0x0200004B RID: 75
			[Flags]
			public enum SIDD
			{
				// Token: 0x040002B5 RID: 693
				UBYTE_OPS = 0,
				// Token: 0x040002B6 RID: 694
				UWORD_OPS = 1,
				// Token: 0x040002B7 RID: 695
				SBYTE_OPS = 2,
				// Token: 0x040002B8 RID: 696
				SWORD_OPS = 3,
				// Token: 0x040002B9 RID: 697
				CMP_EQUAL_ANY = 0,
				// Token: 0x040002BA RID: 698
				CMP_RANGES = 4,
				// Token: 0x040002BB RID: 699
				CMP_EQUAL_EACH = 8,
				// Token: 0x040002BC RID: 700
				CMP_EQUAL_ORDERED = 12,
				// Token: 0x040002BD RID: 701
				POSITIVE_POLARITY = 0,
				// Token: 0x040002BE RID: 702
				NEGATIVE_POLARITY = 16,
				// Token: 0x040002BF RID: 703
				MASKED_POSITIVE_POLARITY = 32,
				// Token: 0x040002C0 RID: 704
				MASKED_NEGATIVE_POLARITY = 48,
				// Token: 0x040002C1 RID: 705
				LEAST_SIGNIFICANT = 0,
				// Token: 0x040002C2 RID: 706
				MOST_SIGNIFICANT = 64,
				// Token: 0x040002C3 RID: 707
				BIT_MASK = 0,
				// Token: 0x040002C4 RID: 708
				UNIT_MASK = 64
			}

			// Token: 0x0200004C RID: 76
			private struct StrBoolArray
			{
				// Token: 0x06000E16 RID: 3606 RVA: 0x00013278 File Offset: 0x00011478
				public unsafe void SetBit(int aindex, int bindex, bool val)
				{
					fixed (ushort* ptr = &this.Bits.FixedElementField)
					{
						ushort* b = ptr;
						if (val)
						{
							ushort* ptr2 = b + aindex;
							*ptr2 |= (ushort)(1 << bindex);
						}
						else
						{
							ushort* ptr3 = b + aindex;
							*ptr3 &= (ushort)(~(ushort)(1 << bindex));
						}
					}
				}

				// Token: 0x06000E17 RID: 3607 RVA: 0x000132C4 File Offset: 0x000114C4
				public unsafe bool GetBit(int aindex, int bindex)
				{
					fixed (ushort* ptr = &this.Bits.FixedElementField)
					{
						return ((int)ptr[aindex] & (1 << bindex)) != 0;
					}
				}

				// Token: 0x040002C5 RID: 709
				[FixedBuffer(typeof(ushort), 16)]
				public X86.Sse4_2.StrBoolArray.<Bits>e__FixedBuffer Bits;

				// Token: 0x0200004D RID: 77
				[CompilerGenerated]
				[UnsafeValueType]
				[StructLayout(LayoutKind.Sequential, Size = 32)]
				public struct <Bits>e__FixedBuffer
				{
					// Token: 0x040002C6 RID: 710
					public ushort FixedElementField;
				}
			}
		}

		// Token: 0x0200004E RID: 78
		public static class Ssse3
		{
			// Token: 0x1700004D RID: 77
			// (get) Token: 0x06000E18 RID: 3608 RVA: 0x000024DA File Offset: 0x000006DA
			public static bool IsSsse3Supported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000E19 RID: 3609 RVA: 0x000132F0 File Offset: 0x000114F0
			[DebuggerStepThrough]
			public unsafe static v128 abs_epi8(v128 a)
			{
				v128 dst = default(v128);
				byte* dptr = &dst.Byte0;
				sbyte* aptr = &a.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					dptr[i] = (byte)Math.Abs((int)aptr[i]);
				}
				return dst;
			}

			// Token: 0x06000E1A RID: 3610 RVA: 0x00013334 File Offset: 0x00011534
			[DebuggerStepThrough]
			public unsafe static v128 abs_epi16(v128 a)
			{
				v128 dst = default(v128);
				ushort* dptr = &dst.UShort0;
				short* aptr = &a.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					dptr[i] = (ushort)Math.Abs((int)aptr[i]);
				}
				return dst;
			}

			// Token: 0x06000E1B RID: 3611 RVA: 0x0001337C File Offset: 0x0001157C
			[DebuggerStepThrough]
			public unsafe static v128 abs_epi32(v128 a)
			{
				v128 dst = default(v128);
				uint* dptr = &dst.UInt0;
				int* aptr = &a.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = (uint)Math.Abs((long)aptr[i]);
				}
				return dst;
			}

			// Token: 0x06000E1C RID: 3612 RVA: 0x000133C8 File Offset: 0x000115C8
			[DebuggerStepThrough]
			public unsafe static v128 shuffle_epi8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				byte* dptr = &dst.Byte0;
				byte* aptr = &a.Byte0;
				byte* bptr = &b.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					if ((bptr[i] & 128) != 0)
					{
						dptr[i] = 0;
					}
					else
					{
						dptr[i] = aptr[bptr[i] & 15];
					}
				}
				return dst;
			}

			// Token: 0x06000E1D RID: 3613 RVA: 0x00013430 File Offset: 0x00011630
			[DebuggerStepThrough]
			public unsafe static v128 alignr_epi8(v128 a, v128 b, int count)
			{
				v128 dst = default(v128);
				byte* dptr = &dst.Byte0;
				byte* aptr = &a.Byte0 + count;
				byte* bptr = &b.Byte0;
				int i;
				for (i = 0; i < 16 - count; i++)
				{
					*(dptr++) = *(aptr++);
				}
				while (i < 16)
				{
					*(dptr++) = *(bptr++);
					i++;
				}
				return dst;
			}

			// Token: 0x06000E1E RID: 3614 RVA: 0x0001349C File Offset: 0x0001169C
			[DebuggerStepThrough]
			public unsafe static v128 hadd_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = aptr[2 * i + 1] + aptr[2 * i];
					dptr[i + 4] = bptr[2 * i + 1] + bptr[2 * i];
				}
				return dst;
			}

			// Token: 0x06000E1F RID: 3615 RVA: 0x00013520 File Offset: 0x00011720
			[DebuggerStepThrough]
			public unsafe static v128 hadds_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = X86.Saturate_To_Int16((int)(aptr[2 * i + 1] + aptr[2 * i]));
					dptr[i + 4] = X86.Saturate_To_Int16((int)(bptr[2 * i + 1] + bptr[2 * i]));
				}
				return dst;
			}

			// Token: 0x06000E20 RID: 3616 RVA: 0x000135AC File Offset: 0x000117AC
			[DebuggerStepThrough]
			public static v128 hadd_epi32(v128 a, v128 b)
			{
				return new v128
				{
					SInt0 = a.SInt1 + a.SInt0,
					SInt1 = a.SInt3 + a.SInt2,
					SInt2 = b.SInt1 + b.SInt0,
					SInt3 = b.SInt3 + b.SInt2
				};
			}

			// Token: 0x06000E21 RID: 3617 RVA: 0x00013614 File Offset: 0x00011814
			[DebuggerStepThrough]
			public unsafe static v128 hsub_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = aptr[2 * i] - aptr[2 * i + 1];
					dptr[i + 4] = bptr[2 * i] - bptr[2 * i + 1];
				}
				return dst;
			}

			// Token: 0x06000E22 RID: 3618 RVA: 0x00013698 File Offset: 0x00011898
			[DebuggerStepThrough]
			public unsafe static v128 hsubs_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				for (int i = 0; i <= 3; i++)
				{
					dptr[i] = X86.Saturate_To_Int16((int)(aptr[2 * i] - aptr[2 * i + 1]));
					dptr[i + 4] = X86.Saturate_To_Int16((int)(bptr[2 * i] - bptr[2 * i + 1]));
				}
				return dst;
			}

			// Token: 0x06000E23 RID: 3619 RVA: 0x00013724 File Offset: 0x00011924
			[DebuggerStepThrough]
			public static v128 hsub_epi32(v128 a, v128 b)
			{
				return new v128
				{
					SInt0 = a.SInt0 - a.SInt1,
					SInt1 = a.SInt2 - a.SInt3,
					SInt2 = b.SInt0 - b.SInt1,
					SInt3 = b.SInt2 - b.SInt3
				};
			}

			// Token: 0x06000E24 RID: 3620 RVA: 0x0001378C File Offset: 0x0001198C
			[DebuggerStepThrough]
			public unsafe static v128 maddubs_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				byte* aptr = &a.Byte0;
				sbyte* bptr = &b.SByte0;
				for (int i = 0; i <= 7; i++)
				{
					int tmp = (int)(aptr[2 * i + 1] * (byte)bptr[2 * i + 1] + aptr[2 * i] * (byte)bptr[2 * i]);
					dptr[i] = X86.Saturate_To_Int16(tmp);
				}
				return dst;
			}

			// Token: 0x06000E25 RID: 3621 RVA: 0x00013804 File Offset: 0x00011A04
			[DebuggerStepThrough]
			public unsafe static v128 mulhrs_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					int tmp = (int)(aptr[i] * bptr[i]);
					tmp >>= 14;
					tmp++;
					tmp >>= 1;
					dptr[i] = (short)tmp;
				}
				return dst;
			}

			// Token: 0x06000E26 RID: 3622 RVA: 0x00013878 File Offset: 0x00011A78
			[DebuggerStepThrough]
			public unsafe static v128 sign_epi8(v128 a, v128 b)
			{
				v128 dst = default(v128);
				sbyte* dptr = &dst.SByte0;
				sbyte* aptr = &a.SByte0;
				sbyte* bptr = &b.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					if (bptr[i] < 0)
					{
						dptr[i] = -aptr[i];
					}
					else if (bptr[i] == 0)
					{
						dptr[i] = 0;
					}
					else
					{
						dptr[i] = aptr[i];
					}
				}
				return dst;
			}

			// Token: 0x06000E27 RID: 3623 RVA: 0x000138EC File Offset: 0x00011AEC
			[DebuggerStepThrough]
			public unsafe static v128 sign_epi16(v128 a, v128 b)
			{
				v128 dst = default(v128);
				short* dptr = &dst.SShort0;
				short* aptr = &a.SShort0;
				short* bptr = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					if (bptr[i] < 0)
					{
						dptr[i] = -aptr[i];
					}
					else if (bptr[i] == 0)
					{
						dptr[i] = 0;
					}
					else
					{
						dptr[i] = aptr[i];
					}
				}
				return dst;
			}

			// Token: 0x06000E28 RID: 3624 RVA: 0x00013974 File Offset: 0x00011B74
			[DebuggerStepThrough]
			public unsafe static v128 sign_epi32(v128 a, v128 b)
			{
				v128 dst = default(v128);
				int* dptr = &dst.SInt0;
				int* aptr = &a.SInt0;
				int* bptr = &b.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					if (bptr[i] < 0)
					{
						dptr[i] = -aptr[i];
					}
					else if (bptr[i] == 0)
					{
						dptr[i] = 0;
					}
					else
					{
						dptr[i] = aptr[i];
					}
				}
				return dst;
			}
		}
	}
}
