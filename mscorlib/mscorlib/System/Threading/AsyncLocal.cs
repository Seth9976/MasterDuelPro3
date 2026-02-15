using System;

namespace System.Threading
{
	// Token: 0x0200020E RID: 526
	public sealed class AsyncLocal<T> : IAsyncLocal
	{
		// Token: 0x0600143F RID: 5183 RVA: 0x00003CE1 File Offset: 0x00001EE1
		public AsyncLocal()
		{
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x00052AA1 File Offset: 0x00050CA1
		public AsyncLocal(Action<AsyncLocalValueChangedArgs<T>> valueChangedHandler)
		{
			this.m_valueChangedHandler = valueChangedHandler;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x00052AB0 File Offset: 0x00050CB0
		// (set) Token: 0x06001442 RID: 5186 RVA: 0x00052AD7 File Offset: 0x00050CD7
		public T Value
		{
			get
			{
				object localValue = ExecutionContext.GetLocalValue(this);
				if (localValue != null)
				{
					return (T)((object)localValue);
				}
				return default(T);
			}
			set
			{
				ExecutionContext.SetLocalValue(this, value, this.m_valueChangedHandler != null);
			}
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x00052AF0 File Offset: 0x00050CF0
		void IAsyncLocal.OnValueChanged(object previousValueObj, object currentValueObj, bool contextChanged)
		{
			T t = ((previousValueObj == null) ? default(T) : ((T)((object)previousValueObj)));
			T t2 = ((currentValueObj == null) ? default(T) : ((T)((object)currentValueObj)));
			this.m_valueChangedHandler(new AsyncLocalValueChangedArgs<T>(t, t2, contextChanged));
		}

		// Token: 0x040009EE RID: 2542
		private readonly Action<AsyncLocalValueChangedArgs<T>> m_valueChangedHandler;
	}
}
