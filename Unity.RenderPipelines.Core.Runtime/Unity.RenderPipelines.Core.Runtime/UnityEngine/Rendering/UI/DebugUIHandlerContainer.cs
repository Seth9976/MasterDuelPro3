using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002A7 RID: 679
	public class DebugUIHandlerContainer : MonoBehaviour
	{
		// Token: 0x06001225 RID: 4645 RVA: 0x00045538 File Offset: 0x00043738
		internal DebugUIHandlerWidget GetFirstItem()
		{
			if (this.contentHolder.childCount == 0)
			{
				return null;
			}
			List<DebugUIHandlerWidget> items = this.GetActiveChildren();
			if (items.Count == 0)
			{
				return null;
			}
			return items[0];
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0004556C File Offset: 0x0004376C
		internal DebugUIHandlerWidget GetLastItem()
		{
			if (this.contentHolder.childCount == 0)
			{
				return null;
			}
			List<DebugUIHandlerWidget> items = this.GetActiveChildren();
			if (items.Count == 0)
			{
				return null;
			}
			return items[items.Count - 1];
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x000455A8 File Offset: 0x000437A8
		internal bool IsDirectChild(DebugUIHandlerWidget widget)
		{
			return this.contentHolder.childCount != 0 && this.GetActiveChildren().Count((DebugUIHandlerWidget x) => x == widget) > 0;
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x000455EC File Offset: 0x000437EC
		private List<DebugUIHandlerWidget> GetActiveChildren()
		{
			List<DebugUIHandlerWidget> list = new List<DebugUIHandlerWidget>();
			foreach (object obj in this.contentHolder)
			{
				Transform t = (Transform)obj;
				if (t.gameObject.activeInHierarchy)
				{
					DebugUIHandlerWidget c = t.GetComponent<DebugUIHandlerWidget>();
					if (c != null)
					{
						list.Add(c);
					}
				}
			}
			return list;
		}

		// Token: 0x04000C20 RID: 3104
		[SerializeField]
		public RectTransform contentHolder;
	}
}
