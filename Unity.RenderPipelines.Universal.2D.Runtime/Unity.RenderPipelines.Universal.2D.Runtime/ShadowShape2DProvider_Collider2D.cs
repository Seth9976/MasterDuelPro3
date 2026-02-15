using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000075 RID: 117
	[Serializable]
	internal class ShadowShape2DProvider_Collider2D : ShadowShape2DProvider
	{
		// Token: 0x060002DF RID: 735 RVA: 0x00015C08 File Offset: 0x00013E08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool CompareApproximately(ref Bounds a, ref Bounds b)
		{
			return (a.min - b.min).sqrMagnitude <= Mathf.Epsilon && (a.max - b.max).sqrMagnitude <= Mathf.Epsilon;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00015C5C File Offset: 0x00013E5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void TransformBounds2D(Matrix4x4 transform, ref Bounds bounds)
		{
			Vector3 center = transform.MultiplyPoint(bounds.center);
			Vector3 extents = bounds.extents;
			Vector3 axisX = transform.MultiplyVector(new Vector3(extents.x, 0f, 0f));
			Vector3 axisY = transform.MultiplyVector(new Vector3(0f, extents.y, 0f));
			extents.x = MathF.Abs(axisX.x) + MathF.Abs(axisY.x);
			extents.y = MathF.Abs(axisX.y) + MathF.Abs(axisY.y);
			bounds = new Bounds
			{
				center = center,
				extents = extents
			};
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00015D14 File Offset: 0x00013F14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void ClearShapes(ShadowShape2D persistantShapeObject)
		{
			persistantShapeObject.SetShape(default(NativeArray<Vector3>), default(NativeArray<int>), ShadowShape2D.OutlineTopology.Lines, ShadowShape2D.WindingOrder.CounterClockwise, true, false);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00015D40 File Offset: 0x00013F40
		private void CalculateShadows(Collider2D collider, ShadowShape2D persistantShapeObject, Bounds worldCullingBounds)
		{
			if (this.m_ShadowShapeGroup == null)
			{
				this.m_ShadowShapeGroup = new PhysicsShapeGroup2D(collider.shapeCount, 8);
			}
			if (this.m_ShadowShapeBounds == null)
			{
				this.m_ShadowShapeBounds = new List<Bounds>(collider.shapeCount);
			}
			if (this.m_ShadowShapeMinMaxBounds == null)
			{
				this.m_ShadowShapeMinMaxBounds = new List<ShadowShape2DProvider_Collider2D.MinMaxBounds>();
			}
			Rigidbody2D attachedBody = collider.attachedRigidbody;
			Matrix4x4 colliderSpace = (attachedBody ? attachedBody.transform.localToWorldMatrix : Matrix4x4.identity);
			uint shapeHash = collider.GetShapeHash();
			if (shapeHash != this.m_ShadowStateHash)
			{
				this.m_ShadowStateHash = shapeHash;
				this.m_ShadowShapeGroup.Clear();
				if (collider.shapeCount == 0)
				{
					ShadowShape2DProvider_Collider2D.ClearShapes(persistantShapeObject);
					return;
				}
				if (collider.GetShapes(this.m_ShadowShapeGroup) == 0)
				{
					return;
				}
				this.m_LastWorldCullingBounds = worldCullingBounds;
				Bounds combinedBounds = collider.GetShapeBounds(this.m_ShadowShapeBounds, true, false);
				this.m_ShadowCombinedShapeMinMaxBounds = new ShadowShape2DProvider_Collider2D.MinMaxBounds(ref combinedBounds);
				this.m_ShadowShapeMinMaxBounds.Clear();
				this.m_ShadowShapeMinMaxBounds.Capacity = this.m_ShadowShapeBounds.Capacity;
				for (int i = 0; i < this.m_ShadowShapeBounds.Count; i++)
				{
					Bounds shapeBounds = this.m_ShadowShapeBounds[i];
					this.m_ShadowShapeMinMaxBounds.Add(new ShadowShape2DProvider_Collider2D.MinMaxBounds(ref shapeBounds));
				}
				this.m_ShadowDirty = true;
			}
			else
			{
				if (colliderSpace.Equals(this.m_LastColliderSpace) && ShadowShape2DProvider_Collider2D.CompareApproximately(ref this.m_LastWorldCullingBounds, ref worldCullingBounds))
				{
					return;
				}
				this.m_LastWorldCullingBounds = worldCullingBounds;
				this.m_ShadowDirty = true;
			}
			this.m_LastColliderSpace = colliderSpace;
			if (!this.m_ShadowDirty || this.m_ShadowShapeGroup.shapeCount == 0)
			{
				return;
			}
			this.m_ShadowDirty = false;
			ShadowShape2DProvider_Collider2D.TransformBounds2D(Matrix4x4.Inverse(colliderSpace), ref worldCullingBounds);
			ShadowShape2DProvider_Collider2D.MinMaxBounds worldCullingMinMaxBounds = new ShadowShape2DProvider_Collider2D.MinMaxBounds(ref worldCullingBounds);
			if (!this.m_ShadowCombinedShapeMinMaxBounds.Intersects(ref worldCullingMinMaxBounds))
			{
				ShadowShape2DProvider_Collider2D.ClearShapes(persistantShapeObject);
				return;
			}
			int shapeCount = this.m_ShadowShapeGroup.shapeCount;
			List<PhysicsShape2D> shapeGroupShapes = this.m_ShadowShapeGroup.groupShapes;
			List<Vector2> shapeGroupVertices = this.m_ShadowShapeGroup.groupVertices;
			NativeArray<int> visibleShapeIndices = new NativeArray<int>(shapeCount, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			int vertexCount = 0;
			int indexCount = 0;
			int visibleShapeCount = 0;
			for (int j = 0; j < shapeCount; j++)
			{
				if (this.m_ShadowShapeMinMaxBounds[j].Intersects(ref worldCullingMinMaxBounds))
				{
					PhysicsShape2D shape = shapeGroupShapes[j];
					int shapeVertexCount = shape.vertexCount;
					PhysicsShapeType2D shapeType = shape.shapeType;
					vertexCount += shapeVertexCount;
					switch (shapeType)
					{
					case PhysicsShapeType2D.Circle:
					case PhysicsShapeType2D.Capsule:
						indexCount += 2;
						break;
					case PhysicsShapeType2D.Polygon:
						indexCount += 2 * shapeVertexCount;
						break;
					case PhysicsShapeType2D.Edges:
					{
						Vector2 startVertex = shapeGroupVertices[shape.vertexStartIndex];
						bool openEdges = (shapeGroupVertices[shape.vertexStartIndex + shape.vertexCount - 1] - startVertex).sqrMagnitude > Mathf.Epsilon;
						indexCount += 2 * (openEdges ? (shapeVertexCount - 1) : shapeVertexCount);
						break;
					}
					}
					visibleShapeIndices[visibleShapeCount++] = j;
				}
			}
			if (visibleShapeCount > 0)
			{
				NativeArray<float> radii = new NativeArray<float>(vertexCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<Vector3> vertices = new NativeArray<Vector3>(vertexCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<int> indices = new NativeArray<int>(indexCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
				int vertexIndex = 0;
				int indiceIndex = 0;
				for (int k = 0; k < visibleShapeCount; k++)
				{
					PhysicsShape2D shape2 = shapeGroupShapes[visibleShapeIndices[k]];
					PhysicsShapeType2D shapeType2 = shape2.shapeType;
					float radius = shape2.radius;
					int shapeVertexIndex = shape2.vertexStartIndex;
					int shapeVertexCount2 = shape2.vertexCount;
					switch (shapeType2)
					{
					case PhysicsShapeType2D.Circle:
						radii[vertexIndex] = radius;
						indices[indiceIndex++] = vertexIndex;
						indices[indiceIndex++] = vertexIndex;
						vertices[vertexIndex++] = shapeGroupVertices[shapeVertexIndex];
						break;
					case PhysicsShapeType2D.Capsule:
						radii[vertexIndex] = radius;
						indices[indiceIndex++] = vertexIndex;
						vertices[vertexIndex++] = shapeGroupVertices[shapeVertexIndex++];
						radii[vertexIndex] = radius;
						indices[indiceIndex++] = vertexIndex;
						vertices[vertexIndex++] = shapeGroupVertices[shapeVertexIndex++];
						break;
					case PhysicsShapeType2D.Polygon:
					{
						int startIndex = vertexIndex;
						int edgeIndex = vertexIndex;
						for (int l = 0; l < shapeVertexCount2 - 1; l++)
						{
							radii[vertexIndex] = radius;
							vertices[vertexIndex++] = shapeGroupVertices[shapeVertexIndex++];
							indices[indiceIndex++] = edgeIndex++;
							indices[indiceIndex++] = edgeIndex;
						}
						radii[vertexIndex] = radius;
						vertices[vertexIndex++] = shapeGroupVertices[shapeVertexIndex++];
						indices[indiceIndex++] = edgeIndex;
						indices[indiceIndex++] = startIndex;
						break;
					}
					case PhysicsShapeType2D.Edges:
					{
						int startIndex2 = vertexIndex;
						int edgeIndex2 = vertexIndex;
						for (int m = 0; m < shapeVertexCount2 - 1; m++)
						{
							radii[vertexIndex] = radius;
							vertices[vertexIndex++] = shapeGroupVertices[shapeVertexIndex++];
							indices[indiceIndex++] = edgeIndex2++;
							indices[indiceIndex++] = edgeIndex2;
						}
						radii[vertexIndex] = radius;
						vertices[vertexIndex++] = shapeGroupVertices[shapeVertexIndex++];
						Vector2 startVertex2 = shapeGroupVertices[shape2.vertexStartIndex];
						if ((shapeGroupVertices[shape2.vertexStartIndex + shape2.vertexCount - 1] - startVertex2).sqrMagnitude <= Mathf.Epsilon)
						{
							indices[indiceIndex++] = edgeIndex2;
							indices[indiceIndex++] = startIndex2;
						}
						break;
					}
					}
				}
				Matrix4x4 toShadowSpace = collider.transform.worldToLocalMatrix * colliderSpace;
				Renderer renderer;
				bool createInteriorGeometry = !collider.TryGetComponent<Renderer>(out renderer);
				persistantShapeObject.SetShape(vertices, indices, radii, toShadowSpace, ShadowShape2D.WindingOrder.CounterClockwise, true, createInteriorGeometry);
				indices.Dispose();
				vertices.Dispose();
				radii.Dispose();
			}
			else
			{
				ShadowShape2DProvider_Collider2D.ClearShapes(persistantShapeObject);
			}
			visibleShapeIndices.Dispose();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0001638D File Offset: 0x0001458D
		public override bool IsShapeSource(Component sourceComponent)
		{
			return sourceComponent is Collider2D;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00016398 File Offset: 0x00014598
		public override void OnPersistantDataCreated(Component sourceComponent, ShadowShape2D persistantShadowShapeData)
		{
			this.m_ShadowStateHash = 0U;
			this.m_ShadowCombinedShapeMinMaxBounds = default(ShadowShape2DProvider_Collider2D.MinMaxBounds);
			this.m_LastColliderSpace = Matrix4x4.identity;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000163B8 File Offset: 0x000145B8
		public override void OnBeforeRender(Component sourceComponent, Bounds worldCullingBounds, ShadowShape2D persistantShadowShape)
		{
			Collider2D collider = (Collider2D)sourceComponent;
			this.CalculateShadows(collider, persistantShadowShape, worldCullingBounds);
		}

		// Token: 0x04000292 RID: 658
		private const float k_InitialTrim = 0.05f;

		// Token: 0x04000293 RID: 659
		private List<Bounds> m_ShadowShapeBounds;

		// Token: 0x04000294 RID: 660
		private List<ShadowShape2DProvider_Collider2D.MinMaxBounds> m_ShadowShapeMinMaxBounds;

		// Token: 0x04000295 RID: 661
		private ShadowShape2DProvider_Collider2D.MinMaxBounds m_ShadowCombinedShapeMinMaxBounds;

		// Token: 0x04000296 RID: 662
		private Bounds m_LastWorldCullingBounds;

		// Token: 0x04000297 RID: 663
		private Matrix4x4 m_LastColliderSpace;

		// Token: 0x04000298 RID: 664
		private bool m_ShadowDirty = true;

		// Token: 0x04000299 RID: 665
		private uint m_ShadowStateHash;

		// Token: 0x0400029A RID: 666
		private PhysicsShapeGroup2D m_ShadowShapeGroup;

		// Token: 0x02000076 RID: 118
		private struct MinMaxBounds
		{
			// Token: 0x060002E7 RID: 743 RVA: 0x000163E4 File Offset: 0x000145E4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool Intersects(ref ShadowShape2DProvider_Collider2D.MinMaxBounds bounds)
			{
				return this.min.x <= bounds.max.x && this.max.x >= bounds.min.x && this.min.y <= bounds.max.y && this.max.y >= bounds.min.y && this.min.z <= bounds.max.z && this.max.z >= bounds.min.z;
			}

			// Token: 0x060002E8 RID: 744 RVA: 0x00016486 File Offset: 0x00014686
			public MinMaxBounds(ref Bounds bounds)
			{
				this.min = bounds.min;
				this.max = bounds.max;
			}

			// Token: 0x0400029B RID: 667
			public Vector3 min;

			// Token: 0x0400029C RID: 668
			public Vector3 max;
		}
	}
}
