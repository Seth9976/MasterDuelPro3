using System;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Scripting;

namespace DG.Tweening
{
	// Token: 0x0200005D RID: 93
	public static class DOTweenModuleUtils
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00005624 File Offset: 0x00003824
		[Preserve]
		public static void Init()
		{
			if (DOTweenModuleUtils._initialized)
			{
				return;
			}
			DOTweenModuleUtils._initialized = true;
			DOTweenExternalCommand.SetOrientationOnPath += DOTweenModuleUtils.Physics.SetOrientationOnPath;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005645 File Offset: 0x00003845
		[Preserve]
		private static void Preserver()
		{
			AppDomain.CurrentDomain.GetAssemblies();
			typeof(MonoBehaviour).GetMethod("Stub");
		}

		// Token: 0x0400008C RID: 140
		private static bool _initialized;

		// Token: 0x0200005E RID: 94
		public static class Physics
		{
			// Token: 0x0600014D RID: 333 RVA: 0x00005667 File Offset: 0x00003867
			public static void SetOrientationOnPath(PathOptions options, Tween t, Quaternion newRot, Transform trans)
			{
				if (options.isRigidbody)
				{
					((Rigidbody)t.target).rotation = newRot;
					return;
				}
				trans.rotation = newRot;
			}

			// Token: 0x0600014E RID: 334 RVA: 0x0000568A File Offset: 0x0000388A
			public static bool HasRigidbody2D(Component target)
			{
				return target.GetComponent<Rigidbody2D>() != null;
			}

			// Token: 0x0600014F RID: 335 RVA: 0x00005698 File Offset: 0x00003898
			[Preserve]
			public static bool HasRigidbody(Component target)
			{
				return target.GetComponent<Rigidbody>() != null;
			}

			// Token: 0x06000150 RID: 336 RVA: 0x000056A8 File Offset: 0x000038A8
			[Preserve]
			public static TweenerCore<Vector3, Path, PathOptions> CreateDOTweenPathTween(MonoBehaviour target, bool tweenRigidbody, bool isLocal, Path path, float duration, PathMode pathMode)
			{
				TweenerCore<Vector3, Path, PathOptions> t = null;
				bool rBodyFoundAndTweened = false;
				if (tweenRigidbody)
				{
					Rigidbody rBody = target.GetComponent<Rigidbody>();
					if (rBody != null)
					{
						rBodyFoundAndTweened = true;
						t = (isLocal ? rBody.DOLocalPath(path, duration, pathMode) : rBody.DOPath(path, duration, pathMode));
					}
				}
				if (!rBodyFoundAndTweened && tweenRigidbody)
				{
					Rigidbody2D rBody2D = target.GetComponent<Rigidbody2D>();
					if (rBody2D != null)
					{
						rBodyFoundAndTweened = true;
						t = (isLocal ? rBody2D.DOLocalPath(path, duration, pathMode) : rBody2D.DOPath(path, duration, pathMode));
					}
				}
				if (!rBodyFoundAndTweened)
				{
					t = (isLocal ? target.transform.DOLocalPath(path, duration, pathMode) : target.transform.DOPath(path, duration, pathMode));
				}
				return t;
			}
		}
	}
}
