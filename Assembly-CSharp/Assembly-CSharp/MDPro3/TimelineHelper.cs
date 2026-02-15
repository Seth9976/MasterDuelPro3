using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.Utility;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using YgomSystem.ElementSystem;

namespace MDPro3
{
	// Token: 0x02001245 RID: 4677
	public static class TimelineHelper
	{
		// Token: 0x06008A26 RID: 35366 RVA: 0x00110488 File Offset: 0x0010E688
		public static async UniTask PlaySummonTimelineAsync()
		{
			if (!(OcgCore.summonCard == null))
			{
				TimelineHelper.code = OcgCore.summonCard.GetData().Id;
				TimelineHelper.data = CardsManager.Get(TimelineHelper.code, false);
				TimelineHelper.materials = OcgCore.materialCards;
				TimelineHelper.reason = TimelineHelper.materials[0].p.reason;
				await TimelineHelper.CacheCutin(TimelineHelper.code);
				await TimelineHelper.CacheUnitCardsAsync();
				if (TimelineHelper.materials.Count > 0)
				{
					TimelineHelper.ShowUnitCardsAsync();
				}
				PlayableDirector director = TimelineHelper.GetPlayableDirector();
				director.AutoDestroy(false);
				Program.instance.ocgcore.allGameObjects.Add(director.gameObject);
				double strongSummontime = TimelineHelper.GetDirectorLabelTime(director, "StrongSummon");
				double startCardTime = TimelineHelper.GetDirectorLabelTime(director, "StartCard");
				UniTask directorTask = director.WaitToTimeAsync(strongSummontime, true, default(CancellationToken));
				UniTask inputTask = UniTask.WaitUntil(() => UserInput.MouseLeftDown, PlayerLoopTiming.Update, default(CancellationToken), false);
				await UniTask.WhenAny(new UniTask[] { directorTask, inputTask });
				if (director.time < strongSummontime)
				{
					director.time = strongSummontime;
					AudioManager.ResetSESource();
				}
				global::UnityEngine.Object.Destroy(TimelineHelper.unitCards);
				await director.WaitToTimeAsync(startCardTime, false, default(CancellationToken));
				await TimelineHelper.StartCard();
			}
		}

		// Token: 0x06008A27 RID: 35367 RVA: 0x001104C4 File Offset: 0x0010E6C4
		private static async UniTask CacheUnitCardsAsync()
		{
			if (TimelineHelper.data.HasType(CardType.Fusion) && OcgCore.chainSolvingCard != null && OcgCore.chainSolvingCard.GetData().GetOriginalID() == 48130397)
			{
				if (TimelineHelper.materials.Count > 8)
				{
					await ABLoader.LoadFromFolderAsync<PlayableDirector>("MasterDuel/Timeline/Summon/SummonFusion/SummonFusion07445ShowUnitCard08", true, false, null);
				}
				else
				{
					await ABLoader.LoadFromFolderAsync<PlayableDirector>("MasterDuel/Timeline/Summon/SummonFusion/SummonFusion07445ShowUnitCard0" + TimelineHelper.materials.Count.ToString(), true, false, null);
				}
			}
			else if (TimelineHelper.data.HasType(CardType.Fusion) && OcgCore.chainSolvingCard != null && OcgCore.chainSolvingCard.GetData().GetOriginalID() == 74063034)
			{
				string path = "MasterDuel/Timeline/Summon/SummonFusion/SummonFusion12852ShowUnitCard08";
				if (TimelineHelper.materials.Count < 8)
				{
					path = "MasterDuel/Timeline/Summon/SummonFusion/SummonFusion12852ShowUnitCard0" + TimelineHelper.materials.Count.ToString();
				}
				if (OcgCore.chainSolvingCard.GetData().Id == 74063035)
				{
					path = path.Replace("12852", "03432");
				}
				await ABLoader.LoadFromFolderAsync<PlayableDirector>(path, true, false, null);
			}
		}

