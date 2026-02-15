using System;

namespace Spine
{
	// Token: 0x02000062 RID: 98
	public class IkConstraint : IUpdatable
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		public IkConstraint(IkConstraintData data, Skeleton skeleton)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data", "data cannot be null.");
			}
			this.data = data;
			this.bones = new ExposedList<Bone>(data.bones.Count);
			foreach (BoneData boneData in data.bones)
			{
				this.bones.Add(skeleton.bones.Items[boneData.index]);
			}
			this.target = skeleton.bones.Items[data.target.index];
			this.mix = data.mix;
			this.softness = data.softness;
			this.bendDirection = data.bendDirection;
			this.compress = data.compress;
			this.stretch = data.stretch;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000DBE4 File Offset: 0x0000BDE4
		public IkConstraint(IkConstraint constraint, Skeleton skeleton)
			: this(constraint.data, skeleton)
		{
			this.mix = constraint.mix;
			this.softness = constraint.softness;
			this.bendDirection = constraint.bendDirection;
			this.compress = constraint.compress;
			this.stretch = constraint.stretch;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000DC3C File Offset: 0x0000BE3C
		public void SetToSetupPose()
		{
			IkConstraintData data = this.data;
			this.mix = data.mix;
			this.softness = data.softness;
			this.bendDirection = data.bendDirection;
			this.compress = data.compress;
			this.stretch = data.stretch;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000DC8C File Offset: 0x0000BE8C
		public void Update(Skeleton.Physics physics)
		{
			if (this.mix == 0f)
			{
				return;
			}
			Bone target = this.target;
			Bone[] bones = this.bones.Items;
			int count = this.bones.Count;
			if (count == 1)
			{
				IkConstraint.Apply(bones[0], target.worldX, target.worldY, this.compress, this.stretch, this.data.uniform, this.mix);
				return;
			}
			if (count != 2)
			{
				return;
			}
			IkConstraint.Apply(bones[0], bones[1], target.worldX, target.worldY, this.bendDirection, this.stretch, this.data.uniform, this.softness, this.mix);
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000DD3B File Offset: 0x0000BF3B
		public ExposedList<Bone> Bones
		{
			get
			{
				return this.bones;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000DD43 File Offset: 0x0000BF43
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000DD4B File Offset: 0x0000BF4B
		public Bone Target
		{
			get
			{
				return this.target;
			}
			set
			{
				this.target = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000DD54 File Offset: 0x0000BF54
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000DD5C File Offset: 0x0000BF5C
		public float Mix
		{
			get
			{
				return this.mix;
			}
			set
			{
				this.mix = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000DD65 File Offset: 0x0000BF65
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000DD6D File Offset: 0x0000BF6D
		public float Softness
		{
			get
			{
				return this.softness;
			}
			set
			{
				this.softness = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000DD76 File Offset: 0x0000BF76
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000DD7E File Offset: 0x0000BF7E
		public int BendDirection
		{
			get
			{
				return this.bendDirection;
			}
			set
			{
				this.bendDirection = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000DD87 File Offset: 0x0000BF87
		// (set) Token: 0x06000337 RID: 823 RVA: 0x0000DD8F File Offset: 0x0000BF8F
		public bool Compress
		{
			get
			{
				return this.compress;
			}
			set
			{
				this.compress = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000DD98 File Offset: 0x0000BF98
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000DDA0 File Offset: 0x0000BFA0
		public bool Stretch
		{
			get
			{
				return this.stretch;
			}
			set
			{
				this.stretch = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool Active
		{
			get
			{
				return this.active;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000DDB1 File Offset: 0x0000BFB1
		public IkConstraintData Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000DDB9 File Offset: 0x0000BFB9
		public override string ToString()
		{
			return this.data.name;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000DDC8 File Offset: 0x0000BFC8
		public static void Apply(Bone bone, float targetX, float targetY, bool compress, bool stretch, bool uniform, float alpha)
		{
			if (bone == null)
			{
				throw new ArgumentNullException("bone", "bone cannot be null.");
			}
			Bone p = bone.parent;
			float pa = p.a;
			float pb = p.b;
			float pc = p.c;
			float pd = p.d;
			float rotationIK = -bone.ashearX - bone.arotation;
			Inherit inherit = bone.inherit;
			float tx;
			float ty;
			if (inherit != Inherit.OnlyTranslation)
			{
				if (inherit == Inherit.NoRotationOrReflection)
				{
					float s = Math.Abs(pa * pd - pb * pc) / Math.Max(0.0001f, pa * pa + pc * pc);
					float sa = pa / bone.skeleton.scaleX;
					float sc = pc / bone.skeleton.ScaleY;
					pb = -sc * s * bone.skeleton.scaleX;
					pd = sa * s * bone.skeleton.ScaleY;
					rotationIK += MathUtils.Atan2Deg(sc, sa);
				}
				float x = targetX - p.worldX;
				float y = targetY - p.worldY;
				float d = pa * pd - pb * pc;
				if (Math.Abs(d) <= 0.0001f)
				{
					tx = 0f;
					ty = 0f;
				}
				else
				{
					tx = (x * pd - y * pb) / d - bone.ax;
					ty = (y * pa - x * pc) / d - bone.ay;
				}
			}
			else
			{
				tx = (targetX - bone.worldX) * (float)Math.Sign(bone.skeleton.ScaleX);
				ty = (targetY - bone.worldY) * (float)Math.Sign(bone.skeleton.ScaleY);
			}
			rotationIK += MathUtils.Atan2Deg(ty, tx);
			if (bone.ascaleX < 0f)
			{
				rotationIK += 180f;
			}
			if (rotationIK > 180f)
			{
				rotationIK -= 360f;
			}
			else if (rotationIK < -180f)
			{
				rotationIK += 360f;
			}
			float sx = bone.ascaleX;
			float sy = bone.ascaleY;
			if (compress || stretch)
			{
				inherit = bone.inherit;
				if (inherit - Inherit.NoScale <= 1)
				{
					tx = targetX - bone.worldX;
					ty = targetY - bone.worldY;
				}
				float b = bone.data.length * sx;
				if (b > 0.0001f)
				{
					float dd = tx * tx + ty * ty;
					if ((compress && dd < b * b) || (stretch && dd > b * b))
					{
						float s2 = ((float)Math.Sqrt((double)dd) / b - 1f) * alpha + 1f;
						sx *= s2;
						if (uniform)
						{
							sy *= s2;
						}
					}
				}
			}
			bone.UpdateWorldTransform(bone.ax, bone.ay, bone.arotation + rotationIK * alpha, sx, sy, bone.ashearX, bone.ashearY);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000E080 File Offset: 0x0000C280
		public static void Apply(Bone parent, Bone child, float targetX, float targetY, int bendDir, bool stretch, bool uniform, float softness, float alpha)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent", "parent cannot be null.");
			}
			if (child == null)
			{
				throw new ArgumentNullException("child", "child cannot be null.");
			}
			if (parent.inherit != Inherit.Normal || child.inherit != Inherit.Normal)
			{
				return;
			}
			float px = parent.ax;
			float py = parent.ay;
			float psx = parent.ascaleX;
			float psy = parent.ascaleY;
			float sx = psx;
			float sy = psy;
			float csx = child.ascaleX;
			int os;
			int s2;
			if (psx < 0f)
			{
				psx = -psx;
				os = 180;
				s2 = -1;
			}
			else
			{
				os = 0;
				s2 = 1;
			}
			if (psy < 0f)
			{
				psy = -psy;
				s2 = -s2;
			}
			int os2;
			if (csx < 0f)
			{
				csx = -csx;
				os2 = 180;
			}
			else
			{
				os2 = 0;
			}
			float cx = child.ax;
			float a = parent.a;
			float b = parent.b;
			float c = parent.c;
			float d = parent.d;
			bool u = Math.Abs(psx - psy) <= 0.0001f;
			float cy;
			float cwx;
			float cwy;
			if (!u || stretch)
			{
				cy = 0f;
				cwx = a * cx + parent.worldX;
				cwy = c * cx + parent.worldY;
			}
			else
			{
				cy = child.ay;
				cwx = a * cx + b * cy + parent.worldX;
				cwy = c * cx + d * cy + parent.worldY;
			}
			Bone pp = parent.parent;
			a = pp.a;
			b = pp.b;
			c = pp.c;
			d = pp.d;
			float id = a * d - b * c;
			float x = cwx - pp.worldX;
			float y = cwy - pp.worldY;
			id = ((Math.Abs(id) <= 0.0001f) ? 0f : (1f / id));
			float num = (x * d - y * b) * id - px;
			float dy = (y * a - x * c) * id - py;
			float l = (float)Math.Sqrt((double)(num * num + dy * dy));
			float l2 = child.data.length * csx;
			if (l < 0.0001f)
			{
				IkConstraint.Apply(parent, targetX, targetY, false, stretch, false, alpha);
				child.UpdateWorldTransform(cx, cy, 0f, child.ascaleX, child.ascaleY, child.ashearX, child.ashearY);
				return;
			}
			x = targetX - pp.worldX;
			y = targetY - pp.worldY;
			float tx = (x * d - y * b) * id - px;
			float ty = (y * a - x * c) * id - py;
			float dd = tx * tx + ty * ty;
			if (softness != 0f)
			{
				softness *= psx * (csx + 1f) * 0.5f;
				float td = (float)Math.Sqrt((double)dd);
				float sd = td - l - l2 * psx + softness;
				if (sd > 0f)
				{
					float p = Math.Min(1f, sd / (softness * 2f)) - 1f;
					p = (sd - softness * (1f - p * p)) / td;
					tx -= p * tx;
					ty -= p * ty;
					dd = tx * tx + ty * ty;
				}
			}
			float a2;
			float a3;
			if (u)
			{
				l2 *= psx;
				float cos = (dd - l * l - l2 * l2) / (2f * l * l2);
				if (cos < -1f)
				{
					cos = -1f;
					a2 = 3.1415927f * (float)bendDir;
				}
				else if (cos > 1f)
				{
					cos = 1f;
					a2 = 0f;
					if (stretch)
					{
						a = ((float)Math.Sqrt((double)dd) / (l + l2) - 1f) * alpha + 1f;
						sx *= a;
						if (uniform)
						{
							sy *= a;
						}
					}
				}
				else
				{
					a2 = (float)Math.Acos((double)cos) * (float)bendDir;
				}
				a = l + l2 * cos;
				b = l2 * (float)Math.Sin((double)a2);
				a3 = (float)Math.Atan2((double)(ty * a - tx * b), (double)(tx * a + ty * b));
			}
			else
			{
				a = psx * l2;
				b = psy * l2;
				float aa = a * a;
				float bb = b * b;
				float ta = (float)Math.Atan2((double)ty, (double)tx);
				c = bb * l * l + aa * dd - aa * bb;
				float c2 = -2f * bb * l;
				float c3 = bb - aa;
				d = c2 * c2 - 4f * c3 * c;
				if (d >= 0f)
				{
					float q = (float)Math.Sqrt((double)d);
					if (c2 < 0f)
					{
						q = -q;
					}
					q = -(c2 + q) * 0.5f;
					float r0 = q / c3;
					float r = c / q;
					float r2 = ((Math.Abs(r0) < Math.Abs(r)) ? r0 : r);
					r0 = dd - r2 * r2;
					if (r0 >= 0f)
					{
						y = (float)Math.Sqrt((double)r0) * (float)bendDir;
						a3 = ta - (float)Math.Atan2((double)y, (double)r2);
						a2 = (float)Math.Atan2((double)(y / psy), (double)((r2 - l) / psx));
						goto IL_0606;
					}
				}
				float minAngle = 3.1415927f;
				float minX = l - a;
				float minDist = minX * minX;
				float minY = 0f;
				float maxAngle = 0f;
				float maxX = l + a;
				float maxDist = maxX * maxX;
				float maxY = 0f;
				c = -a * l / (aa - bb);
				if (c >= -1f && c <= 1f)
				{
					c = (float)Math.Acos((double)c);
					x = a * (float)Math.Cos((double)c) + l;
					y = b * (float)Math.Sin((double)c);
					d = x * x + y * y;
					if (d < minDist)
					{
						minAngle = c;
						minDist = d;
						minX = x;
						minY = y;
					}
					if (d > maxDist)
					{
						maxAngle = c;
						maxDist = d;
						maxX = x;
						maxY = y;
					}
				}
				if (dd <= (minDist + maxDist) * 0.5f)
				{
					a3 = ta - (float)Math.Atan2((double)(minY * (float)bendDir), (double)minX);
					a2 = minAngle * (float)bendDir;
				}
				else
				{
					a3 = ta - (float)Math.Atan2((double)(maxY * (float)bendDir), (double)maxX);
					a2 = maxAngle * (float)bendDir;
				}
			}
			IL_0606:
			float os3 = (float)Math.Atan2((double)cy, (double)cx) * (float)s2;
			float rotation = parent.arotation;
			a3 = (a3 - os3) * 57.295776f + (float)os - rotation;
			if (a3 > 180f)
			{
				a3 -= 360f;
			}
			else if (a3 < -180f)
			{
				a3 += 360f;
			}
			parent.UpdateWorldTransform(px, py, rotation + a3 * alpha, sx, sy, 0f, 0f);
			rotation = child.arotation;
			a2 = ((a2 + os3) * 57.295776f - child.ashearX) * (float)s2 + (float)os2 - rotation;
			if (a2 > 180f)
			{
				a2 -= 360f;
			}
			else if (a2 < -180f)
			{
				a2 += 360f;
			}
			child.UpdateWorldTransform(cx, cy, rotation + a2 * alpha, child.ascaleX, child.ascaleY, child.ashearX, child.ashearY);
		}

		// Token: 0x040001C8 RID: 456
		internal readonly IkConstraintData data;

		// Token: 0x040001C9 RID: 457
		internal readonly ExposedList<Bone> bones = new ExposedList<Bone>();

		// Token: 0x040001CA RID: 458
		internal Bone target;

		// Token: 0x040001CB RID: 459
		internal int bendDirection;

		// Token: 0x040001CC RID: 460
		internal bool compress;

		// Token: 0x040001CD RID: 461
		internal bool stretch;

		// Token: 0x040001CE RID: 462
		internal float mix = 1f;

		// Token: 0x040001CF RID: 463
		internal float softness;

		// Token: 0x040001D0 RID: 464
		internal bool active;
	}
}
