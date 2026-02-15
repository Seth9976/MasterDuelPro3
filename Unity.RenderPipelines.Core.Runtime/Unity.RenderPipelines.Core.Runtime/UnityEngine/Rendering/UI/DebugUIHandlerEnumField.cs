using System;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002A9 RID: 681
	public class DebugUIHandlerEnumField : DebugUIHandlerField<DebugUI.EnumField>
	{
		// Token: 0x0600122C RID: 4652 RVA: 0x0004567C File Offset: 0x0004387C
		public override void OnIncrement(bool fast)
		{
			if (this.m_Field.enumValues.Length == 0)
			{
				return;
			}
			int[] array = this.m_Field.enumValues;
			int index = this.m_Field.currentIndex;
			if (index == array.Length - 1)
			{
				index = 0;
			}
			else if (fast)
			{
				int[] separators = this.m_Field.quickSeparators;
				if (separators == null)
				{
					this.m_Field.InitQuickSeparators();
					separators = this.m_Field.quickSeparators;
				}
				int idxSup = 0;
				while (idxSup < separators.Length && index + 1 > separators[idxSup])
				{
					idxSup++;
				}
				if (idxSup == separators.Length)
				{
					index = 0;
				}
				else
				{
					index = separators[idxSup];
				}
			}
			else
			{
				index++;
			}
			this.m_Field.SetValue(array[index]);
			this.m_Field.currentIndex = index;
			this.UpdateValueLabel();
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00045730 File Offset: 0x00043930
		public override void OnDecrement(bool fast)
		{
			if (this.m_Field.enumValues.Length == 0)
			{
				return;
			}
			int[] array = this.m_Field.enumValues;
			int index = this.m_Field.currentIndex;
			if (index == 0)
			{
				if (fast)
				{
					int[] separators = this.m_Field.quickSeparators;
					if (separators == null)
					{
						this.m_Field.InitQuickSeparators();
						separators = this.m_Field.quickSeparators;
					}
					index = separators[separators.Length - 1];
				}
				else
				{
					index = array.Length - 1;
				}
			}
			else if (fast)
			{
				int[] separators2 = this.m_Field.quickSeparators;
				if (separators2 == null)
				{
					this.m_Field.InitQuickSeparators();
					separators2 = this.m_Field.quickSeparators;
				}
				int idxInf = separators2.Length - 1;
				while (idxInf > 0 && index <= separators2[idxInf])
				{
					idxInf--;
				}
				index = separators2[idxInf];
			}
			else
			{
				index--;
			}
			this.m_Field.SetValue(array[index]);
			this.m_Field.currentIndex = index;
			this.UpdateValueLabel();
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00045814 File Offset: 0x00043A14
		public override void UpdateValueLabel()
		{
			int index = this.m_Field.currentIndex;
			if (index < 0)
			{
				index = 0;
			}
			base.SetLabelText(this.m_Field.enumNames[index].text);
		}
	}
}
