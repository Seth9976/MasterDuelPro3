using System;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class USndPlugin : MonoBehaviour
{
	// Token: 0x0600019A RID: 410 RVA: 0x0000216D File Offset: 0x0000036D
	public static void Init(string objName, string funcName)
	{
	}

	// Token: 0x0600019B RID: 411 RVA: 0x0000216D File Offset: 0x0000036D
	public static void UpdateAndroidMusicStatus()
	{
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0000216D File Offset: 0x0000036D
	private void OnDestroy()
	{
	}

	// Token: 0x0600019D RID: 413 RVA: 0x0000216D File Offset: 0x0000036D
	private void OnApplicationFocus(bool focus)
	{
	}

	// Token: 0x0600019E RID: 414 RVA: 0x000029CC File Offset: 0x00000BCC
	public static bool IsMusicPlaying()
	{
		return false;
	}

	// Token: 0x0600019F RID: 415 RVA: 0x000029CC File Offset: 0x00000BCC
	public static bool IsOtherAudioPlaying()
	{
		return false;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x000029CC File Offset: 0x00000BCC
	public static bool IsSpeaker()
	{
		return false;
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x000029CC File Offset: 0x00000BCC
	public static bool IsMannerMode()
	{
		return false;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x0000216D File Offset: 0x0000036D
	public static void SetAudioFocus()
	{
	}

	// Token: 0x04000220 RID: 544
	public static bool isSetAudioFocus;

	// Token: 0x04000221 RID: 545
	private static USndPlugin_abstract plugin;
}
