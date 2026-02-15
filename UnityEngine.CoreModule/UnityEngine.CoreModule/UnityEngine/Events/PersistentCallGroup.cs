using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x0200022C RID: 556
	[Serializable]
	internal class PersistentCallGroup
	{
		// Token: 0x06001451 RID: 5201 RVA: 0x0002ABAB File Offset: 0x00028DAB
		public PersistentCallGroup()
		{
			this.m_Calls = new List<PersistentCall>();
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x0002ABC0 File Offset: 0x00028DC0
		public int Count
		{
			get
			{
				return this.m_Calls.Count;
			}
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0002ABE0 File Offset: 0x00028DE0
		public void Initialize(InvokableCallList invokableList, UnityEventBase unityEventBase)
		{
			foreach (PersistentCall persistentCall in this.m_Calls)
			{
				bool flag = !persistentCall.IsValid();
				if (!flag)
				{
					BaseInvokableCall call = persistentCall.GetRuntimeCall(unityEventBase);
					bool flag2 = call != null;
					if (flag2)
					{
						invokableList.AddPersistentInvokableCall(call);
					}
				}
			}
		}

		// Token: 0x04000789 RID: 1929
		[SerializeField]
		[FormerlySerializedAs("m_Listeners")]
		private List<PersistentCall> m_Calls;
	}
}
