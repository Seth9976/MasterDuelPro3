using System;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002B6 RID: 694
	public class DebugUIHandlerObjectList : DebugUIHandlerField<DebugUI.ObjectListField>
	{
		// Token: 0x0600127A RID: 4730 RVA: 0x000465C7 File Offset: 0x000447C7
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Index = 0;
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x000465D7 File Offset: 0x000447D7
		public override void OnIncrement(bool fast)
		{
			this.m_Index++;
			this.UpdateValueLabel();
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x000465ED File Offset: 0x000447ED
		public override void OnDecrement(bool fast)
		{
			this.m_Index--;
			this.UpdateValueLabel();
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00046604 File Offset: 0x00044804
		public override void UpdateValueLabel()
		{
			string text = "Empty";
			Object[] values = this.m_Field.GetValue();
			if (values != null)
			{
				this.m_Index = Math.Clamp(this.m_Index, 0, values.Length - 1);
				text = values[this.m_Index].name;
			}
			base.SetLabelText(text);
		}

		// Token: 0x04000C52 RID: 3154
		private int m_Index;
	}
}
