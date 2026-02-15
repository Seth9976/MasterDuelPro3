using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002C7 RID: 711
	public class DebugUIHandlerVector4 : DebugUIHandlerWidget
	{
		// Token: 0x060012F7 RID: 4855 RVA: 0x00047E10 File Offset: 0x00046010
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Vector4Field>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			this.fieldX.getter = () => this.m_Field.GetValue().x;
			this.fieldX.setter = delegate(float x)
			{
				this.SetValue(x, true, false, false, false);
			};
			this.fieldX.nextUIHandler = this.fieldY;
			this.SetupSettings(this.fieldX);
			this.fieldY.getter = () => this.m_Field.GetValue().y;
			this.fieldY.setter = delegate(float x)
			{
				this.SetValue(x, false, true, false, false);
			};
			this.fieldY.previousUIHandler = this.fieldX;
			this.fieldY.nextUIHandler = this.fieldZ;
			this.SetupSettings(this.fieldY);
			this.fieldZ.getter = () => this.m_Field.GetValue().z;
			this.fieldZ.setter = delegate(float x)
			{
				this.SetValue(x, false, false, true, false);
			};
			this.fieldZ.previousUIHandler = this.fieldY;
			this.fieldZ.nextUIHandler = this.fieldW;
			this.SetupSettings(this.fieldZ);
			this.fieldW.getter = () => this.m_Field.GetValue().w;
			this.fieldW.setter = delegate(float x)
			{
				this.SetValue(x, false, false, false, true);
			};
			this.fieldW.previousUIHandler = this.fieldZ;
			this.SetupSettings(this.fieldW);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00047FA0 File Offset: 0x000461A0
		private void SetValue(float v, bool x = false, bool y = false, bool z = false, bool w = false)
		{
			Vector4 vec = this.m_Field.GetValue();
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
			if (w)
			{
				vec.w = v;
			}
			this.m_Field.SetValue(vec);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00047FF4 File Offset: 0x000461F4
		private void SetupSettings(DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = () => this.m_Field.incStep;
			field.incStepMultGetter = () => this.m_Field.incStepMult;
			field.decimalsGetter = () => (float)this.m_Field.decimals;
			field.Init();
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00048044 File Offset: 0x00046244
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

		// Token: 0x060012FB RID: 4859 RVA: 0x000480BB File Offset: 0x000462BB
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x000480CE File Offset: 0x000462CE
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x000480DC File Offset: 0x000462DC
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x000480EA File Offset: 0x000462EA
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00048108 File Offset: 0x00046308
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

		// Token: 0x04000C8F RID: 3215
		public Text nameLabel;

		// Token: 0x04000C90 RID: 3216
		public UIFoldout valueToggle;

		// Token: 0x04000C91 RID: 3217
		public DebugUIHandlerIndirectFloatField fieldX;

		// Token: 0x04000C92 RID: 3218
		public DebugUIHandlerIndirectFloatField fieldY;

		// Token: 0x04000C93 RID: 3219
		public DebugUIHandlerIndirectFloatField fieldZ;

		// Token: 0x04000C94 RID: 3220
		public DebugUIHandlerIndirectFloatField fieldW;

		// Token: 0x04000C95 RID: 3221
		private DebugUI.Vector4Field m_Field;

		// Token: 0x04000C96 RID: 3222
		private DebugUIHandlerContainer m_Container;
	}
}
