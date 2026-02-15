using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x020000B7 RID: 183
	internal struct ModuleHandle
	{
		// Token: 0x060003E9 RID: 1001 RVA: 0x0001CEFF File Offset: 0x0001B0FF
		internal static void Copy<T>(NativeArray<T> src, int srcIndex, NativeArray<T> dst, int dstIndex, int length) where T : struct
		{
			NativeArray<T>.Copy(src, srcIndex, dst, dstIndex, length);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001CF0C File Offset: 0x0001B10C
		internal static void Copy<T>(NativeArray<T> src, NativeArray<T> dst, int length) where T : struct
		{
			ModuleHandle.Copy<T>(src, 0, dst, 0, length);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001CF18 File Offset: 0x0001B118
		internal unsafe static void InsertionSort<T, U>(void* array, int lo, int hi, U comp) where T : struct where U : IComparer<T>
		{
			for (int i = lo; i < hi; i++)
			{
				int j = i;
				T t = UnsafeUtility.ReadArrayElement<T>(array, i + 1);
				while (j >= lo && comp.Compare(t, UnsafeUtility.ReadArrayElement<T>(array, j)) < 0)
				{
					UnsafeUtility.WriteArrayElement<T>(array, j + 1, UnsafeUtility.ReadArrayElement<T>(array, j));
					j--;
				}
				UnsafeUtility.WriteArrayElement<T>(array, j + 1, t);
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001CF7C File Offset: 0x0001B17C
		internal static int GetLower<T, U, X>(NativeArray<T> values, int count, U check, X condition) where T : struct where U : struct where X : ICondition2<T, U>
		{
			int i = 0;
			int h = count - 1;
			int j = i - 1;
			while (i <= h)
			{
				int k = i + h >> 1;
				float t = 0f;
				if (condition.Test(values[k], check, ref t))
				{
					j = k;
					i = k + 1;
				}
				else
				{
					h = k - 1;
				}
			}
			return j;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001CFD0 File Offset: 0x0001B1D0
		internal static int GetUpper<T, U, X>(NativeArray<T> values, int count, U check, X condition) where T : struct where U : struct where X : ICondition2<T, U>
		{
			int i = 0;
			int h = count - 1;
			int j = h + 1;
			while (i <= h)
			{
				int k = i + h >> 1;
				float t = 0f;
				if (condition.Test(values[k], check, ref t))
				{
					j = k;
					h = k - 1;
				}
				else
				{
					i = k + 1;
				}
			}
			return j;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001D024 File Offset: 0x0001B224
		internal static int GetEqual<T, U, X>(NativeArray<T> values, int count, U check, X condition) where T : struct where U : struct where X : ICondition2<T, U>
		{
			int i = 0;
			int h = count - 1;
			while (i <= h)
			{
				int j = i + h >> 1;
				float t = 0f;
				condition.Test(values[j], check, ref t);
				if (t == 0f)
				{
					return j;
				}
				if (t <= 0f)
				{
					i = j + 1;
				}
				else
				{
					h = j - 1;
				}
			}
			return -1;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001D080 File Offset: 0x0001B280
		internal static float OrientFast(float2 a, float2 b, float2 c)
		{
			float epsilon = 1.110223E-16f;
			float det = (b.y - a.y) * (c.x - b.x) - (b.x - a.x) * (c.y - b.y);
			if (math.abs(det) < epsilon)
			{
				return 0f;
			}
			return det;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001D0DC File Offset: 0x0001B2DC
		internal static double OrientFastDouble(double2 a, double2 b, double2 c)
		{
			double epsilon = 1.1102230246251565E-16;
			double det = (b.y - a.y) * (c.x - b.x) - (b.x - a.x) * (c.y - b.y);
			if (math.abs(det) < epsilon)
			{
				return 0.0;
			}
			return det;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001D140 File Offset: 0x0001B340
		internal static UCircle CircumCircle(UTriangle tri)
		{
			float xa = tri.va.x * tri.va.x;
			float xb = tri.vb.x * tri.vb.x;
			float xc = tri.vc.x * tri.vc.x;
			float ya = tri.va.y * tri.va.y;
			float yb = tri.vb.y * tri.vb.y;
			float yc = tri.vc.y * tri.vc.y;
			float c = 2f * ((tri.vb.x - tri.va.x) * (tri.vc.y - tri.va.y) - (tri.vb.y - tri.va.y) * (tri.vc.x - tri.va.x));
			float x = ((tri.vc.y - tri.va.y) * (xb - xa + yb - ya) + (tri.va.y - tri.vb.y) * (xc - xa + yc - ya)) / c;
			float y = ((tri.va.x - tri.vc.x) * (xb - xa + yb - ya) + (tri.vb.x - tri.va.x) * (xc - xa + yc - ya)) / c;
			float vx = tri.va.x - x;
			float vy = tri.va.y - y;
			return new UCircle
			{
				center = new float2(x, y),
				radius = math.sqrt(vx * vx + vy * vy)
			};
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001D323 File Offset: 0x0001B523
		internal static bool IsInsideCircle(UCircle c, float2 v)
		{
			return math.distance(v, c.center) < c.radius;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001D33C File Offset: 0x0001B53C
		internal static float TriangleArea(float2 va, float2 vb, float2 vc)
		{
			float3 a = new float3(va.x, va.y, 0f);
			float3 b = new float3(vb.x, vb.y, 0f);
			float3 c = new float3(vc.x, vc.y, 0f);
			return math.abs(math.cross(a - b, a - c).z) * 0.5f;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001D3B4 File Offset: 0x0001B5B4
		internal static float Sign(float2 p1, float2 p2, float2 p3)
		{
			return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001D3F0 File Offset: 0x0001B5F0
		internal static bool IsInsideTriangle(float2 pt, float2 v1, float2 v2, float2 v3)
		{
			float num = ModuleHandle.Sign(pt, v1, v2);
			float d2 = ModuleHandle.Sign(pt, v2, v3);
			float d3 = ModuleHandle.Sign(pt, v3, v1);
			bool has_neg = num < 0f || d2 < 0f || d3 < 0f;
			bool has_pos = num > 0f || d2 > 0f || d3 > 0f;
			return !has_neg || !has_pos;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001D454 File Offset: 0x0001B654
		internal static bool IsInsideTriangleApproximate(float2 pt, float2 v1, float2 v2, float2 v3)
		{
			float num = ModuleHandle.TriangleArea(v1, v2, v3);
			float d = ModuleHandle.TriangleArea(pt, v1, v2);
			float d2 = ModuleHandle.TriangleArea(pt, v2, v3);
			float d3 = ModuleHandle.TriangleArea(pt, v3, v1);
			float epsilon = 1.110223E-16f;
			return Mathf.Abs(num - (d + d2 + d3)) < epsilon;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001D498 File Offset: 0x0001B698
		internal static bool IsInsideCircle(float2 a, float2 b, float2 c, float2 p)
		{
			float num = math.dot(a, a);
			float cd = math.dot(b, b);
			float ef = math.dot(c, c);
			float ax = a.x;
			float ay = a.y;
			float bx = b.x;
			float by = b.y;
			float cx = c.x;
			float cy = c.y;
			float circum_x = (num * (cy - by) + cd * (ay - cy) + ef * (by - ay)) / (ax * (cy - by) + bx * (ay - cy) + cx * (by - ay));
			float circum_y = (num * (cx - bx) + cd * (ax - cx) + ef * (bx - ax)) / (ay * (cx - bx) + by * (ax - cx) + cy * (bx - ax));
			float2 circum = new float2
			{
				x = circum_x / 2f,
				y = circum_y / 2f
			};
			float num2 = math.distance(a, circum);
			float dist = math.distance(p, circum);
			return num2 - dist > 1E-05f;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001D58C File Offset: 0x0001B78C
		internal static void BuildTriangles(NativeArray<float2> vertices, int vertexCount, NativeArray<int> indices, int indexCount, ref NativeArray<UTriangle> triangles, ref int triangleCount, ref float maxArea, ref float avgArea, ref float minArea)
		{
			for (int i = 0; i < indexCount; i += 3)
			{
				UTriangle tri = default(UTriangle);
				int i2 = indices[i];
				int i3 = indices[i + 1];
				int i4 = indices[i + 2];
				tri.va = vertices[i2];
				tri.vb = vertices[i3];
				tri.vc = vertices[i4];
				tri.c = ModuleHandle.CircumCircle(tri);
				tri.area = ModuleHandle.TriangleArea(tri.va, tri.vb, tri.vc);
				maxArea = math.max(tri.area, maxArea);
				minArea = math.min(tri.area, minArea);
				avgArea += tri.area;
				int num = triangleCount;
				triangleCount = num + 1;
				triangles[num] = tri;
			}
			avgArea /= (float)triangleCount;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001D67C File Offset: 0x0001B87C
		internal static void BuildTriangles(NativeArray<float2> vertices, int vertexCount, NativeArray<int> indices, int indexCount, ref NativeArray<UTriangle> triangles, ref int triangleCount, ref float maxArea, ref float avgArea, ref float minArea, ref float maxEdge, ref float avgEdge, ref float minEdge)
		{
			for (int i = 0; i < indexCount; i += 3)
			{
				UTriangle tri = default(UTriangle);
				int i2 = indices[i];
				int i3 = indices[i + 1];
				int i4 = indices[i + 2];
				tri.va = vertices[i2];
				tri.vb = vertices[i3];
				tri.vc = vertices[i4];
				tri.c = ModuleHandle.CircumCircle(tri);
				tri.area = ModuleHandle.TriangleArea(tri.va, tri.vb, tri.vc);
				maxArea = math.max(tri.area, maxArea);
				minArea = math.min(tri.area, minArea);
				avgArea += tri.area;
				float e = math.distance(tri.va, tri.vb);
				float e2 = math.distance(tri.vb, tri.vc);
				float e3 = math.distance(tri.vc, tri.va);
				maxEdge = math.max(e, maxEdge);
				maxEdge = math.max(e2, maxEdge);
				maxEdge = math.max(e3, maxEdge);
				minEdge = math.min(e, minEdge);
				minEdge = math.min(e2, minEdge);
				minEdge = math.min(e3, minEdge);
				avgEdge += e;
				avgEdge += e2;
				avgEdge += e3;
				int num = triangleCount;
				triangleCount = num + 1;
				triangles[num] = tri;
			}
			avgArea /= (float)triangleCount;
			avgEdge /= (float)indexCount;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001D818 File Offset: 0x0001BA18
		internal static void BuildTrianglesAndEdges(NativeArray<float2> vertices, int vertexCount, NativeArray<int> indices, int indexCount, ref NativeArray<UTriangle> triangles, ref int triangleCount, ref NativeArray<int4> delaEdges, ref int delaEdgeCount, ref float maxArea, ref float avgArea, ref float minArea)
		{
			for (int i = 0; i < indexCount; i += 3)
			{
				UTriangle tri = default(UTriangle);
				int i2 = indices[i];
				int i3 = indices[i + 1];
				int i4 = indices[i + 2];
				tri.va = vertices[i2];
				tri.vb = vertices[i3];
				tri.vc = vertices[i4];
				tri.c = ModuleHandle.CircumCircle(tri);
				tri.area = ModuleHandle.TriangleArea(tri.va, tri.vb, tri.vc);
				maxArea = math.max(tri.area, maxArea);
				minArea = math.min(tri.area, minArea);
				avgArea += tri.area;
				tri.indices = new int3(i2, i3, i4);
				int num = delaEdgeCount;
				delaEdgeCount = num + 1;
				delaEdges[num] = new int4(math.min(i2, i3), math.max(i2, i3), triangleCount, -1);
				num = delaEdgeCount;
				delaEdgeCount = num + 1;
				delaEdges[num] = new int4(math.min(i3, i4), math.max(i3, i4), triangleCount, -1);
				num = delaEdgeCount;
				delaEdgeCount = num + 1;
				delaEdges[num] = new int4(math.min(i4, i2), math.max(i4, i2), triangleCount, -1);
				num = triangleCount;
				triangleCount = num + 1;
				triangles[num] = tri;
			}
			avgArea /= (float)triangleCount;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001D9A0 File Offset: 0x0001BBA0
		private static void CopyGraph(NativeArray<float2> srcPoints, int srcPointCount, ref NativeArray<float2> dstPoints, ref int dstPointCount, NativeArray<int2> srcEdges, int srcEdgeCount, ref NativeArray<int2> dstEdges, ref int dstEdgeCount)
		{
			dstEdgeCount = srcEdgeCount;
			dstPointCount = srcPointCount;
			ModuleHandle.Copy<int2>(srcEdges, dstEdges, srcEdgeCount);
			ModuleHandle.Copy<float2>(srcPoints, dstPoints, srcPointCount);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001D9C7 File Offset: 0x0001BBC7
		private static void CopyGeometry(NativeArray<int> srcIndices, int srcIndexCount, ref NativeArray<int> dstIndices, ref int dstIndexCount, NativeArray<float2> srcVertices, int srcVertexCount, ref NativeArray<float2> dstVertices, ref int dstVertexCount)
		{
			dstIndexCount = srcIndexCount;
			dstVertexCount = srcVertexCount;
			ModuleHandle.Copy<int>(srcIndices, dstIndices, srcIndexCount);
			ModuleHandle.Copy<float2>(srcVertices, dstVertices, srcVertexCount);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001D9EE File Offset: 0x0001BBEE
		private static void TransferOutput(NativeArray<int2> srcEdges, int srcEdgeCount, ref NativeArray<int2> dstEdges, ref int dstEdgeCount, NativeArray<int> srcIndices, int srcIndexCount, ref NativeArray<int> dstIndices, ref int dstIndexCount, NativeArray<float2> srcVertices, int srcVertexCount, ref NativeArray<float2> dstVertices, ref int dstVertexCount)
		{
			dstEdgeCount = srcEdgeCount;
			dstIndexCount = srcIndexCount;
			dstVertexCount = srcVertexCount;
			ModuleHandle.Copy<int2>(srcEdges, dstEdges, srcEdgeCount);
			ModuleHandle.Copy<int>(srcIndices, dstIndices, srcIndexCount);
			ModuleHandle.Copy<float2>(srcVertices, dstVertices, srcVertexCount);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001DA2C File Offset: 0x0001BC2C
		private static void GraphConditioner(NativeArray<float2> points, ref NativeArray<float2> pgPoints, ref int pgPointCount, ref NativeArray<int2> pgEdges, ref int pgEdgeCount, bool resetTopology)
		{
			float2 min = new float2(float.PositiveInfinity, float.PositiveInfinity);
			float2 max = float2.zero;
			for (int i = 0; i < points.Length; i++)
			{
				min = math.min(points[i], min);
				max = math.max(points[i], max);
			}
			float2 mid = (max - min) * 0.5f;
			float kNonRect = 0.0001f;
			pgPointCount = (resetTopology ? 0 : pgPointCount);
			int pc = pgPointCount;
			int num = pgPointCount;
			pgPointCount = num + 1;
			pgPoints[num] = new float2(min.x, min.y);
			num = pgPointCount;
			pgPointCount = num + 1;
			pgPoints[num] = new float2(min.x - kNonRect, min.y + mid.y);
			num = pgPointCount;
			pgPointCount = num + 1;
			pgPoints[num] = new float2(min.x, max.y);
			num = pgPointCount;
			pgPointCount = num + 1;
			pgPoints[num] = new float2(min.x + mid.x, max.y + kNonRect);
			num = pgPointCount;
			pgPointCount = num + 1;
			pgPoints[num] = new float2(max.x, max.y);
			num = pgPointCount;
			pgPointCount = num + 1;
			pgPoints[num] = new float2(max.x + kNonRect, min.y + mid.y);
			num = pgPointCount;
			pgPointCount = num + 1;
			pgPoints[num] = new float2(max.x, min.y);
			num = pgPointCount;
			pgPointCount = num + 1;
			pgPoints[num] = new float2(min.x + mid.x, min.y - kNonRect);
			pgEdgeCount = 8;
			pgEdges[0] = new int2(pc, pc + 1);
			pgEdges[1] = new int2(pc + 1, pc + 2);
			pgEdges[2] = new int2(pc + 2, pc + 3);
			pgEdges[3] = new int2(pc + 3, pc + 4);
			pgEdges[4] = new int2(pc + 4, pc + 5);
			pgEdges[5] = new int2(pc + 5, pc + 6);
			pgEdges[6] = new int2(pc + 6, pc + 7);
			pgEdges[7] = new int2(pc + 7, pc);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001DC8C File Offset: 0x0001BE8C
		private static void Reorder(int startVertexCount, int index, ref NativeArray<int> indices, ref int indexCount, ref NativeArray<float2> vertices, ref int vertexCount)
		{
			bool found = false;
			for (int i = 0; i < indexCount; i++)
			{
				if (indices[i] == index)
				{
					found = true;
					break;
				}
			}
			if (!found)
			{
				vertexCount--;
				vertices[index] = vertices[vertexCount];
				for (int j = 0; j < indexCount; j++)
				{
					if (indices[j] == vertexCount)
					{
						indices[j] = index;
					}
				}
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001DCF4 File Offset: 0x0001BEF4
		internal static void VertexCleanupConditioner(int startVertexCount, ref NativeArray<int> indices, ref int indexCount, ref NativeArray<float2> vertices, ref int vertexCount)
		{
			for (int i = startVertexCount; i < vertexCount; i++)
			{
				ModuleHandle.Reorder(startVertexCount, i, ref indices, ref indexCount, ref vertices, ref vertexCount);
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001DD1C File Offset: 0x0001BF1C
		public static float4 ConvexQuad(Allocator allocator, NativeArray<float2> points, NativeArray<int2> edges, ref NativeArray<float2> outVertices, ref int outVertexCount, ref NativeArray<int> outIndices, ref int outIndexCount, ref NativeArray<int2> outEdges, ref int outEdgeCount)
		{
			float4 ret = float4.zero;
			outEdgeCount = 0;
			outIndexCount = 0;
			outVertexCount = 0;
			if (points.Length < 3 || points.Length >= ModuleHandle.kMaxVertexCount)
			{
				return ret;
			}
			int pgEdgeCount = 0;
			int pgPointCount = 0;
			NativeArray<int2> pgEdges = new NativeArray<int2>(ModuleHandle.kMaxEdgeCount, allocator, NativeArrayOptions.ClearMemory);
			NativeArray<float2> pgPoints = new NativeArray<float2>(ModuleHandle.kMaxVertexCount, allocator, NativeArrayOptions.ClearMemory);
			ModuleHandle.GraphConditioner(points, ref pgPoints, ref pgPointCount, ref pgEdges, ref pgEdgeCount, true);
			Tessellator.Tessellate(allocator, pgPoints, pgPointCount, pgEdges, pgEdgeCount, ref outVertices, ref outVertexCount, ref outIndices, ref outIndexCount);
			pgPoints.Dispose();
			pgEdges.Dispose();
			return ret;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001DDA8 File Offset: 0x0001BFA8
		public static float4 Tessellate(Allocator allocator, NativeArray<float2> points, NativeArray<int2> edges, ref NativeArray<float2> outVertices, ref int outVertexCount, ref NativeArray<int> outIndices, ref int outIndexCount, ref NativeArray<int2> outEdges, ref int outEdgeCount)
		{
			float4 ret = float4.zero;
			outEdgeCount = 0;
			outIndexCount = 0;
			outVertexCount = 0;
			if (points.Length < 3 || points.Length >= ModuleHandle.kMaxVertexCount)
			{
				return ret;
			}
			bool validGraph = false;
			bool handleEdgeCase = false;
			int pgEdgeCount = 0;
			int pgPointCount = 0;
			NativeArray<int2> pgEdges = new NativeArray<int2>(edges.Length * 8, allocator, NativeArrayOptions.ClearMemory);
			NativeArray<float2> pgPoints = new NativeArray<float2>(points.Length * 4, allocator, NativeArrayOptions.ClearMemory);
			if (edges.Length != 0)
			{
				validGraph = PlanarGraph.Validate(allocator, points, points.Length, edges, edges.Length, ref pgPoints, ref pgPointCount, ref pgEdges, ref pgEdgeCount);
			}
			if (!validGraph)
			{
				outEdgeCount = edges.Length;
				outVertexCount = points.Length;
				ModuleHandle.Copy<int2>(edges, outEdges, edges.Length);
				ModuleHandle.Copy<float2>(points, outVertices, points.Length);
			}
			if (pgPointCount > 2 && pgEdgeCount > 2)
			{
				NativeArray<int> tsIndices = new NativeArray<int>(pgPointCount * 8, allocator, NativeArrayOptions.ClearMemory);
				NativeArray<float2> tsVertices = new NativeArray<float2>(pgPointCount * 4, allocator, NativeArrayOptions.ClearMemory);
				int tsIndexCount = 0;
				int tsVertexCount = 0;
				validGraph = Tessellator.Tessellate(allocator, pgPoints, pgPointCount, pgEdges, pgEdgeCount, ref tsVertices, ref tsVertexCount, ref tsIndices, ref tsIndexCount);
				if (validGraph)
				{
					ModuleHandle.TransferOutput(pgEdges, pgEdgeCount, ref outEdges, ref outEdgeCount, tsIndices, tsIndexCount, ref outIndices, ref outIndexCount, tsVertices, tsVertexCount, ref outVertices, ref outVertexCount);
					if (handleEdgeCase)
					{
						outEdgeCount = 0;
					}
				}
				tsVertices.Dispose();
				tsIndices.Dispose();
			}
			pgPoints.Dispose();
			pgEdges.Dispose();
			return ret;
		}

		// Token: 0x0400032C RID: 812
		internal static readonly int kMaxArea = 65536;

		// Token: 0x0400032D RID: 813
		internal static readonly int kMaxEdgeCount = 65536;

		// Token: 0x0400032E RID: 814
		internal static readonly int kMaxIndexCount = 65536;

		// Token: 0x0400032F RID: 815
		internal static readonly int kMaxVertexCount = 65536;

		// Token: 0x04000330 RID: 816
		internal static readonly int kMaxTriangleCount = ModuleHandle.kMaxIndexCount / 3;

		// Token: 0x04000331 RID: 817
		internal static readonly int kMaxRefineIterations = 48;

		// Token: 0x04000332 RID: 818
		internal static readonly int kMaxSmoothenIterations = 256;

		// Token: 0x04000333 RID: 819
		internal static readonly float kIncrementAreaFactor = 1.2f;
	}
}
