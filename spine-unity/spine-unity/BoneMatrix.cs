using System;

namespace Spine
{
	// Token: 0x02000004 RID: 4
	public struct BoneMatrix
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public static BoneMatrix CalculateSetupWorld(BoneData boneData)
		{
			if (boneData == null)
			{
				return default(BoneMatrix);
			}
			if (boneData.Parent == null)
			{
				return BoneMatrix.GetInheritedInternal(boneData, default(BoneMatrix));
			}
			BoneMatrix result = BoneMatrix.CalculateSetupWorld(boneData.Parent);
			return BoneMatrix.GetInheritedInternal(boneData, result);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002108 File Offset: 0x00000308
		private static BoneMatrix GetInheritedInternal(BoneData boneData, BoneMatrix parentMatrix)
		{
			if (boneData.Parent == null)
			{
				return new BoneMatrix(boneData);
			}
			float pa = parentMatrix.a;
			float pb = parentMatrix.b;
			float pc = parentMatrix.c;
			float pd = parentMatrix.d;
			BoneMatrix result = default(BoneMatrix);
			result.x = pa * boneData.X + pb * boneData.Y + parentMatrix.x;
			result.y = pc * boneData.X + pd * boneData.Y + parentMatrix.y;
			switch (boneData.Inherit)
			{
			case Inherit.Normal:
			{
				float num = boneData.Rotation + 90f + boneData.ShearY;
				float la = MathUtils.CosDeg(boneData.Rotation + boneData.ShearX) * boneData.ScaleX;
				float lb = MathUtils.CosDeg(num) * boneData.ScaleY;
				float lc = MathUtils.SinDeg(boneData.Rotation + boneData.ShearX) * boneData.ScaleX;
				float ld = MathUtils.SinDeg(num) * boneData.ScaleY;
				result.a = pa * la + pb * lc;
				result.b = pa * lb + pb * ld;
				result.c = pc * la + pd * lc;
				result.d = pc * lb + pd * ld;
				break;
			}
			case Inherit.OnlyTranslation:
			{
				float rotationY = boneData.Rotation + 90f + boneData.ShearY;
				result.a = MathUtils.CosDeg(boneData.Rotation + boneData.ShearX) * boneData.ScaleX;
				result.b = MathUtils.CosDeg(rotationY) * boneData.ScaleY;
				result.c = MathUtils.SinDeg(boneData.Rotation + boneData.ShearX) * boneData.ScaleX;
				result.d = MathUtils.SinDeg(rotationY) * boneData.ScaleY;
				break;
			}
			case Inherit.NoRotationOrReflection:
			{
				float s = pa * pa + pc * pc;
				float prx;
				if (s > 0.0001f)
				{
					s = Math.Abs(pa * pd - pb * pc) / s;
					pb = pc * s;
					pd = pa * s;
					prx = MathUtils.Atan2(pc, pa) * 57.295776f;
				}
				else
				{
					pa = 0f;
					pc = 0f;
					prx = 90f - MathUtils.Atan2(pd, pb) * 57.295776f;
				}
				float rx = boneData.Rotation + boneData.ShearX - prx;
				float num2 = boneData.Rotation + boneData.ShearY - prx + 90f;
				float la2 = MathUtils.CosDeg(rx) * boneData.ScaleX;
				float lb2 = MathUtils.CosDeg(num2) * boneData.ScaleY;
				float lc2 = MathUtils.SinDeg(rx) * boneData.ScaleX;
				float ld2 = MathUtils.SinDeg(num2) * boneData.ScaleY;
				result.a = pa * la2 - pb * lc2;
				result.b = pa * lb2 - pb * ld2;
				result.c = pc * la2 + pd * lc2;
				result.d = pc * lb2 + pd * ld2;
				break;
			}
			case Inherit.NoScale:
			case Inherit.NoScaleOrReflection:
			{
				float cos = MathUtils.CosDeg(boneData.Rotation);
				float sin = MathUtils.SinDeg(boneData.Rotation);
				float za = pa * cos + pb * sin;
				float zc = pc * cos + pd * sin;
				float s2 = (float)Math.Sqrt((double)(za * za + zc * zc));
				if (s2 > 1E-05f)
				{
					s2 = 1f / s2;
				}
				za *= s2;
				zc *= s2;
				s2 = (float)Math.Sqrt((double)(za * za + zc * zc));
				float num3 = 1.5707964f + MathUtils.Atan2(zc, za);
				float zb = MathUtils.Cos(num3) * s2;
				float zd = MathUtils.Sin(num3) * s2;
				float la3 = MathUtils.CosDeg(boneData.ShearX) * boneData.ScaleX;
				float lb3 = MathUtils.CosDeg(90f + boneData.ShearY) * boneData.ScaleY;
				float lc3 = MathUtils.SinDeg(boneData.ShearX) * boneData.ScaleX;
				float ld3 = MathUtils.SinDeg(90f + boneData.ShearY) * boneData.ScaleY;
				if (boneData.Inherit != Inherit.NoScaleOrReflection && pa * pd - pb * pc < 0f)
				{
					zb = -zb;
					zd = -zd;
				}
				result.a = za * la3 + zb * lc3;
				result.b = za * lb3 + zb * ld3;
				result.c = zc * la3 + zd * lc3;
				result.d = zc * lb3 + zd * ld3;
				break;
			}
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002550 File Offset: 0x00000750
		public BoneMatrix(BoneData boneData)
		{
			float rotationY = boneData.Rotation + 90f + boneData.ShearY;
			float rotationX = boneData.Rotation + boneData.ShearX;
			this.a = MathUtils.CosDeg(rotationX) * boneData.ScaleX;
			this.c = MathUtils.SinDeg(rotationX) * boneData.ScaleX;
			this.b = MathUtils.CosDeg(rotationY) * boneData.ScaleY;
			this.d = MathUtils.SinDeg(rotationY) * boneData.ScaleY;
			this.x = boneData.X;
			this.y = boneData.Y;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000025E4 File Offset: 0x000007E4
		public BoneMatrix(Bone bone)
		{
			float rotationY = bone.Rotation + 90f + bone.ShearY;
			float rotationX = bone.Rotation + bone.ShearX;
			this.a = MathUtils.CosDeg(rotationX) * bone.ScaleX;
			this.c = MathUtils.SinDeg(rotationX) * bone.ScaleX;
			this.b = MathUtils.CosDeg(rotationY) * bone.ScaleY;
			this.d = MathUtils.SinDeg(rotationY) * bone.ScaleY;
			this.x = bone.X;
			this.y = bone.Y;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002678 File Offset: 0x00000878
		public BoneMatrix TransformMatrix(BoneMatrix local)
		{
			return new BoneMatrix
			{
				a = this.a * local.a + this.b * local.c,
				b = this.a * local.b + this.b * local.d,
				c = this.c * local.a + this.d * local.c,
				d = this.c * local.b + this.d * local.d,
				x = this.a * local.x + this.b * local.y + this.x,
				y = this.c * local.x + this.d * local.y + this.y
			};
		}

		// Token: 0x04000006 RID: 6
		public float a;

		// Token: 0x04000007 RID: 7
		public float b;

		// Token: 0x04000008 RID: 8
		public float c;

		// Token: 0x04000009 RID: 9
		public float d;

		// Token: 0x0400000A RID: 10
		public float x;

		// Token: 0x0400000B RID: 11
		public float y;
	}
}
