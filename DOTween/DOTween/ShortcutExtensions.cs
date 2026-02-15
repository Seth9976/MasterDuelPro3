using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.CustomPlugins;
using DG.Tweening.Plugins;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x0200001E RID: 30
	public static class ShortcutExtensions
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00004D1C File Offset: 0x00002F1C
		public static TweenerCore<float, float, FloatOptions> DOAspect(this Camera target, float endValue, float duration)
		{
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.aspect, delegate(float x)
			{
				target.aspect = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004D64 File Offset: 0x00002F64
		public static TweenerCore<Color, Color, ColorOptions> DOColor(this Camera target, Color endValue, float duration)
		{
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.To(() => target.backgroundColor, delegate(Color x)
			{
				target.backgroundColor = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004DAC File Offset: 0x00002FAC
		public static TweenerCore<float, float, FloatOptions> DOFarClipPlane(this Camera target, float endValue, float duration)
		{
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.farClipPlane, delegate(float x)
			{
				target.farClipPlane = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004DF4 File Offset: 0x00002FF4
		public static TweenerCore<float, float, FloatOptions> DOFieldOfView(this Camera target, float endValue, float duration)
		{
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.fieldOfView, delegate(float x)
			{
				target.fieldOfView = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004E3C File Offset: 0x0000303C
		public static TweenerCore<float, float, FloatOptions> DONearClipPlane(this Camera target, float endValue, float duration)
		{
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.nearClipPlane, delegate(float x)
			{
				target.nearClipPlane = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004E84 File Offset: 0x00003084
		public static TweenerCore<float, float, FloatOptions> DOOrthoSize(this Camera target, float endValue, float duration)
		{
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.orthographicSize, delegate(float x)
			{
				target.orthographicSize = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004ECC File Offset: 0x000030CC
		public static TweenerCore<Rect, Rect, RectOptions> DOPixelRect(this Camera target, Rect endValue, float duration)
		{
			TweenerCore<Rect, Rect, RectOptions> tweenerCore = DOTween.To(() => target.pixelRect, delegate(Rect x)
			{
				target.pixelRect = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004F14 File Offset: 0x00003114
		public static TweenerCore<Rect, Rect, RectOptions> DORect(this Camera target, Rect endValue, float duration)
		{
			TweenerCore<Rect, Rect, RectOptions> tweenerCore = DOTween.To(() => target.rect, delegate(Rect x)
			{
				target.rect = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004F5C File Offset: 0x0000315C
		public static Tweener DOShakePosition(this Camera target, float duration, float strength = 3f, int vibrato = 10, float randomness = 90f, bool fadeOut = true, ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (Debugger.logPriority > 0)
				{
					Debug.LogWarning("DOShakePosition: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return DOTween.Shake(() => target.transform.localPosition, delegate(Vector3 x)
			{
				target.transform.localPosition = x;
			}, duration, strength, vibrato, randomness, true, fadeOut, randomnessMode).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetCameraShakePosition);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004FCC File Offset: 0x000031CC
		public static Tweener DOShakePosition(this Camera target, float duration, Vector3 strength, int vibrato = 10, float randomness = 90f, bool fadeOut = true, ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (Debugger.logPriority > 0)
				{
					Debug.LogWarning("DOShakePosition: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return DOTween.Shake(() => target.transform.localPosition, delegate(Vector3 x)
			{
				target.transform.localPosition = x;
			}, duration, strength, vibrato, randomness, fadeOut, randomnessMode).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetCameraShakePosition);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000503C File Offset: 0x0000323C
		public static Tweener DOShakeRotation(this Camera target, float duration, float strength = 90f, int vibrato = 10, float randomness = 90f, bool fadeOut = true, ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (Debugger.logPriority > 0)
				{
					Debug.LogWarning("DOShakeRotation: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return DOTween.Shake(() => target.transform.localEulerAngles, delegate(Vector3 x)
			{
				target.transform.localRotation = Quaternion.Euler(x);
			}, duration, strength, vibrato, randomness, false, fadeOut, randomnessMode).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000050AC File Offset: 0x000032AC
		public static Tweener DOShakeRotation(this Camera target, float duration, Vector3 strength, int vibrato = 10, float randomness = 90f, bool fadeOut = true, ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (Debugger.logPriority > 0)
				{
					Debug.LogWarning("DOShakeRotation: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return DOTween.Shake(() => target.transform.localEulerAngles, delegate(Vector3 x)
			{
				target.transform.localRotation = Quaternion.Euler(x);
			}, duration, strength, vibrato, randomness, fadeOut, randomnessMode).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000511C File Offset: 0x0000331C
		public static TweenerCore<Color, Color, ColorOptions> DOColor(this Light target, Color endValue, float duration)
		{
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005164 File Offset: 0x00003364
		public static TweenerCore<float, float, FloatOptions> DOIntensity(this Light target, float endValue, float duration)
		{
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.intensity, delegate(float x)
			{
				target.intensity = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000051AC File Offset: 0x000033AC
		public static TweenerCore<float, float, FloatOptions> DOShadowStrength(this Light target, float endValue, float duration)
		{
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.shadowStrength, delegate(float x)
			{
				target.shadowStrength = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000051F4 File Offset: 0x000033F4
		public static Tweener DOColor(this LineRenderer target, Color2 startValue, Color2 endValue, float duration)
		{
			return DOTween.To(() => startValue, delegate(Color2 x)
			{
				target.SetColors(x.ca, x.cb);
			}, endValue, duration).SetTarget(target);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005240 File Offset: 0x00003440
		public static TweenerCore<Color, Color, ColorOptions> DOColor(this Material target, Color endValue, float duration)
		{
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005288 File Offset: 0x00003488
		public static TweenerCore<Color, Color, ColorOptions> DOColor(this Material target, Color endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.To(() => target.GetColor(property), delegate(Color x)
			{
				target.SetColor(property, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005300 File Offset: 0x00003500
		public static TweenerCore<Color, Color, ColorOptions> DOColor(this Material target, Color endValue, int propertyID, float duration)
		{
			if (!target.HasProperty(propertyID))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(propertyID);
				}
				return null;
			}
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.To(() => target.GetColor(propertyID), delegate(Color x)
			{
				target.SetColor(propertyID, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005378 File Offset: 0x00003578
		public static TweenerCore<Color, Color, ColorOptions> DOFade(this Material target, float endValue, float duration)
		{
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.ToAlpha(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000053C0 File Offset: 0x000035C0
		public static TweenerCore<Color, Color, ColorOptions> DOFade(this Material target, float endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.ToAlpha(() => target.GetColor(property), delegate(Color x)
			{
				target.SetColor(property, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005438 File Offset: 0x00003638
		public static TweenerCore<Color, Color, ColorOptions> DOFade(this Material target, float endValue, int propertyID, float duration)
		{
			if (!target.HasProperty(propertyID))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(propertyID);
				}
				return null;
			}
			TweenerCore<Color, Color, ColorOptions> tweenerCore = DOTween.ToAlpha(() => target.GetColor(propertyID), delegate(Color x)
			{
				target.SetColor(propertyID, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000054B0 File Offset: 0x000036B0
		public static TweenerCore<float, float, FloatOptions> DOFloat(this Material target, float endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.GetFloat(property), delegate(float x)
			{
				target.SetFloat(property, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005528 File Offset: 0x00003728
		public static TweenerCore<float, float, FloatOptions> DOFloat(this Material target, float endValue, int propertyID, float duration)
		{
			if (!target.HasProperty(propertyID))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(propertyID);
				}
				return null;
			}
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.GetFloat(propertyID), delegate(float x)
			{
				target.SetFloat(propertyID, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000055A0 File Offset: 0x000037A0
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOOffset(this Material target, Vector2 endValue, float duration)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.mainTextureOffset, delegate(Vector2 x)
			{
				target.mainTextureOffset = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000055E8 File Offset: 0x000037E8
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOOffset(this Material target, Vector2 endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.GetTextureOffset(property), delegate(Vector2 x)
			{
				target.SetTextureOffset(property, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005660 File Offset: 0x00003860
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOTiling(this Material target, Vector2 endValue, float duration)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.mainTextureScale, delegate(Vector2 x)
			{
				target.mainTextureScale = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000056A8 File Offset: 0x000038A8
		public static TweenerCore<Vector2, Vector2, VectorOptions> DOTiling(this Material target, Vector2 endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			TweenerCore<Vector2, Vector2, VectorOptions> tweenerCore = DOTween.To(() => target.GetTextureScale(property), delegate(Vector2 x)
			{
				target.SetTextureScale(property, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005720 File Offset: 0x00003920
		public static TweenerCore<Vector4, Vector4, VectorOptions> DOVector(this Material target, Vector4 endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			TweenerCore<Vector4, Vector4, VectorOptions> tweenerCore = DOTween.To(() => target.GetVector(property), delegate(Vector4 x)
			{
				target.SetVector(property, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005798 File Offset: 0x00003998
		public static TweenerCore<Vector4, Vector4, VectorOptions> DOVector(this Material target, Vector4 endValue, int propertyID, float duration)
		{
			if (!target.HasProperty(propertyID))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(propertyID);
				}
				return null;
			}
			TweenerCore<Vector4, Vector4, VectorOptions> tweenerCore = DOTween.To(() => target.GetVector(propertyID), delegate(Vector4 x)
			{
				target.SetVector(propertyID, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005810 File Offset: 0x00003A10
		public static Tweener DOResize(this TrailRenderer target, float toStartWidth, float toEndWidth, float duration)
		{
			return DOTween.To(() => new Vector2(target.startWidth, target.endWidth), delegate(Vector2 x)
			{
				target.startWidth = x.x;
				target.endWidth = x.y;
			}, new Vector2(toStartWidth, toEndWidth), duration).SetTarget(target);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000585C File Offset: 0x00003A5C
		public static TweenerCore<float, float, FloatOptions> DOTime(this TrailRenderer target, float endValue, float duration)
		{
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.time, delegate(float x)
			{
				target.time = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000058A4 File Offset: 0x00003AA4
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOMove(this Transform target, Vector3 endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.position, delegate(Vector3 x)
			{
				target.position = x;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000058F0 File Offset: 0x00003AF0
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOMoveX(this Transform target, float endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.position, delegate(Vector3 x)
			{
				target.position = x;
			}, new Vector3(endValue, 0f, 0f), duration);
			tweenerCore.SetOptions(AxisConstraint.X, snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000594C File Offset: 0x00003B4C
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOMoveY(this Transform target, float endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.position, delegate(Vector3 x)
			{
				target.position = x;
			}, new Vector3(0f, endValue, 0f), duration);
			tweenerCore.SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000059A8 File Offset: 0x00003BA8
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOMoveZ(this Transform target, float endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.position, delegate(Vector3 x)
			{
				target.position = x;
			}, new Vector3(0f, 0f, endValue), duration);
			tweenerCore.SetOptions(AxisConstraint.Z, snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005A04 File Offset: 0x00003C04
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOLocalMove(this Transform target, Vector3 endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.localPosition, delegate(Vector3 x)
			{
				target.localPosition = x;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005A50 File Offset: 0x00003C50
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOLocalMoveX(this Transform target, float endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.localPosition, delegate(Vector3 x)
			{
				target.localPosition = x;
			}, new Vector3(endValue, 0f, 0f), duration);
			tweenerCore.SetOptions(AxisConstraint.X, snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005AAC File Offset: 0x00003CAC
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOLocalMoveY(this Transform target, float endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.localPosition, delegate(Vector3 x)
			{
				target.localPosition = x;
			}, new Vector3(0f, endValue, 0f), duration);
			tweenerCore.SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005B08 File Offset: 0x00003D08
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOLocalMoveZ(this Transform target, float endValue, float duration, bool snapping = false)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.localPosition, delegate(Vector3 x)
			{
				target.localPosition = x;
			}, new Vector3(0f, 0f, endValue), duration);
			tweenerCore.SetOptions(AxisConstraint.Z, snapping).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005B64 File Offset: 0x00003D64
		public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DORotate(this Transform target, Vector3 endValue, float duration, RotateMode mode = RotateMode.Fast)
		{
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tweenerCore = DOTween.To(() => target.rotation, delegate(Quaternion x)
			{
				target.rotation = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			tweenerCore.plugOptions.rotateMode = mode;
			return tweenerCore;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005BB8 File Offset: 0x00003DB8
		public static TweenerCore<Quaternion, Quaternion, NoOptions> DORotateQuaternion(this Transform target, Quaternion endValue, float duration)
		{
			TweenerCore<Quaternion, Quaternion, NoOptions> tweenerCore = DOTween.To<Quaternion, Quaternion, NoOptions>(PureQuaternionPlugin.Plug(), () => target.rotation, delegate(Quaternion x)
			{
				target.rotation = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005C04 File Offset: 0x00003E04
		public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DOLocalRotate(this Transform target, Vector3 endValue, float duration, RotateMode mode = RotateMode.Fast)
		{
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tweenerCore = DOTween.To(() => target.localRotation, delegate(Quaternion x)
			{
				target.localRotation = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			tweenerCore.plugOptions.rotateMode = mode;
			return tweenerCore;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005C58 File Offset: 0x00003E58
		public static TweenerCore<Quaternion, Quaternion, NoOptions> DOLocalRotateQuaternion(this Transform target, Quaternion endValue, float duration)
		{
			TweenerCore<Quaternion, Quaternion, NoOptions> tweenerCore = DOTween.To<Quaternion, Quaternion, NoOptions>(PureQuaternionPlugin.Plug(), () => target.localRotation, delegate(Quaternion x)
			{
				target.localRotation = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOScale(this Transform target, Vector3 endValue, float duration)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.localScale, delegate(Vector3 x)
			{
				target.localScale = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005CEC File Offset: 0x00003EEC
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOScale(this Transform target, float endValue, float duration)
		{
			Vector3 vector = new Vector3(endValue, endValue, endValue);
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.localScale, delegate(Vector3 x)
			{
				target.localScale = x;
			}, vector, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005D3C File Offset: 0x00003F3C
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOScaleX(this Transform target, float endValue, float duration)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.localScale, delegate(Vector3 x)
			{
				target.localScale = x;
			}, new Vector3(endValue, 0f, 0f), duration);
			tweenerCore.SetOptions(AxisConstraint.X, false).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005D98 File Offset: 0x00003F98
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOScaleY(this Transform target, float endValue, float duration)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.localScale, delegate(Vector3 x)
			{
				target.localScale = x;
			}, new Vector3(0f, endValue, 0f), duration);
			tweenerCore.SetOptions(AxisConstraint.Y, false).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005DF4 File Offset: 0x00003FF4
		public static TweenerCore<Vector3, Vector3, VectorOptions> DOScaleZ(this Transform target, float endValue, float duration)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => target.localScale, delegate(Vector3 x)
			{
				target.localScale = x;
			}, new Vector3(0f, 0f, endValue), duration);
			tweenerCore.SetOptions(AxisConstraint.Z, false).SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005E50 File Offset: 0x00004050
		public static Tweener DOLookAt(this Transform target, Vector3 towards, float duration, AxisConstraint axisConstraint = AxisConstraint.None, Vector3? up = null)
		{
			return target.LookAt(towards, duration, axisConstraint, up, false);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005E5E File Offset: 0x0000405E
		public static Tweener DODynamicLookAt(this Transform target, Vector3 towards, float duration, AxisConstraint axisConstraint = AxisConstraint.None, Vector3? up = null)
		{
			return target.LookAt(towards, duration, axisConstraint, up, true);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005E6C File Offset: 0x0000406C
		private static Tweener LookAt(this Transform target, Vector3 towards, float duration, AxisConstraint axisConstraint, Vector3? up, bool dynamic)
		{
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tweenerCore = DOTween.To(() => target.rotation, delegate(Quaternion x)
			{
				target.rotation = x;
			}, towards, duration).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetLookAt);
			tweenerCore.plugOptions.axisConstraint = axisConstraint;
			tweenerCore.plugOptions.up = ((up == null) ? Vector3.up : up.Value);
			if (dynamic)
			{
				tweenerCore.plugOptions.dynamicLookAt = true;
				tweenerCore.plugOptions.dynamicLookAtWorldPosition = towards;
			}
			else
			{
				tweenerCore.plugOptions.dynamicLookAt = false;
			}
			return tweenerCore;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005F10 File Offset: 0x00004110
		public static Tweener DOPunchPosition(this Transform target, Vector3 punch, float duration, int vibrato = 10, float elasticity = 1f, bool snapping = false)
		{
			if (duration <= 0f)
			{
				if (Debugger.logPriority > 0)
				{
					Debug.LogWarning("DOPunchPosition: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return DOTween.Punch(() => target.localPosition, delegate(Vector3 x)
			{
				target.localPosition = x;
			}, punch, duration, vibrato, elasticity).SetTarget(target).SetOptions(snapping);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005F7C File Offset: 0x0000417C
		public static Tweener DOPunchScale(this Transform target, Vector3 punch, float duration, int vibrato = 10, float elasticity = 1f)
		{
			if (duration <= 0f)
			{
				if (Debugger.logPriority > 0)
				{
					Debug.LogWarning("DOPunchScale: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return DOTween.Punch(() => target.localScale, delegate(Vector3 x)
			{
				target.localScale = x;
			}, punch, duration, vibrato, elasticity).SetTarget(target);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005FE0 File Offset: 0x000041E0
		public static Tweener DOPunchRotation(this Transform target, Vector3 punch, float duration, int vibrato = 10, float elasticity = 1f)
		{
			if (duration <= 0f)
			{
				if (Debugger.logPriority > 0)
				{
					Debug.LogWarning("DOPunchRotation: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return DOTween.Punch(() => target.localEulerAngles, delegate(Vector3 x)
			{
				target.localRotation = Quaternion.Euler(x);
			}, punch, duration, vibrato, elasticity).SetTarget(target);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006044 File Offset: 0x00004244
		public static Tweener DOShakePosition(this Transform target, float duration, float strength = 1f, int vibrato = 10, float randomness = 90f, bool snapping = false, bool fadeOut = true, ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (Debugger.logPriority > 0)
				{
					Debug.LogWarning("DOShakePosition: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return DOTween.Shake(() => target.localPosition, delegate(Vector3 x)
			{
				target.localPosition = x;
			}, duration, strength, vibrato, randomness, false, fadeOut, randomnessMode).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake)
				.SetOptions(snapping);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000060BC File Offset: 0x000042BC
		public static Tweener DOShakePosition(this Transform target, float duration, Vector3 strength, int vibrato = 10, float randomness = 90f, bool snapping = false, bool fadeOut = true, ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (Debugger.logPriority > 0)
				{
					Debug.LogWarning("DOShakePosition: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return DOTween.Shake(() => target.localPosition, delegate(Vector3 x)
			{
				target.localPosition = x;
			}, duration, strength, vibrato, randomness, fadeOut, randomnessMode).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake)
				.SetOptions(snapping);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006130 File Offset: 0x00004330
		public static Tweener DOShakeRotation(this Transform target, float duration, float strength = 90f, int vibrato = 10, float randomness = 90f, bool fadeOut = true, ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (Debugger.logPriority > 0)
				{
					Debug.LogWarning("DOShakeRotation: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return DOTween.Shake(() => target.localEulerAngles, delegate(Vector3 x)
			{
				target.localRotation = Quaternion.Euler(x);
			}, duration, strength, vibrato, randomness, false, fadeOut, randomnessMode).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000061A0 File Offset: 0x000043A0
		public static Tweener DOShakeRotation(this Transform target, float duration, Vector3 strength, int vibrato = 10, float randomness = 90f, bool fadeOut = true, ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (Debugger.logPriority > 0)
				{
					Debug.LogWarning("DOShakeRotation: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return DOTween.Shake(() => target.localEulerAngles, delegate(Vector3 x)
			{
				target.localRotation = Quaternion.Euler(x);
			}, duration, strength, vibrato, randomness, fadeOut, randomnessMode).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006210 File Offset: 0x00004410
		public static Tweener DOShakeScale(this Transform target, float duration, float strength = 1f, int vibrato = 10, float randomness = 90f, bool fadeOut = true, ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				Debug.Log(Debugger.logPriority);
				if (Debugger.logPriority > 0)
				{
					Debug.LogWarning("DOShakeScale: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return DOTween.Shake(() => target.localScale, delegate(Vector3 x)
			{
				target.localScale = x;
			}, duration, strength, vibrato, randomness, false, fadeOut, randomnessMode).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006290 File Offset: 0x00004490
		public static Tweener DOShakeScale(this Transform target, float duration, Vector3 strength, int vibrato = 10, float randomness = 90f, bool fadeOut = true, ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (Debugger.logPriority > 0)
				{
					Debug.LogWarning("DOShakeScale: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return DOTween.Shake(() => target.localScale, delegate(Vector3 x)
			{
				target.localScale = x;
			}, duration, strength, vibrato, randomness, fadeOut, randomnessMode).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006300 File Offset: 0x00004500
		public static Sequence DOJump(this Transform target, Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = target.position.y;
			float offsetY = -1f;
			bool offsetYSet = false;
			Sequence s = DOTween.Sequence();
			Tween yTween = DOTween.To(() => target.position, delegate(Vector3 x)
			{
				target.position = x;
			}, new Vector3(0f, jumpPower, 0f), duration / (float)(numJumps * 2)).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad)
				.SetRelative<Tweener>()
				.SetLoops(numJumps * 2, LoopType.Yoyo)
				.OnStart(delegate
				{
					startPosY = target.position.y;
				});
			s.Append(DOTween.To(() => target.position, delegate(Vector3 x)
			{
				target.position = x;
			}, new Vector3(endValue.x, 0f, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)).Join(DOTween.To(() => target.position, delegate(Vector3 x)
			{
				target.position = x;
			}, new Vector3(0f, 0f, endValue.z), duration).SetOptions(AxisConstraint.Z, snapping).SetEase(Ease.Linear)).Join(yTween)
				.SetTarget(target)
				.SetEase(DOTween.defaultEaseType);
			yTween.OnUpdate(delegate
			{
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = (s.isRelative ? endValue.y : (endValue.y - startPosY));
				}
				Vector3 position = target.position;
				position.y += DOVirtual.EasedValue(0f, offsetY, yTween.ElapsedPercentage(true), Ease.OutQuad);
				target.position = position;
			});
			return s;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000064A0 File Offset: 0x000046A0
		public static Sequence DOLocalJump(this Transform target, Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = target.localPosition.y;
			float offsetY = -1f;
			bool offsetYSet = false;
			Sequence s = DOTween.Sequence();
			Tween yTween = DOTween.To(() => target.localPosition, delegate(Vector3 x)
			{
				target.localPosition = x;
			}, new Vector3(0f, jumpPower, 0f), duration / (float)(numJumps * 2)).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad)
				.SetRelative<Tweener>()
				.SetLoops(numJumps * 2, LoopType.Yoyo)
				.OnStart(delegate
				{
					startPosY = target.localPosition.y;
				});
			s.Append(DOTween.To(() => target.localPosition, delegate(Vector3 x)
			{
				target.localPosition = x;
			}, new Vector3(endValue.x, 0f, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)).Join(DOTween.To(() => target.localPosition, delegate(Vector3 x)
			{
				target.localPosition = x;
			}, new Vector3(0f, 0f, endValue.z), duration).SetOptions(AxisConstraint.Z, snapping).SetEase(Ease.Linear)).Join(yTween)
				.SetTarget(target)
				.SetEase(DOTween.defaultEaseType);
			yTween.OnUpdate(delegate
			{
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = (s.isRelative ? endValue.y : (endValue.y - startPosY));
				}
				Vector3 localPosition = target.localPosition;
				localPosition.y += DOVirtual.EasedValue(0f, offsetY, yTween.ElapsedPercentage(true), Ease.OutQuad);
				target.localPosition = localPosition;
			});
			return s;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006640 File Offset: 0x00004840
		public static TweenerCore<Vector3, Path, PathOptions> DOPath(this Transform target, Vector3[] path, float duration, PathType pathType = PathType.Linear, PathMode pathMode = PathMode.Full3D, int resolution = 10, Color? gizmoColor = null)
		{
			if (resolution < 1)
			{
				resolution = 1;
			}
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => target.position, delegate(Vector3 x)
			{
				target.position = x;
			}, new Path(pathType, path, resolution, gizmoColor), duration).SetTarget(target);
			tweenerCore.plugOptions.mode = pathMode;
			return tweenerCore;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000066A8 File Offset: 0x000048A8
		public static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(this Transform target, Vector3[] path, float duration, PathType pathType = PathType.Linear, PathMode pathMode = PathMode.Full3D, int resolution = 10, Color? gizmoColor = null)
		{
			if (resolution < 1)
			{
				resolution = 1;
			}
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => target.localPosition, delegate(Vector3 x)
			{
				target.localPosition = x;
			}, new Path(pathType, path, resolution, gizmoColor), duration).SetTarget(target);
			tweenerCore.plugOptions.mode = pathMode;
			tweenerCore.plugOptions.useLocalPosition = true;
			return tweenerCore;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000671C File Offset: 0x0000491C
		public static TweenerCore<Vector3, Path, PathOptions> DOPath(this Transform target, Path path, float duration, PathMode pathMode = PathMode.Full3D)
		{
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => target.position, delegate(Vector3 x)
			{
				target.position = x;
			}, path, duration).SetTarget(target);
			tweenerCore.plugOptions.mode = pathMode;
			return tweenerCore;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006774 File Offset: 0x00004974
		public static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(this Transform target, Path path, float duration, PathMode pathMode = PathMode.Full3D)
		{
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => target.localPosition, delegate(Vector3 x)
			{
				target.localPosition = x;
			}, path, duration).SetTarget(target);
			tweenerCore.plugOptions.mode = pathMode;
			tweenerCore.plugOptions.useLocalPosition = true;
			return tweenerCore;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000067D8 File Offset: 0x000049D8
		public static TweenerCore<float, float, FloatOptions> DOTimeScale(this Tween target, float endValue, float duration)
		{
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => target.timeScale, delegate(float x)
			{
				target.timeScale = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006820 File Offset: 0x00004A20
		public static Tweener DOBlendableColor(this Light target, Color endValue, float duration)
		{
			endValue -= target.color;
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate(Color x)
			{
				Color color = x - to;
				to = x;
				target.color += color;
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget(target);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000689C File Offset: 0x00004A9C
		public static Tweener DOBlendableColor(this Material target, Color endValue, float duration)
		{
			endValue -= target.color;
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate(Color x)
			{
				Color color = x - to;
				to = x;
				target.color += color;
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget(target);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006918 File Offset: 0x00004B18
		public static Tweener DOBlendableColor(this Material target, Color endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			endValue -= target.GetColor(property);
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate(Color x)
			{
				Color color = x - to;
				to = x;
				target.SetColor(property, target.GetColor(property) + color);
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget(target);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000069C8 File Offset: 0x00004BC8
		public static Tweener DOBlendableColor(this Material target, Color endValue, int propertyID, float duration)
		{
			if (!target.HasProperty(propertyID))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(propertyID);
				}
				return null;
			}
			endValue -= target.GetColor(propertyID);
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate(Color x)
			{
				Color color = x - to;
				to = x;
				target.SetColor(propertyID, target.GetColor(propertyID) + color);
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget(target);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006A78 File Offset: 0x00004C78
		public static Tweener DOBlendableMoveBy(this Transform target, Vector3 byValue, float duration, bool snapping = false)
		{
			Vector3 to = Vector3.zero;
			return DOTween.To(() => to, delegate(Vector3 x)
			{
				Vector3 vector = x - to;
				to = x;
				target.position += vector;
			}, byValue, duration).Blendable<Vector3, Vector3, VectorOptions>().SetOptions(snapping)
				.SetTarget(target);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006AD4 File Offset: 0x00004CD4
		public static Tweener DOBlendableLocalMoveBy(this Transform target, Vector3 byValue, float duration, bool snapping = false)
		{
			Vector3 to = Vector3.zero;
			return DOTween.To(() => to, delegate(Vector3 x)
			{
				Vector3 vector = x - to;
				to = x;
				target.localPosition += vector;
			}, byValue, duration).Blendable<Vector3, Vector3, VectorOptions>().SetOptions(snapping)
				.SetTarget(target);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006B30 File Offset: 0x00004D30
		public static Tweener DOBlendableRotateBy(this Transform target, Vector3 byValue, float duration, RotateMode mode = RotateMode.Fast)
		{
			Quaternion to = Quaternion.identity;
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tweenerCore = DOTween.To(() => to, delegate(Quaternion x)
			{
				Quaternion quaternion = x * Quaternion.Inverse(to);
				to = x;
				Quaternion rotation = target.rotation;
				target.rotation = rotation * Quaternion.Inverse(rotation) * quaternion * rotation;
			}, byValue, duration).Blendable<Quaternion, Vector3, QuaternionOptions>().SetTarget(target);
			tweenerCore.plugOptions.rotateMode = mode;
			return tweenerCore;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006B90 File Offset: 0x00004D90
		public static Tweener DOBlendableLocalRotateBy(this Transform target, Vector3 byValue, float duration, RotateMode mode = RotateMode.Fast)
		{
			Quaternion to = Quaternion.identity;
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tweenerCore = DOTween.To(() => to, delegate(Quaternion x)
			{
				Quaternion quaternion = x * Quaternion.Inverse(to);
				to = x;
				Quaternion localRotation = target.localRotation;
				target.localRotation = localRotation * Quaternion.Inverse(localRotation) * quaternion * localRotation;
			}, byValue, duration).Blendable<Quaternion, Vector3, QuaternionOptions>().SetTarget(target);
			tweenerCore.plugOptions.rotateMode = mode;
			return tweenerCore;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006BF0 File Offset: 0x00004DF0
		public static Tweener DOBlendablePunchRotation(this Transform target, Vector3 punch, float duration, int vibrato = 10, float elasticity = 1f)
		{
			if (duration <= 0f)
			{
				if (Debugger.logPriority > 0)
				{
					Debug.LogWarning("DOBlendablePunchRotation: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			Vector3 to = Vector3.zero;
			return DOTween.Punch(() => to, delegate(Vector3 v)
			{
				Quaternion quaternion = Quaternion.Euler(to.x, to.y, to.z);
				Quaternion quaternion2 = Quaternion.Euler(v.x, v.y, v.z) * Quaternion.Inverse(quaternion);
				to = v;
				Quaternion rotation = target.rotation;
				target.rotation = rotation * Quaternion.Inverse(rotation) * quaternion2 * rotation;
			}, punch, duration, vibrato, elasticity).Blendable<Vector3, Vector3[], Vector3ArrayOptions>().SetTarget(target);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006C64 File Offset: 0x00004E64
		public static Tweener DOBlendableScaleBy(this Transform target, Vector3 byValue, float duration)
		{
			Vector3 to = Vector3.zero;
			return DOTween.To(() => to, delegate(Vector3 x)
			{
				Vector3 vector = x - to;
				to = x;
				target.localScale += vector;
			}, byValue, duration).Blendable<Vector3, Vector3, VectorOptions>().SetTarget(target);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006CB8 File Offset: 0x00004EB8
		public static int DOComplete(this Component target, bool withCallbacks = false)
		{
			return DOTween.Complete(target, withCallbacks);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006CB8 File Offset: 0x00004EB8
		public static int DOComplete(this Material target, bool withCallbacks = false)
		{
			return DOTween.Complete(target, withCallbacks);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006CC1 File Offset: 0x00004EC1
		public static int DOKill(this Component target, bool complete = false)
		{
			return DOTween.Kill(target, complete);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006CC1 File Offset: 0x00004EC1
		public static int DOKill(this Material target, bool complete = false)
		{
			return DOTween.Kill(target, complete);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006CCA File Offset: 0x00004ECA
		public static int DOFlip(this Component target)
		{
			return DOTween.Flip(target);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006CCA File Offset: 0x00004ECA
		public static int DOFlip(this Material target)
		{
			return DOTween.Flip(target);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006CD2 File Offset: 0x00004ED2
		public static int DOGoto(this Component target, float to, bool andPlay = false)
		{
			return DOTween.Goto(target, to, andPlay);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006CD2 File Offset: 0x00004ED2
		public static int DOGoto(this Material target, float to, bool andPlay = false)
		{
			return DOTween.Goto(target, to, andPlay);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006CDC File Offset: 0x00004EDC
		public static int DOPause(this Component target)
		{
			return DOTween.Pause(target);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006CDC File Offset: 0x00004EDC
		public static int DOPause(this Material target)
		{
			return DOTween.Pause(target);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006CE4 File Offset: 0x00004EE4
		public static int DOPlay(this Component target)
		{
			return DOTween.Play(target);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006CE4 File Offset: 0x00004EE4
		public static int DOPlay(this Material target)
		{
			return DOTween.Play(target);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006CEC File Offset: 0x00004EEC
		public static int DOPlayBackwards(this Component target)
		{
			return DOTween.PlayBackwards(target);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006CEC File Offset: 0x00004EEC
		public static int DOPlayBackwards(this Material target)
		{
			return DOTween.PlayBackwards(target);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006CF4 File Offset: 0x00004EF4
		public static int DOPlayForward(this Component target)
		{
			return DOTween.PlayForward(target);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006CF4 File Offset: 0x00004EF4
		public static int DOPlayForward(this Material target)
		{
			return DOTween.PlayForward(target);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006CFC File Offset: 0x00004EFC
		public static int DORestart(this Component target, bool includeDelay = true)
		{
			return DOTween.Restart(target, includeDelay, -1f);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006CFC File Offset: 0x00004EFC
		public static int DORestart(this Material target, bool includeDelay = true)
		{
			return DOTween.Restart(target, includeDelay, -1f);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006D0A File Offset: 0x00004F0A
		public static int DORewind(this Component target, bool includeDelay = true)
		{
			return DOTween.Rewind(target, includeDelay);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006D0A File Offset: 0x00004F0A
		public static int DORewind(this Material target, bool includeDelay = true)
		{
			return DOTween.Rewind(target, includeDelay);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006D13 File Offset: 0x00004F13
		public static int DOSmoothRewind(this Component target)
		{
			return DOTween.SmoothRewind(target);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006D13 File Offset: 0x00004F13
		public static int DOSmoothRewind(this Material target)
		{
			return DOTween.SmoothRewind(target);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006D1B File Offset: 0x00004F1B
		public static int DOTogglePause(this Component target)
		{
			return DOTween.TogglePause(target);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006D1B File Offset: 0x00004F1B
		public static int DOTogglePause(this Material target)
		{
			return DOTween.TogglePause(target);
		}
	}
}
