using System;
using System.Collections.Generic;
using YgomGame.Utility;
using YgomSystem.UI;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B86 RID: 2950
	[Serializable]
	public class MDMarkupBoardContainer : MDMarkupContainerBase
	{
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060054BA RID: 21690 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.ContainerType containerType
		{
			get
			{
				return (MDMarkupDef.ContainerType)0;
			}
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Clear()
		{
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x0000216A File Offset: 0x0000036A
		public override object ExportJsonObj()
		{
			return null;
		}

		// Token: 0x060054BD RID: 21693 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x060054BE RID: 21694 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InnerSearchUseTextGroups(List<string> useTextGroups)
		{
		}

		// Token: 0x04009202 RID: 37378
		public MDMarkupBoardContainer.FooterContent footerContent;

		// Token: 0x02000B87 RID: 2951
		[Serializable]
		public class FooterContent
		{
			// Token: 0x060054C0 RID: 21696 RVA: 0x0000216D File Offset: 0x0000036D
			public void Clear()
			{
			}

			// Token: 0x060054C1 RID: 21697 RVA: 0x0000216A File Offset: 0x0000036A
			public object ExportJsonObj()
			{
				return null;
			}

			// Token: 0x060054C2 RID: 21698 RVA: 0x0000216D File Offset: 0x0000036D
			public void ImportJsonObj(object jsonObj)
			{
			}

			// Token: 0x04009203 RID: 37379
			public List<MDMarkupBoardContainer.FooterContent.Content> contents;

			// Token: 0x02000B88 RID: 2952
			public enum ContentType
			{
				// Token: 0x04009205 RID: 37381
				Text,
				// Token: 0x04009206 RID: 37382
				Button
			}

			// Token: 0x02000B89 RID: 2953
			[Serializable]
			public class Content
			{
				// Token: 0x060054C4 RID: 21700 RVA: 0x0000216A File Offset: 0x0000036A
				public object ExportJsonObj()
				{
					return null;
				}

				// Token: 0x060054C5 RID: 21701 RVA: 0x0000216D File Offset: 0x0000036D
				public void ImportJsonObj(object jsonObj)
				{
				}

				// Token: 0x04009207 RID: 37383
				public MDMarkupBoardContainer.FooterContent.ContentType tp;

				// Token: 0x04009208 RID: 37384
				public GlobalTextData text;

				// Token: 0x04009209 RID: 37385
				public string url;

				// Token: 0x0400920A RID: 37386
				public SelectorManager.KeyType shortcut;

				// Token: 0x0400920B RID: 37387
				public bool consoleOnly;

				// Token: 0x0400920C RID: 37388
				public bool interactable;
			}
		}
	}
}
