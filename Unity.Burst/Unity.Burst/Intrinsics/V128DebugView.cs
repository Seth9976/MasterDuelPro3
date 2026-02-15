using System;
using System.Diagnostics;

namespace Unity.Burst.Intrinsics
{
	// Token: 0x02000034 RID: 52
	internal class V128DebugView
	{
		// Token: 0x06000A60 RID: 2656 RVA: 0x000067F9 File Offset: 0x000049F9
		public V128DebugView(v128 value)
		{
			this.m_Value = value;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00006808 File Offset: 0x00004A08
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public byte[] Byte
		{
			get
			{
				return new byte[]
				{
					this.m_Value.Byte0,
					this.m_Value.Byte1,
					this.m_Value.Byte2,
					this.m_Value.Byte3,
					this.m_Value.Byte4,
					this.m_Value.Byte5,
					this.m_Value.Byte6,
					this.m_Value.Byte7,
					this.m_Value.Byte8,
					this.m_Value.Byte9,
					this.m_Value.Byte10,
					this.m_Value.Byte11,
					this.m_Value.Byte12,
					this.m_Value.Byte13,
					this.m_Value.Byte14,
					this.m_Value.Byte15
				};
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00006904 File Offset: 0x00004B04
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public sbyte[] SByte
		{
			get
			{
				return new sbyte[]
				{
					this.m_Value.SByte0,
					this.m_Value.SByte1,
					this.m_Value.SByte2,
					this.m_Value.SByte3,
					this.m_Value.SByte4,
					this.m_Value.SByte5,
					this.m_Value.SByte6,
					this.m_Value.SByte7,
					this.m_Value.SByte8,
					this.m_Value.SByte9,
					this.m_Value.SByte10,
					this.m_Value.SByte11,
					this.m_Value.SByte12,
					this.m_Value.SByte13,
					this.m_Value.SByte14,
					this.m_Value.SByte15
				};
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00006A00 File Offset: 0x00004C00
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public ushort[] UShort
		{
			get
			{
				return new ushort[]
				{
					this.m_Value.UShort0,
					this.m_Value.UShort1,
					this.m_Value.UShort2,
					this.m_Value.UShort3,
					this.m_Value.UShort4,
					this.m_Value.UShort5,
					this.m_Value.UShort6,
					this.m_Value.UShort7
				};
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00006A84 File Offset: 0x00004C84
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public short[] SShort
		{
			get
			{
				return new short[]
				{
					this.m_Value.SShort0,
					this.m_Value.SShort1,
					this.m_Value.SShort2,
					this.m_Value.SShort3,
					this.m_Value.SShort4,
					this.m_Value.SShort5,
					this.m_Value.SShort6,
					this.m_Value.SShort7
				};
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00006B07 File Offset: 0x00004D07
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public uint[] UInt
		{
			get
			{
				return new uint[]
				{
					this.m_Value.UInt0,
					this.m_Value.UInt1,
					this.m_Value.UInt2,
					this.m_Value.UInt3
				};
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00006B47 File Offset: 0x00004D47
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public int[] SInt
		{
			get
			{
				return new int[]
				{
					this.m_Value.SInt0,
					this.m_Value.SInt1,
					this.m_Value.SInt2,
					this.m_Value.SInt3
				};
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00006B87 File Offset: 0x00004D87
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public float[] Float
		{
			get
			{
				return new float[]
				{
					this.m_Value.Float0,
					this.m_Value.Float1,
					this.m_Value.Float2,
					this.m_Value.Float3
				};
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00006BC7 File Offset: 0x00004DC7
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public long[] SLong
		{
			get
			{
				return new long[]
				{
					this.m_Value.SLong0,
					this.m_Value.SLong1
				};
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00006BEB File Offset: 0x00004DEB
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public ulong[] ULong
		{
			get
			{
				return new ulong[]
				{
					this.m_Value.ULong0,
					this.m_Value.ULong1
				};
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00006C0F File Offset: 0x00004E0F
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public double[] Double
		{
			get
			{
				return new double[]
				{
					this.m_Value.Double0,
					this.m_Value.Double1
				};
			}
		}

		// Token: 0x04000177 RID: 375
		private v128 m_Value;
	}
}
