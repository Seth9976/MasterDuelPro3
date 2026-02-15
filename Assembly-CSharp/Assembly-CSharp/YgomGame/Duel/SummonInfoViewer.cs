using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.Utility;

namespace YgomGame.Duel
{
	// Token: 0x02000F2A RID: 3882
	public class SummonInfoViewer : MonoBehaviour
	{
		// Token: 0x06007250 RID: 29264 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, Action<SummonInfoViewer> onLoaded)
		{
		}

		// Token: 0x06007251 RID: 29265 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06007252 RID: 29266 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupInfo(SharedDefinition.Location location, ElementObjectManager root)
		{
		}

		// Token: 0x06007253 RID: 29267 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDisp(bool disp)
		{
		}

		// Token: 0x06007254 RID: 29268 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDisp(SharedDefinition.Location location, bool disp)
		{
		}

		// Token: 0x06007255 RID: 29269 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsHideLock(SharedDefinition.Location location)
		{
			return false;
		}

		// Token: 0x06007256 RID: 29270 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetInitializeParams(SharedDefinition.Location location, bool spSummonNG, bool summonNG, int summonNum)
		{
		}

		// Token: 0x06007257 RID: 29271 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDispSPSummonNG(SharedDefinition.Location location, bool disp)
		{
		}

		// Token: 0x06007258 RID: 29272 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDispSummonNG(SharedDefinition.Location location, bool disp)
		{
		}

		// Token: 0x06007259 RID: 29273 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSummonNum(SharedDefinition.Location location, int num, bool showIfChanged)
		{
		}

		// Token: 0x0600725A RID: 29274 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTotalAtk(SharedDefinition.Location location, int totalAtk)
		{
		}

		// Token: 0x0600725B RID: 29275 RVA: 0x0000216D File Offset: 0x0000036D
		private void EnqueueTweenConductor(GameObject gameObject, TweenConductor tweenConductor)
		{
		}

		// Token: 0x0600725C RID: 29276 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayQueuedTweenConductor(GameObject gameObject)
		{
		}

		// Token: 0x0400ABE9 RID: 44009
		[SerializeField]
		private GameObject prefabUI;

		// Token: 0x0400ABEA RID: 44010
		private ElementObjectManager ui;

		// Token: 0x0400ABEB RID: 44011
		private Dictionary<SharedDefinition.Location, SummonInfoViewer.Info> infoList;

		// Token: 0x0400ABEC RID: 44012
		private Dictionary<SharedDefinition.Location, bool> dispDict;

		// Token: 0x0400ABED RID: 44013
		private Dictionary<SharedDefinition.Location, int> hideLockCounterDict;

		// Token: 0x0400ABEE RID: 44014
		private const string prefabPath = "Prefabs/Duel/SummonInfoViewer";

		// Token: 0x0400ABEF RID: 44015
		private Dictionary<GameObject, Queue<TweenConductor>> tweenConductors;

		// Token: 0x02000F2B RID: 3883
		private class Info
		{
			// Token: 0x0400ABF0 RID: 44016
			public GameObject root;

			// Token: 0x0400ABF1 RID: 44017
			public GameObject spSummonNG;

			// Token: 0x0400ABF2 RID: 44018
			public GameObject summonNG;

			// Token: 0x0400ABF3 RID: 44019
			public GameObject spSummonNGIcon;

			// Token: 0x0400ABF4 RID: 44020
			public GameObject summonNGIcon;

			// Token: 0x0400ABF5 RID: 44021
			public TMP_Text textSummonNum;

			// Token: 0x0400ABF6 RID: 44022
			public TMP_Text textTotalAtk;

			// Token: 0x0400ABF7 RID: 44023
			public bool isSpSummonNG;

			// Token: 0x0400ABF8 RID: 44024
			public bool isSummonNG;

			// Token: 0x0400ABF9 RID: 44025
			public int summonNum;

			// Token: 0x0400ABFA RID: 44026
			public int totalAtk;
		}
	}
}
