using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

// Token: 0x02000017 RID: 23
public class CardPictureSystemCheckViewController : ViewController, IGenericScrollViewSupport
{
	// Token: 0x06000044 RID: 68 RVA: 0x0000216D File Offset: 0x0000036D
	private void Start()
	{
	}

	// Token: 0x06000045 RID: 69 RVA: 0x0000216D File Offset: 0x0000036D
	private void OnReady()
	{
	}

	// Token: 0x06000046 RID: 70 RVA: 0x0000216D File Offset: 0x0000036D
	private void Update()
	{
	}

	// Token: 0x06000047 RID: 71 RVA: 0x0000216D File Offset: 0x0000036D
	private void OnSelectMode(CardPictureSystemCheckViewController.Mode mode)
	{
	}

	// Token: 0x06000048 RID: 72 RVA: 0x000029C5 File Offset: 0x00000BC5
	private float GetSimpleScrollValue()
	{
		return 0f;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000029C5 File Offset: 0x00000BC5
	private float GetRandomScrollValue()
	{
		return 0f;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x0000216D File Offset: 0x0000036D
	public void OnItemSetData(GameObject gob, int dataindex)
	{
	}

	// Token: 0x0600004B RID: 75 RVA: 0x0000216D File Offset: 0x0000036D
	public void OnItemExit(GameObject gob, int dataindex)
	{
	}

	// Token: 0x0600004C RID: 76 RVA: 0x0000216D File Offset: 0x0000036D
	public void OnItemInitialize(GameObject gob)
	{
	}

	// Token: 0x0600004D RID: 77 RVA: 0x0000216D File Offset: 0x0000036D
	public void OnGsvStanby()
	{
	}

	// Token: 0x04000030 RID: 48
	private bool m_Start;

	// Token: 0x04000031 RID: 49
	private GenericScrollView m_Gsv;

	// Token: 0x04000032 RID: 50
	private ElementObjectManager m_RootEom;

	// Token: 0x04000033 RID: 51
	private ElementObjectManager m_CommonRootEom;

	// Token: 0x04000034 RID: 52
	private ElementObjectManager m_CardRootEom;

	// Token: 0x04000035 RID: 53
	private ElementObjectManager m_IllustRootEom;

	// Token: 0x04000036 RID: 54
	private ElementObjectManager m_RandomScrollSliderEom;

	// Token: 0x04000037 RID: 55
	private ElementObjectManager m_SimpleScrollSliderEom;

	// Token: 0x04000038 RID: 56
	private List<int> m_DataList;

	// Token: 0x04000039 RID: 57
	private Button m_IllustModeButton;

	// Token: 0x0400003A RID: 58
	private Button m_CardModeButton;

	// Token: 0x0400003B RID: 59
	private CardPictureSystemCheckViewController.Mode m_Mode;

	// Token: 0x0400003C RID: 60
	private Slider m_RandomScrollSlider;

	// Token: 0x0400003D RID: 61
	private Slider m_SimpleScrollSlider;

	// Token: 0x02000018 RID: 24
	private enum Mode
	{
		// Token: 0x0400003F RID: 63
		Card,
		// Token: 0x04000040 RID: 64
		Illust
	}
}
