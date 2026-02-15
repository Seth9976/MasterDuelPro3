using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
	/// <summary>Provides functionality to split a string into text elements and to iterate through those text elements.</summary>
	// Token: 0x020006BA RID: 1722
	[ComVisible(true)]
	[Serializable]
	public class StringInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.StringInfo" /> class. </summary>
		// Token: 0x0600362B RID: 13867 RVA: 0x000D108A File Offset: 0x000CF28A
		public StringInfo()
			: this("")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.StringInfo" /> class to a specified string.</summary>
		/// <param name="value">A string to initialize this <see cref="T:System.Globalization.StringInfo" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x0600362C RID: 13868 RVA: 0x000D1097 File Offset: 0x000CF297
		public StringInfo(string value)
		{
			this.String = value;
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000D10A6 File Offset: 0x000CF2A6
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_str = string.Empty;
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000D10B3 File Offset: 0x000CF2B3
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_str.Length == 0)
			{
				this.m_indexes = null;
			}
		}

		/// <summary>Indicates whether the current <see cref="T:System.Globalization.StringInfo" /> object is equal to a specified object.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is a <see cref="T:System.Globalization.StringInfo" /> object and its <see cref="P:System.Globalization.StringInfo.String" /> property equals the <see cref="P:System.Globalization.StringInfo.String" /> property of this <see cref="T:System.Globalization.StringInfo" /> object; otherwise, false.</returns>
		/// <param name="value">An object.</param>
		// Token: 0x0600362F RID: 13871 RVA: 0x000D10CC File Offset: 0x000CF2CC
		[ComVisible(false)]
		public override bool Equals(object value)
		{
			StringInfo stringInfo = value as StringInfo;
			return stringInfo != null && this.m_str.Equals(stringInfo.m_str);
		}

		/// <summary>Calculates a hash code for the value of the current <see cref="T:System.Globalization.StringInfo" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code based on the string value of this <see cref="T:System.Globalization.StringInfo" /> object.</returns>
		// Token: 0x06003630 RID: 13872 RVA: 0x000D10F6 File Offset: 0x000CF2F6
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return this.m_str.GetHashCode();
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06003631 RID: 13873 RVA: 0x000D1103 File Offset: 0x000CF303
		private int[] Indexes
		{
			get
			{
				if (this.m_indexes == null && 0 < this.String.Length)
				{
					this.m_indexes = StringInfo.ParseCombiningCharacters(this.String);
				}
				return this.m_indexes;
			}
		}

		/// <summary>Gets or sets the value of the current <see cref="T:System.Globalization.StringInfo" /> object.</summary>
		/// <returns>The string that is the value of the current <see cref="T:System.Globalization.StringInfo" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is null.</exception>
		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06003632 RID: 13874 RVA: 0x000D1132 File Offset: 0x000CF332
		// (set) Token: 0x06003633 RID: 13875 RVA: 0x000D113A File Offset: 0x000CF33A
		public string String
		{
			get
			{
				return this.m_str;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("String", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.m_str = value;
				this.m_indexes = null;
			}
		}

		/// <summary>Gets the number of text elements in the current <see cref="T:System.Globalization.StringInfo" /> object.</summary>
		/// <returns>The number of base characters, surrogate pairs, and combining character sequences in this <see cref="T:System.Globalization.StringInfo" /> object.</returns>
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06003634 RID: 13876 RVA: 0x000D1162 File Offset: 0x000CF362
		public int LengthInTextElements
		{
			get
			{
				if (this.Indexes == null)
				{
					return 0;
				}
				return this.Indexes.Length;
			}
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000D1178 File Offset: 0x000CF378
		internal static int GetCurrentTextElementLen(string str, int index, int len, ref UnicodeCategory ucCurrent, ref int currentCharCount)
		{
			if (index + currentCharCount == len)
			{
				return currentCharCount;
			}
			int num;
			UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, index + currentCharCount, out num);
			if (CharUnicodeInfo.IsCombiningCategory(unicodeCategory) && !CharUnicodeInfo.IsCombiningCategory(ucCurrent) && ucCurrent != UnicodeCategory.Format && ucCurrent != UnicodeCategory.Control && ucCurrent != UnicodeCategory.OtherNotAssigned && ucCurrent != UnicodeCategory.Surrogate)
			{
				int num2 = index;
				for (index += currentCharCount + num; index < len; index += num)
				{
					unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, index, out num);
					if (!CharUnicodeInfo.IsCombiningCategory(unicodeCategory))
					{
						ucCurrent = unicodeCategory;
						currentCharCount = num;
						break;
					}
				}
				return index - num2;
			}
			int num3 = currentCharCount;
			ucCurrent = unicodeCategory;
			currentCharCount = num;
			return num3;
		}

		/// <summary>Returns the indexes of each base character, high surrogate, or control character within the specified string.</summary>
		/// <returns>An array of integers that contains the zero-based indexes of each base character, high surrogate, or control character within the specified string.</returns>
		/// <param name="str">The string to search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		// Token: 0x06003636 RID: 13878 RVA: 0x000D1208 File Offset: 0x000CF408
		public static int[] ParseCombiningCharacters(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int length = str.Length;
			int[] array = new int[length];
			if (length == 0)
			{
				return array;
			}
			int num = 0;
			int i = 0;
			int num2;
			UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, 0, out num2);
			while (i < length)
			{
				array[num++] = i;
				i += StringInfo.GetCurrentTextElementLen(str, i, length, ref unicodeCategory, ref num2);
			}
			if (num < length)
			{
				int[] array2 = new int[num];
				Array.Copy(array, array2, num);
				return array2;
			}
			return array;
		}

		// Token: 0x04001D33 RID: 7475
		[OptionalField(VersionAdded = 2)]
		private string m_str;

		// Token: 0x04001D34 RID: 7476
		[NonSerialized]
		private int[] m_indexes;
	}
}
