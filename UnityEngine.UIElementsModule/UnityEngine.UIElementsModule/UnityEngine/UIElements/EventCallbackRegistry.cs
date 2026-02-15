using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D1 RID: 465
	internal class EventCallbackRegistry
	{
		// Token: 0x06000D14 RID: 3348 RVA: 0x0003D5A4 File Offset: 0x0003B7A4
		private static EventCallbackList GetCallbackList(EventCallbackList initializer = null)
		{
			return EventCallbackRegistry.s_ListPool.Get(initializer);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0003D5C1 File Offset: 0x0003B7C1
		private static void ReleaseCallbackList(EventCallbackList toRelease)
		{
			EventCallbackRegistry.s_ListPool.Release(toRelease);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0003D5D0 File Offset: 0x0003B7D0
		private ref EventCallbackRegistry.DynamicCallbackList GetDynamicCallbackList(TrickleDown useTrickleDown)
		{
			return ref useTrickleDown == TrickleDown.TrickleDown ? ref this.m_TrickleDownCallbacks : ref this.m_BubbleUpCallbacks;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0003D5F4 File Offset: 0x0003B7F4
		public void RegisterCallback<TEventType>([NotNull] EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown, InvokePolicy invokePolicy = InvokePolicy.Default) where TEventType : EventBase<TEventType>, new()
		{
			long eventTypeId = EventBase<TEventType>.TypeId();
			ref EventCallbackRegistry.DynamicCallbackList dynamicCallbackList = ref this.GetDynamicCallbackList(useTrickleDown);
			EventCallbackList callbackList = dynamicCallbackList.GetCallbackListForReading();
			EventCallbackFunctor<TEventType> functor = callbackList.Find(eventTypeId, callback) as EventCallbackFunctor<TEventType>;
			bool flag = functor != null;
			if (flag)
			{
				functor.invokePolicy = invokePolicy;
			}
			else
			{
				callbackList = dynamicCallbackList.GetCallbackListForWriting();
				callbackList.Add(EventCallbackFunctor<TEventType>.GetPooled(eventTypeId, callback, invokePolicy));
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0003D650 File Offset: 0x0003B850
		public void RegisterCallback<TEventType, TCallbackArgs>([NotNull] EventCallback<TEventType, TCallbackArgs> callback, TCallbackArgs userArgs, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown, InvokePolicy invokePolicy = InvokePolicy.Default) where TEventType : EventBase<TEventType>, new()
		{
			long eventTypeId = EventBase<TEventType>.TypeId();
			ref EventCallbackRegistry.DynamicCallbackList dynamicCallbackList = ref this.GetDynamicCallbackList(useTrickleDown);
			EventCallbackList callbackList = dynamicCallbackList.GetCallbackListForReading();
			EventCallbackFunctor<TEventType, TCallbackArgs> functor = callbackList.Find(eventTypeId, callback) as EventCallbackFunctor<TEventType, TCallbackArgs>;
			bool flag = functor != null;
			if (flag)
			{
				functor.invokePolicy = invokePolicy;
				functor.userArgs = userArgs;
			}
			else
			{
				callbackList = dynamicCallbackList.GetCallbackListForWriting();
				callbackList.Add(EventCallbackFunctor<TEventType, TCallbackArgs>.GetPooled(eventTypeId, callback, userArgs, invokePolicy));
			}
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0003D6B8 File Offset: 0x0003B8B8
		public bool UnregisterCallback<TEventType>([NotNull] EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			return this.GetDynamicCallbackList(useTrickleDown).UnregisterCallback(EventBase<TEventType>.TypeId(), callback);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0003D6DC File Offset: 0x0003B8DC
		public bool UnregisterCallback<TEventType, TCallbackArgs>([NotNull] EventCallback<TEventType, TCallbackArgs> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			return this.GetDynamicCallbackList(useTrickleDown).UnregisterCallback(EventBase<TEventType>.TypeId(), callback);
		}

		// Token: 0x0400082A RID: 2090
		private static readonly EventCallbackListPool s_ListPool = new EventCallbackListPool();

		// Token: 0x0400082B RID: 2091
		internal EventCallbackRegistry.DynamicCallbackList m_TrickleDownCallbacks = EventCallbackRegistry.DynamicCallbackList.Create(TrickleDown.TrickleDown);

		// Token: 0x0400082C RID: 2092
		internal EventCallbackRegistry.DynamicCallbackList m_BubbleUpCallbacks = EventCallbackRegistry.DynamicCallbackList.Create(TrickleDown.NoTrickleDown);

		// Token: 0x020001D2 RID: 466
		internal struct DynamicCallbackList
		{
			// Token: 0x06000D1D RID: 3357 RVA: 0x0003D730 File Offset: 0x0003B930
			public static EventCallbackRegistry.DynamicCallbackList Create(TrickleDown useTrickleDown)
			{
				return new EventCallbackRegistry.DynamicCallbackList
				{
					m_UseTrickleDown = useTrickleDown,
					m_Callbacks = EventCallbackList.EmptyList,
					m_TemporaryCallbacks = null,
					m_UnregisteredCallbacksDuringInvoke = null,
					m_IsInvoking = 0
				};
			}

			// Token: 0x06000D1E RID: 3358 RVA: 0x0003D778 File Offset: 0x0003B978
			[NotNull]
			public EventCallbackList GetCallbackListForWriting()
			{
				EventCallbackList eventCallbackList;
				if (this.m_IsInvoking != 0)
				{
					if ((eventCallbackList = this.m_TemporaryCallbacks) == null)
					{
						eventCallbackList = (this.m_TemporaryCallbacks = EventCallbackRegistry.GetCallbackList(this.m_Callbacks));
					}
				}
				else
				{
					eventCallbackList = ((this.m_Callbacks != EventCallbackList.EmptyList) ? this.m_Callbacks : (this.m_Callbacks = EventCallbackRegistry.GetCallbackList(null)));
				}
				return eventCallbackList;
			}

			// Token: 0x06000D1F RID: 3359 RVA: 0x0003D7D8 File Offset: 0x0003B9D8
			[NotNull]
			public readonly EventCallbackList GetCallbackListForReading()
			{
				return this.m_TemporaryCallbacks ?? this.m_Callbacks;
			}

			// Token: 0x06000D20 RID: 3360 RVA: 0x0003D7FC File Offset: 0x0003B9FC
			public bool UnregisterCallback(long eventTypeId, [NotNull] Delegate callback)
			{
				EventCallbackList callbackList = this.GetCallbackListForWriting();
				EventCallbackFunctorBase functor;
				bool flag = !callbackList.Remove(eventTypeId, callback, out functor);
				bool flag2;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					bool flag3 = this.m_IsInvoking > 0;
					if (flag3)
					{
						List<EventCallbackFunctorBase> list;
						if ((list = this.m_UnregisteredCallbacksDuringInvoke) == null)
						{
							list = (this.m_UnregisteredCallbacksDuringInvoke = CollectionPool<List<EventCallbackFunctorBase>, EventCallbackFunctorBase>.Get());
						}
						list.Add(functor);
					}
					else
					{
						functor.Dispose();
					}
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x06000D21 RID: 3361 RVA: 0x0003D864 File Offset: 0x0003BA64
			public unsafe void Invoke(EventBase evt, BaseVisualElementPanel panel, VisualElement target)
			{
				this.BeginInvoke();
				try
				{
					bool enabled = !evt.skipDisabledElements || target.enabledInHierarchy;
					long eventTypeId = evt.eventTypeId;
					Span<EventCallbackFunctorBase> span = this.m_Callbacks.Span;
					for (int i = 0; i < span.Length; i++)
					{
						EventCallbackFunctorBase callback = *span[i];
						bool flag = callback.eventTypeId == eventTypeId && target.elementPanel == panel && (enabled || (callback.invokePolicy & InvokePolicy.IncludeDisabled) > InvokePolicy.Default);
						if (flag)
						{
							bool flag2 = (callback.invokePolicy & InvokePolicy.Once) > InvokePolicy.Default;
							if (flag2)
							{
								callback.UnregisterCallback(target, this.m_UseTrickleDown);
							}
							callback.Invoke(evt);
							bool isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
							if (isImmediatePropagationStopped)
							{
								break;
							}
						}
					}
				}
				finally
				{
					this.EndInvoke();
				}
			}

			// Token: 0x06000D22 RID: 3362 RVA: 0x0003D948 File Offset: 0x0003BB48
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void BeginInvoke()
			{
				this.m_IsInvoking++;
			}

			// Token: 0x06000D23 RID: 3363 RVA: 0x0003D95C File Offset: 0x0003BB5C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void EndInvoke()
			{
				this.m_IsInvoking--;
				bool flag = this.m_IsInvoking == 0;
				if (flag)
				{
					bool flag2 = this.m_TemporaryCallbacks != null;
					if (flag2)
					{
						bool flag3 = this.m_Callbacks != EventCallbackList.EmptyList;
						if (flag3)
						{
							EventCallbackRegistry.ReleaseCallbackList(this.m_Callbacks);
						}
						this.m_Callbacks = EventCallbackRegistry.GetCallbackList(this.m_TemporaryCallbacks);
						EventCallbackRegistry.ReleaseCallbackList(this.m_TemporaryCallbacks);
						this.m_TemporaryCallbacks = null;
						bool flag4 = this.m_UnregisteredCallbacksDuringInvoke != null;
						if (flag4)
						{
							foreach (EventCallbackFunctorBase functor in this.m_UnregisteredCallbacksDuringInvoke)
							{
								functor.Dispose();
							}
							CollectionPool<List<EventCallbackFunctorBase>, EventCallbackFunctorBase>.Release(this.m_UnregisteredCallbacksDuringInvoke);
							this.m_UnregisteredCallbacksDuringInvoke = null;
						}
					}
				}
			}

			// Token: 0x0400082D RID: 2093
			private TrickleDown m_UseTrickleDown;

			// Token: 0x0400082E RID: 2094
			[NotNull]
			private EventCallbackList m_Callbacks;

			// Token: 0x0400082F RID: 2095
			[CanBeNull]
			private EventCallbackList m_TemporaryCallbacks;

			// Token: 0x04000830 RID: 2096
			[CanBeNull]
			private List<EventCallbackFunctorBase> m_UnregisteredCallbacksDuringInvoke;

			// Token: 0x04000831 RID: 2097
			private int m_IsInvoking;
		}
	}
}
