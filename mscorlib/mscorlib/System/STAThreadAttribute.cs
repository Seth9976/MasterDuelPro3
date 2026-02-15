using System;

namespace System
{
	/// <summary>Indicates that the COM threading model for an application is single-threaded apartment (STA). </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200014D RID: 333
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class STAThreadAttribute : Attribute
	{
	}
}
