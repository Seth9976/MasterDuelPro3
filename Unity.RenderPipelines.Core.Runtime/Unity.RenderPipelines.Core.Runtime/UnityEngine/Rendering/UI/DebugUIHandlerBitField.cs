using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002A0 RID: 672
	public class DebugUIHandlerBitField : DebugUIHandlerWidget
	{
		// Token: 0x060011E7 RID: 4583 RVA: 0x0004443C File Offset: 0x0004263C
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.BitField>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			int toggleIndex = 0;
			foreach (GUIContent enumName in this.m_Field.enumNames)
			{
				if (toggleIndex < this.toggles.Count)
				{
					DebugUIHandlerIndirectToggle debugUIHandlerIndirectToggle = this.toggles[toggleIndex];
					debugUIHandlerIndirectToggle.getter = new Func<int, bool>(this.GetValue);
					debugUIHandlerIndirectToggle.setter = new Action<int, bool>(this.SetValue);
					debugUIHandlerIndirectToggle.nextUIHandler = ((toggleIndex < this.m_Field.enumNames.Length - 1) ? this.toggles[toggleIndex + 1] : null);
					debugUIHandlerIndirectToggle.previousUIHandler = ((toggleIndex > 0) ? this.toggles[toggleIndex - 1] : null);
					debugUIHandlerIndirectToggle.parentUIHandler = this;
					debugUIHandlerIndirectToggle.index = toggleIndex;
					debugUIHandlerIndirectToggle.nameLabel.text = enumName.text;
					debugUIHandlerIndirectToggle.Init();
					toggleIndex++;
				}
			}
			while (toggleIndex < this.toggles.Count)
			{
				CoreUtils.Destroy(this.toggles[toggleIndex].gameObject);
				this.toggles[toggleIndex] = null;
				toggleIndex++;
			}
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00044588 File Offset: 0x00042788
		private bool GetValue(int index)
		{
			if (index == 0)
			{
				return false;
			}
			index--;
			return (Convert.ToInt32(this.m_Field.GetValue()) & (1 << index)) != 0;
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x000445B0 File Offset: 0x000427B0
		private void SetValue(int index, bool value)
		{
			if (index == 0)
			{
				this.m_Field.SetValue(Enum.ToObject(this.m_Field.enumType, 0));
				using (List<DebugUIHandlerIndirectToggle>.Enumerator enumerator = this.toggles.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DebugUIHandlerIndirectToggle toggle = enumerator.Current;
						if (toggle != null && toggle.getter != null)
						{
							toggle.UpdateValueLabel();
						}
					}
					return;
				}
			}
			int intValue = Convert.ToInt32(this.m_Field.GetValue());
			if (value)
			{
				intValue |= this.m_Field.enumValues[index];
			}
			else
			{
				intValue &= ~this.m_Field.enumValues[index];
			}
			this.m_Field.SetValue(Enum.ToObject(this.m_Field.enumType, intValue));
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00044680 File Offset: 0x00042880
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (fromNext || !this.valueToggle.isOn)
			{
				this.nameLabel.color = this.colorSelected;
			}
			else if (this.valueToggle.isOn)
			{
				if (this.m_Container.IsDirectChild(previous))
				{
					this.nameLabel.color = this.colorSelected;
				}
				else
				{
					DebugUIHandlerWidget lastItem = this.m_Container.GetLastItem();
					DebugManager.instance.ChangeSelection(lastItem, false);
				}
			}
			return true;
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x000446F7 File Offset: 0x000428F7
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0004470A File Offset: 0x0004290A
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00044718 File Offset: 0x00042918
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00044726 File Offset: 0x00042926
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00044744 File Offset: 0x00042944
		public override DebugUIHandlerWidget Next()
		{
			if (!this.valueToggle.isOn || this.m_Container == null)
			{
				return base.Next();
			}
			DebugUIHandlerWidget firstChild = this.m_Container.GetFirstItem();
			if (firstChild == null)
			{
				return base.Next();
			}
			return firstChild;
		}

		// Token: 0x04000C02 RID: 3074
		public Text nameLabel;

		// Token: 0x04000C03 RID: 3075
		public UIFoldout valueToggle;

		// Token: 0x04000C04 RID: 3076
		public List<DebugUIHandlerIndirectToggle> toggles;

		// Token: 0x04000C05 RID: 3077
		private DebugUI.BitField m_Field;

		// Token: 0x04000C06 RID: 3078
		private DebugUIHandlerContainer m_Container;
	}
}
