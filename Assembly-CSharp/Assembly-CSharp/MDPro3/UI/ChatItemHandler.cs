using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013E1 RID: 5089
	public class ChatItemHandler : MonoBehaviour
	{
		// Token: 0x06009363 RID: 37731 RVA: 0x0014D318 File Offset: 0x0014B518
		public void Start()
		{
			this.text0.text = this.text;
			this.text1.text = this.text;
			int target = this.GetFrameWidth();
			if (target > 360)
			{
				target = 360;
			}
			if (target < 150)
			{
				target = 150;
			}
			this.frame0.GetComponent<RectTransform>().sizeDelta = new Vector2((float)target, this.frameHeight0);
			target = this.GetFrameWidth() + 80;
			if (target > 380)
			{
				target = 380;
			}
			if (target < 150)
			{
				target = 150;
			}
			this.frame1.GetComponent<RectTransform>().sizeDelta = new Vector2((float)target, this.frameHeight1);
			this.anchor.localScale = Vector3.zero;
			this.anchor.DOScale(1f, 0.2f);
			if (this.frame == 0)
			{
				global::UnityEngine.Object.Destroy(this.frame1.gameObject);
			}
			else
			{
				global::UnityEngine.Object.Destroy(this.frame0.gameObject);
			}
			global::UnityEngine.Object.Destroy(base.gameObject, (this.time < 2f) ? 2f : this.time);
		}

		// Token: 0x06009364 RID: 37732 RVA: 0x0014D43F File Offset: 0x0014B63F
		public void BeGray()
		{
			if (this.frame0 != null)
			{
				this.frame0.color = Color.gray;
			}
			if (this.frame1 != null)
			{
				this.frame1.color = Color.gray;
			}
		}

		// Token: 0x06009365 RID: 37733 RVA: 0x0014D480 File Offset: 0x0014B680
		private int GetFrameWidth()
		{
			string[] lines = this.text.Split("\n", StringSplitOptions.None);
			int maxC = 0;
			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i].Length > maxC)
				{
					maxC = lines[i].Length;
				}
			}
			return maxC * this.charaWidth;
		}

		// Token: 0x0400D1C1 RID: 53697
		public RectTransform anchor;

		// Token: 0x0400D1C2 RID: 53698
		public Image frame0;

		// Token: 0x0400D1C3 RID: 53699
		public Image frame1;

		// Token: 0x0400D1C4 RID: 53700
		public TextMeshProUGUI text0;

		// Token: 0x0400D1C5 RID: 53701
		public TextMeshProUGUI text1;

		// Token: 0x0400D1C6 RID: 53702
		public string text;

		// Token: 0x0400D1C7 RID: 53703
		public float time;

		// Token: 0x0400D1C8 RID: 53704
		public int frame;

		// Token: 0x0400D1C9 RID: 53705
		private float frameHeight0 = 120f;

		// Token: 0x0400D1CA RID: 53706
		private float frameHeight1 = 160f;

		// Token: 0x0400D1CB RID: 53707
		private int charaWidth = 40;
	}
}
