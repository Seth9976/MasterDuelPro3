using System;
using System.Runtime.CompilerServices;
using Unity.IntegerTime;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x0200001C RID: 28
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct TextInputEvent : IEventProperties
	{
		// Token: 0x1700002D RID: 45
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002F13 File Offset: 0x00001113
		public DiscreteTime timestamp
		{
			[CompilerGenerated]
			set
			{
				this.<timestamp>k__BackingField = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002F1C File Offset: 0x0000111C
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002F24 File Offset: 0x00001124
		public EventSource eventSource { readonly get; set; }

		// Token: 0x1700002F RID: 47
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002F2D File Offset: 0x0000112D
		public uint playerId
		{
			[CompilerGenerated]
			set
			{
				this.<playerId>k__BackingField = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002F36 File Offset: 0x00001136
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002F3E File Offset: 0x0000113E
		public EventModifiers eventModifiers { readonly get; set; }

		// Token: 0x0600006D RID: 109 RVA: 0x00002F48 File Offset: 0x00001148
		public override string ToString()
		{
			string str = ((this.character == '\0') ? string.Empty : this.character.ToString());
			return string.Format("text input 0x{0:x8} '{1}'", (int)this.character, str);
		}

		// Token: 0x0400009D RID: 157
		public char character;
	}
}
