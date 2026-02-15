using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200009D RID: 157
	public interface ISignalSource6D
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003B9 RID: 953
		float SignalDuration { get; }

		// Token: 0x060003BA RID: 954
		void GetSignal(float timeSinceSignalStart, out Vector3 pos, out Quaternion rot);
	}
}
