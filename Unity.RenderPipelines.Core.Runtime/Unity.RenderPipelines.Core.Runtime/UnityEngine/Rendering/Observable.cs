using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005C RID: 92
	public struct Observable<T>
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060004BA RID: 1210 RVA: 0x00008ED0 File Offset: 0x000070D0
		// (remove) Token: 0x060004BB RID: 1211 RVA: 0x00008F08 File Offset: 0x00007108
		public event Action<T> onValueChanged;

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00008F3D File Offset: 0x0000713D
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x00008F45 File Offset: 0x00007145
		public T value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				if (!EqualityComparer<T>.Default.Equals(value, this.m_Value))
				{
					this.m_Value = value;
					Action<T> action = this.onValueChanged;
					if (action == null)
					{
						return;
					}
					action(value);
				}
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00008F72 File Offset: 0x00007172
		public Observable(T newValue)
		{
			this.m_Value = newValue;
			this.onValueChanged = null;
		}

		// Token: 0x04000136 RID: 310
		private T m_Value;
	}
}
