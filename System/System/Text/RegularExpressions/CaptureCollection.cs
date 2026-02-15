using System;
using System.Diagnostics;
using System.Reflection;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the set of captures made by a single capturing group. </summary>
	// Token: 0x02000124 RID: 292
	[DefaultMember("Item")]
	[DebuggerTypeProxy(typeof(CollectionDebuggerProxy<Capture>))]
	[DebuggerDisplay("Count = {Count}")]
	public class CaptureCollection
	{
		// Token: 0x040004BB RID: 1211
		private readonly Group _group;

		// Token: 0x040004BC RID: 1212
		private readonly int _capcount;

		// Token: 0x040004BD RID: 1213
		private Capture[] _captures;
	}
}
