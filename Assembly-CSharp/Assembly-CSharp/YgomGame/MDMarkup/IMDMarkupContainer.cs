using System;
using System.Collections.Generic;
using YgomGame.Utility;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B70 RID: 2928
	public interface IMDMarkupContainer
	{
		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06005473 RID: 21619
		MDMarkupDef.ContainerType containerType { get; }

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06005474 RID: 21620
		GlobalTextData title { get; }

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06005475 RID: 21621
		List<IMDMarkupContent> contents { get; }

		// Token: 0x06005476 RID: 21622
		void Clear();

		// Token: 0x06005477 RID: 21623
		object ExportJsonObj();

		// Token: 0x06005478 RID: 21624
		void ImportJsonObj(object jsonObj);

		// Token: 0x06005479 RID: 21625
		void ImportJson(string json);

		// Token: 0x0600547A RID: 21626
		IReadOnlyList<string> SearchUseTextGruops();
	}
}
