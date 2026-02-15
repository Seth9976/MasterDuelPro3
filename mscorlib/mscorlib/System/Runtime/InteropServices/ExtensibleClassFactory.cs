using System;
using System.Collections;

namespace System.Runtime.InteropServices
{
	/// <summary>Enables customization of managed objects that extend from unmanaged objects during creation.</summary>
	// Token: 0x02000545 RID: 1349
	[ComVisible(true)]
	public sealed class ExtensibleClassFactory
	{
		// Token: 0x0600295E RID: 10590 RVA: 0x000A8754 File Offset: 0x000A6954
		internal static ObjectCreationDelegate GetObjectCreationCallback(Type t)
		{
			return ExtensibleClassFactory.hashtable[t] as ObjectCreationDelegate;
		}

		// Token: 0x04001590 RID: 5520
		private static readonly Hashtable hashtable = new Hashtable();
	}
}
