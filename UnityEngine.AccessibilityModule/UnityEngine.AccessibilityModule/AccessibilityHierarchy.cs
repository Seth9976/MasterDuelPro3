using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Accessibility
{
	// Token: 0x0200000F RID: 15
	public class AccessibilityHierarchy
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002B34 File Offset: 0x00000D34
		public bool TryGetNode(int id, out AccessibilityNode node)
		{
			return this.m_Nodes.TryGetValue(id, out node);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002B54 File Offset: 0x00000D54
		internal void FreeNative()
		{
			foreach (AccessibilityNode rootNode in this.m_RootNodes)
			{
				rootNode.FreeNative(true);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public bool TryGetNodeAt(float horizontalPosition, float verticalPosition, out AccessibilityNode node)
		{
			Vector2 position = new Vector2(horizontalPosition, verticalPosition);
			node = AccessibilityHierarchy.<TryGetNodeAt>g__FindNodeContainingPoint|27_0(this.m_RootNodes, position);
			return node != null;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002BE0 File Offset: 0x00000DE0
		[CompilerGenerated]
		internal static AccessibilityNode <TryGetNodeAt>g__FindNodeContainingPoint|27_0(IList<AccessibilityNode> nodes, Vector2 pos)
		{
			for (int i = nodes.Count - 1; i >= 0; i--)
			{
				AccessibilityNode curNode = nodes[i];
				bool flag = curNode.state.HasFlag(AccessibilityState.Disabled);
				if (!flag)
				{
					AccessibilityNode childNodeContainingPoint = AccessibilityHierarchy.<TryGetNodeAt>g__FindNodeContainingPoint|27_0(curNode.childList, pos);
					bool flag2 = childNodeContainingPoint != null;
					AccessibilityNode accessibilityNode;
					if (flag2)
					{
						accessibilityNode = childNodeContainingPoint;
					}
					else
					{
						bool flag3 = curNode.isActive && curNode.frame.Contains(pos);
						if (!flag3)
						{
							goto IL_0074;
						}
						accessibilityNode = curNode;
					}
					return accessibilityNode;
				}
				IL_0074:;
			}
			return null;
		}

		// Token: 0x0400004A RID: 74
		internal List<AccessibilityNode> m_RootNodes;

		// Token: 0x0400004B RID: 75
		private readonly IDictionary<int, AccessibilityNode> m_Nodes;
	}
}
