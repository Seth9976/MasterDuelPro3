using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000040 RID: 64
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal class TextGenerator
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000BB39 File Offset: 0x00009D39
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000BB40 File Offset: 0x00009D40
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static bool IsExecutingJob { get; set; }

		// Token: 0x060001A1 RID: 417 RVA: 0x0000BB48 File Offset: 0x00009D48
		public void GenerateText(TextGenerationSettings settings, TextInfo textInfo)
		{
			bool canWriteOnAsset = !TextGenerator.IsExecutingJob;
			bool flag = settings.fontAsset == null || settings.fontAsset.characterLookupTable == null;
			if (flag)
			{
				Debug.LogWarning("Can't Generate Mesh, No Font Asset has been assigned.");
			}
			else
			{
				bool flag2 = textInfo == null;
				if (flag2)
				{
					Debug.LogError("Null TextInfo provided to TextGenerator. Cannot update its content.");
				}
				else
				{
					this.Prepare(settings, textInfo);
					bool flag3 = canWriteOnAsset;
					if (flag3)
					{
						FontAsset.UpdateFontAssetsInUpdateQueue();
					}
					this.GenerateTextMesh(settings, textInfo);
				}
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000BBC4 File Offset: 0x00009DC4
		public bool isTextTruncated
		{
			get
			{
				return this.m_IsTextTruncated;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000BBDC File Offset: 0x00009DDC
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal void GenerateTextMesh(TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			bool flag = generationSettings.fontAsset == null || generationSettings.fontAsset.characterLookupTable == null;
			if (flag)
			{
				Debug.LogWarning("Can't Generate Mesh! No Font Asset has been assigned.");
			}
			else
			{
				bool flag2 = textInfo != null;
				if (flag2)
				{
					textInfo.Clear();
				}
				bool flag3 = this.m_TextProcessingArray == null || this.m_TextProcessingArray.Length == 0 || this.m_TextProcessingArray[0].unicode == 0U;
				if (flag3)
				{
					TextGenerator.ClearMesh(true, textInfo);
					this.m_PreferredWidth = 0f;
					this.m_PreferredHeight = 0f;
				}
				else
				{
					uint charCode;
					float maxVisibleDescender;
					this.ParsingPhase(textInfo, generationSettings, out charCode, out maxVisibleDescender);
					float fontSizeDelta = this.m_MaxFontSize - this.m_MinFontSize;
					bool flag4 = generationSettings.autoSize && fontSizeDelta > 0.051f && this.m_FontSize < generationSettings.fontSizeMax && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount;
					if (flag4)
					{
						bool flag5 = this.m_CharWidthAdjDelta < generationSettings.charWidthMaxAdj / 100f;
						if (flag5)
						{
							this.m_CharWidthAdjDelta = 0f;
						}
						this.m_MinFontSize = this.m_FontSize;
						float sizeDelta = Mathf.Max((this.m_MaxFontSize - this.m_FontSize) / 2f, 0.05f);
						this.m_FontSize += sizeDelta;
						this.m_FontSize = Mathf.Min((float)((int)(this.m_FontSize * 20f + 0.5f)) / 20f, generationSettings.charWidthMaxAdj);
					}
					else
					{
						bool flag6 = this.m_AutoSizeIterationCount >= this.m_AutoSizeMaxIterationCount;
						if (flag6)
						{
							Debug.Log("Auto Size Iteration Count: " + this.m_AutoSizeIterationCount.ToString() + ". Final Point Size: " + this.m_FontSize.ToString());
						}
						bool flag7 = this.m_CharacterCount == 0 || (this.m_CharacterCount == 1 && charCode == 3U);
						if (flag7)
						{
							TextGenerator.ClearMesh(true, textInfo);
						}
						else
						{
							this.LayoutPhase(textInfo, generationSettings, maxVisibleDescender);
							for (int i = 1; i < textInfo.materialCount; i++)
							{
								textInfo.meshInfo[i].ClearUnusedVertices();
								bool flag8 = generationSettings.geometrySortingOrder > VertexSortingOrder.Normal;
								if (flag8)
								{
									textInfo.meshInfo[i].SortGeometry(VertexSortingOrder.Reverse);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000BE38 File Offset: 0x0000A038
		private bool ValidateHtmlTag(TextProcessingElement[] chars, int startIndex, out int endIndex, TextGenerationSettings generationSettings, TextInfo textInfo, out bool isThreadSuccess)
		{
			bool canWriteOnAsset = !TextGenerator.IsExecutingJob;
			isThreadSuccess = true;
			TextSettings textSettings = generationSettings.textSettings;
			int tagCharCount = 0;
			byte attributeFlag = 0;
			int attributeIndex = 0;
			this.ClearMarkupTagAttributes();
			TagValueType tagValueType = TagValueType.None;
			TagUnitType tagUnitType = TagUnitType.Pixels;
			endIndex = startIndex;
			bool isTagSet = false;
			bool isValidHtmlTag = false;
			bool startedWithQuotes = false;
			bool startedWithDoubleQuotes = false;
			int i = startIndex;
			while (i < chars.Length && chars[i].unicode != 0U && tagCharCount < this.m_HtmlTag.Length && chars[i].unicode != 60U)
			{
				uint unicode = chars[i].unicode;
				bool flag = unicode == 62U;
				if (flag)
				{
					isValidHtmlTag = true;
					endIndex = i;
					this.m_HtmlTag[tagCharCount] = '\0';
					break;
				}
				this.m_HtmlTag[tagCharCount] = (char)unicode;
				tagCharCount++;
				bool flag2 = attributeFlag == 1;
				if (flag2)
				{
					bool flag3 = tagValueType == TagValueType.None;
					if (flag3)
					{
						bool flag4 = unicode == 43U || unicode == 45U || unicode == 46U || (unicode >= 48U && unicode <= 57U);
						if (flag4)
						{
							tagUnitType = TagUnitType.Pixels;
							tagValueType = (this.m_XmlAttribute[attributeIndex].valueType = TagValueType.NumericalValue);
							this.m_XmlAttribute[attributeIndex].valueStartIndex = tagCharCount - 1;
							RichTextTagAttribute[] xmlAttribute = this.m_XmlAttribute;
							int num = attributeIndex;
							xmlAttribute[num].valueLength = xmlAttribute[num].valueLength + 1;
						}
						else
						{
							bool flag5 = unicode == 35U;
							if (flag5)
							{
								tagUnitType = TagUnitType.Pixels;
								tagValueType = (this.m_XmlAttribute[attributeIndex].valueType = TagValueType.ColorValue);
								this.m_XmlAttribute[attributeIndex].valueStartIndex = tagCharCount - 1;
								RichTextTagAttribute[] xmlAttribute2 = this.m_XmlAttribute;
								int num2 = attributeIndex;
								xmlAttribute2[num2].valueLength = xmlAttribute2[num2].valueLength + 1;
							}
							else
							{
								bool flag6 = unicode == 39U;
								if (flag6)
								{
									tagUnitType = TagUnitType.Pixels;
									tagValueType = (this.m_XmlAttribute[attributeIndex].valueType = TagValueType.StringValue);
									this.m_XmlAttribute[attributeIndex].valueStartIndex = tagCharCount;
									startedWithQuotes = true;
								}
								else
								{
									bool flag7 = unicode == 34U;
									if (flag7)
									{
										tagUnitType = TagUnitType.Pixels;
										tagValueType = (this.m_XmlAttribute[attributeIndex].valueType = TagValueType.StringValue);
										this.m_XmlAttribute[attributeIndex].valueStartIndex = tagCharCount;
										startedWithDoubleQuotes = true;
									}
									else
									{
										tagUnitType = TagUnitType.Pixels;
										tagValueType = (this.m_XmlAttribute[attributeIndex].valueType = TagValueType.StringValue);
										this.m_XmlAttribute[attributeIndex].valueStartIndex = tagCharCount - 1;
										this.m_XmlAttribute[attributeIndex].valueHashCode = ((this.m_XmlAttribute[attributeIndex].valueHashCode << 5) + this.m_XmlAttribute[attributeIndex].valueHashCode) ^ (int)TextGeneratorUtilities.ToUpperFast((char)unicode);
										RichTextTagAttribute[] xmlAttribute3 = this.m_XmlAttribute;
										int num3 = attributeIndex;
										xmlAttribute3[num3].valueLength = xmlAttribute3[num3].valueLength + 1;
									}
								}
							}
						}
					}
					else
					{
						bool flag8 = tagValueType == TagValueType.NumericalValue;
						if (flag8)
						{
							bool flag9 = unicode == 112U || unicode == 101U || unicode == 37U || unicode == 32U;
							if (flag9)
							{
								attributeFlag = 2;
								tagValueType = TagValueType.None;
								uint num4 = unicode;
								uint num5 = num4;
								if (num5 != 37U)
								{
									if (num5 != 101U)
									{
										tagUnitType = (this.m_XmlAttribute[attributeIndex].unitType = TagUnitType.Pixels);
									}
									else
									{
										tagUnitType = (this.m_XmlAttribute[attributeIndex].unitType = TagUnitType.FontUnits);
									}
								}
								else
								{
									tagUnitType = (this.m_XmlAttribute[attributeIndex].unitType = TagUnitType.Percentage);
								}
								attributeIndex++;
								this.m_XmlAttribute[attributeIndex].nameHashCode = 0;
								this.m_XmlAttribute[attributeIndex].valueHashCode = 0;
								this.m_XmlAttribute[attributeIndex].valueType = TagValueType.None;
								this.m_XmlAttribute[attributeIndex].unitType = TagUnitType.Pixels;
								this.m_XmlAttribute[attributeIndex].valueStartIndex = 0;
								this.m_XmlAttribute[attributeIndex].valueLength = 0;
							}
							else
							{
								RichTextTagAttribute[] xmlAttribute4 = this.m_XmlAttribute;
								int num6 = attributeIndex;
								xmlAttribute4[num6].valueLength = xmlAttribute4[num6].valueLength + 1;
							}
						}
						else
						{
							bool flag10 = tagValueType == TagValueType.ColorValue;
							if (flag10)
							{
								bool flag11 = unicode != 32U;
								if (flag11)
								{
									RichTextTagAttribute[] xmlAttribute5 = this.m_XmlAttribute;
									int num7 = attributeIndex;
									xmlAttribute5[num7].valueLength = xmlAttribute5[num7].valueLength + 1;
								}
								else
								{
									attributeFlag = 2;
									tagValueType = TagValueType.None;
									tagUnitType = TagUnitType.Pixels;
									attributeIndex++;
									this.m_XmlAttribute[attributeIndex].nameHashCode = 0;
									this.m_XmlAttribute[attributeIndex].valueType = TagValueType.None;
									this.m_XmlAttribute[attributeIndex].unitType = TagUnitType.Pixels;
									this.m_XmlAttribute[attributeIndex].valueHashCode = 0;
									this.m_XmlAttribute[attributeIndex].valueStartIndex = 0;
									this.m_XmlAttribute[attributeIndex].valueLength = 0;
								}
							}
							else
							{
								bool flag12 = tagValueType == TagValueType.StringValue;
								if (flag12)
								{
									bool flag13 = (!startedWithDoubleQuotes || unicode != 34U) && (!startedWithQuotes || unicode != 39U);
									if (flag13)
									{
										this.m_XmlAttribute[attributeIndex].valueHashCode = ((this.m_XmlAttribute[attributeIndex].valueHashCode << 5) + this.m_XmlAttribute[attributeIndex].valueHashCode) ^ (int)TextGeneratorUtilities.ToUpperFast((char)unicode);
										RichTextTagAttribute[] xmlAttribute6 = this.m_XmlAttribute;
										int num8 = attributeIndex;
										xmlAttribute6[num8].valueLength = xmlAttribute6[num8].valueLength + 1;
									}
									else
									{
										attributeFlag = 2;
										tagValueType = TagValueType.None;
										tagUnitType = TagUnitType.Pixels;
										attributeIndex++;
										this.m_XmlAttribute[attributeIndex].nameHashCode = 0;
										this.m_XmlAttribute[attributeIndex].valueType = TagValueType.None;
										this.m_XmlAttribute[attributeIndex].unitType = TagUnitType.Pixels;
										this.m_XmlAttribute[attributeIndex].valueHashCode = 0;
										this.m_XmlAttribute[attributeIndex].valueStartIndex = 0;
										this.m_XmlAttribute[attributeIndex].valueLength = 0;
									}
								}
							}
						}
					}
				}
				bool flag14 = unicode == 61U;
				if (flag14)
				{
					attributeFlag = 1;
				}
				bool flag15 = attributeFlag == 0 && unicode == 32U;
				if (flag15)
				{
					bool flag16 = isTagSet;
					if (flag16)
					{
						return false;
					}
					isTagSet = true;
					attributeFlag = 2;
					tagValueType = TagValueType.None;
					tagUnitType = TagUnitType.Pixels;
					attributeIndex++;
					this.m_XmlAttribute[attributeIndex].nameHashCode = 0;
					this.m_XmlAttribute[attributeIndex].valueType = TagValueType.None;
					this.m_XmlAttribute[attributeIndex].unitType = TagUnitType.Pixels;
					this.m_XmlAttribute[attributeIndex].valueHashCode = 0;
					this.m_XmlAttribute[attributeIndex].valueStartIndex = 0;
					this.m_XmlAttribute[attributeIndex].valueLength = 0;
				}
				bool flag17 = attributeFlag == 0;
				if (flag17)
				{
					this.m_XmlAttribute[attributeIndex].nameHashCode = ((this.m_XmlAttribute[attributeIndex].nameHashCode << 5) + this.m_XmlAttribute[attributeIndex].nameHashCode) ^ (int)TextGeneratorUtilities.ToUpperFast((char)unicode);
				}
				bool flag18 = attributeFlag == 2 && unicode == 32U;
				if (flag18)
				{
					attributeFlag = 0;
				}
				i++;
			}
			bool flag19 = !isValidHtmlTag;
			if (flag19)
			{
				return false;
			}
			bool flag20 = this.m_TagNoParsing && this.m_XmlAttribute[0].nameHashCode != -294095813;
			if (flag20)
			{
				return false;
			}
			bool flag21 = this.m_XmlAttribute[0].nameHashCode == -294095813;
			if (flag21)
			{
				this.m_TagNoParsing = false;
				return true;
			}
			bool flag22 = this.m_HtmlTag[0] == '#' && tagCharCount == 4;
			if (flag22)
			{
				this.m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(this.m_HtmlTag, 0, tagCharCount);
				this.m_ColorStack.Add(this.m_HtmlColor);
				return true;
			}
			bool flag23 = this.m_HtmlTag[0] == '#' && tagCharCount == 5;
			if (flag23)
			{
				this.m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(this.m_HtmlTag, 0, tagCharCount);
				this.m_ColorStack.Add(this.m_HtmlColor);
				return true;
			}
			bool flag24 = this.m_HtmlTag[0] == '#' && tagCharCount == 7;
			if (flag24)
			{
				this.m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(this.m_HtmlTag, 0, tagCharCount);
				this.m_ColorStack.Add(this.m_HtmlColor);
				return true;
			}
			bool flag25 = this.m_HtmlTag[0] == '#' && tagCharCount == 9;
			if (flag25)
			{
				this.m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(this.m_HtmlTag, 0, tagCharCount);
				this.m_ColorStack.Add(this.m_HtmlColor);
				return true;
			}
			MarkupTag nameHashCode4 = (MarkupTag)this.m_XmlAttribute[0].nameHashCode;
			MarkupTag markupTag = nameHashCode4;
			if (markupTag <= MarkupTag.SLASH_STRIKETHROUGH)
			{
				if (markupTag <= MarkupTag.LINE_INDENT)
				{
					if (markupTag <= MarkupTag.SLASH_INDENT)
					{
						if (markupTag <= MarkupTag.SLASH_MARGIN)
						{
							if (markupTag <= MarkupTag.FONT_WEIGHT)
							{
								if (markupTag == MarkupTag.GRADIENT)
								{
									int gradientPresetHashCode = this.m_XmlAttribute[0].valueHashCode;
									TextColorGradient tempColorGradientPreset;
									bool flag26 = MaterialReferenceManager.TryGetColorGradientPreset(gradientPresetHashCode, out tempColorGradientPreset);
									if (flag26)
									{
										this.m_ColorGradientPreset = tempColorGradientPreset;
									}
									else
									{
										bool flag27 = tempColorGradientPreset == null;
										if (flag27)
										{
											bool flag28 = !canWriteOnAsset;
											if (flag28)
											{
												isThreadSuccess = false;
												return false;
											}
											tempColorGradientPreset = Resources.Load<TextColorGradient>(textSettings.defaultColorGradientPresetsPath + new string(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength));
										}
										bool flag29 = tempColorGradientPreset == null;
										if (flag29)
										{
											return false;
										}
										MaterialReferenceManager.AddColorGradientPreset(gradientPresetHashCode, tempColorGradientPreset);
										this.m_ColorGradientPreset = tempColorGradientPreset;
									}
									this.m_ColorGradientPresetIsTinted = false;
									int j = 1;
									while (j < this.m_XmlAttribute.Length && this.m_XmlAttribute[j].nameHashCode != 0)
									{
										int nameHashCode = this.m_XmlAttribute[j].nameHashCode;
										MarkupTag markupTag2 = (MarkupTag)nameHashCode;
										MarkupTag markupTag3 = markupTag2;
										if (markupTag3 == MarkupTag.TINT)
										{
											this.m_ColorGradientPresetIsTinted = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[j].valueStartIndex, this.m_XmlAttribute[j].valueLength) != 0f;
										}
										j++;
									}
									this.m_ColorGradientStack.Add(this.m_ColorGradientPreset);
									return true;
								}
								if (markupTag != MarkupTag.FONT_WEIGHT)
								{
									goto IL_4676;
								}
								float value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
								bool flag30 = value == -32768f;
								if (flag30)
								{
									return false;
								}
								int num9 = (int)value;
								int num10 = num9;
								if (num10 <= 400)
								{
									if (num10 <= 200)
									{
										if (num10 != 100)
										{
											if (num10 == 200)
											{
												this.m_FontWeightInternal = TextFontWeight.ExtraLight;
											}
										}
										else
										{
											this.m_FontWeightInternal = TextFontWeight.Thin;
										}
									}
									else if (num10 != 300)
									{
										if (num10 == 400)
										{
											this.m_FontWeightInternal = TextFontWeight.Regular;
										}
									}
									else
									{
										this.m_FontWeightInternal = TextFontWeight.Light;
									}
								}
								else if (num10 <= 600)
								{
									if (num10 != 500)
									{
										if (num10 == 600)
										{
											this.m_FontWeightInternal = TextFontWeight.SemiBold;
										}
									}
									else
									{
										this.m_FontWeightInternal = TextFontWeight.Medium;
									}
								}
								else if (num10 != 700)
								{
									if (num10 != 800)
									{
										if (num10 == 900)
										{
											this.m_FontWeightInternal = TextFontWeight.Black;
										}
									}
									else
									{
										this.m_FontWeightInternal = TextFontWeight.Heavy;
									}
								}
								else
								{
									this.m_FontWeightInternal = TextFontWeight.Bold;
								}
								this.m_FontWeightStack.Add(this.m_FontWeightInternal);
								return true;
							}
							else
							{
								if (markupTag == MarkupTag.SLASH_GRADIENT)
								{
									this.m_ColorGradientPreset = this.m_ColorGradientStack.Remove();
									return true;
								}
								if (markupTag == MarkupTag.ACTION)
								{
									int actionID = this.m_XmlAttribute[0].valueHashCode;
									bool isTextLayoutPhase = this.m_isTextLayoutPhase;
									if (isTextLayoutPhase)
									{
										this.m_ActionStack.Add(actionID);
										Debug.Log("Action ID: [" + actionID.ToString() + "] First character index: " + this.m_CharacterCount.ToString());
									}
									return true;
								}
								if (markupTag != MarkupTag.SLASH_MARGIN)
								{
									goto IL_4676;
								}
								this.m_MarginLeft = 0f;
								this.m_MarginRight = 0f;
								return true;
							}
						}
						else if (markupTag <= MarkupTag.CHARACTER_SPACE)
						{
							if (markupTag == MarkupTag.SLASH_MONOSPACE)
							{
								this.m_MonoSpacing = 0f;
								this.m_DuoSpace = false;
								return true;
							}
							if (markupTag != MarkupTag.CHARACTER_SPACE)
							{
								goto IL_4676;
							}
							float value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
							bool flag31 = value == -32768f;
							if (flag31)
							{
								return false;
							}
							switch (tagUnitType)
							{
							case TagUnitType.Pixels:
								this.m_CSpacing = value * (generationSettings.isOrthographic ? 1f : 0.1f);
								break;
							case TagUnitType.FontUnits:
								this.m_CSpacing = value * (generationSettings.isOrthographic ? 1f : 0.1f) * this.m_CurrentFontSize;
								break;
							case TagUnitType.Percentage:
								return false;
							}
							return true;
						}
						else if (markupTag != MarkupTag.INDENT)
						{
							if (markupTag == MarkupTag.LOWERCASE)
							{
								this.m_FontStyleInternal |= FontStyles.LowerCase;
								this.m_FontStyleStack.Add(FontStyles.LowerCase);
								return true;
							}
							if (markupTag != MarkupTag.SLASH_INDENT)
							{
								goto IL_4676;
							}
							this.m_TagIndent = this.m_IndentStack.Remove();
							return true;
						}
						else
						{
							float value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
							bool flag32 = value == -32768f;
							if (flag32)
							{
								return false;
							}
							switch (tagUnitType)
							{
							case TagUnitType.Pixels:
								this.m_TagIndent = value * (generationSettings.isOrthographic ? 1f : 0.1f);
								break;
							case TagUnitType.FontUnits:
								this.m_TagIndent = value * (generationSettings.isOrthographic ? 1f : 0.1f) * this.m_CurrentFontSize;
								break;
							case TagUnitType.Percentage:
								this.m_TagIndent = this.m_MarginWidth * value / 100f;
								break;
							}
							this.m_IndentStack.Add(this.m_TagIndent);
							this.m_XAdvance = this.m_TagIndent;
							return true;
						}
					}
					else if (markupTag <= MarkupTag.SLASH_ACTION)
					{
						if (markupTag <= MarkupTag.SLASH_CHARACTER_SPACE)
						{
							if (markupTag == MarkupTag.SLASH_LOWERCASE)
							{
								bool flag33 = (generationSettings.fontStyle & FontStyles.LowerCase) != FontStyles.LowerCase;
								if (flag33)
								{
									bool flag34 = this.m_FontStyleStack.Remove(FontStyles.LowerCase) == 0;
									if (flag34)
									{
										this.m_FontStyleInternal &= ~FontStyles.LowerCase;
									}
								}
								return true;
							}
							if (markupTag != MarkupTag.SLASH_CHARACTER_SPACE)
							{
								goto IL_4676;
							}
							bool flag35 = !this.m_isTextLayoutPhase || textInfo == null;
							if (flag35)
							{
								return true;
							}
							bool flag36 = this.m_CharacterCount > 0;
							if (flag36)
							{
								this.m_XAdvance -= this.m_CSpacing;
								textInfo.textElementInfo[this.m_CharacterCount - 1].xAdvance = this.m_XAdvance;
							}
							this.m_CSpacing = 0f;
							return true;
						}
						else if (markupTag != MarkupTag.MARGIN)
						{
							if (markupTag != MarkupTag.MONOSPACE)
							{
								if (markupTag != MarkupTag.SLASH_ACTION)
								{
									goto IL_4676;
								}
								bool isTextLayoutPhase2 = this.m_isTextLayoutPhase;
								if (isTextLayoutPhase2)
								{
									Debug.Log("Action ID: [" + this.m_ActionStack.CurrentItem().ToString() + "] Last character index: " + (this.m_CharacterCount - 1).ToString());
								}
								this.m_ActionStack.Remove();
								return true;
							}
							else
							{
								float value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
								bool flag37 = value == -32768f;
								if (flag37)
								{
									return false;
								}
								switch (this.m_XmlAttribute[0].unitType)
								{
								case TagUnitType.Pixels:
									this.m_MonoSpacing = value * (generationSettings.isOrthographic ? 1f : 0.1f);
									break;
								case TagUnitType.FontUnits:
									this.m_MonoSpacing = value * (generationSettings.isOrthographic ? 1f : 0.1f) * this.m_CurrentFontSize;
									break;
								case TagUnitType.Percentage:
									return false;
								}
								bool flag38 = this.m_XmlAttribute[1].nameHashCode == 582810522;
								if (flag38)
								{
									this.m_DuoSpace = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[1].valueStartIndex, this.m_XmlAttribute[1].valueLength) != 0f;
								}
								return true;
							}
						}
						else
						{
							TagValueType valueType = this.m_XmlAttribute[0].valueType;
							TagValueType tagValueType2 = valueType;
							float value;
							if (tagValueType2 == TagValueType.None)
							{
								int k = 1;
								while (k < this.m_XmlAttribute.Length && this.m_XmlAttribute[k].nameHashCode != 0)
								{
									int nameHashCode2 = this.m_XmlAttribute[k].nameHashCode;
									MarkupTag markupTag4 = (MarkupTag)nameHashCode2;
									MarkupTag markupTag5 = markupTag4;
									if (markupTag5 != MarkupTag.LEFT)
									{
										if (markupTag5 == MarkupTag.RIGHT)
										{
											value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[k].valueStartIndex, this.m_XmlAttribute[k].valueLength);
											bool flag39 = value == -32768f;
											if (flag39)
											{
												return false;
											}
											switch (this.m_XmlAttribute[k].unitType)
											{
											case TagUnitType.Pixels:
												this.m_MarginRight = value * (generationSettings.isOrthographic ? 1f : 0.1f);
												break;
											case TagUnitType.FontUnits:
												this.m_MarginRight = value * (generationSettings.isOrthographic ? 1f : 0.1f) * this.m_CurrentFontSize;
												break;
											case TagUnitType.Percentage:
												this.m_MarginRight = (this.m_MarginWidth - ((this.m_Width != -1f) ? this.m_Width : 0f)) * value / 100f;
												break;
											}
											this.m_MarginRight = ((this.m_MarginRight >= 0f) ? this.m_MarginRight : 0f);
										}
									}
									else
									{
										value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[k].valueStartIndex, this.m_XmlAttribute[k].valueLength);
										bool flag40 = value == -32768f;
										if (flag40)
										{
											return false;
										}
										switch (this.m_XmlAttribute[k].unitType)
										{
										case TagUnitType.Pixels:
											this.m_MarginLeft = value * (generationSettings.isOrthographic ? 1f : 0.1f);
											break;
										case TagUnitType.FontUnits:
											this.m_MarginLeft = value * (generationSettings.isOrthographic ? 1f : 0.1f) * this.m_CurrentFontSize;
											break;
										case TagUnitType.Percentage:
											this.m_MarginLeft = (this.m_MarginWidth - ((this.m_Width != -1f) ? this.m_Width : 0f)) * value / 100f;
											break;
										}
										this.m_MarginLeft = ((this.m_MarginLeft >= 0f) ? this.m_MarginLeft : 0f);
									}
									k++;
								}
								return true;
							}
							if (tagValueType2 != TagValueType.NumericalValue)
							{
								return false;
							}
							value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
							bool flag41 = value == -32768f;
							if (flag41)
							{
								return false;
							}
							switch (tagUnitType)
							{
							case TagUnitType.Pixels:
								this.m_MarginLeft = value * (generationSettings.isOrthographic ? 1f : 0.1f);
								break;
							case TagUnitType.FontUnits:
								this.m_MarginLeft = value * (generationSettings.isOrthographic ? 1f : 0.1f) * this.m_CurrentFontSize;
								break;
							case TagUnitType.Percentage:
								this.m_MarginLeft = (this.m_MarginWidth - ((this.m_Width != -1f) ? this.m_Width : 0f)) * value / 100f;
								break;
							}
							this.m_MarginLeft = ((this.m_MarginLeft >= 0f) ? this.m_MarginLeft : 0f);
							this.m_MarginRight = this.m_MarginLeft;
							return true;
						}
					}
					else if (markupTag <= MarkupTag.ROTATE)
					{
						if (markupTag == MarkupTag.SLASH_MATERIAL)
						{
							MaterialReference materialReference = this.m_MaterialReferenceStack.Remove();
							this.m_CurrentMaterial = materialReference.material;
							this.m_CurrentMaterialIndex = materialReference.index;
							return true;
						}
						if (markupTag != MarkupTag.ROTATE)
						{
							goto IL_4676;
						}
						float value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
						bool flag42 = value == -32768f;
						if (flag42)
						{
							return false;
						}
						this.m_FXRotation = Quaternion.Euler(0f, 0f, value);
						return true;
					}
					else if (markupTag != MarkupTag.SPRITE)
					{
						if (markupTag == MarkupTag.SLASH_TABLE)
						{
							return false;
						}
						if (markupTag != MarkupTag.LINE_INDENT)
						{
							goto IL_4676;
						}
						float value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
						bool flag43 = value == -32768f;
						if (flag43)
						{
							return false;
						}
						switch (tagUnitType)
						{
						case TagUnitType.Pixels:
							this.m_TagLineIndent = value * (generationSettings.isOrthographic ? 1f : 0.1f);
							break;
						case TagUnitType.FontUnits:
							this.m_TagLineIndent = value * (generationSettings.isOrthographic ? 1f : 0.1f) * this.m_CurrentFontSize;
							break;
						case TagUnitType.Percentage:
							this.m_TagLineIndent = this.m_MarginWidth * value / 100f;
							break;
						}
						this.m_XAdvance += this.m_TagLineIndent;
						return true;
					}
					else
					{
						int spriteAssetHashCode = this.m_XmlAttribute[0].valueHashCode;
						this.m_SpriteIndex = -1;
						bool flag44 = this.m_XmlAttribute[0].valueType == TagValueType.None || this.m_XmlAttribute[0].valueType == TagValueType.NumericalValue;
						if (flag44)
						{
							bool flag45 = generationSettings.spriteAsset != null;
							if (flag45)
							{
								this.m_CurrentSpriteAsset = generationSettings.spriteAsset;
							}
							else
							{
								bool flag46 = textSettings.defaultSpriteAsset != null;
								if (flag46)
								{
									this.m_CurrentSpriteAsset = textSettings.defaultSpriteAsset;
								}
								else
								{
									bool flag47 = TextSettings.s_GlobalSpriteAsset != null;
									if (flag47)
									{
										this.m_CurrentSpriteAsset = TextSettings.s_GlobalSpriteAsset;
									}
								}
							}
							bool flag48 = this.m_CurrentSpriteAsset == null;
							if (flag48)
							{
								return false;
							}
						}
						else
						{
							SpriteAsset tempSpriteAsset;
							bool flag49 = MaterialReferenceManager.TryGetSpriteAsset(spriteAssetHashCode, out tempSpriteAsset);
							if (flag49)
							{
								this.m_CurrentSpriteAsset = tempSpriteAsset;
							}
							else
							{
								bool flag50 = tempSpriteAsset == null;
								if (flag50)
								{
									bool flag51 = tempSpriteAsset == null;
									if (flag51)
									{
										bool flag52 = !canWriteOnAsset;
										if (flag52)
										{
											isThreadSuccess = false;
											return false;
										}
										tempSpriteAsset = Resources.Load<SpriteAsset>(textSettings.defaultSpriteAssetPath + new string(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength));
									}
								}
								bool flag53 = tempSpriteAsset == null;
								if (flag53)
								{
									return false;
								}
								MaterialReferenceManager.AddSpriteAsset(spriteAssetHashCode, tempSpriteAsset);
								this.m_CurrentSpriteAsset = tempSpriteAsset;
							}
						}
						bool flag54 = this.m_XmlAttribute[0].valueType == TagValueType.NumericalValue;
						if (flag54)
						{
							int index = (int)TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
							bool flag55 = index == -32768;
							if (flag55)
							{
								return false;
							}
							bool flag56 = index > this.m_CurrentSpriteAsset.spriteCharacterTable.Count - 1;
							if (flag56)
							{
								return false;
							}
							this.m_SpriteIndex = index;
						}
						this.m_SpriteColor = Color.white;
						this.m_TintSprite = false;
						int l = 0;
						while (l < this.m_XmlAttribute.Length && this.m_XmlAttribute[l].nameHashCode != 0)
						{
							int nameHashCode3 = this.m_XmlAttribute[l].nameHashCode;
							int index2 = 0;
							MarkupTag markupTag6 = (MarkupTag)nameHashCode3;
							MarkupTag markupTag7 = markupTag6;
							if (markupTag7 <= MarkupTag.NAME)
							{
								if (markupTag7 != MarkupTag.ANIM)
								{
									if (markupTag7 != MarkupTag.NAME)
									{
										goto IL_3B80;
									}
									this.m_CurrentSpriteAsset = SpriteAsset.SearchForSpriteByHashCode(this.m_CurrentSpriteAsset, this.m_XmlAttribute[l].valueHashCode, true, out index2, null);
									bool flag57 = index2 == -1;
									if (flag57)
									{
										return false;
									}
									this.m_SpriteIndex = index2;
								}
								else
								{
									int paramCount = TextGeneratorUtilities.GetAttributeParameters(this.m_HtmlTag, this.m_XmlAttribute[l].valueStartIndex, this.m_XmlAttribute[l].valueLength, ref this.m_AttributeParameterValues);
									bool flag58 = paramCount != 3;
									if (flag58)
									{
										return false;
									}
									this.m_SpriteIndex = (int)this.m_AttributeParameterValues[0];
									bool isTextLayoutPhase3 = this.m_isTextLayoutPhase;
									if (isTextLayoutPhase3)
									{
									}
								}
							}
							else if (markupTag7 != MarkupTag.TINT)
							{
								if (markupTag7 != MarkupTag.COLOR)
								{
									if (markupTag7 != MarkupTag.INDEX)
									{
										goto IL_3B80;
									}
									index2 = (int)TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[1].valueStartIndex, this.m_XmlAttribute[1].valueLength);
									bool flag59 = index2 == -32768;
									if (flag59)
									{
										return false;
									}
									bool flag60 = index2 > this.m_CurrentSpriteAsset.spriteCharacterTable.Count - 1;
									if (flag60)
									{
										return false;
									}
									this.m_SpriteIndex = index2;
								}
								else
								{
									this.m_SpriteColor = TextGeneratorUtilities.HexCharsToColor(this.m_HtmlTag, this.m_XmlAttribute[l].valueStartIndex, this.m_XmlAttribute[l].valueLength);
								}
							}
							else
							{
								this.m_TintSprite = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[l].valueStartIndex, this.m_XmlAttribute[l].valueLength) != 0f;
							}
							IL_3B9C:
							l++;
							continue;
							IL_3B80:
							bool flag61 = nameHashCode3 != -991527447;
							if (flag61)
							{
								return false;
							}
							goto IL_3B9C;
						}
						bool flag62 = this.m_SpriteIndex == -1;
						if (flag62)
						{
							return false;
						}
						this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentSpriteAsset.material, this.m_CurrentSpriteAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
						this.m_TextElementType = TextElementType.Sprite;
						return true;
					}
				}
				else
				{
					if (markupTag <= MarkupTag.MARGIN_LEFT)
					{
						if (markupTag <= MarkupTag.SLASH_FONT_WEIGHT)
						{
							if (markupTag <= MarkupTag.SLASH_ALLCAPS)
							{
								if (markupTag != MarkupTag.LINE_HEIGHT)
								{
									if (markupTag != MarkupTag.SLASH_ALLCAPS)
									{
										goto IL_4676;
									}
								}
								else
								{
									float value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
									bool flag63 = value == -32768f;
									if (flag63)
									{
										return false;
									}
									switch (tagUnitType)
									{
									case TagUnitType.Pixels:
										this.m_LineHeight = value * (generationSettings.isOrthographic ? 1f : 0.1f);
										break;
									case TagUnitType.FontUnits:
										this.m_LineHeight = value * (generationSettings.isOrthographic ? 1f : 0.1f) * this.m_CurrentFontSize;
										break;
									case TagUnitType.Percentage:
									{
										float fontScale = this.m_CurrentFontSize / this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
										this.m_LineHeight = generationSettings.fontAsset.faceInfo.lineHeight * value / 100f * fontScale;
										break;
									}
									}
									return true;
								}
							}
							else
							{
								if (markupTag == MarkupTag.SMALLCAPS)
								{
									this.m_FontStyleInternal |= FontStyles.SmallCaps;
									this.m_FontStyleStack.Add(FontStyles.SmallCaps);
									return true;
								}
								if (markupTag == MarkupTag.SLASH_ROTATE)
								{
									this.m_FXRotation = Quaternion.identity;
									return true;
								}
								if (markupTag != MarkupTag.SLASH_FONT_WEIGHT)
								{
									goto IL_4676;
								}
								this.m_FontWeightStack.Remove();
								bool flag64 = this.m_FontStyleInternal == FontStyles.Bold;
								if (flag64)
								{
									this.m_FontWeightInternal = TextFontWeight.Bold;
								}
								else
								{
									this.m_FontWeightInternal = this.m_FontWeightStack.Peek();
								}
								return true;
							}
						}
						else if (markupTag <= MarkupTag.MARGIN_RIGHT)
						{
							if (markupTag != MarkupTag.SLASH_UPPERCASE)
							{
								if (markupTag != MarkupTag.MARGIN_RIGHT)
								{
									goto IL_4676;
								}
								float value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
								bool flag65 = value == -32768f;
								if (flag65)
								{
									return false;
								}
								switch (tagUnitType)
								{
								case TagUnitType.Pixels:
									this.m_MarginRight = value * (generationSettings.isOrthographic ? 1f : 0.1f);
									break;
								case TagUnitType.FontUnits:
									this.m_MarginRight = value * (generationSettings.isOrthographic ? 1f : 0.1f) * this.m_CurrentFontSize;
									break;
								case TagUnitType.Percentage:
									this.m_MarginRight = (this.m_MarginWidth - ((this.m_Width != -1f) ? this.m_Width : 0f)) * value / 100f;
									break;
								}
								this.m_MarginRight = ((this.m_MarginRight >= 0f) ? this.m_MarginRight : 0f);
								return true;
							}
						}
						else
						{
							if (markupTag == MarkupTag.NO_PARSE)
							{
								this.m_TagNoParsing = true;
								return true;
							}
							if (markupTag == MarkupTag.UPPERCASE)
							{
								goto IL_3C84;
							}
							if (markupTag != MarkupTag.MARGIN_LEFT)
							{
								goto IL_4676;
							}
							float value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
							bool flag66 = value == -32768f;
							if (flag66)
							{
								return false;
							}
							switch (tagUnitType)
							{
							case TagUnitType.Pixels:
								this.m_MarginLeft = value * (generationSettings.isOrthographic ? 1f : 0.1f);
								break;
							case TagUnitType.FontUnits:
								this.m_MarginLeft = value * (generationSettings.isOrthographic ? 1f : 0.1f) * this.m_CurrentFontSize;
								break;
							case TagUnitType.Percentage:
								this.m_MarginLeft = (this.m_MarginWidth - ((this.m_Width != -1f) ? this.m_Width : 0f)) * value / 100f;
								break;
							}
							this.m_MarginLeft = ((this.m_MarginLeft >= 0f) ? this.m_MarginLeft : 0f);
							return true;
						}
						bool flag67 = (generationSettings.fontStyle & FontStyles.UpperCase) != FontStyles.UpperCase;
						if (flag67)
						{
							bool flag68 = this.m_FontStyleStack.Remove(FontStyles.UpperCase) == 0;
							if (flag68)
							{
								this.m_FontStyleInternal &= ~FontStyles.UpperCase;
							}
						}
						return true;
					}
					if (markupTag <= MarkupTag.STRIKETHROUGH)
					{
						if (markupTag <= MarkupTag.A)
						{
							if (markupTag == MarkupTag.SLASH_VERTICAL_OFFSET)
							{
								this.m_BaselineOffset = 0f;
								return true;
							}
							if (markupTag != MarkupTag.A)
							{
								goto IL_4676;
							}
							bool flag69 = this.m_isTextLayoutPhase && !this.m_IsCalculatingPreferredValues;
							if (flag69)
							{
								bool flag70 = generationSettings.isIMGUI && textInfo != null;
								if (flag70)
								{
									int index3 = textInfo.linkCount;
									bool flag71 = index3 + 1 > textInfo.linkInfo.Length;
									if (flag71)
									{
										TextInfo.Resize<LinkInfo>(ref textInfo.linkInfo, index3 + 1);
									}
									textInfo.linkInfo[index3].hashCode = 2535353;
									textInfo.linkInfo[index3].linkTextfirstCharacterIndex = this.m_CharacterCount;
									textInfo.linkInfo[index3].linkIdFirstCharacterIndex = 3;
									bool flag72 = this.m_XmlAttribute[1].valueLength > 0;
									if (flag72)
									{
										textInfo.linkInfo[index3].SetLinkId(this.m_HtmlTag, 2, this.m_XmlAttribute[1].valueLength + this.m_XmlAttribute[1].valueStartIndex - 1);
									}
								}
								else
								{
									bool flag73 = this.m_XmlAttribute[1].nameHashCode == 2535353 && textInfo != null;
									if (flag73)
									{
										int index4 = textInfo.linkCount;
										bool flag74 = index4 + 1 > textInfo.linkInfo.Length;
										if (flag74)
										{
											TextInfo.Resize<LinkInfo>(ref textInfo.linkInfo, index4 + 1);
										}
										textInfo.linkInfo[index4].hashCode = 2535353;
										textInfo.linkInfo[index4].linkTextfirstCharacterIndex = this.m_CharacterCount;
										textInfo.linkInfo[index4].linkIdFirstCharacterIndex = startIndex + this.m_XmlAttribute[1].valueStartIndex;
										textInfo.linkInfo[index4].SetLinkId(this.m_HtmlTag, this.m_XmlAttribute[1].valueStartIndex, this.m_XmlAttribute[1].valueLength);
									}
								}
								textInfo.linkCount++;
							}
							return true;
						}
						else
						{
							if (markupTag == MarkupTag.BOLD)
							{
								this.m_FontStyleInternal |= FontStyles.Bold;
								this.m_FontStyleStack.Add(FontStyles.Bold);
								this.m_FontWeightInternal = TextFontWeight.Bold;
								return true;
							}
							if (markupTag == MarkupTag.ITALIC)
							{
								this.m_FontStyleInternal |= FontStyles.Italic;
								this.m_FontStyleStack.Add(FontStyles.Italic);
								bool flag75 = this.m_XmlAttribute[1].nameHashCode == 75347905;
								if (flag75)
								{
									this.m_ItalicAngle = (int)TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[1].valueStartIndex, this.m_XmlAttribute[1].valueLength);
									bool flag76 = this.m_ItalicAngle < -180 || this.m_ItalicAngle > 180;
									if (flag76)
									{
										return false;
									}
								}
								else
								{
									this.m_ItalicAngle = (int)this.m_CurrentFontAsset.italicStyleSlant;
								}
								this.m_ItalicAngleStack.Add(this.m_ItalicAngle);
								return true;
							}
							if (markupTag != MarkupTag.STRIKETHROUGH)
							{
								goto IL_4676;
							}
							this.m_FontStyleInternal |= FontStyles.Strikethrough;
							this.m_FontStyleStack.Add(FontStyles.Strikethrough);
							bool flag77 = this.m_XmlAttribute[1].nameHashCode == 81999901;
							if (flag77)
							{
								this.m_StrikethroughColor = TextGeneratorUtilities.HexCharsToColor(this.m_HtmlTag, this.m_XmlAttribute[1].valueStartIndex, this.m_XmlAttribute[1].valueLength);
								this.m_StrikethroughColor.a = ((this.m_HtmlColor.a < this.m_StrikethroughColor.a) ? this.m_HtmlColor.a : this.m_StrikethroughColor.a);
								bool flag78 = textInfo != null;
								if (flag78)
								{
									textInfo.hasMultipleColors = true;
								}
							}
							else
							{
								this.m_StrikethroughColor = this.m_HtmlColor;
							}
							this.m_StrikethroughColorStack.Add(this.m_StrikethroughColor);
							return true;
						}
					}
					else if (markupTag <= MarkupTag.SLASH_BOLD)
					{
						if (markupTag == MarkupTag.UNDERLINE)
						{
							this.m_FontStyleInternal |= FontStyles.Underline;
							this.m_FontStyleStack.Add(FontStyles.Underline);
							bool flag79 = this.m_XmlAttribute[1].nameHashCode == 81999901;
							if (flag79)
							{
								this.m_UnderlineColor = TextGeneratorUtilities.HexCharsToColor(this.m_HtmlTag, this.m_XmlAttribute[1].valueStartIndex, this.m_XmlAttribute[1].valueLength);
								this.m_UnderlineColor.a = ((this.m_HtmlColor.a < this.m_UnderlineColor.a) ? this.m_HtmlColor.a : this.m_UnderlineColor.a);
								bool flag80 = textInfo != null;
								if (flag80)
								{
									textInfo.hasMultipleColors = true;
								}
							}
							else
							{
								this.m_UnderlineColor = this.m_HtmlColor;
							}
							this.m_UnderlineColorStack.Add(this.m_UnderlineColor);
							return true;
						}
						if (markupTag == MarkupTag.SLASH_ITALIC)
						{
							bool flag81 = (generationSettings.fontStyle & FontStyles.Italic) != FontStyles.Italic;
							if (flag81)
							{
								this.m_ItalicAngle = this.m_ItalicAngleStack.Remove();
								bool flag82 = this.m_FontStyleStack.Remove(FontStyles.Italic) == 0;
								if (flag82)
								{
									this.m_FontStyleInternal &= ~FontStyles.Italic;
								}
							}
							return true;
						}
						if (markupTag != MarkupTag.SLASH_BOLD)
						{
							goto IL_4676;
						}
						bool flag83 = (generationSettings.fontStyle & FontStyles.Bold) != FontStyles.Bold;
						if (flag83)
						{
							bool flag84 = this.m_FontStyleStack.Remove(FontStyles.Bold) == 0;
							if (flag84)
							{
								this.m_FontStyleInternal &= ~FontStyles.Bold;
								this.m_FontWeightInternal = this.m_FontWeightStack.Peek();
							}
						}
						return true;
					}
					else
					{
						if (markupTag == MarkupTag.SLASH_A)
						{
							bool flag85 = this.m_isTextLayoutPhase && !this.m_IsCalculatingPreferredValues && textInfo != null;
							if (flag85)
							{
								bool flag86 = textInfo.linkInfo.Length == 0 || textInfo.linkCount <= 0;
								if (flag86)
								{
									bool displayWarnings = generationSettings.textSettings.displayWarnings;
									if (displayWarnings)
									{
										Debug.LogWarning("There seems to be an issue with the formatting of the <a> tag. Possible issues include: missing or misplaced closing '>', missing or incorrect attribute, or unclosed quotes for attribute values. Please review the tag syntax.");
									}
								}
								else
								{
									int index5 = textInfo.linkCount - 1;
									textInfo.linkInfo[index5].linkTextLength = this.m_CharacterCount - textInfo.linkInfo[index5].linkTextfirstCharacterIndex;
								}
							}
							return true;
						}
						if (markupTag == MarkupTag.SLASH_UNDERLINE)
						{
							bool flag87 = (generationSettings.fontStyle & FontStyles.Underline) != FontStyles.Underline;
							if (flag87)
							{
								bool flag88 = this.m_FontStyleStack.Remove(FontStyles.Underline) == 0;
								if (flag88)
								{
									this.m_FontStyleInternal &= ~FontStyles.Underline;
								}
							}
							this.m_UnderlineColor = this.m_UnderlineColorStack.Remove();
							return true;
						}
						if (markupTag != MarkupTag.SLASH_STRIKETHROUGH)
						{
							goto IL_4676;
						}
						bool flag89 = (generationSettings.fontStyle & FontStyles.Strikethrough) != FontStyles.Strikethrough;
						if (flag89)
						{
							bool flag90 = this.m_FontStyleStack.Remove(FontStyles.Strikethrough) == 0;
							if (flag90)
							{
								this.m_FontStyleInternal &= ~FontStyles.Strikethrough;
							}
						}
						this.m_StrikethroughColor = this.m_StrikethroughColorStack.Remove();
						return true;
					}
				}
			}
			else if (markupTag <= MarkupTag.SLASH_SIZE)
			{
				if (markupTag <= MarkupTag.PAGE)
				{
					if (markupTag <= MarkupTag.SLASH_SUPERSCRIPT)
					{
						if (markupTag <= MarkupTag.SUBSCRIPT)
						{
							if (markupTag != MarkupTag.POSITION)
							{
								if (markupTag != MarkupTag.SUBSCRIPT)
								{
									goto IL_4676;
								}
								this.m_FontScaleMultiplier *= ((this.m_CurrentFontAsset.faceInfo.subscriptSize > 0f) ? this.m_CurrentFontAsset.faceInfo.subscriptSize : 1f);
								this.m_BaselineOffsetStack.Push(this.m_BaselineOffset);
								float fontScale = this.m_CurrentFontSize / this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
								this.m_BaselineOffset += this.m_CurrentFontAsset.faceInfo.subscriptOffset * fontScale * this.m_FontScaleMultiplier;
								this.m_FontStyleStack.Add(FontStyles.Subscript);
								this.m_FontStyleInternal |= FontStyles.Subscript;
								return true;
							}
							else
							{
								float value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
								bool flag91 = value == -32768f;
								if (flag91)
								{
									return false;
								}
								switch (tagUnitType)
								{
								case TagUnitType.Pixels:
									this.m_XAdvance = value * (generationSettings.isOrthographic ? 1f : 0.1f);
									return true;
								case TagUnitType.FontUnits:
									this.m_XAdvance = value * this.m_CurrentFontSize * (generationSettings.isOrthographic ? 1f : 0.1f);
									return true;
								case TagUnitType.Percentage:
									this.m_XAdvance = this.m_MarginWidth * value / 100f;
									return true;
								default:
									return false;
								}
							}
						}
						else
						{
							if (markupTag == MarkupTag.SUPERSCRIPT)
							{
								this.m_FontScaleMultiplier *= ((this.m_CurrentFontAsset.faceInfo.superscriptSize > 0f) ? this.m_CurrentFontAsset.faceInfo.superscriptSize : 1f);
								this.m_BaselineOffsetStack.Push(this.m_BaselineOffset);
								float fontScale = this.m_CurrentFontSize / this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
								this.m_BaselineOffset += this.m_CurrentFontAsset.faceInfo.superscriptOffset * fontScale * this.m_FontScaleMultiplier;
								this.m_FontStyleStack.Add(FontStyles.Superscript);
								this.m_FontStyleInternal |= FontStyles.Superscript;
								return true;
							}
							if (markupTag == MarkupTag.SLASH_SUBSCRIPT)
							{
								bool flag92 = (this.m_FontStyleInternal & FontStyles.Subscript) == FontStyles.Subscript;
								if (flag92)
								{
									bool flag93 = this.m_FontScaleMultiplier < 1f;
									if (flag93)
									{
										this.m_BaselineOffset = this.m_BaselineOffsetStack.Pop();
										this.m_FontScaleMultiplier /= ((this.m_CurrentFontAsset.faceInfo.subscriptSize > 0f) ? this.m_CurrentFontAsset.faceInfo.subscriptSize : 1f);
									}
									bool flag94 = this.m_FontStyleStack.Remove(FontStyles.Subscript) == 0;
									if (flag94)
									{
										this.m_FontStyleInternal &= ~FontStyles.Subscript;
									}
								}
								return true;
							}
							if (markupTag != MarkupTag.SLASH_SUPERSCRIPT)
							{
								goto IL_4676;
							}
							bool flag95 = (this.m_FontStyleInternal & FontStyles.Superscript) == FontStyles.Superscript;
							if (flag95)
							{
								bool flag96 = this.m_FontScaleMultiplier < 1f;
								if (flag96)
								{
									this.m_BaselineOffset = this.m_BaselineOffsetStack.Pop();
									this.m_FontScaleMultiplier /= ((this.m_CurrentFontAsset.faceInfo.superscriptSize > 0f) ? this.m_CurrentFontAsset.faceInfo.superscriptSize : 1f);
								}
								bool flag97 = this.m_FontStyleStack.Remove(FontStyles.Superscript) == 0;
								if (flag97)
								{
									this.m_FontStyleInternal &= ~FontStyles.Superscript;
								}
							}
							return true;
						}
					}
					else if (markupTag <= MarkupTag.FONT)
					{
						if (markupTag == MarkupTag.SLASH_POSITION)
						{
							this.m_IsIgnoringAlignment = false;
							return true;
						}
						if (markupTag != MarkupTag.FONT)
						{
							goto IL_4676;
						}
						int fontHashCode = this.m_XmlAttribute[0].valueHashCode;
						int materialAttributeHashCode = this.m_XmlAttribute[1].nameHashCode;
						int materialHashCode = this.m_XmlAttribute[1].valueHashCode;
						bool flag98 = fontHashCode == -620974005;
						if (flag98)
						{
							this.m_CurrentFontAsset = this.m_MaterialReferences[0].fontAsset;
							this.m_CurrentMaterial = this.m_MaterialReferences[0].material;
							this.m_CurrentMaterialIndex = 0;
							this.m_MaterialReferenceStack.Add(this.m_MaterialReferences[0]);
							return true;
						}
						FontAsset tempFont;
						MaterialReferenceManager.TryGetFontAsset(fontHashCode, out tempFont);
						bool flag99 = tempFont == null;
						if (flag99)
						{
							bool flag100 = tempFont == null;
							if (flag100)
							{
								bool flag101 = !canWriteOnAsset;
								if (flag101)
								{
									isThreadSuccess = false;
									return false;
								}
								tempFont = Resources.Load<FontAsset>(textSettings.defaultFontAssetPath + new string(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength));
							}
							bool flag102 = tempFont == null;
							if (flag102)
							{
								return false;
							}
							MaterialReferenceManager.AddFontAsset(tempFont);
						}
						bool flag103 = materialAttributeHashCode == 0 && materialHashCode == 0;
						if (flag103)
						{
							this.m_CurrentMaterial = tempFont.material;
							this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentMaterial, tempFont, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
							this.m_MaterialReferenceStack.Add(this.m_MaterialReferences[this.m_CurrentMaterialIndex]);
						}
						else
						{
							bool flag104 = materialAttributeHashCode == 825491659;
							if (!flag104)
							{
								return false;
							}
							Material tempMaterial;
							bool flag105 = MaterialReferenceManager.TryGetMaterial(materialHashCode, out tempMaterial);
							if (flag105)
							{
								this.m_CurrentMaterial = tempMaterial;
								this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentMaterial, tempFont, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
								this.m_MaterialReferenceStack.Add(this.m_MaterialReferences[this.m_CurrentMaterialIndex]);
							}
							else
							{
								bool flag106 = !canWriteOnAsset;
								if (flag106)
								{
									isThreadSuccess = false;
									return false;
								}
								tempMaterial = Resources.Load<Material>(textSettings.defaultFontAssetPath + new string(this.m_HtmlTag, this.m_XmlAttribute[1].valueStartIndex, this.m_XmlAttribute[1].valueLength));
								bool flag107 = tempMaterial == null;
								if (flag107)
								{
									return false;
								}
								MaterialReferenceManager.AddFontMaterial(materialHashCode, tempMaterial);
								this.m_CurrentMaterial = tempMaterial;
								this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentMaterial, tempFont, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
								this.m_MaterialReferenceStack.Add(this.m_MaterialReferences[this.m_CurrentMaterialIndex]);
							}
						}
						this.m_CurrentFontAsset = tempFont;
						return true;
					}
					else
					{
						if (markupTag == MarkupTag.LINK)
						{
							bool flag108 = this.m_isTextLayoutPhase && !this.m_IsCalculatingPreferredValues && textInfo != null;
							if (flag108)
							{
								int index6 = textInfo.linkCount;
								bool flag109 = index6 + 1 > textInfo.linkInfo.Length;
								if (flag109)
								{
									TextInfo.Resize<LinkInfo>(ref textInfo.linkInfo, index6 + 1);
								}
								textInfo.linkInfo[index6].hashCode = this.m_XmlAttribute[0].valueHashCode;
								textInfo.linkInfo[index6].linkTextfirstCharacterIndex = this.m_CharacterCount;
								textInfo.linkInfo[index6].linkIdFirstCharacterIndex = startIndex + this.m_XmlAttribute[0].valueStartIndex;
								textInfo.linkInfo[index6].SetLinkId(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
							}
							return true;
						}
						if (markupTag == MarkupTag.MARK)
						{
							this.m_FontStyleInternal |= FontStyles.Highlight;
							this.m_FontStyleStack.Add(FontStyles.Highlight);
							Color32 highlightColor = new Color32(byte.MaxValue, byte.MaxValue, 0, 64);
							Offset highlightPadding = Offset.zero;
							int m = 0;
							while (m < this.m_XmlAttribute.Length && this.m_XmlAttribute[m].nameHashCode != 0)
							{
								MarkupTag nameHashCode5 = (MarkupTag)this.m_XmlAttribute[m].nameHashCode;
								MarkupTag markupTag8 = nameHashCode5;
								if (markupTag8 != MarkupTag.PADDING)
								{
									if (markupTag8 != MarkupTag.MARK)
									{
										if (markupTag8 == MarkupTag.COLOR)
										{
											highlightColor = TextGeneratorUtilities.HexCharsToColor(this.m_HtmlTag, this.m_XmlAttribute[m].valueStartIndex, this.m_XmlAttribute[m].valueLength);
										}
									}
									else
									{
										bool flag110 = this.m_XmlAttribute[m].valueType == TagValueType.ColorValue;
										if (flag110)
										{
											highlightColor = TextGeneratorUtilities.HexCharsToColor(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
										}
									}
								}
								else
								{
									int paramCount2 = TextGeneratorUtilities.GetAttributeParameters(this.m_HtmlTag, this.m_XmlAttribute[m].valueStartIndex, this.m_XmlAttribute[m].valueLength, ref this.m_AttributeParameterValues);
									bool flag111 = paramCount2 != 4;
									if (flag111)
									{
										return false;
									}
									highlightPadding = new Offset(this.m_AttributeParameterValues[0], this.m_AttributeParameterValues[1], this.m_AttributeParameterValues[2], this.m_AttributeParameterValues[3]);
									highlightPadding *= this.m_FontSize * 0.01f * (generationSettings.isOrthographic ? 1f : 0.1f);
								}
								m++;
							}
							highlightColor.a = ((this.m_HtmlColor.a < highlightColor.a) ? this.m_HtmlColor.a : highlightColor.a);
							this.m_HighlightState = new HighlightState(highlightColor, highlightPadding);
							this.m_HighlightStateStack.Push(this.m_HighlightState);
							bool flag112 = textInfo != null;
							if (flag112)
							{
								textInfo.hasMultipleColors = true;
							}
							return true;
						}
						if (markupTag != MarkupTag.PAGE)
						{
							goto IL_4676;
						}
						bool flag113 = generationSettings.overflowMode == TextOverflowMode.Page;
						if (flag113)
						{
							this.m_XAdvance = 0f + this.m_TagLineIndent + this.m_TagIndent;
							this.m_LineOffset = 0f;
							this.m_PageNumber++;
							this.m_IsNewPage = true;
						}
						return true;
					}
				}
				else if (markupTag <= MarkupTag.TH)
				{
					if (markupTag <= MarkupTag.SIZE)
					{
						if (markupTag == MarkupTag.NO_BREAK)
						{
							this.m_IsNonBreakingSpace = true;
							return true;
						}
						if (markupTag != MarkupTag.SIZE)
						{
							goto IL_4676;
						}
						float value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
						bool flag114 = value == -32768f;
						if (flag114)
						{
							return false;
						}
						switch (tagUnitType)
						{
						case TagUnitType.Pixels:
						{
							bool flag115 = this.m_HtmlTag[5] == '+';
							if (flag115)
							{
								this.m_CurrentFontSize = this.m_FontSize + value;
								this.m_SizeStack.Add(this.m_CurrentFontSize);
								return true;
							}
							bool flag116 = this.m_HtmlTag[5] == '-';
							if (flag116)
							{
								this.m_CurrentFontSize = this.m_FontSize + value;
								this.m_SizeStack.Add(this.m_CurrentFontSize);
								return true;
							}
							this.m_CurrentFontSize = value;
							this.m_SizeStack.Add(this.m_CurrentFontSize);
							return true;
						}
						case TagUnitType.FontUnits:
							this.m_CurrentFontSize = this.m_FontSize * value;
							this.m_SizeStack.Add(this.m_CurrentFontSize);
							return true;
						case TagUnitType.Percentage:
							this.m_CurrentFontSize = this.m_FontSize * value / 100f;
							this.m_SizeStack.Add(this.m_CurrentFontSize);
							return true;
						default:
							return false;
						}
					}
					else
					{
						if (markupTag == MarkupTag.TR)
						{
							return false;
						}
						if (markupTag == MarkupTag.TD)
						{
							return false;
						}
						if (markupTag != MarkupTag.TH)
						{
							goto IL_4676;
						}
						return false;
					}
				}
				else if (markupTag <= MarkupTag.SLASH_MARK)
				{
					if (markupTag == MarkupTag.SLASH_NO_BREAK)
					{
						this.m_IsNonBreakingSpace = false;
						return true;
					}
					if (markupTag != MarkupTag.SLASH_MARK)
					{
						goto IL_4676;
					}
					bool flag117 = (generationSettings.fontStyle & FontStyles.Highlight) != FontStyles.Highlight;
					if (flag117)
					{
						this.m_HighlightStateStack.Remove();
						this.m_HighlightState = this.m_HighlightStateStack.current;
						bool flag118 = this.m_FontStyleStack.Remove(FontStyles.Highlight) == 0;
						if (flag118)
						{
							this.m_FontStyleInternal &= ~FontStyles.Highlight;
						}
					}
					return true;
				}
				else
				{
					if (markupTag == MarkupTag.SLASH_LINK)
					{
						bool flag119 = this.m_isTextLayoutPhase && !this.m_IsCalculatingPreferredValues && textInfo != null;
						if (flag119)
						{
							bool flag120 = textInfo.linkCount < textInfo.linkInfo.Length;
							if (flag120)
							{
								textInfo.linkInfo[textInfo.linkCount].linkTextLength = this.m_CharacterCount - textInfo.linkInfo[textInfo.linkCount].linkTextfirstCharacterIndex;
								textInfo.linkCount++;
							}
						}
						return true;
					}
					if (markupTag == MarkupTag.SLASH_FONT)
					{
						MaterialReference materialReference2 = this.m_MaterialReferenceStack.Remove();
						this.m_CurrentFontAsset = materialReference2.fontAsset;
						this.m_CurrentMaterial = materialReference2.material;
						this.m_CurrentMaterialIndex = materialReference2.index;
						return true;
					}
					if (markupTag != MarkupTag.SLASH_SIZE)
					{
						goto IL_4676;
					}
					this.m_CurrentFontSize = this.m_SizeStack.Remove();
					return true;
				}
			}
			else if (markupTag <= MarkupTag.SLASH_TH)
			{
				if (markupTag <= MarkupTag.SLASH_LINE_INDENT)
				{
					if (markupTag <= MarkupTag.ALPHA)
					{
						if (markupTag == MarkupTag.ALIGN)
						{
							MarkupTag valueHashCode = (MarkupTag)this.m_XmlAttribute[0].valueHashCode;
							MarkupTag markupTag9 = valueHashCode;
							if (markupTag9 <= MarkupTag.LEFT)
							{
								if (markupTag9 == MarkupTag.CENTER)
								{
									this.m_LineJustification = TextAlignment.MiddleCenter;
									this.m_LineJustificationStack.Add(this.m_LineJustification);
									return true;
								}
								if (markupTag9 == MarkupTag.LEFT)
								{
									this.m_LineJustification = TextAlignment.MiddleLeft;
									this.m_LineJustificationStack.Add(this.m_LineJustification);
									return true;
								}
							}
							else
							{
								if (markupTag9 == MarkupTag.FLUSH)
								{
									this.m_LineJustification = TextAlignment.MiddleFlush;
									this.m_LineJustificationStack.Add(this.m_LineJustification);
									return true;
								}
								if (markupTag9 == MarkupTag.RIGHT)
								{
									this.m_LineJustification = TextAlignment.MiddleRight;
									this.m_LineJustificationStack.Add(this.m_LineJustification);
									return true;
								}
								if (markupTag9 == MarkupTag.JUSTIFIED)
								{
									this.m_LineJustification = TextAlignment.MiddleJustified;
									this.m_LineJustificationStack.Add(this.m_LineJustification);
									return true;
								}
							}
							return false;
						}
						if (markupTag != MarkupTag.ALPHA)
						{
							goto IL_4676;
						}
						bool flag121 = this.m_XmlAttribute[0].valueLength != 3;
						if (flag121)
						{
							return false;
						}
						this.m_HtmlColor.a = (byte)(TextGeneratorUtilities.HexToInt(this.m_HtmlTag[7]) * 16U + TextGeneratorUtilities.HexToInt(this.m_HtmlTag[8]));
						return true;
					}
					else if (markupTag != MarkupTag.COLOR)
					{
						if (markupTag == MarkupTag.CLASS)
						{
							return false;
						}
						if (markupTag != MarkupTag.SLASH_LINE_INDENT)
						{
							goto IL_4676;
						}
						this.m_TagLineIndent = 0f;
						return true;
					}
					else
					{
						bool flag122 = textInfo != null;
						if (flag122)
						{
							textInfo.hasMultipleColors = true;
						}
						bool flag123 = this.m_HtmlTag[6] == '#' || this.m_HtmlTag[7] == '#';
						if (flag123)
						{
							int tagLength = tagCharCount;
							bool flag124 = this.m_HtmlTag[6] == '#';
							if (flag124)
							{
								startIndex = 6;
							}
							else
							{
								startIndex = 7;
								tagLength--;
							}
							this.m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(this.m_HtmlTag, startIndex, tagLength - startIndex);
							this.m_ColorStack.Add(this.m_HtmlColor);
							return true;
						}
						int valueHashCode2 = this.m_XmlAttribute[0].valueHashCode;
						int num11 = valueHashCode2;
						if (num11 <= 2284356)
						{
							if (num11 <= -1108587920)
							{
								if (num11 <= -1812576107)
								{
									if (num11 == -1960309918)
									{
										this.m_HtmlColor = new Color32(0, 0, 160, byte.MaxValue);
										this.m_ColorStack.Add(this.m_HtmlColor);
										return true;
									}
									if (num11 == -1812576107)
									{
										this.m_HtmlColor = new Color32(byte.MaxValue, 0, byte.MaxValue, byte.MaxValue);
										this.m_ColorStack.Add(this.m_HtmlColor);
										return true;
									}
								}
								else
								{
									if (num11 == -1355621936)
									{
										this.m_HtmlColor = new Color32(128, 0, 0, byte.MaxValue);
										this.m_ColorStack.Add(this.m_HtmlColor);
										return true;
									}
									if (num11 == -1250222130)
									{
										this.m_HtmlColor = new Color32(160, 32, 240, byte.MaxValue);
										this.m_ColorStack.Add(this.m_HtmlColor);
										return true;
									}
									if (num11 == -1108587920)
									{
										this.m_HtmlColor = new Color32(byte.MaxValue, 128, 0, byte.MaxValue);
										this.m_ColorStack.Add(this.m_HtmlColor);
										return true;
									}
								}
							}
							else if (num11 <= -960329321)
							{
								if (num11 == -1014785338)
								{
									this.m_HtmlColor = new Color32(0, 0, 0, 0);
									this.m_ColorStack.Add(this.m_HtmlColor);
									return true;
								}
								if (num11 == -1002715645)
								{
									this.m_HtmlColor = new Color32(byte.MaxValue, 0, byte.MaxValue, byte.MaxValue);
									this.m_ColorStack.Add(this.m_HtmlColor);
									return true;
								}
								if (num11 == -960329321)
								{
									this.m_HtmlColor = new Color32(192, 192, 192, byte.MaxValue);
									this.m_ColorStack.Add(this.m_HtmlColor);
									return true;
								}
							}
							else
							{
								if (num11 == -882444668)
								{
									this.m_HtmlColor = Color.yellow;
									this.m_ColorStack.Add(this.m_HtmlColor);
									return true;
								}
								if (num11 == 91635)
								{
									this.m_HtmlColor = Color.red;
									this.m_ColorStack.Add(this.m_HtmlColor);
									return true;
								}
								if (num11 == 2284356)
								{
									this.m_HtmlColor = new Color32(0, byte.MaxValue, byte.MaxValue, byte.MaxValue);
									this.m_ColorStack.Add(this.m_HtmlColor);
									return true;
								}
							}
						}
						else if (num11 <= 2947772)
						{
							if (num11 <= 2638345)
							{
								if (num11 == 2457214)
								{
									this.m_HtmlColor = Color.blue;
									this.m_ColorStack.Add(this.m_HtmlColor);
									return true;
								}
								if (num11 == 2504597)
								{
									this.m_HtmlColor = new Color32(0, byte.MaxValue, byte.MaxValue, byte.MaxValue);
									this.m_ColorStack.Add(this.m_HtmlColor);
									return true;
								}
								if (num11 == 2638345)
								{
									this.m_HtmlColor = new Color32(128, 128, 128, byte.MaxValue);
									this.m_ColorStack.Add(this.m_HtmlColor);
									return true;
								}
							}
							else
							{
								if (num11 == 2656045)
								{
									this.m_HtmlColor = new Color32(0, byte.MaxValue, 0, byte.MaxValue);
									this.m_ColorStack.Add(this.m_HtmlColor);
									return true;
								}
								if (num11 == 2876352)
								{
									this.m_HtmlColor = new Color32(0, 0, 128, byte.MaxValue);
									this.m_ColorStack.Add(this.m_HtmlColor);
									return true;
								}
								if (num11 == 2947772)
								{
									this.m_HtmlColor = new Color32(0, 128, 128, byte.MaxValue);
									this.m_ColorStack.Add(this.m_HtmlColor);
									return true;
								}
							}
						}
						else if (num11 <= 87065851)
						{
							if (num11 == 81017702)
							{
								this.m_HtmlColor = new Color32(165, 42, 42, byte.MaxValue);
								this.m_ColorStack.Add(this.m_HtmlColor);
								return true;
							}
							if (num11 == 81074727)
							{
								this.m_HtmlColor = Color.black;
								this.m_ColorStack.Add(this.m_HtmlColor);
								return true;
							}
							if (num11 == 87065851)
							{
								this.m_HtmlColor = Color.green;
								this.m_ColorStack.Add(this.m_HtmlColor);
								return true;
							}
						}
						else
						{
							if (num11 == 95492953)
							{
								this.m_HtmlColor = new Color32(128, 128, 0, byte.MaxValue);
								this.m_ColorStack.Add(this.m_HtmlColor);
								return true;
							}
							if (num11 == 105680263)
							{
								this.m_HtmlColor = Color.white;
								this.m_ColorStack.Add(this.m_HtmlColor);
								return true;
							}
							if (num11 == 341063360)
							{
								this.m_HtmlColor = new Color32(173, 216, 230, byte.MaxValue);
								this.m_ColorStack.Add(this.m_HtmlColor);
								return true;
							}
						}
						return false;
					}
				}
				else if (markupTag <= MarkupTag.SCALE)
				{
					if (markupTag != MarkupTag.SPACE)
					{
						if (markupTag != MarkupTag.SCALE)
						{
							goto IL_4676;
						}
						float value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
						bool flag125 = value == -32768f;
						if (flag125)
						{
							return false;
						}
						this.m_FXScale = new Vector3(value, 1f, 1f);
						return true;
					}
					else
					{
						float value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
						bool flag126 = value == -32768f;
						if (flag126)
						{
							return false;
						}
						switch (tagUnitType)
						{
						case TagUnitType.Pixels:
							this.m_XAdvance += value * (generationSettings.isOrthographic ? 1f : 0.1f);
							return true;
						case TagUnitType.FontUnits:
							this.m_XAdvance += value * (generationSettings.isOrthographic ? 1f : 0.1f) * this.m_CurrentFontSize;
							return true;
						case TagUnitType.Percentage:
							return false;
						default:
							return false;
						}
					}
				}
				else if (markupTag != MarkupTag.WIDTH)
				{
					if (markupTag == MarkupTag.SLASH_TR)
					{
						return false;
					}
					if (markupTag != MarkupTag.SLASH_TH)
					{
						goto IL_4676;
					}
					return false;
				}
				else
				{
					float value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
					bool flag127 = value == -32768f;
					if (flag127)
					{
						return false;
					}
					switch (tagUnitType)
					{
					case TagUnitType.Pixels:
						this.m_Width = value * (generationSettings.isOrthographic ? 1f : 0.1f);
						break;
					case TagUnitType.FontUnits:
						return false;
					case TagUnitType.Percentage:
						this.m_Width = this.m_MarginWidth * value / 100f;
						break;
					}
					return true;
				}
			}
			else if (markupTag <= MarkupTag.TABLE)
			{
				if (markupTag <= MarkupTag.SLASH_SMALLCAPS)
				{
					if (markupTag == MarkupTag.SLASH_TD)
					{
						return false;
					}
					if (markupTag != MarkupTag.SLASH_SMALLCAPS)
					{
						goto IL_4676;
					}
					bool flag128 = (generationSettings.fontStyle & FontStyles.SmallCaps) != FontStyles.SmallCaps;
					if (flag128)
					{
						bool flag129 = this.m_FontStyleStack.Remove(FontStyles.SmallCaps) == 0;
						if (flag129)
						{
							this.m_FontStyleInternal &= ~FontStyles.SmallCaps;
						}
					}
					return true;
				}
				else
				{
					if (markupTag == MarkupTag.SLASH_LINE_HEIGHT)
					{
						this.m_LineHeight = -32767f;
						return true;
					}
					if (markupTag != MarkupTag.ALLCAPS)
					{
						if (markupTag != MarkupTag.TABLE)
						{
							goto IL_4676;
						}
						return false;
					}
				}
			}
			else if (markupTag <= MarkupTag.SLASH_ALIGN)
			{
				if (markupTag != MarkupTag.MATERIAL)
				{
					if (markupTag == MarkupTag.SLASH_COLOR)
					{
						this.m_HtmlColor = this.m_ColorStack.Remove();
						return true;
					}
					if (markupTag != MarkupTag.SLASH_ALIGN)
					{
						goto IL_4676;
					}
					this.m_LineJustification = this.m_LineJustificationStack.Remove();
					return true;
				}
				else
				{
					int materialHashCode = this.m_XmlAttribute[0].valueHashCode;
					bool flag130 = materialHashCode == -620974005;
					if (flag130)
					{
						this.m_CurrentMaterial = this.m_MaterialReferences[0].material;
						this.m_CurrentMaterialIndex = 0;
						this.m_MaterialReferenceStack.Add(this.m_MaterialReferences[0]);
						return true;
					}
					Material tempMaterial;
					bool flag131 = MaterialReferenceManager.TryGetMaterial(materialHashCode, out tempMaterial);
					if (flag131)
					{
						this.m_CurrentMaterial = tempMaterial;
						this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentMaterial, this.m_CurrentFontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
						this.m_MaterialReferenceStack.Add(this.m_MaterialReferences[this.m_CurrentMaterialIndex]);
					}
					else
					{
						bool flag132 = !canWriteOnAsset;
						if (flag132)
						{
							isThreadSuccess = false;
							return false;
						}
						tempMaterial = Resources.Load<Material>(textSettings.defaultFontAssetPath + new string(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength));
						bool flag133 = tempMaterial == null;
						if (flag133)
						{
							return false;
						}
						MaterialReferenceManager.AddFontMaterial(materialHashCode, tempMaterial);
						this.m_CurrentMaterial = tempMaterial;
						this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentMaterial, this.m_CurrentFontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
						this.m_MaterialReferenceStack.Add(this.m_MaterialReferences[this.m_CurrentMaterialIndex]);
					}
					return true;
				}
			}
			else
			{
				if (markupTag == MarkupTag.SLASH_WIDTH)
				{
					this.m_Width = -1f;
					return true;
				}
				if (markupTag == MarkupTag.SLASH_SCALE)
				{
					this.m_FXScale = Vector3.one;
					return true;
				}
				if (markupTag != MarkupTag.VERTICAL_OFFSET)
				{
					goto IL_4676;
				}
				float value = TextGeneratorUtilities.ConvertToFloat(this.m_HtmlTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
				bool flag134 = value == -32768f;
				if (flag134)
				{
					return false;
				}
				switch (tagUnitType)
				{
				case TagUnitType.Pixels:
					this.m_BaselineOffset = value * (generationSettings.isOrthographic ? 1f : 0.1f);
					return true;
				case TagUnitType.FontUnits:
					this.m_BaselineOffset = value * (generationSettings.isOrthographic ? 1f : 0.1f) * this.m_CurrentFontSize;
					return true;
				case TagUnitType.Percentage:
					return false;
				default:
					return false;
				}
			}
			IL_3C84:
			this.m_FontStyleInternal |= FontStyles.UpperCase;
			this.m_FontStyleStack.Add(FontStyles.UpperCase);
			return true;
			IL_4676:
			return false;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000104C4 File Offset: 0x0000E6C4
		private void ClearMarkupTagAttributes()
		{
			int length = this.m_XmlAttribute.Length;
			for (int i = 0; i < length; i++)
			{
				this.m_XmlAttribute[i] = default(RichTextTagAttribute);
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00010500 File Offset: 0x0000E700
		private void SaveWordWrappingState(ref WordWrapState state, int index, int count, TextInfo textInfo)
		{
			state.currentFontAsset = this.m_CurrentFontAsset;
			state.currentSpriteAsset = this.m_CurrentSpriteAsset;
			state.currentMaterial = this.m_CurrentMaterial;
			state.currentMaterialIndex = this.m_CurrentMaterialIndex;
			state.previousWordBreak = index;
			state.totalCharacterCount = count;
			state.visibleCharacterCount = this.m_LineVisibleCharacterCount;
			state.visibleSpaceCount = this.m_LineVisibleSpaceCount;
			state.visibleLinkCount = textInfo.linkCount;
			state.firstCharacterIndex = this.m_FirstCharacterOfLine;
			state.firstVisibleCharacterIndex = this.m_FirstVisibleCharacterOfLine;
			state.lastVisibleCharIndex = this.m_LastVisibleCharacterOfLine;
			state.fontStyle = this.m_FontStyleInternal;
			state.italicAngle = this.m_ItalicAngle;
			state.fontScaleMultiplier = this.m_FontScaleMultiplier;
			state.currentFontSize = this.m_CurrentFontSize;
			state.xAdvance = this.m_XAdvance;
			state.maxCapHeight = this.m_MaxCapHeight;
			state.maxAscender = this.m_MaxAscender;
			state.maxDescender = this.m_MaxDescender;
			state.maxLineAscender = this.m_MaxLineAscender;
			state.maxLineDescender = this.m_MaxLineDescender;
			state.startOfLineAscender = this.m_StartOfLineAscender;
			state.preferredWidth = this.m_PreferredWidth;
			state.preferredHeight = this.m_PreferredHeight;
			state.meshExtents = this.m_MeshExtents;
			state.pageAscender = this.m_PageAscender;
			state.lineNumber = this.m_LineNumber;
			state.lineOffset = this.m_LineOffset;
			state.baselineOffset = this.m_BaselineOffset;
			state.isDrivenLineSpacing = this.m_IsDrivenLineSpacing;
			state.vertexColor = this.m_HtmlColor;
			state.underlineColor = this.m_UnderlineColor;
			state.strikethroughColor = this.m_StrikethroughColor;
			state.highlightColor = this.m_HighlightColor;
			state.highlightState = this.m_HighlightState;
			state.isNonBreakingSpace = this.m_IsNonBreakingSpace;
			state.tagNoParsing = this.m_TagNoParsing;
			state.fxScale = this.m_FXScale;
			state.fxRotation = this.m_FXRotation;
			state.basicStyleStack = this.m_FontStyleStack;
			state.italicAngleStack = this.m_ItalicAngleStack;
			state.colorStack = this.m_ColorStack;
			state.underlineColorStack = this.m_UnderlineColorStack;
			state.strikethroughColorStack = this.m_StrikethroughColorStack;
			state.highlightColorStack = this.m_HighlightColorStack;
			state.colorGradientStack = this.m_ColorGradientStack;
			state.highlightStateStack = this.m_HighlightStateStack;
			state.sizeStack = this.m_SizeStack;
			state.indentStack = this.m_IndentStack;
			state.fontWeightStack = this.m_FontWeightStack;
			state.styleStack = this.m_StyleStack;
			state.baselineStack = this.m_BaselineOffsetStack;
			state.actionStack = this.m_ActionStack;
			state.materialReferenceStack = this.m_MaterialReferenceStack;
			state.lineJustificationStack = this.m_LineJustificationStack;
			state.lastBaseGlyphIndex = this.m_LastBaseGlyphIndex;
			state.spriteAnimationId = this.m_SpriteAnimationId;
			bool flag = this.m_LineNumber < textInfo.lineInfo.Length;
			if (flag)
			{
				state.lineInfo = textInfo.lineInfo[this.m_LineNumber];
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000107EC File Offset: 0x0000E9EC
		private int RestoreWordWrappingState(ref WordWrapState state, TextInfo textInfo)
		{
			int index = state.previousWordBreak;
			this.m_CurrentFontAsset = state.currentFontAsset;
			this.m_CurrentSpriteAsset = state.currentSpriteAsset;
			this.m_CurrentMaterial = state.currentMaterial;
			this.m_CurrentMaterialIndex = state.currentMaterialIndex;
			this.m_CharacterCount = state.totalCharacterCount + 1;
			this.m_LineVisibleCharacterCount = state.visibleCharacterCount;
			this.m_LineVisibleSpaceCount = state.visibleSpaceCount;
			textInfo.linkCount = state.visibleLinkCount;
			this.m_FirstCharacterOfLine = state.firstCharacterIndex;
			this.m_FirstVisibleCharacterOfLine = state.firstVisibleCharacterIndex;
			this.m_LastVisibleCharacterOfLine = state.lastVisibleCharIndex;
			this.m_FontStyleInternal = state.fontStyle;
			this.m_ItalicAngle = state.italicAngle;
			this.m_FontScaleMultiplier = state.fontScaleMultiplier;
			this.m_CurrentFontSize = state.currentFontSize;
			this.m_XAdvance = state.xAdvance;
			this.m_MaxCapHeight = state.maxCapHeight;
			this.m_MaxAscender = state.maxAscender;
			this.m_MaxDescender = state.maxDescender;
			this.m_MaxLineAscender = state.maxLineAscender;
			this.m_MaxLineDescender = state.maxLineDescender;
			this.m_StartOfLineAscender = state.startOfLineAscender;
			this.m_PreferredWidth = state.preferredWidth;
			this.m_PreferredHeight = state.preferredHeight;
			this.m_MeshExtents = state.meshExtents;
			this.m_PageAscender = state.pageAscender;
			this.m_LineNumber = state.lineNumber;
			this.m_LineOffset = state.lineOffset;
			this.m_BaselineOffset = state.baselineOffset;
			this.m_IsDrivenLineSpacing = state.isDrivenLineSpacing;
			this.m_HtmlColor = state.vertexColor;
			this.m_UnderlineColor = state.underlineColor;
			this.m_StrikethroughColor = state.strikethroughColor;
			this.m_HighlightColor = state.highlightColor;
			this.m_HighlightState = state.highlightState;
			this.m_IsNonBreakingSpace = state.isNonBreakingSpace;
			this.m_TagNoParsing = state.tagNoParsing;
			this.m_FXScale = state.fxScale;
			this.m_FXRotation = state.fxRotation;
			this.m_FontStyleStack = state.basicStyleStack;
			this.m_ItalicAngleStack = state.italicAngleStack;
			this.m_ColorStack = state.colorStack;
			this.m_UnderlineColorStack = state.underlineColorStack;
			this.m_StrikethroughColorStack = state.strikethroughColorStack;
			this.m_HighlightColorStack = state.highlightColorStack;
			this.m_ColorGradientStack = state.colorGradientStack;
			this.m_HighlightStateStack = state.highlightStateStack;
			this.m_SizeStack = state.sizeStack;
			this.m_IndentStack = state.indentStack;
			this.m_FontWeightStack = state.fontWeightStack;
			this.m_StyleStack = state.styleStack;
			this.m_BaselineOffsetStack = state.baselineStack;
			this.m_ActionStack = state.actionStack;
			this.m_MaterialReferenceStack = state.materialReferenceStack;
			this.m_LineJustificationStack = state.lineJustificationStack;
			this.m_LastBaseGlyphIndex = state.lastBaseGlyphIndex;
			this.m_SpriteAnimationId = state.spriteAnimationId;
			bool flag = this.m_LineNumber < textInfo.lineInfo.Length;
			if (flag)
			{
				textInfo.lineInfo[this.m_LineNumber] = state.lineInfo;
			}
			return index;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00010AE0 File Offset: 0x0000ECE0
		private void SaveGlyphVertexInfo(float padding, float stylePadding, Color32 vertexColor, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.position = textInfo.textElementInfo[this.m_CharacterCount].bottomLeft;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.position = textInfo.textElementInfo[this.m_CharacterCount].topLeft;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.position = textInfo.textElementInfo[this.m_CharacterCount].topRight;
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.position = textInfo.textElementInfo[this.m_CharacterCount].bottomRight;
			vertexColor.a = ((this.m_FontColor32.a < vertexColor.a) ? this.m_FontColor32.a : vertexColor.a);
			bool isColorGlyph = (this.m_CurrentFontAsset.m_AtlasRenderMode & (GlyphRenderMode)65536) == (GlyphRenderMode)65536;
			bool flag = generationSettings.fontColorGradient == null || isColorGlyph;
			if (flag)
			{
				vertexColor = (isColorGlyph ? new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, vertexColor.a) : vertexColor);
				textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.color = vertexColor;
				textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.color = vertexColor;
				textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.color = vertexColor;
				textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.color = vertexColor;
			}
			else
			{
				bool flag2 = !generationSettings.overrideRichTextColors && this.m_ColorStack.index > 1;
				if (flag2)
				{
					textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.color = vertexColor;
					textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.color = vertexColor;
					textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.color = vertexColor;
					textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.color = vertexColor;
				}
				else
				{
					bool flag3 = generationSettings.fontColorGradientPreset != null;
					if (flag3)
					{
						textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.color = generationSettings.fontColorGradientPreset.bottomLeft * vertexColor;
						textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.color = generationSettings.fontColorGradientPreset.topLeft * vertexColor;
						textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.color = generationSettings.fontColorGradientPreset.topRight * vertexColor;
						textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.color = generationSettings.fontColorGradientPreset.bottomRight * vertexColor;
					}
					else
					{
						textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.color = generationSettings.fontColorGradient.bottomLeft * vertexColor;
						textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.color = generationSettings.fontColorGradient.topLeft * vertexColor;
						textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.color = generationSettings.fontColorGradient.topRight * vertexColor;
						textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.color = generationSettings.fontColorGradient.bottomRight * vertexColor;
					}
				}
			}
			bool flag4 = this.m_ColorGradientPreset != null && !isColorGlyph;
			if (flag4)
			{
				bool colorGradientPresetIsTinted = this.m_ColorGradientPresetIsTinted;
				if (colorGradientPresetIsTinted)
				{
					TextElementInfo[] textElementInfo = textInfo.textElementInfo;
					int characterCount = this.m_CharacterCount;
					textElementInfo[characterCount].vertexBottomLeft.color = textElementInfo[characterCount].vertexBottomLeft.color * this.m_ColorGradientPreset.bottomLeft;
					TextElementInfo[] textElementInfo2 = textInfo.textElementInfo;
					int characterCount2 = this.m_CharacterCount;
					textElementInfo2[characterCount2].vertexTopLeft.color = textElementInfo2[characterCount2].vertexTopLeft.color * this.m_ColorGradientPreset.topLeft;
					TextElementInfo[] textElementInfo3 = textInfo.textElementInfo;
					int characterCount3 = this.m_CharacterCount;
					textElementInfo3[characterCount3].vertexTopRight.color = textElementInfo3[characterCount3].vertexTopRight.color * this.m_ColorGradientPreset.topRight;
					TextElementInfo[] textElementInfo4 = textInfo.textElementInfo;
					int characterCount4 = this.m_CharacterCount;
					textElementInfo4[characterCount4].vertexBottomRight.color = textElementInfo4[characterCount4].vertexBottomRight.color * this.m_ColorGradientPreset.bottomRight;
				}
				else
				{
					textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.color = this.m_ColorGradientPreset.bottomLeft.MinAlpha(vertexColor);
					textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.color = this.m_ColorGradientPreset.topLeft.MinAlpha(vertexColor);
					textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.color = this.m_ColorGradientPreset.topRight.MinAlpha(vertexColor);
					textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.color = this.m_ColorGradientPreset.bottomRight.MinAlpha(vertexColor);
				}
			}
			stylePadding = 0f;
			Glyph altGlyph = textInfo.textElementInfo[this.m_CharacterCount].alternativeGlyph;
			GlyphRect glyphRect = ((altGlyph == null) ? this.m_CachedTextElement.m_Glyph.glyphRect : altGlyph.glyphRect);
			Vector2 uVBottomLeft;
			uVBottomLeft.x = ((float)glyphRect.x - padding - stylePadding) / (float)this.m_CurrentFontAsset.atlasWidth;
			uVBottomLeft.y = ((float)glyphRect.y - padding - stylePadding) / (float)this.m_CurrentFontAsset.atlasHeight;
			Vector2 uVTopLeft;
			uVTopLeft.x = uVBottomLeft.x;
			uVTopLeft.y = ((float)glyphRect.y + padding + stylePadding + (float)glyphRect.height) / (float)this.m_CurrentFontAsset.atlasHeight;
			Vector2 uVTopRight;
			uVTopRight.x = ((float)glyphRect.x + padding + stylePadding + (float)glyphRect.width) / (float)this.m_CurrentFontAsset.atlasWidth;
			uVTopRight.y = uVTopLeft.y;
			Vector2 uVBottomRight;
			uVBottomRight.x = uVTopRight.x;
			uVBottomRight.y = uVBottomLeft.y;
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.uv = uVBottomLeft;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.uv = uVTopLeft;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.uv = uVTopRight;
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.uv = uVBottomRight;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000112DC File Offset: 0x0000F4DC
		private void SaveSpriteVertexInfo(Color32 vertexColor, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.position = textInfo.textElementInfo[this.m_CharacterCount].bottomLeft;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.position = textInfo.textElementInfo[this.m_CharacterCount].topLeft;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.position = textInfo.textElementInfo[this.m_CharacterCount].topRight;
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.position = textInfo.textElementInfo[this.m_CharacterCount].bottomRight;
			bool tintSprites = generationSettings.tintSprites;
			if (tintSprites)
			{
				this.m_TintSprite = true;
			}
			Color32 spriteColor = (this.m_TintSprite ? ColorUtilities.MultiplyColors(this.m_SpriteColor, vertexColor) : this.m_SpriteColor);
			spriteColor.a = ((spriteColor.a < this.m_FontColor32.a) ? ((spriteColor.a < vertexColor.a) ? spriteColor.a : vertexColor.a) : this.m_FontColor32.a);
			Color32 c0 = spriteColor;
			Color32 c = spriteColor;
			Color32 c2 = spriteColor;
			Color32 c3 = spriteColor;
			bool flag = generationSettings.fontColorGradient != null;
			if (flag)
			{
				bool flag2 = generationSettings.fontColorGradientPreset != null;
				if (flag2)
				{
					c0 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(c0, generationSettings.fontColorGradientPreset.bottomLeft) : c0);
					c = (this.m_TintSprite ? ColorUtilities.MultiplyColors(c, generationSettings.fontColorGradientPreset.topLeft) : c);
					c2 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(c2, generationSettings.fontColorGradientPreset.topRight) : c2);
					c3 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(c3, generationSettings.fontColorGradientPreset.bottomRight) : c3);
				}
				else
				{
					c0 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(c0, generationSettings.fontColorGradient.bottomLeft) : c0);
					c = (this.m_TintSprite ? ColorUtilities.MultiplyColors(c, generationSettings.fontColorGradient.topLeft) : c);
					c2 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(c2, generationSettings.fontColorGradient.topRight) : c2);
					c3 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(c3, generationSettings.fontColorGradient.bottomRight) : c3);
				}
			}
			bool flag3 = this.m_ColorGradientPreset != null;
			if (flag3)
			{
				c0 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(c0, this.m_ColorGradientPreset.bottomLeft) : c0);
				c = (this.m_TintSprite ? ColorUtilities.MultiplyColors(c, this.m_ColorGradientPreset.topLeft) : c);
				c2 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(c2, this.m_ColorGradientPreset.topRight) : c2);
				c3 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(c3, this.m_ColorGradientPreset.bottomRight) : c3);
			}
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.color = c0;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.color = c;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.color = c2;
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.color = c3;
			Vector2 uv0 = new Vector2((float)this.m_CachedTextElement.glyph.glyphRect.x / this.m_CurrentSpriteAsset.width, (float)this.m_CachedTextElement.glyph.glyphRect.y / this.m_CurrentSpriteAsset.height);
			Vector2 uv = new Vector2(uv0.x, (float)(this.m_CachedTextElement.glyph.glyphRect.y + this.m_CachedTextElement.glyph.glyphRect.height) / this.m_CurrentSpriteAsset.height);
			Vector2 uv2 = new Vector2((float)(this.m_CachedTextElement.glyph.glyphRect.x + this.m_CachedTextElement.glyph.glyphRect.width) / this.m_CurrentSpriteAsset.width, uv.y);
			Vector2 uv3 = new Vector2(uv2.x, uv0.y);
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.uv = uv0;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.uv = uv;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.uv = uv2;
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.uv = uv3;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00011814 File Offset: 0x0000FA14
		private void DrawUnderlineMesh(Vector3 start, Vector3 end, float startScale, float endScale, float maxScale, float sdfScale, Color32 underlineColor, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			this.GetUnderlineSpecialCharacter(generationSettings);
			bool flag = this.m_Underline.character == null;
			if (flag)
			{
				bool displayWarnings = generationSettings.textSettings.displayWarnings;
				if (displayWarnings)
				{
					Debug.LogWarning("Unable to add underline or strikethrough since the character [0x5F] used by these features is not present in the Font Asset assigned to this text object.");
				}
			}
			else
			{
				int index = textInfo.meshInfo[this.m_CurrentMaterialIndex].vertexCount;
				int newVerticesCount = index + 12;
				bool flag2 = newVerticesCount > textInfo.meshInfo[this.m_CurrentMaterialIndex].vertexBufferSize;
				if (flag2)
				{
					textInfo.meshInfo[this.m_CurrentMaterialIndex].ResizeMeshInfo(newVerticesCount / 4, generationSettings.isIMGUI);
				}
				start.y = Mathf.Min(start.y, end.y);
				end.y = Mathf.Min(start.y, end.y);
				GlyphMetrics underlineGlyphMetrics = this.m_Underline.character.glyph.metrics;
				GlyphRect underlineGlyphRect = this.m_Underline.character.glyph.glyphRect;
				float underlineThickness = this.m_Underline.fontAsset.faceInfo.underlineThickness;
				start.x += (startScale - maxScale) * this.m_Padding;
				end.x += (maxScale - endScale) * this.m_Padding;
				float segmentWidth = (underlineGlyphMetrics.width * 0.5f + this.m_Padding) * maxScale;
				float segmentRatio = 1f;
				float segmentWidthSum = 2f * segmentWidth;
				float meshWidth = end.x - start.x;
				bool flag3 = meshWidth < segmentWidthSum;
				if (flag3)
				{
					segmentRatio = meshWidth / segmentWidthSum;
					segmentWidth *= segmentRatio;
				}
				TextCoreVertex[] vertexData = textInfo.meshInfo[this.m_CurrentMaterialIndex].vertexData;
				float posLeft = start.x;
				float posMidLeft = start.x + segmentWidth;
				float posMidRight = end.x - segmentWidth;
				float posRight = end.x;
				float posBottom = start.y - (underlineThickness + this.m_Padding) * maxScale;
				float posTop = start.y + this.m_Padding * maxScale;
				bool flag4 = textInfo.vertexDataLayout == VertexDataLayout.VBO;
				if (flag4)
				{
					vertexData[index].position = new Vector3(posLeft, posBottom);
					vertexData[index + 1].position = new Vector3(posLeft, posTop);
					vertexData[index + 2].position = new Vector3(posMidLeft, posTop);
					vertexData[index + 3].position = new Vector3(posMidLeft, posBottom);
					vertexData[index + 4].position = new Vector3(posMidLeft, posBottom);
					vertexData[index + 5].position = new Vector3(posMidLeft, posTop);
					vertexData[index + 6].position = new Vector3(posMidRight, posTop);
					vertexData[index + 7].position = new Vector3(posMidRight, posBottom);
					vertexData[index + 8].position = new Vector3(posMidRight, posBottom);
					vertexData[index + 9].position = new Vector3(posMidRight, posTop);
					vertexData[index + 10].position = new Vector3(posRight, posTop);
					vertexData[index + 11].position = new Vector3(posRight, posBottom);
				}
				else
				{
					Vector3[] vertices = textInfo.meshInfo[this.m_CurrentMaterialIndex].vertices;
					vertices[index] = new Vector3(posLeft, posBottom);
					vertices[index + 1] = new Vector3(posLeft, posTop);
					vertices[index + 2] = new Vector3(posMidLeft, posTop);
					vertices[index + 3] = new Vector3(posMidLeft, posBottom);
					vertices[index + 4] = new Vector3(posMidLeft, posBottom);
					vertices[index + 5] = new Vector3(posMidLeft, posTop);
					vertices[index + 6] = new Vector3(posMidRight, posTop);
					vertices[index + 7] = new Vector3(posMidRight, posBottom);
					vertices[index + 8] = new Vector3(posMidRight, posBottom);
					vertices[index + 9] = new Vector3(posMidRight, posTop);
					vertices[index + 10] = new Vector3(posRight, posTop);
					vertices[index + 11] = new Vector3(posRight, posBottom);
				}
				bool inverseYAxis = generationSettings.inverseYAxis;
				if (inverseYAxis)
				{
					Vector3 axisOffset;
					axisOffset.x = 0f;
					axisOffset.y = generationSettings.screenRect.height;
					axisOffset.z = 0f;
					bool flag5 = textInfo.vertexDataLayout == VertexDataLayout.VBO;
					if (flag5)
					{
						for (int i = 0; i < 12; i++)
						{
							textInfo.meshInfo[this.m_CurrentMaterialIndex].vertexData[index + i].position.y = textInfo.meshInfo[this.m_CurrentMaterialIndex].vertexData[index + i].position.y * -1f + axisOffset.y;
						}
					}
					else
					{
						for (int j = 0; j < 12; j++)
						{
							textInfo.meshInfo[this.m_CurrentMaterialIndex].vertices[index + j].y = textInfo.meshInfo[this.m_CurrentMaterialIndex].vertices[index + j].y * -1f + axisOffset.y;
						}
					}
				}
				float invAtlasWidth = 1f / (float)this.m_Underline.fontAsset.atlasWidth;
				float invAtlasHeight = 1f / (float)this.m_Underline.fontAsset.atlasHeight;
				float uvSegmentWidth = ((float)underlineGlyphRect.width * 0.5f + this.m_Padding) * segmentRatio * invAtlasWidth;
				float uvLeft0 = ((float)underlineGlyphRect.x - this.m_Padding) * invAtlasWidth;
				float uvLeft = uvLeft0 + uvSegmentWidth;
				float uvMid = ((float)underlineGlyphRect.x + (float)underlineGlyphRect.width * 0.5f) * invAtlasWidth;
				float uvRight = ((float)(underlineGlyphRect.x + underlineGlyphRect.width) + this.m_Padding) * invAtlasWidth;
				float uvRight2 = uvRight - uvSegmentWidth;
				float uvBottom = ((float)underlineGlyphRect.y - this.m_Padding) * invAtlasHeight;
				float uvTop = ((float)(underlineGlyphRect.y + underlineGlyphRect.height) + this.m_Padding) * invAtlasHeight;
				bool flag6 = textInfo.vertexDataLayout == VertexDataLayout.VBO;
				if (flag6)
				{
					vertexData[index].uv0 = new Vector4(uvLeft0, uvBottom);
					vertexData[1 + index].uv0 = new Vector4(uvLeft0, uvTop);
					vertexData[2 + index].uv0 = new Vector4(uvLeft, uvTop);
					vertexData[3 + index].uv0 = new Vector4(uvLeft, uvBottom);
					vertexData[4 + index].uv0 = new Vector4(uvMid, uvBottom);
					vertexData[5 + index].uv0 = new Vector4(uvMid, uvTop);
					vertexData[6 + index].uv0 = new Vector4(uvMid, uvTop);
					vertexData[7 + index].uv0 = new Vector4(uvMid, uvBottom);
					vertexData[8 + index].uv0 = new Vector4(uvRight2, uvBottom);
					vertexData[9 + index].uv0 = new Vector4(uvRight2, uvTop);
					vertexData[10 + index].uv0 = new Vector4(uvRight, uvTop);
					vertexData[11 + index].uv0 = new Vector4(uvRight, uvBottom);
				}
				else
				{
					float xScale = Mathf.Abs(sdfScale);
					Vector4[] uvs0 = textInfo.meshInfo[this.m_CurrentMaterialIndex].uvs0;
					uvs0[index] = new Vector4(uvLeft0, uvBottom, 0f, xScale);
					uvs0[1 + index] = new Vector4(uvLeft0, uvTop, 0f, xScale);
					uvs0[2 + index] = new Vector4(uvLeft, uvTop, 0f, xScale);
					uvs0[3 + index] = new Vector4(uvLeft, uvBottom, 0f, xScale);
					uvs0[4 + index] = new Vector4(uvMid, uvBottom, 0f, xScale);
					uvs0[5 + index] = new Vector4(uvMid, uvTop, 0f, xScale);
					uvs0[6 + index] = new Vector4(uvMid, uvTop, 0f, xScale);
					uvs0[7 + index] = new Vector4(uvMid, uvBottom, 0f, xScale);
					uvs0[8 + index] = new Vector4(uvRight2, uvBottom, 0f, xScale);
					uvs0[9 + index] = new Vector4(uvRight2, uvTop, 0f, xScale);
					uvs0[10 + index] = new Vector4(uvRight, uvTop, 0f, xScale);
					uvs0[11 + index] = new Vector4(uvRight, uvBottom, 0f, xScale);
				}
				float invMeshWidth = 1f / meshWidth;
				bool flag7 = textInfo.vertexDataLayout == VertexDataLayout.VBO;
				if (flag7)
				{
					float max_UvX = (vertexData[index + 2].position.x - start.x) * invMeshWidth;
					vertexData[index].uv2 = new Vector2(0f, 0f);
					vertexData[1 + index].uv2 = new Vector2(0f, 1f);
					vertexData[2 + index].uv2 = new Vector2(max_UvX, 1f);
					vertexData[3 + index].uv2 = new Vector2(max_UvX, 0f);
					float min_UvX = (vertexData[index + 4].position.x - start.x) * invMeshWidth;
					max_UvX = (vertexData[index + 6].position.x - start.x) * invMeshWidth;
					vertexData[4 + index].uv2 = new Vector2(min_UvX, 0f);
					vertexData[5 + index].uv2 = new Vector2(min_UvX, 1f);
					vertexData[6 + index].uv2 = new Vector2(max_UvX, 1f);
					vertexData[7 + index].uv2 = new Vector2(max_UvX, 0f);
					min_UvX = (vertexData[index + 8].position.x - start.x) * invMeshWidth;
					vertexData[8 + index].uv2 = new Vector2(min_UvX, 0f);
					vertexData[9 + index].uv2 = new Vector2(min_UvX, 1f);
					vertexData[10 + index].uv2 = new Vector2(1f, 1f);
					vertexData[11 + index].uv2 = new Vector2(1f, 0f);
				}
				else
				{
					Vector3[] vertices2 = textInfo.meshInfo[this.m_CurrentMaterialIndex].vertices;
					float max_UvX2 = (vertices2[index + 2].x - start.x) * invMeshWidth;
					Vector2[] uvs = textInfo.meshInfo[this.m_CurrentMaterialIndex].uvs2;
					uvs[index] = new Vector2(0f, 0f);
					uvs[1 + index] = new Vector2(0f, 1f);
					uvs[2 + index] = new Vector2(max_UvX2, 1f);
					uvs[3 + index] = new Vector2(max_UvX2, 0f);
					float min_UvX = (vertices2[index + 4].x - start.x) * invMeshWidth;
					max_UvX2 = (vertices2[index + 6].x - start.x) * invMeshWidth;
					uvs[4 + index] = new Vector2(min_UvX, 0f);
					uvs[5 + index] = new Vector2(min_UvX, 1f);
					uvs[6 + index] = new Vector2(max_UvX2, 1f);
					uvs[7 + index] = new Vector2(max_UvX2, 0f);
					min_UvX = (vertices2[index + 8].x - start.x) * invMeshWidth;
					uvs[8 + index] = new Vector2(min_UvX, 0f);
					uvs[9 + index] = new Vector2(min_UvX, 1f);
					uvs[10 + index] = new Vector2(1f, 1f);
					uvs[11 + index] = new Vector2(1f, 0f);
				}
				underlineColor.a = ((this.m_FontColor32.a < underlineColor.a) ? this.m_FontColor32.a : underlineColor.a);
				bool flag8 = textInfo.vertexDataLayout == VertexDataLayout.VBO;
				if (flag8)
				{
					for (int k = 0; k < 12; k++)
					{
						vertexData[k + index].color = underlineColor;
					}
				}
				else
				{
					Color32[] colors32 = textInfo.meshInfo[this.m_CurrentMaterialIndex].colors32;
					for (int l = 0; l < 12; l++)
					{
						colors32[l + index] = underlineColor;
					}
				}
				MeshInfo[] meshInfo = textInfo.meshInfo;
				int currentMaterialIndex = this.m_CurrentMaterialIndex;
				meshInfo[currentMaterialIndex].vertexCount = meshInfo[currentMaterialIndex].vertexCount + 12;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000125C8 File Offset: 0x000107C8
		private void DrawTextHighlight(Vector3 start, Vector3 end, Color32 highlightColor, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			this.GetUnderlineSpecialCharacter(generationSettings);
			bool flag = this.m_Underline.character == null;
			if (flag)
			{
				bool displayWarnings = generationSettings.textSettings.displayWarnings;
				if (displayWarnings)
				{
					Debug.LogWarning("Unable to add highlight since the primary Font Asset doesn't contain the underline character.");
				}
			}
			else
			{
				int index = textInfo.meshInfo[this.m_CurrentMaterialIndex].vertexCount;
				int newVerticesCount = index + 4;
				bool flag2 = newVerticesCount > textInfo.meshInfo[this.m_CurrentMaterialIndex].vertexBufferSize;
				if (flag2)
				{
					textInfo.meshInfo[this.m_CurrentMaterialIndex].ResizeMeshInfo(newVerticesCount / 4, generationSettings.isIMGUI);
				}
				TextCoreVertex[] vertexData = textInfo.meshInfo[this.m_CurrentMaterialIndex].vertexData;
				bool flag3 = textInfo.vertexDataLayout == VertexDataLayout.VBO;
				if (flag3)
				{
					vertexData[index].position = start;
					vertexData[index + 1].position = new Vector3(start.x, end.y, 0f);
					vertexData[index + 2].position = end;
					vertexData[index + 3].position = new Vector3(end.x, start.y, 0f);
				}
				else
				{
					Vector3[] vertices = textInfo.meshInfo[this.m_CurrentMaterialIndex].vertices;
					vertices[index] = start;
					vertices[index + 1] = new Vector3(start.x, end.y, 0f);
					vertices[index + 2] = end;
					vertices[index + 3] = new Vector3(end.x, start.y, 0f);
				}
				bool inverseYAxis = generationSettings.inverseYAxis;
				if (inverseYAxis)
				{
					Vector3 axisOffset;
					axisOffset.x = 0f;
					axisOffset.y = generationSettings.screenRect.height;
					axisOffset.z = 0f;
					bool flag4 = textInfo.vertexDataLayout == VertexDataLayout.VBO;
					if (flag4)
					{
						for (int i = 0; i < 4; i++)
						{
							vertexData[index + i].position.y = vertexData[index + i].position.y * -1f + axisOffset.y;
						}
					}
					else
					{
						for (int j = 0; j < 4; j++)
						{
							textInfo.meshInfo[this.m_CurrentMaterialIndex].vertices[index + j].y = textInfo.meshInfo[this.m_CurrentMaterialIndex].vertices[index + j].y * -1f + axisOffset.y;
						}
					}
				}
				int atlasWidth = this.m_Underline.fontAsset.atlasWidth;
				int atlasHeight = this.m_Underline.fontAsset.atlasHeight;
				GlyphRect glyphRect = this.m_Underline.character.glyph.glyphRect;
				Vector2 uvGlyphCenter = new Vector2(((float)glyphRect.x + (float)glyphRect.width / 2f) / (float)atlasWidth, ((float)glyphRect.y + (float)glyphRect.height / 2f) / (float)atlasHeight);
				Vector2 uvTexelSize = new Vector2(1f / (float)atlasWidth, 1f / (float)atlasHeight);
				bool flag5 = textInfo.vertexDataLayout == VertexDataLayout.VBO;
				if (flag5)
				{
					vertexData[index].uv0 = uvGlyphCenter - uvTexelSize;
					vertexData[1 + index].uv0 = uvGlyphCenter + new Vector2(-uvTexelSize.x, uvTexelSize.y);
					vertexData[2 + index].uv0 = uvGlyphCenter + uvTexelSize;
					vertexData[3 + index].uv0 = uvGlyphCenter + new Vector2(uvTexelSize.x, -uvTexelSize.y);
				}
				else
				{
					Vector4[] uvs0 = textInfo.meshInfo[this.m_CurrentMaterialIndex].uvs0;
					uvs0[index] = uvGlyphCenter - uvTexelSize;
					uvs0[1 + index] = uvGlyphCenter + new Vector2(-uvTexelSize.x, uvTexelSize.y);
					uvs0[2 + index] = uvGlyphCenter + uvTexelSize;
					uvs0[3 + index] = uvGlyphCenter + new Vector2(uvTexelSize.x, -uvTexelSize.y);
				}
				Vector2 customUV = new Vector2(0f, 1f);
				bool flag6 = textInfo.vertexDataLayout == VertexDataLayout.VBO;
				if (flag6)
				{
					vertexData[index].uv2 = customUV;
					vertexData[1 + index].uv2 = customUV;
					vertexData[2 + index].uv2 = customUV;
					vertexData[3 + index].uv2 = customUV;
				}
				else
				{
					Vector2[] uvs = textInfo.meshInfo[this.m_CurrentMaterialIndex].uvs2;
					uvs[index] = customUV;
					uvs[1 + index] = customUV;
					uvs[2 + index] = customUV;
					uvs[3 + index] = customUV;
				}
				highlightColor.a = ((this.m_FontColor32.a < highlightColor.a) ? this.m_FontColor32.a : highlightColor.a);
				bool flag7 = textInfo.vertexDataLayout == VertexDataLayout.VBO;
				if (flag7)
				{
					vertexData[index].color = highlightColor;
					vertexData[1 + index].color = highlightColor;
					vertexData[2 + index].color = highlightColor;
					vertexData[3 + index].color = highlightColor;
				}
				else
				{
					Color32[] colors = textInfo.meshInfo[this.m_CurrentMaterialIndex].colors32;
					colors[index] = highlightColor;
					colors[1 + index] = highlightColor;
					colors[2 + index] = highlightColor;
					colors[3 + index] = highlightColor;
				}
				MeshInfo[] meshInfo = textInfo.meshInfo;
				int currentMaterialIndex = this.m_CurrentMaterialIndex;
				meshInfo[currentMaterialIndex].vertexCount = meshInfo[currentMaterialIndex].vertexCount + 4;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00012BD1 File Offset: 0x00010DD1
		private static void ClearMesh(bool updateMesh, TextInfo textInfo)
		{
			textInfo.ClearMeshInfo(updateMesh);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00012BDC File Offset: 0x00010DDC
		public void LayoutPhase(TextInfo textInfo, TextGenerationSettings generationSettings, float maxVisibleDescender)
		{
			Vector4 margins = generationSettings.margins;
			int pageToDisplay = generationSettings.pageToDisplay;
			int lastVertIndex = this.m_MaterialReferences[this.m_Underline.materialIndex].referenceCount * 4;
			textInfo.meshInfo[this.m_CurrentMaterialIndex].Clear(false);
			Vector3 anchorOffset = Vector3.zero;
			Vector3[] corners = this.m_RectTransformCorners;
			TextAlignment textAlignment = generationSettings.textAlignment;
			TextAlignment textAlignment2 = textAlignment;
			if (textAlignment2 <= TextAlignment.BottomGeoAligned)
			{
				if (textAlignment2 <= TextAlignment.MiddleRight)
				{
					if (textAlignment2 <= TextAlignment.TopJustified)
					{
						if (textAlignment2 - TextAlignment.TopLeft > 1 && textAlignment2 != TextAlignment.TopRight && textAlignment2 != TextAlignment.TopJustified)
						{
							goto IL_05DE;
						}
					}
					else if (textAlignment2 <= TextAlignment.TopGeoAligned)
					{
						if (textAlignment2 != TextAlignment.TopFlush && textAlignment2 != TextAlignment.TopGeoAligned)
						{
							goto IL_05DE;
						}
					}
					else
					{
						if (textAlignment2 - TextAlignment.MiddleLeft > 1 && textAlignment2 != TextAlignment.MiddleRight)
						{
							goto IL_05DE;
						}
						goto IL_0349;
					}
					bool flag = generationSettings.overflowMode != TextOverflowMode.Page;
					if (flag)
					{
						anchorOffset = corners[1] + new Vector3(0f + margins.x, 0f - this.m_MaxAscender - margins.y, 0f);
					}
					else
					{
						anchorOffset = corners[1] + new Vector3(0f + margins.x, 0f - textInfo.pageInfo[pageToDisplay].ascender - margins.y, 0f);
					}
					goto IL_05DE;
				}
				if (textAlignment2 <= TextAlignment.BottomCenter)
				{
					if (textAlignment2 <= TextAlignment.MiddleFlush)
					{
						if (textAlignment2 != TextAlignment.MiddleJustified && textAlignment2 != TextAlignment.MiddleFlush)
						{
							goto IL_05DE;
						}
						goto IL_0349;
					}
					else
					{
						if (textAlignment2 == TextAlignment.MiddleGeoAligned)
						{
							goto IL_0349;
						}
						if (textAlignment2 - TextAlignment.BottomLeft > 1)
						{
							goto IL_05DE;
						}
					}
				}
				else if (textAlignment2 <= TextAlignment.BottomJustified)
				{
					if (textAlignment2 != TextAlignment.BottomRight && textAlignment2 != TextAlignment.BottomJustified)
					{
						goto IL_05DE;
					}
				}
				else if (textAlignment2 != TextAlignment.BottomFlush && textAlignment2 != TextAlignment.BottomGeoAligned)
				{
					goto IL_05DE;
				}
				bool flag2 = generationSettings.overflowMode != TextOverflowMode.Page;
				if (flag2)
				{
					anchorOffset = corners[0] + new Vector3(0f + margins.x, 0f - maxVisibleDescender + margins.w, 0f);
				}
				else
				{
					anchorOffset = corners[0] + new Vector3(0f + margins.x, 0f - textInfo.pageInfo[pageToDisplay].descender + margins.w, 0f);
				}
				goto IL_05DE;
				IL_0349:
				bool flag3 = generationSettings.overflowMode != TextOverflowMode.Page;
				if (flag3)
				{
					anchorOffset = (corners[0] + corners[1]) / 2f + new Vector3(0f + margins.x, 0f - (this.m_MaxAscender + margins.y + maxVisibleDescender - margins.w) / 2f, 0f);
				}
				else
				{
					anchorOffset = (corners[0] + corners[1]) / 2f + new Vector3(0f + margins.x, 0f - (textInfo.pageInfo[pageToDisplay].ascender + margins.y + textInfo.pageInfo[pageToDisplay].descender - margins.w) / 2f, 0f);
				}
			}
			else
			{
				if (textAlignment2 <= TextAlignment.MidlineRight)
				{
					if (textAlignment2 <= TextAlignment.BaselineJustified)
					{
						if (textAlignment2 - TextAlignment.BaselineLeft > 1 && textAlignment2 != TextAlignment.BaselineRight && textAlignment2 != TextAlignment.BaselineJustified)
						{
							goto IL_05DE;
						}
					}
					else if (textAlignment2 <= TextAlignment.BaselineGeoAligned)
					{
						if (textAlignment2 != TextAlignment.BaselineFlush && textAlignment2 != TextAlignment.BaselineGeoAligned)
						{
							goto IL_05DE;
						}
					}
					else
					{
						if (textAlignment2 - TextAlignment.MidlineLeft > 1 && textAlignment2 != TextAlignment.MidlineRight)
						{
							goto IL_05DE;
						}
						goto IL_0509;
					}
					anchorOffset = (corners[0] + corners[1]) / 2f + new Vector3(0f + margins.x, 0f, 0f);
					goto IL_05DE;
				}
				if (textAlignment2 <= TextAlignment.CaplineCenter)
				{
					if (textAlignment2 <= TextAlignment.MidlineFlush)
					{
						if (textAlignment2 != TextAlignment.MidlineJustified && textAlignment2 != TextAlignment.MidlineFlush)
						{
							goto IL_05DE;
						}
						goto IL_0509;
					}
					else
					{
						if (textAlignment2 == TextAlignment.MidlineGeoAligned)
						{
							goto IL_0509;
						}
						if (textAlignment2 - TextAlignment.CaplineLeft > 1)
						{
							goto IL_05DE;
						}
					}
				}
				else if (textAlignment2 <= TextAlignment.CaplineJustified)
				{
					if (textAlignment2 != TextAlignment.CaplineRight && textAlignment2 != TextAlignment.CaplineJustified)
					{
						goto IL_05DE;
					}
				}
				else if (textAlignment2 != TextAlignment.CaplineFlush && textAlignment2 != TextAlignment.CaplineGeoAligned)
				{
					goto IL_05DE;
				}
				anchorOffset = (corners[0] + corners[1]) / 2f + new Vector3(0f + margins.x, 0f - (this.m_MaxCapHeight - margins.y - margins.w) / 2f, 0f);
				goto IL_05DE;
				IL_0509:
				anchorOffset = (corners[0] + corners[1]) / 2f + new Vector3(0f + margins.x, 0f - (this.m_MeshExtents.max.y + margins.y + this.m_MeshExtents.min.y - margins.w) / 2f, 0f);
			}
			IL_05DE:
			Vector3 justificationOffset = Vector3.zero;
			Vector3 offset = Vector3.zero;
			int wordCount = 0;
			int lineCount = 0;
			int lastLine = 0;
			bool isFirstSeperator = false;
			bool isStartOfWord = false;
			int wordFirstChar = 0;
			Color32 underlineColor = Color.white;
			Color32 strikethroughColor = Color.white;
			HighlightState highlightState = new HighlightState(new Color32(byte.MaxValue, byte.MaxValue, 0, 64), Offset.zero);
			float xScale = 0f;
			float xScaleMax = 0f;
			float underlineStartScale = 0f;
			float underlineMaxScale = 0f;
			float underlineBaseLine = 32767f;
			int lastPage = 0;
			float strikethroughPointSize = 0f;
			float strikethroughScale = 0f;
			float strikethroughBaseline = 0f;
			bool beginUnderline = false;
			Vector3 underlineStart = Vector3.zero;
			Vector3 underlineEnd = Vector3.zero;
			bool beginStrikethrough = false;
			Vector3 strikethroughStart = Vector3.zero;
			Vector3 strikethroughEnd = Vector3.zero;
			bool beginHighlight = false;
			Vector3 highlightStart = Vector3.zero;
			Vector3 highlightEnd = Vector3.zero;
			TextElementInfo[] textElementInfos = textInfo.textElementInfo;
			int i = 0;
			while (i < this.m_CharacterCount)
			{
				FontAsset currentFontAsset = textElementInfos[i].fontAsset;
				char unicode = (char)textElementInfos[i].character;
				bool isWhiteSpace = char.IsWhiteSpace(unicode);
				int currentLine = textElementInfos[i].lineNumber;
				LineInfo lineInfo = textInfo.lineInfo[currentLine];
				lineCount = currentLine + 1;
				TextAlignment lineAlignment = lineInfo.alignment;
				TextAlignment textAlignment3 = lineAlignment;
				TextAlignment textAlignment4 = textAlignment3;
				if (textAlignment4 <= TextAlignment.BottomGeoAligned)
				{
					if (textAlignment4 <= TextAlignment.MiddleJustified)
					{
						if (textAlignment4 <= TextAlignment.TopFlush)
						{
							switch (textAlignment4)
							{
							case TextAlignment.TopLeft:
								goto IL_0933;
							case TextAlignment.TopCenter:
								goto IL_0985;
							case (TextAlignment)259:
								break;
							case TextAlignment.TopRight:
								goto IL_0A13;
							default:
								if (textAlignment4 == TextAlignment.TopJustified || textAlignment4 == TextAlignment.TopFlush)
								{
									goto IL_0A71;
								}
								break;
							}
						}
						else
						{
							if (textAlignment4 == TextAlignment.TopGeoAligned)
							{
								goto IL_09BE;
							}
							switch (textAlignment4)
							{
							case TextAlignment.MiddleLeft:
								goto IL_0933;
							case TextAlignment.MiddleCenter:
								goto IL_0985;
							case (TextAlignment)515:
								break;
							case TextAlignment.MiddleRight:
								goto IL_0A13;
							default:
								if (textAlignment4 == TextAlignment.MiddleJustified)
								{
									goto IL_0A71;
								}
								break;
							}
						}
					}
					else if (textAlignment4 <= TextAlignment.BottomRight)
					{
						if (textAlignment4 == TextAlignment.MiddleFlush)
						{
							goto IL_0A71;
						}
						if (textAlignment4 == TextAlignment.MiddleGeoAligned)
						{
							goto IL_09BE;
						}
						switch (textAlignment4)
						{
						case TextAlignment.BottomLeft:
							goto IL_0933;
						case TextAlignment.BottomCenter:
							goto IL_0985;
						case TextAlignment.BottomRight:
							goto IL_0A13;
						}
					}
					else
					{
						if (textAlignment4 == TextAlignment.BottomJustified || textAlignment4 == TextAlignment.BottomFlush)
						{
							goto IL_0A71;
						}
						if (textAlignment4 == TextAlignment.BottomGeoAligned)
						{
							goto IL_09BE;
						}
					}
				}
				else if (textAlignment4 <= TextAlignment.MidlineJustified)
				{
					if (textAlignment4 <= TextAlignment.BaselineFlush)
					{
						switch (textAlignment4)
						{
						case TextAlignment.BaselineLeft:
							goto IL_0933;
						case TextAlignment.BaselineCenter:
							goto IL_0985;
						case (TextAlignment)2051:
							break;
						case TextAlignment.BaselineRight:
							goto IL_0A13;
						default:
							if (textAlignment4 == TextAlignment.BaselineJustified || textAlignment4 == TextAlignment.BaselineFlush)
							{
								goto IL_0A71;
							}
							break;
						}
					}
					else
					{
						if (textAlignment4 == TextAlignment.BaselineGeoAligned)
						{
							goto IL_09BE;
						}
						switch (textAlignment4)
						{
						case TextAlignment.MidlineLeft:
							goto IL_0933;
						case TextAlignment.MidlineCenter:
							goto IL_0985;
						case (TextAlignment)4099:
							break;
						case TextAlignment.MidlineRight:
							goto IL_0A13;
						default:
							if (textAlignment4 == TextAlignment.MidlineJustified)
							{
								goto IL_0A71;
							}
							break;
						}
					}
				}
				else if (textAlignment4 <= TextAlignment.CaplineRight)
				{
					if (textAlignment4 == TextAlignment.MidlineFlush)
					{
						goto IL_0A71;
					}
					if (textAlignment4 == TextAlignment.MidlineGeoAligned)
					{
						goto IL_09BE;
					}
					switch (textAlignment4)
					{
					case TextAlignment.CaplineLeft:
						goto IL_0933;
					case TextAlignment.CaplineCenter:
						goto IL_0985;
					case TextAlignment.CaplineRight:
						goto IL_0A13;
					}
				}
				else
				{
					if (textAlignment4 == TextAlignment.CaplineJustified || textAlignment4 == TextAlignment.CaplineFlush)
					{
						goto IL_0A71;
					}
					if (textAlignment4 == TextAlignment.CaplineGeoAligned)
					{
						goto IL_09BE;
					}
				}
				IL_0D3D:
				offset = anchorOffset + justificationOffset;
				bool isCharacterVisible = textElementInfos[i].isVisible;
				bool flag4 = isCharacterVisible;
				if (flag4)
				{
					TextElementType elementType = textElementInfos[i].elementType;
					TextElementType textElementType = elementType;
					TextElementType textElementType2 = textElementType;
					if (textElementType2 != TextElementType.Character)
					{
						if (textElementType2 != TextElementType.Sprite)
						{
						}
					}
					else
					{
						Extents lineExtents = lineInfo.lineExtents;
						float uvOffset = generationSettings.uvLineOffset * (float)currentLine % 1f;
						switch (generationSettings.horizontalMapping)
						{
						case TextureMapping.Character:
							textElementInfos[i].vertexBottomLeft.uv2.x = 0f;
							textElementInfos[i].vertexTopLeft.uv2.x = 0f;
							textElementInfos[i].vertexTopRight.uv2.x = 1f;
							textElementInfos[i].vertexBottomRight.uv2.x = 1f;
							break;
						case TextureMapping.Line:
						{
							bool flag5 = generationSettings.textAlignment != TextAlignment.MiddleJustified;
							if (flag5)
							{
								textElementInfos[i].vertexBottomLeft.uv2.x = (textElementInfos[i].vertexBottomLeft.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + uvOffset;
								textElementInfos[i].vertexTopLeft.uv2.x = (textElementInfos[i].vertexTopLeft.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + uvOffset;
								textElementInfos[i].vertexTopRight.uv2.x = (textElementInfos[i].vertexTopRight.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + uvOffset;
								textElementInfos[i].vertexBottomRight.uv2.x = (textElementInfos[i].vertexBottomRight.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + uvOffset;
							}
							else
							{
								textElementInfos[i].vertexBottomLeft.uv2.x = (textElementInfos[i].vertexBottomLeft.position.x + justificationOffset.x - this.m_MeshExtents.min.x) / (this.m_MeshExtents.max.x - this.m_MeshExtents.min.x) + uvOffset;
								textElementInfos[i].vertexTopLeft.uv2.x = (textElementInfos[i].vertexTopLeft.position.x + justificationOffset.x - this.m_MeshExtents.min.x) / (this.m_MeshExtents.max.x - this.m_MeshExtents.min.x) + uvOffset;
								textElementInfos[i].vertexTopRight.uv2.x = (textElementInfos[i].vertexTopRight.position.x + justificationOffset.x - this.m_MeshExtents.min.x) / (this.m_MeshExtents.max.x - this.m_MeshExtents.min.x) + uvOffset;
								textElementInfos[i].vertexBottomRight.uv2.x = (textElementInfos[i].vertexBottomRight.position.x + justificationOffset.x - this.m_MeshExtents.min.x) / (this.m_MeshExtents.max.x - this.m_MeshExtents.min.x) + uvOffset;
							}
							break;
						}
						case TextureMapping.Paragraph:
							textElementInfos[i].vertexBottomLeft.uv2.x = (textElementInfos[i].vertexBottomLeft.position.x + justificationOffset.x - this.m_MeshExtents.min.x) / (this.m_MeshExtents.max.x - this.m_MeshExtents.min.x) + uvOffset;
							textElementInfos[i].vertexTopLeft.uv2.x = (textElementInfos[i].vertexTopLeft.position.x + justificationOffset.x - this.m_MeshExtents.min.x) / (this.m_MeshExtents.max.x - this.m_MeshExtents.min.x) + uvOffset;
							textElementInfos[i].vertexTopRight.uv2.x = (textElementInfos[i].vertexTopRight.position.x + justificationOffset.x - this.m_MeshExtents.min.x) / (this.m_MeshExtents.max.x - this.m_MeshExtents.min.x) + uvOffset;
							textElementInfos[i].vertexBottomRight.uv2.x = (textElementInfos[i].vertexBottomRight.position.x + justificationOffset.x - this.m_MeshExtents.min.x) / (this.m_MeshExtents.max.x - this.m_MeshExtents.min.x) + uvOffset;
							break;
						case TextureMapping.MatchAspect:
						{
							switch (generationSettings.verticalMapping)
							{
							case TextureMapping.Character:
								textElementInfos[i].vertexBottomLeft.uv2.y = 0f;
								textElementInfos[i].vertexTopLeft.uv2.y = 1f;
								textElementInfos[i].vertexTopRight.uv2.y = 0f;
								textElementInfos[i].vertexBottomRight.uv2.y = 1f;
								break;
							case TextureMapping.Line:
								textElementInfos[i].vertexBottomLeft.uv2.y = (textElementInfos[i].vertexBottomLeft.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + uvOffset;
								textElementInfos[i].vertexTopLeft.uv2.y = (textElementInfos[i].vertexTopLeft.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + uvOffset;
								textElementInfos[i].vertexTopRight.uv2.y = textElementInfos[i].vertexBottomLeft.uv2.y;
								textElementInfos[i].vertexBottomRight.uv2.y = textElementInfos[i].vertexTopLeft.uv2.y;
								break;
							case TextureMapping.Paragraph:
								textElementInfos[i].vertexBottomLeft.uv2.y = (textElementInfos[i].vertexBottomLeft.position.y - this.m_MeshExtents.min.y) / (this.m_MeshExtents.max.y - this.m_MeshExtents.min.y) + uvOffset;
								textElementInfos[i].vertexTopLeft.uv2.y = (textElementInfos[i].vertexTopLeft.position.y - this.m_MeshExtents.min.y) / (this.m_MeshExtents.max.y - this.m_MeshExtents.min.y) + uvOffset;
								textElementInfos[i].vertexTopRight.uv2.y = textElementInfos[i].vertexBottomLeft.uv2.y;
								textElementInfos[i].vertexBottomRight.uv2.y = textElementInfos[i].vertexTopLeft.uv2.y;
								break;
							case TextureMapping.MatchAspect:
								Debug.Log("ERROR: Cannot Match both Vertical & Horizontal.");
								break;
							}
							float xDelta = (1f - (textElementInfos[i].vertexBottomLeft.uv2.y + textElementInfos[i].vertexTopLeft.uv2.y) * textElementInfos[i].aspectRatio) / 2f;
							textElementInfos[i].vertexBottomLeft.uv2.x = textElementInfos[i].vertexBottomLeft.uv2.y * textElementInfos[i].aspectRatio + xDelta + uvOffset;
							textElementInfos[i].vertexTopLeft.uv2.x = textElementInfos[i].vertexBottomLeft.uv2.x;
							textElementInfos[i].vertexTopRight.uv2.x = textElementInfos[i].vertexTopLeft.uv2.y * textElementInfos[i].aspectRatio + xDelta + uvOffset;
							textElementInfos[i].vertexBottomRight.uv2.x = textElementInfos[i].vertexTopRight.uv2.x;
							break;
						}
						}
						switch (generationSettings.verticalMapping)
						{
						case TextureMapping.Character:
							textElementInfos[i].vertexBottomLeft.uv2.y = 0f;
							textElementInfos[i].vertexTopLeft.uv2.y = 1f;
							textElementInfos[i].vertexTopRight.uv2.y = 1f;
							textElementInfos[i].vertexBottomRight.uv2.y = 0f;
							break;
						case TextureMapping.Line:
							textElementInfos[i].vertexBottomLeft.uv2.y = (textElementInfos[i].vertexBottomLeft.position.y - lineInfo.descender) / (lineInfo.ascender - lineInfo.descender);
							textElementInfos[i].vertexTopLeft.uv2.y = (textElementInfos[i].vertexTopLeft.position.y - lineInfo.descender) / (lineInfo.ascender - lineInfo.descender);
							textElementInfos[i].vertexTopRight.uv2.y = textElementInfos[i].vertexTopLeft.uv2.y;
							textElementInfos[i].vertexBottomRight.uv2.y = textElementInfos[i].vertexBottomLeft.uv2.y;
							break;
						case TextureMapping.Paragraph:
							textElementInfos[i].vertexBottomLeft.uv2.y = (textElementInfos[i].vertexBottomLeft.position.y - this.m_MeshExtents.min.y) / (this.m_MeshExtents.max.y - this.m_MeshExtents.min.y);
							textElementInfos[i].vertexTopLeft.uv2.y = (textElementInfos[i].vertexTopLeft.position.y - this.m_MeshExtents.min.y) / (this.m_MeshExtents.max.y - this.m_MeshExtents.min.y);
							textElementInfos[i].vertexTopRight.uv2.y = textElementInfos[i].vertexTopLeft.uv2.y;
							textElementInfos[i].vertexBottomRight.uv2.y = textElementInfos[i].vertexBottomLeft.uv2.y;
							break;
						case TextureMapping.MatchAspect:
						{
							float yDelta = (1f - (textElementInfos[i].vertexBottomLeft.uv2.x + textElementInfos[i].vertexTopRight.uv2.x) / textElementInfos[i].aspectRatio) / 2f;
							textElementInfos[i].vertexBottomLeft.uv2.y = yDelta + textElementInfos[i].vertexBottomLeft.uv2.x / textElementInfos[i].aspectRatio;
							textElementInfos[i].vertexTopLeft.uv2.y = yDelta + textElementInfos[i].vertexTopRight.uv2.x / textElementInfos[i].aspectRatio;
							textElementInfos[i].vertexBottomRight.uv2.y = textElementInfos[i].vertexBottomLeft.uv2.y;
							textElementInfos[i].vertexTopRight.uv2.y = textElementInfos[i].vertexTopLeft.uv2.y;
							break;
						}
						}
						xScale = textElementInfos[i].scale * (1f - this.m_CharWidthAdjDelta) * 1f;
						bool flag6 = !textElementInfos[i].isUsingAlternateTypeface && (textElementInfos[i].style & FontStyles.Bold) == FontStyles.Bold;
						if (flag6)
						{
							xScale *= -1f;
						}
						textElementInfos[i].vertexBottomLeft.uv.w = xScale;
						textElementInfos[i].vertexTopLeft.uv.w = xScale;
						textElementInfos[i].vertexTopRight.uv.w = xScale;
						textElementInfos[i].vertexBottomRight.uv.w = xScale;
						textElementInfos[i].vertexBottomLeft.uv2.x = 1f;
						textElementInfos[i].vertexBottomLeft.uv2.y = xScale;
						textElementInfos[i].vertexTopLeft.uv2.x = 1f;
						textElementInfos[i].vertexTopLeft.uv2.y = xScale;
						textElementInfos[i].vertexTopRight.uv2.x = 1f;
						textElementInfos[i].vertexTopRight.uv2.y = xScale;
						textElementInfos[i].vertexBottomRight.uv2.x = 1f;
						textElementInfos[i].vertexBottomRight.uv2.y = xScale;
					}
					bool flag7 = i < generationSettings.maxVisibleCharacters && wordCount < generationSettings.maxVisibleWords && currentLine < generationSettings.maxVisibleLines && generationSettings.overflowMode != TextOverflowMode.Page;
					if (flag7)
					{
						TextElementInfo[] array = textElementInfos;
						int num = i;
						array[num].vertexBottomLeft.position = array[num].vertexBottomLeft.position + offset;
						TextElementInfo[] array2 = textElementInfos;
						int num2 = i;
						array2[num2].vertexTopLeft.position = array2[num2].vertexTopLeft.position + offset;
						TextElementInfo[] array3 = textElementInfos;
						int num3 = i;
						array3[num3].vertexTopRight.position = array3[num3].vertexTopRight.position + offset;
						TextElementInfo[] array4 = textElementInfos;
						int num4 = i;
						array4[num4].vertexBottomRight.position = array4[num4].vertexBottomRight.position + offset;
					}
					else
					{
						bool flag8 = i < generationSettings.maxVisibleCharacters && wordCount < generationSettings.maxVisibleWords && currentLine < generationSettings.maxVisibleLines && generationSettings.overflowMode == TextOverflowMode.Page && textElementInfos[i].pageNumber == pageToDisplay;
						if (flag8)
						{
							TextElementInfo[] array5 = textElementInfos;
							int num5 = i;
							array5[num5].vertexBottomLeft.position = array5[num5].vertexBottomLeft.position + offset;
							TextElementInfo[] array6 = textElementInfos;
							int num6 = i;
							array6[num6].vertexTopLeft.position = array6[num6].vertexTopLeft.position + offset;
							TextElementInfo[] array7 = textElementInfos;
							int num7 = i;
							array7[num7].vertexTopRight.position = array7[num7].vertexTopRight.position + offset;
							TextElementInfo[] array8 = textElementInfos;
							int num8 = i;
							array8[num8].vertexBottomRight.position = array8[num8].vertexBottomRight.position + offset;
						}
						else
						{
							textElementInfos[i].vertexBottomLeft.position = Vector3.zero;
							textElementInfos[i].vertexTopLeft.position = Vector3.zero;
							textElementInfos[i].vertexTopRight.position = Vector3.zero;
							textElementInfos[i].vertexBottomRight.position = Vector3.zero;
							textElementInfos[i].isVisible = false;
						}
					}
					bool convertToLinearSpace = generationSettings.shouldConvertToLinearSpace;
					bool flag9 = elementType == TextElementType.Character;
					if (flag9)
					{
						TextGeneratorUtilities.FillCharacterVertexBuffers(i, convertToLinearSpace, generationSettings, textInfo);
					}
					else
					{
						bool flag10 = elementType == TextElementType.Sprite;
						if (flag10)
						{
							TextGeneratorUtilities.FillSpriteVertexBuffers(i, convertToLinearSpace, generationSettings, textInfo);
						}
					}
				}
				TextElementInfo[] textElementInfo = textInfo.textElementInfo;
				int num9 = i;
				textElementInfo[num9].bottomLeft = textElementInfo[num9].bottomLeft + offset;
				TextElementInfo[] textElementInfo2 = textInfo.textElementInfo;
				int num10 = i;
				textElementInfo2[num10].topLeft = textElementInfo2[num10].topLeft + offset;
				TextElementInfo[] textElementInfo3 = textInfo.textElementInfo;
				int num11 = i;
				textElementInfo3[num11].topRight = textElementInfo3[num11].topRight + offset;
				TextElementInfo[] textElementInfo4 = textInfo.textElementInfo;
				int num12 = i;
				textElementInfo4[num12].bottomRight = textElementInfo4[num12].bottomRight + offset;
				TextElementInfo[] textElementInfo5 = textInfo.textElementInfo;
				int num13 = i;
				textElementInfo5[num13].origin = textElementInfo5[num13].origin + offset.x;
				TextElementInfo[] textElementInfo6 = textInfo.textElementInfo;
				int num14 = i;
				textElementInfo6[num14].xAdvance = textElementInfo6[num14].xAdvance + offset.x;
				TextElementInfo[] textElementInfo7 = textInfo.textElementInfo;
				int num15 = i;
				textElementInfo7[num15].ascender = textElementInfo7[num15].ascender + offset.y;
				TextElementInfo[] textElementInfo8 = textInfo.textElementInfo;
				int num16 = i;
				textElementInfo8[num16].descender = textElementInfo8[num16].descender + offset.y;
				TextElementInfo[] textElementInfo9 = textInfo.textElementInfo;
				int num17 = i;
				textElementInfo9[num17].baseLine = textElementInfo9[num17].baseLine + offset.y;
				bool flag11 = isCharacterVisible;
				if (flag11)
				{
				}
				bool flag12 = currentLine != lastLine || i == this.m_CharacterCount - 1;
				if (flag12)
				{
					bool flag13 = currentLine != lastLine;
					if (flag13)
					{
						int lastCharacterIndex = ((generationSettings.textWrappingMode == TextWrappingMode.PreserveWhitespace || generationSettings.textWrappingMode == TextWrappingMode.PreserveWhitespaceNoWrap) ? textInfo.lineInfo[lastLine].lastCharacterIndex : textInfo.lineInfo[lastLine].lastVisibleCharacterIndex);
						LineInfo[] lineInfo2 = textInfo.lineInfo;
						int num18 = lastLine;
						lineInfo2[num18].baseline = lineInfo2[num18].baseline + offset.y;
						LineInfo[] lineInfo3 = textInfo.lineInfo;
						int num19 = lastLine;
						lineInfo3[num19].ascender = lineInfo3[num19].ascender + offset.y;
						LineInfo[] lineInfo4 = textInfo.lineInfo;
						int num20 = lastLine;
						lineInfo4[num20].descender = lineInfo4[num20].descender + offset.y;
						LineInfo[] lineInfo5 = textInfo.lineInfo;
						int num21 = lastLine;
						lineInfo5[num21].maxAdvance = lineInfo5[num21].maxAdvance + offset.x;
						textInfo.lineInfo[lastLine].lineExtents.min = new Vector2(textInfo.textElementInfo[textInfo.lineInfo[lastLine].firstCharacterIndex].bottomLeft.x, textInfo.lineInfo[lastLine].descender);
						textInfo.lineInfo[lastLine].lineExtents.max = new Vector2(textInfo.textElementInfo[lastCharacterIndex].topRight.x, textInfo.lineInfo[lastLine].ascender);
					}
					bool flag14 = i == this.m_CharacterCount - 1;
					if (flag14)
					{
						int lastCharacterIndex2 = ((generationSettings.textWrappingMode == TextWrappingMode.PreserveWhitespace || generationSettings.textWrappingMode == TextWrappingMode.PreserveWhitespaceNoWrap) ? textInfo.lineInfo[currentLine].lastCharacterIndex : textInfo.lineInfo[currentLine].lastVisibleCharacterIndex);
						LineInfo[] lineInfo6 = textInfo.lineInfo;
						int num22 = currentLine;
						lineInfo6[num22].baseline = lineInfo6[num22].baseline + offset.y;
						LineInfo[] lineInfo7 = textInfo.lineInfo;
						int num23 = currentLine;
						lineInfo7[num23].ascender = lineInfo7[num23].ascender + offset.y;
						LineInfo[] lineInfo8 = textInfo.lineInfo;
						int num24 = currentLine;
						lineInfo8[num24].descender = lineInfo8[num24].descender + offset.y;
						LineInfo[] lineInfo9 = textInfo.lineInfo;
						int num25 = currentLine;
						lineInfo9[num25].maxAdvance = lineInfo9[num25].maxAdvance + offset.x;
						textInfo.lineInfo[currentLine].lineExtents.min = new Vector2(textInfo.textElementInfo[textInfo.lineInfo[currentLine].firstCharacterIndex].bottomLeft.x, textInfo.lineInfo[currentLine].descender);
						textInfo.lineInfo[currentLine].lineExtents.max = new Vector2(textInfo.textElementInfo[lastCharacterIndex2].topRight.x, textInfo.lineInfo[currentLine].ascender);
					}
				}
				bool flag15 = char.IsLetterOrDigit(unicode) || unicode == '-' || unicode == '\u00ad' || unicode == '‐' || unicode == '‑';
				if (flag15)
				{
					bool flag16 = !isStartOfWord;
					if (flag16)
					{
						isStartOfWord = true;
						wordFirstChar = i;
					}
					bool flag17 = isStartOfWord && i == this.m_CharacterCount - 1;
					if (flag17)
					{
						int size = textInfo.wordInfo.Length;
						int index = textInfo.wordCount;
						bool flag18 = textInfo.wordCount + 1 > size;
						if (flag18)
						{
							TextInfo.Resize<WordInfo>(ref textInfo.wordInfo, size + 1);
						}
						int wordLastChar = i;
						textInfo.wordInfo[index].firstCharacterIndex = wordFirstChar;
						textInfo.wordInfo[index].lastCharacterIndex = wordLastChar;
						textInfo.wordInfo[index].characterCount = wordLastChar - wordFirstChar + 1;
						wordCount++;
						textInfo.wordCount++;
						LineInfo[] lineInfo10 = textInfo.lineInfo;
						int num26 = currentLine;
						lineInfo10[num26].wordCount = lineInfo10[num26].wordCount + 1;
					}
				}
				else
				{
					bool flag19 = isStartOfWord || (i == 0 && (!char.IsPunctuation(unicode) || isWhiteSpace || unicode == '\u200b' || i == this.m_CharacterCount - 1));
					if (flag19)
					{
						bool flag20 = i > 0 && i < textElementInfos.Length - 1 && i < this.m_CharacterCount && (unicode == '\'' || unicode == '’') && char.IsLetterOrDigit((char)textElementInfos[i - 1].character) && char.IsLetterOrDigit((char)textElementInfos[i + 1].character);
						if (!flag20)
						{
							int wordLastChar = ((i == this.m_CharacterCount - 1 && char.IsLetterOrDigit(unicode)) ? i : (i - 1));
							isStartOfWord = false;
							int size2 = textInfo.wordInfo.Length;
							int index2 = textInfo.wordCount;
							bool flag21 = textInfo.wordCount + 1 > size2;
							if (flag21)
							{
								TextInfo.Resize<WordInfo>(ref textInfo.wordInfo, size2 + 1);
							}
							textInfo.wordInfo[index2].firstCharacterIndex = wordFirstChar;
							textInfo.wordInfo[index2].lastCharacterIndex = wordLastChar;
							textInfo.wordInfo[index2].characterCount = wordLastChar - wordFirstChar + 1;
							wordCount++;
							textInfo.wordCount++;
							LineInfo[] lineInfo11 = textInfo.lineInfo;
							int num27 = currentLine;
							lineInfo11[num27].wordCount = lineInfo11[num27].wordCount + 1;
						}
					}
				}
				bool isUnderline = (textInfo.textElementInfo[i].style & FontStyles.Underline) == FontStyles.Underline;
				bool flag22 = isUnderline;
				if (flag22)
				{
					bool isUnderlineVisible = true;
					int currentPage = textInfo.textElementInfo[i].pageNumber;
					textInfo.textElementInfo[i].underlineVertexIndex = lastVertIndex;
					bool flag23 = i > generationSettings.maxVisibleCharacters || currentLine > generationSettings.maxVisibleLines || (generationSettings.overflowMode == TextOverflowMode.Page && currentPage + 1 != generationSettings.pageToDisplay);
					if (flag23)
					{
						isUnderlineVisible = false;
					}
					bool flag24 = !isWhiteSpace && unicode != '\u200b';
					if (flag24)
					{
						underlineMaxScale = Mathf.Max(underlineMaxScale, textInfo.textElementInfo[i].scale);
						xScaleMax = Mathf.Max(xScaleMax, Mathf.Abs(xScale));
						underlineBaseLine = Mathf.Min((currentPage == lastPage) ? underlineBaseLine : 32767f, textInfo.textElementInfo[i].baseLine + currentFontAsset.faceInfo.underlineOffset * underlineMaxScale);
						lastPage = currentPage;
					}
					bool flag25 = !beginUnderline && isUnderlineVisible && i <= lineInfo.lastVisibleCharacterIndex && unicode != '\n' && unicode != '\v' && unicode != '\r';
					if (flag25)
					{
						bool flag26 = i == lineInfo.lastVisibleCharacterIndex && char.IsSeparator(unicode);
						if (!flag26)
						{
							beginUnderline = true;
							underlineStartScale = textInfo.textElementInfo[i].scale;
							bool flag27 = underlineMaxScale == 0f;
							if (flag27)
							{
								underlineMaxScale = underlineStartScale;
								xScaleMax = xScale;
							}
							underlineStart = new Vector3(textInfo.textElementInfo[i].bottomLeft.x, underlineBaseLine, 0f);
							underlineColor = textInfo.textElementInfo[i].underlineColor;
						}
					}
					bool flag28 = beginUnderline && this.m_CharacterCount == 1;
					if (flag28)
					{
						beginUnderline = false;
						underlineEnd = new Vector3(textInfo.textElementInfo[i].topRight.x, underlineBaseLine, 0f);
						float underlineEndScale = textInfo.textElementInfo[i].scale;
						this.DrawUnderlineMesh(underlineStart, underlineEnd, underlineStartScale, underlineEndScale, underlineMaxScale, xScaleMax, underlineColor, generationSettings, textInfo);
						underlineMaxScale = 0f;
						xScaleMax = 0f;
						underlineBaseLine = 32767f;
					}
					else
					{
						bool flag29 = beginUnderline && (i == lineInfo.lastCharacterIndex || i >= lineInfo.lastVisibleCharacterIndex);
						if (flag29)
						{
							bool flag30 = isWhiteSpace || unicode == '\u200b';
							float underlineEndScale;
							if (flag30)
							{
								int lastVisibleCharacterIndex = lineInfo.lastVisibleCharacterIndex;
								underlineEnd = new Vector3(textInfo.textElementInfo[lastVisibleCharacterIndex].topRight.x, underlineBaseLine, 0f);
								underlineEndScale = textInfo.textElementInfo[lastVisibleCharacterIndex].scale;
							}
							else
							{
								underlineEnd = new Vector3(textInfo.textElementInfo[i].topRight.x, underlineBaseLine, 0f);
								underlineEndScale = textInfo.textElementInfo[i].scale;
							}
							beginUnderline = false;
							this.DrawUnderlineMesh(underlineStart, underlineEnd, underlineStartScale, underlineEndScale, underlineMaxScale, xScaleMax, underlineColor, generationSettings, textInfo);
							underlineMaxScale = 0f;
							xScaleMax = 0f;
							underlineBaseLine = 32767f;
						}
						else
						{
							bool flag31 = beginUnderline && !isUnderlineVisible;
							if (flag31)
							{
								beginUnderline = false;
								underlineEnd = new Vector3(textInfo.textElementInfo[i - 1].topRight.x, underlineBaseLine, 0f);
								float underlineEndScale = textInfo.textElementInfo[i - 1].scale;
								this.DrawUnderlineMesh(underlineStart, underlineEnd, underlineStartScale, underlineEndScale, underlineMaxScale, xScaleMax, underlineColor, generationSettings, textInfo);
								underlineMaxScale = 0f;
								xScaleMax = 0f;
								underlineBaseLine = 32767f;
							}
							else
							{
								bool flag32 = beginUnderline && i < this.m_CharacterCount - 1 && !ColorUtilities.CompareColors(underlineColor, textInfo.textElementInfo[i + 1].underlineColor);
								if (flag32)
								{
									beginUnderline = false;
									underlineEnd = new Vector3(textInfo.textElementInfo[i].topRight.x, underlineBaseLine, 0f);
									float underlineEndScale = textInfo.textElementInfo[i].scale;
									this.DrawUnderlineMesh(underlineStart, underlineEnd, underlineStartScale, underlineEndScale, underlineMaxScale, xScaleMax, underlineColor, generationSettings, textInfo);
									underlineMaxScale = 0f;
									xScaleMax = 0f;
									underlineBaseLine = 32767f;
								}
							}
						}
					}
				}
				else
				{
					bool flag33 = beginUnderline;
					if (flag33)
					{
						beginUnderline = false;
						underlineEnd = new Vector3(textInfo.textElementInfo[i - 1].topRight.x, underlineBaseLine, 0f);
						float underlineEndScale = textInfo.textElementInfo[i - 1].scale;
						this.DrawUnderlineMesh(underlineStart, underlineEnd, underlineStartScale, underlineEndScale, underlineMaxScale, xScaleMax, underlineColor, generationSettings, textInfo);
						underlineMaxScale = 0f;
						xScaleMax = 0f;
						underlineBaseLine = 32767f;
					}
				}
				bool isStrikethrough = (textInfo.textElementInfo[i].style & FontStyles.Strikethrough) == FontStyles.Strikethrough;
				float strikethroughOffset = currentFontAsset.faceInfo.strikethroughOffset;
				bool flag34 = isStrikethrough;
				if (flag34)
				{
					bool isStrikeThroughVisible = true;
					textInfo.textElementInfo[i].strikethroughVertexIndex = this.m_MaterialReferences[this.m_Underline.materialIndex].referenceCount * 4;
					bool flag35 = i > generationSettings.maxVisibleCharacters || currentLine > generationSettings.maxVisibleLines || (generationSettings.overflowMode == TextOverflowMode.Page && textInfo.textElementInfo[i].pageNumber + 1 != generationSettings.pageToDisplay);
					if (flag35)
					{
						isStrikeThroughVisible = false;
					}
					bool flag36 = !beginStrikethrough && isStrikeThroughVisible && i <= lineInfo.lastVisibleCharacterIndex && unicode != '\n' && unicode != '\v' && unicode != '\r';
					if (flag36)
					{
						bool flag37 = i == lineInfo.lastVisibleCharacterIndex && char.IsSeparator(unicode);
						if (!flag37)
						{
							beginStrikethrough = true;
							strikethroughPointSize = textInfo.textElementInfo[i].pointSize;
							strikethroughScale = textInfo.textElementInfo[i].scale;
							strikethroughStart = new Vector3(textInfo.textElementInfo[i].bottomLeft.x, textInfo.textElementInfo[i].baseLine + strikethroughOffset * strikethroughScale, 0f);
							strikethroughColor = textInfo.textElementInfo[i].strikethroughColor;
							strikethroughBaseline = textInfo.textElementInfo[i].baseLine;
						}
					}
					bool flag38 = beginStrikethrough && this.m_CharacterCount == 1;
					if (flag38)
					{
						beginStrikethrough = false;
						strikethroughEnd = new Vector3(textInfo.textElementInfo[i].topRight.x, textInfo.textElementInfo[i].baseLine + strikethroughOffset * strikethroughScale, 0f);
						this.DrawUnderlineMesh(strikethroughStart, strikethroughEnd, strikethroughScale, strikethroughScale, strikethroughScale, xScale, strikethroughColor, generationSettings, textInfo);
					}
					else
					{
						bool flag39 = beginStrikethrough && i == lineInfo.lastCharacterIndex;
						if (flag39)
						{
							bool flag40 = isWhiteSpace || unicode == '\u200b';
							if (flag40)
							{
								int lastVisibleCharacterIndex2 = lineInfo.lastVisibleCharacterIndex;
								strikethroughEnd = new Vector3(textInfo.textElementInfo[lastVisibleCharacterIndex2].topRight.x, textInfo.textElementInfo[lastVisibleCharacterIndex2].baseLine + strikethroughOffset * strikethroughScale, 0f);
							}
							else
							{
								strikethroughEnd = new Vector3(textInfo.textElementInfo[i].topRight.x, textInfo.textElementInfo[i].baseLine + strikethroughOffset * strikethroughScale, 0f);
							}
							beginStrikethrough = false;
							this.DrawUnderlineMesh(strikethroughStart, strikethroughEnd, strikethroughScale, strikethroughScale, strikethroughScale, xScale, strikethroughColor, generationSettings, textInfo);
						}
						else
						{
							bool flag41 = beginStrikethrough && i < this.m_CharacterCount && (textInfo.textElementInfo[i + 1].pointSize != strikethroughPointSize || !TextGeneratorUtilities.Approximately(textInfo.textElementInfo[i + 1].baseLine + offset.y, strikethroughBaseline));
							if (flag41)
							{
								beginStrikethrough = false;
								int lastVisibleCharacterIndex3 = lineInfo.lastVisibleCharacterIndex;
								bool flag42 = i > lastVisibleCharacterIndex3;
								if (flag42)
								{
									strikethroughEnd = new Vector3(textInfo.textElementInfo[lastVisibleCharacterIndex3].topRight.x, textInfo.textElementInfo[lastVisibleCharacterIndex3].baseLine + strikethroughOffset * strikethroughScale, 0f);
								}
								else
								{
									strikethroughEnd = new Vector3(textInfo.textElementInfo[i].topRight.x, textInfo.textElementInfo[i].baseLine + strikethroughOffset * strikethroughScale, 0f);
								}
								this.DrawUnderlineMesh(strikethroughStart, strikethroughEnd, strikethroughScale, strikethroughScale, strikethroughScale, xScale, strikethroughColor, generationSettings, textInfo);
							}
							else
							{
								bool flag43 = beginStrikethrough && i < this.m_CharacterCount && currentFontAsset.GetHashCode() != textElementInfos[i + 1].fontAsset.GetHashCode();
								if (flag43)
								{
									beginStrikethrough = false;
									strikethroughEnd = new Vector3(textInfo.textElementInfo[i].topRight.x, textInfo.textElementInfo[i].baseLine + strikethroughOffset * strikethroughScale, 0f);
									this.DrawUnderlineMesh(strikethroughStart, strikethroughEnd, strikethroughScale, strikethroughScale, strikethroughScale, xScale, strikethroughColor, generationSettings, textInfo);
								}
								else
								{
									bool flag44 = beginStrikethrough && !isStrikeThroughVisible;
									if (flag44)
									{
										beginStrikethrough = false;
										strikethroughEnd = new Vector3(textInfo.textElementInfo[i - 1].topRight.x, textInfo.textElementInfo[i - 1].baseLine + strikethroughOffset * strikethroughScale, 0f);
										this.DrawUnderlineMesh(strikethroughStart, strikethroughEnd, strikethroughScale, strikethroughScale, strikethroughScale, xScale, strikethroughColor, generationSettings, textInfo);
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag45 = beginStrikethrough;
					if (flag45)
					{
						beginStrikethrough = false;
						strikethroughEnd = new Vector3(textInfo.textElementInfo[i - 1].topRight.x, textInfo.textElementInfo[i - 1].baseLine + strikethroughOffset * strikethroughScale, 0f);
						this.DrawUnderlineMesh(strikethroughStart, strikethroughEnd, strikethroughScale, strikethroughScale, strikethroughScale, xScale, strikethroughColor, generationSettings, textInfo);
					}
				}
				bool isHighlight = (textInfo.textElementInfo[i].style & FontStyles.Highlight) == FontStyles.Highlight;
				bool flag46 = isHighlight;
				if (flag46)
				{
					bool isHighlightVisible = true;
					int currentPage2 = textInfo.textElementInfo[i].pageNumber;
					bool flag47 = i > generationSettings.maxVisibleCharacters || currentLine > generationSettings.maxVisibleLines || (generationSettings.overflowMode == TextOverflowMode.Page && currentPage2 + 1 != generationSettings.pageToDisplay);
					if (flag47)
					{
						isHighlightVisible = false;
					}
					bool flag48 = !beginHighlight && isHighlightVisible && i <= lineInfo.lastVisibleCharacterIndex && unicode != '\n' && unicode != '\v' && unicode != '\r';
					if (flag48)
					{
						bool flag49 = i == lineInfo.lastVisibleCharacterIndex && char.IsSeparator(unicode);
						if (!flag49)
						{
							beginHighlight = true;
							highlightStart = TextGeneratorUtilities.largePositiveVector2;
							highlightEnd = TextGeneratorUtilities.largeNegativeVector2;
							highlightState = textInfo.textElementInfo[i].highlightState;
						}
					}
					bool flag50 = beginHighlight;
					if (flag50)
					{
						TextElementInfo currentCharacter = textInfo.textElementInfo[i];
						HighlightState currentState = currentCharacter.highlightState;
						bool isColorTransition = false;
						bool flag51 = highlightState != currentState;
						if (flag51)
						{
							bool flag52 = isWhiteSpace;
							if (flag52)
							{
								highlightEnd.x = (highlightEnd.x - highlightState.padding.right + currentCharacter.origin) / 2f;
							}
							else
							{
								highlightEnd.x = (highlightEnd.x - highlightState.padding.right + currentCharacter.bottomLeft.x) / 2f;
							}
							highlightStart.y = Mathf.Min(highlightStart.y, currentCharacter.descender);
							highlightEnd.y = Mathf.Max(highlightEnd.y, currentCharacter.ascender);
							this.DrawTextHighlight(highlightStart, highlightEnd, highlightState.color, generationSettings, textInfo);
							beginHighlight = true;
							highlightStart = new Vector2(highlightEnd.x, currentCharacter.descender - currentState.padding.bottom);
							bool flag53 = isWhiteSpace;
							if (flag53)
							{
								highlightEnd = new Vector2(currentCharacter.xAdvance + currentState.padding.right, currentCharacter.ascender + currentState.padding.top);
							}
							else
							{
								highlightEnd = new Vector2(currentCharacter.topRight.x + currentState.padding.right, currentCharacter.ascender + currentState.padding.top);
							}
							highlightState = currentState;
							isColorTransition = true;
						}
						bool flag54 = !isColorTransition;
						if (flag54)
						{
							bool flag55 = isWhiteSpace;
							if (flag55)
							{
								highlightStart.x = Mathf.Min(highlightStart.x, currentCharacter.origin - highlightState.padding.left);
								highlightEnd.x = Mathf.Max(highlightEnd.x, currentCharacter.xAdvance + highlightState.padding.right);
							}
							else
							{
								highlightStart.x = Mathf.Min(highlightStart.x, currentCharacter.bottomLeft.x - highlightState.padding.left);
								highlightEnd.x = Mathf.Max(highlightEnd.x, currentCharacter.topRight.x + highlightState.padding.right);
							}
							highlightStart.y = Mathf.Min(highlightStart.y, currentCharacter.descender - highlightState.padding.bottom);
							highlightEnd.y = Mathf.Max(highlightEnd.y, currentCharacter.ascender + highlightState.padding.top);
						}
					}
					bool flag56 = beginHighlight && this.m_CharacterCount == 1;
					if (flag56)
					{
						beginHighlight = false;
						this.DrawTextHighlight(highlightStart, highlightEnd, highlightState.color, generationSettings, textInfo);
					}
					else
					{
						bool flag57 = beginHighlight && (i == lineInfo.lastCharacterIndex || i >= lineInfo.lastVisibleCharacterIndex);
						if (flag57)
						{
							beginHighlight = false;
							this.DrawTextHighlight(highlightStart, highlightEnd, highlightState.color, generationSettings, textInfo);
						}
						else
						{
							bool flag58 = beginHighlight && !isHighlightVisible;
							if (flag58)
							{
								beginHighlight = false;
								this.DrawTextHighlight(highlightStart, highlightEnd, highlightState.color, generationSettings, textInfo);
							}
						}
					}
				}
				else
				{
					bool flag59 = beginHighlight;
					if (flag59)
					{
						beginHighlight = false;
						this.DrawTextHighlight(highlightStart, highlightEnd, highlightState.color, generationSettings, textInfo);
					}
				}
				lastLine = currentLine;
				i++;
				continue;
				IL_0933:
				bool flag60 = !generationSettings.isRightToLeft;
				if (flag60)
				{
					justificationOffset = new Vector3(0f + lineInfo.marginLeft, 0f, 0f);
				}
				else
				{
					justificationOffset = new Vector3(0f - lineInfo.maxAdvance, 0f, 0f);
				}
				goto IL_0D3D;
				IL_0985:
				justificationOffset = new Vector3(lineInfo.marginLeft + lineInfo.width / 2f - lineInfo.maxAdvance / 2f, 0f, 0f);
				goto IL_0D3D;
				IL_09BE:
				justificationOffset = new Vector3(lineInfo.marginLeft + lineInfo.width / 2f - (lineInfo.lineExtents.min.x + lineInfo.lineExtents.max.x) / 2f, 0f, 0f);
				goto IL_0D3D;
				IL_0A13:
				bool flag61 = !generationSettings.isRightToLeft;
				if (flag61)
				{
					justificationOffset = new Vector3(lineInfo.marginLeft + lineInfo.width - lineInfo.maxAdvance, 0f, 0f);
				}
				else
				{
					justificationOffset = new Vector3(lineInfo.marginLeft + lineInfo.width, 0f, 0f);
				}
				goto IL_0D3D;
				IL_0A71:
				bool flag62 = i > lineInfo.lastVisibleCharacterIndex || unicode == '\n' || unicode == '\u00ad' || unicode == '\u200b' || unicode == '\u2060' || unicode == '\u0003';
				if (flag62)
				{
					goto IL_0D3D;
				}
				char lastCharOfCurrentLine = (char)textElementInfos[lineInfo.lastCharacterIndex].character;
				bool isFlush = (lineAlignment & (TextAlignment)16) == (TextAlignment)16;
				bool flag63 = (!char.IsControl(lastCharOfCurrentLine) && currentLine < this.m_LineNumber) || isFlush || lineInfo.maxAdvance > lineInfo.width;
				if (flag63)
				{
					bool flag64 = currentLine != lastLine || i == 0 || i == generationSettings.firstVisibleCharacter;
					if (flag64)
					{
						bool flag65 = !generationSettings.isRightToLeft;
						if (flag65)
						{
							justificationOffset = new Vector3(lineInfo.marginLeft, 0f, 0f);
						}
						else
						{
							justificationOffset = new Vector3(lineInfo.marginLeft + lineInfo.width, 0f, 0f);
						}
						bool flag66 = char.IsSeparator(unicode);
						isFirstSeperator = flag66;
					}
					else
					{
						float gap = (generationSettings.isRightToLeft ? (lineInfo.width + lineInfo.maxAdvance) : (lineInfo.width - lineInfo.maxAdvance));
						int visibleCount = lineInfo.visibleCharacterCount - 1 + lineInfo.controlCharacterCount;
						int spaces = lineInfo.spaceCount - lineInfo.controlCharacterCount;
						bool flag67 = isFirstSeperator;
						if (flag67)
						{
							spaces--;
							visibleCount++;
						}
						float ratio = ((spaces > 0) ? generationSettings.wordWrappingRatio : 1f);
						bool flag68 = spaces < 1;
						if (flag68)
						{
							spaces = 1;
						}
						bool flag69 = unicode != '\u00a0' && (unicode == '\t' || char.IsSeparator(unicode));
						if (flag69)
						{
							bool flag70 = !generationSettings.isRightToLeft;
							if (flag70)
							{
								justificationOffset += new Vector3(gap * (1f - ratio) / (float)spaces, 0f, 0f);
							}
							else
							{
								justificationOffset -= new Vector3(gap * (1f - ratio) / (float)spaces, 0f, 0f);
							}
						}
						else
						{
							bool flag71 = !generationSettings.isRightToLeft;
							if (flag71)
							{
								justificationOffset += new Vector3(gap * ratio / (float)visibleCount, 0f, 0f);
							}
							else
							{
								justificationOffset -= new Vector3(gap * ratio / (float)visibleCount, 0f, 0f);
							}
						}
					}
				}
				else
				{
					bool flag72 = !generationSettings.isRightToLeft;
					if (flag72)
					{
						justificationOffset = new Vector3(lineInfo.marginLeft, 0f, 0f);
					}
					else
					{
						justificationOffset = new Vector3(lineInfo.marginLeft + lineInfo.width, 0f, 0f);
					}
				}
				goto IL_0D3D;
			}
			textInfo.characterCount = this.m_CharacterCount;
			textInfo.spriteCount = this.m_SpriteCount;
			textInfo.lineCount = lineCount;
			textInfo.wordCount = ((wordCount != 0 && this.m_CharacterCount > 0) ? wordCount : 1);
			textInfo.pageCount = this.m_PageNumber + 1;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000160EC File Offset: 0x000142EC
		public void ParsingPhase(TextInfo textInfo, TextGenerationSettings generationSettings, out uint charCode, out float maxVisibleDescender)
		{
			TextSettings textSettings = generationSettings.textSettings;
			this.m_CurrentMaterial = generationSettings.material;
			this.m_CurrentMaterialIndex = 0;
			this.m_MaterialReferenceStack.SetDefault(new MaterialReference(this.m_CurrentMaterialIndex, this.m_CurrentFontAsset, null, this.m_CurrentMaterial, this.m_Padding));
			this.m_CurrentSpriteAsset = generationSettings.spriteAsset;
			int totalCharacterCount = this.m_TotalCharacterCount;
			float baseScale = this.m_FontSize / generationSettings.fontAsset.m_FaceInfo.pointSize * generationSettings.fontAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
			float currentElementScale = baseScale;
			float currentEmScale = this.m_FontSize * 0.01f * (generationSettings.isOrthographic ? 1f : 0.1f);
			this.m_FontScaleMultiplier = 1f;
			this.m_CurrentFontSize = this.m_FontSize;
			this.m_SizeStack.SetDefault(this.m_CurrentFontSize);
			charCode = 0U;
			this.m_FontStyleInternal = generationSettings.fontStyle;
			this.m_FontWeightInternal = (((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? TextFontWeight.Bold : generationSettings.fontWeight);
			this.m_FontWeightStack.SetDefault(this.m_FontWeightInternal);
			this.m_FontStyleStack.Clear();
			this.m_LineJustification = generationSettings.textAlignment;
			this.m_LineJustificationStack.SetDefault(this.m_LineJustification);
			float padding = 0f;
			this.m_BaselineOffset = 0f;
			this.m_BaselineOffsetStack.Clear();
			this.m_FontColor32 = generationSettings.color;
			this.m_HtmlColor = this.m_FontColor32;
			this.m_UnderlineColor = this.m_HtmlColor;
			this.m_StrikethroughColor = this.m_HtmlColor;
			this.m_ColorStack.SetDefault(this.m_HtmlColor);
			this.m_UnderlineColorStack.SetDefault(this.m_HtmlColor);
			this.m_StrikethroughColorStack.SetDefault(this.m_HtmlColor);
			this.m_HighlightStateStack.SetDefault(new HighlightState(this.m_HtmlColor, Offset.zero));
			this.m_ColorGradientPreset = null;
			this.m_ColorGradientStack.SetDefault(null);
			this.m_ItalicAngle = (int)this.m_CurrentFontAsset.italicStyleSlant;
			this.m_ItalicAngleStack.SetDefault(this.m_ItalicAngle);
			this.m_ActionStack.Clear();
			this.m_FXScale = Vector3.one;
			this.m_FXRotation = Quaternion.identity;
			this.m_LineOffset = 0f;
			this.m_LineHeight = -32767f;
			float lineGap = this.m_CurrentFontAsset.faceInfo.lineHeight - (this.m_CurrentFontAsset.m_FaceInfo.ascentLine - this.m_CurrentFontAsset.m_FaceInfo.descentLine);
			this.m_CSpacing = 0f;
			this.m_MonoSpacing = 0f;
			this.m_XAdvance = 0f;
			this.m_TagLineIndent = 0f;
			this.m_TagIndent = 0f;
			this.m_IndentStack.SetDefault(0f);
			this.m_TagNoParsing = false;
			this.m_CharacterCount = 0;
			this.m_FirstCharacterOfLine = 0;
			this.m_LastCharacterOfLine = 0;
			this.m_FirstVisibleCharacterOfLine = 0;
			this.m_LastVisibleCharacterOfLine = 0;
			this.m_MaxLineAscender = -32767f;
			this.m_MaxLineDescender = 32767f;
			this.m_LineNumber = 0;
			this.m_StartOfLineAscender = 0f;
			this.m_LineVisibleCharacterCount = 0;
			this.m_LineVisibleSpaceCount = 0;
			bool isStartOfNewLine = true;
			this.m_IsDrivenLineSpacing = false;
			this.m_FirstOverflowCharacterIndex = -1;
			this.m_LastBaseGlyphIndex = int.MinValue;
			bool kerning = generationSettings.fontFeatures.Contains(OTL_FeatureTag.kern);
			bool markToBase = generationSettings.fontFeatures.Contains(OTL_FeatureTag.mark);
			bool markToMark = generationSettings.fontFeatures.Contains(OTL_FeatureTag.mkmk);
			this.m_PageNumber = 0;
			int pageToDisplay = Mathf.Clamp(generationSettings.pageToDisplay - 1, 0, textInfo.pageInfo.Length - 1);
			textInfo.ClearPageInfo();
			Vector4 margins = generationSettings.margins;
			float marginWidth = ((this.m_MarginWidth > 0f) ? this.m_MarginWidth : 0f);
			float marginHeight = ((this.m_MarginHeight > 0f) ? this.m_MarginHeight : 0f);
			this.m_MarginLeft = 0f;
			this.m_MarginRight = 0f;
			this.m_Width = -1f;
			float widthOfTextArea = marginWidth + 0.0001f - this.m_MarginLeft - this.m_MarginRight;
			this.m_MeshExtents.min = TextGeneratorUtilities.largePositiveVector2;
			this.m_MeshExtents.max = TextGeneratorUtilities.largeNegativeVector2;
			textInfo.ClearLineInfo();
			this.m_MaxCapHeight = 0f;
			this.m_MaxAscender = 0f;
			this.m_MaxDescender = 0f;
			this.m_PageAscender = 0f;
			maxVisibleDescender = 0f;
			bool isMaxVisibleDescenderSet = false;
			this.m_IsNewPage = false;
			bool isFirstWordOfLine = true;
			this.m_IsNonBreakingSpace = false;
			bool ignoreNonBreakingSpace = false;
			int lastSoftLineBreak = 0;
			CharacterSubstitution characterToSubstitute = new CharacterSubstitution(-1, 0U);
			bool isSoftHyphenIgnored = false;
			TextWrappingMode wordWrap = generationSettings.textWrappingMode;
			this.SaveWordWrappingState(ref this.m_SavedWordWrapState, -1, -1, textInfo);
			this.SaveWordWrappingState(ref this.m_SavedLineState, -1, -1, textInfo);
			this.SaveWordWrappingState(ref this.m_SavedEllipsisState, -1, -1, textInfo);
			this.SaveWordWrappingState(ref this.m_SavedLastValidState, -1, -1, textInfo);
			this.SaveWordWrappingState(ref this.m_SavedSoftLineBreakState, -1, -1, textInfo);
			this.m_EllipsisInsertionCandidateStack.Clear();
			this.m_IsTextTruncated = false;
			int restoreCount = 0;
			int i = 0;
			while (i < this.m_TextProcessingArray.Length && this.m_TextProcessingArray[i].unicode > 0U)
			{
				charCode = this.m_TextProcessingArray[i].unicode;
				bool flag = restoreCount > 5;
				if (flag)
				{
					Debug.LogError("Line breaking recursion max threshold hit... Character [" + charCode.ToString() + "] index: " + i.ToString());
					characterToSubstitute.index = this.m_CharacterCount;
					characterToSubstitute.unicode = 3U;
				}
				bool flag2 = charCode == 26U;
				if (!flag2)
				{
					bool flag3 = generationSettings.richText && charCode == 60U;
					if (flag3)
					{
						this.m_isTextLayoutPhase = true;
						this.m_TextElementType = TextElementType.Character;
						int endTagIndex;
						bool isThreadSuccess;
						bool flag4 = this.ValidateHtmlTag(this.m_TextProcessingArray, i + 1, out endTagIndex, generationSettings, textInfo, out isThreadSuccess);
						if (flag4)
						{
							i = endTagIndex;
							bool flag5 = this.m_TextElementType == TextElementType.Character;
							if (flag5)
							{
								goto IL_43A9;
							}
						}
					}
					else
					{
						this.m_TextElementType = textInfo.textElementInfo[this.m_CharacterCount].elementType;
						this.m_CurrentMaterialIndex = textInfo.textElementInfo[this.m_CharacterCount].materialReferenceIndex;
						this.m_CurrentFontAsset = textInfo.textElementInfo[this.m_CharacterCount].fontAsset;
					}
					int previousMaterialIndex = this.m_CurrentMaterialIndex;
					bool isUsingAltTypeface = textInfo.textElementInfo[this.m_CharacterCount].isUsingAlternateTypeface;
					this.m_isTextLayoutPhase = false;
					bool isInjectedCharacter = false;
					bool flag6 = characterToSubstitute.index == this.m_CharacterCount;
					if (flag6)
					{
						charCode = characterToSubstitute.unicode;
						this.m_TextElementType = TextElementType.Character;
						isInjectedCharacter = true;
						uint num = charCode;
						uint num2 = num;
						if (num2 != 3U)
						{
							if (num2 != 45U)
							{
								if (num2 == 8230U)
								{
									textInfo.textElementInfo[this.m_CharacterCount].textElement = this.m_Ellipsis.character;
									textInfo.textElementInfo[this.m_CharacterCount].elementType = TextElementType.Character;
									textInfo.textElementInfo[this.m_CharacterCount].fontAsset = this.m_Ellipsis.fontAsset;
									textInfo.textElementInfo[this.m_CharacterCount].material = this.m_Ellipsis.material;
									textInfo.textElementInfo[this.m_CharacterCount].materialReferenceIndex = this.m_Ellipsis.materialIndex;
									MaterialReference[] materialReferences = this.m_MaterialReferences;
									int materialIndex = this.m_Underline.materialIndex;
									materialReferences[materialIndex].referenceCount = materialReferences[materialIndex].referenceCount + 1;
									this.m_IsTextTruncated = true;
									characterToSubstitute.index = this.m_CharacterCount + 1;
									characterToSubstitute.unicode = 3U;
								}
							}
						}
						else
						{
							textInfo.textElementInfo[this.m_CharacterCount].textElement = this.m_CurrentFontAsset.characterLookupTable[3U];
							this.m_IsTextTruncated = true;
						}
					}
					bool flag7 = this.m_CharacterCount < generationSettings.firstVisibleCharacter && charCode != 3U;
					if (flag7)
					{
						textInfo.textElementInfo[this.m_CharacterCount].isVisible = false;
						textInfo.textElementInfo[this.m_CharacterCount].character = 8203U;
						textInfo.textElementInfo[this.m_CharacterCount].lineNumber = 0;
						this.m_CharacterCount++;
					}
					else
					{
						float smallCapsMultiplier = 1f;
						bool flag8 = this.m_TextElementType == TextElementType.Character;
						if (flag8)
						{
							bool flag9 = (this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase;
							if (flag9)
							{
								bool flag10 = char.IsLower((char)charCode);
								if (flag10)
								{
									charCode = (uint)char.ToUpper((char)charCode);
								}
							}
							else
							{
								bool flag11 = (this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase;
								if (flag11)
								{
									bool flag12 = char.IsUpper((char)charCode);
									if (flag12)
									{
										charCode = (uint)char.ToLower((char)charCode);
									}
								}
								else
								{
									bool flag13 = (this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps;
									if (flag13)
									{
										bool flag14 = char.IsLower((char)charCode);
										if (flag14)
										{
											smallCapsMultiplier = 0.8f;
											charCode = (uint)char.ToUpper((char)charCode);
										}
									}
								}
							}
						}
						float baselineOffset = 0f;
						float elementAscentLine = 0f;
						float elementDescentLine = 0f;
						bool flag15 = this.m_TextElementType == TextElementType.Sprite;
						if (flag15)
						{
							SpriteCharacter sprite = (SpriteCharacter)textInfo.textElementInfo[this.m_CharacterCount].textElement;
							this.m_CurrentSpriteAsset = sprite.textAsset as SpriteAsset;
							this.m_SpriteIndex = (int)sprite.glyphIndex;
							bool flag16 = sprite == null;
							if (flag16)
							{
								goto IL_43A9;
							}
							bool flag17 = charCode == 60U;
							if (flag17)
							{
								charCode = (uint)(57344 + this.m_SpriteIndex);
							}
							else
							{
								this.m_SpriteColor = Color.white;
							}
							float fontScale = this.m_CurrentFontSize / this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
							bool flag18 = this.m_CurrentSpriteAsset.m_FaceInfo.pointSize > 0f;
							if (flag18)
							{
								float spriteScale = this.m_CurrentFontSize / this.m_CurrentSpriteAsset.m_FaceInfo.pointSize * this.m_CurrentSpriteAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
								currentElementScale = sprite.m_Scale * sprite.m_Glyph.scale * spriteScale;
								elementAscentLine = this.m_CurrentSpriteAsset.m_FaceInfo.ascentLine;
								baselineOffset = this.m_CurrentSpriteAsset.m_FaceInfo.baseline * fontScale * this.m_FontScaleMultiplier * this.m_CurrentSpriteAsset.m_FaceInfo.scale;
								elementDescentLine = this.m_CurrentSpriteAsset.m_FaceInfo.descentLine;
							}
							else
							{
								float spriteScale2 = this.m_CurrentFontSize / this.m_CurrentFontAsset.m_FaceInfo.pointSize * this.m_CurrentFontAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
								currentElementScale = this.m_CurrentFontAsset.m_FaceInfo.ascentLine / sprite.m_Glyph.metrics.height * sprite.m_Scale * sprite.m_Glyph.scale * spriteScale2;
								float scaleDelta = spriteScale2 / currentElementScale;
								elementAscentLine = this.m_CurrentFontAsset.m_FaceInfo.ascentLine * scaleDelta;
								baselineOffset = this.m_CurrentFontAsset.m_FaceInfo.baseline * fontScale * this.m_FontScaleMultiplier * this.m_CurrentFontAsset.m_FaceInfo.scale;
								elementDescentLine = this.m_CurrentFontAsset.m_FaceInfo.descentLine * scaleDelta;
							}
							this.m_CachedTextElement = sprite;
							textInfo.textElementInfo[this.m_CharacterCount].elementType = TextElementType.Sprite;
							textInfo.textElementInfo[this.m_CharacterCount].scale = currentElementScale;
							textInfo.textElementInfo[this.m_CharacterCount].spriteAsset = this.m_CurrentSpriteAsset;
							textInfo.textElementInfo[this.m_CharacterCount].fontAsset = this.m_CurrentFontAsset;
							textInfo.textElementInfo[this.m_CharacterCount].materialReferenceIndex = this.m_CurrentMaterialIndex;
							this.m_CurrentMaterialIndex = previousMaterialIndex;
							padding = 0f;
						}
						else
						{
							bool flag19 = this.m_TextElementType == TextElementType.Character;
							if (flag19)
							{
								this.m_CachedTextElement = textInfo.textElementInfo[this.m_CharacterCount].textElement;
								bool flag20 = this.m_CachedTextElement == null;
								if (flag20)
								{
									goto IL_43A9;
								}
								this.m_CurrentFontAsset = textInfo.textElementInfo[this.m_CharacterCount].fontAsset;
								this.m_CurrentMaterial = textInfo.textElementInfo[this.m_CharacterCount].material;
								this.m_CurrentMaterialIndex = textInfo.textElementInfo[this.m_CharacterCount].materialReferenceIndex;
								bool flag21 = isInjectedCharacter && this.m_TextProcessingArray[i].unicode == 10U && this.m_CharacterCount != this.m_FirstCharacterOfLine;
								float adjustedScale;
								if (flag21)
								{
									adjustedScale = textInfo.textElementInfo[this.m_CharacterCount - 1].pointSize * smallCapsMultiplier / this.m_CurrentFontAsset.m_FaceInfo.pointSize * this.m_CurrentFontAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
								}
								else
								{
									adjustedScale = this.m_CurrentFontSize * smallCapsMultiplier / this.m_CurrentFontAsset.m_FaceInfo.pointSize * this.m_CurrentFontAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
								}
								bool flag22 = isInjectedCharacter && charCode == 8230U;
								if (flag22)
								{
									elementAscentLine = 0f;
									elementDescentLine = 0f;
								}
								else
								{
									elementAscentLine = this.m_CurrentFontAsset.m_FaceInfo.ascentLine;
									elementDescentLine = this.m_CurrentFontAsset.m_FaceInfo.descentLine;
								}
								currentElementScale = adjustedScale * this.m_FontScaleMultiplier * this.m_CachedTextElement.m_Scale * this.m_CachedTextElement.m_Glyph.scale;
								baselineOffset = this.m_CurrentFontAsset.m_FaceInfo.baseline * adjustedScale * this.m_FontScaleMultiplier * this.m_CurrentFontAsset.m_FaceInfo.scale;
								textInfo.textElementInfo[this.m_CharacterCount].elementType = TextElementType.Character;
								textInfo.textElementInfo[this.m_CharacterCount].scale = currentElementScale;
								padding = this.m_Padding;
							}
						}
						float currentElementUnmodifiedScale = currentElementScale;
						bool flag23 = charCode == 173U || charCode == 3U;
						if (flag23)
						{
							currentElementScale = 0f;
						}
						textInfo.textElementInfo[this.m_CharacterCount].character = charCode;
						textInfo.textElementInfo[this.m_CharacterCount].pointSize = this.m_CurrentFontSize;
						textInfo.textElementInfo[this.m_CharacterCount].color = this.m_HtmlColor;
						textInfo.textElementInfo[this.m_CharacterCount].underlineColor = this.m_UnderlineColor;
						textInfo.textElementInfo[this.m_CharacterCount].strikethroughColor = this.m_StrikethroughColor;
						textInfo.textElementInfo[this.m_CharacterCount].highlightState = this.m_HighlightState;
						textInfo.textElementInfo[this.m_CharacterCount].style = this.m_FontStyleInternal;
						bool flag24 = this.m_FontWeightInternal == TextFontWeight.Bold;
						if (flag24)
						{
							TextElementInfo[] textElementInfo = textInfo.textElementInfo;
							int characterCount = this.m_CharacterCount;
							textElementInfo[characterCount].style = textElementInfo[characterCount].style | FontStyles.Bold;
						}
						Glyph altGlyph = textInfo.textElementInfo[this.m_CharacterCount].alternativeGlyph;
						GlyphMetrics currentGlyphMetrics = ((altGlyph == null) ? this.m_CachedTextElement.m_Glyph.metrics : altGlyph.metrics);
						bool isWhiteSpace = charCode <= 65535U && char.IsWhiteSpace((char)charCode);
						GlyphValueRecord glyphAdjustments = default(GlyphValueRecord);
						float characterSpacingAdjustment = generationSettings.characterSpacing;
						bool flag25 = kerning && this.m_TextElementType == TextElementType.Character;
						if (flag25)
						{
							uint baseGlyphIndex = this.m_CachedTextElement.m_GlyphIndex;
							bool flag26 = this.m_CharacterCount < totalCharacterCount - 1 && textInfo.textElementInfo[this.m_CharacterCount + 1].elementType == TextElementType.Character;
							GlyphPairAdjustmentRecord adjustmentPair;
							if (flag26)
							{
								uint nextGlyphIndex = textInfo.textElementInfo[this.m_CharacterCount + 1].textElement.m_GlyphIndex;
								uint key = (nextGlyphIndex << 16) | baseGlyphIndex;
								bool flag27 = this.m_CurrentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(key, out adjustmentPair);
								if (flag27)
								{
									glyphAdjustments = adjustmentPair.firstAdjustmentRecord.glyphValueRecord;
									characterSpacingAdjustment = (((adjustmentPair.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : characterSpacingAdjustment);
								}
							}
							bool flag28 = this.m_CharacterCount >= 1;
							if (flag28)
							{
								uint previousGlyphIndex = textInfo.textElementInfo[this.m_CharacterCount - 1].textElement.m_GlyphIndex;
								uint key2 = (baseGlyphIndex << 16) | previousGlyphIndex;
								bool flag29 = textInfo.textElementInfo[this.m_CharacterCount - 1].elementType == TextElementType.Character && this.m_CurrentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(key2, out adjustmentPair);
								if (flag29)
								{
									glyphAdjustments += adjustmentPair.secondAdjustmentRecord.glyphValueRecord;
									characterSpacingAdjustment = (((adjustmentPair.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : characterSpacingAdjustment);
								}
							}
							textInfo.textElementInfo[this.m_CharacterCount].adjustedHorizontalAdvance = glyphAdjustments.xAdvance;
						}
						bool isBaseGlyph = TextGeneratorUtilities.IsBaseGlyph(charCode);
						bool flag30 = isBaseGlyph;
						if (flag30)
						{
							this.m_LastBaseGlyphIndex = this.m_CharacterCount;
						}
						bool flag31 = this.m_CharacterCount > 0 && !isBaseGlyph;
						if (flag31)
						{
							bool flag32 = markToBase && this.m_LastBaseGlyphIndex != int.MinValue && this.m_LastBaseGlyphIndex == this.m_CharacterCount - 1;
							if (flag32)
							{
								Glyph baseGlyph = textInfo.textElementInfo[this.m_LastBaseGlyphIndex].textElement.glyph;
								uint baseGlyphIndex2 = baseGlyph.index;
								uint markGlyphIndex = this.m_CachedTextElement.glyphIndex;
								uint key3 = (markGlyphIndex << 16) | baseGlyphIndex2;
								MarkToBaseAdjustmentRecord glyphAdjustmentRecord;
								bool flag33 = this.m_CurrentFontAsset.fontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.TryGetValue(key3, out glyphAdjustmentRecord);
								if (flag33)
								{
									float advanceOffset = (textInfo.textElementInfo[this.m_LastBaseGlyphIndex].origin - this.m_XAdvance) / currentElementScale;
									glyphAdjustments.xPlacement = advanceOffset + glyphAdjustmentRecord.baseGlyphAnchorPoint.xCoordinate - glyphAdjustmentRecord.markPositionAdjustment.xPositionAdjustment;
									glyphAdjustments.yPlacement = glyphAdjustmentRecord.baseGlyphAnchorPoint.yCoordinate - glyphAdjustmentRecord.markPositionAdjustment.yPositionAdjustment;
									characterSpacingAdjustment = 0f;
								}
							}
							else
							{
								bool wasLookupApplied = false;
								bool flag34 = markToMark;
								if (flag34)
								{
									int characterLookupIndex = this.m_CharacterCount - 1;
									while (characterLookupIndex >= 0 && characterLookupIndex != this.m_LastBaseGlyphIndex)
									{
										Glyph baseMarkGlyph = textInfo.textElementInfo[characterLookupIndex].textElement.glyph;
										uint baseGlyphIndex3 = baseMarkGlyph.index;
										uint combiningMarkGlyphIndex = this.m_CachedTextElement.glyphIndex;
										uint key4 = (combiningMarkGlyphIndex << 16) | baseGlyphIndex3;
										MarkToMarkAdjustmentRecord glyphAdjustmentRecord2;
										bool flag35 = this.m_CurrentFontAsset.fontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.TryGetValue(key4, out glyphAdjustmentRecord2);
										if (flag35)
										{
											float baseMarkOrigin = (textInfo.textElementInfo[characterLookupIndex].origin - this.m_XAdvance) / currentElementScale;
											float currentBaseline = baselineOffset - this.m_LineOffset + this.m_BaselineOffset;
											float baseMarkBaseline = (textInfo.textElementInfo[characterLookupIndex].baseLine - currentBaseline) / currentElementScale;
											glyphAdjustments.xPlacement = baseMarkOrigin + glyphAdjustmentRecord2.baseMarkGlyphAnchorPoint.xCoordinate - glyphAdjustmentRecord2.combiningMarkPositionAdjustment.xPositionAdjustment;
											glyphAdjustments.yPlacement = baseMarkBaseline + glyphAdjustmentRecord2.baseMarkGlyphAnchorPoint.yCoordinate - glyphAdjustmentRecord2.combiningMarkPositionAdjustment.yPositionAdjustment;
											characterSpacingAdjustment = 0f;
											wasLookupApplied = true;
											break;
										}
										characterLookupIndex--;
									}
								}
								bool flag36 = markToBase && this.m_LastBaseGlyphIndex != int.MinValue && !wasLookupApplied;
								if (flag36)
								{
									Glyph baseGlyph2 = textInfo.textElementInfo[this.m_LastBaseGlyphIndex].textElement.glyph;
									uint baseGlyphIndex4 = baseGlyph2.index;
									uint markGlyphIndex2 = this.m_CachedTextElement.glyphIndex;
									uint key5 = (markGlyphIndex2 << 16) | baseGlyphIndex4;
									MarkToBaseAdjustmentRecord glyphAdjustmentRecord3;
									bool flag37 = this.m_CurrentFontAsset.fontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.TryGetValue(key5, out glyphAdjustmentRecord3);
									if (flag37)
									{
										float advanceOffset2 = (textInfo.textElementInfo[this.m_LastBaseGlyphIndex].origin - this.m_XAdvance) / currentElementScale;
										glyphAdjustments.xPlacement = advanceOffset2 + glyphAdjustmentRecord3.baseGlyphAnchorPoint.xCoordinate - glyphAdjustmentRecord3.markPositionAdjustment.xPositionAdjustment;
										glyphAdjustments.yPlacement = glyphAdjustmentRecord3.baseGlyphAnchorPoint.yCoordinate - glyphAdjustmentRecord3.markPositionAdjustment.yPositionAdjustment;
										characterSpacingAdjustment = 0f;
									}
								}
							}
						}
						elementAscentLine += glyphAdjustments.yPlacement;
						elementDescentLine += glyphAdjustments.yPlacement;
						bool isRightToLeft = generationSettings.isRightToLeft;
						if (isRightToLeft)
						{
							this.m_XAdvance -= currentGlyphMetrics.horizontalAdvance * (1f - this.m_CharWidthAdjDelta) * currentElementScale;
							bool flag38 = isWhiteSpace || charCode == 8203U;
							if (flag38)
							{
								this.m_XAdvance -= generationSettings.wordSpacing * currentEmScale;
							}
						}
						float monoAdvance = 0f;
						bool flag39 = this.m_MonoSpacing != 0f && charCode != 8203U;
						if (flag39)
						{
							bool flag40 = this.m_DuoSpace && (charCode == 46U || charCode == 58U || charCode == 44U);
							if (flag40)
							{
								monoAdvance = (this.m_MonoSpacing / 4f - (currentGlyphMetrics.width / 2f + currentGlyphMetrics.horizontalBearingX) * currentElementScale) * (1f - this.m_CharWidthAdjDelta);
							}
							else
							{
								monoAdvance = (this.m_MonoSpacing / 2f - (currentGlyphMetrics.width / 2f + currentGlyphMetrics.horizontalBearingX) * currentElementScale) * (1f - this.m_CharWidthAdjDelta);
							}
							this.m_XAdvance += monoAdvance;
						}
						bool hasGradientScale = this.m_CurrentFontAsset.atlasRenderMode != GlyphRenderMode.SMOOTH && this.m_CurrentFontAsset.atlasRenderMode != GlyphRenderMode.COLOR;
						bool flag41 = this.m_TextElementType == TextElementType.Character && !isUsingAltTypeface && (textInfo.textElementInfo[this.m_CharacterCount].style & FontStyles.Bold) == FontStyles.Bold;
						float stylePadding;
						float boldSpacingAdjustment;
						if (flag41)
						{
							bool flag42 = hasGradientScale;
							if (flag42)
							{
								float gradientScale = ((generationSettings.isIMGUI && this.m_CurrentMaterial.HasFloat(TextShaderUtilities.ID_GradientScale)) ? this.m_CurrentMaterial.GetFloat(TextShaderUtilities.ID_GradientScale) : ((float)(this.m_CurrentFontAsset.atlasPadding + 1)));
								stylePadding = this.m_CurrentFontAsset.boldStyleWeight / 4f * gradientScale;
								bool flag43 = stylePadding + padding > gradientScale;
								if (flag43)
								{
									padding = gradientScale - stylePadding;
								}
							}
							else
							{
								stylePadding = 0f;
							}
							boldSpacingAdjustment = this.m_CurrentFontAsset.boldStyleSpacing;
						}
						else
						{
							bool flag44 = hasGradientScale;
							if (flag44)
							{
								float gradientScale2 = ((generationSettings.isIMGUI && this.m_CurrentMaterial.HasFloat(TextShaderUtilities.ID_GradientScale)) ? this.m_CurrentMaterial.GetFloat(TextShaderUtilities.ID_GradientScale) : ((float)(this.m_CurrentFontAsset.atlasPadding + 1)));
								stylePadding = this.m_CurrentFontAsset.m_RegularStyleWeight / 4f * gradientScale2;
								bool flag45 = stylePadding + padding > gradientScale2;
								if (flag45)
								{
									padding = gradientScale2 - stylePadding;
								}
							}
							else
							{
								stylePadding = 0f;
							}
							boldSpacingAdjustment = 0f;
						}
						Vector3 topLeft;
						topLeft.x = this.m_XAdvance + (currentGlyphMetrics.horizontalBearingX * this.m_FXScale.x - padding - stylePadding + glyphAdjustments.xPlacement) * currentElementScale * (1f - this.m_CharWidthAdjDelta);
						topLeft.y = baselineOffset + (currentGlyphMetrics.horizontalBearingY + padding + glyphAdjustments.yPlacement) * currentElementScale - this.m_LineOffset + this.m_BaselineOffset;
						topLeft.z = 0f;
						Vector3 bottomLeft;
						bottomLeft.x = topLeft.x;
						bottomLeft.y = topLeft.y - (currentGlyphMetrics.height + padding * 2f) * currentElementScale;
						bottomLeft.z = 0f;
						Vector3 topRight;
						topRight.x = bottomLeft.x + (currentGlyphMetrics.width * this.m_FXScale.x + padding * 2f + stylePadding * 2f) * currentElementScale * (1f - this.m_CharWidthAdjDelta);
						topRight.y = topLeft.y;
						topRight.z = 0f;
						Vector3 bottomRight;
						bottomRight.x = topRight.x;
						bottomRight.y = bottomLeft.y;
						bottomRight.z = 0f;
						bool flag46 = this.m_TextElementType == TextElementType.Character && !isUsingAltTypeface && (this.m_FontStyleInternal & FontStyles.Italic) == FontStyles.Italic;
						if (flag46)
						{
							float shearValue = (float)this.m_ItalicAngle * 0.01f;
							float midPoint = (this.m_CurrentFontAsset.m_FaceInfo.capLine - (this.m_CurrentFontAsset.m_FaceInfo.baseline + this.m_BaselineOffset)) / 2f * this.m_FontScaleMultiplier * this.m_CurrentFontAsset.m_FaceInfo.scale;
							Vector3 topShear = new Vector3(shearValue * ((currentGlyphMetrics.horizontalBearingY + padding + stylePadding - midPoint) * currentElementScale), 0f, 0f);
							Vector3 bottomShear = new Vector3(shearValue * ((currentGlyphMetrics.horizontalBearingY - currentGlyphMetrics.height - padding - stylePadding - midPoint) * currentElementScale), 0f, 0f);
							topLeft += topShear;
							bottomLeft += bottomShear;
							topRight += topShear;
							bottomRight += bottomShear;
						}
						bool flag47 = this.m_FXRotation != Quaternion.identity;
						if (flag47)
						{
							Matrix4x4 rotationMatrix = Matrix4x4.Rotate(this.m_FXRotation);
							Vector3 positionOffset = (topRight + bottomLeft) / 2f;
							topLeft = rotationMatrix.MultiplyPoint3x4(topLeft - positionOffset) + positionOffset;
							bottomLeft = rotationMatrix.MultiplyPoint3x4(bottomLeft - positionOffset) + positionOffset;
							topRight = rotationMatrix.MultiplyPoint3x4(topRight - positionOffset) + positionOffset;
							bottomRight = rotationMatrix.MultiplyPoint3x4(bottomRight - positionOffset) + positionOffset;
						}
						textInfo.textElementInfo[this.m_CharacterCount].bottomLeft = bottomLeft;
						textInfo.textElementInfo[this.m_CharacterCount].topLeft = topLeft;
						textInfo.textElementInfo[this.m_CharacterCount].topRight = topRight;
						textInfo.textElementInfo[this.m_CharacterCount].bottomRight = bottomRight;
						textInfo.textElementInfo[this.m_CharacterCount].origin = this.m_XAdvance + glyphAdjustments.xPlacement * currentElementScale;
						textInfo.textElementInfo[this.m_CharacterCount].baseLine = baselineOffset - this.m_LineOffset + this.m_BaselineOffset + glyphAdjustments.yPlacement * currentElementScale;
						textInfo.textElementInfo[this.m_CharacterCount].aspectRatio = (topRight.x - bottomLeft.x) / (topLeft.y - bottomLeft.y);
						float elementAscender = ((this.m_TextElementType == TextElementType.Character) ? (elementAscentLine * currentElementScale / smallCapsMultiplier + this.m_BaselineOffset) : (elementAscentLine * currentElementScale + this.m_BaselineOffset));
						float elementDescender = ((this.m_TextElementType == TextElementType.Character) ? (elementDescentLine * currentElementScale / smallCapsMultiplier + this.m_BaselineOffset) : (elementDescentLine * currentElementScale + this.m_BaselineOffset));
						float adjustedAscender = elementAscender;
						float adjustedDescender = elementDescender;
						bool isFirstCharacterOfLine = this.m_CharacterCount == this.m_FirstCharacterOfLine;
						bool flag48 = isFirstCharacterOfLine || !isWhiteSpace;
						if (flag48)
						{
							bool flag49 = this.m_BaselineOffset != 0f;
							if (flag49)
							{
								adjustedAscender = Mathf.Max((elementAscender - this.m_BaselineOffset) / this.m_FontScaleMultiplier, adjustedAscender);
								adjustedDescender = Mathf.Min((elementDescender - this.m_BaselineOffset) / this.m_FontScaleMultiplier, adjustedDescender);
							}
							this.m_MaxLineAscender = Mathf.Max(adjustedAscender, this.m_MaxLineAscender);
							this.m_MaxLineDescender = Mathf.Min(adjustedDescender, this.m_MaxLineDescender);
						}
						bool flag50 = isFirstCharacterOfLine || !isWhiteSpace;
						if (flag50)
						{
							textInfo.textElementInfo[this.m_CharacterCount].adjustedAscender = adjustedAscender;
							textInfo.textElementInfo[this.m_CharacterCount].adjustedDescender = adjustedDescender;
							textInfo.textElementInfo[this.m_CharacterCount].ascender = elementAscender - this.m_LineOffset;
							this.m_MaxDescender = (textInfo.textElementInfo[this.m_CharacterCount].descender = elementDescender - this.m_LineOffset);
						}
						else
						{
							textInfo.textElementInfo[this.m_CharacterCount].adjustedAscender = this.m_MaxLineAscender;
							textInfo.textElementInfo[this.m_CharacterCount].adjustedDescender = this.m_MaxLineDescender;
							textInfo.textElementInfo[this.m_CharacterCount].ascender = this.m_MaxLineAscender - this.m_LineOffset;
							this.m_MaxDescender = (textInfo.textElementInfo[this.m_CharacterCount].descender = this.m_MaxLineDescender - this.m_LineOffset);
						}
						bool flag51 = this.m_LineNumber == 0 || this.m_IsNewPage;
						if (flag51)
						{
							bool flag52 = isFirstCharacterOfLine || !isWhiteSpace;
							if (flag52)
							{
								this.m_MaxAscender = this.m_MaxLineAscender;
								this.m_MaxCapHeight = Mathf.Max(this.m_MaxCapHeight, this.m_CurrentFontAsset.m_FaceInfo.capLine * currentElementScale / smallCapsMultiplier);
							}
						}
						bool flag53 = this.m_LineOffset == 0f;
						if (flag53)
						{
							bool flag54 = isFirstCharacterOfLine || !isWhiteSpace;
							if (flag54)
							{
								this.m_PageAscender = ((this.m_PageAscender > elementAscender) ? this.m_PageAscender : elementAscender);
							}
						}
						textInfo.textElementInfo[this.m_CharacterCount].isVisible = false;
						bool flag55 = charCode == 9U || ((wordWrap == TextWrappingMode.PreserveWhitespace || wordWrap == TextWrappingMode.PreserveWhitespaceNoWrap) && (isWhiteSpace || charCode == 8203U)) || (!isWhiteSpace && charCode != 8203U && charCode != 173U && charCode != 3U) || (charCode == 173U && !isSoftHyphenIgnored) || this.m_TextElementType == TextElementType.Sprite;
						if (flag55)
						{
							textInfo.textElementInfo[this.m_CharacterCount].isVisible = true;
							float marginLeft = this.m_MarginLeft;
							float marginRight = this.m_MarginRight;
							bool flag56 = isInjectedCharacter;
							if (flag56)
							{
								marginLeft = textInfo.lineInfo[this.m_LineNumber].marginLeft;
								marginRight = textInfo.lineInfo[this.m_LineNumber].marginRight;
							}
							widthOfTextArea = ((this.m_Width != -1f) ? Mathf.Min(marginWidth + 0.0001f - marginLeft - marginRight, this.m_Width) : (marginWidth + 0.0001f - marginLeft - marginRight));
							float textWidth = Mathf.Abs(this.m_XAdvance) + ((!generationSettings.isRightToLeft) ? currentGlyphMetrics.horizontalAdvance : 0f) * (1f - this.m_CharWidthAdjDelta) * ((charCode == 173U) ? currentElementUnmodifiedScale : currentElementScale);
							float textHeight = this.m_MaxAscender - (this.m_MaxLineDescender - this.m_LineOffset) + ((this.m_LineOffset > 0f && !this.m_IsDrivenLineSpacing) ? (this.m_MaxLineAscender - this.m_StartOfLineAscender) : 0f);
							int testedCharacterCount = this.m_CharacterCount;
							bool flag57 = textHeight > marginHeight + 0.0001f;
							if (flag57)
							{
								bool flag58 = this.m_FirstOverflowCharacterIndex == -1;
								if (flag58)
								{
									this.m_FirstOverflowCharacterIndex = this.m_CharacterCount;
								}
								bool autoSize = generationSettings.autoSize;
								if (autoSize)
								{
									bool flag59 = this.m_LineSpacingDelta > generationSettings.lineSpacingMax && this.m_LineOffset > 0f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount;
									if (flag59)
									{
										float adjustmentDelta = (marginHeight - textHeight) / (float)this.m_LineNumber;
										this.m_LineSpacingDelta = Mathf.Max(this.m_LineSpacingDelta + adjustmentDelta / baseScale, generationSettings.lineSpacingMax);
										break;
									}
									bool flag60 = this.m_FontSize > generationSettings.fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount;
									if (flag60)
									{
										this.m_MaxFontSize = this.m_FontSize;
										float sizeDelta = Mathf.Max((this.m_FontSize - this.m_MinFontSize) / 2f, 0.05f);
										this.m_FontSize -= sizeDelta;
										this.m_FontSize = Mathf.Max((float)((int)(this.m_FontSize * 20f + 0.5f)) / 20f, generationSettings.fontSizeMin);
										break;
									}
								}
								switch (generationSettings.overflowMode)
								{
								case TextOverflowMode.Ellipsis:
								{
									bool flag61 = this.m_LineNumber > 0;
									if (flag61)
									{
										bool flag62 = this.m_EllipsisInsertionCandidateStack.Count == 0;
										if (flag62)
										{
											i = -1;
											this.m_CharacterCount = 0;
											characterToSubstitute.index = 0;
											characterToSubstitute.unicode = 3U;
											this.m_FirstCharacterOfLine = 0;
											goto IL_43A9;
										}
										WordWrapState ellipsisState = this.m_EllipsisInsertionCandidateStack.Pop();
										i = this.RestoreWordWrappingState(ref ellipsisState, textInfo);
										i--;
										this.m_CharacterCount--;
										characterToSubstitute.index = this.m_CharacterCount;
										characterToSubstitute.unicode = 8230U;
										restoreCount++;
										goto IL_43A9;
									}
									break;
								}
								case TextOverflowMode.Truncate:
									i = this.RestoreWordWrappingState(ref this.m_SavedLastValidState, textInfo);
									characterToSubstitute.index = testedCharacterCount;
									characterToSubstitute.unicode = 3U;
									goto IL_43A9;
								case TextOverflowMode.Page:
								{
									bool flag63 = i < 0 || testedCharacterCount == 0;
									if (flag63)
									{
										i = -1;
										this.m_CharacterCount = 0;
										characterToSubstitute.index = 0;
										characterToSubstitute.unicode = 3U;
										goto IL_43A9;
									}
									bool flag64 = this.m_MaxLineAscender - this.m_MaxLineDescender > marginHeight + 0.0001f;
									if (flag64)
									{
										i = this.RestoreWordWrappingState(ref this.m_SavedLineState, textInfo);
										characterToSubstitute.index = testedCharacterCount;
										characterToSubstitute.unicode = 3U;
										goto IL_43A9;
									}
									i = this.RestoreWordWrappingState(ref this.m_SavedLineState, textInfo);
									this.m_IsNewPage = true;
									this.m_FirstCharacterOfLine = this.m_CharacterCount;
									this.m_MaxLineAscender = -32767f;
									this.m_MaxLineDescender = 32767f;
									this.m_StartOfLineAscender = 0f;
									this.m_XAdvance = 0f + this.m_TagIndent;
									this.m_LineOffset = 0f;
									this.m_MaxAscender = 0f;
									this.m_PageAscender = 0f;
									this.m_LineNumber++;
									this.m_PageNumber++;
									goto IL_43A9;
								}
								case TextOverflowMode.Linked:
									i = this.RestoreWordWrappingState(ref this.m_SavedLastValidState, textInfo);
									characterToSubstitute.index = testedCharacterCount;
									characterToSubstitute.unicode = 3U;
									goto IL_43A9;
								}
							}
							bool flag65 = isBaseGlyph && textWidth > widthOfTextArea;
							if (flag65)
							{
								bool flag66 = wordWrap != TextWrappingMode.NoWrap && wordWrap != TextWrappingMode.PreserveWhitespaceNoWrap && this.m_CharacterCount != this.m_FirstCharacterOfLine;
								if (flag66)
								{
									i = this.RestoreWordWrappingState(ref this.m_SavedWordWrapState, textInfo);
									bool flag67 = this.m_LineHeight == -32767f;
									float lineOffsetDelta;
									if (flag67)
									{
										float ascender = textInfo.textElementInfo[this.m_CharacterCount].adjustedAscender;
										lineOffsetDelta = ((this.m_LineOffset > 0f && !this.m_IsDrivenLineSpacing) ? (this.m_MaxLineAscender - this.m_StartOfLineAscender) : 0f) - this.m_MaxLineDescender + ascender + (lineGap + this.m_LineSpacingDelta) * baseScale + generationSettings.lineSpacing * currentEmScale;
									}
									else
									{
										lineOffsetDelta = this.m_LineHeight + generationSettings.lineSpacing * currentEmScale;
										this.m_IsDrivenLineSpacing = true;
									}
									float newTextHeight = this.m_MaxAscender + lineOffsetDelta + this.m_LineOffset - textInfo.textElementInfo[this.m_CharacterCount].adjustedDescender;
									bool flag68 = textInfo.textElementInfo[this.m_CharacterCount - 1].character == 173U && !isSoftHyphenIgnored;
									if (flag68)
									{
										bool flag69 = generationSettings.overflowMode == TextOverflowMode.Overflow || newTextHeight < marginHeight + 0.0001f;
										if (flag69)
										{
											characterToSubstitute.index = this.m_CharacterCount - 1;
											characterToSubstitute.unicode = 45U;
											i--;
											this.m_CharacterCount--;
											goto IL_43A9;
										}
									}
									isSoftHyphenIgnored = false;
									bool flag70 = textInfo.textElementInfo[this.m_CharacterCount].character == 173U;
									if (flag70)
									{
										isSoftHyphenIgnored = true;
										goto IL_43A9;
									}
									bool flag71 = generationSettings.autoSize && isFirstWordOfLine;
									if (flag71)
									{
										bool flag72 = this.m_CharWidthAdjDelta < generationSettings.charWidthMaxAdj / 100f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount;
										if (flag72)
										{
											float adjustedTextWidth = textWidth;
											bool flag73 = this.m_CharWidthAdjDelta > 0f;
											if (flag73)
											{
												adjustedTextWidth /= 1f - this.m_CharWidthAdjDelta;
											}
											float adjustmentDelta2 = textWidth - (widthOfTextArea - 0.0001f);
											this.m_CharWidthAdjDelta += adjustmentDelta2 / adjustedTextWidth;
											this.m_CharWidthAdjDelta = Mathf.Min(this.m_CharWidthAdjDelta, generationSettings.charWidthMaxAdj / 100f);
											break;
										}
										bool flag74 = this.m_FontSize > generationSettings.fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount;
										if (flag74)
										{
											this.m_MaxFontSize = this.m_FontSize;
											float sizeDelta2 = Mathf.Max((this.m_FontSize - this.m_MinFontSize) / 2f, 0.05f);
											this.m_FontSize -= sizeDelta2;
											this.m_FontSize = Mathf.Max((float)((int)(this.m_FontSize * 20f + 0.5f)) / 20f, generationSettings.fontSizeMin);
											break;
										}
									}
									int savedSoftLineBreakingSpace = this.m_SavedSoftLineBreakState.previousWordBreak;
									bool flag75 = isFirstWordOfLine && savedSoftLineBreakingSpace != -1;
									if (flag75)
									{
										bool flag76 = savedSoftLineBreakingSpace != lastSoftLineBreak;
										if (flag76)
										{
											i = this.RestoreWordWrappingState(ref this.m_SavedSoftLineBreakState, textInfo);
											lastSoftLineBreak = savedSoftLineBreakingSpace;
											bool flag77 = textInfo.textElementInfo[this.m_CharacterCount - 1].character == 173U;
											if (flag77)
											{
												characterToSubstitute.index = this.m_CharacterCount - 1;
												characterToSubstitute.unicode = 45U;
												i--;
												this.m_CharacterCount--;
												goto IL_43A9;
											}
										}
									}
									bool flag78 = newTextHeight > marginHeight + 0.0001f;
									if (!flag78)
									{
										this.InsertNewLine(i, baseScale, currentElementScale, currentEmScale, boldSpacingAdjustment, characterSpacingAdjustment, widthOfTextArea, lineGap, ref isMaxVisibleDescenderSet, ref maxVisibleDescender, generationSettings, textInfo);
										isStartOfNewLine = true;
										isFirstWordOfLine = true;
										goto IL_43A9;
									}
									bool flag79 = this.m_FirstOverflowCharacterIndex == -1;
									if (flag79)
									{
										this.m_FirstOverflowCharacterIndex = this.m_CharacterCount;
									}
									bool autoSize2 = generationSettings.autoSize;
									if (autoSize2)
									{
										bool flag80 = this.m_LineSpacingDelta > generationSettings.lineSpacingMax && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount;
										if (flag80)
										{
											float adjustmentDelta3 = (marginHeight - newTextHeight) / (float)(this.m_LineNumber + 1);
											this.m_LineSpacingDelta = Mathf.Max(this.m_LineSpacingDelta + adjustmentDelta3 / baseScale, generationSettings.lineSpacingMax);
											break;
										}
										bool flag81 = this.m_CharWidthAdjDelta < generationSettings.charWidthMaxAdj / 100f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount;
										if (flag81)
										{
											float adjustedTextWidth2 = textWidth;
											bool flag82 = this.m_CharWidthAdjDelta > 0f;
											if (flag82)
											{
												adjustedTextWidth2 /= 1f - this.m_CharWidthAdjDelta;
											}
											float adjustmentDelta4 = textWidth - (widthOfTextArea - 0.0001f);
											this.m_CharWidthAdjDelta += adjustmentDelta4 / adjustedTextWidth2;
											this.m_CharWidthAdjDelta = Mathf.Min(this.m_CharWidthAdjDelta, generationSettings.charWidthMaxAdj / 100f);
											break;
										}
										bool flag83 = this.m_FontSize > generationSettings.fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount;
										if (flag83)
										{
											this.m_MaxFontSize = this.m_FontSize;
											float sizeDelta3 = Mathf.Max((this.m_FontSize - this.m_MinFontSize) / 2f, 0.05f);
											this.m_FontSize -= sizeDelta3;
											this.m_FontSize = Mathf.Max((float)((int)(this.m_FontSize * 20f + 0.5f)) / 20f, generationSettings.fontSizeMin);
											break;
										}
									}
									switch (generationSettings.overflowMode)
									{
									case TextOverflowMode.Overflow:
									case TextOverflowMode.Masking:
									case TextOverflowMode.ScrollRect:
										this.InsertNewLine(i, baseScale, currentElementScale, currentEmScale, boldSpacingAdjustment, characterSpacingAdjustment, widthOfTextArea, lineGap, ref isMaxVisibleDescenderSet, ref maxVisibleDescender, generationSettings, textInfo);
										isStartOfNewLine = true;
										isFirstWordOfLine = true;
										goto IL_43A9;
									case TextOverflowMode.Ellipsis:
									{
										bool flag84 = this.m_EllipsisInsertionCandidateStack.Count == 0;
										if (flag84)
										{
											i = -1;
											this.m_CharacterCount = 0;
											characterToSubstitute.index = 0;
											characterToSubstitute.unicode = 3U;
											this.m_FirstCharacterOfLine = 0;
											goto IL_43A9;
										}
										WordWrapState ellipsisState2 = this.m_EllipsisInsertionCandidateStack.Pop();
										i = this.RestoreWordWrappingState(ref ellipsisState2, textInfo);
										i--;
										this.m_CharacterCount--;
										characterToSubstitute.index = this.m_CharacterCount;
										characterToSubstitute.unicode = 8230U;
										restoreCount++;
										goto IL_43A9;
									}
									case TextOverflowMode.Truncate:
										i = this.RestoreWordWrappingState(ref this.m_SavedLastValidState, textInfo);
										characterToSubstitute.index = testedCharacterCount;
										characterToSubstitute.unicode = 3U;
										goto IL_43A9;
									case TextOverflowMode.Page:
										this.m_IsNewPage = true;
										this.InsertNewLine(i, baseScale, currentElementScale, currentEmScale, boldSpacingAdjustment, characterSpacingAdjustment, widthOfTextArea, lineGap, ref isMaxVisibleDescenderSet, ref maxVisibleDescender, generationSettings, textInfo);
										this.m_StartOfLineAscender = 0f;
										this.m_LineOffset = 0f;
										this.m_MaxAscender = 0f;
										this.m_PageAscender = 0f;
										this.m_PageNumber++;
										isStartOfNewLine = true;
										isFirstWordOfLine = true;
										goto IL_43A9;
									case TextOverflowMode.Linked:
										characterToSubstitute.index = this.m_CharacterCount;
										characterToSubstitute.unicode = 3U;
										goto IL_43A9;
									}
								}
								else
								{
									bool flag85 = generationSettings.autoSize && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount;
									if (flag85)
									{
										bool flag86 = this.m_CharWidthAdjDelta < generationSettings.charWidthMaxAdj / 100f;
										if (flag86)
										{
											float adjustedTextWidth3 = textWidth;
											bool flag87 = this.m_CharWidthAdjDelta > 0f;
											if (flag87)
											{
												adjustedTextWidth3 /= 1f - this.m_CharWidthAdjDelta;
											}
											float adjustmentDelta5 = textWidth - (widthOfTextArea - 0.0001f);
											this.m_CharWidthAdjDelta += adjustmentDelta5 / adjustedTextWidth3;
											this.m_CharWidthAdjDelta = Mathf.Min(this.m_CharWidthAdjDelta, generationSettings.charWidthMaxAdj / 100f);
											break;
										}
										bool flag88 = this.m_FontSize > generationSettings.fontSizeMin;
										if (flag88)
										{
											this.m_MaxFontSize = this.m_FontSize;
											float sizeDelta4 = Mathf.Max((this.m_FontSize - this.m_MinFontSize) / 2f, 0.05f);
											this.m_FontSize -= sizeDelta4;
											this.m_FontSize = Mathf.Max((float)((int)(this.m_FontSize * 20f + 0.5f)) / 20f, generationSettings.fontSizeMin);
											break;
										}
									}
									switch (generationSettings.overflowMode)
									{
									case TextOverflowMode.Ellipsis:
									{
										bool flag89 = this.m_EllipsisInsertionCandidateStack.Count == 0;
										if (flag89)
										{
											i = -1;
											this.m_CharacterCount = 0;
											characterToSubstitute.index = 0;
											characterToSubstitute.unicode = 3U;
											this.m_FirstCharacterOfLine = 0;
											goto IL_43A9;
										}
										WordWrapState ellipsisState3 = this.m_EllipsisInsertionCandidateStack.Pop();
										i = this.RestoreWordWrappingState(ref ellipsisState3, textInfo);
										i--;
										this.m_CharacterCount--;
										characterToSubstitute.index = this.m_CharacterCount;
										characterToSubstitute.unicode = 8230U;
										restoreCount++;
										goto IL_43A9;
									}
									case TextOverflowMode.Truncate:
										i = this.RestoreWordWrappingState(ref this.m_SavedWordWrapState, textInfo);
										characterToSubstitute.index = testedCharacterCount;
										characterToSubstitute.unicode = 3U;
										goto IL_43A9;
									case TextOverflowMode.Linked:
										i = this.RestoreWordWrappingState(ref this.m_SavedWordWrapState, textInfo);
										characterToSubstitute.index = this.m_CharacterCount;
										characterToSubstitute.unicode = 3U;
										goto IL_43A9;
									}
								}
							}
							bool flag90 = isWhiteSpace;
							if (flag90)
							{
								textInfo.textElementInfo[this.m_CharacterCount].isVisible = false;
								LineInfo[] lineInfo = textInfo.lineInfo;
								int lineNumber = this.m_LineNumber;
								this.m_LineVisibleSpaceCount = (lineInfo[lineNumber].spaceCount = lineInfo[lineNumber].spaceCount + 1);
								textInfo.lineInfo[this.m_LineNumber].marginLeft = marginLeft;
								textInfo.lineInfo[this.m_LineNumber].marginRight = marginRight;
								textInfo.spaceCount++;
								bool flag91 = charCode == 160U;
								if (flag91)
								{
									LineInfo[] lineInfo2 = textInfo.lineInfo;
									int lineNumber2 = this.m_LineNumber;
									lineInfo2[lineNumber2].controlCharacterCount = lineInfo2[lineNumber2].controlCharacterCount + 1;
								}
							}
							else
							{
								bool flag92 = charCode == 173U;
								if (flag92)
								{
									textInfo.textElementInfo[this.m_CharacterCount].isVisible = false;
								}
								else
								{
									bool overrideRichTextColors = generationSettings.overrideRichTextColors;
									Color32 vertexColor;
									if (overrideRichTextColors)
									{
										vertexColor = this.m_FontColor32;
									}
									else
									{
										vertexColor = this.m_HtmlColor;
									}
									bool flag93 = this.m_TextElementType == TextElementType.Character;
									if (flag93)
									{
										this.SaveGlyphVertexInfo(padding, stylePadding, vertexColor, generationSettings, textInfo);
									}
									else
									{
										bool flag94 = this.m_TextElementType == TextElementType.Sprite;
										if (flag94)
										{
											this.SaveSpriteVertexInfo(vertexColor, generationSettings, textInfo);
										}
									}
									bool flag95 = isStartOfNewLine;
									if (flag95)
									{
										isStartOfNewLine = false;
										this.m_FirstVisibleCharacterOfLine = this.m_CharacterCount;
									}
									this.m_LineVisibleCharacterCount++;
									this.m_LastVisibleCharacterOfLine = this.m_CharacterCount;
									textInfo.lineInfo[this.m_LineNumber].marginLeft = marginLeft;
									textInfo.lineInfo[this.m_LineNumber].marginRight = marginRight;
								}
							}
						}
						else
						{
							bool flag96 = generationSettings.overflowMode == TextOverflowMode.Linked && (charCode == 10U || charCode == 11U);
							if (flag96)
							{
								float textHeight2 = this.m_MaxAscender - (this.m_MaxLineDescender - this.m_LineOffset) + ((this.m_LineOffset > 0f && !this.m_IsDrivenLineSpacing) ? (this.m_MaxLineAscender - this.m_StartOfLineAscender) : 0f);
								int testedCharacterCount2 = this.m_CharacterCount;
								bool flag97 = textHeight2 > marginHeight + 0.0001f;
								if (flag97)
								{
									bool flag98 = this.m_FirstOverflowCharacterIndex == -1;
									if (flag98)
									{
										this.m_FirstOverflowCharacterIndex = this.m_CharacterCount;
									}
									i = this.RestoreWordWrappingState(ref this.m_SavedLastValidState, textInfo);
									characterToSubstitute.index = testedCharacterCount2;
									characterToSubstitute.unicode = 3U;
									goto IL_43A9;
								}
							}
							bool flag99 = (charCode == 10U || charCode == 11U || charCode == 160U || charCode == 8199U || charCode == 8232U || charCode == 8233U || char.IsSeparator((char)charCode)) && charCode != 173U && charCode != 8203U && charCode != 8288U;
							if (flag99)
							{
								LineInfo[] lineInfo3 = textInfo.lineInfo;
								int lineNumber3 = this.m_LineNumber;
								lineInfo3[lineNumber3].spaceCount = lineInfo3[lineNumber3].spaceCount + 1;
								textInfo.spaceCount++;
							}
							bool flag100 = charCode == 160U;
							if (flag100)
							{
								LineInfo[] lineInfo4 = textInfo.lineInfo;
								int lineNumber4 = this.m_LineNumber;
								lineInfo4[lineNumber4].controlCharacterCount = lineInfo4[lineNumber4].controlCharacterCount + 1;
							}
						}
						bool flag101 = generationSettings.overflowMode == TextOverflowMode.Ellipsis && (!isInjectedCharacter || charCode == 45U);
						if (flag101)
						{
							float fontScale2 = this.m_CurrentFontSize / this.m_Ellipsis.fontAsset.m_FaceInfo.pointSize * this.m_Ellipsis.fontAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
							float scale = fontScale2 * this.m_FontScaleMultiplier * this.m_Ellipsis.character.m_Scale * this.m_Ellipsis.character.m_Glyph.scale;
							float marginLeft2 = this.m_MarginLeft;
							float marginRight2 = this.m_MarginRight;
							bool flag102 = charCode == 10U && this.m_CharacterCount != this.m_FirstCharacterOfLine;
							if (flag102)
							{
								fontScale2 = textInfo.textElementInfo[this.m_CharacterCount - 1].pointSize / this.m_Ellipsis.fontAsset.m_FaceInfo.pointSize * this.m_Ellipsis.fontAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
								scale = fontScale2 * this.m_FontScaleMultiplier * this.m_Ellipsis.character.m_Scale * this.m_Ellipsis.character.m_Glyph.scale;
								marginLeft2 = textInfo.lineInfo[this.m_LineNumber].marginLeft;
								marginRight2 = textInfo.lineInfo[this.m_LineNumber].marginRight;
							}
							float textWidth2 = Mathf.Abs(this.m_XAdvance) + ((!generationSettings.isRightToLeft) ? this.m_Ellipsis.character.m_Glyph.metrics.horizontalAdvance : 0f) * (1f - this.m_CharWidthAdjDelta) * scale;
							float widthOfTextAreaForEllipsis = ((this.m_Width != -1f) ? Mathf.Min(marginWidth + 0.0001f - marginLeft2 - marginRight2, this.m_Width) : (marginWidth + 0.0001f - marginLeft2 - marginRight2));
							bool flag103 = textWidth2 < widthOfTextAreaForEllipsis;
							if (flag103)
							{
								this.SaveWordWrappingState(ref this.m_SavedEllipsisState, i, this.m_CharacterCount, textInfo);
								this.m_EllipsisInsertionCandidateStack.Push(this.m_SavedEllipsisState);
							}
						}
						textInfo.textElementInfo[this.m_CharacterCount].lineNumber = this.m_LineNumber;
						textInfo.textElementInfo[this.m_CharacterCount].pageNumber = this.m_PageNumber;
						bool flag104 = (charCode != 10U && charCode != 11U && charCode != 13U && !isInjectedCharacter) || textInfo.lineInfo[this.m_LineNumber].characterCount == 1;
						if (flag104)
						{
							textInfo.lineInfo[this.m_LineNumber].alignment = this.m_LineJustification;
						}
						bool flag105 = charCode != 8203U;
						if (flag105)
						{
							bool flag106 = charCode == 9U;
							if (flag106)
							{
								float tabSize = this.m_CurrentFontAsset.m_FaceInfo.tabWidth * (float)this.m_CurrentFontAsset.tabMultiple * currentElementScale;
								float tabs = Mathf.Ceil(this.m_XAdvance / tabSize) * tabSize;
								this.m_XAdvance = ((tabs > this.m_XAdvance) ? tabs : (this.m_XAdvance + tabSize));
							}
							else
							{
								bool flag107 = this.m_MonoSpacing != 0f;
								if (flag107)
								{
									bool flag108 = this.m_DuoSpace && (charCode == 46U || charCode == 58U || charCode == 44U);
									float monoAdjustment;
									if (flag108)
									{
										monoAdjustment = this.m_MonoSpacing / 2f - monoAdvance;
									}
									else
									{
										monoAdjustment = this.m_MonoSpacing - monoAdvance;
									}
									this.m_XAdvance += (monoAdjustment + (this.m_CurrentFontAsset.regularStyleSpacing + characterSpacingAdjustment) * currentEmScale + this.m_CSpacing) * (1f - this.m_CharWidthAdjDelta);
									bool flag109 = isWhiteSpace || charCode == 8203U;
									if (flag109)
									{
										this.m_XAdvance += generationSettings.wordSpacing * currentEmScale;
									}
								}
								else
								{
									bool isRightToLeft2 = generationSettings.isRightToLeft;
									if (isRightToLeft2)
									{
										this.m_XAdvance -= (glyphAdjustments.xAdvance * currentElementScale + (this.m_CurrentFontAsset.regularStyleSpacing + characterSpacingAdjustment + boldSpacingAdjustment) * currentEmScale + this.m_CSpacing) * (1f - this.m_CharWidthAdjDelta);
										bool flag110 = isWhiteSpace || charCode == 8203U;
										if (flag110)
										{
											this.m_XAdvance -= generationSettings.wordSpacing * currentEmScale;
										}
									}
									else
									{
										this.m_XAdvance += ((currentGlyphMetrics.horizontalAdvance * this.m_FXScale.x + glyphAdjustments.xAdvance) * currentElementScale + (this.m_CurrentFontAsset.regularStyleSpacing + characterSpacingAdjustment + boldSpacingAdjustment) * currentEmScale + this.m_CSpacing) * (1f - this.m_CharWidthAdjDelta);
										bool flag111 = isWhiteSpace || charCode == 8203U;
										if (flag111)
										{
											this.m_XAdvance += generationSettings.wordSpacing * currentEmScale;
										}
									}
								}
							}
						}
						textInfo.textElementInfo[this.m_CharacterCount].xAdvance = this.m_XAdvance;
						bool flag112 = charCode == 13U;
						if (flag112)
						{
							this.m_XAdvance = 0f + this.m_TagIndent;
						}
						bool flag113 = generationSettings.overflowMode == TextOverflowMode.Page && charCode != 10U && charCode != 11U && charCode != 13U && charCode != 8232U && charCode != 8232U;
						if (flag113)
						{
							bool flag114 = this.m_PageNumber + 1 > textInfo.pageInfo.Length;
							if (flag114)
							{
								TextInfo.Resize<PageInfo>(ref textInfo.pageInfo, this.m_PageNumber + 1, true);
							}
							textInfo.pageInfo[this.m_PageNumber].ascender = this.m_PageAscender;
							textInfo.pageInfo[this.m_PageNumber].descender = ((this.m_MaxDescender < textInfo.pageInfo[this.m_PageNumber].descender) ? this.m_MaxDescender : textInfo.pageInfo[this.m_PageNumber].descender);
							bool isNewPage = this.m_IsNewPage;
							if (isNewPage)
							{
								this.m_IsNewPage = false;
								textInfo.pageInfo[this.m_PageNumber].firstCharacterIndex = this.m_CharacterCount;
							}
							textInfo.pageInfo[this.m_PageNumber].lastCharacterIndex = this.m_CharacterCount;
						}
						bool flag115 = charCode == 10U || charCode == 11U || charCode == 3U || charCode == 8232U || charCode == 8232U || (charCode == 45U && isInjectedCharacter) || this.m_CharacterCount == totalCharacterCount - 1;
						if (flag115)
						{
							float baselineAdjustmentDelta = this.m_MaxLineAscender - this.m_StartOfLineAscender;
							bool flag116 = this.m_LineOffset > 0f && Math.Abs(baselineAdjustmentDelta) > 0.01f && !this.m_IsDrivenLineSpacing && !this.m_IsNewPage;
							if (flag116)
							{
								TextGeneratorUtilities.AdjustLineOffset(this.m_FirstCharacterOfLine, this.m_CharacterCount, baselineAdjustmentDelta, textInfo);
								this.m_MaxDescender -= baselineAdjustmentDelta;
								this.m_LineOffset += baselineAdjustmentDelta;
								bool flag117 = this.m_SavedEllipsisState.lineNumber == this.m_LineNumber;
								if (flag117)
								{
									this.m_SavedEllipsisState = this.m_EllipsisInsertionCandidateStack.Pop();
									this.m_SavedEllipsisState.startOfLineAscender = this.m_SavedEllipsisState.startOfLineAscender + baselineAdjustmentDelta;
									this.m_SavedEllipsisState.lineOffset = this.m_SavedEllipsisState.lineOffset + baselineAdjustmentDelta;
									this.m_EllipsisInsertionCandidateStack.Push(this.m_SavedEllipsisState);
								}
							}
							this.m_IsNewPage = false;
							float lineAscender = this.m_MaxLineAscender - this.m_LineOffset;
							float lineDescender = this.m_MaxLineDescender - this.m_LineOffset;
							this.m_MaxDescender = ((this.m_MaxDescender < lineDescender) ? this.m_MaxDescender : lineDescender);
							bool flag118 = !isMaxVisibleDescenderSet;
							if (flag118)
							{
								maxVisibleDescender = this.m_MaxDescender;
							}
							bool flag119 = generationSettings.useMaxVisibleDescender && (this.m_CharacterCount >= generationSettings.maxVisibleCharacters || this.m_LineNumber >= generationSettings.maxVisibleLines);
							if (flag119)
							{
								isMaxVisibleDescenderSet = true;
							}
							textInfo.lineInfo[this.m_LineNumber].firstCharacterIndex = this.m_FirstCharacterOfLine;
							textInfo.lineInfo[this.m_LineNumber].firstVisibleCharacterIndex = (this.m_FirstVisibleCharacterOfLine = ((this.m_FirstCharacterOfLine > this.m_FirstVisibleCharacterOfLine) ? this.m_FirstCharacterOfLine : this.m_FirstVisibleCharacterOfLine));
							textInfo.lineInfo[this.m_LineNumber].lastCharacterIndex = (this.m_LastCharacterOfLine = this.m_CharacterCount);
							textInfo.lineInfo[this.m_LineNumber].lastVisibleCharacterIndex = (this.m_LastVisibleCharacterOfLine = ((this.m_LastVisibleCharacterOfLine < this.m_FirstVisibleCharacterOfLine) ? this.m_FirstVisibleCharacterOfLine : this.m_LastVisibleCharacterOfLine));
							int firstCharacterIndex = this.m_FirstVisibleCharacterOfLine;
							int lastCharacterIndex = this.m_LastVisibleCharacterOfLine;
							bool flag120 = generationSettings.textWrappingMode == TextWrappingMode.PreserveWhitespace || generationSettings.textWrappingMode == TextWrappingMode.PreserveWhitespaceNoWrap;
							if (flag120)
							{
								bool flag121 = textInfo.textElementInfo[this.m_LastCharacterOfLine].xAdvance != 0f;
								if (flag121)
								{
									firstCharacterIndex = this.m_FirstCharacterOfLine;
									lastCharacterIndex = this.m_LastCharacterOfLine;
								}
							}
							textInfo.lineInfo[this.m_LineNumber].characterCount = textInfo.lineInfo[this.m_LineNumber].lastCharacterIndex - textInfo.lineInfo[this.m_LineNumber].firstCharacterIndex + 1;
							textInfo.lineInfo[this.m_LineNumber].visibleCharacterCount = this.m_LineVisibleCharacterCount;
							textInfo.lineInfo[this.m_LineNumber].lineExtents.min = new Vector2(textInfo.textElementInfo[firstCharacterIndex].bottomLeft.x, lineDescender);
							textInfo.lineInfo[this.m_LineNumber].lineExtents.max = new Vector2(textInfo.textElementInfo[lastCharacterIndex].topRight.x, lineAscender);
							textInfo.lineInfo[this.m_LineNumber].length = (generationSettings.isIMGUI ? textInfo.textElementInfo[lastCharacterIndex].xAdvance : (textInfo.lineInfo[this.m_LineNumber].lineExtents.max.x - padding * currentElementScale));
							textInfo.lineInfo[this.m_LineNumber].width = widthOfTextArea;
							bool flag122 = textInfo.lineInfo[this.m_LineNumber].characterCount == 1;
							if (flag122)
							{
								textInfo.lineInfo[this.m_LineNumber].alignment = this.m_LineJustification;
							}
							float maxAdvanceOffset = ((this.m_CurrentFontAsset.regularStyleSpacing + characterSpacingAdjustment + boldSpacingAdjustment) * currentEmScale + this.m_CSpacing) * (1f - this.m_CharWidthAdjDelta);
							bool isVisible = textInfo.textElementInfo[this.m_LastVisibleCharacterOfLine].isVisible;
							if (isVisible)
							{
								textInfo.lineInfo[this.m_LineNumber].maxAdvance = textInfo.textElementInfo[this.m_LastVisibleCharacterOfLine].xAdvance + (generationSettings.isRightToLeft ? maxAdvanceOffset : (-maxAdvanceOffset));
							}
							else
							{
								textInfo.lineInfo[this.m_LineNumber].maxAdvance = textInfo.textElementInfo[this.m_LastCharacterOfLine].xAdvance + (generationSettings.isRightToLeft ? maxAdvanceOffset : (-maxAdvanceOffset));
							}
							textInfo.lineInfo[this.m_LineNumber].baseline = 0f - this.m_LineOffset;
							textInfo.lineInfo[this.m_LineNumber].ascender = lineAscender;
							textInfo.lineInfo[this.m_LineNumber].descender = lineDescender;
							textInfo.lineInfo[this.m_LineNumber].lineHeight = lineAscender - lineDescender + lineGap * baseScale;
							bool flag123 = charCode == 10U || charCode == 11U || charCode == 45U || charCode == 8232U || charCode == 8233U;
							if (flag123)
							{
								this.SaveWordWrappingState(ref this.m_SavedLineState, i, this.m_CharacterCount, textInfo);
								this.m_LineNumber++;
								isStartOfNewLine = true;
								ignoreNonBreakingSpace = false;
								isFirstWordOfLine = true;
								this.m_FirstCharacterOfLine = this.m_CharacterCount + 1;
								this.m_LineVisibleCharacterCount = 0;
								this.m_LineVisibleSpaceCount = 0;
								bool flag124 = this.m_LineNumber >= textInfo.lineInfo.Length;
								if (flag124)
								{
									TextGeneratorUtilities.ResizeLineExtents(this.m_LineNumber, textInfo);
								}
								float lastVisibleAscender = textInfo.textElementInfo[this.m_CharacterCount].adjustedAscender;
								bool flag125 = this.m_LineHeight == -32767f;
								if (flag125)
								{
									float lineOffsetDelta2 = 0f - this.m_MaxLineDescender + lastVisibleAscender + (lineGap + this.m_LineSpacingDelta) * baseScale + (generationSettings.lineSpacing + ((charCode == 10U || charCode == 8233U) ? generationSettings.paragraphSpacing : 0f)) * currentEmScale;
									this.m_LineOffset += lineOffsetDelta2;
									this.m_IsDrivenLineSpacing = false;
								}
								else
								{
									this.m_LineOffset += this.m_LineHeight + (generationSettings.lineSpacing + ((charCode == 10U || charCode == 8233U) ? generationSettings.paragraphSpacing : 0f)) * currentEmScale;
									this.m_IsDrivenLineSpacing = true;
								}
								this.m_MaxLineAscender = -32767f;
								this.m_MaxLineDescender = 32767f;
								this.m_StartOfLineAscender = lastVisibleAscender;
								this.m_XAdvance = 0f + this.m_TagLineIndent + this.m_TagIndent;
								this.SaveWordWrappingState(ref this.m_SavedWordWrapState, i, this.m_CharacterCount, textInfo);
								this.SaveWordWrappingState(ref this.m_SavedLastValidState, i, this.m_CharacterCount, textInfo);
								this.m_CharacterCount++;
								goto IL_43A9;
							}
							bool flag126 = charCode == 3U;
							if (flag126)
							{
								i = this.m_TextProcessingArray.Length;
							}
						}
						bool isVisible2 = textInfo.textElementInfo[this.m_CharacterCount].isVisible;
						if (isVisible2)
						{
							this.m_MeshExtents.min.x = Mathf.Min(this.m_MeshExtents.min.x, textInfo.textElementInfo[this.m_CharacterCount].bottomLeft.x);
							this.m_MeshExtents.min.y = Mathf.Min(this.m_MeshExtents.min.y, textInfo.textElementInfo[this.m_CharacterCount].bottomLeft.y);
							this.m_MeshExtents.max.x = Mathf.Max(this.m_MeshExtents.max.x, textInfo.textElementInfo[this.m_CharacterCount].topRight.x);
							this.m_MeshExtents.max.y = Mathf.Max(this.m_MeshExtents.max.y, textInfo.textElementInfo[this.m_CharacterCount].topRight.y);
						}
						bool flag127 = (wordWrap != TextWrappingMode.NoWrap && wordWrap != TextWrappingMode.PreserveWhitespaceNoWrap) || generationSettings.overflowMode == TextOverflowMode.Truncate || generationSettings.overflowMode == TextOverflowMode.Ellipsis || generationSettings.overflowMode == TextOverflowMode.Linked;
						if (flag127)
						{
							bool shouldSaveHardLineBreak = false;
							bool shouldSaveSoftLineBreak = false;
							bool flag128 = (isWhiteSpace || charCode == 8203U || (charCode == 45U && (this.m_CharacterCount <= 0 || !char.IsWhiteSpace((char)textInfo.textElementInfo[this.m_CharacterCount - 1].character))) || charCode == 173U) && (!this.m_IsNonBreakingSpace || ignoreNonBreakingSpace) && charCode != 160U && charCode != 8199U && charCode != 8209U && charCode != 8239U && charCode != 8288U;
							if (flag128)
							{
								bool flag129 = charCode != 45U || this.m_CharacterCount <= 0 || !char.IsWhiteSpace((char)textInfo.textElementInfo[this.m_CharacterCount - 1].character);
								if (flag129)
								{
									isFirstWordOfLine = false;
									shouldSaveHardLineBreak = true;
									this.m_SavedSoftLineBreakState.previousWordBreak = -1;
								}
							}
							else
							{
								bool flag130 = !this.m_IsNonBreakingSpace && ((TextGeneratorUtilities.IsHangul(charCode) && !textSettings.lineBreakingRules.useModernHangulLineBreakingRules) || TextGeneratorUtilities.IsCJK(charCode));
								if (flag130)
								{
									bool isCurrentLeadingCharacter = textSettings.lineBreakingRules.leadingCharactersLookup.Contains(charCode);
									bool isNextFollowingCharacter = this.m_CharacterCount < totalCharacterCount - 1 && textSettings.lineBreakingRules.followingCharactersLookup.Contains(textInfo.textElementInfo[this.m_CharacterCount + 1].character);
									bool flag131 = !isCurrentLeadingCharacter;
									if (flag131)
									{
										bool flag132 = !isNextFollowingCharacter;
										if (flag132)
										{
											isFirstWordOfLine = false;
											shouldSaveHardLineBreak = true;
										}
										bool flag133 = isFirstWordOfLine;
										if (flag133)
										{
											bool flag134 = isWhiteSpace;
											if (flag134)
											{
												shouldSaveSoftLineBreak = true;
											}
											shouldSaveHardLineBreak = true;
										}
									}
									else
									{
										bool flag135 = isFirstWordOfLine && isFirstCharacterOfLine;
										if (flag135)
										{
											bool flag136 = isWhiteSpace;
											if (flag136)
											{
												shouldSaveSoftLineBreak = true;
											}
											shouldSaveHardLineBreak = true;
										}
									}
								}
								else
								{
									bool flag137 = !this.m_IsNonBreakingSpace && this.m_CharacterCount + 1 < totalCharacterCount && TextGeneratorUtilities.IsCJK(textInfo.textElementInfo[this.m_CharacterCount + 1].character);
									if (flag137)
									{
										shouldSaveHardLineBreak = true;
									}
									else
									{
										bool flag138 = isFirstWordOfLine;
										if (flag138)
										{
											bool flag139 = (isWhiteSpace && charCode != 160U) || (charCode == 173U && !isSoftHyphenIgnored);
											if (flag139)
											{
												shouldSaveSoftLineBreak = true;
											}
											shouldSaveHardLineBreak = true;
										}
									}
								}
							}
							bool flag140 = shouldSaveHardLineBreak;
							if (flag140)
							{
								this.SaveWordWrappingState(ref this.m_SavedWordWrapState, i, this.m_CharacterCount, textInfo);
							}
							bool flag141 = shouldSaveSoftLineBreak;
							if (flag141)
							{
								this.SaveWordWrappingState(ref this.m_SavedSoftLineBreakState, i, this.m_CharacterCount, textInfo);
							}
						}
						this.SaveWordWrappingState(ref this.m_SavedLastValidState, i, this.m_CharacterCount, textInfo);
						this.m_CharacterCount++;
					}
				}
				IL_43A9:
				i++;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0001A4E0 File Offset: 0x000186E0
		private void InsertNewLine(int i, float baseScale, float currentElementScale, float currentEmScale, float boldSpacingAdjustment, float characterSpacingAdjustment, float width, float lineGap, ref bool isMaxVisibleDescenderSet, ref float maxVisibleDescender, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			float baselineAdjustmentDelta = this.m_MaxLineAscender - this.m_StartOfLineAscender;
			bool flag = this.m_LineOffset > 0f && Math.Abs(baselineAdjustmentDelta) > 0.01f && !this.m_IsDrivenLineSpacing && !this.m_IsNewPage;
			if (flag)
			{
				TextGeneratorUtilities.AdjustLineOffset(this.m_FirstCharacterOfLine, this.m_CharacterCount, baselineAdjustmentDelta, textInfo);
				this.m_MaxDescender -= baselineAdjustmentDelta;
				this.m_LineOffset += baselineAdjustmentDelta;
			}
			float lineAscender = this.m_MaxLineAscender - this.m_LineOffset;
			float lineDescender = this.m_MaxLineDescender - this.m_LineOffset;
			this.m_MaxDescender = ((this.m_MaxDescender < lineDescender) ? this.m_MaxDescender : lineDescender);
			bool flag2 = !isMaxVisibleDescenderSet;
			if (flag2)
			{
				maxVisibleDescender = this.m_MaxDescender;
			}
			bool flag3 = generationSettings.useMaxVisibleDescender && (this.m_CharacterCount >= generationSettings.maxVisibleCharacters || this.m_LineNumber >= generationSettings.maxVisibleLines);
			if (flag3)
			{
				isMaxVisibleDescenderSet = true;
			}
			textInfo.lineInfo[this.m_LineNumber].firstCharacterIndex = this.m_FirstCharacterOfLine;
			textInfo.lineInfo[this.m_LineNumber].firstVisibleCharacterIndex = (this.m_FirstVisibleCharacterOfLine = ((this.m_FirstCharacterOfLine > this.m_FirstVisibleCharacterOfLine) ? this.m_FirstCharacterOfLine : this.m_FirstVisibleCharacterOfLine));
			textInfo.lineInfo[this.m_LineNumber].lastCharacterIndex = (this.m_LastCharacterOfLine = ((this.m_CharacterCount - 1 > 0) ? (this.m_CharacterCount - 1) : 0));
			textInfo.lineInfo[this.m_LineNumber].lastVisibleCharacterIndex = (this.m_LastVisibleCharacterOfLine = ((this.m_LastVisibleCharacterOfLine < this.m_FirstVisibleCharacterOfLine) ? this.m_FirstVisibleCharacterOfLine : this.m_LastVisibleCharacterOfLine));
			textInfo.lineInfo[this.m_LineNumber].characterCount = textInfo.lineInfo[this.m_LineNumber].lastCharacterIndex - textInfo.lineInfo[this.m_LineNumber].firstCharacterIndex + 1;
			textInfo.lineInfo[this.m_LineNumber].visibleCharacterCount = this.m_LineVisibleCharacterCount;
			textInfo.lineInfo[this.m_LineNumber].lineExtents.min = new Vector2(textInfo.textElementInfo[this.m_FirstVisibleCharacterOfLine].bottomLeft.x, lineDescender);
			textInfo.lineInfo[this.m_LineNumber].lineExtents.max = new Vector2(textInfo.textElementInfo[this.m_LastVisibleCharacterOfLine].topRight.x, lineAscender);
			textInfo.lineInfo[this.m_LineNumber].length = (generationSettings.isIMGUI ? textInfo.textElementInfo[this.m_LastVisibleCharacterOfLine].xAdvance : textInfo.lineInfo[this.m_LineNumber].lineExtents.max.x);
			textInfo.lineInfo[this.m_LineNumber].width = width;
			float glyphAdjustment = textInfo.textElementInfo[this.m_LastVisibleCharacterOfLine].adjustedHorizontalAdvance;
			float maxAdvanceOffset = (glyphAdjustment * currentElementScale + (this.m_CurrentFontAsset.regularStyleSpacing + characterSpacingAdjustment + boldSpacingAdjustment) * currentEmScale + this.m_CSpacing) * (1f - generationSettings.charWidthMaxAdj);
			float adjustedHorizontalAdvance = (textInfo.lineInfo[this.m_LineNumber].maxAdvance = textInfo.textElementInfo[this.m_LastVisibleCharacterOfLine].xAdvance + (generationSettings.isRightToLeft ? maxAdvanceOffset : (-maxAdvanceOffset)));
			textInfo.textElementInfo[this.m_LastVisibleCharacterOfLine].xAdvance = adjustedHorizontalAdvance;
			textInfo.lineInfo[this.m_LineNumber].baseline = 0f - this.m_LineOffset;
			textInfo.lineInfo[this.m_LineNumber].ascender = lineAscender;
			textInfo.lineInfo[this.m_LineNumber].descender = lineDescender;
			textInfo.lineInfo[this.m_LineNumber].lineHeight = lineAscender - lineDescender + lineGap * baseScale;
			this.m_FirstCharacterOfLine = this.m_CharacterCount;
			this.m_LineVisibleCharacterCount = 0;
			this.m_LineVisibleSpaceCount = 0;
			this.SaveWordWrappingState(ref this.m_SavedLineState, i, this.m_CharacterCount - 1, textInfo);
			this.m_LineNumber++;
			bool flag4 = this.m_LineNumber >= textInfo.lineInfo.Length;
			if (flag4)
			{
				TextGeneratorUtilities.ResizeLineExtents(this.m_LineNumber, textInfo);
			}
			bool flag5 = this.m_LineHeight == -32767f;
			if (flag5)
			{
				float ascender = textInfo.textElementInfo[this.m_CharacterCount].adjustedAscender;
				float lineOffsetDelta = 0f - this.m_MaxLineDescender + ascender + (lineGap + this.m_LineSpacingDelta) * baseScale + generationSettings.lineSpacing * currentEmScale;
				this.m_LineOffset += lineOffsetDelta;
				this.m_StartOfLineAscender = ascender;
			}
			else
			{
				this.m_LineOffset += this.m_LineHeight + generationSettings.lineSpacing * currentEmScale;
			}
			this.m_MaxLineAscender = -32767f;
			this.m_MaxLineDescender = 32767f;
			this.m_XAdvance = 0f + this.m_TagIndent;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0001AA44 File Offset: 0x00018C44
		public Vector2 GetPreferredValues(TextGenerationSettings settings, TextInfo textInfo)
		{
			bool flag = settings.fontAsset == null || settings.fontAsset.characterLookupTable == null;
			Vector2 vector;
			if (flag)
			{
				Debug.LogWarning("Can't Generate Mesh, No Font Asset has been assigned.");
				vector = Vector2.zero;
			}
			else
			{
				this.Prepare(settings, textInfo);
				vector = this.GetPreferredValuesInternal(settings, textInfo);
			}
			return vector;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0001AAA0 File Offset: 0x00018CA0
		private Vector2 GetPreferredValuesInternal(TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			bool flag = generationSettings.textSettings == null;
			Vector2 vector;
			if (flag)
			{
				vector = Vector2.zero;
			}
			else
			{
				float fontSize = (generationSettings.autoSize ? generationSettings.fontSizeMax : this.m_FontSize);
				this.m_MinFontSize = generationSettings.fontSizeMin;
				this.m_MaxFontSize = generationSettings.fontSizeMax;
				this.m_CharWidthAdjDelta = 0f;
				Vector2 margin = new Vector2((this.m_MarginWidth != 0f) ? this.m_MarginWidth : 32767f, (this.m_MarginHeight != 0f) ? this.m_MarginHeight : 32767f);
				this.m_AutoSizeIterationCount = 0;
				vector = this.CalculatePreferredValues(ref fontSize, margin, generationSettings.autoSize, generationSettings, textInfo);
			}
			return vector;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0001AB5C File Offset: 0x00018D5C
		protected virtual Vector2 CalculatePreferredValues(ref float fontSize, Vector2 marginSize, bool isTextAutoSizingEnabled, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			bool flag = generationSettings.fontAsset == null || generationSettings.fontAsset.characterLookupTable == null;
			Vector2 vector;
			if (flag)
			{
				Debug.LogWarning("Can't Generate Mesh! No Font Asset has been assigned.");
				vector = Vector2.zero;
			}
			else
			{
				bool flag2 = this.m_TextProcessingArray == null || this.m_TextProcessingArray.Length == 0 || this.m_TextProcessingArray[0].unicode == 0U;
				if (flag2)
				{
					vector = Vector2.zero;
				}
				else
				{
					this.m_CurrentFontAsset = generationSettings.fontAsset;
					this.m_CurrentMaterial = generationSettings.material;
					this.m_CurrentMaterialIndex = 0;
					this.m_MaterialReferenceStack.SetDefault(new MaterialReference(0, this.m_CurrentFontAsset, null, this.m_CurrentMaterial, this.m_Padding));
					int totalCharacterCount = this.m_TotalCharacterCount;
					bool flag3 = this.m_InternalTextElementInfo == null || totalCharacterCount > this.m_InternalTextElementInfo.Length;
					if (flag3)
					{
						this.m_InternalTextElementInfo = new TextElementInfo[(totalCharacterCount > 1024) ? (totalCharacterCount + 256) : Mathf.NextPowerOfTwo(totalCharacterCount)];
					}
					float baseScale = fontSize / generationSettings.fontAsset.faceInfo.pointSize * generationSettings.fontAsset.faceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
					float currentElementScale = baseScale;
					float currentEmScale = fontSize * 0.01f * (generationSettings.isOrthographic ? 1f : 0.1f);
					this.m_FontScaleMultiplier = 1f;
					this.m_CurrentFontSize = fontSize;
					this.m_SizeStack.SetDefault(this.m_CurrentFontSize);
					this.m_FontStyleInternal = generationSettings.fontStyle;
					this.m_FontWeightInternal = (((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? TextFontWeight.Bold : generationSettings.fontWeight);
					this.m_FontWeightStack.SetDefault(this.m_FontWeightInternal);
					this.m_FontStyleStack.Clear();
					this.m_LineJustification = generationSettings.textAlignment;
					this.m_LineJustificationStack.SetDefault(this.m_LineJustification);
					this.m_BaselineOffset = 0f;
					this.m_BaselineOffsetStack.Clear();
					this.m_FXScale = Vector3.one;
					this.m_LineOffset = 0f;
					this.m_LineHeight = -32767f;
					float lineGap = this.m_CurrentFontAsset.faceInfo.lineHeight - (this.m_CurrentFontAsset.faceInfo.ascentLine - this.m_CurrentFontAsset.faceInfo.descentLine);
					this.m_CSpacing = 0f;
					this.m_MonoSpacing = 0f;
					this.m_XAdvance = 0f;
					this.m_TagLineIndent = 0f;
					this.m_TagIndent = 0f;
					this.m_IndentStack.SetDefault(0f);
					this.m_TagNoParsing = false;
					this.m_CharacterCount = 0;
					this.m_FirstCharacterOfLine = 0;
					this.m_MaxLineAscender = -32767f;
					this.m_MaxLineDescender = 32767f;
					this.m_LineNumber = 0;
					this.m_StartOfLineAscender = 0f;
					this.m_IsDrivenLineSpacing = false;
					this.m_LastBaseGlyphIndex = int.MinValue;
					bool kerning = generationSettings.fontFeatures.Contains(OTL_FeatureTag.kern);
					bool markToBase = generationSettings.fontFeatures.Contains(OTL_FeatureTag.mark);
					bool markToMark = generationSettings.fontFeatures.Contains(OTL_FeatureTag.mkmk);
					TextSettings textSettings = generationSettings.textSettings;
					float marginWidth = marginSize.x;
					float marginHeight = marginSize.y;
					this.m_MarginLeft = 0f;
					this.m_MarginRight = 0f;
					this.m_Width = -1f;
					float widthOfTextArea = marginWidth + 0.0001f - this.m_MarginLeft - this.m_MarginRight;
					TextWrappingMode textWrapMode = generationSettings.textWrappingMode;
					float renderedWidth = 0f;
					float renderedHeight = 0f;
					this.m_IsCalculatingPreferredValues = true;
					this.m_MaxCapHeight = 0f;
					this.m_MaxAscender = 0f;
					this.m_MaxDescender = 0f;
					bool isMaxVisibleDescenderSet = false;
					bool isFirstWordOfLine = true;
					this.m_IsNonBreakingSpace = false;
					bool ignoreNonBreakingSpace = false;
					CharacterSubstitution characterToSubstitute = new CharacterSubstitution(-1, 0U);
					bool isSoftHyphenIgnored = false;
					WordWrapState internalWordWrapState = default(WordWrapState);
					WordWrapState internalLineState = default(WordWrapState);
					WordWrapState internalSoftLineBreak = default(WordWrapState);
					this.m_IsTextTruncated = false;
					this.m_AutoSizeIterationCount++;
					int i = 0;
					while (i < this.m_TextProcessingArray.Length && this.m_TextProcessingArray[i].unicode > 0U)
					{
						uint charCode = this.m_TextProcessingArray[i].unicode;
						bool flag4 = charCode == 26U;
						if (!flag4)
						{
							bool flag5 = generationSettings.richText && charCode == 60U;
							if (flag5)
							{
								this.m_isTextLayoutPhase = true;
								this.m_TextElementType = TextElementType.Character;
								int endTagIndex;
								bool isThreadSuccess;
								bool flag6 = this.ValidateHtmlTag(this.m_TextProcessingArray, i + 1, out endTagIndex, generationSettings, textInfo, out isThreadSuccess);
								if (flag6)
								{
									i = endTagIndex;
									bool flag7 = this.m_TextElementType == TextElementType.Character;
									if (flag7)
									{
										goto IL_224B;
									}
								}
							}
							else
							{
								this.m_TextElementType = textInfo.textElementInfo[this.m_CharacterCount].elementType;
								this.m_CurrentMaterialIndex = textInfo.textElementInfo[this.m_CharacterCount].materialReferenceIndex;
								this.m_CurrentFontAsset = textInfo.textElementInfo[this.m_CharacterCount].fontAsset;
							}
							int prevMaterialIndex = this.m_CurrentMaterialIndex;
							bool isUsingAltTypeface = textInfo.textElementInfo[this.m_CharacterCount].isUsingAlternateTypeface;
							this.m_isTextLayoutPhase = false;
							bool isInjectedCharacter = false;
							bool flag8 = characterToSubstitute.index == this.m_CharacterCount;
							if (flag8)
							{
								charCode = characterToSubstitute.unicode;
								this.m_TextElementType = TextElementType.Character;
								isInjectedCharacter = true;
								uint num = charCode;
								uint num2 = num;
								if (num2 != 3U)
								{
									if (num2 != 45U)
									{
										if (num2 == 8230U)
										{
											this.m_InternalTextElementInfo[this.m_CharacterCount].textElement = this.m_Ellipsis.character;
											this.m_InternalTextElementInfo[this.m_CharacterCount].elementType = TextElementType.Character;
											this.m_InternalTextElementInfo[this.m_CharacterCount].fontAsset = this.m_Ellipsis.fontAsset;
											this.m_InternalTextElementInfo[this.m_CharacterCount].material = this.m_Ellipsis.material;
											this.m_InternalTextElementInfo[this.m_CharacterCount].materialReferenceIndex = this.m_Ellipsis.materialIndex;
											this.m_IsTextTruncated = true;
											characterToSubstitute.index = this.m_CharacterCount + 1;
											characterToSubstitute.unicode = 3U;
										}
									}
								}
								else
								{
									this.m_InternalTextElementInfo[this.m_CharacterCount].textElement = this.m_CurrentFontAsset.characterLookupTable[3U];
									this.m_IsTextTruncated = true;
								}
							}
							bool flag9 = this.m_CharacterCount < generationSettings.firstVisibleCharacter && charCode != 3U;
							if (flag9)
							{
								this.m_InternalTextElementInfo[this.m_CharacterCount].isVisible = false;
								this.m_InternalTextElementInfo[this.m_CharacterCount].character = 8203U;
								this.m_InternalTextElementInfo[this.m_CharacterCount].lineNumber = 0;
								this.m_CharacterCount++;
							}
							else
							{
								float smallCapsMultiplier = 1f;
								bool flag10 = this.m_TextElementType == TextElementType.Character;
								if (flag10)
								{
									bool flag11 = (this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase;
									if (flag11)
									{
										bool flag12 = char.IsLower((char)charCode);
										if (flag12)
										{
											charCode = (uint)char.ToUpper((char)charCode);
										}
									}
									else
									{
										bool flag13 = (this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase;
										if (flag13)
										{
											bool flag14 = char.IsUpper((char)charCode);
											if (flag14)
											{
												charCode = (uint)char.ToLower((char)charCode);
											}
										}
										else
										{
											bool flag15 = (this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps;
											if (flag15)
											{
												bool flag16 = char.IsLower((char)charCode);
												if (flag16)
												{
													smallCapsMultiplier = 0.8f;
													charCode = (uint)char.ToUpper((char)charCode);
												}
											}
										}
									}
								}
								float baselineOffset = 0f;
								float elementAscentLine = 0f;
								float elementDescentLine = 0f;
								bool flag17 = this.m_TextElementType == TextElementType.Sprite;
								if (flag17)
								{
									SpriteCharacter sprite = (SpriteCharacter)textInfo.textElementInfo[this.m_CharacterCount].textElement;
									this.m_CurrentSpriteAsset = sprite.textAsset as SpriteAsset;
									this.m_SpriteIndex = (int)sprite.glyphIndex;
									bool flag18 = sprite == null;
									if (flag18)
									{
										goto IL_224B;
									}
									bool flag19 = charCode == 60U;
									if (flag19)
									{
										charCode = (uint)(57344 + this.m_SpriteIndex);
									}
									bool flag20 = this.m_CurrentSpriteAsset.faceInfo.pointSize > 0f;
									if (flag20)
									{
										float spriteScale = this.m_CurrentFontSize / this.m_CurrentSpriteAsset.faceInfo.pointSize * this.m_CurrentSpriteAsset.faceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
										currentElementScale = sprite.scale * sprite.glyph.scale * spriteScale;
										elementAscentLine = this.m_CurrentSpriteAsset.faceInfo.ascentLine;
										elementDescentLine = this.m_CurrentSpriteAsset.faceInfo.descentLine;
									}
									else
									{
										float spriteScale2 = this.m_CurrentFontSize / this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
										currentElementScale = this.m_CurrentFontAsset.faceInfo.ascentLine / sprite.glyph.metrics.height * sprite.scale * sprite.glyph.scale * spriteScale2;
										float scaleDelta = spriteScale2 / currentElementScale;
										elementAscentLine = this.m_CurrentFontAsset.faceInfo.ascentLine * scaleDelta;
										elementDescentLine = this.m_CurrentFontAsset.faceInfo.descentLine * scaleDelta;
									}
									this.m_CachedTextElement = sprite;
									this.m_InternalTextElementInfo[this.m_CharacterCount].elementType = TextElementType.Sprite;
									this.m_InternalTextElementInfo[this.m_CharacterCount].scale = currentElementScale;
									this.m_CurrentMaterialIndex = prevMaterialIndex;
								}
								else
								{
									bool flag21 = this.m_TextElementType == TextElementType.Character;
									if (flag21)
									{
										this.m_CachedTextElement = textInfo.textElementInfo[this.m_CharacterCount].textElement;
										bool flag22 = this.m_CachedTextElement == null;
										if (flag22)
										{
											goto IL_224B;
										}
										this.m_CurrentFontAsset = textInfo.textElementInfo[this.m_CharacterCount].fontAsset;
										this.m_CurrentMaterial = textInfo.textElementInfo[this.m_CharacterCount].material;
										this.m_CurrentMaterialIndex = textInfo.textElementInfo[this.m_CharacterCount].materialReferenceIndex;
										bool flag23 = isInjectedCharacter && this.m_TextProcessingArray[i].unicode == 10U && this.m_CharacterCount != this.m_FirstCharacterOfLine;
										float adjustedScale;
										if (flag23)
										{
											adjustedScale = textInfo.textElementInfo[this.m_CharacterCount - 1].pointSize * smallCapsMultiplier / this.m_CurrentFontAsset.m_FaceInfo.pointSize * this.m_CurrentFontAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
										}
										else
										{
											adjustedScale = this.m_CurrentFontSize * smallCapsMultiplier / this.m_CurrentFontAsset.m_FaceInfo.pointSize * this.m_CurrentFontAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
										}
										bool flag24 = isInjectedCharacter && charCode == 8230U;
										if (flag24)
										{
											elementAscentLine = 0f;
											elementDescentLine = 0f;
										}
										else
										{
											elementAscentLine = this.m_CurrentFontAsset.m_FaceInfo.ascentLine;
											elementDescentLine = this.m_CurrentFontAsset.m_FaceInfo.descentLine;
										}
										currentElementScale = adjustedScale * this.m_FontScaleMultiplier * this.m_CachedTextElement.scale;
										this.m_InternalTextElementInfo[this.m_CharacterCount].elementType = TextElementType.Character;
									}
								}
								float currentElementUnmodifiedScale = currentElementScale;
								bool flag25 = charCode == 173U || charCode == 3U;
								if (flag25)
								{
									currentElementScale = 0f;
								}
								this.m_InternalTextElementInfo[this.m_CharacterCount].character = (uint)((ushort)charCode);
								this.m_InternalTextElementInfo[this.m_CharacterCount].style = this.m_FontStyleInternal;
								bool flag26 = this.m_FontWeightInternal == TextFontWeight.Bold;
								if (flag26)
								{
									TextElementInfo[] internalTextElementInfo = this.m_InternalTextElementInfo;
									int characterCount = this.m_CharacterCount;
									internalTextElementInfo[characterCount].style = internalTextElementInfo[characterCount].style | FontStyles.Bold;
								}
								Glyph altGlyph = textInfo.textElementInfo[this.m_CharacterCount].alternativeGlyph;
								GlyphMetrics currentGlyphMetrics = ((altGlyph == null) ? this.m_CachedTextElement.m_Glyph.metrics : altGlyph.metrics);
								bool isWhiteSpace = charCode <= 65535U && char.IsWhiteSpace((char)charCode);
								GlyphValueRecord glyphAdjustments = default(GlyphValueRecord);
								float characterSpacingAdjustment = generationSettings.characterSpacing;
								bool flag27 = kerning && this.m_TextElementType == TextElementType.Character;
								if (flag27)
								{
									uint baseGlyphIndex = this.m_CachedTextElement.m_GlyphIndex;
									bool flag28 = this.m_CharacterCount < totalCharacterCount - 1 && textInfo.textElementInfo[this.m_CharacterCount + 1].elementType == TextElementType.Character;
									GlyphPairAdjustmentRecord adjustmentPair;
									if (flag28)
									{
										uint nextGlyphIndex = textInfo.textElementInfo[this.m_CharacterCount + 1].textElement.m_GlyphIndex;
										uint key = (nextGlyphIndex << 16) | baseGlyphIndex;
										bool flag29 = this.m_CurrentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(key, out adjustmentPair);
										if (flag29)
										{
											glyphAdjustments = adjustmentPair.firstAdjustmentRecord.glyphValueRecord;
											characterSpacingAdjustment = (((adjustmentPair.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : characterSpacingAdjustment);
										}
									}
									bool flag30 = this.m_CharacterCount >= 1;
									if (flag30)
									{
										uint previousGlyphIndex = textInfo.textElementInfo[this.m_CharacterCount - 1].textElement.m_GlyphIndex;
										uint key2 = (baseGlyphIndex << 16) | previousGlyphIndex;
										bool flag31 = textInfo.textElementInfo[this.m_CharacterCount - 1].elementType == TextElementType.Character && this.m_CurrentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(key2, out adjustmentPair);
										if (flag31)
										{
											glyphAdjustments += adjustmentPair.secondAdjustmentRecord.glyphValueRecord;
											characterSpacingAdjustment = (((adjustmentPair.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : characterSpacingAdjustment);
										}
									}
									this.m_InternalTextElementInfo[this.m_CharacterCount].adjustedHorizontalAdvance = glyphAdjustments.xAdvance;
								}
								bool isBaseGlyph = TextGeneratorUtilities.IsBaseGlyph(charCode);
								bool flag32 = isBaseGlyph;
								if (flag32)
								{
									this.m_LastBaseGlyphIndex = this.m_CharacterCount;
								}
								bool flag33 = this.m_CharacterCount > 0 && !isBaseGlyph;
								if (flag33)
								{
									bool flag34 = this.m_LastBaseGlyphIndex != int.MinValue && this.m_LastBaseGlyphIndex == this.m_CharacterCount - 1;
									if (flag34)
									{
										Glyph baseGlyph = textInfo.textElementInfo[this.m_LastBaseGlyphIndex].textElement.glyph;
										uint baseGlyphIndex2 = baseGlyph.index;
										uint markGlyphIndex = this.m_CachedTextElement.glyphIndex;
										uint key3 = (markGlyphIndex << 16) | baseGlyphIndex2;
										MarkToBaseAdjustmentRecord glyphAdjustmentRecord;
										bool flag35 = this.m_CurrentFontAsset.fontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.TryGetValue(key3, out glyphAdjustmentRecord);
										if (flag35)
										{
											float advanceOffset = (this.m_InternalTextElementInfo[this.m_LastBaseGlyphIndex].origin - this.m_XAdvance) / currentElementScale;
											glyphAdjustments.xPlacement = advanceOffset + glyphAdjustmentRecord.baseGlyphAnchorPoint.xCoordinate - glyphAdjustmentRecord.markPositionAdjustment.xPositionAdjustment;
											glyphAdjustments.yPlacement = glyphAdjustmentRecord.baseGlyphAnchorPoint.yCoordinate - glyphAdjustmentRecord.markPositionAdjustment.yPositionAdjustment;
											characterSpacingAdjustment = 0f;
										}
									}
									else
									{
										bool wasLookupApplied = false;
										int characterLookupIndex = this.m_CharacterCount - 1;
										while (characterLookupIndex >= 0 && characterLookupIndex != this.m_LastBaseGlyphIndex)
										{
											Glyph baseMarkGlyph = textInfo.textElementInfo[characterLookupIndex].textElement.glyph;
											uint baseGlyphIndex3 = baseMarkGlyph.index;
											uint combiningMarkGlyphIndex = this.m_CachedTextElement.glyphIndex;
											uint key4 = (combiningMarkGlyphIndex << 16) | baseGlyphIndex3;
											MarkToMarkAdjustmentRecord glyphAdjustmentRecord2;
											bool flag36 = this.m_CurrentFontAsset.fontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.TryGetValue(key4, out glyphAdjustmentRecord2);
											if (flag36)
											{
												float baseMarkOrigin = (textInfo.textElementInfo[characterLookupIndex].origin - this.m_XAdvance) / currentElementScale;
												float currentBaseline = baselineOffset - this.m_LineOffset + this.m_BaselineOffset;
												float baseMarkBaseline = (this.m_InternalTextElementInfo[characterLookupIndex].baseLine - currentBaseline) / currentElementScale;
												glyphAdjustments.xPlacement = baseMarkOrigin + glyphAdjustmentRecord2.baseMarkGlyphAnchorPoint.xCoordinate - glyphAdjustmentRecord2.combiningMarkPositionAdjustment.xPositionAdjustment;
												glyphAdjustments.yPlacement = baseMarkBaseline + glyphAdjustmentRecord2.baseMarkGlyphAnchorPoint.yCoordinate - glyphAdjustmentRecord2.combiningMarkPositionAdjustment.yPositionAdjustment;
												characterSpacingAdjustment = 0f;
												wasLookupApplied = true;
												break;
											}
											characterLookupIndex--;
										}
										bool flag37 = this.m_LastBaseGlyphIndex != int.MinValue && !wasLookupApplied;
										if (flag37)
										{
											Glyph baseGlyph2 = textInfo.textElementInfo[this.m_LastBaseGlyphIndex].textElement.glyph;
											uint baseGlyphIndex4 = baseGlyph2.index;
											uint markGlyphIndex2 = this.m_CachedTextElement.glyphIndex;
											uint key5 = (markGlyphIndex2 << 16) | baseGlyphIndex4;
											MarkToBaseAdjustmentRecord glyphAdjustmentRecord3;
											bool flag38 = this.m_CurrentFontAsset.fontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.TryGetValue(key5, out glyphAdjustmentRecord3);
											if (flag38)
											{
												float advanceOffset2 = (this.m_InternalTextElementInfo[this.m_LastBaseGlyphIndex].origin - this.m_XAdvance) / currentElementScale;
												glyphAdjustments.xPlacement = advanceOffset2 + glyphAdjustmentRecord3.baseGlyphAnchorPoint.xCoordinate - glyphAdjustmentRecord3.markPositionAdjustment.xPositionAdjustment;
												glyphAdjustments.yPlacement = glyphAdjustmentRecord3.baseGlyphAnchorPoint.yCoordinate - glyphAdjustmentRecord3.markPositionAdjustment.yPositionAdjustment;
												characterSpacingAdjustment = 0f;
											}
										}
									}
								}
								elementAscentLine += glyphAdjustments.yPlacement;
								elementDescentLine += glyphAdjustments.yPlacement;
								float monoAdvance = 0f;
								bool flag39 = this.m_MonoSpacing != 0f && charCode != 8203U;
								if (flag39)
								{
									monoAdvance = (this.m_MonoSpacing / 2f - (this.m_CachedTextElement.glyph.metrics.width / 2f + this.m_CachedTextElement.glyph.metrics.horizontalBearingX) * currentElementScale) * (1f - this.m_CharWidthAdjDelta);
									this.m_XAdvance += monoAdvance;
								}
								float boldSpacingAdjustment = 0f;
								bool flag40 = this.m_TextElementType == TextElementType.Character && !isUsingAltTypeface && (this.m_InternalTextElementInfo[this.m_CharacterCount].style & FontStyles.Bold) == FontStyles.Bold;
								if (flag40)
								{
									boldSpacingAdjustment = this.m_CurrentFontAsset.boldStyleSpacing;
								}
								this.m_InternalTextElementInfo[this.m_CharacterCount].origin = this.m_XAdvance + glyphAdjustments.xPlacement * currentElementScale;
								this.m_InternalTextElementInfo[this.m_CharacterCount].baseLine = baselineOffset - this.m_LineOffset + this.m_BaselineOffset + glyphAdjustments.yPlacement * currentElementScale;
								float elementAscender = ((this.m_TextElementType == TextElementType.Character) ? (elementAscentLine * currentElementScale / smallCapsMultiplier + this.m_BaselineOffset) : (elementAscentLine * currentElementScale + this.m_BaselineOffset));
								float elementDescender = ((this.m_TextElementType == TextElementType.Character) ? (elementDescentLine * currentElementScale / smallCapsMultiplier + this.m_BaselineOffset) : (elementDescentLine * currentElementScale + this.m_BaselineOffset));
								float adjustedAscender = elementAscender;
								float adjustedDescender = elementDescender;
								bool isFirstCharacterOfLine = this.m_CharacterCount == this.m_FirstCharacterOfLine;
								bool flag41 = isFirstCharacterOfLine || !isWhiteSpace;
								if (flag41)
								{
									bool flag42 = this.m_BaselineOffset != 0f;
									if (flag42)
									{
										adjustedAscender = Mathf.Max((elementAscender - this.m_BaselineOffset) / this.m_FontScaleMultiplier, adjustedAscender);
										adjustedDescender = Mathf.Min((elementDescender - this.m_BaselineOffset) / this.m_FontScaleMultiplier, adjustedDescender);
									}
									this.m_MaxLineAscender = Mathf.Max(adjustedAscender, this.m_MaxLineAscender);
									this.m_MaxLineDescender = Mathf.Min(adjustedDescender, this.m_MaxLineDescender);
								}
								bool flag43 = isFirstCharacterOfLine || !isWhiteSpace;
								if (flag43)
								{
									this.m_InternalTextElementInfo[this.m_CharacterCount].adjustedAscender = adjustedAscender;
									this.m_InternalTextElementInfo[this.m_CharacterCount].adjustedDescender = adjustedDescender;
									this.m_InternalTextElementInfo[this.m_CharacterCount].ascender = elementAscender - this.m_LineOffset;
									this.m_MaxDescender = (this.m_InternalTextElementInfo[this.m_CharacterCount].descender = elementDescender - this.m_LineOffset);
								}
								else
								{
									this.m_InternalTextElementInfo[this.m_CharacterCount].adjustedAscender = this.m_MaxLineAscender;
									this.m_InternalTextElementInfo[this.m_CharacterCount].adjustedDescender = this.m_MaxLineDescender;
									this.m_InternalTextElementInfo[this.m_CharacterCount].ascender = this.m_MaxLineAscender - this.m_LineOffset;
									this.m_MaxDescender = (this.m_InternalTextElementInfo[this.m_CharacterCount].descender = this.m_MaxLineDescender - this.m_LineOffset);
								}
								bool flag44 = this.m_LineNumber == 0 || this.m_IsNewPage;
								if (flag44)
								{
									bool flag45 = isFirstCharacterOfLine || !isWhiteSpace;
									if (flag45)
									{
										this.m_MaxAscender = this.m_MaxLineAscender;
										this.m_MaxCapHeight = Mathf.Max(this.m_MaxCapHeight, this.m_CurrentFontAsset.m_FaceInfo.capLine * currentElementScale / smallCapsMultiplier);
									}
								}
								bool flag46 = this.m_LineOffset == 0f;
								if (flag46)
								{
									bool flag47 = !isWhiteSpace || this.m_CharacterCount == this.m_FirstCharacterOfLine;
									if (flag47)
									{
										this.m_PageAscender = ((this.m_PageAscender > elementAscender) ? this.m_PageAscender : elementAscender);
									}
								}
								bool flag48 = charCode == 9U || charCode == 8203U || ((textWrapMode == TextWrappingMode.PreserveWhitespace || textWrapMode == TextWrappingMode.PreserveWhitespaceNoWrap) && (isWhiteSpace || charCode == 8203U)) || (!isWhiteSpace && charCode != 8203U && charCode != 173U && charCode != 3U) || (charCode == 173U && !isSoftHyphenIgnored) || this.m_TextElementType == TextElementType.Sprite;
								if (flag48)
								{
									widthOfTextArea = ((this.m_Width != -1f) ? Mathf.Min(marginWidth + 0.0001f - this.m_MarginLeft - this.m_MarginRight, this.m_Width) : (marginWidth + 0.0001f - this.m_MarginLeft - this.m_MarginRight));
									float textWidth = Mathf.Abs(this.m_XAdvance) + currentGlyphMetrics.horizontalAdvance * (1f - this.m_CharWidthAdjDelta) * ((charCode == 173U) ? currentElementUnmodifiedScale : currentElementScale);
									bool flag49 = isBaseGlyph && textWidth > widthOfTextArea;
									if (flag49)
									{
										bool flag50 = textWrapMode != TextWrappingMode.NoWrap && textWrapMode != TextWrappingMode.PreserveWhitespaceNoWrap && this.m_CharacterCount != this.m_FirstCharacterOfLine;
										if (flag50)
										{
											i = this.RestoreWordWrappingState(ref internalWordWrapState, textInfo);
											bool flag51 = this.m_InternalTextElementInfo[this.m_CharacterCount - 1].character == 173U && !isSoftHyphenIgnored && generationSettings.overflowMode == TextOverflowMode.Overflow;
											if (flag51)
											{
												characterToSubstitute.index = this.m_CharacterCount - 1;
												characterToSubstitute.unicode = 45U;
												i--;
												this.m_CharacterCount--;
												goto IL_224B;
											}
											isSoftHyphenIgnored = false;
											bool flag52 = this.m_InternalTextElementInfo[this.m_CharacterCount].character == 173U;
											if (flag52)
											{
												isSoftHyphenIgnored = true;
												goto IL_224B;
											}
											bool flag53 = isTextAutoSizingEnabled && isFirstWordOfLine;
											if (flag53)
											{
												bool flag54 = this.m_CharWidthAdjDelta < generationSettings.charWidthMaxAdj / 100f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount;
												if (flag54)
												{
													float adjustedTextWidth = textWidth;
													bool flag55 = this.m_CharWidthAdjDelta > 0f;
													if (flag55)
													{
														adjustedTextWidth /= 1f - this.m_CharWidthAdjDelta;
													}
													float adjustmentDelta = textWidth - (widthOfTextArea - 0.0001f);
													this.m_CharWidthAdjDelta += adjustmentDelta / adjustedTextWidth;
													this.m_CharWidthAdjDelta = Mathf.Min(this.m_CharWidthAdjDelta, generationSettings.charWidthMaxAdj / 100f);
													return Vector2.zero;
												}
												bool flag56 = fontSize > generationSettings.fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount;
												if (flag56)
												{
													this.m_MaxFontSize = fontSize;
													float sizeDelta = Mathf.Max((fontSize - this.m_MinFontSize) / 2f, 0.05f);
													fontSize -= sizeDelta;
													fontSize = Mathf.Max((float)((int)(fontSize * 20f + 0.5f)) / 20f, generationSettings.fontSizeMin);
												}
											}
											float baselineAdjustmentDelta = this.m_MaxLineAscender - this.m_StartOfLineAscender;
											bool flag57 = this.m_LineOffset > 0f && Math.Abs(baselineAdjustmentDelta) > 0.01f && !this.m_IsDrivenLineSpacing && !this.m_IsNewPage;
											if (flag57)
											{
												this.m_MaxDescender -= baselineAdjustmentDelta;
												this.m_LineOffset += baselineAdjustmentDelta;
											}
											float lineAscender = this.m_MaxLineAscender - this.m_LineOffset;
											float lineDescender = this.m_MaxLineDescender - this.m_LineOffset;
											this.m_MaxDescender = ((this.m_MaxDescender < lineDescender) ? this.m_MaxDescender : lineDescender);
											bool flag58 = !isMaxVisibleDescenderSet;
											if (flag58)
											{
												float maxVisibleDescender = this.m_MaxDescender;
											}
											bool flag59 = generationSettings.useMaxVisibleDescender && (this.m_CharacterCount >= generationSettings.maxVisibleCharacters || this.m_LineNumber >= generationSettings.maxVisibleLines);
											if (flag59)
											{
												isMaxVisibleDescenderSet = true;
											}
											this.m_FirstCharacterOfLine = this.m_CharacterCount;
											this.m_LineVisibleCharacterCount = 0;
											this.SaveWordWrappingState(ref internalLineState, i, this.m_CharacterCount - 1, textInfo);
											this.m_LineNumber++;
											float ascender = this.m_InternalTextElementInfo[this.m_CharacterCount].adjustedAscender;
											bool flag60 = this.m_LineHeight == -32767f;
											if (flag60)
											{
												this.m_LineOffset += 0f - this.m_MaxLineDescender + ascender + (lineGap + this.m_LineSpacingDelta) * baseScale + generationSettings.lineSpacing * currentEmScale;
												this.m_IsDrivenLineSpacing = false;
											}
											else
											{
												this.m_LineOffset += this.m_LineHeight + generationSettings.lineSpacing * currentEmScale;
												this.m_IsDrivenLineSpacing = true;
											}
											this.m_MaxLineAscender = -32767f;
											this.m_MaxLineDescender = 32767f;
											this.m_StartOfLineAscender = ascender;
											this.m_XAdvance = 0f + this.m_TagIndent;
											isFirstWordOfLine = true;
											goto IL_224B;
										}
									}
									renderedWidth = Mathf.Max(renderedWidth, textWidth + this.m_MarginLeft + this.m_MarginRight);
									renderedHeight = Mathf.Max(renderedHeight, this.m_MaxAscender - this.m_MaxDescender);
								}
								bool flag61 = this.m_LineOffset > 0f && !TextGeneratorUtilities.Approximately(this.m_MaxLineAscender, this.m_StartOfLineAscender) && !this.m_IsDrivenLineSpacing && !this.m_IsNewPage;
								if (flag61)
								{
									float offsetDelta = this.m_MaxLineAscender - this.m_StartOfLineAscender;
									this.m_MaxDescender -= offsetDelta;
									this.m_LineOffset += offsetDelta;
									this.m_StartOfLineAscender += offsetDelta;
									internalWordWrapState.lineOffset = this.m_LineOffset;
									internalWordWrapState.startOfLineAscender = this.m_StartOfLineAscender;
								}
								bool flag62 = charCode != 8203U;
								if (flag62)
								{
									bool flag63 = charCode == 9U;
									if (flag63)
									{
										float tabSize = this.m_CurrentFontAsset.faceInfo.tabWidth * (float)this.m_CurrentFontAsset.tabMultiple * currentElementScale;
										float tabs = Mathf.Ceil(this.m_XAdvance / tabSize) * tabSize;
										this.m_XAdvance = ((tabs > this.m_XAdvance) ? tabs : (this.m_XAdvance + tabSize));
									}
									else
									{
										bool flag64 = this.m_MonoSpacing != 0f;
										if (flag64)
										{
											bool flag65 = this.m_DuoSpace && (charCode == 46U || charCode == 58U || charCode == 44U);
											float monoAdjustment;
											if (flag65)
											{
												monoAdjustment = this.m_MonoSpacing / 2f - monoAdvance;
											}
											else
											{
												monoAdjustment = this.m_MonoSpacing - monoAdvance;
											}
											this.m_XAdvance += (monoAdjustment + (this.m_CurrentFontAsset.regularStyleSpacing + characterSpacingAdjustment) * currentEmScale + this.m_CSpacing) * (1f - this.m_CharWidthAdjDelta);
											bool flag66 = isWhiteSpace || charCode == 8203U;
											if (flag66)
											{
												this.m_XAdvance += generationSettings.wordSpacing * currentEmScale;
											}
										}
										else
										{
											this.m_XAdvance += ((currentGlyphMetrics.horizontalAdvance * this.m_FXScale.x + glyphAdjustments.xAdvance) * currentElementScale + (this.m_CurrentFontAsset.regularStyleSpacing + characterSpacingAdjustment + boldSpacingAdjustment) * currentEmScale + this.m_CSpacing) * (1f - this.m_CharWidthAdjDelta);
											bool flag67 = isWhiteSpace || charCode == 8203U;
											if (flag67)
											{
												this.m_XAdvance += generationSettings.wordSpacing * currentEmScale;
											}
										}
									}
								}
								bool flag68 = charCode == 13U;
								if (flag68)
								{
									this.m_XAdvance = 0f + this.m_TagIndent;
								}
								bool flag69 = charCode == 10U || charCode == 11U || charCode == 3U || charCode == 8232U || charCode == 8233U || this.m_CharacterCount == totalCharacterCount - 1;
								if (flag69)
								{
									float baselineAdjustmentDelta2 = this.m_MaxLineAscender - this.m_StartOfLineAscender;
									bool flag70 = this.m_LineOffset > 0f && Math.Abs(baselineAdjustmentDelta2) > 0.01f && !this.m_IsDrivenLineSpacing && !this.m_IsNewPage;
									if (flag70)
									{
										this.m_MaxDescender -= baselineAdjustmentDelta2;
										this.m_LineOffset += baselineAdjustmentDelta2;
									}
									this.m_IsNewPage = false;
									float lineDescender2 = this.m_MaxLineDescender - this.m_LineOffset;
									this.m_MaxDescender = ((this.m_MaxDescender < lineDescender2) ? this.m_MaxDescender : lineDescender2);
									bool flag71 = charCode == 10U || charCode == 11U || charCode == 45U || charCode == 8232U || charCode == 8233U;
									if (flag71)
									{
										this.SaveWordWrappingState(ref internalLineState, i, this.m_CharacterCount, textInfo);
										this.SaveWordWrappingState(ref internalWordWrapState, i, this.m_CharacterCount, textInfo);
										this.m_LineNumber++;
										this.m_FirstCharacterOfLine = this.m_CharacterCount + 1;
										float ascender2 = this.m_InternalTextElementInfo[this.m_CharacterCount].adjustedAscender;
										bool flag72 = this.m_LineHeight == -32767f;
										if (flag72)
										{
											float lineOffsetDelta = 0f - this.m_MaxLineDescender + ascender2 + (lineGap + this.m_LineSpacingDelta) * baseScale + (generationSettings.lineSpacing + ((charCode == 10U || charCode == 8233U) ? generationSettings.paragraphSpacing : 0f)) * currentEmScale;
											this.m_LineOffset += lineOffsetDelta;
											this.m_IsDrivenLineSpacing = false;
										}
										else
										{
											this.m_LineOffset += this.m_LineHeight + (generationSettings.lineSpacing + ((charCode == 10U || charCode == 8233U) ? generationSettings.paragraphSpacing : 0f)) * currentEmScale;
											this.m_IsDrivenLineSpacing = true;
										}
										this.m_MaxLineAscender = -32767f;
										this.m_MaxLineDescender = 32767f;
										this.m_StartOfLineAscender = ascender2;
										this.m_XAdvance = 0f + this.m_TagLineIndent + this.m_TagIndent;
										this.m_CharacterCount++;
										goto IL_224B;
									}
									bool flag73 = charCode == 3U;
									if (flag73)
									{
										i = this.m_TextProcessingArray.Length;
									}
								}
								bool flag74 = (textWrapMode != TextWrappingMode.NoWrap && textWrapMode != TextWrappingMode.PreserveWhitespaceNoWrap) || generationSettings.overflowMode == TextOverflowMode.Truncate || generationSettings.overflowMode == TextOverflowMode.Ellipsis;
								if (flag74)
								{
									bool shouldSaveHardLineBreak = false;
									bool shouldSaveSoftLineBreak = false;
									bool flag75 = (isWhiteSpace || charCode == 8203U || charCode == 45U || charCode == 173U) && (!this.m_IsNonBreakingSpace || ignoreNonBreakingSpace) && charCode != 160U && charCode != 8199U && charCode != 8209U && charCode != 8239U && charCode != 8288U;
									if (flag75)
									{
										bool flag76 = charCode != 45U || this.m_CharacterCount <= 0 || !char.IsWhiteSpace((char)textInfo.textElementInfo[this.m_CharacterCount - 1].character);
										if (flag76)
										{
											isFirstWordOfLine = false;
											shouldSaveHardLineBreak = true;
											internalSoftLineBreak.previousWordBreak = -1;
										}
									}
									else
									{
										bool flag77 = !this.m_IsNonBreakingSpace && ((TextGeneratorUtilities.IsHangul(charCode) && !textSettings.lineBreakingRules.useModernHangulLineBreakingRules) || TextGeneratorUtilities.IsCJK(charCode));
										if (flag77)
										{
											bool isCurrentLeadingCharacter = textSettings.lineBreakingRules.leadingCharactersLookup.Contains(charCode);
											bool isNextFollowingCharacter = this.m_CharacterCount < totalCharacterCount - 1 && textSettings.lineBreakingRules.leadingCharactersLookup.Contains(this.m_InternalTextElementInfo[this.m_CharacterCount + 1].character);
											bool flag78 = !isCurrentLeadingCharacter;
											if (flag78)
											{
												bool flag79 = !isNextFollowingCharacter;
												if (flag79)
												{
													isFirstWordOfLine = false;
													shouldSaveHardLineBreak = true;
												}
												bool flag80 = isFirstWordOfLine;
												if (flag80)
												{
													bool flag81 = isWhiteSpace;
													if (flag81)
													{
														shouldSaveSoftLineBreak = true;
													}
													shouldSaveHardLineBreak = true;
												}
											}
											else
											{
												bool flag82 = isFirstWordOfLine && isFirstCharacterOfLine;
												if (flag82)
												{
													bool flag83 = isWhiteSpace;
													if (flag83)
													{
														shouldSaveSoftLineBreak = true;
													}
													shouldSaveHardLineBreak = true;
												}
											}
										}
										else
										{
											bool flag84 = !this.m_IsNonBreakingSpace && this.m_CharacterCount + 1 < totalCharacterCount && TextGeneratorUtilities.IsCJK(textInfo.textElementInfo[this.m_CharacterCount + 1].character);
											if (flag84)
											{
												shouldSaveHardLineBreak = true;
											}
											else
											{
												bool flag85 = isFirstWordOfLine;
												if (flag85)
												{
													bool flag86 = (isWhiteSpace && charCode != 160U) || (charCode == 173U && !isSoftHyphenIgnored);
													if (flag86)
													{
														shouldSaveSoftLineBreak = true;
													}
													shouldSaveHardLineBreak = true;
												}
											}
										}
									}
									bool flag87 = shouldSaveHardLineBreak;
									if (flag87)
									{
										this.SaveWordWrappingState(ref internalWordWrapState, i, this.m_CharacterCount, textInfo);
									}
									bool flag88 = shouldSaveSoftLineBreak;
									if (flag88)
									{
										this.SaveWordWrappingState(ref internalSoftLineBreak, i, this.m_CharacterCount, textInfo);
									}
								}
								this.m_CharacterCount++;
							}
						}
						IL_224B:
						i++;
					}
					float fontSizeDelta = this.m_MaxFontSize - this.m_MinFontSize;
					bool flag89 = isTextAutoSizingEnabled && fontSizeDelta > 0.051f && fontSize < generationSettings.fontSizeMax && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount;
					if (flag89)
					{
						bool flag90 = this.m_CharWidthAdjDelta < generationSettings.charWidthMaxAdj / 100f;
						if (flag90)
						{
							this.m_CharWidthAdjDelta = 0f;
						}
						this.m_MinFontSize = fontSize;
						float sizeDelta2 = Mathf.Max((this.m_MaxFontSize - fontSize) / 2f, 0.05f);
						fontSize += sizeDelta2;
						fontSize = Mathf.Min((float)((int)(fontSize * 20f + 0.5f)) / 20f, generationSettings.fontSizeMax);
						vector = Vector2.zero;
					}
					else
					{
						this.m_IsCalculatingPreferredValues = false;
						renderedWidth += ((generationSettings.margins.x > 0f) ? generationSettings.margins.x : 0f);
						renderedWidth += ((generationSettings.margins.z > 0f) ? generationSettings.margins.z : 0f);
						renderedHeight += ((generationSettings.margins.y > 0f) ? generationSettings.margins.y : 0f);
						renderedHeight += ((generationSettings.margins.w > 0f) ? generationSettings.margins.w : 0f);
						bool flag91 = renderedWidth != 0f;
						if (flag91)
						{
							renderedWidth = (float)((int)(renderedWidth * 100f + 1f)) / 100f;
						}
						bool flag92 = renderedHeight != 0f;
						if (flag92)
						{
							renderedHeight = (float)((int)(renderedHeight * 100f + 1f)) / 100f;
						}
						vector = new Vector2(renderedWidth, renderedHeight);
					}
				}
			}
			return vector;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0001CFC0 File Offset: 0x0001B1C0
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal void Prepare(TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			this.m_Padding = generationSettings.extraPadding;
			this.m_CurrentFontAsset = generationSettings.fontAsset;
			this.m_FontStyleInternal = generationSettings.fontStyle;
			this.m_FontWeightInternal = (((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? TextFontWeight.Bold : generationSettings.fontWeight);
			this.GetSpecialCharacters(generationSettings);
			this.ComputeMarginSize(generationSettings.screenRect, generationSettings.margins);
			RenderedText renderedText = generationSettings.renderedText;
			this.PopulateTextBackingArray(in renderedText);
			this.PopulateTextProcessingArray(generationSettings);
			this.SetArraySizes(this.m_TextProcessingArray, generationSettings, textInfo);
			bool autoSize = generationSettings.autoSize;
			if (autoSize)
			{
				this.m_FontSize = Mathf.Clamp(generationSettings.fontSize, generationSettings.fontSizeMin, generationSettings.fontSizeMax);
			}
			else
			{
				this.m_FontSize = generationSettings.fontSize;
			}
			this.m_MaxFontSize = generationSettings.fontSizeMax;
			this.m_MinFontSize = generationSettings.fontSizeMin;
			this.m_LineSpacingDelta = 0f;
			this.m_CharWidthAdjDelta = 0f;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0001D0B8 File Offset: 0x0001B2B8
		internal bool PrepareFontAsset(TextGenerationSettings generationSettings)
		{
			this.m_CurrentFontAsset = generationSettings.fontAsset;
			this.m_FontStyleInternal = generationSettings.fontStyle;
			this.m_FontWeightInternal = (((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? TextFontWeight.Bold : generationSettings.fontWeight);
			bool flag = !this.GetSpecialCharacters(generationSettings);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				RenderedText renderedText = generationSettings.renderedText;
				this.PopulateTextBackingArray(in renderedText);
				this.PopulateTextProcessingArray(generationSettings);
				flag2 = this.PopulateFontAsset(generationSettings, this.m_TextProcessingArray);
			}
			return flag2;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0001D138 File Offset: 0x0001B338
		private int SetArraySizes(TextProcessingElement[] textProcessingArray, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			TextSettings textSettings = generationSettings.textSettings;
			int spriteCount = 0;
			this.m_TotalCharacterCount = 0;
			this.m_isTextLayoutPhase = false;
			this.m_TagNoParsing = false;
			this.m_FontStyleInternal = generationSettings.fontStyle;
			this.m_FontStyleStack.Clear();
			this.m_FontWeightInternal = (((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? TextFontWeight.Bold : generationSettings.fontWeight);
			this.m_FontWeightStack.SetDefault(this.m_FontWeightInternal);
			this.m_CurrentFontAsset = generationSettings.fontAsset;
			this.m_CurrentMaterial = generationSettings.material;
			this.m_CurrentMaterialIndex = 0;
			this.m_MaterialReferenceStack.SetDefault(new MaterialReference(this.m_CurrentMaterialIndex, this.m_CurrentFontAsset, null, this.m_CurrentMaterial, this.m_Padding));
			this.m_MaterialReferenceIndexLookup.Clear();
			MaterialReference.AddMaterialReference(this.m_CurrentMaterial, this.m_CurrentFontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
			this.m_CurrentSpriteAsset = null;
			bool flag = textInfo == null;
			if (flag)
			{
				textInfo = new TextInfo(VertexDataLayout.Mesh);
			}
			else
			{
				bool flag2 = textInfo.textElementInfo.Length < this.m_InternalTextProcessingArraySize;
				if (flag2)
				{
					TextInfo.Resize<TextElementInfo>(ref textInfo.textElementInfo, this.m_InternalTextProcessingArraySize, false);
				}
			}
			this.m_TextElementType = TextElementType.Character;
			bool flag3 = generationSettings.overflowMode == TextOverflowMode.Ellipsis;
			if (flag3)
			{
				this.GetEllipsisSpecialCharacter(generationSettings);
				bool flag4 = this.m_Ellipsis.character != null;
				if (flag4)
				{
					bool flag5 = this.m_Ellipsis.fontAsset.GetHashCode() != this.m_CurrentFontAsset.GetHashCode();
					if (flag5)
					{
						bool flag6 = textSettings.matchMaterialPreset && this.m_CurrentMaterial.GetHashCode() != this.m_Ellipsis.fontAsset.material.GetHashCode();
						if (flag6)
						{
							this.m_Ellipsis.material = MaterialManager.GetFallbackMaterial(this.m_CurrentMaterial, this.m_Ellipsis.fontAsset.material);
						}
						else
						{
							this.m_Ellipsis.material = this.m_Ellipsis.fontAsset.material;
						}
						this.m_Ellipsis.materialIndex = MaterialReference.AddMaterialReference(this.m_Ellipsis.material, this.m_Ellipsis.fontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
						this.m_MaterialReferences[this.m_Ellipsis.materialIndex].referenceCount = 0;
					}
				}
				else
				{
					generationSettings.overflowMode = TextOverflowMode.Truncate;
					bool displayWarnings = textSettings.displayWarnings;
					if (displayWarnings)
					{
						Debug.LogWarning("The character used for Ellipsis is not available in font asset [" + this.m_CurrentFontAsset.name + "] or any potential fallbacks. Switching Text Overflow mode to Truncate.");
					}
				}
			}
			bool ligature = generationSettings.fontFeatures.Contains(OTL_FeatureTag.liga);
			int i = 0;
			while (i < textProcessingArray.Length && textProcessingArray[i].unicode > 0U)
			{
				bool flag7 = textInfo.textElementInfo == null || this.m_TotalCharacterCount >= textInfo.textElementInfo.Length;
				if (flag7)
				{
					TextInfo.Resize<TextElementInfo>(ref textInfo.textElementInfo, this.m_TotalCharacterCount + 1, true);
				}
				uint unicode = textProcessingArray[i].unicode;
				int prevMaterialIndex = this.m_CurrentMaterialIndex;
				bool flag8 = generationSettings.richText && unicode == 60U;
				if (!flag8)
				{
					goto IL_0477;
				}
				prevMaterialIndex = this.m_CurrentMaterialIndex;
				int endTagIndex;
				bool isThreadSuccess;
				bool flag9 = this.ValidateHtmlTag(textProcessingArray, i + 1, out endTagIndex, generationSettings, textInfo, out isThreadSuccess);
				if (!flag9)
				{
					goto IL_0477;
				}
				int tagStartIndex = textProcessingArray[i].stringIndex;
				i = endTagIndex;
				bool flag10 = this.m_TextElementType == TextElementType.Sprite;
				if (flag10)
				{
					MaterialReference[] materialReferences = this.m_MaterialReferences;
					int currentMaterialIndex = this.m_CurrentMaterialIndex;
					materialReferences[currentMaterialIndex].referenceCount = materialReferences[currentMaterialIndex].referenceCount + 1;
					textInfo.textElementInfo[this.m_TotalCharacterCount].character = (uint)((ushort)(57344 + this.m_SpriteIndex));
					textInfo.textElementInfo[this.m_TotalCharacterCount].fontAsset = this.m_CurrentFontAsset;
					textInfo.textElementInfo[this.m_TotalCharacterCount].materialReferenceIndex = this.m_CurrentMaterialIndex;
					textInfo.textElementInfo[this.m_TotalCharacterCount].textElement = this.m_CurrentSpriteAsset.spriteCharacterTable[this.m_SpriteIndex];
					textInfo.textElementInfo[this.m_TotalCharacterCount].elementType = this.m_TextElementType;
					textInfo.textElementInfo[this.m_TotalCharacterCount].index = tagStartIndex;
					textInfo.textElementInfo[this.m_TotalCharacterCount].stringLength = textProcessingArray[i].stringIndex - tagStartIndex + 1;
					this.m_TextElementType = TextElementType.Character;
					this.m_CurrentMaterialIndex = prevMaterialIndex;
					spriteCount++;
					this.m_TotalCharacterCount++;
				}
				IL_0E74:
				i++;
				continue;
				IL_0477:
				bool isUsingAlternativeTypeface = false;
				bool isUsingFallbackOrAlternativeTypeface = false;
				FontAsset prevFontAsset = this.m_CurrentFontAsset;
				Material prevMaterial = this.m_CurrentMaterial;
				prevMaterialIndex = this.m_CurrentMaterialIndex;
				bool flag11 = this.m_TextElementType == TextElementType.Character;
				if (flag11)
				{
					bool flag12 = (this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase;
					if (flag12)
					{
						bool flag13 = char.IsLower((char)unicode);
						if (flag13)
						{
							unicode = (uint)char.ToUpper((char)unicode);
						}
					}
					else
					{
						bool flag14 = (this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase;
						if (flag14)
						{
							bool flag15 = char.IsUpper((char)unicode);
							if (flag15)
							{
								unicode = (uint)char.ToLower((char)unicode);
							}
						}
						else
						{
							bool flag16 = (this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps;
							if (flag16)
							{
								bool flag17 = char.IsLower((char)unicode);
								if (flag17)
								{
									unicode = (uint)char.ToUpper((char)unicode);
								}
							}
						}
					}
				}
				TextElement character = null;
				uint nextCharacter = ((i + 1 < textProcessingArray.Length) ? textProcessingArray[i + 1].unicode : 0U);
				bool flag18 = generationSettings.emojiFallbackSupport && ((TextGeneratorUtilities.IsEmojiPresentationForm(unicode) && nextCharacter != 65038U) || (TextGeneratorUtilities.IsEmoji(unicode) && nextCharacter == 65039U));
				if (flag18)
				{
					bool flag19 = textSettings.emojiFallbackTextAssets != null && textSettings.emojiFallbackTextAssets.Count > 0;
					if (flag19)
					{
						character = FontAssetUtilities.GetTextElementFromTextAssets(unicode, this.m_CurrentFontAsset, textSettings.emojiFallbackTextAssets, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface, ligature);
						bool flag20 = character != null;
						if (flag20)
						{
						}
					}
				}
				bool flag21 = character == null;
				if (flag21)
				{
					character = this.GetTextElement(generationSettings, unicode, this.m_CurrentFontAsset, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface, ligature);
				}
				bool flag22 = character == null;
				if (flag22)
				{
					this.DoMissingGlyphCallback(unicode, textProcessingArray[i].stringIndex, this.m_CurrentFontAsset, textInfo);
					uint srcGlyph = unicode;
					unicode = (textProcessingArray[i].unicode = (uint)((textSettings.missingCharacterUnicode == 0) ? 9633 : textSettings.missingCharacterUnicode));
					character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, this.m_CurrentFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface, ligature);
					bool flag23 = character == null;
					if (flag23)
					{
						character = FontAssetUtilities.GetCharacterFromFontAssetsInternal(unicode, this.m_CurrentFontAsset, textSettings.GetFallbackFontAssets(generationSettings.isEditorRenderingModeBitmap ? ((int)(generationSettings.fontSize * generationSettings.pixelsPerPoint)) : (-1)), textSettings.fallbackOSFontAssets, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface, ligature);
					}
					bool flag24 = character == null;
					if (flag24)
					{
						bool flag25 = textSettings.defaultFontAsset != null;
						if (flag25)
						{
							character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, textSettings.defaultFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface, ligature);
						}
					}
					bool flag26 = character == null;
					if (flag26)
					{
						unicode = (textProcessingArray[i].unicode = 32U);
						character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, this.m_CurrentFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface, ligature);
					}
					bool flag27 = character == null;
					if (flag27)
					{
						unicode = (textProcessingArray[i].unicode = 3U);
						character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, this.m_CurrentFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface, ligature);
					}
					bool displayWarnings2 = textSettings.displayWarnings;
					if (displayWarnings2)
					{
						bool isMainThread = !JobsUtility.IsExecutingJob;
						string formattedWarning = ((srcGlyph > 65535U) ? string.Format("The character with Unicode value \\U{0:X8} was not found in the [{1}] font asset or any potential fallbacks. It was replaced by Unicode character \\u{2:X4}.", srcGlyph, isMainThread ? generationSettings.fontAsset.name : generationSettings.fontAsset.GetHashCode(), character.unicode) : string.Format("The character with Unicode value \\u{0:X4} was not found in the [{1}] font asset or any potential fallbacks. It was replaced by Unicode character \\u{2:X4}.", srcGlyph, isMainThread ? generationSettings.fontAsset.name : generationSettings.fontAsset.GetHashCode(), character.unicode));
						Debug.LogWarning(formattedWarning);
					}
				}
				textInfo.textElementInfo[this.m_TotalCharacterCount].alternativeGlyph = null;
				bool flag28 = character.elementType == TextElementType.Character;
				if (flag28)
				{
					bool flag29 = character.textAsset.instanceID != this.m_CurrentFontAsset.instanceID;
					if (flag29)
					{
						isUsingFallbackOrAlternativeTypeface = true;
						this.m_CurrentFontAsset = character.textAsset as FontAsset;
					}
					bool flag30 = (nextCharacter >= 65024U && nextCharacter <= 65039U) || (nextCharacter >= 917760U && nextCharacter <= 917999U);
					if (flag30)
					{
						uint variantGlyphIndex;
						bool flag31 = !this.m_CurrentFontAsset.TryGetGlyphVariantIndexInternal(unicode, nextCharacter, out variantGlyphIndex);
						if (flag31)
						{
							variantGlyphIndex = this.m_CurrentFontAsset.GetGlyphVariantIndex(unicode, nextCharacter);
							this.m_CurrentFontAsset.TryAddGlyphVariantIndexInternal(unicode, nextCharacter, variantGlyphIndex);
						}
						bool flag32 = variantGlyphIndex > 0U;
						if (flag32)
						{
							Glyph glyph;
							bool flag33 = this.m_CurrentFontAsset.TryAddGlyphInternal(variantGlyphIndex, out glyph);
							if (flag33)
							{
								textInfo.textElementInfo[this.m_TotalCharacterCount].alternativeGlyph = glyph;
							}
						}
						textProcessingArray[i + 1].unicode = 26U;
						i++;
					}
					List<LigatureSubstitutionRecord> records;
					bool flag34 = ligature && this.m_CurrentFontAsset.fontFeatureTable.m_LigatureSubstitutionRecordLookup.TryGetValue(character.glyphIndex, out records);
					if (flag34)
					{
						bool flag35 = records == null;
						if (flag35)
						{
							break;
						}
						for (int j = 0; j < records.Count; j++)
						{
							LigatureSubstitutionRecord record = records[j];
							int componentCount = record.componentGlyphIDs.Length;
							uint ligatureGlyphID = record.ligatureGlyphID;
							for (int k = 1; k < componentCount; k++)
							{
								uint componentUnicode = textProcessingArray[i + k].unicode;
								bool flag36;
								uint glyphIndex = this.m_CurrentFontAsset.GetGlyphIndex(componentUnicode, out flag36);
								bool flag37 = glyphIndex == record.componentGlyphIDs[k];
								if (!flag37)
								{
									ligatureGlyphID = 0U;
									break;
								}
							}
							bool flag38 = ligatureGlyphID > 0U;
							if (flag38)
							{
								Glyph glyph2;
								bool flag39 = this.m_CurrentFontAsset.TryAddGlyphInternal(ligatureGlyphID, out glyph2);
								if (flag39)
								{
									textInfo.textElementInfo[this.m_TotalCharacterCount].alternativeGlyph = glyph2;
									for (int c = 0; c < componentCount; c++)
									{
										bool flag40 = c == 0;
										if (flag40)
										{
											textProcessingArray[i + c].length = componentCount;
										}
										else
										{
											textProcessingArray[i + c].unicode = 26U;
										}
									}
									i += componentCount - 1;
									break;
								}
							}
						}
					}
				}
				textInfo.textElementInfo[this.m_TotalCharacterCount].elementType = TextElementType.Character;
				textInfo.textElementInfo[this.m_TotalCharacterCount].textElement = character;
				textInfo.textElementInfo[this.m_TotalCharacterCount].isUsingAlternateTypeface = isUsingAlternativeTypeface;
				textInfo.textElementInfo[this.m_TotalCharacterCount].character = (uint)((ushort)unicode);
				textInfo.textElementInfo[this.m_TotalCharacterCount].index = textProcessingArray[i].stringIndex;
				textInfo.textElementInfo[this.m_TotalCharacterCount].stringLength = textProcessingArray[i].length;
				textInfo.textElementInfo[this.m_TotalCharacterCount].fontAsset = this.m_CurrentFontAsset;
				bool flag41 = character.elementType == TextElementType.Sprite;
				if (flag41)
				{
					SpriteAsset spriteAssetRef = character.textAsset as SpriteAsset;
					this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(spriteAssetRef.material, spriteAssetRef, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
					MaterialReference[] materialReferences2 = this.m_MaterialReferences;
					int currentMaterialIndex2 = this.m_CurrentMaterialIndex;
					materialReferences2[currentMaterialIndex2].referenceCount = materialReferences2[currentMaterialIndex2].referenceCount + 1;
					textInfo.textElementInfo[this.m_TotalCharacterCount].elementType = TextElementType.Sprite;
					textInfo.textElementInfo[this.m_TotalCharacterCount].materialReferenceIndex = this.m_CurrentMaterialIndex;
					this.m_TextElementType = TextElementType.Character;
					this.m_CurrentMaterialIndex = prevMaterialIndex;
					spriteCount++;
					this.m_TotalCharacterCount++;
					goto IL_0E74;
				}
				bool flag42 = isUsingFallbackOrAlternativeTypeface && this.m_CurrentFontAsset.instanceID != generationSettings.fontAsset.instanceID;
				if (flag42)
				{
					bool matchMaterialPreset = textSettings.matchMaterialPreset;
					if (matchMaterialPreset)
					{
						this.m_CurrentMaterial = MaterialManager.GetFallbackMaterial(this.m_CurrentMaterial, this.m_CurrentFontAsset.material);
					}
					else
					{
						this.m_CurrentMaterial = this.m_CurrentFontAsset.material;
					}
					this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentMaterial, this.m_CurrentFontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
				}
				bool flag43 = character != null && character.glyph.atlasIndex > 0;
				if (flag43)
				{
					this.m_CurrentMaterial = MaterialManager.GetFallbackMaterial(this.m_CurrentFontAsset, this.m_CurrentMaterial, character.glyph.atlasIndex);
					this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentMaterial, this.m_CurrentFontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
					isUsingFallbackOrAlternativeTypeface = true;
				}
				bool flag44 = !char.IsWhiteSpace((char)unicode) && unicode != 8203U;
				if (flag44)
				{
					bool flag45 = generationSettings.isIMGUI && this.m_MaterialReferences[this.m_CurrentMaterialIndex].referenceCount >= 16383;
					if (flag45)
					{
						this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(new Material(this.m_CurrentMaterial), this.m_CurrentFontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
					}
					MaterialReference[] materialReferences3 = this.m_MaterialReferences;
					int currentMaterialIndex3 = this.m_CurrentMaterialIndex;
					materialReferences3[currentMaterialIndex3].referenceCount = materialReferences3[currentMaterialIndex3].referenceCount + 1;
				}
				textInfo.textElementInfo[this.m_TotalCharacterCount].material = this.m_CurrentMaterial;
				textInfo.textElementInfo[this.m_TotalCharacterCount].materialReferenceIndex = this.m_CurrentMaterialIndex;
				this.m_MaterialReferences[this.m_CurrentMaterialIndex].isFallbackMaterial = isUsingFallbackOrAlternativeTypeface;
				bool flag46 = isUsingFallbackOrAlternativeTypeface;
				if (flag46)
				{
					this.m_MaterialReferences[this.m_CurrentMaterialIndex].fallbackMaterial = prevMaterial;
					this.m_CurrentFontAsset = prevFontAsset;
					this.m_CurrentMaterial = prevMaterial;
					this.m_CurrentMaterialIndex = prevMaterialIndex;
				}
				this.m_TotalCharacterCount++;
				goto IL_0E74;
			}
			bool isCalculatingPreferredValues = this.m_IsCalculatingPreferredValues;
			int num;
			if (isCalculatingPreferredValues)
			{
				this.m_IsCalculatingPreferredValues = false;
				num = this.m_TotalCharacterCount;
			}
			else
			{
				textInfo.spriteCount = spriteCount;
				int materialCount = (textInfo.materialCount = this.m_MaterialReferenceIndexLookup.Count);
				bool flag47 = materialCount > textInfo.meshInfo.Length;
				if (flag47)
				{
					TextInfo.Resize<MeshInfo>(ref textInfo.meshInfo, materialCount, false);
				}
				bool flag48 = this.m_VertexBufferAutoSizeReduction && textInfo.textElementInfo.Length - this.m_TotalCharacterCount > 256;
				if (flag48)
				{
					TextInfo.Resize<TextElementInfo>(ref textInfo.textElementInfo, Mathf.Max(this.m_TotalCharacterCount + 1, 256), true);
				}
				for (int l = 0; l < materialCount; l++)
				{
					int referenceCount = this.m_MaterialReferences[l].referenceCount;
					bool flag49 = (textInfo.meshInfo[l].vertexData == null && textInfo.meshInfo[l].vertices == null) || textInfo.meshInfo[l].vertexBufferSize < referenceCount * 4;
					if (flag49)
					{
						bool flag50 = textInfo.meshInfo[l].vertexData == null && textInfo.meshInfo[l].vertices == null;
						if (flag50)
						{
							textInfo.meshInfo[l] = new MeshInfo(referenceCount + 1, textInfo.vertexDataLayout, generationSettings.isIMGUI);
						}
						else
						{
							textInfo.meshInfo[l].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount), generationSettings.isIMGUI);
						}
					}
					else
					{
						bool flag51 = textInfo.meshInfo[l].vertexBufferSize - referenceCount * 4 > 1024;
						if (flag51)
						{
							textInfo.meshInfo[l].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.Max(Mathf.NextPowerOfTwo(referenceCount), 256), generationSettings.isIMGUI);
						}
					}
					textInfo.meshInfo[l].material = this.m_MaterialReferences[l].material;
					textInfo.meshInfo[l].glyphRenderMode = this.m_MaterialReferences[l].fontAsset.atlasRenderMode;
				}
				num = this.m_TotalCharacterCount;
			}
			return num;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0001E250 File Offset: 0x0001C450
		private TextElement GetTextElement(TextGenerationSettings generationSettings, uint unicode, FontAsset fontAsset, FontStyles fontStyle, TextFontWeight fontWeight, out bool isUsingAlternativeTypeface, bool populateLigatures)
		{
			bool canWriteOnAsset = !TextGenerator.IsExecutingJob;
			TextSettings textSettings = generationSettings.textSettings;
			Character character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, fontAsset, false, fontStyle, fontWeight, out isUsingAlternativeTypeface, populateLigatures);
			bool flag = character != null;
			TextElement textElement;
			if (flag)
			{
				textElement = character;
			}
			else
			{
				bool flag2 = !canWriteOnAsset && (fontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic || fontAsset.atlasPopulationMode == AtlasPopulationMode.DynamicOS);
				if (flag2)
				{
					textElement = null;
				}
				else
				{
					bool flag3 = fontAsset.m_FallbackFontAssetTable != null && fontAsset.m_FallbackFontAssetTable.Count > 0;
					if (flag3)
					{
						character = FontAssetUtilities.GetCharacterFromFontAssetsInternal(unicode, fontAsset, fontAsset.m_FallbackFontAssetTable, null, true, fontStyle, fontWeight, out isUsingAlternativeTypeface, populateLigatures);
					}
					bool flag4 = character != null;
					if (flag4)
					{
						fontAsset.AddCharacterToLookupCache(unicode, character, fontStyle, fontWeight);
						textElement = character;
					}
					else
					{
						bool fontAssetEquals = (canWriteOnAsset ? (fontAsset.instanceID == generationSettings.fontAsset.instanceID) : (fontAsset == generationSettings.fontAsset));
						bool flag5 = !fontAssetEquals;
						if (flag5)
						{
							character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, generationSettings.fontAsset, false, fontStyle, fontWeight, out isUsingAlternativeTypeface, populateLigatures);
							bool flag6 = character != null;
							if (flag6)
							{
								this.m_CurrentMaterialIndex = 0;
								this.m_CurrentMaterial = this.m_MaterialReferences[0].material;
								fontAsset.AddCharacterToLookupCache(unicode, character, fontStyle, fontWeight);
								return character;
							}
							bool flag7 = generationSettings.fontAsset.m_FallbackFontAssetTable != null && generationSettings.fontAsset.m_FallbackFontAssetTable.Count > 0;
							if (flag7)
							{
								character = FontAssetUtilities.GetCharacterFromFontAssetsInternal(unicode, fontAsset, generationSettings.fontAsset.m_FallbackFontAssetTable, null, true, fontStyle, fontWeight, out isUsingAlternativeTypeface, populateLigatures);
							}
							bool flag8 = character != null;
							if (flag8)
							{
								fontAsset.AddCharacterToLookupCache(unicode, character, fontStyle, fontWeight);
								return character;
							}
						}
						bool flag9 = generationSettings.spriteAsset != null;
						if (flag9)
						{
							SpriteCharacter spriteCharacter = FontAssetUtilities.GetSpriteCharacterFromSpriteAsset(unicode, generationSettings.spriteAsset, true);
							bool flag10 = spriteCharacter != null;
							if (flag10)
							{
								return spriteCharacter;
							}
						}
						bool flag11 = textSettings.GetStaticFallbackOSFontAsset() == null && !canWriteOnAsset;
						if (flag11)
						{
							textElement = null;
						}
						else
						{
							character = FontAssetUtilities.GetCharacterFromFontAssetsInternal(unicode, fontAsset, textSettings.GetFallbackFontAssets(generationSettings.isEditorRenderingModeBitmap ? ((int)(generationSettings.fontSize * generationSettings.pixelsPerPoint)) : (-1)), textSettings.fallbackOSFontAssets, true, fontStyle, fontWeight, out isUsingAlternativeTypeface, populateLigatures);
							bool flag12 = character != null;
							if (flag12)
							{
								fontAsset.AddCharacterToLookupCache(unicode, character, fontStyle, fontWeight);
								textElement = character;
							}
							else
							{
								bool flag13 = textSettings.defaultFontAsset != null;
								if (flag13)
								{
									character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, textSettings.defaultFontAsset, true, fontStyle, fontWeight, out isUsingAlternativeTypeface, populateLigatures);
								}
								bool flag14 = character != null;
								if (flag14)
								{
									fontAsset.AddCharacterToLookupCache(unicode, character, fontStyle, fontWeight);
									textElement = character;
								}
								else
								{
									bool flag15 = textSettings.defaultSpriteAsset != null;
									if (flag15)
									{
										bool flag16 = !canWriteOnAsset && textSettings.defaultSpriteAsset.m_SpriteCharacterLookup == null;
										if (flag16)
										{
											return null;
										}
										SpriteCharacter spriteCharacter2 = FontAssetUtilities.GetSpriteCharacterFromSpriteAsset(unicode, textSettings.defaultSpriteAsset, true);
										bool flag17 = spriteCharacter2 != null;
										if (flag17)
										{
											return spriteCharacter2;
										}
									}
									textElement = null;
								}
							}
						}
					}
				}
			}
			return textElement;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0001E548 File Offset: 0x0001C748
		private void PopulateTextBackingArray(in RenderedText sourceText)
		{
			int writeIndex = 0;
			int length = sourceText.CharacterCount;
			bool flag = length >= this.m_TextBackingArray.Capacity;
			if (flag)
			{
				this.m_TextBackingArray.Resize(length);
			}
			foreach (char character in sourceText)
			{
				this.m_TextBackingArray[writeIndex] = (uint)character;
				writeIndex++;
			}
			this.m_TextBackingArray[writeIndex] = 0U;
			this.m_TextBackingArray.Count = writeIndex;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0001E5D0 File Offset: 0x0001C7D0
		private void PopulateTextProcessingArray(TextGenerationSettings generationSettings)
		{
			int srcLength = this.m_TextBackingArray.Count;
			bool flag = this.m_TextProcessingArray.Length < srcLength;
			if (flag)
			{
				TextGeneratorUtilities.ResizeInternalArray<TextProcessingElement>(ref this.m_TextProcessingArray, srcLength);
			}
			TextProcessingStack<int>.SetDefault(this.m_TextStyleStacks, 0);
			this.m_TextStyleStackDepth = 0;
			int writeIndex = 0;
			int styleHashCode = this.m_TextStyleStacks[0].Pop();
			TextStyle textStyle = TextGeneratorUtilities.GetStyle(generationSettings, styleHashCode);
			bool flag2 = textStyle != null && textStyle.hashCode != -1183493901;
			if (flag2)
			{
				TextGeneratorUtilities.InsertOpeningStyleTag(textStyle, ref this.m_TextProcessingArray, ref writeIndex, ref this.m_TextStyleStackDepth, ref this.m_TextStyleStacks, ref generationSettings);
			}
			bool tagNoParsing = generationSettings.tagNoParsing;
			int readIndex = 0;
			while (readIndex < srcLength)
			{
				uint c = this.m_TextBackingArray[readIndex];
				bool flag3 = c == 0U;
				if (flag3)
				{
					break;
				}
				bool flag4 = c == 92U && readIndex < srcLength - 1;
				if (flag4)
				{
					uint num = this.m_TextBackingArray[readIndex + 1];
					uint num2 = num;
					if (num2 != 85U)
					{
						if (num2 != 92U)
						{
							switch (num2)
							{
							case 110U:
							{
								bool flag5 = !generationSettings.parseControlCharacters;
								if (!flag5)
								{
									this.m_TextProcessingArray[writeIndex] = new TextProcessingElement
									{
										elementType = TextProcessingElementType.TextCharacterElement,
										stringIndex = readIndex,
										length = 1,
										unicode = 10U
									};
									readIndex++;
									writeIndex++;
									goto IL_0A01;
								}
								break;
							}
							case 114U:
							{
								bool flag6 = !generationSettings.parseControlCharacters;
								if (!flag6)
								{
									this.m_TextProcessingArray[writeIndex] = new TextProcessingElement
									{
										elementType = TextProcessingElementType.TextCharacterElement,
										stringIndex = readIndex,
										length = 1,
										unicode = 13U
									};
									readIndex++;
									writeIndex++;
									goto IL_0A01;
								}
								break;
							}
							case 116U:
							{
								bool flag7 = !generationSettings.parseControlCharacters;
								if (!flag7)
								{
									this.m_TextProcessingArray[writeIndex] = new TextProcessingElement
									{
										elementType = TextProcessingElementType.TextCharacterElement,
										stringIndex = readIndex,
										length = 1,
										unicode = 9U
									};
									readIndex++;
									writeIndex++;
									goto IL_0A01;
								}
								break;
							}
							case 117U:
							{
								bool flag8 = srcLength > readIndex + 5 && TextGeneratorUtilities.IsValidUTF16(this.m_TextBackingArray, readIndex + 2);
								if (flag8)
								{
									this.m_TextProcessingArray[writeIndex] = new TextProcessingElement
									{
										elementType = TextProcessingElementType.TextCharacterElement,
										stringIndex = readIndex,
										length = 6,
										unicode = TextGeneratorUtilities.GetUTF16(this.m_TextBackingArray, readIndex + 2)
									};
									readIndex += 5;
									writeIndex++;
									goto IL_0A01;
								}
								break;
							}
							case 118U:
							{
								bool flag9 = !generationSettings.parseControlCharacters;
								if (!flag9)
								{
									this.m_TextProcessingArray[writeIndex] = new TextProcessingElement
									{
										elementType = TextProcessingElementType.TextCharacterElement,
										stringIndex = readIndex,
										length = 1,
										unicode = 11U
									};
									readIndex++;
									writeIndex++;
									goto IL_0A01;
								}
								break;
							}
							}
						}
						else
						{
							bool flag10 = !generationSettings.parseControlCharacters;
							if (!flag10)
							{
								readIndex++;
							}
						}
					}
					else
					{
						bool flag11 = srcLength > readIndex + 9 && TextGeneratorUtilities.IsValidUTF32(this.m_TextBackingArray, readIndex + 2);
						if (flag11)
						{
							this.m_TextProcessingArray[writeIndex] = new TextProcessingElement
							{
								elementType = TextProcessingElementType.TextCharacterElement,
								stringIndex = readIndex,
								length = 10,
								unicode = TextGeneratorUtilities.GetUTF32(this.m_TextBackingArray, readIndex + 2)
							};
							readIndex += 9;
							writeIndex++;
							goto IL_0A01;
						}
					}
					goto IL_03B4;
				}
				goto IL_03B4;
				IL_0A01:
				readIndex++;
				continue;
				IL_03B4:
				bool flag12 = c >= 55296U && c <= 56319U && srcLength > readIndex + 1 && this.m_TextBackingArray[readIndex + 1] >= 56320U && this.m_TextBackingArray[readIndex + 1] <= 57343U;
				if (flag12)
				{
					this.m_TextProcessingArray[writeIndex] = new TextProcessingElement
					{
						elementType = TextProcessingElementType.TextCharacterElement,
						stringIndex = readIndex,
						length = 2,
						unicode = TextGeneratorUtilities.ConvertToUTF32(c, this.m_TextBackingArray[readIndex + 1])
					};
					readIndex++;
					writeIndex++;
					goto IL_0A01;
				}
				bool flag13 = c == 60U && generationSettings.richText;
				if (flag13)
				{
					int hashCode = TextGeneratorUtilities.GetMarkupTagHashCode(this.m_TextBackingArray, readIndex + 1);
					MarkupTag markupTag = (MarkupTag)hashCode;
					MarkupTag markupTag2 = markupTag;
					if (markupTag2 <= MarkupTag.CR)
					{
						if (markupTag2 <= MarkupTag.A)
						{
							if (markupTag2 != MarkupTag.NO_PARSE)
							{
								if (markupTag2 != MarkupTag.SLASH_NO_PARSE)
								{
									if (markupTag2 == MarkupTag.A)
									{
										bool flag14 = this.m_TextBackingArray.Count > readIndex + 4 && this.m_TextBackingArray[readIndex + 3] == 104U && this.m_TextBackingArray[readIndex + 4] == 114U;
										if (flag14)
										{
											TextGeneratorUtilities.InsertOpeningTextStyle(TextGeneratorUtilities.GetStyle(generationSettings, 65), ref this.m_TextProcessingArray, ref writeIndex, ref this.m_TextStyleStackDepth, ref this.m_TextStyleStacks, ref generationSettings);
										}
									}
								}
								else
								{
									tagNoParsing = false;
								}
							}
							else
							{
								tagNoParsing = true;
							}
						}
						else if (markupTag2 != MarkupTag.SLASH_A)
						{
							if (markupTag2 != MarkupTag.BR)
							{
								if (markupTag2 == MarkupTag.CR)
								{
									bool flag15 = tagNoParsing;
									if (!flag15)
									{
										bool flag16 = writeIndex == this.m_TextProcessingArray.Length;
										if (flag16)
										{
											TextGeneratorUtilities.ResizeInternalArray<TextProcessingElement>(ref this.m_TextProcessingArray);
										}
										this.m_TextProcessingArray[writeIndex] = new TextProcessingElement
										{
											elementType = TextProcessingElementType.TextCharacterElement,
											stringIndex = readIndex,
											length = 4,
											unicode = 13U
										};
										writeIndex++;
										readIndex += 3;
										goto IL_0A01;
									}
								}
							}
							else
							{
								bool flag17 = tagNoParsing;
								if (!flag17)
								{
									bool flag18 = writeIndex == this.m_TextProcessingArray.Length;
									if (flag18)
									{
										TextGeneratorUtilities.ResizeInternalArray<TextProcessingElement>(ref this.m_TextProcessingArray);
									}
									this.m_TextProcessingArray[writeIndex] = new TextProcessingElement
									{
										elementType = TextProcessingElementType.TextCharacterElement,
										stringIndex = readIndex,
										length = 4,
										unicode = 10U
									};
									writeIndex++;
									readIndex += 3;
									goto IL_0A01;
								}
							}
						}
						else
						{
							TextGeneratorUtilities.InsertClosingTextStyle(TextGeneratorUtilities.GetStyle(generationSettings, 65), ref this.m_TextProcessingArray, ref writeIndex, ref this.m_TextStyleStackDepth, ref this.m_TextStyleStacks, ref generationSettings);
						}
					}
					else if (markupTag2 <= MarkupTag.NBSP)
					{
						if (markupTag2 != MarkupTag.SHY)
						{
							if (markupTag2 != MarkupTag.ZWJ)
							{
								if (markupTag2 == MarkupTag.NBSP)
								{
									bool flag19 = tagNoParsing;
									if (!flag19)
									{
										bool flag20 = writeIndex == this.m_TextProcessingArray.Length;
										if (flag20)
										{
											TextGeneratorUtilities.ResizeInternalArray<TextProcessingElement>(ref this.m_TextProcessingArray);
										}
										this.m_TextProcessingArray[writeIndex] = new TextProcessingElement
										{
											elementType = TextProcessingElementType.TextCharacterElement,
											stringIndex = readIndex,
											length = 6,
											unicode = 160U
										};
										writeIndex++;
										readIndex += 5;
										goto IL_0A01;
									}
								}
							}
							else
							{
								bool flag21 = tagNoParsing;
								if (!flag21)
								{
									bool flag22 = writeIndex == this.m_TextProcessingArray.Length;
									if (flag22)
									{
										TextGeneratorUtilities.ResizeInternalArray<TextProcessingElement>(ref this.m_TextProcessingArray);
									}
									this.m_TextProcessingArray[writeIndex] = new TextProcessingElement
									{
										elementType = TextProcessingElementType.TextCharacterElement,
										stringIndex = readIndex,
										length = 5,
										unicode = 8205U
									};
									writeIndex++;
									readIndex += 4;
									goto IL_0A01;
								}
							}
						}
						else
						{
							bool flag23 = tagNoParsing;
							if (!flag23)
							{
								bool flag24 = writeIndex == this.m_TextProcessingArray.Length;
								if (flag24)
								{
									TextGeneratorUtilities.ResizeInternalArray<TextProcessingElement>(ref this.m_TextProcessingArray);
								}
								this.m_TextProcessingArray[writeIndex] = new TextProcessingElement
								{
									elementType = TextProcessingElementType.TextCharacterElement,
									stringIndex = readIndex,
									length = 5,
									unicode = 173U
								};
								writeIndex++;
								readIndex += 4;
								goto IL_0A01;
							}
						}
					}
					else if (markupTag2 != MarkupTag.ZWSP)
					{
						if (markupTag2 != MarkupTag.STYLE)
						{
							if (markupTag2 == MarkupTag.SLASH_STYLE)
							{
								bool flag25 = tagNoParsing;
								if (!flag25)
								{
									int closeWriteIndex = writeIndex;
									TextGeneratorUtilities.ReplaceClosingStyleTag(ref this.m_TextProcessingArray, ref writeIndex, ref this.m_TextStyleStackDepth, ref this.m_TextStyleStacks, ref generationSettings);
									while (closeWriteIndex < writeIndex)
									{
										this.m_TextProcessingArray[closeWriteIndex].stringIndex = readIndex;
										this.m_TextProcessingArray[closeWriteIndex].length = 8;
										closeWriteIndex++;
									}
									readIndex += 7;
									goto IL_0A01;
								}
							}
						}
						else
						{
							bool flag26 = tagNoParsing;
							if (!flag26)
							{
								int openWriteIndex = writeIndex;
								int srcOffset;
								bool flag27 = TextGeneratorUtilities.ReplaceOpeningStyleTag(ref this.m_TextBackingArray, readIndex, out srcOffset, ref this.m_TextProcessingArray, ref writeIndex, ref this.m_TextStyleStackDepth, ref this.m_TextStyleStacks, ref generationSettings);
								if (flag27)
								{
									while (openWriteIndex < writeIndex)
									{
										this.m_TextProcessingArray[openWriteIndex].stringIndex = readIndex;
										this.m_TextProcessingArray[openWriteIndex].length = srcOffset - readIndex + 1;
										openWriteIndex++;
									}
									readIndex = srcOffset;
									goto IL_0A01;
								}
							}
						}
					}
					else
					{
						bool flag28 = tagNoParsing;
						if (!flag28)
						{
							bool flag29 = writeIndex == this.m_TextProcessingArray.Length;
							if (flag29)
							{
								TextGeneratorUtilities.ResizeInternalArray<TextProcessingElement>(ref this.m_TextProcessingArray);
							}
							this.m_TextProcessingArray[writeIndex] = new TextProcessingElement
							{
								elementType = TextProcessingElementType.TextCharacterElement,
								stringIndex = readIndex,
								length = 6,
								unicode = 8203U
							};
							writeIndex++;
							readIndex += 5;
							goto IL_0A01;
						}
					}
				}
				bool flag30 = writeIndex == this.m_TextProcessingArray.Length;
				if (flag30)
				{
					TextGeneratorUtilities.ResizeInternalArray<TextProcessingElement>(ref this.m_TextProcessingArray);
				}
				this.m_TextProcessingArray[writeIndex] = new TextProcessingElement
				{
					elementType = TextProcessingElementType.TextCharacterElement,
					stringIndex = readIndex,
					length = 1,
					unicode = c
				};
				writeIndex++;
				goto IL_0A01;
			}
			this.m_TextStyleStackDepth = 0;
			bool flag31 = textStyle != null && textStyle.hashCode != -1183493901;
			if (flag31)
			{
				TextGeneratorUtilities.InsertClosingStyleTag(ref this.m_TextProcessingArray, ref writeIndex, ref this.m_TextStyleStackDepth, ref this.m_TextStyleStacks, ref generationSettings);
			}
			bool flag32 = writeIndex == this.m_TextProcessingArray.Length;
			if (flag32)
			{
				TextGeneratorUtilities.ResizeInternalArray<TextProcessingElement>(ref this.m_TextProcessingArray);
			}
			this.m_TextProcessingArray[writeIndex].unicode = 0U;
			this.m_InternalTextProcessingArraySize = writeIndex;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0001F068 File Offset: 0x0001D268
		private bool PopulateFontAsset(TextGenerationSettings generationSettings, TextProcessingElement[] textProcessingArray)
		{
			bool canWriteOnAsset = !TextGenerator.IsExecutingJob;
			TextSettings textSettings = generationSettings.textSettings;
			int spriteCount = 0;
			this.m_TotalCharacterCount = 0;
			this.m_isTextLayoutPhase = false;
			this.m_TagNoParsing = false;
			this.m_FontStyleInternal = generationSettings.fontStyle;
			this.m_FontStyleStack.Clear();
			this.m_FontWeightInternal = (((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? TextFontWeight.Bold : generationSettings.fontWeight);
			this.m_FontWeightStack.SetDefault(this.m_FontWeightInternal);
			this.m_CurrentFontAsset = generationSettings.fontAsset;
			this.m_CurrentMaterial = generationSettings.material;
			this.m_CurrentMaterialIndex = 0;
			this.m_MaterialReferenceStack.SetDefault(new MaterialReference(this.m_CurrentMaterialIndex, this.m_CurrentFontAsset, null, this.m_CurrentMaterial, this.m_Padding));
			this.m_MaterialReferenceIndexLookup.Clear();
			MaterialReference.AddMaterialReference(this.m_CurrentMaterial, this.m_CurrentFontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
			this.m_TextElementType = TextElementType.Character;
			bool flag = generationSettings.overflowMode == TextOverflowMode.Ellipsis;
			if (flag)
			{
				this.GetEllipsisSpecialCharacter(generationSettings);
				bool flag2 = this.m_Ellipsis.character != null;
				if (flag2)
				{
					bool flag3 = this.m_Ellipsis.fontAsset.GetHashCode() != this.m_CurrentFontAsset.GetHashCode();
					if (flag3)
					{
						bool flag4 = textSettings.matchMaterialPreset && this.m_CurrentMaterial.GetHashCode() != this.m_Ellipsis.fontAsset.material.GetHashCode();
						if (flag4)
						{
							bool flag5 = !canWriteOnAsset;
							if (flag5)
							{
								return false;
							}
							this.m_Ellipsis.material = MaterialManager.GetFallbackMaterial(this.m_CurrentMaterial, this.m_Ellipsis.fontAsset.material);
						}
						else
						{
							this.m_Ellipsis.material = this.m_Ellipsis.fontAsset.material;
						}
						this.m_Ellipsis.materialIndex = MaterialReference.AddMaterialReference(this.m_Ellipsis.material, this.m_Ellipsis.fontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
						this.m_MaterialReferences[this.m_Ellipsis.materialIndex].referenceCount = 0;
					}
				}
			}
			bool ligature = generationSettings.fontFeatures.Contains(OTL_FeatureTag.liga);
			int i = 0;
			while (i < textProcessingArray.Length && textProcessingArray[i].unicode > 0U)
			{
				uint unicode = textProcessingArray[i].unicode;
				int prevMaterialIndex = this.m_CurrentMaterialIndex;
				bool flag6 = generationSettings.richText && unicode == 60U;
				if (!flag6)
				{
					goto IL_02EC;
				}
				prevMaterialIndex = this.m_CurrentMaterialIndex;
				int endTagIndex;
				bool isThreadSuccess;
				bool flag7 = this.ValidateHtmlTag(textProcessingArray, i + 1, out endTagIndex, generationSettings, null, out isThreadSuccess);
				if (flag7)
				{
					int tagStartIndex = textProcessingArray[i].stringIndex;
					i = endTagIndex;
					bool flag8 = this.m_TextElementType == TextElementType.Sprite;
					if (flag8)
					{
						this.m_TextElementType = TextElementType.Character;
						this.m_CurrentMaterialIndex = prevMaterialIndex;
						spriteCount++;
						this.m_TotalCharacterCount++;
					}
				}
				else
				{
					bool flag9 = !isThreadSuccess;
					if (flag9)
					{
						return false;
					}
					goto IL_02EC;
				}
				IL_0BF9:
				i++;
				continue;
				IL_02EC:
				bool isUsingFallbackOrAlternativeTypeface = false;
				FontAsset prevFontAsset = this.m_CurrentFontAsset;
				Material prevMaterial = this.m_CurrentMaterial;
				prevMaterialIndex = this.m_CurrentMaterialIndex;
				bool flag10 = this.m_TextElementType == TextElementType.Character;
				if (flag10)
				{
					bool flag11 = (this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase;
					if (flag11)
					{
						bool flag12 = char.IsLower((char)unicode);
						if (flag12)
						{
							unicode = (uint)char.ToUpper((char)unicode);
						}
					}
					else
					{
						bool flag13 = (this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase;
						if (flag13)
						{
							bool flag14 = char.IsUpper((char)unicode);
							if (flag14)
							{
								unicode = (uint)char.ToLower((char)unicode);
							}
						}
						else
						{
							bool flag15 = (this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps;
							if (flag15)
							{
								bool flag16 = char.IsLower((char)unicode);
								if (flag16)
								{
									unicode = (uint)char.ToUpper((char)unicode);
								}
							}
						}
					}
				}
				bool flag17 = !canWriteOnAsset && this.m_CurrentFontAsset.m_CharacterLookupDictionary == null;
				if (flag17)
				{
					return false;
				}
				TextElement character = null;
				uint nextCharacter = ((i + 1 < textProcessingArray.Length) ? textProcessingArray[i + 1].unicode : 0U);
				bool flag18 = generationSettings.emojiFallbackSupport && ((TextGeneratorUtilities.IsEmojiPresentationForm(unicode) && nextCharacter != 65038U) || (TextGeneratorUtilities.IsEmoji(unicode) && nextCharacter == 65039U));
				if (flag18)
				{
					bool flag19 = textSettings.emojiFallbackTextAssets != null && textSettings.emojiFallbackTextAssets.Count > 0;
					if (flag19)
					{
						bool flag20;
						character = FontAssetUtilities.GetTextElementFromTextAssets(unicode, this.m_CurrentFontAsset, textSettings.emojiFallbackTextAssets, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag20, ligature);
						bool flag21 = character != null;
						if (flag21)
						{
						}
					}
				}
				bool flag22 = character == null;
				if (flag22)
				{
					bool flag20;
					character = this.GetTextElement(generationSettings, unicode, this.m_CurrentFontAsset, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag20, ligature);
				}
				bool flag23 = character == null;
				if (flag23)
				{
					bool flag24 = !canWriteOnAsset;
					if (flag24)
					{
						return false;
					}
					uint srcGlyph = unicode;
					unicode = (textProcessingArray[i].unicode = (uint)((textSettings.missingCharacterUnicode == 0) ? 9633 : textSettings.missingCharacterUnicode));
					bool flag20;
					character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, this.m_CurrentFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag20, ligature);
					bool flag25 = character == null;
					if (flag25)
					{
						bool flag26 = textSettings.GetFallbackFontAssets(generationSettings.isEditorRenderingModeBitmap ? ((int)(generationSettings.fontSize * generationSettings.pixelsPerPoint)) : (-1)) == null && !canWriteOnAsset;
						if (flag26)
						{
							return false;
						}
						character = FontAssetUtilities.GetCharacterFromFontAssetsInternal(unicode, this.m_CurrentFontAsset, textSettings.GetFallbackFontAssets(generationSettings.isEditorRenderingModeBitmap ? ((int)(generationSettings.fontSize * generationSettings.pixelsPerPoint)) : (-1)), textSettings.fallbackOSFontAssets, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag20, ligature);
					}
					bool flag27 = character == null;
					if (flag27)
					{
						bool flag28 = textSettings.defaultFontAsset != null;
						if (flag28)
						{
							character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, textSettings.defaultFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag20, ligature);
						}
					}
					bool flag29 = character == null;
					if (flag29)
					{
						unicode = (textProcessingArray[i].unicode = 32U);
						character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, this.m_CurrentFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag20, ligature);
					}
					bool flag30 = character == null;
					if (flag30)
					{
						unicode = (textProcessingArray[i].unicode = 3U);
						character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, this.m_CurrentFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag20, ligature);
					}
					bool displayWarnings = textSettings.displayWarnings;
					if (displayWarnings)
					{
						string formattedWarning = ((srcGlyph > 65535U) ? string.Format("The character with Unicode value \\U{0:X8} was not found in the [{1}] font asset or any potential fallbacks. It was replaced by Unicode character \\u{2:X4}.", srcGlyph, generationSettings.fontAsset.name, character.unicode) : string.Format("The character with Unicode value \\u{0:X4} was not found in the [{1}] font asset or any potential fallbacks. It was replaced by Unicode character \\u{2:X4}.", srcGlyph, generationSettings.fontAsset.name, character.unicode));
						Debug.LogWarning(formattedWarning);
					}
				}
				bool flag31 = character.elementType == TextElementType.Character;
				if (flag31)
				{
					bool textAssetEquals = (canWriteOnAsset ? (character.textAsset.instanceID == this.m_CurrentFontAsset.instanceID) : (character.textAsset == this.m_CurrentFontAsset));
					bool flag32 = !textAssetEquals;
					if (flag32)
					{
						isUsingFallbackOrAlternativeTypeface = true;
						this.m_CurrentFontAsset = character.textAsset as FontAsset;
					}
					bool flag33 = (nextCharacter >= 65024U && nextCharacter <= 65039U) || (nextCharacter >= 917760U && nextCharacter <= 917999U);
					if (flag33)
					{
						uint variantGlyphIndex;
						bool flag34 = !this.m_CurrentFontAsset.TryGetGlyphVariantIndexInternal(unicode, nextCharacter, out variantGlyphIndex);
						if (flag34)
						{
							bool flag35 = !canWriteOnAsset;
							if (flag35)
							{
								return false;
							}
							variantGlyphIndex = this.m_CurrentFontAsset.GetGlyphVariantIndex(unicode, nextCharacter);
							this.m_CurrentFontAsset.TryAddGlyphVariantIndexInternal(unicode, nextCharacter, variantGlyphIndex);
						}
						bool flag36 = variantGlyphIndex > 0U;
						if (flag36)
						{
							Glyph glyph;
							this.m_CurrentFontAsset.TryAddGlyphInternal(variantGlyphIndex, out glyph);
						}
						textProcessingArray[i + 1].unicode = 26U;
						i++;
					}
					List<LigatureSubstitutionRecord> records;
					bool flag37 = ligature && this.m_CurrentFontAsset.fontFeatureTable.m_LigatureSubstitutionRecordLookup.TryGetValue(character.glyphIndex, out records);
					if (flag37)
					{
						bool flag38 = records == null;
						if (flag38)
						{
							break;
						}
						for (int j = 0; j < records.Count; j++)
						{
							LigatureSubstitutionRecord record = records[j];
							int componentCount = record.componentGlyphIDs.Length;
							uint ligatureGlyphID = record.ligatureGlyphID;
							for (int k = 1; k < componentCount; k++)
							{
								uint componentUnicode = textProcessingArray[i + k].unicode;
								bool success;
								uint glyphIndex = this.m_CurrentFontAsset.GetGlyphIndex(componentUnicode, out success);
								bool flag39 = !success;
								if (flag39)
								{
									return false;
								}
								bool flag40 = glyphIndex == record.componentGlyphIDs[k];
								if (!flag40)
								{
									ligatureGlyphID = 0U;
									break;
								}
							}
							bool flag41 = ligatureGlyphID > 0U;
							if (flag41)
							{
								bool flag42 = !canWriteOnAsset;
								if (flag42)
								{
									return false;
								}
								Glyph glyph2;
								bool flag43 = this.m_CurrentFontAsset.TryAddGlyphInternal(ligatureGlyphID, out glyph2);
								if (flag43)
								{
									for (int c = 0; c < componentCount; c++)
									{
										bool flag44 = c == 0;
										if (flag44)
										{
											textProcessingArray[i + c].length = componentCount;
										}
										else
										{
											textProcessingArray[i + c].unicode = 26U;
										}
									}
									i += componentCount - 1;
									break;
								}
							}
						}
					}
				}
				bool flag45 = character.elementType == TextElementType.Sprite;
				if (flag45)
				{
					SpriteAsset spriteAssetRef = character.textAsset as SpriteAsset;
					this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(spriteAssetRef.material, spriteAssetRef, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
					this.m_TextElementType = TextElementType.Character;
					this.m_CurrentMaterialIndex = prevMaterialIndex;
					spriteCount++;
					this.m_TotalCharacterCount++;
					goto IL_0BF9;
				}
				bool flag46 = isUsingFallbackOrAlternativeTypeface && this.m_CurrentFontAsset.instanceID != generationSettings.fontAsset.instanceID;
				if (flag46)
				{
					bool flag47 = canWriteOnAsset;
					if (flag47)
					{
						bool matchMaterialPreset = textSettings.matchMaterialPreset;
						if (matchMaterialPreset)
						{
							this.m_CurrentMaterial = MaterialManager.GetFallbackMaterial(this.m_CurrentMaterial, this.m_CurrentFontAsset.material);
						}
						else
						{
							this.m_CurrentMaterial = this.m_CurrentFontAsset.material;
						}
					}
					else
					{
						bool matchMaterialPreset2 = textSettings.matchMaterialPreset;
						if (matchMaterialPreset2)
						{
							return false;
						}
						this.m_CurrentMaterial = this.m_CurrentFontAsset.material;
					}
					this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentMaterial, this.m_CurrentFontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
				}
				bool flag48 = character != null && character.glyph.atlasIndex > 0;
				if (flag48)
				{
					bool flag49 = canWriteOnAsset;
					if (!flag49)
					{
						return false;
					}
					this.m_CurrentMaterial = MaterialManager.GetFallbackMaterial(this.m_CurrentFontAsset, this.m_CurrentMaterial, character.glyph.atlasIndex);
					this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentMaterial, this.m_CurrentFontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
					isUsingFallbackOrAlternativeTypeface = true;
				}
				bool flag50 = !char.IsWhiteSpace((char)unicode) && unicode != 8203U;
				if (flag50)
				{
					bool flag51 = generationSettings.isIMGUI && this.m_MaterialReferences[this.m_CurrentMaterialIndex].referenceCount >= 16383;
					if (flag51)
					{
						this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(new Material(this.m_CurrentMaterial), this.m_CurrentFontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
					}
					MaterialReference[] materialReferences = this.m_MaterialReferences;
					int currentMaterialIndex = this.m_CurrentMaterialIndex;
					materialReferences[currentMaterialIndex].referenceCount = materialReferences[currentMaterialIndex].referenceCount + 1;
				}
				this.m_MaterialReferences[this.m_CurrentMaterialIndex].isFallbackMaterial = isUsingFallbackOrAlternativeTypeface;
				bool flag52 = isUsingFallbackOrAlternativeTypeface;
				if (flag52)
				{
					this.m_MaterialReferences[this.m_CurrentMaterialIndex].fallbackMaterial = prevMaterial;
					this.m_CurrentFontAsset = prevFontAsset;
					this.m_CurrentMaterial = prevMaterial;
					this.m_CurrentMaterialIndex = prevMaterialIndex;
				}
				this.m_TotalCharacterCount++;
				goto IL_0BF9;
			}
			return true;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0001FCA0 File Offset: 0x0001DEA0
		private void ComputeMarginSize(Rect rect, Vector4 margins)
		{
			this.m_MarginWidth = rect.width - margins.x - margins.z;
			this.m_MarginHeight = rect.height - margins.y - margins.w;
			this.m_RectTransformCorners[0].x = 0f;
			this.m_RectTransformCorners[0].y = 0f;
			this.m_RectTransformCorners[1].x = 0f;
			this.m_RectTransformCorners[1].y = rect.height;
			this.m_RectTransformCorners[2].x = rect.width;
			this.m_RectTransformCorners[2].y = rect.height;
			this.m_RectTransformCorners[3].x = rect.width;
			this.m_RectTransformCorners[3].y = 0f;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0001FD9C File Offset: 0x0001DF9C
		protected bool GetSpecialCharacters(TextGenerationSettings generationSettings)
		{
			bool flag = !this.GetEllipsisSpecialCharacter(generationSettings);
			return !flag && this.GetUnderlineSpecialCharacter(generationSettings);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0001FDC8 File Offset: 0x0001DFC8
		protected bool GetEllipsisSpecialCharacter(TextGenerationSettings generationSettings)
		{
			bool canWriteOnAsset = !TextGenerator.IsExecutingJob;
			FontAsset fontAsset = this.m_CurrentFontAsset ?? generationSettings.fontAsset;
			TextSettings textSettings = generationSettings.textSettings;
			bool populateLigature = generationSettings.fontFeatures.Contains(OTL_FeatureTag.liga);
			bool isUsingAlternativeTypeface;
			Character character = FontAssetUtilities.GetCharacterFromFontAsset(8230U, fontAsset, false, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface, populateLigature);
			bool flag = character == null;
			if (flag)
			{
				bool flag2 = fontAsset.m_FallbackFontAssetTable != null && fontAsset.m_FallbackFontAssetTable.Count > 0;
				if (flag2)
				{
					character = FontAssetUtilities.GetCharacterFromFontAssetsInternal(8230U, fontAsset, fontAsset.m_FallbackFontAssetTable, null, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface, populateLigature);
				}
			}
			bool flag3 = character == null;
			if (flag3)
			{
				bool flag4 = textSettings.GetStaticFallbackOSFontAsset() == null && !canWriteOnAsset;
				if (flag4)
				{
					return false;
				}
				character = FontAssetUtilities.GetCharacterFromFontAssetsInternal(8230U, fontAsset, textSettings.GetFallbackFontAssets(generationSettings.isEditorRenderingModeBitmap ? ((int)(generationSettings.fontSize * generationSettings.pixelsPerPoint)) : (-1)), textSettings.fallbackOSFontAssets, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface, populateLigature);
			}
			bool flag5 = character == null;
			if (flag5)
			{
				bool flag6 = textSettings.defaultFontAsset != null;
				if (flag6)
				{
					character = FontAssetUtilities.GetCharacterFromFontAsset(8230U, textSettings.defaultFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface, populateLigature);
				}
			}
			bool flag7 = character != null;
			if (flag7)
			{
				this.m_Ellipsis = new TextGenerator.SpecialCharacter(character, 0);
			}
			return true;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0001FF48 File Offset: 0x0001E148
		protected bool GetUnderlineSpecialCharacter(TextGenerationSettings generationSettings)
		{
			bool canWriteOnAsset = !TextGenerator.IsExecutingJob;
			FontAsset fontAsset = this.m_CurrentFontAsset ?? generationSettings.fontAsset;
			TextSettings textSettings = generationSettings.textSettings;
			bool populateLigature = generationSettings.fontFeatures.Contains(OTL_FeatureTag.liga);
			bool isUsingAlternativeTypeface;
			Character character = FontAssetUtilities.GetCharacterFromFontAsset(95U, fontAsset, false, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface, populateLigature);
			bool flag = character == null && !canWriteOnAsset;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = character != null;
				if (flag3)
				{
					this.m_Underline = new TextGenerator.SpecialCharacter(character, this.m_CurrentMaterialIndex);
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0001FFDF File Offset: 0x0001E1DF
		protected void DoMissingGlyphCallback(uint unicode, int stringIndex, FontAsset fontAsset, TextInfo textInfo)
		{
			TextGenerator.MissingCharacterEventCallback onMissingCharacter = TextGenerator.OnMissingCharacter;
			if (onMissingCharacter != null)
			{
				onMissingCharacter(unicode, stringIndex, textInfo, fontAsset);
			}
		}

		// Token: 0x040001C3 RID: 451
		private const int k_Tab = 9;

		// Token: 0x040001C4 RID: 452
		private const int k_LineFeed = 10;

		// Token: 0x040001C5 RID: 453
		private const int k_VerticalTab = 11;

		// Token: 0x040001C6 RID: 454
		private const int k_CarriageReturn = 13;

		// Token: 0x040001C7 RID: 455
		private const int k_Space = 32;

		// Token: 0x040001C8 RID: 456
		private const int k_DoubleQuotes = 34;

		// Token: 0x040001C9 RID: 457
		private const int k_NumberSign = 35;

		// Token: 0x040001CA RID: 458
		private const int k_PercentSign = 37;

		// Token: 0x040001CB RID: 459
		private const int k_SingleQuote = 39;

		// Token: 0x040001CC RID: 460
		private const int k_Plus = 43;

		// Token: 0x040001CD RID: 461
		private const int k_Period = 46;

		// Token: 0x040001CE RID: 462
		private const int k_LesserThan = 60;

		// Token: 0x040001CF RID: 463
		private const int k_Equal = 61;

		// Token: 0x040001D0 RID: 464
		private const int k_GreaterThan = 62;

		// Token: 0x040001D1 RID: 465
		private const int k_Underline = 95;

		// Token: 0x040001D2 RID: 466
		private const int k_NoBreakSpace = 160;

		// Token: 0x040001D3 RID: 467
		private const int k_SoftHyphen = 173;

		// Token: 0x040001D4 RID: 468
		private const int k_HyphenMinus = 45;

		// Token: 0x040001D5 RID: 469
		private const int k_FigureSpace = 8199;

		// Token: 0x040001D6 RID: 470
		private const int k_Hyphen = 8208;

		// Token: 0x040001D7 RID: 471
		private const int k_NonBreakingHyphen = 8209;

		// Token: 0x040001D8 RID: 472
		private const int k_ZeroWidthSpace = 8203;

		// Token: 0x040001D9 RID: 473
		private const int k_NarrowNoBreakSpace = 8239;

		// Token: 0x040001DA RID: 474
		private const int k_WordJoiner = 8288;

		// Token: 0x040001DB RID: 475
		private const int k_HorizontalEllipsis = 8230;

		// Token: 0x040001DC RID: 476
		private const int k_LineSeparator = 8232;

		// Token: 0x040001DD RID: 477
		private const int k_ParagraphSeparator = 8233;

		// Token: 0x040001DE RID: 478
		private const int k_RightSingleQuote = 8217;

		// Token: 0x040001DF RID: 479
		private const int k_Square = 9633;

		// Token: 0x040001E0 RID: 480
		private const int k_HangulJamoStart = 4352;

		// Token: 0x040001E1 RID: 481
		private const int k_HangulJamoEnd = 4607;

		// Token: 0x040001E2 RID: 482
		private const int k_CjkStart = 11904;

		// Token: 0x040001E3 RID: 483
		private const int k_CjkEnd = 40959;

		// Token: 0x040001E4 RID: 484
		private const int k_HangulJameExtendedStart = 43360;

		// Token: 0x040001E5 RID: 485
		private const int k_HangulJameExtendedEnd = 43391;

		// Token: 0x040001E6 RID: 486
		private const int k_HangulSyllablesStart = 44032;

		// Token: 0x040001E7 RID: 487
		private const int k_HangulSyllablesEnd = 55295;

		// Token: 0x040001E8 RID: 488
		private const int k_CjkIdeographsStart = 63744;

		// Token: 0x040001E9 RID: 489
		private const int k_CjkIdeographsEnd = 64255;

		// Token: 0x040001EA RID: 490
		private const int k_CjkFormsStart = 65072;

		// Token: 0x040001EB RID: 491
		private const int k_CjkFormsEnd = 65103;

		// Token: 0x040001EC RID: 492
		private const int k_CjkHalfwidthStart = 65280;

		// Token: 0x040001ED RID: 493
		private const int k_CjkHalfwidthEnd = 65519;

		// Token: 0x040001EE RID: 494
		private const int k_EndOfText = 3;

		// Token: 0x040001EF RID: 495
		private const float k_FloatUnset = -32767f;

		// Token: 0x040001F0 RID: 496
		private const int k_MaxCharacters = 8;

		// Token: 0x040001F1 RID: 497
		private static TextGenerator s_TextGenerator;

		// Token: 0x040001F3 RID: 499
		private TextBackingContainer m_TextBackingArray = new TextBackingContainer(4);

		// Token: 0x040001F4 RID: 500
		internal TextProcessingElement[] m_TextProcessingArray = new TextProcessingElement[8];

		// Token: 0x040001F5 RID: 501
		internal int m_InternalTextProcessingArraySize;

		// Token: 0x040001F6 RID: 502
		[SerializeField]
		protected bool m_VertexBufferAutoSizeReduction = false;

		// Token: 0x040001F7 RID: 503
		private char[] m_HtmlTag = new char[256];

		// Token: 0x040001F8 RID: 504
		internal HighlightState m_HighlightState = new HighlightState(Color.white, Offset.zero);

		// Token: 0x040001F9 RID: 505
		protected bool m_IsIgnoringAlignment;

		// Token: 0x040001FA RID: 506
		protected bool m_IsTextTruncated;

		// Token: 0x040001FB RID: 507
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static TextGenerator.MissingCharacterEventCallback OnMissingCharacter;

		// Token: 0x040001FC RID: 508
		private Vector3[] m_RectTransformCorners = new Vector3[4];

		// Token: 0x040001FD RID: 509
		private float m_MarginWidth;

		// Token: 0x040001FE RID: 510
		private float m_MarginHeight;

		// Token: 0x040001FF RID: 511
		private float m_PreferredWidth;

		// Token: 0x04000200 RID: 512
		private float m_PreferredHeight;

		// Token: 0x04000201 RID: 513
		private FontAsset m_CurrentFontAsset;

		// Token: 0x04000202 RID: 514
		private Material m_CurrentMaterial;

		// Token: 0x04000203 RID: 515
		private int m_CurrentMaterialIndex;

		// Token: 0x04000204 RID: 516
		private TextProcessingStack<MaterialReference> m_MaterialReferenceStack = new TextProcessingStack<MaterialReference>(new MaterialReference[16]);

		// Token: 0x04000205 RID: 517
		private float m_Padding;

		// Token: 0x04000206 RID: 518
		private SpriteAsset m_CurrentSpriteAsset;

		// Token: 0x04000207 RID: 519
		private int m_TotalCharacterCount;

		// Token: 0x04000208 RID: 520
		private float m_FontSize;

		// Token: 0x04000209 RID: 521
		private float m_FontScaleMultiplier;

		// Token: 0x0400020A RID: 522
		private float m_CurrentFontSize;

		// Token: 0x0400020B RID: 523
		private TextProcessingStack<float> m_SizeStack = new TextProcessingStack<float>(16);

		// Token: 0x0400020C RID: 524
		protected TextProcessingStack<int>[] m_TextStyleStacks = new TextProcessingStack<int>[8];

		// Token: 0x0400020D RID: 525
		protected int m_TextStyleStackDepth = 0;

		// Token: 0x0400020E RID: 526
		private FontStyles m_FontStyleInternal = FontStyles.Normal;

		// Token: 0x0400020F RID: 527
		private FontStyleStack m_FontStyleStack;

		// Token: 0x04000210 RID: 528
		private TextFontWeight m_FontWeightInternal = TextFontWeight.Regular;

		// Token: 0x04000211 RID: 529
		private TextProcessingStack<TextFontWeight> m_FontWeightStack = new TextProcessingStack<TextFontWeight>(8);

		// Token: 0x04000212 RID: 530
		private TextAlignment m_LineJustification;

		// Token: 0x04000213 RID: 531
		private TextProcessingStack<TextAlignment> m_LineJustificationStack = new TextProcessingStack<TextAlignment>(16);

		// Token: 0x04000214 RID: 532
		private float m_BaselineOffset;

		// Token: 0x04000215 RID: 533
		private TextProcessingStack<float> m_BaselineOffsetStack = new TextProcessingStack<float>(new float[16]);

		// Token: 0x04000216 RID: 534
		private Color32 m_FontColor32;

		// Token: 0x04000217 RID: 535
		private Color32 m_HtmlColor;

		// Token: 0x04000218 RID: 536
		private Color32 m_UnderlineColor;

		// Token: 0x04000219 RID: 537
		private Color32 m_StrikethroughColor;

		// Token: 0x0400021A RID: 538
		private TextProcessingStack<Color32> m_ColorStack = new TextProcessingStack<Color32>(new Color32[16]);

		// Token: 0x0400021B RID: 539
		private TextProcessingStack<Color32> m_UnderlineColorStack = new TextProcessingStack<Color32>(new Color32[16]);

		// Token: 0x0400021C RID: 540
		private TextProcessingStack<Color32> m_StrikethroughColorStack = new TextProcessingStack<Color32>(new Color32[16]);

		// Token: 0x0400021D RID: 541
		private TextProcessingStack<Color32> m_HighlightColorStack = new TextProcessingStack<Color32>(new Color32[16]);

		// Token: 0x0400021E RID: 542
		private TextProcessingStack<HighlightState> m_HighlightStateStack = new TextProcessingStack<HighlightState>(new HighlightState[16]);

		// Token: 0x0400021F RID: 543
		private TextProcessingStack<int> m_ItalicAngleStack = new TextProcessingStack<int>(new int[16]);

		// Token: 0x04000220 RID: 544
		private TextColorGradient m_ColorGradientPreset;

		// Token: 0x04000221 RID: 545
		private TextProcessingStack<TextColorGradient> m_ColorGradientStack = new TextProcessingStack<TextColorGradient>(new TextColorGradient[16]);

		// Token: 0x04000222 RID: 546
		private bool m_ColorGradientPresetIsTinted;

		// Token: 0x04000223 RID: 547
		private TextProcessingStack<int> m_ActionStack = new TextProcessingStack<int>(new int[16]);

		// Token: 0x04000224 RID: 548
		private float m_LineOffset;

		// Token: 0x04000225 RID: 549
		private float m_LineHeight;

		// Token: 0x04000226 RID: 550
		private bool m_IsDrivenLineSpacing;

		// Token: 0x04000227 RID: 551
		private float m_CSpacing;

		// Token: 0x04000228 RID: 552
		private float m_MonoSpacing;

		// Token: 0x04000229 RID: 553
		private bool m_DuoSpace;

		// Token: 0x0400022A RID: 554
		private float m_XAdvance;

		// Token: 0x0400022B RID: 555
		private float m_TagLineIndent;

		// Token: 0x0400022C RID: 556
		private float m_TagIndent;

		// Token: 0x0400022D RID: 557
		private TextProcessingStack<float> m_IndentStack = new TextProcessingStack<float>(new float[16]);

		// Token: 0x0400022E RID: 558
		private bool m_TagNoParsing;

		// Token: 0x0400022F RID: 559
		private int m_CharacterCount;

		// Token: 0x04000230 RID: 560
		private int m_FirstCharacterOfLine;

		// Token: 0x04000231 RID: 561
		private int m_LastCharacterOfLine;

		// Token: 0x04000232 RID: 562
		private int m_FirstVisibleCharacterOfLine;

		// Token: 0x04000233 RID: 563
		private int m_LastVisibleCharacterOfLine;

		// Token: 0x04000234 RID: 564
		private float m_MaxLineAscender;

		// Token: 0x04000235 RID: 565
		private float m_MaxLineDescender;

		// Token: 0x04000236 RID: 566
		private int m_LineNumber;

		// Token: 0x04000237 RID: 567
		private int m_LineVisibleCharacterCount;

		// Token: 0x04000238 RID: 568
		private int m_LineVisibleSpaceCount;

		// Token: 0x04000239 RID: 569
		private int m_FirstOverflowCharacterIndex;

		// Token: 0x0400023A RID: 570
		private int m_PageNumber;

		// Token: 0x0400023B RID: 571
		private float m_MarginLeft;

		// Token: 0x0400023C RID: 572
		private float m_MarginRight;

		// Token: 0x0400023D RID: 573
		private float m_Width;

		// Token: 0x0400023E RID: 574
		private Extents m_MeshExtents;

		// Token: 0x0400023F RID: 575
		private float m_MaxCapHeight;

		// Token: 0x04000240 RID: 576
		private float m_MaxAscender;

		// Token: 0x04000241 RID: 577
		private float m_MaxDescender;

		// Token: 0x04000242 RID: 578
		private bool m_IsNewPage;

		// Token: 0x04000243 RID: 579
		private bool m_IsNonBreakingSpace;

		// Token: 0x04000244 RID: 580
		private WordWrapState m_SavedWordWrapState;

		// Token: 0x04000245 RID: 581
		private WordWrapState m_SavedLineState;

		// Token: 0x04000246 RID: 582
		private WordWrapState m_SavedEllipsisState = default(WordWrapState);

		// Token: 0x04000247 RID: 583
		private WordWrapState m_SavedLastValidState = default(WordWrapState);

		// Token: 0x04000248 RID: 584
		private WordWrapState m_SavedSoftLineBreakState = default(WordWrapState);

		// Token: 0x04000249 RID: 585
		private TextElementType m_TextElementType;

		// Token: 0x0400024A RID: 586
		private bool m_isTextLayoutPhase;

		// Token: 0x0400024B RID: 587
		private int m_SpriteIndex;

		// Token: 0x0400024C RID: 588
		private Color32 m_SpriteColor;

		// Token: 0x0400024D RID: 589
		private TextElement m_CachedTextElement;

		// Token: 0x0400024E RID: 590
		private Color32 m_HighlightColor;

		// Token: 0x0400024F RID: 591
		private float m_CharWidthAdjDelta;

		// Token: 0x04000250 RID: 592
		private float m_MaxFontSize;

		// Token: 0x04000251 RID: 593
		private float m_MinFontSize;

		// Token: 0x04000252 RID: 594
		private int m_AutoSizeIterationCount;

		// Token: 0x04000253 RID: 595
		private int m_AutoSizeMaxIterationCount = 100;

		// Token: 0x04000254 RID: 596
		private float m_StartOfLineAscender;

		// Token: 0x04000255 RID: 597
		private float m_LineSpacingDelta;

		// Token: 0x04000256 RID: 598
		internal MaterialReference[] m_MaterialReferences = new MaterialReference[8];

		// Token: 0x04000257 RID: 599
		private int m_SpriteCount = 0;

		// Token: 0x04000258 RID: 600
		private TextProcessingStack<int> m_StyleStack = new TextProcessingStack<int>(new int[16]);

		// Token: 0x04000259 RID: 601
		private TextProcessingStack<WordWrapState> m_EllipsisInsertionCandidateStack = new TextProcessingStack<WordWrapState>(8, 8);

		// Token: 0x0400025A RID: 602
		private int m_SpriteAnimationId;

		// Token: 0x0400025B RID: 603
		private int m_ItalicAngle;

		// Token: 0x0400025C RID: 604
		private Vector3 m_FXScale;

		// Token: 0x0400025D RID: 605
		private Quaternion m_FXRotation;

		// Token: 0x0400025E RID: 606
		private int m_LastBaseGlyphIndex;

		// Token: 0x0400025F RID: 607
		private float m_PageAscender;

		// Token: 0x04000260 RID: 608
		private RichTextTagAttribute[] m_XmlAttribute = new RichTextTagAttribute[8];

		// Token: 0x04000261 RID: 609
		private float[] m_AttributeParameterValues = new float[16];

		// Token: 0x04000262 RID: 610
		private Dictionary<int, int> m_MaterialReferenceIndexLookup = new Dictionary<int, int>();

		// Token: 0x04000263 RID: 611
		private bool m_IsCalculatingPreferredValues;

		// Token: 0x04000264 RID: 612
		private bool m_TintSprite;

		// Token: 0x04000265 RID: 613
		protected TextGenerator.SpecialCharacter m_Ellipsis;

		// Token: 0x04000266 RID: 614
		protected TextGenerator.SpecialCharacter m_Underline;

		// Token: 0x04000267 RID: 615
		private TextElementInfo[] m_InternalTextElementInfo;

		// Token: 0x02000041 RID: 65
		// (Invoke) Token: 0x060001C1 RID: 449
		public delegate void MissingCharacterEventCallback(uint unicode, int stringIndex, TextInfo text, FontAsset fontAsset);

		// Token: 0x02000042 RID: 66
		protected struct SpecialCharacter
		{
			// Token: 0x060001C2 RID: 450 RVA: 0x000201F4 File Offset: 0x0001E3F4
			public SpecialCharacter(Character character, int materialIndex)
			{
				this.character = character;
				this.fontAsset = character.textAsset as FontAsset;
				this.material = ((this.fontAsset != null) ? this.fontAsset.material : null);
				this.materialIndex = materialIndex;
			}

			// Token: 0x04000268 RID: 616
			public Character character;

			// Token: 0x04000269 RID: 617
			public FontAsset fontAsset;

			// Token: 0x0400026A RID: 618
			public Material material;

			// Token: 0x0400026B RID: 619
			public int materialIndex;
		}
	}
}
