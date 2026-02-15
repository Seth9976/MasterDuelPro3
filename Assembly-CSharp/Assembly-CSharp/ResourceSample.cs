using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200004B RID: 75
public class ResourceSample : MonoBehaviour
{
	// Token: 0x0600013A RID: 314 RVA: 0x0000216D File Offset: 0x0000036D
	private void Start()
	{
	}

	// Token: 0x0600013B RID: 315 RVA: 0x0000216D File Offset: 0x0000036D
	private void OnClick()
	{
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0000216A File Offset: 0x0000036A
	private IEnumerator yLoadAsync()
	{
		return null;
	}

	// Token: 0x040001CF RID: 463
	[SerializeField]
	private string m_loadPath;

	// Token: 0x040001D0 RID: 464
	[SerializeField]
	private string m_loadAsyncPath;

	// Token: 0x040001D1 RID: 465
	[SerializeField]
	private string m_loadRawImageAsyncPath;

	// Token: 0x040001D2 RID: 466
	[SerializeField]
	private Image m_image;

	// Token: 0x040001D3 RID: 467
	[SerializeField]
	private Image m_asyncImage;

	// Token: 0x040001D4 RID: 468
	[SerializeField]
	private RawImage m_rawImage;

	// Token: 0x040001D5 RID: 469
	[SerializeField]
	private Button m_clickButton;
}
