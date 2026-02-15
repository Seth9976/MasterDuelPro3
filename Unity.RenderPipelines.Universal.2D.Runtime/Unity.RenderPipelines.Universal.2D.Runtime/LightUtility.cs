using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine.Rendering.Universal.UTess;
using UnityEngine.U2D;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000038 RID: 56
	internal static class LightUtility
	{
		// Token: 0x06000161 RID: 353 RVA: 0x0000C170 File Offset: 0x0000A370
		public static bool CheckForChange(Light2D.LightType a, ref Light2D.LightType b)
		{
			bool flag = a != b;
			b = a;
			return flag;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000C17D File Offset: 0x0000A37D
		public static bool CheckForChange(Component a, ref Component b)
		{
			bool flag = a != b;
			b = a;
			return flag;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000C170 File Offset: 0x0000A370
		public static bool CheckForChange(int a, ref int b)
		{
			bool flag = a != b;
			b = a;
			return flag;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000C18A File Offset: 0x0000A38A
		public static bool CheckForChange(float a, ref float b)
		{
			bool flag = a != b;
			b = a;
			return flag;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000C197 File Offset: 0x0000A397
		public static bool CheckForChange(bool a, ref bool b)
		{
			bool flag = a != b;
			b = a;
			return flag;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000C1A4 File Offset: 0x0000A3A4
		private static bool TestPivot(List<IntPoint> path, int activePoint, long lastPoint)
		{
			for (int i = activePoint; i < path.Count; i++)
			{
				if (path[i].N > lastPoint)
				{
					return true;
				}
			}
			return path[activePoint].N == -1L;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
		private static List<IntPoint> DegeneratePivots(List<IntPoint> path, List<IntPoint> inPath, ref int interiorStart)
		{
			List<IntPoint> degenerate = new List<IntPoint>();
			long minN = path[0].N;
			long maxN = path[0].N;
			for (int i = 1; i < path.Count; i++)
			{
				if (path[i].N != -1L)
				{
					minN = Math.Min(minN, path[i].N);
					maxN = Math.Max(maxN, path[i].N);
				}
			}
			for (long j = 0L; j < minN; j += 1L)
			{
				IntPoint ins = path[(int)minN];
				ins.N = j;
				degenerate.Add(ins);
			}
			degenerate.AddRange(path.GetRange(0, path.Count));
			interiorStart = degenerate.Count;
			for (long k = maxN + 1L; k < (long)inPath.Count; k += 1L)
			{
				IntPoint ins2 = inPath[(int)k];
				ins2.N = k;
				degenerate.Add(ins2);
			}
			return degenerate;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		private static List<IntPoint> SortPivots(List<IntPoint> outPath, List<IntPoint> inPath)
		{
			List<IntPoint> sorted = new List<IntPoint>();
			IntPoint intPoint = outPath[0];
			long max = outPath[0].N;
			int minIndex = 0;
			bool newMin = true;
			for (int i = 1; i < outPath.Count; i++)
			{
				if (max > outPath[i].N && newMin && outPath[i].N != -1L)
				{
					max = outPath[i].N;
					minIndex = i;
					newMin = false;
				}
				else if (outPath[i].N >= max)
				{
					max = outPath[i].N;
					newMin = true;
				}
			}
			sorted.AddRange(outPath.GetRange(minIndex, outPath.Count - minIndex));
			sorted.AddRange(outPath.GetRange(0, minIndex));
			return sorted;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000C398 File Offset: 0x0000A598
		private static List<IntPoint> FixPivots(List<IntPoint> outPath, List<IntPoint> inPath, ref int interiorStart)
		{
			List<IntPoint> path = LightUtility.SortPivots(outPath, inPath);
			long pivotPoint = path[0].N;
			for (int i = 1; i < path.Count; i++)
			{
				int j = ((i == path.Count - 1) ? 0 : (i + 1));
				IntPoint prev = path[i - 1];
				IntPoint curr = path[i];
				IntPoint next = path[j];
				if (prev.N > curr.N && LightUtility.TestPivot(path, i, pivotPoint))
				{
					if (prev.N == next.N)
					{
						curr.N = prev.N;
					}
					else
					{
						curr.N = ((pivotPoint + 1L < (long)inPath.Count) ? (pivotPoint + 1L) : 0L);
					}
					curr.D = 3L;
					path[i] = curr;
				}
				pivotPoint = path[i].N;
			}
			int k = 1;
			while (k < path.Count - 1)
			{
				IntPoint prev2 = path[k - 1];
				IntPoint curr2 = path[k];
				IntPoint next2 = path[k + 1];
				if (curr2.N - prev2.N > 1L)
				{
					if (curr2.N == next2.N)
					{
						IntPoint ins = curr2;
						ins.N -= 1L;
						path[k] = ins;
					}
					else
					{
						IntPoint ins2 = curr2;
						ins2.N -= 1L;
						path.Insert(k, ins2);
					}
				}
				else
				{
					k++;
				}
			}
			return LightUtility.DegeneratePivots(path, inPath, ref interiorStart);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000C524 File Offset: 0x0000A724
		internal static List<Vector2> GetOutlinePath(Vector3[] shapePath, float offsetDistance)
		{
			List<IntPoint> path = new List<IntPoint>();
			List<Vector2> output = new List<Vector2>();
			for (int i = 0; i < shapePath.Length; i++)
			{
				Vector2 newPoint = new Vector2(shapePath[i].x, shapePath[i].y) * 10000f;
				path.Add(new IntPoint((long)newPoint.x, (long)newPoint.y));
			}
			List<List<IntPoint>> solution = new List<List<IntPoint>>();
			ClipperOffset clipperOffset = new ClipperOffset(24.0);
			clipperOffset.AddPath(path, JoinTypes.jtRound, EndTypes.etClosedPolygon);
			clipperOffset.Execute(ref solution, (double)(10000f * offsetDistance), path.Count);
			if (solution.Count > 0)
			{
				int interiorStart = 0;
				List<IntPoint> outPath = solution[0];
				outPath = LightUtility.FixPivots(outPath, path, ref interiorStart);
				for (int j = 0; j < outPath.Count; j++)
				{
					output.Add(new Vector2((float)outPath[j].X / 10000f, (float)outPath[j].Y / 10000f));
				}
			}
			return output;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000C630 File Offset: 0x0000A830
		private static void TransferToMesh(NativeArray<LightUtility.LightMeshVertex> vertices, int vertexCount, NativeArray<ushort> indices, int indexCount, Light2D light)
		{
			Mesh lightMesh = light.lightMesh;
			lightMesh.SetVertexBufferParams(vertexCount, LightUtility.LightMeshVertex.VertexLayout);
			lightMesh.SetVertexBufferData<LightUtility.LightMeshVertex>(vertices, 0, 0, vertexCount, 0, MeshUpdateFlags.Default);
			lightMesh.SetIndices<ushort>(indices, 0, indexCount, MeshTopology.Triangles, 0, true, 0);
			light.vertices = new LightUtility.LightMeshVertex[vertexCount];
			NativeArray<LightUtility.LightMeshVertex>.Copy(vertices, light.vertices, vertexCount);
			light.indices = new ushort[indexCount];
			NativeArray<ushort>.Copy(indices, light.indices, indexCount);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000C6A0 File Offset: 0x0000A8A0
		public static Bounds GenerateShapeMesh(Light2D light, Vector3[] shapePath, float falloffDistance, float batchColor)
		{
			UnityEngine.Random.State restoreState = UnityEngine.Random.state;
			UnityEngine.Random.InitState(123456);
			Color meshInteriorColor = new Color(0f, 0f, batchColor, 1f);
			Color meshExteriorColor = new Color(0f, 0f, batchColor, 0f);
			int inEdgeCount = shapePath.Length;
			NativeArray<int2> tessInEdges = new NativeArray<int2>(inEdgeCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<float2> tessInVertices = new NativeArray<float2>(inEdgeCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < inEdgeCount; i++)
			{
				int edgeEnd = i + 1;
				if (edgeEnd == inEdgeCount)
				{
					edgeEnd = 0;
				}
				int2 edge = new int2(i, edgeEnd);
				tessInEdges[i] = edge;
				int index = edge.x;
				tessInVertices[index] = new float2(shapePath[index].x, shapePath[index].y);
			}
			NativeArray<int> tessOutIndices = new NativeArray<int>(tessInEdges.Length * 8, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<float2> tessOutVertices = new NativeArray<float2>(tessInEdges.Length * 8, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<int2> tessOutEdges = new NativeArray<int2>(tessInEdges.Length * 8, Allocator.Temp, NativeArrayOptions.ClearMemory);
			int tessOutVertexCount = 0;
			int tessOutIndexCount = 0;
			int tessOutEdgeCount = 0;
			ModuleHandle.Tessellate(Allocator.Temp, tessInVertices, tessInEdges, ref tessOutVertices, ref tessOutVertexCount, ref tessOutIndices, ref tessOutIndexCount, ref tessOutEdges, ref tessOutEdgeCount);
			int inputPointCount = shapePath.Length;
			List<IntPoint> path = new List<IntPoint>();
			for (int j = 0; j < inputPointCount; j++)
			{
				long nx = (long)((double)shapePath[j].x * 10000.0);
				long ny = (long)((double)shapePath[j].y * 10000.0);
				path.Add(new IntPoint(nx + (long)UnityEngine.Random.Range(-10, 10), ny + (long)UnityEngine.Random.Range(-10, 10))
				{
					N = (long)j,
					D = -1L
				});
			}
			int lastPointIndex = inputPointCount - 1;
			int interiorStartPoint = 0;
			List<List<IntPoint>> solution = new List<List<IntPoint>>();
			ClipperOffset clipperOffset = new ClipperOffset(24.0);
			clipperOffset.AddPath(path, JoinTypes.jtRound, EndTypes.etClosedPolygon);
			clipperOffset.Execute(ref solution, (double)(10000f * falloffDistance), path.Count);
			if (solution.Count > 0)
			{
				List<IntPoint> outPath = solution[0];
				long minPath = (long)inputPointCount;
				for (int k = 0; k < outPath.Count; k++)
				{
					minPath = ((outPath[k].N != -1L) ? Math.Min(minPath, outPath[k].N) : minPath);
				}
				bool containsStart = minPath == 0L;
				outPath = LightUtility.FixPivots(outPath, path, ref interiorStartPoint);
				int totalOutVertices = tessOutVertexCount + outPath.Count + inputPointCount;
				int totalOutIndices = tessOutIndexCount + outPath.Count * 6 + 6;
				NativeArray<LightUtility.LightMeshVertex> outVertices = new NativeArray<LightUtility.LightMeshVertex>(totalOutVertices, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<ushort> outIndices = new NativeArray<ushort>(totalOutIndices, Allocator.Temp, NativeArrayOptions.ClearMemory);
				for (int l = 0; l < tessOutIndexCount; l++)
				{
					outIndices[l] = (ushort)tessOutIndices[l];
				}
				for (int m = 0; m < tessOutVertexCount; m++)
				{
					int num = m;
					LightUtility.LightMeshVertex lightMeshVertex = new LightUtility.LightMeshVertex
					{
						position = new float3(tessOutVertices[m].x, tessOutVertices[m].y, 0f),
						color = meshInteriorColor
					};
					outVertices[num] = lightMeshVertex;
				}
				int vcount = tessOutVertexCount;
				int icount = tessOutIndexCount;
				ushort[] innerIndices = new ushort[inputPointCount];
				for (int n = 0; n < inputPointCount; n++)
				{
					int num2 = vcount++;
					LightUtility.LightMeshVertex lightMeshVertex = new LightUtility.LightMeshVertex
					{
						position = new float3(shapePath[n].x, shapePath[n].y, 0f),
						color = meshInteriorColor
					};
					outVertices[num2] = lightMeshVertex;
					innerIndices[n] = (ushort)(vcount - 1);
				}
				ushort saveIndex = (ushort)vcount;
				ushort pathStart = saveIndex;
				long prevIndex = ((outPath[0].N == -1L) ? 0L : outPath[0].N);
				for (int i2 = 0; i2 < outPath.Count; i2++)
				{
					IntPoint curr = outPath[i2];
					float2 currPoint = new float2((float)curr.X / 10000f, (float)curr.Y / 10000f);
					long currIndex = ((curr.N == -1L) ? 0L : curr.N);
					int num3 = vcount++;
					LightUtility.LightMeshVertex lightMeshVertex = new LightUtility.LightMeshVertex
					{
						position = new float3(currPoint.x, currPoint.y, 0f),
						color = ((interiorStartPoint > i2) ? meshExteriorColor : meshInteriorColor)
					};
					outVertices[num3] = lightMeshVertex;
					if (prevIndex != currIndex)
					{
						outIndices[icount++] = innerIndices[(int)(checked((IntPtr)prevIndex))];
						outIndices[icount++] = innerIndices[(int)(checked((IntPtr)currIndex))];
						outIndices[icount++] = (ushort)(vcount - 1);
					}
					outIndices[icount++] = innerIndices[(int)(checked((IntPtr)prevIndex))];
					outIndices[icount++] = saveIndex;
					saveIndex = (outIndices[icount++] = (ushort)(vcount - 1));
					prevIndex = currIndex;
				}
				outIndices[icount++] = pathStart;
				outIndices[icount++] = innerIndices[(int)(checked((IntPtr)minPath))];
				outIndices[icount++] = (containsStart ? innerIndices[lastPointIndex] : saveIndex);
				outIndices[icount++] = (containsStart ? pathStart : saveIndex);
				outIndices[icount++] = (containsStart ? saveIndex : innerIndices[(int)(checked((IntPtr)minPath))]);
				if (containsStart)
				{
					float kTolerance = 0.001f;
					ushort connectingPoint = innerIndices[lastPointIndex];
					bool flag = MathF.Abs(outVertices[(int)connectingPoint].position.x - outVertices[(int)outIndices[icount - 1]].position.x) > kTolerance || MathF.Abs(outVertices[(int)connectingPoint].position.y - outVertices[(int)outIndices[icount - 1]].position.y) > kTolerance;
					bool testB = MathF.Abs(outVertices[(int)connectingPoint].position.x - outVertices[(int)outIndices[icount - 2]].position.x) > kTolerance || MathF.Abs(outVertices[(int)connectingPoint].position.y - outVertices[(int)outIndices[icount - 2]].position.y) > kTolerance;
					if (!flag || !testB)
					{
						connectingPoint = (ushort)(interiorStartPoint + inputPointCount + tessOutVertexCount - 1);
					}
					outIndices[icount++] = connectingPoint;
				}
				else
				{
					outIndices[icount++] = innerIndices[(int)(checked((IntPtr)(unchecked(minPath - 1L))))];
				}
				LightUtility.TransferToMesh(outVertices, vcount, outIndices, icount, light);
			}
			UnityEngine.Random.state = restoreState;
			return light.lightMesh.GetSubMesh(0).bounds;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public static Bounds GenerateParametricMesh(Light2D light, float radius, float falloffDistance, float angle, int sides, float batchColor)
		{
			float angleOffset = 1.5707964f + 0.017453292f * angle;
			if (sides < 3)
			{
				radius = 0.70710677f * radius;
				sides = 4;
			}
			if (sides == 4)
			{
				angleOffset = 0.7853982f + 0.017453292f * angle;
			}
			int vertexCount = 1 + 2 * sides;
			int indexCount = 9 * sides;
			NativeArray<LightUtility.LightMeshVertex> vertices = new NativeArray<LightUtility.LightMeshVertex>(vertexCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<ushort> triangles = new NativeArray<ushort>(indexCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
			ushort centerIndex = (ushort)(2 * sides);
			Mesh mesh = light.lightMesh;
			Color color = new Color(0f, 0f, batchColor, 1f);
			vertices[(int)centerIndex] = new LightUtility.LightMeshVertex
			{
				position = float3.zero,
				color = color
			};
			float radiansPerSide = 6.2831855f / (float)sides;
			float3 min = new float3(float.MaxValue, float.MaxValue, 0f);
			float3 max = new float3(float.MinValue, float.MinValue, 0f);
			for (int i = 0; i < sides; i++)
			{
				float endAngle = (float)(i + 1) * radiansPerSide;
				float3 extrudeDir = new float3(math.cos(endAngle + angleOffset), math.sin(endAngle + angleOffset), 0f);
				float3 endPoint = radius * extrudeDir;
				int vertexIndex = (2 * i + 2) % (2 * sides);
				vertices[vertexIndex] = new LightUtility.LightMeshVertex
				{
					position = endPoint,
					color = new Color(extrudeDir.x, extrudeDir.y, batchColor, 0f)
				};
				vertices[vertexIndex + 1] = new LightUtility.LightMeshVertex
				{
					position = endPoint,
					color = color
				};
				int triangleIndex = 9 * i;
				triangles[triangleIndex] = (ushort)(vertexIndex + 1);
				triangles[triangleIndex + 1] = (ushort)(2 * i + 1);
				triangles[triangleIndex + 2] = centerIndex;
				triangles[triangleIndex + 3] = (ushort)vertexIndex;
				triangles[triangleIndex + 4] = (ushort)(2 * i);
				triangles[triangleIndex + 5] = (ushort)(2 * i + 1);
				triangles[triangleIndex + 6] = (ushort)(vertexIndex + 1);
				triangles[triangleIndex + 7] = (ushort)vertexIndex;
				triangles[triangleIndex + 8] = (ushort)(2 * i + 1);
				min = math.min(min, endPoint + extrudeDir * falloffDistance);
				max = math.max(max, endPoint + extrudeDir * falloffDistance);
			}
			mesh.SetVertexBufferParams(vertexCount, LightUtility.LightMeshVertex.VertexLayout);
			mesh.SetVertexBufferData<LightUtility.LightMeshVertex>(vertices, 0, 0, vertexCount, 0, MeshUpdateFlags.Default);
			mesh.SetIndices<ushort>(triangles, MeshTopology.Triangles, 0, false, 0);
			light.vertices = new LightUtility.LightMeshVertex[vertexCount];
			NativeArray<LightUtility.LightMeshVertex>.Copy(vertices, light.vertices, vertexCount);
			light.indices = new ushort[indexCount];
			NativeArray<ushort>.Copy(triangles, light.indices, indexCount);
			return new Bounds
			{
				min = min,
				max = max
			};
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000D05C File Offset: 0x0000B25C
		public static Bounds GenerateSpriteMesh(Light2D light, Sprite sprite, float batchColor)
		{
			Mesh mesh = light.lightMesh;
			if (sprite == null)
			{
				mesh.Clear();
				return new Bounds(Vector3.zero, Vector3.zero);
			}
			Vector2[] uv = sprite.uv;
			NativeSlice<Vector3> srcVertices = sprite.GetVertexAttribute(VertexAttribute.Position);
			NativeSlice<Vector2> srcUVs = sprite.GetVertexAttribute(VertexAttribute.TexCoord0);
			NativeArray<ushort> srcIndices = sprite.GetIndices();
			0.5f * (sprite.bounds.min + sprite.bounds.max);
			NativeArray<LightUtility.LightMeshVertex> vertices = new NativeArray<LightUtility.LightMeshVertex>(srcIndices.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
			Color color = new Color(0f, 0f, batchColor, 1f);
			for (int i = 0; i < srcVertices.Length; i++)
			{
				vertices[i] = new LightUtility.LightMeshVertex
				{
					position = new Vector3(srcVertices[i].x, srcVertices[i].y, 0f),
					color = color,
					uv = srcUVs[i]
				};
			}
			mesh.SetVertexBufferParams(vertices.Length, LightUtility.LightMeshVertex.VertexLayout);
			mesh.SetVertexBufferData<LightUtility.LightMeshVertex>(vertices, 0, 0, vertices.Length, 0, MeshUpdateFlags.Default);
			mesh.SetIndices<ushort>(srcIndices, MeshTopology.Triangles, 0, true, 0);
			light.vertices = new LightUtility.LightMeshVertex[vertices.Length];
			NativeArray<LightUtility.LightMeshVertex>.Copy(vertices, light.vertices, vertices.Length);
			light.indices = new ushort[srcIndices.Length];
			NativeArray<ushort>.Copy(srcIndices, light.indices, srcIndices.Length);
			return mesh.GetSubMesh(0).bounds;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000D1FC File Offset: 0x0000B3FC
		public static int GetShapePathHash(Vector3[] path)
		{
			int hashCode = -2128831035;
			if (path != null)
			{
				foreach (Vector3 point in path)
				{
					hashCode = (hashCode * 16777619) ^ point.GetHashCode();
				}
			}
			else
			{
				hashCode = 0;
			}
			return hashCode;
		}

		// Token: 0x02000039 RID: 57
		private enum PivotType
		{
			// Token: 0x0400011E RID: 286
			PivotBase,
			// Token: 0x0400011F RID: 287
			PivotCurve,
			// Token: 0x04000120 RID: 288
			PivotIntersect,
			// Token: 0x04000121 RID: 289
			PivotSkip,
			// Token: 0x04000122 RID: 290
			PivotClip
		}

		// Token: 0x0200003A RID: 58
		[Serializable]
		internal struct LightMeshVertex
		{
			// Token: 0x04000123 RID: 291
			public Vector3 position;

			// Token: 0x04000124 RID: 292
			public Color color;

			// Token: 0x04000125 RID: 293
			public Vector2 uv;

			// Token: 0x04000126 RID: 294
			public static readonly VertexAttributeDescriptor[] VertexLayout = new VertexAttributeDescriptor[]
			{
				new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3, 0),
				new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.Float32, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2, 0)
			};
		}
	}
}
