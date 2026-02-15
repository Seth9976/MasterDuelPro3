using System;
using System.Linq;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200042D RID: 1069
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class StyleSelector
	{
		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x00070D48 File Offset: 0x0006EF48
		// (set) Token: 0x06001EFE RID: 7934 RVA: 0x00070D60 File Offset: 0x0006EF60
		public StyleSelectorPart[] parts
		{
			get
			{
				return this.m_Parts;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set
			{
				this.m_Parts = value;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x00070D6C File Offset: 0x0006EF6C
		// (set) Token: 0x06001F00 RID: 7936 RVA: 0x00070D84 File Offset: 0x0006EF84
		public StyleSelectorRelationship previousRelationship
		{
			get
			{
				return this.m_PreviousRelationship;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set
			{
				this.m_PreviousRelationship = value;
			}
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x00070D90 File Offset: 0x0006EF90
		public override string ToString()
		{
			return string.Join(", ", this.parts.Select((StyleSelectorPart p) => p.ToString()).ToArray<string>());
		}

		// Token: 0x04000D87 RID: 3463
		[SerializeField]
		private StyleSelectorPart[] m_Parts;

		// Token: 0x04000D88 RID: 3464
		[SerializeField]
		private StyleSelectorRelationship m_PreviousRelationship;

		// Token: 0x04000D89 RID: 3465
		internal int pseudoStateMask = -1;

		// Token: 0x04000D8A RID: 3466
		internal int negatedPseudoStateMask = -1;
	}
}