		// Token: 0x06008A28 RID: 35368 RVA: 0x00110500 File Offset: 0x0010E700
		private static async UniTaskVoid ShowUnitCardsAsync()
		{
			TimelineHelper.useSpecialUnitCard = false;
			int maxCount = 8;
			if (TimelineHelper.data.HasType(CardType.Fusion) && OcgCore.chainSolvingCard != null && OcgCore.chainSolvingCard.GetData().GetOriginalID() == 48130397)
			{
				TimelineHelper.useSpecialUnitCard = true;
				if (TimelineHelper.materials.Count > 8)
				{
					TimelineHelper.unitCards = ABLoader.LoadFromFolder<PlayableDirector>("MasterDuel/Timeline/Summon/SummonFusion/SummonFusion07445ShowUnitCard08", true, true);
				}
				else
				{
					TimelineHelper.unitCards = ABLoader.LoadFromFolder<PlayableDirector>("MasterDuel/Timeline/Summon/SummonFusion/SummonFusion07445ShowUnitCard0" + TimelineHelper.materials.Count.ToString(), true, true);
				}
			}
			else if (TimelineHelper.data.HasType(CardType.Fusion) && OcgCore.chainSolvingCard != null && OcgCore.chainSolvingCard.GetData().GetOriginalID() == 74063034)
			{
				TimelineHelper.useSpecialUnitCard = true;
				string path = "MasterDuel/Timeline/Summon/SummonFusion/SummonFusion12852ShowUnitCard08";
				if (TimelineHelper.materials.Count < 8)
				{
					path = "MasterDuel/Timeline/Summon/SummonFusion/SummonFusion12852ShowUnitCard0" + TimelineHelper.materials.Count.ToString();
				}
				if (OcgCore.chainSolvingCard.GetData().Id == 74063035)
				{
					path = path.Replace("12852", "03432");
				}
				UniTask<GameObject>.Awaiter awaiter = ABLoader.LoadFromFolderAsync<PlayableDirector>(path, true, true, null).GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					await awaiter;
					UniTask<GameObject>.Awaiter awaiter2;
					awaiter = awaiter2;
					awaiter2 = default(UniTask<GameObject>.Awaiter);
				}
				TimelineHelper.unitCards = awaiter.GetResult();
			}
			else if (TimelineHelper.data.HasType(CardType.Fusion))
			{
				if (TimelineHelper.materials.Count > 8)
				{
					TimelineHelper.unitCards = ABLoader.LoadMasterDuelGameObject("SummonFusionShowUnitCard08");
				}
				else
				{
					TimelineHelper.unitCards = ABLoader.LoadMasterDuelGameObject("SummonFusionShowUnitCard0" + TimelineHelper.materials.Count.ToString());
				}
			}
			else if (TimelineHelper.data.HasType(CardType.Synchro))
			{
				if (TimelineHelper.materials.Count > 6)
				{
					TimelineHelper.unitCards = ABLoader.LoadMasterDuelGameObject("SummonSynchroShowUnitCard06");
				}
				else
				{
					TimelineHelper.unitCards = ABLoader.LoadMasterDuelGameObject("SummonSynchroShowUnitCard0" + TimelineHelper.materials.Count.ToString());
				}
				maxCount = 6;
			}
			else if (TimelineHelper.data.HasType(CardType.Xyz))
			{
				if (TimelineHelper.materials.Count > 6)
				{
					TimelineHelper.unitCards = ABLoader.LoadMasterDuelGameObject("SummonXYZShowUnitCard06");
				}
				else
				{
					TimelineHelper.unitCards = ABLoader.LoadMasterDuelGameObject("SummonXYZShowUnitCard0" + TimelineHelper.materials.Count.ToString());
				}
				maxCount = 6;
			}
			else if (TimelineHelper.data.HasType(CardType.Link))
			{
				if (TimelineHelper.materials.Count > 8)
				{
					TimelineHelper.unitCards = ABLoader.LoadMasterDuelGameObject("SummonLinkShowUnitCard08");
				}
				else
				{
					TimelineHelper.unitCards = ABLoader.LoadMasterDuelGameObject("SummonLinkShowUnitCard0" + TimelineHelper.materials.Count.ToString());
				}
			}
			else
			{
				if (TimelineHelper.materials.Count > 6)
				{
					TimelineHelper.unitCards = ABLoader.LoadMasterDuelGameObject("SummonRitualShowUnitCard06");
				}
				else
				{
					TimelineHelper.unitCards = ABLoader.LoadMasterDuelGameObject("SummonRitualShowUnitCard0" + TimelineHelper.materials.Count.ToString());
				}
				maxCount = 6;
			}
			ElementObjectManager manager = TimelineHelper.unitCards.GetComponent<ElementObjectManager>();
			for (int i = 0; i < Mathf.Min(TimelineHelper.materials.Count, maxCount); i++)
			{
				Renderer element = manager.GetElement<ElementObjectManager>("DummyCard" + (i + 1).ToString("00")).GetElement<MeshRenderer>("DummyCardModel_front");
				int code = TimelineHelper.materials[i].GetData().Id;
				if (code == 0)
				{
					code = TimelineHelper.materials[i].GetValidData().Id;
				}
				TimelineHelper.RefreshCardFace(element, code, false);
				if (TimelineHelper.data.HasType(CardType.Synchro))
				{
					if (TimelineHelper.materials[i].GetData().HasType(CardType.Tuner))
					{
						global::UnityEngine.Object.Destroy(manager.GetElement(string.Format("Synchro01Card0{0}", i + 1)));
					}
					else
					{
						global::UnityEngine.Object.Destroy(manager.GetElement(string.Format("Synchro00Card0{0}", i + 1)));
					}
				}
			}
			Program.instance.ocgcore.allGameObjects.Add(TimelineHelper.unitCards);
			await TimelineHelper.unitCards.GetComponent<PlayableDirector>().AutoDestroy(true);
		}

