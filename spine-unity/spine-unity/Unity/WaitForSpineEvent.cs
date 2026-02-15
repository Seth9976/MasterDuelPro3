using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x0200007C RID: 124
	public class WaitForSpineEvent : IEnumerator
	{
		// Token: 0x06000375 RID: 885 RVA: 0x00013A2C File Offset: 0x00011C2C
		private void Subscribe(AnimationState state, EventData eventDataReference, bool unsubscribe)
		{
			if (state == null)
			{
				Debug.LogWarning("AnimationState argument was null. Coroutine will continue immediately.");
				this.m_WasFired = true;
				return;
			}
			if (eventDataReference == null)
			{
				Debug.LogWarning("eventDataReference argument was null. Coroutine will continue immediately.");
				this.m_WasFired = true;
				return;
			}
			this.m_AnimationState = state;
			this.m_TargetEvent = eventDataReference;
			state.Event += this.HandleAnimationStateEvent;
			this.m_unsubscribeAfterFiring = unsubscribe;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00013A8C File Offset: 0x00011C8C
		private void SubscribeByName(AnimationState state, string eventName, bool unsubscribe)
		{
			if (state == null)
			{
				Debug.LogWarning("AnimationState argument was null. Coroutine will continue immediately.");
				this.m_WasFired = true;
				return;
			}
			if (string.IsNullOrEmpty(eventName))
			{
				Debug.LogWarning("eventName argument was null. Coroutine will continue immediately.");
				this.m_WasFired = true;
				return;
			}
			this.m_AnimationState = state;
			this.m_EventName = eventName;
			state.Event += this.HandleAnimationStateEventByName;
			this.m_unsubscribeAfterFiring = unsubscribe;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00013AEF File Offset: 0x00011CEF
		public WaitForSpineEvent(AnimationState state, EventData eventDataReference, bool unsubscribeAfterFiring = true)
		{
			this.Subscribe(state, eventDataReference, unsubscribeAfterFiring);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00013B00 File Offset: 0x00011D00
		public WaitForSpineEvent(SkeletonAnimation skeletonAnimation, EventData eventDataReference, bool unsubscribeAfterFiring = true)
		{
			this.Subscribe(skeletonAnimation.state, eventDataReference, unsubscribeAfterFiring);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00013B16 File Offset: 0x00011D16
		public WaitForSpineEvent(AnimationState state, string eventName, bool unsubscribeAfterFiring = true)
		{
			this.SubscribeByName(state, eventName, unsubscribeAfterFiring);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00013B27 File Offset: 0x00011D27
		public WaitForSpineEvent(SkeletonAnimation skeletonAnimation, string eventName, bool unsubscribeAfterFiring = true)
		{
			this.SubscribeByName(skeletonAnimation.state, eventName, unsubscribeAfterFiring);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00013B40 File Offset: 0x00011D40
		private void HandleAnimationStateEventByName(TrackEntry trackEntry, Event e)
		{
			this.m_WasFired |= e.Data.Name == this.m_EventName;
			if (this.m_WasFired && this.m_unsubscribeAfterFiring)
			{
				this.m_AnimationState.Event -= this.HandleAnimationStateEventByName;
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00013B98 File Offset: 0x00011D98
		private void HandleAnimationStateEvent(TrackEntry trackEntry, Event e)
		{
			this.m_WasFired |= e.Data == this.m_TargetEvent;
			if (this.m_WasFired && this.m_unsubscribeAfterFiring)
			{
				this.m_AnimationState.Event -= this.HandleAnimationStateEvent;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00013BE7 File Offset: 0x00011DE7
		// (set) Token: 0x0600037E RID: 894 RVA: 0x00013BEF File Offset: 0x00011DEF
		public bool WillUnsubscribeAfterFiring
		{
			get
			{
				return this.m_unsubscribeAfterFiring;
			}
			set
			{
				this.m_unsubscribeAfterFiring = value;
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00013BF8 File Offset: 0x00011DF8
		public WaitForSpineEvent NowWaitFor(AnimationState state, EventData eventDataReference, bool unsubscribeAfterFiring = true)
		{
			((IEnumerator)this).Reset();
			this.Clear(state);
			this.Subscribe(state, eventDataReference, unsubscribeAfterFiring);
			return this;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00013C11 File Offset: 0x00011E11
		public WaitForSpineEvent NowWaitFor(AnimationState state, string eventName, bool unsubscribeAfterFiring = true)
		{
			((IEnumerator)this).Reset();
			this.Clear(state);
			this.SubscribeByName(state, eventName, unsubscribeAfterFiring);
			return this;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00013C2A File Offset: 0x00011E2A
		private void Clear(AnimationState state)
		{
			state.Event -= this.HandleAnimationStateEvent;
			state.Event -= this.HandleAnimationStateEventByName;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00013C50 File Offset: 0x00011E50
		bool IEnumerator.MoveNext()
		{
			if (this.m_WasFired)
			{
				((IEnumerator)this).Reset();
				return false;
			}
			return true;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00013C63 File Offset: 0x00011E63
		void IEnumerator.Reset()
		{
			this.m_WasFired = false;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0001394C File Offset: 0x00011B4C
		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000232 RID: 562
		private EventData m_TargetEvent;

		// Token: 0x04000233 RID: 563
		private string m_EventName;

		// Token: 0x04000234 RID: 564
		private AnimationState m_AnimationState;

		// Token: 0x04000235 RID: 565
		private bool m_WasFired;

		// Token: 0x04000236 RID: 566
		private bool m_unsubscribeAfterFiring;
	}
}
