using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace MDPro3.Duel
{
	// Token: 0x020014BB RID: 5307
	public static class DuelEffectUtil
	{
		// Token: 0x06009B04 RID: 39684 RVA: 0x0017F3A4 File Offset: 0x0017D5A4
		public static void SetDeckModelAppearance(ElementObjectManager deckManager, int cardCount, ElementObjectManager fromManager)
		{
			if (cardCount > 0)
			{
				Transform deckSetOffset = deckManager.GetElement<Transform>("DeckSetOffset");
				deckSetOffset.localScale = new Vector3(1f, 0.91f * (float)cardCount / 40f, 1f);
				string label = deckManager.GetComponent<ElementObject>().label;
				if (label.Contains("Main") || label == "EnExDeck")
				{
					deckSetOffset.localEulerAngles = new Vector3(0f, 180f, 0f);
				}
				Material protectorMat = fromManager.GetNestedElement<MeshRenderer>("CardShuffleTop/CardModel01_back").material;
				deckManager.GetNestedElement<MeshRenderer>("DummyDeck/DummyCardModel_back").material = protectorMat;
				return;
			}
			global::UnityEngine.Object.Destroy(deckManager.GetElement("BaseFog"));
			deckManager.GetElement<Transform>("DeckSetOffset").localScale = Vector3.zero;
		}

		// Token: 0x0400D8FF RID: 55551
		private const float dummyDeckHeightScaleFor40Cards = 0.91f;
	}
}
