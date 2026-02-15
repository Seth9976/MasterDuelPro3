using System;
using System.Runtime.CompilerServices;
using Unity.IntegerTime;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x0200000F RID: 15
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct IMECompositionEvent : IEventProperties
	{
		// Token: 0x17000018 RID: 24
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000026EC File Offset: 0x000008EC
		public DiscreteTime timestamp
		{
			[CompilerGenerated]
			set
			{
				this.<timestamp>k__BackingField = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000026F5 File Offset: 0x000008F5
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000026FD File Offset: 0x000008FD
		public EventSource eventSource { readonly get; set; }

		// Token: 0x1700001A RID: 26
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002706 File Offset: 0x00000906
		public uint playerId
		{
			[CompilerGenerated]
			set
			{
				this.<playerId>k__BackingField = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000270F File Offset: 0x0000090F
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002717 File Offset: 0x00000917
		public EventModifiers eventModifiers { readonly get; set; }

		// Token: 0x06000036 RID: 54 RVA: 0x00002720 File Offset: 0x00000920
		public override string ToString()
		{
			return "IME '" + this.compositionString + "'";
		}

		// Token: 0x0400004C RID: 76
		public string compositionString;
	}
}
