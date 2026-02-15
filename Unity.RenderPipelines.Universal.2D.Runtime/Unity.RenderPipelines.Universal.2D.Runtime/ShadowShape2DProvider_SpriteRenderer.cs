using System;
using Unity.Collections;
using UnityEngine.Events;
using UnityEngine.U2D;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000077 RID: 119
	[Serializable]
	internal class ShadowShape2DProvider_SpriteRenderer : ShadowShape2DProvider
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x000164A0 File Offset: 0x000146A0
		private void SetFullRectShapeData(SpriteRenderer spriteRenderer, ShadowShape2D shadowShape2D)
		{
			if (spriteRenderer.drawMode != SpriteDrawMode.Simple)
			{
				Sprite sprite = spriteRenderer.sprite;
				Vector2 srSize = spriteRenderer.size;
				Vector3 pivot = new Vector2(srSize.x * sprite.pivot.x / sprite.rect.width, srSize.y * sprite.pivot.y / sprite.rect.height);
				Rect rect = new Rect(-pivot, new Vector2(srSize.x, srSize.y));
				NativeArray<Vector3> vertices = new NativeArray<Vector3>(4, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<int> indices = new NativeArray<int>(8, Allocator.Temp, NativeArrayOptions.ClearMemory);
				vertices[0] = new Vector3(rect.min.x, rect.min.y);
				vertices[1] = new Vector3(rect.min.x, rect.max.y);
				vertices[2] = new Vector3(rect.max.x, rect.max.y);
				vertices[3] = new Vector3(rect.max.x, rect.min.y);
				indices[0] = 0;
				indices[1] = 1;
				indices[2] = 1;
				indices[3] = 2;
				indices[4] = 2;
				indices[5] = 3;
				indices[6] = 3;
				indices[7] = 0;
				shadowShape2D.SetShape(vertices, indices, ShadowShape2D.OutlineTopology.Lines, ShadowShape2D.WindingOrder.Clockwise, true, false);
				vertices.Dispose();
				indices.Dispose();
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00016644 File Offset: 0x00014844
		private void SetPersistantShapeData(Sprite sprite, ShadowShape2D shadowShape2D, NativeSlice<Vector3> vertexSlice)
		{
			if (shadowShape2D != null)
			{
				NativeArray<ushort> ushortIndices = sprite.GetIndices();
				NativeArray<int> indices = new NativeArray<int>(ushortIndices.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<Vector3> vertices = new NativeArray<Vector3>(vertexSlice.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
				for (int i = 0; i < indices.Length; i++)
				{
					indices[i] = (int)ushortIndices[i];
				}
				for (int j = 0; j < vertices.Length; j++)
				{
					vertices[j] = vertexSlice[j];
				}
				shadowShape2D.SetShape(vertices, indices, ShadowShape2D.OutlineTopology.Triangles, ShadowShape2D.WindingOrder.Clockwise, true, false);
				vertices.Dispose();
				indices.Dispose();
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000166E4 File Offset: 0x000148E4
		private void TryToSetPersistantShapeData(SpriteRenderer spriteRenderer, ShadowShape2D persistantShadowShape, bool force)
		{
			if (spriteRenderer != null && spriteRenderer.sprite != null)
			{
				if (spriteRenderer.drawMode != SpriteDrawMode.Simple && (spriteRenderer.size.x != this.m_CurrentDrawModeSize.x || spriteRenderer.size.y != this.m_CurrentDrawModeSize.y || spriteRenderer.drawMode != this.m_CurrentDrawMode || force))
				{
					this.m_CurrentDrawModeSize = spriteRenderer.size;
					this.SetFullRectShapeData(spriteRenderer, persistantShadowShape);
				}
				else if (spriteRenderer.drawMode != this.m_CurrentDrawMode || force)
				{
					Sprite sprite = spriteRenderer.sprite;
					NativeSlice<Vector3> vertexSlice = sprite.GetVertexAttribute(VertexAttribute.Position);
					this.SetPersistantShapeData(sprite, this.m_PersistantShapeData, vertexSlice);
				}
				this.m_CurrentDrawMode = spriteRenderer.drawMode;
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000167B2 File Offset: 0x000149B2
		private void UpdatePersistantShapeData(SpriteRenderer spriteRenderer)
		{
			this.TryToSetPersistantShapeData(spriteRenderer, this.m_PersistantShapeData, true);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00011A70 File Offset: 0x0000FC70
		public override int Priority()
		{
			return 1;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000167C2 File Offset: 0x000149C2
		public override bool IsShapeSource(Component sourceComponent)
		{
			return sourceComponent is SpriteRenderer;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000167D0 File Offset: 0x000149D0
		public override void OnPersistantDataCreated(Component sourceComponent, ShadowShape2D persistantShadowShape)
		{
			SpriteRenderer spriteRenderer = (SpriteRenderer)sourceComponent;
			this.m_PersistantShapeData = persistantShadowShape;
			spriteRenderer.RegisterSpriteChangeCallback(new UnityAction<SpriteRenderer>(this.UpdatePersistantShapeData));
			if (spriteRenderer.sprite != null)
			{
				float trimEdge = ShadowShapeProvider2DUtility.GetTrimEdgeFromBounds(spriteRenderer.bounds, 0.05f);
				persistantShadowShape.SetDefaultTrim(trimEdge);
			}
			this.TryToSetPersistantShapeData(spriteRenderer, persistantShadowShape, true);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0001682C File Offset: 0x00014A2C
		public override void OnBeforeRender(Component sourceComponent, Bounds worldCullingBounds, ShadowShape2D persistantShadowShape)
		{
			SpriteRenderer spriteRenderer = (SpriteRenderer)sourceComponent;
			persistantShadowShape.SetFlip(spriteRenderer.flipX, spriteRenderer.flipY);
			this.TryToSetPersistantShapeData(spriteRenderer, persistantShadowShape, false);
		}

		// Token: 0x0400029D RID: 669
		private const float k_InitialTrim = 0.05f;

		// Token: 0x0400029E RID: 670
		private ShadowShape2D m_PersistantShapeData;

		// Token: 0x0400029F RID: 671
		private SpriteDrawMode m_CurrentDrawMode;

		// Token: 0x040002A0 RID: 672
		private Vector2 m_CurrentDrawModeSize;
	}
}
