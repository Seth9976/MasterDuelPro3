using System;

namespace Spine
{
	// Token: 0x0200006E RID: 110
	public class PhysicsConstraint : IUpdatable
	{
		// Token: 0x06000393 RID: 915 RVA: 0x0000FCD0 File Offset: 0x0000DED0
		public PhysicsConstraint(PhysicsConstraintData data, Skeleton skeleton)
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
			this.bone = skeleton.bones.Items[data.bone.index];
			this.inertia = data.inertia;
			this.strength = data.strength;
			this.damping = data.damping;
			this.massInverse = data.massInverse;
			this.wind = data.wind;
			this.gravity = data.gravity;
			this.mix = data.mix;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000FD90 File Offset: 0x0000DF90
		public PhysicsConstraint(PhysicsConstraint constraint, Skeleton skeleton)
			: this(constraint.data, skeleton)
		{
			this.inertia = constraint.inertia;
			this.strength = constraint.strength;
			this.damping = constraint.damping;
			this.massInverse = constraint.massInverse;
			this.wind = constraint.wind;
			this.gravity = constraint.gravity;
			this.mix = constraint.mix;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000FE00 File Offset: 0x0000E000
		public void Reset()
		{
			this.remaining = 0f;
			this.lastTime = this.skeleton.time;
			this.reset = true;
			this.xOffset = 0f;
			this.xVelocity = 0f;
			this.yOffset = 0f;
			this.yVelocity = 0f;
			this.rotateOffset = 0f;
			this.rotateVelocity = 0f;
			this.scaleOffset = 0f;
			this.scaleVelocity = 0f;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000FE88 File Offset: 0x0000E088
		public void SetToSetupPose()
		{
			PhysicsConstraintData data = this.data;
			this.inertia = data.inertia;
			this.strength = data.strength;
			this.damping = data.damping;
			this.massInverse = data.massInverse;
			this.wind = data.wind;
			this.gravity = data.gravity;
			this.mix = data.mix;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
		public void Translate(float x, float y)
		{
			this.ux -= x;
			this.uy -= y;
			this.cx -= x;
			this.cy -= y;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000FF2C File Offset: 0x0000E12C
		public void Rotate(float x, float y, float degrees)
		{
			float num = degrees * 0.017453292f;
			float cos = (float)Math.Cos((double)num);
			float sin = (float)Math.Sin((double)num);
			float dx = this.cx - x;
			float dy = this.cy - y;
			this.Translate(dx * cos - dy * sin - dx, dx * sin + dy * cos - dy);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000FF7C File Offset: 0x0000E17C
		public void Update(Skeleton.Physics physics)
		{
			float mix = this.mix;
			if (mix == 0f)
			{
				return;
			}
			bool x = this.data.x > 0f;
			bool y = this.data.y > 0f;
			bool rotateOrShearX = this.data.rotate > 0f || this.data.shearX > 0f;
			bool scaleX = this.data.scaleX > 0f;
			Bone bone = this.bone;
			float i = bone.data.length;
			switch (physics)
			{
			case Skeleton.Physics.None:
				return;
			case Skeleton.Physics.Reset:
				this.Reset();
				break;
			case Skeleton.Physics.Update:
				break;
			case Skeleton.Physics.Pose:
				if (x)
				{
					bone.worldX += this.xOffset * mix * this.data.x;
				}
				if (y)
				{
					bone.worldY += this.yOffset * mix * this.data.y;
					goto IL_068A;
				}
				goto IL_068A;
			default:
				goto IL_068A;
			}
			Skeleton skeleton = this.skeleton;
			float delta = Math.Max(skeleton.time - this.lastTime, 0f);
			this.remaining += delta;
			this.lastTime = skeleton.time;
			float bx = bone.worldX;
			float by = bone.worldY;
			if (this.reset)
			{
				this.reset = false;
				this.ux = bx;
				this.uy = by;
			}
			else
			{
				float a = this.remaining;
				float j = this.inertia;
				float t = this.data.step;
				float f = skeleton.data.referenceScale;
				float d = -1f;
				float qx = this.data.limit * delta;
				float qy = qx * Math.Abs(skeleton.ScaleY);
				qx *= Math.Abs(skeleton.ScaleX);
				if (x || y)
				{
					if (x)
					{
						float u = (this.ux - bx) * j;
						this.xOffset += ((u > qx) ? qx : ((u < -qx) ? (-qx) : u));
						this.ux = bx;
					}
					if (y)
					{
						float u2 = (this.uy - by) * j;
						this.yOffset += ((u2 > qy) ? qy : ((u2 < -qy) ? (-qy) : u2));
						this.uy = by;
					}
					if (a >= t)
					{
						d = (float)Math.Pow((double)this.damping, (double)(60f * t));
						float k = this.massInverse * t;
						float e = this.strength;
						float w = this.wind * f;
						float g = (Bone.yDown ? (-this.gravity) : this.gravity) * f;
						do
						{
							if (x)
							{
								this.xVelocity += (w - this.xOffset * e) * k;
								this.xOffset += this.xVelocity * t;
								this.xVelocity *= d;
							}
							if (y)
							{
								this.yVelocity -= (g + this.yOffset * e) * k;
								this.yOffset += this.yVelocity * t;
								this.yVelocity *= d;
							}
							a -= t;
						}
						while (a >= t);
					}
					if (x)
					{
						bone.worldX += this.xOffset * mix * this.data.x;
					}
					if (y)
					{
						bone.worldY += this.yOffset * mix * this.data.y;
					}
				}
				if (rotateOrShearX || scaleX)
				{
					float ca = (float)Math.Atan2((double)bone.c, (double)bone.a);
					float mr = 0f;
					float dx = this.cx - bone.worldX;
					float dy = this.cy - bone.worldY;
					if (dx > qx)
					{
						dx = qx;
					}
					else if (dx < -qx)
					{
						dx = -qx;
					}
					if (dy > qy)
					{
						dy = qy;
					}
					else if (dy < -qy)
					{
						dy = -qy;
					}
					float c;
					float s;
					if (rotateOrShearX)
					{
						mr = (this.data.rotate + this.data.shearX) * mix;
						float r = (float)Math.Atan2((double)(dy + this.ty), (double)(dx + this.tx)) - ca - this.rotateOffset * mr;
						this.rotateOffset += (r - (float)Math.Ceiling((double)(r * 0.15915494f - 0.5f)) * 6.2831855f) * j;
						r = this.rotateOffset * mr + ca;
						c = (float)Math.Cos((double)r);
						s = (float)Math.Sin((double)r);
						if (scaleX)
						{
							r = i * bone.WorldScaleX;
							if (r > 0f)
							{
								this.scaleOffset += (dx * c + dy * s) * j / r;
							}
						}
					}
					else
					{
						c = (float)Math.Cos((double)ca);
						s = (float)Math.Sin((double)ca);
						float r2 = i * bone.WorldScaleX;
						if (r2 > 0f)
						{
							this.scaleOffset += (dx * c + dy * s) * j / r2;
						}
					}
					a = this.remaining;
					if (a >= t)
					{
						if (d == -1f)
						{
							d = (float)Math.Pow((double)this.damping, (double)(60f * t));
						}
						float l = this.massInverse * t;
						float e2 = this.strength;
						float w2 = this.wind;
						float g2 = (Bone.yDown ? (-this.gravity) : this.gravity);
						float h = i / f;
						for (;;)
						{
							a -= t;
							if (scaleX)
							{
								this.scaleVelocity += (w2 * c - g2 * s - this.scaleOffset * e2) * l;
								this.scaleOffset += this.scaleVelocity * t;
								this.scaleVelocity *= d;
							}
							if (rotateOrShearX)
							{
								this.rotateVelocity -= ((w2 * s + g2 * c) * h + this.rotateOffset * e2) * l;
								this.rotateOffset += this.rotateVelocity * t;
								this.rotateVelocity *= d;
								if (a < t)
								{
									break;
								}
								float num = this.rotateOffset * mr + ca;
								c = (float)Math.Cos((double)num);
								s = (float)Math.Sin((double)num);
							}
							else if (a < t)
							{
								break;
							}
						}
					}
				}
				this.remaining = a;
			}
			this.cx = bone.worldX;
			this.cy = bone.worldY;
			IL_068A:
			if (rotateOrShearX)
			{
				float o = this.rotateOffset * mix;
				if (this.data.shearX > 0f)
				{
					float r3 = 0f;
					float s2;
					float c2;
					float a2;
					if (this.data.rotate > 0f)
					{
						r3 = o * this.data.rotate;
						s2 = (float)Math.Sin((double)r3);
						c2 = (float)Math.Cos((double)r3);
						a2 = bone.b;
						bone.b = c2 * a2 - s2 * bone.d;
						bone.d = s2 * a2 + c2 * bone.d;
					}
					r3 += o * this.data.shearX;
					s2 = (float)Math.Sin((double)r3);
					c2 = (float)Math.Cos((double)r3);
					a2 = bone.a;
					bone.a = c2 * a2 - s2 * bone.c;
					bone.c = s2 * a2 + c2 * bone.c;
				}
				else
				{
					o *= this.data.rotate;
					float s2 = (float)Math.Sin((double)o);
					float c2 = (float)Math.Cos((double)o);
					float a2 = bone.a;
					bone.a = c2 * a2 - s2 * bone.c;
					bone.c = s2 * a2 + c2 * bone.c;
					a2 = bone.b;
					bone.b = c2 * a2 - s2 * bone.d;
					bone.d = s2 * a2 + c2 * bone.d;
				}
			}
			if (scaleX)
			{
				float s3 = 1f + this.scaleOffset * mix * this.data.scaleX;
				bone.a *= s3;
				bone.c *= s3;
			}
			if (physics != Skeleton.Physics.Pose)
			{
				this.tx = i * bone.a;
				this.ty = i * bone.c;
			}
			bone.UpdateAppliedTransform();
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00010812 File Offset: 0x0000EA12
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0001081A File Offset: 0x0000EA1A
		public Bone Bone
		{
			get
			{
				return this.bone;
			}
			set
			{
				this.bone = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00010823 File Offset: 0x0000EA23
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0001082B File Offset: 0x0000EA2B
		public float Inertia
		{
			get
			{
				return this.inertia;
			}
			set
			{
				this.inertia = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00010834 File Offset: 0x0000EA34
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0001083C File Offset: 0x0000EA3C
		public float Strength
		{
			get
			{
				return this.strength;
			}
			set
			{
				this.strength = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00010845 File Offset: 0x0000EA45
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0001084D File Offset: 0x0000EA4D
		public float Damping
		{
			get
			{
				return this.damping;
			}
			set
			{
				this.damping = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00010856 File Offset: 0x0000EA56
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x0001085E File Offset: 0x0000EA5E
		public float MassInverse
		{
			get
			{
				return this.massInverse;
			}
			set
			{
				this.massInverse = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00010867 File Offset: 0x0000EA67
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x0001086F File Offset: 0x0000EA6F
		public float Wind
		{
			get
			{
				return this.wind;
			}
			set
			{
				this.wind = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00010878 File Offset: 0x0000EA78
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x00010880 File Offset: 0x0000EA80
		public float Gravity
		{
			get
			{
				return this.gravity;
			}
			set
			{
				this.gravity = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00010889 File Offset: 0x0000EA89
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x00010891 File Offset: 0x0000EA91
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

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0001089A File Offset: 0x0000EA9A
		public bool Active
		{
			get
			{
				return this.active;
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000108A2 File Offset: 0x0000EAA2
		public PhysicsConstraintData getData()
		{
			return this.data;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003AC RID: 940 RVA: 0x000108A2 File Offset: 0x0000EAA2
		public PhysicsConstraintData Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000108AA File Offset: 0x0000EAAA
		public override string ToString()
		{
			return this.data.name;
		}

		// Token: 0x0400020C RID: 524
		internal readonly PhysicsConstraintData data;

		// Token: 0x0400020D RID: 525
		public Bone bone;

		// Token: 0x0400020E RID: 526
		internal float inertia;

		// Token: 0x0400020F RID: 527
		internal float strength;

		// Token: 0x04000210 RID: 528
		internal float damping;

		// Token: 0x04000211 RID: 529
		internal float massInverse;

		// Token: 0x04000212 RID: 530
		internal float wind;

		// Token: 0x04000213 RID: 531
		internal float gravity;

		// Token: 0x04000214 RID: 532
		internal float mix;

		// Token: 0x04000215 RID: 533
		private bool reset = true;

		// Token: 0x04000216 RID: 534
		private float ux;

		// Token: 0x04000217 RID: 535
		private float uy;

		// Token: 0x04000218 RID: 536
		private float cx;

		// Token: 0x04000219 RID: 537
		private float cy;

		// Token: 0x0400021A RID: 538
		private float tx;

		// Token: 0x0400021B RID: 539
		private float ty;

		// Token: 0x0400021C RID: 540
		private float xOffset;

		// Token: 0x0400021D RID: 541
		private float xVelocity;

		// Token: 0x0400021E RID: 542
		private float yOffset;

		// Token: 0x0400021F RID: 543
		private float yVelocity;

		// Token: 0x04000220 RID: 544
		private float rotateOffset;

		// Token: 0x04000221 RID: 545
		private float rotateVelocity;

		// Token: 0x04000222 RID: 546
		private float scaleOffset;

		// Token: 0x04000223 RID: 547
		private float scaleVelocity;

		// Token: 0x04000224 RID: 548
		internal bool active;

		// Token: 0x04000225 RID: 549
		private readonly Skeleton skeleton;

		// Token: 0x04000226 RID: 550
		private float remaining;

		// Token: 0x04000227 RID: 551
		private float lastTime;
	}
}
