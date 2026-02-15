using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200018B RID: 395
	public static class AsyncTriggerExtensions
	{
		// Token: 0x06000961 RID: 2401 RVA: 0x0002A10A File Offset: 0x0002830A
		public static AsyncAwakeTrigger GetAsyncAwakeTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncAwakeTrigger>(gameObject);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0002A112 File Offset: 0x00028312
		public static AsyncAwakeTrigger GetAsyncAwakeTrigger(this Component component)
		{
			return component.gameObject.GetAsyncAwakeTrigger();
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0002A11F File Offset: 0x0002831F
		public static AsyncDestroyTrigger GetAsyncDestroyTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncDestroyTrigger>(gameObject);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0002A127 File Offset: 0x00028327
		public static AsyncDestroyTrigger GetAsyncDestroyTrigger(this Component component)
		{
			return component.gameObject.GetAsyncDestroyTrigger();
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0002A134 File Offset: 0x00028334
		public static AsyncStartTrigger GetAsyncStartTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncStartTrigger>(gameObject);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0002A13C File Offset: 0x0002833C
		public static AsyncStartTrigger GetAsyncStartTrigger(this Component component)
		{
			return component.gameObject.GetAsyncStartTrigger();
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0002A14C File Offset: 0x0002834C
		private static T GetOrAddComponent<T>(GameObject gameObject) where T : Component
		{
			T component;
			if (!gameObject.TryGetComponent<T>(out component))
			{
				component = gameObject.AddComponent<T>();
			}
			return component;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0002A16B File Offset: 0x0002836B
		public static UniTask OnDestroyAsync(this GameObject gameObject)
		{
			return gameObject.GetAsyncDestroyTrigger().OnDestroyAsync();
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0002A178 File Offset: 0x00028378
		public static UniTask OnDestroyAsync(this Component component)
		{
			return component.GetAsyncDestroyTrigger().OnDestroyAsync();
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002A185 File Offset: 0x00028385
		public static UniTask StartAsync(this GameObject gameObject)
		{
			return gameObject.GetAsyncStartTrigger().StartAsync();
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0002A192 File Offset: 0x00028392
		public static UniTask StartAsync(this Component component)
		{
			return component.GetAsyncStartTrigger().StartAsync();
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0002A19F File Offset: 0x0002839F
		public static UniTask AwakeAsync(this GameObject gameObject)
		{
			return gameObject.GetAsyncAwakeTrigger().AwakeAsync();
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0002A1AC File Offset: 0x000283AC
		public static UniTask AwakeAsync(this Component component)
		{
			return component.GetAsyncAwakeTrigger().AwakeAsync();
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0002A1B9 File Offset: 0x000283B9
		public static AsyncFixedUpdateTrigger GetAsyncFixedUpdateTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncFixedUpdateTrigger>(gameObject);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0002A1C1 File Offset: 0x000283C1
		public static AsyncFixedUpdateTrigger GetAsyncFixedUpdateTrigger(this Component component)
		{
			return component.gameObject.GetAsyncFixedUpdateTrigger();
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0002A1CE File Offset: 0x000283CE
		public static AsyncLateUpdateTrigger GetAsyncLateUpdateTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncLateUpdateTrigger>(gameObject);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0002A1D6 File Offset: 0x000283D6
		public static AsyncLateUpdateTrigger GetAsyncLateUpdateTrigger(this Component component)
		{
			return component.gameObject.GetAsyncLateUpdateTrigger();
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0002A1E3 File Offset: 0x000283E3
		public static AsyncAnimatorIKTrigger GetAsyncAnimatorIKTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncAnimatorIKTrigger>(gameObject);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0002A1EB File Offset: 0x000283EB
		public static AsyncAnimatorIKTrigger GetAsyncAnimatorIKTrigger(this Component component)
		{
			return component.gameObject.GetAsyncAnimatorIKTrigger();
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0002A1F8 File Offset: 0x000283F8
		public static AsyncAnimatorMoveTrigger GetAsyncAnimatorMoveTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncAnimatorMoveTrigger>(gameObject);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0002A200 File Offset: 0x00028400
		public static AsyncAnimatorMoveTrigger GetAsyncAnimatorMoveTrigger(this Component component)
		{
			return component.gameObject.GetAsyncAnimatorMoveTrigger();
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0002A20D File Offset: 0x0002840D
		public static AsyncApplicationFocusTrigger GetAsyncApplicationFocusTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncApplicationFocusTrigger>(gameObject);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0002A215 File Offset: 0x00028415
		public static AsyncApplicationFocusTrigger GetAsyncApplicationFocusTrigger(this Component component)
		{
			return component.gameObject.GetAsyncApplicationFocusTrigger();
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0002A222 File Offset: 0x00028422
		public static AsyncApplicationPauseTrigger GetAsyncApplicationPauseTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncApplicationPauseTrigger>(gameObject);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0002A22A File Offset: 0x0002842A
		public static AsyncApplicationPauseTrigger GetAsyncApplicationPauseTrigger(this Component component)
		{
			return component.gameObject.GetAsyncApplicationPauseTrigger();
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0002A237 File Offset: 0x00028437
		public static AsyncApplicationQuitTrigger GetAsyncApplicationQuitTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncApplicationQuitTrigger>(gameObject);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0002A23F File Offset: 0x0002843F
		public static AsyncApplicationQuitTrigger GetAsyncApplicationQuitTrigger(this Component component)
		{
			return component.gameObject.GetAsyncApplicationQuitTrigger();
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0002A24C File Offset: 0x0002844C
		public static AsyncAudioFilterReadTrigger GetAsyncAudioFilterReadTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncAudioFilterReadTrigger>(gameObject);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0002A254 File Offset: 0x00028454
		public static AsyncAudioFilterReadTrigger GetAsyncAudioFilterReadTrigger(this Component component)
		{
			return component.gameObject.GetAsyncAudioFilterReadTrigger();
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0002A261 File Offset: 0x00028461
		public static AsyncBecameInvisibleTrigger GetAsyncBecameInvisibleTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncBecameInvisibleTrigger>(gameObject);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0002A269 File Offset: 0x00028469
		public static AsyncBecameInvisibleTrigger GetAsyncBecameInvisibleTrigger(this Component component)
		{
			return component.gameObject.GetAsyncBecameInvisibleTrigger();
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0002A276 File Offset: 0x00028476
		public static AsyncBecameVisibleTrigger GetAsyncBecameVisibleTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncBecameVisibleTrigger>(gameObject);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0002A27E File Offset: 0x0002847E
		public static AsyncBecameVisibleTrigger GetAsyncBecameVisibleTrigger(this Component component)
		{
			return component.gameObject.GetAsyncBecameVisibleTrigger();
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0002A28B File Offset: 0x0002848B
		public static AsyncBeforeTransformParentChangedTrigger GetAsyncBeforeTransformParentChangedTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncBeforeTransformParentChangedTrigger>(gameObject);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0002A293 File Offset: 0x00028493
		public static AsyncBeforeTransformParentChangedTrigger GetAsyncBeforeTransformParentChangedTrigger(this Component component)
		{
			return component.gameObject.GetAsyncBeforeTransformParentChangedTrigger();
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0002A2A0 File Offset: 0x000284A0
		public static AsyncOnCanvasGroupChangedTrigger GetAsyncOnCanvasGroupChangedTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncOnCanvasGroupChangedTrigger>(gameObject);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0002A2A8 File Offset: 0x000284A8
		public static AsyncOnCanvasGroupChangedTrigger GetAsyncOnCanvasGroupChangedTrigger(this Component component)
		{
			return component.gameObject.GetAsyncOnCanvasGroupChangedTrigger();
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0002A2B5 File Offset: 0x000284B5
		public static AsyncCollisionEnterTrigger GetAsyncCollisionEnterTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncCollisionEnterTrigger>(gameObject);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002A2BD File Offset: 0x000284BD
		public static AsyncCollisionEnterTrigger GetAsyncCollisionEnterTrigger(this Component component)
		{
			return component.gameObject.GetAsyncCollisionEnterTrigger();
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0002A2CA File Offset: 0x000284CA
		public static AsyncCollisionEnter2DTrigger GetAsyncCollisionEnter2DTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncCollisionEnter2DTrigger>(gameObject);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0002A2D2 File Offset: 0x000284D2
		public static AsyncCollisionEnter2DTrigger GetAsyncCollisionEnter2DTrigger(this Component component)
		{
			return component.gameObject.GetAsyncCollisionEnter2DTrigger();
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0002A2DF File Offset: 0x000284DF
		public static AsyncCollisionExitTrigger GetAsyncCollisionExitTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncCollisionExitTrigger>(gameObject);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0002A2E7 File Offset: 0x000284E7
		public static AsyncCollisionExitTrigger GetAsyncCollisionExitTrigger(this Component component)
		{
			return component.gameObject.GetAsyncCollisionExitTrigger();
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0002A2F4 File Offset: 0x000284F4
		public static AsyncCollisionExit2DTrigger GetAsyncCollisionExit2DTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncCollisionExit2DTrigger>(gameObject);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0002A2FC File Offset: 0x000284FC
		public static AsyncCollisionExit2DTrigger GetAsyncCollisionExit2DTrigger(this Component component)
		{
			return component.gameObject.GetAsyncCollisionExit2DTrigger();
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0002A309 File Offset: 0x00028509
		public static AsyncCollisionStayTrigger GetAsyncCollisionStayTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncCollisionStayTrigger>(gameObject);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0002A311 File Offset: 0x00028511
		public static AsyncCollisionStayTrigger GetAsyncCollisionStayTrigger(this Component component)
		{
			return component.gameObject.GetAsyncCollisionStayTrigger();
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0002A31E File Offset: 0x0002851E
		public static AsyncCollisionStay2DTrigger GetAsyncCollisionStay2DTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncCollisionStay2DTrigger>(gameObject);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0002A326 File Offset: 0x00028526
		public static AsyncCollisionStay2DTrigger GetAsyncCollisionStay2DTrigger(this Component component)
		{
			return component.gameObject.GetAsyncCollisionStay2DTrigger();
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002A333 File Offset: 0x00028533
		public static AsyncControllerColliderHitTrigger GetAsyncControllerColliderHitTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncControllerColliderHitTrigger>(gameObject);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0002A33B File Offset: 0x0002853B
		public static AsyncControllerColliderHitTrigger GetAsyncControllerColliderHitTrigger(this Component component)
		{
			return component.gameObject.GetAsyncControllerColliderHitTrigger();
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0002A348 File Offset: 0x00028548
		public static AsyncDisableTrigger GetAsyncDisableTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncDisableTrigger>(gameObject);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002A350 File Offset: 0x00028550
		public static AsyncDisableTrigger GetAsyncDisableTrigger(this Component component)
		{
			return component.gameObject.GetAsyncDisableTrigger();
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002A35D File Offset: 0x0002855D
		public static AsyncDrawGizmosTrigger GetAsyncDrawGizmosTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncDrawGizmosTrigger>(gameObject);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0002A365 File Offset: 0x00028565
		public static AsyncDrawGizmosTrigger GetAsyncDrawGizmosTrigger(this Component component)
		{
			return component.gameObject.GetAsyncDrawGizmosTrigger();
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0002A372 File Offset: 0x00028572
		public static AsyncDrawGizmosSelectedTrigger GetAsyncDrawGizmosSelectedTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncDrawGizmosSelectedTrigger>(gameObject);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0002A37A File Offset: 0x0002857A
		public static AsyncDrawGizmosSelectedTrigger GetAsyncDrawGizmosSelectedTrigger(this Component component)
		{
			return component.gameObject.GetAsyncDrawGizmosSelectedTrigger();
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0002A387 File Offset: 0x00028587
		public static AsyncEnableTrigger GetAsyncEnableTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncEnableTrigger>(gameObject);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0002A38F File Offset: 0x0002858F
		public static AsyncEnableTrigger GetAsyncEnableTrigger(this Component component)
		{
			return component.gameObject.GetAsyncEnableTrigger();
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0002A39C File Offset: 0x0002859C
		public static AsyncGUITrigger GetAsyncGUITrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncGUITrigger>(gameObject);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002A3A4 File Offset: 0x000285A4
		public static AsyncGUITrigger GetAsyncGUITrigger(this Component component)
		{
			return component.gameObject.GetAsyncGUITrigger();
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0002A3B1 File Offset: 0x000285B1
		public static AsyncJointBreakTrigger GetAsyncJointBreakTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncJointBreakTrigger>(gameObject);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0002A3B9 File Offset: 0x000285B9
		public static AsyncJointBreakTrigger GetAsyncJointBreakTrigger(this Component component)
		{
			return component.gameObject.GetAsyncJointBreakTrigger();
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0002A3C6 File Offset: 0x000285C6
		public static AsyncJointBreak2DTrigger GetAsyncJointBreak2DTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncJointBreak2DTrigger>(gameObject);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0002A3CE File Offset: 0x000285CE
		public static AsyncJointBreak2DTrigger GetAsyncJointBreak2DTrigger(this Component component)
		{
			return component.gameObject.GetAsyncJointBreak2DTrigger();
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0002A3DB File Offset: 0x000285DB
		public static AsyncMouseDownTrigger GetAsyncMouseDownTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncMouseDownTrigger>(gameObject);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0002A3E3 File Offset: 0x000285E3
		public static AsyncMouseDownTrigger GetAsyncMouseDownTrigger(this Component component)
		{
			return component.gameObject.GetAsyncMouseDownTrigger();
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0002A3F0 File Offset: 0x000285F0
		public static AsyncMouseDragTrigger GetAsyncMouseDragTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncMouseDragTrigger>(gameObject);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002A3F8 File Offset: 0x000285F8
		public static AsyncMouseDragTrigger GetAsyncMouseDragTrigger(this Component component)
		{
			return component.gameObject.GetAsyncMouseDragTrigger();
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0002A405 File Offset: 0x00028605
		public static AsyncMouseEnterTrigger GetAsyncMouseEnterTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncMouseEnterTrigger>(gameObject);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0002A40D File Offset: 0x0002860D
		public static AsyncMouseEnterTrigger GetAsyncMouseEnterTrigger(this Component component)
		{
			return component.gameObject.GetAsyncMouseEnterTrigger();
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0002A41A File Offset: 0x0002861A
		public static AsyncMouseExitTrigger GetAsyncMouseExitTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncMouseExitTrigger>(gameObject);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0002A422 File Offset: 0x00028622
		public static AsyncMouseExitTrigger GetAsyncMouseExitTrigger(this Component component)
		{
			return component.gameObject.GetAsyncMouseExitTrigger();
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0002A42F File Offset: 0x0002862F
		public static AsyncMouseOverTrigger GetAsyncMouseOverTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncMouseOverTrigger>(gameObject);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0002A437 File Offset: 0x00028637
		public static AsyncMouseOverTrigger GetAsyncMouseOverTrigger(this Component component)
		{
			return component.gameObject.GetAsyncMouseOverTrigger();
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002A444 File Offset: 0x00028644
		public static AsyncMouseUpTrigger GetAsyncMouseUpTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncMouseUpTrigger>(gameObject);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0002A44C File Offset: 0x0002864C
		public static AsyncMouseUpTrigger GetAsyncMouseUpTrigger(this Component component)
		{
			return component.gameObject.GetAsyncMouseUpTrigger();
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002A459 File Offset: 0x00028659
		public static AsyncMouseUpAsButtonTrigger GetAsyncMouseUpAsButtonTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncMouseUpAsButtonTrigger>(gameObject);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0002A461 File Offset: 0x00028661
		public static AsyncMouseUpAsButtonTrigger GetAsyncMouseUpAsButtonTrigger(this Component component)
		{
			return component.gameObject.GetAsyncMouseUpAsButtonTrigger();
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002A46E File Offset: 0x0002866E
		public static AsyncParticleCollisionTrigger GetAsyncParticleCollisionTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncParticleCollisionTrigger>(gameObject);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002A476 File Offset: 0x00028676
		public static AsyncParticleCollisionTrigger GetAsyncParticleCollisionTrigger(this Component component)
		{
			return component.gameObject.GetAsyncParticleCollisionTrigger();
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002A483 File Offset: 0x00028683
		public static AsyncParticleSystemStoppedTrigger GetAsyncParticleSystemStoppedTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncParticleSystemStoppedTrigger>(gameObject);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002A48B File Offset: 0x0002868B
		public static AsyncParticleSystemStoppedTrigger GetAsyncParticleSystemStoppedTrigger(this Component component)
		{
			return component.gameObject.GetAsyncParticleSystemStoppedTrigger();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002A498 File Offset: 0x00028698
		public static AsyncParticleTriggerTrigger GetAsyncParticleTriggerTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncParticleTriggerTrigger>(gameObject);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002A4A0 File Offset: 0x000286A0
		public static AsyncParticleTriggerTrigger GetAsyncParticleTriggerTrigger(this Component component)
		{
			return component.gameObject.GetAsyncParticleTriggerTrigger();
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002A4AD File Offset: 0x000286AD
		public static AsyncParticleUpdateJobScheduledTrigger GetAsyncParticleUpdateJobScheduledTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncParticleUpdateJobScheduledTrigger>(gameObject);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002A4B5 File Offset: 0x000286B5
		public static AsyncParticleUpdateJobScheduledTrigger GetAsyncParticleUpdateJobScheduledTrigger(this Component component)
		{
			return component.gameObject.GetAsyncParticleUpdateJobScheduledTrigger();
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002A4C2 File Offset: 0x000286C2
		public static AsyncPostRenderTrigger GetAsyncPostRenderTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncPostRenderTrigger>(gameObject);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0002A4CA File Offset: 0x000286CA
		public static AsyncPostRenderTrigger GetAsyncPostRenderTrigger(this Component component)
		{
			return component.gameObject.GetAsyncPostRenderTrigger();
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0002A4D7 File Offset: 0x000286D7
		public static AsyncPreCullTrigger GetAsyncPreCullTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncPreCullTrigger>(gameObject);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0002A4DF File Offset: 0x000286DF
		public static AsyncPreCullTrigger GetAsyncPreCullTrigger(this Component component)
		{
			return component.gameObject.GetAsyncPreCullTrigger();
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0002A4EC File Offset: 0x000286EC
		public static AsyncPreRenderTrigger GetAsyncPreRenderTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncPreRenderTrigger>(gameObject);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0002A4F4 File Offset: 0x000286F4
		public static AsyncPreRenderTrigger GetAsyncPreRenderTrigger(this Component component)
		{
			return component.gameObject.GetAsyncPreRenderTrigger();
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0002A501 File Offset: 0x00028701
		public static AsyncRectTransformDimensionsChangeTrigger GetAsyncRectTransformDimensionsChangeTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncRectTransformDimensionsChangeTrigger>(gameObject);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0002A509 File Offset: 0x00028709
		public static AsyncRectTransformDimensionsChangeTrigger GetAsyncRectTransformDimensionsChangeTrigger(this Component component)
		{
			return component.gameObject.GetAsyncRectTransformDimensionsChangeTrigger();
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0002A516 File Offset: 0x00028716
		public static AsyncRectTransformRemovedTrigger GetAsyncRectTransformRemovedTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncRectTransformRemovedTrigger>(gameObject);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0002A51E File Offset: 0x0002871E
		public static AsyncRectTransformRemovedTrigger GetAsyncRectTransformRemovedTrigger(this Component component)
		{
			return component.gameObject.GetAsyncRectTransformRemovedTrigger();
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0002A52B File Offset: 0x0002872B
		public static AsyncRenderImageTrigger GetAsyncRenderImageTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncRenderImageTrigger>(gameObject);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0002A533 File Offset: 0x00028733
		public static AsyncRenderImageTrigger GetAsyncRenderImageTrigger(this Component component)
		{
			return component.gameObject.GetAsyncRenderImageTrigger();
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0002A540 File Offset: 0x00028740
		public static AsyncRenderObjectTrigger GetAsyncRenderObjectTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncRenderObjectTrigger>(gameObject);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0002A548 File Offset: 0x00028748
		public static AsyncRenderObjectTrigger GetAsyncRenderObjectTrigger(this Component component)
		{
			return component.gameObject.GetAsyncRenderObjectTrigger();
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0002A555 File Offset: 0x00028755
		public static AsyncServerInitializedTrigger GetAsyncServerInitializedTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncServerInitializedTrigger>(gameObject);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0002A55D File Offset: 0x0002875D
		public static AsyncServerInitializedTrigger GetAsyncServerInitializedTrigger(this Component component)
		{
			return component.gameObject.GetAsyncServerInitializedTrigger();
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002A56A File Offset: 0x0002876A
		public static AsyncTransformChildrenChangedTrigger GetAsyncTransformChildrenChangedTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncTransformChildrenChangedTrigger>(gameObject);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002A572 File Offset: 0x00028772
		public static AsyncTransformChildrenChangedTrigger GetAsyncTransformChildrenChangedTrigger(this Component component)
		{
			return component.gameObject.GetAsyncTransformChildrenChangedTrigger();
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002A57F File Offset: 0x0002877F
		public static AsyncTransformParentChangedTrigger GetAsyncTransformParentChangedTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncTransformParentChangedTrigger>(gameObject);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0002A587 File Offset: 0x00028787
		public static AsyncTransformParentChangedTrigger GetAsyncTransformParentChangedTrigger(this Component component)
		{
			return component.gameObject.GetAsyncTransformParentChangedTrigger();
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0002A594 File Offset: 0x00028794
		public static AsyncTriggerEnterTrigger GetAsyncTriggerEnterTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncTriggerEnterTrigger>(gameObject);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0002A59C File Offset: 0x0002879C
		public static AsyncTriggerEnterTrigger GetAsyncTriggerEnterTrigger(this Component component)
		{
			return component.gameObject.GetAsyncTriggerEnterTrigger();
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002A5A9 File Offset: 0x000287A9
		public static AsyncTriggerEnter2DTrigger GetAsyncTriggerEnter2DTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncTriggerEnter2DTrigger>(gameObject);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0002A5B1 File Offset: 0x000287B1
		public static AsyncTriggerEnter2DTrigger GetAsyncTriggerEnter2DTrigger(this Component component)
		{
			return component.gameObject.GetAsyncTriggerEnter2DTrigger();
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0002A5BE File Offset: 0x000287BE
		public static AsyncTriggerExitTrigger GetAsyncTriggerExitTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncTriggerExitTrigger>(gameObject);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0002A5C6 File Offset: 0x000287C6
		public static AsyncTriggerExitTrigger GetAsyncTriggerExitTrigger(this Component component)
		{
			return component.gameObject.GetAsyncTriggerExitTrigger();
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0002A5D3 File Offset: 0x000287D3
		public static AsyncTriggerExit2DTrigger GetAsyncTriggerExit2DTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncTriggerExit2DTrigger>(gameObject);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002A5DB File Offset: 0x000287DB
		public static AsyncTriggerExit2DTrigger GetAsyncTriggerExit2DTrigger(this Component component)
		{
			return component.gameObject.GetAsyncTriggerExit2DTrigger();
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0002A5E8 File Offset: 0x000287E8
		public static AsyncTriggerStayTrigger GetAsyncTriggerStayTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncTriggerStayTrigger>(gameObject);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0002A5F0 File Offset: 0x000287F0
		public static AsyncTriggerStayTrigger GetAsyncTriggerStayTrigger(this Component component)
		{
			return component.gameObject.GetAsyncTriggerStayTrigger();
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0002A5FD File Offset: 0x000287FD
		public static AsyncTriggerStay2DTrigger GetAsyncTriggerStay2DTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncTriggerStay2DTrigger>(gameObject);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002A605 File Offset: 0x00028805
		public static AsyncTriggerStay2DTrigger GetAsyncTriggerStay2DTrigger(this Component component)
		{
			return component.gameObject.GetAsyncTriggerStay2DTrigger();
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0002A612 File Offset: 0x00028812
		public static AsyncValidateTrigger GetAsyncValidateTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncValidateTrigger>(gameObject);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002A61A File Offset: 0x0002881A
		public static AsyncValidateTrigger GetAsyncValidateTrigger(this Component component)
		{
			return component.gameObject.GetAsyncValidateTrigger();
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002A627 File Offset: 0x00028827
		public static AsyncWillRenderObjectTrigger GetAsyncWillRenderObjectTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncWillRenderObjectTrigger>(gameObject);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0002A62F File Offset: 0x0002882F
		public static AsyncWillRenderObjectTrigger GetAsyncWillRenderObjectTrigger(this Component component)
		{
			return component.gameObject.GetAsyncWillRenderObjectTrigger();
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002A63C File Offset: 0x0002883C
		public static AsyncResetTrigger GetAsyncResetTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncResetTrigger>(gameObject);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002A644 File Offset: 0x00028844
		public static AsyncResetTrigger GetAsyncResetTrigger(this Component component)
		{
			return component.gameObject.GetAsyncResetTrigger();
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0002A651 File Offset: 0x00028851
		public static AsyncUpdateTrigger GetAsyncUpdateTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncUpdateTrigger>(gameObject);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0002A659 File Offset: 0x00028859
		public static AsyncUpdateTrigger GetAsyncUpdateTrigger(this Component component)
		{
			return component.gameObject.GetAsyncUpdateTrigger();
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0002A666 File Offset: 0x00028866
		public static AsyncBeginDragTrigger GetAsyncBeginDragTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncBeginDragTrigger>(gameObject);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0002A66E File Offset: 0x0002886E
		public static AsyncBeginDragTrigger GetAsyncBeginDragTrigger(this Component component)
		{
			return component.gameObject.GetAsyncBeginDragTrigger();
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0002A67B File Offset: 0x0002887B
		public static AsyncCancelTrigger GetAsyncCancelTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncCancelTrigger>(gameObject);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0002A683 File Offset: 0x00028883
		public static AsyncCancelTrigger GetAsyncCancelTrigger(this Component component)
		{
			return component.gameObject.GetAsyncCancelTrigger();
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0002A690 File Offset: 0x00028890
		public static AsyncDeselectTrigger GetAsyncDeselectTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncDeselectTrigger>(gameObject);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0002A698 File Offset: 0x00028898
		public static AsyncDeselectTrigger GetAsyncDeselectTrigger(this Component component)
		{
			return component.gameObject.GetAsyncDeselectTrigger();
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0002A6A5 File Offset: 0x000288A5
		public static AsyncDragTrigger GetAsyncDragTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncDragTrigger>(gameObject);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0002A6AD File Offset: 0x000288AD
		public static AsyncDragTrigger GetAsyncDragTrigger(this Component component)
		{
			return component.gameObject.GetAsyncDragTrigger();
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0002A6BA File Offset: 0x000288BA
		public static AsyncDropTrigger GetAsyncDropTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncDropTrigger>(gameObject);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002A6C2 File Offset: 0x000288C2
		public static AsyncDropTrigger GetAsyncDropTrigger(this Component component)
		{
			return component.gameObject.GetAsyncDropTrigger();
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0002A6CF File Offset: 0x000288CF
		public static AsyncEndDragTrigger GetAsyncEndDragTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncEndDragTrigger>(gameObject);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0002A6D7 File Offset: 0x000288D7
		public static AsyncEndDragTrigger GetAsyncEndDragTrigger(this Component component)
		{
			return component.gameObject.GetAsyncEndDragTrigger();
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0002A6E4 File Offset: 0x000288E4
		public static AsyncInitializePotentialDragTrigger GetAsyncInitializePotentialDragTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncInitializePotentialDragTrigger>(gameObject);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0002A6EC File Offset: 0x000288EC
		public static AsyncInitializePotentialDragTrigger GetAsyncInitializePotentialDragTrigger(this Component component)
		{
			return component.gameObject.GetAsyncInitializePotentialDragTrigger();
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0002A6F9 File Offset: 0x000288F9
		public static AsyncMoveTrigger GetAsyncMoveTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncMoveTrigger>(gameObject);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0002A701 File Offset: 0x00028901
		public static AsyncMoveTrigger GetAsyncMoveTrigger(this Component component)
		{
			return component.gameObject.GetAsyncMoveTrigger();
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0002A70E File Offset: 0x0002890E
		public static AsyncPointerClickTrigger GetAsyncPointerClickTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncPointerClickTrigger>(gameObject);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0002A716 File Offset: 0x00028916
		public static AsyncPointerClickTrigger GetAsyncPointerClickTrigger(this Component component)
		{
			return component.gameObject.GetAsyncPointerClickTrigger();
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0002A723 File Offset: 0x00028923
		public static AsyncPointerDownTrigger GetAsyncPointerDownTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncPointerDownTrigger>(gameObject);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002A72B File Offset: 0x0002892B
		public static AsyncPointerDownTrigger GetAsyncPointerDownTrigger(this Component component)
		{
			return component.gameObject.GetAsyncPointerDownTrigger();
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002A738 File Offset: 0x00028938
		public static AsyncPointerEnterTrigger GetAsyncPointerEnterTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncPointerEnterTrigger>(gameObject);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002A740 File Offset: 0x00028940
		public static AsyncPointerEnterTrigger GetAsyncPointerEnterTrigger(this Component component)
		{
			return component.gameObject.GetAsyncPointerEnterTrigger();
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0002A74D File Offset: 0x0002894D
		public static AsyncPointerExitTrigger GetAsyncPointerExitTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncPointerExitTrigger>(gameObject);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002A755 File Offset: 0x00028955
		public static AsyncPointerExitTrigger GetAsyncPointerExitTrigger(this Component component)
		{
			return component.gameObject.GetAsyncPointerExitTrigger();
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002A762 File Offset: 0x00028962
		public static AsyncPointerUpTrigger GetAsyncPointerUpTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncPointerUpTrigger>(gameObject);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002A76A File Offset: 0x0002896A
		public static AsyncPointerUpTrigger GetAsyncPointerUpTrigger(this Component component)
		{
			return component.gameObject.GetAsyncPointerUpTrigger();
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0002A777 File Offset: 0x00028977
		public static AsyncScrollTrigger GetAsyncScrollTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncScrollTrigger>(gameObject);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0002A77F File Offset: 0x0002897F
		public static AsyncScrollTrigger GetAsyncScrollTrigger(this Component component)
		{
			return component.gameObject.GetAsyncScrollTrigger();
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002A78C File Offset: 0x0002898C
		public static AsyncSelectTrigger GetAsyncSelectTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncSelectTrigger>(gameObject);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0002A794 File Offset: 0x00028994
		public static AsyncSelectTrigger GetAsyncSelectTrigger(this Component component)
		{
			return component.gameObject.GetAsyncSelectTrigger();
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0002A7A1 File Offset: 0x000289A1
		public static AsyncSubmitTrigger GetAsyncSubmitTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncSubmitTrigger>(gameObject);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0002A7A9 File Offset: 0x000289A9
		public static AsyncSubmitTrigger GetAsyncSubmitTrigger(this Component component)
		{
			return component.gameObject.GetAsyncSubmitTrigger();
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0002A7B6 File Offset: 0x000289B6
		public static AsyncUpdateSelectedTrigger GetAsyncUpdateSelectedTrigger(this GameObject gameObject)
		{
			return AsyncTriggerExtensions.GetOrAddComponent<AsyncUpdateSelectedTrigger>(gameObject);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002A7BE File Offset: 0x000289BE
		public static AsyncUpdateSelectedTrigger GetAsyncUpdateSelectedTrigger(this Component component)
		{
			return component.gameObject.GetAsyncUpdateSelectedTrigger();
		}
	}
}
