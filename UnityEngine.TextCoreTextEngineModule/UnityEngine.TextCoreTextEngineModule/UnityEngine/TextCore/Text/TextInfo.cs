using System;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200005E RID: 94
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal class TextInfo
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0002DB9E File Offset: 0x0002BD9E
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0002DBA6 File Offset: 0x0002BDA6
		public VertexDataLayout vertexDataLayout { get; private set; }

		// Token: 0x060002A4 RID: 676 RVA: 0x0002DBAF File Offset: 0x0002BDAF
		public void RemoveFromCache()
		{
			Action action = this.removedFromCache;
			if (action != null)
			{
				action();
			}
			this.removedFromCache = null;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0002DBCC File Offset: 0x0002BDCC
		public TextInfo(VertexDataLayout vertexDataLayout)
		{
			this.vertexDataLayout = vertexDataLayout;
			this.textElementInfo = new TextElementInfo[4];
			this.wordInfo = new WordInfo[1];
			this.lineInfo = new LineInfo[1];
			this.pageInfo = new PageInfo[1];
			this.linkInfo = Array.Empty<LinkInfo>();
			this.meshInfo = Array.Empty<MeshInfo>();
			this.materialCount = 0;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0002DC40 File Offset: 0x0002BE40
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal void Clear()
		{
			this.characterCount = 0;
			this.spaceCount = 0;
			this.wordCount = 0;
			this.linkCount = 0;
			this.lineCount = 0;
			this.pageCount = 0;
			this.spriteCount = 0;
			this.hasMultipleColors = false;
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].vertexCount = 0;
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0002DCB4 File Offset: 0x0002BEB4
		internal void ClearMeshInfo(bool updateMesh)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].Clear(updateMesh);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0002DCEC File Offset: 0x0002BEEC
		internal void ClearLineInfo()
		{
			bool flag = this.lineInfo == null;
			if (flag)
			{
				this.lineInfo = new LineInfo[1];
			}
			for (int i = 0; i < this.lineInfo.Length; i++)
			{
				this.lineInfo[i].characterCount = 0;
				this.lineInfo[i].spaceCount = 0;
				this.lineInfo[i].wordCount = 0;
				this.lineInfo[i].controlCharacterCount = 0;
				this.lineInfo[i].ascender = TextInfo.s_InfinityVectorNegative.x;
				this.lineInfo[i].baseline = 0f;
				this.lineInfo[i].descender = TextInfo.s_InfinityVectorPositive.x;
				this.lineInfo[i].maxAdvance = 0f;
				this.lineInfo[i].marginLeft = 0f;
				this.lineInfo[i].marginRight = 0f;
				this.lineInfo[i].lineExtents.min = TextInfo.s_InfinityVectorPositive;
				this.lineInfo[i].lineExtents.max = TextInfo.s_InfinityVectorNegative;
				this.lineInfo[i].width = 0f;
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0002DE54 File Offset: 0x0002C054
		internal void ClearPageInfo()
		{
			bool flag = this.pageInfo == null;
			if (flag)
			{
				this.pageInfo = new PageInfo[2];
			}
			int length = this.pageInfo.Length;
			for (int i = 0; i < length; i++)
			{
				this.pageInfo[i].firstCharacterIndex = 0;
				this.pageInfo[i].lastCharacterIndex = 0;
				this.pageInfo[i].ascender = -32767f;
				this.pageInfo[i].baseLine = 0f;
				this.pageInfo[i].descender = 32767f;
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0002DEFC File Offset: 0x0002C0FC
		internal static void Resize<T>(ref T[] array, int size)
		{
			int newSize = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
			Array.Resize<T>(ref array, newSize);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0002DF2C File Offset: 0x0002C12C
		internal static void Resize<T>(ref T[] array, int size, bool isBlockAllocated)
		{
			if (isBlockAllocated)
			{
				size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
			}
			bool flag = size == array.Length;
			if (!flag)
			{
				Array.Resize<T>(ref array, size);
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0002DF70 File Offset: 0x0002C170
		public virtual Vector2 GetCursorPositionFromStringIndexUsingCharacterHeight(int index, Rect screenRect, float lineHeight, bool inverseYAxis = true)
		{
			Vector2 result = screenRect.position;
			bool flag = this.characterCount == 0;
			Vector2 vector;
			if (flag)
			{
				vector = (inverseYAxis ? new Vector2(0f, lineHeight) : result);
			}
			else
			{
				int validIndex = ((index >= this.characterCount) ? (this.characterCount - 1) : index);
				TextElementInfo character = this.textElementInfo[validIndex];
				float descender = character.descender;
				float vectorX = ((index >= this.characterCount) ? character.xAdvance : character.origin);
				result += (inverseYAxis ? new Vector2(vectorX, screenRect.height - descender) : new Vector2(vectorX, descender));
				vector = result;
			}
			return vector;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0002E01C File Offset: 0x0002C21C
		public Vector2 GetCursorPositionFromStringIndexUsingLineHeight(int index, Rect screenRect, float lineHeight, bool useXAdvance = false, bool inverseYAxis = true)
		{
			Vector2 result = screenRect.position;
			bool flag = this.characterCount == 0 || index < 0;
			Vector2 vector;
			if (flag)
			{
				vector = (inverseYAxis ? new Vector2(0f, lineHeight) : result);
			}
			else
			{
				bool flag2 = index >= this.characterCount;
				if (flag2)
				{
					index = this.characterCount - 1;
				}
				TextElementInfo character = this.textElementInfo[index];
				LineInfo line = this.lineInfo[character.lineNumber];
				bool flag3 = index >= this.characterCount - 1 || useXAdvance;
				if (flag3)
				{
					result += (inverseYAxis ? new Vector2(character.xAdvance, screenRect.height - line.descender) : new Vector2(character.xAdvance, line.descender));
					vector = result;
				}
				else
				{
					result += (inverseYAxis ? new Vector2(character.origin, screenRect.height - line.descender) : new Vector2(character.origin, line.descender));
					vector = result;
				}
			}
			return vector;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0002E12C File Offset: 0x0002C32C
		public int GetCursorIndexFromPosition(Vector2 position, Rect screenRect, bool inverseYAxis = true)
		{
			if (inverseYAxis)
			{
				position.y = screenRect.height - position.y;
			}
			int lineNumber = 0;
			bool flag = this.lineCount > 1;
			if (flag)
			{
				lineNumber = this.FindNearestLine(position);
			}
			int index = this.FindNearestCharacterOnLine(position, lineNumber, false);
			TextElementInfo cInfo = this.textElementInfo[index];
			Vector3 bl = cInfo.bottomLeft;
			Vector3 tr = cInfo.topRight;
			float insertPosition = (position.x - bl.x) / (tr.x - bl.x);
			return (insertPosition < 0.5f || cInfo.character == 10U) ? index : (index + 1);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0002E1D4 File Offset: 0x0002C3D4
		public int LineDownCharacterPosition(int originalPos)
		{
			bool flag = originalPos >= this.characterCount;
			int num;
			if (flag)
			{
				num = this.characterCount - 1;
			}
			else
			{
				TextElementInfo originChar = this.textElementInfo[originalPos];
				int originLine = originChar.lineNumber;
				bool flag2 = originLine + 1 >= this.lineCount;
				if (flag2)
				{
					num = this.characterCount - 1;
				}
				else
				{
					int endCharIdx = this.lineInfo[originLine + 1].lastCharacterIndex;
					int closest = -1;
					float distance = float.PositiveInfinity;
					float range = 0f;
					int i = this.lineInfo[originLine + 1].firstCharacterIndex;
					while (i < endCharIdx)
					{
						TextElementInfo currentChar = this.textElementInfo[i];
						float d = originChar.origin - currentChar.origin;
						float r = d / (currentChar.xAdvance - currentChar.origin);
						bool flag3 = r >= 0f && r <= 1f;
						if (flag3)
						{
							bool flag4 = r < 0.5f;
							if (flag4)
							{
								return i;
							}
							return i + 1;
						}
						else
						{
							d = Mathf.Abs(d);
							bool flag5 = d < distance;
							if (flag5)
							{
								closest = i;
								distance = d;
								range = r;
							}
							i++;
						}
					}
					bool flag6 = closest == -1;
					if (flag6)
					{
						num = endCharIdx;
					}
					else
					{
						bool flag7 = range < 0.5f;
						if (flag7)
						{
							num = closest;
						}
						else
						{
							num = closest + 1;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0002E34C File Offset: 0x0002C54C
		public int LineUpCharacterPosition(int originalPos)
		{
			bool flag = originalPos >= this.characterCount;
			if (flag)
			{
				originalPos--;
			}
			TextElementInfo originChar = this.textElementInfo[originalPos];
			int originLine = originChar.lineNumber;
			bool flag2 = originLine - 1 < 0;
			int num;
			if (flag2)
			{
				num = 0;
			}
			else
			{
				int endCharIdx = this.lineInfo[originLine].firstCharacterIndex - 1;
				int closest = -1;
				float distance = float.PositiveInfinity;
				float range = 0f;
				int i = this.lineInfo[originLine - 1].firstCharacterIndex;
				while (i < endCharIdx)
				{
					TextElementInfo currentChar = this.textElementInfo[i];
					float d = originChar.origin - currentChar.origin;
					float r = d / (currentChar.xAdvance - currentChar.origin);
					bool flag3 = r >= 0f && r <= 1f;
					if (flag3)
					{
						bool flag4 = r < 0.5f;
						if (flag4)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						d = Mathf.Abs(d);
						bool flag5 = d < distance;
						if (flag5)
						{
							closest = i;
							distance = d;
							range = r;
						}
						i++;
					}
				}
				bool flag6 = closest == -1;
				if (flag6)
				{
					num = endCharIdx;
				}
				else
				{
					bool flag7 = range < 0.5f;
					if (flag7)
					{
						num = closest;
					}
					else
					{
						num = closest + 1;
					}
				}
			}
			return num;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0002E4AC File Offset: 0x0002C6AC
		public int FindNearestLine(Vector2 position)
		{
			float distance = float.PositiveInfinity;
			int closest = -1;
			for (int i = 0; i < this.lineCount; i++)
			{
				LineInfo line = this.lineInfo[i];
				float ascender = line.ascender;
				float descender = line.descender;
				bool flag = ascender > position.y && descender < position.y;
				if (flag)
				{
					return i;
				}
				float d0 = Mathf.Abs(ascender - position.y);
				float d = Mathf.Abs(descender - position.y);
				float d2 = Mathf.Min(d0, d);
				bool flag2 = d2 < distance;
				if (flag2)
				{
					distance = d2;
					closest = i;
				}
			}
			return closest;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0002E568 File Offset: 0x0002C768
		public int FindNearestCharacterOnLine(Vector2 position, int line, bool visibleOnly)
		{
			bool flag = line >= this.lineInfo.Length || line < 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				int firstCharacter = this.lineInfo[line].firstCharacterIndex;
				int lastCharacter = this.lineInfo[line].lastCharacterIndex;
				float distanceSqr = float.PositiveInfinity;
				int closest = lastCharacter;
				for (int i = firstCharacter; i <= lastCharacter; i++)
				{
					TextElementInfo cInfo = this.textElementInfo[i];
					bool flag2 = visibleOnly && !cInfo.isVisible;
					if (!flag2)
					{
						bool flag3 = cInfo.character == 13U || cInfo.character == 10U;
						if (!flag3)
						{
							Vector3 bl = cInfo.bottomLeft;
							Vector3 tl = new Vector3(cInfo.bottomLeft.x, cInfo.topRight.y, 0f);
							Vector3 tr = cInfo.topRight;
							Vector3 br = new Vector3(cInfo.topRight.x, cInfo.bottomLeft.y, 0f);
							bool flag4 = TextInfo.PointIntersectRectangle(position, bl, tl, tr, br);
							if (flag4)
							{
								closest = i;
								break;
							}
							float dbl = TextInfo.DistanceToLine(bl, tl, position);
							float dtl = TextInfo.DistanceToLine(tl, tr, position);
							float dtr = TextInfo.DistanceToLine(tr, br, position);
							float dbr = TextInfo.DistanceToLine(br, bl, position);
							float d = ((dbl < dtl) ? dbl : dtl);
							d = ((d < dtr) ? d : dtr);
							d = ((d < dbr) ? d : dbr);
							bool flag5 = distanceSqr > d;
							if (flag5)
							{
								distanceSqr = d;
								closest = i;
							}
						}
					}
				}
				num = closest;
			}
			return num;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0002E738 File Offset: 0x0002C938
		public int FindIntersectingLink(Vector3 position, Rect screenRect, bool inverseYAxis = true)
		{
			if (inverseYAxis)
			{
				position.y = screenRect.height - position.y;
			}
			for (int i = 0; i < this.linkCount; i++)
			{
				LinkInfo link = this.linkInfo[i];
				bool isBeginRegion = false;
				Vector3 bl = Vector3.zero;
				Vector3 tl = Vector3.zero;
				Vector3 br = Vector3.zero;
				Vector3 tr = Vector3.zero;
				for (int j = 0; j < link.linkTextLength; j++)
				{
					int characterIndex = link.linkTextfirstCharacterIndex + j;
					TextElementInfo currentCharInfo = this.textElementInfo[characterIndex];
					int currentLine = currentCharInfo.lineNumber;
					bool flag = !isBeginRegion;
					if (flag)
					{
						isBeginRegion = true;
						bl = new Vector3(currentCharInfo.bottomLeft.x, currentCharInfo.descender, 0f);
						tl = new Vector3(currentCharInfo.bottomLeft.x, currentCharInfo.ascender, 0f);
						bool flag2 = link.linkTextLength == 1;
						if (flag2)
						{
							isBeginRegion = false;
							br = new Vector3(currentCharInfo.topRight.x, currentCharInfo.descender, 0f);
							tr = new Vector3(currentCharInfo.topRight.x, currentCharInfo.ascender, 0f);
							bool flag3 = TextInfo.PointIntersectRectangle(position, bl, tl, tr, br);
							if (flag3)
							{
								return i;
							}
						}
					}
					bool flag4 = isBeginRegion && j == link.linkTextLength - 1;
					if (flag4)
					{
						isBeginRegion = false;
						br = new Vector3(currentCharInfo.topRight.x, currentCharInfo.descender, 0f);
						tr = new Vector3(currentCharInfo.topRight.x, currentCharInfo.ascender, 0f);
						bool flag5 = TextInfo.PointIntersectRectangle(position, bl, tl, tr, br);
						if (flag5)
						{
							return i;
						}
					}
					else
					{
						bool flag6 = isBeginRegion && currentLine != this.textElementInfo[characterIndex + 1].lineNumber;
						if (flag6)
						{
							isBeginRegion = false;
							br = new Vector3(currentCharInfo.topRight.x, currentCharInfo.descender, 0f);
							tr = new Vector3(currentCharInfo.topRight.x, currentCharInfo.ascender, 0f);
							bool flag7 = TextInfo.PointIntersectRectangle(position, bl, tl, tr, br);
							if (flag7)
							{
								return i;
							}
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0002E9B0 File Offset: 0x0002CBB0
		public int GetCorrespondingStringIndex(int index)
		{
			bool flag = index <= 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				num = this.textElementInfo[index - 1].index + this.textElementInfo[index - 1].stringLength;
			}
			return num;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0002E9F8 File Offset: 0x0002CBF8
		public LineInfo GetLineInfoFromCharacterIndex(int index)
		{
			return this.lineInfo[this.GetLineNumber(index)];
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0002EA1C File Offset: 0x0002CC1C
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

		// Token: 0x060002B7 RID: 695 RVA: 0x0002EA90 File Offset: 0x0002CC90
		private static float DistanceToLine(Vector3 a, Vector3 b, Vector3 point)
		{
			Vector3 i = b - a;
			Vector3 pa = a - point;
			float c = Vector3.Dot(i, pa);
			bool flag = c > 0f;
			float num;
			if (flag)
			{
				num = Vector3.Dot(pa, pa);
			}
			else
			{
				Vector3 bp = point - b;
				bool flag2 = Vector3.Dot(i, bp) > 0f;
				if (flag2)
				{
					num = Vector3.Dot(bp, bp);
				}
				else
				{
					Vector3 e = pa - i * (c / Vector3.Dot(i, i));
					num = Vector3.Dot(e, e);
				}
			}
			return num;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0002EB1C File Offset: 0x0002CD1C
		public int GetLineNumber(int index)
		{
			bool flag = index <= 0;
			if (flag)
			{
				index = 0;
			}
			bool flag2 = index >= this.characterCount;
			if (flag2)
			{
				index = Mathf.Max(0, this.characterCount - 1);
			}
			return this.textElementInfo[index].lineNumber;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0002EB70 File Offset: 0x0002CD70
		public float GetLineHeight(int lineNumber)
		{
			bool flag = lineNumber <= 0;
			if (flag)
			{
				lineNumber = 0;
			}
			bool flag2 = lineNumber >= this.lineCount;
			if (flag2)
			{
				lineNumber = Mathf.Max(0, this.lineCount - 1);
			}
			return this.lineInfo[lineNumber].lineHeight;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0002EBC4 File Offset: 0x0002CDC4
		public float GetLineHeightFromCharacterIndex(int index)
		{
			bool flag = index <= 0;
			if (flag)
			{
				index = 0;
			}
			bool flag2 = index >= this.characterCount;
			if (flag2)
			{
				index = Mathf.Max(0, this.characterCount - 1);
			}
			return this.GetLineHeight(this.textElementInfo[index].lineNumber);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0002EC1C File Offset: 0x0002CE1C
		public float GetCharacterHeightFromIndex(int index)
		{
			bool flag = index <= 0;
			if (flag)
			{
				index = 0;
			}
			bool flag2 = index >= this.characterCount;
			if (flag2)
			{
				index = Mathf.Max(0, this.characterCount - 1);
			}
			TextElementInfo characterInfo = this.textElementInfo[index];
			return characterInfo.ascender - characterInfo.descender;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0002EC78 File Offset: 0x0002CE78
		public string Substring(int startIndex, int length)
		{
			bool flag = startIndex < 0 || startIndex + length > this.characterCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException();
			}
			StringBuilder result = new StringBuilder(length);
			for (int i = startIndex; i < startIndex + length; i++)
			{
				uint codePoint = this.textElementInfo[i].character;
				bool flag2 = codePoint >= 65536U && codePoint <= 1114111U;
				if (flag2)
				{
					uint highSurrogate = 55296U + (codePoint - 65536U >> 10);
					uint lowSurrogate = 56320U + ((codePoint - 65536U) & 1023U);
					result.Append((char)highSurrogate);
					result.Append((char)lowSurrogate);
				}
				else
				{
					result.Append((char)codePoint);
				}
			}
			return result.ToString();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0002ED48 File Offset: 0x0002CF48
		public int IndexOf(char value, int startIndex)
		{
			bool flag = startIndex < 0 || startIndex >= this.characterCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException();
			}
			for (int i = startIndex; i < this.characterCount; i++)
			{
				bool flag2 = this.textElementInfo[i].character == (uint)value;
				if (flag2)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0002EDB0 File Offset: 0x0002CFB0
		public int LastIndexOf(char value, int startIndex)
		{
			bool flag = startIndex < 0 || startIndex >= this.characterCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException();
			}
			for (int i = startIndex; i >= 0; i--)
			{
				bool flag2 = this.textElementInfo[i].character == (uint)value;
				if (flag2)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0400037A RID: 890
		private static Vector2 s_InfinityVectorPositive = new Vector2(32767f, 32767f);

		// Token: 0x0400037B RID: 891
		private static Vector2 s_InfinityVectorNegative = new Vector2(-32767f, -32767f);

		// Token: 0x0400037C RID: 892
		public int characterCount;

		// Token: 0x0400037D RID: 893
		public int spriteCount;

		// Token: 0x0400037E RID: 894
		public int spaceCount;

		// Token: 0x0400037F RID: 895
		public int wordCount;

		// Token: 0x04000380 RID: 896
		public int linkCount;

		// Token: 0x04000381 RID: 897
		public int lineCount;

		// Token: 0x04000382 RID: 898
		public int pageCount;

		// Token: 0x04000383 RID: 899
		public int materialCount;

		// Token: 0x04000384 RID: 900
		public TextElementInfo[] textElementInfo;

		// Token: 0x04000385 RID: 901
		public WordInfo[] wordInfo;

		// Token: 0x04000386 RID: 902
		public LinkInfo[] linkInfo;

		// Token: 0x04000387 RID: 903
		public LineInfo[] lineInfo;

		// Token: 0x04000388 RID: 904
		public PageInfo[] pageInfo;

		// Token: 0x04000389 RID: 905
		public MeshInfo[] meshInfo;

		// Token: 0x0400038A RID: 906
		public double lastTimeInCache;

		// Token: 0x0400038B RID: 907
		public Action removedFromCache;

		// Token: 0x0400038D RID: 909
		public bool hasMultipleColors = false;
	}
}
