using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace Willow.InGameField
{
	// Token: 0x0200155B RID: 5467
	[RequireDerived]
	public abstract class BaseFieldEventListener : MonoBehaviour
	{
		// Token: 0x06009EBE RID: 40638
		public abstract BaseFieldEvent GetTargetFieldEvent();

		// Token: 0x06009EBF RID: 40639
		public abstract void SetOrOverwriteEvent(BaseFieldEvent fieldEvent);
	}
}
