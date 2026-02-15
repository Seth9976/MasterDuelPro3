using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x020000B2 RID: 178
	internal struct WordWrapState
	{
		// Token: 0x040005DB RID: 1499
		public int previous_WordBreak;

		// Token: 0x040005DC RID: 1500
		public int total_CharacterCount;

		// Token: 0x040005DD RID: 1501
		public int visible_CharacterCount;

		// Token: 0x040005DE RID: 1502
		public int visibleSpaceCount;

		// Token: 0x040005DF RID: 1503
		public int visible_SpriteCount;

		// Token: 0x040005E0 RID: 1504
		public int visible_LinkCount;

		// Token: 0x040005E1 RID: 1505
		public int firstCharacterIndex;

		// Token: 0x040005E2 RID: 1506
		public int firstVisibleCharacterIndex;

		// Token: 0x040005E3 RID: 1507
		public int lastCharacterIndex;

		// Token: 0x040005E4 RID: 1508
		public int lastVisibleCharIndex;

		// Token: 0x040005E5 RID: 1509
		public int lineNumber;

		// Token: 0x040005E6 RID: 1510
		public float maxCapHeight;

		// Token: 0x040005E7 RID: 1511
		public float maxAscender;

		// Token: 0x040005E8 RID: 1512
		public float maxDescender;

		// Token: 0x040005E9 RID: 1513
		public float startOfLineAscender;

		// Token: 0x040005EA RID: 1514
		public float maxLineAscender;

		// Token: 0x040005EB RID: 1515
		public float maxLineDescender;

		// Token: 0x040005EC RID: 1516
		public float pageAscender;

		// Token: 0x040005ED RID: 1517
		public HorizontalAlignmentOptions horizontalAlignment;

		// Token: 0x040005EE RID: 1518
		public float marginLeft;

		// Token: 0x040005EF RID: 1519
		public float marginRight;

		// Token: 0x040005F0 RID: 1520
		public float xAdvance;

		// Token: 0x040005F1 RID: 1521
		public float preferredWidth;

		// Token: 0x040005F2 RID: 1522
		public float preferredHeight;

		// Token: 0x040005F3 RID: 1523
		public float renderedWidth;

		// Token: 0x040005F4 RID: 1524
		public float renderedHeight;

		// Token: 0x040005F5 RID: 1525
		public float previousLineScale;

		// Token: 0x040005F6 RID: 1526
		public int wordCount;

		// Token: 0x040005F7 RID: 1527
		public FontStyles fontStyle;

		// Token: 0x040005F8 RID: 1528
		public int italicAngle;

		// Token: 0x040005F9 RID: 1529
		public float fontScaleMultiplier;

		// Token: 0x040005FA RID: 1530
		public float currentFontSize;

		// Token: 0x040005FB RID: 1531
		public float baselineOffset;

		// Token: 0x040005FC RID: 1532
		public float lineOffset;

		// Token: 0x040005FD RID: 1533
		public bool isDrivenLineSpacing;

		// Token: 0x040005FE RID: 1534
		public int lastBaseGlyphIndex;

		// Token: 0x040005FF RID: 1535
		public float cSpace;

		// Token: 0x04000600 RID: 1536
		public float mSpace;

		// Token: 0x04000601 RID: 1537
		public TMP_TextInfo textInfo;

		// Token: 0x04000602 RID: 1538
		public TMP_LineInfo lineInfo;

		// Token: 0x04000603 RID: 1539
		public Color32 vertexColor;

		// Token: 0x04000604 RID: 1540
		public Color32 underlineColor;

		// Token: 0x04000605 RID: 1541
		public Color32 strikethroughColor;

		// Token: 0x04000606 RID: 1542
		public HighlightState highlightState;

		// Token: 0x04000607 RID: 1543
		public TMP_FontStyleStack basicStyleStack;

		// Token: 0x04000608 RID: 1544
		public TMP_TextProcessingStack<int> italicAngleStack;

		// Token: 0x04000609 RID: 1545
		public TMP_TextProcessingStack<Color32> colorStack;

		// Token: 0x0400060A RID: 1546
		public TMP_TextProcessingStack<Color32> underlineColorStack;

		// Token: 0x0400060B RID: 1547
		public TMP_TextProcessingStack<Color32> strikethroughColorStack;

		// Token: 0x0400060C RID: 1548
		public TMP_TextProcessingStack<Color32> highlightColorStack;

		// Token: 0x0400060D RID: 1549
		public TMP_TextProcessingStack<HighlightState> highlightStateStack;

		// Token: 0x0400060E RID: 1550
		public TMP_TextProcessingStack<TMP_ColorGradient> colorGradientStack;

		// Token: 0x0400060F RID: 1551
		public TMP_TextProcessingStack<float> sizeStack;

		// Token: 0x04000610 RID: 1552
		public TMP_TextProcessingStack<float> indentStack;

		// Token: 0x04000611 RID: 1553
		public TMP_TextProcessingStack<FontWeight> fontWeightStack;

		// Token: 0x04000612 RID: 1554
		public TMP_TextProcessingStack<int> styleStack;

		// Token: 0x04000613 RID: 1555
		public TMP_TextProcessingStack<float> baselineStack;

		// Token: 0x04000614 RID: 1556
		public TMP_TextProcessingStack<int> actionStack;

		// Token: 0x04000615 RID: 1557
		public TMP_TextProcessingStack<MaterialReference> materialReferenceStack;

		// Token: 0x04000616 RID: 1558
		public TMP_TextProcessingStack<HorizontalAlignmentOptions> lineJustificationStack;

		// Token: 0x04000617 RID: 1559
		public int spriteAnimationID;

		// Token: 0x04000618 RID: 1560
		public TMP_FontAsset currentFontAsset;

		// Token: 0x04000619 RID: 1561
		public TMP_SpriteAsset currentSpriteAsset;

		// Token: 0x0400061A RID: 1562
		public Material currentMaterial;

		// Token: 0x0400061B RID: 1563
		public int currentMaterialIndex;

		// Token: 0x0400061C RID: 1564
		public Extents meshExtents;

		// Token: 0x0400061D RID: 1565
		public bool tagNoParsing;

		// Token: 0x0400061E RID: 1566
		public bool isNonBreakingSpace;

		// Token: 0x0400061F RID: 1567
		public Quaternion fxRotation;

		// Token: 0x04000620 RID: 1568
		public Vector3 fxScale;
	}
}