		// Token: 0x06008A29 RID: 35369 RVA: 0x0011053C File Offset: 0x0010E73C
		private static PlayableDirector GetPlayableDirector()
		{
			if (TimelineHelper.data.HasType(CardType.Fusion))
			{
				return TimelineHelper.GetFusionDirector();
			}
			if (TimelineHelper.data.HasType(CardType.Synchro))
			{
				return TimelineHelper.GetSynchroDirector();
			}
			if (TimelineHelper.data.HasType(CardType.Xyz))
			{
				return TimelineHelper.GetXyzDirector();
			}
			if (TimelineHelper.data.HasType(CardType.Link))
			{
				return TimelineHelper.GetLinkDirector();
			}
			return TimelineHelper.GetRitualDirector();
		}

		// Token: 0x06008A2A RID: 35370 RVA: 0x001105A8 File Offset: 0x0010E7A8
		private static PlayableDirector GetFusionDirector()
		{
			GameObject go;
			if (TimelineHelper.materials.Count > 5)
			{
				go = ABLoader.LoadMasterDuelGameObject("FusionNum");
			}
			else
			{
				go = ABLoader.LoadMasterDuelGameObject(string.Format("SummonFusion0{0}_01", TimelineHelper.materials.Count));
			}
			ElementObjectManager manager = go.GetComponent<ElementObjectManager>();
			TimelineHelper.dummyCard = manager.GetElement<Transform>("PostFusionPosDummy");
			TimelineHelper.RefreshCardFrame(manager.GetElement<Renderer>("CardModel"), TimelineHelper.code);
			TimelineHelper.RefreshCardFrame(manager.GetElement<Renderer>("PostFusion"), TimelineHelper.code);
			switch (TimelineHelper.materials.Count)
			{
			case 1:
			{
				Renderer card = manager.GetElement<Renderer>("FusionCard01");
				TimelineHelper.RefreshCardFrame(card, TimelineHelper.materials.Count, 0);
				break;
			}
			case 2:
			case 3:
			case 4:
			{
				Renderer card = manager.GetElement<Renderer>("FusionCard01");
				Renderer element = manager.GetElement<Renderer>("FusionCard02");
				TimelineHelper.RefreshCardFrame(card, TimelineHelper.materials.Count, 0);
				TimelineHelper.RefreshCardFrame(element, TimelineHelper.materials.Count, 0);
				break;
			}
			case 5:
			{
				for (int i = 1; i < 6; i++)
				{
					TimelineHelper.RefreshCardFrame(manager.GetElement<Renderer>("FusionCard0" + i.ToString()), 1, i - 1);
				}
				TimelineHelper.RefreshCardFrame(manager.GetElement<Renderer>("FusionCardAll"), 5, 0);
				break;
			}
			default:
			{
				for (int j = 1; j < 7; j++)
				{
					TimelineHelper.RefreshCardFrame(manager.GetElement<Renderer>("FusionCard0" + j.ToString()), 1, j - 1);
				}
				break;
			}
			}
			if (TimelineHelper.useSpecialUnitCard && TimelineHelper.materials.Count > 0)
			{
				manager.GetElement("BlackNormal").SetActive(false);
				manager.GetElement("BlackSSummon").SetActive(true);
			}
			return go.GetComponent<PlayableDirector>();
		}

