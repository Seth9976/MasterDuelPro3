using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002BD RID: 701
	public class DebugUIHandlerRow : DebugUIHandlerFoldout
	{
		// Token: 0x060012A1 RID: 4769 RVA: 0x00046CD6 File Offset: 0x00044ED6
		protected override void OnEnable()
		{
			this.m_Timer = 0f;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00046CE4 File Offset: 0x00044EE4
		private GameObject GetChild(int index)
		{
			if (index < 0)
			{
				return null;
			}
			if (base.gameObject.transform != null)
			{
				Transform firstChild = base.gameObject.transform.GetChild(1);
				if (firstChild != null && firstChild.childCount > index)
				{
					return firstChild.GetChild(index).gameObject;
				}
			}
			return null;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00046D3C File Offset: 0x00044F3C
		private bool TryGetChild(int index, out GameObject child)
		{
			child = this.GetChild(index);
			return child != null;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00046D50 File Offset: 0x00044F50
		private bool IsActive(DebugUI.Table table, int index, GameObject child)
		{
			if (!table.GetColumnVisibility(index))
			{
				return false;
			}
			Transform valueChild = child.transform.Find("Value");
			Text text;
			return !(valueChild != null) || !valueChild.TryGetComponent<Text>(out text) || !string.IsNullOrEmpty(text.text);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00046D9C File Offset: 0x00044F9C
		protected void Update()
		{
			DebugUI.Table.Row row = base.CastWidget<DebugUI.Table.Row>();
			DebugUI.Table table = row.parent as DebugUI.Table;
			float refreshRate = 0.1f;
			bool refreshRow = this.m_Timer >= refreshRate;
			if (refreshRow)
			{
				this.m_Timer -= refreshRate;
			}
			this.m_Timer += Time.deltaTime;
			for (int i = 0; i < row.children.Count; i++)
			{
				GameObject child;
				if (this.TryGetChild(i, out child))
				{
					bool active = this.IsActive(table, i, child);
					if (child != null)
					{
						child.SetActive(active);
					}
					if (active && refreshRow)
					{
						DebugUIHandlerColor color;
						if (child.TryGetComponent<DebugUIHandlerColor>(out color))
						{
							color.UpdateColor();
						}
						DebugUIHandlerToggle toggle;
						if (child.TryGetComponent<DebugUIHandlerToggle>(out toggle))
						{
							toggle.UpdateValueLabel();
						}
						DebugUIHandlerObjectList list;
						if (child.TryGetComponent<DebugUIHandlerObjectList>(out list))
						{
							list.UpdateValueLabel();
						}
					}
				}
			}
			DebugUIHandlerWidget itemWidget = this.GetChild(0).GetComponent<DebugUIHandlerWidget>();
			DebugUIHandlerWidget previous = null;
			for (int j = 0; j < row.children.Count; j++)
			{
				itemWidget.previousUIHandler = previous;
				GameObject child2;
				if (this.TryGetChild(j, out child2))
				{
					if (this.IsActive(table, j, child2))
					{
						previous = itemWidget;
					}
					bool found = false;
					for (int k = j + 1; k < row.children.Count; k++)
					{
						GameObject innerChild;
						if (this.TryGetChild(k, out innerChild) && this.IsActive(table, k, innerChild))
						{
							DebugUIHandlerWidget childWidget = child2.GetComponent<DebugUIHandlerWidget>();
							itemWidget.nextUIHandler = childWidget;
							itemWidget = childWidget;
							j = k - 1;
							found = true;
							break;
						}
					}
					if (!found)
					{
						itemWidget.nextUIHandler = null;
						return;
					}
				}
			}
		}

		// Token: 0x04000C68 RID: 3176
		private float m_Timer;
	}
}
