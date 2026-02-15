using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000264 RID: 612
	internal static class StringHelpers
	{
		// Token: 0x06001628 RID: 5672 RVA: 0x00063FB8 File Offset: 0x000621B8
		public static string Escape(this string str, string chars = "\n\t\r\\\"", string replacements = "ntr\\\"")
		{
			if (str == null)
			{
				return null;
			}
			bool hasCharacterThatNeedsEscaping = false;
			foreach (char ch in str)
			{
				if (chars.Contains(ch))
				{
					hasCharacterThatNeedsEscaping = true;
					break;
				}
			}
			if (!hasCharacterThatNeedsEscaping)
			{
				return str;
			}
			StringBuilder builder = new StringBuilder();
			foreach (char ch2 in str)
			{
				int index = chars.IndexOf(ch2);
				if (index == -1)
				{
					builder.Append(ch2);
				}
				else
				{
					builder.Append('\\');
					builder.Append(replacements[index]);
				}
			}
			return builder.ToString();
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00064058 File Offset: 0x00062258
		public static string Unescape(this string str, string chars = "ntr\\\"", string replacements = "\n\t\r\\\"")
		{
			if (str == null)
			{
				return str;
			}
			if (!str.Contains('\\'))
			{
				return str;
			}
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < str.Length; i++)
			{
				char ch = str[i];
				if (ch == '\\' && i < str.Length - 2)
				{
					i++;
					ch = str[i];
					int index = chars.IndexOf(ch);
					if (index != -1)
					{
						builder.Append(replacements[index]);
					}
					else
					{
						builder.Append(ch);
					}
				}
				else
				{
					builder.Append(ch);
				}
			}
			return builder.ToString();
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000640E5 File Offset: 0x000622E5
		public static bool Contains(this string str, char ch)
		{
			return str != null && str.IndexOf(ch) != -1;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x000640F9 File Offset: 0x000622F9
		public static bool Contains(this string str, string text, StringComparison comparison)
		{
			return str != null && str.IndexOf(text, comparison) != -1;
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00064110 File Offset: 0x00062310
		public static string GetPlural(this string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str == "Mouse")
			{
				return "Mice";
			}
			if (str == "mouse")
			{
				return "mice";
			}
			if (str == "Axis")
			{
				return "Axes";
			}
			if (!(str == "axis"))
			{
				return str + "s";
			}
			return "axes";
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00064184 File Offset: 0x00062384
		public static string NicifyMemorySize(long numBytes)
		{
			if (numBytes > 1073741824L)
			{
				long gb = numBytes / 1073741824L;
				float remainder = (float)(numBytes % 1073741824L) / 1f;
				return string.Format("{0} GB", (float)gb + remainder);
			}
			if (numBytes > 1048576L)
			{
				long mb = numBytes / 1048576L;
				float remainder2 = (float)(numBytes % 1048576L) / 1f;
				return string.Format("{0} MB", (float)mb + remainder2);
			}
			if (numBytes > 1024L)
			{
				long kb = numBytes / 1024L;
				float remainder3 = (float)(numBytes % 1024L) / 1f;
				return string.Format("{0} KB", (float)kb + remainder3);
			}
			return string.Format("{0} Bytes", numBytes);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00064248 File Offset: 0x00062448
		public static bool FromNicifiedMemorySize(string text, out long result, long defaultMultiplier = 1L)
		{
			text = text.Trim();
			long multiplier = defaultMultiplier;
			if (text.EndsWith("MB", StringComparison.InvariantCultureIgnoreCase))
			{
				multiplier = 1048576L;
				text = text.Substring(0, text.Length - 2);
			}
			else if (text.EndsWith("GB", StringComparison.InvariantCultureIgnoreCase))
			{
				multiplier = 1073741824L;
				text = text.Substring(0, text.Length - 2);
			}
			else if (text.EndsWith("KB", StringComparison.InvariantCultureIgnoreCase))
			{
				multiplier = 1024L;
				text = text.Substring(0, text.Length - 2);
			}
			else if (text.EndsWith("Bytes", StringComparison.InvariantCultureIgnoreCase))
			{
				multiplier = 1L;
				text = text.Substring(0, text.Length - "Bytes".Length);
			}
			long num;
			if (!long.TryParse(text, out num))
			{
				result = 0L;
				return false;
			}
			result = num * multiplier;
			return true;
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x00064318 File Offset: 0x00062518
		public static int CountOccurrences(this string str, char ch)
		{
			if (str == null)
			{
				return 0;
			}
			int length = str.Length;
			int index = 0;
			int count = 0;
			while (index < length)
			{
				int nextIndex = str.IndexOf(ch, index);
				if (nextIndex == -1)
				{
					break;
				}
				count++;
				index = nextIndex + 1;
			}
			return count;
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x00064351 File Offset: 0x00062551
		public static IEnumerable<Substring> Tokenize(this string str)
		{
			int pos = 0;
			int length = str.Length;
			while (pos < length)
			{
				while (pos < length && char.IsWhiteSpace(str[pos]))
				{
					pos++;
				}
				if (pos == length)
				{
					break;
				}
				if (str[pos] == '"')
				{
					pos++;
					int endPos = pos;
					while (endPos < length && str[endPos] != '"')
					{
						int num;
						if (str[endPos] == '\\' && endPos < length - 1)
						{
							num = endPos + 1;
							endPos = num;
						}
						num = endPos + 1;
						endPos = num;
					}
					yield return new Substring(str, pos, endPos - pos);
					pos = endPos + 1;
				}
				else
				{
					int endPos = pos;
					while (endPos < length && !char.IsWhiteSpace(str[endPos]))
					{
						int num = endPos + 1;
						endPos = num;
					}
					yield return new Substring(str, pos, endPos - pos);
					pos = endPos;
				}
			}
			yield break;
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x00064361 File Offset: 0x00062561
		public static IEnumerable<string> Split(this string str, Func<char, bool> predicate)
		{
			if (string.IsNullOrEmpty(str))
			{
				yield break;
			}
			int length = str.Length;
			int position = 0;
			while (position < length)
			{
				char ch = str[position];
				if (predicate(ch))
				{
					int num = position + 1;
					position = num;
				}
				else
				{
					int startPosition = position;
					int num = position + 1;
					for (position = num; position < length; position = num)
					{
						ch = str[position];
						if (predicate(ch))
						{
							break;
						}
						num = position + 1;
					}
					int endPosition = position;
					yield return str.Substring(startPosition, endPosition - startPosition);
				}
			}
			yield break;
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x00064378 File Offset: 0x00062578
		public static string Join<TValue>(string separator, params TValue[] values)
		{
			return StringHelpers.Join<TValue>(values, separator);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x00064384 File Offset: 0x00062584
		public static string Join<TValue>(IEnumerable<TValue> values, string separator)
		{
			string firstValue = null;
			int valueCount = 0;
			StringBuilder result = null;
			foreach (TValue value in values)
			{
				if (value != null)
				{
					string str = value.ToString();
					if (!string.IsNullOrEmpty(str))
					{
						valueCount++;
						if (valueCount == 1)
						{
							firstValue = str;
						}
						else
						{
							if (valueCount == 2)
							{
								result = new StringBuilder();
								result.Append(firstValue);
							}
							result.Append(separator);
							result.Append(str);
						}
					}
				}
			}
			if (valueCount == 0)
			{
				return null;
			}
			if (valueCount == 1)
			{
				return firstValue;
			}
			return result.ToString();
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00064430 File Offset: 0x00062630
		public static string MakeUniqueName<TExisting>(string baseName, IEnumerable<TExisting> existingSet, Func<TExisting, string> getNameFunc)
		{
			if (getNameFunc == null)
			{
				throw new ArgumentNullException("getNameFunc");
			}
			if (existingSet == null)
			{
				return baseName;
			}
			string name = baseName;
			string nameLowerCase = name.ToLower();
			bool nameIsUnique = false;
			int namesTried = 1;
			if (baseName.Length > 0)
			{
				int lastDigit = baseName.Length;
				while (lastDigit > 0 && char.IsDigit(baseName[lastDigit - 1]))
				{
					lastDigit--;
				}
				if (lastDigit != baseName.Length)
				{
					namesTried = int.Parse(baseName.Substring(lastDigit)) + 1;
					baseName = baseName.Substring(0, lastDigit);
				}
			}
			while (!nameIsUnique)
			{
				nameIsUnique = true;
				foreach (TExisting existing in existingSet)
				{
					if (getNameFunc(existing).ToLower() == nameLowerCase)
					{
						name = string.Format("{0}{1}", baseName, namesTried);
						nameLowerCase = name.ToLower();
						nameIsUnique = false;
						namesTried++;
						break;
					}
				}
			}
			return name;
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0006452C File Offset: 0x0006272C
		public static bool CharacterSeparatedListsHaveAtLeastOneCommonElement(string firstList, string secondList, char separator)
		{
			if (firstList == null)
			{
				throw new ArgumentNullException("firstList");
			}
			if (secondList == null)
			{
				throw new ArgumentNullException("secondList");
			}
			int indexInFirst = 0;
			int lengthOfFirst = firstList.Length;
			int lengthOfSecond = secondList.Length;
			while (indexInFirst < lengthOfFirst)
			{
				if (firstList[indexInFirst] == separator)
				{
					indexInFirst++;
				}
				int endIndexInFirst = indexInFirst + 1;
				while (endIndexInFirst < lengthOfFirst && firstList[endIndexInFirst] != separator)
				{
					endIndexInFirst++;
				}
				int lengthOfCurrentInFirst = endIndexInFirst - indexInFirst;
				int endIndexInSecond;
				for (int indexInSecond = 0; indexInSecond < lengthOfSecond; indexInSecond = endIndexInSecond + 1)
				{
					if (secondList[indexInSecond] == separator)
					{
						indexInSecond++;
					}
					endIndexInSecond = indexInSecond + 1;
					while (endIndexInSecond < lengthOfSecond && secondList[endIndexInSecond] != separator)
					{
						endIndexInSecond++;
					}
					int lengthOfCurrentInSecond = endIndexInSecond - indexInSecond;
					if (lengthOfCurrentInFirst == lengthOfCurrentInSecond)
					{
						int startIndexInFirst = indexInFirst;
						int startIndexInSecond = indexInSecond;
						bool isMatch = true;
						for (int i = 0; i < lengthOfCurrentInFirst; i++)
						{
							char c = firstList[startIndexInFirst + i];
							char second = secondList[startIndexInSecond + i];
							if (char.ToLowerInvariant(c) != char.ToLowerInvariant(second))
							{
								isMatch = false;
								break;
							}
						}
						if (isMatch)
						{
							return true;
						}
					}
				}
				indexInFirst = endIndexInFirst + 1;
			}
			return false;
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x00064640 File Offset: 0x00062840
		public static int ParseInt(string str, int pos)
		{
			int multiply = 1;
			int result = 0;
			int length = str.Length;
			while (pos < length)
			{
				int digit = (int)(str[pos] - '0');
				if (digit < 0 || digit > 9)
				{
					break;
				}
				result = result * multiply + digit;
				multiply *= 10;
				pos++;
			}
			return result;
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x00064684 File Offset: 0x00062884
		public static bool WriteStringToBuffer(string text, IntPtr buffer, int bufferSizeInCharacters)
		{
			uint offset = 0U;
			return StringHelpers.WriteStringToBuffer(text, buffer, bufferSizeInCharacters, ref offset);
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x000646A0 File Offset: 0x000628A0
		public unsafe static bool WriteStringToBuffer(string text, IntPtr buffer, int bufferSizeInCharacters, ref uint offset)
		{
			if (buffer == IntPtr.Zero)
			{
				throw new ArgumentNullException("buffer");
			}
			int length = (string.IsNullOrEmpty(text) ? 0 : text.Length);
			if (length > 65535)
			{
				throw new ArgumentException(string.Format("String exceeds max size of {0} characters", ushort.MaxValue), "text");
			}
			long endOffset = (long)((ulong)offset + (ulong)((long)(2 * length)) + 4UL);
			if (endOffset > (long)bufferSizeInCharacters)
			{
				return false;
			}
			byte* ptr = (byte*)(void*)buffer + offset;
			*(short*)ptr = (short)((ushort)length);
			ptr += 2;
			int i = 0;
			while (i < length)
			{
				*(short*)ptr = (short)text[i];
				i++;
				ptr += 2;
			}
			offset = (uint)endOffset;
			return true;
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00064744 File Offset: 0x00062944
		public static string ReadStringFromBuffer(IntPtr buffer, int bufferSize)
		{
			uint offset = 0U;
			return StringHelpers.ReadStringFromBuffer(buffer, bufferSize, ref offset);
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0006475C File Offset: 0x0006295C
		public unsafe static string ReadStringFromBuffer(IntPtr buffer, int bufferSize, ref uint offset)
		{
			if (buffer == IntPtr.Zero)
			{
				throw new ArgumentNullException("buffer");
			}
			if ((ulong)(offset + 4U) > (ulong)((long)bufferSize))
			{
				return null;
			}
			byte* ptr = (byte*)(void*)buffer + offset;
			ushort length = *(ushort*)ptr;
			ptr += 2;
			if (length == 0)
			{
				return null;
			}
			long endOffset = (long)((ulong)offset + (ulong)((long)(2 * length)) + 4UL);
			if (endOffset > (long)bufferSize)
			{
				return null;
			}
			string text = Marshal.PtrToStringUni(new IntPtr((void*)ptr), (int)length);
			offset = (uint)endOffset;
			return text;
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x000647C6 File Offset: 0x000629C6
		public static bool IsPrintable(this char ch)
		{
			return !char.IsControl(ch) && !char.IsWhiteSpace(ch);
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000647DC File Offset: 0x000629DC
		public static string WithAllWhitespaceStripped(this string str)
		{
			StringBuilder buffer = new StringBuilder();
			foreach (char ch in str)
			{
				if (!char.IsWhiteSpace(ch))
				{
					buffer.Append(ch);
				}
			}
			return buffer.ToString();
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00064820 File Offset: 0x00062A20
		public static bool InvariantEqualsIgnoreCase(this string left, string right)
		{
			if (string.IsNullOrEmpty(left))
			{
				return string.IsNullOrEmpty(right);
			}
			return string.Equals(left, right, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0006483C File Offset: 0x00062A3C
		public static string ExpandTemplateString(string template, Func<string, string> mapFunc)
		{
			if (string.IsNullOrEmpty(template))
			{
				throw new ArgumentNullException("template");
			}
			if (mapFunc == null)
			{
				throw new ArgumentNullException("mapFunc");
			}
			StringBuilder buffer = new StringBuilder();
			int length = template.Length;
			for (int i = 0; i < length; i++)
			{
				char ch = template[i];
				if (ch != '{')
				{
					buffer.Append(ch);
				}
				else
				{
					i++;
					int tokenStartPos = i;
					while (i < length && template[i] != '}')
					{
						i++;
					}
					string token = template.Substring(tokenStartPos, i - tokenStartPos);
					string mapped = mapFunc(token);
					buffer.Append(mapped);
				}
			}
			return buffer.ToString();
		}
	}
}
