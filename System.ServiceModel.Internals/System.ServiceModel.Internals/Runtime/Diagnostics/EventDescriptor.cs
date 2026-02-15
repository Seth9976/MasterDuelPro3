using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000037 RID: 55
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct EventDescriptor
	{
		// Token: 0x06000130 RID: 304 RVA: 0x0000614C File Offset: 0x0000434C
		public EventDescriptor(int id, byte version, byte channel, byte level, byte opcode, int task, long keywords)
		{
			if (id < 0)
			{
				throw Fx.Exception.ArgumentOutOfRange("id", id, "Value Must Be Non Negative");
			}
			if (id > 65535)
			{
				throw Fx.Exception.ArgumentOutOfRange("id", id, string.Empty);
			}
			this.m_id = (ushort)id;
			this.m_version = version;
			this.m_channel = channel;
			this.m_level = level;
			this.m_opcode = opcode;
			this.m_keywords = keywords;
			if (task < 0)
			{
				throw Fx.Exception.ArgumentOutOfRange("task", task, "Value Must Be Non Negative");
			}
			if (task > 65535)
			{
				throw Fx.Exception.ArgumentOutOfRange("task", task, string.Empty);
			}
			this.m_task = (ushort)task;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006218 File Offset: 0x00004418
		public int EventId
		{
			get
			{
				return (int)this.m_id;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006220 File Offset: 0x00004420
		public byte Channel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00006228 File Offset: 0x00004428
		public byte Level
		{
			get
			{
				return this.m_level;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00006230 File Offset: 0x00004430
		public byte Opcode
		{
			get
			{
				return this.m_opcode;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00006238 File Offset: 0x00004438
		public long Keywords
		{
			get
			{
				return this.m_keywords;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006240 File Offset: 0x00004440
		public override bool Equals(object obj)
		{
			return obj is EventDescriptor && this.Equals((EventDescriptor)obj);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006258 File Offset: 0x00004458
		public override int GetHashCode()
		{
			return (int)(this.m_id ^ (ushort)this.m_version ^ (ushort)this.m_channel ^ (ushort)this.m_level ^ (ushort)this.m_opcode ^ this.m_task) ^ (int)this.m_keywords;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000628C File Offset: 0x0000448C
		public bool Equals(EventDescriptor other)
		{
			return this.m_id == other.m_id && this.m_version == other.m_version && this.m_channel == other.m_channel && this.m_level == other.m_level && this.m_opcode == other.m_opcode && this.m_task == other.m_task && this.m_keywords == other.m_keywords;
		}

		// Token: 0x04000086 RID: 134
		[FieldOffset(0)]
		private ushort m_id;

		// Token: 0x04000087 RID: 135
		[FieldOffset(2)]
		private byte m_version;

		// Token: 0x04000088 RID: 136
		[FieldOffset(3)]
		private byte m_channel;

		// Token: 0x04000089 RID: 137
		[FieldOffset(4)]
		private byte m_level;

		// Token: 0x0400008A RID: 138
		[FieldOffset(5)]
		private byte m_opcode;

		// Token: 0x0400008B RID: 139
		[FieldOffset(6)]
		private ushort m_task;

		// Token: 0x0400008C RID: 140
		[FieldOffset(8)]
		private long m_keywords;
	}
}
