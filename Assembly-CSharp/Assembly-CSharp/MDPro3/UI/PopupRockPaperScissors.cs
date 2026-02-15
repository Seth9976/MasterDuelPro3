using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001399 RID: 5017
	public class PopupRockPaperScissors : PopupBase
	{
		// Token: 0x06009100 RID: 37120 RVA: 0x00140464 File Offset: 0x0013E664
		private void Start()
		{
			this.rock.onClick.AddListener(delegate
			{
				TcpHelper.CtosMessage_HandResult(2);
				this.Hide();
			});
			this.paper.onClick.AddListener(delegate
			{
				TcpHelper.CtosMessage_HandResult(3);
				this.Hide();
			});
			this.scissors.onClick.AddListener(delegate
			{
				TcpHelper.CtosMessage_HandResult(1);
				this.Hide();
			});
			this.LoadAsync();
		}

		// Token: 0x06009101 RID: 37121 RVA: 0x001404CC File Offset: 0x0013E6CC
		private async UniTask LoadAsync()
		{
			RawImage rawImage = this.rock.GetComponent<RawImage>();
			Texture2D texture2D = await TextureManager.LoadPicFromFileAsync("Picture/DIY/Rock.png");
			rawImage.texture = texture2D;
			rawImage = null;
			this.rock.GetComponent<RawImage>().color = Color.white;
			rawImage = this.paper.GetComponent<RawImage>();
			texture2D = await TextureManager.LoadPicFromFileAsync("Picture/DIY/Paper.png");
			rawImage.texture = texture2D;
			rawImage = null;
			this.paper.GetComponent<RawImage>().color = Color.white;
			rawImage = this.scissors.GetComponent<RawImage>();
			texture2D = await TextureManager.LoadPicFromFileAsync("Picture/DIY/Scissors.png");
			rawImage.texture = texture2D;
			rawImage = null;
			this.scissors.GetComponent<RawImage>().color = Color.white;
		}

		// Token: 0x0400CFCD RID: 53197
		[Header("Popup Rock Paper Scissors Reference")]
		public Button rock;

		// Token: 0x0400CFCE RID: 53198
		public Button paper;

		// Token: 0x0400CFCF RID: 53199
		public Button scissors;
	}
}
