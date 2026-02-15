using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020001A4 RID: 420
	[NativeHeader("Runtime/Export/Scripting/GameObject.bindings.h")]
	[UsedByNativeCode]
	[ExcludeFromPreset]
	public sealed class GameObject : Object
	{
		// Token: 0x06001034 RID: 4148 RVA: 0x00022880 File Offset: 0x00020A80
		[FreeFunction("GameObjectBindings::CreatePrimitive")]
		public static GameObject CreatePrimitive(PrimitiveType type)
		{
			return Unmarshal.UnmarshalUnityObject<GameObject>(GameObject.CreatePrimitive_Injected(type));
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00022898 File Offset: 0x00020A98
		public unsafe T GetComponent<T>()
		{
			CastHelper<T> h = default(CastHelper<T>);
			this.GetComponentFastPath(typeof(T), new IntPtr((void*)(&h.onePointerFurtherThanT)));
			return h.t;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x000228D8 File Offset: 0x00020AD8
		[FreeFunction(Name = "GameObjectBindings::GetComponentFromType", HasExplicitThis = true, ThrowsException = true)]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponent(Type type)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Component>(GameObject.GetComponent_Injected(intPtr, type));
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00022900 File Offset: 0x00020B00
		[FreeFunction(Name = "GameObjectBindings::GetComponentFastPath", HasExplicitThis = true, ThrowsException = true)]
		internal void GetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GameObject.GetComponentFastPath_Injected(intPtr, type, oneFurtherThanResultValue);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00022924 File Offset: 0x00020B24
		[FreeFunction(Name = "Scripting::GetScriptingWrapperOfComponentOfGameObject", HasExplicitThis = true)]
		internal unsafe Component GetComponentByName(string type)
		{
			Component component;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(type, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = type.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr componentByName_Injected = GameObject.GetComponentByName_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				IntPtr componentByName_Injected;
				component = Unmarshal.UnmarshalUnityObject<Component>(componentByName_Injected);
				char* ptr = null;
			}
			return component;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00022994 File Offset: 0x00020B94
		[FreeFunction(Name = "Scripting::GetScriptingWrapperOfComponentOfGameObjectWithCase", HasExplicitThis = true)]
		internal unsafe Component GetComponentByNameWithCase(string type, bool caseSensitive)
		{
			Component component;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(type, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = type.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr componentByNameWithCase_Injected = GameObject.GetComponentByNameWithCase_Injected(intPtr, ref managedSpanWrapper, caseSensitive);
			}
			finally
			{
				IntPtr componentByNameWithCase_Injected;
				component = Unmarshal.UnmarshalUnityObject<Component>(componentByNameWithCase_Injected);
				char* ptr = null;
			}
			return component;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00022A04 File Offset: 0x00020C04
		public Component GetComponent(string type)
		{
			return this.GetComponentByName(type);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00022A20 File Offset: 0x00020C20
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[FreeFunction(Name = "GameObjectBindings::GetComponentInChildren", HasExplicitThis = true, ThrowsException = true)]
		public Component GetComponentInChildren(Type type, bool includeInactive)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Component>(GameObject.GetComponentInChildren_Injected(intPtr, type, includeInactive));
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00022A4C File Offset: 0x00020C4C
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type type)
		{
			return this.GetComponentInChildren(type, false);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00022A68 File Offset: 0x00020C68
		[ExcludeFromDocs]
		public T GetComponentInChildren<T>()
		{
			bool includeInactive = false;
			return this.GetComponentInChildren<T>(includeInactive);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00022A84 File Offset: 0x00020C84
		public T GetComponentInChildren<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), includeInactive));
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00022AAC File Offset: 0x00020CAC
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[FreeFunction(Name = "GameObjectBindings::GetComponentInParent", HasExplicitThis = true, ThrowsException = true)]
		public Component GetComponentInParent(Type type, bool includeInactive)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Component>(GameObject.GetComponentInParent_Injected(intPtr, type, includeInactive));
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00022AD8 File Offset: 0x00020CD8
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type type)
		{
			return this.GetComponentInParent(type, false);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00022AF4 File Offset: 0x00020CF4
		[ExcludeFromDocs]
		public T GetComponentInParent<T>()
		{
			bool includeInactive = false;
			return this.GetComponentInParent<T>(includeInactive);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00022B10 File Offset: 0x00020D10
		public T GetComponentInParent<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInParent(typeof(T), includeInactive));
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00022B38 File Offset: 0x00020D38
		[FreeFunction(Name = "GameObjectBindings::GetComponentsInternal", HasExplicitThis = true, ThrowsException = true)]
		private Array GetComponentsInternal(Type type, bool useSearchTypeAsArrayReturnType, bool recursive, bool includeInactive, bool reverse, object resultList)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GameObject.GetComponentsInternal_Injected(intPtr, type, useSearchTypeAsArrayReturnType, recursive, includeInactive, reverse, resultList);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00022B64 File Offset: 0x00020D64
		public Component[] GetComponents(Type type)
		{
			return (Component[])this.GetComponentsInternal(type, false, false, true, false, null);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00022B88 File Offset: 0x00020D88
		public T[] GetComponents<T>()
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, false, true, false, null);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00022BB4 File Offset: 0x00020DB4
		public void GetComponents(Type type, List<Component> results)
		{
			this.GetComponentsInternal(type, false, false, true, false, results);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00022BC4 File Offset: 0x00020DC4
		public void GetComponents<T>(List<T> results)
		{
			this.GetComponentsInternal(typeof(T), true, false, true, false, results);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00022BE0 File Offset: 0x00020DE0
		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type type)
		{
			bool includeInactive = false;
			return this.GetComponentsInChildren(type, includeInactive);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00022BFC File Offset: 0x00020DFC
		public Component[] GetComponentsInChildren(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return (Component[])this.GetComponentsInternal(type, false, true, includeInactive, false, null);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00022C20 File Offset: 0x00020E20
		public T[] GetComponentsInChildren<T>(bool includeInactive)
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, false, null);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00022C4C File Offset: 0x00020E4C
		public void GetComponentsInChildren<T>(bool includeInactive, List<T> results)
		{
			this.GetComponentsInternal(typeof(T), true, true, includeInactive, false, results);
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00022C68 File Offset: 0x00020E68
		public T[] GetComponentsInChildren<T>()
		{
			return this.GetComponentsInChildren<T>(false);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00022C81 File Offset: 0x00020E81
		public void GetComponentsInChildren<T>(List<T> results)
		{
			this.GetComponentsInChildren<T>(false, results);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00022C90 File Offset: 0x00020E90
		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type type)
		{
			bool includeInactive = false;
			return this.GetComponentsInParent(type, includeInactive);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00022CAC File Offset: 0x00020EAC
		public Component[] GetComponentsInParent(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return (Component[])this.GetComponentsInternal(type, false, true, includeInactive, true, null);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00022CCF File Offset: 0x00020ECF
		public void GetComponentsInParent<T>(bool includeInactive, List<T> results)
		{
			this.GetComponentsInternal(typeof(T), true, true, includeInactive, true, results);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00022CE8 File Offset: 0x00020EE8
		public T[] GetComponentsInParent<T>(bool includeInactive)
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, true, null);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00022D14 File Offset: 0x00020F14
		public T[] GetComponentsInParent<T>()
		{
			return this.GetComponentsInParent<T>(false);
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00022D30 File Offset: 0x00020F30
		public unsafe bool TryGetComponent<T>(out T component)
		{
			CastHelper<T> h = default(CastHelper<T>);
			this.TryGetComponentFastPath(typeof(T), new IntPtr((void*)(&h.onePointerFurtherThanT)));
			component = h.t;
			return h.t != null;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00022D84 File Offset: 0x00020F84
		public bool TryGetComponent(Type type, out Component component)
		{
			component = this.TryGetComponentInternal(type);
			return component != null;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00022DA8 File Offset: 0x00020FA8
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[FreeFunction(Name = "GameObjectBindings::TryGetComponentFromType", HasExplicitThis = true, ThrowsException = true)]
		internal Component TryGetComponentInternal(Type type)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Component>(GameObject.TryGetComponentInternal_Injected(intPtr, type));
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00022DD0 File Offset: 0x00020FD0
		[FreeFunction(Name = "GameObjectBindings::TryGetComponentFastPath", HasExplicitThis = true, ThrowsException = true)]
		internal void TryGetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GameObject.TryGetComponentFastPath_Injected(intPtr, type, oneFurtherThanResultValue);
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00022DF4 File Offset: 0x00020FF4
		public static GameObject FindWithTag(string tag)
		{
			return GameObject.FindGameObjectWithTag(tag);
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00022E0C File Offset: 0x0002100C
		[FreeFunction(Name = "GameObjectBindings::FindGameObjectsWithTagForListInternal", ThrowsException = true)]
		private unsafe static void FindGameObjectsWithTagForListInternal(string tag, object results)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GameObject.FindGameObjectsWithTagForListInternal_Injected(ref managedSpanWrapper, results);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00022E64 File Offset: 0x00021064
		public static void FindGameObjectsWithTag(string tag, List<GameObject> results)
		{
			GameObject.FindGameObjectsWithTagForListInternal(tag, results);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00022E6F File Offset: 0x0002106F
		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			this.SendMessageUpwards(methodName, null, options);
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00022E7C File Offset: 0x0002107C
		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00022E89 File Offset: 0x00021089
		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			this.BroadcastMessage(methodName, null, options);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00022E98 File Offset: 0x00021098
		[FreeFunction(Name = "MonoAddComponent", HasExplicitThis = true)]
		internal unsafe Component AddComponentInternal(string className)
		{
			Component component;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(className, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = className.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr2 = GameObject.AddComponentInternal_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				IntPtr intPtr2;
				component = Unmarshal.UnmarshalUnityObject<Component>(intPtr2);
				char* ptr = null;
			}
			return component;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00022F08 File Offset: 0x00021108
		[FreeFunction(Name = "MonoAddComponentWithType", HasExplicitThis = true)]
		private Component Internal_AddComponentWithType(Type componentType)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Component>(GameObject.Internal_AddComponentWithType_Injected(intPtr, componentType));
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00022F30 File Offset: 0x00021130
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component AddComponent(Type componentType)
		{
			return this.Internal_AddComponentWithType(componentType);
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00022F4C File Offset: 0x0002114C
		public T AddComponent<T>() where T : Component
		{
			return this.AddComponent(typeof(T)) as T;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00022F78 File Offset: 0x00021178
		public int GetComponentCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GameObject.GetComponentCount_Injected(intPtr);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00022F9C File Offset: 0x0002119C
		[NativeName("QueryComponentAtIndex<Unity::Component>")]
		internal Component QueryComponentAtIndex(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Component>(GameObject.QueryComponentAtIndex_Injected(intPtr, index));
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00022FC4 File Offset: 0x000211C4
		public Component GetComponentAtIndex(int index)
		{
			bool flag = index < 0 || index >= this.GetComponentCount();
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", "Valid range is 0 to GetComponentCount() - 1.");
			}
			return this.QueryComponentAtIndex(index);
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00023004 File Offset: 0x00021204
		public T GetComponentAtIndex<T>(int index) where T : Component
		{
			T component = (T)((object)this.GetComponentAtIndex(index));
			bool flag = component == null;
			if (flag)
			{
				throw new InvalidCastException();
			}
			return component;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0002303C File Offset: 0x0002123C
		public int GetComponentIndex(Component component)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GameObject.GetComponentIndex_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Component>(component));
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x00023064 File Offset: 0x00021264
		public Transform transform
		{
			[FreeFunction("GameObjectBindings::GetTransform", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Transform>(GameObject.get_transform_Injected(intPtr));
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x0002308C File Offset: 0x0002128C
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x000230B0 File Offset: 0x000212B0
		public int layer
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GameObject.get_layer_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GameObject.set_layer_Injected(intPtr, value);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x000230D4 File Offset: 0x000212D4
		// (set) Token: 0x0600106A RID: 4202 RVA: 0x000230F8 File Offset: 0x000212F8
		[Obsolete("GameObject.active is obsolete. Use GameObject.SetActive(), GameObject.activeSelf or GameObject.activeInHierarchy.")]
		public bool active
		{
			[NativeMethod(Name = "IsActive")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GameObject.get_active_Injected(intPtr);
			}
			[NativeMethod(Name = "SetSelfActive")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GameObject.set_active_Injected(intPtr, value);
			}
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0002311C File Offset: 0x0002131C
		[NativeMethod(Name = "SetSelfActive")]
		public void SetActive(bool value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GameObject.SetActive_Injected(intPtr, value);
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x00023140 File Offset: 0x00021340
		public bool activeSelf
		{
			[NativeMethod(Name = "IsSelfActive")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GameObject.get_activeSelf_Injected(intPtr);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x00023164 File Offset: 0x00021364
		public bool activeInHierarchy
		{
			[NativeMethod(Name = "IsActive")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GameObject.get_activeInHierarchy_Injected(intPtr);
			}
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00023188 File Offset: 0x00021388
		[Obsolete("gameObject.SetActiveRecursively() is obsolete. Use GameObject.SetActive(), which is now inherited by children.")]
		[NativeMethod(Name = "SetActiveRecursivelyDeprecated")]
		public void SetActiveRecursively(bool state)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GameObject.SetActiveRecursively_Injected(intPtr, state);
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x000231AC File Offset: 0x000213AC
		// (set) Token: 0x06001070 RID: 4208 RVA: 0x000231D0 File Offset: 0x000213D0
		public bool isStatic
		{
			[NativeMethod(Name = "GetIsStaticDeprecated")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GameObject.get_isStatic_Injected(intPtr);
			}
			[NativeMethod(Name = "SetIsStaticDeprecated")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GameObject.set_isStatic_Injected(intPtr, value);
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x000231F4 File Offset: 0x000213F4
		internal bool isStaticBatchable
		{
			[NativeMethod(Name = "IsStaticBatchable")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GameObject.get_isStaticBatchable_Injected(intPtr);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x00023218 File Offset: 0x00021418
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x00023258 File Offset: 0x00021458
		public unsafe string tag
		{
			[FreeFunction("GameObjectBindings::GetTag", HasExplicitThis = true)]
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					GameObject.get_tag_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			[FreeFunction("GameObjectBindings::SetTag", HasExplicitThis = true)]
			set
			{
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					GameObject.set_tag_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x000232BC File Offset: 0x000214BC
		public bool CompareTag(string tag)
		{
			return this.CompareTag_Internal(tag);
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x000232C5 File Offset: 0x000214C5
		public bool CompareTag(TagHandle tag)
		{
			return this.CompareTagHandle_Internal(tag);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x000232D0 File Offset: 0x000214D0
		[FreeFunction(Name = "GameObjectBindings::CompareTag", HasExplicitThis = true)]
		private unsafe bool CompareTag_Internal(string tag)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = GameObject.CompareTag_Internal_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00023338 File Offset: 0x00021538
		[FreeFunction(Name = "GameObjectBindings::CompareTagHandle", HasExplicitThis = true)]
		private bool CompareTagHandle_Internal(TagHandle tag)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GameObject.CompareTagHandle_Internal_Injected(intPtr, ref tag);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0002335C File Offset: 0x0002155C
		[FreeFunction(Name = "GameObjectBindings::FindGameObjectWithTag", ThrowsException = true)]
		public unsafe static GameObject FindGameObjectWithTag(string tag)
		{
			GameObject gameObject;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr = GameObject.FindGameObjectWithTag_Injected(ref managedSpanWrapper);
			}
			finally
			{
				IntPtr intPtr;
				gameObject = Unmarshal.UnmarshalUnityObject<GameObject>(intPtr);
				char* ptr = null;
			}
			return gameObject;
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x000233BC File Offset: 0x000215BC
		[FreeFunction(Name = "GameObjectBindings::FindGameObjectsWithTag", ThrowsException = true)]
		public unsafe static GameObject[] FindGameObjectsWithTag(string tag)
		{
			GameObject[] array;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				array = GameObject.FindGameObjectsWithTag_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return array;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00023414 File Offset: 0x00021614
		[FreeFunction(Name = "Scripting::SendScriptingMessageUpwards", HasExplicitThis = true)]
		public unsafe void SendMessageUpwards(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(methodName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = methodName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GameObject.SendMessageUpwards_Injected(intPtr, ref managedSpanWrapper, value, options);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0002347C File Offset: 0x0002167C
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			this.SendMessageUpwards(methodName, value, options);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00023498 File Offset: 0x00021698
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object value = null;
			this.SendMessageUpwards(methodName, value, options);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x000234B4 File Offset: 0x000216B4
		[FreeFunction(Name = "Scripting::SendScriptingMessage", HasExplicitThis = true)]
		public unsafe void SendMessage(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(methodName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = methodName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GameObject.SendMessage_Injected(intPtr, ref managedSpanWrapper, value, options);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0002351C File Offset: 0x0002171C
		[ExcludeFromDocs]
		public void SendMessage(string methodName, object value)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			this.SendMessage(methodName, value, options);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00023538 File Offset: 0x00021738
		[ExcludeFromDocs]
		public void SendMessage(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object value = null;
			this.SendMessage(methodName, value, options);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00023554 File Offset: 0x00021754
		[FreeFunction(Name = "Scripting::BroadcastScriptingMessage", HasExplicitThis = true)]
		public unsafe void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(methodName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = methodName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GameObject.BroadcastMessage_Injected(intPtr, ref managedSpanWrapper, parameter, options);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x000235BC File Offset: 0x000217BC
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			this.BroadcastMessage(methodName, parameter, options);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x000235D8 File Offset: 0x000217D8
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object parameter = null;
			this.BroadcastMessage(methodName, parameter, options);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x000235F4 File Offset: 0x000217F4
		public GameObject(string name)
		{
			GameObject.Internal_CreateGameObject(this, name);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00023606 File Offset: 0x00021806
		public GameObject()
		{
			GameObject.Internal_CreateGameObject(this, null);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00023618 File Offset: 0x00021818
		public GameObject(string name, params Type[] components)
		{
			GameObject.Internal_CreateGameObject(this, name);
			foreach (Type t in components)
			{
				this.AddComponent(t);
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00023654 File Offset: 0x00021854
		[FreeFunction(Name = "GameObjectBindings::Internal_CreateGameObject")]
		private unsafe static void Internal_CreateGameObject([Writable] GameObject self, string name)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GameObject.Internal_CreateGameObject_Injected(self, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000236AC File Offset: 0x000218AC
		[FreeFunction(Name = "GameObjectBindings::Find")]
		public unsafe static GameObject Find(string name)
		{
			GameObject gameObject;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr = GameObject.Find_Injected(ref managedSpanWrapper);
			}
			finally
			{
				IntPtr intPtr;
				gameObject = Unmarshal.UnmarshalUnityObject<GameObject>(intPtr);
				char* ptr = null;
			}
			return gameObject;
		}

		// Token: 0x06001088 RID: 4232
		[FreeFunction(Name = "GameObjectBindings::SetGameObjectsActiveByInstanceID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGameObjectsActive(IntPtr instanceIds, int instanceCount, bool active);

		// Token: 0x06001089 RID: 4233 RVA: 0x0002370C File Offset: 0x0002190C
		public static void SetGameObjectsActive(NativeArray<int> instanceIDs, bool active)
		{
			bool flag = !instanceIDs.IsCreated;
			if (flag)
			{
				throw new ArgumentException("NativeArray is uninitialized", "instanceIDs");
			}
			bool flag2 = instanceIDs.Length == 0;
			if (!flag2)
			{
				GameObject.SetGameObjectsActive((IntPtr)instanceIDs.GetUnsafeReadOnlyPtr<int>(), instanceIDs.Length, active);
			}
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00023764 File Offset: 0x00021964
		public unsafe static void SetGameObjectsActive(ReadOnlySpan<int> instanceIDs, bool active)
		{
			bool flag = instanceIDs.Length == 0;
			if (!flag)
			{
				fixed (int* pinnableReference = instanceIDs.GetPinnableReference())
				{
					int* instanceIDsPtr = pinnableReference;
					GameObject.SetGameObjectsActive((IntPtr)((void*)instanceIDsPtr), instanceIDs.Length, active);
				}
			}
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x000237A8 File Offset: 0x000219A8
		[FreeFunction("GameObjectBindings::InstantiateGameObjectsByInstanceID")]
		private static void InstantiateGameObjects(int sourceInstanceID, IntPtr newInstanceIDs, IntPtr newTransformInstanceIDs, int count, Scene destinationScene)
		{
			GameObject.InstantiateGameObjects_Injected(sourceInstanceID, newInstanceIDs, newTransformInstanceIDs, count, ref destinationScene);
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x000237C0 File Offset: 0x000219C0
		public static void InstantiateGameObjects(int sourceInstanceID, int count, NativeArray<int> newInstanceIDs, NativeArray<int> newTransformInstanceIDs, Scene destinationScene = default(Scene))
		{
			bool flag = !newInstanceIDs.IsCreated;
			if (flag)
			{
				throw new ArgumentException("NativeArray is uninitialized", "newInstanceIDs");
			}
			bool flag2 = !newTransformInstanceIDs.IsCreated;
			if (flag2)
			{
				throw new ArgumentException("NativeArray is uninitialized", "newTransformInstanceIDs");
			}
			bool flag3 = count == 0;
			if (!flag3)
			{
				bool flag4 = count != newInstanceIDs.Length || count != newTransformInstanceIDs.Length;
				if (flag4)
				{
					throw new ArgumentException("Size mismatch! Both arrays must already be the size of count.");
				}
				GameObject.InstantiateGameObjects(sourceInstanceID, (IntPtr)newInstanceIDs.GetUnsafeReadOnlyPtr<int>(), (IntPtr)newTransformInstanceIDs.GetUnsafeReadOnlyPtr<int>(), newInstanceIDs.Length, destinationScene);
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00023864 File Offset: 0x00021A64
		[FreeFunction(Name = "GameObjectBindings::GetSceneByInstanceID")]
		public static Scene GetScene(int instanceID)
		{
			Scene scene;
			GameObject.GetScene_Injected(instanceID, out scene);
			return scene;
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x0002387C File Offset: 0x00021A7C
		public Scene scene
		{
			[FreeFunction("GameObjectBindings::GetScene", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Scene scene;
				GameObject.get_scene_Injected(intPtr, out scene);
				return scene;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x000238A4 File Offset: 0x00021AA4
		public ulong sceneCullingMask
		{
			[FreeFunction(Name = "GameObjectBindings::GetSceneCullingMask", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GameObject.get_sceneCullingMask_Injected(intPtr);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x000238C8 File Offset: 0x00021AC8
		public GameObject gameObject
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06001091 RID: 4241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreatePrimitive_Injected(PrimitiveType type);

		// Token: 0x06001092 RID: 4242
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetComponent_Injected(IntPtr _unity_self, Type type);

		// Token: 0x06001093 RID: 4243
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetComponentFastPath_Injected(IntPtr _unity_self, Type type, IntPtr oneFurtherThanResultValue);

		// Token: 0x06001094 RID: 4244
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetComponentByName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper type);

		// Token: 0x06001095 RID: 4245
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetComponentByNameWithCase_Injected(IntPtr _unity_self, ref ManagedSpanWrapper type, bool caseSensitive);

		// Token: 0x06001096 RID: 4246
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetComponentInChildren_Injected(IntPtr _unity_self, Type type, bool includeInactive);

		// Token: 0x06001097 RID: 4247
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetComponentInParent_Injected(IntPtr _unity_self, Type type, bool includeInactive);

		// Token: 0x06001098 RID: 4248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Array GetComponentsInternal_Injected(IntPtr _unity_self, Type type, bool useSearchTypeAsArrayReturnType, bool recursive, bool includeInactive, bool reverse, object resultList);

		// Token: 0x06001099 RID: 4249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr TryGetComponentInternal_Injected(IntPtr _unity_self, Type type);

		// Token: 0x0600109A RID: 4250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TryGetComponentFastPath_Injected(IntPtr _unity_self, Type type, IntPtr oneFurtherThanResultValue);

		// Token: 0x0600109B RID: 4251
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FindGameObjectsWithTagForListInternal_Injected(ref ManagedSpanWrapper tag, object results);

		// Token: 0x0600109C RID: 4252
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr AddComponentInternal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper className);

		// Token: 0x0600109D RID: 4253
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_AddComponentWithType_Injected(IntPtr _unity_self, Type componentType);

		// Token: 0x0600109E RID: 4254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetComponentCount_Injected(IntPtr _unity_self);

		// Token: 0x0600109F RID: 4255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr QueryComponentAtIndex_Injected(IntPtr _unity_self, int index);

		// Token: 0x060010A0 RID: 4256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetComponentIndex_Injected(IntPtr _unity_self, IntPtr component);

		// Token: 0x060010A1 RID: 4257
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_transform_Injected(IntPtr _unity_self);

		// Token: 0x060010A2 RID: 4258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_layer_Injected(IntPtr _unity_self);

		// Token: 0x060010A3 RID: 4259
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_layer_Injected(IntPtr _unity_self, int value);

		// Token: 0x060010A4 RID: 4260
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_active_Injected(IntPtr _unity_self);

		// Token: 0x060010A5 RID: 4261
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_active_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060010A6 RID: 4262
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetActive_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060010A7 RID: 4263
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_activeSelf_Injected(IntPtr _unity_self);

		// Token: 0x060010A8 RID: 4264
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_activeInHierarchy_Injected(IntPtr _unity_self);

		// Token: 0x060010A9 RID: 4265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetActiveRecursively_Injected(IntPtr _unity_self, bool state);

		// Token: 0x060010AA RID: 4266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isStatic_Injected(IntPtr _unity_self);

		// Token: 0x060010AB RID: 4267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_isStatic_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060010AC RID: 4268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isStaticBatchable_Injected(IntPtr _unity_self);

		// Token: 0x060010AD RID: 4269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_tag_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x060010AE RID: 4270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_tag_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060010AF RID: 4271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CompareTag_Internal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper tag);

		// Token: 0x060010B0 RID: 4272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CompareTagHandle_Internal_Injected(IntPtr _unity_self, [In] ref TagHandle tag);

		// Token: 0x060010B1 RID: 4273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FindGameObjectWithTag_Injected(ref ManagedSpanWrapper tag);

		// Token: 0x060010B2 RID: 4274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GameObject[] FindGameObjectsWithTag_Injected(ref ManagedSpanWrapper tag);

		// Token: 0x060010B3 RID: 4275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendMessageUpwards_Injected(IntPtr _unity_self, ref ManagedSpanWrapper methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x060010B4 RID: 4276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendMessage_Injected(IntPtr _unity_self, ref ManagedSpanWrapper methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x060010B5 RID: 4277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BroadcastMessage_Injected(IntPtr _unity_self, ref ManagedSpanWrapper methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x060010B6 RID: 4278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateGameObject_Injected([Writable] GameObject self, ref ManagedSpanWrapper name);

		// Token: 0x060010B7 RID: 4279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Find_Injected(ref ManagedSpanWrapper name);

		// Token: 0x060010B8 RID: 4280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InstantiateGameObjects_Injected(int sourceInstanceID, IntPtr newInstanceIDs, IntPtr newTransformInstanceIDs, int count, [In] ref Scene destinationScene);

		// Token: 0x060010B9 RID: 4281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetScene_Injected(int instanceID, out Scene ret);

		// Token: 0x060010BA RID: 4282
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_scene_Injected(IntPtr _unity_self, out Scene ret);

		// Token: 0x060010BB RID: 4283
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong get_sceneCullingMask_Injected(IntPtr _unity_self);
	}
}
