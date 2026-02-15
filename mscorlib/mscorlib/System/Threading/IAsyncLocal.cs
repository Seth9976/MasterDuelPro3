using System;

namespace System.Threading
{
	// Token: 0x0200020F RID: 527
	internal interface IAsyncLocal
	{
		// Token: 0x06001444 RID: 5188
		void OnValueChanged(object previousValue, object currentValue, bool contextChanged);
	}
}
