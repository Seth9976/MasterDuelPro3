using System;

namespace Spine
{
	// Token: 0x02000058 RID: 88
	public class Bone : IUpdatable
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000B9D3 File Offset: 0x00009BD3
		public BoneData Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000B9DB File Offset: 0x00009BDB
		public Skeleton Skeleton
		{
			get
			{
				return this.skeleton;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000B9E3 File Offset: 0x00009BE3
		public Bone Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000B9EB File Offset: 0x00009BEB
		public ExposedList<Bone> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000B9F3 File Offset: 0x00009BF3
		public bool Active
		{
			get
			{
				return this.active;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000B9FB File Offset: 0x00009BFB
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000BA03 File Offset: 0x00009C03
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

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000BA0C File Offset: 0x00009C0C
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000BA14 File Offset: 0x00009C14
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

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000BA1D File Offset: 0x00009C1D
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000BA25 File Offset: 0x00009C25
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000BA2E File Offset: 0x00009C2E
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000BA36 File Offset: 0x00009C36
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

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000BA3F File Offset: 0x00009C3F
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000BA47 File Offset: 0x00009C47
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

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000BA50 File Offset: 0x00009C50
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000BA58 File Offset: 0x00009C58
		public float ShearX
		{
			get
			{
				return this.shearX;
			}
			set
			{
				this.shearX = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000BA61 File Offset: 0x00009C61
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000BA69 File Offset: 0x00009C69
		public float ShearY
		{
			get
			{
				return this.shearY;
			}
			set
			{
				this.shearY = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000BA72 File Offset: 0x00009C72
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000BA7A File Offset: 0x00009C7A
		public Inherit Inherit
		{
			get
			{
				return this.inherit;
			}
			set
			{
				this.inherit = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000BA83 File Offset: 0x00009C83
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000BA8B File Offset: 0x00009C8B
		public float AppliedRotation
		{
			get
			{
				return this.arotation;
			}
			set
			{
				this.arotation = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000BA94 File Offset: 0x00009C94
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000BA9C File Offset: 0x00009C9C
		public float AX
		{
			get
			{
				return this.ax;
			}
			set
			{
				this.ax = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000BAA5 File Offset: 0x00009CA5
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000BAAD File Offset: 0x00009CAD
		public float AY
		{
			get
			{
				return this.ay;
			}
			set
			{
				this.ay = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000BAB6 File Offset: 0x00009CB6
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000BABE File Offset: 0x00009CBE
		public float AScaleX
		{
			get
			{
				return this.ascaleX;
			}
			set
			{
				this.ascaleX = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000BAC7 File Offset: 0x00009CC7
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000BACF File Offset: 0x00009CCF
		public float AScaleY
		{
			get
			{
				return this.ascaleY;
			}
			set
			{
				this.ascaleY = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000BAE0 File Offset: 0x00009CE0
		public float AShearX
		{
			get
			{
				return this.ashearX;
			}
			set
			{
				this.ashearX = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000BAE9 File Offset: 0x00009CE9
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0000BAF1 File Offset: 0x00009CF1
		public float AShearY
		{
			get
			{
				return this.ashearY;
			}
			set
			{
				this.ashearY = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000BAFA File Offset: 0x00009CFA
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000BB02 File Offset: 0x00009D02
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

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000BB0B File Offset: 0x00009D0B
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000BB13 File Offset: 0x00009D13
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

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000BB1C File Offset: 0x00009D1C
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000BB24 File Offset: 0x00009D24
		public float C
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000BB2D File Offset: 0x00009D2D
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000BB35 File Offset: 0x00009D35
		public float D
		{
			get
			{
				return this.d;
			}
			set
			{
				this.d = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000BB3E File Offset: 0x00009D3E
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000BB46 File Offset: 0x00009D46
		public float WorldX
		{
			get
			{
				return this.worldX;
			}
			set
			{
				this.worldX = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000BB4F File Offset: 0x00009D4F
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000BB57 File Offset: 0x00009D57
		public float WorldY
		{
			get
			{
				return this.worldY;
			}
			set
			{
				this.worldY = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000BB60 File Offset: 0x00009D60
		public float WorldRotationX
		{
			get
			{
				return MathUtils.Atan2Deg(this.c, this.a);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000BB73 File Offset: 0x00009D73
		public float WorldRotationY
		{
			get
			{
				return MathUtils.Atan2Deg(this.d, this.b);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000BB86 File Offset: 0x00009D86
		public float WorldScaleX
		{
			get
			{
				return (float)Math.Sqrt((double)(this.a * this.a + this.c * this.c));
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000BBAA File Offset: 0x00009DAA
		public float WorldScaleY
		{
			get
			{
				return (float)Math.Sqrt((double)(this.b * this.b + this.d * this.d));
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000BBD0 File Offset: 0x00009DD0
		public Bone(BoneData data, Skeleton skeleton, Bone parent)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data", "data cannot be null.");
			}
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			}
			this.data = data;
			this.skeleton = skeleton;
			this.parent = parent;
			this.SetToSetupPose();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000BC30 File Offset: 0x00009E30
		public Bone(Bone bone, Skeleton skeleton, Bone parent)
		{
			if (bone == null)
			{
				throw new ArgumentNullException("bone", "bone cannot be null.");
			}
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			}
			this.skeleton = skeleton;
			this.parent = parent;
			this.data = bone.data;
			this.x = bone.x;
			this.y = bone.y;
			this.rotation = bone.rotation;
			this.scaleX = bone.scaleX;
			this.scaleY = bone.scaleY;
			this.shearX = bone.shearX;
			this.shearY = bone.shearY;
			this.inherit = bone.inherit;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000BCEE File Offset: 0x00009EEE
		public void Update(Skeleton.Physics physics)
		{
			this.UpdateWorldTransform(this.ax, this.ay, this.arotation, this.ascaleX, this.ascaleY, this.ashearX, this.ashearY);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000BD20 File Offset: 0x00009F20
		public void UpdateWorldTransform()
		{
			this.UpdateWorldTransform(this.x, this.y, this.rotation, this.scaleX, this.scaleY, this.shearX, this.shearY);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000BD54 File Offset: 0x00009F54
		public void UpdateWorldTransform(float x, float y, float rotation, float scaleX, float scaleY, float shearX, float shearY)
		{
			this.ax = x;
			this.ay = y;
			this.arotation = rotation;
			this.ascaleX = scaleX;
			this.ascaleY = scaleY;
			this.ashearX = shearX;
			this.ashearY = shearY;
			Bone parent = this.parent;
			if (parent == null)
			{
				Skeleton skeleton = this.skeleton;
				float sx = skeleton.scaleX;
				float sy = skeleton.ScaleY;
				float rx = (rotation + shearX) * 0.017453292f;
				float ry = (rotation + 90f + shearY) * 0.017453292f;
				this.a = (float)Math.Cos((double)rx) * scaleX * sx;
				this.b = (float)Math.Cos((double)ry) * scaleY * sx;
				this.c = (float)Math.Sin((double)rx) * scaleX * sy;
				this.d = (float)Math.Sin((double)ry) * scaleY * sy;
				this.worldX = x * sx + skeleton.x;
				this.worldY = y * sy + skeleton.y;
				return;
			}
			float pa = parent.a;
			float pb = parent.b;
			float pc = parent.c;
			float pd = parent.d;
			this.worldX = pa * x + pb * y + parent.worldX;
			this.worldY = pc * x + pd * y + parent.worldY;
			switch (this.inherit)
			{
			case Inherit.Normal:
			{
				float num = (rotation + shearX) * 0.017453292f;
				float ry2 = (rotation + 90f + shearY) * 0.017453292f;
				float la = (float)Math.Cos((double)num) * scaleX;
				float lb = (float)Math.Cos((double)ry2) * scaleY;
				float lc = (float)Math.Sin((double)num) * scaleX;
				float ld = (float)Math.Sin((double)ry2) * scaleY;
				this.a = pa * la + pb * lc;
				this.b = pa * lb + pb * ld;
				this.c = pc * la + pd * lc;
				this.d = pc * lb + pd * ld;
				return;
			}
			case Inherit.OnlyTranslation:
			{
				float rx2 = (rotation + shearX) * 0.017453292f;
				float ry3 = (rotation + 90f + shearY) * 0.017453292f;
				this.a = (float)Math.Cos((double)rx2) * scaleX;
				this.b = (float)Math.Cos((double)ry3) * scaleY;
				this.c = (float)Math.Sin((double)rx2) * scaleX;
				this.d = (float)Math.Sin((double)ry3) * scaleY;
				break;
			}
			case Inherit.NoRotationOrReflection:
			{
				float sx2 = 1f / this.skeleton.scaleX;
				float sy2 = 1f / this.skeleton.ScaleY;
				pa *= sx2;
				pc *= sy2;
				float s = pa * pa + pc * pc;
				float prx;
				if (s > 0.0001f)
				{
					s = Math.Abs(pa * pd * sy2 - pb * sx2 * pc) / s;
					pb = pc * s;
					pd = pa * s;
					prx = MathUtils.Atan2Deg(pc, pa);
				}
				else
				{
					pa = 0f;
					pc = 0f;
					prx = 90f - MathUtils.Atan2Deg(pd, pb);
				}
				float rx3 = (rotation + shearX - prx) * 0.017453292f;
				float num2 = (rotation + shearY - prx + 90f) * 0.017453292f;
				float la2 = (float)Math.Cos((double)rx3) * scaleX;
				float lb2 = (float)Math.Cos((double)num2) * scaleY;
				float lc2 = (float)Math.Sin((double)rx3) * scaleX;
				float ld2 = (float)Math.Sin((double)num2) * scaleY;
				this.a = pa * la2 - pb * lc2;
				this.b = pa * lb2 - pb * ld2;
				this.c = pc * la2 + pd * lc2;
				this.d = pc * lb2 + pd * ld2;
				break;
			}
			case Inherit.NoScale:
			case Inherit.NoScaleOrReflection:
			{
				rotation *= 0.017453292f;
				float cos = (float)Math.Cos((double)rotation);
				float sin = (float)Math.Sin((double)rotation);
				float za = (pa * cos + pb * sin) / this.skeleton.scaleX;
				float zc = (pc * cos + pd * sin) / this.skeleton.ScaleY;
				float s2 = (float)Math.Sqrt((double)(za * za + zc * zc));
				if (s2 > 1E-05f)
				{
					s2 = 1f / s2;
				}
				za *= s2;
				zc *= s2;
				s2 = (float)Math.Sqrt((double)(za * za + zc * zc));
				if (this.inherit == Inherit.NoScale && pa * pd - pb * pc < 0f != (this.skeleton.scaleX < 0f != this.skeleton.ScaleY < 0f))
				{
					s2 = -s2;
				}
				rotation = 1.5707964f + MathUtils.Atan2(zc, za);
				float zb = (float)Math.Cos((double)rotation) * s2;
				float zd = (float)Math.Sin((double)rotation) * s2;
				shearX *= 0.017453292f;
				shearY = (90f + shearY) * 0.017453292f;
				float la3 = (float)Math.Cos((double)shearX) * scaleX;
				float lb3 = (float)Math.Cos((double)shearY) * scaleY;
				float lc3 = (float)Math.Sin((double)shearX) * scaleX;
				float ld3 = (float)Math.Sin((double)shearY) * scaleY;
				this.a = za * la3 + zb * lc3;
				this.b = za * lb3 + zb * ld3;
				this.c = zc * la3 + zd * lc3;
				this.d = zc * lb3 + zd * ld3;
				break;
			}
			}
			this.a *= this.skeleton.scaleX;
			this.b *= this.skeleton.scaleX;
			this.c *= this.skeleton.ScaleY;
			this.d *= this.skeleton.ScaleY;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		public void SetToSetupPose()
		{
			BoneData data = this.data;
			this.x = data.x;
			this.y = data.y;
			this.rotation = data.rotation;
			this.scaleX = data.scaleX;
			this.scaleY = data.ScaleY;
			this.shearX = data.shearX;
			this.shearY = data.shearY;
			this.inherit = data.inherit;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000C358 File Offset: 0x0000A558
		public void UpdateAppliedTransform()
		{
			Bone parent = this.parent;
			if (parent == null)
			{
				this.ax = this.worldX - this.skeleton.x;
				this.ay = this.worldY - this.skeleton.y;
				float a = this.a;
				float b = this.b;
				float c = this.c;
				float d = this.d;
				this.arotation = MathUtils.Atan2Deg(c, a);
				this.ascaleX = (float)Math.Sqrt((double)(a * a + c * c));
				this.ascaleY = (float)Math.Sqrt((double)(b * b + d * d));
				this.ashearX = 0f;
				this.ashearY = MathUtils.Atan2Deg(a * b + c * d, a * d - b * c);
				return;
			}
			float pa = parent.a;
			float pb = parent.b;
			float pc = parent.c;
			float pd = parent.d;
			float pid = 1f / (pa * pd - pb * pc);
			float ia = pd * pid;
			float ib = pb * pid;
			float ic = pc * pid;
			float id = pa * pid;
			float dx = this.worldX - parent.worldX;
			float dy = this.worldY - parent.worldY;
			this.ax = dx * ia - dy * ib;
			this.ay = dy * id - dx * ic;
			float ra;
			float rb;
			float rc;
			float rd;
			if (this.inherit == Inherit.OnlyTranslation)
			{
				ra = this.a;
				rb = this.b;
				rc = this.c;
				rd = this.d;
			}
			else
			{
				Inherit inherit = this.inherit;
				if (inherit != Inherit.NoRotationOrReflection)
				{
					if (inherit - Inherit.NoScale <= 1)
					{
						float num = this.rotation * 0.017453292f;
						float cos = (float)Math.Cos((double)num);
						float sin = (float)Math.Sin((double)num);
						pa = (pa * cos + pb * sin) / this.skeleton.scaleX;
						pc = (pc * cos + pd * sin) / this.skeleton.ScaleY;
						float s = (float)Math.Sqrt((double)(pa * pa + pc * pc));
						if (s > 1E-05f)
						{
							s = 1f / s;
						}
						pa *= s;
						pc *= s;
						s = (float)Math.Sqrt((double)(pa * pa + pc * pc));
						if (this.inherit == Inherit.NoScale && pid < 0f != (this.skeleton.scaleX < 0f != this.skeleton.ScaleY < 0f))
						{
							s = -s;
						}
						float num2 = 1.5707964f + MathUtils.Atan2(pc, pa);
						pb = (float)Math.Cos((double)num2) * s;
						pd = (float)Math.Sin((double)num2) * s;
						pid = 1f / (pa * pd - pb * pc);
						ia = pd * pid;
						ib = pb * pid;
						ic = pc * pid;
						id = pa * pid;
					}
				}
				else
				{
					float s2 = Math.Abs(pa * pd - pb * pc) / (pa * pa + pc * pc);
					float skeletonScaleY = this.skeleton.ScaleY;
					pb = -pc * this.skeleton.scaleX * s2 / skeletonScaleY;
					pd = pa * skeletonScaleY * s2 / this.skeleton.scaleX;
					pid = 1f / (pa * pd - pb * pc);
					ia = pd * pid;
					ib = pb * pid;
				}
				ra = ia * this.a - ib * this.c;
				rb = ia * this.b - ib * this.d;
				rc = id * this.c - ic * this.a;
				rd = id * this.d - ic * this.b;
			}
			this.ashearX = 0f;
			this.ascaleX = (float)Math.Sqrt((double)(ra * ra + rc * rc));
			if (this.ascaleX > 0.0001f)
			{
				float det = ra * rd - rb * rc;
				this.ascaleY = det / this.ascaleX;
				this.ashearY = -MathUtils.Atan2Deg(ra * rb + rc * rd, det);
				this.arotation = MathUtils.Atan2Deg(rc, ra);
				return;
			}
			this.ascaleX = 0f;
			this.ascaleY = (float)Math.Sqrt((double)(rb * rb + rd * rd));
			this.ashearY = 0f;
			this.arotation = 90f - MathUtils.Atan2Deg(rd, rb);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000C790 File Offset: 0x0000A990
		public void WorldToLocal(float worldX, float worldY, out float localX, out float localY)
		{
			float a = this.a;
			float b = this.b;
			float c = this.c;
			float d = this.d;
			float det = a * d - b * c;
			float x = worldX - this.worldX;
			float y = worldY - this.worldY;
			localX = (x * d - y * b) / det;
			localY = (y * a - x * c) / det;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000C7F3 File Offset: 0x0000A9F3
		public void LocalToWorld(float localX, float localY, out float worldX, out float worldY)
		{
			worldX = localX * this.a + localY * this.b + this.worldX;
			worldY = localX * this.c + localY * this.d + this.worldY;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000C82A File Offset: 0x0000AA2A
		public void WorldToParent(float worldX, float worldY, out float parentX, out float parentY)
		{
			if (this.parent == null)
			{
				parentX = worldX;
				parentY = worldY;
				return;
			}
			this.parent.WorldToLocal(worldX, worldY, out parentX, out parentY);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000C84C File Offset: 0x0000AA4C
		public void ParentToWorld(float parentX, float parentY, out float worldX, out float worldY)
		{
			if (this.parent == null)
			{
				worldX = parentX;
				worldY = parentY;
				return;
			}
			this.parent.LocalToWorld(parentX, parentY, out worldX, out worldY);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000C870 File Offset: 0x0000AA70
		public float WorldToLocalRotation(float worldRotation)
		{
			worldRotation *= 0.017453292f;
			float sin = (float)Math.Sin((double)worldRotation);
			float cos = (float)Math.Cos((double)worldRotation);
			return MathUtils.Atan2Deg(this.a * sin - this.c * cos, this.d * cos - this.b * sin) + this.rotation - this.shearX;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000C8D0 File Offset: 0x0000AAD0
		public float LocalToWorldRotation(float localRotation)
		{
			localRotation = (localRotation - this.rotation - this.shearX) * 0.017453292f;
			float sin = (float)Math.Sin((double)localRotation);
			float cos = (float)Math.Cos((double)localRotation);
			return MathUtils.Atan2Deg(cos * this.c + sin * this.d, cos * this.a + sin * this.b);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000C930 File Offset: 0x0000AB30
		public void RotateWorld(float degrees)
		{
			degrees *= 0.017453292f;
			float sin = (float)Math.Sin((double)degrees);
			float cos = (float)Math.Cos((double)degrees);
			float ra = this.a;
			float rb = this.b;
			this.a = cos * ra - sin * this.c;
			this.b = cos * rb - sin * this.d;
			this.c = sin * ra + cos * this.c;
			this.d = sin * rb + cos * this.d;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000C9AE File Offset: 0x0000ABAE
		public override string ToString()
		{
			return this.data.name;
		}

		// Token: 0x0400017E RID: 382
		public static bool yDown;

		// Token: 0x0400017F RID: 383
		internal BoneData data;

		// Token: 0x04000180 RID: 384
		internal Skeleton skeleton;

		// Token: 0x04000181 RID: 385
		internal Bone parent;

		// Token: 0x04000182 RID: 386
		internal ExposedList<Bone> children = new ExposedList<Bone>();

		// Token: 0x04000183 RID: 387
		internal float x;

		// Token: 0x04000184 RID: 388
		internal float y;

		// Token: 0x04000185 RID: 389
		internal float rotation;

		// Token: 0x04000186 RID: 390
		internal float scaleX;

		// Token: 0x04000187 RID: 391
		internal float scaleY;

		// Token: 0x04000188 RID: 392
		internal float shearX;

		// Token: 0x04000189 RID: 393
		internal float shearY;

		// Token: 0x0400018A RID: 394
		internal float ax;

		// Token: 0x0400018B RID: 395
		internal float ay;

		// Token: 0x0400018C RID: 396
		internal float arotation;

		// Token: 0x0400018D RID: 397
		internal float ascaleX;

		// Token: 0x0400018E RID: 398
		internal float ascaleY;

		// Token: 0x0400018F RID: 399
		internal float ashearX;

		// Token: 0x04000190 RID: 400
		internal float ashearY;

		// Token: 0x04000191 RID: 401
		internal float a;

		// Token: 0x04000192 RID: 402
		internal float b;

		// Token: 0x04000193 RID: 403
		internal float worldX;

		// Token: 0x04000194 RID: 404
		internal float c;

		// Token: 0x04000195 RID: 405
		internal float d;

		// Token: 0x04000196 RID: 406
		internal float worldY;

		// Token: 0x04000197 RID: 407
		internal Inherit inherit;

		// Token: 0x04000198 RID: 408
		internal bool sorted;

		// Token: 0x04000199 RID: 409
		internal bool active;
	}
}
