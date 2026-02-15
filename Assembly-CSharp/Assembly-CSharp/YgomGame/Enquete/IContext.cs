using System;
using System.Collections.Generic;

namespace YgomGame.Enquete
{
	// Token: 0x02000C1B RID: 3099
	public interface IContext
	{
		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600587A RID: 22650
		string label { get; }

		// Token: 0x0600587B RID: 22651
		void Import(object jsonData);

		// Token: 0x0600587C RID: 22652
		void SearchDependencieTextGroups(List<string> resultList);
	}
}
