using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x020005CB RID: 1483
	internal struct StyleSyntaxToken
	{
		// Token: 0x06002823 RID: 10275 RVA: 0x000A6199 File Offset: 0x000A4399
		public StyleSyntaxToken(StyleSyntaxTokenType t)
		{
			this.type = t;
			this.text = null;
			this.number = 0;
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000A61B1 File Offset: 0x000A43B1
		public StyleSyntaxToken(StyleSyntaxTokenType type, string text)
		{
			this.type = type;
			this.text = text;
			this.number = 0;
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x000A61C9 File Offset: 0x000A43C9
		public StyleSyntaxToken(StyleSyntaxTokenType type, int number)
		{
			this.type = type;
			this.text = null;
			this.number = number;
		}

		// Token: 0x04001564 RID: 5476
		public StyleSyntaxTokenType type;

		// Token: 0x04001565 RID: 5477
		public string text;

		// Token: 0x04001566 RID: 5478
		public int number;
	}
}
