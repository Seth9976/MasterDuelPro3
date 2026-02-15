using System;
using System.Text.RegularExpressions;

namespace Unity.Hierarchy
{
	// Token: 0x02000011 RID: 17
	internal class DefaultHierarchySearchQueryParser : IHierarchySearchQueryParser
	{
		// Token: 0x04000024 RID: 36
		private static readonly Regex s_Filter = new Regex("([#$\\w\\[\\]]+)(<=|<|>=|>|<|=|:)(.*)", RegexOptions.Compiled);
	}
}
