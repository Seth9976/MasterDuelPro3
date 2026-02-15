using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Duel;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Deck
{
	// Token: 0x0200100A RID: 4106
	public class TransitionCard : CardBase
	{
		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06007B98 RID: 31640 RVA: 0x000F6724 File Offset: 0x000F4924
		private Vector3 target
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06007B99 RID: 31641 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isAvailableTarget
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007B9A RID: 31642 RVA: 0x0000216A File Offset: 0x0000036A
		public static TransitionCard Create(TransitionCard prefab, Transform parent)
		{
			return null;
		}

		// Token: 0x06007B9B RID: 31643 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06007B9C RID: 31644 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(int id, int style)
		{
		}

		// Token: 0x06007B9D RID: 31645 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(CardBaseData cbd)
		{
		}

		// Token: 0x06007B9E RID: 31646 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPosition(Vector3 position)
		{
		}

		// Token: 0x06007B9F RID: 31647 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartCardTracing(TransitionCard.MotionMode mode, Vector3 basePosition, Transform target, bool outFade, TransitionCard.Size size, Action onFinished)
		{
		}

		// Token: 0x06007BA0 RID: 31648 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartPositionTracing(TransitionCard.MotionMode mode, Vector3 basePosition, Vector3 target, bool outFade, TransitionCard.Size size, Action onFinished)
		{
		}

		// Token: 0x06007BA1 RID: 31649 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartAddEffect(Transform target, TransitionCard.Size size, Action onFinished)
		{
		}

		// Token: 0x06007BA2 RID: 31650 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartTracing(TransitionCard.MotionMode mode, Vector3 basePosition, bool outFade, TransitionCard.Size size, Action onFinished)
		{
		}

		// Token: 0x06007BA3 RID: 31651 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06007BA4 RID: 31652 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePivotPosition()
		{
		}

		// Token: 0x06007BA5 RID: 31653 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateMoving()
		{
		}

		// Token: 0x06007BA6 RID: 31654 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateFadeOut()
		{
		}

		// Token: 0x06007BA7 RID: 31655 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateHistory()
		{
		}

		// Token: 0x06007BA8 RID: 31656 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCardPosition()
		{
		}

		// Token: 0x06007BA9 RID: 31657 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnTraceFinished()
		{
		}

		// Token: 0x06007BAA RID: 31658 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayTween(List<Tween> tweenList)
		{
		}

		// Token: 0x06007BAB RID: 31659 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTweenDuration(List<Tween> tweenList, float duration)
		{
		}

		// Token: 0x06007BAC RID: 31660 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsTweenPlaying(List<Tween> tweenList)
		{
			return false;
		}

		// Token: 0x0400B334 RID: 45876
		[SerializeField]
		private BezierMotionContainer bezierMotionAdd;

		// Token: 0x0400B335 RID: 45877
		[SerializeField]
		private BezierMotionContainer bezierMotionRemove;

		// Token: 0x0400B336 RID: 45878
		private ElementObjectManager elements;

		// Token: 0x0400B337 RID: 45879
		private List<RawImage> cardImages;

		// Token: 0x0400B338 RID: 45880
		private List<Tween> tweenIn;

		// Token: 0x0400B339 RID: 45881
		private List<Tween> tweenOut;

		// Token: 0x0400B33A RID: 45882
		private List<Tween> tweenOutFade;

		// Token: 0x0400B33B RID: 45883
		private List<Tween> tweenLargeSize;

		// Token: 0x0400B33C RID: 45884
		private List<Tween> tweenSmallSize;

		// Token: 0x0400B33D RID: 45885
		private Transform pivot;

		// Token: 0x0400B33E RID: 45886
		private Vector3 basePosition;

		// Token: 0x0400B33F RID: 45887
		private Transform targetTransform;

		// Token: 0x0400B340 RID: 45888
		private Vector3 targetPosition;

		// Token: 0x0400B341 RID: 45889
		private Vector3[] positionHistory;

		// Token: 0x0400B342 RID: 45890
		private float remainTime;

		// Token: 0x0400B343 RID: 45891
		private const float traceTime = 0.5f;

		// Token: 0x0400B344 RID: 45892
		private ChainedBezierMotion motion;

		// Token: 0x0400B345 RID: 45893
		private bool outFade;

		// Token: 0x0400B346 RID: 45894
		private Action onTraceFinishedCallback;

		// Token: 0x0400B347 RID: 45895
		private TransitionCard.TraceTargetType traceTargetType;

		// Token: 0x0400B348 RID: 45896
		private TransitionCard.MotionMode mode;

		// Token: 0x0400B349 RID: 45897
		private TransitionCard.Step step;

		// Token: 0x0200100B RID: 4107
		public enum TraceTargetType
		{
			// Token: 0x0400B34B RID: 45899
			Transform,
			// Token: 0x0400B34C RID: 45900
			Position
		}

		// Token: 0x0200100C RID: 4108
		public enum MotionMode
		{
			// Token: 0x0400B34E RID: 45902
			Linear,
			// Token: 0x0400B34F RID: 45903
			BezierAdd,
			// Token: 0x0400B350 RID: 45904
			BezierRemove,
			// Token: 0x0400B351 RID: 45905
			NoMove
		}

		// Token: 0x0200100D RID: 4109
		public enum Size
		{
			// Token: 0x0400B353 RID: 45907
			NoChange,
			// Token: 0x0400B354 RID: 45908
			ToLarge,
			// Token: 0x0400B355 RID: 45909
			ToSmall
		}

		// Token: 0x0200100E RID: 4110
		private enum Step
		{
			// Token: 0x0400B357 RID: 45911
			Idle,
			// Token: 0x0400B358 RID: 45912
			Moving,
			// Token: 0x0400B359 RID: 45913
			FadeOut
		}
	}
}
