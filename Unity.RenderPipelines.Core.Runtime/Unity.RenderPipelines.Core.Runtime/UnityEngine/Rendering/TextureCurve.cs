using System;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x020001DF RID: 479
	[Serializable]
	public class TextureCurve : IDisposable
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0003232F File Offset: 0x0003052F
		// (set) Token: 0x06000D9B RID: 3483 RVA: 0x00032337 File Offset: 0x00030537
		public int length { get; private set; }

		// Token: 0x170001B7 RID: 439
		public Keyframe this[int index]
		{
			get
			{
				return this.m_Curve[index];
			}
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0003234E File Offset: 0x0003054E
		public TextureCurve(AnimationCurve baseCurve, float zeroValue, bool loop, in Vector2 bounds)
			: this(baseCurve.keys, zeroValue, loop, in bounds)
		{
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00032360 File Offset: 0x00030560
		public TextureCurve(Keyframe[] keys, float zeroValue, bool loop, in Vector2 bounds)
		{
			this.m_Curve = new AnimationCurve(keys);
			this.m_ZeroValue = zeroValue;
			this.m_Loop = loop;
			Vector2 vector = bounds;
			this.m_Range = vector.magnitude;
			this.length = keys.Length;
			this.SetDirty();
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x000323B1 File Offset: 0x000305B1
		public void Dispose()
		{
			this.Release();
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x000323B9 File Offset: 0x000305B9
		public void Release()
		{
			if (this.m_Texture != null)
			{
				CoreUtils.Destroy(this.m_Texture);
			}
			this.m_Texture = null;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000323DB File Offset: 0x000305DB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetDirty()
		{
			this.m_IsCurveDirty = true;
			this.m_IsTextureDirty = true;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x000323EB File Offset: 0x000305EB
		private static GraphicsFormat GetTextureFormat()
		{
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R16_SFloat, GraphicsFormatUsage.SetPixels))
			{
				return GraphicsFormat.R16_SFloat;
			}
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R8_UNorm, GraphicsFormatUsage.SetPixels))
			{
				return GraphicsFormat.R8_UNorm;
			}
			return GraphicsFormat.R8G8B8A8_UNorm;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00032410 File Offset: 0x00030610
		public Texture2D GetTexture()
		{
			if (this.m_Texture == null)
			{
				this.m_Texture = new Texture2D(128, 1, TextureCurve.GetTextureFormat(), TextureCreationFlags.None);
				this.m_Texture.name = "CurveTexture";
				this.m_Texture.hideFlags = HideFlags.HideAndDontSave;
				this.m_Texture.filterMode = FilterMode.Bilinear;
				this.m_Texture.wrapMode = TextureWrapMode.Clamp;
				this.m_Texture.anisoLevel = 0;
				this.m_IsTextureDirty = true;
			}
			if (this.m_IsTextureDirty)
			{
				Color[] pixels = new Color[128];
				for (int i = 0; i < pixels.Length; i++)
				{
					pixels[i].r = this.Evaluate((float)i * 0.0078125f);
				}
				this.m_Texture.SetPixels(pixels);
				this.m_Texture.Apply(false, false);
				this.m_IsTextureDirty = false;
			}
			return this.m_Texture;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x000324EC File Offset: 0x000306EC
		public float Evaluate(float time)
		{
			if (this.m_IsCurveDirty)
			{
				this.length = this.m_Curve.length;
			}
			if (this.length == 0)
			{
				return this.m_ZeroValue;
			}
			if (!this.m_Loop || this.length == 1)
			{
				return this.m_Curve.Evaluate(time);
			}
			if (this.m_IsCurveDirty)
			{
				if (this.m_LoopingCurve == null)
				{
					this.m_LoopingCurve = new AnimationCurve();
				}
				Keyframe prev = this.m_Curve[this.length - 1];
				prev.time -= this.m_Range;
				Keyframe next = this.m_Curve[0];
				next.time += this.m_Range;
				this.m_LoopingCurve.keys = this.m_Curve.keys;
				this.m_LoopingCurve.AddKey(prev);
				this.m_LoopingCurve.AddKey(next);
				this.m_IsCurveDirty = false;
			}
			return this.m_LoopingCurve.Evaluate(time);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000325E9 File Offset: 0x000307E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int AddKey(float time, float value)
		{
			int num = this.m_Curve.AddKey(time, value);
			if (num > -1)
			{
				this.SetDirty();
			}
			return num;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00032602 File Offset: 0x00030802
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int MoveKey(int index, in Keyframe key)
		{
			int num = this.m_Curve.MoveKey(index, key);
			this.SetDirty();
			return num;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0003261C File Offset: 0x0003081C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RemoveKey(int index)
		{
			this.m_Curve.RemoveKey(index);
			this.SetDirty();
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00032630 File Offset: 0x00030830
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SmoothTangents(int index, float weight)
		{
			this.m_Curve.SmoothTangents(index, weight);
			this.SetDirty();
		}

		// Token: 0x0400091F RID: 2335
		private const int k_Precision = 128;

		// Token: 0x04000920 RID: 2336
		private const float k_Step = 0.0078125f;

		// Token: 0x04000922 RID: 2338
		[SerializeField]
		private bool m_Loop;

		// Token: 0x04000923 RID: 2339
		[SerializeField]
		private float m_ZeroValue;

		// Token: 0x04000924 RID: 2340
		[SerializeField]
		private float m_Range;

		// Token: 0x04000925 RID: 2341
		[SerializeField]
		private AnimationCurve m_Curve;

		// Token: 0x04000926 RID: 2342
		private AnimationCurve m_LoopingCurve;

		// Token: 0x04000927 RID: 2343
		private Texture2D m_Texture;

		// Token: 0x04000928 RID: 2344
		private bool m_IsCurveDirty;

		// Token: 0x04000929 RID: 2345
		private bool m_IsTextureDirty;
	}
}
