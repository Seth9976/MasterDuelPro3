using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F2 RID: 242
	public class Label : TextElement
	{
		// Token: 0x06000789 RID: 1929 RVA: 0x00024596 File Offset: 0x00022796
		public Label()
			: this(string.Empty)
		{
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000245A5 File Offset: 0x000227A5
		public Label(string text)
		{
			base.AddToClassList(Label.ussClassName);
			this.text = text;
		}

		// Token: 0x040004AA RID: 1194
		public new static readonly string ussClassName = "unity-label";

		// Token: 0x020000F3 RID: 243
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Label, Label.UxmlTraits>
		{
		}

		// Token: 0x020000F4 RID: 244
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextElement.UxmlTraits
		{
		}
	}
}
