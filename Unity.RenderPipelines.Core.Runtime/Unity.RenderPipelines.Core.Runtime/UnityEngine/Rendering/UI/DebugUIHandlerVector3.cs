using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002C6 RID: 710
	public class DebugUIHandlerVector3 : DebugUIHandlerWidget
	{
		// Token: 0x060012E4 RID: 4836 RVA: 0x00047AB0 File Offset: 0x00045CB0
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Vector3Field>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			this.fieldX.getter = () => this.m_Field.GetValue().x;
			this.fieldX.setter = delegate(float v)
			{
				this.SetValue(v, true, false, false);
			};
			this.fieldX.nextUIHandler = this.fieldY;
			this.SetupSettings(this.fieldX);
			this.fieldY.getter = () => this.m_Field.GetValue().y;
			this.fieldY.setter = delegate(float v)
			{
				this.SetValue(v, false, true, false);
			};
			this.fieldY.previousUIHandler = this.fieldX;
			this.fieldY.nextUIHandler = this.fieldZ;
			this.SetupSettings(this.fieldY);
			this.fieldZ.getter = () => this.m_Field.GetValue().z;
			this.fieldZ.setter = delegate(float v)
			{
				this.SetValue(v, false, false, true);
			};
			this.fieldZ.previousUIHandler = this.fieldY;
			this.SetupSettings(this.fieldZ);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00047BE4 File Offset: 0x00045DE4
		private void SetValue(float v, bool x = false, bool y = false, bool z = false)
		{
			Vector3 vec = this.m_Field.GetValue();
			if (x)
			{
				vec.x = v;
			}
			if (y)
			{
				vec.y = v;
			}
			if (z)
			{
				vec.z = v;
			}
			this.m_Field.SetValue(vec);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00047C2C File Offset: 0x00045E2C
		private void SetupSettings(DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = () => this.m_Field.incStep;
			field.incStepMultGetter = () => this.m_Field.incStepMult;
			field.decimalsGetter = () => (float)this.m_Field.decimals;
			field.Init();
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00047C7C File Offset: 0x00045E7C
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

		// Token: 0x060012E8 RID: 4840 RVA: 0x00047CF3 File Offset: 0x00045EF3
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00047D06 File Offset: 0x00045F06
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00047D14 File Offset: 0x00045F14
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00047D22 File Offset: 0x00045F22
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00047D40 File Offset: 0x00045F40
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

		// Token: 0x04000C88 RID: 3208
		public Text nameLabel;

		// Token: 0x04000C89 RID: 3209
		public UIFoldout valueToggle;

		// Token: 0x04000C8A RID: 3210
		public DebugUIHandlerIndirectFloatField fieldX;

		// Token: 0x04000C8B RID: 3211
		public DebugUIHandlerIndirectFloatField fieldY;

		// Token: 0x04000C8C RID: 3212
		public DebugUIHandlerIndirectFloatField fieldZ;

		// Token: 0x04000C8D RID: 3213
		private DebugUI.Vector3Field m_Field;

		// Token: 0x04000C8E RID: 3214
		private DebugUIHandlerContainer m_Container;
	}
}