		// Token: 0x06008A2B RID: 35371 RVA: 0x00110774 File Offset: 0x0010E974
		private static PlayableDirector GetSynchroDirector()
		{
			GameObject go;
			if (TimelineHelper.materials.Count > 0)
			{
				go = ABLoader.LoadMasterDuelGameObject("SummonSynchro01");
			}
			else
			{
				go = ABLoader.LoadMasterDuelGameObject("SummonSynchro02");
			}
			ElementObjectManager manager = go.GetComponent<ElementObjectManager>();
			ElementObjectManager element = manager.GetElement<ElementObjectManager>("SummonSynchroPostSynchro");
			TimelineHelper.dummyCard = element.GetElement<Transform>("DummyCardSynchro");
			TimelineHelper.RefreshCardFace(element.GetNestedElement<Renderer>("DummyCardSynchro/DummyCardModel_front"), TimelineHelper.code, false);
			TimelineHelper.RefreshCardFace(element.GetElement<Renderer>("DummyCardSynchroAdd"), TimelineHelper.code, true);
			if (TimelineHelper.materials.Count > 0)
			{
				int tunerLevel = TimelineHelper.GetTunerLevel();
				int level = TimelineHelper.data.Level;
				int nonTunerLevel = level - tunerLevel;
				for (int i = 1; i < 12; i++)
				{
					if (i != nonTunerLevel)
					{
						manager.GetElement("NumberNonTuner" + i.ToString("00")).SetActive(false);
						manager.GetElement("SynchroStarLevel" + i.ToString("00")).SetActive(false);
					}
				}
				for (int j = 1; j < 12; j++)
				{
					if (j != tunerLevel)
					{
						manager.GetElement("NumberTuner" + j.ToString("00")).SetActive(false);
					}
				}
				if (level < 5)
				{
					global::UnityEngine.Object.Destroy(manager.GetElement("SynchroCircle02"));
					global::UnityEngine.Object.Destroy(manager.GetElement("SynchroCircle03"));
				}
				else if (level < 9)
				{
					global::UnityEngine.Object.Destroy(manager.GetElement("SynchroCircle01"));
					global::UnityEngine.Object.Destroy(manager.GetElement("SynchroCircle03"));
				}
				else
				{
					global::UnityEngine.Object.Destroy(manager.GetElement("SynchroCircle01"));
					global::UnityEngine.Object.Destroy(manager.GetElement("SynchroCircle02"));
				}
			}
			return go.GetComponent<PlayableDirector>();
		}

		// Token: 0x06008A2C RID: 35372 RVA: 0x00110924 File Offset: 0x0010EB24
		private static PlayableDirector GetXyzDirector()
		{
			GameObject go;
			if (TimelineHelper.materials.Count == 0)
			{
				go = ABLoader.LoadMasterDuelGameObject("SummonXYZ00_01");
			}
			else if (TimelineHelper.materials.Count == 1)
			{
				go = ABLoader.LoadMasterDuelGameObject("SummonXYZ01_01");
			}
			else if (TimelineHelper.materials.Count == 2)
			{
				go = ABLoader.LoadMasterDuelGameObject("SummonXYZ02_01");
			}
			else
			{
				go = ABLoader.LoadMasterDuelGameObject("SummonXYZ03_01");
			}
			ElementObjectManager component = go.GetComponent<ElementObjectManager>();
			TimelineHelper.dummyCard = component.GetElement<Transform>("DummyCardXYZ");
			TimelineHelper.RefreshCardFace(component.GetNestedElement<Renderer>("DummyCardXYZ/DummyCardModel_front"), TimelineHelper.code, false);
			if (DeviceInfo.OnAndroid())
			{
				foreach (MeshRenderer child in go.transform.GetComponentsInChildren<MeshRenderer>(true))
				{
					if (child.name.StartsWith("XYZInMesh"))
					{
						child.material.GetTexture("_Texture2D").wrapMode = TextureWrapMode.Clamp;
					}
				}
			}
			return go.GetComponent<PlayableDirector>();
		}

