using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Used by <see cref="M:System.AppDomain.DoCallBack(System.CrossAppDomainDelegate)" /> for cross-application domain calls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C8 RID: 456
	// (Invoke) Token: 0x060011F2 RID: 4594
	[ComVisible(true)]
	public delegate void CrossAppDomainDelegate();
}
