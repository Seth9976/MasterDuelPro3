using System;
using DG.Tweening.Core;
using DG.Tweening.Plugins;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x02000063 RID: 99
	public static class DOTweenProShortcuts
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00006B12 File Offset: 0x00004D12
		static DOTweenProShortcuts()
		{
			new SpiralPlugin();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006B1C File Offset: 0x00004D1C
		public static Tweener DOSpiral(this Transform target, float duration, Vector3? axis = null, SpiralMode mode = SpiralMode.Expand, float speed = 1f, float frequency = 10f, float depth = 0f, bool snapping = false)
		{
			if (Mathf.Approximately(speed, 0f))
			{
				speed = 1f;
			}
			if (axis != null)
			{
				Vector3? vector = axis;
				Vector3 zero = Vector3.zero;
				if (vector == null || (vector != null && !(vector.GetValueOrDefault() == zero)))
				{
					goto IL_0066;
				}
			}
			axis = new Vector3?(Vector3.forward);
			IL_0066:
			TweenerCore<Vector3, Vector3, SpiralOptions> tweenerCore = DOTween.To<Vector3, Vector3, SpiralOptions>(SpiralPlugin.Get(), () => target.localPosition, delegate(Vector3 x)
			{
				target.localPosition = x;
			}, axis.Value, duration).SetTarget(target);
			tweenerCore.plugOptions.mode = mode;
			tweenerCore.plugOptions.speed = speed;
			tweenerCore.plugOptions.frequency = frequency;
			tweenerCore.plugOptions.depth = depth;
			tweenerCore.plugOptions.snapping = snapping;
			return tweenerCore;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006C04 File Offset: 0x00004E04
		public static Tweener DOSpiral(this Rigidbody target, float duration, Vector3? axis = null, SpiralMode mode = SpiralMode.Expand, float speed = 1f, float frequency = 10f, float depth = 0f, bool snapping = false)
		{
			if (Mathf.Approximately(speed, 0f))
			{
				speed = 1f;
			}
			if (axis != null)
			{
				Vector3? vector = axis;
				Vector3 zero = Vector3.zero;
				if (vector == null || (vector != null && !(vector.GetValueOrDefault() == zero)))
				{
					goto IL_0066;
				}
			}
			axis = new Vector3?(Vector3.forward);
			IL_0066:
			TweenerCore<Vector3, Vector3, SpiralOptions> tweenerCore = DOTween.To<Vector3, Vector3, SpiralOptions>(SpiralPlugin.Get(), () => target.position, new DOSetter<Vector3>(target.MovePosition), axis.Value, duration).SetTarget(target);
			tweenerCore.plugOptions.mode = mode;
			tweenerCore.plugOptions.speed = speed;
			tweenerCore.plugOptions.frequency = frequency;
			tweenerCore.plugOptions.depth = depth;
			tweenerCore.plugOptions.snapping = snapping;
			return tweenerCore;
		}
	}
}
