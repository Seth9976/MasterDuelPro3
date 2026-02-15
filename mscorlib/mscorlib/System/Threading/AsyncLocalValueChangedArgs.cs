using System;

namespace System.Threading
{
	// Token: 0x02000210 RID: 528
	public readonly struct AsyncLocalValueChangedArgs<T>
	{
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x00052B3A File Offset: 0x00050D3A
		public T PreviousValue { get; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x00052B42 File Offset: 0x00050D42
		public T CurrentValue { get; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x00052B4A File Offset: 0x00050D4A
		public bool ThreadContextChanged { get; }

		// Token: 0x06001448 RID: 5192 RVA: 0x00052B52 File Offset: 0x00050D52
		internal AsyncLocalValueChangedArgs(T previousValue, T currentValue, bool contextChanged)
		{
			this = default(AsyncLocalValueChangedArgs<T>);
			this.PreviousValue = previousValue;
			this.CurrentValue = currentValue;
			this.ThreadContextChanged = contextChanged;
		}
	}
}
