using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002B9 RID: 697
	internal class DebugUIHandlerPersistentCanvas : MonoBehaviour
	{
		// Token: 0x06001291 RID: 4753 RVA: 0x00046930 File Offset: 0x00044B30
		internal void Toggle(DebugUI.Value widget, string displayName = null)
		{
			int index = this.m_Items.FindIndex((DebugUIHandlerValue x) => x.GetWidget() == widget);
			if (index > -1)
			{
				CoreUtils.Destroy(this.m_Items[index].gameObject);
				this.m_Items.RemoveAt(index);
				return;
			}
			DebugUIHandlerValue uiHandler = Object.Instantiate<RectTransform>(this.valuePrefab, this.panel, false).gameObject.GetComponent<DebugUIHandlerValue>();
			uiHandler.SetWidget(widget);
			uiHandler.nameLabel.text = (string.IsNullOrEmpty(displayName) ? widget.displayName : displayName);
			this.m_Items.Add(uiHandler);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x000469E0 File Offset: 0x00044BE0
		internal void Toggle(DebugUI.ValueTuple widget, int? forceTupleIndex = null)
		{
			DebugUI.ValueTuple val = this.m_ValueTupleWidgets.Find((DebugUI.ValueTuple x) => x == widget);
			int tupleIndex = ((val != null) ? val.pinnedElementIndex : (-1));
			if (val != null)
			{
				this.m_ValueTupleWidgets.Remove(val);
				this.Toggle(widget.values[tupleIndex], null);
			}
			if (forceTupleIndex != null)
			{
				tupleIndex = forceTupleIndex.Value;
			}
			if (tupleIndex + 1 < widget.numElements)
			{
				widget.pinnedElementIndex = tupleIndex + 1;
				string displayName = widget.displayName;
				if (widget.parent is DebugUI.Foldout)
				{
					string[] columnLabels = (widget.parent as DebugUI.Foldout).columnLabels;
					if (columnLabels != null && widget.pinnedElementIndex < columnLabels.Length)
					{
						displayName = displayName + " (" + columnLabels[widget.pinnedElementIndex] + ")";
					}
				}
				this.Toggle(widget.values[widget.pinnedElementIndex], displayName);
				this.m_ValueTupleWidgets.Add(widget);
				return;
			}
			widget.pinnedElementIndex = -1;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00046B1A File Offset: 0x00044D1A
		internal bool IsEmpty()
		{
			return this.m_Items.Count == 0;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00046B2C File Offset: 0x00044D2C
		internal void Clear()
		{
			foreach (DebugUIHandlerValue debugUIHandlerValue in this.m_Items)
			{
				CoreUtils.Destroy(debugUIHandlerValue.gameObject);
			}
			this.m_Items.Clear();
		}

		// Token: 0x04000C5D RID: 3165
		public RectTransform panel;

		// Token: 0x04000C5E RID: 3166
		public RectTransform valuePrefab;

		// Token: 0x04000C5F RID: 3167
		private List<DebugUIHandlerValue> m_Items = new List<DebugUIHandlerValue>();

		// Token: 0x04000C60 RID: 3168
		private List<DebugUI.ValueTuple> m_ValueTupleWidgets = new List<DebugUI.ValueTuple>();
	}
}
