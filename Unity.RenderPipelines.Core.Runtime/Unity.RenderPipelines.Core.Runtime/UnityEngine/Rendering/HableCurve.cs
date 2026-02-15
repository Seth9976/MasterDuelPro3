using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001D3 RID: 467
	public class HableCurve
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00030DD3 File Offset: 0x0002EFD3
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x00030DDB File Offset: 0x0002EFDB
		public float whitePoint { get; private set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00030DE4 File Offset: 0x0002EFE4
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x00030DEC File Offset: 0x0002EFEC
		public float inverseWhitePoint { get; private set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00030DF5 File Offset: 0x0002EFF5
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x00030DFD File Offset: 0x0002EFFD
		public float x0 { get; private set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x00030E06 File Offset: 0x0002F006
		// (set) Token: 0x06000D51 RID: 3409 RVA: 0x00030E0E File Offset: 0x0002F00E
		public float x1 { get; private set; }

		// Token: 0x06000D52 RID: 3410 RVA: 0x00030E18 File Offset: 0x0002F018
		public HableCurve()
		{
			for (int i = 0; i < 3; i++)
			{
				this.segments[i] = new HableCurve.Segment();
			}
			this.uniforms = new HableCurve.Uniforms(this);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00030E5C File Offset: 0x0002F05C
		public float Eval(float x)
		{
			float normX = x * this.inverseWhitePoint;
			int index = ((normX < this.x0) ? 0 : ((normX < this.x1) ? 1 : 2));
			return this.segments[index].Eval(normX);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00030E9C File Offset: 0x0002F09C
		public void Init(float toeStrength, float toeLength, float shoulderStrength, float shoulderLength, float shoulderAngle, float gamma)
		{
			HableCurve.DirectParams dstParams = default(HableCurve.DirectParams);
			toeLength = Mathf.Pow(Mathf.Clamp01(toeLength), 2.2f);
			toeStrength = Mathf.Clamp01(toeStrength);
			shoulderAngle = Mathf.Clamp01(shoulderAngle);
			shoulderStrength = Mathf.Clamp(shoulderStrength, 1E-05f, 0.99999f);
			shoulderLength = Mathf.Max(0f, shoulderLength);
			gamma = Mathf.Max(1E-05f, gamma);
			float x0 = toeLength * 0.5f;
			float y0 = (1f - toeStrength) * x0;
			float remainingY = 1f - y0;
			float num = x0 + remainingY;
			float y1_offset = (1f - shoulderStrength) * remainingY;
			float x = x0 + y1_offset;
			float y = y0 + y1_offset;
			float extraW = Mathf.Pow(2f, shoulderLength) - 1f;
			float W = num + extraW;
			dstParams.x0 = x0;
			dstParams.y0 = y0;
			dstParams.x1 = x;
			dstParams.y1 = y;
			dstParams.W = W;
			dstParams.gamma = gamma;
			dstParams.overshootX = dstParams.W * 2f * shoulderAngle * shoulderLength;
			dstParams.overshootY = 0.5f * shoulderAngle * shoulderLength;
			this.InitSegments(dstParams);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00030FB8 File Offset: 0x0002F1B8
		private void InitSegments(HableCurve.DirectParams srcParams)
		{
			HableCurve.DirectParams paramsCopy = srcParams;
			this.whitePoint = srcParams.W;
			this.inverseWhitePoint = 1f / srcParams.W;
			paramsCopy.W = 1f;
			paramsCopy.x0 /= srcParams.W;
			paramsCopy.x1 /= srcParams.W;
			paramsCopy.overshootX = srcParams.overshootX / srcParams.W;
			float i;
			float b;
			this.AsSlopeIntercept(out i, out b, paramsCopy.x0, paramsCopy.x1, paramsCopy.y0, paramsCopy.y1);
			float g = srcParams.gamma;
			HableCurve.Segment segment = this.segments[1];
			segment.offsetX = -(b / i);
			segment.offsetY = 0f;
			segment.scaleX = 1f;
			segment.scaleY = 1f;
			segment.lnA = g * Mathf.Log(i);
			segment.B = g;
			float toeM = this.EvalDerivativeLinearGamma(i, b, g, paramsCopy.x0);
			float shoulderM = this.EvalDerivativeLinearGamma(i, b, g, paramsCopy.x1);
			paramsCopy.y0 = Mathf.Max(1E-05f, Mathf.Pow(paramsCopy.y0, paramsCopy.gamma));
			paramsCopy.y1 = Mathf.Max(1E-05f, Mathf.Pow(paramsCopy.y1, paramsCopy.gamma));
			paramsCopy.overshootY = Mathf.Pow(1f + paramsCopy.overshootY, paramsCopy.gamma) - 1f;
			this.x0 = paramsCopy.x0;
			this.x1 = paramsCopy.x1;
			HableCurve.Segment segment2 = this.segments[0];
			segment2.offsetX = 0f;
			segment2.offsetY = 0f;
			segment2.scaleX = 1f;
			segment2.scaleY = 1f;
			float lnA;
			float B;
			this.SolveAB(out lnA, out B, paramsCopy.x0, paramsCopy.y0, toeM);
			segment2.lnA = lnA;
			segment2.B = B;
			HableCurve.Segment segment3 = this.segments[2];
			float x0 = 1f + paramsCopy.overshootX - paramsCopy.x1;
			float y0 = 1f + paramsCopy.overshootY - paramsCopy.y1;
			float lnA2;
			float B2;
			this.SolveAB(out lnA2, out B2, x0, y0, shoulderM);
			segment3.offsetX = 1f + paramsCopy.overshootX;
			segment3.offsetY = 1f + paramsCopy.overshootY;
			segment3.scaleX = -1f;
			segment3.scaleY = -1f;
			segment3.lnA = lnA2;
			segment3.B = B2;
			float scale = this.segments[2].Eval(1f);
			float invScale = 1f / scale;
			this.segments[0].offsetY *= invScale;
			this.segments[0].scaleY *= invScale;
			this.segments[1].offsetY *= invScale;
			this.segments[1].scaleY *= invScale;
			this.segments[2].offsetY *= invScale;
			this.segments[2].scaleY *= invScale;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x000312D1 File Offset: 0x0002F4D1
		private void SolveAB(out float lnA, out float B, float x0, float y0, float m)
		{
			B = m * x0 / y0;
			lnA = Mathf.Log(y0) - B * Mathf.Log(x0);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x000312F0 File Offset: 0x0002F4F0
		private void AsSlopeIntercept(out float m, out float b, float x0, float x1, float y0, float y1)
		{
			float dy = y1 - y0;
			float dx = x1 - x0;
			if (dx == 0f)
			{
				m = 1f;
			}
			else
			{
				m = dy / dx;
			}
			b = y0 - x0 * m;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00031327 File Offset: 0x0002F527
		private float EvalDerivativeLinearGamma(float m, float b, float g, float x)
		{
			return g * m * Mathf.Pow(m * x + b, g - 1f);
		}

		// Token: 0x040008F9 RID: 2297
		public readonly HableCurve.Segment[] segments = new HableCurve.Segment[3];

		// Token: 0x040008FA RID: 2298
		public readonly HableCurve.Uniforms uniforms;

		// Token: 0x020001D4 RID: 468
		public class Segment
		{
			// Token: 0x06000D59 RID: 3417 RVA: 0x00031340 File Offset: 0x0002F540
			public float Eval(float x)
			{
				float x2 = (x - this.offsetX) * this.scaleX;
				float y0 = 0f;
				if (x2 > 0f)
				{
					y0 = Mathf.Exp(this.lnA + this.B * Mathf.Log(x2));
				}
				return y0 * this.scaleY + this.offsetY;
			}

			// Token: 0x040008FB RID: 2299
			public float offsetX;

			// Token: 0x040008FC RID: 2300
			public float offsetY;

			// Token: 0x040008FD RID: 2301
			public float scaleX;

			// Token: 0x040008FE RID: 2302
			public float scaleY;

			// Token: 0x040008FF RID: 2303
			public float lnA;

			// Token: 0x04000900 RID: 2304
			public float B;
		}

		// Token: 0x020001D5 RID: 469
		private struct DirectParams
		{
			// Token: 0x04000901 RID: 2305
			internal float x0;

			// Token: 0x04000902 RID: 2306
			internal float y0;

			// Token: 0x04000903 RID: 2307
			internal float x1;

			// Token: 0x04000904 RID: 2308
			internal float y1;

			// Token: 0x04000905 RID: 2309
			internal float W;

			// Token: 0x04000906 RID: 2310
			internal float overshootX;

			// Token: 0x04000907 RID: 2311
			internal float overshootY;

			// Token: 0x04000908 RID: 2312
			internal float gamma;
		}

		// Token: 0x020001D6 RID: 470
		public class Uniforms
		{
			// Token: 0x06000D5B RID: 3419 RVA: 0x00031394 File Offset: 0x0002F594
			internal Uniforms(HableCurve parent)
			{
				this.parent = parent;
			}

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x06000D5C RID: 3420 RVA: 0x000313A3 File Offset: 0x0002F5A3
			public Vector4 curve
			{
				get
				{
					return new Vector4(this.parent.inverseWhitePoint, this.parent.x0, this.parent.x1, 0f);
				}
			}

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x06000D5D RID: 3421 RVA: 0x000313D0 File Offset: 0x0002F5D0
			public Vector4 toeSegmentA
			{
				get
				{
					return new Vector4(this.parent.segments[0].offsetX, this.parent.segments[0].offsetY, this.parent.segments[0].scaleX, this.parent.segments[0].scaleY);
				}
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0003142A File Offset: 0x0002F62A
			public Vector4 toeSegmentB
			{
				get
				{
					return new Vector4(this.parent.segments[0].lnA, this.parent.segments[0].B, 0f, 0f);
				}
			}

			// Token: 0x170001AF RID: 431
			// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00031460 File Offset: 0x0002F660
			public Vector4 midSegmentA
			{
				get
				{
					return new Vector4(this.parent.segments[1].offsetX, this.parent.segments[1].offsetY, this.parent.segments[1].scaleX, this.parent.segments[1].scaleY);
				}
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x06000D60 RID: 3424 RVA: 0x000314BA File Offset: 0x0002F6BA
			public Vector4 midSegmentB
			{
				get
				{
					return new Vector4(this.parent.segments[1].lnA, this.parent.segments[1].B, 0f, 0f);
				}
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000D61 RID: 3425 RVA: 0x000314F0 File Offset: 0x0002F6F0
			public Vector4 shoSegmentA
			{
				get
				{
					return new Vector4(this.parent.segments[2].offsetX, this.parent.segments[2].offsetY, this.parent.segments[2].scaleX, this.parent.segments[2].scaleY);
				}
			}

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0003154A File Offset: 0x0002F74A
			public Vector4 shoSegmentB
			{
				get
				{
					return new Vector4(this.parent.segments[2].lnA, this.parent.segments[2].B, 0f, 0f);
				}
			}

			// Token: 0x04000909 RID: 2313
			private HableCurve parent;
		}
	}
}
