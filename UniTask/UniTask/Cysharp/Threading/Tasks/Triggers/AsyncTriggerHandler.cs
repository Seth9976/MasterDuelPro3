using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks.Sources;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ParticleSystemJobs;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000195 RID: 405
	public sealed class AsyncTriggerHandler<T> : IAsyncOneShotTrigger, IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>, ITriggerHandler<T>, IDisposable, IAsyncFixedUpdateHandler, IAsyncLateUpdateHandler, IAsyncOnAnimatorIKHandler, IAsyncOnAnimatorMoveHandler, IAsyncOnApplicationFocusHandler, IAsyncOnApplicationPauseHandler, IAsyncOnApplicationQuitHandler, IAsyncOnAudioFilterReadHandler, IAsyncOnBecameInvisibleHandler, IAsyncOnBecameVisibleHandler, IAsyncOnBeforeTransformParentChangedHandler, IAsyncOnCanvasGroupChangedHandler, IAsyncOnCollisionEnterHandler, IAsyncOnCollisionEnter2DHandler, IAsyncOnCollisionExitHandler, IAsyncOnCollisionExit2DHandler, IAsyncOnCollisionStayHandler, IAsyncOnCollisionStay2DHandler, IAsyncOnControllerColliderHitHandler, IAsyncOnDisableHandler, IAsyncOnDrawGizmosHandler, IAsyncOnDrawGizmosSelectedHandler, IAsyncOnEnableHandler, IAsyncOnGUIHandler, IAsyncOnJointBreakHandler, IAsyncOnJointBreak2DHandler, IAsyncOnMouseDownHandler, IAsyncOnMouseDragHandler, IAsyncOnMouseEnterHandler, IAsyncOnMouseExitHandler, IAsyncOnMouseOverHandler, IAsyncOnMouseUpHandler, IAsyncOnMouseUpAsButtonHandler, IAsyncOnParticleCollisionHandler, IAsyncOnParticleSystemStoppedHandler, IAsyncOnParticleTriggerHandler, IAsyncOnParticleUpdateJobScheduledHandler, IAsyncOnPostRenderHandler, IAsyncOnPreCullHandler, IAsyncOnPreRenderHandler, IAsyncOnRectTransformDimensionsChangeHandler, IAsyncOnRectTransformRemovedHandler, IAsyncOnRenderImageHandler, IAsyncOnRenderObjectHandler, IAsyncOnServerInitializedHandler, IAsyncOnTransformChildrenChangedHandler, IAsyncOnTransformParentChangedHandler, IAsyncOnTriggerEnterHandler, IAsyncOnTriggerEnter2DHandler, IAsyncOnTriggerExitHandler, IAsyncOnTriggerExit2DHandler, IAsyncOnTriggerStayHandler, IAsyncOnTriggerStay2DHandler, IAsyncOnValidateHandler, IAsyncOnWillRenderObjectHandler, IAsyncResetHandler, IAsyncUpdateHandler, IAsyncOnBeginDragHandler, IAsyncOnCancelHandler, IAsyncOnDeselectHandler, IAsyncOnDragHandler, IAsyncOnDropHandler, IAsyncOnEndDragHandler, IAsyncOnInitializePotentialDragHandler, IAsyncOnMoveHandler, IAsyncOnPointerClickHandler, IAsyncOnPointerDownHandler, IAsyncOnPointerEnterHandler, IAsyncOnPointerExitHandler, IAsyncOnPointerUpHandler, IAsyncOnScrollHandler, IAsyncOnSelectHandler, IAsyncOnSubmitHandler, IAsyncOnUpdateSelectedHandler
	{
		// Token: 0x06000A2A RID: 2602 RVA: 0x0002AB67 File Offset: 0x00028D67
		UniTask IAsyncOneShotTrigger.OneShotAsync()
		{
			this.core.Reset();
			return new UniTask(this, this.core.Version);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0002AB85 File Offset: 0x00028D85
		internal CancellationToken CancellationToken
		{
			get
			{
				return this.cancellationToken;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0002AB8D File Offset: 0x00028D8D
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x0002AB95 File Offset: 0x00028D95
		ITriggerHandler<T> ITriggerHandler<T>.Prev { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0002AB9E File Offset: 0x00028D9E
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x0002ABA6 File Offset: 0x00028DA6
		ITriggerHandler<T> ITriggerHandler<T>.Next { get; set; }

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002ABB0 File Offset: 0x00028DB0
		internal AsyncTriggerHandler(AsyncTriggerBase<T> trigger, bool callOnce)
		{
			if (this.cancellationToken.IsCancellationRequested)
			{
				this.isDisposed = true;
				return;
			}
			this.trigger = trigger;
			this.cancellationToken = default(CancellationToken);
			this.registration = default(CancellationTokenRegistration);
			this.callOnce = callOnce;
			trigger.AddHandler(this);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002AC08 File Offset: 0x00028E08
		internal AsyncTriggerHandler(AsyncTriggerBase<T> trigger, CancellationToken cancellationToken, bool callOnce)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				this.isDisposed = true;
				return;
			}
			this.trigger = trigger;
			this.cancellationToken = cancellationToken;
			this.callOnce = callOnce;
			trigger.AddHandler(this);
			if (cancellationToken.CanBeCanceled)
			{
				this.registration = cancellationToken.RegisterWithoutCaptureExecutionContext(AsyncTriggerHandler<T>.cancellationCallback, this);
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002AC64 File Offset: 0x00028E64
		private static void CancellationCallback(object state)
		{
			AsyncTriggerHandler<T> self = (AsyncTriggerHandler<T>)state;
			self.Dispose();
			self.core.TrySetCanceled(self.cancellationToken);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002AC90 File Offset: 0x00028E90
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.registration.Dispose();
				this.trigger.RemoveHandler(this);
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002ACB8 File Offset: 0x00028EB8
		T IUniTaskSource<T>.GetResult(short token)
		{
			T result;
			try
			{
				result = this.core.GetResult(token);
			}
			finally
			{
				if (this.callOnce)
				{
					this.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002ACF4 File Offset: 0x00028EF4
		void ITriggerHandler<T>.OnNext(T value)
		{
			this.core.TrySetResult(value);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002AD03 File Offset: 0x00028F03
		void ITriggerHandler<T>.OnCanceled(CancellationToken cancellationToken)
		{
			this.core.TrySetCanceled(cancellationToken);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0002AD12 File Offset: 0x00028F12
		void ITriggerHandler<T>.OnCompleted()
		{
			this.core.TrySetCanceled(CancellationToken.None);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0002AD25 File Offset: 0x00028F25
		void ITriggerHandler<T>.OnError(Exception ex)
		{
			this.core.TrySetException(ex);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00028C84 File Offset: 0x00026E84
		void IUniTaskSource.GetResult(short token)
		{
			((IUniTaskSource<T>)this).GetResult(token);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0002AD34 File Offset: 0x00028F34
		UniTaskStatus IUniTaskSource.GetStatus(short token)
		{
			return this.core.GetStatus(token);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0002AD42 File Offset: 0x00028F42
		UniTaskStatus IUniTaskSource.UnsafeGetStatus()
		{
			return this.core.UnsafeGetStatus();
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002AD4F File Offset: 0x00028F4F
		void IUniTaskSource.OnCompleted(Action<object> continuation, object state, short token)
		{
			this.core.OnCompleted(continuation, state, token);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncFixedUpdateHandler.FixedUpdateAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncLateUpdateHandler.LateUpdateAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002AD82 File Offset: 0x00028F82
		UniTask<int> IAsyncOnAnimatorIKHandler.OnAnimatorIKAsync()
		{
			this.core.Reset();
			return new UniTask<int>((IUniTaskSource<int>)this, this.core.Version);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnAnimatorMoveHandler.OnAnimatorMoveAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002ADA5 File Offset: 0x00028FA5
		UniTask<bool> IAsyncOnApplicationFocusHandler.OnApplicationFocusAsync()
		{
			this.core.Reset();
			return new UniTask<bool>((IUniTaskSource<bool>)this, this.core.Version);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002ADA5 File Offset: 0x00028FA5
		UniTask<bool> IAsyncOnApplicationPauseHandler.OnApplicationPauseAsync()
		{
			this.core.Reset();
			return new UniTask<bool>((IUniTaskSource<bool>)this, this.core.Version);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnApplicationQuitHandler.OnApplicationQuitAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002ADC8 File Offset: 0x00028FC8
		[return: TupleElementNames(new string[] { "data", "channels" })]
		UniTask<ValueTuple<float[], int>> IAsyncOnAudioFilterReadHandler.OnAudioFilterReadAsync()
		{
			this.core.Reset();
			return new UniTask<ValueTuple<float[], int>>((IUniTaskSource<ValueTuple<float[], int>>)this, this.core.Version);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnBecameInvisibleHandler.OnBecameInvisibleAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnBecameVisibleHandler.OnBecameVisibleAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnBeforeTransformParentChangedHandler.OnBeforeTransformParentChangedAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnCanvasGroupChangedHandler.OnCanvasGroupChangedAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002ADEB File Offset: 0x00028FEB
		UniTask<Collision> IAsyncOnCollisionEnterHandler.OnCollisionEnterAsync()
		{
			this.core.Reset();
			return new UniTask<Collision>((IUniTaskSource<Collision>)this, this.core.Version);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002AE0E File Offset: 0x0002900E
		UniTask<Collision2D> IAsyncOnCollisionEnter2DHandler.OnCollisionEnter2DAsync()
		{
			this.core.Reset();
			return new UniTask<Collision2D>((IUniTaskSource<Collision2D>)this, this.core.Version);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0002ADEB File Offset: 0x00028FEB
		UniTask<Collision> IAsyncOnCollisionExitHandler.OnCollisionExitAsync()
		{
			this.core.Reset();
			return new UniTask<Collision>((IUniTaskSource<Collision>)this, this.core.Version);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002AE0E File Offset: 0x0002900E
		UniTask<Collision2D> IAsyncOnCollisionExit2DHandler.OnCollisionExit2DAsync()
		{
			this.core.Reset();
			return new UniTask<Collision2D>((IUniTaskSource<Collision2D>)this, this.core.Version);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002ADEB File Offset: 0x00028FEB
		UniTask<Collision> IAsyncOnCollisionStayHandler.OnCollisionStayAsync()
		{
			this.core.Reset();
			return new UniTask<Collision>((IUniTaskSource<Collision>)this, this.core.Version);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002AE0E File Offset: 0x0002900E
		UniTask<Collision2D> IAsyncOnCollisionStay2DHandler.OnCollisionStay2DAsync()
		{
			this.core.Reset();
			return new UniTask<Collision2D>((IUniTaskSource<Collision2D>)this, this.core.Version);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0002AE31 File Offset: 0x00029031
		UniTask<ControllerColliderHit> IAsyncOnControllerColliderHitHandler.OnControllerColliderHitAsync()
		{
			this.core.Reset();
			return new UniTask<ControllerColliderHit>((IUniTaskSource<ControllerColliderHit>)this, this.core.Version);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnDisableHandler.OnDisableAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnDrawGizmosHandler.OnDrawGizmosAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnDrawGizmosSelectedHandler.OnDrawGizmosSelectedAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnEnableHandler.OnEnableAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnGUIHandler.OnGUIAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002AE54 File Offset: 0x00029054
		UniTask<float> IAsyncOnJointBreakHandler.OnJointBreakAsync()
		{
			this.core.Reset();
			return new UniTask<float>((IUniTaskSource<float>)this, this.core.Version);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002AE77 File Offset: 0x00029077
		UniTask<Joint2D> IAsyncOnJointBreak2DHandler.OnJointBreak2DAsync()
		{
			this.core.Reset();
			return new UniTask<Joint2D>((IUniTaskSource<Joint2D>)this, this.core.Version);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnMouseDownHandler.OnMouseDownAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnMouseDragHandler.OnMouseDragAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnMouseEnterHandler.OnMouseEnterAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnMouseExitHandler.OnMouseExitAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnMouseOverHandler.OnMouseOverAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnMouseUpHandler.OnMouseUpAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnMouseUpAsButtonHandler.OnMouseUpAsButtonAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002AE9A File Offset: 0x0002909A
		UniTask<GameObject> IAsyncOnParticleCollisionHandler.OnParticleCollisionAsync()
		{
			this.core.Reset();
			return new UniTask<GameObject>((IUniTaskSource<GameObject>)this, this.core.Version);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnParticleSystemStoppedHandler.OnParticleSystemStoppedAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnParticleTriggerHandler.OnParticleTriggerAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002AEBD File Offset: 0x000290BD
		UniTask<ParticleSystemJobData> IAsyncOnParticleUpdateJobScheduledHandler.OnParticleUpdateJobScheduledAsync()
		{
			this.core.Reset();
			return new UniTask<ParticleSystemJobData>((IUniTaskSource<ParticleSystemJobData>)this, this.core.Version);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnPostRenderHandler.OnPostRenderAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnPreCullHandler.OnPreCullAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnPreRenderHandler.OnPreRenderAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnRectTransformDimensionsChangeHandler.OnRectTransformDimensionsChangeAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnRectTransformRemovedHandler.OnRectTransformRemovedAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0002AEE0 File Offset: 0x000290E0
		[return: TupleElementNames(new string[] { "source", "destination" })]
		UniTask<ValueTuple<RenderTexture, RenderTexture>> IAsyncOnRenderImageHandler.OnRenderImageAsync()
		{
			this.core.Reset();
			return new UniTask<ValueTuple<RenderTexture, RenderTexture>>((IUniTaskSource<ValueTuple<RenderTexture, RenderTexture>>)this, this.core.Version);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnRenderObjectHandler.OnRenderObjectAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnServerInitializedHandler.OnServerInitializedAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnTransformChildrenChangedHandler.OnTransformChildrenChangedAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnTransformParentChangedHandler.OnTransformParentChangedAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002AF03 File Offset: 0x00029103
		UniTask<Collider> IAsyncOnTriggerEnterHandler.OnTriggerEnterAsync()
		{
			this.core.Reset();
			return new UniTask<Collider>((IUniTaskSource<Collider>)this, this.core.Version);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002AF26 File Offset: 0x00029126
		UniTask<Collider2D> IAsyncOnTriggerEnter2DHandler.OnTriggerEnter2DAsync()
		{
			this.core.Reset();
			return new UniTask<Collider2D>((IUniTaskSource<Collider2D>)this, this.core.Version);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002AF03 File Offset: 0x00029103
		UniTask<Collider> IAsyncOnTriggerExitHandler.OnTriggerExitAsync()
		{
			this.core.Reset();
			return new UniTask<Collider>((IUniTaskSource<Collider>)this, this.core.Version);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002AF26 File Offset: 0x00029126
		UniTask<Collider2D> IAsyncOnTriggerExit2DHandler.OnTriggerExit2DAsync()
		{
			this.core.Reset();
			return new UniTask<Collider2D>((IUniTaskSource<Collider2D>)this, this.core.Version);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002AF03 File Offset: 0x00029103
		UniTask<Collider> IAsyncOnTriggerStayHandler.OnTriggerStayAsync()
		{
			this.core.Reset();
			return new UniTask<Collider>((IUniTaskSource<Collider>)this, this.core.Version);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002AF26 File Offset: 0x00029126
		UniTask<Collider2D> IAsyncOnTriggerStay2DHandler.OnTriggerStay2DAsync()
		{
			this.core.Reset();
			return new UniTask<Collider2D>((IUniTaskSource<Collider2D>)this, this.core.Version);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnValidateHandler.OnValidateAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncOnWillRenderObjectHandler.OnWillRenderObjectAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncResetHandler.ResetAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002AD5F File Offset: 0x00028F5F
		UniTask IAsyncUpdateHandler.UpdateAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002AF49 File Offset: 0x00029149
		UniTask<PointerEventData> IAsyncOnBeginDragHandler.OnBeginDragAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002AF6C File Offset: 0x0002916C
		UniTask<BaseEventData> IAsyncOnCancelHandler.OnCancelAsync()
		{
			this.core.Reset();
			return new UniTask<BaseEventData>((IUniTaskSource<BaseEventData>)this, this.core.Version);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002AF6C File Offset: 0x0002916C
		UniTask<BaseEventData> IAsyncOnDeselectHandler.OnDeselectAsync()
		{
			this.core.Reset();
			return new UniTask<BaseEventData>((IUniTaskSource<BaseEventData>)this, this.core.Version);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002AF49 File Offset: 0x00029149
		UniTask<PointerEventData> IAsyncOnDragHandler.OnDragAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002AF49 File Offset: 0x00029149
		UniTask<PointerEventData> IAsyncOnDropHandler.OnDropAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002AF49 File Offset: 0x00029149
		UniTask<PointerEventData> IAsyncOnEndDragHandler.OnEndDragAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002AF49 File Offset: 0x00029149
		UniTask<PointerEventData> IAsyncOnInitializePotentialDragHandler.OnInitializePotentialDragAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002AF8F File Offset: 0x0002918F
		UniTask<AxisEventData> IAsyncOnMoveHandler.OnMoveAsync()
		{
			this.core.Reset();
			return new UniTask<AxisEventData>((IUniTaskSource<AxisEventData>)this, this.core.Version);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002AF49 File Offset: 0x00029149
		UniTask<PointerEventData> IAsyncOnPointerClickHandler.OnPointerClickAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002AF49 File Offset: 0x00029149
		UniTask<PointerEventData> IAsyncOnPointerDownHandler.OnPointerDownAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002AF49 File Offset: 0x00029149
		UniTask<PointerEventData> IAsyncOnPointerEnterHandler.OnPointerEnterAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002AF49 File Offset: 0x00029149
		UniTask<PointerEventData> IAsyncOnPointerExitHandler.OnPointerExitAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002AF49 File Offset: 0x00029149
		UniTask<PointerEventData> IAsyncOnPointerUpHandler.OnPointerUpAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0002AF49 File Offset: 0x00029149
		UniTask<PointerEventData> IAsyncOnScrollHandler.OnScrollAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0002AF6C File Offset: 0x0002916C
		UniTask<BaseEventData> IAsyncOnSelectHandler.OnSelectAsync()
		{
			this.core.Reset();
			return new UniTask<BaseEventData>((IUniTaskSource<BaseEventData>)this, this.core.Version);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002AF6C File Offset: 0x0002916C
		UniTask<BaseEventData> IAsyncOnSubmitHandler.OnSubmitAsync()
		{
			this.core.Reset();
			return new UniTask<BaseEventData>((IUniTaskSource<BaseEventData>)this, this.core.Version);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0002AF6C File Offset: 0x0002916C
		UniTask<BaseEventData> IAsyncOnUpdateSelectedHandler.OnUpdateSelectedAsync()
		{
			this.core.Reset();
			return new UniTask<BaseEventData>((IUniTaskSource<BaseEventData>)this, this.core.Version);
		}

		// Token: 0x0400064C RID: 1612
		private static Action<object> cancellationCallback = new Action<object>(AsyncTriggerHandler<T>.CancellationCallback);

		// Token: 0x0400064D RID: 1613
		private readonly AsyncTriggerBase<T> trigger;

		// Token: 0x0400064E RID: 1614
		private CancellationToken cancellationToken;

		// Token: 0x0400064F RID: 1615
		private CancellationTokenRegistration registration;

		// Token: 0x04000650 RID: 1616
		private bool isDisposed;

		// Token: 0x04000651 RID: 1617
		private bool callOnce;

		// Token: 0x04000652 RID: 1618
		private UniTaskCompletionSourceCore<T> core;
	}
}
