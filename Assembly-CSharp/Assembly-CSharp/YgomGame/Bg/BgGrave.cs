using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Bg
{
	// Token: 0x0200113B RID: 4411
	public class BgGrave : MonoBehaviour
	{
		// Token: 0x06008334 RID: 33588 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetParticleEffect(BgGrave.ParticleEffectIdx startIdx, BgGrave.ParticleEffectIdx endIdx)
		{
		}

		// Token: 0x06008335 RID: 33589 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChangeMaterialProperty(string meshLabel, string propertyName, float start, float end, float target)
		{
		}

		// Token: 0x06008336 RID: 33590 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAnimaterTrigger(Animator animator, string start, string end, float target = 0.5f)
		{
		}

		// Token: 0x06008337 RID: 33591 RVA: 0x0000216A File Offset: 0x0000036A
		public static BgGrave Create(GameObject res, Transform root, BgUnit.Side side)
		{
			return null;
		}

		// Token: 0x06008338 RID: 33592 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(BgUnit.Side side)
		{
		}

		// Token: 0x06008339 RID: 33593 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600833A RID: 33594 RVA: 0x0000216D File Offset: 0x0000036D
		private void EffectUpdate()
		{
		}

		// Token: 0x0600833B RID: 33595 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBgGrave(int player, int position, bool cardIn)
		{
		}

		// Token: 0x0600833C RID: 33596 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetBgGraveInner(int position, int num, bool cardIn)
		{
		}

		// Token: 0x0600833D RID: 33597 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBgGraveHighlight(bool flg)
		{
		}

		// Token: 0x0600833E RID: 33598 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCursorEnterGrave()
		{
		}

		// Token: 0x0600833F RID: 33599 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCursorExitGrave()
		{
		}

		// Token: 0x06008340 RID: 33600 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnSelectPressedGrave()
		{
		}

		// Token: 0x06008341 RID: 33601 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnSelectReleasedGrave()
		{
		}

		// Token: 0x06008342 RID: 33602 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCardIntoGrave()
		{
		}

		// Token: 0x06008343 RID: 33603 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCardInGrave(bool flg)
		{
		}

		// Token: 0x06008344 RID: 33604 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCardOutGrave(int num)
		{
		}

		// Token: 0x06008345 RID: 33605 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCursorEnterExclude()
		{
		}

		// Token: 0x06008346 RID: 33606 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCursorExitExclude()
		{
		}

		// Token: 0x06008347 RID: 33607 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnSelectPressedExclude()
		{
		}

		// Token: 0x06008348 RID: 33608 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnSelectReleasedExclude()
		{
		}

		// Token: 0x06008349 RID: 33609 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCardIntoExclude()
		{
		}

		// Token: 0x0600834A RID: 33610 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCardInExclude(bool flg)
		{
		}

		// Token: 0x0600834B RID: 33611 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCardOutExclude(int num)
		{
		}

		// Token: 0x0400BE8D RID: 48781
		private const string posGraveLabel = "POS_Grave";

		// Token: 0x0400BE8E RID: 48782
		private const string posExclueLabel = "POS_Exclude";

		// Token: 0x0400BE8F RID: 48783
		private const string meshElementLabel01 = "Material01";

		// Token: 0x0400BE90 RID: 48784
		private const string graveHighlightAnimatorLabelBase = "GraveHighlight";

		// Token: 0x0400BE91 RID: 48785
		private const string excludeHighlightAnimatorLabelBase = "ExcludeHighlight";

		// Token: 0x0400BE92 RID: 48786
		private const string highlightTriggerName = "On";

		// Token: 0x0400BE93 RID: 48787
		private const string graveMouseOverPropertyName = "_GraveMouseOver";

		// Token: 0x0400BE94 RID: 48788
		private const string excludeMouseOverPropertyName = "_ExcludeMouseOver";

		// Token: 0x0400BE95 RID: 48789
		private const string gravePressButtonPropertyName = "_GravePressButton";

		// Token: 0x0400BE96 RID: 48790
		private const string excludePressButtonPropertyName = "_ExcludePressButton";

		// Token: 0x0400BE97 RID: 48791
		private const string graveExistPropertyName = "_GraveCardExist";

		// Token: 0x0400BE98 RID: 48792
		private const string excludeExistPropertyName = "_ExcludeCardExist";

		// Token: 0x0400BE99 RID: 48793
		private const string graveInElementLabel = "GraveIn";

		// Token: 0x0400BE9A RID: 48794
		private const string graveInendElementLabel = "GraveInend";

		// Token: 0x0400BE9B RID: 48795
		private const string graveOutElementLabel = "GraveOut";

		// Token: 0x0400BE9C RID: 48796
		private const string graveOutendElementLabel = "GraveOutend";

		// Token: 0x0400BE9D RID: 48797
		private const string excludeInElementLabel = "ExcludeIn";

		// Token: 0x0400BE9E RID: 48798
		private const string excludeInendElementLabel = "ExcludeInend";

		// Token: 0x0400BE9F RID: 48799
		private const string excludeOutElementLabel = "ExcludeOut";

		// Token: 0x0400BEA0 RID: 48800
		private const string excludeOutendElementLabel = "ExcludeOutend";

		// Token: 0x0400BEA1 RID: 48801
		private const string graveIdleS1ElementLabel = "GraveIdleS1";

		// Token: 0x0400BEA2 RID: 48802
		private const string graveIdleS2ElementLabel = "GraveIdleS2";

		// Token: 0x0400BEA3 RID: 48803
		private const string graveIdleS3ElementLabel = "GraveIdleS3";

		// Token: 0x0400BEA4 RID: 48804
		private const string excludeIdleS1ElementLabel = "ExcludeIdleS1";

		// Token: 0x0400BEA5 RID: 48805
		private const string excludeIdleS2ElementLabel = "ExcludeIdleS2";

		// Token: 0x0400BEA6 RID: 48806
		private const string excludeIdleS3ElementLabel = "ExcludeIdleS3";

		// Token: 0x0400BEA7 RID: 48807
		private const float particleEffectWait = 0.5f;

		// Token: 0x0400BEA8 RID: 48808
		private const float materialUpdateWait = 0.05f;

		// Token: 0x0400BEA9 RID: 48809
		private const float cardExistMaterialUpdateWait = 0.5f;

		// Token: 0x0400BEAA RID: 48810
		private Dictionary<string, Renderer> renderers;

		// Token: 0x0400BEAB RID: 48811
		private Animator excludeHighlightAnimator;

		// Token: 0x0400BEAC RID: 48812
		private Animator graveHighlightAnimator;

		// Token: 0x0400BEAD RID: 48813
		public GameObject posGrave;

		// Token: 0x0400BEAE RID: 48814
		public GameObject posExclude;

		// Token: 0x0400BEAF RID: 48815
		private BgGrave.GraveParticle[] particleEffect;

		// Token: 0x0400BEB0 RID: 48816
		private List<BgGrave.GraveEffect> effectUpdateList;

		// Token: 0x0400BEB1 RID: 48817
		private List<BgGrave.GraveEffect> removeEffectUpdateList;

		// Token: 0x0200113C RID: 4412
		public enum GraveType
		{
			// Token: 0x0400BEB3 RID: 48819
			None,
			// Token: 0x0400BEB4 RID: 48820
			Common,
			// Token: 0x0400BEB5 RID: 48821
			Unique
		}

		// Token: 0x0200113D RID: 4413
		private enum ParticleEffectIdx
		{
			// Token: 0x0400BEB7 RID: 48823
			GraveIn,
			// Token: 0x0400BEB8 RID: 48824
			GraveInend,
			// Token: 0x0400BEB9 RID: 48825
			GraveOut,
			// Token: 0x0400BEBA RID: 48826
			GraveOutend,
			// Token: 0x0400BEBB RID: 48827
			ExcludeIn,
			// Token: 0x0400BEBC RID: 48828
			ExcludeInend,
			// Token: 0x0400BEBD RID: 48829
			ExcludeOut,
			// Token: 0x0400BEBE RID: 48830
			ExcludeOutend,
			// Token: 0x0400BEBF RID: 48831
			GraveIdleS1,
			// Token: 0x0400BEC0 RID: 48832
			GraveIdleS2,
			// Token: 0x0400BEC1 RID: 48833
			GraveIdleS3,
			// Token: 0x0400BEC2 RID: 48834
			ExcludeIdleS1,
			// Token: 0x0400BEC3 RID: 48835
			ExcludeIdleS2,
			// Token: 0x0400BEC4 RID: 48836
			ExcludeIdleS3,
			// Token: 0x0400BEC5 RID: 48837
			Max
		}

		// Token: 0x0200113E RID: 4414
		private class GraveParticle
		{
			// Token: 0x0400BEC6 RID: 48838
			public ParticleSystem effect;

			// Token: 0x0400BEC7 RID: 48839
			public BgEffectSettingInner setting;
		}

		// Token: 0x0200113F RID: 4415
		private class GraveEffect
		{
			// Token: 0x0600834E RID: 33614 RVA: 0x00002739 File Offset: 0x00000939
			public GraveEffect(Action startFunc, Action<float> updateFunc, Action endFunc, float target)
			{
			}

			// Token: 0x0600834F RID: 33615 RVA: 0x0000216D File Offset: 0x0000036D
			public void Update()
			{
			}

			// Token: 0x0400BEC8 RID: 48840
			private Action start;

			// Token: 0x0400BEC9 RID: 48841
			private Action<float> update;

			// Token: 0x0400BECA RID: 48842
			private Action end;

			// Token: 0x0400BECB RID: 48843
			private float targetTime;

			// Token: 0x0400BECC RID: 48844
			private float time;

			// Token: 0x0400BECD RID: 48845
			public string name;

			// Token: 0x0400BECE RID: 48846
			public bool remove;
		}
	}
}