		// Token: 0x06008A2D RID: 35373 RVA: 0x00110A0C File Offset: 0x0010EC0C
		private static PlayableDirector GetLinkDirector()
		{
			int linkCount = TimelineHelper.data.GetLinkCount();
			GameObject go;
			if (linkCount == 1)
			{
				go = ABLoader.LoadMasterDuelGameObject("SummonLink01_01");
			}
			else if (linkCount == 2)
			{
				go = ABLoader.LoadMasterDuelGameObject("SummonLink02_01");
			}
			else
			{
				go = ABLoader.LoadMasterDuelGameObject("SummonLink03_01");
			}
			ElementObjectManager manager = go.GetComponent<ElementObjectManager>();
			ElementObjectManager element = manager.GetElement<ElementObjectManager>("SummonLinkPostLink");
			TimelineHelper.dummyCard = element.GetElement<Transform>("DummyCardLink");
			TimelineHelper.RefreshCardFace(element.GetNestedElement<Renderer>("DummyCardLink/DummyCardModel_front"), TimelineHelper.code, false);
			TimelineHelper.RefreshCardFace(element.GetElement<Renderer>("DummyCardLinkAdd"), TimelineHelper.code, true);
			int linkMarkers = TimelineHelper.data.LinkMarker;
			linkMarkers = TimelineHelper.DestroyLinkTrail(manager.GetElement<ElementObjectManager>("LinkTrailIn01"), linkMarkers, (linkCount > 5) ? 2 : 1);
			if (linkCount > 1)
			{
				linkMarkers = TimelineHelper.DestroyLinkTrail(manager.GetElement<ElementObjectManager>("LinkTrailIn02"), linkMarkers, (linkCount > 4) ? 2 : 1);
			}
			if (linkCount > 2)
			{
				TimelineHelper.DestroyLinkTrail(manager.GetElement<ElementObjectManager>("LinkTrailIn03"), linkMarkers, (linkCount > 3) ? 2 : 1);
			}
			if (DeviceInfo.OnAndroid())
			{
				foreach (MeshRenderer child in go.transform.GetComponentsInChildren<MeshRenderer>(true))
				{
					if (child.name.StartsWith("SummonLinkTrail"))
					{
						child.material.GetTexture("_Texture2D").wrapMode = TextureWrapMode.Clamp;
					}
				}
			}
			return go.GetComponent<PlayableDirector>();
		}

		// Token: 0x06008A2E RID: 35374 RVA: 0x00110B60 File Offset: 0x0010ED60
		private static PlayableDirector GetRitualDirector()
		{
			GameObject go;
			if (TimelineHelper.materials.Count > 0)
			{
				go = ABLoader.LoadMasterDuelGameObject("SummonRitual01");
			}
			else
			{
				go = ABLoader.LoadMasterDuelGameObject("SummonRitual02");
			}
			ElementObjectManager manager = go.GetComponent<ElementObjectManager>();
			ElementObjectManager element = manager.GetElement<ElementObjectManager>("SummonRitualPostRitual");
			TimelineHelper.dummyCard = element.GetElement<Transform>("DummyCardRitual");
			TimelineHelper.RefreshCardFace(element.GetNestedElement<Renderer>("DummyCardRitual/DummyCardModel_front"), TimelineHelper.code, false);
			TimelineHelper.RefreshCardFace(element.GetElement<Renderer>("DummyCardRitualAdd"), TimelineHelper.code, true);
			switch (TimelineHelper.materials.Count)
			{
			case 1:
				manager.GetElement("RitualTrailIn02").SetActive(false);
				manager.GetElement("RitualTrailIn03").SetActive(false);
				break;
			case 2:
				manager.GetElement("RitualTrailIn01").SetActive(false);
				manager.GetElement("RitualTrailIn03").SetActive(false);
				break;
			case 3:
				manager.GetElement("RitualTrailIn01").SetActive(false);
				manager.GetElement("RitualTrailIn02").SetActive(false);
				break;
			case 4:
				manager.GetElement("RitualTrailIn02").SetActive(false);
				break;
			case 5:
				manager.GetElement("RitualTrailIn01").SetActive(false);
				break;
			}
			return go.GetComponent<PlayableDirector>();
		}

		// Token: 0x06008A2F RID: 35375 RVA: 0x00110CA8 File Offset: 0x0010EEA8
		private static async UniTask CacheCutin(int code)
		{
			if (CutinViewer.HasCutin(code))
			{
				if (CutinViewer.codes.Contains(code))
				{
					await ABLoader.LoadFromFolderAsync<PlayableDirector>("MonsterCutin/" + code.ToString(), true, false, null);
				}
				else
				{
					await ABLoader.LoadFromFileAsync("MonsterCutin2/" + code.ToString(), true, false);
				}
			}
		}

