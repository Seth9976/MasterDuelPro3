using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000258 RID: 600
	internal class RuntimePanel : BaseRuntimePanel, IRuntimePanel, IPanel, IDisposable
	{
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x00045C77 File Offset: 0x00043E77
		public PanelSettings panelSettings
		{
			get
			{
				return this.m_PanelSettings;
			}
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00045C80 File Offset: 0x00043E80
		public static RuntimePanel Create(ScriptableObject ownerObject)
		{
			return new RuntimePanel(ownerObject);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00045C98 File Offset: 0x00043E98
		private RuntimePanel(ScriptableObject ownerObject)
			: base(ownerObject, RuntimePanel.s_EventDispatcher)
		{
			this.focusController = new FocusController(new NavigateFocusRing(this.visualTree));
			this.m_PanelSettings = ownerObject as PanelSettings;
			base.name = ((this.m_PanelSettings != null) ? this.m_PanelSettings.name : "RuntimePanel");
			this.visualTree.RegisterCallback<FocusEvent, RuntimePanel>(delegate(FocusEvent e, RuntimePanel p)
			{
				p.OnElementFocus(e);
			}, this, TrickleDown.TrickleDown);
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00045D2C File Offset: 0x00043F2C
		internal override void Update()
		{
			bool flag = this.m_PanelSettings != null;
			if (flag)
			{
				this.m_PanelSettings.ApplyPanelSettings();
			}
			base.Update();
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00045D5D File Offset: 0x00043F5D
		private void OnElementFocus(FocusEvent evt)
		{
			UIElementsRuntimeUtility.defaultEventSystem.OnFocusEvent(this, evt);
		}

		// Token: 0x0400092F RID: 2351
		internal static readonly EventDispatcher s_EventDispatcher = RuntimeEventDispatcher.Create();

		// Token: 0x04000930 RID: 2352
		private readonly PanelSettings m_PanelSettings;
	}
}
