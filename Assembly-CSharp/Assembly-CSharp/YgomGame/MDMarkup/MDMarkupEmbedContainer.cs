using System;
using System.Collections.Generic;
using YgomGame.Utility;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BAD RID: 2989
	[Serializable]
	public class MDMarkupEmbedContainer : IMDMarkupContainer
	{
		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x0600556F RID: 21871 RVA: 0x000029CC File Offset: 0x00000BCC
		public MDMarkupDef.ContainerType containerType
		{
			get
			{
				return (MDMarkupDef.ContainerType)0;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06005570 RID: 21872 RVA: 0x0000216A File Offset: 0x0000036A
		public GlobalTextData title
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06005571 RID: 21873 RVA: 0x0000216A File Offset: 0x0000036A
		public List<IMDMarkupContent> contents
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005572 RID: 21874 RVA: 0x0000216D File Offset: 0x0000036D
		public void Clear()
		{
		}

		// Token: 0x06005573 RID: 21875 RVA: 0x0000216A File Offset: 0x0000036A
		public object ExportJsonObj()
		{
			return null;
		}

		// Token: 0x06005574 RID: 21876 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportJson(string json)
		{
		}

		// Token: 0x06005575 RID: 21877 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x06005576 RID: 21878 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<string> SearchUseTextGruops()
		{
			return null;
		}

		// Token: 0x04009295 RID: 37525
		public MDMarkupContentEmbedContainerTab embedContent;
	}
}
