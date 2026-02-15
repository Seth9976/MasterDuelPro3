using System;

namespace Spine
{
	// Token: 0x02000082 RID: 130
	public class SlotData
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001BFCD File Offset: 0x0001A1CD
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0001BFD5 File Offset: 0x0001A1D5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0001BFDD File Offset: 0x0001A1DD
		public BoneData BoneData
		{
			get
			{
				return this.boneData;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0001BFE5 File Offset: 0x0001A1E5
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x0001BFED File Offset: 0x0001A1ED
		public float R
		{
			get
			{
				return this.r;
			}
			set
			{
				this.r = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0001BFF6 File Offset: 0x0001A1F6
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x0001BFFE File Offset: 0x0001A1FE
		public float G
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0001C007 File Offset: 0x0001A207
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x0001C00F File Offset: 0x0001A20F
		public float B
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0001C018 File Offset: 0x0001A218
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x0001C020 File Offset: 0x0001A220
		public float A
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0001C029 File Offset: 0x0001A229
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x0001C031 File Offset: 0x0001A231
		public float R2
		{
			get
			{
				return this.r2;
			}
			set
			{
				this.r2 = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0001C03A File Offset: 0x0001A23A
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x0001C042 File Offset: 0x0001A242
		public float G2
		{
			get
			{
				return this.g2;
			}
			set
			{
				this.g2 = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0001C04B File Offset: 0x0001A24B
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x0001C053 File Offset: 0x0001A253
		public float B2
		{
			get
			{
				return this.b2;
			}
			set
			{
				this.b2 = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0001C05C File Offset: 0x0001A25C
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x0001C064 File Offset: 0x0001A264
		public bool HasSecondColor
		{
			get
			{
				return this.hasSecondColor;
			}
			set
			{
				this.hasSecondColor = value;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0001C06D File Offset: 0x0001A26D
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x0001C075 File Offset: 0x0001A275
		public string AttachmentName
		{
			get
			{
				return this.attachmentName;
			}
			set
			{
				this.attachmentName = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0001C07E File Offset: 0x0001A27E
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x0001C086 File Offset: 0x0001A286
		public BlendMode BlendMode
		{
			get
			{
				return this.blendMode;
			}
			set
			{
				this.blendMode = value;
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001C090 File Offset: 0x0001A290
		public SlotData(int index, string name, BoneData boneData)
		{
			if (index < 0)
			{
				throw new ArgumentException("index must be >= 0.", "index");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name", "name cannot be null.");
			}
			if (boneData == null)
			{
				throw new ArgumentNullException("boneData", "boneData cannot be null.");
			}
			this.index = index;
			this.name = name;
			this.boneData = boneData;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001BFD5 File Offset: 0x0001A1D5
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x040002CD RID: 717
		internal int index;

		// Token: 0x040002CE RID: 718
		internal string name;

		// Token: 0x040002CF RID: 719
		internal BoneData boneData;

		// Token: 0x040002D0 RID: 720
		internal float r = 1f;

		// Token: 0x040002D1 RID: 721
		internal float g = 1f;

		// Token: 0x040002D2 RID: 722
		internal float b = 1f;

		// Token: 0x040002D3 RID: 723
		internal float a = 1f;

		// Token: 0x040002D4 RID: 724
		internal float r2;

		// Token: 0x040002D5 RID: 725
		internal float g2;

		// Token: 0x040002D6 RID: 726
		internal float b2;

		// Token: 0x040002D7 RID: 727
		internal bool hasSecondColor;

		// Token: 0x040002D8 RID: 728
		internal string attachmentName;

		// Token: 0x040002D9 RID: 729
		internal BlendMode blendMode;
	}
}
