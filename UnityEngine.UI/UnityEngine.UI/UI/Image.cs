using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Serialization;
using UnityEngine.Sprites;
using UnityEngine.U2D;

namespace UnityEngine.UI
{
	// Token: 0x02000026 RID: 38
	[RequireComponent(typeof(CanvasRenderer))]
	[AddComponentMenu("UI/Image", 11)]
	public class Image : MaskableGraphic, ISerializationCallbackReceiver, ILayoutElement, ICanvasRaycastFilter
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006BF8 File Offset: 0x00004DF8
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00006C00 File Offset: 0x00004E00
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				if (this.m_Sprite != null)
				{
					if (this.m_Sprite != value)
					{
						this.m_SkipLayoutUpdate = this.m_Sprite.rect.size.Equals(value ? value.rect.size : Vector2.zero);
						this.m_SkipMaterialUpdate = this.m_Sprite.texture == (value ? value.texture : null);
						this.m_Sprite = value;
						this.<set_sprite>g__ResetAlphaHitThresholdIfNeeded|11_0();
						this.SetAllDirty();
						this.TrackSprite();
						return;
					}
				}
				else if (value != null)
				{
					this.m_SkipLayoutUpdate = value.rect.size == Vector2.zero;
					this.m_SkipMaterialUpdate = value.texture == null;
					this.m_Sprite = value;
					this.<set_sprite>g__ResetAlphaHitThresholdIfNeeded|11_0();
					this.SetAllDirty();
					this.TrackSprite();
				}
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006D00 File Offset: 0x00004F00
		public void DisableSpriteOptimizations()
		{
			this.m_SkipLayoutUpdate = false;
			this.m_SkipMaterialUpdate = false;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006D10 File Offset: 0x00004F10
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00006D18 File Offset: 0x00004F18
		public Sprite overrideSprite
		{
			get
			{
				return this.activeSprite;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Sprite>(ref this.m_OverrideSprite, value))
				{
					this.SetAllDirty();
					this.TrackSprite();
				}
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006D34 File Offset: 0x00004F34
		private Sprite activeSprite
		{
			get
			{
				if (!(this.m_OverrideSprite != null))
				{
					return this.sprite;
				}
				return this.m_OverrideSprite;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00006D51 File Offset: 0x00004F51
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00006D59 File Offset: 0x00004F59
		public Image.Type type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Image.Type>(ref this.m_Type, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00006D6F File Offset: 0x00004F6F
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00006D77 File Offset: 0x00004F77
		public bool preserveAspect
		{
			get
			{
				return this.m_PreserveAspect;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_PreserveAspect, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00006D8D File Offset: 0x00004F8D
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00006D95 File Offset: 0x00004F95
		public bool fillCenter
		{
			get
			{
				return this.m_FillCenter;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_FillCenter, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00006DAB File Offset: 0x00004FAB
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00006DB3 File Offset: 0x00004FB3
		public Image.FillMethod fillMethod
		{
			get
			{
				return this.m_FillMethod;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Image.FillMethod>(ref this.m_FillMethod, value))
				{
					this.SetVerticesDirty();
					this.m_FillOrigin = 0;
				}
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00006DD0 File Offset: 0x00004FD0
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00006DD8 File Offset: 0x00004FD8
		public float fillAmount
		{
			get
			{
				return this.m_FillAmount;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FillAmount, Mathf.Clamp01(value)))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00006DF3 File Offset: 0x00004FF3
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00006DFB File Offset: 0x00004FFB
		public bool fillClockwise
		{
			get
			{
				return this.m_FillClockwise;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_FillClockwise, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006E11 File Offset: 0x00005011
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00006E19 File Offset: 0x00005019
		public int fillOrigin
		{
			get
			{
				return this.m_FillOrigin;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_FillOrigin, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006E2F File Offset: 0x0000502F
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00006E3D File Offset: 0x0000503D
		[Obsolete("eventAlphaThreshold has been deprecated. Use eventMinimumAlphaThreshold instead (UnityUpgradable) -> alphaHitTestMinimumThreshold")]
		public float eventAlphaThreshold
		{
			get
			{
				return 1f - this.alphaHitTestMinimumThreshold;
			}
			set
			{
				this.alphaHitTestMinimumThreshold = 1f - value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00006E4C File Offset: 0x0000504C
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00006E54 File Offset: 0x00005054
		public float alphaHitTestMinimumThreshold
		{
			get
			{
				return this.m_AlphaHitTestMinimumThreshold;
			}
			set
			{
				if (this.sprite != null && (GraphicsFormatUtility.IsCrunchFormat(this.sprite.texture.format) || !this.sprite.texture.isReadable))
				{
					throw new InvalidOperationException("alphaHitTestMinimumThreshold should not be modified on a texture not readeable or not using Crunch Compression.");
				}
				this.m_AlphaHitTestMinimumThreshold = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00006EAA File Offset: 0x000050AA
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00006EB2 File Offset: 0x000050B2
		public bool useSpriteMesh
		{
			get
			{
				return this.m_UseSpriteMesh;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_UseSpriteMesh, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006EC8 File Offset: 0x000050C8
		protected Image()
		{
			base.useLegacyMeshGeneration = false;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00006F18 File Offset: 0x00005118
		public static Material defaultETC1GraphicMaterial
		{
			get
			{
				if (Image.s_ETC1DefaultUI == null)
				{
					Image.s_ETC1DefaultUI = Canvas.GetETC1SupportedCanvasMaterial();
				}
				return Image.s_ETC1DefaultUI;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00006F38 File Offset: 0x00005138
		public override Texture mainTexture
		{
			get
			{
				if (!(this.activeSprite == null))
				{
					return this.activeSprite.texture;
				}
				if (this.material != null && this.material.mainTexture != null)
				{
					return this.material.mainTexture;
				}
				return Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00006F94 File Offset: 0x00005194
		public bool hasBorder
		{
			get
			{
				return this.activeSprite != null && this.activeSprite.border.sqrMagnitude > 0f;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00006FCB File Offset: 0x000051CB
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00006FD3 File Offset: 0x000051D3
		public float pixelsPerUnitMultiplier
		{
			get
			{
				return this.m_PixelsPerUnitMultiplier;
			}
			set
			{
				this.m_PixelsPerUnitMultiplier = Mathf.Max(0.01f, value);
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00006FEC File Offset: 0x000051EC
		public float pixelsPerUnit
		{
			get
			{
				float spritePixelsPerUnit = 100f;
				if (this.activeSprite)
				{
					spritePixelsPerUnit = this.activeSprite.pixelsPerUnit;
				}
				if (base.canvas)
				{
					this.m_CachedReferencePixelsPerUnit = base.canvas.referencePixelsPerUnit;
				}
				return spritePixelsPerUnit / this.m_CachedReferencePixelsPerUnit;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000703E File Offset: 0x0000523E
		protected float multipliedPixelsPerUnit
		{
			get
			{
				return this.pixelsPerUnit * this.m_PixelsPerUnitMultiplier;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00007050 File Offset: 0x00005250
		// (set) Token: 0x06000163 RID: 355 RVA: 0x0000709E File Offset: 0x0000529E
		public override Material material
		{
			get
			{
				if (this.m_Material != null)
				{
					return this.m_Material;
				}
				if (this.activeSprite && this.activeSprite.associatedAlphaSplitTexture != null)
				{
					return Image.defaultETC1GraphicMaterial;
				}
				return this.defaultMaterial;
			}
			set
			{
				base.material = value;
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void OnBeforeSerialize()
		{
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000070A8 File Offset: 0x000052A8
		public virtual void OnAfterDeserialize()
		{
			if (this.m_FillOrigin < 0)
			{
				this.m_FillOrigin = 0;
			}
			else if (this.m_FillMethod == Image.FillMethod.Horizontal && this.m_FillOrigin > 1)
			{
				this.m_FillOrigin = 0;
			}
			else if (this.m_FillMethod == Image.FillMethod.Vertical && this.m_FillOrigin > 1)
			{
				this.m_FillOrigin = 0;
			}
			else if (this.m_FillOrigin > 3)
			{
				this.m_FillOrigin = 0;
			}
			this.m_FillAmount = Mathf.Clamp(this.m_FillAmount, 0f, 1f);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00007128 File Offset: 0x00005328
		private void PreserveSpriteAspectRatio(ref Rect rect, Vector2 spriteSize)
		{
			float spriteRatio = spriteSize.x / spriteSize.y;
			float rectRatio = rect.width / rect.height;
			if (spriteRatio > rectRatio)
			{
				float oldHeight = rect.height;
				rect.height = rect.width * (1f / spriteRatio);
				rect.y += (oldHeight - rect.height) * base.rectTransform.pivot.y;
				return;
			}
			float oldWidth = rect.width;
			rect.width = rect.height * spriteRatio;
			rect.x += (oldWidth - rect.width) * base.rectTransform.pivot.x;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000071D4 File Offset: 0x000053D4
		private Vector4 GetDrawingDimensions(bool shouldPreserveAspect)
		{
			Vector4 padding = ((this.activeSprite == null) ? Vector4.zero : DataUtility.GetPadding(this.activeSprite));
			Vector2 size = ((this.activeSprite == null) ? Vector2.zero : new Vector2(this.activeSprite.rect.width, this.activeSprite.rect.height));
			Rect r = base.GetPixelAdjustedRect();
			int spriteW = Mathf.RoundToInt(size.x);
			int spriteH = Mathf.RoundToInt(size.y);
			Vector4 v = new Vector4(padding.x / (float)spriteW, padding.y / (float)spriteH, ((float)spriteW - padding.z) / (float)spriteW, ((float)spriteH - padding.w) / (float)spriteH);
			if (shouldPreserveAspect && size.sqrMagnitude > 0f)
			{
				this.PreserveSpriteAspectRatio(ref r, size);
			}
			v = new Vector4(r.x + r.width * v.x, r.y + r.height * v.y, r.x + r.width * v.z, r.y + r.height * v.w);
			return v;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00007318 File Offset: 0x00005518
		public override void SetNativeSize()
		{
			if (this.activeSprite != null)
			{
				float w = this.activeSprite.rect.width / this.pixelsPerUnit;
				float h = this.activeSprite.rect.height / this.pixelsPerUnit;
				base.rectTransform.anchorMax = base.rectTransform.anchorMin;
				base.rectTransform.sizeDelta = new Vector2(w, h);
				this.SetAllDirty();
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007398 File Offset: 0x00005598
		protected override void OnPopulateMesh(VertexHelper toFill)
		{
			if (this.activeSprite == null)
			{
				base.OnPopulateMesh(toFill);
				return;
			}
			switch (this.type)
			{
			case Image.Type.Simple:
				if (!this.useSpriteMesh)
				{
					this.GenerateSimpleSprite(toFill, this.m_PreserveAspect);
					return;
				}
				this.GenerateSprite(toFill, this.m_PreserveAspect);
				return;
			case Image.Type.Sliced:
				this.GenerateSlicedSprite(toFill);
				return;
			case Image.Type.Tiled:
				this.GenerateTiledSprite(toFill);
				return;
			case Image.Type.Filled:
				this.GenerateFilledSprite(toFill, this.m_PreserveAspect);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000741A File Offset: 0x0000561A
		private void TrackSprite()
		{
			if (this.activeSprite != null && this.activeSprite.texture == null)
			{
				Image.TrackImage(this);
				this.m_Tracked = true;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000744A File Offset: 0x0000564A
		protected override void OnEnable()
		{
			base.OnEnable();
			this.TrackSprite();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00007458 File Offset: 0x00005658
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.m_Tracked)
			{
				Image.UnTrackImage(this);
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007470 File Offset: 0x00005670
		protected override void UpdateMaterial()
		{
			base.UpdateMaterial();
			if (this.activeSprite == null)
			{
				base.canvasRenderer.SetAlphaTexture(null);
				return;
			}
			Texture2D alphaTex = this.activeSprite.associatedAlphaSplitTexture;
			if (alphaTex != null)
			{
				base.canvasRenderer.SetAlphaTexture(alphaTex);
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000074C0 File Offset: 0x000056C0
		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			if (base.canvas == null)
			{
				this.m_CachedReferencePixelsPerUnit = 100f;
				return;
			}
			if (base.canvas.referencePixelsPerUnit != this.m_CachedReferencePixelsPerUnit)
			{
				this.m_CachedReferencePixelsPerUnit = base.canvas.referencePixelsPerUnit;
				if (this.type == Image.Type.Sliced || this.type == Image.Type.Tiled)
				{
					this.SetVerticesDirty();
					this.SetLayoutDirty();
				}
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007530 File Offset: 0x00005730
		private void GenerateSimpleSprite(VertexHelper vh, bool lPreserveAspect)
		{
			Vector4 v = this.GetDrawingDimensions(lPreserveAspect);
			Vector4 uv = ((this.activeSprite != null) ? DataUtility.GetOuterUV(this.activeSprite) : Vector4.zero);
			Color color32 = this.color;
			vh.Clear();
			vh.AddVert(new Vector3(v.x, v.y), color32, new Vector2(uv.x, uv.y));
			vh.AddVert(new Vector3(v.x, v.w), color32, new Vector2(uv.x, uv.w));
			vh.AddVert(new Vector3(v.z, v.w), color32, new Vector2(uv.z, uv.w));
			vh.AddVert(new Vector3(v.z, v.y), color32, new Vector2(uv.z, uv.y));
			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 0);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007654 File Offset: 0x00005854
		private void GenerateSprite(VertexHelper vh, bool lPreserveAspect)
		{
			Vector2 spriteSize = new Vector2(this.activeSprite.rect.width, this.activeSprite.rect.height);
			Vector2 spritePivot = this.activeSprite.pivot / spriteSize;
			Vector2 pivot = base.rectTransform.pivot;
			Rect r = base.GetPixelAdjustedRect();
			if (lPreserveAspect & (spriteSize.sqrMagnitude > 0f))
			{
				this.PreserveSpriteAspectRatio(ref r, spriteSize);
			}
			Vector2 drawingSize = new Vector2(r.width, r.height);
			Vector3 spriteBoundSize = this.activeSprite.bounds.size;
			Vector2 drawOffset = (pivot - spritePivot) * drawingSize;
			Color color32 = this.color;
			vh.Clear();
			Vector2[] vertices = this.activeSprite.vertices;
			Vector2[] uvs = this.activeSprite.uv;
			for (int i = 0; i < vertices.Length; i++)
			{
				vh.AddVert(new Vector3(vertices[i].x / spriteBoundSize.x * drawingSize.x - drawOffset.x, vertices[i].y / spriteBoundSize.y * drawingSize.y - drawOffset.y), color32, new Vector2(uvs[i].x, uvs[i].y));
			}
			ushort[] triangles = this.activeSprite.triangles;
			for (int j = 0; j < triangles.Length; j += 3)
			{
				vh.AddTriangle((int)triangles[j], (int)triangles[j + 1], (int)triangles[j + 2]);
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007804 File Offset: 0x00005A04
		private void GenerateSlicedSprite(VertexHelper toFill)
		{
			if (!this.hasBorder)
			{
				this.GenerateSimpleSprite(toFill, false);
				return;
			}
			Vector4 outer;
			Vector4 inner;
			Vector4 padding;
			Vector4 border;
			if (this.activeSprite != null)
			{
				outer = DataUtility.GetOuterUV(this.activeSprite);
				inner = DataUtility.GetInnerUV(this.activeSprite);
				padding = DataUtility.GetPadding(this.activeSprite);
				border = this.activeSprite.border;
			}
			else
			{
				outer = Vector4.zero;
				inner = Vector4.zero;
				padding = Vector4.zero;
				border = Vector4.zero;
			}
			Rect rect = base.GetPixelAdjustedRect();
			Vector4 adjustedBorders = this.GetAdjustedBorders(border / this.multipliedPixelsPerUnit, rect);
			padding /= this.multipliedPixelsPerUnit;
			Image.s_VertScratch[0] = new Vector2(padding.x, padding.y);
			Image.s_VertScratch[3] = new Vector2(rect.width - padding.z, rect.height - padding.w);
			Image.s_VertScratch[1].x = adjustedBorders.x;
			Image.s_VertScratch[1].y = adjustedBorders.y;
			Image.s_VertScratch[2].x = rect.width - adjustedBorders.z;
			Image.s_VertScratch[2].y = rect.height - adjustedBorders.w;
			for (int i = 0; i < 4; i++)
			{
				Vector2[] array = Image.s_VertScratch;
				int num = i;
				array[num].x = array[num].x + rect.x;
				Vector2[] array2 = Image.s_VertScratch;
				int num2 = i;
				array2[num2].y = array2[num2].y + rect.y;
			}
			Image.s_UVScratch[0] = new Vector2(outer.x, outer.y);
			Image.s_UVScratch[1] = new Vector2(inner.x, inner.y);
			Image.s_UVScratch[2] = new Vector2(inner.z, inner.w);
			Image.s_UVScratch[3] = new Vector2(outer.z, outer.w);
			toFill.Clear();
			for (int x = 0; x < 3; x++)
			{
				int x2 = x + 1;
				for (int y = 0; y < 3; y++)
				{
					if (this.m_FillCenter || x != 1 || y != 1)
					{
						int y2 = y + 1;
						if (Image.s_VertScratch[x2].x - Image.s_VertScratch[x].x > 0f && Image.s_VertScratch[y2].y - Image.s_VertScratch[y].y > 0f)
						{
							Image.AddQuad(toFill, new Vector2(Image.s_VertScratch[x].x, Image.s_VertScratch[y].y), new Vector2(Image.s_VertScratch[x2].x, Image.s_VertScratch[y2].y), this.color, new Vector2(Image.s_UVScratch[x].x, Image.s_UVScratch[y].y), new Vector2(Image.s_UVScratch[x2].x, Image.s_UVScratch[y2].y));
						}
					}
				}
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007B6C File Offset: 0x00005D6C
		private void GenerateTiledSprite(VertexHelper toFill)
		{
			Vector4 outer;
			Vector4 inner;
			Vector4 border;
			Vector2 spriteSize;
			if (this.activeSprite != null)
			{
				outer = DataUtility.GetOuterUV(this.activeSprite);
				inner = DataUtility.GetInnerUV(this.activeSprite);
				border = this.activeSprite.border;
				spriteSize = this.activeSprite.rect.size;
			}
			else
			{
				outer = Vector4.zero;
				inner = Vector4.zero;
				border = Vector4.zero;
				spriteSize = Vector2.one * 100f;
			}
			Rect rect = base.GetPixelAdjustedRect();
			float tileWidth = (spriteSize.x - border.x - border.z) / this.multipliedPixelsPerUnit;
			float tileHeight = (spriteSize.y - border.y - border.w) / this.multipliedPixelsPerUnit;
			border = this.GetAdjustedBorders(border / this.multipliedPixelsPerUnit, rect);
			Vector2 uvMin = new Vector2(inner.x, inner.y);
			Vector2 uvMax = new Vector2(inner.z, inner.w);
			float xMin = border.x;
			float xMax = rect.width - border.z;
			float yMin = border.y;
			float yMax = rect.height - border.w;
			toFill.Clear();
			Vector2 clipped = uvMax;
			if (tileWidth <= 0f)
			{
				tileWidth = xMax - xMin;
			}
			if (tileHeight <= 0f)
			{
				tileHeight = yMax - yMin;
			}
			if (this.activeSprite != null && (this.hasBorder || this.activeSprite.packed || (this.activeSprite.texture != null && this.activeSprite.texture.wrapMode != TextureWrapMode.Repeat)))
			{
				long nTilesW;
				long nTilesH;
				if (this.m_FillCenter)
				{
					nTilesW = (long)Math.Ceiling((double)((xMax - xMin) / tileWidth));
					nTilesH = (long)Math.Ceiling((double)((yMax - yMin) / tileHeight));
					double nVertices;
					if (this.hasBorder)
					{
						nVertices = ((double)nTilesW + 2.0) * ((double)nTilesH + 2.0) * 4.0;
					}
					else
					{
						nVertices = (double)(nTilesW * nTilesH) * 4.0;
					}
					if (nVertices > 65000.0)
					{
						Debug.LogError("Too many sprite tiles on Image \"" + base.name + "\". The tile size will be increased. To remove the limit on the number of tiles, set the Wrap mode to Repeat in the Image Import Settings", this);
						double num = 16250.0;
						double imageRatio;
						if (this.hasBorder)
						{
							imageRatio = ((double)nTilesW + 2.0) / ((double)nTilesH + 2.0);
						}
						else
						{
							imageRatio = (double)nTilesW / (double)nTilesH;
						}
						double targetTilesW = Math.Sqrt(num / imageRatio);
						double targetTilesH = targetTilesW * imageRatio;
						if (this.hasBorder)
						{
							targetTilesW -= 2.0;
							targetTilesH -= 2.0;
						}
						nTilesW = (long)Math.Floor(targetTilesW);
						nTilesH = (long)Math.Floor(targetTilesH);
						tileWidth = (xMax - xMin) / (float)nTilesW;
						tileHeight = (yMax - yMin) / (float)nTilesH;
					}
				}
				else if (this.hasBorder)
				{
					nTilesW = (long)Math.Ceiling((double)((xMax - xMin) / tileWidth));
					nTilesH = (long)Math.Ceiling((double)((yMax - yMin) / tileHeight));
					if (((double)(nTilesH + nTilesW) + 2.0) * 2.0 * 4.0 > 65000.0)
					{
						Debug.LogError("Too many sprite tiles on Image \"" + base.name + "\". The tile size will be increased. To remove the limit on the number of tiles, set the Wrap mode to Repeat in the Image Import Settings", this);
						double num2 = 16250.0;
						double imageRatio2 = (double)nTilesW / (double)nTilesH;
						double num3 = (num2 - 4.0) / (2.0 * (1.0 + imageRatio2));
						double targetTilesH2 = num3 * imageRatio2;
						nTilesW = (long)Math.Floor(num3);
						nTilesH = (long)Math.Floor(targetTilesH2);
						tileWidth = (xMax - xMin) / (float)nTilesW;
						tileHeight = (yMax - yMin) / (float)nTilesH;
					}
				}
				else
				{
					nTilesW = (nTilesH = 0L);
				}
				if (this.m_FillCenter)
				{
					for (long i = 0L; i < nTilesH; i += 1L)
					{
						float y = yMin + (float)i * tileHeight;
						float y2 = yMin + (float)(i + 1L) * tileHeight;
						if (y2 > yMax)
						{
							clipped.y = uvMin.y + (uvMax.y - uvMin.y) * (yMax - y) / (y2 - y);
							y2 = yMax;
						}
						clipped.x = uvMax.x;
						for (long j = 0L; j < nTilesW; j += 1L)
						{
							float x = xMin + (float)j * tileWidth;
							float x2 = xMin + (float)(j + 1L) * tileWidth;
							if (x2 > xMax)
							{
								clipped.x = uvMin.x + (uvMax.x - uvMin.x) * (xMax - x) / (x2 - x);
								x2 = xMax;
							}
							Image.AddQuad(toFill, new Vector2(x, y) + rect.position, new Vector2(x2, y2) + rect.position, this.color, uvMin, clipped);
						}
					}
				}
				if (this.hasBorder)
				{
					clipped = uvMax;
					for (long k = 0L; k < nTilesH; k += 1L)
					{
						float y3 = yMin + (float)k * tileHeight;
						float y4 = yMin + (float)(k + 1L) * tileHeight;
						if (y4 > yMax)
						{
							clipped.y = uvMin.y + (uvMax.y - uvMin.y) * (yMax - y3) / (y4 - y3);
							y4 = yMax;
						}
						Image.AddQuad(toFill, new Vector2(0f, y3) + rect.position, new Vector2(xMin, y4) + rect.position, this.color, new Vector2(outer.x, uvMin.y), new Vector2(uvMin.x, clipped.y));
						Image.AddQuad(toFill, new Vector2(xMax, y3) + rect.position, new Vector2(rect.width, y4) + rect.position, this.color, new Vector2(uvMax.x, uvMin.y), new Vector2(outer.z, clipped.y));
					}
					clipped = uvMax;
					for (long l = 0L; l < nTilesW; l += 1L)
					{
						float x3 = xMin + (float)l * tileWidth;
						float x4 = xMin + (float)(l + 1L) * tileWidth;
						if (x4 > xMax)
						{
							clipped.x = uvMin.x + (uvMax.x - uvMin.x) * (xMax - x3) / (x4 - x3);
							x4 = xMax;
						}
						Image.AddQuad(toFill, new Vector2(x3, 0f) + rect.position, new Vector2(x4, yMin) + rect.position, this.color, new Vector2(uvMin.x, outer.y), new Vector2(clipped.x, uvMin.y));
						Image.AddQuad(toFill, new Vector2(x3, yMax) + rect.position, new Vector2(x4, rect.height) + rect.position, this.color, new Vector2(uvMin.x, uvMax.y), new Vector2(clipped.x, outer.w));
					}
					Image.AddQuad(toFill, new Vector2(0f, 0f) + rect.position, new Vector2(xMin, yMin) + rect.position, this.color, new Vector2(outer.x, outer.y), new Vector2(uvMin.x, uvMin.y));
					Image.AddQuad(toFill, new Vector2(xMax, 0f) + rect.position, new Vector2(rect.width, yMin) + rect.position, this.color, new Vector2(uvMax.x, outer.y), new Vector2(outer.z, uvMin.y));
					Image.AddQuad(toFill, new Vector2(0f, yMax) + rect.position, new Vector2(xMin, rect.height) + rect.position, this.color, new Vector2(outer.x, uvMax.y), new Vector2(uvMin.x, outer.w));
					Image.AddQuad(toFill, new Vector2(xMax, yMax) + rect.position, new Vector2(rect.width, rect.height) + rect.position, this.color, new Vector2(uvMax.x, uvMax.y), new Vector2(outer.z, outer.w));
					return;
				}
			}
			else
			{
				Vector2 uvScale = new Vector2((xMax - xMin) / tileWidth, (yMax - yMin) / tileHeight);
				if (this.m_FillCenter)
				{
					Image.AddQuad(toFill, new Vector2(xMin, yMin) + rect.position, new Vector2(xMax, yMax) + rect.position, this.color, Vector2.Scale(uvMin, uvScale), Vector2.Scale(uvMax, uvScale));
				}
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000084D8 File Offset: 0x000066D8
		private static void AddQuad(VertexHelper vertexHelper, Vector3[] quadPositions, Color32 color, Vector3[] quadUVs)
		{
			int startIndex = vertexHelper.currentVertCount;
			for (int i = 0; i < 4; i++)
			{
				vertexHelper.AddVert(quadPositions[i], color, quadUVs[i]);
			}
			vertexHelper.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
			vertexHelper.AddTriangle(startIndex + 2, startIndex + 3, startIndex);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000852C File Offset: 0x0000672C
		private static void AddQuad(VertexHelper vertexHelper, Vector2 posMin, Vector2 posMax, Color32 color, Vector2 uvMin, Vector2 uvMax)
		{
			int startIndex = vertexHelper.currentVertCount;
			vertexHelper.AddVert(new Vector3(posMin.x, posMin.y, 0f), color, new Vector2(uvMin.x, uvMin.y));
			vertexHelper.AddVert(new Vector3(posMin.x, posMax.y, 0f), color, new Vector2(uvMin.x, uvMax.y));
			vertexHelper.AddVert(new Vector3(posMax.x, posMax.y, 0f), color, new Vector2(uvMax.x, uvMax.y));
			vertexHelper.AddVert(new Vector3(posMax.x, posMin.y, 0f), color, new Vector2(uvMax.x, uvMin.y));
			vertexHelper.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
			vertexHelper.AddTriangle(startIndex + 2, startIndex + 3, startIndex);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008630 File Offset: 0x00006830
		private Vector4 GetAdjustedBorders(Vector4 border, Rect adjustedRect)
		{
			Rect originalRect = base.rectTransform.rect;
			for (int axis = 0; axis <= 1; axis++)
			{
				if (originalRect.size[axis] != 0f)
				{
					float borderScaleRatio = adjustedRect.size[axis] / originalRect.size[axis];
					ref Vector4 ptr = ref border;
					int num = axis;
					ptr[num] *= borderScaleRatio;
					ptr = ref border;
					num = axis + 2;
					ptr[num] *= borderScaleRatio;
				}
				float combinedBorders = border[axis] + border[axis + 2];
				if (adjustedRect.size[axis] < combinedBorders && combinedBorders != 0f)
				{
					float borderScaleRatio = adjustedRect.size[axis] / combinedBorders;
					ref Vector4 ptr = ref border;
					int num = axis;
					ptr[num] *= borderScaleRatio;
					ptr = ref border;
					num = axis + 2;
					ptr[num] *= borderScaleRatio;
				}
			}
			return border;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000874C File Offset: 0x0000694C
		private void GenerateFilledSprite(VertexHelper toFill, bool preserveAspect)
		{
			toFill.Clear();
			if (this.m_FillAmount < 0.001f)
			{
				return;
			}
			Vector4 v = this.GetDrawingDimensions(preserveAspect);
			object obj = ((this.activeSprite != null) ? DataUtility.GetOuterUV(this.activeSprite) : Vector4.zero);
			UIVertex uiv = UIVertex.simpleVert;
			uiv.color = this.color;
			object obj2 = obj;
			float tx0 = obj2.x;
			float ty0 = obj2.y;
			float tx = obj2.z;
			float ty = obj2.w;
			if (this.m_FillMethod == Image.FillMethod.Horizontal || this.m_FillMethod == Image.FillMethod.Vertical)
			{
				if (this.fillMethod == Image.FillMethod.Horizontal)
				{
					float fill = (tx - tx0) * this.m_FillAmount;
					if (this.m_FillOrigin == 1)
					{
						v.x = v.z - (v.z - v.x) * this.m_FillAmount;
						tx0 = tx - fill;
					}
					else
					{
						v.z = v.x + (v.z - v.x) * this.m_FillAmount;
						tx = tx0 + fill;
					}
				}
				else if (this.fillMethod == Image.FillMethod.Vertical)
				{
					float fill2 = (ty - ty0) * this.m_FillAmount;
					if (this.m_FillOrigin == 1)
					{
						v.y = v.w - (v.w - v.y) * this.m_FillAmount;
						ty0 = ty - fill2;
					}
					else
					{
						v.w = v.y + (v.w - v.y) * this.m_FillAmount;
						ty = ty0 + fill2;
					}
				}
			}
			Image.s_Xy[0] = new Vector2(v.x, v.y);
			Image.s_Xy[1] = new Vector2(v.x, v.w);
			Image.s_Xy[2] = new Vector2(v.z, v.w);
			Image.s_Xy[3] = new Vector2(v.z, v.y);
			Image.s_Uv[0] = new Vector2(tx0, ty0);
			Image.s_Uv[1] = new Vector2(tx0, ty);
			Image.s_Uv[2] = new Vector2(tx, ty);
			Image.s_Uv[3] = new Vector2(tx, ty0);
			if (this.m_FillAmount < 1f && this.m_FillMethod != Image.FillMethod.Horizontal && this.m_FillMethod != Image.FillMethod.Vertical)
			{
				if (this.fillMethod == Image.FillMethod.Radial90)
				{
					if (Image.RadialCut(Image.s_Xy, Image.s_Uv, this.m_FillAmount, this.m_FillClockwise, this.m_FillOrigin))
					{
						Image.AddQuad(toFill, Image.s_Xy, this.color, Image.s_Uv);
						return;
					}
				}
				else
				{
					if (this.fillMethod == Image.FillMethod.Radial180)
					{
						for (int side = 0; side < 2; side++)
						{
							int even = ((this.m_FillOrigin > 1) ? 1 : 0);
							float fy0;
							float fy;
							float fx0;
							float fx;
							if (this.m_FillOrigin == 0 || this.m_FillOrigin == 2)
							{
								fy0 = 0f;
								fy = 1f;
								if (side == even)
								{
									fx0 = 0f;
									fx = 0.5f;
								}
								else
								{
									fx0 = 0.5f;
									fx = 1f;
								}
							}
							else
							{
								fx0 = 0f;
								fx = 1f;
								if (side == even)
								{
									fy0 = 0.5f;
									fy = 1f;
								}
								else
								{
									fy0 = 0f;
									fy = 0.5f;
								}
							}
							Image.s_Xy[0].x = Mathf.Lerp(v.x, v.z, fx0);
							Image.s_Xy[1].x = Image.s_Xy[0].x;
							Image.s_Xy[2].x = Mathf.Lerp(v.x, v.z, fx);
							Image.s_Xy[3].x = Image.s_Xy[2].x;
							Image.s_Xy[0].y = Mathf.Lerp(v.y, v.w, fy0);
							Image.s_Xy[1].y = Mathf.Lerp(v.y, v.w, fy);
							Image.s_Xy[2].y = Image.s_Xy[1].y;
							Image.s_Xy[3].y = Image.s_Xy[0].y;
							Image.s_Uv[0].x = Mathf.Lerp(tx0, tx, fx0);
							Image.s_Uv[1].x = Image.s_Uv[0].x;
							Image.s_Uv[2].x = Mathf.Lerp(tx0, tx, fx);
							Image.s_Uv[3].x = Image.s_Uv[2].x;
							Image.s_Uv[0].y = Mathf.Lerp(ty0, ty, fy0);
							Image.s_Uv[1].y = Mathf.Lerp(ty0, ty, fy);
							Image.s_Uv[2].y = Image.s_Uv[1].y;
							Image.s_Uv[3].y = Image.s_Uv[0].y;
							float val = (this.m_FillClockwise ? (this.fillAmount * 2f - (float)side) : (this.m_FillAmount * 2f - (float)(1 - side)));
							if (Image.RadialCut(Image.s_Xy, Image.s_Uv, Mathf.Clamp01(val), this.m_FillClockwise, (side + this.m_FillOrigin + 3) % 4))
							{
								Image.AddQuad(toFill, Image.s_Xy, this.color, Image.s_Uv);
							}
						}
						return;
					}
					if (this.fillMethod == Image.FillMethod.Radial360)
					{
						for (int corner = 0; corner < 4; corner++)
						{
							float fx2;
							float fx3;
							if (corner < 2)
							{
								fx2 = 0f;
								fx3 = 0.5f;
							}
							else
							{
								fx2 = 0.5f;
								fx3 = 1f;
							}
							float fy2;
							float fy3;
							if (corner == 0 || corner == 3)
							{
								fy2 = 0f;
								fy3 = 0.5f;
							}
							else
							{
								fy2 = 0.5f;
								fy3 = 1f;
							}
							Image.s_Xy[0].x = Mathf.Lerp(v.x, v.z, fx2);
							Image.s_Xy[1].x = Image.s_Xy[0].x;
							Image.s_Xy[2].x = Mathf.Lerp(v.x, v.z, fx3);
							Image.s_Xy[3].x = Image.s_Xy[2].x;
							Image.s_Xy[0].y = Mathf.Lerp(v.y, v.w, fy2);
							Image.s_Xy[1].y = Mathf.Lerp(v.y, v.w, fy3);
							Image.s_Xy[2].y = Image.s_Xy[1].y;
							Image.s_Xy[3].y = Image.s_Xy[0].y;
							Image.s_Uv[0].x = Mathf.Lerp(tx0, tx, fx2);
							Image.s_Uv[1].x = Image.s_Uv[0].x;
							Image.s_Uv[2].x = Mathf.Lerp(tx0, tx, fx3);
							Image.s_Uv[3].x = Image.s_Uv[2].x;
							Image.s_Uv[0].y = Mathf.Lerp(ty0, ty, fy2);
							Image.s_Uv[1].y = Mathf.Lerp(ty0, ty, fy3);
							Image.s_Uv[2].y = Image.s_Uv[1].y;
							Image.s_Uv[3].y = Image.s_Uv[0].y;
							float val2 = (this.m_FillClockwise ? (this.m_FillAmount * 4f - (float)((corner + this.m_FillOrigin) % 4)) : (this.m_FillAmount * 4f - (float)(3 - (corner + this.m_FillOrigin) % 4)));
							if (Image.RadialCut(Image.s_Xy, Image.s_Uv, Mathf.Clamp01(val2), this.m_FillClockwise, (corner + 2) % 4))
							{
								Image.AddQuad(toFill, Image.s_Xy, this.color, Image.s_Uv);
							}
						}
						return;
					}
				}
			}
			else
			{
				Image.AddQuad(toFill, Image.s_Xy, this.color, Image.s_Uv);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00009028 File Offset: 0x00007228
		private static bool RadialCut(Vector3[] xy, Vector3[] uv, float fill, bool invert, int corner)
		{
			if (fill < 0.001f)
			{
				return false;
			}
			if ((corner & 1) == 1)
			{
				invert = !invert;
			}
			if (!invert && fill > 0.999f)
			{
				return true;
			}
			float angle = Mathf.Clamp01(fill);
			if (invert)
			{
				angle = 1f - angle;
			}
			angle *= 1.5707964f;
			float cos = Mathf.Cos(angle);
			float sin = Mathf.Sin(angle);
			Image.RadialCut(xy, cos, sin, invert, corner);
			Image.RadialCut(uv, cos, sin, invert, corner);
			return true;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00009098 File Offset: 0x00007298
		private static void RadialCut(Vector3[] xy, float cos, float sin, bool invert, int corner)
		{
			int i = (corner + 1) % 4;
			int i2 = (corner + 2) % 4;
			int i3 = (corner + 3) % 4;
			if ((corner & 1) == 1)
			{
				if (sin > cos)
				{
					cos /= sin;
					sin = 1f;
					if (invert)
					{
						xy[i].x = Mathf.Lerp(xy[corner].x, xy[i2].x, cos);
						xy[i2].x = xy[i].x;
					}
				}
				else if (cos > sin)
				{
					sin /= cos;
					cos = 1f;
					if (!invert)
					{
						xy[i2].y = Mathf.Lerp(xy[corner].y, xy[i2].y, sin);
						xy[i3].y = xy[i2].y;
					}
				}
				else
				{
					cos = 1f;
					sin = 1f;
				}
				if (!invert)
				{
					xy[i3].x = Mathf.Lerp(xy[corner].x, xy[i2].x, cos);
					return;
				}
				xy[i].y = Mathf.Lerp(xy[corner].y, xy[i2].y, sin);
				return;
			}
			else
			{
				if (cos > sin)
				{
					sin /= cos;
					cos = 1f;
					if (!invert)
					{
						xy[i].y = Mathf.Lerp(xy[corner].y, xy[i2].y, sin);
						xy[i2].y = xy[i].y;
					}
				}
				else if (sin > cos)
				{
					cos /= sin;
					sin = 1f;
					if (invert)
					{
						xy[i2].x = Mathf.Lerp(xy[corner].x, xy[i2].x, cos);
						xy[i3].x = xy[i2].x;
					}
				}
				else
				{
					cos = 1f;
					sin = 1f;
				}
				if (invert)
				{
					xy[i3].y = Mathf.Lerp(xy[corner].y, xy[i2].y, sin);
					return;
				}
				xy[i].x = Mathf.Lerp(xy[corner].x, xy[i2].x, cos);
				return;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000092F6 File Offset: 0x000074F6
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00009300 File Offset: 0x00007500
		public virtual float preferredWidth
		{
			get
			{
				if (this.activeSprite == null)
				{
					return 0f;
				}
				if (this.type == Image.Type.Sliced || this.type == Image.Type.Tiled)
				{
					return DataUtility.GetMinSize(this.activeSprite).x / this.pixelsPerUnit;
				}
				return this.activeSprite.rect.size.x / this.pixelsPerUnit;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000936A File Offset: 0x0000756A
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000092F6 File Offset: 0x000074F6
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00009374 File Offset: 0x00007574
		public virtual float preferredHeight
		{
			get
			{
				if (this.activeSprite == null)
				{
					return 0f;
				}
				if (this.type == Image.Type.Sliced || this.type == Image.Type.Tiled)
				{
					return DataUtility.GetMinSize(this.activeSprite).y / this.pixelsPerUnit;
				}
				return this.activeSprite.rect.size.y / this.pixelsPerUnit;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000936A File Offset: 0x0000756A
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000093DE File Offset: 0x000075DE
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000093E4 File Offset: 0x000075E4
		public virtual bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
		{
			if (this.alphaHitTestMinimumThreshold <= 0f)
			{
				return true;
			}
			if (this.alphaHitTestMinimumThreshold > 1f)
			{
				return false;
			}
			if (this.activeSprite == null)
			{
				return true;
			}
			Vector2 local;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(base.rectTransform, screenPoint, eventCamera, out local))
			{
				return false;
			}
			Rect rect = base.GetPixelAdjustedRect();
			if (this.m_PreserveAspect)
			{
				this.PreserveSpriteAspectRatio(ref rect, new Vector2((float)this.activeSprite.texture.width, (float)this.activeSprite.texture.height));
			}
			local.x += base.rectTransform.pivot.x * rect.width;
			local.y += base.rectTransform.pivot.y * rect.height;
			local = this.MapCoordinate(local, rect);
			float x = local.x / (float)this.activeSprite.texture.width;
			float y = local.y / (float)this.activeSprite.texture.height;
			bool flag;
			try
			{
				flag = this.activeSprite.texture.GetPixelBilinear(x, y).a >= this.alphaHitTestMinimumThreshold;
			}
			catch (UnityException e)
			{
				Debug.LogError("Using alphaHitTestMinimumThreshold greater than 0 on Image whose sprite texture cannot be read. " + e.Message + " Also make sure to disable sprite packing for this sprite.", this);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000954C File Offset: 0x0000774C
		private Vector2 MapCoordinate(Vector2 local, Rect rect)
		{
			Rect spriteRect = this.activeSprite.rect;
			if (this.type == Image.Type.Simple || this.type == Image.Type.Filled)
			{
				return new Vector2(spriteRect.position.x + local.x * spriteRect.width / rect.width, spriteRect.position.y + local.y * spriteRect.height / rect.height);
			}
			Vector4 border = this.activeSprite.border;
			Vector4 adjustedBorder = this.GetAdjustedBorders(border / this.pixelsPerUnit, rect);
			for (int i = 0; i < 2; i++)
			{
				if (local[i] > adjustedBorder[i])
				{
					if (rect.size[i] - local[i] <= adjustedBorder[i + 2])
					{
						ref Vector2 ptr = ref local;
						int num = i;
						ptr[num] -= rect.size[i] - spriteRect.size[i];
					}
					else if (this.type == Image.Type.Sliced)
					{
						float lerp = Mathf.InverseLerp(adjustedBorder[i], rect.size[i] - adjustedBorder[i + 2], local[i]);
						local[i] = Mathf.Lerp(border[i], spriteRect.size[i] - border[i + 2], lerp);
					}
					else
					{
						ref Vector2 ptr = ref local;
						int num = i;
						ptr[num] -= adjustedBorder[i];
						local[i] = Mathf.Repeat(local[i], spriteRect.size[i] - border[i] - border[i + 2]);
						ptr = ref local;
						num = i;
						ptr[num] += border[i];
					}
				}
			}
			return local + spriteRect.position;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00009770 File Offset: 0x00007970
		private static void RebuildImage(SpriteAtlas spriteAtlas)
		{
			for (int i = Image.m_TrackedTexturelessImages.Count - 1; i >= 0; i--)
			{
				Image g = Image.m_TrackedTexturelessImages[i];
				if (null != g.activeSprite && spriteAtlas.CanBindTo(g.activeSprite))
				{
					g.SetAllDirty();
					Image.m_TrackedTexturelessImages.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000097CD File Offset: 0x000079CD
		private static void TrackImage(Image g)
		{
			if (!Image.s_Initialized)
			{
				SpriteAtlasManager.atlasRegistered += Image.RebuildImage;
				Image.s_Initialized = true;
			}
			Image.m_TrackedTexturelessImages.Add(g);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000097F8 File Offset: 0x000079F8
		private static void UnTrackImage(Image g)
		{
			Image.m_TrackedTexturelessImages.Remove(g);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00009806 File Offset: 0x00007A06
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetMaterialDirty();
			this.SetVerticesDirty();
			base.SetRaycastDirty();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00009858 File Offset: 0x00007A58
		[CompilerGenerated]
		private void <set_sprite>g__ResetAlphaHitThresholdIfNeeded|11_0()
		{
			if (!this.<set_sprite>g__SpriteSupportsAlphaHitTest|11_1() && this.m_AlphaHitTestMinimumThreshold > 0f)
			{
				Debug.LogWarning("Sprite was changed for one not readable or with Crunch Compression. Resetting the AlphaHitThreshold to 0.", this);
				this.m_AlphaHitTestMinimumThreshold = 0f;
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00009888 File Offset: 0x00007A88
		[CompilerGenerated]
		private bool <set_sprite>g__SpriteSupportsAlphaHitTest|11_1()
		{
			return this.m_Sprite != null && this.m_Sprite.texture != null && !GraphicsFormatUtility.IsCrunchFormat(this.m_Sprite.texture.format) && this.m_Sprite.texture.isReadable;
		}

		// Token: 0x04000096 RID: 150
		protected static Material s_ETC1DefaultUI = null;

		// Token: 0x04000097 RID: 151
		[FormerlySerializedAs("m_Frame")]
		[SerializeField]
		private Sprite m_Sprite;

		// Token: 0x04000098 RID: 152
		[NonSerialized]
		private Sprite m_OverrideSprite;

		// Token: 0x04000099 RID: 153
		[SerializeField]
		private Image.Type m_Type;

		// Token: 0x0400009A RID: 154
		[SerializeField]
		private bool m_PreserveAspect;

		// Token: 0x0400009B RID: 155
		[SerializeField]
		private bool m_FillCenter = true;

		// Token: 0x0400009C RID: 156
		[SerializeField]
		private Image.FillMethod m_FillMethod = Image.FillMethod.Radial360;

		// Token: 0x0400009D RID: 157
		[Range(0f, 1f)]
		[SerializeField]
		private float m_FillAmount = 1f;

		// Token: 0x0400009E RID: 158
		[SerializeField]
		private bool m_FillClockwise = true;

		// Token: 0x0400009F RID: 159
		[SerializeField]
		private int m_FillOrigin;

		// Token: 0x040000A0 RID: 160
		private float m_AlphaHitTestMinimumThreshold;

		// Token: 0x040000A1 RID: 161
		private bool m_Tracked;

		// Token: 0x040000A2 RID: 162
		[SerializeField]
		private bool m_UseSpriteMesh;

		// Token: 0x040000A3 RID: 163
		[SerializeField]
		private float m_PixelsPerUnitMultiplier = 1f;

		// Token: 0x040000A4 RID: 164
		private float m_CachedReferencePixelsPerUnit = 100f;

		// Token: 0x040000A5 RID: 165
		private static readonly Vector2[] s_VertScratch = new Vector2[4];

		// Token: 0x040000A6 RID: 166
		private static readonly Vector2[] s_UVScratch = new Vector2[4];

		// Token: 0x040000A7 RID: 167
		private static readonly Vector3[] s_Xy = new Vector3[4];

		// Token: 0x040000A8 RID: 168
		private static readonly Vector3[] s_Uv = new Vector3[4];

		// Token: 0x040000A9 RID: 169
		private static List<Image> m_TrackedTexturelessImages = new List<Image>();

		// Token: 0x040000AA RID: 170
		private static bool s_Initialized;

		// Token: 0x02000027 RID: 39
		public enum Type
		{
			// Token: 0x040000AC RID: 172
			Simple,
			// Token: 0x040000AD RID: 173
			Sliced,
			// Token: 0x040000AE RID: 174
			Tiled,
			// Token: 0x040000AF RID: 175
			Filled
		}

		// Token: 0x02000028 RID: 40
		public enum FillMethod
		{
			// Token: 0x040000B1 RID: 177
			Horizontal,
			// Token: 0x040000B2 RID: 178
			Vertical,
			// Token: 0x040000B3 RID: 179
			Radial90,
			// Token: 0x040000B4 RID: 180
			Radial180,
			// Token: 0x040000B5 RID: 181
			Radial360
		}

		// Token: 0x02000029 RID: 41
		public enum OriginHorizontal
		{
			// Token: 0x040000B7 RID: 183
			Left,
			// Token: 0x040000B8 RID: 184
			Right
		}

		// Token: 0x0200002A RID: 42
		public enum OriginVertical
		{
			// Token: 0x040000BA RID: 186
			Bottom,
			// Token: 0x040000BB RID: 187
			Top
		}

		// Token: 0x0200002B RID: 43
		public enum Origin90
		{
			// Token: 0x040000BD RID: 189
			BottomLeft,
			// Token: 0x040000BE RID: 190
			TopLeft,
			// Token: 0x040000BF RID: 191
			TopRight,
			// Token: 0x040000C0 RID: 192
			BottomRight
		}

		// Token: 0x0200002C RID: 44
		public enum Origin180
		{
			// Token: 0x040000C2 RID: 194
			Bottom,
			// Token: 0x040000C3 RID: 195
			Left,
			// Token: 0x040000C4 RID: 196
			Top,
			// Token: 0x040000C5 RID: 197
			Right
		}

		// Token: 0x0200002D RID: 45
		public enum Origin360
		{
			// Token: 0x040000C7 RID: 199
			Bottom,
			// Token: 0x040000C8 RID: 200
			Right,
			// Token: 0x040000C9 RID: 201
			Top,
			// Token: 0x040000CA RID: 202
			Left
		}
	}
}
