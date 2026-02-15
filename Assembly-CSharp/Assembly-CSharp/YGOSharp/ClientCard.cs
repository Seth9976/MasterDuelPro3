using System;
using System.Collections.Generic;
using System.IO;

namespace YGOSharp
{
	// Token: 0x020001B1 RID: 433
	public class ClientCard
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0001FC4C File Offset: 0x0001DE4C
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x0001FC54 File Offset: 0x0001DE54
		public int Code { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0001FC5D File Offset: 0x0001DE5D
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x0001FC65 File Offset: 0x0001DE65
		public int Controler { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001FC6E File Offset: 0x0001DE6E
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0001FC76 File Offset: 0x0001DE76
		public int Location { get; private set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001FC7F File Offset: 0x0001DE7F
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x0001FC87 File Offset: 0x0001DE87
		public int Sequence { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001FC90 File Offset: 0x0001DE90
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x0001FC98 File Offset: 0x0001DE98
		public int Position { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001FCA1 File Offset: 0x0001DEA1
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x0001FCA9 File Offset: 0x0001DEA9
		public IList<ClientCard> Overlay { get; private set; }

		// Token: 0x06000680 RID: 1664 RVA: 0x0001FCB2 File Offset: 0x0001DEB2
		public ClientCard()
		{
			this.Overlay = new List<ClientCard>();
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001FCC5 File Offset: 0x0001DEC5
		public ClientCard(int code)
		{
			this.Overlay = new List<ClientCard>();
			this.Code = code;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001FCE0 File Offset: 0x0001DEE0
		public void Update(BinaryReader reader)
		{
			int flag = reader.ReadInt32();
			if ((flag & 1) != 0)
			{
				this.Code = reader.ReadInt32();
			}
			if ((flag & 2) != 0)
			{
				this.Controler = (int)reader.ReadByte();
				this.Location = (int)reader.ReadByte();
				this.Sequence = (int)reader.ReadByte();
				this.Position = (int)reader.ReadByte();
			}
			if ((flag & 4) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 8) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 16) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 32) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 64) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 128) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 256) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 512) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 1024) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 2048) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 4096) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 8192) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 16384) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 32768) != 0)
			{
				int count = reader.ReadInt32();
				for (int i = 0; i < count; i++)
				{
					reader.ReadInt32();
				}
			}
			if ((flag & 65536) != 0)
			{
				int count2 = reader.ReadInt32();
				this.Overlay.Clear();
				for (int j = 0; j < count2; j++)
				{
					ClientCard xyz = new ClientCard(reader.ReadInt32());
					this.Overlay.Add(xyz);
					xyz.Controler = this.Controler;
					xyz.Location = this.Location | 128;
					xyz.Sequence = this.Sequence;
					xyz.Position = 0;
				}
			}
			if ((flag & 131072) != 0)
			{
				int count3 = reader.ReadInt32();
				for (int k = 0; k < count3; k++)
				{
					reader.ReadInt32();
				}
			}
			if ((flag & 262144) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 524288) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 2097152) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 4194304) != 0)
			{
				reader.ReadInt32();
			}
			if ((flag & 8388608) != 0)
			{
				reader.ReadInt32();
				reader.ReadInt32();
			}
		}
	}
}
