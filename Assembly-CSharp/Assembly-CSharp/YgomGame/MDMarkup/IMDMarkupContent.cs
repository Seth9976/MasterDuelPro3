using System;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B72 RID: 2930
	public interface IMDMarkupContent
	{
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x0600547E RID: 21630
		MDMarkupDef.MarkupType markupType { get; }

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x0600547F RID: 21631
		int contentIndent { get; }

		// Token: 0x06005480 RID: 21632
		string ToJson();

		// Token: 0x06005481 RID: 21633
		object ExportJsonObj();

		// Token: 0x06005482 RID: 21634
		void ImportJsonObj(object jsonObj);
	}
}
