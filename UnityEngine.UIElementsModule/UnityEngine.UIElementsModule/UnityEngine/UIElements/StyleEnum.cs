using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements
{
	// Token: 0x020003CF RID: 975
	public struct StyleEnum<T> : IStyleValue<T>, IEquatable<StyleEnum<T>> where T : struct, IConvertible
	{
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x0006B380 File Offset: 0x00069580
		// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x0006B3AB File Offset: 0x000695AB
		public T value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(T);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x0006B3BC File Offset: 0x000695BC
		// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x0006B3D4 File Offset: 0x000695D4
		public StyleKeyword keyword
		{
			get
			{
				return this.m_Keyword;
			}
			set
			{
				this.m_Keyword = value;
			}
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0006B3DE File Offset: 0x000695DE
		public StyleEnum(T v)
		{
			this = new StyleEnum<T>(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0006B3EC File Offset: 0x000695EC
		public StyleEnum(StyleKeyword keyword)
		{
			this = new StyleEnum<T>(default(T), keyword);
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0006B40B File Offset: 0x0006960B
		internal StyleEnum(T v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0006B41C File Offset: 0x0006961C
		public static bool operator ==(StyleEnum<T> lhs, StyleEnum<T> rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && UnsafeUtility.EnumEquals<T>(lhs.m_Value, rhs.m_Value);
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x0006B450 File Offset: 0x00069650
		public static bool operator !=(StyleEnum<T> lhs, StyleEnum<T> rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x0006B46C File Offset: 0x0006966C
		public static implicit operator StyleEnum<T>(StyleKeyword keyword)
		{
			return new StyleEnum<T>(keyword);
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0006B484 File Offset: 0x00069684
		public static implicit operator StyleEnum<T>(T v)
		{
			return new StyleEnum<T>(v);
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0006B49C File Offset: 0x0006969C
		public bool Equals(StyleEnum<T> other)
		{
			return other == this;
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0006B4BC File Offset: 0x000696BC
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleEnum<T>)
			{
				StyleEnum<T> other = (StyleEnum<T>)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0006B4E8 File Offset: 0x000696E8
		public override int GetHashCode()
		{
			return (UnsafeUtility.EnumToInt<T>(this.m_Value) * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0006B514 File Offset: 0x00069714
		public override string ToString()
		{
			return this.DebugString<T>();
		}

		// Token: 0x04000C91 RID: 3217
		private T m_Value;

		// Token: 0x04000C92 RID: 3218
		private StyleKeyword m_Keyword;
	}
}
