using System;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000005 RID: 5
	internal abstract class AtlasBase
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020C4 File Offset: 0x000002C4
		public virtual bool TryGetAtlas(VisualElement ctx, Texture2D src, out TextureId atlas, out RectInt atlasRect)
		{
			atlas = TextureId.invalid;
			atlasRect = default(RectInt);
			return false;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020EA File Offset: 0x000002EA
		public virtual void ReturnAtlas(VisualElement ctx, Texture2D src, TextureId atlas)
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020EA File Offset: 0x000002EA
		public virtual void Reset()
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020EA File Offset: 0x000002EA
		protected virtual void OnAssignedToPanel(IPanel panel)
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020EA File Offset: 0x000002EA
		protected virtual void OnRemovedFromPanel(IPanel panel)
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020EA File Offset: 0x000002EA
		protected virtual void OnUpdateDynamicTextures(IPanel panel)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020ED File Offset: 0x000002ED
		internal void InvokeAssignedToPanel(IPanel panel)
		{
			this.OnAssignedToPanel(panel);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020F8 File Offset: 0x000002F8
		internal void InvokeRemovedFromPanel(IPanel panel)
		{
			this.OnRemovedFromPanel(panel);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002103 File Offset: 0x00000303
		internal void InvokeUpdateDynamicTextures(IPanel panel)
		{
			this.OnUpdateDynamicTextures(panel);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002110 File Offset: 0x00000310
		protected static void RepaintTexturedElements(IPanel panel)
		{
			Panel p = panel as Panel;
			UIRRepaintUpdater updater = ((p != null) ? p.GetUpdater(VisualTreeUpdatePhase.Repaint) : null) as UIRRepaintUpdater;
			if (updater != null)
			{
				RenderChain renderChain = updater.renderChain;
				if (renderChain != null)
				{
					renderChain.RepaintTexturedElements();
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000214F File Offset: 0x0000034F
		protected void SetDynamicTexture(TextureId id, Texture texture)
		{
			this.textureRegistry.UpdateDynamic(id, texture);
		}

		// Token: 0x04000001 RID: 1
		internal TextureRegistry textureRegistry = TextureRegistry.instance;
	}
}
