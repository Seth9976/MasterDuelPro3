using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Duel;

namespace YgomGame.Card
{
	// Token: 0x02001107 RID: 4359
	public class CardModel : MonoBehaviour
	{
		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x06008195 RID: 33173 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06008196 RID: 33174 RVA: 0x0000216D File Offset: 0x0000036D
		public int cardId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x06008197 RID: 33175 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06008198 RID: 33176 RVA: 0x0000216D File Offset: 0x0000036D
		public CardRoot.ModelType modelType
		{
			[CompilerGenerated]
			get
			{
				return (CardRoot.ModelType)0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x06008199 RID: 33177 RVA: 0x0000216A File Offset: 0x0000036A
		private static global::UnityEngine.Object modelSrc
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x0600819A RID: 33178 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600819B RID: 33179 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isTerminated
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

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x0600819C RID: 33180 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600819D RID: 33181 RVA: 0x0000216D File Offset: 0x0000036D
		public bool loadingFront
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

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x0600819E RID: 33182 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool loadingBack
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x0600819F RID: 33183 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060081A0 RID: 33184 RVA: 0x0000216D File Offset: 0x0000036D
		public GameObject frontModel
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x060081A1 RID: 33185 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060081A2 RID: 33186 RVA: 0x0000216D File Offset: 0x0000036D
		public GameObject backModel
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x060081A3 RID: 33187 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060081A4 RID: 33188 RVA: 0x0000216D File Offset: 0x0000036D
		public GameObject sideModel
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x060081A5 RID: 33189 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060081A6 RID: 33190 RVA: 0x0000216D File Offset: 0x0000036D
		public MeshRenderer frontRenderer
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x060081A7 RID: 33191 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060081A8 RID: 33192 RVA: 0x0000216D File Offset: 0x0000036D
		public MeshRenderer backRenderer
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x060081A9 RID: 33193 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060081AA RID: 33194 RVA: 0x0000216D File Offset: 0x0000036D
		public MeshRenderer sideRenderer
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x060081AB RID: 33195 RVA: 0x000F6958 File Offset: 0x000F4B58
		public Quaternion currentRotation
		{
			get
			{
				return default(Quaternion);
			}
		}

		// Token: 0x060081AC RID: 33196 RVA: 0x000F6970 File Offset: 0x000F4B70
		public static ValueTuple<CardModel, GameObject> CreateAsync(Transform parent, int cardId, int sleeveId, int rareId, Action<CardModel> onFinished = null)
		{
			return default(ValueTuple<CardModel, GameObject>);
		}

		// Token: 0x060081AD RID: 33197 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartLoadTexture(int cardId, int sleeveId, int styleID, Action<CardModel> onFinished = null)
		{
		}

		// Token: 0x060081AE RID: 33198 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardModel AddComponent(GameObject go)
		{
			return null;
		}

		// Token: 0x060081AF RID: 33199 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x060081B0 RID: 33200 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardTexAsync(int cardId, int styleID, Action<CardModel> onLoaded = null)
		{
		}

		// Token: 0x060081B1 RID: 33201 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardTexAsync(MeshRenderer target, int cardId, int rareId, Action<CardModel> onLoaded = null, bool force = false)
		{
		}

		// Token: 0x060081B2 RID: 33202 RVA: 0x0000216D File Offset: 0x0000036D
		public void CrearContents()
		{
		}

		// Token: 0x060081B3 RID: 33203 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show()
		{
		}

		// Token: 0x060081B4 RID: 33204 RVA: 0x0000216D File Offset: 0x0000036D
		public void Hide()
		{
		}

		// Token: 0x060081B5 RID: 33205 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x060081B6 RID: 33206 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator LoadTexturesProcess(int cardId, int sleeveId, int styleID, Action<CardModel> onFinished)
		{
			return null;
		}

		// Token: 0x060081B7 RID: 33207 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBackInsight(bool insight)
		{
		}

		// Token: 0x060081B8 RID: 33208 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRotation(Quaternion rotation)
		{
		}

		// Token: 0x060081B9 RID: 33209 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFrontColor(Color color)
		{
		}

		// Token: 0x060081BA RID: 33210 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBackColor(Color color)
		{
		}

		// Token: 0x060081BB RID: 33211 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetModelType(CardRoot.ModelType modelType)
		{
		}

		// Token: 0x0400BA11 RID: 47633
		private static global::UnityEngine.Object m_modelSrc;

		// Token: 0x0400BA12 RID: 47634
		private int styleId;

		// Token: 0x0400BA13 RID: 47635
		private int sleeveId;

		// Token: 0x0400BA14 RID: 47636
		private static Quaternion rotationBase;

		// Token: 0x0400BA15 RID: 47637
		private static readonly string cardModelResPath;

		// Token: 0x0400BA16 RID: 47638
		public const float thickness = 0.05f;

		// Token: 0x0400BA17 RID: 47639
		public static readonly Vector3 size;
	}
}
