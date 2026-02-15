using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000032 RID: 50
	[Serializable]
	public class SpriteCharacter : TextElement
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000AC3C File Offset: 0x00008E3C
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000AC54 File Offset: 0x00008E54
		public SpriteCharacter()
		{
			this.m_ElementType = TextElementType.Sprite;
		}

		// Token: 0x04000150 RID: 336
		[SerializeField]
		private string m_Name;
	}
}
