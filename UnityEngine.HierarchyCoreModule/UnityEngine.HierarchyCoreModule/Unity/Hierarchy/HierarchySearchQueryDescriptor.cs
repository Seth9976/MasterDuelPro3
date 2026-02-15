using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Hierarchy
{
	// Token: 0x02000026 RID: 38
	[RequiredByNativeCode]
	[NativeHeader("Modules/HierarchyCore/Public/HierarchySearch.h")]
	[NativeAsStruct]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class HierarchySearchQueryDescriptor
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003AF7 File Offset: 0x00001CF7
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00003AFF File Offset: 0x00001CFF
		public HierarchySearchFilter[] SystemFilters { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003B08 File Offset: 0x00001D08
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00003B10 File Offset: 0x00001D10
		public HierarchySearchFilter[] Filters { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003B19 File Offset: 0x00001D19
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00003B21 File Offset: 0x00001D21
		public string[] TextValues { get; set; }

		// Token: 0x1700002A RID: 42
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00003B2A File Offset: 0x00001D2A
		public bool Strict
		{
			[CompilerGenerated]
			set
			{
				this.<Strict>k__BackingField = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00003B33 File Offset: 0x00001D33
		public bool Invalid
		{
			[CompilerGenerated]
			set
			{
				this.<Invalid>k__BackingField = value;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003B3C File Offset: 0x00001D3C
		public unsafe HierarchySearchQueryDescriptor(HierarchySearchFilter[] filters = null, string[] textValues = null)
		{
			filters = filters ?? new HierarchySearchFilter[0];
			textValues = textValues ?? new string[0];
			this.Filters = HierarchySearchQueryDescriptor.Where<HierarchySearchFilter>(filters, (HierarchySearchFilter f) => !HierarchySearchQueryDescriptor.s_SystemFilters.Contains(f.Name));
			this.SystemFilters = HierarchySearchQueryDescriptor.Where<HierarchySearchFilter>(filters, (HierarchySearchFilter f) => HierarchySearchQueryDescriptor.s_SystemFilters.Contains(f.Name));
			this.TextValues = textValues;
			HierarchySearchFilter strictFilter = *HierarchySearchFilter.Invalid;
			foreach (HierarchySearchFilter f2 in this.SystemFilters)
			{
				bool flag = f2.Name == "strict";
				if (flag)
				{
					strictFilter = f2;
					break;
				}
			}
			this.Invalid = false;
			this.Strict = !strictFilter.IsValid || strictFilter.Value == "true";
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003C40 File Offset: 0x00001E40
		public override string ToString()
		{
			return this.BuildQuery();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003C58 File Offset: 0x00001E58
		[VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
		internal string BuildFilterQuery()
		{
			return string.Join<HierarchySearchFilter>(" ", this.Filters);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003C7C File Offset: 0x00001E7C
		internal string BuildSystemFilterQuery()
		{
			return string.Join<HierarchySearchFilter>(" ", this.SystemFilters);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003CA0 File Offset: 0x00001EA0
		internal string BuildTextQuery()
		{
			string[] textValues = new string[this.TextValues.Length];
			for (int i = 0; i < textValues.Length; i++)
			{
				textValues[i] = HierarchySearchFilter.QuoteStringIfNeeded(this.TextValues[i]);
			}
			return string.Join(" ", textValues);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003CF0 File Offset: 0x00001EF0
		internal string BuildQuery()
		{
			string query = "";
			bool flag = this.SystemFilters.Length != 0;
			if (flag)
			{
				query += this.BuildSystemFilterQuery();
			}
			bool flag2 = this.Filters.Length != 0;
			if (flag2)
			{
				bool flag3 = query.Length > 0;
				if (flag3)
				{
					query += " ";
				}
				query += this.BuildFilterQuery();
			}
			bool flag4 = this.TextValues.Length != 0;
			if (flag4)
			{
				bool flag5 = query.Length > 0;
				if (flag5)
				{
					query += " ";
				}
				query += this.BuildTextQuery();
			}
			return query;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003D98 File Offset: 0x00001F98
		private static T[] Where<T>(IEnumerable<T> src, Func<T, bool> pred)
		{
			int count = 0;
			foreach (T e in src)
			{
				bool flag = pred(e);
				if (flag)
				{
					count++;
				}
			}
			T[] a = new T[count];
			int i = 0;
			foreach (T e2 in src)
			{
				bool flag2 = pred(e2);
				if (flag2)
				{
					a[i++] = e2;
				}
			}
			return a;
		}

		// Token: 0x0400006A RID: 106
		private static readonly HashSet<string> s_SystemFilters = new HashSet<string>(new string[] { "nodetype", "strict" });

		// Token: 0x0400006B RID: 107
		private static readonly HierarchySearchQueryDescriptor s_Empty = new HierarchySearchQueryDescriptor(null, null);

		// Token: 0x0400006C RID: 108
		private static readonly HierarchySearchQueryDescriptor s_InvalidQuery = new HierarchySearchQueryDescriptor(null, null)
		{
			Invalid = true
		};
	}
}
