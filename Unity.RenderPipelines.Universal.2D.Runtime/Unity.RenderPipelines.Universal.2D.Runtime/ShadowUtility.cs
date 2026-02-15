using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AOT;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine.Rendering.Universal.UTess;
using UnityEngine.U2D;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200007F RID: 127
	[BurstCompile]
	internal class ShadowUtility
	{
		// Token: 0x06000317 RID: 791 RVA: 0x000172FA File Offset: 0x000154FA
		private unsafe static int GetNextShapeStart(int currentShape, int* inShapeStartingEdgePtr, int inShapeStartingEdgeLength, int maxValue)
		{
			if (currentShape + 1 >= inShapeStartingEdgeLength || inShapeStartingEdgePtr[currentShape + 1] < 0)
			{
				return maxValue;
			}
			return inShapeStartingEdgePtr[currentShape + 1];
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00017319 File Offset: 0x00015519
		[BurstCompile]
		[MonoPInvokeCallback(typeof(ShadowUtility.CalculateProjectionInfo_000002E7$PostfixBurstDelegate))]
		internal static void CalculateProjectionInfo(ref NativeArray<Vector3> inVertices, ref NativeArray<ShadowEdge> inEdges, ref NativeArray<int> inShapeStartingEdge, ref NativeArray<bool> inShapeIsClosedArray, ref NativeArray<Vector2> outProjectionInfo)
		{
			ShadowUtility.CalculateProjectionInfo_000002E7$BurstDirectCall.Invoke(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, ref outProjectionInfo);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00017326 File Offset: 0x00015526
		[BurstCompile]
		[MonoPInvokeCallback(typeof(ShadowUtility.CalculateVertices_000002E8$PostfixBurstDelegate))]
		internal static void CalculateVertices(ref NativeArray<Vector3> inVertices, ref NativeArray<ShadowEdge> inEdges, ref NativeArray<Vector2> inEdgeOtherPoints, ref NativeArray<ShadowUtility.ShadowMeshVertex> outMeshVertices)
		{
			ShadowUtility.CalculateVertices_000002E8$BurstDirectCall.Invoke(ref inVertices, ref inEdges, ref inEdgeOtherPoints, ref outMeshVertices);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00017331 File Offset: 0x00015531
		[BurstCompile]
		[MonoPInvokeCallback(typeof(ShadowUtility.CalculateTriangles_000002E9$PostfixBurstDelegate))]
		internal static void CalculateTriangles(ref NativeArray<Vector3> inVertices, ref NativeArray<ShadowEdge> inEdges, ref NativeArray<int> inShapeStartingEdge, ref NativeArray<bool> inShapeIsClosedArray, ref NativeArray<int> outMeshIndices)
		{
			ShadowUtility.CalculateTriangles_000002E9$BurstDirectCall.Invoke(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, ref outMeshIndices);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0001733E File Offset: 0x0001553E
		[BurstCompile]
		[MonoPInvokeCallback(typeof(ShadowUtility.CalculateLocalBounds_000002EA$PostfixBurstDelegate))]
		internal static void CalculateLocalBounds(ref NativeArray<Vector3> inVertices, out Bounds retBounds)
		{
			ShadowUtility.CalculateLocalBounds_000002EA$BurstDirectCall.Invoke(ref inVertices, out retBounds);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00017347 File Offset: 0x00015547
		[BurstCompile]
		[MonoPInvokeCallback(typeof(ShadowUtility.GenerateInteriorMesh_000002EB$PostfixBurstDelegate))]
		private static void GenerateInteriorMesh(ref NativeArray<ShadowUtility.ShadowMeshVertex> inVertices, ref NativeArray<int> inIndices, ref NativeArray<ShadowEdge> inEdges, out NativeArray<ShadowUtility.ShadowMeshVertex> outVertices, out NativeArray<int> outIndices, out int outStartIndex, out int outIndexCount)
		{
			ShadowUtility.GenerateInteriorMesh_000002EB$BurstDirectCall.Invoke(ref inVertices, ref inIndices, ref inEdges, out outVertices, out outIndices, out outStartIndex, out outIndexCount);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00017358 File Offset: 0x00015558
		public static Bounds GenerateShadowMesh(Mesh mesh, NativeArray<Vector3> inVertices, NativeArray<ShadowEdge> inEdges, NativeArray<int> inShapeStartingEdge, NativeArray<bool> inShapeIsClosedArray, bool allowContraction, bool fill, ShadowShape2D.OutlineTopology topology)
		{
			int meshVertexCount = inVertices.Length + 4 * inEdges.Length;
			int meshIndexCount = inEdges.Length * 3 * 3;
			NativeArray<Vector2> meshProjectionInfo = new NativeArray<Vector2>(meshVertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			NativeArray<int> meshIndices = new NativeArray<int>(meshIndexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			NativeArray<ShadowUtility.ShadowMeshVertex> meshVertices = new NativeArray<ShadowUtility.ShadowMeshVertex>(meshVertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			ShadowUtility.CalculateProjectionInfo(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, ref meshProjectionInfo);
			ShadowUtility.CalculateVertices(ref inVertices, ref inEdges, ref meshProjectionInfo, ref meshVertices);
			ShadowUtility.CalculateTriangles(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, ref meshIndices);
			int fillSubmeshStartIndex = 0;
			int fillSubmeshIndexCount = 0;
			NativeArray<ShadowUtility.ShadowMeshVertex> finalVertices;
			NativeArray<int> finalIndices;
			if (fill)
			{
				ShadowUtility.GenerateInteriorMesh(ref meshVertices, ref meshIndices, ref inEdges, out finalVertices, out finalIndices, out fillSubmeshStartIndex, out fillSubmeshIndexCount);
				meshVertices.Dispose();
				meshIndices.Dispose();
			}
			else
			{
				finalVertices = meshVertices;
				finalIndices = meshIndices;
			}
			mesh.SetVertexBufferParams(finalVertices.Length, ShadowUtility.m_VertexLayout);
			mesh.SetVertexBufferData<ShadowUtility.ShadowMeshVertex>(finalVertices, 0, 0, finalVertices.Length, 0, MeshUpdateFlags.Default);
			mesh.SetIndexBufferParams(finalIndices.Length, IndexFormat.UInt32);
			mesh.SetIndexBufferData<int>(finalIndices, 0, 0, finalIndices.Length, MeshUpdateFlags.Default);
			mesh.SetSubMesh(0, new SubMeshDescriptor(0, finalIndices.Length, MeshTopology.Triangles), MeshUpdateFlags.Default);
			mesh.subMeshCount = 1;
			meshProjectionInfo.Dispose();
			finalVertices.Dispose();
			finalIndices.Dispose();
			Bounds retLocalBound;
			ShadowUtility.CalculateLocalBounds(ref inVertices, out retLocalBound);
			return retLocalBound;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00017482 File Offset: 0x00015682
		[BurstCompile]
		[MonoPInvokeCallback(typeof(ShadowUtility.CalculateEdgesFromLines_000002ED$PostfixBurstDelegate))]
		public static void CalculateEdgesFromLines(ref NativeArray<int> indices, out NativeArray<ShadowEdge> outEdges, out NativeArray<int> outShapeStartingEdge, out NativeArray<bool> outShapeIsClosedArray)
		{
			ShadowUtility.CalculateEdgesFromLines_000002ED$BurstDirectCall.Invoke(ref indices, out outEdges, out outShapeStartingEdge, out outShapeIsClosedArray);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0001748D File Offset: 0x0001568D
		[BurstCompile]
		[MonoPInvokeCallback(typeof(ShadowUtility.GetVertexReferenceStats_000002EE$PostfixBurstDelegate))]
		internal static void GetVertexReferenceStats(ref NativeArray<Vector3> vertices, ref NativeArray<ShadowEdge> edges, int vertexCount, out bool hasReusedVertices, out int newVertexCount, out NativeArray<ShadowUtility.RemappingInfo> remappingInfo)
		{
			ShadowUtility.GetVertexReferenceStats_000002EE$BurstDirectCall.Invoke(ref vertices, ref edges, vertexCount, out hasReusedVertices, out newVertexCount, out remappingInfo);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0001749C File Offset: 0x0001569C
		public static bool IsTriangleReversed(NativeArray<Vector3> vertices, int idx0, int idx1, int idx2)
		{
			Vector3 v0 = vertices[idx0];
			Vector3 v = vertices[idx1];
			Vector3 v2 = vertices[idx2];
			return Mathf.Sign(v0.x * v.y + v.x * v2.y + v2.x * v0.y - (v0.y * v.x + v.y * v2.x + v2.y * v0.x)) >= 0f;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00017526 File Offset: 0x00015726
		[BurstCompile]
		[MonoPInvokeCallback(typeof(ShadowUtility.CalculateEdgesFromTriangles_000002F0$PostfixBurstDelegate))]
		public static void CalculateEdgesFromTriangles(ref NativeArray<Vector3> vertices, ref NativeArray<int> indices, bool duplicatesVertices, out NativeArray<Vector3> newVertices, out NativeArray<ShadowEdge> outEdges, out NativeArray<int> outShapeStartingEdge, out NativeArray<bool> outShapeIsClosedArray)
		{
			ShadowUtility.CalculateEdgesFromTriangles_000002F0$BurstDirectCall.Invoke(ref vertices, ref indices, duplicatesVertices, out newVertices, out outEdges, out outShapeStartingEdge, out outShapeIsClosedArray);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00017537 File Offset: 0x00015737
		[BurstCompile]
		[MonoPInvokeCallback(typeof(ShadowUtility.ReverseWindingOrder_000002F1$PostfixBurstDelegate))]
		public static void ReverseWindingOrder(ref NativeArray<int> inShapeStartingEdge, ref NativeArray<ShadowEdge> inOutSortedEdges)
		{
			ShadowUtility.ReverseWindingOrder_000002F1$BurstDirectCall.Invoke(ref inShapeStartingEdge, ref inOutSortedEdges);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00017540 File Offset: 0x00015740
		private static int GetClosedPathCount(ref NativeArray<int> inShapeStartingEdge, ref NativeArray<bool> inShapeIsClosedArray)
		{
			int count = 0;
			int i = 0;
			while (i < inShapeStartingEdge.Length && inShapeStartingEdge[i] >= 0)
			{
				count++;
				i++;
			}
			return count;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00017570 File Offset: 0x00015770
		private static void GetPathInfo(NativeArray<ShadowEdge> inEdges, NativeArray<int> inShapeStartingEdge, NativeArray<bool> inShapeIsClosedArray, out int closedPathArrayCount, out int closedPathsCount, out int openPathArrayCount, out int openPathsCount)
		{
			closedPathArrayCount = 0;
			openPathArrayCount = 0;
			closedPathsCount = 0;
			openPathsCount = 0;
			int i = 0;
			while (i < inShapeStartingEdge.Length && inShapeStartingEdge[i] >= 0)
			{
				int start = inShapeStartingEdge[i];
				int edges = ((i < inShapeStartingEdge.Length - 1 && inShapeStartingEdge[i + 1] != -1) ? inShapeStartingEdge[i + 1] : inEdges.Length) - start;
				if (inShapeIsClosedArray[i])
				{
					closedPathArrayCount += edges + 1;
					closedPathsCount++;
				}
				else
				{
					openPathArrayCount += edges + 1;
					openPathsCount++;
				}
				i++;
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001760F File Offset: 0x0001580F
		[BurstCompile]
		[MonoPInvokeCallback(typeof(ShadowUtility.ClipEdges_000002F4$PostfixBurstDelegate))]
		public static void ClipEdges(ref NativeArray<Vector3> inVertices, ref NativeArray<ShadowEdge> inEdges, ref NativeArray<int> inShapeStartingEdge, ref NativeArray<bool> inShapeIsClosedArray, float contractEdge, out NativeArray<Vector3> outVertices, out NativeArray<ShadowEdge> outEdges, out NativeArray<int> outShapeStartingEdge)
		{
			ShadowUtility.ClipEdges_000002F4$BurstDirectCall.Invoke(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, contractEdge, out outVertices, out outEdges, out outShapeStartingEdge);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00017650 File Offset: 0x00015850
		[BurstCompile]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void CalculateProjectionInfo$BurstManaged(ref NativeArray<Vector3> inVertices, ref NativeArray<ShadowEdge> inEdges, ref NativeArray<int> inShapeStartingEdge, ref NativeArray<bool> inShapeIsClosedArray, ref NativeArray<Vector2> outProjectionInfo)
		{
			Vector3* inVerticesPtr = (Vector3*)inVertices.m_Buffer;
			ShadowEdge* inEdgesPtr = (ShadowEdge*)inEdges.m_Buffer;
			int* inShapeStartingEdgePtr = (int*)inShapeStartingEdge.m_Buffer;
			bool* inShapeIsClosedArrayPtr = (bool*)inShapeIsClosedArray.m_Buffer;
			Vector2* outProjectionInfoPtr = (Vector2*)outProjectionInfo.m_Buffer;
			Vector2 tmpVec2 = default(Vector2);
			int inEdgesLength = inEdges.Length;
			int inShapeStartingEdgeLength = inShapeStartingEdge.Length;
			int inVerticesLength = inVertices.Length;
			int currentShape = 0;
			int shapeStart = 0;
			int nextShapeStart = ShadowUtility.GetNextShapeStart(currentShape, inShapeStartingEdgePtr, inShapeStartingEdgeLength, inEdgesLength);
			int shapeSize = nextShapeStart;
			for (int i = 0; i < inEdgesLength; i++)
			{
				if (i == nextShapeStart)
				{
					currentShape++;
					shapeStart = nextShapeStart;
					nextShapeStart = ShadowUtility.GetNextShapeStart(currentShape, inShapeStartingEdgePtr, inShapeStartingEdgeLength, inEdgesLength);
					shapeSize = nextShapeStart - shapeStart;
				}
				int nextEdgeIndex = (i - shapeStart + 1) % shapeSize + shapeStart;
				int prevEdgeIndex = (i - shapeStart + shapeSize - 1) % shapeSize + shapeStart;
				int v0 = inEdgesPtr[i].v0;
				int v = inEdgesPtr[i].v1;
				int prev = inEdgesPtr[prevEdgeIndex].v0;
				int next0 = inEdgesPtr[nextEdgeIndex].v1;
				tmpVec2.x = inVerticesPtr[v0].x;
				tmpVec2.y = inVerticesPtr[v0].y;
				Vector2 startPt = tmpVec2;
				tmpVec2.x = inVerticesPtr[v].x;
				tmpVec2.y = inVerticesPtr[v].y;
				Vector2 endPt = tmpVec2;
				tmpVec2.x = inVerticesPtr[prev].x;
				tmpVec2.y = inVerticesPtr[prev].y;
				tmpVec2.x = inVerticesPtr[next0].x;
				tmpVec2.y = inVerticesPtr[next0].y;
				outProjectionInfoPtr[v0] = endPt;
				int additionalVerticesStart = 4 * i + inVerticesLength;
				outProjectionInfoPtr[additionalVerticesStart] = endPt;
				outProjectionInfoPtr[additionalVerticesStart + 1] = startPt;
				outProjectionInfoPtr[additionalVerticesStart + 2] = endPt;
				outProjectionInfoPtr[additionalVerticesStart + 3] = endPt;
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000178A0 File Offset: 0x00015AA0
		[BurstCompile]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void CalculateVertices$BurstManaged(ref NativeArray<Vector3> inVertices, ref NativeArray<ShadowEdge> inEdges, ref NativeArray<Vector2> inEdgeOtherPoints, ref NativeArray<ShadowUtility.ShadowMeshVertex> outMeshVertices)
		{
			Vector3* inVerticesPtr = (Vector3*)inVertices.m_Buffer;
			ShadowEdge* inEdgesPtr = (ShadowEdge*)inEdges.m_Buffer;
			Vector2* inEdgeOtherPointsPtr = (Vector2*)inEdgeOtherPoints.m_Buffer;
			ShadowUtility.ShadowMeshVertex* outMeshVerticesPtr = (ShadowUtility.ShadowMeshVertex*)outMeshVertices.m_Buffer;
			Vector2 tmpVec2 = default(Vector2);
			int inEdgesLength = inEdges.Length;
			int inVerticesLength = inVertices.Length;
			for (int i = 0; i < inVerticesLength; i++)
			{
				tmpVec2.x = inVerticesPtr[i].x;
				tmpVec2.y = inVerticesPtr[i].y;
				ShadowUtility.ShadowMeshVertex originalShadowMesh = new ShadowUtility.ShadowMeshVertex(ShadowUtility.ProjectionType.ProjectionNone, tmpVec2, inEdgeOtherPointsPtr[i]);
				outMeshVerticesPtr[i] = originalShadowMesh;
			}
			for (int j = 0; j < inEdgesLength; j++)
			{
				int v0 = inEdgesPtr[j].v0;
				int v = inEdgesPtr[j].v1;
				tmpVec2.x = inVerticesPtr[v0].x;
				tmpVec2.y = inVerticesPtr[v0].y;
				Vector2 pt0 = tmpVec2;
				tmpVec2.x = inVerticesPtr[v].x;
				tmpVec2.y = inVerticesPtr[v].y;
				Vector2 pt = tmpVec2;
				int additionalVerticesStart = 4 * j + inVerticesLength;
				ShadowUtility.ShadowMeshVertex additionalVertex0 = new ShadowUtility.ShadowMeshVertex(ShadowUtility.ProjectionType.ProjectionHard, pt0, inEdgeOtherPointsPtr[additionalVerticesStart]);
				ShadowUtility.ShadowMeshVertex additionalVertex = new ShadowUtility.ShadowMeshVertex(ShadowUtility.ProjectionType.ProjectionHard, pt, inEdgeOtherPointsPtr[additionalVerticesStart + 1]);
				ShadowUtility.ShadowMeshVertex additionalVertex2 = new ShadowUtility.ShadowMeshVertex(ShadowUtility.ProjectionType.ProjectionSoftLeft, pt0, inEdgeOtherPointsPtr[additionalVerticesStart + 2]);
				ShadowUtility.ShadowMeshVertex additionalVertex3 = new ShadowUtility.ShadowMeshVertex(ShadowUtility.ProjectionType.ProjectionSoftRight, pt0, inEdgeOtherPointsPtr[additionalVerticesStart + 3]);
				outMeshVerticesPtr[additionalVerticesStart] = additionalVertex0;
				outMeshVerticesPtr[additionalVerticesStart + 1] = additionalVertex;
				outMeshVerticesPtr[additionalVerticesStart + 2] = additionalVertex2;
				outMeshVerticesPtr[additionalVerticesStart + 3] = additionalVertex3;
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00017AC4 File Offset: 0x00015CC4
		[BurstCompile]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void CalculateTriangles$BurstManaged(ref NativeArray<Vector3> inVertices, ref NativeArray<ShadowEdge> inEdges, ref NativeArray<int> inShapeStartingEdge, ref NativeArray<bool> inShapeIsClosedArray, ref NativeArray<int> outMeshIndices)
		{
			ShadowEdge* inEdgesPtr = (ShadowEdge*)inEdges.m_Buffer;
			int* inShapeStartingEdgePtr = (int*)inShapeStartingEdge.m_Buffer;
			int* outMeshIndicesPtr = (int*)outMeshIndices.m_Buffer;
			int inEdgesLength = inEdges.Length;
			int inShapeStartingEdgeLength = inShapeStartingEdge.Length;
			int inVerticesLength = inVertices.Length;
			int meshIndex = 0;
			for (int shapeIndex = 0; shapeIndex < inShapeStartingEdgeLength; shapeIndex++)
			{
				int startingIndex = inShapeStartingEdgePtr[shapeIndex];
				if (startingIndex < 0)
				{
					return;
				}
				int endIndex = inEdgesLength;
				if (shapeIndex + 1 < inShapeStartingEdgeLength && inShapeStartingEdgePtr[shapeIndex + 1] > -1)
				{
					endIndex = inShapeStartingEdgePtr[shapeIndex + 1];
				}
				for (int i = startingIndex; i < endIndex; i++)
				{
					int v0 = inEdgesPtr[i].v0;
					int v = inEdgesPtr[i].v1;
					int additionalVerticesStart = 4 * i + inVerticesLength;
					outMeshIndicesPtr[(IntPtr)(meshIndex++) * 4] = (int)((ushort)v0);
					outMeshIndicesPtr[(IntPtr)(meshIndex++) * 4] = (int)((ushort)additionalVerticesStart);
					outMeshIndicesPtr[(IntPtr)(meshIndex++) * 4] = (int)((ushort)(additionalVerticesStart + 1));
					outMeshIndicesPtr[(IntPtr)(meshIndex++) * 4] = (int)((ushort)(additionalVerticesStart + 1));
					outMeshIndicesPtr[(IntPtr)(meshIndex++) * 4] = (int)((ushort)v);
					outMeshIndicesPtr[(IntPtr)(meshIndex++) * 4] = (int)((ushort)v0);
				}
				for (int j = startingIndex; j < endIndex; j++)
				{
					int v2 = inEdgesPtr[j].v0;
					ShadowEdge shadowEdge = inEdgesPtr[j];
					int additionalVerticesStart2 = 4 * j + inVerticesLength;
					outMeshIndicesPtr[(IntPtr)(meshIndex++) * 4] = (int)((ushort)v2);
					outMeshIndicesPtr[(IntPtr)(meshIndex++) * 4] = (int)((ushort)additionalVerticesStart2 + 2);
					outMeshIndicesPtr[(IntPtr)(meshIndex++) * 4] = (int)((ushort)additionalVerticesStart2 + 3);
				}
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00017C6C File Offset: 0x00015E6C
		[BurstCompile]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void CalculateLocalBounds$BurstManaged(ref NativeArray<Vector3> inVertices, out Bounds retBounds)
		{
			if (inVertices.Length <= 0)
			{
				retBounds = new Bounds(Vector3.zero, Vector3.zero);
				return;
			}
			Vector2 minVec = Vector2.positiveInfinity;
			Vector2 maxVec = Vector2.negativeInfinity;
			Vector3* inVerticesPtr = (Vector3*)inVertices.m_Buffer;
			int inVerticesLength = inVertices.Length;
			for (int i = 0; i < inVerticesLength; i++)
			{
				Vector2 vertex = new Vector2(inVerticesPtr[i].x, inVerticesPtr[i].y);
				minVec = Vector2.Min(minVec, vertex);
				maxVec = Vector2.Max(maxVec, vertex);
			}
			retBounds = new Bounds
			{
				max = maxVec,
				min = minVec
			};
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00017D28 File Offset: 0x00015F28
		[BurstCompile]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void GenerateInteriorMesh$BurstManaged(ref NativeArray<ShadowUtility.ShadowMeshVertex> inVertices, ref NativeArray<int> inIndices, ref NativeArray<ShadowEdge> inEdges, out NativeArray<ShadowUtility.ShadowMeshVertex> outVertices, out NativeArray<int> outIndices, out int outStartIndex, out int outIndexCount)
		{
			int inEdgeCount = inEdges.Length;
			NativeArray<int2> tessInEdges = new NativeArray<int2>(inEdgeCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			NativeArray<float2> tessInVertices = new NativeArray<float2>(inEdgeCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			for (int i = 0; i < inEdgeCount; i++)
			{
				int2 edge = new int2(inEdges[i].v0, inEdges[i].v1);
				tessInEdges[i] = edge;
				int index = edge.x;
				tessInVertices[index] = new float2(inVertices[index].position.x, inVertices[index].position.y);
			}
			NativeArray<int> tessOutIndices = new NativeArray<int>(tessInVertices.Length * 8, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			NativeArray<float2> tessOutVertices = new NativeArray<float2>(tessInVertices.Length * 4, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			NativeArray<int2> tessOutEdges = new NativeArray<int2>(tessInEdges.Length * 4, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			int tessOutVertexCount = 0;
			int tessOutIndexCount = 0;
			int tessOutEdgeCount = 0;
			ModuleHandle.Tessellate(Allocator.Persistent, tessInVertices, tessInEdges, ref tessOutVertices, ref tessOutVertexCount, ref tessOutIndices, ref tessOutIndexCount, ref tessOutEdges, ref tessOutEdgeCount);
			int indexOffset = inIndices.Length;
			int vertexOffset = inVertices.Length;
			int totalOutVertices = tessOutVertexCount + inVertices.Length;
			int totalOutIndices = tessOutIndexCount + inIndices.Length;
			outVertices = new NativeArray<ShadowUtility.ShadowMeshVertex>(totalOutVertices, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			outIndices = new NativeArray<int>(totalOutIndices, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			for (int j = 0; j < inVertices.Length; j++)
			{
				outVertices[j] = inVertices[j];
			}
			for (int k = 0; k < tessOutVertexCount; k++)
			{
				float2 tessVertex = tessOutVertices[k];
				ShadowUtility.ShadowMeshVertex vertex = new ShadowUtility.ShadowMeshVertex(ShadowUtility.ProjectionType.ProjectionNone, tessVertex, Vector2.zero);
				outVertices[k + vertexOffset] = vertex;
			}
			for (int l = 0; l < inIndices.Length; l++)
			{
				outIndices[l] = inIndices[l];
			}
			for (int m = 0; m < tessOutIndexCount; m++)
			{
				outIndices[m + indexOffset] = tessOutIndices[m] + vertexOffset;
			}
			outStartIndex = indexOffset;
			outIndexCount = tessOutIndexCount;
			tessInEdges.Dispose();
			tessInVertices.Dispose();
			tessOutIndices.Dispose();
			tessOutVertices.Dispose();
			tessOutEdges.Dispose();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00017F40 File Offset: 0x00016140
		[BurstCompile]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void CalculateEdgesFromLines$BurstManaged(ref NativeArray<int> indices, out NativeArray<ShadowEdge> outEdges, out NativeArray<int> outShapeStartingEdge, out NativeArray<bool> outShapeIsClosedArray)
		{
			int numOfEdges = indices.Length >> 1;
			NativeArray<int> tempShapeStartIndices = new NativeArray<int>(numOfEdges, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			NativeArray<bool> tempShapeIsClosedArray = new NativeArray<bool>(numOfEdges, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			int* indicesPtr = (int*)indices.m_Buffer;
			int* tempShapeStartIndicesPtr = (int*)tempShapeStartIndices.m_Buffer;
			bool* tempShapeIsClosedArrayPtr = (bool*)tempShapeIsClosedArray.m_Buffer;
			int indicesLength = indices.Length;
			int shapeCount = 0;
			int shapeStart = *indicesPtr;
			int lastIndex = *indicesPtr;
			bool closedShapeFound = false;
			*tempShapeStartIndicesPtr = 0;
			for (int i = 0; i < indicesLength; i += 2)
			{
				if (closedShapeFound)
				{
					shapeStart = indicesPtr[i];
					tempShapeIsClosedArrayPtr[shapeCount] = true;
					tempShapeStartIndicesPtr[(IntPtr)(++shapeCount) * 4] = i >> 1;
					closedShapeFound = false;
				}
				else if (indicesPtr[i] != lastIndex)
				{
					tempShapeIsClosedArrayPtr[shapeCount] = false;
					tempShapeStartIndicesPtr[(IntPtr)(++shapeCount) * 4] = i >> 1;
					shapeStart = indicesPtr[i];
				}
				if (shapeStart == indicesPtr[i + 1])
				{
					closedShapeFound = true;
				}
				lastIndex = indicesPtr[i + 1];
			}
			tempShapeIsClosedArrayPtr[shapeCount++] = closedShapeFound;
			outShapeStartingEdge = new NativeArray<int>(shapeCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			outShapeIsClosedArray = new NativeArray<bool>(shapeCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			int* outShapeStartingEdgePtr = (int*)outShapeStartingEdge.m_Buffer;
			bool* outShapeIsClosedArrayPtr = (bool*)outShapeIsClosedArray.m_Buffer;
			for (int j = 0; j < shapeCount; j++)
			{
				outShapeStartingEdgePtr[j] = tempShapeStartIndicesPtr[j];
				outShapeIsClosedArrayPtr[j] = tempShapeIsClosedArrayPtr[j];
			}
			tempShapeStartIndices.Dispose();
			tempShapeIsClosedArray.Dispose();
			outEdges = new NativeArray<ShadowEdge>(numOfEdges, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			ShadowEdge* outEdgesPtr = (ShadowEdge*)outEdges.m_Buffer;
			for (int k = 0; k < numOfEdges; k++)
			{
				int indicesIndex = k << 1;
				int v0Index = indicesPtr[indicesIndex];
				int v1Index = indicesPtr[indicesIndex + 1];
				outEdgesPtr[k] = new ShadowEdge(v0Index, v1Index);
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000180FC File Offset: 0x000162FC
		[BurstCompile]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void GetVertexReferenceStats$BurstManaged(ref NativeArray<Vector3> vertices, ref NativeArray<ShadowEdge> edges, int vertexCount, out bool hasReusedVertices, out int newVertexCount, out NativeArray<ShadowUtility.RemappingInfo> remappingInfo)
		{
			int edgeCount = edges.Length;
			newVertexCount = 0;
			hasReusedVertices = false;
			remappingInfo = new NativeArray<ShadowUtility.RemappingInfo>(vertexCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			ShadowUtility.RemappingInfo* remappingInfoPtr = (ShadowUtility.RemappingInfo*)remappingInfo.GetUnsafePtr<ShadowUtility.RemappingInfo>();
			ShadowEdge* edgesPtr = (ShadowEdge*)edges.GetUnsafePtr<ShadowEdge>();
			for (int i = 0; i < vertexCount; i++)
			{
				remappingInfoPtr[i].Initialize();
			}
			for (int j = 0; j < edgeCount; j++)
			{
				int v0 = edgesPtr[j].v0;
				remappingInfoPtr[v0].count = remappingInfoPtr[v0].count + 1;
				if (remappingInfoPtr[v0].count > 1)
				{
					hasReusedVertices = true;
				}
				newVertexCount++;
			}
			for (int k = 0; k < edgeCount; k++)
			{
				int v = edgesPtr[k].v1;
				if (remappingInfoPtr[v].count == 0)
				{
					remappingInfoPtr[v].count = 1;
					newVertexCount++;
				}
			}
			int startPos = 0;
			for (int l = 0; l < vertexCount; l++)
			{
				if (remappingInfoPtr[l].count > 0)
				{
					remappingInfoPtr[l].index = startPos;
					startPos += remappingInfoPtr[l].count;
				}
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00018264 File Offset: 0x00016464
		[BurstCompile]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void CalculateEdgesFromTriangles$BurstManaged(ref NativeArray<Vector3> vertices, ref NativeArray<int> indices, bool duplicatesVertices, out NativeArray<Vector3> newVertices, out NativeArray<ShadowEdge> outEdges, out NativeArray<int> outShapeStartingEdge, out NativeArray<bool> outShapeIsClosedArray)
		{
			Clipper2D.Solution solution = default(Clipper2D.Solution);
			Clipper2D.ExecuteArguments executeArguments = new Clipper2D.ExecuteArguments(Clipper2D.InitOptions.ioDefault, Clipper2D.ClipType.ctUnion, Clipper2D.PolyFillType.pftEvenOdd, Clipper2D.PolyFillType.pftEvenOdd, false, false, false);
			int triangleCount = indices.Length / 3;
			NativeArray<Vector2> points = new NativeArray<Vector2>(indices.Length, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			NativeArray<int> pathSizes = new NativeArray<int>(triangleCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			NativeArray<Clipper2D.PathArguments> pathArguments = new NativeArray<Clipper2D.PathArguments>(triangleCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			Vector2* pointsPtr = (Vector2*)points.GetUnsafePtr<Vector2>();
			int* pathSizesPtr = (int*)pathSizes.GetUnsafePtr<int>();
			Clipper2D.PathArguments* pathArgumentsPtr = (Clipper2D.PathArguments*)pathArguments.GetUnsafePtr<Clipper2D.PathArguments>();
			Vector3* verticesPtr = (Vector3*)vertices.GetUnsafePtr<Vector3>();
			Clipper2D.PathArguments sharedPathArg = new Clipper2D.PathArguments(Clipper2D.PolyType.ptSubject, true);
			for (int i = 0; i < triangleCount; i++)
			{
				pathSizesPtr[i] = 3;
				pathArgumentsPtr[i] = sharedPathArg;
				int pointOffset = 3 * i;
				pointsPtr[pointOffset] = verticesPtr[indices[pointOffset]];
				pointsPtr[pointOffset + 1] = verticesPtr[indices[pointOffset + 1]];
				pointsPtr[pointOffset + 2] = verticesPtr[indices[pointOffset + 2]];
			}
			Clipper2D.Execute(ref solution, points, pathSizes, pathArguments, executeArguments, Allocator.Persistent, 65536, false);
			points.Dispose();
			pathSizes.Dispose();
			pathArguments.Dispose();
			int pointLen = solution.points.Length;
			int shapeCount = solution.pathSizes.Length;
			newVertices = new NativeArray<Vector3>(pointLen, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			outEdges = new NativeArray<ShadowEdge>(pointLen, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			outShapeStartingEdge = new NativeArray<int>(shapeCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			outShapeIsClosedArray = new NativeArray<bool>(shapeCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			int* solutionPathSizesPtr = (int*)solution.pathSizes.GetUnsafePtr<int>();
			Vector2* solutionPointsPtr = (Vector2*)solution.points.GetUnsafePtr<Vector2>();
			Vector3* newVerticesPtr = (Vector3*)newVertices.GetUnsafePtr<Vector3>();
			ShadowEdge* outEdgesPtr = (ShadowEdge*)outEdges.GetUnsafePtr<ShadowEdge>();
			int* outShapeStartingEdgePtr = (int*)outShapeStartingEdge.GetUnsafePtr<int>();
			bool* outShapeIsClosedArrayPtr = (bool*)outShapeIsClosedArray.GetUnsafePtr<bool>();
			int nextStart = 0;
			for (int shapeIndex = 0; shapeIndex < shapeCount; shapeIndex++)
			{
				int num = nextStart;
				int curPathSize = solutionPathSizesPtr[shapeIndex];
				outShapeStartingEdgePtr[shapeIndex] = nextStart;
				nextStart += curPathSize;
				int previousVertex = nextStart - 1;
				for (int pointIndex = num; pointIndex < nextStart; pointIndex++)
				{
					newVerticesPtr[pointIndex] = solutionPointsPtr[pointIndex];
					outEdgesPtr[pointIndex] = new ShadowEdge(previousVertex, pointIndex);
					previousVertex = pointIndex;
				}
				outShapeIsClosedArrayPtr[shapeIndex] = true;
			}
			solution.Dispose();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0001852C File Offset: 0x0001672C
		[BurstCompile]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ReverseWindingOrder$BurstManaged(ref NativeArray<int> inShapeStartingEdge, ref NativeArray<ShadowEdge> inOutSortedEdges)
		{
			for (int shapeIndex = 0; shapeIndex < inShapeStartingEdge.Length; shapeIndex++)
			{
				int startingIndex = inShapeStartingEdge[shapeIndex];
				if (startingIndex < 0)
				{
					return;
				}
				int endIndex = inOutSortedEdges.Length;
				if (shapeIndex + 1 < inShapeStartingEdge.Length && inShapeStartingEdge[shapeIndex + 1] > -1)
				{
					endIndex = inShapeStartingEdge[shapeIndex + 1];
				}
				int count = endIndex - startingIndex;
				for (int i = 0; i < count >> 1; i++)
				{
					int edgeAIndex = startingIndex + i;
					int edgeBIndex = startingIndex + count - 1 - i;
					ShadowEdge edgeA = inOutSortedEdges[edgeAIndex];
					ShadowEdge edgeB = inOutSortedEdges[edgeBIndex];
					edgeA.Reverse();
					edgeB.Reverse();
					inOutSortedEdges[edgeAIndex] = edgeB;
					inOutSortedEdges[edgeBIndex] = edgeA;
				}
				if ((count & 1) == 1)
				{
					int edgeAIndex2 = startingIndex + (count >> 1);
					ShadowEdge edgeA2 = inOutSortedEdges[edgeAIndex2];
					edgeA2.Reverse();
					inOutSortedEdges[edgeAIndex2] = edgeA2;
				}
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001860C File Offset: 0x0001680C
		[BurstCompile]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void ClipEdges$BurstManaged(ref NativeArray<Vector3> inVertices, ref NativeArray<ShadowEdge> inEdges, ref NativeArray<int> inShapeStartingEdge, ref NativeArray<bool> inShapeIsClosedArray, float contractEdge, out NativeArray<Vector3> outVertices, out NativeArray<ShadowEdge> outEdges, out NativeArray<int> outShapeStartingEdge)
		{
			Allocator k_ClippingAllocator = Allocator.Persistent;
			int k_Precision = 65536;
			int closedPathArrayCount;
			int closedPathCount;
			int openPathArrayCount;
			int openPathCount;
			ShadowUtility.GetPathInfo(inEdges, inShapeStartingEdge, inShapeIsClosedArray, out closedPathArrayCount, out closedPathCount, out openPathArrayCount, out openPathCount);
			NativeArray<Clipper2D.PathArguments> clipperPathArguments = new NativeArray<Clipper2D.PathArguments>(closedPathCount, k_ClippingAllocator, NativeArrayOptions.ClearMemory);
			NativeArray<int> closedPathSizes = new NativeArray<int>(closedPathCount, k_ClippingAllocator, NativeArrayOptions.ClearMemory);
			NativeArray<Vector2> closedPath = new NativeArray<Vector2>(closedPathArrayCount, k_ClippingAllocator, NativeArrayOptions.ClearMemory);
			NativeArray<int> openPathSizes = new NativeArray<int>(openPathCount, k_ClippingAllocator, NativeArrayOptions.ClearMemory);
			NativeArray<Vector2> openPath = new NativeArray<Vector2>(openPathArrayCount, k_ClippingAllocator, NativeArrayOptions.ClearMemory);
			Clipper2D.PathArguments* clipperPathArgumentsPtr = (Clipper2D.PathArguments*)clipperPathArguments.m_Buffer;
			int* closedPathSizesPtr = (int*)closedPathSizes.m_Buffer;
			Vector2* closedPathPtr = (Vector2*)closedPath.m_Buffer;
			int* openPathSizesPtr = (int*)openPathSizes.m_Buffer;
			Vector2* openPathPtr = (Vector2*)openPath.m_Buffer;
			int* inShapeStartingEdgePtr = (int*)inShapeStartingEdge.m_Buffer;
			bool* inShapeIsClosedArrayPtr = (bool*)inShapeIsClosedArray.m_Buffer;
			Vector3* inVerticesPtr = (Vector3*)inVertices.m_Buffer;
			ShadowEdge* inEdgesPtr = (ShadowEdge*)inEdges.m_Buffer;
			int inEdgesLength = inEdges.Length;
			Vector2 tmpVec2 = default(Vector2);
			Vector3 tmpVec3 = Vector3.zero;
			int closedPathArrayIndex = 0;
			int closedPathSizesIndex = 0;
			int openPathArrayIndex = 0;
			int openPathSizesIndex = 0;
			int totalPathCount = closedPathCount + openPathCount;
			for (int shapeStartIndex = 0; shapeStartIndex < totalPathCount; shapeStartIndex++)
			{
				int currentShapeStart = inShapeStartingEdgePtr[shapeStartIndex];
				int numberOfEdges = ((shapeStartIndex + 1 < totalPathCount) ? inShapeStartingEdgePtr[shapeStartIndex + 1] : inEdgesLength) - currentShapeStart;
				if (inShapeIsClosedArrayPtr[shapeStartIndex])
				{
					closedPathSizesPtr[closedPathSizesIndex] = numberOfEdges + 1;
					clipperPathArgumentsPtr[closedPathSizesIndex] = new Clipper2D.PathArguments(Clipper2D.PolyType.ptSubject, true);
					closedPathSizesIndex++;
					for (int i = 0; i < numberOfEdges; i++)
					{
						Vector3 vec3 = inVerticesPtr[inEdgesPtr[i + currentShapeStart].v0];
						tmpVec2.x = vec3.x;
						tmpVec2.y = vec3.y;
						closedPathPtr[(IntPtr)(closedPathArrayIndex++) * (IntPtr)sizeof(Vector2)] = tmpVec2;
					}
					closedPathPtr[(IntPtr)(closedPathArrayIndex++) * (IntPtr)sizeof(Vector2)] = inVerticesPtr[inEdgesPtr[numberOfEdges + currentShapeStart - 1].v1];
				}
				else
				{
					openPathSizesPtr[(IntPtr)(openPathSizesIndex++) * 4] = numberOfEdges + 1;
					for (int j = 0; j < numberOfEdges; j++)
					{
						Vector3 vec4 = inVerticesPtr[inEdgesPtr[j + currentShapeStart].v0];
						tmpVec2.x = vec4.x;
						tmpVec2.y = vec4.y;
						openPathPtr[(IntPtr)(openPathArrayIndex++) * (IntPtr)sizeof(Vector2)] = tmpVec2;
					}
					openPathPtr[(IntPtr)(openPathArrayIndex++) * (IntPtr)sizeof(Vector2)] = inVerticesPtr[inEdgesPtr[numberOfEdges + currentShapeStart - 1].v1];
				}
			}
			NativeArray<Vector2> clipperOffsetPath = closedPath;
			NativeArray<int> clipperOffsetPathSizes = closedPathSizes;
			Clipper2D.Solution clipperSolution = default(Clipper2D.Solution);
			if (closedPathSizes.Length > 1)
			{
				Clipper2D.ExecuteArguments executeArguments = new Clipper2D.ExecuteArguments
				{
					clipType = Clipper2D.ClipType.ctUnion,
					clipFillType = Clipper2D.PolyFillType.pftEvenOdd,
					subjFillType = Clipper2D.PolyFillType.pftEvenOdd,
					strictlySimple = false,
					preserveColinear = false
				};
				Clipper2D.Execute(ref clipperSolution, closedPath, closedPathSizes, clipperPathArguments, executeArguments, k_ClippingAllocator, k_Precision, true);
				clipperOffsetPath = clipperSolution.points;
				clipperOffsetPathSizes = clipperSolution.pathSizes;
			}
			ClipperOffset2D.Solution offsetSolution = default(ClipperOffset2D.Solution);
			NativeArray<ClipperOffset2D.PathArguments> offsetPathArguments = new NativeArray<ClipperOffset2D.PathArguments>(clipperOffsetPathSizes.Length, k_ClippingAllocator, NativeArrayOptions.ClearMemory);
			ClipperOffset2D.Execute(ref offsetSolution, clipperOffsetPath, clipperOffsetPathSizes, offsetPathArguments, k_ClippingAllocator, (double)(-(double)contractEdge), 2.0, 0.25, 0.0, (double)k_Precision, false);
			if (offsetSolution.pathSizes.Length > 0 || openPathCount > 0)
			{
				int vertexPos = 0;
				int solutionPathLens = offsetSolution.pathSizes.Length + openPathCount;
				outVertices = new NativeArray<Vector3>(offsetSolution.points.Length + openPathArrayCount, k_ClippingAllocator, NativeArrayOptions.ClearMemory);
				outEdges = new NativeArray<ShadowEdge>(offsetSolution.points.Length + openPathArrayCount, k_ClippingAllocator, NativeArrayOptions.ClearMemory);
				outShapeStartingEdge = new NativeArray<int>(solutionPathLens, k_ClippingAllocator, NativeArrayOptions.ClearMemory);
				Vector3* outVerticesPtr = (Vector3*)outVertices.m_Buffer;
				ShadowEdge* outEdgesPtr = (ShadowEdge*)outEdges.m_Buffer;
				int* outShapeStartingEdgePtr = (int*)outShapeStartingEdge.m_Buffer;
				Vector2* offsetSolutionPointsPtr = (Vector2*)offsetSolution.points.m_Buffer;
				int offsetSolutionPointsLength = offsetSolution.points.Length;
				int* offsetSolutionPathSizesPtr = (int*)offsetSolution.pathSizes.m_Buffer;
				int offsetSolutionPathSizesLength = offsetSolution.pathSizes.Length;
				for (int k = 0; k < offsetSolutionPointsLength; k++)
				{
					tmpVec3.x = offsetSolutionPointsPtr[k].x;
					tmpVec3.y = offsetSolutionPointsPtr[k].y;
					outVerticesPtr[(IntPtr)(vertexPos++) * (IntPtr)sizeof(Vector3)] = tmpVec3;
				}
				int start = 0;
				for (int pathSizeIndex = 0; pathSizeIndex < offsetSolutionPathSizesLength; pathSizeIndex++)
				{
					int pathSize = offsetSolutionPathSizesPtr[pathSizeIndex];
					int end = start + pathSize;
					outShapeStartingEdgePtr[pathSizeIndex] = start;
					for (int shapeIndex = 0; shapeIndex < pathSize; shapeIndex++)
					{
						ShadowEdge edge = new ShadowEdge(shapeIndex + start, (shapeIndex + 1) % pathSize + start);
						outEdgesPtr[shapeIndex + start] = edge;
					}
					start = end;
				}
				int pathStartIndex = offsetSolutionPathSizesLength;
				start = vertexPos;
				for (int l = 0; l < openPath.Length; l++)
				{
					tmpVec3.x = openPathPtr[l].x;
					tmpVec3.y = openPathPtr[l].y;
					outVerticesPtr[(IntPtr)(vertexPos++) * (IntPtr)sizeof(Vector3)] = tmpVec3;
				}
				for (int openPathIndex = 0; openPathIndex < openPathCount; openPathIndex++)
				{
					int pathSize2 = openPathSizesPtr[openPathIndex];
					int end2 = start + pathSize2;
					outShapeStartingEdgePtr[pathStartIndex + openPathIndex] = start;
					for (int shapeIndex2 = 0; shapeIndex2 < pathSize2 - 1; shapeIndex2++)
					{
						ShadowEdge edge2 = new ShadowEdge(shapeIndex2 + start, shapeIndex2 + 1);
						outEdgesPtr[shapeIndex2 + start] = edge2;
					}
					start = end2;
				}
			}
			else
			{
				outVertices = new NativeArray<Vector3>(0, k_ClippingAllocator, NativeArrayOptions.ClearMemory);
				outEdges = new NativeArray<ShadowEdge>(0, k_ClippingAllocator, NativeArrayOptions.ClearMemory);
				outShapeStartingEdge = new NativeArray<int>(0, k_ClippingAllocator, NativeArrayOptions.ClearMemory);
			}
			closedPathSizes.Dispose();
			closedPath.Dispose();
			openPathSizes.Dispose();
			openPath.Dispose();
			clipperPathArguments.Dispose();
			offsetPathArguments.Dispose();
			clipperSolution.Dispose();
			offsetSolution.Dispose();
		}

		// Token: 0x040002B6 RID: 694
		internal const int k_AdditionalVerticesPerEdge = 4;

		// Token: 0x040002B7 RID: 695
		internal const int k_VerticesPerTriangle = 3;

		// Token: 0x040002B8 RID: 696
		internal const int k_TrianglesPerEdge = 3;

		// Token: 0x040002B9 RID: 697
		internal const int k_MinimumEdges = 3;

		// Token: 0x040002BA RID: 698
		internal const int k_SafeSize = 40;

		// Token: 0x040002BB RID: 699
		private static VertexAttributeDescriptor[] m_VertexLayout = new VertexAttributeDescriptor[]
		{
			new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3, 0),
			new VertexAttributeDescriptor(VertexAttribute.Tangent, VertexAttributeFormat.Float32, 4, 0)
		};

		// Token: 0x02000080 RID: 128
		public enum ProjectionType
		{
			// Token: 0x040002BD RID: 701
			ProjectionNone = -1,
			// Token: 0x040002BE RID: 702
			ProjectionHard,
			// Token: 0x040002BF RID: 703
			ProjectionSoftLeft,
			// Token: 0x040002C0 RID: 704
			ProjectionSoftRight = 3
		}

		// Token: 0x02000081 RID: 129
		internal struct ShadowMeshVertex
		{
			// Token: 0x06000332 RID: 818 RVA: 0x00018C64 File Offset: 0x00016E64
			internal ShadowMeshVertex(ShadowUtility.ProjectionType inProjectionType, Vector2 inEdgePosition0, Vector2 inEdgePosition1)
			{
				this.position.x = inEdgePosition0.x;
				this.position.y = inEdgePosition0.y;
				this.position.z = 0f;
				this.tangent.x = (float)inProjectionType;
				this.tangent.y = 0f;
				this.tangent.z = inEdgePosition1.x;
				this.tangent.w = inEdgePosition1.y;
			}

			// Token: 0x040002C1 RID: 705
			internal Vector3 position;

			// Token: 0x040002C2 RID: 706
			internal Vector4 tangent;
		}

		// Token: 0x02000082 RID: 130
		internal struct RemappingInfo
		{
			// Token: 0x06000333 RID: 819 RVA: 0x00018CE2 File Offset: 0x00016EE2
			public void Initialize()
			{
				this.count = 0;
				this.index = -1;
				this.v0Offset = 0;
				this.v1Offset = 0;
			}

			// Token: 0x040002C3 RID: 707
			public int count;

			// Token: 0x040002C4 RID: 708
			public int index;

			// Token: 0x040002C5 RID: 709
			public int v0Offset;

			// Token: 0x040002C6 RID: 710
			public int v1Offset;
		}

		// Token: 0x02000083 RID: 131
		// (Invoke) Token: 0x06000335 RID: 821
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CalculateProjectionInfo_000002E7$PostfixBurstDelegate(ref NativeArray<Vector3> inVertices, ref NativeArray<ShadowEdge> inEdges, ref NativeArray<int> inShapeStartingEdge, ref NativeArray<bool> inShapeIsClosedArray, ref NativeArray<Vector2> outProjectionInfo);

		// Token: 0x02000084 RID: 132
		internal static class CalculateProjectionInfo_000002E7$BurstDirectCall
		{
			// Token: 0x06000338 RID: 824 RVA: 0x00018D00 File Offset: 0x00016F00
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (ShadowUtility.CalculateProjectionInfo_000002E7$BurstDirectCall.Pointer == 0)
				{
					ShadowUtility.CalculateProjectionInfo_000002E7$BurstDirectCall.Pointer = BurstCompiler.CompileFunctionPointer<ShadowUtility.CalculateProjectionInfo_000002E7$PostfixBurstDelegate>(new ShadowUtility.CalculateProjectionInfo_000002E7$PostfixBurstDelegate(ShadowUtility.CalculateProjectionInfo)).Value;
				}
				A_0 = ShadowUtility.CalculateProjectionInfo_000002E7$BurstDirectCall.Pointer;
			}

			// Token: 0x06000339 RID: 825 RVA: 0x00018D40 File Offset: 0x00016F40
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				ShadowUtility.CalculateProjectionInfo_000002E7$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x0600033A RID: 826 RVA: 0x00018D58 File Offset: 0x00016F58
			public static void Invoke(ref NativeArray<Vector3> inVertices, ref NativeArray<ShadowEdge> inEdges, ref NativeArray<int> inShapeStartingEdge, ref NativeArray<bool> inShapeIsClosedArray, ref NativeArray<Vector2> outProjectionInfo)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = ShadowUtility.CalculateProjectionInfo_000002E7$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Unity.Collections.NativeArray`1<UnityEngine.Vector3>&,Unity.Collections.NativeArray`1<UnityEngine.Rendering.Universal.ShadowEdge>&,Unity.Collections.NativeArray`1<System.Int32>&,Unity.Collections.NativeArray`1<System.Boolean>&,Unity.Collections.NativeArray`1<UnityEngine.Vector2>&), ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, ref outProjectionInfo, functionPointer);
						return;
					}
				}
				ShadowUtility.CalculateProjectionInfo$BurstManaged(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, ref outProjectionInfo);
			}

			// Token: 0x040002C7 RID: 711
			private static IntPtr Pointer;
		}

		// Token: 0x02000085 RID: 133
		// (Invoke) Token: 0x0600033C RID: 828
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CalculateVertices_000002E8$PostfixBurstDelegate(ref NativeArray<Vector3> inVertices, ref NativeArray<ShadowEdge> inEdges, ref NativeArray<Vector2> inEdgeOtherPoints, ref NativeArray<ShadowUtility.ShadowMeshVertex> outMeshVertices);

		// Token: 0x02000086 RID: 134
		internal static class CalculateVertices_000002E8$BurstDirectCall
		{
			// Token: 0x0600033F RID: 831 RVA: 0x00018D94 File Offset: 0x00016F94
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (ShadowUtility.CalculateVertices_000002E8$BurstDirectCall.Pointer == 0)
				{
					ShadowUtility.CalculateVertices_000002E8$BurstDirectCall.Pointer = BurstCompiler.CompileFunctionPointer<ShadowUtility.CalculateVertices_000002E8$PostfixBurstDelegate>(new ShadowUtility.CalculateVertices_000002E8$PostfixBurstDelegate(ShadowUtility.CalculateVertices)).Value;
				}
				A_0 = ShadowUtility.CalculateVertices_000002E8$BurstDirectCall.Pointer;
			}

			// Token: 0x06000340 RID: 832 RVA: 0x00018DD4 File Offset: 0x00016FD4
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				ShadowUtility.CalculateVertices_000002E8$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000341 RID: 833 RVA: 0x00018DEC File Offset: 0x00016FEC
			public static void Invoke(ref NativeArray<Vector3> inVertices, ref NativeArray<ShadowEdge> inEdges, ref NativeArray<Vector2> inEdgeOtherPoints, ref NativeArray<ShadowUtility.ShadowMeshVertex> outMeshVertices)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = ShadowUtility.CalculateVertices_000002E8$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Unity.Collections.NativeArray`1<UnityEngine.Vector3>&,Unity.Collections.NativeArray`1<UnityEngine.Rendering.Universal.ShadowEdge>&,Unity.Collections.NativeArray`1<UnityEngine.Vector2>&,Unity.Collections.NativeArray`1<UnityEngine.Rendering.Universal.ShadowUtility/ShadowMeshVertex>&), ref inVertices, ref inEdges, ref inEdgeOtherPoints, ref outMeshVertices, functionPointer);
						return;
					}
				}
				ShadowUtility.CalculateVertices$BurstManaged(ref inVertices, ref inEdges, ref inEdgeOtherPoints, ref outMeshVertices);
			}

			// Token: 0x040002C8 RID: 712
			private static IntPtr Pointer;
		}

		// Token: 0x02000087 RID: 135
		// (Invoke) Token: 0x06000343 RID: 835
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CalculateTriangles_000002E9$PostfixBurstDelegate(ref NativeArray<Vector3> inVertices, ref NativeArray<ShadowEdge> inEdges, ref NativeArray<int> inShapeStartingEdge, ref NativeArray<bool> inShapeIsClosedArray, ref NativeArray<int> outMeshIndices);

		// Token: 0x02000088 RID: 136
		internal static class CalculateTriangles_000002E9$BurstDirectCall
		{
			// Token: 0x06000346 RID: 838 RVA: 0x00018E24 File Offset: 0x00017024
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (ShadowUtility.CalculateTriangles_000002E9$BurstDirectCall.Pointer == 0)
				{
					ShadowUtility.CalculateTriangles_000002E9$BurstDirectCall.Pointer = BurstCompiler.CompileFunctionPointer<ShadowUtility.CalculateTriangles_000002E9$PostfixBurstDelegate>(new ShadowUtility.CalculateTriangles_000002E9$PostfixBurstDelegate(ShadowUtility.CalculateTriangles)).Value;
				}
				A_0 = ShadowUtility.CalculateTriangles_000002E9$BurstDirectCall.Pointer;
			}

			// Token: 0x06000347 RID: 839 RVA: 0x00018E64 File Offset: 0x00017064
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				ShadowUtility.CalculateTriangles_000002E9$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000348 RID: 840 RVA: 0x00018E7C File Offset: 0x0001707C
			public static void Invoke(ref NativeArray<Vector3> inVertices, ref NativeArray<ShadowEdge> inEdges, ref NativeArray<int> inShapeStartingEdge, ref NativeArray<bool> inShapeIsClosedArray, ref NativeArray<int> outMeshIndices)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = ShadowUtility.CalculateTriangles_000002E9$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Unity.Collections.NativeArray`1<UnityEngine.Vector3>&,Unity.Collections.NativeArray`1<UnityEngine.Rendering.Universal.ShadowEdge>&,Unity.Collections.NativeArray`1<System.Int32>&,Unity.Collections.NativeArray`1<System.Boolean>&,Unity.Collections.NativeArray`1<System.Int32>&), ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, ref outMeshIndices, functionPointer);
						return;
					}
				}
				ShadowUtility.CalculateTriangles$BurstManaged(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, ref outMeshIndices);
			}

			// Token: 0x040002C9 RID: 713
			private static IntPtr Pointer;
		}

		// Token: 0x02000089 RID: 137
		// (Invoke) Token: 0x0600034A RID: 842
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CalculateLocalBounds_000002EA$PostfixBurstDelegate(ref NativeArray<Vector3> inVertices, out Bounds retBounds);

		// Token: 0x0200008A RID: 138
		internal static class CalculateLocalBounds_000002EA$BurstDirectCall
		{
			// Token: 0x0600034D RID: 845 RVA: 0x00018EB8 File Offset: 0x000170B8
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (ShadowUtility.CalculateLocalBounds_000002EA$BurstDirectCall.Pointer == 0)
				{
					ShadowUtility.CalculateLocalBounds_000002EA$BurstDirectCall.Pointer = BurstCompiler.CompileFunctionPointer<ShadowUtility.CalculateLocalBounds_000002EA$PostfixBurstDelegate>(new ShadowUtility.CalculateLocalBounds_000002EA$PostfixBurstDelegate(ShadowUtility.CalculateLocalBounds)).Value;
				}
				A_0 = ShadowUtility.CalculateLocalBounds_000002EA$BurstDirectCall.Pointer;
			}

			// Token: 0x0600034E RID: 846 RVA: 0x00018EF8 File Offset: 0x000170F8
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				ShadowUtility.CalculateLocalBounds_000002EA$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x0600034F RID: 847 RVA: 0x00018F10 File Offset: 0x00017110
			public static void Invoke(ref NativeArray<Vector3> inVertices, out Bounds retBounds)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = ShadowUtility.CalculateLocalBounds_000002EA$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Unity.Collections.NativeArray`1<UnityEngine.Vector3>&,UnityEngine.Bounds&), ref inVertices, ref retBounds, functionPointer);
						return;
					}
				}
				ShadowUtility.CalculateLocalBounds$BurstManaged(ref inVertices, out retBounds);
			}

			// Token: 0x040002CA RID: 714
			private static IntPtr Pointer;
		}

		// Token: 0x0200008B RID: 139
		// (Invoke) Token: 0x06000351 RID: 849
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void GenerateInteriorMesh_000002EB$PostfixBurstDelegate(ref NativeArray<ShadowUtility.ShadowMeshVertex> inVertices, ref NativeArray<int> inIndices, ref NativeArray<ShadowEdge> inEdges, out NativeArray<ShadowUtility.ShadowMeshVertex> outVertices, out NativeArray<int> outIndices, out int outStartIndex, out int outIndexCount);

		// Token: 0x0200008C RID: 140
		internal static class GenerateInteriorMesh_000002EB$BurstDirectCall
		{
			// Token: 0x06000354 RID: 852 RVA: 0x00018F44 File Offset: 0x00017144
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (ShadowUtility.GenerateInteriorMesh_000002EB$BurstDirectCall.Pointer == 0)
				{
					ShadowUtility.GenerateInteriorMesh_000002EB$BurstDirectCall.Pointer = BurstCompiler.CompileFunctionPointer<ShadowUtility.GenerateInteriorMesh_000002EB$PostfixBurstDelegate>(new ShadowUtility.GenerateInteriorMesh_000002EB$PostfixBurstDelegate(ShadowUtility.GenerateInteriorMesh)).Value;
				}
				A_0 = ShadowUtility.GenerateInteriorMesh_000002EB$BurstDirectCall.Pointer;
			}

			// Token: 0x06000355 RID: 853 RVA: 0x00018F84 File Offset: 0x00017184
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				ShadowUtility.GenerateInteriorMesh_000002EB$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000356 RID: 854 RVA: 0x00018F9C File Offset: 0x0001719C
			public static void Invoke(ref NativeArray<ShadowUtility.ShadowMeshVertex> inVertices, ref NativeArray<int> inIndices, ref NativeArray<ShadowEdge> inEdges, out NativeArray<ShadowUtility.ShadowMeshVertex> outVertices, out NativeArray<int> outIndices, out int outStartIndex, out int outIndexCount)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = ShadowUtility.GenerateInteriorMesh_000002EB$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Unity.Collections.NativeArray`1<UnityEngine.Rendering.Universal.ShadowUtility/ShadowMeshVertex>&,Unity.Collections.NativeArray`1<System.Int32>&,Unity.Collections.NativeArray`1<UnityEngine.Rendering.Universal.ShadowEdge>&,Unity.Collections.NativeArray`1<UnityEngine.Rendering.Universal.ShadowUtility/ShadowMeshVertex>&,Unity.Collections.NativeArray`1<System.Int32>&,System.Int32&,System.Int32&), ref inVertices, ref inIndices, ref inEdges, ref outVertices, ref outIndices, ref outStartIndex, ref outIndexCount, functionPointer);
						return;
					}
				}
				ShadowUtility.GenerateInteriorMesh$BurstManaged(ref inVertices, ref inIndices, ref inEdges, out outVertices, out outIndices, out outStartIndex, out outIndexCount);
			}

			// Token: 0x040002CB RID: 715
			private static IntPtr Pointer;
		}

		// Token: 0x0200008D RID: 141
		// (Invoke) Token: 0x06000358 RID: 856
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CalculateEdgesFromLines_000002ED$PostfixBurstDelegate(ref NativeArray<int> indices, out NativeArray<ShadowEdge> outEdges, out NativeArray<int> outShapeStartingEdge, out NativeArray<bool> outShapeIsClosedArray);

		// Token: 0x0200008E RID: 142
		internal static class CalculateEdgesFromLines_000002ED$BurstDirectCall
		{
			// Token: 0x0600035B RID: 859 RVA: 0x00018FE0 File Offset: 0x000171E0
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (ShadowUtility.CalculateEdgesFromLines_000002ED$BurstDirectCall.Pointer == 0)
				{
					ShadowUtility.CalculateEdgesFromLines_000002ED$BurstDirectCall.Pointer = BurstCompiler.CompileFunctionPointer<ShadowUtility.CalculateEdgesFromLines_000002ED$PostfixBurstDelegate>(new ShadowUtility.CalculateEdgesFromLines_000002ED$PostfixBurstDelegate(ShadowUtility.CalculateEdgesFromLines)).Value;
				}
				A_0 = ShadowUtility.CalculateEdgesFromLines_000002ED$BurstDirectCall.Pointer;
			}

			// Token: 0x0600035C RID: 860 RVA: 0x00019020 File Offset: 0x00017220
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				ShadowUtility.CalculateEdgesFromLines_000002ED$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x0600035D RID: 861 RVA: 0x00019038 File Offset: 0x00017238
			public static void Invoke(ref NativeArray<int> indices, out NativeArray<ShadowEdge> outEdges, out NativeArray<int> outShapeStartingEdge, out NativeArray<bool> outShapeIsClosedArray)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = ShadowUtility.CalculateEdgesFromLines_000002ED$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Unity.Collections.NativeArray`1<System.Int32>&,Unity.Collections.NativeArray`1<UnityEngine.Rendering.Universal.ShadowEdge>&,Unity.Collections.NativeArray`1<System.Int32>&,Unity.Collections.NativeArray`1<System.Boolean>&), ref indices, ref outEdges, ref outShapeStartingEdge, ref outShapeIsClosedArray, functionPointer);
						return;
					}
				}
				ShadowUtility.CalculateEdgesFromLines$BurstManaged(ref indices, out outEdges, out outShapeStartingEdge, out outShapeIsClosedArray);
			}

			// Token: 0x040002CC RID: 716
			private static IntPtr Pointer;
		}

		// Token: 0x0200008F RID: 143
		// (Invoke) Token: 0x0600035F RID: 863
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void GetVertexReferenceStats_000002EE$PostfixBurstDelegate(ref NativeArray<Vector3> vertices, ref NativeArray<ShadowEdge> edges, int vertexCount, out bool hasReusedVertices, out int newVertexCount, out NativeArray<ShadowUtility.RemappingInfo> remappingInfo);

		// Token: 0x02000090 RID: 144
		internal static class GetVertexReferenceStats_000002EE$BurstDirectCall
		{
			// Token: 0x06000362 RID: 866 RVA: 0x00019070 File Offset: 0x00017270
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (ShadowUtility.GetVertexReferenceStats_000002EE$BurstDirectCall.Pointer == 0)
				{
					ShadowUtility.GetVertexReferenceStats_000002EE$BurstDirectCall.Pointer = BurstCompiler.CompileFunctionPointer<ShadowUtility.GetVertexReferenceStats_000002EE$PostfixBurstDelegate>(new ShadowUtility.GetVertexReferenceStats_000002EE$PostfixBurstDelegate(ShadowUtility.GetVertexReferenceStats)).Value;
				}
				A_0 = ShadowUtility.GetVertexReferenceStats_000002EE$BurstDirectCall.Pointer;
			}

			// Token: 0x06000363 RID: 867 RVA: 0x000190B0 File Offset: 0x000172B0
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				ShadowUtility.GetVertexReferenceStats_000002EE$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000364 RID: 868 RVA: 0x000190C8 File Offset: 0x000172C8
			public static void Invoke(ref NativeArray<Vector3> vertices, ref NativeArray<ShadowEdge> edges, int vertexCount, out bool hasReusedVertices, out int newVertexCount, out NativeArray<ShadowUtility.RemappingInfo> remappingInfo)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = ShadowUtility.GetVertexReferenceStats_000002EE$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Unity.Collections.NativeArray`1<UnityEngine.Vector3>&,Unity.Collections.NativeArray`1<UnityEngine.Rendering.Universal.ShadowEdge>&,System.Int32,System.Boolean&,System.Int32&,Unity.Collections.NativeArray`1<UnityEngine.Rendering.Universal.ShadowUtility/RemappingInfo>&), ref vertices, ref edges, vertexCount, ref hasReusedVertices, ref newVertexCount, ref remappingInfo, functionPointer);
						return;
					}
				}
				ShadowUtility.GetVertexReferenceStats$BurstManaged(ref vertices, ref edges, vertexCount, out hasReusedVertices, out newVertexCount, out remappingInfo);
			}

			// Token: 0x040002CD RID: 717
			private static IntPtr Pointer;
		}

		// Token: 0x02000091 RID: 145
		// (Invoke) Token: 0x06000366 RID: 870
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CalculateEdgesFromTriangles_000002F0$PostfixBurstDelegate(ref NativeArray<Vector3> vertices, ref NativeArray<int> indices, bool duplicatesVertices, out NativeArray<Vector3> newVertices, out NativeArray<ShadowEdge> outEdges, out NativeArray<int> outShapeStartingEdge, out NativeArray<bool> outShapeIsClosedArray);

		// Token: 0x02000092 RID: 146
		internal static class CalculateEdgesFromTriangles_000002F0$BurstDirectCall
		{
			// Token: 0x06000369 RID: 873 RVA: 0x00019108 File Offset: 0x00017308
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (ShadowUtility.CalculateEdgesFromTriangles_000002F0$BurstDirectCall.Pointer == 0)
				{
					ShadowUtility.CalculateEdgesFromTriangles_000002F0$BurstDirectCall.Pointer = BurstCompiler.CompileFunctionPointer<ShadowUtility.CalculateEdgesFromTriangles_000002F0$PostfixBurstDelegate>(new ShadowUtility.CalculateEdgesFromTriangles_000002F0$PostfixBurstDelegate(ShadowUtility.CalculateEdgesFromTriangles)).Value;
				}
				A_0 = ShadowUtility.CalculateEdgesFromTriangles_000002F0$BurstDirectCall.Pointer;
			}

			// Token: 0x0600036A RID: 874 RVA: 0x00019148 File Offset: 0x00017348
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				ShadowUtility.CalculateEdgesFromTriangles_000002F0$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x0600036B RID: 875 RVA: 0x00019160 File Offset: 0x00017360
			public static void Invoke(ref NativeArray<Vector3> vertices, ref NativeArray<int> indices, bool duplicatesVertices, out NativeArray<Vector3> newVertices, out NativeArray<ShadowEdge> outEdges, out NativeArray<int> outShapeStartingEdge, out NativeArray<bool> outShapeIsClosedArray)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = ShadowUtility.CalculateEdgesFromTriangles_000002F0$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Unity.Collections.NativeArray`1<UnityEngine.Vector3>&,Unity.Collections.NativeArray`1<System.Int32>&,System.Boolean,Unity.Collections.NativeArray`1<UnityEngine.Vector3>&,Unity.Collections.NativeArray`1<UnityEngine.Rendering.Universal.ShadowEdge>&,Unity.Collections.NativeArray`1<System.Int32>&,Unity.Collections.NativeArray`1<System.Boolean>&), ref vertices, ref indices, duplicatesVertices, ref newVertices, ref outEdges, ref outShapeStartingEdge, ref outShapeIsClosedArray, functionPointer);
						return;
					}
				}
				ShadowUtility.CalculateEdgesFromTriangles$BurstManaged(ref vertices, ref indices, duplicatesVertices, out newVertices, out outEdges, out outShapeStartingEdge, out outShapeIsClosedArray);
			}

			// Token: 0x040002CE RID: 718
			private static IntPtr Pointer;
		}

		// Token: 0x02000093 RID: 147
		// (Invoke) Token: 0x0600036D RID: 877
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ReverseWindingOrder_000002F1$PostfixBurstDelegate(ref NativeArray<int> inShapeStartingEdge, ref NativeArray<ShadowEdge> inOutSortedEdges);

		// Token: 0x02000094 RID: 148
		internal static class ReverseWindingOrder_000002F1$BurstDirectCall
		{
			// Token: 0x06000370 RID: 880 RVA: 0x000191A4 File Offset: 0x000173A4
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (ShadowUtility.ReverseWindingOrder_000002F1$BurstDirectCall.Pointer == 0)
				{
					ShadowUtility.ReverseWindingOrder_000002F1$BurstDirectCall.Pointer = BurstCompiler.CompileFunctionPointer<ShadowUtility.ReverseWindingOrder_000002F1$PostfixBurstDelegate>(new ShadowUtility.ReverseWindingOrder_000002F1$PostfixBurstDelegate(ShadowUtility.ReverseWindingOrder)).Value;
				}
				A_0 = ShadowUtility.ReverseWindingOrder_000002F1$BurstDirectCall.Pointer;
			}

			// Token: 0x06000371 RID: 881 RVA: 0x000191E4 File Offset: 0x000173E4
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				ShadowUtility.ReverseWindingOrder_000002F1$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000372 RID: 882 RVA: 0x000191FC File Offset: 0x000173FC
			public static void Invoke(ref NativeArray<int> inShapeStartingEdge, ref NativeArray<ShadowEdge> inOutSortedEdges)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = ShadowUtility.ReverseWindingOrder_000002F1$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Unity.Collections.NativeArray`1<System.Int32>&,Unity.Collections.NativeArray`1<UnityEngine.Rendering.Universal.ShadowEdge>&), ref inShapeStartingEdge, ref inOutSortedEdges, functionPointer);
						return;
					}
				}
				ShadowUtility.ReverseWindingOrder$BurstManaged(ref inShapeStartingEdge, ref inOutSortedEdges);
			}

			// Token: 0x040002CF RID: 719
			private static IntPtr Pointer;
		}

		// Token: 0x02000095 RID: 149
		// (Invoke) Token: 0x06000374 RID: 884
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ClipEdges_000002F4$PostfixBurstDelegate(ref NativeArray<Vector3> inVertices, ref NativeArray<ShadowEdge> inEdges, ref NativeArray<int> inShapeStartingEdge, ref NativeArray<bool> inShapeIsClosedArray, float contractEdge, out NativeArray<Vector3> outVertices, out NativeArray<ShadowEdge> outEdges, out NativeArray<int> outShapeStartingEdge);

		// Token: 0x02000096 RID: 150
		internal static class ClipEdges_000002F4$BurstDirectCall
		{
			// Token: 0x06000377 RID: 887 RVA: 0x00019230 File Offset: 0x00017430
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (ShadowUtility.ClipEdges_000002F4$BurstDirectCall.Pointer == 0)
				{
					ShadowUtility.ClipEdges_000002F4$BurstDirectCall.Pointer = BurstCompiler.CompileFunctionPointer<ShadowUtility.ClipEdges_000002F4$PostfixBurstDelegate>(new ShadowUtility.ClipEdges_000002F4$PostfixBurstDelegate(ShadowUtility.ClipEdges)).Value;
				}
				A_0 = ShadowUtility.ClipEdges_000002F4$BurstDirectCall.Pointer;
			}

			// Token: 0x06000378 RID: 888 RVA: 0x00019270 File Offset: 0x00017470
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				ShadowUtility.ClipEdges_000002F4$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000379 RID: 889 RVA: 0x00019288 File Offset: 0x00017488
			public static void Invoke(ref NativeArray<Vector3> inVertices, ref NativeArray<ShadowEdge> inEdges, ref NativeArray<int> inShapeStartingEdge, ref NativeArray<bool> inShapeIsClosedArray, float contractEdge, out NativeArray<Vector3> outVertices, out NativeArray<ShadowEdge> outEdges, out NativeArray<int> outShapeStartingEdge)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = ShadowUtility.ClipEdges_000002F4$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Unity.Collections.NativeArray`1<UnityEngine.Vector3>&,Unity.Collections.NativeArray`1<UnityEngine.Rendering.Universal.ShadowEdge>&,Unity.Collections.NativeArray`1<System.Int32>&,Unity.Collections.NativeArray`1<System.Boolean>&,System.Single,Unity.Collections.NativeArray`1<UnityEngine.Vector3>&,Unity.Collections.NativeArray`1<UnityEngine.Rendering.Universal.ShadowEdge>&,Unity.Collections.NativeArray`1<System.Int32>&), ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, contractEdge, ref outVertices, ref outEdges, ref outShapeStartingEdge, functionPointer);
						return;
					}
				}
				ShadowUtility.ClipEdges$BurstManaged(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, contractEdge, out outVertices, out outEdges, out outShapeStartingEdge);
			}

			// Token: 0x040002D0 RID: 720
			private static IntPtr Pointer;
		}
	}
}
