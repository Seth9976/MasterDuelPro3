using System;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000079 RID: 121
	[Serializable]
	internal class ShadowMesh2D : ShadowShape2D
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0001689B File Offset: 0x00014A9B
		public Mesh mesh
		{
			get
			{
				return this.m_Mesh;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x000168A3 File Offset: 0x00014AA3
		public BoundingSphere boundingSphere
		{
			get
			{
				return this.m_BoundingSphere;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x000168AB File Offset: 0x00014AAB
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x000168B3 File Offset: 0x00014AB3
		public ShadowMesh2D.EdgeProcessing edgeProcessing
		{
			get
			{
				return this.m_EdgeProcessing;
			}
			set
			{
				this.m_EdgeProcessing = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x000168BC File Offset: 0x00014ABC
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x000168C4 File Offset: 0x00014AC4
		public float trimEdge
		{
			get
			{
				return this.m_TrimEdge;
			}
			set
			{
				this.m_TrimEdge = value;
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000168D0 File Offset: 0x00014AD0
		internal static void DuplicateShadowMesh(Mesh source, out Mesh dest)
		{
			dest = new Mesh();
			dest.Clear();
			if (source != null)
			{
				dest.vertices = source.vertices;
				dest.tangents = source.tangents;
				dest.triangles = source.triangles;
				dest.bounds = source.bounds;
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00016928 File Offset: 0x00014B28
		internal void CopyFrom(ShadowMesh2D source)
		{
			ShadowMesh2D.DuplicateShadowMesh(source.m_Mesh, out this.m_Mesh);
			this.m_TrimEdge = source.trimEdge;
			this.m_LocalBounds = source.m_LocalBounds;
			this.m_EdgeProcessing = source.edgeProcessing;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00016960 File Offset: 0x00014B60
		internal void AddCircle(Vector3 center, float r, NativeArray<Vector3> generatedVertices, NativeArray<int> generatedIndices, bool reverseWindingOrder, ref int vertexWritePos, ref int indexWritePos)
		{
			float direction = (float)(reverseWindingOrder ? 1 : (-1));
			float segments = 16f;
			int startWritePos = vertexWritePos;
			int i = 0;
			while ((float)i < segments)
			{
				float angle = direction * (6.2831855f * (float)i / segments);
				float x = r * Mathf.Cos(angle) + center.x;
				float y = r * Mathf.Sin(angle) + center.y;
				int num = indexWritePos;
				indexWritePos = num + 1;
				generatedIndices[num] = vertexWritePos;
				num = indexWritePos;
				indexWritePos = num + 1;
				generatedIndices[num] = (((float)(i + 1) < segments) ? (vertexWritePos + 1) : startWritePos);
				num = vertexWritePos;
				vertexWritePos = num + 1;
				generatedVertices[num] = new Vector3(x, y, 0f);
				i++;
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00016A2C File Offset: 0x00014C2C
		internal void AddCapsuleCap(Vector3 center, float r, Vector3 otherCenter, NativeArray<Vector3> generatedVertices, NativeArray<int> generatedIndices, bool reverseWindingOrder, ref int vertexWritePos, ref int indexWritePos)
		{
			float segments = 8f;
			Vector3 normalized = (otherCenter - center).normalized;
			float absCenterAngle = Mathf.Acos(Vector3.Dot(normalized, new Vector3(1f, 0f, 0f)));
			float angleSign = ((Vector3.Dot(normalized, new Vector3(0f, 1f, 0f)) < 0f) ? (-1f) : 1f);
			float centerAngle = absCenterAngle * angleSign;
			float startAngle;
			float endAngle;
			if (reverseWindingOrder)
			{
				float HalfPI = 1.5707964f;
				startAngle = centerAngle + HalfPI;
				endAngle = startAngle + 3.1415927f;
			}
			else
			{
				float ThreeHalfsPI = 4.712389f;
				startAngle = centerAngle + ThreeHalfsPI;
				endAngle = startAngle - 3.1415927f;
			}
			float deltaAngle = endAngle - startAngle;
			int i = 0;
			float angle;
			int num;
			while ((float)i < segments)
			{
				angle = deltaAngle * (float)i / segments + startAngle;
				float x = r * Mathf.Cos(angle) + center.x;
				float y = r * Mathf.Sin(angle) + center.y;
				num = indexWritePos;
				indexWritePos = num + 1;
				generatedIndices[num] = vertexWritePos;
				num = indexWritePos;
				indexWritePos = num + 1;
				generatedIndices[num] = vertexWritePos + 1;
				num = vertexWritePos;
				vertexWritePos = num + 1;
				generatedVertices[num] = new Vector3(x, y, 0f);
				i++;
			}
			angle = deltaAngle + startAngle;
			num = vertexWritePos;
			vertexWritePos = num + 1;
			generatedVertices[num] = new Vector3(r * Mathf.Cos(angle) + center.x, r * Mathf.Sin(angle) + center.y, 0f);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00016BBC File Offset: 0x00014DBC
		internal void AddCapsule(Vector3 pt0, Vector3 pt1, float r0, float r1, NativeArray<Vector3> generatedVertices, NativeArray<int> generatedIndices, bool reverseWindingOrder, ref int vertexWritePos, ref int indexWritePos)
		{
			Vector3 delta = (pt1 - pt0).normalized;
			new Vector3(delta.y, -delta.x, 0f);
			new Vector3(-delta.y, delta.x, 0f);
			if (pt1.x < pt0.x)
			{
				Vector3 vector = pt0;
				pt0 = pt1;
				pt1 = vector;
			}
			int circle0Start = vertexWritePos;
			this.AddCapsuleCap(pt0, r0, pt1, generatedVertices, generatedIndices, reverseWindingOrder, ref vertexWritePos, ref indexWritePos);
			int num = indexWritePos;
			indexWritePos = num + 1;
			generatedIndices[num] = vertexWritePos - 1;
			num = indexWritePos;
			indexWritePos = num + 1;
			generatedIndices[num] = vertexWritePos;
			this.AddCapsuleCap(pt1, r1, pt0, generatedVertices, generatedIndices, reverseWindingOrder, ref vertexWritePos, ref indexWritePos);
			num = indexWritePos;
			indexWritePos = num + 1;
			generatedIndices[num] = vertexWritePos - 1;
			num = indexWritePos;
			indexWritePos = num + 1;
			generatedIndices[num] = circle0Start;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00016CA0 File Offset: 0x00014EA0
		internal int AddShape(NativeArray<Vector3> vertices, NativeArray<int> indices, int indicesProcessed, NativeArray<Vector3> generatedVertices, NativeArray<int> generatedIndices, ref int vertexWritePos, ref int indexWritePos)
		{
			int indexToProcess = indicesProcessed;
			int prevIndex = indices[indexToProcess];
			int startIndex = indices[indexToProcess];
			int startWriteIndex = vertexWritePos;
			int num = vertexWritePos;
			vertexWritePos = num + 1;
			generatedVertices[num] = vertices[prevIndex];
			bool continueProcessing = true;
			while (indexToProcess < indices.Length && continueProcessing)
			{
				int index0 = indices[indexToProcess++];
				int index = indices[indexToProcess++];
				num = indexWritePos;
				indexWritePos = num + 1;
				generatedIndices[num] = vertexWritePos - 1;
				if (index != startIndex)
				{
					num = indexWritePos;
					indexWritePos = num + 1;
					generatedIndices[num] = vertexWritePos;
					num = vertexWritePos;
					vertexWritePos = num + 1;
					generatedVertices[num] = vertices[index];
					continueProcessing = index0 == prevIndex;
				}
				else
				{
					num = indexWritePos;
					indexWritePos = num + 1;
					generatedIndices[num] = startWriteIndex;
					continueProcessing = false;
				}
				prevIndex = index;
			}
			return indexToProcess;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00016D94 File Offset: 0x00014F94
		public override void SetShape(NativeArray<Vector3> vertices, NativeArray<int> indices, NativeArray<float> radii, Matrix4x4 transform, ShadowShape2D.WindingOrder windingOrder = ShadowShape2D.WindingOrder.Clockwise, bool allowTriming = true, bool createInteriorGeometry = false)
		{
			if (this.m_TrimEdge == -1f)
			{
				this.m_TrimEdge = this.m_InitialTrim;
			}
			if (this.m_Mesh == null)
			{
				this.m_Mesh = new Mesh();
			}
			if (indices.Length == 0)
			{
				this.m_Mesh.Clear();
				return;
			}
			bool reverseWindingOrder = windingOrder == ShadowShape2D.WindingOrder.CounterClockwise;
			int circleCount = 0;
			int capsuleCount = 0;
			for (int i = 0; i < indices.Length; i += 2)
			{
				int index0 = indices[i];
				int index = indices[i + 1];
				if (radii[index0] > 0f || radii[index] > 0f)
				{
					if (index0 == index)
					{
						circleCount++;
					}
					else
					{
						capsuleCount++;
					}
				}
			}
			int capsuleStraightSegments = capsuleCount * 2;
			int capsuleCapSegments = capsuleCount * 8;
			int circleSegments = circleCount * 2 * 8;
			int lineCount = (indices.Length >> 1) - (capsuleCount + circleCount);
			int indexCount = 2 * (lineCount + capsuleStraightSegments + 2 * capsuleCapSegments + circleSegments);
			int vertexCount = indexCount;
			NativeArray<Vector3> generatedVertices = new NativeArray<Vector3>(vertexCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<int> generatedIndices = new NativeArray<int>(indexCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
			int vertexWritePos = 0;
			int indexWritePos = 0;
			int indicesProcessed = 0;
			while (indicesProcessed < indices.Length)
			{
				int v0 = indices[indicesProcessed];
				int v = indices[indicesProcessed + 1];
				float r0 = radii[v0];
				float r = radii[v];
				if (radii[v0] > 0f || radii[v] > 0f)
				{
					Vector3 pt0 = vertices[v0];
					Vector3 pt = vertices[v];
					if (vertices[v0].x == vertices[v].x && vertices[v0].y == vertices[v].y)
					{
						this.AddCircle(pt0, r0, generatedVertices, generatedIndices, reverseWindingOrder, ref vertexWritePos, ref indexWritePos);
					}
					else
					{
						this.AddCapsule(pt0, pt, r0, r, generatedVertices, generatedIndices, reverseWindingOrder, ref vertexWritePos, ref indexWritePos);
					}
					indicesProcessed += 2;
				}
				else
				{
					indicesProcessed = this.AddShape(vertices, indices, indicesProcessed, generatedVertices, generatedIndices, ref vertexWritePos, ref indexWritePos);
				}
			}
			for (int j = 0; j < generatedVertices.Length; j++)
			{
				generatedVertices[j] = transform.MultiplyPoint(generatedVertices[j]);
			}
			NativeArray<ShadowEdge> calculatedEdges;
			NativeArray<int> calculatedStartingEdges;
			NativeArray<bool> calculatedIsClosedArray;
			ShadowUtility.CalculateEdgesFromLines(ref generatedIndices, out calculatedEdges, out calculatedStartingEdges, out calculatedIsClosedArray);
			if (reverseWindingOrder)
			{
				ShadowUtility.ReverseWindingOrder(ref calculatedStartingEdges, ref calculatedEdges);
			}
			if (this.m_EdgeProcessing == ShadowMesh2D.EdgeProcessing.Clipping)
			{
				NativeArray<Vector3> clippedVertices;
				NativeArray<ShadowEdge> clippedEdges;
				NativeArray<int> clippedStartingIndices;
				ShadowUtility.ClipEdges(ref generatedVertices, ref calculatedEdges, ref calculatedStartingEdges, ref calculatedIsClosedArray, this.trimEdge, out clippedVertices, out clippedEdges, out clippedStartingIndices);
				if (clippedStartingIndices.Length > 0)
				{
					this.m_LocalBounds = ShadowUtility.GenerateShadowMesh(this.m_Mesh, clippedVertices, clippedEdges, clippedStartingIndices, calculatedIsClosedArray, true, createInteriorGeometry, ShadowShape2D.OutlineTopology.Lines);
				}
				else
				{
					this.m_LocalBounds = default(Bounds);
					this.m_Mesh.Clear();
				}
				clippedVertices.Dispose();
				clippedEdges.Dispose();
				clippedStartingIndices.Dispose();
			}
			else
			{
				this.m_LocalBounds = ShadowUtility.GenerateShadowMesh(this.m_Mesh, generatedVertices, calculatedEdges, calculatedStartingEdges, calculatedIsClosedArray, true, createInteriorGeometry, ShadowShape2D.OutlineTopology.Lines);
			}
			generatedVertices.Dispose();
			generatedIndices.Dispose();
			calculatedEdges.Dispose();
			calculatedIsClosedArray.Dispose();
			calculatedStartingEdges.Dispose();
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000170A4 File Offset: 0x000152A4
		private bool AreDegenerateVertices(NativeArray<Vector3> vertices)
		{
			if (vertices.Length == 0)
			{
				return true;
			}
			int prevIndex = vertices.Length - 1;
			for (int i = 0; i < vertices.Length; i++)
			{
				if (vertices[prevIndex].x != vertices[i].x || vertices[prevIndex].y != vertices[i].y)
				{
					return false;
				}
				prevIndex = i;
			}
			return true;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00017118 File Offset: 0x00015318
		public override void SetShape(NativeArray<Vector3> vertices, NativeArray<int> indices, ShadowShape2D.OutlineTopology outlineTopology, ShadowShape2D.WindingOrder windingOrder = ShadowShape2D.WindingOrder.Clockwise, bool allowTrimming = true, bool createInteriorGeometry = false)
		{
			if (this.AreDegenerateVertices(vertices))
			{
				return;
			}
			if (this.m_TrimEdge == -1f)
			{
				this.m_TrimEdge = this.m_InitialTrim;
			}
			bool disposeVertices = false;
			if (this.m_Mesh == null)
			{
				this.m_Mesh = new Mesh();
			}
			if (indices.Length == 0)
			{
				this.m_Mesh.Clear();
				return;
			}
			NativeArray<ShadowEdge> edges;
			NativeArray<int> shapeStartingIndices;
			NativeArray<bool> shapeIsClosedArray;
			if (outlineTopology == ShadowShape2D.OutlineTopology.Triangles)
			{
				NativeArray<Vector3> newVertices;
				ShadowUtility.CalculateEdgesFromTriangles(ref vertices, ref indices, true, out newVertices, out edges, out shapeStartingIndices, out shapeIsClosedArray);
				disposeVertices = true;
				vertices = newVertices;
			}
			else
			{
				ShadowUtility.CalculateEdgesFromLines(ref indices, out edges, out shapeStartingIndices, out shapeIsClosedArray);
			}
			if (windingOrder == ShadowShape2D.WindingOrder.CounterClockwise)
			{
				ShadowUtility.ReverseWindingOrder(ref shapeStartingIndices, ref edges);
			}
			if (this.m_EdgeProcessing == ShadowMesh2D.EdgeProcessing.Clipping && allowTrimming)
			{
				NativeArray<Vector3> clippedVertices;
				NativeArray<ShadowEdge> clippedEdges;
				NativeArray<int> clippedStartingIndices;
				ShadowUtility.ClipEdges(ref vertices, ref edges, ref shapeStartingIndices, ref shapeIsClosedArray, this.trimEdge, out clippedVertices, out clippedEdges, out clippedStartingIndices);
				this.m_LocalBounds = ShadowUtility.GenerateShadowMesh(this.m_Mesh, clippedVertices, clippedEdges, clippedStartingIndices, shapeIsClosedArray, allowTrimming, createInteriorGeometry, outlineTopology);
				clippedVertices.Dispose();
				clippedEdges.Dispose();
				clippedStartingIndices.Dispose();
			}
			else
			{
				this.m_LocalBounds = ShadowUtility.GenerateShadowMesh(this.m_Mesh, vertices, edges, shapeStartingIndices, shapeIsClosedArray, allowTrimming, createInteriorGeometry, outlineTopology);
			}
			if (disposeVertices)
			{
				vertices.Dispose();
			}
			edges.Dispose();
			shapeStartingIndices.Dispose();
			shapeIsClosedArray.Dispose();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00017245 File Offset: 0x00015445
		public void SetShapeWithLines(NativeArray<Vector3> vertices, NativeArray<int> indices, bool allowTrimming)
		{
			this.SetShape(vertices, indices, ShadowShape2D.OutlineTopology.Lines, ShadowShape2D.WindingOrder.Clockwise, allowTrimming, false);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00017253 File Offset: 0x00015453
		public override void SetFlip(bool flipX, bool flipY)
		{
			this.m_FlipX = flipX;
			this.m_FlipY = flipY;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00017263 File Offset: 0x00015463
		public override void GetFlip(out bool flipX, out bool flipY)
		{
			flipX = this.m_FlipX;
			flipY = this.m_FlipY;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00017275 File Offset: 0x00015475
		public override void SetDefaultTrim(float trim)
		{
			this.m_InitialTrim = trim;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00017280 File Offset: 0x00015480
		public void UpdateBoundingSphere(Transform transform)
		{
			Vector3 maxBound = transform.TransformPoint(this.m_LocalBounds.max);
			Vector3 minBound = transform.TransformPoint(this.m_LocalBounds.min);
			Vector3 center = 0.5f * (maxBound + minBound);
			float radius = Vector3.Magnitude(maxBound - center);
			this.m_BoundingSphere = new BoundingSphere(center, radius);
		}

		// Token: 0x040002A3 RID: 675
		internal const int k_CapsuleCapSegments = 8;

		// Token: 0x040002A4 RID: 676
		internal const float k_TrimEdgeUninitialized = -1f;

		// Token: 0x040002A5 RID: 677
		[SerializeField]
		private Mesh m_Mesh;

		// Token: 0x040002A6 RID: 678
		[SerializeField]
		private Bounds m_LocalBounds;

		// Token: 0x040002A7 RID: 679
		[SerializeField]
		private ShadowMesh2D.EdgeProcessing m_EdgeProcessing = ShadowMesh2D.EdgeProcessing.Clipping;

		// Token: 0x040002A8 RID: 680
		[SerializeField]
		private float m_TrimEdge = -1f;

		// Token: 0x040002A9 RID: 681
		[SerializeField]
		private bool m_FlipX;

		// Token: 0x040002AA RID: 682
		[SerializeField]
		private bool m_FlipY;

		// Token: 0x040002AB RID: 683
		[SerializeField]
		private float m_InitialTrim;

		// Token: 0x040002AC RID: 684
		internal BoundingSphere m_BoundingSphere;

		// Token: 0x0200007A RID: 122
		public enum EdgeProcessing
		{
			// Token: 0x040002AE RID: 686
			None,
			// Token: 0x040002AF RID: 687
			Clipping
		}
	}
}
