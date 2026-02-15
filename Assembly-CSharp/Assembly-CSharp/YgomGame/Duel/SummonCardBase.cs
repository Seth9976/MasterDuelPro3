using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace YgomGame.Duel
{
	// Token: 0x02000F28 RID: 3880
	public abstract class SummonCardBase
	{
		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06007225 RID: 29221 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007226 RID: 29222 RVA: 0x0000216D File Offset: 0x0000036D
		public bool finished
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

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06007227 RID: 29223
		protected abstract string timelinePath { get; }

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06007228 RID: 29224
		protected abstract string seLabel { get; }

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06007229 RID: 29225
		protected abstract string trailOffsetLabel { get; }

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x0600722A RID: 29226 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool isLoading
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600722B RID: 29227 RVA: 0x0000216D File Offset: 0x0000036D
		protected void Initialize(int cardID, int uniqueID, int rareID, Vector3 position, Quaternion rotation, CardRoot.ModelType modelType, Action onLoadFinished, Action onPlayFinished)
		{
		}

		// Token: 0x0600722C RID: 29228 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadCardFront(int cardID, int rareID, Action<Texture2D> onFinished)
		{
		}

		// Token: 0x0600722D RID: 29229 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadCardBack(int sleeveID, UnityAction<Material> onFinished)
		{
		}

		// Token: 0x0600722E RID: 29230 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadTimeline(string path, int materialNum, Action<bool> onLoaded)
		{
		}

		// Token: 0x0600722F RID: 29231 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnLoadFinished(Vector3 position, Quaternion rotation, CardRoot.ModelType modelType, Action onLoadFinished, Action onPlayFinished)
		{
		}

		// Token: 0x0400ABD0 RID: 43984
		protected Texture2D textureFront;

		// Token: 0x0400ABD1 RID: 43985
		protected Material protectorMaterial;

		// Token: 0x0400ABD2 RID: 43986
		protected int loadCounter;

		// Token: 0x0400ABD3 RID: 43987
		protected GameObject autoReleaseCardPicture;
	}
}
