using System;
using Cysharp.Threading.Tasks;
using MDPro3.Servant;
using MDPro3.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200138E RID: 5006
	public class PopupDuelPosition : PopupDuel
	{
		// Token: 0x060090C6 RID: 37062 RVA: 0x0013DF00 File Offset: 0x0013C100
		public override void InitializeSelections()
		{
			this.RefreshCard(this.code);
			if (this.count == 3)
			{
				this.btnPositionAttack.onClick.AddListener(new UnityAction(this.OnAttack));
				this.btnPositionDefense.onClick.AddListener(new UnityAction(this.OnDefense));
				this.btnPositionDefenseDown.onClick.AddListener(new UnityAction(this.OnDefenseDown));
				return;
			}
			bool p = false;
			bool p2 = false;
			bool p3 = false;
			if (this.option1 == 1)
			{
				this.btnPositionAttack.onClick.AddListener(new UnityAction(this.OnAttack));
				this.positionAttack.GetComponent<RectTransform>().anchoredPosition = new Vector2(-150f, 0f);
				p = true;
			}
			else if (this.option1 == 2)
			{
				this.btnPositionAttack.onClick.AddListener(new UnityAction(this.OnAttackDown));
				this.positionAttack.GetComponent<RectTransform>().anchoredPosition = new Vector2(-150f, 0f);
				p = true;
			}
			else
			{
				this.btnPositionDefense.onClick.AddListener(new UnityAction(this.OnDefense));
				this.positionDefense.GetComponent<RectTransform>().anchoredPosition = new Vector2(-150f, 0f);
				p2 = true;
			}
			if (this.option2 == 4)
			{
				this.btnPositionDefense.onClick.AddListener(new UnityAction(this.OnDefense));
				this.positionDefense.GetComponent<RectTransform>().anchoredPosition = new Vector2(150f, 0f);
				p2 = true;
			}
			else
			{
				this.btnPositionDefenseDown.onClick.AddListener(new UnityAction(this.OnDefenseDown));
				this.positionDefenseDown.GetComponent<RectTransform>().anchoredPosition = new Vector2(150f, 0f);
				p3 = true;
			}
			if (!p)
			{
				global::UnityEngine.Object.Destroy(this.positionAttack.gameObject);
			}
			if (!p2)
			{
				global::UnityEngine.Object.Destroy(this.positionDefense.gameObject);
			}
			if (!p3)
			{
				global::UnityEngine.Object.Destroy(this.positionDefenseDown.gameObject);
			}
		}

		// Token: 0x060090C7 RID: 37063 RVA: 0x0013E10C File Offset: 0x0013C30C
		private async UniTask RefreshCard(int code)
		{
			Texture texture = await CardImageLoader.LoadCardAsync(code, false, base.destroyCancellationToken, false);
			Material mat = MaterialLoader.GetCardMaterial(code, false);
			if (this.positionAttack != null)
			{
				if (this.option1 == 1)
				{
					this.positionAttack.material = mat;
					this.positionAttack.texture = texture;
				}
				else
				{
					this.positionAttack.material = OcgCore.myProtector;
				}
			}
			if (this.positionDefense != null)
			{
				this.positionDefense.material = mat;
				this.positionDefense.texture = texture;
			}
			if (this.positionDefenseDown != null)
			{
				this.positionDefenseDown.material = OcgCore.myProtector;
			}
		}

		// Token: 0x060090C8 RID: 37064 RVA: 0x0013E158 File Offset: 0x0013C358
		private void OnAttack()
		{
			this.Hide();
			BinaryMaster p = new BinaryMaster(null);
			p.writer.Write(1);
			base.SendReturn(p.Get());
		}

		// Token: 0x060090C9 RID: 37065 RVA: 0x0013E18C File Offset: 0x0013C38C
		private void OnAttackDown()
		{
			this.Hide();
			BinaryMaster p = new BinaryMaster(null);
			p.writer.Write(2);
			base.SendReturn(p.Get());
		}

		// Token: 0x060090CA RID: 37066 RVA: 0x0013E1C0 File Offset: 0x0013C3C0
		private void OnDefense()
		{
			this.Hide();
			BinaryMaster p = new BinaryMaster(null);
			p.writer.Write(4);
			base.SendReturn(p.Get());
		}

		// Token: 0x060090CB RID: 37067 RVA: 0x0013E1F4 File Offset: 0x0013C3F4
		private void OnDefenseDown()
		{
			this.Hide();
			BinaryMaster p = new BinaryMaster(null);
			p.writer.Write(8);
			base.SendReturn(p.Get());
		}

		// Token: 0x0400CF7D RID: 53117
		[Header("Popup Duel Position Reference")]
		public RawImage positionAttack;

		// Token: 0x0400CF7E RID: 53118
		public RawImage positionDefense;

		// Token: 0x0400CF7F RID: 53119
		public RawImage positionDefenseDown;

		// Token: 0x0400CF80 RID: 53120
		public Button btnPositionAttack;

		// Token: 0x0400CF81 RID: 53121
		public Button btnPositionDefense;

		// Token: 0x0400CF82 RID: 53122
		public Button btnPositionDefenseDown;

		// Token: 0x0400CF83 RID: 53123
		public int code;

		// Token: 0x0400CF84 RID: 53124
		public int count;

		// Token: 0x0400CF85 RID: 53125
		public int option1;

		// Token: 0x0400CF86 RID: 53126
		public int option2;
	}
}
