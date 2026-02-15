using System;

namespace Spine
{
	// Token: 0x02000069 RID: 105
	public class PathConstraint : IUpdatable
	{
		// Token: 0x06000364 RID: 868 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		public PathConstraint(PathConstraintData data, Skeleton skeleton)
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
			this.bones = new ExposedList<Bone>(data.Bones.Count);
			foreach (BoneData boneData in data.bones)
			{
				this.bones.Add(skeleton.bones.Items[boneData.index]);
			}
			this.target = skeleton.slots.Items[data.target.index];
			this.position = data.position;
			this.spacing = data.spacing;
			this.mixRotate = data.mixRotate;
			this.mixX = data.mixX;
			this.mixY = data.mixY;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000EB44 File Offset: 0x0000CD44
		public PathConstraint(PathConstraint constraint, Skeleton skeleton)
			: this(constraint.data, skeleton)
		{
			this.position = constraint.position;
			this.spacing = constraint.spacing;
			this.mixRotate = constraint.mixRotate;
			this.mixX = constraint.mixX;
			this.mixY = constraint.mixY;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000EB9C File Offset: 0x0000CD9C
		public static void ArraysFill(float[] a, int fromIndex, int toIndex, float val)
		{
			for (int i = fromIndex; i < toIndex; i++)
			{
				a[i] = val;
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000EBBC File Offset: 0x0000CDBC
		public void SetToSetupPose()
		{
			PathConstraintData data = this.data;
			this.position = data.position;
			this.spacing = data.spacing;
			this.mixRotate = data.mixRotate;
			this.mixX = data.mixX;
			this.mixY = data.mixY;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000EC0C File Offset: 0x0000CE0C
		public void Update(Skeleton.Physics physics)
		{
			PathAttachment attachment = this.target.Attachment as PathAttachment;
			if (attachment == null)
			{
				return;
			}
			float mixRotate = this.mixRotate;
			float mixX = this.mixX;
			float mixY = this.mixY;
			if (mixRotate == 0f && mixX == 0f && mixY == 0f)
			{
				return;
			}
			PathConstraintData data = this.data;
			bool tangents = data.rotateMode == RotateMode.Tangent;
			bool scale = data.rotateMode == RotateMode.ChainScale;
			int boneCount = this.bones.Count;
			int spacesCount = (tangents ? boneCount : (boneCount + 1));
			Bone[] bonesItems = this.bones.Items;
			float[] spaces = this.spaces.Resize(spacesCount).Items;
			float[] lengths = (scale ? this.lengths.Resize(boneCount).Items : null);
			float spacing = this.spacing;
			SpacingMode spacingMode = data.spacingMode;
			if (spacingMode != SpacingMode.Percent)
			{
				if (spacingMode != SpacingMode.Proportional)
				{
					bool lengthSpacing = data.spacingMode == SpacingMode.Length;
					int i = 0;
					int j = spacesCount - 1;
					while (i < j)
					{
						Bone bone = bonesItems[i];
						float setupLength = bone.data.length;
						if (setupLength < 1E-05f)
						{
							if (scale)
							{
								lengths[i] = 0f;
							}
							spaces[++i] = spacing;
						}
						else
						{
							float num = setupLength * bone.a;
							float y = setupLength * bone.c;
							float length = (float)Math.Sqrt((double)(num * num + y * y));
							if (scale)
							{
								lengths[i] = length;
							}
							spaces[++i] = (lengthSpacing ? (setupLength + spacing) : spacing) * length / setupLength;
						}
					}
				}
				else
				{
					float sum = 0f;
					int k = 0;
					int l = spacesCount - 1;
					while (k < l)
					{
						Bone bone2 = bonesItems[k];
						float setupLength2 = bone2.data.length;
						if (setupLength2 < 1E-05f)
						{
							if (scale)
							{
								lengths[k] = 0f;
							}
							spaces[++k] = spacing;
						}
						else
						{
							float x = setupLength2 * bone2.a;
							float y2 = setupLength2 * bone2.c;
							float length2 = (float)Math.Sqrt((double)(x * x + y2 * y2));
							if (scale)
							{
								lengths[k] = length2;
							}
							spaces[++k] = length2;
							sum += length2;
						}
					}
					if (sum > 0f)
					{
						sum = (float)spacesCount / sum * spacing;
						for (int m = 1; m < spacesCount; m++)
						{
							spaces[m] *= sum;
						}
					}
				}
			}
			else
			{
				if (scale)
				{
					int n = 0;
					int n2 = spacesCount - 1;
					while (n < n2)
					{
						Bone bone3 = bonesItems[n];
						float length5 = bone3.data.length;
						float x2 = length5 * bone3.a;
						float y3 = length5 * bone3.c;
						lengths[n] = (float)Math.Sqrt((double)(x2 * x2 + y3 * y3));
						n++;
					}
				}
				PathConstraint.ArraysFill(spaces, 1, spacesCount, spacing);
			}
			float[] positions = this.ComputeWorldPositions(attachment, spacesCount, tangents);
			float boneX = positions[0];
			float boneY = positions[1];
			float offsetRotation = data.offsetRotation;
			bool tip;
			if (offsetRotation == 0f)
			{
				tip = data.rotateMode == RotateMode.Chain;
			}
			else
			{
				tip = false;
				Bone p = this.target.bone;
				offsetRotation *= ((p.a * p.d - p.b * p.c > 0f) ? 0.017453292f : (-0.017453292f));
			}
			int i2 = 0;
			int p2 = 3;
			while (i2 < boneCount)
			{
				Bone bone4 = bonesItems[i2];
				bone4.worldX += (boneX - bone4.worldX) * mixX;
				bone4.worldY += (boneY - bone4.worldY) * mixY;
				float x3 = positions[p2];
				float num2 = positions[p2 + 1];
				float dx = x3 - boneX;
				float dy = num2 - boneY;
				if (scale)
				{
					float length3 = lengths[i2];
					if (length3 >= 1E-05f)
					{
						float s = ((float)Math.Sqrt((double)(dx * dx + dy * dy)) / length3 - 1f) * mixRotate + 1f;
						bone4.a *= s;
						bone4.c *= s;
					}
				}
				boneX = x3;
				boneY = num2;
				if (mixRotate > 0f)
				{
					float a = bone4.a;
					float b = bone4.b;
					float c = bone4.c;
					float d = bone4.d;
					float r;
					if (tangents)
					{
						r = positions[p2 - 1];
					}
					else if (spaces[i2 + 1] < 1E-05f)
					{
						r = positions[p2 + 2];
					}
					else
					{
						r = MathUtils.Atan2(dy, dx);
					}
					r -= MathUtils.Atan2(c, a);
					float cos;
					float sin;
					if (tip)
					{
						cos = MathUtils.Cos(r);
						sin = MathUtils.Sin(r);
						float length4 = bone4.data.length;
						boneX += (length4 * (cos * a - sin * c) - dx) * mixRotate;
						boneY += (length4 * (sin * a + cos * c) - dy) * mixRotate;
					}
					else
					{
						r += offsetRotation;
					}
					if (r > 3.1415927f)
					{
						r -= 6.2831855f;
					}
					else if (r < -3.1415927f)
					{
						r += 6.2831855f;
					}
					r *= mixRotate;
					cos = MathUtils.Cos(r);
					sin = MathUtils.Sin(r);
					bone4.a = cos * a - sin * c;
					bone4.b = cos * b - sin * d;
					bone4.c = sin * a + cos * c;
					bone4.d = sin * b + cos * d;
				}
				bone4.UpdateAppliedTransform();
				i2++;
				p2 += 3;
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000F19C File Offset: 0x0000D39C
		private float[] ComputeWorldPositions(PathAttachment path, int spacesCount, bool tangents)
		{
			Slot target = this.target;
			float position = this.position;
			float[] spaces = this.spaces.Items;
			float[] output = this.positions.Resize(spacesCount * 3 + 2).Items;
			bool closed = path.Closed;
			int verticesLength = path.WorldVerticesLength;
			int curveCount = verticesLength / 6;
			int prevCurve = -1;
			float pathLength;
			SpacingMode spacingMode;
			float multiplier;
			float[] world;
			if (!path.ConstantSpeed)
			{
				float[] lengths = path.Lengths;
				curveCount -= (closed ? 1 : 2);
				pathLength = lengths[curveCount];
				if (this.data.positionMode == PositionMode.Percent)
				{
					position *= pathLength;
				}
				spacingMode = this.data.spacingMode;
				if (spacingMode != SpacingMode.Percent)
				{
					if (spacingMode != SpacingMode.Proportional)
					{
						multiplier = 1f;
					}
					else
					{
						multiplier = pathLength / (float)spacesCount;
					}
				}
				else
				{
					multiplier = pathLength;
				}
				world = this.world.Resize(8).Items;
				int i = 0;
				int o = 0;
				int curve = 0;
				while (i < spacesCount)
				{
					float space = spaces[i] * multiplier;
					position += space;
					float p = position;
					if (closed)
					{
						p %= pathLength;
						if (p < 0f)
						{
							p += pathLength;
						}
						curve = 0;
					}
					else
					{
						if (p < 0f)
						{
							if (prevCurve != -2)
							{
								prevCurve = -2;
								path.ComputeWorldVertices(target, 2, 4, world, 0, 2);
							}
							PathConstraint.AddBeforePosition(p, world, 0, output, o);
							goto IL_022B;
						}
						if (p > pathLength)
						{
							if (prevCurve != -3)
							{
								prevCurve = -3;
								path.ComputeWorldVertices(target, verticesLength - 6, 4, world, 0, 2);
							}
							PathConstraint.AddAfterPosition(p - pathLength, world, 0, output, o);
							goto IL_022B;
						}
					}
					float length;
					for (;;)
					{
						length = lengths[curve];
						if (p <= length)
						{
							break;
						}
						curve++;
					}
					if (curve == 0)
					{
						p /= length;
					}
					else
					{
						float prev = lengths[curve - 1];
						p = (p - prev) / (length - prev);
					}
					if (curve != prevCurve)
					{
						prevCurve = curve;
						if (closed && curve == curveCount)
						{
							path.ComputeWorldVertices(target, verticesLength - 4, 4, world, 0, 2);
							path.ComputeWorldVertices(target, 0, 4, world, 4, 2);
						}
						else
						{
							path.ComputeWorldVertices(target, curve * 6 + 2, 8, world, 0, 2);
						}
					}
					PathConstraint.AddCurvePosition(p, world[0], world[1], world[2], world[3], world[4], world[5], world[6], world[7], output, o, tangents || (i > 0 && space < 1E-05f));
					IL_022B:
					i++;
					o += 3;
				}
				return output;
			}
			if (closed)
			{
				verticesLength += 2;
				world = this.world.Resize(verticesLength).Items;
				path.ComputeWorldVertices(target, 2, verticesLength - 4, world, 0, 2);
				path.ComputeWorldVertices(target, 0, 2, world, verticesLength - 4, 2);
				world[verticesLength - 2] = world[0];
				world[verticesLength - 1] = world[1];
			}
			else
			{
				curveCount--;
				verticesLength -= 4;
				world = this.world.Resize(verticesLength).Items;
				path.ComputeWorldVertices(target, 2, verticesLength, world, 0, 2);
			}
			float[] curves = this.curves.Resize(curveCount).Items;
			pathLength = 0f;
			float x = world[0];
			float y = world[1];
			float cx = 0f;
			float cy = 0f;
			float cx2 = 0f;
			float cy2 = 0f;
			float x2 = 0f;
			float y2 = 0f;
			int j = 0;
			int w = 2;
			while (j < curveCount)
			{
				cx = world[w];
				cy = world[w + 1];
				cx2 = world[w + 2];
				cy2 = world[w + 3];
				x2 = world[w + 4];
				y2 = world[w + 5];
				float tmpx = (x - cx * 2f + cx2) * 0.1875f;
				float tmpy = (y - cy * 2f + cy2) * 0.1875f;
				float dddfx = ((cx - cx2) * 3f - x + x2) * 0.09375f;
				float dddfy = ((cy - cy2) * 3f - y + y2) * 0.09375f;
				float ddfx = tmpx * 2f + dddfx;
				float ddfy = tmpy * 2f + dddfy;
				float dfx = (cx - x) * 0.75f + tmpx + dddfx * 0.16666667f;
				float dfy = (cy - y) * 0.75f + tmpy + dddfy * 0.16666667f;
				pathLength += (float)Math.Sqrt((double)(dfx * dfx + dfy * dfy));
				dfx += ddfx;
				dfy += ddfy;
				ddfx += dddfx;
				ddfy += dddfy;
				pathLength += (float)Math.Sqrt((double)(dfx * dfx + dfy * dfy));
				dfx += ddfx;
				dfy += ddfy;
				pathLength += (float)Math.Sqrt((double)(dfx * dfx + dfy * dfy));
				dfx += ddfx + dddfx;
				dfy += ddfy + dddfy;
				pathLength += (float)Math.Sqrt((double)(dfx * dfx + dfy * dfy));
				curves[j] = pathLength;
				x = x2;
				y = y2;
				j++;
				w += 6;
			}
			if (this.data.positionMode == PositionMode.Percent)
			{
				position *= pathLength;
			}
			spacingMode = this.data.spacingMode;
			if (spacingMode != SpacingMode.Percent)
			{
				if (spacingMode != SpacingMode.Proportional)
				{
					multiplier = 1f;
				}
				else
				{
					multiplier = pathLength / (float)spacesCount;
				}
			}
			else
			{
				multiplier = pathLength;
			}
			float[] segments = this.segments;
			float curveLength = 0f;
			int k = 0;
			int o2 = 0;
			int curve2 = 0;
			int segment = 0;
			while (k < spacesCount)
			{
				float space2 = spaces[k] * multiplier;
				position += space2;
				float p2 = position;
				if (closed)
				{
					p2 %= pathLength;
					if (p2 < 0f)
					{
						p2 += pathLength;
					}
					curve2 = 0;
				}
				else
				{
					if (p2 < 0f)
					{
						PathConstraint.AddBeforePosition(p2, world, 0, output, o2);
						goto IL_0806;
					}
					if (p2 > pathLength)
					{
						PathConstraint.AddAfterPosition(p2 - pathLength, world, verticesLength - 4, output, o2);
						goto IL_0806;
					}
				}
				float length2;
				for (;;)
				{
					length2 = curves[curve2];
					if (p2 <= length2)
					{
						break;
					}
					curve2++;
				}
				if (curve2 == 0)
				{
					p2 /= length2;
				}
				else
				{
					float prev2 = curves[curve2 - 1];
					p2 = (p2 - prev2) / (length2 - prev2);
				}
				if (curve2 != prevCurve)
				{
					prevCurve = curve2;
					int ii = curve2 * 6;
					x = world[ii];
					y = world[ii + 1];
					cx = world[ii + 2];
					cy = world[ii + 3];
					cx2 = world[ii + 4];
					cy2 = world[ii + 5];
					x2 = world[ii + 6];
					y2 = world[ii + 7];
					float tmpx = (x - cx * 2f + cx2) * 0.03f;
					float tmpy = (y - cy * 2f + cy2) * 0.03f;
					float dddfx = ((cx - cx2) * 3f - x + x2) * 0.006f;
					float dddfy = ((cy - cy2) * 3f - y + y2) * 0.006f;
					float ddfx = tmpx * 2f + dddfx;
					float ddfy = tmpy * 2f + dddfy;
					float dfx = (cx - x) * 0.3f + tmpx + dddfx * 0.16666667f;
					float dfy = (cy - y) * 0.3f + tmpy + dddfy * 0.16666667f;
					curveLength = (float)Math.Sqrt((double)(dfx * dfx + dfy * dfy));
					segments[0] = curveLength;
					for (ii = 1; ii < 8; ii++)
					{
						dfx += ddfx;
						dfy += ddfy;
						ddfx += dddfx;
						ddfy += dddfy;
						curveLength += (float)Math.Sqrt((double)(dfx * dfx + dfy * dfy));
						segments[ii] = curveLength;
					}
					dfx += ddfx;
					dfy += ddfy;
					curveLength += (float)Math.Sqrt((double)(dfx * dfx + dfy * dfy));
					segments[8] = curveLength;
					dfx += ddfx + dddfx;
					dfy += ddfy + dddfy;
					curveLength += (float)Math.Sqrt((double)(dfx * dfx + dfy * dfy));
					segments[9] = curveLength;
					segment = 0;
				}
				p2 *= curveLength;
				float length3;
				for (;;)
				{
					length3 = segments[segment];
					if (p2 <= length3)
					{
						break;
					}
					segment++;
				}
				if (segment == 0)
				{
					p2 /= length3;
				}
				else
				{
					float prev3 = segments[segment - 1];
					p2 = (float)segment + (p2 - prev3) / (length3 - prev3);
				}
				PathConstraint.AddCurvePosition(p2 * 0.1f, x, y, cx, cy, cx2, cy2, x2, y2, output, o2, tangents || (k > 0 && space2 < 1E-05f));
				IL_0806:
				k++;
				o2 += 3;
			}
			return output;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		private static void AddBeforePosition(float p, float[] temp, int i, float[] output, int o)
		{
			float x = temp[i];
			float y = temp[i + 1];
			float dx = temp[i + 2] - x;
			float r = MathUtils.Atan2(temp[i + 3] - y, dx);
			output[o] = x + p * MathUtils.Cos(r);
			output[o + 1] = y + p * MathUtils.Sin(r);
			output[o + 2] = r;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000FA18 File Offset: 0x0000DC18
		private static void AddAfterPosition(float p, float[] temp, int i, float[] output, int o)
		{
			float x = temp[i + 2];
			float y = temp[i + 3];
			float dx = x - temp[i];
			float r = MathUtils.Atan2(y - temp[i + 1], dx);
			output[o] = x + p * MathUtils.Cos(r);
			output[o + 1] = y + p * MathUtils.Sin(r);
			output[o + 2] = r;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000FA6C File Offset: 0x0000DC6C
		private static void AddCurvePosition(float p, float x1, float y1, float cx1, float cy1, float cx2, float cy2, float x2, float y2, float[] output, int o, bool tangents)
		{
			if (p < 1E-05f || float.IsNaN(p))
			{
				output[o] = x1;
				output[o + 1] = y1;
				output[o + 2] = (float)Math.Atan2((double)(cy1 - y1), (double)(cx1 - x1));
				return;
			}
			float tt = p * p;
			float ttt = tt * p;
			float u = 1f - p;
			float uu = u * u;
			float uuu = uu * u;
			float ut = u * p;
			float ut2 = ut * 3f;
			float uut3 = u * ut2;
			float utt3 = ut2 * p;
			float x3 = x1 * uuu + cx1 * uut3 + cx2 * utt3 + x2 * ttt;
			float y3 = y1 * uuu + cy1 * uut3 + cy2 * utt3 + y2 * ttt;
			output[o] = x3;
			output[o + 1] = y3;
			if (tangents)
			{
				if (p < 0.001f)
				{
					output[o + 2] = (float)Math.Atan2((double)(cy1 - y1), (double)(cx1 - x1));
					return;
				}
				output[o + 2] = (float)Math.Atan2((double)(y3 - (y1 * uu + cy1 * ut * 2f + cy2 * tt)), (double)(x3 - (x1 * uu + cx1 * ut * 2f + cx2 * tt)));
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000FB7E File Offset: 0x0000DD7E
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000FB86 File Offset: 0x0000DD86
		public float Position
		{
			get
			{
				return this.position;
			}
			set
			{
				this.position = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000FB8F File Offset: 0x0000DD8F
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000FB97 File Offset: 0x0000DD97
		public float Spacing
		{
			get
			{
				return this.spacing;
			}
			set
			{
				this.spacing = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000FBA8 File Offset: 0x0000DDA8
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

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000FBB1 File Offset: 0x0000DDB1
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000FBB9 File Offset: 0x0000DDB9
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

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000FBC2 File Offset: 0x0000DDC2
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000FBCA File Offset: 0x0000DDCA
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

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000FBD3 File Offset: 0x0000DDD3
		public ExposedList<Bone> Bones
		{
			get
			{
				return this.bones;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000FBDB File Offset: 0x0000DDDB
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0000FBE3 File Offset: 0x0000DDE3
		public Slot Target
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

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000FBEC File Offset: 0x0000DDEC
		public bool Active
		{
			get
			{
				return this.active;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000FBF4 File Offset: 0x0000DDF4
		public PathConstraintData Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000FBFC File Offset: 0x0000DDFC
		public override string ToString()
		{
			return this.data.name;
		}

		// Token: 0x040001E2 RID: 482
		private const int NONE = -1;

		// Token: 0x040001E3 RID: 483
		private const int BEFORE = -2;

		// Token: 0x040001E4 RID: 484
		private const int AFTER = -3;

		// Token: 0x040001E5 RID: 485
		private const float Epsilon = 1E-05f;

		// Token: 0x040001E6 RID: 486
		internal readonly PathConstraintData data;

		// Token: 0x040001E7 RID: 487
		internal readonly ExposedList<Bone> bones;

		// Token: 0x040001E8 RID: 488
		internal Slot target;

		// Token: 0x040001E9 RID: 489
		internal float position;

		// Token: 0x040001EA RID: 490
		internal float spacing;

		// Token: 0x040001EB RID: 491
		internal float mixRotate;

		// Token: 0x040001EC RID: 492
		internal float mixX;

		// Token: 0x040001ED RID: 493
		internal float mixY;

		// Token: 0x040001EE RID: 494
		internal bool active;

		// Token: 0x040001EF RID: 495
		internal readonly ExposedList<float> spaces = new ExposedList<float>();

		// Token: 0x040001F0 RID: 496
		internal readonly ExposedList<float> positions = new ExposedList<float>();

		// Token: 0x040001F1 RID: 497
		internal readonly ExposedList<float> world = new ExposedList<float>();

		// Token: 0x040001F2 RID: 498
		internal readonly ExposedList<float> curves = new ExposedList<float>();

		// Token: 0x040001F3 RID: 499
		internal readonly ExposedList<float> lengths = new ExposedList<float>();

		// Token: 0x040001F4 RID: 500
		internal readonly float[] segments = new float[10];
	}
}
