using System;

// Token: 0x0200005A RID: 90
public abstract class USndPlugin_abstract
{
	// Token: 0x060001A4 RID: 420
	public abstract void Init();

	// Token: 0x060001A5 RID: 421
	public abstract void Delete();

	// Token: 0x060001A6 RID: 422
	public abstract bool IsMusicPlaying();

	// Token: 0x060001A7 RID: 423
	public abstract bool IsOtherAudioPlaying();

	// Token: 0x060001A8 RID: 424
	public abstract bool IsSpeaker();

	// Token: 0x060001A9 RID: 425
	public abstract bool IsMannerMode();

	// Token: 0x060001AA RID: 426
	public abstract void SetAudioFocus();
}
