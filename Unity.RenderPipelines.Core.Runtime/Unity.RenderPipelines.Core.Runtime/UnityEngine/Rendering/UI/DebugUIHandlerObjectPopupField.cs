using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002B7 RID: 695
	public class DebugUIHandlerObjectPopupField : DebugUIHandlerField<DebugUI.ObjectPopupField>
	{
		// Token: 0x0600127F RID: 4735 RVA: 0x0004665A File Offset: 0x0004485A
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Index = 0;
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0004666C File Offset: 0x0004486C
		private void ChangeSelectedObject()
		{
			if (this.m_Field == null)
			{
				return;
			}
			IEnumerable<Object> elements = this.m_Field.getObjects();
			if (elements == null)
			{
				return;
			}
			Object[] array = elements.ToArray<Object>();
			int count = array.Count<Object>();
			if (this.m_Index >= count)
			{
				this.m_Index = 0;
			}
			else if (this.m_Index < 0)
			{
				this.m_Index = count - 1;
			}
			Object newSelectedValue = array[this.m_Index];
			this.m_Field.SetValue(newSelectedValue);
			this.UpdateValueLabel();
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x000466E2 File Offset: 0x000448E2
		public override void OnIncrement(bool fast)
		{
			this.m_Index++;
			this.ChangeSelectedObject();
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x000466F8 File Offset: 0x000448F8
		public override void OnDecrement(bool fast)
		{
			this.m_Index--;
			this.ChangeSelectedObject();
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00046710 File Offset: 0x00044910
		public override void UpdateValueLabel()
		{
			Object selectedObject = this.m_Field.GetValue();
			string text = ((selectedObject != null) ? selectedObject.name : "Empty");
			base.SetLabelText(text);
		}

		// Token: 0x04000C53 RID: 3155
		private int m_Index;
	}
}
