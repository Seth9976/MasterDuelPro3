using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI.Popup
{
	// Token: 0x0200148A RID: 5258
	public class PopupRockPaperScissors : Popup
	{
		// Token: 0x06009A02 RID: 39426 RVA: 0x0016F8B0 File Offset: 0x0016DAB0
		protected override void Initialize()
		{
			base.Initialize();
			this.cancelCallHide = false;
			base.Manager.GetElement<Button>("RockButton").onClick.AddListener(delegate
			{
				TcpHelper.CtosMessage_HandResult(2);
				this.Hide();
			});
			base.Manager.GetElement<Button>("PaperButton").onClick.AddListener(delegate
			{
				TcpHelper.CtosMessage_HandResult(3);
				this.Hide();
			});
			base.Manager.GetElement<Button>("ScissorsButton").onClick.AddListener(delegate
			{
				TcpHelper.CtosMessage_HandResult(1);
				this.Hide();
			});
		}

		// Token: 0x06009A03 RID: 39427 RVA: 0x0016F93C File Offset: 0x0016DB3C
		private void Start()
		{
			this.LoadAsync();
		}

		// Token: 0x06009A04 RID: 39428 RVA: 0x0016F948 File Offset: 0x0016DB48
		private async UniTask LoadAsync()
		{
			Texture2D pic = await TextureManager.LoadPicFromFileAsync("Picture/DIY/Rock.png");
			if (pic != null)
			{
				RawImage element = base.Manager.GetElement<RawImage>("RockButton");
				element.texture = pic;
				element.color = Color.white;
			}
			pic = await TextureManager.LoadPicFromFileAsync("Picture/DIY/Paper.png");
			if (pic != null)
			{
				RawImage element2 = base.Manager.GetElement<RawImage>("PaperButton");
				element2.texture = pic;
				element2.color = Color.white;
			}
			pic = await TextureManager.LoadPicFromFileAsync("Picture/DIY/Scissors.png");
			if (pic != null)
			{
				RawImage element3 = base.Manager.GetElement<RawImage>("ScissorsButton");
				element3.texture = pic;
				element3.color = Color.white;
			}
		}
	}
}
