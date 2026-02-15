using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	/// <summary>Allows the event tracing for Windows (ETW) name to be defined independently of the name of the event source class.   </summary>
	// Token: 0x020006EB RID: 1771
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class EventSourceAttribute : Attribute
	{
		/// <summary>Gets or sets the event source identifier.</summary>
		/// <returns>The event source identifier.</returns>
		// Token: 0x17000897 RID: 2199
		// (set) Token: 0x060037D4 RID: 14292 RVA: 0x000DB775 File Offset: 0x000D9975
		public string Guid
		{
			[CompilerGenerated]
			set
			{
				this.<Guid>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the name of the event source.</summary>
		/// <returns>The name of the event source.</returns>
		// Token: 0x17000898 RID: 2200
		// (set) Token: 0x060037D5 RID: 14293 RVA: 0x000DB77E File Offset: 0x000D997E
		public string Name
		{
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}
	}
}
