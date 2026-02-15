using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E0 RID: 224
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal interface IEditableElement
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060006C1 RID: 1729
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		Action editingStarted { get; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060006C2 RID: 1730
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		Action editingEnded { get; }
	}
}
