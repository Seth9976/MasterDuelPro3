using System;
using System.Threading.Tasks;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x0200004D RID: 77
	public static class DOTweenModuleUnityVersion
	{
		// Token: 0x0600011D RID: 285 RVA: 0x0000498C File Offset: 0x00002B8C
		public static Sequence DOGradientColor(this Material target, Gradient gradient, float duration)
		{
			Sequence s = DOTween.Sequence();
			GradientColorKey[] colors = gradient.colorKeys;
			int len = colors.Length;
			for (int i = 0; i < len; i++)
			{
				GradientColorKey c = colors[i];
				if (i == 0 && c.time <= 0f)
				{
					target.color = c.color;
				}
				else
				{
					float colorDuration = ((i == len - 1) ? (duration - s.Duration(false)) : (duration * ((i == 0) ? c.time : (c.time - colors[i - 1].time))));
					s.Append(target.DOColor(c.color, colorDuration).SetEase(Ease.Linear));
				}
			}
			s.SetTarget(target);
			return s;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004A44 File Offset: 0x00002C44
		public static Sequence DOGradientColor(this Material target, Gradient gradient, string property, float duration)
		{
			Sequence s = DOTween.Sequence();
			GradientColorKey[] colors = gradient.colorKeys;
			int len = colors.Length;
			for (int i = 0; i < len; i++)
			{
				GradientColorKey c = colors[i];
				if (i == 0 && c.time <= 0f)
				{
					target.SetColor(property, c.color);
				}
				else
				{
					float colorDuration = ((i == len - 1) ? (duration - s.Duration(false)) : (duration * ((i == 0) ? c.time : (c.time - colors[i - 1].time))));
					s.Append(target.DOColor(c.color, property, colorDuration).SetEase(Ease.Linear));
				}
			}
			s.SetTarget(target);
			return s;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004AFB File Offset: 0x00002CFB
		public static CustomYieldInstruction WaitForCompletion(this Tween t, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new DOTweenCYInstruction.WaitForCompletion(t);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004B1B File Offset: 0x00002D1B
		public static CustomYieldInstruction WaitForRewind(this Tween t, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new DOTweenCYInstruction.WaitForRewind(t);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004B3B File Offset: 0x00002D3B
		public static CustomYieldInstruction WaitForKill(this Tween t, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new DOTweenCYInstruction.WaitForKill(t);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004B5B File Offset: 0x00002D5B
		public static CustomYieldInstruction WaitForElapsedLoops(this Tween t, int elapsedLoops, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new DOTweenCYInstruction.WaitForElapsedLoops(t, elapsedLoops);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004B7C File Offset: 0x00002D7C
		public static CustomYieldInstruction WaitForPosition(this Tween t, float position, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new DOTweenCYInstruction.WaitForPosition(t, position);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004B9D File Offset: 0x00002D9D
		public static CustomYieldInstruction WaitForStart(this Tween t, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new DOTweenCYInstruction.WaitForStart(t);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004BC0 File Offset: 0x00002DC0
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOOffset(this Material target, Vector2 endValue, int propertyID, float duration)
		{
			if (!target.HasProperty(propertyID))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(propertyID);
				}
				return null;
			}
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.GetTextureOffset(propertyID), delegate(Vector2 x)
			{
				target.SetTextureOffset(propertyID, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004C38 File Offset: 0x00002E38
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOTiling(this Material target, Vector2 endValue, int propertyID, float duration)
		{
			if (!target.HasProperty(propertyID))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(propertyID);
				}
				return null;
			}
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.GetTextureScale(propertyID), delegate(Vector2 x)
			{
				target.SetTextureScale(propertyID, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004CB0 File Offset: 0x00002EB0
		public static async Task AsyncWaitForCompletion(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else
			{
				while (t.active && !t.IsComplete())
				{
					await Task.Yield();
				}
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004CF4 File Offset: 0x00002EF4
		public static async Task AsyncWaitForRewind(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else
			{
				while (t.active && (!t.playedOnce || t.position * (float)(t.CompletedLoops() + 1) > 0f))
				{
					await Task.Yield();
				}
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004D38 File Offset: 0x00002F38
		public static async Task AsyncWaitForKill(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else
			{
				while (t.active)
				{
					await Task.Yield();
				}
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004D7C File Offset: 0x00002F7C
		public static async Task AsyncWaitForElapsedLoops(this Tween t, int elapsedLoops)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else
			{
				while (t.active && t.CompletedLoops() < elapsedLoops)
				{
					await Task.Yield();
				}
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004DC8 File Offset: 0x00002FC8
		public static async Task AsyncWaitForPosition(this Tween t, float position)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else
			{
				while (t.active && t.position * (float)(t.CompletedLoops() + 1) < position)
				{
					await Task.Yield();
				}
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004E14 File Offset: 0x00003014
		public static async Task AsyncWaitForStart(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else
			{
				while (t.active && !t.playedOnce)
				{
					await Task.Yield();
				}
			}
		}
	}
}
