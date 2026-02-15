using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000042 RID: 66
	public readonly struct BindablePropertyChangedEventArgs
	{
		// Token: 0x060001FB RID: 507 RVA: 0x000097B8 File Offset: 0x000079B8
		public BindablePropertyChangedEventArgs(in BindingId propertyName)
		{
			this.m_PropertyName = propertyName;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000097C6 File Offset: 0x000079C6
		public BindingId propertyName
		{
			get
			{
				return this.m_PropertyName;
			}
		}

		// Token: 0x04000140 RID: 320
		private readonly BindingId m_PropertyName;
	}
}
