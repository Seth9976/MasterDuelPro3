using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D6 RID: 470
	public abstract class CallbackEventHandler : IEventHandler
	{
		// Token: 0x06000D3B RID: 3387 RVA: 0x0003E8B4 File Offset: 0x0003CAB4
		public void RegisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			EventCallbackRegistry eventCallbackRegistry;
			if ((eventCallbackRegistry = this.m_CallbackRegistry) == null)
			{
				eventCallbackRegistry = (this.m_CallbackRegistry = new EventCallbackRegistry());
			}
			eventCallbackRegistry.RegisterCallback<TEventType>(callback, useTrickleDown, InvokePolicy.Default);
			this.AddEventCategories<TEventType>(useTrickleDown);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0003E900 File Offset: 0x0003CB00
		private void AddEventCategories<TEventType>(TrickleDown useTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			VisualElement ve = this as VisualElement;
			bool flag = ve != null;
			if (flag)
			{
				ve.AddEventCallbackCategories(1 << (int)EventBase<TEventType>.EventCategory, useTrickleDown);
			}
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0003E934 File Offset: 0x0003CB34
		public void RegisterCallback<TEventType, TUserArgsType>(EventCallback<TEventType, TUserArgsType> callback, TUserArgsType userArgs, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			EventCallbackRegistry eventCallbackRegistry;
			if ((eventCallbackRegistry = this.m_CallbackRegistry) == null)
			{
				eventCallbackRegistry = (this.m_CallbackRegistry = new EventCallbackRegistry());
			}
			eventCallbackRegistry.RegisterCallback<TEventType, TUserArgsType>(callback, userArgs, useTrickleDown, InvokePolicy.Default);
			this.AddEventCategories<TEventType>(useTrickleDown);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0003E980 File Offset: 0x0003CB80
		internal void RegisterCallback<TEventType>(EventCallback<TEventType> callback, InvokePolicy invokePolicy, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			EventCallbackRegistry eventCallbackRegistry;
			if ((eventCallbackRegistry = this.m_CallbackRegistry) == null)
			{
				eventCallbackRegistry = (this.m_CallbackRegistry = new EventCallbackRegistry());
			}
			eventCallbackRegistry.RegisterCallback<TEventType>(callback, useTrickleDown, invokePolicy);
			this.AddEventCategories<TEventType>(useTrickleDown);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0003E9B8 File Offset: 0x0003CBB8
		public void UnregisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			EventCallbackRegistry callbackRegistry = this.m_CallbackRegistry;
			if (callbackRegistry != null)
			{
				callbackRegistry.UnregisterCallback<TEventType>(callback, useTrickleDown);
			}
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0003E9F0 File Offset: 0x0003CBF0
		public void UnregisterCallback<TEventType, TUserArgsType>(EventCallback<TEventType, TUserArgsType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			EventCallbackRegistry callbackRegistry = this.m_CallbackRegistry;
			if (callbackRegistry != null)
			{
				callbackRegistry.UnregisterCallback<TEventType, TUserArgsType>(callback, useTrickleDown);
			}
		}

		// Token: 0x06000D41 RID: 3393
		public abstract void SendEvent(EventBase e);

		// Token: 0x06000D42 RID: 3394
		internal abstract void SendEvent(EventBase e, DispatchMode dispatchMode);

		// Token: 0x06000D43 RID: 3395 RVA: 0x000020EA File Offset: 0x000002EA
		[EventInterest(EventInterestOptions.Inherit)]
		[Obsolete("Use HandleEventBubbleUp. Before proceeding, make sure you understand the latest changes to UIToolkit event propagation rules by visiting Unity's manual page https://docs.unity3d.com/Manual/UIE-Events-Dispatching.html")]
		protected virtual void ExecuteDefaultActionAtTarget(EventBase evt)
		{
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x000020EA File Offset: 0x000002EA
		[EventInterest(EventInterestOptions.Inherit)]
		protected virtual void HandleEventBubbleUp(EventBase evt)
		{
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x000020EA File Offset: 0x000002EA
		[EventInterest(EventInterestOptions.Inherit)]
		internal virtual void HandleEventBubbleUpDisabled(EventBase evt)
		{
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0003EA25 File Offset: 0x0003CC25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void HandleEventBubbleUpInternal(EventBase evt)
		{
			this.HandleEventBubbleUp(evt);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x000020EA File Offset: 0x000002EA
		[EventInterest(EventInterestOptions.Inherit)]
		protected virtual void HandleEventTrickleDown(EventBase evt)
		{
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x000020EA File Offset: 0x000002EA
		[EventInterest(EventInterestOptions.Inherit)]
		internal virtual void HandleEventTrickleDownDisabled(EventBase evt)
		{
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0003EA2F File Offset: 0x0003CC2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void HandleEventTrickleDownInternal(EventBase evt)
		{
			this.HandleEventTrickleDown(evt);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x000020EA File Offset: 0x000002EA
		[Obsolete("Use HandleEventBubbleUp. Before proceeding, make sure you understand the latest changes to UIToolkit event propagation rules by visiting Unity's manual page https://docs.unity3d.com/Manual/UIE-Events-Dispatching.html")]
		[EventInterest(EventInterestOptions.Inherit)]
		protected virtual void ExecuteDefaultAction(EventBase evt)
		{
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x000020EA File Offset: 0x000002EA
		[EventInterest(EventInterestOptions.Inherit)]
		[Obsolete("Use HandleEventBubbleUpDisabled.")]
		internal virtual void ExecuteDefaultActionDisabledAtTarget(EventBase evt)
		{
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x000020EA File Offset: 0x000002EA
		[EventInterest(EventInterestOptions.Inherit)]
		[Obsolete("Use HandleEventBubbleUpDisabled.")]
		internal virtual void ExecuteDefaultActionDisabled(EventBase evt)
		{
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0003EA39 File Offset: 0x0003CC39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ExecuteDefaultActionInternal(EventBase evt)
		{
			this.ExecuteDefaultAction(evt);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0003EA43 File Offset: 0x0003CC43
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ExecuteDefaultActionDisabledInternal(EventBase evt)
		{
			this.ExecuteDefaultActionDisabled(evt);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0003EA4D File Offset: 0x0003CC4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ExecuteDefaultActionAtTargetInternal(EventBase evt)
		{
			this.ExecuteDefaultActionAtTarget(evt);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0003EA57 File Offset: 0x0003CC57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ExecuteDefaultActionDisabledAtTargetInternal(EventBase evt)
		{
			this.ExecuteDefaultActionDisabledAtTarget(evt);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0003EA64 File Offset: 0x0003CC64
		protected void NotifyPropertyChanged(in BindingId property)
		{
			VisualElement element = this as VisualElement;
			bool flag = ((element != null) ? element.elementPanel : null) == null;
			if (!flag)
			{
				using (PropertyChangedEvent evt = PropertyChangedEvent.GetPooled(in property))
				{
					evt.target = this;
					this.SendEvent(evt);
				}
			}
		}

		// Token: 0x04000839 RID: 2105
		internal bool isIMGUIContainer = false;

		// Token: 0x0400083A RID: 2106
		internal EventCallbackRegistry m_CallbackRegistry;

		// Token: 0x0400083B RID: 2107
		internal const string HandleEventBubbleUpName = "HandleEventBubbleUp";

		// Token: 0x0400083C RID: 2108
		internal const string HandleEventTrickleDownName = "HandleEventTrickleDown";

		// Token: 0x0400083D RID: 2109
		internal const string ExecuteDefaultActionName = "ExecuteDefaultAction";

		// Token: 0x0400083E RID: 2110
		internal const string ExecuteDefaultActionAtTargetName = "ExecuteDefaultActionAtTarget";
	}
}
