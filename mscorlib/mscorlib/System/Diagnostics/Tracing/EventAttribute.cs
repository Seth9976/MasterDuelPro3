using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies additional event schema information for an event.</summary>
	// Token: 0x020006E8 RID: 1768
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class EventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventAttribute" /> class with the specified event identifier.</summary>
		/// <param name="eventId">The event identifier for the event.</param>
		// Token: 0x060037B9 RID: 14265 RVA: 0x000DB5D5 File Offset: 0x000D97D5
		public EventAttribute(int eventId)
		{
			this.EventId = eventId;
		}

		/// <summary>Gets or sets the identifier for the event.</summary>
		/// <returns>The event identifier.</returns>
		// Token: 0x1700088F RID: 2191
		// (set) Token: 0x060037BA RID: 14266 RVA: 0x000DB5E4 File Offset: 0x000D97E4
		private int EventId
		{
			[CompilerGenerated]
			set
			{
				this.<EventId>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the level for the event.</summary>
		/// <returns>One of the enumeration values that specifies the level for the event.</returns>
		// Token: 0x17000890 RID: 2192
		// (set) Token: 0x060037BB RID: 14267 RVA: 0x000DB5ED File Offset: 0x000D97ED
		public EventLevel Level
		{
			[CompilerGenerated]
			set
			{
				this.<Level>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the keywords for the event.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x17000891 RID: 2193
		// (set) Token: 0x060037BC RID: 14268 RVA: 0x000DB5F6 File Offset: 0x000D97F6
		public EventKeywords Keywords
		{
			[CompilerGenerated]
			set
			{
				this.<Keywords>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the message for the event.</summary>
		/// <returns>The message for the event.</returns>
		// Token: 0x17000892 RID: 2194
		// (set) Token: 0x060037BD RID: 14269 RVA: 0x000DB5FF File Offset: 0x000D97FF
		public string Message
		{
			[CompilerGenerated]
			set
			{
				this.<Message>k__BackingField = value;
			}
		}
	}
}
