using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BC2 RID: 3010
	public class MDMarkupPrefabsFactory : MDMarkupWidgetFactoryBase
	{
		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x060055D8 RID: 21976 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyDictionary<string, GameObject> templateMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060055D9 RID: 21977 RVA: 0x000F4CB6 File Offset: 0x000F2EB6
		public MDMarkupPrefabsFactory(Dictionary<string, GameObject> templateMap)
		{
		}

		// Token: 0x060055DA RID: 21978 RVA: 0x0000216A File Offset: 0x0000036A
		public override IMDMarkupWidget CreateChild(MDMarkupIndentWidget indentWidget)
		{
			return null;
		}

		// Token: 0x040092E9 RID: 37609
		private readonly Dictionary<string, GameObject> m_TemplateMap;
	}
}