		// Token: 0x06008A30 RID: 35376 RVA: 0x00110CEC File Offset: 0x0010EEEC
		private static async UniTask RefreshCardFace(Renderer face, int code, bool post = false)
		{
			Texture texture = await CardImageLoader.LoadCardAsync(code, false, face.GetCancellationTokenOnDestroy(), false);
			if (!post)
			{
				face.material = MaterialLoader.GetCardMaterial(code, true);
			}
			face.material.mainTexture = texture;
		}

		// Token: 0x06008A31 RID: 35377 RVA: 0x00110D40 File Offset: 0x0010EF40
		private static async UniTask RefreshCardFrame(Renderer face, int code)
		{
			Texture frame = await CardImageLoader.LoadCardAsync(code, false, face.GetCancellationTokenOnDestroy(), false);
			face.material.SetTexture("_CardFrameA", frame);
		}

		// Token: 0x06008A32 RID: 35378 RVA: 0x00110D8C File Offset: 0x0010EF8C
		private static async UniTask RefreshCardFrame(Renderer face, int count, int order)
		{
			for (int i = 0; i < count; i++)
			{
				int code = TimelineHelper.materials[i + order].GetData().Id;
				if (code == 0)
				{
					code = TimelineHelper.materials[i + order].GetValidData().Id;
				}
				Texture cardTex = await CardImageLoader.LoadCardAsync(code, false, face.GetCancellationTokenOnDestroy(), false);
				face.material.SetTexture("_CardFrame" + ((char)(65 + i)).ToString(), cardTex);
			}
		}

		// Token: 0x06008A33 RID: 35379 RVA: 0x00110DE0 File Offset: 0x0010EFE0
		private static int GetTunerLevel()
		{
			int tunerLevel = 0;
			bool levelForSelect = false;
			foreach (GameCard material in TimelineHelper.materials)
			{
				tunerLevel += material.levelForSelect_1;
			}
			if (tunerLevel == TimelineHelper.data.Level)
			{
				levelForSelect = true;
			}
			tunerLevel = 0;
			foreach (GameCard material2 in TimelineHelper.materials)
			{
				if (material2.GetData().HasType(CardType.Tuner))
				{
					if (levelForSelect)
					{
						tunerLevel += material2.levelForSelect_1;
					}
					else
					{
						tunerLevel += material2.levelForSelect_2;
					}
				}
			}
			if (tunerLevel == 0)
			{
				foreach (GameCard gameCard in TimelineHelper.materials)
				{
					Card data = gameCard.GetValidData();
					if (data.HasType(CardType.Tuner))
					{
						tunerLevel += data.Level;
					}
				}
				if (tunerLevel == 0)
				{
					tunerLevel = TimelineHelper.materials[0].GetValidData().Level;
				}
			}
			return tunerLevel;
		}

