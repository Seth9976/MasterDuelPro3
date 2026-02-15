using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Utility;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B94 RID: 2964
	[Serializable]
	public class MDMarkupContentFullImagePage : MDMarkupContentPageBase, IMDMarkupContentGlobalText
	{
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06005515 RID: 21781 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.MarkupType markupType
		{
			get
			{
				return MDMarkupDef.MarkupType.None;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06005516 RID: 21782 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005517 RID: 21783 RVA: 0x0000216D File Offset: 0x0000036D
		public Sprite imageSprite
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06005518 RID: 21784 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005519 RID: 21785 RVA: 0x0000216D File Offset: 0x0000036D
		public GameObject prefab
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<string> GetTextGloups()
		{
			return null;
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x0000216A File Offset: 0x0000036A
		protected override object OnExportJsonObj(object jsonObj)
		{
			return null;
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x04009235 RID: 37429
		[SerializeField]
		public GlobalTextData caption;

		// Token: 0x04009236 RID: 37430
		[SerializeField]
		public string resourcePath;

		// Token: 0x04009237 RID: 37431
		[SerializeField]
		public List<URLSchemeButton> buttons;

		// Token: 0x04009238 RID: 37432
		public bool isCreatedSprite;
	}
}
