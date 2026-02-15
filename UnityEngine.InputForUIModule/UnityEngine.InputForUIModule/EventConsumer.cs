using System;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000021 RID: 33
	// (Invoke) Token: 0x0600007F RID: 127
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal delegate bool EventConsumer(in Event ev);
}
