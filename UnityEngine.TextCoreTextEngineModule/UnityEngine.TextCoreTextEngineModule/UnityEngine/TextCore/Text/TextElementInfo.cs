using System;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200003F RID: 63
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal struct TextElementInfo
	{
		// Token: 0x0600019E RID: 414 RVA: 0x0000B730 File Offset: 0x00009930
		public override string ToString()
		{
			return string.Format("{0}: {1}\n{2}: {3}\n{4}: {5}\n{6}: {7}\n{8}: {9}\n{10}: {11}\n{12}: {13}\n{14}: {15}\n{16}: {17}\n{18}: {19}\n{20}: {21}\n{22}: {23}\n{24}: {25}\n{26}: {27}\n{28}: {29}\n{30}: {31}\n{32}: {33}\n{34}: {35}\n{36}: {37}\n{38}: {39}\n{40}: {41}\n{42}: {43}\n{44}: {45}\n{46}: {47}\n{48}: {49}\n{50}: {51}\n{52}: {53}\n{54}: {55}\n{56}: {57}\n{58}: {59}\n{60}: {61}\n{62}: {63}\n{64}: {65}\n{66}: {67}\n{68}: {69}\n{70}: {71}\n{72}: {73}\n{74}: {75}\n{76}: {77}\n{78}: {79}\n{80}: {81}\n{82}: {83}\n{84}: {85}", new object[]
			{
				"character", this.character, "index", this.index, "elementType", this.elementType, "stringLength", this.stringLength, "textElement", this.textElement,
				"alternativeGlyph", this.alternativeGlyph, "fontAsset", this.fontAsset, "spriteAsset", this.spriteAsset, "spriteIndex", this.spriteIndex, "material", this.material,
				"materialReferenceIndex", this.materialReferenceIndex, "isUsingAlternateTypeface", this.isUsingAlternateTypeface, "pointSize", this.pointSize, "lineNumber", this.lineNumber, "pageNumber", this.pageNumber,
				"vertexIndex", this.vertexIndex, "vertexTopLeft", this.vertexTopLeft, "vertexBottomLeft", this.vertexBottomLeft, "vertexTopRight", this.vertexTopRight, "vertexBottomRight", this.vertexBottomRight,
				"topLeft", this.topLeft, "bottomLeft", this.bottomLeft, "topRight", this.topRight, "bottomRight", this.bottomRight, "origin", this.origin,
				"ascender", this.ascender, "baseLine", this.baseLine, "descender", this.descender, "adjustedAscender", this.adjustedAscender, "adjustedDescender", this.adjustedDescender,
				"adjustedHorizontalAdvance", this.adjustedHorizontalAdvance, "xAdvance", this.xAdvance, "aspectRatio", this.aspectRatio, "scale", this.scale, "color", this.color,
				"underlineColor", this.underlineColor, "underlineVertexIndex", this.underlineVertexIndex, "strikethroughColor", this.strikethroughColor, "strikethroughVertexIndex", this.strikethroughVertexIndex, "highlightColor", this.highlightColor,
				"highlightState", this.highlightState, "style", this.style, "isVisible", this.isVisible
			});
		}

		// Token: 0x04000198 RID: 408
		public uint character;

		// Token: 0x04000199 RID: 409
		public int index;

		// Token: 0x0400019A RID: 410
		public TextElementType elementType;

		// Token: 0x0400019B RID: 411
		public int stringLength;

		// Token: 0x0400019C RID: 412
		public TextElement textElement;

		// Token: 0x0400019D RID: 413
		public Glyph alternativeGlyph;

		// Token: 0x0400019E RID: 414
		public FontAsset fontAsset;

		// Token: 0x0400019F RID: 415
		public SpriteAsset spriteAsset;

		// Token: 0x040001A0 RID: 416
		public int spriteIndex;

		// Token: 0x040001A1 RID: 417
		public Material material;

		// Token: 0x040001A2 RID: 418
		public int materialReferenceIndex;

		// Token: 0x040001A3 RID: 419
		public bool isUsingAlternateTypeface;

		// Token: 0x040001A4 RID: 420
		public float pointSize;

		// Token: 0x040001A5 RID: 421
		public int lineNumber;

		// Token: 0x040001A6 RID: 422
		public int pageNumber;

		// Token: 0x040001A7 RID: 423
		public int vertexIndex;

		// Token: 0x040001A8 RID: 424
		public TextVertex vertexTopLeft;

		// Token: 0x040001A9 RID: 425
		public TextVertex vertexBottomLeft;

		// Token: 0x040001AA RID: 426
		public TextVertex vertexTopRight;

		// Token: 0x040001AB RID: 427
		public TextVertex vertexBottomRight;

		// Token: 0x040001AC RID: 428
		public Vector3 topLeft;

		// Token: 0x040001AD RID: 429
		public Vector3 bottomLeft;

		// Token: 0x040001AE RID: 430
		public Vector3 topRight;

		// Token: 0x040001AF RID: 431
		public Vector3 bottomRight;

		// Token: 0x040001B0 RID: 432
		public float origin;

		// Token: 0x040001B1 RID: 433
		public float ascender;

		// Token: 0x040001B2 RID: 434
		public float baseLine;

		// Token: 0x040001B3 RID: 435
		public float descender;

		// Token: 0x040001B4 RID: 436
		internal float adjustedAscender;

		// Token: 0x040001B5 RID: 437
		internal float adjustedDescender;

		// Token: 0x040001B6 RID: 438
		internal float adjustedHorizontalAdvance;

		// Token: 0x040001B7 RID: 439
		public float xAdvance;

		// Token: 0x040001B8 RID: 440
		public float aspectRatio;

		// Token: 0x040001B9 RID: 441
		public float scale;

		// Token: 0x040001BA RID: 442
		public Color32 color;

		// Token: 0x040001BB RID: 443
		public Color32 underlineColor;

		// Token: 0x040001BC RID: 444
		public int underlineVertexIndex;

		// Token: 0x040001BD RID: 445
		public Color32 strikethroughColor;

		// Token: 0x040001BE RID: 446
		public int strikethroughVertexIndex;

		// Token: 0x040001BF RID: 447
		public Color32 highlightColor;

		// Token: 0x040001C0 RID: 448
		public HighlightState highlightState;

		// Token: 0x040001C1 RID: 449
		public FontStyles style;

		// Token: 0x040001C2 RID: 450
		public bool isVisible;
	}
}
