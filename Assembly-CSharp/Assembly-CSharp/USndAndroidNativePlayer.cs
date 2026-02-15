using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
public class USndAndroidNativePlayer
{
	// Token: 0x06000193 RID: 403 RVA: 0x0000216D File Offset: 0x0000036D
	public static void Initialize(int maxNum)
	{
	}

	// Token: 0x06000194 RID: 404 RVA: 0x0000216D File Offset: 0x0000036D
	public static void Terminate()
	{
	}

	// Token: 0x06000195 RID: 405 RVA: 0x000029CC File Offset: 0x00000BCC
	public static int LoadData(string saveName, string className, string funcName)
	{
		return 0;
	}

	// Token: 0x06000196 RID: 406 RVA: 0x000029CC File Offset: 0x00000BCC
	public static int Play(int soundId, float volume, float rate)
	{
		return 0;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x0000216D File Offset: 0x0000036D
	public static void Stop(int streamId)
	{
	}

	// Token: 0x06000198 RID: 408 RVA: 0x0000216D File Offset: 0x0000036D
	public static void Unload(int soundId)
	{
	}

	// Token: 0x0400021E RID: 542
	private static USndAndroidNativePlayer player;

	// Token: 0x0400021F RID: 543
	private static AndroidJavaObject plugin;
}
