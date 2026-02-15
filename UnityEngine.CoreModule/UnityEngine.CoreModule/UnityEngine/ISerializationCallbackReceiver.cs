using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001D1 RID: 465
	[RequiredByNativeCode]
	public interface ISerializationCallbackReceiver
	{
		// Token: 0x060011CC RID: 4556
		[RequiredByNativeCode]
		void OnBeforeSerialize();

		// Token: 0x060011CD RID: 4557
		[RequiredByNativeCode]
		void OnAfterDeserialize();
	}
}
