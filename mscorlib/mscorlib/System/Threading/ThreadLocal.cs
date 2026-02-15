using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Threading
{
	/// <summary>Provides thread-local storage of data.</summary>
	/// <typeparam name="T">Specifies the type of data stored per-thread.</typeparam>
	// Token: 0x0200024D RID: 589
	[DebuggerTypeProxy(typeof(SystemThreading_ThreadLocalDebugView<>))]
	[DebuggerDisplay("IsValueCreated={IsValueCreated}, Value={ValueForDebugDisplay}, Count={ValuesCountForDebugDisplay}")]
	public class ThreadLocal<T> : IDisposable
	{
		/// <summary>Initializes the <see cref="T:System.Threading.ThreadLocal`1" /> instance.</summary>
		// Token: 0x06001594 RID: 5524 RVA: 0x00056F07 File Offset: 0x00055107
		public ThreadLocal()
		{
			this.Initialize(null, false);
		}

		/// <summary>Initializes the <see cref="T:System.Threading.ThreadLocal`1" /> instance.</summary>
		/// <param name="trackAllValues">Whether to track all values set on the instance and expose them through the <see cref="P:System.Threading.ThreadLocal`1.Values" /> property.</param>
		// Token: 0x06001595 RID: 5525 RVA: 0x00056F23 File Offset: 0x00055123
		public ThreadLocal(bool trackAllValues)
		{
			this.Initialize(null, trackAllValues);
		}

		/// <summary>Initializes the <see cref="T:System.Threading.ThreadLocal`1" /> instance with the specified <paramref name="valueFactory" /> function.</summary>
		/// <param name="valueFactory">The  <see cref="T:System.Func`1" /> invoked to produce a lazily-initialized value when an attempt is made to retrieve <see cref="P:System.Threading.ThreadLocal`1.Value" /> without it having been previously initialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="valueFactory" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x06001596 RID: 5526 RVA: 0x00056F3F File Offset: 0x0005513F
		public ThreadLocal(Func<T> valueFactory)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.Initialize(valueFactory, false);
		}

		/// <summary>Initializes the <see cref="T:System.Threading.ThreadLocal`1" /> instance with the specified <paramref name="valueFactory" /> function.</summary>
		/// <param name="valueFactory">The <see cref="T:System.Func`1" /> invoked to produce a lazily-initialized value when an attempt is made to retrieve <see cref="P:System.Threading.ThreadLocal`1.Value" /> without it having been previously initialized.</param>
		/// <param name="trackAllValues">Whether to track all values set on the instance and expose them via the <see cref="P:System.Threading.ThreadLocal`1.Values" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="valueFactory" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x06001597 RID: 5527 RVA: 0x00056F69 File Offset: 0x00055169
		public ThreadLocal(Func<T> valueFactory, bool trackAllValues)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.Initialize(valueFactory, trackAllValues);
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x00056F94 File Offset: 0x00055194
		private void Initialize(Func<T> valueFactory, bool trackAllValues)
		{
			this.m_valueFactory = valueFactory;
			this.m_trackAllValues = trackAllValues;
			try
			{
			}
			finally
			{
				this.m_idComplement = ~ThreadLocal<T>.s_idManager.GetId();
				this.m_initialized = true;
			}
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x00056FDC File Offset: 0x000551DC
		~ThreadLocal()
		{
			this.Dispose(false);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.ThreadLocal`1" /> class.</summary>
		// Token: 0x0600159A RID: 5530 RVA: 0x0005700C File Offset: 0x0005520C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the resources used by this <see cref="T:System.Threading.ThreadLocal`1" /> instance.</summary>
		/// <param name="disposing">A Boolean value that indicates whether this method is being called due to a call to <see cref="M:System.Threading.ThreadLocal`1.Dispose" />.</param>
		// Token: 0x0600159B RID: 5531 RVA: 0x0005701C File Offset: 0x0005521C
		protected virtual void Dispose(bool disposing)
		{
			ThreadLocal<T>.IdManager idManager = ThreadLocal<T>.s_idManager;
			int num;
			lock (idManager)
			{
				num = ~this.m_idComplement;
				this.m_idComplement = 0;
				if (num < 0 || !this.m_initialized)
				{
					return;
				}
				this.m_initialized = false;
				for (ThreadLocal<T>.LinkedSlot linkedSlot = this.m_linkedSlot.Next; linkedSlot != null; linkedSlot = linkedSlot.Next)
				{
					ThreadLocal<T>.LinkedSlotVolatile[] slotArray = linkedSlot.SlotArray;
					if (slotArray != null)
					{
						linkedSlot.SlotArray = null;
						slotArray[num].Value.Value = default(T);
						slotArray[num].Value = null;
					}
				}
			}
			this.m_linkedSlot = null;
			ThreadLocal<T>.s_idManager.ReturnId(num);
		}

		/// <summary>Creates and returns a string representation of this instance for the current thread.</summary>
		/// <returns>The result of calling <see cref="M:System.Object.ToString" /> on the <see cref="P:System.Threading.ThreadLocal`1.Value" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ThreadLocal`1" /> instance has been disposed.</exception>
		/// <exception cref="T:System.NullReferenceException">The <see cref="P:System.Threading.ThreadLocal`1.Value" /> for the current thread is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.InvalidOperationException">The initialization function attempted to reference <see cref="P:System.Threading.ThreadLocal`1.Value" /> recursively.</exception>
		/// <exception cref="T:System.MissingMemberException">No default constructor is provided and no value factory is supplied.</exception>
		// Token: 0x0600159C RID: 5532 RVA: 0x000570F0 File Offset: 0x000552F0
		public override string ToString()
		{
			T value = this.Value;
			return value.ToString();
		}

		/// <summary>Gets or sets the value of this instance for the current thread.</summary>
		/// <returns>Returns an instance of the object that this ThreadLocal is responsible for initializing.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ThreadLocal`1" /> instance has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The initialization function attempted to reference <see cref="P:System.Threading.ThreadLocal`1.Value" /> recursively.</exception>
		/// <exception cref="T:System.MissingMemberException">No default constructor is provided and no value factory is supplied.</exception>
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x00057114 File Offset: 0x00055314
		// (set) Token: 0x0600159E RID: 5534 RVA: 0x00057168 File Offset: 0x00055368
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public T Value
		{
			get
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this.m_idComplement;
				ThreadLocal<T>.LinkedSlot value;
				if (array != null && num >= 0 && num < array.Length && (value = array[num].Value) != null && this.m_initialized)
				{
					return value.Value;
				}
				return this.GetValueSlow();
			}
			set
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this.m_idComplement;
				ThreadLocal<T>.LinkedSlot value2;
				if (array != null && num >= 0 && num < array.Length && (value2 = array[num].Value) != null && this.m_initialized)
				{
					value2.Value = value;
					return;
				}
				this.SetValueSlow(value, array);
			}
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x000571BC File Offset: 0x000553BC
		private T GetValueSlow()
		{
			if (~this.m_idComplement < 0)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
			}
			Debugger.NotifyOfCrossThreadDependency();
			T t;
			if (this.m_valueFactory == null)
			{
				t = default(T);
			}
			else
			{
				t = this.m_valueFactory();
				if (this.IsValueCreated)
				{
					throw new InvalidOperationException(Environment.GetResourceString("ValueFactory attempted to access the Value property of this instance."));
				}
			}
			this.Value = t;
			return t;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00057228 File Offset: 0x00055428
		private void SetValueSlow(T value, ThreadLocal<T>.LinkedSlotVolatile[] slotArray)
		{
			int num = ~this.m_idComplement;
			if (num < 0)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
			}
			if (slotArray == null)
			{
				slotArray = new ThreadLocal<T>.LinkedSlotVolatile[ThreadLocal<T>.GetNewTableSize(num + 1)];
				ThreadLocal<T>.ts_finalizationHelper = new ThreadLocal<T>.FinalizationHelper(slotArray, this.m_trackAllValues);
				ThreadLocal<T>.ts_slotArray = slotArray;
			}
			if (num >= slotArray.Length)
			{
				this.GrowTable(ref slotArray, num + 1);
				ThreadLocal<T>.ts_finalizationHelper.SlotArray = slotArray;
				ThreadLocal<T>.ts_slotArray = slotArray;
			}
			if (slotArray[num].Value == null)
			{
				this.CreateLinkedSlot(slotArray, num, value);
				return;
			}
			ThreadLocal<T>.LinkedSlot value2 = slotArray[num].Value;
			if (!this.m_initialized)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
			}
			value2.Value = value;
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x000572E4 File Offset: 0x000554E4
		private void CreateLinkedSlot(ThreadLocal<T>.LinkedSlotVolatile[] slotArray, int id, T value)
		{
			ThreadLocal<T>.LinkedSlot linkedSlot = new ThreadLocal<T>.LinkedSlot(slotArray);
			ThreadLocal<T>.IdManager idManager = ThreadLocal<T>.s_idManager;
			lock (idManager)
			{
				if (!this.m_initialized)
				{
					throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
				}
				ThreadLocal<T>.LinkedSlot next = this.m_linkedSlot.Next;
				linkedSlot.Next = next;
				linkedSlot.Previous = this.m_linkedSlot;
				linkedSlot.Value = value;
				if (next != null)
				{
					next.Previous = linkedSlot;
				}
				this.m_linkedSlot.Next = linkedSlot;
				slotArray[id].Value = linkedSlot;
			}
		}

		/// <summary>Gets a list for all of the values currently stored by all of the threads that have accessed this instance.</summary>
		/// <returns>A list for all of the values currently stored by all of the threads that have accessed this instance.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ThreadLocal`1" /> instance has been disposed.</exception>
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x00057394 File Offset: 0x00055594
		public IList<T> Values
		{
			get
			{
				if (!this.m_trackAllValues)
				{
					throw new InvalidOperationException(Environment.GetResourceString("The ThreadLocal object is not tracking values. To use the Values property, use a ThreadLocal constructor that accepts the trackAllValues parameter and set the parameter to true."));
				}
				List<T> valuesAsList = this.GetValuesAsList();
				if (valuesAsList == null)
				{
					throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
				}
				return valuesAsList;
			}
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x000573C8 File Offset: 0x000555C8
		private List<T> GetValuesAsList()
		{
			List<T> list = new List<T>();
			if (~this.m_idComplement == -1)
			{
				return null;
			}
			for (ThreadLocal<T>.LinkedSlot linkedSlot = this.m_linkedSlot.Next; linkedSlot != null; linkedSlot = linkedSlot.Next)
			{
				list.Add(linkedSlot.Value);
			}
			return list;
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x00057410 File Offset: 0x00055610
		private int ValuesCountForDebugDisplay
		{
			get
			{
				int num = 0;
				for (ThreadLocal<T>.LinkedSlot linkedSlot = this.m_linkedSlot.Next; linkedSlot != null; linkedSlot = linkedSlot.Next)
				{
					num++;
				}
				return num;
			}
		}

		/// <summary>Gets whether <see cref="P:System.Threading.ThreadLocal`1.Value" /> is initialized on the current thread.</summary>
		/// <returns>true if <see cref="P:System.Threading.ThreadLocal`1.Value" /> is initialized on the current thread; otherwise false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ThreadLocal`1" /> instance has been disposed.</exception>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x00057440 File Offset: 0x00055640
		public bool IsValueCreated
		{
			get
			{
				int num = ~this.m_idComplement;
				if (num < 0)
				{
					throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
				}
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				return array != null && num < array.Length && array[num].Value != null;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x0005748C File Offset: 0x0005568C
		internal T ValueForDebugDisplay
		{
			get
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this.m_idComplement;
				ThreadLocal<T>.LinkedSlot value;
				if (array == null || num >= array.Length || (value = array[num].Value) == null || !this.m_initialized)
				{
					return default(T);
				}
				return value.Value;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x000574DC File Offset: 0x000556DC
		internal List<T> ValuesForDebugDisplay
		{
			get
			{
				return this.GetValuesAsList();
			}
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x000574E4 File Offset: 0x000556E4
		private void GrowTable(ref ThreadLocal<T>.LinkedSlotVolatile[] table, int minLength)
		{
			ThreadLocal<T>.LinkedSlotVolatile[] array = new ThreadLocal<T>.LinkedSlotVolatile[ThreadLocal<T>.GetNewTableSize(minLength)];
			ThreadLocal<T>.IdManager idManager = ThreadLocal<T>.s_idManager;
			lock (idManager)
			{
				for (int i = 0; i < table.Length; i++)
				{
					ThreadLocal<T>.LinkedSlot value = table[i].Value;
					if (value != null && value.SlotArray != null)
					{
						value.SlotArray = array;
						array[i] = table[i];
					}
				}
			}
			table = array;
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x00057574 File Offset: 0x00055774
		private static int GetNewTableSize(int minSize)
		{
			if (minSize > 2146435071)
			{
				return int.MaxValue;
			}
			int num = minSize - 1;
			num |= num >> 1;
			num |= num >> 2;
			num |= num >> 4;
			num |= num >> 8;
			num |= num >> 16;
			num++;
			if (num > 2146435071)
			{
				num = 2146435071;
			}
			return num;
		}

		// Token: 0x04000AA6 RID: 2726
		private Func<T> m_valueFactory;

		// Token: 0x04000AA7 RID: 2727
		[ThreadStatic]
		private static ThreadLocal<T>.LinkedSlotVolatile[] ts_slotArray;

		// Token: 0x04000AA8 RID: 2728
		[ThreadStatic]
		private static ThreadLocal<T>.FinalizationHelper ts_finalizationHelper;

		// Token: 0x04000AA9 RID: 2729
		private int m_idComplement;

		// Token: 0x04000AAA RID: 2730
		private volatile bool m_initialized;

		// Token: 0x04000AAB RID: 2731
		private static ThreadLocal<T>.IdManager s_idManager = new ThreadLocal<T>.IdManager();

		// Token: 0x04000AAC RID: 2732
		private ThreadLocal<T>.LinkedSlot m_linkedSlot = new ThreadLocal<T>.LinkedSlot(null);

		// Token: 0x04000AAD RID: 2733
		private bool m_trackAllValues;

		// Token: 0x0200024E RID: 590
		private struct LinkedSlotVolatile
		{
			// Token: 0x04000AAE RID: 2734
			internal volatile ThreadLocal<T>.LinkedSlot Value;
		}

		// Token: 0x0200024F RID: 591
		private sealed class LinkedSlot
		{
			// Token: 0x060015AB RID: 5547 RVA: 0x000575D3 File Offset: 0x000557D3
			internal LinkedSlot(ThreadLocal<T>.LinkedSlotVolatile[] slotArray)
			{
				this.SlotArray = slotArray;
			}

			// Token: 0x04000AAF RID: 2735
			internal volatile ThreadLocal<T>.LinkedSlot Next;

			// Token: 0x04000AB0 RID: 2736
			internal volatile ThreadLocal<T>.LinkedSlot Previous;

			// Token: 0x04000AB1 RID: 2737
			internal volatile ThreadLocal<T>.LinkedSlotVolatile[] SlotArray;

			// Token: 0x04000AB2 RID: 2738
			internal T Value;
		}

		// Token: 0x02000250 RID: 592
		private class IdManager
		{
			// Token: 0x060015AC RID: 5548 RVA: 0x000575E4 File Offset: 0x000557E4
			internal int GetId()
			{
				List<bool> freeIds = this.m_freeIds;
				int num2;
				lock (freeIds)
				{
					int num = this.m_nextIdToTry;
					while (num < this.m_freeIds.Count && !this.m_freeIds[num])
					{
						num++;
					}
					if (num == this.m_freeIds.Count)
					{
						this.m_freeIds.Add(false);
					}
					else
					{
						this.m_freeIds[num] = false;
					}
					this.m_nextIdToTry = num + 1;
					num2 = num;
				}
				return num2;
			}

			// Token: 0x060015AD RID: 5549 RVA: 0x0005767C File Offset: 0x0005587C
			internal void ReturnId(int id)
			{
				List<bool> freeIds = this.m_freeIds;
				lock (freeIds)
				{
					this.m_freeIds[id] = true;
					if (id < this.m_nextIdToTry)
					{
						this.m_nextIdToTry = id;
					}
				}
			}

			// Token: 0x04000AB3 RID: 2739
			private int m_nextIdToTry;

			// Token: 0x04000AB4 RID: 2740
			private List<bool> m_freeIds = new List<bool>();
		}

		// Token: 0x02000251 RID: 593
		private class FinalizationHelper
		{
			// Token: 0x060015AF RID: 5551 RVA: 0x000576E7 File Offset: 0x000558E7
			internal FinalizationHelper(ThreadLocal<T>.LinkedSlotVolatile[] slotArray, bool trackAllValues)
			{
				this.SlotArray = slotArray;
				this.m_trackAllValues = trackAllValues;
			}

			// Token: 0x060015B0 RID: 5552 RVA: 0x00057700 File Offset: 0x00055900
			protected override void Finalize()
			{
				try
				{
					ThreadLocal<T>.LinkedSlotVolatile[] slotArray = this.SlotArray;
					for (int i = 0; i < slotArray.Length; i++)
					{
						ThreadLocal<T>.LinkedSlot value = slotArray[i].Value;
						if (value != null)
						{
							if (this.m_trackAllValues)
							{
								value.SlotArray = null;
							}
							else
							{
								ThreadLocal<T>.IdManager s_idManager = ThreadLocal<T>.s_idManager;
								lock (s_idManager)
								{
									if (value.Next != null)
									{
										value.Next.Previous = value.Previous;
									}
									value.Previous.Next = value.Next;
								}
							}
						}
					}
				}
				finally
				{
					base.Finalize();
				}
			}

			// Token: 0x04000AB5 RID: 2741
			internal ThreadLocal<T>.LinkedSlotVolatile[] SlotArray;

			// Token: 0x04000AB6 RID: 2742
			private bool m_trackAllValues;
		}
	}
}
