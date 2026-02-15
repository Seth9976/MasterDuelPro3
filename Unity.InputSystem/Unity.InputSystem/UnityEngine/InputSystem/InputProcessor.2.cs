using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000086 RID: 134
	public abstract class InputProcessor<TValue> : InputProcessor where TValue : struct
	{
		// Token: 0x0600062A RID: 1578
		public abstract TValue Process(TValue value, InputControl control);

		// Token: 0x0600062B RID: 1579 RVA: 0x00019700 File Offset: 0x00017900
		public override object ProcessAsObject(object value, InputControl control)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is TValue))
			{
				throw new ArgumentException(string.Format("Expecting value of type '{0}' but got value '{1}' of type '{2}'", typeof(TValue).Name, value, value.GetType().Name), "value");
			}
			TValue valueOfType = (TValue)((object)value);
			return this.Process(valueOfType, control);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00019768 File Offset: 0x00017968
		public unsafe override void Process(void* buffer, int bufferSize, InputControl control)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int valueSize = UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < valueSize)
			{
				throw new ArgumentException(string.Format("Expected buffer of at least {0} bytes but got buffer with just {1} bytes", valueSize, bufferSize), "bufferSize");
			}
			TValue value = default(TValue);
			void* valuePtr = UnsafeUtility.AddressOf<TValue>(ref value);
			UnsafeUtility.MemCpy(valuePtr, buffer, (long)valueSize);
			value = this.Process(value, control);
			valuePtr = UnsafeUtility.AddressOf<TValue>(ref value);
			UnsafeUtility.MemCpy(buffer, valuePtr, (long)valueSize);
		}
	}
}
