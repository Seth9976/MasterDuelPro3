using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200043B RID: 1083
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class StyleVariableContext
	{
		// Token: 0x06001F3E RID: 7998 RVA: 0x00071A7C File Offset: 0x0006FC7C
		public void Add(StyleVariable sv)
		{
			StyleVariableContext.<>c__DisplayClass7_0 CS$<>8__locals1;
			CS$<>8__locals1.hash = sv.GetHashCode();
			int hashIndex = this.m_SortedHash.BinarySearch(CS$<>8__locals1.hash);
			bool flag = hashIndex >= 0;
			if (flag)
			{
				int variableIndex = this.m_Variables.Count - 1;
				bool flag2 = this.m_UnsortedHash[variableIndex] == CS$<>8__locals1.hash;
				if (flag2)
				{
					return;
				}
				for (variableIndex--; variableIndex >= 0; variableIndex--)
				{
					bool flag3 = this.m_UnsortedHash[variableIndex] == CS$<>8__locals1.hash;
					if (flag3)
					{
						this.m_VariableHash ^= StyleVariableContext.<Add>g__ComputeOrderSensitiveHash|7_0(variableIndex, ref CS$<>8__locals1);
						this.m_Variables.RemoveAt(variableIndex);
						this.m_UnsortedHash.RemoveAt(variableIndex);
						break;
					}
				}
			}
			else
			{
				this.m_SortedHash.Insert(~hashIndex, CS$<>8__locals1.hash);
			}
			this.m_VariableHash ^= StyleVariableContext.<Add>g__ComputeOrderSensitiveHash|7_0(this.m_Variables.Count, ref CS$<>8__locals1);
			this.m_Variables.Add(sv);
			this.m_UnsortedHash.Add(CS$<>8__locals1.hash);
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00071BA8 File Offset: 0x0006FDA8
		public void AddInitialRange(StyleVariableContext other)
		{
			bool flag = other.m_Variables.Count > 0;
			if (flag)
			{
				Debug.Assert(this.m_Variables.Count == 0);
				this.m_VariableHash = other.m_VariableHash;
				this.m_Variables.AddRange(other.m_Variables);
				this.m_SortedHash.AddRange(other.m_SortedHash);
				this.m_UnsortedHash.AddRange(other.m_UnsortedHash);
			}
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x00071C20 File Offset: 0x0006FE20
		public void Clear()
		{
			bool flag = this.m_Variables.Count > 0;
			if (flag)
			{
				this.m_Variables.Clear();
				this.m_VariableHash = 0;
				this.m_SortedHash.Clear();
				this.m_UnsortedHash.Clear();
			}
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x00071C6D File Offset: 0x0006FE6D
		public StyleVariableContext()
		{
			this.m_Variables = new List<StyleVariable>();
			this.m_VariableHash = 0;
			this.m_SortedHash = new List<int>();
			this.m_UnsortedHash = new List<int>();
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x00071CA0 File Offset: 0x0006FEA0
		public StyleVariableContext(StyleVariableContext other)
		{
			this.m_Variables = new List<StyleVariable>(other.m_Variables);
			this.m_VariableHash = other.m_VariableHash;
			this.m_SortedHash = new List<int>(other.m_SortedHash);
			this.m_UnsortedHash = new List<int>(other.m_UnsortedHash);
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x00071CF4 File Offset: 0x0006FEF4
		public bool TryFindVariable(string name, out StyleVariable v)
		{
			for (int i = this.m_Variables.Count - 1; i >= 0; i--)
			{
				bool flag = this.m_Variables[i].name == name;
				if (flag)
				{
					v = this.m_Variables[i];
					return true;
				}
			}
			v = default(StyleVariable);
			return false;
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00071D64 File Offset: 0x0006FF64
		public int GetVariableHash()
		{
			return this.m_VariableHash;
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x00071D88 File Offset: 0x0006FF88
		[CompilerGenerated]
		internal static int <Add>g__ComputeOrderSensitiveHash|7_0(int index, ref StyleVariableContext.<>c__DisplayClass7_0 A_1)
		{
			return (index + 1) * A_1.hash;
		}

		// Token: 0x04000DD4 RID: 3540
		public static readonly StyleVariableContext none = new StyleVariableContext();

		// Token: 0x04000DD5 RID: 3541
		private int m_VariableHash;

		// Token: 0x04000DD6 RID: 3542
		private List<StyleVariable> m_Variables;

		// Token: 0x04000DD7 RID: 3543
		private List<int> m_SortedHash;

		// Token: 0x04000DD8 RID: 3544
		private List<int> m_UnsortedHash;
	}
}
