using System;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200004A RID: 74
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEngine.IMGUIModule", "UnityEditor.GraphToolsFoundationModule" })]
	internal readonly struct RenderedText : IEquatable<RenderedText>, IEquatable<string>
	{
		// Token: 0x060001CD RID: 461 RVA: 0x00020F76 File Offset: 0x0001F176
		public RenderedText(string value)
		{
			this = new RenderedText(value, 0, (value != null) ? value.Length : 0, null);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00020F8F File Offset: 0x0001F18F
		public RenderedText(string value, string suffix)
		{
			this = new RenderedText(value, 0, (value != null) ? value.Length : 0, suffix);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00020FA8 File Offset: 0x0001F1A8
		public RenderedText(string value, int start, int length, string suffix = null)
		{
			bool flag = string.IsNullOrEmpty(value);
			if (flag)
			{
				start = 0;
				length = 0;
			}
			else
			{
				bool flag2 = start < 0;
				if (flag2)
				{
					start = 0;
				}
				else
				{
					bool flag3 = start >= value.Length;
					if (flag3)
					{
						start = value.Length;
						length = 0;
					}
				}
				bool flag4 = length < 0;
				if (flag4)
				{
					length = 0;
				}
				else
				{
					bool flag5 = length > value.Length - start;
					if (flag5)
					{
						length = value.Length - start;
					}
				}
			}
			this.value = value;
			this.valueStart = start;
			this.valueLength = length;
			this.suffix = suffix;
			this.repeat = '\0';
			this.repeatCount = 0;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0002104C File Offset: 0x0001F24C
		public RenderedText(char repeat, int repeatCount, string suffix = null)
		{
			bool flag = repeatCount < 0;
			if (flag)
			{
				repeatCount = 0;
			}
			this.value = null;
			this.valueStart = 0;
			this.valueLength = 0;
			this.suffix = suffix;
			this.repeat = repeat;
			this.repeatCount = repeatCount;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00021090 File Offset: 0x0001F290
		public int CharacterCount
		{
			get
			{
				int count = this.valueLength + this.repeatCount;
				bool flag = this.suffix != null;
				if (flag)
				{
					count += this.suffix.Length;
				}
				return count;
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000210CC File Offset: 0x0001F2CC
		public RenderedText.Enumerator GetEnumerator()
		{
			return new RenderedText.Enumerator(in this);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000210D4 File Offset: 0x0001F2D4
		public string CreateString()
		{
			char[] chars = new char[this.CharacterCount];
			int writeIndex = 0;
			foreach (char c in this)
			{
				chars[writeIndex++] = c;
			}
			return new string(chars);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00021120 File Offset: 0x0001F320
		public bool Equals(RenderedText other)
		{
			return this.value == other.value && this.valueStart == other.valueStart && this.valueLength == other.valueLength && this.suffix == other.suffix && this.repeat == other.repeat && this.repeatCount == other.repeatCount;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00021194 File Offset: 0x0001F394
		public bool Equals(string other)
		{
			int otherLength = ((other != null) ? other.Length : 0);
			int length = this.CharacterCount;
			bool flag = otherLength != length;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = otherLength == 0;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					int compIndex = 0;
					foreach (char c in this)
					{
						bool flag4 = c != other[compIndex++];
						if (flag4)
						{
							return false;
						}
					}
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00021218 File Offset: 0x0001F418
		public override bool Equals(object obj)
		{
			string otherString = obj as string;
			bool flag;
			if (otherString == null || !this.Equals(otherString))
			{
				if (obj is RenderedText)
				{
					RenderedText otherRenderedText = (RenderedText)obj;
					flag = this.Equals(otherRenderedText);
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0002125C File Offset: 0x0001F45C
		public override int GetHashCode()
		{
			return HashCode.Combine<string, int, int, string, char, int>(this.value, this.valueStart, this.valueLength, this.suffix, this.repeat, this.repeatCount);
		}

		// Token: 0x040002EC RID: 748
		public readonly string value;

		// Token: 0x040002ED RID: 749
		public readonly int valueStart;

		// Token: 0x040002EE RID: 750
		public readonly int valueLength;

		// Token: 0x040002EF RID: 751
		public readonly string suffix;

		// Token: 0x040002F0 RID: 752
		public readonly char repeat;

		// Token: 0x040002F1 RID: 753
		public readonly int repeatCount;

		// Token: 0x0200004B RID: 75
		public struct Enumerator
		{
			// Token: 0x1700005D RID: 93
			// (get) Token: 0x060001D8 RID: 472 RVA: 0x00021297 File Offset: 0x0001F497
			public char Current
			{
				get
				{
					return this.m_Current;
				}
			}

			// Token: 0x060001D9 RID: 473 RVA: 0x0002129F File Offset: 0x0001F49F
			public Enumerator(in RenderedText source)
			{
				this.m_Source = source;
				this.m_Stage = 0;
				this.m_StageIndex = 0;
				this.m_Current = '\0';
			}

			// Token: 0x060001DA RID: 474 RVA: 0x000212C4 File Offset: 0x0001F4C4
			public bool MoveNext()
			{
				bool flag = this.m_Stage == 0;
				if (flag)
				{
					bool flag2 = this.m_Source.value != null;
					if (flag2)
					{
						int start = this.m_Source.valueStart;
						int end = this.m_Source.valueStart + this.m_Source.valueLength;
						bool flag3 = this.m_StageIndex < start;
						if (flag3)
						{
							this.m_StageIndex = start;
						}
						bool flag4 = this.m_StageIndex < end;
						if (flag4)
						{
							this.m_Current = this.m_Source.value[this.m_StageIndex];
							this.m_StageIndex++;
							return true;
						}
					}
					this.m_Stage = 1;
					this.m_StageIndex = 0;
				}
				bool flag5 = this.m_Stage == 1;
				if (flag5)
				{
					bool flag6 = this.m_StageIndex < this.m_Source.repeatCount;
					if (flag6)
					{
						this.m_Current = this.m_Source.repeat;
						this.m_StageIndex++;
						return true;
					}
					this.m_Stage = 2;
					this.m_StageIndex = 0;
				}
				bool flag7 = this.m_Stage == 2;
				if (flag7)
				{
					bool flag8 = this.m_Source.suffix != null && this.m_StageIndex < this.m_Source.suffix.Length;
					if (flag8)
					{
						this.m_Current = this.m_Source.suffix[this.m_StageIndex];
						this.m_StageIndex++;
						return true;
					}
					this.m_Stage = 3;
					this.m_StageIndex = 0;
				}
				return false;
			}

			// Token: 0x040002F2 RID: 754
			private readonly RenderedText m_Source;

			// Token: 0x040002F3 RID: 755
			private int m_Stage;

			// Token: 0x040002F4 RID: 756
			private int m_StageIndex;

			// Token: 0x040002F5 RID: 757
			private char m_Current;
		}
	}
}
