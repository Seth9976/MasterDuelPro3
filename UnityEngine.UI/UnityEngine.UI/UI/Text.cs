using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000074 RID: 116
	[RequireComponent(typeof(CanvasRenderer))]
	[AddComponentMenu("UI/Legacy/Text", 100)]
	public class Text : MaskableGraphic, ILayoutElement
	{
		// Token: 0x060004A3 RID: 1187 RVA: 0x0001558D File Offset: 0x0001378D
		protected Text()
		{
			base.useLegacyMeshGeneration = false;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x000155C0 File Offset: 0x000137C0
		public TextGenerator cachedTextGenerator
		{
			get
			{
				TextGenerator textGenerator;
				if ((textGenerator = this.m_TextCache) == null)
				{
					textGenerator = (this.m_TextCache = ((this.m_Text.Length != 0) ? new TextGenerator(this.m_Text.Length) : new TextGenerator()));
				}
				return textGenerator;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00015604 File Offset: 0x00013804
		public TextGenerator cachedTextGeneratorForLayout
		{
			get
			{
				TextGenerator textGenerator;
				if ((textGenerator = this.m_TextCacheForLayout) == null)
				{
					textGenerator = (this.m_TextCacheForLayout = new TextGenerator());
				}
				return textGenerator;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0001562C File Offset: 0x0001382C
		public override Texture mainTexture
		{
			get
			{
				if (this.font != null && this.font.material != null && this.font.material.mainTexture != null)
				{
					return this.font.material.mainTexture;
				}
				if (this.m_Material != null)
				{
					return this.m_Material.mainTexture;
				}
				return base.mainTexture;
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000156A4 File Offset: 0x000138A4
		public void FontTextureChanged()
		{
			if (!this)
			{
				return;
			}
			if (this.m_DisableFontTextureRebuiltCallback)
			{
				return;
			}
			this.cachedTextGenerator.Invalidate();
			if (!this.IsActive())
			{
				return;
			}
			if (CanvasUpdateRegistry.IsRebuildingGraphics() || CanvasUpdateRegistry.IsRebuildingLayout())
			{
				this.UpdateGeometry();
				return;
			}
			this.SetAllDirty();
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000156F2 File Offset: 0x000138F2
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x00015700 File Offset: 0x00013900
		public Font font
		{
			get
			{
				return this.m_FontData.font;
			}
			set
			{
				if (this.m_FontData.font == value)
				{
					return;
				}
				if (base.isActiveAndEnabled)
				{
					FontUpdateTracker.UntrackText(this);
				}
				this.m_FontData.font = value;
				if (base.isActiveAndEnabled)
				{
					FontUpdateTracker.TrackText(this);
				}
				this.SetAllDirty();
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001574F File Offset: 0x0001394F
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x00015758 File Offset: 0x00013958
		public virtual string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					if (this.m_Text != value)
					{
						this.m_Text = value;
						this.SetVerticesDirty();
						this.SetLayoutDirty();
					}
					return;
				}
				if (string.IsNullOrEmpty(this.m_Text))
				{
					return;
				}
				this.m_Text = "";
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x000157AE File Offset: 0x000139AE
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x000157BB File Offset: 0x000139BB
		public bool supportRichText
		{
			get
			{
				return this.m_FontData.richText;
			}
			set
			{
				if (this.m_FontData.richText == value)
				{
					return;
				}
				this.m_FontData.richText = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x000157E4 File Offset: 0x000139E4
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x000157F1 File Offset: 0x000139F1
		public bool resizeTextForBestFit
		{
			get
			{
				return this.m_FontData.bestFit;
			}
			set
			{
				if (this.m_FontData.bestFit == value)
				{
					return;
				}
				this.m_FontData.bestFit = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0001581A File Offset: 0x00013A1A
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00015827 File Offset: 0x00013A27
		public int resizeTextMinSize
		{
			get
			{
				return this.m_FontData.minSize;
			}
			set
			{
				if (this.m_FontData.minSize == value)
				{
					return;
				}
				this.m_FontData.minSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00015850 File Offset: 0x00013A50
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0001585D File Offset: 0x00013A5D
		public int resizeTextMaxSize
		{
			get
			{
				return this.m_FontData.maxSize;
			}
			set
			{
				if (this.m_FontData.maxSize == value)
				{
					return;
				}
				this.m_FontData.maxSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00015886 File Offset: 0x00013A86
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x00015893 File Offset: 0x00013A93
		public TextAnchor alignment
		{
			get
			{
				return this.m_FontData.alignment;
			}
			set
			{
				if (this.m_FontData.alignment == value)
				{
					return;
				}
				this.m_FontData.alignment = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000158BC File Offset: 0x00013ABC
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x000158C9 File Offset: 0x00013AC9
		public bool alignByGeometry
		{
			get
			{
				return this.m_FontData.alignByGeometry;
			}
			set
			{
				if (this.m_FontData.alignByGeometry == value)
				{
					return;
				}
				this.m_FontData.alignByGeometry = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x000158EC File Offset: 0x00013AEC
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x000158F9 File Offset: 0x00013AF9
		public int fontSize
		{
			get
			{
				return this.m_FontData.fontSize;
			}
			set
			{
				if (this.m_FontData.fontSize == value)
				{
					return;
				}
				this.m_FontData.fontSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00015922 File Offset: 0x00013B22
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x0001592F File Offset: 0x00013B2F
		public HorizontalWrapMode horizontalOverflow
		{
			get
			{
				return this.m_FontData.horizontalOverflow;
			}
			set
			{
				if (this.m_FontData.horizontalOverflow == value)
				{
					return;
				}
				this.m_FontData.horizontalOverflow = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00015958 File Offset: 0x00013B58
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x00015965 File Offset: 0x00013B65
		public VerticalWrapMode verticalOverflow
		{
			get
			{
				return this.m_FontData.verticalOverflow;
			}
			set
			{
				if (this.m_FontData.verticalOverflow == value)
				{
					return;
				}
				this.m_FontData.verticalOverflow = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0001598E File Offset: 0x00013B8E
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x0001599B File Offset: 0x00013B9B
		public float lineSpacing
		{
			get
			{
				return this.m_FontData.lineSpacing;
			}
			set
			{
				if (this.m_FontData.lineSpacing == value)
				{
					return;
				}
				this.m_FontData.lineSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x000159C4 File Offset: 0x00013BC4
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x000159D1 File Offset: 0x00013BD1
		public FontStyle fontStyle
		{
			get
			{
				return this.m_FontData.fontStyle;
			}
			set
			{
				if (this.m_FontData.fontStyle == value)
				{
					return;
				}
				this.m_FontData.fontStyle = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x000159FC File Offset: 0x00013BFC
		public float pixelsPerUnit
		{
			get
			{
				Canvas localCanvas = base.canvas;
				if (!localCanvas)
				{
					return 1f;
				}
				if (!this.font || this.font.dynamic)
				{
					return localCanvas.scaleFactor;
				}
				if (this.m_FontData.fontSize <= 0 || this.font.fontSize <= 0)
				{
					return 1f;
				}
				return (float)this.font.fontSize / (float)this.m_FontData.fontSize;
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00015A7A File Offset: 0x00013C7A
		protected override void OnEnable()
		{
			base.OnEnable();
			this.cachedTextGenerator.Invalidate();
			FontUpdateTracker.TrackText(this);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00015A93 File Offset: 0x00013C93
		protected override void OnDisable()
		{
			FontUpdateTracker.UntrackText(this);
			base.OnDisable();
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00015AA1 File Offset: 0x00013CA1
		protected override void UpdateGeometry()
		{
			if (this.font != null)
			{
				base.UpdateGeometry();
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00015AB7 File Offset: 0x00013CB7
		internal void AssignDefaultFont()
		{
			this.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00015AC9 File Offset: 0x00013CC9
		internal void AssignDefaultFontIfNecessary()
		{
			if (this.font == null)
			{
				this.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00015AEC File Offset: 0x00013CEC
		public TextGenerationSettings GetGenerationSettings(Vector2 extents)
		{
			TextGenerationSettings settings = default(TextGenerationSettings);
			settings.generationExtents = extents;
			if (this.font != null && this.font.dynamic)
			{
				settings.fontSize = this.m_FontData.fontSize;
				settings.resizeTextMinSize = this.m_FontData.minSize;
				settings.resizeTextMaxSize = this.m_FontData.maxSize;
			}
			settings.textAnchor = this.m_FontData.alignment;
			settings.alignByGeometry = this.m_FontData.alignByGeometry;
			settings.scaleFactor = this.pixelsPerUnit;
			settings.color = this.color;
			settings.font = this.font;
			settings.pivot = base.rectTransform.pivot;
			settings.richText = this.m_FontData.richText;
			settings.lineSpacing = this.m_FontData.lineSpacing;
			settings.fontStyle = this.m_FontData.fontStyle;
			settings.resizeTextForBestFit = this.m_FontData.bestFit;
			settings.updateBounds = false;
			settings.horizontalOverflow = this.m_FontData.horizontalOverflow;
			settings.verticalOverflow = this.m_FontData.verticalOverflow;
			return settings;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00015C2C File Offset: 0x00013E2C
		public static Vector2 GetTextAnchorPivot(TextAnchor anchor)
		{
			switch (anchor)
			{
			case TextAnchor.UpperLeft:
				return new Vector2(0f, 1f);
			case TextAnchor.UpperCenter:
				return new Vector2(0.5f, 1f);
			case TextAnchor.UpperRight:
				return new Vector2(1f, 1f);
			case TextAnchor.MiddleLeft:
				return new Vector2(0f, 0.5f);
			case TextAnchor.MiddleCenter:
				return new Vector2(0.5f, 0.5f);
			case TextAnchor.MiddleRight:
				return new Vector2(1f, 0.5f);
			case TextAnchor.LowerLeft:
				return new Vector2(0f, 0f);
			case TextAnchor.LowerCenter:
				return new Vector2(0.5f, 0f);
			case TextAnchor.LowerRight:
				return new Vector2(1f, 0f);
			default:
				return Vector2.zero;
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00015D00 File Offset: 0x00013F00
		protected override void OnPopulateMesh(VertexHelper toFill)
		{
			if (this.font == null)
			{
				return;
			}
			this.m_DisableFontTextureRebuiltCallback = true;
			Vector2 extents = base.rectTransform.rect.size;
			TextGenerationSettings settings = this.GetGenerationSettings(extents);
			this.cachedTextGenerator.PopulateWithErrors(this.text, settings, base.gameObject);
			IList<UIVertex> verts = this.cachedTextGenerator.verts;
			float unitsPerPixel = 1f / this.pixelsPerUnit;
			int vertCount = verts.Count;
			if (vertCount <= 0)
			{
				toFill.Clear();
				return;
			}
			Vector2 roundingOffset = new Vector2(verts[0].position.x, verts[0].position.y) * unitsPerPixel;
			roundingOffset = base.PixelAdjustPoint(roundingOffset) - roundingOffset;
			toFill.Clear();
			if (roundingOffset != Vector2.zero)
			{
				for (int i = 0; i < vertCount; i++)
				{
					int tempVertsIndex = i & 3;
					this.m_TempVerts[tempVertsIndex] = verts[i];
					UIVertex[] tempVerts = this.m_TempVerts;
					int num = tempVertsIndex;
					tempVerts[num].position = tempVerts[num].position * unitsPerPixel;
					UIVertex[] tempVerts2 = this.m_TempVerts;
					int num2 = tempVertsIndex;
					tempVerts2[num2].position.x = tempVerts2[num2].position.x + roundingOffset.x;
					UIVertex[] tempVerts3 = this.m_TempVerts;
					int num3 = tempVertsIndex;
					tempVerts3[num3].position.y = tempVerts3[num3].position.y + roundingOffset.y;
					if (tempVertsIndex == 3)
					{
						toFill.AddUIVertexQuad(this.m_TempVerts);
					}
				}
			}
			else
			{
				for (int j = 0; j < vertCount; j++)
				{
					int tempVertsIndex2 = j & 3;
					this.m_TempVerts[tempVertsIndex2] = verts[j];
					UIVertex[] tempVerts4 = this.m_TempVerts;
					int num4 = tempVertsIndex2;
					tempVerts4[num4].position = tempVerts4[num4].position * unitsPerPixel;
					if (tempVertsIndex2 == 3)
					{
						toFill.AddUIVertexQuad(this.m_TempVerts);
					}
				}
			}
			this.m_DisableFontTextureRebuiltCallback = false;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x000092F6 File Offset: 0x000074F6
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00015EF0 File Offset: 0x000140F0
		public virtual float preferredWidth
		{
			get
			{
				TextGenerationSettings settings = this.GetGenerationSettings(Vector2.zero);
				return this.cachedTextGeneratorForLayout.GetPreferredWidth(this.m_Text, settings) / this.pixelsPerUnit;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000936A File Offset: 0x0000756A
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x000092F6 File Offset: 0x000074F6
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00015F24 File Offset: 0x00014124
		public virtual float preferredHeight
		{
			get
			{
				TextGenerationSettings settings = this.GetGenerationSettings(new Vector2(base.GetPixelAdjustedRect().size.x, 0f));
				return this.cachedTextGeneratorForLayout.GetPreferredHeight(this.m_Text, settings) / this.pixelsPerUnit;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000936A File Offset: 0x0000756A
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x000093DE File Offset: 0x000075DE
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04000247 RID: 583
		[SerializeField]
		private FontData m_FontData = FontData.defaultFontData;

		// Token: 0x04000248 RID: 584
		[TextArea(3, 10)]
		[SerializeField]
		protected string m_Text = string.Empty;

		// Token: 0x04000249 RID: 585
		private TextGenerator m_TextCache;

		// Token: 0x0400024A RID: 586
		private TextGenerator m_TextCacheForLayout;

		// Token: 0x0400024B RID: 587
		protected static Material s_DefaultText;

		// Token: 0x0400024C RID: 588
		[NonSerialized]
		protected bool m_DisableFontTextureRebuiltCallback;

		// Token: 0x0400024D RID: 589
		private readonly UIVertex[] m_TempVerts = new UIVertex[4];
	}
}
