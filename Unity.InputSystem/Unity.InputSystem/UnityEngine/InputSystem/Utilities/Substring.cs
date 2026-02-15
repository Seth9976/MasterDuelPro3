using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000267 RID: 615
	internal struct Substring : IComparable<Substring>, IEquatable<Substring>
	{
		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x00064CA7 File Offset: 0x00062EA7
		public bool isEmpty
		{
			get
			{
				return this.m_Length == 0;
			}
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00064CB2 File Offset: 0x00062EB2
		public Substring(string str)
		{
			this.m_String = str;
			this.m_Index = 0;
			if (str != null)
			{
				this.m_Length = str.Length;
				return;
			}
			this.m_Length = 0;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00064CD9 File Offset: 0x00062ED9
		public Substring(string str, int index, int length)
		{
			this.m_String = str;
			this.m_Index = index;
			this.m_Length = length;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00064CF0 File Offset: 0x00062EF0
		public Substring(string str, int index)
		{
			this.m_String = str;
			this.m_Index = index;
			this.m_Length = str.Length - index;
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00064D10 File Offset: 0x00062F10
		public override bool Equals(object obj)
		{
			if (obj is Substring)
			{
				Substring other = (Substring)obj;
				return this.Equals(other);
			}
			string str = obj as string;
			return str != null && this.Equals(str);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00064D48 File Offset: 0x00062F48
		public bool Equals(string other)
		{
			if (string.IsNullOrEmpty(other))
			{
				return this.m_Length == 0;
			}
			if (other.Length != this.m_Length)
			{
				return false;
			}
			for (int i = 0; i < this.m_Length; i++)
			{
				if (other[i] != this.m_String[this.m_Index + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00064DA7 File Offset: 0x00062FA7
		public bool Equals(Substring other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00064DB3 File Offset: 0x00062FB3
		public bool Equals(InternedString other)
		{
			return this.length == other.length && string.Compare(this.m_String, this.m_Index, other.ToString(), 0, this.length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00064DEF File Offset: 0x00062FEF
		public int CompareTo(Substring other)
		{
			return Substring.Compare(this, other, StringComparison.CurrentCulture);
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00064E00 File Offset: 0x00063000
		public static int Compare(Substring left, Substring right, StringComparison comparison)
		{
			if (left.m_Length == right.m_Length)
			{
				return string.Compare(left.m_String, left.m_Index, right.m_String, right.m_Index, left.m_Length, comparison);
			}
			if (left.m_Length < right.m_Length)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00064E54 File Offset: 0x00063054
		public bool StartsWith(string str)
		{
			if (str.Length > this.length)
			{
				return false;
			}
			for (int i = 0; i < str.Length; i++)
			{
				if (this.m_String[this.m_Index + i] != str[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x00064EA1 File Offset: 0x000630A1
		public string Substr(int index = 0, int length = -1)
		{
			if (length < 0)
			{
				length = this.length - index;
			}
			return this.m_String.Substring(this.m_Index + index, length);
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00064EC5 File Offset: 0x000630C5
		public override string ToString()
		{
			if (this.m_String == null)
			{
				return string.Empty;
			}
			return this.m_String.Substring(this.m_Index, this.m_Length);
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x00064EEC File Offset: 0x000630EC
		public override int GetHashCode()
		{
			if (this.m_String == null)
			{
				return 0;
			}
			if (this.m_Index == 0 && this.m_Length == this.m_String.Length)
			{
				return this.m_String.GetHashCode();
			}
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00064F3B File Offset: 0x0006313B
		public static bool operator ==(Substring a, Substring b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00064F45 File Offset: 0x00063145
		public static bool operator !=(Substring a, Substring b)
		{
			return !a.Equals(b);
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00064F52 File Offset: 0x00063152
		public static bool operator ==(Substring a, InternedString b)
		{
			return a.Equals(b);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00064F5C File Offset: 0x0006315C
		public static bool operator !=(Substring a, InternedString b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x00064F69 File Offset: 0x00063169
		public static bool operator ==(InternedString a, Substring b)
		{
			return b.Equals(a);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00064F73 File Offset: 0x00063173
		public static bool operator !=(InternedString a, Substring b)
		{
			return !b.Equals(a);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00064F80 File Offset: 0x00063180
		public static implicit operator Substring(string s)
		{
			return new Substring(s);
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x00064F88 File Offset: 0x00063188
		public int length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x00064F90 File Offset: 0x00063190
		public int index
		{
			get
			{
				return this.m_Index;
			}
		}

		// Token: 0x17000601 RID: 1537
		public char this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_Length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.m_String[this.m_Index + index];
			}
		}

		// Token: 0x04000CBF RID: 3263
		private readonly string m_String;

		// Token: 0x04000CC0 RID: 3264
		private readonly int m_Index;

		// Token: 0x04000CC1 RID: 3265
		private readonly int m_Length;
	}
}
