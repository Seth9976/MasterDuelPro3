using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Effect
{
	// Token: 0x02000C35 RID: 3125
	public class EffectHandler : MonoBehaviour
	{
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06005910 RID: 22800 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005911 RID: 22801 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isPlaying
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06005912 RID: 22802 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(string path, Transform parent, Action<EffectHandler> onLoadedCallback, bool autoDestroy)
		{
		}

		// Token: 0x06005913 RID: 22803 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectHandler Create(GameObject target, bool autoDestroy)
		{
			return null;
		}

		// Token: 0x06005914 RID: 22804 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06005915 RID: 22805 RVA: 0x0000216D File Offset: 0x0000036D
		public void Setup()
		{
		}

		// Token: 0x06005916 RID: 22806 RVA: 0x0000216D File Offset: 0x0000036D
		public void Play(Action onFinishedCallback)
		{
		}

		// Token: 0x06005917 RID: 22807 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsPlaying()
		{
			return false;
		}

		// Token: 0x06005918 RID: 22808 RVA: 0x0000216D File Offset: 0x0000036D
		public void Stop()
		{
		}

		// Token: 0x06005919 RID: 22809 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600591A RID: 22810 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDisp(bool disp)
		{
		}

		// Token: 0x040094FD RID: 38141
		private ParticleSystem[] particles;

		// Token: 0x040094FE RID: 38142
		private TrailRenderer[] trails;

		// Token: 0x040094FF RID: 38143
		private bool autoDestroy;

		// Token: 0x04009500 RID: 38144
		private Action onFinishedCallback;
	}
}
