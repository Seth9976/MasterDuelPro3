using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x020001A3 RID: 419
	public static class ArrayExtensions
	{
		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002AFBC File Offset: 0x000291BC
		public static void ResizeArray<T>(this NativeArray<T> array, int capacity) where T : struct
		{
			NativeArray<T> newArray = new NativeArray<T>(capacity, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			if (array.IsCreated)
			{
				NativeArray<T>.Copy(array, newArray, array.Length);
				array.Dispose();
			}
			array = newArray;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0002AFFC File Offset: 0x000291FC
		public static void ResizeArray(this TransformAccessArray array, int capacity)
		{
			TransformAccessArray newArray = new TransformAccessArray(capacity, -1);
			if (array.isCreated)
			{
				for (int i = 0; i < array.length; i++)
				{
					newArray.Add(array[i]);
				}
				array.Dispose();
			}
			array = newArray;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002B046 File Offset: 0x00029246
		public static void ResizeArray<T>(ref T[] array, int capacity)
		{
			if (array == null)
			{
				array = new T[capacity];
				return;
			}
			Array.Resize<T>(ref array, capacity);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002B05C File Offset: 0x0002925C
		public unsafe static void FillArray<[IsUnmanaged] T>(this NativeArray<T> array, in T value, int startIndex = 0, int length = -1) where T : struct, ValueType
		{
			T* ptr = (T*)array.GetUnsafePtr<T>();
			int endIndex = ((length == -1) ? array.Length : (startIndex + length));
			for (int i = startIndex; i < endIndex; i++)
			{
				ptr[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = value;
			}
		}
	}
}
