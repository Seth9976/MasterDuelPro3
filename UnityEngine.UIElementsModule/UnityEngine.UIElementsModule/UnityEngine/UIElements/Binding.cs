using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x0200001C RID: 28
	[UxmlObject]
	public abstract class Binding
	{
		// Token: 0x0600007B RID: 123 RVA: 0x000032E6 File Offset: 0x000014E6
		public static void SetGlobalLogLevel(BindingLogLevel logLevel)
		{
			DataBindingManager.globalLogLevel = logLevel;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000032F0 File Offset: 0x000014F0
		public static void SetPanelLogLevel(IPanel panel, BindingLogLevel logLevel)
		{
			BaseVisualElementPanel elementPanel = panel as BaseVisualElementPanel;
			bool flag = elementPanel != null;
			if (flag)
			{
				elementPanel.dataBindingManager.logLevel = logLevel;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000331C File Offset: 0x0000151C
		public static void ResetPanelLogLevel(IPanel panel)
		{
			BaseVisualElementPanel elementPanel = panel as BaseVisualElementPanel;
			bool flag = elementPanel != null;
			if (flag)
			{
				elementPanel.dataBindingManager.ResetLogLevel();
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003347 File Offset: 0x00001547
		public bool isDirty
		{
			get
			{
				return this.m_Dirty;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000334F File Offset: 0x0000154F
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003357 File Offset: 0x00001557
		[CreateProperty]
		public BindingUpdateTrigger updateTrigger
		{
			get
			{
				return this.m_UpdateTrigger;
			}
			set
			{
				this.m_UpdateTrigger = value;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003360 File Offset: 0x00001560
		public void MarkDirty()
		{
			this.m_Dirty = true;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000336A File Offset: 0x0000156A
		internal void ClearDirty()
		{
			this.m_Dirty = false;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000020EA File Offset: 0x000002EA
		protected internal virtual void OnActivated(in BindingActivationContext context)
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000020EA File Offset: 0x000002EA
		protected internal virtual void OnDeactivated(in BindingActivationContext context)
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000020EA File Offset: 0x000002EA
		protected internal virtual void OnDataSourceChanged(in DataSourceContextChanged context)
		{
		}

		// Token: 0x04000035 RID: 53
		private bool m_Dirty;

		// Token: 0x04000036 RID: 54
		private BindingUpdateTrigger m_UpdateTrigger;

		// Token: 0x04000038 RID: 56
		internal const string k_UpdateTriggerTooltip = "This informs the binding system of whether the binding object should be updated on every frame, when a change occurs in the source or on every frame if change detection is impossible, and when explicitly marked as dirty.";
	}
}
