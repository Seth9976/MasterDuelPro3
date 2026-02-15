using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Hierarchy
{
	// Token: 0x02000025 RID: 37
	[RequiredByNativeCode]
	[NativeHeader("Modules/HierarchyCore/Public/HierarchySearch.h")]
	[Serializable]
	public struct HierarchySearchFilter
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000396B File Offset: 0x00001B6B
		public static readonly ref HierarchySearchFilter Invalid
		{
			get
			{
				return ref HierarchySearchFilter.s_Invalid;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003972 File Offset: 0x00001B72
		public bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.Name);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003982 File Offset: 0x00001B82
		public readonly string Name { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000398A File Offset: 0x00001B8A
		public readonly string Value { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003992 File Offset: 0x00001B92
		public readonly float NumValue { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000399A File Offset: 0x00001B9A
		public readonly HierarchySearchFilterOperator Op { get; }

		// Token: 0x060000D2 RID: 210 RVA: 0x000039A4 File Offset: 0x00001BA4
		public static string ToString(HierarchySearchFilterOperator op)
		{
			string text;
			switch (op)
			{
			case HierarchySearchFilterOperator.Equal:
				text = "=";
				break;
			case HierarchySearchFilterOperator.Contains:
				text = ":";
				break;
			case HierarchySearchFilterOperator.Greater:
				text = ">";
				break;
			case HierarchySearchFilterOperator.GreaterOrEqual:
				text = ">=";
				break;
			case HierarchySearchFilterOperator.Lesser:
				text = "<";
				break;
			case HierarchySearchFilterOperator.LesserOrEqual:
				text = "<=";
				break;
			case HierarchySearchFilterOperator.NotEqual:
				text = "!=";
				break;
			case HierarchySearchFilterOperator.Not:
				text = "-";
				break;
			default:
				throw new NotImplementedException(string.Format("Cannot convert {0} to string", op));
			}
			return text;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003A38 File Offset: 0x00001C38
		public override string ToString()
		{
			string rhs = (float.IsNaN(this.NumValue) ? this.Value : this.NumValue.ToString());
			return this.Name + HierarchySearchFilter.ToString(this.Op) + HierarchySearchFilter.QuoteStringIfNeeded(rhs);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003A8C File Offset: 0x00001C8C
		internal static string QuoteStringIfNeeded(string s)
		{
			bool flag = s.Length > 0 && s.IndexOfAny(HierarchySearchFilter.s_WhiteSpaces) != -1 && s[0] != '"';
			string text;
			if (flag)
			{
				text = "\"" + s + "\"";
			}
			else
			{
				text = s;
			}
			return text;
		}

		// Token: 0x04000064 RID: 100
		private static readonly char[] s_WhiteSpaces = new char[] { ' ', '\t', '\n' };

		// Token: 0x04000065 RID: 101
		private static readonly HierarchySearchFilter s_Invalid;
	}
}
