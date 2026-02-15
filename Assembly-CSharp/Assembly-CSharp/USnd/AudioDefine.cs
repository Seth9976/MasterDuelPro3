using System;

namespace USnd
{
	// Token: 0x0200117D RID: 4477
	public class AudioDefine
	{
		// Token: 0x0400C05D RID: 49245
		public static readonly int DEFAULT_SAMPLE_RATE;

		// Token: 0x0400C05E RID: 49246
		public static readonly bool ANDROID_MANNER_MODE_MUTE;

		// Token: 0x0400C05F RID: 49247
		public static readonly float DEFAULT_VOLUME;

		// Token: 0x0400C060 RID: 49248
		public static readonly float DEFAULT_PAN;

		// Token: 0x0400C061 RID: 49249
		public static readonly int DEFAULT_PITCH;

		// Token: 0x0400C062 RID: 49250
		public static readonly int DEFAULT_FADE;

		// Token: 0x0400C063 RID: 49251
		public static readonly int INSTANCE_ID_ERROR;

		// Token: 0x0400C064 RID: 49252
		public static readonly float VOLUME_MAX;

		// Token: 0x0400C065 RID: 49253
		public static readonly float VOLUME_MIN;

		// Token: 0x0400C066 RID: 49254
		public static readonly float PAN_LEFT;

		// Token: 0x0400C067 RID: 49255
		public static readonly float PAN_RIGHT;

		// Token: 0x0400C068 RID: 49256
		public static readonly float PAN_CENTER;

		// Token: 0x0400C069 RID: 49257
		public static readonly int PITCH_MAX;

		// Token: 0x0400C06A RID: 49258
		public static readonly int PITCH_MIN;

		// Token: 0x0400C06B RID: 49259
		public static readonly int PITCH_NORMAL;

		// Token: 0x0400C06C RID: 49260
		public static readonly int TABLE_UPPER_VERSION;

		// Token: 0x0400C06D RID: 49261
		public static readonly int TABLE_LOWER_VERSION;

		// Token: 0x0400C06E RID: 49262
		public static readonly int TABLE_ADD_IS_ANDROID_NATIVE_VERSION;

		// Token: 0x0400C06F RID: 49263
		public static readonly int TABLE_ADD_INTERVAL_VERSION;

		// Token: 0x0400C070 RID: 49264
		public static readonly int LIST_CAPACITY;

		// Token: 0x0400C071 RID: 49265
		public static readonly float[] PITCH_VALUES;

		// Token: 0x0200117E RID: 4478
		public enum INSTANCE_STATUS
		{
			// Token: 0x0400C073 RID: 49267
			STOP,
			// Token: 0x0400C074 RID: 49268
			STOP_SOON,
			// Token: 0x0400C075 RID: 49269
			PREPARE,
			// Token: 0x0400C076 RID: 49270
			PLAY,
			// Token: 0x0400C077 RID: 49271
			PAUSE,
			// Token: 0x0400C078 RID: 49272
			PAUSE_SOON
		}

		// Token: 0x0200117F RID: 4479
		public enum LOAD_XML_STATUS
		{
			// Token: 0x0400C07A RID: 49274
			STANDBY,
			// Token: 0x0400C07B RID: 49275
			LOADING,
			// Token: 0x0400C07C RID: 49276
			FINISH,
			// Token: 0x0400C07D RID: 49277
			ERROR
		}

		// Token: 0x02001180 RID: 4480
		public enum LOAD_JSON_STATUS
		{
			// Token: 0x0400C07F RID: 49279
			STANDBY,
			// Token: 0x0400C080 RID: 49280
			LOADING,
			// Token: 0x0400C081 RID: 49281
			FINISH,
			// Token: 0x0400C082 RID: 49282
			ERROR
		}
	}
}
