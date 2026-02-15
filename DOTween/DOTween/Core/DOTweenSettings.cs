using System;
using DG.Tweening.Core.Enums;
using UnityEngine;

namespace DG.Tweening.Core
{
	// Token: 0x020000AB RID: 171
	public class DOTweenSettings : ScriptableObject
	{
		// Token: 0x040001E5 RID: 485
		public const string AssetName = "DOTweenSettings";

		// Token: 0x040001E6 RID: 486
		public const string AssetFullFilename = "DOTweenSettings.asset";

		// Token: 0x040001E7 RID: 487
		public bool useSafeMode = true;

		// Token: 0x040001E8 RID: 488
		public DOTweenSettings.SafeModeOptions safeModeOptions = new DOTweenSettings.SafeModeOptions();

		// Token: 0x040001E9 RID: 489
		public float timeScale = 1f;

		// Token: 0x040001EA RID: 490
		public float unscaledTimeScale = 1f;

		// Token: 0x040001EB RID: 491
		public bool useSmoothDeltaTime;

		// Token: 0x040001EC RID: 492
		public float maxSmoothUnscaledTime = 0.15f;

		// Token: 0x040001ED RID: 493
		public RewindCallbackMode rewindCallbackMode;

		// Token: 0x040001EE RID: 494
		public bool showUnityEditorReport;

		// Token: 0x040001EF RID: 495
		public LogBehaviour logBehaviour;

		// Token: 0x040001F0 RID: 496
		public bool drawGizmos = true;

		// Token: 0x040001F1 RID: 497
		public bool defaultRecyclable;

		// Token: 0x040001F2 RID: 498
		public AutoPlay defaultAutoPlay = AutoPlay.All;

		// Token: 0x040001F3 RID: 499
		public UpdateType defaultUpdateType;

		// Token: 0x040001F4 RID: 500
		public bool defaultTimeScaleIndependent;

		// Token: 0x040001F5 RID: 501
		public Ease defaultEaseType = Ease.OutQuad;

		// Token: 0x040001F6 RID: 502
		public float defaultEaseOvershootOrAmplitude = 1.70158f;

		// Token: 0x040001F7 RID: 503
		public float defaultEasePeriod;

		// Token: 0x040001F8 RID: 504
		public bool defaultAutoKill = true;

		// Token: 0x040001F9 RID: 505
		public LoopType defaultLoopType;

		// Token: 0x040001FA RID: 506
		public bool debugMode;

		// Token: 0x040001FB RID: 507
		public bool debugStoreTargetId = true;

		// Token: 0x040001FC RID: 508
		public bool showPreviewPanel = true;

		// Token: 0x040001FD RID: 509
		public DOTweenSettings.SettingsLocation storeSettingsLocation;

		// Token: 0x040001FE RID: 510
		public DOTweenSettings.ModulesSetup modules = new DOTweenSettings.ModulesSetup();

		// Token: 0x040001FF RID: 511
		public bool createASMDEF;

		// Token: 0x04000200 RID: 512
		public bool showPlayingTweens;

		// Token: 0x04000201 RID: 513
		public bool showPausedTweens;

		// Token: 0x020000AC RID: 172
		public enum SettingsLocation
		{
			// Token: 0x04000203 RID: 515
			AssetsDirectory,
			// Token: 0x04000204 RID: 516
			DOTweenDirectory,
			// Token: 0x04000205 RID: 517
			DemigiantDirectory
		}

		// Token: 0x020000AD RID: 173
		[Serializable]
		public class SafeModeOptions
		{
			// Token: 0x04000206 RID: 518
			public SafeModeLogBehaviour logBehaviour = SafeModeLogBehaviour.Warning;

			// Token: 0x04000207 RID: 519
			public NestedTweenFailureBehaviour nestedTweenFailureBehaviour;
		}

		// Token: 0x020000AE RID: 174
		[Serializable]
		public class ModulesSetup
		{
			// Token: 0x04000208 RID: 520
			public bool showPanel;

			// Token: 0x04000209 RID: 521
			public bool audioEnabled = true;

			// Token: 0x0400020A RID: 522
			public bool physicsEnabled = true;

			// Token: 0x0400020B RID: 523
			public bool physics2DEnabled = true;

			// Token: 0x0400020C RID: 524
			public bool spriteEnabled = true;

			// Token: 0x0400020D RID: 525
			public bool uiEnabled = true;

			// Token: 0x0400020E RID: 526
			public bool textMeshProEnabled;

			// Token: 0x0400020F RID: 527
			public bool tk2DEnabled;

			// Token: 0x04000210 RID: 528
			public bool deAudioEnabled;

			// Token: 0x04000211 RID: 529
			public bool deUnityExtendedEnabled;

			// Token: 0x04000212 RID: 530
			public bool epoOutlineEnabled;
		}
	}
}
