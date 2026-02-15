using System;

namespace Spine
{
	// Token: 0x02000050 RID: 80
	public class MeshAttachment : VertexAttachment, IHasTextureRegion
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000A343 File Offset: 0x00008543
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000A34B File Offset: 0x0000854B
		public TextureRegion Region
		{
			get
			{
				return this.region;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("region", "region cannot be null.");
				}
				this.region = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000A367 File Offset: 0x00008567
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000A36F File Offset: 0x0000856F
		public int HullLength
		{
			get
			{
				return this.hullLength;
			}
			set
			{
				this.hullLength = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000A378 File Offset: 0x00008578
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000A380 File Offset: 0x00008580
		public float[] RegionUVs
		{
			get
			{
				return this.regionUVs;
			}
			set
			{
				this.regionUVs = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000A389 File Offset: 0x00008589
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000A391 File Offset: 0x00008591
		public float[] UVs
		{
			get
			{
				return this.uvs;
			}
			set
			{
				this.uvs = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000A39A File Offset: 0x0000859A
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000A3A2 File Offset: 0x000085A2
		public int[] Triangles
		{
			get
			{
				return this.triangles;
			}
			set
			{
				this.triangles = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000A3AB File Offset: 0x000085AB
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000A3B3 File Offset: 0x000085B3
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

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000A3BC File Offset: 0x000085BC
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000A3C4 File Offset: 0x000085C4
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

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000A3CD File Offset: 0x000085CD
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000A3D5 File Offset: 0x000085D5
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

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000A3DE File Offset: 0x000085DE
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x0000A3E6 File Offset: 0x000085E6
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

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000A3EF File Offset: 0x000085EF
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000A3F7 File Offset: 0x000085F7
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000A400 File Offset: 0x00008600
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000A408 File Offset: 0x00008608
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

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000A411 File Offset: 0x00008611
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000A41C File Offset: 0x0000861C
		public MeshAttachment ParentMesh
		{
			get
			{
				return this.parentMesh;
			}
			set
			{
				this.parentMesh = value;
				if (value != null)
				{
					this.bones = value.bones;
					this.vertices = value.vertices;
					this.worldVerticesLength = value.worldVerticesLength;
					this.regionUVs = value.regionUVs;
					this.triangles = value.triangles;
					this.HullLength = value.HullLength;
					this.Edges = value.Edges;
					this.Width = value.Width;
					this.Height = value.Height;
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000A49F File Offset: 0x0000869F
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000A4A7 File Offset: 0x000086A7
		public int[] Edges { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000A4B0 File Offset: 0x000086B0
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000A4B8 File Offset: 0x000086B8
		public float Width { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000A4C1 File Offset: 0x000086C1
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0000A4C9 File Offset: 0x000086C9
		public float Height { get; set; }

		// Token: 0x06000200 RID: 512 RVA: 0x0000A4D2 File Offset: 0x000086D2
		public MeshAttachment(string name)
			: base(name)
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A508 File Offset: 0x00008708
		protected MeshAttachment(MeshAttachment other)
			: base(other)
		{
			if (this.parentMesh != null)
			{
				throw new ArgumentException("Use newLinkedMesh to copy a linked mesh.");
			}
			this.region = other.region;
			this.path = other.path;
			this.r = other.r;
			this.g = other.g;
			this.b = other.b;
			this.a = other.a;
			this.regionUVs = new float[other.regionUVs.Length];
			Array.Copy(other.regionUVs, 0, this.regionUVs, 0, this.regionUVs.Length);
			this.uvs = new float[other.uvs.Length];
			Array.Copy(other.uvs, 0, this.uvs, 0, this.uvs.Length);
			this.triangles = new int[other.triangles.Length];
			Array.Copy(other.triangles, 0, this.triangles, 0, this.triangles.Length);
			this.hullLength = other.hullLength;
			this.sequence = ((other.sequence == null) ? null : new Sequence(other.sequence));
			if (other.Edges != null)
			{
				this.Edges = new int[other.Edges.Length];
				Array.Copy(other.Edges, 0, this.Edges, 0, this.Edges.Length);
			}
			this.Width = other.Width;
			this.Height = other.Height;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A6A4 File Offset: 0x000088A4
		public void UpdateRegion()
		{
			float[] regionUVs = this.regionUVs;
			if (this.uvs == null || this.uvs.Length != regionUVs.Length)
			{
				this.uvs = new float[regionUVs.Length];
			}
			float[] uvs = this.uvs;
			int i = uvs.Length;
			float u;
			float v;
			float width;
			float height;
			if (this.region is AtlasRegion)
			{
				u = this.region.u;
				v = this.region.v;
				AtlasRegion region = (AtlasRegion)this.region;
				float textureWidth = (float)this.region.width / (region.u2 - region.u);
				float textureHeight = (float)this.region.height / (region.v2 - region.v);
				int degrees = region.degrees;
				if (degrees == 90)
				{
					u -= ((float)region.originalHeight - region.offsetY - (float)region.packedWidth) / textureWidth;
					v -= ((float)region.originalWidth - region.offsetX - (float)region.packedHeight) / textureHeight;
					width = (float)region.originalHeight / textureWidth;
					height = (float)region.originalWidth / textureHeight;
					for (int j = 0; j < i; j += 2)
					{
						uvs[j] = u + regionUVs[j + 1] * width;
						uvs[j + 1] = v + (1f - regionUVs[j]) * height;
					}
					return;
				}
				if (degrees == 180)
				{
					u -= ((float)region.originalWidth - region.offsetX - (float)region.packedWidth) / textureWidth;
					v -= region.offsetY / textureHeight;
					width = (float)region.originalWidth / textureWidth;
					height = (float)region.originalHeight / textureHeight;
					for (int k = 0; k < i; k += 2)
					{
						uvs[k] = u + (1f - regionUVs[k]) * width;
						uvs[k + 1] = v + (1f - regionUVs[k + 1]) * height;
					}
					return;
				}
				if (degrees == 270)
				{
					u -= region.offsetY / textureWidth;
					v -= region.offsetX / textureHeight;
					width = (float)region.originalHeight / textureWidth;
					height = (float)region.originalWidth / textureHeight;
					for (int l = 0; l < i; l += 2)
					{
						uvs[l] = u + (1f - regionUVs[l + 1]) * width;
						uvs[l + 1] = v + regionUVs[l] * height;
					}
					return;
				}
				u -= region.offsetX / textureWidth;
				v -= ((float)region.originalHeight - region.offsetY - (float)region.packedHeight) / textureHeight;
				width = (float)region.originalWidth / textureWidth;
				height = (float)region.originalHeight / textureHeight;
			}
			else if (this.region == null)
			{
				v = (u = 0f);
				height = (width = 1f);
			}
			else
			{
				u = this.region.u;
				v = this.region.v;
				width = this.region.u2 - u;
				height = this.region.v2 - v;
			}
			for (int m = 0; m < i; m += 2)
			{
				uvs[m] = u + regionUVs[m] * width;
				uvs[m + 1] = v + regionUVs[m + 1] * height;
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000A9D2 File Offset: 0x00008BD2
		public override void ComputeWorldVertices(Slot slot, int start, int count, float[] worldVertices, int offset, int stride = 2)
		{
			if (this.sequence != null)
			{
				this.sequence.Apply(slot, this);
			}
			base.ComputeWorldVertices(slot, start, count, worldVertices, offset, stride);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000A9F8 File Offset: 0x00008BF8
		public MeshAttachment NewLinkedMesh()
		{
			MeshAttachment mesh = new MeshAttachment(base.Name);
			mesh.timelineAttachment = this.timelineAttachment;
			mesh.region = this.region;
			mesh.path = this.path;
			mesh.r = this.r;
			mesh.g = this.g;
			mesh.b = this.b;
			mesh.a = this.a;
			mesh.ParentMesh = ((this.parentMesh != null) ? this.parentMesh : this);
			if (mesh.Region != null)
			{
				mesh.UpdateRegion();
			}
			return mesh;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000AA8B File Offset: 0x00008C8B
		public override Attachment Copy()
		{
			if (this.parentMesh == null)
			{
				return new MeshAttachment(this);
			}
			return this.NewLinkedMesh();
		}

		// Token: 0x04000136 RID: 310
		internal TextureRegion region;

		// Token: 0x04000137 RID: 311
		internal string path;

		// Token: 0x04000138 RID: 312
		internal float[] regionUVs;

		// Token: 0x04000139 RID: 313
		internal float[] uvs;

		// Token: 0x0400013A RID: 314
		internal int[] triangles;

		// Token: 0x0400013B RID: 315
		internal float r = 1f;

		// Token: 0x0400013C RID: 316
		internal float g = 1f;

		// Token: 0x0400013D RID: 317
		internal float b = 1f;

		// Token: 0x0400013E RID: 318
		internal float a = 1f;

		// Token: 0x0400013F RID: 319
		internal int hullLength;

		// Token: 0x04000140 RID: 320
		private MeshAttachment parentMesh;

		// Token: 0x04000141 RID: 321
		private Sequence sequence;
	}
}
