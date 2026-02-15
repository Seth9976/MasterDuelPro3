using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Bindings;
using UnityEngine.TextCore.Text;

namespace UnityEngine.TextCore
{
	// Token: 0x0200000D RID: 13
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static class RichTextTagParser
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002528 File Offset: 0x00000728
		private unsafe static bool tagMatch(ReadOnlySpan<char> tagCandidate, [Nullable(1)] string tagName)
		{
			return tagCandidate.StartsWith(tagName.AsSpan()) && (tagCandidate.Length == tagName.Length || (!char.IsLetter((char)(*tagCandidate[tagName.Length])) && *tagCandidate[tagName.Length] != 45));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000258C File Offset: 0x0000078C
		private unsafe static bool SpanToEnum(ReadOnlySpan<char> tagCandidate, out RichTextTagParser.TagType tagType, [Nullable(2)] out string error, out ReadOnlySpan<char> attribute)
		{
			for (int i = 0; i < RichTextTagParser.TagsInfo.Length; i++)
			{
				string tagName = RichTextTagParser.TagsInfo[i].name;
				bool flag = RichTextTagParser.tagMatch(tagCandidate, tagName);
				if (flag)
				{
					tagType = RichTextTagParser.TagsInfo[i].TagType;
					error = null;
					attribute = tagCandidate.Slice(tagName.Length);
					return true;
				}
			}
			bool flag2 = tagCandidate.Length > 4 && *tagCandidate[0] == 35;
			if (flag2)
			{
				tagType = RichTextTagParser.TagType.Color;
				error = null;
				attribute = tagCandidate;
				return true;
			}
			error = "Unknown tag: " + tagCandidate.ToString();
			tagType = RichTextTagParser.TagType.Unknown;
			attribute = null;
			return false;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002658 File Offset: 0x00000858
		[NullableContext(1)]
		internal unsafe static List<RichTextTagParser.Tag> FindTags(string inputStr, [Nullable(new byte[] { 2, 1 })] List<RichTextTagParser.ParseError> errors = null)
		{
			char[] input = inputStr.ToCharArray();
			List<RichTextTagParser.Tag> result = new List<RichTextTagParser.Tag>();
			int pos = 0;
			for (;;)
			{
				int start = Array.IndexOf<char>(input, '<', pos);
				bool flag = start == -1;
				if (flag)
				{
					break;
				}
				int end = Array.IndexOf<char>(input, '>', start);
				bool flag2 = end == -1;
				if (flag2)
				{
					break;
				}
				bool isClosing = input.Length > start + 1 && input[start + 1] == '/';
				bool flag3 = end == start + 1;
				if (flag3)
				{
					if (errors != null)
					{
						errors.Add(new RichTextTagParser.ParseError("Empty tag", start));
					}
					pos = end + 1;
				}
				else
				{
					pos = end + 1;
					bool flag4 = !isClosing;
					if (flag4)
					{
						Span<char> span = input.AsSpan(start + 1, end - start - 1);
						RichTextTagParser.TagType tagType;
						string error;
						ReadOnlySpan<char> atributeSection;
						bool flag5 = RichTextTagParser.SpanToEnum(span, out tagType, out error, out atributeSection);
						if (flag5)
						{
							RichTextTagParser.TagValue value = null;
							bool flag6 = tagType == RichTextTagParser.TagType.Color;
							if (flag6)
							{
								bool flag7 = atributeSection.Length >= 2 && *atributeSection[0] == 61;
								if (flag7)
								{
									atributeSection = atributeSection.Slice(1);
								}
								bool flag8 = atributeSection.Length >= 4 && *atributeSection[0] == 34 && *atributeSection[atributeSection.Length - 1] == 34;
								if (flag8)
								{
									ReadOnlySpan<char> readOnlySpan = atributeSection.Slice(1, atributeSection.Length - 2);
									Color color;
									ColorUtility.TryParseHtmlString(readOnlySpan.ToString(), out color);
									value = new RichTextTagParser.TagValue(color);
								}
								else
								{
									Color color2;
									ColorUtility.TryParseHtmlString(atributeSection.ToString(), out color2);
									value = new RichTextTagParser.TagValue(color2);
								}
								bool flag9 = value == null;
								if (flag9)
								{
									if (errors != null)
									{
										errors.Add(new RichTextTagParser.ParseError("Invalid color value", start));
									}
									pos = start + 1;
									continue;
								}
							}
							bool flag10 = tagType == RichTextTagParser.TagType.Link || tagType == RichTextTagParser.TagType.Hyperlink;
							if (flag10)
							{
								bool flag11 = tagType == RichTextTagParser.TagType.Hyperlink && atributeSection.StartsWith(" href=");
								if (flag11)
								{
									atributeSection = atributeSection.Slice(" href=".Length);
								}
								bool flag12 = atributeSection.Length >= 1 && *atributeSection[0] == 61;
								if (flag12)
								{
									atributeSection = atributeSection.Slice(1);
								}
								bool flag13 = atributeSection.Length >= 2 && *atributeSection[0] == 34 && *atributeSection[atributeSection.Length - 1] == 34;
								if (flag13)
								{
									ReadOnlySpan<char> readOnlySpan = atributeSection.Slice(1, atributeSection.Length - 2);
									value = new RichTextTagParser.TagValue(readOnlySpan.ToString());
								}
								else
								{
									value = new RichTextTagParser.TagValue(atributeSection.ToString());
								}
							}
							result.Add(new RichTextTagParser.Tag
							{
								tagType = tagType,
								start = start,
								end = end,
								isClosing = isClosing,
								value = value
							});
							bool flag14 = tagType == RichTextTagParser.TagType.NoParse;
							if (flag14)
							{
								bool flag15 = (start = input.AsSpan(pos).IndexOf("</noparse>")) == -1;
								if (flag15)
								{
									break;
								}
								start += pos;
								end = start + "</noparse>".Length;
								result.Add(new RichTextTagParser.Tag
								{
									tagType = RichTextTagParser.TagType.NoParse,
									start = start,
									end = end,
									isClosing = true
								});
								pos = end + 1;
							}
						}
						else
						{
							bool flag16 = error != null;
							if (flag16)
							{
								if (errors != null)
								{
									errors.Add(new RichTextTagParser.ParseError(error, start));
								}
							}
							pos = start + 1;
						}
					}
					else
					{
						ReadOnlySpan<char> readOnlySpan;
						RichTextTagParser.TagType tagType2;
						string error2;
						bool flag17 = RichTextTagParser.SpanToEnum(input.AsSpan(start + 2, end - start - 2), out tagType2, out error2, out readOnlySpan);
						if (flag17)
						{
							result.Add(new RichTextTagParser.Tag
							{
								tagType = tagType2,
								start = start,
								end = end,
								isClosing = isClosing
							});
						}
						else
						{
							bool flag18 = error2 != null;
							if (flag18)
							{
								if (errors != null)
								{
									errors.Add(new RichTextTagParser.ParseError(error2, start));
								}
							}
							pos = start + 1;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002A90 File Offset: 0x00000C90
		[NullableContext(1)]
		internal unsafe static List<RichTextTagParser.Tag> PickResultingTags(List<RichTextTagParser.Tag> allTags, string input, int atPosition, [Nullable(2)] List<RichTextTagParser.Tag> applicableTags = null)
		{
			bool flag = applicableTags == null;
			if (flag)
			{
				applicableTags = new List<RichTextTagParser.Tag>();
			}
			else
			{
				applicableTags.Clear();
			}
			int startingPos = 0;
			Debug.Assert(string.IsNullOrEmpty(input) || (atPosition < input.Length && atPosition >= 0), "Invalid position");
			Debug.Assert(startingPos <= atPosition && startingPos >= 0, "Invalid starting position");
			int previousTagPosition = 0;
			foreach (RichTextTagParser.Tag tag in allTags)
			{
				Debug.Assert(tag.start >= previousTagPosition, "Tags are not sorted");
				previousTagPosition = tag.end + 1;
			}
			foreach (RichTextTagParser.Tag tag2 in applicableTags)
			{
				Debug.Assert(tag2.end <= startingPos, "Tag end pass the point where we should start parsing");
				Debug.Assert(allTags.Contains(tag2));
			}
			int count = allTags.Count;
			Span<int?> parents;
			Span<int?> lastTagOfType;
			int i;
			checked
			{
				Span<int?> span = new Span<int?>(stackalloc byte[unchecked((UIntPtr)count) * (UIntPtr)sizeof(int?)], count);
				parents = span;
				int num = RichTextTagParser.TagsInfo.Length;
				span = new Span<int?>(stackalloc byte[unchecked((UIntPtr)num) * (UIntPtr)sizeof(int?)], num);
				lastTagOfType = span;
				i = -1;
			}
			foreach (RichTextTagParser.Tag tag3 in allTags)
			{
				i++;
				bool flag2 = tag3.end < startingPos;
				if (!flag2)
				{
					bool flag3 = tag3.tagType == RichTextTagParser.TagType.NoParse;
					if (!flag3)
					{
						bool flag4 = tag3.start > atPosition;
						if (flag4)
						{
							break;
						}
						bool isClosing = tag3.isClosing;
						if (isClosing)
						{
							bool flag5 = lastTagOfType[(int)tag3.tagType] != null;
							if (flag5)
							{
								bool flag6 = parents[i] != null;
								if (flag6)
								{
									*lastTagOfType[(int)tag3.tagType] = *parents[i];
								}
								else
								{
									*lastTagOfType[(int)tag3.tagType] = null;
								}
							}
						}
						else
						{
							int? currentLastTagIndex = *lastTagOfType[(int)tag3.tagType];
							bool flag7 = currentLastTagIndex != null;
							if (flag7)
							{
								*parents[i] = currentLastTagIndex;
							}
							*lastTagOfType[(int)tag3.tagType] = new int?(i);
						}
					}
				}
			}
			int currentTagIndex = 0;
			foreach (RichTextTagParser.Tag tag4 in allTags)
			{
				int? lastTag = *lastTagOfType[(int)tag4.tagType];
				bool flag8 = lastTag != null && currentTagIndex == lastTag.Value;
				if (flag8)
				{
					applicableTags.Add(tag4);
				}
				currentTagIndex++;
			}
			return applicableTags;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002E18 File Offset: 0x00001018
		[NullableContext(1)]
		internal static RichTextTagParser.Segment[] GenerateSegments(string input, List<RichTextTagParser.Tag> tags)
		{
			List<RichTextTagParser.Segment> segments = new List<RichTextTagParser.Segment>();
			int afterPreviousTagEnd = 0;
			for (int i = 0; i < tags.Count; i++)
			{
				Debug.Assert(tags[i].start >= afterPreviousTagEnd);
				bool flag = tags[i].start > afterPreviousTagEnd;
				if (flag)
				{
					segments.Add(new RichTextTagParser.Segment
					{
						start = afterPreviousTagEnd,
						end = tags[i].start - 1
					});
				}
				afterPreviousTagEnd = tags[i].end + 1;
			}
			bool flag2 = afterPreviousTagEnd < input.Length;
			if (flag2)
			{
				segments.Add(new RichTextTagParser.Segment
				{
					start = afterPreviousTagEnd,
					end = input.Length - 1
				});
			}
			return segments.ToArray();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002EF8 File Offset: 0x000010F8
		[NullableContext(1)]
		internal static void ApplyStateToSegment(string input, List<RichTextTagParser.Tag> tags, RichTextTagParser.Segment[] segments)
		{
			for (int i = 0; i < segments.Length; i++)
			{
				segments[i].tags = RichTextTagParser.PickResultingTags(tags, input, segments[i].start, null);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002F3C File Offset: 0x0000113C
		[NullableContext(1)]
		private static int AddLink(RichTextTagParser.TagType type, string value, [Nullable(new byte[] { 1, 0, 1 })] List<ValueTuple<int, RichTextTagParser.TagType, string>> links)
		{
			foreach (ValueTuple<int, RichTextTagParser.TagType, string> valueTuple in links)
			{
				int index = valueTuple.Item1;
				RichTextTagParser.TagType listType = valueTuple.Item2;
				string listValue = valueTuple.Item3;
				bool flag = type == listType && value == listValue;
				if (flag)
				{
					return index;
				}
			}
			int nextIndex = links.Count;
			links.Add(new ValueTuple<int, RichTextTagParser.TagType, string>(nextIndex, type, value));
			return nextIndex;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002FD4 File Offset: 0x000011D4
		private static TextSpan CreateTextSpan(RichTextTagParser.Segment segment, ref NativeTextGenerationSettings tgs, [Nullable(new byte[] { 1, 0, 1 })] List<ValueTuple<int, RichTextTagParser.TagType, string>> links, Color hyperlinkColor)
		{
			TextSpan textSpan = tgs.CreateTextSpan();
			bool flag = segment.tags == null;
			TextSpan textSpan2;
			if (flag)
			{
				textSpan2 = textSpan;
			}
			else
			{
				for (int i = 0; i < segment.tags.Count; i++)
				{
					switch (segment.tags[i].tagType)
					{
					case RichTextTagParser.TagType.Hyperlink:
					{
						RichTextTagParser.TagType tagType = RichTextTagParser.TagType.Hyperlink;
						RichTextTagParser.TagValue value = segment.tags[i].value;
						textSpan.linkID = RichTextTagParser.AddLink(tagType, ((value != null) ? value.StringValue : null) ?? "", links);
						textSpan.color = hyperlinkColor;
						textSpan.fontStyle |= FontStyles.Underline;
						break;
					}
					case RichTextTagParser.TagType.AllCaps:
					case RichTextTagParser.TagType.Uppercase:
						textSpan.fontStyle |= FontStyles.UpperCase;
						break;
					case RichTextTagParser.TagType.Bold:
						textSpan.fontWeight = TextFontWeight.Bold;
						break;
					case RichTextTagParser.TagType.Color:
						textSpan.color = segment.tags[i].value.ColorValue;
						break;
					case RichTextTagParser.TagType.Italic:
						textSpan.fontStyle |= FontStyles.Italic;
						break;
					case RichTextTagParser.TagType.Link:
					{
						RichTextTagParser.TagType tagType2 = RichTextTagParser.TagType.Link;
						RichTextTagParser.TagValue value2 = segment.tags[i].value;
						textSpan.linkID = RichTextTagParser.AddLink(tagType2, ((value2 != null) ? value2.StringValue : null) ?? "", links);
						break;
					}
					case RichTextTagParser.TagType.Lowercase:
					case RichTextTagParser.TagType.SmallCaps:
						textSpan.fontStyle |= FontStyles.LowerCase;
						break;
					case RichTextTagParser.TagType.Mark:
						textSpan.fontStyle |= FontStyles.Highlight;
						break;
					case RichTextTagParser.TagType.NoParse:
					case RichTextTagParser.TagType.Unknown:
						throw new InvalidOperationException("Invalid tag type" + segment.tags[i].tagType.ToString());
					case RichTextTagParser.TagType.Strikethrough:
						textSpan.fontStyle |= FontStyles.Strikethrough;
						break;
					case RichTextTagParser.TagType.Size:
						textSpan.fontSize = (int)(segment.tags[i].value.NumericalValue / 64f);
						break;
					case RichTextTagParser.TagType.Subscript:
						textSpan.fontStyle |= FontStyles.Subscript;
						break;
					case RichTextTagParser.TagType.Superscript:
						textSpan.fontStyle |= FontStyles.Superscript;
						break;
					case RichTextTagParser.TagType.Underline:
						textSpan.fontStyle |= FontStyles.Underline;
						break;
					}
				}
				textSpan2 = textSpan;
			}
			return textSpan2;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00003294 File Offset: 0x00001494
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static void CreateTextGenerationSettingsArray(ref NativeTextGenerationSettings tgs, [Nullable(new byte[] { 1, 0, 1 })] List<ValueTuple<int, RichTextTagParser.TagType, string>> links, Color hyperlinkColor)
		{
			links.Clear();
			List<RichTextTagParser.Tag> tags = RichTextTagParser.FindTags(tgs.text, null);
			RichTextTagParser.Segment[] segments = RichTextTagParser.GenerateSegments(tgs.text, tags);
			RichTextTagParser.ApplyStateToSegment(tgs.text, tags, segments);
			StringBuilder parsedTextBuilder = new StringBuilder(tgs.text.Length);
			tgs.textSpans = new TextSpan[segments.Length];
			int parsedIndex = 0;
			for (int i = 0; i < segments.Length; i++)
			{
				RichTextTagParser.Segment segment = segments[i];
				string segmentText = tgs.text.Substring(segment.start, segment.end + 1 - segment.start);
				TextSpan textSpan = RichTextTagParser.CreateTextSpan(segment, ref tgs, links, hyperlinkColor);
				textSpan.startIndex = parsedIndex;
				textSpan.length = segmentText.Length;
				tgs.textSpans[i] = textSpan;
				parsedTextBuilder.Append(segmentText);
				parsedIndex += segmentText.Length;
			}
			tgs.text = parsedTextBuilder.ToString();
		}

		// Token: 0x04000031 RID: 49
		[Nullable(1)]
		internal static readonly RichTextTagParser.TagTypeInfo[] TagsInfo = new RichTextTagParser.TagTypeInfo[]
		{
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Hyperlink, "a", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Align, "align", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.AllCaps, "allcaps", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Alpha, "alpha", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Bold, "b", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Br, "br", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Color, "color", RichTextTagParser.TagValueType.ColorValue, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.CSpace, "cspace", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Font, "font", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.FontWeight, "font-weight", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Italic, "i", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Indent, "indent", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.LineHeight, "line-height", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.LineIndent, "line-indent", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Link, "link", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Lowercase, "lowercase", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Mark, "mark", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Mspace, "mspace", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.NoBr, "nobr", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.NoParse, "noparse", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Strikethrough, "s", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Size, "size", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.SmallCaps, "smallcaps", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Space, "space", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Sprite, "sprite", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Style, "style", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Subscript, "sub", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Superscript, "sup", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Underline, "u", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Uppercase, "uppercase", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels)
		};

		// Token: 0x0200000E RID: 14
		public enum TagType
		{
			// Token: 0x04000033 RID: 51
			Hyperlink,
			// Token: 0x04000034 RID: 52
			Align,
			// Token: 0x04000035 RID: 53
			AllCaps,
			// Token: 0x04000036 RID: 54
			Alpha,
			// Token: 0x04000037 RID: 55
			Bold,
			// Token: 0x04000038 RID: 56
			Br,
			// Token: 0x04000039 RID: 57
			Color,
			// Token: 0x0400003A RID: 58
			CSpace,
			// Token: 0x0400003B RID: 59
			Font,
			// Token: 0x0400003C RID: 60
			FontWeight,
			// Token: 0x0400003D RID: 61
			Italic,
			// Token: 0x0400003E RID: 62
			Indent,
			// Token: 0x0400003F RID: 63
			LineHeight,
			// Token: 0x04000040 RID: 64
			LineIndent,
			// Token: 0x04000041 RID: 65
			Link,
			// Token: 0x04000042 RID: 66
			Lowercase,
			// Token: 0x04000043 RID: 67
			Mark,
			// Token: 0x04000044 RID: 68
			Mspace,
			// Token: 0x04000045 RID: 69
			NoBr,
			// Token: 0x04000046 RID: 70
			NoParse,
			// Token: 0x04000047 RID: 71
			Strikethrough,
			// Token: 0x04000048 RID: 72
			Size,
			// Token: 0x04000049 RID: 73
			SmallCaps,
			// Token: 0x0400004A RID: 74
			Space,
			// Token: 0x0400004B RID: 75
			Sprite,
			// Token: 0x0400004C RID: 76
			Style,
			// Token: 0x0400004D RID: 77
			Subscript,
			// Token: 0x0400004E RID: 78
			Superscript,
			// Token: 0x0400004F RID: 79
			Underline,
			// Token: 0x04000050 RID: 80
			Uppercase,
			// Token: 0x04000051 RID: 81
			Unknown
		}

		// Token: 0x0200000F RID: 15
		[NullableContext(1)]
		[Nullable(0)]
		internal class TagTypeInfo : IEquatable<RichTextTagParser.TagTypeInfo>
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000017 RID: 23 RVA: 0x000035B3 File Offset: 0x000017B3
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[CompilerGenerated]
				get
				{
					return typeof(RichTextTagParser.TagTypeInfo);
				}
			}

			// Token: 0x06000018 RID: 24 RVA: 0x000035BF File Offset: 0x000017BF
			internal TagTypeInfo(RichTextTagParser.TagType tagType, string name, RichTextTagParser.TagValueType valueType = RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType unitType = RichTextTagParser.TagUnitType.Pixels)
			{
				this.TagType = tagType;
				this.name = name;
				this.valueType = valueType;
				this.unitType = unitType;
			}

			// Token: 0x06000019 RID: 25 RVA: 0x000035E8 File Offset: 0x000017E8
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("TagTypeInfo");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x0600001A RID: 26 RVA: 0x00003634 File Offset: 0x00001834
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder builder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.Append("TagType = ");
				builder.Append(this.TagType.ToString());
				builder.Append(", name = ");
				builder.Append(this.name);
				builder.Append(", valueType = ");
				builder.Append(this.valueType.ToString());
				builder.Append(", unitType = ");
				builder.Append(this.unitType.ToString());
				return true;
			}

			// Token: 0x0600001B RID: 27 RVA: 0x000036CC File Offset: 0x000018CC
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<RichTextTagParser.TagType>.Default.GetHashCode(this.TagType)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.name)) * -1521134295 + EqualityComparer<RichTextTagParser.TagValueType>.Default.GetHashCode(this.valueType)) * -1521134295 + EqualityComparer<RichTextTagParser.TagUnitType>.Default.GetHashCode(this.unitType);
			}

			// Token: 0x0600001C RID: 28 RVA: 0x00003745 File Offset: 0x00001945
			[CompilerGenerated]
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as RichTextTagParser.TagTypeInfo);
			}

			// Token: 0x0600001D RID: 29 RVA: 0x00003754 File Offset: 0x00001954
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals(RichTextTagParser.TagTypeInfo other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<RichTextTagParser.TagType>.Default.Equals(this.TagType, other.TagType) && EqualityComparer<string>.Default.Equals(this.name, other.name) && EqualityComparer<RichTextTagParser.TagValueType>.Default.Equals(this.valueType, other.valueType) && EqualityComparer<RichTextTagParser.TagUnitType>.Default.Equals(this.unitType, other.unitType));
			}

			// Token: 0x04000052 RID: 82
			public RichTextTagParser.TagType TagType;

			// Token: 0x04000053 RID: 83
			public string name;

			// Token: 0x04000054 RID: 84
			public RichTextTagParser.TagValueType valueType;

			// Token: 0x04000055 RID: 85
			public RichTextTagParser.TagUnitType unitType;
		}

		// Token: 0x02000010 RID: 16
		internal enum TagValueType
		{
			// Token: 0x04000057 RID: 87
			None,
			// Token: 0x04000058 RID: 88
			NumericalValue,
			// Token: 0x04000059 RID: 89
			StringValue,
			// Token: 0x0400005A RID: 90
			ColorValue = 4
		}

		// Token: 0x02000011 RID: 17
		internal enum TagUnitType
		{
			// Token: 0x0400005C RID: 92
			Pixels,
			// Token: 0x0400005D RID: 93
			FontUnits,
			// Token: 0x0400005E RID: 94
			Percentage
		}

		// Token: 0x02000012 RID: 18
		[NullableContext(2)]
		[Nullable(0)]
		internal class TagValue : IEquatable<RichTextTagParser.TagValue>
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600001E RID: 30 RVA: 0x000037DF File Offset: 0x000019DF
			[Nullable(1)]
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[NullableContext(1)]
				[CompilerGenerated]
				get
				{
					return typeof(RichTextTagParser.TagValue);
				}
			}

			// Token: 0x0600001F RID: 31 RVA: 0x000037EB File Offset: 0x000019EB
			internal TagValue(Color value)
			{
				this.type = RichTextTagParser.TagValueType.ColorValue;
				this.m_colorValue = value;
			}

			// Token: 0x06000020 RID: 32 RVA: 0x00003803 File Offset: 0x00001A03
			[NullableContext(1)]
			internal TagValue(string value)
			{
				this.type = RichTextTagParser.TagValueType.StringValue;
				this.m_stringValue = value;
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000021 RID: 33 RVA: 0x0000381C File Offset: 0x00001A1C
			internal string StringValue
			{
				get
				{
					bool flag = this.type != RichTextTagParser.TagValueType.StringValue;
					if (flag)
					{
						throw new InvalidOperationException("Not a string value");
					}
					return this.m_stringValue;
				}
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000022 RID: 34 RVA: 0x00003850 File Offset: 0x00001A50
			internal float NumericalValue
			{
				get
				{
					bool flag = this.type != RichTextTagParser.TagValueType.NumericalValue;
					if (flag)
					{
						throw new InvalidOperationException("Not a numerical value");
					}
					return this.m_numericalValue;
				}
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000023 RID: 35 RVA: 0x00003884 File Offset: 0x00001A84
			internal Color ColorValue
			{
				get
				{
					bool flag = this.type != RichTextTagParser.TagValueType.ColorValue;
					if (flag)
					{
						throw new InvalidOperationException("Not a color value");
					}
					return this.m_colorValue;
				}
			}

			// Token: 0x06000024 RID: 36 RVA: 0x000038B8 File Offset: 0x00001AB8
			[NullableContext(1)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("TagValue");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x06000025 RID: 37 RVA: 0x00003904 File Offset: 0x00001B04
			[CompilerGenerated]
			[NullableContext(1)]
			protected virtual bool PrintMembers(StringBuilder builder)
			{
				return false;
			}

			// Token: 0x06000026 RID: 38 RVA: 0x00003908 File Offset: 0x00001B08
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<RichTextTagParser.TagValueType>.Default.GetHashCode(this.type)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.m_stringValue)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.m_numericalValue)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.m_colorValue);
			}

			// Token: 0x06000027 RID: 39 RVA: 0x00003981 File Offset: 0x00001B81
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as RichTextTagParser.TagValue);
			}

			// Token: 0x06000028 RID: 40 RVA: 0x00003990 File Offset: 0x00001B90
			[CompilerGenerated]
			public virtual bool Equals(RichTextTagParser.TagValue other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<RichTextTagParser.TagValueType>.Default.Equals(this.type, other.type) && EqualityComparer<string>.Default.Equals(this.m_stringValue, other.m_stringValue) && EqualityComparer<float>.Default.Equals(this.m_numericalValue, other.m_numericalValue) && EqualityComparer<Color>.Default.Equals(this.m_colorValue, other.m_colorValue));
			}

			// Token: 0x0400005F RID: 95
			internal RichTextTagParser.TagValueType type;

			// Token: 0x04000060 RID: 96
			private string m_stringValue;

			// Token: 0x04000061 RID: 97
			private float m_numericalValue;

			// Token: 0x04000062 RID: 98
			private Color m_colorValue;
		}

		// Token: 0x02000013 RID: 19
		internal struct Tag
		{
			// Token: 0x04000063 RID: 99
			public RichTextTagParser.TagType tagType;

			// Token: 0x04000064 RID: 100
			public bool isClosing;

			// Token: 0x04000065 RID: 101
			public int start;

			// Token: 0x04000066 RID: 102
			public int end;

			// Token: 0x04000067 RID: 103
			[Nullable(2)]
			public RichTextTagParser.TagValue value;
		}

		// Token: 0x02000014 RID: 20
		public struct Segment
		{
			// Token: 0x04000068 RID: 104
			[Nullable(2)]
			public List<RichTextTagParser.Tag> tags;

			// Token: 0x04000069 RID: 105
			public int start;

			// Token: 0x0400006A RID: 106
			public int end;
		}

		// Token: 0x02000015 RID: 21
		[NullableContext(1)]
		[Nullable(0)]
		internal class ParseError : IEquatable<RichTextTagParser.ParseError>
		{
			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000029 RID: 41 RVA: 0x00003A1B File Offset: 0x00001C1B
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[CompilerGenerated]
				get
				{
					return typeof(RichTextTagParser.ParseError);
				}
			}

			// Token: 0x0600002A RID: 42 RVA: 0x00003A27 File Offset: 0x00001C27
			internal ParseError(string message, int position)
			{
				this.message = message;
				this.position = position;
			}

			// Token: 0x0600002B RID: 43 RVA: 0x00003A40 File Offset: 0x00001C40
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("ParseError");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x0600002C RID: 44 RVA: 0x00003A8C File Offset: 0x00001C8C
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder builder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.Append("position = ");
				builder.Append(this.position.ToString());
				builder.Append(", message = ");
				builder.Append(this.message);
				return true;
			}

			// Token: 0x0600002D RID: 45 RVA: 0x00003ADC File Offset: 0x00001CDC
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.position)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.message);
			}

			// Token: 0x0600002E RID: 46 RVA: 0x00003B1C File Offset: 0x00001D1C
			[CompilerGenerated]
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as RichTextTagParser.ParseError);
			}

			// Token: 0x0600002F RID: 47 RVA: 0x00003B2C File Offset: 0x00001D2C
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals(RichTextTagParser.ParseError other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.position, other.position) && EqualityComparer<string>.Default.Equals(this.message, other.message));
			}

			// Token: 0x0400006B RID: 107
			public readonly int position;

			// Token: 0x0400006C RID: 108
			public readonly string message;
		}
	}
}
