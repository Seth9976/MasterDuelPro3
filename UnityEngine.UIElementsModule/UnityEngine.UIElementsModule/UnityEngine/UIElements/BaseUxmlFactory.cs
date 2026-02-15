using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004A3 RID: 1187
	[Obsolete("BaseUxmlFactory<TCreatedType, TTraits> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public abstract class BaseUxmlFactory<TCreatedType, TTraits> where TCreatedType : new() where TTraits : BaseUxmlTraits, new()
	{
		// Token: 0x06002208 RID: 8712 RVA: 0x0007C7EE File Offset: 0x0007A9EE
		protected BaseUxmlFactory()
		{
			this.m_Traits = new TTraits();
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x0007C804 File Offset: 0x0007AA04
		public virtual string uxmlName
		{
			get
			{
				return typeof(TCreatedType).Name;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x0007C828 File Offset: 0x0007AA28
		public virtual string uxmlNamespace
		{
			get
			{
				return typeof(TCreatedType).Namespace ?? string.Empty;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x0007C854 File Offset: 0x0007AA54
		public virtual string uxmlQualifiedName
		{
			get
			{
				return typeof(TCreatedType).FullName;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x0007C875 File Offset: 0x0007AA75
		public virtual Type uxmlType
		{
			get
			{
				return typeof(TCreatedType);
			}
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x0007C884 File Offset: 0x0007AA84
		public virtual bool AcceptsAttributeBag(IUxmlAttributes bag, CreationContext cc)
		{
			return true;
		}

		// Token: 0x04000F08 RID: 3848
		internal TTraits m_Traits;
	}
}
