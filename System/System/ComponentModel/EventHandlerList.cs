using System;

namespace System.ComponentModel
{
	/// <summary>Provides a simple list of delegates. This class cannot be inherited.</summary>
	// Token: 0x02000245 RID: 581
	public sealed class EventHandlerList : IDisposable
	{
		// Token: 0x06000DFC RID: 3580 RVA: 0x0003E74E File Offset: 0x0003C94E
		internal EventHandlerList(Component parent)
		{
			this._parent = parent;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventHandlerList" /> class. </summary>
		// Token: 0x06000DFD RID: 3581 RVA: 0x000026E5 File Offset: 0x000008E5
		public EventHandlerList()
		{
		}

		/// <summary>Gets or sets the delegate for the specified object.</summary>
		/// <returns>The delegate for the specified key, or null if a delegate does not exist.</returns>
		/// <param name="key">An object to find in the list. </param>
		// Token: 0x170002F4 RID: 756
		public Delegate this[object key]
		{
			get
			{
				EventHandlerList.ListEntry listEntry = null;
				if (this._parent == null || this._parent.CanRaiseEventsInternal)
				{
					listEntry = this.Find(key);
				}
				if (listEntry == null)
				{
					return null;
				}
				return listEntry._handler;
			}
			set
			{
				EventHandlerList.ListEntry listEntry = this.Find(key);
				if (listEntry != null)
				{
					listEntry._handler = value;
					return;
				}
				this._head = new EventHandlerList.ListEntry(key, value, this._head);
			}
		}

		/// <summary>Adds a delegate to the list.</summary>
		/// <param name="key">The object that owns the event. </param>
		/// <param name="value">The delegate to add to the list. </param>
		// Token: 0x06000E00 RID: 3584 RVA: 0x0003E7CC File Offset: 0x0003C9CC
		public void AddHandler(object key, Delegate value)
		{
			EventHandlerList.ListEntry listEntry = this.Find(key);
			if (listEntry != null)
			{
				listEntry._handler = Delegate.Combine(listEntry._handler, value);
				return;
			}
			this._head = new EventHandlerList.ListEntry(key, value, this._head);
		}

		/// <summary>Disposes the delegate list.</summary>
		// Token: 0x06000E01 RID: 3585 RVA: 0x0003E80A File Offset: 0x0003CA0A
		public void Dispose()
		{
			this._head = null;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0003E814 File Offset: 0x0003CA14
		private EventHandlerList.ListEntry Find(object key)
		{
			EventHandlerList.ListEntry listEntry = this._head;
			while (listEntry != null && listEntry._key != key)
			{
				listEntry = listEntry._next;
			}
			return listEntry;
		}

		/// <summary>Removes a delegate from the list.</summary>
		/// <param name="key">The object that owns the event. </param>
		/// <param name="value">The delegate to remove from the list. </param>
		// Token: 0x06000E03 RID: 3587 RVA: 0x0003E840 File Offset: 0x0003CA40
		public void RemoveHandler(object key, Delegate value)
		{
			EventHandlerList.ListEntry listEntry = this.Find(key);
			if (listEntry != null)
			{
				listEntry._handler = Delegate.Remove(listEntry._handler, value);
			}
		}

		// Token: 0x0400099D RID: 2461
		private EventHandlerList.ListEntry _head;

		// Token: 0x0400099E RID: 2462
		private Component _parent;

		// Token: 0x02000246 RID: 582
		private sealed class ListEntry
		{
			// Token: 0x06000E04 RID: 3588 RVA: 0x0003E86A File Offset: 0x0003CA6A
			public ListEntry(object key, Delegate handler, EventHandlerList.ListEntry next)
			{
				this._next = next;
				this._key = key;
				this._handler = handler;
			}

			// Token: 0x0400099F RID: 2463
			internal EventHandlerList.ListEntry _next;

			// Token: 0x040009A0 RID: 2464
			internal object _key;

			// Token: 0x040009A1 RID: 2465
			internal Delegate _handler;
		}
	}
}
