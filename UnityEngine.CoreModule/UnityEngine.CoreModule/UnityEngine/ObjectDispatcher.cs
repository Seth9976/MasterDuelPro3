using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000155 RID: 341
	[NativeHeader("Runtime/Misc/ObjectDispatcher.h")]
	[RequiredByNativeCode]
	[StaticAccessor("GetObjectDispatcher()", StaticAccessorType.Dot)]
	internal sealed class ObjectDispatcher : IDisposable
	{
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x0001F640 File Offset: 0x0001D840
		public bool valid
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0001F664 File Offset: 0x0001D864
		public ObjectDispatcher()
		{
			this.m_Ptr = ObjectDispatcher.CreateDispatchSystemHandle();
			this.m_TypeDataCallback = new Action<TypeDispatchData>(this.DispatchCallback);
			this.m_TransformDataCallback = new Action<TransformDispatchData>(this.DispatchCallback);
			this.m_TransformComponentCallback = new Action<Component[]>(this.DispatchCallback);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0001F6C8 File Offset: 0x0001D8C8
		~ObjectDispatcher()
		{
			this.Dispose(false);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0001F6FC File Offset: 0x0001D8FC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0001F710 File Offset: 0x0001D910
		private void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				ObjectDispatcher.DestroyDispatchSystemHandle(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0001F74C File Offset: 0x0001D94C
		private void ValidateSystemHandleAndThrow()
		{
			bool flag = !this.valid;
			if (flag)
			{
				throw new Exception("The ObjectDispatcher is invalid or has been disposed.");
			}
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0001F774 File Offset: 0x0001D974
		private void ValidateTypeAndThrow(Type type)
		{
			bool flag = !type.IsSubclassOf(typeof(Object));
			if (flag)
			{
				throw new Exception("Only types inherited from UnityEngine.Object are supported.");
			}
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0001F7A4 File Offset: 0x0001D9A4
		private void ValidateComponentTypeAndThrow(Type type)
		{
			bool flag = !type.IsSubclassOf(typeof(Component));
			if (flag)
			{
				throw new Exception("Only types inherited from UnityEngine.Component are supported.");
			}
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0001F7D4 File Offset: 0x0001D9D4
		private void DispatchCallback(TypeDispatchData data)
		{
			this.m_TypeDispatchData = default(TypeDispatchData);
			this.m_TypeDispatchData.changed = data.changed;
			this.m_TypeDispatchData.changedID = new NativeArray<int>(data.changedID, this.m_DispatchAllocator);
			this.m_TypeDispatchData.destroyedID = new NativeArray<int>(data.destroyedID, this.m_DispatchAllocator);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0001F838 File Offset: 0x0001DA38
		private void DispatchCallback(TransformDispatchData data)
		{
			this.m_TransformDispatchData = default(TransformDispatchData);
			this.m_TransformDispatchData.transformedID = new NativeArray<int>(data.transformedID, this.m_DispatchAllocator);
			this.m_TransformDispatchData.parentID = new NativeArray<int>(data.parentID, this.m_DispatchAllocator);
			this.m_TransformDispatchData.localToWorldMatrices = new NativeArray<Matrix4x4>(data.localToWorldMatrices, this.m_DispatchAllocator);
			this.m_TransformDispatchData.positions = new NativeArray<Vector3>(data.positions, this.m_DispatchAllocator);
			this.m_TransformDispatchData.rotations = new NativeArray<Quaternion>(data.rotations, this.m_DispatchAllocator);
			this.m_TransformDispatchData.scales = new NativeArray<Vector3>(data.scales, this.m_DispatchAllocator);
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0001F8FA File Offset: 0x0001DAFA
		private void DispatchCallback(Component[] components)
		{
			this.m_TransformedComponents = components;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0001F904 File Offset: 0x0001DB04
		public void DispatchTypeChangesAndClear(Type type, Action<TypeDispatchData> callback, bool sortByInstanceID = false, bool noScriptingArray = false)
		{
			this.ValidateSystemHandleAndThrow();
			this.ValidateTypeAndThrow(type);
			ObjectDispatcher.DispatchTypeChangesAndClear(this.m_Ptr, type, ObjectDispatcher.s_TypeDispatch, sortByInstanceID, noScriptingArray, callback);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0001F92C File Offset: 0x0001DB2C
		public void DispatchTransformChangesAndClear(Type type, ObjectDispatcher.TransformTrackingType trackingType, Action<TransformDispatchData> callback)
		{
			this.ValidateSystemHandleAndThrow();
			this.ValidateComponentTypeAndThrow(type);
			ObjectDispatcher.DispatchTransformDataChangesAndClear(this.m_Ptr, type, trackingType, ObjectDispatcher.s_TransformDispatch, callback);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0001F954 File Offset: 0x0001DB54
		public TypeDispatchData GetTypeChangesAndClear(Type type, Allocator allocator, bool sortByInstanceID = false, bool noScriptingArray = false)
		{
			this.m_DispatchAllocator = allocator;
			this.DispatchTypeChangesAndClear(type, this.m_TypeDataCallback, sortByInstanceID, noScriptingArray);
			return this.m_TypeDispatchData;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0001F984 File Offset: 0x0001DB84
		public TransformDispatchData GetTransformChangesAndClear(Type type, ObjectDispatcher.TransformTrackingType trackingType, Allocator allocator)
		{
			this.m_DispatchAllocator = allocator;
			this.DispatchTransformChangesAndClear(type, trackingType, this.m_TransformDataCallback);
			return this.m_TransformDispatchData;
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0001F9B4 File Offset: 0x0001DBB4
		public void EnableTypeTracking(ObjectDispatcher.TypeTrackingFlags typeTrackingMask, params Type[] types)
		{
			this.ValidateSystemHandleAndThrow();
			foreach (Type type in types)
			{
				this.ValidateTypeAndThrow(type);
				ObjectDispatcher.EnableTypeTracking(this.m_Ptr, type, typeTrackingMask);
			}
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0001F9F8 File Offset: 0x0001DBF8
		public void EnableTransformTracking(ObjectDispatcher.TransformTrackingType trackingType, params Type[] types)
		{
			this.ValidateSystemHandleAndThrow();
			foreach (Type type in types)
			{
				this.ValidateComponentTypeAndThrow(type);
				ObjectDispatcher.EnableTransformTracking(this.m_Ptr, type, trackingType);
			}
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0001FA3C File Offset: 0x0001DC3C
		public TypeDispatchData GetTypeChangesAndClear<T>(Allocator allocator, bool sortByInstanceID = false, bool noScriptingArray = false) where T : Object
		{
			return this.GetTypeChangesAndClear(typeof(T), allocator, sortByInstanceID, noScriptingArray);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0001FA64 File Offset: 0x0001DC64
		public TransformDispatchData GetTransformChangesAndClear<T>(ObjectDispatcher.TransformTrackingType trackingType, Allocator allocator) where T : Object
		{
			return this.GetTransformChangesAndClear(typeof(T), trackingType, allocator);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0001FA88 File Offset: 0x0001DC88
		public void EnableTypeTracking<T>(ObjectDispatcher.TypeTrackingFlags typeTrackingMask = ObjectDispatcher.TypeTrackingFlags.Default) where T : Object
		{
			this.EnableTypeTracking(typeTrackingMask, new Type[] { typeof(T) });
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0001FAA6 File Offset: 0x0001DCA6
		public void EnableTransformTracking<T>(ObjectDispatcher.TransformTrackingType trackingType) where T : Object
		{
			this.EnableTransformTracking(trackingType, new Type[] { typeof(T) });
		}

		// Token: 0x06000EF9 RID: 3833
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateDispatchSystemHandle();

		// Token: 0x06000EFA RID: 3834
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyDispatchSystemHandle(IntPtr ptr);

		// Token: 0x06000EFB RID: 3835
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableTypeTracking(IntPtr ptr, Type type, ObjectDispatcher.TypeTrackingFlags typeTrackingMask);

		// Token: 0x06000EFC RID: 3836
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableTransformTracking(IntPtr ptr, Type type, ObjectDispatcher.TransformTrackingType trackingType);

		// Token: 0x06000EFD RID: 3837
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DispatchTypeChangesAndClear(IntPtr ptr, Type type, Action<Object[], IntPtr, IntPtr, int, int, Action<TypeDispatchData>> callback, bool sortByInstanceID, bool noScriptingArray, Action<TypeDispatchData> param);

		// Token: 0x06000EFE RID: 3838
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DispatchTransformDataChangesAndClear(IntPtr ptr, Type type, ObjectDispatcher.TransformTrackingType trackingType, Action<IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, int, Action<TransformDispatchData>> callback, Action<TransformDispatchData> param);

		// Token: 0x040005DF RID: 1503
		private IntPtr m_Ptr = IntPtr.Zero;

		// Token: 0x040005E0 RID: 1504
		private Allocator m_DispatchAllocator;

		// Token: 0x040005E1 RID: 1505
		private TypeDispatchData m_TypeDispatchData;

		// Token: 0x040005E2 RID: 1506
		private TransformDispatchData m_TransformDispatchData;

		// Token: 0x040005E3 RID: 1507
		private Component[] m_TransformedComponents;

		// Token: 0x040005E4 RID: 1508
		private Action<TypeDispatchData> m_TypeDataCallback;

		// Token: 0x040005E5 RID: 1509
		private Action<TransformDispatchData> m_TransformDataCallback;

		// Token: 0x040005E6 RID: 1510
		private Action<Component[]> m_TransformComponentCallback;

		// Token: 0x040005E7 RID: 1511
		private static Action<Object[], IntPtr, IntPtr, int, int, Action<TypeDispatchData>> s_TypeDispatch = delegate(Object[] changed, IntPtr changedID, IntPtr destroyedID, int changedCount, int destroyedCount, Action<TypeDispatchData> callback)
		{
			NativeArray<int> changedIDArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(changedID.ToPointer(), changedCount, Allocator.Invalid);
			NativeArray<int> destroyedIDArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(destroyedID.ToPointer(), destroyedCount, Allocator.Invalid);
			TypeDispatchData dispatchData = new TypeDispatchData
			{
				changed = changed,
				changedID = changedIDArray,
				destroyedID = destroyedIDArray
			};
			callback(dispatchData);
		};

		// Token: 0x040005E8 RID: 1512
		private static Action<IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, int, Action<TransformDispatchData>> s_TransformDispatch = delegate(IntPtr transformed, IntPtr parents, IntPtr localToWorldMatrices, IntPtr positions, IntPtr rotations, IntPtr scales, int count, Action<TransformDispatchData> callback)
		{
			NativeArray<int> transformedArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(transformed.ToPointer(), count, Allocator.Invalid);
			NativeArray<int> parentArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(parents.ToPointer(), (parents != IntPtr.Zero) ? count : 0, Allocator.Invalid);
			NativeArray<Matrix4x4> localToWorldMatricesArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>(localToWorldMatrices.ToPointer(), (localToWorldMatrices != IntPtr.Zero) ? count : 0, Allocator.Invalid);
			NativeArray<Vector3> positionsArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3>(positions.ToPointer(), (positions != IntPtr.Zero) ? count : 0, Allocator.Invalid);
			NativeArray<Quaternion> rotationsArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Quaternion>(rotations.ToPointer(), (rotations != IntPtr.Zero) ? count : 0, Allocator.Invalid);
			NativeArray<Vector3> scalesArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3>(scales.ToPointer(), (scales != IntPtr.Zero) ? count : 0, Allocator.Invalid);
			TransformDispatchData dispatchData2 = new TransformDispatchData
			{
				transformedID = transformedArray,
				parentID = parentArray,
				localToWorldMatrices = localToWorldMatricesArray,
				positions = positionsArray,
				rotations = rotationsArray,
				scales = scalesArray
			};
			callback(dispatchData2);
		};

		// Token: 0x02000156 RID: 342
		public enum TransformTrackingType
		{
			// Token: 0x040005EA RID: 1514
			GlobalTRS,
			// Token: 0x040005EB RID: 1515
			LocalTRS,
			// Token: 0x040005EC RID: 1516
			Hierarchy
		}

		// Token: 0x02000157 RID: 343
		[Flags]
		public enum TypeTrackingFlags
		{
			// Token: 0x040005EE RID: 1518
			SceneObjects = 1,
			// Token: 0x040005EF RID: 1519
			Assets = 2,
			// Token: 0x040005F0 RID: 1520
			EditorOnlyObjects = 4,
			// Token: 0x040005F1 RID: 1521
			Default = 3,
			// Token: 0x040005F2 RID: 1522
			All = 7
		}
	}
}
