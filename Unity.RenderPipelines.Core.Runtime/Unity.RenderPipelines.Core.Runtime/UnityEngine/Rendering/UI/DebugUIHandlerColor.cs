using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002A6 RID: 678
	public class DebugUIHandlerColor : DebugUIHandlerWidget
	{
		// Token: 0x0600120F RID: 4623 RVA: 0x000450F0 File Offset: 0x000432F0
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.ColorField>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			this.fieldR.getter = () => this.m_Field.GetValue().r;
			this.fieldR.setter = delegate(float x)
			{
				this.SetValue(x, true, false, false, false);
			};
			this.fieldR.nextUIHandler = this.fieldG;
			this.SetupSettings(this.fieldR);
			this.fieldG.getter = () => this.m_Field.GetValue().g;
			this.fieldG.setter = delegate(float x)
			{
				this.SetValue(x, false, true, false, false);
			};
			this.fieldG.previousUIHandler = this.fieldR;
			this.fieldG.nextUIHandler = this.fieldB;
			this.SetupSettings(this.fieldG);
			this.fieldB.getter = () => this.m_Field.GetValue().b;
			this.fieldB.setter = delegate(float x)
			{
				this.SetValue(x, false, false, true, false);
			};
			this.fieldB.previousUIHandler = this.fieldG;
			this.fieldB.nextUIHandler = (this.m_Field.showAlpha ? this.fieldA : null);
			this.SetupSettings(this.fieldB);
			this.fieldA.gameObject.SetActive(this.m_Field.showAlpha);
			this.fieldA.getter = () => this.m_Field.GetValue().a;
			this.fieldA.setter = delegate(float x)
			{
				this.SetValue(x, false, false, false, true);
			};
			this.fieldA.previousUIHandler = this.fieldB;
			this.SetupSettings(this.fieldA);
			this.UpdateColor();
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x000452B4 File Offset: 0x000434B4
		private void SetValue(float x, bool r = false, bool g = false, bool b = false, bool a = false)
		{
			Color color = this.m_Field.GetValue();
			if (r)
			{
				color.r = x;
			}
			if (g)
			{
				color.g = x;
			}
			if (b)
			{
				color.b = x;
			}
			if (a)
			{
				color.a = x;
			}
			this.m_Field.SetValue(color);
			this.UpdateColor();
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00045310 File Offset: 0x00043510
		private void SetupSettings(DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = () => this.m_Field.incStep;
			field.incStepMultGetter = () => this.m_Field.incStepMult;
			field.decimalsGetter = () => (float)this.m_Field.decimals;
			field.Init();
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00045360 File Offset: 0x00043560
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

		// Token: 0x06001213 RID: 4627 RVA: 0x000453D7 File Offset: 0x000435D7
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x000453EA File Offset: 0x000435EA
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x000453F8 File Offset: 0x000435F8
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00045406 File Offset: 0x00043606
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00045421 File Offset: 0x00043621
		internal void UpdateColor()
		{
			if (this.colorImage != null)
			{
				this.colorImage.color = this.m_Field.GetValue();
			}
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00045448 File Offset: 0x00043648
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

		// Token: 0x04000C17 RID: 3095
		public Text nameLabel;

		// Token: 0x04000C18 RID: 3096
		public UIFoldout valueToggle;

		// Token: 0x04000C19 RID: 3097
		public Image colorImage;

		// Token: 0x04000C1A RID: 3098
		public DebugUIHandlerIndirectFloatField fieldR;

		// Token: 0x04000C1B RID: 3099
		public DebugUIHandlerIndirectFloatField fieldG;

		// Token: 0x04000C1C RID: 3100
		public DebugUIHandlerIndirectFloatField fieldB;

		// Token: 0x04000C1D RID: 3101
		public DebugUIHandlerIndirectFloatField fieldA;

		// Token: 0x04000C1E RID: 3102
		private DebugUI.ColorField m_Field;

		// Token: 0x04000C1F RID: 3103
		private DebugUIHandlerContainer m_Container;
	}
}
