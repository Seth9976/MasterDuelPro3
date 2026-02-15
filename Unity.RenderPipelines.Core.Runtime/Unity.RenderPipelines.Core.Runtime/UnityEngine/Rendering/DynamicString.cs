using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000049 RID: 73
	[DebuggerDisplay("Size = {size} Capacity = {capacity}")]
	public class DynamicString : DynamicArray<char>
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x000088E2 File Offset: 0x00006AE2
		public DynamicString()
		{
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000088EC File Offset: 0x00006AEC
		public DynamicString(string s)
			: base(s.Length, true)
		{
			for (int i = 0; i < s.Length; i++)
			{
				this.m_Array[i] = s[i];
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00008926 File Offset: 0x00006B26
		public DynamicString(int capacity)
			: base(capacity, false)
		{
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00008930 File Offset: 0x00006B30
		public void Append(string s)
		{
			int offset = base.size;
			base.Reserve(base.size + s.Length, true);
			for (int i = 0; i < s.Length; i++)
			{
				this.m_Array[offset + i] = s[i];
			}
			base.size += s.Length;
			base.BumpVersion();
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00008993 File Offset: 0x00006B93
		public void Append(DynamicString s)
		{
			base.AddRange(s);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000899C File Offset: 0x00006B9C
		public override string ToString()
		{
			return new string(this.m_Array, 0, base.size);
		}
	}
}
