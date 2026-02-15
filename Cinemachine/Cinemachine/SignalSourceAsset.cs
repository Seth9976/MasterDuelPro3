using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200009E RID: 158
	[DocumentationSorting(DocumentationSortingAttribute.Level.API)]
	public abstract class SignalSourceAsset : ScriptableObject, ISignalSource6D
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003BB RID: 955
		public abstract float SignalDuration { get; }

		// Token: 0x060003BC RID: 956
		public abstract void GetSignal(float timeSinceSignalStart, out Vector3 pos, out Quaternion rot);
	}
}
