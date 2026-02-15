using System;

namespace Spine
{
	// Token: 0x02000084 RID: 132
	public class TransformConstraint : IUpdatable
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x0001C120 File Offset: 0x0001A320
		public TransformConstraint(TransformConstraintData data, Skeleton skeleton)
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
			this.bones = new ExposedList<Bone>();
			foreach (BoneData boneData in data.bones)
			{
				this.bones.Add(skeleton.bones.Items[boneData.index]);
			}
			this.target = skeleton.bones.Items[data.target.index];
			this.mixRotate = data.mixRotate;
			this.mixX = data.mixX;
			this.mixY = data.mixY;
			this.mixScaleX = data.mixScaleX;
			this.mixScaleY = data.mixScaleY;
			this.mixShearY = data.mixShearY;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001C22C File Offset: 0x0001A42C
		public TransformConstraint(TransformConstraint constraint, Skeleton skeleton)
			: this(constraint.data, skeleton)
		{
			this.mixRotate = constraint.mixRotate;
			this.mixX = constraint.mixX;
			this.mixY = constraint.mixY;
			this.mixScaleX = constraint.mixScaleX;
			this.mixScaleY = constraint.mixScaleY;
			this.mixShearY = constraint.mixShearY;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001C290 File Offset: 0x0001A490
		public void SetToSetupPose()
		{
			TransformConstraintData data = this.data;
			this.mixRotate = data.mixRotate;
			this.mixX = data.mixX;
			this.mixY = data.mixY;
			this.mixScaleX = data.mixScaleX;
			this.mixScaleY = data.mixScaleY;
			this.mixShearY = data.mixShearY;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001C2EC File Offset: 0x0001A4EC
		public void Update(Skeleton.Physics physics)
		{
			if (this.mixRotate == 0f && this.mixX == 0f && this.mixY == 0f && this.mixScaleX == 0f && this.mixScaleY == 0f && this.mixShearY == 0f)
			{
				return;
			}
			if (this.data.local)
			{
				if (this.data.relative)
				{
					this.ApplyRelativeLocal();
					return;
				}
				this.ApplyAbsoluteLocal();
				return;
			}
			else
			{
				if (this.data.relative)
				{
					this.ApplyRelativeWorld();
					return;
				}
				this.ApplyAbsoluteWorld();
				return;
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001C38C File Offset: 0x0001A58C
		private void ApplyAbsoluteWorld()
		{
			float mixRotate = this.mixRotate;
			float mixX = this.mixX;
			float mixY = this.mixY;
			float mixScaleX = this.mixScaleX;
			float mixScaleY = this.mixScaleY;
			float mixShearY = this.mixShearY;
			bool translate = mixX != 0f || mixY != 0f;
			Bone target = this.target;
			float ta = target.a;
			float tb = target.b;
			float tc = target.c;
			float td = target.d;
			float degRadReflect = ((ta * td - tb * tc > 0f) ? 0.017453292f : (-0.017453292f));
			float offsetRotation = this.data.offsetRotation * degRadReflect;
			float offsetShearY = this.data.offsetShearY * degRadReflect;
			Bone[] bones = this.bones.Items;
			int i = 0;
			int j = this.bones.Count;
			while (i < j)
			{
				Bone bone = bones[i];
				if (mixRotate != 0f)
				{
					float a = bone.a;
					float b = bone.b;
					float c = bone.c;
					float d = bone.d;
					float r = MathUtils.Atan2(tc, ta) - MathUtils.Atan2(c, a) + offsetRotation;
					if (r > 3.1415927f)
					{
						r -= 6.2831855f;
					}
					else if (r < -3.1415927f)
					{
						r += 6.2831855f;
					}
					r *= mixRotate;
					float cos = MathUtils.Cos(r);
					float sin = MathUtils.Sin(r);
					bone.a = cos * a - sin * c;
					bone.b = cos * b - sin * d;
					bone.c = sin * a + cos * c;
					bone.d = sin * b + cos * d;
				}
				if (translate)
				{
					float tx;
					float ty;
					target.LocalToWorld(this.data.offsetX, this.data.offsetY, out tx, out ty);
					bone.worldX += (tx - bone.worldX) * mixX;
					bone.worldY += (ty - bone.worldY) * mixY;
				}
				if (mixScaleX != 0f)
				{
					float s = (float)Math.Sqrt((double)(bone.a * bone.a + bone.c * bone.c));
					if (s != 0f)
					{
						s = (s + ((float)Math.Sqrt((double)(ta * ta + tc * tc)) - s + this.data.offsetScaleX) * mixScaleX) / s;
					}
					bone.a *= s;
					bone.c *= s;
				}
				if (mixScaleY != 0f)
				{
					float s2 = (float)Math.Sqrt((double)(bone.b * bone.b + bone.d * bone.d));
					if (s2 != 0f)
					{
						s2 = (s2 + ((float)Math.Sqrt((double)(tb * tb + td * td)) - s2 + this.data.offsetScaleY) * mixScaleY) / s2;
					}
					bone.b *= s2;
					bone.d *= s2;
				}
				if (mixShearY > 0f)
				{
					float b2 = bone.b;
					float d2 = bone.d;
					float by = MathUtils.Atan2(d2, b2);
					float r2 = MathUtils.Atan2(td, tb) - MathUtils.Atan2(tc, ta) - (by - MathUtils.Atan2(bone.c, bone.a));
					if (r2 > 3.1415927f)
					{
						r2 -= 6.2831855f;
					}
					else if (r2 < -3.1415927f)
					{
						r2 += 6.2831855f;
					}
					r2 = by + (r2 + offsetShearY) * mixShearY;
					float s3 = (float)Math.Sqrt((double)(b2 * b2 + d2 * d2));
					bone.b = MathUtils.Cos(r2) * s3;
					bone.d = MathUtils.Sin(r2) * s3;
				}
				bone.UpdateAppliedTransform();
				i++;
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001C77C File Offset: 0x0001A97C
		private void ApplyRelativeWorld()
		{
			float mixRotate = this.mixRotate;
			float mixX = this.mixX;
			float mixY = this.mixY;
			float mixScaleX = this.mixScaleX;
			float mixScaleY = this.mixScaleY;
			float mixShearY = this.mixShearY;
			bool translate = mixX != 0f || mixY != 0f;
			Bone target = this.target;
			float ta = target.a;
			float tb = target.b;
			float tc = target.c;
			float td = target.d;
			float degRadReflect = ((ta * td - tb * tc > 0f) ? 0.017453292f : (-0.017453292f));
			float offsetRotation = this.data.offsetRotation * degRadReflect;
			float offsetShearY = this.data.offsetShearY * degRadReflect;
			Bone[] bones = this.bones.Items;
			int i = 0;
			int j = this.bones.Count;
			while (i < j)
			{
				Bone bone = bones[i];
				if (mixRotate != 0f)
				{
					float a = bone.a;
					float b = bone.b;
					float c = bone.c;
					float d = bone.d;
					float r = MathUtils.Atan2(tc, ta) + offsetRotation;
					if (r > 3.1415927f)
					{
						r -= 6.2831855f;
					}
					else if (r < -3.1415927f)
					{
						r += 6.2831855f;
					}
					r *= mixRotate;
					float cos = MathUtils.Cos(r);
					float sin = MathUtils.Sin(r);
					bone.a = cos * a - sin * c;
					bone.b = cos * b - sin * d;
					bone.c = sin * a + cos * c;
					bone.d = sin * b + cos * d;
				}
				if (translate)
				{
					float tx;
					float ty;
					target.LocalToWorld(this.data.offsetX, this.data.offsetY, out tx, out ty);
					bone.worldX += tx * mixX;
					bone.worldY += ty * mixY;
				}
				if (mixScaleX != 0f)
				{
					float s = ((float)Math.Sqrt((double)(ta * ta + tc * tc)) - 1f + this.data.offsetScaleX) * mixScaleX + 1f;
					bone.a *= s;
					bone.c *= s;
				}
				if (mixScaleY != 0f)
				{
					float s2 = ((float)Math.Sqrt((double)(tb * tb + td * td)) - 1f + this.data.offsetScaleY) * mixScaleY + 1f;
					bone.b *= s2;
					bone.d *= s2;
				}
				if (mixShearY > 0f)
				{
					float r2 = MathUtils.Atan2(td, tb) - MathUtils.Atan2(tc, ta);
					if (r2 > 3.1415927f)
					{
						r2 -= 6.2831855f;
					}
					else if (r2 < -3.1415927f)
					{
						r2 += 6.2831855f;
					}
					float b2 = bone.b;
					float d2 = bone.d;
					r2 = MathUtils.Atan2(d2, b2) + (r2 - 1.5707964f + offsetShearY) * mixShearY;
					float s3 = (float)Math.Sqrt((double)(b2 * b2 + d2 * d2));
					bone.b = MathUtils.Cos(r2) * s3;
					bone.d = MathUtils.Sin(r2) * s3;
				}
				bone.UpdateAppliedTransform();
				i++;
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001CAE0 File Offset: 0x0001ACE0
		private void ApplyAbsoluteLocal()
		{
			float mixRotate = this.mixRotate;
			float mixX = this.mixX;
			float mixY = this.mixY;
			float mixScaleX = this.mixScaleX;
			float mixScaleY = this.mixScaleY;
			float mixShearY = this.mixShearY;
			Bone target = this.target;
			Bone[] bones = this.bones.Items;
			int i = 0;
			int j = this.bones.Count;
			while (i < j)
			{
				Bone bone = bones[i];
				float rotation = bone.arotation;
				if (mixRotate != 0f)
				{
					rotation += (target.arotation - rotation + this.data.offsetRotation) * mixRotate;
				}
				float x = bone.ax;
				float y = bone.ay;
				x += (target.ax - x + this.data.offsetX) * mixX;
				y += (target.ay - y + this.data.offsetY) * mixY;
				float scaleX = bone.ascaleX;
				float scaleY = bone.ascaleY;
				if (mixScaleX != 0f && scaleX != 0f)
				{
					scaleX = (scaleX + (target.ascaleX - scaleX + this.data.offsetScaleX) * mixScaleX) / scaleX;
				}
				if (mixScaleY != 0f && scaleY != 0f)
				{
					scaleY = (scaleY + (target.ascaleY - scaleY + this.data.offsetScaleY) * mixScaleY) / scaleY;
				}
				float shearY = bone.ashearY;
				if (mixShearY != 0f)
				{
					shearY += (target.ashearY - shearY + this.data.offsetShearY) * mixShearY;
				}
				bone.UpdateWorldTransform(x, y, rotation, scaleX, scaleY, bone.ashearX, shearY);
				i++;
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001CC94 File Offset: 0x0001AE94
		private void ApplyRelativeLocal()
		{
			float mixRotate = this.mixRotate;
			float mixX = this.mixX;
			float mixY = this.mixY;
			float mixScaleX = this.mixScaleX;
			float mixScaleY = this.mixScaleY;
			float mixShearY = this.mixShearY;
			Bone target = this.target;
			Bone[] bones = this.bones.Items;
			int i = 0;
			int j = this.bones.Count;
			while (i < j)
			{
				Bone bone = bones[i];
				float rotation = bone.arotation + (target.arotation + this.data.offsetRotation) * mixRotate;
				float x = bone.ax + (target.ax + this.data.offsetX) * mixX;
				float y = bone.ay + (target.ay + this.data.offsetY) * mixY;
				float scaleX = bone.ascaleX * ((target.ascaleX - 1f + this.data.offsetScaleX) * mixScaleX + 1f);
				float scaleY = bone.ascaleY * ((target.ascaleY - 1f + this.data.offsetScaleY) * mixScaleY + 1f);
				float shearY = bone.ashearY + (target.ashearY + this.data.offsetShearY) * mixShearY;
				bone.UpdateWorldTransform(x, y, rotation, scaleX, scaleY, bone.ashearX, shearY);
				i++;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0001CDFB File Offset: 0x0001AFFB
		public ExposedList<Bone> Bones
		{
			get
			{
				return this.bones;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0001CE03 File Offset: 0x0001B003
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x0001CE0B File Offset: 0x0001B00B
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

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0001CE14 File Offset: 0x0001B014
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x0001CE1C File Offset: 0x0001B01C
		public float MixRotate
		{
			get
			{
				return this.mixRotate;
			}
			set
			{
				this.mixRotate = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0001CE25 File Offset: 0x0001B025
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x0001CE2D File Offset: 0x0001B02D
		public float MixX
		{
			get
			{
				return this.mixX;
			}
			set
			{
				this.mixX = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0001CE36 File Offset: 0x0001B036
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x0001CE3E File Offset: 0x0001B03E
		public float MixY
		{
			get
			{
				return this.mixY;
			}
			set
			{
				this.mixY = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001CE47 File Offset: 0x0001B047
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x0001CE4F File Offset: 0x0001B04F
		public float MixScaleX
		{
			get
			{
				return this.mixScaleX;
			}
			set
			{
				this.mixScaleX = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0001CE58 File Offset: 0x0001B058
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x0001CE60 File Offset: 0x0001B060
		public float MixScaleY
		{
			get
			{
				return this.mixScaleY;
			}
			set
			{
				this.mixScaleY = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0001CE69 File Offset: 0x0001B069
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x0001CE71 File Offset: 0x0001B071
		public float MixShearY
		{
			get
			{
				return this.mixShearY;
			}
			set
			{
				this.mixShearY = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0001CE7A File Offset: 0x0001B07A
		public bool Active
		{
			get
			{
				return this.active;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0001CE82 File Offset: 0x0001B082
		public TransformConstraintData Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001CE8A File Offset: 0x0001B08A
		public override string ToString()
		{
			return this.data.name;
		}

		// Token: 0x040002E0 RID: 736
		internal readonly TransformConstraintData data;

		// Token: 0x040002E1 RID: 737
		internal readonly ExposedList<Bone> bones;

		// Token: 0x040002E2 RID: 738
		internal Bone target;

		// Token: 0x040002E3 RID: 739
		internal float mixRotate;

		// Token: 0x040002E4 RID: 740
		internal float mixX;

		// Token: 0x040002E5 RID: 741
		internal float mixY;

		// Token: 0x040002E6 RID: 742
		internal float mixScaleX;

		// Token: 0x040002E7 RID: 743
		internal float mixScaleY;

		// Token: 0x040002E8 RID: 744
		internal float mixShearY;

		// Token: 0x040002E9 RID: 745
		internal bool active;
	}
}
