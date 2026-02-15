using System;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	/// <summary>Manages serialization processes at run time. This class cannot be inherited.</summary>
	// Token: 0x020004B5 RID: 1205
	public sealed class SerializationObjectManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationObjectManager" /> class. </summary>
		/// <param name="context">An instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class that contains information about the current serialization operation.</param>
		// Token: 0x06002661 RID: 9825 RVA: 0x0009ABDC File Offset: 0x00098DDC
		public SerializationObjectManager(StreamingContext context)
		{
			this._context = context;
			this._objectSeenTable = new Dictionary<object, object>();
		}

		/// <summary>Registers the object upon which events will be raised.</summary>
		/// <param name="obj">The object to register.</param>
		// Token: 0x06002662 RID: 9826 RVA: 0x0009ABF8 File Offset: 0x00098DF8
		public void RegisterObject(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			if (serializationEventsForType.HasOnSerializingEvents && this._objectSeenTable.TryAdd(obj, true))
			{
				serializationEventsForType.InvokeOnSerializing(obj, this._context);
				this.AddOnSerialized(obj);
			}
		}

		/// <summary>Invokes the OnSerializing callback event if the type of the object has one; and registers the object for raising the OnSerialized event if the type of the object has one.</summary>
		// Token: 0x06002663 RID: 9827 RVA: 0x0009AC41 File Offset: 0x00098E41
		public void RaiseOnSerializedEvent()
		{
			SerializationEventHandler onSerializedHandler = this._onSerializedHandler;
			if (onSerializedHandler == null)
			{
				return;
			}
			onSerializedHandler(this._context);
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x0009AC5C File Offset: 0x00098E5C
		private void AddOnSerialized(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			this._onSerializedHandler = serializationEventsForType.AddOnSerialized(obj, this._onSerializedHandler);
		}

		// Token: 0x04001256 RID: 4694
		private readonly Dictionary<object, object> _objectSeenTable;

		// Token: 0x04001257 RID: 4695
		private readonly StreamingContext _context;

		// Token: 0x04001258 RID: 4696
		private SerializationEventHandler _onSerializedHandler;
	}
}
