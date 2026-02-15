using System;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000022 RID: 34
	internal interface IEventProviderImpl
	{
		// Token: 0x06000080 RID: 128
		void Initialize();

		// Token: 0x06000081 RID: 129
		void Shutdown();

		// Token: 0x06000082 RID: 130
		void Update();

		// Token: 0x06000083 RID: 131
		void OnFocusChanged(bool focus);
	}
}
