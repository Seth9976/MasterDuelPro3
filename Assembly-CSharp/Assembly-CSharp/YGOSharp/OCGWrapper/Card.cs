using System;
using System.Data;
using YGOSharp.OCGWrapper.Enums;

namespace YGOSharp.OCGWrapper
{
	// Token: 0x020001C7 RID: 455
	public class Card
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0002596B File Offset: 0x00023B6B
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x00025973 File Offset: 0x00023B73
		public int Id { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0002597C File Offset: 0x00023B7C
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x00025984 File Offset: 0x00023B84
		public int Ot { get; private set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0002598D File Offset: 0x00023B8D
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x00025995 File Offset: 0x00023B95
		public int Alias { get; private set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0002599E File Offset: 0x00023B9E
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x000259A6 File Offset: 0x00023BA6
		public long Setcode { get; private set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x000259AF File Offset: 0x00023BAF
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x000259B7 File Offset: 0x00023BB7
		public int Type { get; private set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x000259C0 File Offset: 0x00023BC0
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x000259C8 File Offset: 0x00023BC8
		public int Level { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x000259D1 File Offset: 0x00023BD1
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x000259D9 File Offset: 0x00023BD9
		public int LScale { get; private set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x000259E2 File Offset: 0x00023BE2
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x000259EA File Offset: 0x00023BEA
		public int RScale { get; private set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x000259F3 File Offset: 0x00023BF3
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x000259FB File Offset: 0x00023BFB
		public int LinkMarker { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00025A04 File Offset: 0x00023C04
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x00025A0C File Offset: 0x00023C0C
		public int Attribute { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00025A15 File Offset: 0x00023C15
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x00025A1D File Offset: 0x00023C1D
		public int Race { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00025A26 File Offset: 0x00023C26
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x00025A2E File Offset: 0x00023C2E
		public int Attack { get; private set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x00025A37 File Offset: 0x00023C37
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x00025A3F File Offset: 0x00023C3F
		public int Defense { get; private set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x00025A48 File Offset: 0x00023C48
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x00025A50 File Offset: 0x00023C50
		internal Card.CardData Data { get; private set; }

		// Token: 0x060007FF RID: 2047 RVA: 0x00025A59 File Offset: 0x00023C59
		public static Card Get(int id)
		{
			return CardsManager.GetCard(id);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00025A61 File Offset: 0x00023C61
		public bool HasType(CardType type)
		{
			return (this.Type & (int)type) != 0;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00025A6E File Offset: 0x00023C6E
		public bool IsExtraCard()
		{
			return this.HasType(CardType.Fusion) || this.HasType(CardType.Synchro) || this.HasType(CardType.Xyz) || this.HasType(CardType.Link);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00025AA4 File Offset: 0x00023CA4
		internal Card(IDataRecord reader)
		{
			this.Id = reader.GetInt32(0);
			this.Ot = reader.GetInt32(1);
			this.Alias = reader.GetInt32(2);
			this.Setcode = reader.GetInt64(3);
			this.Type = reader.GetInt32(4);
			int levelInfo = reader.GetInt32(5);
			this.Level = levelInfo & 255;
			this.LScale = (levelInfo >> 24) & 255;
			this.RScale = (levelInfo >> 16) & 255;
			this.Race = reader.GetInt32(6);
			this.Attribute = reader.GetInt32(7);
			this.Attack = reader.GetInt32(8);
			this.Defense = reader.GetInt32(9);
			if (this.HasType(CardType.Link))
			{
				this.LinkMarker = this.Defense;
				this.Defense = 0;
			}
			this.Data = new Card.CardData
			{
				Code = this.Id,
				Alias = this.Alias,
				Setcode = this.Setcode,
				Type = this.Type,
				Level = this.Level,
				Attribute = this.Attribute,
				Race = this.Race,
				Attack = this.Attack,
				Defense = this.Defense,
				LScale = this.LScale,
				RScale = this.RScale,
				LinkMarker = this.LinkMarker
			};
		}

		// Token: 0x020001C8 RID: 456
		public struct CardData
		{
			// Token: 0x04000BB5 RID: 2997
			public int Code;

			// Token: 0x04000BB6 RID: 2998
			public int Alias;

			// Token: 0x04000BB7 RID: 2999
			public long Setcode;

			// Token: 0x04000BB8 RID: 3000
			public int Type;

			// Token: 0x04000BB9 RID: 3001
			public int Level;

			// Token: 0x04000BBA RID: 3002
			public int Attribute;

			// Token: 0x04000BBB RID: 3003
			public int Race;

			// Token: 0x04000BBC RID: 3004
			public int Attack;

			// Token: 0x04000BBD RID: 3005
			public int Defense;

			// Token: 0x04000BBE RID: 3006
			public int LScale;

			// Token: 0x04000BBF RID: 3007
			public int RScale;

			// Token: 0x04000BC0 RID: 3008
			public int LinkMarker;
		}
	}
}
