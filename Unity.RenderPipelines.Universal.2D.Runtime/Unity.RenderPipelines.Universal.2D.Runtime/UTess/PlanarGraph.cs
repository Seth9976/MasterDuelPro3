using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x0200009C RID: 156
	internal struct PlanarGraph
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x0001A2E4 File Offset: 0x000184E4
		internal static void RemoveDuplicateEdges(ref NativeArray<int2> edges, ref int edgeCount, NativeArray<int> duplicates, int duplicateCount)
		{
			if (duplicateCount == 0)
			{
				for (int i = 0; i < edgeCount; i++)
				{
					int2 e = edges[i];
					e.x = math.min(edges[i].x, edges[i].y);
					e.y = math.max(edges[i].x, edges[i].y);
					edges[i] = e;
				}
			}
			else
			{
				for (int j = 0; j < edgeCount; j++)
				{
					int2 e2 = edges[j];
					int a = duplicates[e2.x];
					int b = duplicates[e2.y];
					e2.x = math.min(a, b);
					e2.y = math.max(a, b);
					edges[j] = e2;
				}
			}
			ModuleHandle.InsertionSort<int2, TessEdgeCompare>(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<int2>(edges), 0, edgeCount - 1, default(TessEdgeCompare));
			int k = 1;
			for (int l = 1; l < edgeCount; l++)
			{
				int2 prev = edges[l - 1];
				int2 next = edges[l];
				if ((next.x != prev.x || next.y != prev.y) && next.x != next.y)
				{
					edges[k++] = next;
				}
			}
			edgeCount = k;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001A444 File Offset: 0x00018644
		internal static bool CheckCollinear(double2 a0, double2 a1, double2 b0, double2 b1)
		{
			double x = (a1.y - a0.y) / (a1.x - a0.x);
			double y = (b0.y - a0.y) / (b0.x - a0.x);
			double z = (b1.y - a0.y) / (b1.x - a0.x);
			return (!math.isinf(x) || !math.isinf(y) || !math.isinf(z)) && math.abs(x - y) > PlanarGraph.kEpsilon && math.abs(x - z) > PlanarGraph.kEpsilon;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001A4F0 File Offset: 0x000186F0
		internal static bool LineLineIntersection(double2 a0, double2 a1, double2 b0, double2 b1)
		{
			double x0 = ModuleHandle.OrientFastDouble(a0, b0, b1);
			double y0 = ModuleHandle.OrientFastDouble(a1, b0, b1);
			if ((x0 > PlanarGraph.kEpsilon && y0 > PlanarGraph.kEpsilon) || (x0 < -PlanarGraph.kEpsilon && y0 < -PlanarGraph.kEpsilon))
			{
				return false;
			}
			double x = ModuleHandle.OrientFastDouble(b0, a0, a1);
			double y = ModuleHandle.OrientFastDouble(b1, a0, a1);
			return (x <= PlanarGraph.kEpsilon || y <= PlanarGraph.kEpsilon) && (x >= -PlanarGraph.kEpsilon || y >= -PlanarGraph.kEpsilon) && (math.abs(x0) >= PlanarGraph.kEpsilon || math.abs(y0) >= PlanarGraph.kEpsilon || math.abs(x) >= PlanarGraph.kEpsilon || math.abs(y) >= PlanarGraph.kEpsilon || PlanarGraph.CheckCollinear(a0, a1, b0, b1));
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001A5A8 File Offset: 0x000187A8
		internal static bool LineLineIntersection(double2 p1, double2 p2, double2 p3, double2 p4, ref double2 result)
		{
			double bx = p2.x - p1.x;
			double by = p2.y - p1.y;
			double dx = p4.x - p3.x;
			double dy = p4.y - p3.y;
			double bDotDPerp = bx * dy - by * dx;
			if (math.abs(bDotDPerp) < PlanarGraph.kEpsilon)
			{
				return false;
			}
			double num = p3.x - p1.x;
			double cy = p3.y - p1.y;
			double t = (num * dy - cy * dx) / bDotDPerp;
			if (t >= -PlanarGraph.kEpsilon && t <= 1.0 + PlanarGraph.kEpsilon)
			{
				result.x = p1.x + t * bx;
				result.y = p1.y + t * by;
				return true;
			}
			return false;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001A674 File Offset: 0x00018874
		internal static bool CalculateEdgeIntersections(NativeArray<int2> edges, int edgeCount, NativeArray<double2> points, int pointCount, ref NativeArray<int2> results, ref NativeArray<double2> intersects, ref int resultCount)
		{
			resultCount = 0;
			for (int i = 0; i < edgeCount; i++)
			{
				for (int j = i + 1; j < edgeCount; j++)
				{
					int2 e = edges[i];
					int2 f = edges[j];
					if (e.x != f.x && e.x != f.y && e.y != f.x && e.y != f.y)
					{
						double2 a = points[e.x];
						double2 b = points[e.y];
						double2 c = points[f.x];
						double2 d = points[f.y];
						double2 g = double2.zero;
						if (PlanarGraph.LineLineIntersection(a, b, c, d) && PlanarGraph.LineLineIntersection(a, b, c, d, ref g))
						{
							if (resultCount >= intersects.Length)
							{
								return false;
							}
							intersects[resultCount] = g;
							int num = resultCount;
							resultCount = num + 1;
							results[num] = new int2(i, j);
						}
					}
				}
			}
			if (resultCount > edgeCount * PlanarGraph.kMaxIntersectionTolerance)
			{
				return false;
			}
			IntersectionCompare tjc = default(IntersectionCompare);
			tjc.edges = edges;
			tjc.points = points;
			ModuleHandle.InsertionSort<int2, IntersectionCompare>(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<int2>(results), 0, resultCount - 1, tjc);
			return true;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001A7DC File Offset: 0x000189DC
		internal static bool CalculateTJunctions(NativeArray<int2> edges, int edgeCount, NativeArray<double2> points, int pointCount, NativeArray<int2> results, ref int resultCount)
		{
			resultCount = 0;
			for (int i = 0; i < edgeCount; i++)
			{
				for (int j = 0; j < pointCount; j++)
				{
					int2 e = edges[i];
					if (e.x != j && e.y != j)
					{
						double2 @double = points[e.x];
						double2 b = points[e.y];
						double2 c = points[j];
						double2 d = points[j];
						if (PlanarGraph.LineLineIntersection(@double, b, c, d))
						{
							if (resultCount >= results.Length)
							{
								return false;
							}
							int num = resultCount;
							resultCount = num + 1;
							results[num] = new int2(i, j);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001A894 File Offset: 0x00018A94
		internal static bool CutEdges(ref NativeArray<double2> points, ref int pointCount, ref NativeArray<int2> edges, ref int edgeCount, ref NativeArray<int2> tJunctions, ref int tJunctionCount, NativeArray<int2> intersections, NativeArray<double2> intersects, int intersectionCount)
		{
			for (int i = 0; i < intersectionCount; i++)
			{
				int2 @int = intersections[i];
				int e = @int.x;
				int f = @int.y;
				int2 j = int2.zero;
				j.x = e;
				j.y = pointCount;
				int num = tJunctionCount;
				tJunctionCount = num + 1;
				tJunctions[num] = j;
				int2 j2 = int2.zero;
				j2.x = f;
				j2.y = pointCount;
				num = tJunctionCount;
				tJunctionCount = num + 1;
				tJunctions[num] = j2;
				if (pointCount >= points.Length)
				{
					return false;
				}
				num = pointCount;
				pointCount = num + 1;
				points[num] = intersects[i];
			}
			ModuleHandle.InsertionSort<int2, TessJunctionCompare>(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<int2>(tJunctions), 0, tJunctionCount - 1, default(TessJunctionCompare));
			for (int k = tJunctionCount - 1; k >= 0; k--)
			{
				int2 tJunction = tJunctions[k];
				int e2 = tJunction.x;
				int2 edge = edges[e2];
				int s = edge.x;
				int t = edge.y;
				double2 a = points[s];
				double2 b = points[t];
				if (a.x - b.x < 0.0 || (a.x == b.x && a.y - b.y < 0.0))
				{
					int num2 = s;
					s = t;
					t = num2;
				}
				edge.x = s;
				int last = (edge.y = tJunction.y);
				edges[e2] = edge;
				int num;
				while (k > 0 && tJunctions[k - 1].x == e2)
				{
					int next = tJunctions[--k].y;
					int2 te = default(int2);
					te.x = last;
					te.y = next;
					num = edgeCount;
					edgeCount = num + 1;
					edges[num] = te;
					last = next;
				}
				int2 le = default(int2);
				le.x = last;
				le.y = t;
				num = edgeCount;
				edgeCount = num + 1;
				edges[num] = le;
			}
			return true;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001AAD0 File Offset: 0x00018CD0
		internal static void RemoveDuplicatePoints(ref NativeArray<double2> points, ref int pointCount, ref NativeArray<int> duplicates, ref int duplicateCount, Allocator allocator)
		{
			TessLink link = TessLink.CreateLink(pointCount, allocator);
			for (int i = 0; i < pointCount; i++)
			{
				for (int j = i + 1; j < pointCount; j++)
				{
					if (math.distance(points[i], points[j]) < PlanarGraph.kEpsilon)
					{
						link.Link(i, j);
					}
				}
			}
			duplicateCount = 0;
			for (int k = 0; k < pointCount; k++)
			{
				int l = link.Find(k);
				if (l != k)
				{
					duplicateCount++;
					points[l] = math.min(points[k], points[l]);
				}
			}
			if (duplicateCount != 0)
			{
				int prevPointCount = pointCount;
				pointCount = 0;
				for (int m = 0; m < prevPointCount; m++)
				{
					if (link.Find(m) == m)
					{
						duplicates[m] = pointCount;
						int num = pointCount;
						pointCount = num + 1;
						points[num] = points[m];
					}
					else
					{
						duplicates[m] = -1;
					}
				}
				for (int n = 0; n < prevPointCount; n++)
				{
					if (duplicates[n] < 0)
					{
						duplicates[n] = duplicates[link.Find(n)];
					}
				}
			}
			TessLink.DestroyLink(link);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001ABFC File Offset: 0x00018DFC
		internal static bool Validate(Allocator allocator, NativeArray<float2> inputPoints, int pointCount, NativeArray<int2> inputEdges, int edgeCount, ref NativeArray<float2> outputPoints, ref int outputPointCount, ref NativeArray<int2> outputEdges, ref int outputEdgeCount)
		{
			float precisionFudge = 10000f;
			int protectLoop = edgeCount;
			bool requiresFix = true;
			bool validGraph = false;
			NativeArray<int> duplicates = new NativeArray<int>(ModuleHandle.kMaxEdgeCount, allocator, NativeArrayOptions.ClearMemory);
			NativeArray<int2> edges = new NativeArray<int2>(ModuleHandle.kMaxEdgeCount, allocator, NativeArrayOptions.ClearMemory);
			NativeArray<int2> tJunctions = new NativeArray<int2>(ModuleHandle.kMaxEdgeCount, allocator, NativeArrayOptions.ClearMemory);
			NativeArray<int2> edgeIntersections = new NativeArray<int2>(ModuleHandle.kMaxEdgeCount, allocator, NativeArrayOptions.ClearMemory);
			NativeArray<double2> points = new NativeArray<double2>(pointCount * 8, allocator, NativeArrayOptions.ClearMemory);
			NativeArray<double2> intersects = new NativeArray<double2>(pointCount * 8, allocator, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < pointCount; i++)
			{
				points[i] = inputPoints[i] * precisionFudge;
			}
			ModuleHandle.Copy<int2>(inputEdges, edges, edgeCount);
			PlanarGraph.RemoveDuplicateEdges(ref edges, ref edgeCount, duplicates, 0);
			while (requiresFix && --protectLoop > 0)
			{
				int intersectionCount = 0;
				validGraph = PlanarGraph.CalculateEdgeIntersections(edges, edgeCount, points, pointCount, ref edgeIntersections, ref intersects, ref intersectionCount);
				if (!validGraph)
				{
					break;
				}
				int tJunctionCount = 0;
				validGraph = PlanarGraph.CalculateTJunctions(edges, edgeCount, points, pointCount, tJunctions, ref tJunctionCount);
				if (!validGraph)
				{
					break;
				}
				validGraph = PlanarGraph.CutEdges(ref points, ref pointCount, ref edges, ref edgeCount, ref tJunctions, ref tJunctionCount, edgeIntersections, intersects, intersectionCount);
				if (!validGraph)
				{
					break;
				}
				int duplicateCount = 0;
				PlanarGraph.RemoveDuplicatePoints(ref points, ref pointCount, ref duplicates, ref duplicateCount, allocator);
				PlanarGraph.RemoveDuplicateEdges(ref edges, ref edgeCount, duplicates, duplicateCount);
				requiresFix = intersectionCount != 0 || tJunctionCount != 0;
			}
			if (validGraph)
			{
				outputEdgeCount = edgeCount;
				outputPointCount = pointCount;
				ModuleHandle.Copy<int2>(edges, outputEdges, edgeCount);
				for (int j = 0; j < pointCount; j++)
				{
					outputPoints[j] = new float2((float)(points[j].x / (double)precisionFudge), (float)(points[j].y / (double)precisionFudge));
				}
			}
			edges.Dispose();
			points.Dispose();
			intersects.Dispose();
			duplicates.Dispose();
			tJunctions.Dispose();
			edgeIntersections.Dispose();
			return validGraph && protectLoop > 0;
		}

		// Token: 0x040002F5 RID: 757
		private static readonly double kEpsilon = 1E-05;

		// Token: 0x040002F6 RID: 758
		private static readonly int kMaxIntersectionTolerance = 4;
	}
}
