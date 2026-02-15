using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200008F RID: 143
	internal class ConfinerOven
	{
		// Token: 0x06000372 RID: 882 RVA: 0x000143C0 File Offset: 0x000125C0
		public ConfinerOven(in List<List<Vector2>> inputPath, in float aspectRatio, float maxFrustumHeight, float skeletonPadding)
		{
			this.Initialize(in inputPath, in aspectRatio, maxFrustumHeight, Mathf.Max(0f, skeletonPadding) + 1f);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001441C File Offset: 0x0001261C
		public ConfinerOven.BakedSolution GetBakedSolution(float frustumHeight)
		{
			frustumHeight = ((this.m_Cache.userSetMaxFrustumHeight <= 0f) ? frustumHeight : Mathf.Min(this.m_Cache.userSetMaxFrustumHeight, frustumHeight));
			if (this.State == ConfinerOven.BakingState.BAKED && frustumHeight > this.m_Cache.theoriticalMaxFrustumHeight)
			{
				return new ConfinerOven.BakedSolution(this.m_AspectStretcher.Aspect, frustumHeight, false, this.m_PolygonRect, this.m_OriginalPolygon, new List<List<ClipperLib.IntPoint>>
				{
					new List<ClipperLib.IntPoint> { this.m_MidPoint }
				});
			}
			ClipperLib.ClipperOffset clipperOffset = new ClipperLib.ClipperOffset(2.0, 0.25);
			clipperOffset.AddPaths(this.m_OriginalPolygon, ClipperLib.JoinType.jtMiter, ClipperLib.EndType.etClosedPolygon);
			List<List<ClipperLib.IntPoint>> solution = new List<List<ClipperLib.IntPoint>>();
			clipperOffset.Execute(ref solution, (double)(-1f * frustumHeight * 100000f));
			List<List<ClipperLib.IntPoint>> bakedSolution = new List<List<ClipperLib.IntPoint>>();
			if (this.State == ConfinerOven.BakingState.BAKING || this.m_Skeleton.Count == 0)
			{
				bakedSolution = solution;
			}
			else
			{
				ClipperLib.Clipper clipper = new ClipperLib.Clipper(0);
				clipper.AddPaths(solution, ClipperLib.PolyType.ptSubject, true);
				clipper.AddPaths(this.m_Skeleton, ClipperLib.PolyType.ptClip, true);
				clipper.Execute(ClipperLib.ClipType.ctUnion, bakedSolution, ClipperLib.PolyFillType.pftEvenOdd, ClipperLib.PolyFillType.pftEvenOdd);
			}
			return new ConfinerOven.BakedSolution(this.m_AspectStretcher.Aspect, frustumHeight, this.m_MinFrustumHeightWithBones < frustumHeight, this.m_PolygonRect, this.m_OriginalPolygon, bakedSolution);
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00014552 File Offset: 0x00012752
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0001455A File Offset: 0x0001275A
		public ConfinerOven.BakingState State { get; private set; }

		// Token: 0x06000376 RID: 886 RVA: 0x00014564 File Offset: 0x00012764
		private void Initialize(in List<List<Vector2>> inputPath, in float aspectRatio, float maxFrustumHeight, float skeletonPadding)
		{
			this.m_Skeleton.Clear();
			this.m_Cache.userSetMaxFrustumHeight = maxFrustumHeight;
			this.m_MinFrustumHeightWithBones = float.MaxValue;
			this.m_SkeletonPadding = skeletonPadding;
			this.m_PolygonRect = ConfinerOven.<Initialize>g__GetPolygonBoundingBox|24_0(in inputPath);
			this.m_AspectStretcher = new ConfinerOven.AspectStretcher(aspectRatio, this.m_PolygonRect.center.x);
			this.m_Cache.theoriticalMaxFrustumHeight = Mathf.Max(this.m_PolygonRect.width / aspectRatio, this.m_PolygonRect.height) / 2f;
			this.m_OriginalPolygon = new List<List<ClipperLib.IntPoint>>(inputPath.Count);
			for (int i = 0; i < inputPath.Count; i++)
			{
				List<Vector2> srcPath = inputPath[i];
				int numPoints = srcPath.Count;
				List<ClipperLib.IntPoint> path = new List<ClipperLib.IntPoint>(numPoints);
				for (int j = 0; j < numPoints; j++)
				{
					Vector2 p = this.m_AspectStretcher.Stretch(srcPath[j]);
					path.Add(new ClipperLib.IntPoint((double)(p.x * 100000f), (double)(p.y * 100000f)));
				}
				this.m_OriginalPolygon.Add(path);
			}
			this.m_MidPoint = ConfinerOven.<Initialize>g__MidPointOfIntRect|24_1(ClipperLib.ClipperBase.GetBounds(this.m_OriginalPolygon));
			if (this.m_Cache.userSetMaxFrustumHeight < 0f)
			{
				this.State = ConfinerOven.BakingState.BAKED;
				return;
			}
			this.m_Cache.maxFrustumHeight = this.m_Cache.userSetMaxFrustumHeight;
			if (this.m_Cache.maxFrustumHeight == 0f || this.m_Cache.maxFrustumHeight > this.m_Cache.theoriticalMaxFrustumHeight)
			{
				this.m_Cache.maxFrustumHeight = this.m_Cache.theoriticalMaxFrustumHeight;
			}
			this.m_Cache.stepSize = this.m_Cache.maxFrustumHeight;
			this.m_Cache.offsetter = new ClipperLib.ClipperOffset(2.0, 0.25);
			this.m_Cache.offsetter.AddPaths(this.m_OriginalPolygon, ClipperLib.JoinType.jtMiter, ClipperLib.EndType.etClosedPolygon);
			List<List<ClipperLib.IntPoint>> solution = new List<List<ClipperLib.IntPoint>>();
			this.m_Cache.offsetter.Execute(ref solution, 0.0);
			this.m_Cache.solutions = new List<ConfinerOven.PolygonSolution>();
			this.m_Cache.solutions.Add(new ConfinerOven.PolygonSolution
			{
				polygons = solution,
				frustumHeight = 0f
			});
			this.m_Cache.rightCandidate = default(ConfinerOven.PolygonSolution);
			this.m_Cache.leftCandidate = new ConfinerOven.PolygonSolution
			{
				polygons = solution,
				frustumHeight = 0f
			};
			this.m_Cache.currentFrustumHeight = 0f;
			this.m_Cache.maxCandidate = new List<List<ClipperLib.IntPoint>>();
			this.m_Cache.offsetter.Execute(ref this.m_Cache.maxCandidate, (double)(-1f * this.m_Cache.theoriticalMaxFrustumHeight * 100000f));
			this.m_Cache.bakeTime = 0f;
			this.State = ConfinerOven.BakingState.BAKING;
			this.bakeProgress = 0f;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00014870 File Offset: 0x00012A70
		public void BakeConfiner(float maxComputationTimePerFrameInSeconds)
		{
			if (this.State != ConfinerOven.BakingState.BAKING)
			{
				return;
			}
			float startTime = Time.realtimeSinceStartup;
			while (this.m_Cache.solutions.Count < 1000)
			{
				List<List<ClipperLib.IntPoint>> candidate = new List<List<ClipperLib.IntPoint>>(this.m_Cache.leftCandidate.polygons.Count);
				this.m_Cache.stepSize = Mathf.Min(this.m_Cache.stepSize, this.m_Cache.maxFrustumHeight - this.m_Cache.leftCandidate.frustumHeight);
				this.m_Cache.currentFrustumHeight = this.m_Cache.leftCandidate.frustumHeight + this.m_Cache.stepSize;
				if (Math.Abs(this.m_Cache.currentFrustumHeight - this.m_Cache.maxFrustumHeight) < 0.0001f)
				{
					candidate = this.m_Cache.maxCandidate;
				}
				else
				{
					this.m_Cache.offsetter.Execute(ref candidate, (double)(-1f * this.m_Cache.currentFrustumHeight * 100000f));
				}
				if (this.m_Cache.leftCandidate.StateChanged(in candidate))
				{
					this.m_Cache.rightCandidate = new ConfinerOven.PolygonSolution
					{
						polygons = candidate,
						frustumHeight = this.m_Cache.currentFrustumHeight
					};
					this.m_Cache.stepSize = Mathf.Max(this.m_Cache.stepSize / 2f, 0.0005f);
				}
				else
				{
					this.m_Cache.leftCandidate = new ConfinerOven.PolygonSolution
					{
						polygons = candidate,
						frustumHeight = this.m_Cache.currentFrustumHeight
					};
					if (!this.m_Cache.rightCandidate.IsNull)
					{
						this.m_Cache.stepSize = Mathf.Max(this.m_Cache.stepSize / 2f, 0.0005f);
					}
				}
				if (!this.m_Cache.rightCandidate.IsNull && this.m_Cache.stepSize <= 0.0005f)
				{
					this.m_Cache.solutions.Add(this.m_Cache.leftCandidate);
					this.m_Cache.solutions.Add(this.m_Cache.rightCandidate);
					this.m_Cache.leftCandidate = this.m_Cache.rightCandidate;
					this.m_Cache.rightCandidate = default(ConfinerOven.PolygonSolution);
					this.m_Cache.stepSize = this.m_Cache.maxFrustumHeight;
				}
				else if (this.m_Cache.rightCandidate.IsNull || this.m_Cache.leftCandidate.frustumHeight >= this.m_Cache.maxFrustumHeight)
				{
					this.m_Cache.solutions.Add(this.m_Cache.leftCandidate);
					break;
				}
				float elapsedTime = Time.realtimeSinceStartup - startTime;
				if (elapsedTime > maxComputationTimePerFrameInSeconds)
				{
					this.m_Cache.bakeTime = this.m_Cache.bakeTime + elapsedTime;
					if (this.m_Cache.bakeTime > this.m_MaxComputationTimeForFullSkeletonBakeInSeconds)
					{
						this.State = ConfinerOven.BakingState.TIMEOUT;
					}
					this.bakeProgress = this.m_Cache.leftCandidate.frustumHeight / this.m_Cache.maxFrustumHeight;
					return;
				}
			}
			this.<BakeConfiner>g__ComputeSkeleton|25_0(in this.m_Cache.solutions);
			for (int i = this.m_Cache.solutions.Count - 1; i >= 0; i--)
			{
				if (this.m_Cache.solutions[i].polygons.Count == 0)
				{
					this.m_Cache.solutions.RemoveAt(i);
				}
			}
			this.bakeProgress = 1f;
			this.State = ConfinerOven.BakingState.BAKED;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00014C04 File Offset: 0x00012E04
		[CompilerGenerated]
		internal static Rect <Initialize>g__GetPolygonBoundingBox|24_0(in List<List<Vector2>> polygons)
		{
			float minX = float.PositiveInfinity;
			float maxX = float.NegativeInfinity;
			float minY = float.PositiveInfinity;
			float maxY = float.NegativeInfinity;
			for (int i = 0; i < polygons.Count; i++)
			{
				for (int j = 0; j < polygons[i].Count; j++)
				{
					Vector2 p = polygons[i][j];
					minX = Mathf.Min(minX, p.x);
					maxX = Mathf.Max(maxX, p.x);
					minY = Mathf.Min(minY, p.y);
					maxY = Mathf.Max(maxY, p.y);
				}
			}
			return new Rect(minX, minY, Mathf.Max(0f, maxX - minX), Mathf.Max(0f, maxY - minY));
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00014CC7 File Offset: 0x00012EC7
		[CompilerGenerated]
		internal static ClipperLib.IntPoint <Initialize>g__MidPointOfIntRect|24_1(ClipperLib.IntRect bounds)
		{
			return new ClipperLib.IntPoint((bounds.left + bounds.right) / 2L, (bounds.top + bounds.bottom) / 2L);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00014CF0 File Offset: 0x00012EF0
		[CompilerGenerated]
		private void <BakeConfiner>g__ComputeSkeleton|25_0(in List<ConfinerOven.PolygonSolution> solutions)
		{
			ClipperLib.Clipper clipper = new ClipperLib.Clipper(0);
			ClipperLib.ClipperOffset offsetter = new ClipperLib.ClipperOffset(2.0, 0.25);
			for (int i = 1; i < solutions.Count - 1; i += 2)
			{
				ConfinerOven.PolygonSolution prev = solutions[i];
				ConfinerOven.PolygonSolution next = solutions[i + 1];
				double step = (double)(this.m_SkeletonPadding * 100000f * (next.frustumHeight - prev.frustumHeight));
				List<List<ClipperLib.IntPoint>> expandedPrev = new List<List<ClipperLib.IntPoint>>();
				offsetter.Clear();
				offsetter.AddPaths(prev.polygons, ClipperLib.JoinType.jtMiter, ClipperLib.EndType.etClosedPolygon);
				offsetter.Execute(ref expandedPrev, step);
				List<List<ClipperLib.IntPoint>> expandedNext = new List<List<ClipperLib.IntPoint>>();
				offsetter.Clear();
				offsetter.AddPaths(next.polygons, ClipperLib.JoinType.jtMiter, ClipperLib.EndType.etClosedPolygon);
				offsetter.Execute(ref expandedNext, step * 2.0);
				List<List<ClipperLib.IntPoint>> solution = new List<List<ClipperLib.IntPoint>>();
				clipper.Clear();
				clipper.AddPaths(expandedPrev, ClipperLib.PolyType.ptSubject, true);
				clipper.AddPaths(expandedNext, ClipperLib.PolyType.ptClip, true);
				clipper.Execute(ClipperLib.ClipType.ctDifference, solution, ClipperLib.PolyFillType.pftEvenOdd, ClipperLib.PolyFillType.pftEvenOdd);
				if (solution.Count > 0 && solution[0].Count > 0)
				{
					this.m_Skeleton.AddRange(solution);
					if (this.m_MinFrustumHeightWithBones == 3.4028235E+38f)
					{
						this.m_MinFrustumHeightWithBones = next.frustumHeight;
					}
				}
			}
		}

		// Token: 0x04000308 RID: 776
		private float m_MinFrustumHeightWithBones;

		// Token: 0x04000309 RID: 777
		private float m_SkeletonPadding;

		// Token: 0x0400030A RID: 778
		private List<List<ClipperLib.IntPoint>> m_OriginalPolygon;

		// Token: 0x0400030B RID: 779
		private ClipperLib.IntPoint m_MidPoint;

		// Token: 0x0400030C RID: 780
		private List<List<ClipperLib.IntPoint>> m_Skeleton = new List<List<ClipperLib.IntPoint>>();

		// Token: 0x0400030D RID: 781
		private const long k_FloatToIntScaler = 100000L;

		// Token: 0x0400030E RID: 782
		private const float k_IntToFloatScaler = 1E-05f;

		// Token: 0x0400030F RID: 783
		private const float k_MinStepSize = 0.0005f;

		// Token: 0x04000310 RID: 784
		private Rect m_PolygonRect;

		// Token: 0x04000311 RID: 785
		private ConfinerOven.AspectStretcher m_AspectStretcher = new ConfinerOven.AspectStretcher(1f, 0f);

		// Token: 0x04000312 RID: 786
		private float m_MaxComputationTimeForFullSkeletonBakeInSeconds = 5f;

		// Token: 0x04000314 RID: 788
		public float bakeProgress;

		// Token: 0x04000315 RID: 789
		private ConfinerOven.BakingStateCache m_Cache;

		// Token: 0x02000090 RID: 144
		public class BakedSolution
		{
			// Token: 0x0600037B RID: 891 RVA: 0x00014E30 File Offset: 0x00013030
			public BakedSolution(float aspectRatio, float frustumHeight, bool hasBones, Rect polygonBounds, List<List<ClipperLib.IntPoint>> originalPolygon, List<List<ClipperLib.IntPoint>> solution)
			{
				this.m_AspectStretcher = new ConfinerOven.AspectStretcher(aspectRatio, polygonBounds.center.x);
				this.m_FrustumSizeIntSpace = frustumHeight * 100000f;
				this.m_HasBones = hasBones;
				this.m_OriginalPolygon = originalPolygon;
				this.m_Solution = solution;
				float polygonSizeX = polygonBounds.width / aspectRatio * 100000f;
				float polygonSizeY = polygonBounds.height * 100000f;
				this.m_SqrPolygonDiagonal = (double)(polygonSizeX * polygonSizeX + polygonSizeY * polygonSizeY);
			}

			// Token: 0x0600037C RID: 892 RVA: 0x00014EAB File Offset: 0x000130AB
			public bool IsValid()
			{
				return this.m_Solution != null;
			}

			// Token: 0x0600037D RID: 893 RVA: 0x00014EB8 File Offset: 0x000130B8
			public Vector2 ConfinePoint(in Vector2 pointToConfine)
			{
				if (this.m_Solution.Count <= 0)
				{
					return pointToConfine;
				}
				Vector2 pInConfinerSpace = this.m_AspectStretcher.Stretch(pointToConfine);
				ClipperLib.IntPoint p = new ClipperLib.IntPoint((double)(pInConfinerSpace.x * 100000f), (double)(pInConfinerSpace.y * 100000f));
				for (int i = 0; i < this.m_Solution.Count; i++)
				{
					if (ClipperLib.Clipper.PointInPolygon(p, this.m_Solution[i]) != 0)
					{
						return pointToConfine;
					}
				}
				bool checkIntersectOriginal = this.m_HasBones && this.<ConfinePoint>g__IsInsideOriginal|9_1(p);
				ClipperLib.IntPoint closest = p;
				double minDistance = double.MaxValue;
				for (int j = 0; j < this.m_Solution.Count; j++)
				{
					int numPoints = this.m_Solution[j].Count;
					for (int k = 0; k < numPoints; k++)
					{
						ClipperLib.IntPoint l = this.m_Solution[j][k];
						ClipperLib.IntPoint l2 = this.m_Solution[j][(k + 1) % numPoints];
						ClipperLib.IntPoint c = ConfinerOven.BakedSolution.<ConfinePoint>g__IntPointLerp|9_0(l, l2, ConfinerOven.BakedSolution.<ConfinePoint>g__ClosestPointOnSegment|9_2(p, l, l2));
						double num = (double)Mathf.Abs((float)(p.X - c.X));
						double diffY = (double)Mathf.Abs((float)(p.Y - c.Y));
						double distance = num * num + diffY * diffY;
						if (num > (double)this.m_FrustumSizeIntSpace || diffY > (double)this.m_FrustumSizeIntSpace)
						{
							distance += this.m_SqrPolygonDiagonal;
						}
						if (distance < minDistance && (!checkIntersectOriginal || !this.<ConfinePoint>g__DoesIntersectOriginal|9_3(p, c)))
						{
							minDistance = distance;
							closest = c;
						}
					}
				}
				Vector2 result = new Vector2((float)closest.X * 1E-05f, (float)closest.Y * 1E-05f);
				return this.m_AspectStretcher.Unstretch(result);
			}

			// Token: 0x0600037E RID: 894 RVA: 0x00015094 File Offset: 0x00013294
			private static int FindIntersection(in ClipperLib.IntPoint p1, in ClipperLib.IntPoint p2, in ClipperLib.IntPoint p3, in ClipperLib.IntPoint p4)
			{
				double dx12 = (double)(p2.X - p1.X);
				double dy12 = (double)(p2.Y - p1.Y);
				double dx13 = (double)(p4.X - p3.X);
				double dy13 = (double)(p4.Y - p3.Y);
				double denominator = dy12 * dx13 - dx12 * dy13;
				double t = ((double)(p1.X - p3.X) * dy13 + (double)(p3.Y - p1.Y) * dx13) / denominator;
				if (double.IsInfinity(t) || double.IsNaN(t))
				{
					if (ConfinerOven.BakedSolution.<FindIntersection>g__IntPointDiffSqrMagnitude|10_0(p1, p3) < 1000.0 || ConfinerOven.BakedSolution.<FindIntersection>g__IntPointDiffSqrMagnitude|10_0(p1, p4) < 1000.0 || ConfinerOven.BakedSolution.<FindIntersection>g__IntPointDiffSqrMagnitude|10_0(p2, p3) < 1000.0 || ConfinerOven.BakedSolution.<FindIntersection>g__IntPointDiffSqrMagnitude|10_0(p2, p4) < 1000.0)
					{
						return 2;
					}
					return 0;
				}
				else
				{
					double t2 = ((double)(p3.X - p1.X) * dy12 + (double)(p1.Y - p3.Y) * dx12) / -denominator;
					if (t < 0.0 || t > 1.0 || t2 < 0.0 || t2 >= 1.0)
					{
						return 1;
					}
					return 2;
				}
			}

			// Token: 0x0600037F RID: 895 RVA: 0x000151F0 File Offset: 0x000133F0
			[CompilerGenerated]
			internal static ClipperLib.IntPoint <ConfinePoint>g__IntPointLerp|9_0(ClipperLib.IntPoint a, ClipperLib.IntPoint b, float lerp)
			{
				return new ClipperLib.IntPoint
				{
					X = (long)Mathf.RoundToInt((float)a.X + (float)(b.X - a.X) * lerp),
					Y = (long)Mathf.RoundToInt((float)a.Y + (float)(b.Y - a.Y) * lerp)
				};
			}

			// Token: 0x06000380 RID: 896 RVA: 0x00015250 File Offset: 0x00013450
			[CompilerGenerated]
			private bool <ConfinePoint>g__IsInsideOriginal|9_1(ClipperLib.IntPoint point)
			{
				return this.m_OriginalPolygon.Any((List<ClipperLib.IntPoint> t) => ClipperLib.Clipper.PointInPolygon(point, t) != 0);
			}

			// Token: 0x06000381 RID: 897 RVA: 0x00015284 File Offset: 0x00013484
			[CompilerGenerated]
			internal static float <ConfinePoint>g__ClosestPointOnSegment|9_2(ClipperLib.IntPoint point, ClipperLib.IntPoint s0, ClipperLib.IntPoint s1)
			{
				double sX = (double)(s1.X - s0.X);
				double sY = (double)(s1.Y - s0.Y);
				double len2 = sX * sX + sY * sY;
				if (len2 < 1000.0)
				{
					return 0f;
				}
				float num = (float)((double)(point.X - s0.X));
				double s0pY = (double)(point.Y - s0.Y);
				return Mathf.Clamp01((float)(((double)num * sX + s0pY * sY) / len2));
			}

			// Token: 0x06000382 RID: 898 RVA: 0x000152F4 File Offset: 0x000134F4
			[CompilerGenerated]
			private bool <ConfinePoint>g__DoesIntersectOriginal|9_3(ClipperLib.IntPoint l1, ClipperLib.IntPoint l2)
			{
				foreach (List<ClipperLib.IntPoint> original in this.m_OriginalPolygon)
				{
					int numPoints = original.Count;
					for (int i = 0; i < numPoints; i++)
					{
						ClipperLib.IntPoint intPoint = original[i];
						ClipperLib.IntPoint intPoint2 = original[(i + 1) % numPoints];
						if (ConfinerOven.BakedSolution.FindIntersection(in l1, in l2, in intPoint, in intPoint2) == 2)
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x06000383 RID: 899 RVA: 0x00015384 File Offset: 0x00013584
			[CompilerGenerated]
			internal static double <FindIntersection>g__IntPointDiffSqrMagnitude|10_0(ClipperLib.IntPoint point1, ClipperLib.IntPoint point2)
			{
				double num = (double)(point1.X - point2.X);
				double y = (double)(point1.Y - point2.Y);
				return num * num + y * y;
			}

			// Token: 0x04000316 RID: 790
			private float m_FrustumSizeIntSpace;

			// Token: 0x04000317 RID: 791
			private readonly ConfinerOven.AspectStretcher m_AspectStretcher;

			// Token: 0x04000318 RID: 792
			private readonly bool m_HasBones;

			// Token: 0x04000319 RID: 793
			private readonly double m_SqrPolygonDiagonal;

			// Token: 0x0400031A RID: 794
			private List<List<ClipperLib.IntPoint>> m_OriginalPolygon;

			// Token: 0x0400031B RID: 795
			private List<List<ClipperLib.IntPoint>> m_Solution;

			// Token: 0x0400031C RID: 796
			private const double k_ClipperEpsilon = 1000.0;
		}

		// Token: 0x02000092 RID: 146
		private readonly struct AspectStretcher
		{
			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000386 RID: 902 RVA: 0x000153C5 File Offset: 0x000135C5
			public float Aspect { get; }

			// Token: 0x06000387 RID: 903 RVA: 0x000153CD File Offset: 0x000135CD
			public AspectStretcher(float aspect, float centerX)
			{
				this.Aspect = aspect;
				this.m_InverseAspect = 1f / this.Aspect;
				this.m_CenterX = centerX;
			}

			// Token: 0x06000388 RID: 904 RVA: 0x000153EF File Offset: 0x000135EF
			public Vector2 Stretch(Vector2 p)
			{
				return new Vector2((p.x - this.m_CenterX) * this.m_InverseAspect + this.m_CenterX, p.y);
			}

			// Token: 0x06000389 RID: 905 RVA: 0x00015417 File Offset: 0x00013617
			public Vector2 Unstretch(Vector2 p)
			{
				return new Vector2((p.x - this.m_CenterX) * this.Aspect + this.m_CenterX, p.y);
			}

			// Token: 0x0400031F RID: 799
			private readonly float m_InverseAspect;

			// Token: 0x04000320 RID: 800
			private readonly float m_CenterX;
		}

		// Token: 0x02000093 RID: 147
		private struct PolygonSolution
		{
			// Token: 0x0600038A RID: 906 RVA: 0x00015440 File Offset: 0x00013640
			public bool StateChanged(in List<List<ClipperLib.IntPoint>> paths)
			{
				if (paths.Count != this.polygons.Count)
				{
					return true;
				}
				for (int i = 0; i < paths.Count; i++)
				{
					if (paths[i].Count != this.polygons[i].Count)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x0600038B RID: 907 RVA: 0x00015498 File Offset: 0x00013698
			public bool IsNull
			{
				get
				{
					return this.polygons == null;
				}
			}

			// Token: 0x04000321 RID: 801
			public List<List<ClipperLib.IntPoint>> polygons;

			// Token: 0x04000322 RID: 802
			public float frustumHeight;
		}

		// Token: 0x02000094 RID: 148
		public enum BakingState
		{
			// Token: 0x04000324 RID: 804
			BAKING,
			// Token: 0x04000325 RID: 805
			BAKED,
			// Token: 0x04000326 RID: 806
			TIMEOUT
		}

		// Token: 0x02000095 RID: 149
		private struct BakingStateCache
		{
			// Token: 0x04000327 RID: 807
			public ClipperLib.ClipperOffset offsetter;

			// Token: 0x04000328 RID: 808
			public List<ConfinerOven.PolygonSolution> solutions;

			// Token: 0x04000329 RID: 809
			public ConfinerOven.PolygonSolution rightCandidate;

			// Token: 0x0400032A RID: 810
			public ConfinerOven.PolygonSolution leftCandidate;

			// Token: 0x0400032B RID: 811
			public List<List<ClipperLib.IntPoint>> maxCandidate;

			// Token: 0x0400032C RID: 812
			public float stepSize;

			// Token: 0x0400032D RID: 813
			public float maxFrustumHeight;

			// Token: 0x0400032E RID: 814
			public float userSetMaxFrustumHeight;

			// Token: 0x0400032F RID: 815
			public float theoriticalMaxFrustumHeight;

			// Token: 0x04000330 RID: 816
			public float currentFrustumHeight;

			// Token: 0x04000331 RID: 817
			public float bakeTime;
		}
	}
}
