using System;
using System.Collections;
using UnityEngine;

namespace DG.Tweening.Core
{
	// Token: 0x020000A4 RID: 164
	[AddComponentMenu("")]
	public class DOTweenComponent : MonoBehaviour, IDOTweenInit
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x00010AAC File Offset: 0x0000ECAC
		private void Awake()
		{
			if (!(DOTween.instance == null))
			{
				if (Debugger.logPriority >= 1)
				{
					Debugger.LogWarning("Duplicate DOTweenComponent instance found in scene: destroying it", null);
				}
				Object.Destroy(base.gameObject);
				return;
			}
			DOTween.instance = this;
			this.inspectorUpdater = 0;
			this._unscaledTime = Time.realtimeSinceStartup;
			Type looseScriptType = DOTweenUtils.GetLooseScriptType("DG.Tweening.DOTweenModuleUtils");
			if (looseScriptType == null)
			{
				Debugger.LogError("Couldn't load Modules system", null);
				return;
			}
			looseScriptType.GetMethod("Init", 24).Invoke(null, null);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00010B2E File Offset: 0x0000ED2E
		private void Start()
		{
			if (DOTween.instance != this)
			{
				this._duplicateToDestroy = true;
				Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00010B50 File Offset: 0x0000ED50
		private void Update()
		{
			this._unscaledDeltaTime = Time.realtimeSinceStartup - this._unscaledTime;
			if (DOTween.useSmoothDeltaTime && this._unscaledDeltaTime > DOTween.maxSmoothUnscaledTime)
			{
				this._unscaledDeltaTime = DOTween.maxSmoothUnscaledTime;
			}
			if (TweenManager.hasActiveDefaultTweens)
			{
				TweenManager.Update(UpdateType.Normal, (DOTween.useSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) * DOTween.timeScale, this._unscaledDeltaTime * DOTween.unscaledTimeScale * DOTween.timeScale);
			}
			this._unscaledTime = Time.realtimeSinceStartup;
			if (TweenManager.isUnityEditor)
			{
				this.inspectorUpdater++;
				if (DOTween.showUnityEditorReport && TweenManager.hasActiveTweens)
				{
					if (TweenManager.totActiveTweeners > DOTween.maxActiveTweenersReached)
					{
						DOTween.maxActiveTweenersReached = TweenManager.totActiveTweeners;
					}
					if (TweenManager.totActiveSequences > DOTween.maxActiveSequencesReached)
					{
						DOTween.maxActiveSequencesReached = TweenManager.totActiveSequences;
					}
				}
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00010C20 File Offset: 0x0000EE20
		private void LateUpdate()
		{
			if (TweenManager.hasActiveLateTweens)
			{
				TweenManager.Update(UpdateType.Late, (DOTween.useSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) * DOTween.timeScale, this._unscaledDeltaTime * DOTween.unscaledTimeScale * DOTween.timeScale);
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00010C5C File Offset: 0x0000EE5C
		private void FixedUpdate()
		{
			if (TweenManager.hasActiveFixedTweens && Time.timeScale > 0f)
			{
				TweenManager.Update(UpdateType.Fixed, (DOTween.useSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) * DOTween.timeScale, (DOTween.useSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) / Time.timeScale * DOTween.unscaledTimeScale * DOTween.timeScale);
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00010CC0 File Offset: 0x0000EEC0
		private void OnDrawGizmos()
		{
			if (!DOTween.drawGizmos || !TweenManager.isUnityEditor)
			{
				return;
			}
			int count = DOTween.GizmosDelegates.Count;
			if (count == 0)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				DOTween.GizmosDelegates[i]();
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00010D08 File Offset: 0x0000EF08
		private void OnDestroy()
		{
			if (this._duplicateToDestroy)
			{
				return;
			}
			if (DOTween.showUnityEditorReport)
			{
				Debugger.LogReport("Max overall simultaneous active Tweeners/Sequences: " + DOTween.maxActiveTweenersReached.ToString() + "/" + DOTween.maxActiveSequencesReached.ToString());
			}
			if (DOTween.useSafeMode)
			{
				int totErrors = DOTween.safeModeReport.GetTotErrors();
				if (totErrors > 0)
				{
					string text = string.Format("DOTween's safe mode captured {0} errors. This is usually ok (it's what safe mode is there for) but if your game is encountering issues you should set Log Behaviour to Default in DOTween Utility Panel in order to get detailed warnings when an error is captured (consider that these errors are always on the user side).", totErrors);
					if (DOTween.safeModeReport.totMissingTargetOrFieldErrors > 0)
					{
						text = text + "\n- " + DOTween.safeModeReport.totMissingTargetOrFieldErrors.ToString() + " missing target or field errors";
					}
					if (DOTween.safeModeReport.totStartupErrors > 0)
					{
						text = text + "\n- " + DOTween.safeModeReport.totStartupErrors.ToString() + " startup errors";
					}
					if (DOTween.safeModeReport.totCallbackErrors > 0)
					{
						text = text + "\n- " + DOTween.safeModeReport.totCallbackErrors.ToString() + " errors inside callbacks (these might be important)";
					}
					if (DOTween.safeModeReport.totUnsetErrors > 0)
					{
						text = text + "\n- " + DOTween.safeModeReport.totUnsetErrors.ToString() + " undetermined errors (these might be important)";
					}
					Debugger.LogSafeModeReport(text);
				}
			}
			if (DOTween.instance == this)
			{
				DOTween.instance = null;
			}
			DOTween.Clear(true, this._isQuitting);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00010E5F File Offset: 0x0000F05F
		public void OnApplicationPause(bool pauseStatus)
		{
			if (pauseStatus)
			{
				this._paused = true;
				this._pausedTime = Time.realtimeSinceStartup;
				return;
			}
			if (this._paused)
			{
				this._paused = false;
				this._unscaledTime += Time.realtimeSinceStartup - this._pausedTime;
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00010E9F File Offset: 0x0000F09F
		private void OnApplicationQuit()
		{
			this._isQuitting = true;
			DOTween.isQuitting = true;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00010EAE File Offset: 0x0000F0AE
		public IDOTweenInit SetCapacity(int tweenersCapacity, int sequencesCapacity)
		{
			TweenManager.SetCapacities(tweenersCapacity, sequencesCapacity);
			return this;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00010EB8 File Offset: 0x0000F0B8
		internal IEnumerator WaitForCompletion(Tween t)
		{
			while (t.active && !t.isComplete)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00010EC7 File Offset: 0x0000F0C7
		internal IEnumerator WaitForRewind(Tween t)
		{
			while (t.active && (!t.playedOnce || t.position * (float)(t.completedLoops + 1) > 0f))
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00010ED6 File Offset: 0x0000F0D6
		internal IEnumerator WaitForKill(Tween t)
		{
			while (t.active)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00010EE5 File Offset: 0x0000F0E5
		internal IEnumerator WaitForElapsedLoops(Tween t, int elapsedLoops)
		{
			while (t.active && t.completedLoops < elapsedLoops)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00010EFB File Offset: 0x0000F0FB
		internal IEnumerator WaitForPosition(Tween t, float position)
		{
			while (t.active && t.position * (float)(t.completedLoops + 1) < position)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00010F11 File Offset: 0x0000F111
		internal IEnumerator WaitForStart(Tween t)
		{
			while (t.active && !t.playedOnce)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00010F20 File Offset: 0x0000F120
		internal static void Create()
		{
			if (DOTween.instance != null)
			{
				return;
			}
			GameObject gameObject = new GameObject("[DOTween]");
			Object.DontDestroyOnLoad(gameObject);
			DOTween.instance = gameObject.AddComponent<DOTweenComponent>();
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00010F4A File Offset: 0x0000F14A
		internal static void DestroyInstance()
		{
			if (DOTween.instance != null)
			{
				Object.Destroy(DOTween.instance.gameObject);
			}
			DOTween.instance = null;
		}

		// Token: 0x040001CA RID: 458
		public int inspectorUpdater;

		// Token: 0x040001CB RID: 459
		private float _unscaledTime;

		// Token: 0x040001CC RID: 460
		private float _unscaledDeltaTime;

		// Token: 0x040001CD RID: 461
		private bool _paused;

		// Token: 0x040001CE RID: 462
		private float _pausedTime;

		// Token: 0x040001CF RID: 463
		private bool _isQuitting;

		// Token: 0x040001D0 RID: 464
		private bool _duplicateToDestroy;
	}
}
