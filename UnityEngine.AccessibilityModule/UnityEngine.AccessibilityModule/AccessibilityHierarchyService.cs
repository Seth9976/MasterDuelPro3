using System;
using System.Collections.Generic;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000013 RID: 19
	internal class AccessibilityHierarchyService : IService
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000031A5 File Offset: 0x000013A5
		internal AccessibilityHierarchy hierarchy
		{
			get
			{
				return this.m_Hierarchy;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000031AD File Offset: 0x000013AD
		public void Start()
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000031B0 File Offset: 0x000013B0
		public void Stop()
		{
			bool flag = this.m_Hierarchy == null;
			if (!flag)
			{
				this.RemoveActiveHierarchy(true);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000031D8 File Offset: 0x000013D8
		private void RemoveActiveHierarchy(bool notifyScreenChanged)
		{
			bool flag = this.m_Hierarchy == null;
			if (!flag)
			{
				this.m_Hierarchy.FreeNative();
				this.m_Hierarchy = null;
				if (notifyScreenChanged)
				{
					AssistiveSupport.notificationDispatcher.SendScreenChanged(null);
				}
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000321C File Offset: 0x0000141C
		internal bool TryGetNode(int id, out AccessibilityNode node)
		{
			node = null;
			return this.m_Hierarchy != null && this.m_Hierarchy.TryGetNode(id, out node);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000324C File Offset: 0x0000144C
		internal List<AccessibilityNode> GetRootNodes()
		{
			AccessibilityHierarchy hierarchy = this.m_Hierarchy;
			return (hierarchy != null) ? hierarchy.m_RootNodes : null;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003270 File Offset: 0x00001470
		internal bool TryGetNodeAt(float x, float y, out AccessibilityNode node)
		{
			node = null;
			return this.m_Hierarchy != null && this.m_Hierarchy.TryGetNodeAt(x, y, out node);
		}

		// Token: 0x04000062 RID: 98
		private AccessibilityHierarchy m_Hierarchy;
	}
}
