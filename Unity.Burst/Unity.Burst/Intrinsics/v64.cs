using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Unity.Burst.Intrinsics
{
	// Token: 0x02000038 RID: 56
	[DebuggerTypeProxy(typeof(V64DebugView))]
	[StructLayout(LayoutKind.Explicit)]
	public struct v64
	{
		// Token: 0x06000AA0 RID: 2720 RVA: 0x0000844C File Offset: 0x0000664C
		public v64(byte b)
		{
			this = default(v64);
			this.Byte7 = b;
			this.Byte6 = b;
			this.Byte5 = b;
			this.Byte4 = b;
			this.Byte3 = b;
			this.Byte2 = b;
			this.Byte1 = b;
			this.Byte0 = b;
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000084A8 File Offset: 0x000066A8
		public v64(byte a, byte b, byte c, byte d, byte e, byte f, byte g, byte h)
		{
			this = default(v64);
			this.Byte0 = a;
			this.Byte1 = b;
			this.Byte2 = c;
			this.Byte3 = d;
			this.Byte4 = e;
			this.Byte5 = f;
			this.Byte6 = g;
			this.Byte7 = h;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x000084FC File Offset: 0x000066FC
		public v64(sbyte b)
		{
			this = default(v64);
			this.SByte7 = b;
			this.SByte6 = b;
			this.SByte5 = b;
			this.SByte4 = b;
			this.SByte3 = b;
			this.SByte2 = b;
			this.SByte1 = b;
			this.SByte0 = b;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00008558 File Offset: 0x00006758
		public v64(sbyte a, sbyte b, sbyte c, sbyte d, sbyte e, sbyte f, sbyte g, sbyte h)
		{
			this = default(v64);
			this.SByte0 = a;
			this.SByte1 = b;
			this.SByte2 = c;
			this.SByte3 = d;
			this.SByte4 = e;
			this.SByte5 = f;
			this.SByte6 = g;
			this.SByte7 = h;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000085AC File Offset: 0x000067AC
		public v64(short v)
		{
			this = default(v64);
			this.SShort3 = v;
			this.SShort2 = v;
			this.SShort1 = v;
			this.SShort0 = v;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000085E2 File Offset: 0x000067E2
		public v64(short a, short b, short c, short d)
		{
			this = default(v64);
			this.SShort0 = a;
			this.SShort1 = b;
			this.SShort2 = c;
			this.SShort3 = d;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00008608 File Offset: 0x00006808
		public v64(ushort v)
		{
			this = default(v64);
			this.UShort3 = v;
			this.UShort2 = v;
			this.UShort1 = v;
			this.UShort0 = v;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0000863E File Offset: 0x0000683E
		public v64(ushort a, ushort b, ushort c, ushort d)
		{
			this = default(v64);
			this.UShort0 = a;
			this.UShort1 = b;
			this.UShort2 = c;
			this.UShort3 = d;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00008664 File Offset: 0x00006864
		public v64(int v)
		{
			this = default(v64);
			this.SInt1 = v;
			this.SInt0 = v;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00008688 File Offset: 0x00006888
		public v64(int a, int b)
		{
			this = default(v64);
			this.SInt0 = a;
			this.SInt1 = b;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000086A0 File Offset: 0x000068A0
		public v64(uint v)
		{
			this = default(v64);
			this.UInt1 = v;
			this.UInt0 = v;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000086C4 File Offset: 0x000068C4
		public v64(uint a, uint b)
		{
			this = default(v64);
			this.UInt0 = a;
			this.UInt1 = b;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000086DC File Offset: 0x000068DC
		public v64(float f)
		{
			this = default(v64);
			this.Float1 = f;
			this.Float0 = f;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00008700 File Offset: 0x00006900
		public v64(float a, float b)
		{
			this = default(v64);
			this.Float0 = a;
			this.Float1 = b;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00008717 File Offset: 0x00006917
		public v64(double a)
		{
			this = default(v64);
			this.Double0 = a;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00008727 File Offset: 0x00006927
		public v64(long a)
		{
			this = default(v64);
			this.SLong0 = a;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00008737 File Offset: 0x00006937
		public v64(ulong a)
		{
			this = default(v64);
			this.ULong0 = a;
		}

		// Token: 0x04000243 RID: 579
		[FieldOffset(0)]
		public byte Byte0;

		// Token: 0x04000244 RID: 580
		[FieldOffset(1)]
		public byte Byte1;

		// Token: 0x04000245 RID: 581
		[FieldOffset(2)]
		public byte Byte2;

		// Token: 0x04000246 RID: 582
		[FieldOffset(3)]
		public byte Byte3;

		// Token: 0x04000247 RID: 583
		[FieldOffset(4)]
		public byte Byte4;

		// Token: 0x04000248 RID: 584
		[FieldOffset(5)]
		public byte Byte5;

		// Token: 0x04000249 RID: 585
		[FieldOffset(6)]
		public byte Byte6;

		// Token: 0x0400024A RID: 586
		[FieldOffset(7)]
		public byte Byte7;

		// Token: 0x0400024B RID: 587
		[FieldOffset(0)]
		public sbyte SByte0;

		// Token: 0x0400024C RID: 588
		[FieldOffset(1)]
		public sbyte SByte1;

		// Token: 0x0400024D RID: 589
		[FieldOffset(2)]
		public sbyte SByte2;

		// Token: 0x0400024E RID: 590
		[FieldOffset(3)]
		public sbyte SByte3;

		// Token: 0x0400024F RID: 591
		[FieldOffset(4)]
		public sbyte SByte4;

		// Token: 0x04000250 RID: 592
		[FieldOffset(5)]
		public sbyte SByte5;

		// Token: 0x04000251 RID: 593
		[FieldOffset(6)]
		public sbyte SByte6;

		// Token: 0x04000252 RID: 594
		[FieldOffset(7)]
		public sbyte SByte7;

		// Token: 0x04000253 RID: 595
		[FieldOffset(0)]
		public ushort UShort0;

		// Token: 0x04000254 RID: 596
		[FieldOffset(2)]
		public ushort UShort1;

		// Token: 0x04000255 RID: 597
		[FieldOffset(4)]
		public ushort UShort2;

		// Token: 0x04000256 RID: 598
		[FieldOffset(6)]
		public ushort UShort3;

		// Token: 0x04000257 RID: 599
		[FieldOffset(0)]
		public short SShort0;

		// Token: 0x04000258 RID: 600
		[FieldOffset(2)]
		public short SShort1;

		// Token: 0x04000259 RID: 601
		[FieldOffset(4)]
		public short SShort2;

		// Token: 0x0400025A RID: 602
		[FieldOffset(6)]
		public short SShort3;

		// Token: 0x0400025B RID: 603
		[FieldOffset(0)]
		public uint UInt0;

		// Token: 0x0400025C RID: 604
		[FieldOffset(4)]
		public uint UInt1;

		// Token: 0x0400025D RID: 605
		[FieldOffset(0)]
		public int SInt0;

		// Token: 0x0400025E RID: 606
		[FieldOffset(4)]
		public int SInt1;

		// Token: 0x0400025F RID: 607
		[FieldOffset(0)]
		public ulong ULong0;

		// Token: 0x04000260 RID: 608
		[FieldOffset(0)]
		public long SLong0;

		// Token: 0x04000261 RID: 609
		[FieldOffset(0)]
		public float Float0;

		// Token: 0x04000262 RID: 610
		[FieldOffset(4)]
		public float Float1;

		// Token: 0x04000263 RID: 611
		[FieldOffset(0)]
		public double Double0;
	}
}
