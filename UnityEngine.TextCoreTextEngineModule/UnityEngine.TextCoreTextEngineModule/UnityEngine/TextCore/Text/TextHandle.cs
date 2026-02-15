using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000058 RID: 88
	[DebuggerDisplay("{settings.text}")]
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal class TextHandle
	{
		// Token: 0x0600024D RID: 589 RVA: 0x0002C690 File Offset: 0x0002A890
		~TextHandle()
		{
			this.RemoveTextInfoFromTemporaryCache();
			this.RemoveTextInfoFromPermanentCache();
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0002C6C8 File Offset: 0x0002A8C8
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal static void InitThreadArrays()
		{
			bool flag = TextHandle.s_Settings != null && TextHandle.s_Generators != null && TextHandle.s_TextInfosCommon != null;
			if (!flag)
			{
				TextHandle.InitArray<TextGenerationSettings>(ref TextHandle.s_Settings, () => new TextGenerationSettings());
				TextHandle.InitArray<TextGenerator>(ref TextHandle.s_Generators, () => new TextGenerator());
				TextHandle.InitArray<TextInfo>(ref TextHandle.s_TextInfosCommon, () => new TextInfo(VertexDataLayout.VBO));
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0002C774 File Offset: 0x0002A974
		internal static TextGenerationSettings[] settingsArray
		{
			get
			{
				bool flag = TextHandle.s_Settings == null;
				if (flag)
				{
					TextHandle.InitArray<TextGenerationSettings>(ref TextHandle.s_Settings, () => new TextGenerationSettings());
				}
				return TextHandle.s_Settings;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0002C7C4 File Offset: 0x0002A9C4
		internal static TextGenerator[] generators
		{
			get
			{
				bool flag = TextHandle.s_Generators == null;
				if (flag)
				{
					TextHandle.InitArray<TextGenerator>(ref TextHandle.s_Generators, () => new TextGenerator());
				}
				return TextHandle.s_Generators;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0002C814 File Offset: 0x0002AA14
		internal static TextInfo[] textInfosCommon
		{
			get
			{
				bool flag = TextHandle.s_TextInfosCommon == null;
				if (flag)
				{
					TextHandle.InitArray<TextInfo>(ref TextHandle.s_TextInfosCommon, () => new TextInfo(VertexDataLayout.VBO));
				}
				return TextHandle.s_TextInfosCommon;
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0002C864 File Offset: 0x0002AA64
		private static void InitArray<T>(ref T[] array, Func<T> createInstance)
		{
			bool flag = array != null;
			if (!flag)
			{
				array = new T[JobsUtility.ThreadIndexCount];
				for (int i = 0; i < JobsUtility.ThreadIndexCount; i++)
				{
					array[i] = createInstance();
				}
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0002C8AD File Offset: 0x0002AAAD
		internal static TextInfo textInfoCommon
		{
			get
			{
				return TextHandle.textInfosCommon[JobsUtility.ThreadIndex];
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0002C8BA File Offset: 0x0002AABA
		private static TextGenerator generator
		{
			get
			{
				return TextHandle.generators[JobsUtility.ThreadIndex];
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0002C8C7 File Offset: 0x0002AAC7
		internal static TextGenerationSettings settings
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
			get
			{
				return TextHandle.settingsArray[JobsUtility.ThreadIndex];
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0002C8D4 File Offset: 0x0002AAD4
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0002C8DC File Offset: 0x0002AADC
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal Vector2 preferredSize { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0002C8E5 File Offset: 0x0002AAE5
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0002C8ED File Offset: 0x0002AAED
		internal LinkedListNode<TextInfo> TextInfoNode { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0002C8F6 File Offset: 0x0002AAF6
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0002C8FE File Offset: 0x0002AAFE
		internal bool IsCachedPermanent { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0002C907 File Offset: 0x0002AB07
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0002C90F File Offset: 0x0002AB0F
		internal bool IsCachedTemporary { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0002C918 File Offset: 0x0002AB18
		internal bool useAdvancedText
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
			get
			{
				return this.IsAdvancedTextEnabledForElement();
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0002C930 File Offset: 0x0002AB30
		internal int characterCount
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
			get
			{
				return this.useAdvancedText ? this.nativeSettings.text.Length : this.textInfo.characterCount;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0002C968 File Offset: 0x0002AB68
		protected internal static TextLib TextLib
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				bool flag = TextHandle.s_TextLib == null;
				if (flag)
				{
					TextHandle.s_TextLib = new TextLib();
				}
				return TextHandle.s_TextLib;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0002C998 File Offset: 0x0002AB98
		public virtual void AddTextInfoToPermanentCache()
		{
			bool useAdvancedText = this.useAdvancedText;
			if (useAdvancedText)
			{
				bool flag = this.textGenerationInfo == IntPtr.Zero;
				if (flag)
				{
					this.textGenerationInfo = TextGenerationInfo.Create();
				}
				TextHandle.TextLib.GenerateText(this.nativeSettings, this.textGenerationInfo);
			}
			else
			{
				TextHandle.s_PermanentCache.AddTextInfoToCache(this);
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0002C9F8 File Offset: 0x0002ABF8
		public void AddTextInfoToTemporaryCache(int hashCode)
		{
			bool useAdvancedText = this.useAdvancedText;
			if (!useAdvancedText)
			{
				TextHandle.s_TemporaryCache.AddTextInfoToCache(this, hashCode);
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0002CA1F File Offset: 0x0002AC1F
		public void RemoveTextInfoFromTemporaryCache()
		{
			TextHandle.s_TemporaryCache.RemoveTextInfoFromCache(this);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0002CA30 File Offset: 0x0002AC30
		public void RemoveTextInfoFromPermanentCache()
		{
			bool flag = this.textGenerationInfo != IntPtr.Zero;
			if (flag)
			{
				TextGenerationInfo.Destroy(this.textGenerationInfo);
				this.textGenerationInfo = IntPtr.Zero;
			}
			else
			{
				TextHandle.s_PermanentCache.RemoveTextInfoFromCache(this);
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0002CA7B File Offset: 0x0002AC7B
		public static void UpdateCurrentFrame()
		{
			TextHandle.s_TemporaryCache.UpdateCurrentFrame();
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0002CA8C File Offset: 0x0002AC8C
		internal TextInfo textInfo
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
			get
			{
				bool flag = this.TextInfoNode == null;
				TextInfo textInfo;
				if (flag)
				{
					textInfo = TextHandle.textInfoCommon;
				}
				else
				{
					textInfo = this.TextInfoNode.Value;
				}
				return textInfo;
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0002CABE File Offset: 0x0002ACBE
		public void SetDirty()
		{
			this.isDirty = true;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0002CAC8 File Offset: 0x0002ACC8
		public bool IsDirty(int hashCode)
		{
			bool flag = this.m_PreviousGenerationSettingsHash == hashCode && !this.isDirty && (this.IsCachedTemporary || this.IsCachedPermanent);
			return !flag;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0002CB08 File Offset: 0x0002AD08
		public virtual bool IsPlaceholder
		{
			get
			{
				return this.m_IsPlaceholder;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0002CB10 File Offset: 0x0002AD10
		public bool IsElided()
		{
			bool flag = this.textInfo == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.textInfo.characterCount == 0;
				flag2 = flag3 || this.m_IsEllided;
			}
			return flag2;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0002CB4F File Offset: 0x0002AD4F
		protected void UpdatePreferredValues(TextGenerationSettings tgs)
		{
			this.preferredSize = TextHandle.generator.GetPreferredValues(tgs, TextHandle.textInfoCommon);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0002CB6C File Offset: 0x0002AD6C
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal TextInfo Update()
		{
			return this.UpdateWithHash(TextHandle.settings.GetHashCode());
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0002CB90 File Offset: 0x0002AD90
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal TextInfo UpdateWithHash(int hashCode)
		{
			this.m_ScreenRect = TextHandle.settings.screenRect;
			this.m_LineHeightDefault = TextHandle.GetLineHeightDefault(TextHandle.settings);
			this.m_IsPlaceholder = TextHandle.settings.isPlaceholder;
			bool flag = !this.IsDirty(hashCode);
			TextInfo textInfo;
			if (flag)
			{
				textInfo = this.textInfo;
			}
			else
			{
				bool flag2 = TextHandle.settings.fontAsset == null || TextHandle.settings.fontAsset.characterLookupTable == null;
				if (flag2)
				{
					Debug.LogWarning("Can't Generate Mesh, No Font Asset has been assigned.");
					textInfo = this.textInfo;
				}
				else
				{
					TextHandle.generator.GenerateText(TextHandle.settings, this.textInfo);
					this.m_PreviousGenerationSettingsHash = hashCode;
					this.isDirty = false;
					this.m_IsEllided = TextHandle.generator.isTextTruncated;
					textInfo = this.textInfo;
				}
			}
			return textInfo;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0002CC64 File Offset: 0x0002AE64
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal bool PrepareFontAsset()
		{
			bool flag = TextHandle.settings.fontAsset == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = !this.IsDirty(TextHandle.settings.GetHashCode());
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool success = TextHandle.generator.PrepareFontAsset(TextHandle.settings);
					flag2 = success;
				}
			}
			return flag2;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0002CCBC File Offset: 0x0002AEBC
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
		internal void UpdatePreferredSize()
		{
			bool flag = this.textInfo.characterCount <= 0;
			if (!flag)
			{
				float maxAscender = float.MinValue;
				float maxDescender = this.textInfo.textElementInfo[this.textInfo.characterCount - 1].descender;
				float renderedWidth = 0f;
				for (int i = 0; i < this.textInfo.lineCount; i++)
				{
					LineInfo lineInfo = this.textInfo.lineInfo[i];
					maxAscender = Mathf.Max(maxAscender, this.textInfo.textElementInfo[lineInfo.firstVisibleCharacterIndex].ascender);
					maxDescender = Mathf.Min(maxDescender, this.textInfo.textElementInfo[lineInfo.firstVisibleCharacterIndex].descender);
					renderedWidth = (TextHandle.settings.isIMGUI ? Mathf.Max(renderedWidth, lineInfo.length) : Mathf.Max(renderedWidth, lineInfo.lineExtents.max.x - lineInfo.lineExtents.min.x));
				}
				float renderedHeight = maxAscender - maxDescender;
				renderedWidth += ((TextHandle.settings.margins.x > 0f) ? TextHandle.settings.margins.x : 0f);
				renderedWidth += ((TextHandle.settings.margins.z > 0f) ? TextHandle.settings.margins.z : 0f);
				renderedHeight += ((TextHandle.settings.margins.y > 0f) ? TextHandle.settings.margins.y : 0f);
				renderedHeight += ((TextHandle.settings.margins.w > 0f) ? TextHandle.settings.margins.w : 0f);
				renderedWidth = (float)((int)(renderedWidth * 100f + 1f)) / 100f;
				renderedHeight = (float)((int)(renderedHeight * 100f + 1f)) / 100f;
				this.preferredSize = new Vector2(renderedWidth, renderedHeight);
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0002CEDC File Offset: 0x0002B0DC
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static float ConvertPixelUnitsToTextCoreRelativeUnits(float fontSize, FontAsset fontAsset)
		{
			float paddingPercent = 1f / (float)fontAsset.atlasPadding;
			float pointSizeRatio = fontAsset.faceInfo.pointSize / fontSize;
			return paddingPercent * pointSizeRatio;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0002CF14 File Offset: 0x0002B114
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
		internal static float GetLineHeightDefault(TextGenerationSettings settings)
		{
			bool flag = settings != null && settings.fontAsset != null;
			float num;
			if (flag)
			{
				num = settings.fontAsset.faceInfo.lineHeight / settings.fontAsset.faceInfo.pointSize * settings.fontSize;
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0002CF7C File Offset: 0x0002B17C
		public virtual Vector2 GetCursorPositionFromStringIndexUsingCharacterHeight(int index, bool inverseYAxis = true)
		{
			this.AddTextInfoToPermanentCache();
			return this.useAdvancedText ? TextSelectionService.GetCursorPositionFromLogicalIndex(this.textGenerationInfo, index) : this.textInfo.GetCursorPositionFromStringIndexUsingCharacterHeight(index, this.m_ScreenRect, this.m_LineHeightDefault, inverseYAxis);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0002CFC4 File Offset: 0x0002B1C4
		public Vector2 GetCursorPositionFromStringIndexUsingLineHeight(int index, bool useXAdvance = false, bool inverseYAxis = true)
		{
			this.AddTextInfoToPermanentCache();
			return this.useAdvancedText ? TextSelectionService.GetCursorPositionFromLogicalIndex(this.textGenerationInfo, index) : this.textInfo.GetCursorPositionFromStringIndexUsingLineHeight(index, this.m_ScreenRect, this.m_LineHeightDefault, useXAdvance, inverseYAxis);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0002D010 File Offset: 0x0002B210
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal Rect[] GetHighlightRectangles(int cursorIndex, int selectIndex)
		{
			bool flag = !this.useAdvancedText;
			Rect[] array;
			if (flag)
			{
				Debug.LogError("Cannot use GetHighlightRectangles while using Standard Text");
				array = new Rect[0];
			}
			else
			{
				array = TextSelectionService.GetHighlightRectangles(this.textGenerationInfo, cursorIndex, selectIndex);
			}
			return array;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0002D054 File Offset: 0x0002B254
		public int GetCursorIndexFromPosition(Vector2 position, bool inverseYAxis = true)
		{
			return this.useAdvancedText ? TextSelectionService.GetCursorLogicalIndexFromPosition(this.textGenerationInfo, position) : this.textInfo.GetCursorIndexFromPosition(position, this.m_ScreenRect, inverseYAxis);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0002D090 File Offset: 0x0002B290
		public int LineDownCharacterPosition(int originalLogicalPos)
		{
			return this.useAdvancedText ? TextSelectionService.LineDownCharacterPosition(this.textGenerationInfo, originalLogicalPos) : this.textInfo.LineDownCharacterPosition(originalLogicalPos);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0002D0C4 File Offset: 0x0002B2C4
		public int LineUpCharacterPosition(int originalLogicalPos)
		{
			return this.useAdvancedText ? TextSelectionService.LineUpCharacterPosition(this.textGenerationInfo, originalLogicalPos) : this.textInfo.LineUpCharacterPosition(originalLogicalPos);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0002D0F8 File Offset: 0x0002B2F8
		public int FindIntersectingLink(Vector3 position, bool inverseYAxis = true)
		{
			bool useAdvancedText = this.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				Debug.LogError("Cannot use FindIntersectingLink while using Advanced Text");
				num = 0;
			}
			else
			{
				num = this.textInfo.FindIntersectingLink(position, this.m_ScreenRect, inverseYAxis);
			}
			return num;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0002D138 File Offset: 0x0002B338
		public int GetCorrespondingStringIndex(int index)
		{
			return this.useAdvancedText ? index : this.textInfo.GetCorrespondingStringIndex(index);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0002D164 File Offset: 0x0002B364
		public LineInfo GetLineInfoFromCharacterIndex(int index)
		{
			bool useAdvancedText = this.useAdvancedText;
			LineInfo lineInfo;
			if (useAdvancedText)
			{
				Debug.LogError("Cannot use GetLineInfoFromCharacterIndex while using Advanced Text");
				lineInfo = default(LineInfo);
			}
			else
			{
				lineInfo = this.textInfo.GetLineInfoFromCharacterIndex(index);
			}
			return lineInfo;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0002D1A4 File Offset: 0x0002B3A4
		public int GetLineNumber(int index)
		{
			return this.useAdvancedText ? TextSelectionService.GetLineNumber(this.textGenerationInfo, index) : this.textInfo.GetLineNumber(index);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0002D1D8 File Offset: 0x0002B3D8
		public float GetLineHeight(int lineNumber)
		{
			return this.useAdvancedText ? TextSelectionService.GetLineHeight(this.textGenerationInfo, lineNumber) : this.textInfo.GetLineHeight(lineNumber);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0002D20C File Offset: 0x0002B40C
		public float GetLineHeightFromCharacterIndex(int index)
		{
			return this.useAdvancedText ? TextSelectionService.GetCharacterHeightFromIndex(this.textGenerationInfo, index) : this.textInfo.GetLineHeightFromCharacterIndex(index);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0002D240 File Offset: 0x0002B440
		public float GetCharacterHeightFromIndex(int index)
		{
			return this.useAdvancedText ? TextSelectionService.GetCharacterHeightFromIndex(this.textGenerationInfo, index) : this.textInfo.GetCharacterHeightFromIndex(index);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0002D274 File Offset: 0x0002B474
		public string Substring(int startIndex, int length)
		{
			return this.useAdvancedText ? TextSelectionService.Substring(this.textGenerationInfo, startIndex, startIndex + length) : this.textInfo.Substring(startIndex, length);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0002D2AC File Offset: 0x0002B4AC
		public int PreviousCodePointIndex(int currentIndex)
		{
			bool flag = !this.useAdvancedText;
			int num;
			if (flag)
			{
				Debug.LogError("Cannot use PreviousCodePointIndex while using Standard Text");
				num = 0;
			}
			else
			{
				num = TextSelectionService.PreviousCodePointIndex(this.textGenerationInfo, currentIndex);
			}
			return num;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0002D2E8 File Offset: 0x0002B4E8
		public int NextCodePointIndex(int currentIndex)
		{
			bool flag = !this.useAdvancedText;
			int num;
			if (flag)
			{
				Debug.LogError("Cannot use NextCodePointIndex while using Standard Text");
				num = 0;
			}
			else
			{
				num = TextSelectionService.NextCodePointIndex(this.textGenerationInfo, currentIndex);
			}
			return num;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0002D324 File Offset: 0x0002B524
		public int GetStartOfNextWord(int currentIndex)
		{
			bool flag = !this.useAdvancedText;
			int num;
			if (flag)
			{
				Debug.LogError("Cannot use GetStartOfNextWord while using Standard Text");
				num = 0;
			}
			else
			{
				num = TextSelectionService.GetStartOfNextWord(this.textGenerationInfo, currentIndex);
			}
			return num;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0002D360 File Offset: 0x0002B560
		public int GetEndOfPreviousWord(int currentIndex)
		{
			bool flag = !this.useAdvancedText;
			int num;
			if (flag)
			{
				Debug.LogError("Cannot use GetEndOfPreviousWord while using Standard Text");
				num = 0;
			}
			else
			{
				num = TextSelectionService.GetEndOfPreviousWord(this.textGenerationInfo, currentIndex);
			}
			return num;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0002D39C File Offset: 0x0002B59C
		public int GetFirstCharacterIndexOnLine(int currentIndex)
		{
			bool flag = !this.useAdvancedText;
			int num;
			if (flag)
			{
				Debug.LogError("Cannot use GetFirstCharacterIndexOnLine while using Standard Text");
				num = 0;
			}
			else
			{
				num = TextSelectionService.GetFirstCharacterIndexOnLine(this.textGenerationInfo, currentIndex);
			}
			return num;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0002D3D8 File Offset: 0x0002B5D8
		public int GetLastCharacterIndexOnLine(int currentIndex)
		{
			bool flag = !this.useAdvancedText;
			int num;
			if (flag)
			{
				Debug.LogError("Cannot use GetLastCharacterIndexOnLine while using Standard Text");
				num = 0;
			}
			else
			{
				num = TextSelectionService.GetLastCharacterIndexOnLine(this.textGenerationInfo, currentIndex);
			}
			return num;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0002D414 File Offset: 0x0002B614
		public int IndexOf(char value, int startIndex)
		{
			bool useAdvancedText = this.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				Debug.LogError("Cannot use IndexOf while using Advanced Text");
				num = 0;
			}
			else
			{
				num = this.textInfo.IndexOf(value, startIndex);
			}
			return num;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0002D450 File Offset: 0x0002B650
		public int LastIndexOf(char value, int startIndex)
		{
			bool useAdvancedText = this.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				Debug.LogError("Cannot use LastIndexOf while using Advanced Text");
				num = 0;
			}
			else
			{
				num = this.textInfo.LastIndexOf(value, startIndex);
			}
			return num;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0002D48C File Offset: 0x0002B68C
		public void SelectCurrentWord(int index, ref int cursorIndex, ref int selectIndex)
		{
			bool flag = !this.useAdvancedText;
			if (flag)
			{
				Debug.LogError("Cannot use SelectCurrentWord while using Standard Text");
			}
			else
			{
				TextSelectionService.SelectCurrentWord(this.textGenerationInfo, index, ref cursorIndex, ref selectIndex);
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0002D4C4 File Offset: 0x0002B6C4
		public void SelectCurrentParagraph(ref int cursorIndex, ref int selectIndex)
		{
			bool flag = !this.useAdvancedText;
			if (flag)
			{
				Debug.LogError("Cannot use SelectCurrentParagraph while using Standard Text");
			}
			else
			{
				TextSelectionService.SelectCurrentParagraph(this.textGenerationInfo, ref cursorIndex, ref selectIndex);
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0002D4FC File Offset: 0x0002B6FC
		public void SelectToPreviousParagraph(ref int cursorIndex)
		{
			bool flag = !this.useAdvancedText;
			if (flag)
			{
				Debug.LogError("Cannot use SelectToPreviousParagraph while using Standard Text");
			}
			else
			{
				TextSelectionService.SelectToPreviousParagraph(this.textGenerationInfo, ref cursorIndex);
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0002D534 File Offset: 0x0002B734
		public void SelectToNextParagraph(ref int cursorIndex)
		{
			bool flag = !this.useAdvancedText;
			if (flag)
			{
				Debug.LogError("Cannot use SelectToNextParagraph while using Standard Text");
			}
			else
			{
				TextSelectionService.SelectToNextParagraph(this.textGenerationInfo, ref cursorIndex);
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0002D56C File Offset: 0x0002B76C
		public void SelectToStartOfParagraph(ref int cursorIndex)
		{
			bool flag = !this.useAdvancedText;
			if (flag)
			{
				Debug.LogError("Cannot use SelectToStartOfParagraph while using Standard Text");
			}
			else
			{
				TextSelectionService.SelectToStartOfParagraph(this.textGenerationInfo, ref cursorIndex);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0002D5A4 File Offset: 0x0002B7A4
		public void SelectToEndOfParagraph(ref int cursorIndex)
		{
			bool flag = !this.useAdvancedText;
			if (flag)
			{
				Debug.LogError("Cannot use SelectToEndOfParagraph while using Standard Text");
			}
			else
			{
				TextSelectionService.SelectToEndOfParagraph(this.textGenerationInfo, ref cursorIndex);
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0002D5DC File Offset: 0x0002B7DC
		internal virtual bool IsAdvancedTextEnabledForElement()
		{
			return false;
		}

		// Token: 0x04000352 RID: 850
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static TextHandleTemporaryCache s_TemporaryCache = new TextHandleTemporaryCache();

		// Token: 0x04000353 RID: 851
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static TextHandlePermanentCache s_PermanentCache = new TextHandlePermanentCache();

		// Token: 0x04000354 RID: 852
		private static TextGenerationSettings[] s_Settings;

		// Token: 0x04000355 RID: 853
		private static TextGenerator[] s_Generators;

		// Token: 0x04000356 RID: 854
		private static TextInfo[] s_TextInfosCommon;

		// Token: 0x04000357 RID: 855
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal NativeTextGenerationSettings nativeSettings = NativeTextGenerationSettings.Default;

		// Token: 0x04000359 RID: 857
		private Rect m_ScreenRect;

		// Token: 0x0400035A RID: 858
		private float m_LineHeightDefault;

		// Token: 0x0400035B RID: 859
		private bool m_IsPlaceholder;

		// Token: 0x0400035C RID: 860
		private bool m_IsEllided;

		// Token: 0x0400035D RID: 861
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal IntPtr textGenerationInfo = IntPtr.Zero;

		// Token: 0x04000361 RID: 865
		private static TextLib s_TextLib;

		// Token: 0x04000362 RID: 866
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal int m_PreviousGenerationSettingsHash;

		// Token: 0x04000363 RID: 867
		protected internal static List<OTL_FeatureTag> m_ActiveFontFeatures = new List<OTL_FeatureTag> { OTL_FeatureTag.kern };

		// Token: 0x04000364 RID: 868
		private bool isDirty;
	}
}
