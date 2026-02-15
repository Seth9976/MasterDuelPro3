using System;
using System.Text;

namespace Mono.Net.Dns
{
	// Token: 0x02000082 RID: 130
	internal class DnsHeader
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00008078 File Offset: 0x00006278
		public DnsHeader(byte[] bytes, int offset)
			: this(new ArraySegment<byte>(bytes, offset, 12))
		{
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008089 File Offset: 0x00006289
		public DnsHeader(ArraySegment<byte> segment)
		{
			if (segment.Count != 12)
			{
				throw new ArgumentException("Count must be 12", "segment");
			}
			this.bytes = segment;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000080B3 File Offset: 0x000062B3
		// (set) Token: 0x060001FB RID: 507 RVA: 0x000080F0 File Offset: 0x000062F0
		public ushort ID
		{
			get
			{
				return (ushort)((int)this.bytes.Array[this.bytes.Offset] * 256 + (int)this.bytes.Array[this.bytes.Offset + 1]);
			}
			set
			{
				this.bytes.Array[this.bytes.Offset] = (byte)((value & 65280) >> 8);
				this.bytes.Array[this.bytes.Offset + 1] = (byte)(value & 255);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000813F File Offset: 0x0000633F
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00008164 File Offset: 0x00006364
		public bool IsQuery
		{
			get
			{
				return (this.bytes.Array[2 + this.bytes.Offset] & 128) > 0;
			}
			set
			{
				if (!value)
				{
					byte[] array = this.bytes.Array;
					int num = 2 + this.bytes.Offset;
					array[num] |= 128;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 2 + this.bytes.Offset;
				array2[num2] &= 127;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001FE RID: 510 RVA: 0x000081C0 File Offset: 0x000063C0
		public DnsOpCode OpCode
		{
			get
			{
				return (DnsOpCode)((this.bytes.Array[2 + this.bytes.Offset] & 120) >> 3);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000081E1 File Offset: 0x000063E1
		public bool AuthoritativeAnswer
		{
			get
			{
				return (this.bytes.Array[2 + this.bytes.Offset] & 4) > 0;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00008201 File Offset: 0x00006401
		public bool Truncation
		{
			get
			{
				return (this.bytes.Array[2 + this.bytes.Offset] & 2) > 0;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00008221 File Offset: 0x00006421
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00008244 File Offset: 0x00006444
		public bool RecursionDesired
		{
			get
			{
				return (this.bytes.Array[2 + this.bytes.Offset] & 1) > 0;
			}
			set
			{
				if (value)
				{
					byte[] array = this.bytes.Array;
					int num = 2 + this.bytes.Offset;
					array[num] |= 1;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 2 + this.bytes.Offset;
				array2[num2] &= 254;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000829F File Offset: 0x0000649F
		public bool RecursionAvailable
		{
			get
			{
				return (this.bytes.Array[3 + this.bytes.Offset] & 128) > 0;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000082C3 File Offset: 0x000064C3
		public DnsRCode RCode
		{
			get
			{
				return (DnsRCode)(this.bytes.Array[3 + this.bytes.Offset] & 15);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000082E2 File Offset: 0x000064E2
		private static ushort GetUInt16(byte[] bytes, int offset)
		{
			return (ushort)((int)bytes[offset] * 256 + (int)bytes[offset + 1]);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000082F4 File Offset: 0x000064F4
		private static void SetUInt16(byte[] bytes, int offset, ushort val)
		{
			bytes[offset] = (byte)((val & 65280) >> 8);
			bytes[offset + 1] = (byte)(val & 255);
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00008310 File Offset: 0x00006510
		// (set) Token: 0x06000208 RID: 520 RVA: 0x00008323 File Offset: 0x00006523
		public ushort QuestionCount
		{
			get
			{
				return DnsHeader.GetUInt16(this.bytes.Array, 4);
			}
			set
			{
				DnsHeader.SetUInt16(this.bytes.Array, 4, value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00008337 File Offset: 0x00006537
		public ushort AnswerCount
		{
			get
			{
				return DnsHeader.GetUInt16(this.bytes.Array, 6);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000834A File Offset: 0x0000654A
		public ushort AuthorityCount
		{
			get
			{
				return DnsHeader.GetUInt16(this.bytes.Array, 8);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000835D File Offset: 0x0000655D
		public ushort AdditionalCount
		{
			get
			{
				return DnsHeader.GetUInt16(this.bytes.Array, 10);
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008374 File Offset: 0x00006574
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("ID: {0} QR: {1} Opcode: {2} AA: {3} TC: {4} RD: {5} RA: {6} \r\nRCode: {7} ", new object[] { this.ID, this.IsQuery, this.OpCode, this.AuthoritativeAnswer, this.Truncation, this.RecursionDesired, this.RecursionAvailable, this.RCode });
			stringBuilder.AppendFormat("Q: {0} A: {1} NS: {2} AR: {3}\r\n", new object[] { this.QuestionCount, this.AnswerCount, this.AuthorityCount, this.AdditionalCount });
			return stringBuilder.ToString();
		}

		// Token: 0x04000182 RID: 386
		private ArraySegment<byte> bytes;
	}
}
