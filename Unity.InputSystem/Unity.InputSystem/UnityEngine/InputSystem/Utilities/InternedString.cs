using System;
using System.Globalization;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000240 RID: 576
	public struct InternedString : IEquatable<InternedString>, IComparable<InternedString>
	{
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x0005F93E File Offset: 0x0005DB3E
		public int length
		{
			get
			{
				string stringLowerCase = this.m_StringLowerCase;
				if (stringLowerCase == null)
				{
					return 0;
				}
				return stringLowerCase.Length;
			}
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0005F951 File Offset: 0x0005DB51
		public InternedString(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				this.m_StringOriginalCase = null;
				this.m_StringLowerCase = null;
				return;
			}
			this.m_StringOriginalCase = string.Intern(text);
			this.m_StringLowerCase = string.Intern(text.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0005F98C File Offset: 0x0005DB8C
		public bool IsEmpty()
		{
			return this.m_StringLowerCase == null;
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0005F997 File Offset: 0x0005DB97
		public string ToLower()
		{
			return this.m_StringLowerCase;
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0005F9A0 File Offset: 0x0005DBA0
		public override bool Equals(object obj)
		{
			if (obj is InternedString)
			{
				InternedString other = (InternedString)obj;
				return this.Equals(other);
			}
			string str = obj as string;
			if (str == null)
			{
				return false;
			}
			if (this.m_StringLowerCase == null)
			{
				return string.IsNullOrEmpty(str);
			}
			return string.Equals(this.m_StringLowerCase, str.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0005F9F5 File Offset: 0x0005DBF5
		public bool Equals(InternedString other)
		{
			return this.m_StringLowerCase == other.m_StringLowerCase;
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0005FA05 File Offset: 0x0005DC05
		public int CompareTo(InternedString other)
		{
			return string.Compare(this.m_StringLowerCase, other.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0005FA19 File Offset: 0x0005DC19
		public override int GetHashCode()
		{
			if (this.m_StringLowerCase == null)
			{
				return 0;
			}
			return this.m_StringLowerCase.GetHashCode();
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0005FA30 File Offset: 0x0005DC30
		public override string ToString()
		{
			return this.m_StringOriginalCase ?? string.Empty;
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0005FA41 File Offset: 0x0005DC41
		public static bool operator ==(InternedString a, InternedString b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0005FA4B File Offset: 0x0005DC4B
		public static bool operator !=(InternedString a, InternedString b)
		{
			return !a.Equals(b);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0005FA58 File Offset: 0x0005DC58
		public static bool operator ==(InternedString a, string b)
		{
			return string.Compare(a.m_StringLowerCase, b.ToLower(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0005FA74 File Offset: 0x0005DC74
		public static bool operator !=(InternedString a, string b)
		{
			return string.Compare(a.m_StringLowerCase, b.ToLower(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase) != 0;
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0005FA90 File Offset: 0x0005DC90
		public static bool operator ==(string a, InternedString b)
		{
			return string.Compare(a.ToLower(CultureInfo.InvariantCulture), b.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0005FAAC File Offset: 0x0005DCAC
		public static bool operator !=(string a, InternedString b)
		{
			return string.Compare(a.ToLower(CultureInfo.InvariantCulture), b.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase) != 0;
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0005FAC8 File Offset: 0x0005DCC8
		public static bool operator <(InternedString left, InternedString right)
		{
			return string.Compare(left.m_StringLowerCase, right.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase) < 0;
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0005FADF File Offset: 0x0005DCDF
		public static bool operator >(InternedString left, InternedString right)
		{
			return string.Compare(left.m_StringLowerCase, right.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase) > 0;
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0005FAF6 File Offset: 0x0005DCF6
		public static implicit operator string(InternedString str)
		{
			return str.ToString();
		}

		// Token: 0x04000C4C RID: 3148
		private readonly string m_StringOriginalCase;

		// Token: 0x04000C4D RID: 3149
		private readonly string m_StringLowerCase;
	}
}
