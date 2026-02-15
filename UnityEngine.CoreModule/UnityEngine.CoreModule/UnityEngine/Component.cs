using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000196 RID: 406
	[NativeHeader("Runtime/Export/Scripting/Component.bindings.h")]
	[RequiredByNativeCode]
	[NativeClass("Unity::Component")]
	public class Component : Object
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00021BD8 File Offset: 0x0001FDD8
		public Transform transform
		{
			[FreeFunction("GetTransform", HasExplicitThis = true, ThrowsException = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Component>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Transform>(Component.get_transform_Injected(intPtr));
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x00021C00 File Offset: 0x0001FE00
		public GameObject gameObject
		{
			[FreeFunction("GetGameObject", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Component>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<GameObject>(Component.get_gameObject_Injected(intPtr));
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00021C28 File Offset: 0x0001FE28
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponent(Type type)
		{
			return this.gameObject.GetComponent(type);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00021C48 File Offset: 0x0001FE48
		[FreeFunction(HasExplicitThis = true, ThrowsException = true)]
		internal void GetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Component>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Component.GetComponentFastPath_Injected(intPtr, type, oneFurtherThanResultValue);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00021C6C File Offset: 0x0001FE6C
		public unsafe T GetComponent<T>()
		{
			CastHelper<T> h = default(CastHelper<T>);
			this.GetComponentFastPath(typeof(T), new IntPtr((void*)(&h.onePointerFurtherThanT)));
			return h.t;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00021CAC File Offset: 0x0001FEAC
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public bool TryGetComponent(Type type, out Component component)
		{
			return this.gameObject.TryGetComponent(type, out component);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00021CCC File Offset: 0x0001FECC
		public bool TryGetComponent<T>(out T component)
		{
			return this.gameObject.TryGetComponent<T>(out component);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00021CEC File Offset: 0x0001FEEC
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type t, bool includeInactive)
		{
			return this.gameObject.GetComponentInChildren(t, includeInactive);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00021D0C File Offset: 0x0001FF0C
		public T GetComponentInChildren<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), includeInactive));
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00021D34 File Offset: 0x0001FF34
		[ExcludeFromDocs]
		public T GetComponentInChildren<T>()
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), false));
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00021D5C File Offset: 0x0001FF5C
		public T[] GetComponentsInChildren<T>(bool includeInactive)
		{
			return this.gameObject.GetComponentsInChildren<T>(includeInactive);
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00021D7A File Offset: 0x0001FF7A
		public void GetComponentsInChildren<T>(bool includeInactive, List<T> result)
		{
			this.gameObject.GetComponentsInChildren<T>(includeInactive, result);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00021D8C File Offset: 0x0001FF8C
		public T[] GetComponentsInChildren<T>()
		{
			return this.GetComponentsInChildren<T>(false);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00021DA5 File Offset: 0x0001FFA5
		public void GetComponentsInChildren<T>(List<T> results)
		{
			this.GetComponentsInChildren<T>(false, results);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00021DB4 File Offset: 0x0001FFB4
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type t, bool includeInactive)
		{
			return this.gameObject.GetComponentInParent(t, includeInactive);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00021DD4 File Offset: 0x0001FFD4
		public T GetComponentInParent<T>()
		{
			return (T)((object)this.GetComponentInParent(typeof(T), false));
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00021DFC File Offset: 0x0001FFFC
		public T[] GetComponentsInParent<T>(bool includeInactive)
		{
			return this.gameObject.GetComponentsInParent<T>(includeInactive);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00021E1A File Offset: 0x0002001A
		public void GetComponentsInParent<T>(bool includeInactive, List<T> results)
		{
			this.gameObject.GetComponentsInParent<T>(includeInactive, results);
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00021E2C File Offset: 0x0002002C
		public T[] GetComponentsInParent<T>()
		{
			return this.GetComponentsInParent<T>(false);
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00021E48 File Offset: 0x00020048
		[FreeFunction(HasExplicitThis = true, ThrowsException = true)]
		private void GetComponentsForListInternal(Type searchType, object resultList)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Component>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Component.GetComponentsForListInternal_Injected(intPtr, searchType, resultList);
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00021E6C File Offset: 0x0002006C
		public void GetComponents(Type type, List<Component> results)
		{
			this.GetComponentsForListInternal(type, results);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00021E78 File Offset: 0x00020078
		public void GetComponents<T>(List<T> results)
		{
			this.GetComponentsForListInternal(typeof(T), results);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00021E90 File Offset: 0x00020090
		public T[] GetComponents<T>()
		{
			return this.gameObject.GetComponents<T>();
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00021EB0 File Offset: 0x000200B0
		public bool CompareTag(string tag)
		{
			return this.gameObject.CompareTag(tag);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00021ED0 File Offset: 0x000200D0
		[FreeFunction("SendMessage", HasExplicitThis = true)]
		public unsafe void SendMessage(string methodName, object value, SendMessageOptions options)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Component>(this);
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
				Component.SendMessage_Injected(intPtr, ref managedSpanWrapper, value, options);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00021F38 File Offset: 0x00020138
		[FreeFunction("BroadcastMessage", HasExplicitThis = true)]
		public unsafe void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Component>(this);
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
				Component.BroadcastMessage_Injected(intPtr, ref managedSpanWrapper, parameter, options);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001011 RID: 4113
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_transform_Injected(IntPtr _unity_self);

		// Token: 0x06001012 RID: 4114
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_gameObject_Injected(IntPtr _unity_self);

		// Token: 0x06001013 RID: 4115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetComponentFastPath_Injected(IntPtr _unity_self, Type type, IntPtr oneFurtherThanResultValue);

		// Token: 0x06001014 RID: 4116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetComponentsForListInternal_Injected(IntPtr _unity_self, Type searchType, object resultList);

		// Token: 0x06001015 RID: 4117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendMessage_Injected(IntPtr _unity_self, ref ManagedSpanWrapper methodName, object value, SendMessageOptions options);

		// Token: 0x06001016 RID: 4118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BroadcastMessage_Injected(IntPtr _unity_self, ref ManagedSpanWrapper methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);
	}
}
