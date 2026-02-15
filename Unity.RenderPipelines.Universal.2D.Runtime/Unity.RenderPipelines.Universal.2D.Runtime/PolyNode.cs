using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000009 RID: 9
	internal class PolyNode
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002304 File Offset: 0x00000504
		private bool IsHoleNode()
		{
			bool result = true;
			for (PolyNode node = this.m_Parent; node != null; node = node.m_Parent)
			{
				result = !result;
			}
			return result;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000232C File Offset: 0x0000052C
		public int ChildCount
		{
			get
			{
				return this.m_Childs.Count;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002339 File Offset: 0x00000539
		public List<IntPoint> Contour
		{
			get
			{
				return this.m_polygon;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002344 File Offset: 0x00000544
		internal void AddChild(PolyNode Child)
		{
			int cnt = this.m_Childs.Count;
			this.m_Childs.Add(Child);
			Child.m_Parent = this;
			Child.m_Index = cnt;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002377 File Offset: 0x00000577
		public PolyNode GetNext()
		{
			if (this.m_Childs.Count > 0)
			{
				return this.m_Childs[0];
			}
			return this.GetNextSiblingUp();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000239C File Offset: 0x0000059C
		internal PolyNode GetNextSiblingUp()
		{
			if (this.m_Parent == null)
			{
				return null;
			}
			if (this.m_Index == this.m_Parent.m_Childs.Count - 1)
			{
				return this.m_Parent.GetNextSiblingUp();
			}
			return this.m_Parent.m_Childs[this.m_Index + 1];
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000023F1 File Offset: 0x000005F1
		public List<PolyNode> Childs
		{
			get
			{
				return this.m_Childs;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000023F9 File Offset: 0x000005F9
		public PolyNode Parent
		{
			get
			{
				return this.m_Parent;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002401 File Offset: 0x00000601
		public bool IsHole
		{
			get
			{
				return this.IsHoleNode();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002409 File Offset: 0x00000609
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002411 File Offset: 0x00000611
		public bool IsOpen { get; set; }

		// Token: 0x0400000A RID: 10
		internal PolyNode m_Parent;

		// Token: 0x0400000B RID: 11
		internal List<IntPoint> m_polygon = new List<IntPoint>();

		// Token: 0x0400000C RID: 12
		internal int m_Index;

		// Token: 0x0400000D RID: 13
		internal JoinTypes m_jointype;

		// Token: 0x0400000E RID: 14
		internal EndTypes m_endtype;

		// Token: 0x0400000F RID: 15
		internal List<PolyNode> m_Childs = new List<PolyNode>();
	}
}
