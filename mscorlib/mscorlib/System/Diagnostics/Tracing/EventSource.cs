using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	/// <summary>Provides the ability to create events for event tracing for Windows (ETW).</summary>
	// Token: 0x020006E9 RID: 1769
	public class EventSource : IDisposable
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class.</summary>
		// Token: 0x060037BE RID: 14270 RVA: 0x000DB608 File Offset: 0x000D9808
		protected EventSource()
		{
			this.Name = base.GetType().Name;
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x000DB621 File Offset: 0x000D9821
		public EventSource(string eventSourceName)
		{
			this.Name = eventSourceName;
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x000DB630 File Offset: 0x000D9830
		internal EventSource(Guid eventSourceGuid, string eventSourceName)
			: this(eventSourceName)
		{
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x000DB63C File Offset: 0x000D983C
		~EventSource()
		{
			this.Dispose(false);
		}

		/// <summary>The friendly name of the class that is derived from the event source.</summary>
		/// <returns>The friendly name of the derived class.  The default is the simple name of the class.</returns>
		// Token: 0x17000893 RID: 2195
		// (set) Token: 0x060037C2 RID: 14274 RVA: 0x000DB66C File Offset: 0x000D986C
		private string Name
		{
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		/// <summary>Determines whether the current event source is enabled.</summary>
		/// <returns>true if the current event source is enabled; otherwise, false.</returns>
		// Token: 0x060037C3 RID: 14275 RVA: 0x00033991 File Offset: 0x00031B91
		public bool IsEnabled()
		{
			return false;
		}

		/// <summary>Determines whether the current event source that has the specified level and keyword is enabled.</summary>
		/// <returns>true if the event source is enabled; otherwise, false.</returns>
		/// <param name="level">The level of the event source.</param>
		/// <param name="keywords">The keyword of the event source.</param>
		// Token: 0x060037C4 RID: 14276 RVA: 0x00033991 File Offset: 0x00031B91
		public bool IsEnabled(EventLevel level, EventKeywords keywords)
		{
			return false;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class.</summary>
		// Token: 0x060037C5 RID: 14277 RVA: 0x000DB675 File Offset: 0x000D9875
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060037C6 RID: 14278 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Writes an event by using the provided event identifier.</summary>
		/// <param name="eventId">The event identifier.</param>
		// Token: 0x060037C7 RID: 14279 RVA: 0x000DB684 File Offset: 0x000D9884
		protected void WriteEvent(int eventId)
		{
			this.WriteEvent(eventId, new object[0]);
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer argument.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">An integer argument.</param>
		// Token: 0x060037C8 RID: 14280 RVA: 0x000DB693 File Offset: 0x000D9893
		protected void WriteEvent(int eventId, int arg1)
		{
			this.WriteEvent(eventId, new object[] { arg1 });
		}

		/// <summary>Writes an event by using the provided event identifier and string argument.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A string argument.</param>
		// Token: 0x060037C9 RID: 14281 RVA: 0x000DB6AB File Offset: 0x000D98AB
		protected void WriteEvent(int eventId, string arg1)
		{
			this.WriteEvent(eventId, new object[] { arg1 });
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">An integer argument.</param>
		/// <param name="arg2">An integer argument.</param>
		// Token: 0x060037CA RID: 14282 RVA: 0x000DB6BE File Offset: 0x000D98BE
		protected void WriteEvent(int eventId, int arg1, int arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">An integer argument.</param>
		/// <param name="arg2">An integer argument.</param>
		/// <param name="arg3">An integer argument.</param>
		// Token: 0x060037CB RID: 14283 RVA: 0x000DB6DF File Offset: 0x000D98DF
		protected void WriteEvent(int eventId, int arg1, int arg2, int arg3)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2, arg3 });
		}

		/// <summary>Writes an event by using the provided event identifier and 64-bit integer argument.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A 64 bit integer argument.</param>
		// Token: 0x060037CC RID: 14284 RVA: 0x000DB70A File Offset: 0x000D990A
		protected void WriteEvent(int eventId, long arg1)
		{
			this.WriteEvent(eventId, new object[] { arg1 });
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x000DB722 File Offset: 0x000D9922
		protected void WriteEvent(int eventId, long arg1, string arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		/// <summary>Writes an event by using the provided event identifier and array of arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="args">An array of objects.</param>
		// Token: 0x060037CE RID: 14286 RVA: 0x00002C89 File Offset: 0x00000E89
		protected void WriteEvent(int eventId, params object[] args)
		{
		}

		/// <summary>Writes an event by using the provided event identifier and string arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A string argument.</param>
		/// <param name="arg3">A string argument.</param>
		// Token: 0x060037CF RID: 14287 RVA: 0x000DB73E File Offset: 0x000D993E
		protected void WriteEvent(int eventId, string arg1, string arg2, string arg3)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2, arg3 });
		}

		/// <summary>Creates a new <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overload by using the provided event identifier and event data.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="eventDataCount">The number of event data items.</param>
		/// <param name="data">The structure that contains the event data.</param>
		// Token: 0x060037D0 RID: 14288 RVA: 0x00002C89 File Offset: 0x00000E89
		[CLSCompliant(false)]
		protected unsafe void WriteEventCore(int eventId, int eventDataCount, EventSource.EventData* data)
		{
		}

		/// <summary>Provides the event data for creating fast <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overloads by using the <see cref="M:System.Diagnostics.Tracing.EventSource.WriteEventCore(System.Int32,System.Int32,System.Diagnostics.Tracing.EventSource.EventData*)" /> method.</summary>
		// Token: 0x020006EA RID: 1770
		protected internal struct EventData
		{
			/// <summary>Gets or sets the pointer to the data for the new <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overload.</summary>
			/// <returns>The pointer to the data.</returns>
			// Token: 0x17000894 RID: 2196
			// (set) Token: 0x060037D1 RID: 14289 RVA: 0x000DB75A File Offset: 0x000D995A
			public IntPtr DataPointer
			{
				[CompilerGenerated]
				set
				{
					this.<DataPointer>k__BackingField = value;
				}
			}

			/// <summary>Gets or sets the number of payload items in the new <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overload.</summary>
			/// <returns>The number of payload items in the new overload.</returns>
			// Token: 0x17000895 RID: 2197
			// (set) Token: 0x060037D2 RID: 14290 RVA: 0x000DB763 File Offset: 0x000D9963
			public int Size
			{
				[CompilerGenerated]
				set
				{
					this.<Size>k__BackingField = value;
				}
			}

			// Token: 0x17000896 RID: 2198
			// (set) Token: 0x060037D3 RID: 14291 RVA: 0x000DB76C File Offset: 0x000D996C
			internal int Reserved
			{
				[CompilerGenerated]
				set
				{
					this.<Reserved>k__BackingField = value;
				}
			}
		}
	}
}
