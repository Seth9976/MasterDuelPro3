using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020001C3 RID: 451
	[NativeHeader("Runtime/Export/Scripting/UnityEngineObject.bindings.h")]
	[NativeHeader("Runtime/GameCode/CloneObject.h")]
	[NativeHeader("Runtime/SceneManager/SceneManager.h")]
	[RequiredByNativeCode(GenerateProxy = true)]
	[StructLayout(LayoutKind.Sequential)]
	public class Object
	{
		// Token: 0x06001142 RID: 4418 RVA: 0x00024F74 File Offset: 0x00023174
		public unsafe int GetInstanceID()
		{
			bool flag = this.m_CachedPtr == IntPtr.Zero;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				num = *(int*)((byte*)(void*)this.m_CachedPtr + Object.OffsetOfInstanceIDInCPlusPlusObject);
			}
			return num;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00024FB4 File Offset: 0x000231B4
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00024FCC File Offset: 0x000231CC
		public override bool Equals(object other)
		{
			Object otherAsObject = other as Object;
			bool flag = otherAsObject == null && other != null && !(other is Object);
			return !flag && Object.CompareBaseObjects(this, otherAsObject);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00025010 File Offset: 0x00023210
		public static implicit operator bool(Object exists)
		{
			return !Object.CompareBaseObjects(exists, null);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0002502C File Offset: 0x0002322C
		private static bool CompareBaseObjects(Object lhs, Object rhs)
		{
			bool lhsNull = lhs == null;
			bool rhsNull = rhs == null;
			bool flag = rhsNull && lhsNull;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = rhsNull;
				if (flag3)
				{
					flag2 = !Object.IsNativeObjectAlive(lhs);
				}
				else
				{
					bool flag4 = lhsNull;
					if (flag4)
					{
						flag2 = !Object.IsNativeObjectAlive(rhs);
					}
					else
					{
						flag2 = lhs == rhs;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00025080 File Offset: 0x00023280
		private void EnsureRunningOnMainThread()
		{
			bool flag = !Object.CurrentThreadIsMainThread();
			if (flag)
			{
				throw new InvalidOperationException("EnsureRunningOnMainThread can only be called from the main thread");
			}
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x000250A8 File Offset: 0x000232A8
		private static bool IsNativeObjectAlive(Object o)
		{
			return o.GetCachedPtr() != IntPtr.Zero;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x000250CC File Offset: 0x000232CC
		private IntPtr GetCachedPtr()
		{
			return this.m_CachedPtr;
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x000250E4 File Offset: 0x000232E4
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x000250FC File Offset: 0x000232FC
		public string name
		{
			get
			{
				return this.GetName();
			}
			set
			{
				this.SetName(value);
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00025108 File Offset: 0x00023308
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original) where T : Object
		{
			return Object.InstantiateAsync<T>(original, 1, null, ReadOnlySpan<Vector3>.Empty, ReadOnlySpan<Quaternion>.Empty);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0002512C File Offset: 0x0002332C
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, Transform parent) where T : Object
		{
			return Object.InstantiateAsync<T>(original, 1, parent, ReadOnlySpan<Vector3>.Empty, ReadOnlySpan<Quaternion>.Empty);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00025150 File Offset: 0x00023350
		public unsafe static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, Vector3 position, Quaternion rotation) where T : Object
		{
			return Object.InstantiateAsync<T>(original, 1, null, new ReadOnlySpan<Vector3>((void*)(&position), 1), new ReadOnlySpan<Quaternion>((void*)(&rotation), 1));
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00025180 File Offset: 0x00023380
		public unsafe static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, Transform parent, Vector3 position, Quaternion rotation) where T : Object
		{
			return Object.InstantiateAsync<T>(original, 1, parent, new ReadOnlySpan<Vector3>((void*)(&position), 1), new ReadOnlySpan<Quaternion>((void*)(&rotation), 1));
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x000251B0 File Offset: 0x000233B0
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, null, ReadOnlySpan<Vector3>.Empty, ReadOnlySpan<Quaternion>.Empty);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x000251D4 File Offset: 0x000233D4
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, parent, ReadOnlySpan<Vector3>.Empty, ReadOnlySpan<Quaternion>.Empty);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x000251F8 File Offset: 0x000233F8
		public unsafe static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Vector3 position, Quaternion rotation) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, null, new ReadOnlySpan<Vector3>((void*)(&position), 1), new ReadOnlySpan<Quaternion>((void*)(&rotation), 1));
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00025228 File Offset: 0x00023428
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, null, positions, rotations);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00025244 File Offset: 0x00023444
		public unsafe static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, Vector3 position, Quaternion rotation) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, parent, new ReadOnlySpan<Vector3>((void*)(&position), 1), new ReadOnlySpan<Quaternion>((void*)(&rotation), 1));
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00025274 File Offset: 0x00023474
		public unsafe static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, Vector3 position, Quaternion rotation, CancellationToken cancellationToken) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, parent, new ReadOnlySpan<Vector3>((void*)(&position), 1), new ReadOnlySpan<Quaternion>((void*)(&rotation), 1), cancellationToken);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000252A4 File Offset: 0x000234A4
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, parent, positions, rotations, CancellationToken.None);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000252C8 File Offset: 0x000234C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations, CancellationToken cancellationToken) where T : Object
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			bool flag = count <= 0;
			if (flag)
			{
				throw new ArgumentException("Cannot call instantiate multiple with count less or equal to zero");
			}
			fixed (Vector3* pinnableReference = positions.GetPinnableReference())
			{
				Vector3* positionsPtr = pinnableReference;
				fixed (Quaternion* pinnableReference2 = rotations.GetPinnableReference())
				{
					Quaternion* rotationsPtr = pinnableReference2;
					return new AsyncInstantiateOperation<T>(Object.Internal_InstantiateAsyncWithParent(original, count, parent, (IntPtr)((void*)positionsPtr), positions.Length, (IntPtr)((void*)rotationsPtr), rotations.Length, cancellationToken.CanBeCanceled), cancellationToken);
				}
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00025354 File Offset: 0x00023554
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation)
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			bool flag = original is ScriptableObject;
			if (flag)
			{
				throw new ArgumentException("Cannot instantiate a ScriptableObject with a position and rotation");
			}
			Object obj = Object.Internal_InstantiateSingle(original, position, rotation);
			bool flag2 = obj == null;
			if (flag2)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return obj;
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x000253AC File Offset: 0x000235AC
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent)
		{
			bool flag = parent == null;
			Object @object;
			if (flag)
			{
				@object = Object.Instantiate(original, position, rotation);
			}
			else
			{
				Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
				Object obj = Object.Internal_InstantiateSingleWithParent(original, parent, position, rotation);
				bool flag2 = obj == null;
				if (flag2)
				{
					throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
				}
				@object = obj;
			}
			return @object;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00025404 File Offset: 0x00023604
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original)
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			Object obj = Object.Internal_CloneSingle(original);
			bool flag = obj == null;
			if (flag)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return obj;
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00025440 File Offset: 0x00023640
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Scene scene)
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			Object obj = Object.Internal_CloneSingleWithScene(original, scene);
			bool flag = obj == null;
			if (flag)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return obj;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00025480 File Offset: 0x00023680
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Transform parent)
		{
			return Object.Instantiate(original, parent, false);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0002549C File Offset: 0x0002369C
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Transform parent, bool instantiateInWorldSpace)
		{
			bool flag = parent == null;
			Object @object;
			if (flag)
			{
				@object = Object.Instantiate(original);
			}
			else
			{
				Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
				Object obj = Object.Internal_CloneSingleWithParent(original, parent, instantiateInWorldSpace);
				bool flag2 = obj == null;
				if (flag2)
				{
					throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
				}
				@object = obj;
			}
			return @object;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000254F0 File Offset: 0x000236F0
		public static T Instantiate<T>(T original) where T : Object
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			T obj = (T)((object)Object.Internal_CloneSingle(original));
			bool flag = obj == null;
			if (flag)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return obj;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00025540 File Offset: 0x00023740
		public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object
		{
			return (T)((object)Object.Instantiate(original, position, rotation));
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00025564 File Offset: 0x00023764
		public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
		{
			return (T)((object)Object.Instantiate(original, position, rotation, parent));
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0002558C File Offset: 0x0002378C
		public static T Instantiate<T>(T original, Transform parent) where T : Object
		{
			return Object.Instantiate<T>(original, parent, false);
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x000255A8 File Offset: 0x000237A8
		public static T Instantiate<T>(T original, Transform parent, bool worldPositionStays) where T : Object
		{
			return (T)((object)Object.Instantiate(original, parent, worldPositionStays));
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x000255CC File Offset: 0x000237CC
		[NativeMethod(Name = "Scripting::DestroyObjectFromScripting", IsFreeFunction = true, ThrowsException = true)]
		public static void Destroy(Object obj, [DefaultValue("0.0F")] float t)
		{
			Object.Destroy_Injected(Object.MarshalledUnityObject.Marshal<Object>(obj), t);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x000255E8 File Offset: 0x000237E8
		[ExcludeFromDocs]
		public static void Destroy(Object obj)
		{
			float t = 0f;
			Object.Destroy(obj, t);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00025604 File Offset: 0x00023804
		[NativeMethod(Name = "Scripting::DestroyObjectFromScriptingImmediate", IsFreeFunction = true, ThrowsException = true)]
		public static void DestroyImmediate(Object obj, [DefaultValue("false")] bool allowDestroyingAssets)
		{
			Object.DestroyImmediate_Injected(Object.MarshalledUnityObject.Marshal<Object>(obj), allowDestroyingAssets);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00025620 File Offset: 0x00023820
		[ExcludeFromDocs]
		public static void DestroyImmediate(Object obj)
		{
			bool allowDestroyingAssets = false;
			Object.DestroyImmediate(obj, allowDestroyingAssets);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00025638 File Offset: 0x00023838
		[Obsolete("Object.FindObjectsOfType has been deprecated. Use Object.FindObjectsByType instead which lets you decide whether you need the results sorted or not.  FindObjectsOfType sorts the results by InstanceID, but if you do not need this using FindObjectSortMode.None is considerably faster.", false)]
		public static Object[] FindObjectsOfType(Type type)
		{
			return Object.FindObjectsOfType(type, false);
		}

		// Token: 0x06001168 RID: 4456
		[Obsolete("Object.FindObjectsOfType has been deprecated. Use Object.FindObjectsByType instead which lets you decide whether you need the results sorted or not.  FindObjectsOfType sorts the results by InstanceID but if you do not need this using FindObjectSortMode.None is considerably faster.", false)]
		[FreeFunction("UnityEngineObjectBindings::FindObjectsOfType")]
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfType(Type type, bool includeInactive);

		// Token: 0x06001169 RID: 4457 RVA: 0x00025654 File Offset: 0x00023854
		public static Object[] FindObjectsByType(Type type, FindObjectsSortMode sortMode)
		{
			return Object.FindObjectsByType(type, FindObjectsInactive.Exclude, sortMode);
		}

		// Token: 0x0600116A RID: 4458
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[FreeFunction("UnityEngineObjectBindings::FindObjectsByType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsByType(Type type, FindObjectsInactive findObjectsInactive, FindObjectsSortMode sortMode);

		// Token: 0x0600116B RID: 4459 RVA: 0x00025670 File Offset: 0x00023870
		[FreeFunction("GetSceneManager().DontDestroyOnLoad", ThrowsException = true)]
		public static void DontDestroyOnLoad([NotNull] Object target)
		{
			if (target == null)
			{
				ThrowHelper.ThrowArgumentNullException(target, "target");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(target);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(target, "target");
			}
			Object.DontDestroyOnLoad_Injected(intPtr);
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x000256A8 File Offset: 0x000238A8
		// (set) Token: 0x0600116D RID: 4461 RVA: 0x000256CC File Offset: 0x000238CC
		public HideFlags hideFlags
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Object.get_hideFlags_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Object.set_hideFlags_Injected(intPtr, value);
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000256EF File Offset: 0x000238EF
		[Obsolete("use Object.Destroy instead.")]
		public static void DestroyObject(Object obj, [DefaultValue("0.0F")] float t)
		{
			Object.Destroy(obj, t);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000256FC File Offset: 0x000238FC
		[ExcludeFromDocs]
		[Obsolete("use Object.Destroy instead.")]
		public static void DestroyObject(Object obj)
		{
			float t = 0f;
			Object.Destroy(obj, t);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00025718 File Offset: 0x00023918
		[Obsolete("Object.FindSceneObjectsOfType has been deprecated, Use Object.FindObjectsByType instead which lets you decide whether you need the results sorted or not.  FindSceneObjectsOfType sorts the results by InstanceID but if you do not need this using FindObjectSortMode.None is considerably faster.", false)]
		public static Object[] FindSceneObjectsOfType(Type type)
		{
			return Object.FindObjectsOfType(type);
		}

		// Token: 0x06001171 RID: 4465
		[Obsolete("use Resources.FindObjectsOfTypeAll instead.")]
		[FreeFunction("UnityEngineObjectBindings::FindObjectsOfTypeIncludingAssets")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfTypeIncludingAssets(Type type);

		// Token: 0x06001172 RID: 4466 RVA: 0x00025730 File Offset: 0x00023930
		[Obsolete("Object.FindObjectsOfType has been deprecated. Use Object.FindObjectsByType instead which lets you decide whether you need the results sorted or not.  FindObjectsOfType sorts the results by InstanceID but if you do not need this using FindObjectSortMode.None is considerably faster.", false)]
		public static T[] FindObjectsOfType<T>() where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsOfType(typeof(T), false));
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00025758 File Offset: 0x00023958
		public static T[] FindObjectsByType<T>(FindObjectsSortMode sortMode) where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsByType(typeof(T), FindObjectsInactive.Exclude, sortMode));
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00025780 File Offset: 0x00023980
		[Obsolete("Object.FindObjectsOfType has been deprecated. Use Object.FindObjectsByType instead which lets you decide whether you need the results sorted or not.  FindObjectsOfType sorts the results by InstanceID but if you do not need this using FindObjectSortMode.None is considerably faster.", false)]
		public static T[] FindObjectsOfType<T>(bool includeInactive) where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsOfType(typeof(T), includeInactive));
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000257A8 File Offset: 0x000239A8
		public static T[] FindObjectsByType<T>(FindObjectsInactive findObjectsInactive, FindObjectsSortMode sortMode) where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsByType(typeof(T), findObjectsInactive, sortMode));
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000257D0 File Offset: 0x000239D0
		[Obsolete("Object.FindObjectOfType has been deprecated. Use Object.FindFirstObjectByType instead or if finding any instance is acceptable the faster Object.FindAnyObjectByType", false)]
		public static T FindObjectOfType<T>() where T : Object
		{
			return (T)((object)Object.FindObjectOfType(typeof(T), false));
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x000257F8 File Offset: 0x000239F8
		[Obsolete("Object.FindObjectOfType has been deprecated. Use Object.FindFirstObjectByType instead or if finding any instance is acceptable the faster Object.FindAnyObjectByType", false)]
		public static T FindObjectOfType<T>(bool includeInactive) where T : Object
		{
			return (T)((object)Object.FindObjectOfType(typeof(T), includeInactive));
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00025820 File Offset: 0x00023A20
		public static T FindFirstObjectByType<T>() where T : Object
		{
			return (T)((object)Object.FindFirstObjectByType(typeof(T), FindObjectsInactive.Exclude));
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00025848 File Offset: 0x00023A48
		public static T FindAnyObjectByType<T>() where T : Object
		{
			return (T)((object)Object.FindAnyObjectByType(typeof(T), FindObjectsInactive.Exclude));
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00025870 File Offset: 0x00023A70
		public static T FindFirstObjectByType<T>(FindObjectsInactive findObjectsInactive) where T : Object
		{
			return (T)((object)Object.FindFirstObjectByType(typeof(T), findObjectsInactive));
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00025898 File Offset: 0x00023A98
		public static T FindAnyObjectByType<T>(FindObjectsInactive findObjectsInactive) where T : Object
		{
			return (T)((object)Object.FindAnyObjectByType(typeof(T), findObjectsInactive));
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x000258C0 File Offset: 0x00023AC0
		[Obsolete("Please use Resources.FindObjectsOfTypeAll instead")]
		public static Object[] FindObjectsOfTypeAll(Type type)
		{
			return Resources.FindObjectsOfTypeAll(type);
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x000258D8 File Offset: 0x00023AD8
		private static void CheckNullArgument(object arg, string message)
		{
			bool flag = arg == null;
			if (flag)
			{
				throw new ArgumentException(message);
			}
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000258F8 File Offset: 0x00023AF8
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[Obsolete("Object.FindObjectOfType has been deprecated. Use Object.FindFirstObjectByType instead or if finding any instance is acceptable the faster Object.FindAnyObjectByType", false)]
		public static Object FindObjectOfType(Type type)
		{
			Object[] objects = Object.FindObjectsOfType(type, false);
			bool flag = objects.Length != 0;
			Object @object;
			if (flag)
			{
				@object = objects[0];
			}
			else
			{
				@object = null;
			}
			return @object;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00025924 File Offset: 0x00023B24
		public static Object FindFirstObjectByType(Type type)
		{
			Object[] objects = Object.FindObjectsByType(type, FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
			return (objects.Length != 0) ? objects[0] : null;
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0002594C File Offset: 0x00023B4C
		public static Object FindAnyObjectByType(Type type)
		{
			Object[] objects = Object.FindObjectsByType(type, FindObjectsInactive.Exclude, FindObjectsSortMode.None);
			return (objects.Length != 0) ? objects[0] : null;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00025974 File Offset: 0x00023B74
		[Obsolete("Object.FindObjectOfType has been deprecated. Use Object.FindFirstObjectByType instead or if finding any instance is acceptable the faster Object.FindAnyObjectByType", false)]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public static Object FindObjectOfType(Type type, bool includeInactive)
		{
			Object[] objects = Object.FindObjectsOfType(type, includeInactive);
			bool flag = objects.Length != 0;
			Object @object;
			if (flag)
			{
				@object = objects[0];
			}
			else
			{
				@object = null;
			}
			return @object;
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x000259A0 File Offset: 0x00023BA0
		public static Object FindFirstObjectByType(Type type, FindObjectsInactive findObjectsInactive)
		{
			Object[] objects = Object.FindObjectsByType(type, findObjectsInactive, FindObjectsSortMode.InstanceID);
			return (objects.Length != 0) ? objects[0] : null;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000259C8 File Offset: 0x00023BC8
		public static Object FindAnyObjectByType(Type type, FindObjectsInactive findObjectsInactive)
		{
			Object[] objects = Object.FindObjectsByType(type, findObjectsInactive, FindObjectsSortMode.None);
			return (objects.Length != 0) ? objects[0] : null;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x000259F0 File Offset: 0x00023BF0
		public override string ToString()
		{
			return Object.ToString(this);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00025A08 File Offset: 0x00023C08
		public static bool operator ==(Object x, Object y)
		{
			return Object.CompareBaseObjects(x, y);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00025A24 File Offset: 0x00023C24
		public static bool operator !=(Object x, Object y)
		{
			return !Object.CompareBaseObjects(x, y);
		}

		// Token: 0x06001187 RID: 4487
		[NativeMethod(Name = "Object::GetOffsetOfInstanceIdMember", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetOffsetOfInstanceIDInCPlusPlusObject();

		// Token: 0x06001188 RID: 4488
		[NativeMethod(Name = "CurrentThreadIsMainThread", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CurrentThreadIsMainThread();

		// Token: 0x06001189 RID: 4489 RVA: 0x00025A40 File Offset: 0x00023C40
		[NativeMethod(Name = "CloneObject", IsFreeFunction = true, ThrowsException = true)]
		private static Object Internal_CloneSingle([NotNull] Object data)
		{
			if (data == null)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(data);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			return Unmarshal.UnmarshalUnityObject<Object>(Object.Internal_CloneSingle_Injected(intPtr));
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00025A7C File Offset: 0x00023C7C
		[FreeFunction("CloneObjectToScene")]
		private static Object Internal_CloneSingleWithScene([NotNull] Object data, Scene scene)
		{
			if (data == null)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(data);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			return Unmarshal.UnmarshalUnityObject<Object>(Object.Internal_CloneSingleWithScene_Injected(intPtr, ref scene));
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00025ABC File Offset: 0x00023CBC
		[FreeFunction("CloneObject")]
		private static Object Internal_CloneSingleWithParent([NotNull] Object data, [NotNull] Transform parent, bool worldPositionStays)
		{
			if (data == null)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			if (parent == null)
			{
				ThrowHelper.ThrowArgumentNullException(parent, "parent");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(data);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Transform>(parent);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(parent, "parent");
			}
			return Unmarshal.UnmarshalUnityObject<Object>(Object.Internal_CloneSingleWithParent_Injected(intPtr, intPtr2, worldPositionStays));
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00025B1C File Offset: 0x00023D1C
		[FreeFunction("InstantiateAsyncObjects")]
		private static IntPtr Internal_InstantiateAsyncWithParent([NotNull] Object original, int count, Transform parent, IntPtr positions, int positionsCount, IntPtr rotations, int rotationsCount, bool hasManagedCancellationToken)
		{
			if (original == null)
			{
				ThrowHelper.ThrowArgumentNullException(original, "original");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(original);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(original, "original");
			}
			return Object.Internal_InstantiateAsyncWithParent_Injected(intPtr, count, Object.MarshalledUnityObject.Marshal<Transform>(parent), positions, positionsCount, rotations, rotationsCount, hasManagedCancellationToken);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00025B64 File Offset: 0x00023D64
		[FreeFunction("InstantiateObject")]
		private static Object Internal_InstantiateSingle([NotNull] Object data, Vector3 pos, Quaternion rot)
		{
			if (data == null)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(data);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			return Unmarshal.UnmarshalUnityObject<Object>(Object.Internal_InstantiateSingle_Injected(intPtr, ref pos, ref rot));
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00025BA4 File Offset: 0x00023DA4
		[FreeFunction("InstantiateObject")]
		private static Object Internal_InstantiateSingleWithParent([NotNull] Object data, [NotNull] Transform parent, Vector3 pos, Quaternion rot)
		{
			if (data == null)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			if (parent == null)
			{
				ThrowHelper.ThrowArgumentNullException(parent, "parent");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(data);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Transform>(parent);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(parent, "parent");
			}
			return Unmarshal.UnmarshalUnityObject<Object>(Object.Internal_InstantiateSingleWithParent_Injected(intPtr, intPtr2, ref pos, ref rot));
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00025C08 File Offset: 0x00023E08
		[FreeFunction("UnityEngineObjectBindings::ToString")]
		private static string ToString(Object obj)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				Object.ToString_Injected(Object.MarshalledUnityObject.Marshal<Object>(obj), out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00025C40 File Offset: 0x00023E40
		[FreeFunction("UnityEngineObjectBindings::GetName", HasExplicitThis = true)]
		private string GetName()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				Object.GetName_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00025C80 File Offset: 0x00023E80
		[FreeFunction("UnityEngineObjectBindings::IsPersistent")]
		internal static bool IsPersistent([NotNull] Object obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(obj);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			return Object.IsPersistent_Injected(intPtr);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00025CB8 File Offset: 0x00023EB8
		[FreeFunction("UnityEngineObjectBindings::SetName", HasExplicitThis = true)]
		private unsafe void SetName(string name)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Object.SetName_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001193 RID: 4499
		[NativeMethod(Name = "UnityEngineObjectBindings::DoesObjectWithInstanceIDExist", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool DoesObjectWithInstanceIDExist(int instanceID);

		// Token: 0x06001194 RID: 4500 RVA: 0x00025D1C File Offset: 0x00023F1C
		[FreeFunction("UnityEngineObjectBindings::FindObjectFromInstanceID")]
		[VisibleToOtherModules]
		internal static Object FindObjectFromInstanceID(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<Object>(Object.FindObjectFromInstanceID_Injected(instanceID));
		}

		// Token: 0x06001195 RID: 4501
		[FreeFunction("UnityEngineObjectBindings::GetPtrFromInstanceID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetPtrFromInstanceID(int instanceID, Type objectType, out bool isMonoBehaviour);

		// Token: 0x06001196 RID: 4502 RVA: 0x00025D34 File Offset: 0x00023F34
		[VisibleToOtherModules]
		[FreeFunction("UnityEngineObjectBindings::ForceLoadFromInstanceID")]
		internal static Object ForceLoadFromInstanceID(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<Object>(Object.ForceLoadFromInstanceID_Injected(instanceID));
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00025D4C File Offset: 0x00023F4C
		[FreeFunction("UnityEngineObjectBindings::MarkObjectDirty", HasExplicitThis = true)]
		internal void MarkDirty()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Object.MarkDirty_Injected(intPtr);
		}

		// Token: 0x0600119A RID: 4506
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy_Injected(IntPtr obj, [DefaultValue("0.0F")] float t);

		// Token: 0x0600119B RID: 4507
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyImmediate_Injected(IntPtr obj, [DefaultValue("false")] bool allowDestroyingAssets);

		// Token: 0x0600119C RID: 4508
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DontDestroyOnLoad_Injected(IntPtr target);

		// Token: 0x0600119D RID: 4509
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern HideFlags get_hideFlags_Injected(IntPtr _unity_self);

		// Token: 0x0600119E RID: 4510
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_hideFlags_Injected(IntPtr _unity_self, HideFlags value);

		// Token: 0x0600119F RID: 4511
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_CloneSingle_Injected(IntPtr data);

		// Token: 0x060011A0 RID: 4512
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_CloneSingleWithScene_Injected(IntPtr data, [In] ref Scene scene);

		// Token: 0x060011A1 RID: 4513
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_CloneSingleWithParent_Injected(IntPtr data, IntPtr parent, bool worldPositionStays);

		// Token: 0x060011A2 RID: 4514
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_InstantiateAsyncWithParent_Injected(IntPtr original, int count, IntPtr parent, IntPtr positions, int positionsCount, IntPtr rotations, int rotationsCount, bool hasManagedCancellationToken);

		// Token: 0x060011A3 RID: 4515
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_InstantiateSingle_Injected(IntPtr data, [In] ref Vector3 pos, [In] ref Quaternion rot);

		// Token: 0x060011A4 RID: 4516
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_InstantiateSingleWithParent_Injected(IntPtr data, IntPtr parent, [In] ref Vector3 pos, [In] ref Quaternion rot);

		// Token: 0x060011A5 RID: 4517
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ToString_Injected(IntPtr obj, out ManagedSpanWrapper ret);

		// Token: 0x060011A6 RID: 4518
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x060011A7 RID: 4519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsPersistent_Injected(IntPtr obj);

		// Token: 0x060011A8 RID: 4520
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x060011A9 RID: 4521
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FindObjectFromInstanceID_Injected(int instanceID);

		// Token: 0x060011AA RID: 4522
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr ForceLoadFromInstanceID_Injected(int instanceID);

		// Token: 0x060011AB RID: 4523
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MarkDirty_Injected(IntPtr _unity_self);

		// Token: 0x0400069D RID: 1693
		private const int kInstanceID_None = 0;

		// Token: 0x0400069E RID: 1694
		private IntPtr m_CachedPtr;

		// Token: 0x0400069F RID: 1695
		internal static readonly int OffsetOfInstanceIDInCPlusPlusObject = Object.GetOffsetOfInstanceIDInCPlusPlusObject();

		// Token: 0x040006A0 RID: 1696
		private const string objectIsNullMessage = "The Object you want to instantiate is null.";

		// Token: 0x040006A1 RID: 1697
		private const string cloneDestroyedMessage = "Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.";

		// Token: 0x020001C4 RID: 452
		[VisibleToOtherModules]
		internal static class MarshalledUnityObject
		{
			// Token: 0x060011AC RID: 4524 RVA: 0x00025D7C File Offset: 0x00023F7C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static IntPtr Marshal<T>(T obj) where T : Object
			{
				bool flag = obj == null;
				IntPtr intPtr;
				if (flag)
				{
					intPtr = IntPtr.Zero;
				}
				else
				{
					intPtr = Object.MarshalledUnityObject.MarshalNotNull<T>(obj);
				}
				return intPtr;
			}

			// Token: 0x060011AD RID: 4525 RVA: 0x00025DAC File Offset: 0x00023FAC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static IntPtr MarshalNotNull<T>(T obj) where T : Object
			{
				return obj.m_CachedPtr;
			}

			// Token: 0x060011AE RID: 4526 RVA: 0x00003D56 File Offset: 0x00001F56
			public static void TryThrowEditorNullExceptionObject(Object unityObj, string paramterName)
			{
			}
		}
	}
}
