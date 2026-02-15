using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000A54 RID: 2644
	public class DialogViewControllerManager : ViewControllerManager
	{
		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06004D3B RID: 19771 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int selectorRootPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x0000216A File Offset: 0x0000036A
		public static DialogViewControllerManager GetManager()
		{
			return null;
		}

		// Token: 0x06004D3D RID: 19773 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Awake()
		{
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnDestroy()
		{
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Update()
		{
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFadeColorDefault()
		{
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFadeColor(Color color)
		{
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x06004D44 RID: 19780 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x0000216D File Offset: 0x0000036D
		private void SendStackActionAction(ViewController.TransitionType type, ViewController vc, ViewController preVc)
		{
		}

		// Token: 0x04008AD6 RID: 35542
		public static DialogViewControllerManager Instance;

		// Token: 0x04008AD7 RID: 35543
		public const float FADESPEED = 0.2f;

		// Token: 0x04008AD8 RID: 35544
		public Image fillImage;

		// Token: 0x04008AD9 RID: 35545
		public BokehCamera bokeh;

		// Token: 0x04008ADA RID: 35546
		public GameObject hiddenScreen;

		// Token: 0x04008ADB RID: 35547
		public Color defaultFadeColor;

		// Token: 0x04008ADC RID: 35548
		private Color fadeColor;

		// Token: 0x04008ADD RID: 35549
		private float disp;

		// Token: 0x04008ADE RID: 35550
		private bool touchMask;

		// Token: 0x04008ADF RID: 35551
		private bool pauseContent;

		// Token: 0x04008AE0 RID: 35552
		[SerializeField]
		private GraphicRaycaster[] maskRaycasterList;
	}
}
