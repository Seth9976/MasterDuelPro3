using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001380 RID: 4992
	public class NewText : Text
	{
		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06009075 RID: 36981 RVA: 0x0013C2F1 File Offset: 0x0013A4F1
		// (set) Token: 0x06009076 RID: 36982 RVA: 0x0013C2F9 File Offset: 0x0013A4F9
		public int Visiblelines { get; private set; }

		// Token: 0x06009077 RID: 36983 RVA: 0x0013C304 File Offset: 0x0013A504
		private void _UseFitSettings()
		{
			TextGenerationSettings settings = base.GetGenerationSettings(base.rectTransform.rect.size);
			settings.resizeTextForBestFit = false;
			if (!base.resizeTextForBestFit)
			{
				base.cachedTextGenerator.PopulateWithErrors(this.text, settings, base.gameObject);
				return;
			}
			int minSize = base.resizeTextMinSize;
			int txtLen = this.text.Length;
			for (int i = base.resizeTextMaxSize; i >= minSize; i--)
			{
				settings.fontSize = i;
				base.cachedTextGenerator.PopulateWithErrors(this.text, settings, base.gameObject);
				if (base.cachedTextGenerator.characterCountVisible == txtLen)
				{
					break;
				}
			}
		}

		// Token: 0x06009078 RID: 36984 RVA: 0x0013C3B0 File Offset: 0x0013A5B0
		protected override void OnPopulateMesh(VertexHelper toFill)
		{
			if (base.font == null)
			{
				return;
			}
			this.m_DisableFontTextureRebuiltCallback = true;
			this._UseFitSettings();
			IList<UIVertex> verts = base.cachedTextGenerator.verts;
			float unitPerPixel = 1f / base.pixelsPerUnit;
			int vertCount = verts.Count;
			if (vertCount <= 0)
			{
				toFill.Clear();
				return;
			}
			Vector2 roundingOffset = new Vector2(verts[0].position.x, verts[0].position.y) * unitPerPixel;
			roundingOffset = base.PixelAdjustPoint(roundingOffset) - roundingOffset;
			toFill.Clear();
			if (roundingOffset != Vector2.zero)
			{
				for (int i = 0; i < vertCount; i++)
				{
					int tempVertsIndex = i & 3;
					this._tmpVerts[tempVertsIndex] = verts[i];
					UIVertex[] tmpVerts = this._tmpVerts;
					int num = tempVertsIndex;
					tmpVerts[num].position = tmpVerts[num].position * unitPerPixel;
					UIVertex[] tmpVerts2 = this._tmpVerts;
					int num2 = tempVertsIndex;
					tmpVerts2[num2].position.x = tmpVerts2[num2].position.x + roundingOffset.x;
					UIVertex[] tmpVerts3 = this._tmpVerts;
					int num3 = tempVertsIndex;
					tmpVerts3[num3].position.y = tmpVerts3[num3].position.y + roundingOffset.y;
					if (tempVertsIndex == 3)
					{
						toFill.AddUIVertexQuad(this._tmpVerts);
					}
				}
			}
			else
			{
				for (int j = 0; j < vertCount; j++)
				{
					int tempVertsIndex2 = j & 3;
					this._tmpVerts[tempVertsIndex2] = verts[j];
					UIVertex[] tmpVerts4 = this._tmpVerts;
					int num4 = tempVertsIndex2;
					tmpVerts4[num4].position = tmpVerts4[num4].position * unitPerPixel;
					if (tempVertsIndex2 == 3)
					{
						toFill.AddUIVertexQuad(this._tmpVerts);
					}
				}
			}
			this.m_DisableFontTextureRebuiltCallback = false;
			this.Visiblelines = base.cachedTextGenerator.lineCount;
		}

		// Token: 0x0400CF28 RID: 53032
		private readonly UIVertex[] _tmpVerts = new UIVertex[4];
	}
}
