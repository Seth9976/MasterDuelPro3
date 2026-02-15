using System;

namespace Spine
{
	// Token: 0x02000056 RID: 86
	public abstract class VertexAttachment : Attachment
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000B552 File Offset: 0x00009752
		public int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000B55A File Offset: 0x0000975A
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000B562 File Offset: 0x00009762
		public int[] Bones
		{
			get
			{
				return this.bones;
			}
			set
			{
				this.bones = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000B56B File Offset: 0x0000976B
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000B573 File Offset: 0x00009773
		public float[] Vertices
		{
			get
			{
				return this.vertices;
			}
			set
			{
				this.vertices = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000B57C File Offset: 0x0000977C
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000B584 File Offset: 0x00009784
		public int WorldVerticesLength
		{
			get
			{
				return this.worldVerticesLength;
			}
			set
			{
				this.worldVerticesLength = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000B58D File Offset: 0x0000978D
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000B595 File Offset: 0x00009795
		public VertexAttachment TimelineAttachment
		{
			get
			{
				return this.timelineAttachment;
			}
			set
			{
				this.timelineAttachment = value;
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B5A0 File Offset: 0x000097A0
		public VertexAttachment(string name)
			: base(name)
		{
			object obj = VertexAttachment.nextIdLock;
			lock (obj)
			{
				this.id = VertexAttachment.nextID++;
			}
			this.timelineAttachment = this;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000B5FC File Offset: 0x000097FC
		public VertexAttachment(VertexAttachment other)
			: base(other)
		{
			object obj = VertexAttachment.nextIdLock;
			lock (obj)
			{
				this.id = VertexAttachment.nextID++;
			}
			this.timelineAttachment = other.timelineAttachment;
			if (other.bones != null)
			{
				this.bones = new int[other.bones.Length];
				Array.Copy(other.bones, 0, this.bones, 0, this.bones.Length);
			}
			else
			{
				this.bones = null;
			}
			if (other.vertices != null)
			{
				this.vertices = new float[other.vertices.Length];
				Array.Copy(other.vertices, 0, this.vertices, 0, this.vertices.Length);
			}
			else
			{
				this.vertices = null;
			}
			this.worldVerticesLength = other.worldVerticesLength;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B6E8 File Offset: 0x000098E8
		public void ComputeWorldVertices(Slot slot, float[] worldVertices)
		{
			this.ComputeWorldVertices(slot, 0, this.worldVerticesLength, worldVertices, 0, 2);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B6FC File Offset: 0x000098FC
		public virtual void ComputeWorldVertices(Slot slot, int start, int count, float[] worldVertices, int offset, int stride = 2)
		{
			count = offset + (count >> 1) * stride;
			ExposedList<float> deformArray = slot.deform;
			float[] vertices = this.vertices;
			int[] bones = this.bones;
			if (bones == null)
			{
				if (deformArray.Count > 0)
				{
					vertices = deformArray.Items;
				}
				Bone bone3 = slot.bone;
				float x = bone3.worldX;
				float y = bone3.worldY;
				float a = bone3.a;
				float b = bone3.b;
				float c = bone3.c;
				float d = bone3.d;
				int vv = start;
				for (int w = offset; w < count; w += stride)
				{
					float vx = vertices[vv];
					float vy = vertices[vv + 1];
					worldVertices[w] = vx * a + vy * b + x;
					worldVertices[w + 1] = vx * c + vy * d + y;
					vv += 2;
				}
				return;
			}
			int v = 0;
			int skip = 0;
			for (int i = 0; i < start; i += 2)
			{
				int j = bones[v];
				v += j + 1;
				skip += j;
			}
			Bone[] skeletonBones = slot.bone.skeleton.bones.Items;
			if (deformArray.Count == 0)
			{
				int w2 = offset;
				int b2 = skip * 3;
				while (w2 < count)
				{
					float wx = 0f;
					float wy = 0f;
					int k = bones[v++];
					k += v;
					while (v < k)
					{
						Bone bone = skeletonBones[bones[v]];
						float vx2 = vertices[b2];
						float vy2 = vertices[b2 + 1];
						float weight = vertices[b2 + 2];
						wx += (vx2 * bone.a + vy2 * bone.b + bone.worldX) * weight;
						wy += (vx2 * bone.c + vy2 * bone.d + bone.worldY) * weight;
						v++;
						b2 += 3;
					}
					worldVertices[w2] = wx;
					worldVertices[w2 + 1] = wy;
					w2 += stride;
				}
				return;
			}
			float[] deform = deformArray.Items;
			int w3 = offset;
			int b3 = skip * 3;
			int f = skip << 1;
			while (w3 < count)
			{
				float wx2 = 0f;
				float wy2 = 0f;
				int l = bones[v++];
				l += v;
				while (v < l)
				{
					Bone bone2 = skeletonBones[bones[v]];
					float vx3 = vertices[b3] + deform[f];
					float vy3 = vertices[b3 + 1] + deform[f + 1];
					float weight2 = vertices[b3 + 2];
					wx2 += (vx3 * bone2.a + vy3 * bone2.b + bone2.worldX) * weight2;
					wy2 += (vx3 * bone2.c + vy3 * bone2.d + bone2.worldY) * weight2;
					v++;
					b3 += 3;
					f += 2;
				}
				worldVertices[w3] = wx2;
				worldVertices[w3 + 1] = wy2;
				w3 += stride;
			}
		}

		// Token: 0x04000172 RID: 370
		private static int nextID = 0;

		// Token: 0x04000173 RID: 371
		private static readonly object nextIdLock = new object();

		// Token: 0x04000174 RID: 372
		internal readonly int id;

		// Token: 0x04000175 RID: 373
		internal VertexAttachment timelineAttachment;

		// Token: 0x04000176 RID: 374
		internal int[] bones;

		// Token: 0x04000177 RID: 375
		internal float[] vertices;

		// Token: 0x04000178 RID: 376
		internal int worldVerticesLength;
	}
}
