using System;

namespace Spine
{
	// Token: 0x02000053 RID: 83
	public class RegionAttachment : Attachment, IHasTextureRegion
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000AC17 File Offset: 0x00008E17
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000AC1F File Offset: 0x00008E1F
		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000AC28 File Offset: 0x00008E28
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000AC30 File Offset: 0x00008E30
		public float Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000AC39 File Offset: 0x00008E39
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000AC41 File Offset: 0x00008E41
		public float Rotation
		{
			get
			{
				return this.rotation;
			}
			set
			{
				this.rotation = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000AC4A File Offset: 0x00008E4A
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000AC52 File Offset: 0x00008E52
		public float ScaleX
		{
			get
			{
				return this.scaleX;
			}
			set
			{
				this.scaleX = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000AC5B File Offset: 0x00008E5B
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000AC63 File Offset: 0x00008E63
		public float ScaleY
		{
			get
			{
				return this.scaleY;
			}
			set
			{
				this.scaleY = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000AC6C File Offset: 0x00008E6C
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000AC74 File Offset: 0x00008E74
		public float Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000AC7D File Offset: 0x00008E7D
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000AC85 File Offset: 0x00008E85
		public float Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000AC8E File Offset: 0x00008E8E
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000AC96 File Offset: 0x00008E96
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

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000AC9F File Offset: 0x00008E9F
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000ACA7 File Offset: 0x00008EA7
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

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000ACB8 File Offset: 0x00008EB8
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

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000ACC1 File Offset: 0x00008EC1
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000ACC9 File Offset: 0x00008EC9
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

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000ACD2 File Offset: 0x00008ED2
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000ACDA File Offset: 0x00008EDA
		public string Path { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000ACE3 File Offset: 0x00008EE3
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000ACEB File Offset: 0x00008EEB
		public TextureRegion Region
		{
			get
			{
				return this.region;
			}
			set
			{
				this.region = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000ACF4 File Offset: 0x00008EF4
		public float[] Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000ACFC File Offset: 0x00008EFC
		public float[] UVs
		{
			get
			{
				return this.uvs;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000AD04 File Offset: 0x00008F04
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000AD0C File Offset: 0x00008F0C
		public Sequence Sequence
		{
			get
			{
				return this.sequence;
			}
			set
			{
				this.sequence = value;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000AD18 File Offset: 0x00008F18
		public RegionAttachment(string name)
			: base(name)
		{
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000AD88 File Offset: 0x00008F88
		public RegionAttachment(RegionAttachment other)
			: base(other)
		{
			this.region = other.region;
			this.Path = other.Path;
			this.x = other.x;
			this.y = other.y;
			this.scaleX = other.scaleX;
			this.scaleY = other.scaleY;
			this.rotation = other.rotation;
			this.width = other.width;
			this.height = other.height;
			Array.Copy(other.uvs, 0, this.uvs, 0, 8);
			Array.Copy(other.offset, 0, this.offset, 0, 8);
			this.r = other.r;
			this.g = other.g;
			this.b = other.b;
			this.a = other.a;
			this.sequence = ((other.sequence == null) ? null : new Sequence(other.sequence));
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000AED8 File Offset: 0x000090D8
		public void UpdateRegion()
		{
			float[] uvs = this.uvs;
			if (this.region == null)
			{
				uvs[0] = 0f;
				uvs[1] = 0f;
				uvs[2] = 0f;
				uvs[3] = 1f;
				uvs[4] = 1f;
				uvs[5] = 1f;
				uvs[6] = 1f;
				uvs[7] = 0f;
				return;
			}
			float width = this.Width;
			float height = this.Height;
			float localX2 = width / 2f;
			float localY2 = height / 2f;
			float localX3 = -localX2;
			float localY3 = -localY2;
			bool rotated = false;
			if (this.region is AtlasRegion)
			{
				AtlasRegion region = (AtlasRegion)this.region;
				localX3 += region.offsetX / (float)region.originalWidth * width;
				localY3 += region.offsetY / (float)region.originalHeight * height;
				if (region.degrees == 90)
				{
					rotated = true;
					localX2 -= ((float)region.originalWidth - region.offsetX - (float)region.packedHeight) / (float)region.originalWidth * width;
					localY2 -= ((float)region.originalHeight - region.offsetY - (float)region.packedWidth) / (float)region.originalHeight * height;
				}
				else
				{
					localX2 -= ((float)region.originalWidth - region.offsetX - (float)region.packedWidth) / (float)region.originalWidth * width;
					localY2 -= ((float)region.originalHeight - region.offsetY - (float)region.packedHeight) / (float)region.originalHeight * height;
				}
			}
			float scaleX = this.ScaleX;
			float scaleY = this.ScaleY;
			localX3 *= scaleX;
			localY3 *= scaleY;
			localX2 *= scaleX;
			localY2 *= scaleY;
			float num = this.Rotation * 0.017453292f;
			float cos = (float)Math.Cos((double)num);
			float sin = (float)Math.Sin((double)num);
			float x = this.X;
			float y = this.Y;
			float localXCos = localX3 * cos + x;
			float localXSin = localX3 * sin;
			float localYCos = localY3 * cos + y;
			float localYSin = localY3 * sin;
			float localX2Cos = localX2 * cos + x;
			float localX2Sin = localX2 * sin;
			float localY2Cos = localY2 * cos + y;
			float localY2Sin = localY2 * sin;
			float[] array = this.offset;
			array[0] = localXCos - localYSin;
			array[1] = localYCos + localXSin;
			array[2] = localXCos - localY2Sin;
			array[3] = localY2Cos + localXSin;
			array[4] = localX2Cos - localY2Sin;
			array[5] = localY2Cos + localX2Sin;
			array[6] = localX2Cos - localYSin;
			array[7] = localYCos + localX2Sin;
			if (rotated)
			{
				uvs[0] = this.region.u2;
				uvs[1] = this.region.v;
				uvs[2] = this.region.u2;
				uvs[3] = this.region.v2;
				uvs[4] = this.region.u;
				uvs[5] = this.region.v2;
				uvs[6] = this.region.u;
				uvs[7] = this.region.v;
				return;
			}
			uvs[0] = this.region.u2;
			uvs[1] = this.region.v2;
			uvs[2] = this.region.u;
			uvs[3] = this.region.v2;
			uvs[4] = this.region.u;
			uvs[5] = this.region.v;
			uvs[6] = this.region.u2;
			uvs[7] = this.region.v;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000B220 File Offset: 0x00009420
		public void ComputeWorldVertices(Slot slot, float[] worldVertices, int offset, int stride = 2)
		{
			if (this.sequence != null)
			{
				this.sequence.Apply(slot, this);
			}
			float[] array = this.offset;
			Bone bone = slot.Bone;
			float bwx = bone.worldX;
			float bwy = bone.worldY;
			float a = bone.a;
			float b = bone.b;
			float c = bone.c;
			float d = bone.d;
			float offsetX = array[6];
			float offsetY = array[7];
			worldVertices[offset] = offsetX * a + offsetY * b + bwx;
			worldVertices[offset + 1] = offsetX * c + offsetY * d + bwy;
			offset += stride;
			offsetX = array[0];
			offsetY = array[1];
			worldVertices[offset] = offsetX * a + offsetY * b + bwx;
			worldVertices[offset + 1] = offsetX * c + offsetY * d + bwy;
			offset += stride;
			offsetX = array[2];
			offsetY = array[3];
			worldVertices[offset] = offsetX * a + offsetY * b + bwx;
			worldVertices[offset + 1] = offsetX * c + offsetY * d + bwy;
			offset += stride;
			offsetX = array[4];
			offsetY = array[5];
			worldVertices[offset] = offsetX * a + offsetY * b + bwx;
			worldVertices[offset + 1] = offsetX * c + offsetY * d + bwy;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000B332 File Offset: 0x00009532
		public override Attachment Copy()
		{
			return new RegionAttachment(this);
		}

		// Token: 0x0400014B RID: 331
		public const int BLX = 0;

		// Token: 0x0400014C RID: 332
		public const int BLY = 1;

		// Token: 0x0400014D RID: 333
		public const int ULX = 2;

		// Token: 0x0400014E RID: 334
		public const int ULY = 3;

		// Token: 0x0400014F RID: 335
		public const int URX = 4;

		// Token: 0x04000150 RID: 336
		public const int URY = 5;

		// Token: 0x04000151 RID: 337
		public const int BRX = 6;

		// Token: 0x04000152 RID: 338
		public const int BRY = 7;

		// Token: 0x04000153 RID: 339
		internal TextureRegion region;

		// Token: 0x04000154 RID: 340
		internal float x;

		// Token: 0x04000155 RID: 341
		internal float y;

		// Token: 0x04000156 RID: 342
		internal float rotation;

		// Token: 0x04000157 RID: 343
		internal float scaleX = 1f;

		// Token: 0x04000158 RID: 344
		internal float scaleY = 1f;

		// Token: 0x04000159 RID: 345
		internal float width;

		// Token: 0x0400015A RID: 346
		internal float height;

		// Token: 0x0400015B RID: 347
		internal float[] offset = new float[8];

		// Token: 0x0400015C RID: 348
		internal float[] uvs = new float[8];

		// Token: 0x0400015D RID: 349
		internal float r = 1f;

		// Token: 0x0400015E RID: 350
		internal float g = 1f;

		// Token: 0x0400015F RID: 351
		internal float b = 1f;

		// Token: 0x04000160 RID: 352
		internal float a = 1f;

		// Token: 0x04000161 RID: 353
		internal Sequence sequence;
	}
}
