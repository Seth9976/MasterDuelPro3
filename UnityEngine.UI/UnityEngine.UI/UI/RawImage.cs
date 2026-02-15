using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200005E RID: 94
	[RequireComponent(typeof(CanvasRenderer))]
	[AddComponentMenu("UI/Raw Image", 12)]
	public class RawImage : MaskableGraphic
	{
		// Token: 0x06000374 RID: 884 RVA: 0x00010A34 File Offset: 0x0000EC34
		protected RawImage()
		{
			base.useLegacyMeshGeneration = false;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00010A64 File Offset: 0x0000EC64
		public override Texture mainTexture
		{
			get
			{
				if (!(this.m_Texture == null))
				{
					return this.m_Texture;
				}
				if (this.material != null && this.material.mainTexture != null)
				{
					return this.material.mainTexture;
				}
				return Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00010AB8 File Offset: 0x0000ECB8
		// (set) Token: 0x06000377 RID: 887 RVA: 0x00010AC0 File Offset: 0x0000ECC0
		public Texture texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				if (this.m_Texture == value)
				{
					return;
				}
				this.m_Texture = value;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00010AE4 File Offset: 0x0000ECE4
		// (set) Token: 0x06000379 RID: 889 RVA: 0x00010AEC File Offset: 0x0000ECEC
		public Rect uvRect
		{
			get
			{
				return this.m_UVRect;
			}
			set
			{
				if (this.m_UVRect == value)
				{
					return;
				}
				this.m_UVRect = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00010B0C File Offset: 0x0000ED0C
		public override void SetNativeSize()
		{
			Texture tex = this.mainTexture;
			if (tex != null)
			{
				int w = Mathf.RoundToInt((float)tex.width * this.uvRect.width);
				int h = Mathf.RoundToInt((float)tex.height * this.uvRect.height);
				base.rectTransform.anchorMax = base.rectTransform.anchorMin;
				base.rectTransform.sizeDelta = new Vector2((float)w, (float)h);
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00010B8C File Offset: 0x0000ED8C
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			Texture tex = this.mainTexture;
			vh.Clear();
			if (tex != null)
			{
				Rect r = base.GetPixelAdjustedRect();
				Vector4 v = new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);
				float scaleX = (float)tex.width * tex.texelSize.x;
				float scaleY = (float)tex.height * tex.texelSize.y;
				Color color32 = this.color;
				vh.AddVert(new Vector3(v.x, v.y), color32, new Vector2(this.m_UVRect.xMin * scaleX, this.m_UVRect.yMin * scaleY));
				vh.AddVert(new Vector3(v.x, v.w), color32, new Vector2(this.m_UVRect.xMin * scaleX, this.m_UVRect.yMax * scaleY));
				vh.AddVert(new Vector3(v.z, v.w), color32, new Vector2(this.m_UVRect.xMax * scaleX, this.m_UVRect.yMax * scaleY));
				vh.AddVert(new Vector3(v.z, v.y), color32, new Vector2(this.m_UVRect.xMax * scaleX, this.m_UVRect.yMin * scaleY));
				vh.AddTriangle(0, 1, 2);
				vh.AddTriangle(2, 3, 0);
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00009806 File Offset: 0x00007A06
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetMaterialDirty();
			this.SetVerticesDirty();
			base.SetRaycastDirty();
		}

		// Token: 0x040001B1 RID: 433
		[FormerlySerializedAs("m_Tex")]
		[SerializeField]
		private Texture m_Texture;

		// Token: 0x040001B2 RID: 434
		[SerializeField]
		private Rect m_UVRect = new Rect(0f, 0f, 1f, 1f);
	}
}
