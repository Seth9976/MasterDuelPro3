using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MDPro3.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MDPro3
{
	// Token: 0x02001269 RID: 4713
	public class MessageManager : Manager
	{
		// Token: 0x06008AAE RID: 35502 RVA: 0x00115E9C File Offset: 0x0011409C
		public override void Initialize()
		{
			base.Initialize();
			MessageManager.instance = this;
			Addressables.LoadAssetAsync<GameObject>("UI/MessageCast.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				this.messageCast = result.Result;
			};
			Addressables.LoadAssetAsync<GameObject>("UI/MessageToast.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				this.messageToast = result.Result;
			};
			Addressables.LoadAssetAsync<GameObject>("UI/MessageCard.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				this.messageCard = result.Result;
			};
		}

		// Token: 0x06008AAF RID: 35503 RVA: 0x00115F0F File Offset: 0x0011410F
		public override void PerFrameFunction()
		{
			base.PerFrameFunction();
			if (MessageManager.messageFromSubString != string.Empty)
			{
				MessageManager.Cast(MessageManager.messageFromSubString);
				MessageManager.messageFromSubString = string.Empty;
			}
		}

		// Token: 0x06008AB0 RID: 35504 RVA: 0x00115F3C File Offset: 0x0011413C
		public void CastCard(int code)
		{
			CameraManager.UIBlurPlus();
			GameObject item = global::UnityEngine.Object.Instantiate<GameObject>(Program.instance.message_.messageCard);
			Program.instance.ocgcore.allGameObjects.Add(item);
			item.transform.SetParent(MessageManager.instance.transform, false);
			this.RefreshAsync(item, code);
		}

		// Token: 0x06008AB1 RID: 35505 RVA: 0x00115F98 File Offset: 0x00114198
		private async UniTask RefreshAsync(GameObject item, int code)
		{
			RawImage ri = item.GetComponent<RawImage>();
			RawImage rawImage = ri;
			Texture texture = await CardImageLoader.LoadCardAsync(code, false, ri.destroyCancellationToken, false);
			rawImage.texture = texture;
			rawImage = null;
			ri.material = MaterialLoader.GetCardMaterial(code, false);
			RectTransform rect = item.GetComponent<RectTransform>();
			rect.anchoredPosition = new Vector2(200f, -160f);
			rect.DOAnchorPosX(-50f, MessageManager.transitionTime, false);
			DOTween.To(delegate(float v)
			{
			}, 0f, 0f, MessageManager.transitionTime + MessageManager.existTime).OnComplete(delegate
			{
				rect.DOAnchorPosX(200f, MessageManager.transitionTime, false);
			});
			DOTween.To(delegate(float v)
			{
			}, 0f, 0f, MessageManager.existTime + MessageManager.transitionTime * 2f).OnComplete(delegate
			{
				global::UnityEngine.Object.Destroy(item);
				CameraManager.UIBlurMinus();
			});
		}

		// Token: 0x06008AB2 RID: 35506 RVA: 0x00115FE4 File Offset: 0x001141E4
		public static void Toast(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				return;
			}
			CameraManager.UIBlurPlus();
			GameObject item = global::UnityEngine.Object.Instantiate<GameObject>(Program.instance.message_.messageToast);
			item.transform.SetParent(MessageManager.instance.transform, false);
			item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = message;
			global::UnityEngine.Object.Destroy(item, 2f);
			DOTween.To(delegate(float v)
			{
			}, 0f, 0f, 2f).OnComplete(delegate
			{
				global::UnityEngine.Object.Destroy(item);
				CameraManager.UIBlurMinus();
			});
		}

		// Token: 0x06008AB3 RID: 35507 RVA: 0x001160AC File Offset: 0x001142AC
		public static void Cast(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				return;
			}
			if (MessageManager.items.Count > 10)
			{
				return;
			}
			if (Program.instance.message_.messageCast == null)
			{
				MessageManager.cachedMessage.Add(message);
				return;
			}
			if (MessageManager.cachedMessage.Count > 0)
			{
				List<string> list = new List<string>(MessageManager.cachedMessage);
				MessageManager.cachedMessage.Clear();
				foreach (string text in list)
				{
					MessageManager.Cast(text);
				}
			}
			CameraManager.UIBlurPlus();
			GameObject item = global::UnityEngine.Object.Instantiate<GameObject>(Program.instance.message_.messageCast);
			item.transform.SetParent(MessageManager.instance.transform, false);
			item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = message;
			RectTransform rect = item.GetComponent<RectTransform>();
			int id = MessageManager.items.Count;
			MessageManager.items.Add(item);
			rect.anchoredPosition = new Vector2(900f, (float)(-160 - id * 120));
			rect.DOAnchorPosX(-10f, MessageManager.transitionTime, false);
			DOTween.To(delegate(float v)
			{
			}, 0f, 0f, MessageManager.existTime + (float)id).OnComplete(delegate
			{
				rect.DOAnchorPosX(900f, MessageManager.transitionTime, false);
			});
			DOTween.To(delegate(float v)
			{
			}, 0f, 0f, MessageManager.existTime + MessageManager.transitionTime + (float)id).OnComplete(delegate
			{
				MessageManager.items.Remove(item);
				global::UnityEngine.Object.Destroy(item);
				MessageManager.MoveUp();
				CameraManager.UIBlurMinus();
			});
		}

		// Token: 0x06008AB4 RID: 35508 RVA: 0x001162A8 File Offset: 0x001144A8
		private static void MoveUp()
		{
			foreach (GameObject gameObject in MessageManager.items)
			{
				RectTransform component = gameObject.GetComponent<RectTransform>();
				component.DOAnchorPosY(component.anchoredPosition.y + 120f, MessageManager.transitionTime, false);
			}
		}

		// Token: 0x0400C644 RID: 50756
		public GameObject messageCast;

		// Token: 0x0400C645 RID: 50757
		public GameObject messageToast;

		// Token: 0x0400C646 RID: 50758
		public GameObject messageCard;

		// Token: 0x0400C647 RID: 50759
		private static MessageManager instance;

		// Token: 0x0400C648 RID: 50760
		private static List<GameObject> items = new List<GameObject>();

		// Token: 0x0400C649 RID: 50761
		private static readonly float transitionTime = 0.3f;

		// Token: 0x0400C64A RID: 50762
		private static readonly float existTime = 3f;

		// Token: 0x0400C64B RID: 50763
		public static string messageFromSubString = string.Empty;

		// Token: 0x0400C64C RID: 50764
		private static List<string> cachedMessage = new List<string>();
	}
}
