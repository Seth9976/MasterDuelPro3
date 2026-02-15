using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000071 RID: 113
	public abstract class InputControl<TValue> : InputControl where TValue : struct
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00010EEB File Offset: 0x0000F0EB
		public override Type valueType
		{
			get
			{
				return typeof(TValue);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00010EF7 File Offset: 0x0000F0F7
		public override int valueSizeInBytes
		{
			get
			{
				return UnsafeUtility.SizeOf<TValue>();
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00015280 File Offset: 0x00013480
		public unsafe readonly ref TValue value
		{
			get
			{
				if (!InputSystem.s_Manager.readValueCachingFeatureEnabled || this.m_CachedValueIsStale || this.evaluateProcessorsEveryRead)
				{
					this.m_CachedValue = this.ProcessValue(*this.unprocessedValue);
					this.m_CachedValueIsStale = false;
				}
				return ref this.m_CachedValue;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x000152D0 File Offset: 0x000134D0
		internal readonly ref TValue unprocessedValue
		{
			get
			{
				if (base.currentStatePtr == null)
				{
					return ref this.m_UnprocessedCachedValue;
				}
				if (!InputSystem.s_Manager.readValueCachingFeatureEnabled || this.m_UnprocessedCachedValueIsStale)
				{
					this.m_UnprocessedCachedValue = this.ReadUnprocessedValueFromState(base.currentStatePtr);
					this.m_UnprocessedCachedValueIsStale = false;
				}
				return ref this.m_UnprocessedCachedValue;
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00015321 File Offset: 0x00013521
		public unsafe TValue ReadValue()
		{
			return *this.value;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001532E File Offset: 0x0001352E
		public TValue ReadValueFromPreviousFrame()
		{
			return this.ReadValueFromState(base.previousFrameStatePtr);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001533C File Offset: 0x0001353C
		public TValue ReadDefaultValue()
		{
			return this.ReadValueFromState(base.defaultStatePtr);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001534A File Offset: 0x0001354A
		public unsafe TValue ReadValueFromState(void* statePtr)
		{
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return this.ProcessValue(this.ReadUnprocessedValueFromState(statePtr));
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00015369 File Offset: 0x00013569
		public unsafe TValue ReadValueFromStateWithCaching(void* statePtr)
		{
			if (statePtr != base.currentStatePtr)
			{
				return this.ReadValueFromState(statePtr);
			}
			return *this.value;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00015387 File Offset: 0x00013587
		public unsafe TValue ReadUnprocessedValueFromStateWithCaching(void* statePtr)
		{
			if (statePtr != base.currentStatePtr)
			{
				return this.ReadUnprocessedValueFromState(statePtr);
			}
			return *this.unprocessedValue;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000153A5 File Offset: 0x000135A5
		public unsafe TValue ReadUnprocessedValue()
		{
			return *this.unprocessedValue;
		}

		// Token: 0x06000553 RID: 1363
		public unsafe abstract TValue ReadUnprocessedValueFromState(void* statePtr);

		// Token: 0x06000554 RID: 1364 RVA: 0x000153B2 File Offset: 0x000135B2
		public unsafe override object ReadValueFromStateAsObject(void* statePtr)
		{
			return this.ReadValueFromState(statePtr);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000153C0 File Offset: 0x000135C0
		public unsafe override void ReadValueFromStateIntoBuffer(void* statePtr, void* bufferPtr, int bufferSize)
		{
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			if (bufferPtr == null)
			{
				throw new ArgumentNullException("bufferPtr");
			}
			int numBytes = UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < numBytes)
			{
				throw new ArgumentException(string.Format("bufferSize={0} < sizeof(TValue)={1}", bufferSize, numBytes), "bufferSize");
			}
			TValue value = this.ReadValueFromState(statePtr);
			void* valuePtr = UnsafeUtility.AddressOf<TValue>(ref value);
			UnsafeUtility.MemCpy(bufferPtr, valuePtr, (long)numBytes);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00015434 File Offset: 0x00013634
		public unsafe override void WriteValueFromBufferIntoState(void* bufferPtr, int bufferSize, void* statePtr)
		{
			if (bufferPtr == null)
			{
				throw new ArgumentNullException("bufferPtr");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			int numBytes = UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < numBytes)
			{
				throw new ArgumentException(string.Format("bufferSize={0} < sizeof(TValue)={1}", bufferSize, numBytes), "bufferSize");
			}
			TValue value = default(TValue);
			UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TValue>(ref value), bufferPtr, (long)numBytes);
			this.WriteValueIntoState(value, statePtr);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000154AC File Offset: 0x000136AC
		public unsafe override void WriteValueFromObjectIntoState(object value, void* statePtr)
		{
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is TValue))
			{
				value = Convert.ChangeType(value, typeof(TValue));
			}
			TValue valueOfType = (TValue)((object)value);
			this.WriteValueIntoState(valueOfType, statePtr);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00014C02 File Offset: 0x00012E02
		public unsafe virtual void WriteValueIntoState(TValue value, void* statePtr)
		{
			throw new NotSupportedException(string.Format("Control '{0}' does not support writing", this));
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00015500 File Offset: 0x00013700
		public unsafe override object ReadValueFromBufferAsObject(void* buffer, int bufferSize)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int valueSize = UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < valueSize)
			{
				throw new ArgumentException(string.Format("Expecting buffer of at least {0} bytes for value of type {1} but got buffer of only {2} bytes instead", valueSize, typeof(TValue).Name, bufferSize), "bufferSize");
			}
			TValue value = default(TValue);
			UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TValue>(ref value), buffer, (long)valueSize);
			return value;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00015574 File Offset: 0x00013774
		private unsafe static bool CompareValue(ref TValue firstValue, ref TValue secondValue)
		{
			void* ptr = UnsafeUtility.AddressOf<TValue>(ref firstValue);
			void* secondValuePtr = UnsafeUtility.AddressOf<TValue>(ref secondValue);
			return UnsafeUtility.MemCmp(ptr, secondValuePtr, (long)UnsafeUtility.SizeOf<TValue>()) != 0;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000155A0 File Offset: 0x000137A0
		public unsafe override bool CompareValue(void* firstStatePtr, void* secondStatePtr)
		{
			TValue firstValue = this.ReadValueFromState(firstStatePtr);
			TValue secondValue = this.ReadValueFromState(secondStatePtr);
			return InputControl<TValue>.CompareValue(ref firstValue, ref secondValue);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000155C6 File Offset: 0x000137C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TValue ProcessValue(TValue value)
		{
			this.ProcessValue(ref value);
			return value;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000155D4 File Offset: 0x000137D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ProcessValue(ref TValue value)
		{
			if (this.m_ProcessorStack.length <= 0)
			{
				return;
			}
			value = this.m_ProcessorStack.firstValue.Process(value, this);
			if (this.m_ProcessorStack.additionalValues == null)
			{
				return;
			}
			for (int i = 0; i < this.m_ProcessorStack.length - 1; i++)
			{
				value = this.m_ProcessorStack.additionalValues[i].Process(value, this);
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00015654 File Offset: 0x00013854
		internal TProcessor TryGetProcessor<TProcessor>() where TProcessor : InputProcessor<TValue>
		{
			if (this.m_ProcessorStack.length > 0)
			{
				TProcessor processor = this.m_ProcessorStack.firstValue as TProcessor;
				if (processor != null)
				{
					return processor;
				}
				if (this.m_ProcessorStack.additionalValues != null)
				{
					for (int i = 0; i < this.m_ProcessorStack.length - 1; i++)
					{
						TProcessor result = this.m_ProcessorStack.additionalValues[i] as TProcessor;
						if (result != null)
						{
							return result;
						}
					}
				}
			}
			return default(TProcessor);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000156E0 File Offset: 0x000138E0
		internal override void AddProcessor(object processor)
		{
			InputProcessor<TValue> processorOfType = processor as InputProcessor<TValue>;
			if (processorOfType == null)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Cannot add processor of type '",
					processor.GetType().Name,
					"' to control of type '",
					base.GetType().Name,
					"'"
				}), "processor");
			}
			this.m_ProcessorStack.Append(processorOfType);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00015750 File Offset: 0x00013950
		protected override void FinishSetup()
		{
			using (IEnumerator<InputProcessor<TValue>> enumerator = this.m_ProcessorStack.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.cachingPolicy == InputProcessor.CachingPolicy.EvaluateOnEveryRead)
					{
						this.evaluateProcessorsEveryRead = true;
					}
				}
			}
			base.FinishSetup();
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x000157AC File Offset: 0x000139AC
		internal InputProcessor<TValue>[] processors
		{
			get
			{
				return this.m_ProcessorStack.ToArray();
			}
		}

		// Token: 0x04000291 RID: 657
		internal InlinedArray<InputProcessor<TValue>> m_ProcessorStack;

		// Token: 0x04000292 RID: 658
		private TValue m_CachedValue;

		// Token: 0x04000293 RID: 659
		private TValue m_UnprocessedCachedValue;

		// Token: 0x04000294 RID: 660
		internal bool evaluateProcessorsEveryRead;
	}
}
