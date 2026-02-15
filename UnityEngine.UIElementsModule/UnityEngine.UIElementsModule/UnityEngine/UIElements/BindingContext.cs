using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x0200001E RID: 30
	public readonly struct BindingContext
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000338A File Offset: 0x0000158A
		public VisualElement targetElement
		{
			get
			{
				return this.m_TargetElement;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003392 File Offset: 0x00001592
		public BindingId bindingId
		{
			get
			{
				return this.m_BindingId;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000339A File Offset: 0x0000159A
		public PropertyPath dataSourcePath
		{
			get
			{
				return this.m_DataSourcePath;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000033A2 File Offset: 0x000015A2
		public object dataSource
		{
			get
			{
				return this.m_DataSource;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000033AA File Offset: 0x000015AA
		internal BindingContext(VisualElement targetElement, in BindingId bindingId, in PropertyPath resolvedDataSourcePath, object resolvedDataSource)
		{
			this.m_TargetElement = targetElement;
			this.m_BindingId = bindingId;
			this.m_DataSourcePath = resolvedDataSourcePath;
			this.m_DataSource = resolvedDataSource;
		}

		// Token: 0x0400003B RID: 59
		private readonly VisualElement m_TargetElement;

		// Token: 0x0400003C RID: 60
		private readonly BindingId m_BindingId;

		// Token: 0x0400003D RID: 61
		private readonly PropertyPath m_DataSourcePath;

		// Token: 0x0400003E RID: 62
		private readonly object m_DataSource;
	}
}
