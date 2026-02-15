using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006F RID: 111
	public abstract class DebugDisplaySettingsPanel : IDebugDisplaySettingsPanelDisposable, IDebugDisplaySettingsPanel, IDisposable
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00009D79 File Offset: 0x00007F79
		public virtual string PanelName
		{
			get
			{
				DisplayInfoAttribute displayInfo = this.m_DisplayInfo;
				return ((displayInfo != null) ? displayInfo.name : null) ?? string.Empty;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00009D96 File Offset: 0x00007F96
		public virtual int Order
		{
			get
			{
				DisplayInfoAttribute displayInfo = this.m_DisplayInfo;
				if (displayInfo == null)
				{
					return 0;
				}
				return displayInfo.order;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00009DA9 File Offset: 0x00007FA9
		public DebugUI.Widget[] Widgets
		{
			get
			{
				return this.m_Widgets.ToArray();
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x000090C6 File Offset: 0x000072C6
		public virtual DebugUI.Flags Flags
		{
			get
			{
				return DebugUI.Flags.None;
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00009DB6 File Offset: 0x00007FB6
		protected void AddWidget(DebugUI.Widget widget)
		{
			if (widget == null)
			{
				throw new ArgumentNullException("widget");
			}
			this.m_Widgets.Add(widget);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00009DD2 File Offset: 0x00007FD2
		protected void Clear()
		{
			this.m_Widgets.Clear();
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00009DDF File Offset: 0x00007FDF
		public virtual void Dispose()
		{
			this.Clear();
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00009DE8 File Offset: 0x00007FE8
		protected DebugDisplaySettingsPanel()
		{
			this.m_DisplayInfo = base.GetType().GetCustomAttribute<DisplayInfoAttribute>();
			if (this.m_DisplayInfo == null)
			{
				Debug.Log(string.Format("Type {0} should specify the attribute {1}", base.GetType(), "DisplayInfoAttribute"));
			}
		}

		// Token: 0x04000157 RID: 343
		private readonly List<DebugUI.Widget> m_Widgets = new List<DebugUI.Widget>();

		// Token: 0x04000158 RID: 344
		private readonly DisplayInfoAttribute m_DisplayInfo;
	}
}
