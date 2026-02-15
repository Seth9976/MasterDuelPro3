using System;
using System.Diagnostics;

namespace TMPro
{
	// Token: 0x0200009C RID: 156
	[DebuggerDisplay("{DebuggerDisplay()}")]
	internal struct TextProcessingElement
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0002BBBB File Offset: 0x00029DBB
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x0002BBC3 File Offset: 0x00029DC3
		public TextProcessingElementType ElementType
		{
			get
			{
				return this.m_ElementType;
			}
			set
			{
				this.m_ElementType = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0002BBCC File Offset: 0x00029DCC
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x0002BBD4 File Offset: 0x00029DD4
		public int StartIndex
		{
			get
			{
				return this.m_StartIndex;
			}
			set
			{
				this.m_StartIndex = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0002BBDD File Offset: 0x00029DDD
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x0002BBE5 File Offset: 0x00029DE5
		public int Length
		{
			get
			{
				return this.m_Length;
			}
			set
			{
				this.m_Length = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0002BBEE File Offset: 0x00029DEE
		public CharacterElement CharacterElement
		{
			get
			{
				return this.m_CharacterElement;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0002BBF6 File Offset: 0x00029DF6
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x0002BBFE File Offset: 0x00029DFE
		public MarkupElement MarkupElement
		{
			get
			{
				return this.m_MarkupElement;
			}
			set
			{
				this.m_MarkupElement = value;
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0002BC07 File Offset: 0x00029E07
		public TextProcessingElement(TextProcessingElementType elementType, int startIndex, int length)
		{
			this.m_ElementType = elementType;
			this.m_StartIndex = startIndex;
			this.m_Length = length;
			this.m_CharacterElement = default(CharacterElement);
			this.m_MarkupElement = default(MarkupElement);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0002BC36 File Offset: 0x00029E36
		public TextProcessingElement(TMP_TextElement textElement, int startIndex, int length)
		{
			this.m_ElementType = TextProcessingElementType.TextCharacterElement;
			this.m_StartIndex = startIndex;
			this.m_Length = length;
			this.m_CharacterElement = new CharacterElement(textElement);
			this.m_MarkupElement = default(MarkupElement);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0002BC65 File Offset: 0x00029E65
		public TextProcessingElement(CharacterElement characterElement, int startIndex, int length)
		{
			this.m_ElementType = TextProcessingElementType.TextCharacterElement;
			this.m_StartIndex = startIndex;
			this.m_Length = length;
			this.m_CharacterElement = characterElement;
			this.m_MarkupElement = default(MarkupElement);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0002BC8F File Offset: 0x00029E8F
		public TextProcessingElement(MarkupElement markupElement)
		{
			this.m_ElementType = TextProcessingElementType.TextMarkupElement;
			this.m_StartIndex = markupElement.ValueStartIndex;
			this.m_Length = markupElement.ValueLength;
			this.m_CharacterElement = default(CharacterElement);
			this.m_MarkupElement = markupElement;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0002BCC8 File Offset: 0x00029EC8
		public static TextProcessingElement Undefined
		{
			get
			{
				return new TextProcessingElement
				{
					ElementType = TextProcessingElementType.Undefined
				};
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0002BCE8 File Offset: 0x00029EE8
		private string DebuggerDisplay()
		{
			if (this.m_ElementType != TextProcessingElementType.TextCharacterElement)
			{
				return string.Format("Markup = {0}", (MarkupTag)this.m_MarkupElement.NameHashCode);
			}
			return string.Format("Unicode ({0})   '{1}' ", this.m_CharacterElement.Unicode, (char)this.m_CharacterElement.Unicode);
		}

		// Token: 0x04000568 RID: 1384
		private TextProcessingElementType m_ElementType;

		// Token: 0x04000569 RID: 1385
		private int m_StartIndex;

		// Token: 0x0400056A RID: 1386
		private int m_Length;

		// Token: 0x0400056B RID: 1387
		private CharacterElement m_CharacterElement;

		// Token: 0x0400056C RID: 1388
		private MarkupElement m_MarkupElement;
	}
}
