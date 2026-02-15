using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a panel that dynamically lays out its contents horizontally or vertically.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000AB RID: 171
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ProvideProperty("FlowBreak", typeof(Control))]
	[DefaultProperty("FlowDirection")]
	[Docking(DockingBehavior.Ask)]
	[Designer("System.Windows.Forms.Design.FlowLayoutPanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class FlowLayoutPanel : Panel
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001BC6D File Offset: 0x00019E6D
		internal FlowLayoutSettings LayoutSettings
		{
			get
			{
				if (this.settings == null)
				{
					this.settings = new FlowLayoutSettings(this);
				}
				return this.settings;
			}
		}

		// Token: 0x0400043D RID: 1085
		private FlowLayoutSettings settings;
	}
}
