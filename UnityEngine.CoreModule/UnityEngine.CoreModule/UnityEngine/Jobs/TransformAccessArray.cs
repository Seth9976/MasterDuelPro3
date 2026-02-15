using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.Jobs
{
	// Token: 0x020001FA RID: 506
	[NativeType(Header = "Runtime/Transform/ScriptBindings/TransformAccess.bindings.h", CodegenOptions = CodegenOptions.Custom)]
	public struct TransformAccessArray : IDisposable
	{
		// Token: 0x060013AC RID: 5036 RVA: 0x00029788 File Offset: 0x00027988
		public TransformAccessArray(int capacity, int desiredJobCount = -1)
		{
			TransformAccessArray.Allocate(capacity, desiredJobCount, out this);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00029794 File Offset: 0x00027994
		public static void Allocate(int capacity, int desiredJobCount, out TransformAccessArray array)
		{
			array.m_TransformArray = TransformAccessArray.Create(capacity, desiredJobCount);
			UnsafeUtility.LeakRecord(array.m_TransformArray, LeakCategory.TransformAccessArray, 0);
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x000297B4 File Offset: 0x000279B4
		public bool isCreated
		{
			get
			{
				return this.m_TransformArray != IntPtr.Zero;
			}
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x000297D6 File Offset: 0x000279D6
		public void Dispose()
		{
			UnsafeUtility.LeakErase(this.m_TransformArray, LeakCategory.TransformAccessArray);
			TransformAccessArray.DestroyTransformAccessArray(this.m_TransformArray);
			this.m_TransformArray = IntPtr.Zero;
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00029800 File Offset: 0x00027A00
		internal IntPtr GetTransformAccessArrayForSchedule()
		{
			return this.m_TransformArray;
		}

		// Token: 0x1700031E RID: 798
		public Transform this[int index]
		{
			get
			{
				return TransformAccessArray.GetTransform(this.m_TransformArray, index);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x00029838 File Offset: 0x00027A38
		public int length
		{
			get
			{
				return TransformAccessArray.GetLength(this.m_TransformArray);
			}
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00029855 File Offset: 0x00027A55
		public void Add(Transform transform)
		{
			TransformAccessArray.Add(this.m_TransformArray, transform);
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00029865 File Offset: 0x00027A65
		public void RemoveAtSwapBack(int index)
		{
			TransformAccessArray.RemoveAtSwapBack(this.m_TransformArray, index);
		}

		// Token: 0x060013B5 RID: 5045
		[NativeMethod(Name = "TransformAccessArrayBindings::Create", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(int capacity, int desiredJobCount);

		// Token: 0x060013B6 RID: 5046
		[NativeMethod(Name = "DestroyTransformAccessArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyTransformAccessArray(IntPtr transformArray);

		// Token: 0x060013B7 RID: 5047 RVA: 0x00029878 File Offset: 0x00027A78
		[NativeMethod(Name = "TransformAccessArrayBindings::AddTransform", IsFreeFunction = true)]
		private static void Add(IntPtr transformArrayIntPtr, Transform transform)
		{
			TransformAccessArray.Add_Injected(transformArrayIntPtr, Object.MarshalledUnityObject.Marshal<Transform>(transform));
		}

		// Token: 0x060013B8 RID: 5048
		[NativeMethod(Name = "TransformAccessArrayBindings::RemoveAtSwapBack", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveAtSwapBack(IntPtr transformArrayIntPtr, int index);

		// Token: 0x060013B9 RID: 5049
		[NativeMethod(Name = "TransformAccessArrayBindings::GetSortedTransformAccess", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSortedTransformAccess(IntPtr transformArrayIntPtr);

		// Token: 0x060013BA RID: 5050
		[NativeMethod(Name = "TransformAccessArrayBindings::GetSortedToUserIndex", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSortedToUserIndex(IntPtr transformArrayIntPtr);

		// Token: 0x060013BB RID: 5051
		[NativeMethod(Name = "TransformAccessArrayBindings::GetLength", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetLength(IntPtr transformArrayIntPtr);

		// Token: 0x060013BC RID: 5052 RVA: 0x00029894 File Offset: 0x00027A94
		[NativeMethod(Name = "TransformAccessArrayBindings::GetTransform", IsFreeFunction = true, ThrowsException = true)]
		internal static Transform GetTransform(IntPtr transformArrayIntPtr, int index)
		{
			return Unmarshal.UnmarshalUnityObject<Transform>(TransformAccessArray.GetTransform_Injected(transformArrayIntPtr, index));
		}

		// Token: 0x060013BD RID: 5053
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Add_Injected(IntPtr transformArrayIntPtr, IntPtr transform);

		// Token: 0x060013BE RID: 5054
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetTransform_Injected(IntPtr transformArrayIntPtr, int index);

		// Token: 0x04000726 RID: 1830
		private IntPtr m_TransformArray;
	}
}