		// Token: 0x06008A34 RID: 35380 RVA: 0x00110F28 File Offset: 0x0010F128
		private static int DestroyLinkTrail(ElementObjectManager manager, int linkMarkers, int need)
		{
			int foundMarker = 0;
			int foundMarkerCount = 0;
			ElementObjectManager parent = manager.transform.parent.GetComponent<ElementObjectManager>();
			if ((linkMarkers & 128) > 0)
			{
				foundMarkerCount++;
				foundMarker += 128;
			}
			else
			{
				global::UnityEngine.Object.Destroy(manager.GetElement("LinkTrailG02"));
				global::UnityEngine.Object.Destroy(parent.GetElement("Marker" + manager.name.Substring(manager.name.Length - 2, 2) + "_02"));
			}
			if (foundMarkerCount < need && (linkMarkers & 64) > 0)
			{
				foundMarkerCount++;
				foundMarker += 64;
			}
			else
			{
				global::UnityEngine.Object.Destroy(manager.GetElement("LinkTrailG01"));
				global::UnityEngine.Object.Destroy(parent.GetElement("Marker" + manager.name.Substring(manager.name.Length - 2, 2) + "_01"));
			}
			if (foundMarkerCount < need && (linkMarkers & 8) > 0)
			{
				foundMarkerCount++;
				foundMarker += 8;
			}
			else
			{
				global::UnityEngine.Object.Destroy(manager.GetElement("LinkTrailG04"));
				global::UnityEngine.Object.Destroy(parent.GetElement("Marker" + manager.name.Substring(manager.name.Length - 2, 2) + "_04"));
			}
			if (foundMarkerCount < need && (linkMarkers & 1) > 0)
			{
				foundMarkerCount++;
				foundMarker++;
			}
			else
			{
				global::UnityEngine.Object.Destroy(manager.GetElement("LinkTrailG06"));
				global::UnityEngine.Object.Destroy(parent.GetElement("Marker" + manager.name.Substring(manager.name.Length - 2, 2) + "_06"));
			}
			if (foundMarkerCount < need && (linkMarkers & 2) > 0)
			{
				foundMarkerCount++;
				foundMarker += 2;
			}
			else
			{
				global::UnityEngine.Object.Destroy(manager.GetElement("LinkTrailG07"));
				global::UnityEngine.Object.Destroy(parent.GetElement("Marker" + manager.name.Substring(manager.name.Length - 2, 2) + "_07"));
			}
			if (foundMarkerCount < need && (linkMarkers & 4) > 0)
			{
				foundMarkerCount++;
				foundMarker += 4;
			}
			else
			{
				global::UnityEngine.Object.Destroy(manager.GetElement("LinkTrailG08"));
				global::UnityEngine.Object.Destroy(parent.GetElement("Marker" + manager.name.Substring(manager.name.Length - 2, 2) + "_08"));
			}
			if (foundMarkerCount < need && (linkMarkers & 32) > 0)
			{
				foundMarkerCount++;
				foundMarker += 32;
			}
			else
			{
				global::UnityEngine.Object.Destroy(manager.GetElement("LinkTrailG05"));
				global::UnityEngine.Object.Destroy(parent.GetElement("Marker" + manager.name.Substring(manager.name.Length - 2, 2) + "_05"));
			}
			if (foundMarkerCount < need && (linkMarkers & 256) > 0)
			{
				foundMarkerCount++;
				foundMarker += 256;
			}
			else
			{
				global::UnityEngine.Object.Destroy(manager.GetElement("LinkTrailG03"));
				global::UnityEngine.Object.Destroy(parent.GetElement("Marker" + manager.name.Substring(manager.name.Length - 2, 2) + "_03"));
			}
			return linkMarkers - foundMarker;
		}

		// Token: 0x06008A35 RID: 35381 RVA: 0x00111218 File Offset: 0x0010F418
		private static async UniTask StartCard()
		{
			if (!(Program.instance == null))
			{
				if (!(Program.instance.currentServant != Program.instance.ocgcore))
				{
					if (!(TimelineHelper.dummyCard == null))
					{
						if (!(OcgCore.summonCard == null))
						{
							Vector3 position = TimelineHelper.dummyCard.position;
							Vector3 angle = TimelineHelper.dummyCard.eulerAngles;
							angle = new Vector3(-angle.x, angle.y + 180f, -angle.z);
							OcgCore.summonCard.UpdateExDeckTop();
							float interval = 0.5f;
							if (CutinViewer.HasCutin(TimelineHelper.code))
							{
								interval = 1f;
							}
							await OcgCore.summonCard.StartCardSequence(position, angle, interval).WaitAsync(default(CancellationToken));
							TimelineHelper.dummyCard = null;
							OcgCore.summonCard = null;
						}
					}
				}
			}
		}

		// Token: 0x06008A36 RID: 35382 RVA: 0x00111254 File Offset: 0x0010F454
		private static double GetDirectorLabelTime(PlayableDirector director, string label)
		{
			foreach (PlayableBinding pb in director.playableAsset.outputs)
			{
				TrackAsset track = pb.sourceObject as TrackAsset;
				if (track != null)
				{
					foreach (TimelineClip clip in track.GetClips())
					{
						if (clip.displayName == label)
						{
							return clip.start;
						}
					}
				}
			}
			return 3.5;
		}

		// Token: 0x0400C555 RID: 50517
		private static Transform dummyCard;

		// Token: 0x0400C556 RID: 50518
		private static int code;

		// Token: 0x0400C557 RID: 50519
		private static Card data;

		// Token: 0x0400C558 RID: 50520
		private static List<GameCard> materials;

		// Token: 0x0400C559 RID: 50521
		private static uint reason;

		// Token: 0x0400C55A RID: 50522
		private static GameObject unitCards;

		// Token: 0x0400C55B RID: 50523
		private static bool useSpecialUnitCard;
	}
}
