using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200004F RID: 79
	internal struct CharacterSubstitution
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x00021525 File Offset: 0x0001F725
		public CharacterSubstitution(int index, uint unicode)
		{
			this.index = index;
			this.unicode = unicode;
		}

		// Token: 0x04000302 RID: 770
		public int index;

		// Token: 0x04000303 RID: 771
		public uint unicode;
	}
}
