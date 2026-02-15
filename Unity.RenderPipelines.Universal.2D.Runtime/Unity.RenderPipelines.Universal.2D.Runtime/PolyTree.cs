using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000008 RID: 8
	internal class PolyTree : PolyNode
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002250 File Offset: 0x00000450
		public void Clear()
		{
			for (int i = 0; i < this.m_AllPolys.Count; i++)
			{
				this.m_AllPolys[i] = null;
			}
			this.m_AllPolys.Clear();
			this.m_Childs.Clear();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002296 File Offset: 0x00000496
		public PolyNode GetFirst()
		{
			if (this.m_Childs.Count > 0)
			{
				return this.m_Childs[0];
			}
			return null;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000022B4 File Offset: 0x000004B4
		public int Total
		{
			get
			{
				int result = this.m_AllPolys.Count;
				if (result > 0 && this.m_Childs[0] != this.m_AllPolys[0])
				{
					result--;
				}
				return result;
			}
		}

		// Token: 0x04000009 RID: 9
		internal List<PolyNode> m_AllPolys = new List<PolyNode>();
	}
}
