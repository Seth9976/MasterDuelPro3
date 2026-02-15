using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x020000A1 RID: 161
	public static class TMP_TextUtilities
	{
		// Token: 0x060005E4 RID: 1508 RVA: 0x0002C498 File Offset: 0x0002A698
		public static int GetCursorIndexFromPosition(TMP_Text textComponent, Vector3 position, Camera camera)
		{
			int index = TMP_TextUtilities.FindNearestCharacter(textComponent, position, camera, false);
			RectTransform rectTransform = textComponent.rectTransform;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			TMP_CharacterInfo cInfo = textComponent.textInfo.characterInfo[index];
			Vector3 bl = rectTransform.TransformPoint(cInfo.bottomLeft);
			Vector3 tr = rectTransform.TransformPoint(cInfo.topRight);
			if ((position.x - bl.x) / (tr.x - bl.x) < 0.5f)
			{
				return index;
			}
			return index + 1;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0002C518 File Offset: 0x0002A718
		public static int GetCursorIndexFromPosition(TMP_Text textComponent, Vector3 position, Camera camera, out CaretPosition cursor)
		{
			int line = TMP_TextUtilities.FindNearestLine(textComponent, position, camera);
			if (line == -1)
			{
				cursor = CaretPosition.Left;
				return 0;
			}
			int index = TMP_TextUtilities.FindNearestCharacterOnLine(textComponent, position, line, camera, false);
			if (textComponent.textInfo.lineInfo[line].characterCount == 1)
			{
				cursor = CaretPosition.Left;
				return index;
			}
			RectTransform rectTransform = textComponent.rectTransform;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			TMP_CharacterInfo cInfo = textComponent.textInfo.characterInfo[index];
			Vector3 bl = rectTransform.TransformPoint(cInfo.bottomLeft);
			Vector3 tr = rectTransform.TransformPoint(cInfo.topRight);
			if ((position.x - bl.x) / (tr.x - bl.x) < 0.5f)
			{
				cursor = CaretPosition.Left;
				return index;
			}
			cursor = CaretPosition.Right;
			return index;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0002C5D0 File Offset: 0x0002A7D0
		public static int FindNearestLine(TMP_Text text, Vector3 position, Camera camera)
		{
			RectTransform rectTransform = text.rectTransform;
			float distance = float.PositiveInfinity;
			int closest = -1;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			for (int i = 0; i < text.textInfo.lineCount; i++)
			{
				TMP_LineInfo lineInfo = text.textInfo.lineInfo[i];
				float ascender = rectTransform.TransformPoint(new Vector3(0f, lineInfo.ascender, 0f)).y;
				float descender = rectTransform.TransformPoint(new Vector3(0f, lineInfo.descender, 0f)).y;
				if (ascender > position.y && descender < position.y)
				{
					return i;
				}
				float num = Mathf.Abs(ascender - position.y);
				float d = Mathf.Abs(descender - position.y);
				float d2 = Mathf.Min(num, d);
				if (d2 < distance)
				{
					distance = d2;
					closest = i;
				}
			}
			return closest;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0002C6BC File Offset: 0x0002A8BC
		public static int FindNearestCharacterOnLine(TMP_Text text, Vector3 position, int line, Camera camera, bool visibleOnly)
		{
			RectTransform rectTransform = text.rectTransform;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			int firstCharacterIndex = text.textInfo.lineInfo[line].firstCharacterIndex;
			int lastCharacter = text.textInfo.lineInfo[line].lastCharacterIndex;
			float distanceSqr = float.PositiveInfinity;
			int closest = lastCharacter;
			for (int i = firstCharacterIndex; i < lastCharacter; i++)
			{
				TMP_CharacterInfo cInfo = text.textInfo.characterInfo[i];
				if ((!visibleOnly || cInfo.isVisible) && cInfo.character != '\r')
				{
					Vector3 bl = rectTransform.TransformPoint(cInfo.bottomLeft);
					Vector3 tl = rectTransform.TransformPoint(new Vector3(cInfo.bottomLeft.x, cInfo.topRight.y, 0f));
					Vector3 tr = rectTransform.TransformPoint(cInfo.topRight);
					Vector3 br = rectTransform.TransformPoint(new Vector3(cInfo.topRight.x, cInfo.bottomLeft.y, 0f));
					if (TMP_TextUtilities.PointIntersectRectangle(position, bl, tl, tr, br))
					{
						closest = i;
						break;
					}
					float dbl = TMP_TextUtilities.DistanceToLine(bl, tl, position);
					float dtl = TMP_TextUtilities.DistanceToLine(tl, tr, position);
					float dtr = TMP_TextUtilities.DistanceToLine(tr, br, position);
					float dbr = TMP_TextUtilities.DistanceToLine(br, bl, position);
					float d = ((dbl < dtl) ? dbl : dtl);
					d = ((d < dtr) ? d : dtr);
					d = ((d < dbr) ? d : dbr);
					if (distanceSqr > d)
					{
						distanceSqr = d;
						closest = i;
					}
				}
			}
			return closest;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0002C84C File Offset: 0x0002AA4C
		public static bool IsIntersectingRectTransform(RectTransform rectTransform, Vector3 position, Camera camera)
		{
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			rectTransform.GetWorldCorners(TMP_TextUtilities.m_rectWorldCorners);
			return TMP_TextUtilities.PointIntersectRectangle(position, TMP_TextUtilities.m_rectWorldCorners[0], TMP_TextUtilities.m_rectWorldCorners[1], TMP_TextUtilities.m_rectWorldCorners[2], TMP_TextUtilities.m_rectWorldCorners[3]);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0002C8AC File Offset: 0x0002AAAC
		public static int FindIntersectingCharacter(TMP_Text text, Vector3 position, Camera camera, bool visibleOnly)
		{
			RectTransform rectTransform = text.rectTransform;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			for (int i = 0; i < text.textInfo.characterCount; i++)
			{
				TMP_CharacterInfo cInfo = text.textInfo.characterInfo[i];
				if (!visibleOnly || cInfo.isVisible)
				{
					Vector3 bl = rectTransform.TransformPoint(cInfo.bottomLeft);
					Vector3 tl = rectTransform.TransformPoint(new Vector3(cInfo.bottomLeft.x, cInfo.topRight.y, 0f));
					Vector3 tr = rectTransform.TransformPoint(cInfo.topRight);
					Vector3 br = rectTransform.TransformPoint(new Vector3(cInfo.topRight.x, cInfo.bottomLeft.y, 0f));
					if (TMP_TextUtilities.PointIntersectRectangle(position, bl, tl, tr, br))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0002C988 File Offset: 0x0002AB88
		public static int FindNearestCharacter(TMP_Text text, Vector3 position, Camera camera, bool visibleOnly)
		{
			RectTransform rectTransform = text.rectTransform;
			float distanceSqr = float.PositiveInfinity;
			int closest = 0;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			for (int i = 0; i < text.textInfo.characterCount; i++)
			{
				TMP_CharacterInfo cInfo = text.textInfo.characterInfo[i];
				if (!visibleOnly || cInfo.isVisible)
				{
					Vector3 bl = rectTransform.TransformPoint(cInfo.bottomLeft);
					Vector3 tl = rectTransform.TransformPoint(new Vector3(cInfo.bottomLeft.x, cInfo.topRight.y, 0f));
					Vector3 tr = rectTransform.TransformPoint(cInfo.topRight);
					Vector3 br = rectTransform.TransformPoint(new Vector3(cInfo.topRight.x, cInfo.bottomLeft.y, 0f));
					if (TMP_TextUtilities.PointIntersectRectangle(position, bl, tl, tr, br))
					{
						return i;
					}
					float dbl = TMP_TextUtilities.DistanceToLine(bl, tl, position);
					float dtl = TMP_TextUtilities.DistanceToLine(tl, tr, position);
					float dtr = TMP_TextUtilities.DistanceToLine(tr, br, position);
					float dbr = TMP_TextUtilities.DistanceToLine(br, bl, position);
					float d = ((dbl < dtl) ? dbl : dtl);
					d = ((d < dtr) ? d : dtr);
					d = ((d < dbr) ? d : dbr);
					if (distanceSqr > d)
					{
						distanceSqr = d;
						closest = i;
					}
				}
			}
			return closest;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0002CADC File Offset: 0x0002ACDC
		public static int FindIntersectingWord(TMP_Text text, Vector3 position, Camera camera)
		{
			RectTransform rectTransform = text.rectTransform;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			for (int i = 0; i < text.textInfo.wordCount; i++)
			{
				TMP_WordInfo wInfo = text.textInfo.wordInfo[i];
				bool isBeginRegion = false;
				Vector3 bl = Vector3.zero;
				Vector3 tl = Vector3.zero;
				Vector3 br = Vector3.zero;
				Vector3 tr = Vector3.zero;
				float maxAscender = float.NegativeInfinity;
				float minDescender = float.PositiveInfinity;
				for (int j = 0; j < wInfo.characterCount; j++)
				{
					int characterIndex = wInfo.firstCharacterIndex + j;
					TMP_CharacterInfo currentCharInfo = text.textInfo.characterInfo[characterIndex];
					int currentLine = currentCharInfo.lineNumber;
					bool isCharacterVisible = currentCharInfo.isVisible;
					maxAscender = Mathf.Max(maxAscender, currentCharInfo.ascender);
					minDescender = Mathf.Min(minDescender, currentCharInfo.descender);
					if (!isBeginRegion && isCharacterVisible)
					{
						isBeginRegion = true;
						bl = new Vector3(currentCharInfo.bottomLeft.x, currentCharInfo.descender, 0f);
						tl = new Vector3(currentCharInfo.bottomLeft.x, currentCharInfo.ascender, 0f);
						if (wInfo.characterCount == 1)
						{
							isBeginRegion = false;
							br = new Vector3(currentCharInfo.topRight.x, currentCharInfo.descender, 0f);
							tr = new Vector3(currentCharInfo.topRight.x, currentCharInfo.ascender, 0f);
							bl = rectTransform.TransformPoint(new Vector3(bl.x, minDescender, 0f));
							tl = rectTransform.TransformPoint(new Vector3(tl.x, maxAscender, 0f));
							tr = rectTransform.TransformPoint(new Vector3(tr.x, maxAscender, 0f));
							br = rectTransform.TransformPoint(new Vector3(br.x, minDescender, 0f));
							if (TMP_TextUtilities.PointIntersectRectangle(position, bl, tl, tr, br))
							{
								return i;
							}
						}
					}
					if (isBeginRegion && j == wInfo.characterCount - 1)
					{
						isBeginRegion = false;
						br = new Vector3(currentCharInfo.topRight.x, currentCharInfo.descender, 0f);
						tr = new Vector3(currentCharInfo.topRight.x, currentCharInfo.ascender, 0f);
						bl = rectTransform.TransformPoint(new Vector3(bl.x, minDescender, 0f));
						tl = rectTransform.TransformPoint(new Vector3(tl.x, maxAscender, 0f));
						tr = rectTransform.TransformPoint(new Vector3(tr.x, maxAscender, 0f));
						br = rectTransform.TransformPoint(new Vector3(br.x, minDescender, 0f));
						if (TMP_TextUtilities.PointIntersectRectangle(position, bl, tl, tr, br))
						{
							return i;
						}
					}
					else if (isBeginRegion && currentLine != text.textInfo.characterInfo[characterIndex + 1].lineNumber)
					{
						isBeginRegion = false;
						br = new Vector3(currentCharInfo.topRight.x, currentCharInfo.descender, 0f);
						tr = new Vector3(currentCharInfo.topRight.x, currentCharInfo.ascender, 0f);
						bl = rectTransform.TransformPoint(new Vector3(bl.x, minDescender, 0f));
						tl = rectTransform.TransformPoint(new Vector3(tl.x, maxAscender, 0f));
						tr = rectTransform.TransformPoint(new Vector3(tr.x, maxAscender, 0f));
						br = rectTransform.TransformPoint(new Vector3(br.x, minDescender, 0f));
						maxAscender = float.NegativeInfinity;
						minDescender = float.PositiveInfinity;
						if (TMP_TextUtilities.PointIntersectRectangle(position, bl, tl, tr, br))
						{
							return i;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0002CEA8 File Offset: 0x0002B0A8
		public static int FindNearestWord(TMP_Text text, Vector3 position, Camera camera)
		{
			RectTransform rectTransform = text.rectTransform;
			float distanceSqr = float.PositiveInfinity;
			int closest = 0;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			for (int i = 0; i < text.textInfo.wordCount; i++)
			{
				TMP_WordInfo wInfo = text.textInfo.wordInfo[i];
				bool isBeginRegion = false;
				Vector3 bl = Vector3.zero;
				Vector3 tl = Vector3.zero;
				Vector3 br = Vector3.zero;
				Vector3 tr = Vector3.zero;
				for (int j = 0; j < wInfo.characterCount; j++)
				{
					int characterIndex = wInfo.firstCharacterIndex + j;
					TMP_CharacterInfo currentCharInfo = text.textInfo.characterInfo[characterIndex];
					int currentLine = currentCharInfo.lineNumber;
					bool isCharacterVisible = currentCharInfo.isVisible;
					if (!isBeginRegion && isCharacterVisible)
					{
						isBeginRegion = true;
						bl = rectTransform.TransformPoint(new Vector3(currentCharInfo.bottomLeft.x, currentCharInfo.descender, 0f));
						tl = rectTransform.TransformPoint(new Vector3(currentCharInfo.bottomLeft.x, currentCharInfo.ascender, 0f));
						if (wInfo.characterCount == 1)
						{
							isBeginRegion = false;
							br = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.descender, 0f));
							tr = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.ascender, 0f));
							if (TMP_TextUtilities.PointIntersectRectangle(position, bl, tl, tr, br))
							{
								return i;
							}
							float dbl = TMP_TextUtilities.DistanceToLine(bl, tl, position);
							float dtl = TMP_TextUtilities.DistanceToLine(tl, tr, position);
							float dtr = TMP_TextUtilities.DistanceToLine(tr, br, position);
							float dbr = TMP_TextUtilities.DistanceToLine(br, bl, position);
							float d = ((dbl < dtl) ? dbl : dtl);
							d = ((d < dtr) ? d : dtr);
							d = ((d < dbr) ? d : dbr);
							if (distanceSqr > d)
							{
								distanceSqr = d;
								closest = i;
							}
						}
					}
					if (isBeginRegion && j == wInfo.characterCount - 1)
					{
						isBeginRegion = false;
						br = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.descender, 0f));
						tr = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.ascender, 0f));
						if (TMP_TextUtilities.PointIntersectRectangle(position, bl, tl, tr, br))
						{
							return i;
						}
						float dbl2 = TMP_TextUtilities.DistanceToLine(bl, tl, position);
						float dtl2 = TMP_TextUtilities.DistanceToLine(tl, tr, position);
						float dtr2 = TMP_TextUtilities.DistanceToLine(tr, br, position);
						float dbr2 = TMP_TextUtilities.DistanceToLine(br, bl, position);
						float d2 = ((dbl2 < dtl2) ? dbl2 : dtl2);
						d2 = ((d2 < dtr2) ? d2 : dtr2);
						d2 = ((d2 < dbr2) ? d2 : dbr2);
						if (distanceSqr > d2)
						{
							distanceSqr = d2;
							closest = i;
						}
					}
					else if (isBeginRegion && currentLine != text.textInfo.characterInfo[characterIndex + 1].lineNumber)
					{
						isBeginRegion = false;
						br = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.descender, 0f));
						tr = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.ascender, 0f));
						if (TMP_TextUtilities.PointIntersectRectangle(position, bl, tl, tr, br))
						{
							return i;
						}
						float dbl3 = TMP_TextUtilities.DistanceToLine(bl, tl, position);
						float dtl3 = TMP_TextUtilities.DistanceToLine(tl, tr, position);
						float dtr3 = TMP_TextUtilities.DistanceToLine(tr, br, position);
						float dbr3 = TMP_TextUtilities.DistanceToLine(br, bl, position);
						float d3 = ((dbl3 < dtl3) ? dbl3 : dtl3);
						d3 = ((d3 < dtr3) ? d3 : dtr3);
						d3 = ((d3 < dbr3) ? d3 : dbr3);
						if (distanceSqr > d3)
						{
							distanceSqr = d3;
							closest = i;
						}
					}
				}
			}
			return closest;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0002D268 File Offset: 0x0002B468
		public static int FindIntersectingLine(TMP_Text text, Vector3 position, Camera camera)
		{
			RectTransform rectTransform = text.rectTransform;
			int closest = -1;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			for (int i = 0; i < text.textInfo.lineCount; i++)
			{
				TMP_LineInfo lineInfo = text.textInfo.lineInfo[i];
				float ascender = rectTransform.TransformPoint(new Vector3(0f, lineInfo.ascender, 0f)).y;
				float descender = rectTransform.TransformPoint(new Vector3(0f, lineInfo.descender, 0f)).y;
				if (ascender > position.y && descender < position.y)
				{
					return i;
				}
			}
			return closest;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0002D314 File Offset: 0x0002B514
		public static int FindIntersectingLink(TMP_Text text, Vector3 position, Camera camera)
		{
			Transform rectTransform = text.transform;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			for (int i = 0; i < text.textInfo.linkCount; i++)
			{
				TMP_LinkInfo linkInfo = text.textInfo.linkInfo[i];
				bool isBeginRegion = false;
				Vector3 bl = Vector3.zero;
				Vector3 tl = Vector3.zero;
				Vector3 br = Vector3.zero;
				Vector3 tr = Vector3.zero;
				for (int j = 0; j < linkInfo.linkTextLength; j++)
				{
					int characterIndex = linkInfo.linkTextfirstCharacterIndex + j;
					TMP_CharacterInfo currentCharInfo = text.textInfo.characterInfo[characterIndex];
					int currentLine = currentCharInfo.lineNumber;
					if (text.overflowMode != TextOverflowModes.Page || currentCharInfo.pageNumber + 1 == text.pageToDisplay)
					{
						if (!isBeginRegion)
						{
							isBeginRegion = true;
							bl = rectTransform.TransformPoint(new Vector3(currentCharInfo.bottomLeft.x, currentCharInfo.descender, 0f));
							tl = rectTransform.TransformPoint(new Vector3(currentCharInfo.bottomLeft.x, currentCharInfo.ascender, 0f));
							if (linkInfo.linkTextLength == 1)
							{
								isBeginRegion = false;
								br = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.descender, 0f));
								tr = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.ascender, 0f));
								if (TMP_TextUtilities.PointIntersectRectangle(position, bl, tl, tr, br))
								{
									return i;
								}
							}
						}
						if (isBeginRegion && j == linkInfo.linkTextLength - 1)
						{
							isBeginRegion = false;
							br = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.descender, 0f));
							tr = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.ascender, 0f));
							if (TMP_TextUtilities.PointIntersectRectangle(position, bl, tl, tr, br))
							{
								return i;
							}
						}
						else if (isBeginRegion && currentLine != text.textInfo.characterInfo[characterIndex + 1].lineNumber)
						{
							isBeginRegion = false;
							br = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.descender, 0f));
							tr = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.ascender, 0f));
							if (TMP_TextUtilities.PointIntersectRectangle(position, bl, tl, tr, br))
							{
								return i;
							}
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0002D58C File Offset: 0x0002B78C
		public static int FindNearestLink(TMP_Text text, Vector3 position, Camera camera)
		{
			RectTransform rectTransform = text.rectTransform;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			float distanceSqr = float.PositiveInfinity;
			int closest = 0;
			for (int i = 0; i < text.textInfo.linkCount; i++)
			{
				TMP_LinkInfo linkInfo = text.textInfo.linkInfo[i];
				bool isBeginRegion = false;
				Vector3 bl = Vector3.zero;
				Vector3 tl = Vector3.zero;
				Vector3 br = Vector3.zero;
				Vector3 tr = Vector3.zero;
				for (int j = 0; j < linkInfo.linkTextLength; j++)
				{
					int characterIndex = linkInfo.linkTextfirstCharacterIndex + j;
					TMP_CharacterInfo currentCharInfo = text.textInfo.characterInfo[characterIndex];
					int currentLine = currentCharInfo.lineNumber;
					if (text.overflowMode != TextOverflowModes.Page || currentCharInfo.pageNumber + 1 == text.pageToDisplay)
					{
						if (!isBeginRegion)
						{
							isBeginRegion = true;
							bl = rectTransform.TransformPoint(new Vector3(currentCharInfo.bottomLeft.x, currentCharInfo.descender, 0f));
							tl = rectTransform.TransformPoint(new Vector3(currentCharInfo.bottomLeft.x, currentCharInfo.ascender, 0f));
							if (linkInfo.linkTextLength == 1)
							{
								isBeginRegion = false;
								br = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.descender, 0f));
								tr = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.ascender, 0f));
								if (TMP_TextUtilities.PointIntersectRectangle(position, bl, tl, tr, br))
								{
									return i;
								}
								float dbl = TMP_TextUtilities.DistanceToLine(bl, tl, position);
								float dtl = TMP_TextUtilities.DistanceToLine(tl, tr, position);
								float dtr = TMP_TextUtilities.DistanceToLine(tr, br, position);
								float dbr = TMP_TextUtilities.DistanceToLine(br, bl, position);
								float d = ((dbl < dtl) ? dbl : dtl);
								d = ((d < dtr) ? d : dtr);
								d = ((d < dbr) ? d : dbr);
								if (distanceSqr > d)
								{
									distanceSqr = d;
									closest = i;
								}
							}
						}
						if (isBeginRegion && j == linkInfo.linkTextLength - 1)
						{
							isBeginRegion = false;
							br = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.descender, 0f));
							tr = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.ascender, 0f));
							if (TMP_TextUtilities.PointIntersectRectangle(position, bl, tl, tr, br))
							{
								return i;
							}
							float dbl2 = TMP_TextUtilities.DistanceToLine(bl, tl, position);
							float dtl2 = TMP_TextUtilities.DistanceToLine(tl, tr, position);
							float dtr2 = TMP_TextUtilities.DistanceToLine(tr, br, position);
							float dbr2 = TMP_TextUtilities.DistanceToLine(br, bl, position);
							float d2 = ((dbl2 < dtl2) ? dbl2 : dtl2);
							d2 = ((d2 < dtr2) ? d2 : dtr2);
							d2 = ((d2 < dbr2) ? d2 : dbr2);
							if (distanceSqr > d2)
							{
								distanceSqr = d2;
								closest = i;
							}
						}
						else if (isBeginRegion && currentLine != text.textInfo.characterInfo[characterIndex + 1].lineNumber)
						{
							isBeginRegion = false;
							br = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.descender, 0f));
							tr = rectTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, currentCharInfo.ascender, 0f));
							if (TMP_TextUtilities.PointIntersectRectangle(position, bl, tl, tr, br))
							{
								return i;
							}
							float dbl3 = TMP_TextUtilities.DistanceToLine(bl, tl, position);
							float dtl3 = TMP_TextUtilities.DistanceToLine(tl, tr, position);
							float dtr3 = TMP_TextUtilities.DistanceToLine(tr, br, position);
							float dbr3 = TMP_TextUtilities.DistanceToLine(br, bl, position);
							float d3 = ((dbl3 < dtl3) ? dbl3 : dtl3);
							d3 = ((d3 < dtr3) ? d3 : dtr3);
							d3 = ((d3 < dbr3) ? d3 : dbr3);
							if (distanceSqr > d3)
							{
								distanceSqr = d3;
								closest = i;
							}
						}
					}
				}
			}
			return closest;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0002D95C File Offset: 0x0002BB5C
		private static bool PointIntersectRectangle(Vector3 m, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
		{
			Vector3 ab = b - a;
			Vector3 am = m - a;
			Vector3 bc = c - b;
			Vector3 bm = m - b;
			float abamDot = Vector3.Dot(ab, am);
			float bcbmDot = Vector3.Dot(bc, bm);
			return 0f <= abamDot && abamDot <= Vector3.Dot(ab, ab) && 0f <= bcbmDot && bcbmDot <= Vector3.Dot(bc, bc);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0002D9C8 File Offset: 0x0002BBC8
		public static bool ScreenPointToWorldPointInRectangle(Transform transform, Vector2 screenPoint, Camera cam, out Vector3 worldPoint)
		{
			worldPoint = Vector2.zero;
			Ray ray = RectTransformUtility.ScreenPointToRay(cam, screenPoint);
			float enter;
			if (!new Plane(transform.rotation * Vector3.back, transform.position).Raycast(ray, out enter))
			{
				return false;
			}
			worldPoint = ray.GetPoint(enter);
			return true;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0002DA28 File Offset: 0x0002BC28
		private static bool IntersectLinePlane(TMP_TextUtilities.LineSegment line, Vector3 point, Vector3 normal, out Vector3 intersectingPoint)
		{
			intersectingPoint = Vector3.zero;
			Vector3 u = line.Point2 - line.Point1;
			Vector3 w = line.Point1 - point;
			float D = Vector3.Dot(normal, u);
			float N = -Vector3.Dot(normal, w);
			if (Mathf.Abs(D) < Mathf.Epsilon)
			{
				return N == 0f;
			}
			float sI = N / D;
			if (sI < 0f || sI > 1f)
			{
				return false;
			}
			intersectingPoint = line.Point1 + sI * u;
			return true;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0002DABC File Offset: 0x0002BCBC
		public static float DistanceToLine(Vector3 a, Vector3 b, Vector3 point)
		{
			Vector3 i = b - a;
			Vector3 pa = a - point;
			float c = Vector3.Dot(i, pa);
			if (c > 0f)
			{
				return Vector3.Dot(pa, pa);
			}
			Vector3 bp = point - b;
			if (Vector3.Dot(i, bp) > 0f)
			{
				return Vector3.Dot(bp, bp);
			}
			Vector3 vector = pa - i * (c / Vector3.Dot(i, i));
			return Vector3.Dot(vector, vector);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0002B4FF File Offset: 0x000296FF
		public static char ToLowerFast(char c)
		{
			if ((int)c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1)
			{
				return c;
			}
			return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-"[(int)c];
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0002B51D File Offset: 0x0002971D
		public static char ToUpperFast(char c)
		{
			if ((int)c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1)
			{
				return c;
			}
			return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[(int)c];
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0002B53B File Offset: 0x0002973B
		internal static uint ToUpperASCIIFast(uint c)
		{
			if ((ulong)c > (ulong)((long)("-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1)))
			{
				return c;
			}
			return (uint)"-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[(int)c];
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0002DB2C File Offset: 0x0002BD2C
		public static int GetHashCode(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return 0;
			}
			int hashCode = 0;
			for (int i = 0; i < s.Length; i++)
			{
				hashCode = ((hashCode << 5) + hashCode) ^ (int)TMP_TextUtilities.ToUpperFast(s[i]);
			}
			return hashCode;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0002DB6C File Offset: 0x0002BD6C
		public static int GetSimpleHashCode(string s)
		{
			int hashCode = 0;
			for (int i = 0; i < s.Length; i++)
			{
				hashCode = ((hashCode << 5) + hashCode) ^ (int)s[i];
			}
			return hashCode;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0002DB9C File Offset: 0x0002BD9C
		public static uint GetSimpleHashCodeLowercase(string s)
		{
			uint hashCode = 5381U;
			for (int i = 0; i < s.Length; i++)
			{
				hashCode = ((hashCode << 5) + hashCode) ^ (uint)TMP_TextUtilities.ToLowerFast(s[i]);
			}
			return hashCode;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0002DBD4 File Offset: 0x0002BDD4
		public static uint GetHashCodeCaseInSensitive(string s)
		{
			uint hashCode = 0U;
			for (int i = 0; i < s.Length; i++)
			{
				hashCode = ((hashCode << 5) + hashCode) ^ (uint)TMP_TextUtilities.ToUpperFast(s[i]);
			}
			return hashCode;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0002DC08 File Offset: 0x0002BE08
		public static int HexToInt(char hex)
		{
			switch (hex)
			{
			case '0':
				return 0;
			case '1':
				return 1;
			case '2':
				return 2;
			case '3':
				return 3;
			case '4':
				return 4;
			case '5':
				return 5;
			case '6':
				return 6;
			case '7':
				return 7;
			case '8':
				return 8;
			case '9':
				return 9;
			case ':':
			case ';':
			case '<':
			case '=':
			case '>':
			case '?':
			case '@':
				break;
			case 'A':
				return 10;
			case 'B':
				return 11;
			case 'C':
				return 12;
			case 'D':
				return 13;
			case 'E':
				return 14;
			case 'F':
				return 15;
			default:
				switch (hex)
				{
				case 'a':
					return 10;
				case 'b':
					return 11;
				case 'c':
					return 12;
				case 'd':
					return 13;
				case 'e':
					return 14;
				case 'f':
					return 15;
				}
				break;
			}
			return 15;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0002DCD8 File Offset: 0x0002BED8
		public static int StringHexToInt(string s)
		{
			int value = 0;
			for (int i = 0; i < s.Length; i++)
			{
				value += TMP_TextUtilities.HexToInt(s[i]) * (int)Mathf.Pow(16f, (float)(s.Length - 1 - i));
			}
			return value;
		}

		// Token: 0x04000584 RID: 1412
		private static Vector3[] m_rectWorldCorners = new Vector3[4];

		// Token: 0x04000585 RID: 1413
		private const string k_lookupStringL = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-";

		// Token: 0x04000586 RID: 1414
		private const string k_lookupStringU = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-";

		// Token: 0x020000A2 RID: 162
		private struct LineSegment
		{
			// Token: 0x060005FE RID: 1534 RVA: 0x0002DD2C File Offset: 0x0002BF2C
			public LineSegment(Vector3 p1, Vector3 p2)
			{
				this.Point1 = p1;
				this.Point2 = p2;
			}

			// Token: 0x04000587 RID: 1415
			public Vector3 Point1;

			// Token: 0x04000588 RID: 1416
			public Vector3 Point2;
		}
	}
}
