using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000175 RID: 373
	internal class AncestorFilter
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x00035A07 File Offset: 0x00033C07
		private void AddHash(int hash)
		{
			this.m_HashStack.Push(hash);
			this.m_CountingBloomFilter.InsertHash((uint)hash);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00035A24 File Offset: 0x00033C24
		public unsafe bool IsCandidate(StyleComplexSelector complexSel)
		{
			int i = 0;
			while (i < 4)
			{
				bool flag = *((ref complexSel.ancestorHashes.hashes.FixedElementField) + (IntPtr)i * 4) == 0;
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					bool flag3 = !this.m_CountingBloomFilter.ContainsHash((uint)(*((ref complexSel.ancestorHashes.hashes.FixedElementField) + (IntPtr)i * 4)));
					if (!flag3)
					{
						i++;
						continue;
					}
					flag2 = false;
				}
				return flag2;
			}
			return true;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00035A98 File Offset: 0x00033C98
		public void PushElement(VisualElement element)
		{
			int rememberCount = this.m_HashStack.Count;
			this.AddHash(element.typeName.GetHashCode() * 13);
			bool flag = !string.IsNullOrEmpty(element.name);
			if (flag)
			{
				this.AddHash(element.name.GetHashCode() * 17);
			}
			foreach (string cls in element.classList)
			{
				this.AddHash(cls.GetHashCode() * 19);
			}
			this.m_HashStack.Push(this.m_HashStack.Count - rememberCount);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00035B5C File Offset: 0x00033D5C
		public void PopElement()
		{
			int elemCount = this.m_HashStack.Peek();
			this.m_HashStack.Pop();
			while (elemCount > 0)
			{
				int hash = this.m_HashStack.Peek();
				this.m_CountingBloomFilter.RemoveHash((uint)hash);
				this.m_HashStack.Pop();
				elemCount--;
			}
		}

		// Token: 0x0400071D RID: 1821
		private CountingBloomFilter m_CountingBloomFilter;

		// Token: 0x0400071E RID: 1822
		private Stack<int> m_HashStack = new Stack<int>(100);
	}
}
