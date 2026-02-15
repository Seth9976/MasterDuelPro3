using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000262 RID: 610
	internal interface IGroupManager
	{
		// Token: 0x06001091 RID: 4241
		void Init(IGroupBox groupBox);

		// Token: 0x06001092 RID: 4242
		void OnOptionSelectionChanged(IGroupBoxOption selectedOption);

		// Token: 0x06001093 RID: 4243
		void RegisterOption(IGroupBoxOption option);

		// Token: 0x06001094 RID: 4244
		void UnregisterOption(IGroupBoxOption option);
	}
}
