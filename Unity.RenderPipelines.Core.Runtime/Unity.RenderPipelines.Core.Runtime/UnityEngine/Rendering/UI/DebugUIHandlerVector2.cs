using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002C5 RID: 709
	public class DebugUIHandlerVector2 : DebugUIHandlerWidget
	{
		// Token: 0x060012D3 RID: 4819 RVA: 0x000477D8 File Offset: 0x000459D8
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Vector2Field>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			this.fieldX.getter = () => this.m_Field.GetValue().x;
			this.fieldX.setter = delegate(float x)
			{
				this.SetValue(x, true, false);
			};
			this.fieldX.nextUIHandler = this.fieldY;
			this.SetupSettings(this.fieldX);
			this.fieldY.getter = () => this.m_Field.GetValue().y;
			this.fieldY.setter = delegate(float x)
			{
				this.SetValue(x, false, true);
			};
			this.fieldY.previousUIHandler = this.fieldX;
			this.SetupSettings(this.fieldY);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x000478B0 File Offset: 0x00045AB0
		private void SetValue(float v, bool x = false, bool y = false)
		{
			Vector2 vec = this.m_Field.GetValue();
			if (x)
			{
				vec.x = v;
			}
			if (y)
			{
				vec.y = v;
			}
			this.m_Field.SetValue(vec);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x000478EC File Offset: 0x00045AEC
		private void SetupSettings(DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = () => this.m_Field.incStep;
			field.incStepMultGetter = () => this.m_Field.incStepMult;
			field.decimalsGetter = () => (float)this.m_Field.decimals;
			field.Init();
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0004793C File Offset: 0x00045B3C
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

		// Token: 0x060012D7 RID: 4823 RVA: 0x000479B3 File Offset: 0x00045BB3
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x000479C6 File Offset: 0x00045BC6
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x000479D4 File Offset: 0x00045BD4
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x000479E2 File Offset: 0x00045BE2
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00047A00 File Offset: 0x00045C00
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

		// Token: 0x04000C82 RID: 3202
		public Text nameLabel;

		// Token: 0x04000C83 RID: 3203
		public UIFoldout valueToggle;

		// Token: 0x04000C84 RID: 3204
		public DebugUIHandlerIndirectFloatField fieldX;

		// Token: 0x04000C85 RID: 3205
		public DebugUIHandlerIndirectFloatField fieldY;

		// Token: 0x04000C86 RID: 3206
		private DebugUI.Vector2Field m_Field;

		// Token: 0x04000C87 RID: 3207
		private DebugUIHandlerContainer m_Container;
	}
}
