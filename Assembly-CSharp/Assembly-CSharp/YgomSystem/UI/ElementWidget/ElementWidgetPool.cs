using System;

namespace YgomSystem.UI.ElementWidget
{
	// Token: 0x0200068F RID: 1679
	public class ElementWidgetPool<T> : UIObjectPool<T> where T : ElementWidgetBase
	{
		// Token: 0x060034C1 RID: 13505 RVA: 0x000F2C06 File Offset: 0x000F0E06
		public ElementWidgetPool(Func<T> factory)
		{
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000F2C10 File Offset: 0x000F0E10
		protected override T Create()
		{
			return default(T);
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnBeforeRent(T obj)
		{
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnBeforeReturn(T obj)
		{
		}

		// Token: 0x0400301F RID: 12319
		private readonly Func<T> m_Factory;
	}
}
