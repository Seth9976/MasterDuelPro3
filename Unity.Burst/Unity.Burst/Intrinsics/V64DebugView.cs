using System;
using System.Diagnostics;

namespace Unity.Burst.Intrinsics
{
	// Token: 0x02000033 RID: 51
	internal class V64DebugView
	{
		// Token: 0x06000A55 RID: 2645 RVA: 0x000065B4 File Offset: 0x000047B4
		public V64DebugView(v64 value)
		{
			this.m_Value = value;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x000065C4 File Offset: 0x000047C4
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
					this.m_Value.Byte7
				};
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x00006648 File Offset: 0x00004848
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
					this.m_Value.SByte7
				};
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x000066CB File Offset: 0x000048CB
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
					this.m_Value.UShort3
				};
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0000670B File Offset: 0x0000490B
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
					this.m_Value.SShort3
				};
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0000674B File Offset: 0x0000494B
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public uint[] UInt
		{
			get
			{
				return new uint[]
				{
					this.m_Value.UInt0,
					this.m_Value.UInt1
				};
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0000676F File Offset: 0x0000496F
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public int[] SInt
		{
			get
			{
				return new int[]
				{
					this.m_Value.SInt0,
					this.m_Value.SInt1
				};
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00006793 File Offset: 0x00004993
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public float[] Float
		{
			get
			{
				return new float[]
				{
					this.m_Value.Float0,
					this.m_Value.Float1
				};
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x000067B7 File Offset: 0x000049B7
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public long[] SLong
		{
			get
			{
				return new long[] { this.m_Value.SLong0 };
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x000067CD File Offset: 0x000049CD
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public ulong[] ULong
		{
			get
			{
				return new ulong[] { this.m_Value.ULong0 };
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x000067E3 File Offset: 0x000049E3
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public double[] Double
		{
			get
			{
				return new double[] { this.m_Value.Double0 };
			}
		}

		// Token: 0x04000176 RID: 374
		private v64 m_Value;
	}
}
