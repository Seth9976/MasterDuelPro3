using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000057 RID: 87
	public abstract class InputBindingComposite<TValue> : InputBindingComposite where TValue : struct
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00010EEB File Offset: 0x0000F0EB
		public override Type valueType
		{
			get
			{
				return typeof(TValue);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00010EF7 File Offset: 0x0000F0F7
		public override int valueSizeInBytes
		{
			get
			{
				return UnsafeUtility.SizeOf<TValue>();
			}
		}

		// Token: 0x06000415 RID: 1045
		public abstract TValue ReadValue(ref InputBindingCompositeContext context);

		// Token: 0x06000416 RID: 1046 RVA: 0x00010F00 File Offset: 0x0000F100
		public unsafe override void ReadValue(ref InputBindingCompositeContext context, void* buffer, int bufferSize)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int valueSize = UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < valueSize)
			{
				throw new ArgumentException(string.Format("Expected buffer of at least {0} bytes but got buffer of only {1} bytes instead", UnsafeUtility.SizeOf<TValue>(), bufferSize), "bufferSize");
			}
			TValue value = this.ReadValue(ref context);
			void* valuePtr = UnsafeUtility.AddressOf<TValue>(ref value);
			UnsafeUtility.MemCpy(buffer, valuePtr, (long)valueSize);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00010F68 File Offset: 0x0000F168
		public unsafe override object ReadValueAsObject(ref InputBindingCompositeContext context)
		{
			TValue value = default(TValue);
			void* valuePtr = UnsafeUtility.AddressOf<TValue>(ref value);
			this.ReadValue(ref context, valuePtr, UnsafeUtility.SizeOf<TValue>());
			return value;
		}
	}
}
