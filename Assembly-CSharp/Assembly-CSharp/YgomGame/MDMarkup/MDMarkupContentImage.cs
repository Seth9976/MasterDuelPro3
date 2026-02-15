using System;
using TMPro;
using UnityEngine;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B9B RID: 2971
	[Serializable]
	public class MDMarkupContentImage : MDMarkupContentBase
	{
		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06005540 RID: 21824 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.MarkupType markupType
		{
			get
			{
				return MDMarkupDef.MarkupType.None;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06005541 RID: 21825 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int contentIndent
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06005542 RID: 21826 RVA: 0x0000216A File Offset: 0x0000036A
		protected override object OnExportJsonObj(object jsonObj)
		{
			return null;
		}

		// Token: 0x06005543 RID: 21827 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x04009248 RID: 37448
		[MDMarkupIndent]
		[SerializeField]
		private int indent;

		// Token: 0x04009249 RID: 37449
		[SerializeField]
		public TextAlignmentOptions alignment;

		// Token: 0x0400924A RID: 37450
		[SerializeField]
		public string imagePath;

		// Token: 0x0400924B RID: 37451
		[SerializeField]
		public float overrideHeight;

		// Token: 0x0400924C RID: 37452
		[SerializeField]
		public bool ignorePadding;

		// Token: 0x0400924D RID: 37453
		[SerializeField]
		public bool usePrefferedSize;
	}
}
