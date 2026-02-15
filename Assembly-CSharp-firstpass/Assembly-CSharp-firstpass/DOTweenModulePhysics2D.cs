using System;
using DG.Tweening.Core;
using DG.Tweening.Plugins;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x02000014 RID: 20
	public static class DOTweenModulePhysics2D
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002A34 File Offset: 0x00000C34
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOMove(this Rigidbody2D target, Vector2 endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.position, new DOSetter<Vector2>(target.MovePosition), endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A88 File Offset: 0x00000C88
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOMoveX(this Rigidbody2D target, float endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.position, new DOSetter<Vector2>(target.MovePosition), new Vector2(endValue, 0f), duration);
			tweenerCore.SetOptions(AxisConstraint.X, snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOMoveY(this Rigidbody2D target, float endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.position, new DOSetter<Vector2>(target.MovePosition), new Vector2(0f, endValue), duration);
			tweenerCore.SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B40 File Offset: 0x00000D40
		public static TweenerCore<float, float, FloatOptions> DORotate(this Rigidbody2D target, float endValue, float duration)
		{
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.rotation, new DOSetter<float>(target.MoveRotation), endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B8C File Offset: 0x00000D8C
		public static Sequence DOJump(this Rigidbody2D target, Vector2 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = 0f;
			float offsetY = -1f;
			bool offsetYSet = false;
			Sequence s = DOTween.Sequence();
			Tween yTween = DOTween.To(() => target.position, delegate(Vector2 x)
			{
				target.position = x;
			}, new Vector2(0f, jumpPower), duration / (float)(numJumps * 2)).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad)
				.SetRelative<Tweener>()
				.SetLoops(numJumps * 2, LoopType.Yoyo)
				.OnStart(delegate
				{
					startPosY = target.position.y;
				});
			s.Append(DOTween.To(() => target.position, delegate(Vector2 x)
			{
				target.position = x;
			}, new Vector2(endValue.x, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)).Join(yTween).SetTarget(target)
				.SetEase(DOTween.defaultEaseType);
			yTween.OnUpdate(delegate
			{
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = (s.isRelative ? endValue.y : (endValue.y - startPosY));
				}
				Vector3 pos = target.position;
				pos.y += DOVirtual.EasedValue(0f, offsetY, yTween.ElapsedPercentage(true), Ease.OutQuad);
				target.MovePosition(pos);
			});
			return s;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002CCC File Offset: 0x00000ECC
		public static TweenerCore<Vector3, Path, PathOptions> DOPath(this Rigidbody2D target, Vector2[] path, float duration, PathType pathType = PathType.Linear, PathMode pathMode = PathMode.Full3D, int resolution = 10, Color? gizmoColor = null)
		{
			if (resolution < 1)
			{
				resolution = 1;
			}
			int len = path.Length;
			Vector3[] path3D = new Vector3[len];
			for (int i = 0; i < len; i++)
			{
				path3D[i] = path[i];
			}
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => target.position, delegate(Vector3 x)
			{
				target.MovePosition(x);
			}, new Path(pathType, path3D, resolution, gizmoColor), duration).SetTarget(target).SetUpdate(UpdateType.Fixed);
			tweenerCore.plugOptions.isRigidbody2D = true;
			tweenerCore.plugOptions.mode = pathMode;
			return tweenerCore;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D70 File Offset: 0x00000F70
		public static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(this Rigidbody2D target, Vector2[] path, float duration, PathType pathType = PathType.Linear, PathMode pathMode = PathMode.Full3D, int resolution = 10, Color? gizmoColor = null)
		{
			if (resolution < 1)
			{
				resolution = 1;
			}
			int len = path.Length;
			Vector3[] path3D = new Vector3[len];
			for (int i = 0; i < len; i++)
			{
				path3D[i] = path[i];
			}
			Transform trans = target.transform;
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => trans.localPosition, delegate(Vector3 x)
			{
				target.MovePosition((trans.parent == null) ? x : trans.parent.TransformPoint(x));
			}, new Path(pathType, path3D, resolution, gizmoColor), duration).SetTarget(target).SetUpdate(UpdateType.Fixed);
			tweenerCore.plugOptions.isRigidbody2D = true;
			tweenerCore.plugOptions.mode = pathMode;
			tweenerCore.plugOptions.useLocalPosition = true;
			return tweenerCore;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E34 File Offset: 0x00001034
		internal static TweenerCore<Vector3, Path, PathOptions> DOPath(this Rigidbody2D target, Path path, float duration, PathMode pathMode = PathMode.Full3D)
		{
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => target.position, delegate(Vector3 x)
			{
				target.MovePosition(x);
			}, path, duration).SetTarget(target);
			tweenerCore.plugOptions.isRigidbody2D = true;
			tweenerCore.plugOptions.mode = pathMode;
			return tweenerCore;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E98 File Offset: 0x00001098
		internal static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(this Rigidbody2D target, Path path, float duration, PathMode pathMode = PathMode.Full3D)
		{
			Transform trans = target.transform;
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => trans.localPosition, delegate(Vector3 x)
			{
				target.MovePosition((trans.parent == null) ? x : trans.parent.TransformPoint(x));
			}, path, duration).SetTarget(target);
			tweenerCore.plugOptions.isRigidbody2D = true;
			tweenerCore.plugOptions.mode = pathMode;
			tweenerCore.plugOptions.useLocalPosition = true;
			return tweenerCore;
		}
	}
}
