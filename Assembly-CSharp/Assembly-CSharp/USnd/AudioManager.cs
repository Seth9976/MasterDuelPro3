using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Audio;

namespace USnd
{
	// Token: 0x02001181 RID: 4481
	public class AudioManager : MonoBehaviour
	{
		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x060084C7 RID: 33991 RVA: 0x0000216A File Offset: 0x0000036A
		private Transform CacheTransform
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060084C8 RID: 33992 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsInitialized()
		{
			return false;
		}

		// Token: 0x060084C9 RID: 33993 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize(int defaultSampleRate = 0)
		{
		}

		// Token: 0x060084CA RID: 33994 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Terminate()
		{
		}

		// Token: 0x060084CB RID: 33995 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetAudioMixer(AudioMixer mixer)
		{
		}

		// Token: 0x060084CC RID: 33996 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnsetAudioMixer()
		{
		}

		// Token: 0x060084CD RID: 33997 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetSnapshot(string snapName, float time)
		{
		}

		// Token: 0x060084CE RID: 33998 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetAudioMixerExposedParam(string paramName, float value)
		{
		}

		// Token: 0x060084CF RID: 33999 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetAudio3DSettingsFromJson(string jsonStr)
		{
		}

		// Token: 0x060084D0 RID: 34000 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetAudio3DSettings(Audio3DSettings setting)
		{
		}

		// Token: 0x060084D1 RID: 34001 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetAudio3DSettings(Audio3DSettings[] settings)
		{
		}

		// Token: 0x060084D2 RID: 34002 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool LoadBinaryTable(byte[] tableData, int loadId = 0)
		{
			return false;
		}

		// Token: 0x060084D3 RID: 34003 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool LoadJson(string tableData, int loadId = 0)
		{
			return false;
		}

		// Token: 0x060084D4 RID: 34004 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddAudioClip(AudioClip[] clips)
		{
		}

		// Token: 0x060084D5 RID: 34005 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddAudioClip(AudioClip clip)
		{
		}

		// Token: 0x060084D6 RID: 34006 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsExistAudioClip(string clipName)
		{
			return false;
		}

		// Token: 0x060084D7 RID: 34007 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RemoveAudioClip(string clipName)
		{
		}

		// Token: 0x060084D8 RID: 34008 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RemoveAudioClipAll()
		{
		}

		// Token: 0x060084D9 RID: 34009 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool FindLabel(string name)
		{
			return false;
		}

		// Token: 0x060084DA RID: 34010 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool FindCategory(string name)
		{
			return false;
		}

		// Token: 0x060084DB RID: 34011 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool FindMaster(string name)
		{
			return false;
		}

		// Token: 0x060084DC RID: 34012 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CanRemoveLabel(string labelName)
		{
			return false;
		}

		// Token: 0x060084DD RID: 34013 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool UnsetAudioClipToLabel(string labelName)
		{
			return false;
		}

		// Token: 0x060084DE RID: 34014 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnsetAudioClipToLabelLoadId(int loadId)
		{
		}

		// Token: 0x060084DF RID: 34015 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnsetAudioClipToLabelAll()
		{
		}

		// Token: 0x060084E0 RID: 34016 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool RemoveLabel(string labelName)
		{
			return false;
		}

		// Token: 0x060084E1 RID: 34017 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RemoveLabelLoadId(int loadId)
		{
		}

		// Token: 0x060084E2 RID: 34018 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RemoveLabelAll()
		{
		}

		// Token: 0x060084E3 RID: 34019 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RemoveAll()
		{
		}

		// Token: 0x060084E4 RID: 34020 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UpdateRandomSourceInfo(string labelName)
		{
		}

		// Token: 0x060084E5 RID: 34021 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UpdateRandomSourceInfoAll()
		{
		}

		// Token: 0x060084E6 RID: 34022 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadAudioData(string labelName)
		{
		}

		// Token: 0x060084E7 RID: 34023 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadAudioDataLoadId(int loadId)
		{
		}

		// Token: 0x060084E8 RID: 34024 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadAudioData(string labelName)
		{
		}

		// Token: 0x060084E9 RID: 34025 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadAudioDataAll()
		{
		}

		// Token: 0x060084EA RID: 34026 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadAudioDataLoadId(int loadId)
		{
		}

		// Token: 0x060084EB RID: 34027 RVA: 0x000029CC File Offset: 0x00000BCC
		public static AudioDefine.LOAD_XML_STATUS GetLoadXmlStatus()
		{
			return AudioDefine.LOAD_XML_STATUS.STANDBY;
		}

		// Token: 0x060084EC RID: 34028 RVA: 0x000029CC File Offset: 0x00000BCC
		public static AudioDefine.LOAD_JSON_STATUS GetLoadJsonStatus()
		{
			return AudioDefine.LOAD_JSON_STATUS.STANDBY;
		}

		// Token: 0x060084ED RID: 34029 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool LoadMasterXml(Stream xml, Stream xsd = null)
		{
			return false;
		}

		// Token: 0x060084EE RID: 34030 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool LoadCategoryXml(Stream xml, Stream xsd = null)
		{
			return false;
		}

		// Token: 0x060084EF RID: 34031 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool LoadLabelXml(int loadId, Stream xml, Stream xsd = null)
		{
			return false;
		}

		// Token: 0x060084F0 RID: 34032 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetDucking(string categoryName, float targetVolumeFactor, float fadeTime)
		{
		}

		// Token: 0x060084F1 RID: 34033 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetDucking(string categoryName, float fadeTime)
		{
		}

		// Token: 0x060084F2 RID: 34034 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetDuckingAll(float fadeTime)
		{
		}

		// Token: 0x060084F3 RID: 34035 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ForceResetDucking(string categoryName, float fadeTime)
		{
		}

		// Token: 0x060084F4 RID: 34036 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ForceResetDuckingAll(float fadeTime)
		{
		}

		// Token: 0x060084F5 RID: 34037 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Play(string labelName, float delay = -1f)
		{
			return 0;
		}

		// Token: 0x060084F6 RID: 34038 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int PlayOption(string labelName, float volume, float fadeTime, float pan, int pitch, float delay = -1f)
		{
			return 0;
		}

		// Token: 0x060084F7 RID: 34039 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Prepare(string labelName)
		{
			return 0;
		}

		// Token: 0x060084F8 RID: 34040 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int PrepareOption(string labelName, float volume, float fadeTime, float pan, int pitch)
		{
			return 0;
		}

		// Token: 0x060084F9 RID: 34041 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PlayInstance(int instanceId, float delay = -1f)
		{
		}

		// Token: 0x060084FA RID: 34042 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Play3D(string labelName, GameObject target, float delay = -1f)
		{
			return 0;
		}

		// Token: 0x060084FB RID: 34043 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Play3D(string labelName, Vector3 position, float delay = -1f)
		{
			return 0;
		}

		// Token: 0x060084FC RID: 34044 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Play3D(string labelName, Transform target, float delay = -1f)
		{
			return 0;
		}

		// Token: 0x060084FD RID: 34045 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Play2D(string labelName, float delay = -1f)
		{
			return 0;
		}

		// Token: 0x060084FE RID: 34046 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetTrackingObject(int instanceId, GameObject target)
		{
		}

		// Token: 0x060084FF RID: 34047 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetTrackingObject(int instanceId, Transform target)
		{
		}

		// Token: 0x06008500 RID: 34048 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Stop(int instanceId, float fadeTime = -1f)
		{
		}

		// Token: 0x06008501 RID: 34049 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StopLabel(string labelName, float fadeTime = -1f)
		{
		}

		// Token: 0x06008502 RID: 34050 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StopAll(float fadeTime = -1f)
		{
		}

		// Token: 0x06008503 RID: 34051 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OnPause(int instanceId, float fadeTime = -1f)
		{
		}

		// Token: 0x06008504 RID: 34052 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OnPauseAll(float fadeTime = -1f)
		{
		}

		// Token: 0x06008505 RID: 34053 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OffPause(int instanceId, float fadeTime = -1f)
		{
		}

		// Token: 0x06008506 RID: 34054 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OffPauseAll(float fadeTime = -1f)
		{
		}

		// Token: 0x06008507 RID: 34055 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetVolume(int instanceId, float newVolume, float moveTime)
		{
		}

		// Token: 0x06008508 RID: 34056 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetVolume(string labelName, float newVolume, float moveTime)
		{
		}

		// Token: 0x06008509 RID: 34057 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPitch(int instanceId, int newPitch, float moveTime)
		{
		}

		// Token: 0x0600850A RID: 34058 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPitch(string labelName, int newPitch, float moveTime)
		{
		}

		// Token: 0x0600850B RID: 34059 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPan(int instanceId, float newPan, float moveTime)
		{
		}

		// Token: 0x0600850C RID: 34060 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPan(string labelName, float newPan, float moveTime)
		{
		}

		// Token: 0x0600850D RID: 34061 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPosition(int instanceId, Vector3 position)
		{
		}

		// Token: 0x0600850E RID: 34062 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPosition(string labelName, Vector3 position)
		{
		}

		// Token: 0x0600850F RID: 34063 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetPlayPosition(string labelName)
		{
		}

		// Token: 0x06008510 RID: 34064 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetPlayPositionAll()
		{
		}

		// Token: 0x06008511 RID: 34065 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float GetInstanceVolume(int instanceId)
		{
			return 0f;
		}

		// Token: 0x06008512 RID: 34066 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float GetInstanceCalcVolume(int instanceId)
		{
			return 0f;
		}

		// Token: 0x06008513 RID: 34067 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetMasterVolume(string masterName, float volume, float moveTime = 0f)
		{
		}

		// Token: 0x06008514 RID: 34068 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float GetMasterVolume(string masterName)
		{
			return 0f;
		}

		// Token: 0x06008515 RID: 34069 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetCategoryVolume(string categoryName, float volume, float moveTime = 0f)
		{
		}

		// Token: 0x06008516 RID: 34070 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float GetCategoryVolume(string categoryName)
		{
			return 0f;
		}

		// Token: 0x06008517 RID: 34071 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float GetLabelVolume(string labelName)
		{
			return 0f;
		}

		// Token: 0x06008518 RID: 34072 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StopMaster(string masterName, float fadeTime = -1f)
		{
		}

		// Token: 0x06008519 RID: 34073 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OnPauseMaster(string masterName, float fadeTime = -1f)
		{
		}

		// Token: 0x0600851A RID: 34074 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OffPauseMaster(string masterName, float fadeTime = -1f)
		{
		}

		// Token: 0x0600851B RID: 34075 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StopCategory(string categoryName, float fadeTime = -1f)
		{
		}

		// Token: 0x0600851C RID: 34076 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OnPauseLabel(string labelName, float fadeTime = -1f)
		{
		}

		// Token: 0x0600851D RID: 34077 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OffPauseLabel(string labelName, float fadeTime = -1f)
		{
		}

		// Token: 0x0600851E RID: 34078 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OnPauseCategory(string categoryName, float fadeTime = -1f)
		{
		}

		// Token: 0x0600851F RID: 34079 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OffPauseCategory(string categoryName, float fadeTime = -1f)
		{
		}

		// Token: 0x06008520 RID: 34080 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OffPauseCategory(string categoryName, List<int> instanceList, float fadeTime = -1f)
		{
		}

		// Token: 0x06008521 RID: 34081 RVA: 0x000029CC File Offset: 0x00000BCC
		public static AudioDefine.INSTANCE_STATUS GetInstanceStatus(int instanceId)
		{
			return AudioDefine.INSTANCE_STATUS.STOP;
		}

		// Token: 0x06008522 RID: 34082 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsPlayingLabel(string labelName)
		{
			return false;
		}

		// Token: 0x06008523 RID: 34083 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetLabelNum()
		{
			return 0;
		}

		// Token: 0x06008524 RID: 34084 RVA: 0x0000216A File Offset: 0x0000036A
		public static string[] GetLabelNameList()
		{
			return null;
		}

		// Token: 0x06008525 RID: 34085 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCategoryNum()
		{
			return 0;
		}

		// Token: 0x06008526 RID: 34086 RVA: 0x0000216A File Offset: 0x0000036A
		public static string[] GetCategoryNameList()
		{
			return null;
		}

		// Token: 0x06008527 RID: 34087 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetMasterNum()
		{
			return 0;
		}

		// Token: 0x06008528 RID: 34088 RVA: 0x0000216A File Offset: 0x0000036A
		public static string[] GetMasterNameList()
		{
			return null;
		}

		// Token: 0x06008529 RID: 34089 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetCategoryNameSettingOfLabel(string labelName)
		{
			return null;
		}

		// Token: 0x0600852A RID: 34090 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetMasterNameSettingOfCategory(string categoryName)
		{
			return null;
		}

		// Token: 0x0600852B RID: 34091 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float GetPlayTime(int instanceId)
		{
			return 0f;
		}

		// Token: 0x0600852C RID: 34092 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetPlaySamples(int instanceId)
		{
			return 0;
		}

		// Token: 0x0600852D RID: 34093 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetTime(int instanceId, float time)
		{
		}

		// Token: 0x0600852E RID: 34094 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetTimeSamples(int instanceId, int samples)
		{
		}

		// Token: 0x0600852F RID: 34095 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetMute(bool onMute)
		{
		}

		// Token: 0x06008530 RID: 34096 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetMuteStatus()
		{
			return false;
		}

		// Token: 0x06008531 RID: 34097 RVA: 0x0000216A File Offset: 0x0000036A
		public static string[] GetAudioClipNameLoadId(int loadId)
		{
			return null;
		}

		// Token: 0x06008532 RID: 34098 RVA: 0x0000216A File Offset: 0x0000036A
		public static string[] GetAudioClipNameAll()
		{
			return null;
		}

		// Token: 0x06008533 RID: 34099 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetAudioClipName(string labelName)
		{
			return null;
		}

		// Token: 0x06008534 RID: 34100 RVA: 0x0000216A File Offset: 0x0000036A
		public static string[] GetAudioClipNames(string labelName)
		{
			return null;
		}

		// Token: 0x06008535 RID: 34101 RVA: 0x0000216A File Offset: 0x0000036A
		public static string[] GetRandomSourceNames(string labelName)
		{
			return null;
		}

		// Token: 0x06008536 RID: 34102 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetAudioClipToLabelLoadId(int loadId)
		{
		}

		// Token: 0x06008537 RID: 34103 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetAudioClipToLabelAll()
		{
		}

		// Token: 0x06008538 RID: 34104 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetAudioClipToLabel(string labelName)
		{
		}

		// Token: 0x06008539 RID: 34105 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetAndroidNativeToLabel(string labelName, string filePath, string className, string funcName)
		{
		}

		// Token: 0x0600853A RID: 34106 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearObjectPool()
		{
		}

		// Token: 0x0600853B RID: 34107 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float GetLabelLength(string labelName)
		{
			return 0f;
		}

		// Token: 0x0600853C RID: 34108 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetLabelSamples(string labelName)
		{
			return 0;
		}

		// Token: 0x0600853D RID: 34109 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetSpectrumData(int instanceId, float[] sample, int channel, FFTWindow window)
		{
			return false;
		}

		// Token: 0x0600853E RID: 34110 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsLoop(string labelName)
		{
			return false;
		}

		// Token: 0x0600853F RID: 34111 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetLabelMaxPlaybacksNum(string labelName)
		{
			return 0;
		}

		// Token: 0x06008540 RID: 34112 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCategoryMaxPlaybacksNum(string categoryName)
		{
			return 0;
		}

		// Token: 0x06008541 RID: 34113 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCategoryMaxPlaybacksNumFromLabel(string labelName)
		{
			return 0;
		}

		// Token: 0x06008542 RID: 34114 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsInterval(string labelName)
		{
			return false;
		}

		// Token: 0x06008543 RID: 34115 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCurrentPlayNum()
		{
			return 0;
		}

		// Token: 0x06008544 RID: 34116 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06008545 RID: 34117 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnApplicationPause(bool status)
		{
		}

		// Token: 0x06008546 RID: 34118 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnApplicationFocus(bool status)
		{
		}

		// Token: 0x06008547 RID: 34119 RVA: 0x0000216D File Offset: 0x0000036D
		private void onHeadsetPlugCallback(string status)
		{
		}

		// Token: 0x06008548 RID: 34120 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMannerMode(bool onMute)
		{
		}

		// Token: 0x06008549 RID: 34121 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMannerMode()
		{
		}

		// Token: 0x0600854A RID: 34122 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x0600854B RID: 34123 RVA: 0x0000216D File Offset: 0x0000036D
		public void setAudioMixer(AudioMixer mixer)
		{
		}

		// Token: 0x0600854C RID: 34124 RVA: 0x0000216D File Offset: 0x0000036D
		public void unsetAudioMixer()
		{
		}

		// Token: 0x0600854D RID: 34125 RVA: 0x0000216D File Offset: 0x0000036D
		public void setSnapshot(string snapName, float time)
		{
		}

		// Token: 0x0600854E RID: 34126 RVA: 0x0000216D File Offset: 0x0000036D
		public void setAudioMixerExposedParam(string paramName, float value)
		{
		}

		// Token: 0x0600854F RID: 34127 RVA: 0x0000216D File Offset: 0x0000036D
		public void setAudio3DSettingsFromJson(string jsonStr)
		{
		}

		// Token: 0x06008550 RID: 34128 RVA: 0x0000216D File Offset: 0x0000036D
		public void setAudio3DSettings(Audio3DSettings setting)
		{
		}

		// Token: 0x06008551 RID: 34129 RVA: 0x0000216D File Offset: 0x0000036D
		public void setAudio3DSettings(Audio3DSettings[] settings)
		{
		}

		// Token: 0x06008552 RID: 34130 RVA: 0x0000216A File Offset: 0x0000036A
		private string getChunk(byte[] tableData, int startIndex)
		{
			return null;
		}

		// Token: 0x06008553 RID: 34131 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool loadBinaryTable(byte[] tableData, int loadId)
		{
			return false;
		}

		// Token: 0x06008554 RID: 34132 RVA: 0x0000216A File Offset: 0x0000036A
		private string getString(byte[] tableData, ref int startIndex)
		{
			return null;
		}

		// Token: 0x06008555 RID: 34133 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool loadJson(string tableData, int loadId)
		{
			return false;
		}

		// Token: 0x06008556 RID: 34134 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator loadJsonImpl(string tableData, int loadId)
		{
			return null;
		}

		// Token: 0x06008557 RID: 34135 RVA: 0x0000216D File Offset: 0x0000036D
		private void jsonParse()
		{
		}

		// Token: 0x06008558 RID: 34136 RVA: 0x0000216A File Offset: 0x0000036A
		private T[] MasterFromJson<T>(string json)
		{
			return null;
		}

		// Token: 0x06008559 RID: 34137 RVA: 0x0000216A File Offset: 0x0000036A
		private T[] CategoryFromJson<T>(string json)
		{
			return null;
		}

		// Token: 0x0600855A RID: 34138 RVA: 0x0000216A File Offset: 0x0000036A
		private T[] LabelFromJson<T>(string json)
		{
			return null;
		}

		// Token: 0x0600855B RID: 34139 RVA: 0x0000216A File Offset: 0x0000036A
		private Audio3DSettings D3SettingsFromJson(string json)
		{
			return null;
		}

		// Token: 0x0600855C RID: 34140 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool loadLabelJson(int loadId)
		{
			return false;
		}

		// Token: 0x0600855D RID: 34141 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool loadMasterBinary(byte[] tableData, ref int startIndex)
		{
			return false;
		}

		// Token: 0x0600855E RID: 34142 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool loadCategoryBinary(byte[] tableData, ref int startIndex)
		{
			return false;
		}

		// Token: 0x0600855F RID: 34143 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool loadLabelBinary(byte[] tableData, ref int startIndex, int loadId, int tableVer)
		{
			return false;
		}

		// Token: 0x06008560 RID: 34144 RVA: 0x0000216D File Offset: 0x0000036D
		public void addCategorySettings(AudioManager.AudioCategorySettings[] list)
		{
		}

		// Token: 0x06008561 RID: 34145 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool addCategorySettings(AudioManager.AudioCategorySettings category)
		{
			return false;
		}

		// Token: 0x06008562 RID: 34146 RVA: 0x0000216D File Offset: 0x0000036D
		public void addMasterSettings(AudioManager.AudioMasterSettings[] list)
		{
		}

		// Token: 0x06008563 RID: 34147 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool addMasterSettings(AudioManager.AudioMasterSettings master)
		{
			return false;
		}

		// Token: 0x06008564 RID: 34148 RVA: 0x0000216D File Offset: 0x0000036D
		public void addAudioClip(AudioClip[] clips)
		{
		}

		// Token: 0x06008565 RID: 34149 RVA: 0x0000216D File Offset: 0x0000036D
		public void addAudioClip(AudioClip clip)
		{
		}

		// Token: 0x06008566 RID: 34150 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isExistAudioClip(string clipName)
		{
			return false;
		}

		// Token: 0x06008567 RID: 34151 RVA: 0x0000216D File Offset: 0x0000036D
		public void removeAudioClip(string clipName)
		{
		}

		// Token: 0x06008568 RID: 34152 RVA: 0x0000216D File Offset: 0x0000036D
		public void removeAudioClipAll()
		{
		}

		// Token: 0x06008569 RID: 34153 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool findLabel(string name)
		{
			return false;
		}

		// Token: 0x0600856A RID: 34154 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool findCategory(string name)
		{
			return false;
		}

		// Token: 0x0600856B RID: 34155 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool findMaster(string name)
		{
			return false;
		}

		// Token: 0x0600856C RID: 34156 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool canRemoveLabel(string labelName)
		{
			return false;
		}

		// Token: 0x0600856D RID: 34157 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool unsetAudioClipToLabel(string labelName)
		{
			return false;
		}

		// Token: 0x0600856E RID: 34158 RVA: 0x0000216D File Offset: 0x0000036D
		public void unsetAudioClipToLabelLoadId(int loadId)
		{
		}

		// Token: 0x0600856F RID: 34159 RVA: 0x0000216D File Offset: 0x0000036D
		public void unsetAudioClipToLabelAll()
		{
		}

		// Token: 0x06008570 RID: 34160 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool removeLabel(string labelName)
		{
			return false;
		}

		// Token: 0x06008571 RID: 34161 RVA: 0x0000216D File Offset: 0x0000036D
		public void removeLabelLoadId(int loadId)
		{
		}

		// Token: 0x06008572 RID: 34162 RVA: 0x0000216D File Offset: 0x0000036D
		public void removeLabelAll()
		{
		}

		// Token: 0x06008573 RID: 34163 RVA: 0x0000216D File Offset: 0x0000036D
		public void removeAll()
		{
		}

		// Token: 0x06008574 RID: 34164 RVA: 0x0000216D File Offset: 0x0000036D
		private void deleteAudioSource(AudioManager.AudioPlayer player)
		{
		}

		// Token: 0x06008575 RID: 34165 RVA: 0x0000216D File Offset: 0x0000036D
		public void updateRandomSourceInfo(string labelName)
		{
		}

		// Token: 0x06008576 RID: 34166 RVA: 0x0000216D File Offset: 0x0000036D
		public void updateRandomSourceInfoAll()
		{
		}

		// Token: 0x06008577 RID: 34167 RVA: 0x0000216D File Offset: 0x0000036D
		public void loadAudioData(string labelName)
		{
		}

		// Token: 0x06008578 RID: 34168 RVA: 0x0000216D File Offset: 0x0000036D
		public void loadAudioDataLoadId(int loadId)
		{
		}

		// Token: 0x06008579 RID: 34169 RVA: 0x0000216D File Offset: 0x0000036D
		public void unloadAudioData(string labelName)
		{
		}

		// Token: 0x0600857A RID: 34170 RVA: 0x0000216D File Offset: 0x0000036D
		public void unloadAudioDataAll()
		{
		}

		// Token: 0x0600857B RID: 34171 RVA: 0x0000216D File Offset: 0x0000036D
		public void unloadAudioDataLoadId(int loadId)
		{
		}

		// Token: 0x0600857C RID: 34172 RVA: 0x0000216D File Offset: 0x0000036D
		private void setUnityAudioMixer(AudioManager.AudioPlayer player)
		{
		}

		// Token: 0x0600857D RID: 34173 RVA: 0x000029CC File Offset: 0x00000BCC
		public AudioDefine.LOAD_XML_STATUS getLoadXmlStatus()
		{
			return AudioDefine.LOAD_XML_STATUS.STANDBY;
		}

		// Token: 0x0600857E RID: 34174 RVA: 0x000029CC File Offset: 0x00000BCC
		public AudioDefine.LOAD_JSON_STATUS getLoadJsonStatus()
		{
			return AudioDefine.LOAD_JSON_STATUS.STANDBY;
		}

		// Token: 0x0600857F RID: 34175 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool loadMasterXml(Stream xml, Stream xsd = null)
		{
			return false;
		}

		// Token: 0x06008580 RID: 34176 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator loadMasterXmlCoroutine(Stream xml, Stream xsd)
		{
			return null;
		}

		// Token: 0x06008581 RID: 34177 RVA: 0x0000216D File Offset: 0x0000036D
		private void attachMasterSettings(AudioManager.AudioCategorySettings category)
		{
		}

		// Token: 0x06008582 RID: 34178 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool loadCategoryXml(Stream xml, Stream xsd = null)
		{
			return false;
		}

		// Token: 0x06008583 RID: 34179 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator loadCategoryXmlCoroutine(Stream xml, Stream xsd)
		{
			return null;
		}

		// Token: 0x06008584 RID: 34180 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool loadLabelXml(int loadId, Stream xml, Stream xsd = null)
		{
			return false;
		}

		// Token: 0x06008585 RID: 34181 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator loadLabelXmlCoroutine(int loadId, Stream xml, Stream xsd)
		{
			return null;
		}

		// Token: 0x06008586 RID: 34182 RVA: 0x0000216D File Offset: 0x0000036D
		private void orderCategoryInstanceList(List<int> playerList)
		{
		}

		// Token: 0x06008587 RID: 34183 RVA: 0x0000216D File Offset: 0x0000036D
		private void addPlayInfo(AudioManager.AudioPlayer player, int instanceId)
		{
		}

		// Token: 0x06008588 RID: 34184 RVA: 0x0000216D File Offset: 0x0000036D
		private void stopSameSingleGroup(string singleGroup, string playLabelName)
		{
		}

		// Token: 0x06008589 RID: 34185 RVA: 0x000029CC File Offset: 0x00000BCC
		private AudioManager.RESULT checkLabelPlaybacksNum(AudioManager.AudioPlayer player)
		{
			return AudioManager.RESULT.CONTINUE;
		}

		// Token: 0x0600858A RID: 34186 RVA: 0x000029CC File Offset: 0x00000BCC
		private AudioManager.RESULT checkCategoryPlaybacksNum(AudioManager.AudioPlayer player, ref float time, ref bool queueOn)
		{
			return AudioManager.RESULT.CONTINUE;
		}

		// Token: 0x0600858B RID: 34187 RVA: 0x0000216D File Offset: 0x0000036D
		private void startDucking(AudioManager.AudioPlayer player, int instanceId)
		{
		}

		// Token: 0x0600858C RID: 34188 RVA: 0x0000216D File Offset: 0x0000036D
		public void setDucking(string categoryName, float targetVolumeFactor, float fadeTime)
		{
		}

		// Token: 0x0600858D RID: 34189 RVA: 0x0000216D File Offset: 0x0000036D
		public void resetDucking(string categoryName, float fadeTime)
		{
		}

		// Token: 0x0600858E RID: 34190 RVA: 0x0000216D File Offset: 0x0000036D
		public void resetDuckingAll(float fadeTime)
		{
		}

		// Token: 0x0600858F RID: 34191 RVA: 0x0000216D File Offset: 0x0000036D
		public void forceResetDucking(string categoryName, float fadeTime)
		{
		}

		// Token: 0x06008590 RID: 34192 RVA: 0x0000216D File Offset: 0x0000036D
		public void forceResetDuckingAll(float fadeTime)
		{
		}

		// Token: 0x06008591 RID: 34193 RVA: 0x000029CC File Offset: 0x00000BCC
		public int play(string labelName, float delay = -1f)
		{
			return 0;
		}

		// Token: 0x06008592 RID: 34194 RVA: 0x000029CC File Offset: 0x00000BCC
		private int prepareInstance(string labelName, float volume, float fadeTime, float pan, int pitch, float delay, ref AudioManager.AudioPlayer player, ref float time, ref bool queueOn, bool isForce2D)
		{
			return 0;
		}

		// Token: 0x06008593 RID: 34195 RVA: 0x000029CC File Offset: 0x00000BCC
		public int playOption(string labelName, float volume, float fadeTime, float pan, int pitch, float delay)
		{
			return 0;
		}

		// Token: 0x06008594 RID: 34196 RVA: 0x000029CC File Offset: 0x00000BCC
		public int prepare(string labelName, bool isForce2D = false)
		{
			return 0;
		}

		// Token: 0x06008595 RID: 34197 RVA: 0x000029CC File Offset: 0x00000BCC
		public int prepareOption(string labelName, float volume, float fadeTime, float pan, int pitch, bool isForce2D)
		{
			return 0;
		}

		// Token: 0x06008596 RID: 34198 RVA: 0x0000216D File Offset: 0x0000036D
		public void playInstance(int instanceId, float delay = -1f)
		{
		}

		// Token: 0x06008597 RID: 34199 RVA: 0x000029CC File Offset: 0x00000BCC
		public int play3D(string labelName, GameObject target, float delay = -1f)
		{
			return 0;
		}

		// Token: 0x06008598 RID: 34200 RVA: 0x000029CC File Offset: 0x00000BCC
		public int play3D(string labelName, Vector3 position, float delay = -1f)
		{
			return 0;
		}

		// Token: 0x06008599 RID: 34201 RVA: 0x000029CC File Offset: 0x00000BCC
		public int play3D(string labelName, Transform target, float delay = -1f)
		{
			return 0;
		}

		// Token: 0x0600859A RID: 34202 RVA: 0x000029CC File Offset: 0x00000BCC
		public int play2D(string labelName, float delay = -1f)
		{
			return 0;
		}

		// Token: 0x0600859B RID: 34203 RVA: 0x0000216D File Offset: 0x0000036D
		public void setTrackingObject(int instanceId, GameObject target)
		{
		}

		// Token: 0x0600859C RID: 34204 RVA: 0x0000216D File Offset: 0x0000036D
		public void setTrackingObject(int instanceId, Transform target)
		{
		}

		// Token: 0x0600859D RID: 34205 RVA: 0x0000216D File Offset: 0x0000036D
		public void stop(int instanceId, float fadeTime = -1f)
		{
		}

		// Token: 0x0600859E RID: 34206 RVA: 0x0000216D File Offset: 0x0000036D
		public void stopLabel(string labelName, float fadeTime = -1f)
		{
		}

		// Token: 0x0600859F RID: 34207 RVA: 0x0000216D File Offset: 0x0000036D
		public void stopAll(float fadeTime = -1f)
		{
		}

		// Token: 0x060085A0 RID: 34208 RVA: 0x0000216D File Offset: 0x0000036D
		public void onPause(int instanceId, float fadeTime = -1f)
		{
		}

		// Token: 0x060085A1 RID: 34209 RVA: 0x0000216D File Offset: 0x0000036D
		public void onPauseAll(float fadeTime = -1f)
		{
		}

		// Token: 0x060085A2 RID: 34210 RVA: 0x0000216D File Offset: 0x0000036D
		public void offPause(int instanceId, float fadeTime = -1f)
		{
		}

		// Token: 0x060085A3 RID: 34211 RVA: 0x0000216D File Offset: 0x0000036D
		public void offPauseAll(float fadeTime = -1f)
		{
		}

		// Token: 0x060085A4 RID: 34212 RVA: 0x0000216D File Offset: 0x0000036D
		public void setVolume(int instanceId, float newVolume, float moveTime)
		{
		}

		// Token: 0x060085A5 RID: 34213 RVA: 0x0000216D File Offset: 0x0000036D
		public void setVolume(string labelName, float newVolume, float moveTime)
		{
		}

		// Token: 0x060085A6 RID: 34214 RVA: 0x0000216D File Offset: 0x0000036D
		public void setPitch(int instanceId, int newPitch, float moveTime)
		{
		}

		// Token: 0x060085A7 RID: 34215 RVA: 0x0000216D File Offset: 0x0000036D
		public void setPitch(string labelName, int newPitch, float moveTime)
		{
		}

		// Token: 0x060085A8 RID: 34216 RVA: 0x0000216D File Offset: 0x0000036D
		public void setPan(int instanceId, float newPan, float moveTime)
		{
		}

		// Token: 0x060085A9 RID: 34217 RVA: 0x0000216D File Offset: 0x0000036D
		public void setPan(string labelName, float newPan, float moveTime)
		{
		}

		// Token: 0x060085AA RID: 34218 RVA: 0x0000216D File Offset: 0x0000036D
		public void setPosition(int instanceId, Vector3 position)
		{
		}

		// Token: 0x060085AB RID: 34219 RVA: 0x0000216D File Offset: 0x0000036D
		public void setPosition(string labelName, Vector3 position)
		{
		}

		// Token: 0x060085AC RID: 34220 RVA: 0x0000216D File Offset: 0x0000036D
		public void resetPlayPosition(string labelName)
		{
		}

		// Token: 0x060085AD RID: 34221 RVA: 0x0000216D File Offset: 0x0000036D
		public void resetPlayPositionAll()
		{
		}

		// Token: 0x060085AE RID: 34222 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float getInstanceVolume(int instanceId)
		{
			return 0f;
		}

		// Token: 0x060085AF RID: 34223 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float getInstanceCalcVolume(int instanceId)
		{
			return 0f;
		}

		// Token: 0x060085B0 RID: 34224 RVA: 0x0000216D File Offset: 0x0000036D
		public void setMasterVolume(string masterName, float volume, float moveTime = 0f)
		{
		}

		// Token: 0x060085B1 RID: 34225 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float getMasterVolume(string masterName)
		{
			return 0f;
		}

		// Token: 0x060085B2 RID: 34226 RVA: 0x0000216D File Offset: 0x0000036D
		public void setCategoryVolume(string categoryName, float volume, float moveTime = 0f)
		{
		}

		// Token: 0x060085B3 RID: 34227 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float getCategoryVolume(string categoryName)
		{
			return 0f;
		}

		// Token: 0x060085B4 RID: 34228 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float getLabelVolume(string labelName)
		{
			return 0f;
		}

		// Token: 0x060085B5 RID: 34229 RVA: 0x0000216D File Offset: 0x0000036D
		public void stopMaster(string masterName, float fadeTime = -1f)
		{
		}

		// Token: 0x060085B6 RID: 34230 RVA: 0x0000216D File Offset: 0x0000036D
		public void onPauseMaster(string masterName, float fadeTime = -1f)
		{
		}

		// Token: 0x060085B7 RID: 34231 RVA: 0x0000216D File Offset: 0x0000036D
		public void offPauseMaster(string masterName, float fadeTime = -1f)
		{
		}

		// Token: 0x060085B8 RID: 34232 RVA: 0x0000216D File Offset: 0x0000036D
		public void stopCategory(string categoryName, float fadeTime = -1f)
		{
		}

		// Token: 0x060085B9 RID: 34233 RVA: 0x0000216D File Offset: 0x0000036D
		public void onPauseLabel(string labelName, float fadeTime = -1f)
		{
		}

		// Token: 0x060085BA RID: 34234 RVA: 0x0000216D File Offset: 0x0000036D
		public void offPauseLabel(string labelName, float fadeTime = -1f)
		{
		}

		// Token: 0x060085BB RID: 34235 RVA: 0x0000216D File Offset: 0x0000036D
		public void onPauseCategory(string categoryName, float fadeTime = -1f)
		{
		}

		// Token: 0x060085BC RID: 34236 RVA: 0x0000216D File Offset: 0x0000036D
		public void offPauseCategory(string categoryName, float fadeTime = -1f)
		{
		}

		// Token: 0x060085BD RID: 34237 RVA: 0x0000216D File Offset: 0x0000036D
		public void offPauseCategory(string categoryName, List<int> instanceList, float fadeTime = -1f)
		{
		}

		// Token: 0x060085BE RID: 34238 RVA: 0x000029CC File Offset: 0x00000BCC
		public AudioDefine.INSTANCE_STATUS getInstanceStatus(int instanceId)
		{
			return AudioDefine.INSTANCE_STATUS.STOP;
		}

		// Token: 0x060085BF RID: 34239 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isPlayingLabel(string labelName)
		{
			return false;
		}

		// Token: 0x060085C0 RID: 34240 RVA: 0x000029CC File Offset: 0x00000BCC
		public int getLabelNum()
		{
			return 0;
		}

		// Token: 0x060085C1 RID: 34241 RVA: 0x0000216A File Offset: 0x0000036A
		public string[] getLabelNameList()
		{
			return null;
		}

		// Token: 0x060085C2 RID: 34242 RVA: 0x000029CC File Offset: 0x00000BCC
		public int getCategoryNum()
		{
			return 0;
		}

		// Token: 0x060085C3 RID: 34243 RVA: 0x0000216A File Offset: 0x0000036A
		public string[] getCategoryNameList()
		{
			return null;
		}

		// Token: 0x060085C4 RID: 34244 RVA: 0x000029CC File Offset: 0x00000BCC
		public int getMasterNum()
		{
			return 0;
		}

		// Token: 0x060085C5 RID: 34245 RVA: 0x0000216A File Offset: 0x0000036A
		public string[] getMasterNameList()
		{
			return null;
		}

		// Token: 0x060085C6 RID: 34246 RVA: 0x000029CC File Offset: 0x00000BCC
		public int getAudio3DSettingsNum()
		{
			return 0;
		}

		// Token: 0x060085C7 RID: 34247 RVA: 0x0000216A File Offset: 0x0000036A
		public string[] getAudio3DSettingsNameList()
		{
			return null;
		}

		// Token: 0x060085C8 RID: 34248 RVA: 0x0000216D File Offset: 0x0000036D
		public void updateUnityMixerName(string labelName, string newMixerName)
		{
		}

		// Token: 0x060085C9 RID: 34249 RVA: 0x0000216D File Offset: 0x0000036D
		public void updateSpatialGroupName(string labelName, string newGroupName)
		{
		}

		// Token: 0x060085CA RID: 34250 RVA: 0x0000216A File Offset: 0x0000036A
		public string getCategoryNameSettingOfLabel(string labelName)
		{
			return null;
		}

		// Token: 0x060085CB RID: 34251 RVA: 0x0000216A File Offset: 0x0000036A
		public string getMasterNameSettingOfCategory(string categoryName)
		{
			return null;
		}

		// Token: 0x060085CC RID: 34252 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float getPlayTime(int instanceId)
		{
			return 0f;
		}

		// Token: 0x060085CD RID: 34253 RVA: 0x000029CC File Offset: 0x00000BCC
		public int getPlaySamples(int instanceId)
		{
			return 0;
		}

		// Token: 0x060085CE RID: 34254 RVA: 0x0000216D File Offset: 0x0000036D
		public void setTime(int instanceId, float time)
		{
		}

		// Token: 0x060085CF RID: 34255 RVA: 0x0000216D File Offset: 0x0000036D
		public void setTimeSamples(int instanceId, int samples)
		{
		}

		// Token: 0x060085D0 RID: 34256 RVA: 0x0000216D File Offset: 0x0000036D
		public void setMute(bool onMute)
		{
		}

		// Token: 0x060085D1 RID: 34257 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool getMuteStatus()
		{
			return false;
		}

		// Token: 0x060085D2 RID: 34258 RVA: 0x0000216A File Offset: 0x0000036A
		public string[] getAudioClipNameLoadId(int loadId)
		{
			return null;
		}

		// Token: 0x060085D3 RID: 34259 RVA: 0x0000216A File Offset: 0x0000036A
		public string[] getAudioClipNameAll()
		{
			return null;
		}

		// Token: 0x060085D4 RID: 34260 RVA: 0x0000216A File Offset: 0x0000036A
		public string getAudioClipName(string labelName)
		{
			return null;
		}

		// Token: 0x060085D5 RID: 34261 RVA: 0x0000216A File Offset: 0x0000036A
		public string[] getAudioClipNames(string labelName)
		{
			return null;
		}

		// Token: 0x060085D6 RID: 34262 RVA: 0x0000216A File Offset: 0x0000036A
		public string[] getRandomSourceNames(string labelName)
		{
			return null;
		}

		// Token: 0x060085D7 RID: 34263 RVA: 0x0000216D File Offset: 0x0000036D
		public void setAudioClipToLabelLoadId(int loadId)
		{
		}

		// Token: 0x060085D8 RID: 34264 RVA: 0x0000216D File Offset: 0x0000036D
		public void setAudioClipToLabelAll()
		{
		}

		// Token: 0x060085D9 RID: 34265 RVA: 0x0000216D File Offset: 0x0000036D
		public void setAudioClipToLabel(string labelName)
		{
		}

		// Token: 0x060085DA RID: 34266 RVA: 0x0000216D File Offset: 0x0000036D
		public void setAndroidNativeToLabel(string labelName, string filePath, string className, string funcName)
		{
		}

		// Token: 0x060085DB RID: 34267 RVA: 0x0000216D File Offset: 0x0000036D
		public void clearObjectPool()
		{
		}

		// Token: 0x060085DC RID: 34268 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float getLabelLength(string labelName)
		{
			return 0f;
		}

		// Token: 0x060085DD RID: 34269 RVA: 0x000029CC File Offset: 0x00000BCC
		public int getLabelSamples(string labelName)
		{
			return 0;
		}

		// Token: 0x060085DE RID: 34270 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool getSpectrumData(int instanceId, float[] sample, int channel, FFTWindow window)
		{
			return false;
		}

		// Token: 0x060085DF RID: 34271 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isLoop(string labelName)
		{
			return false;
		}

		// Token: 0x060085E0 RID: 34272 RVA: 0x000029CC File Offset: 0x00000BCC
		public int getLabelMaxPlaybacksNum(string labelName)
		{
			return 0;
		}

		// Token: 0x060085E1 RID: 34273 RVA: 0x000029CC File Offset: 0x00000BCC
		public int getCategoryMaxPlaybacksNum(string categoryName)
		{
			return 0;
		}

		// Token: 0x060085E2 RID: 34274 RVA: 0x000029CC File Offset: 0x00000BCC
		public int getCategoryMaxPlaybacksNumFromLabel(string labelName)
		{
			return 0;
		}

		// Token: 0x060085E3 RID: 34275 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isInterval(string labelName)
		{
			return false;
		}

		// Token: 0x060085E4 RID: 34276 RVA: 0x000029CC File Offset: 0x00000BCC
		public int getCurrentPlayNum()
		{
			return 0;
		}

		// Token: 0x060085E5 RID: 34277 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060085E6 RID: 34278 RVA: 0x0000216D File Offset: 0x0000036D
		private void resetDuckingBeforeUpdate(AudioManager.AudioPlayer player)
		{
		}

		// Token: 0x060085E7 RID: 34279 RVA: 0x0000216A File Offset: 0x0000036A
		public static AudioManager.AudioLabelSettings GetLabelInfo_Extention(string name)
		{
			return null;
		}

		// Token: 0x0400C083 RID: 49283
		private static AudioManager manager;

		// Token: 0x0400C084 RID: 49284
		private bool IsOnMute;

		// Token: 0x0400C085 RID: 49285
		private int AndroidSoundPoolNum;

		// Token: 0x0400C086 RID: 49286
		private Dictionary<string, AudioManager.AudioPlayer> sourceDict;

		// Token: 0x0400C087 RID: 49287
		private Dictionary<int, AudioManager.AudioPlayer> playAudioDict;

		// Token: 0x0400C088 RID: 49288
		private Dictionary<string, List<int>> playCategoryDict;

		// Token: 0x0400C089 RID: 49289
		private Dictionary<string, AudioManager.AudioCategorySettings> categoryDict;

		// Token: 0x0400C08A RID: 49290
		private Dictionary<string, AudioManager.AudioMasterSettings> masterDict;

		// Token: 0x0400C08B RID: 49291
		private Dictionary<string, List<string>> playDuckingTrigger;

		// Token: 0x0400C08C RID: 49292
		private List<int> playAudioRemoveKey;

		// Token: 0x0400C08D RID: 49293
		private HashSet<AudioManager.AudioPlayer> playerHashSet;

		// Token: 0x0400C08E RID: 49294
		private Dictionary<string, AudioClip> audioClipDict;

		// Token: 0x0400C08F RID: 49295
		private Dictionary<string, Audio3DSettings> audio3DSettings;

		// Token: 0x0400C090 RID: 49296
		private AudioManager.AudioMixerSettings mixerSettings;

		// Token: 0x0400C091 RID: 49297
		private Transform _cacheTransform;

		// Token: 0x0400C092 RID: 49298
		private Thread jsonThread;

		// Token: 0x0400C093 RID: 49299
		private bool jsonThreadFlag;

		// Token: 0x0400C094 RID: 49300
		private string jsonStr;

		// Token: 0x0400C095 RID: 49301
		private AudioManager.AudioMasterSettings[] tmpMaster;

		// Token: 0x0400C096 RID: 49302
		private AudioManager.AudioCategorySettings[] tmpCategory;

		// Token: 0x0400C097 RID: 49303
		private AudioManager.AudioLabelSettings[] tmpLabel;

		// Token: 0x0400C098 RID: 49304
		private AudioDefine.LOAD_XML_STATUS loadXmlStatus;

		// Token: 0x0400C099 RID: 49305
		private AudioDefine.LOAD_JSON_STATUS loadJsonStatus;

		// Token: 0x02001182 RID: 4482
		[Serializable]
		public class AudioCategorySettings
		{
			// Token: 0x060085E9 RID: 34281 RVA: 0x0000216D File Offset: 0x0000036D
			public void CopySettings(AudioManager.AudioCategorySettings src)
			{
			}

			// Token: 0x060085EA RID: 34282 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetAttachMasterInstance(AudioManager.AudioMasterSettings master)
			{
			}

			// Token: 0x060085EB RID: 34283 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetVolumeFactor()
			{
				return 0f;
			}

			// Token: 0x060085EC RID: 34284 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetCurrentVolume()
			{
				return 0f;
			}

			// Token: 0x060085ED RID: 34285 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetVolumeUpdater(float start, float target, float time)
			{
			}

			// Token: 0x060085EE RID: 34286 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetDuckingVolumeUpdater(float target, float time, bool isLow)
			{
			}

			// Token: 0x060085EF RID: 34287 RVA: 0x0000216D File Offset: 0x0000036D
			public void ClearVolumeUpdater()
			{
			}

			// Token: 0x060085F0 RID: 34288 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool UpdateVolume()
			{
				return false;
			}

			// Token: 0x060085F1 RID: 34289 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool UpdateDuckingVolume()
			{
				return false;
			}

			// Token: 0x0400C09A RID: 49306
			public string categoryName;

			// Token: 0x0400C09B RID: 49307
			public int maxPlaybacksNum;

			// Token: 0x0400C09C RID: 49308
			public float volume;

			// Token: 0x0400C09D RID: 49309
			public string masterName;

			// Token: 0x0400C09E RID: 49310
			private AudioManager.AudioMasterSettings attachMaster;

			// Token: 0x0400C09F RID: 49311
			private AudioManager.AudioParamUpdater volumeUpdater;

			// Token: 0x0400C0A0 RID: 49312
			private AudioManager.AudioParamUpdater duckingUpdater;

			// Token: 0x0400C0A1 RID: 49313
			public float programVolume;

			// Token: 0x0400C0A2 RID: 49314
			public float duckingVolume;
		}

		// Token: 0x02001183 RID: 4483
		private class AudioDebugLog
		{
			// Token: 0x060085F3 RID: 34291 RVA: 0x0000216D File Offset: 0x0000036D
			public static void Break()
			{
			}

			// Token: 0x060085F4 RID: 34292 RVA: 0x0000216D File Offset: 0x0000036D
			public static void Log(object message)
			{
			}

			// Token: 0x060085F5 RID: 34293 RVA: 0x0000216D File Offset: 0x0000036D
			public static void Log(object message, global::UnityEngine.Object context)
			{
			}

			// Token: 0x060085F6 RID: 34294 RVA: 0x0000216D File Offset: 0x0000036D
			public static void LogError(object message)
			{
			}

			// Token: 0x060085F7 RID: 34295 RVA: 0x0000216D File Offset: 0x0000036D
			public static void LogError(object message, global::UnityEngine.Object context)
			{
			}

			// Token: 0x060085F8 RID: 34296 RVA: 0x0000216D File Offset: 0x0000036D
			public static void LogWarning(object message)
			{
			}

			// Token: 0x060085F9 RID: 34297 RVA: 0x0000216D File Offset: 0x0000036D
			public static void LogWarning(object message, global::UnityEngine.Object context)
			{
			}

			// Token: 0x060085FA RID: 34298 RVA: 0x000029CC File Offset: 0x00000BCC
			private static bool IsEnable()
			{
				return false;
			}

			// Token: 0x0400C0A3 RID: 49315
			private static bool isActive;
		}

		// Token: 0x02001184 RID: 4484
		private class AudioInstance
		{
			// Token: 0x060085FC RID: 34300 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetActive(bool active)
			{
			}

			// Token: 0x060085FD RID: 34301 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetInstanceID(int id)
			{
			}

			// Token: 0x060085FE RID: 34302 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetInstanceID()
			{
				return 0;
			}

			// Token: 0x060085FF RID: 34303 RVA: 0x0000216D File Offset: 0x0000036D
			public void Reset(AudioManager.AudioMainPool pool)
			{
			}

			// Token: 0x06008600 RID: 34304 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetRandomIndex()
			{
				return 0;
			}

			// Token: 0x06008601 RID: 34305 RVA: 0x0000216D File Offset: 0x0000036D
			public void Init(AudioSource playSource, float factor, int index)
			{
			}

			// Token: 0x06008602 RID: 34306 RVA: 0x0000216D File Offset: 0x0000036D
			public void Init(AudioSource playSource, AudioManager.AudioLabelSettings labelInfo, float factor, int index)
			{
			}

			// Token: 0x06008603 RID: 34307 RVA: 0x0000216D File Offset: 0x0000036D
			public void UpdateVolumeFactor(float factor)
			{
			}

			// Token: 0x06008604 RID: 34308 RVA: 0x0000216D File Offset: 0x0000036D
			public void Update()
			{
			}

			// Token: 0x06008605 RID: 34309 RVA: 0x0000216D File Offset: 0x0000036D
			public void ResetPlayPosition()
			{
			}

			// Token: 0x06008606 RID: 34310 RVA: 0x0000216D File Offset: 0x0000036D
			public void Prepare(float volume, float fadeTime, float pan, int pitch, int playStartSample)
			{
			}

			// Token: 0x06008607 RID: 34311 RVA: 0x0000216D File Offset: 0x0000036D
			public void Play(float delay)
			{
			}

			// Token: 0x06008608 RID: 34312 RVA: 0x0000216D File Offset: 0x0000036D
			public void Stop(float fadeTime)
			{
			}

			// Token: 0x06008609 RID: 34313 RVA: 0x0000216D File Offset: 0x0000036D
			public void ForceStop()
			{
			}

			// Token: 0x0600860A RID: 34314 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnPause(float fadeTime)
			{
			}

			// Token: 0x0600860B RID: 34315 RVA: 0x0000216D File Offset: 0x0000036D
			public void OffPause(float fadeTime)
			{
			}

			// Token: 0x0600860C RID: 34316 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetVolume(float newVolume, float moveTime)
			{
			}

			// Token: 0x0600860D RID: 34317 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetCurrentVolume()
			{
				return 0f;
			}

			// Token: 0x0600860E RID: 34318 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetCalcVolume()
			{
				return 0f;
			}

			// Token: 0x0600860F RID: 34319 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetPitch(int newPitch, float moveTime)
			{
			}

			// Token: 0x06008610 RID: 34320 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetPan(float newPan, float moveTime)
			{
			}

			// Token: 0x06008611 RID: 34321 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetTrackingObject(GameObject target)
			{
			}

			// Token: 0x06008612 RID: 34322 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetTrackingObject(Transform target)
			{
			}

			// Token: 0x06008613 RID: 34323 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetPosition(Vector3 position)
			{
			}

			// Token: 0x06008614 RID: 34324 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsPlaying()
			{
				return false;
			}

			// Token: 0x06008615 RID: 34325 RVA: 0x000029CC File Offset: 0x00000BCC
			public AudioDefine.INSTANCE_STATUS GetStatus()
			{
				return AudioDefine.INSTANCE_STATUS.STOP;
			}

			// Token: 0x06008616 RID: 34326 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetPrevPlaySamples()
			{
				return 0;
			}

			// Token: 0x06008617 RID: 34327 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetPlayTime()
			{
				return 0f;
			}

			// Token: 0x06008618 RID: 34328 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetPlaySamples()
			{
				return 0;
			}

			// Token: 0x06008619 RID: 34329 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetTime(float time)
			{
			}

			// Token: 0x0600861A RID: 34330 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetTimeSamples(int samples)
			{
			}

			// Token: 0x0600861B RID: 34331 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool GetSpectrumData(int instanceId, float[] sample, int channel, FFTWindow window)
			{
				return false;
			}

			// Token: 0x0600861C RID: 34332 RVA: 0x0000216D File Offset: 0x0000036D
			private void setupVolume(float volume, float fadeTime, bool isPlayLastSamples)
			{
			}

			// Token: 0x0600861D RID: 34333 RVA: 0x0000216D File Offset: 0x0000036D
			private void setupPitch(int pitch)
			{
			}

			// Token: 0x0600861E RID: 34334 RVA: 0x0000216D File Offset: 0x0000036D
			private void setupStereoPan(float pan)
			{
			}

			// Token: 0x0600861F RID: 34335 RVA: 0x000029C5 File Offset: 0x00000BC5
			private float getRandomValue(float min, float max, float unit, bool isconsecutive, float prevValue)
			{
				return 0f;
			}

			// Token: 0x06008620 RID: 34336 RVA: 0x000029C5 File Offset: 0x00000BC5
			private float calcPitchRatio(int cent)
			{
				return 0f;
			}

			// Token: 0x06008621 RID: 34337 RVA: 0x0000216D File Offset: 0x0000036D
			public void UpdateAudio3DSettings(Audio3DSettings audio3d)
			{
			}

			// Token: 0x0400C0A4 RID: 49316
			private bool isAudioDebug;

			// Token: 0x0400C0A5 RID: 49317
			private AudioManager.AudioLabelSettings setting;

			// Token: 0x0400C0A6 RID: 49318
			private AudioSource source;

			// Token: 0x0400C0A7 RID: 49319
			private Transform sourceTransform;

			// Token: 0x0400C0A8 RID: 49320
			private float defaultVolume;

			// Token: 0x0400C0A9 RID: 49321
			private float defaultPan;

			// Token: 0x0400C0AA RID: 49322
			private int defaultPitch;

			// Token: 0x0400C0AB RID: 49323
			private int currentPitch;

			// Token: 0x0400C0AC RID: 49324
			private float currentVolume;

			// Token: 0x0400C0AD RID: 49325
			private float volumeFactor;

			// Token: 0x0400C0AE RID: 49326
			private float ctrlVolumeFactor;

			// Token: 0x0400C0AF RID: 49327
			private AudioManager.AudioParamUpdater volumeUpdater;

			// Token: 0x0400C0B0 RID: 49328
			private AudioManager.AudioParamUpdater panUpdater;

			// Token: 0x0400C0B1 RID: 49329
			private AudioManager.AudioParamUpdater pitchUpdater;

			// Token: 0x0400C0B2 RID: 49330
			private AudioManager.AudioParamUpdater controlUpdater;

			// Token: 0x0400C0B3 RID: 49331
			private int prevPlaySamples;

			// Token: 0x0400C0B4 RID: 49332
			private bool onPause;

			// Token: 0x0400C0B5 RID: 49333
			private int randomIndex;

			// Token: 0x0400C0B6 RID: 49334
			private int instanceId;

			// Token: 0x0400C0B7 RID: 49335
			public bool activeSelf;

			// Token: 0x0400C0B8 RID: 49336
			private AudioManager.AudioInstance.FADE_END_STATE fadeStatus;

			// Token: 0x0400C0B9 RID: 49337
			private AudioDefine.INSTANCE_STATUS status;

			// Token: 0x0400C0BA RID: 49338
			private Transform targetTransform;

			// Token: 0x0400C0BB RID: 49339
			private Vector3 defaultPos;

			// Token: 0x0400C0BC RID: 49340
			private bool isUpdateStart;

			// Token: 0x02001185 RID: 4485
			private enum FADE_END_STATE
			{
				// Token: 0x0400C0BE RID: 49342
				UNSET,
				// Token: 0x0400C0BF RID: 49343
				PAUSE,
				// Token: 0x0400C0C0 RID: 49344
				STOP
			}
		}

		// Token: 0x02001186 RID: 4486
		private class AudioInstancePool
		{
			// Token: 0x1700110C RID: 4364
			// (get) Token: 0x06008623 RID: 34339 RVA: 0x0000216A File Offset: 0x0000036A
			public static AudioManager.AudioInstancePool instance
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06008624 RID: 34340 RVA: 0x0000216D File Offset: 0x0000036D
			public static void Initialize()
			{
			}

			// Token: 0x06008625 RID: 34341 RVA: 0x0000216D File Offset: 0x0000036D
			public void AddEmpty(int num)
			{
			}

			// Token: 0x06008626 RID: 34342 RVA: 0x0000216A File Offset: 0x0000036A
			public AudioManager.AudioInstance AddComponent()
			{
				return null;
			}

			// Token: 0x06008627 RID: 34343 RVA: 0x0000216D File Offset: 0x0000036D
			public void Deactive(AudioManager.AudioInstance instance)
			{
			}

			// Token: 0x06008628 RID: 34344 RVA: 0x0000216D File Offset: 0x0000036D
			public void Clear()
			{
			}

			// Token: 0x0400C0C1 RID: 49345
			private static AudioManager.AudioInstancePool _instance;

			// Token: 0x0400C0C2 RID: 49346
			private static int instanceIdNext;

			// Token: 0x0400C0C3 RID: 49347
			private List<AudioManager.AudioInstance> pool;
		}

		// Token: 0x02001187 RID: 4487
		[Serializable]
		public class AudioLabelSettings
		{
			// Token: 0x0600862A RID: 34346 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetAndroidSoundId(int soundId)
			{
			}

			// Token: 0x0600862B RID: 34347 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetAndroidSoundId()
			{
				return 0;
			}

			// Token: 0x0600862C RID: 34348 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetLoop(bool loop)
			{
			}

			// Token: 0x0600862D RID: 34349 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool GetLoop()
			{
				return false;
			}

			// Token: 0x0600862E RID: 34350 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetClipName(string name)
			{
			}

			// Token: 0x0600862F RID: 34351 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetClipName()
			{
				return null;
			}

			// Token: 0x06008630 RID: 34352 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetAttachCategoryInstance(AudioManager.AudioCategorySettings category)
			{
			}

			// Token: 0x06008631 RID: 34353 RVA: 0x0000216A File Offset: 0x0000036A
			public AudioManager.AudioCategorySettings GetAttachCategory()
			{
				return null;
			}

			// Token: 0x06008632 RID: 34354 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetCategoryName()
			{
				return null;
			}

			// Token: 0x0400C0C4 RID: 49348
			public float volume;

			// Token: 0x0400C0C5 RID: 49349
			public AudioManager.AudioLabelSettings.BEHAVIOR maxPlaybacksBehavior;

			// Token: 0x0400C0C6 RID: 49350
			public int priority;

			// Token: 0x0400C0C7 RID: 49351
			public string categoryName;

			// Token: 0x0400C0C8 RID: 49352
			public string singleGroup;

			// Token: 0x0400C0C9 RID: 49353
			public int maxPlaybacksNum;

			// Token: 0x0400C0CA RID: 49354
			public bool isStealOldest;

			// Token: 0x0400C0CB RID: 49355
			public string unityMixerName;

			// Token: 0x0400C0CC RID: 49356
			public string spatialGroup;

			// Token: 0x0400C0CD RID: 49357
			public float playStartDelay;

			// Token: 0x0400C0CE RID: 49358
			public float playInterval;

			// Token: 0x0400C0CF RID: 49359
			public float pan;

			// Token: 0x0400C0D0 RID: 49360
			public int pitchShiftCent;

			// Token: 0x0400C0D1 RID: 49361
			public bool isPlayLastSamples;

			// Token: 0x0400C0D2 RID: 49362
			public float fadeInTime;

			// Token: 0x0400C0D3 RID: 49363
			public float fadeOutTime;

			// Token: 0x0400C0D4 RID: 49364
			public float fadeInTimeOldSamples;

			// Token: 0x0400C0D5 RID: 49365
			public float fadeOutTimeOnPause;

			// Token: 0x0400C0D6 RID: 49366
			public float fadeInTimeOffPause;

			// Token: 0x0400C0D7 RID: 49367
			public bool isVolumeRandom;

			// Token: 0x0400C0D8 RID: 49368
			public bool inconsecutiveVolume;

			// Token: 0x0400C0D9 RID: 49369
			public float volumeRandomMin;

			// Token: 0x0400C0DA RID: 49370
			public float volumeRandomMax;

			// Token: 0x0400C0DB RID: 49371
			public float volumeRandomUnit;

			// Token: 0x0400C0DC RID: 49372
			public bool isPitchRandom;

			// Token: 0x0400C0DD RID: 49373
			public bool inconsecutivePitch;

			// Token: 0x0400C0DE RID: 49374
			public int pitchRandomMin;

			// Token: 0x0400C0DF RID: 49375
			public int pitchRandomMax;

			// Token: 0x0400C0E0 RID: 49376
			public int pitchRandomUnit;

			// Token: 0x0400C0E1 RID: 49377
			public bool isPanRandom;

			// Token: 0x0400C0E2 RID: 49378
			public bool inconsecutivePan;

			// Token: 0x0400C0E3 RID: 49379
			public float panRandomMin;

			// Token: 0x0400C0E4 RID: 49380
			public float panRandomMax;

			// Token: 0x0400C0E5 RID: 49381
			public float panRandomUnit;

			// Token: 0x0400C0E6 RID: 49382
			public bool isRandomPlay;

			// Token: 0x0400C0E7 RID: 49383
			public bool inconsecutiveSource;

			// Token: 0x0400C0E8 RID: 49384
			public string[] randomSource;

			// Token: 0x0400C0E9 RID: 49385
			public bool isMovePitch;

			// Token: 0x0400C0EA RID: 49386
			public int pitchStart;

			// Token: 0x0400C0EB RID: 49387
			public int pitchEnd;

			// Token: 0x0400C0EC RID: 49388
			public float pitchMoveTime;

			// Token: 0x0400C0ED RID: 49389
			public bool isMovePan;

			// Token: 0x0400C0EE RID: 49390
			public float panStart;

			// Token: 0x0400C0EF RID: 49391
			public float panEnd;

			// Token: 0x0400C0F0 RID: 49392
			public float panMoveTime;

			// Token: 0x0400C0F1 RID: 49393
			public string[] duckingCategories;

			// Token: 0x0400C0F2 RID: 49394
			public float duckingStartTime;

			// Token: 0x0400C0F3 RID: 49395
			public float duckingEndTime;

			// Token: 0x0400C0F4 RID: 49396
			public float duckingVolumeFactor;

			// Token: 0x0400C0F5 RID: 49397
			public bool autoRestoreDucking;

			// Token: 0x0400C0F6 RID: 49398
			public float restoreTime;

			// Token: 0x0400C0F7 RID: 49399
			public bool isAndroidNative;

			// Token: 0x0400C0F8 RID: 49400
			private int androidSoundId;

			// Token: 0x0400C0F9 RID: 49401
			public int loadId;

			// Token: 0x0400C0FA RID: 49402
			public string name;

			// Token: 0x0400C0FB RID: 49403
			private AudioManager.AudioCategorySettings attachCategory;

			// Token: 0x0400C0FC RID: 49404
			public string clipName;

			// Token: 0x0400C0FD RID: 49405
			public bool isLoop;

			// Token: 0x02001188 RID: 4488
			public enum BEHAVIOR
			{
				// Token: 0x0400C0FF RID: 49407
				STEAL_OLDEST,
				// Token: 0x0400C100 RID: 49408
				JUST_FAIL,
				// Token: 0x0400C101 RID: 49409
				QUEUE
			}
		}

		// Token: 0x02001189 RID: 4489
		private class AudioMainPool : MonoBehaviour
		{
			// Token: 0x1700110D RID: 4365
			// (get) Token: 0x06008634 RID: 34356 RVA: 0x0000216A File Offset: 0x0000036A
			private static Transform CacheTransform
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700110E RID: 4366
			// (get) Token: 0x06008635 RID: 34357 RVA: 0x0000216A File Offset: 0x0000036A
			public static AudioManager.AudioMainPool instance
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06008636 RID: 34358 RVA: 0x0000216D File Offset: 0x0000036D
			public static void Initialize(GameObject obj)
			{
			}

			// Token: 0x06008637 RID: 34359 RVA: 0x0000216D File Offset: 0x0000036D
			public static void Terminate()
			{
			}

			// Token: 0x06008638 RID: 34360 RVA: 0x0000216D File Offset: 0x0000036D
			public void AddEmpty(int num)
			{
			}

			// Token: 0x06008639 RID: 34361 RVA: 0x0000216A File Offset: 0x0000036A
			public AudioSource GetClone()
			{
				return null;
			}

			// Token: 0x0600863A RID: 34362 RVA: 0x0000216D File Offset: 0x0000036D
			public void Deactive(AudioSource source)
			{
			}

			// Token: 0x0600863B RID: 34363 RVA: 0x0000216D File Offset: 0x0000036D
			public void Clear()
			{
			}

			// Token: 0x0400C102 RID: 49410
			private List<AudioSource> pool;

			// Token: 0x0400C103 RID: 49411
			private static AudioManager.AudioMainPool _instance;

			// Token: 0x0400C104 RID: 49412
			private static GameObject owner;

			// Token: 0x0400C105 RID: 49413
			private static Transform _cacheTransform;
		}

		// Token: 0x0200118A RID: 4490
		private enum RESULT
		{
			// Token: 0x0400C107 RID: 49415
			CONTINUE,
			// Token: 0x0400C108 RID: 49416
			EXECUTE,
			// Token: 0x0400C109 RID: 49417
			FINISH
		}

		// Token: 0x0200118B RID: 4491
		[Serializable]
		private class Wrapper<T>
		{
			// Token: 0x0400C10A RID: 49418
			public T[] master;

			// Token: 0x0400C10B RID: 49419
			public T[] category;

			// Token: 0x0400C10C RID: 49420
			public T[] label;
		}

		// Token: 0x0200118C RID: 4492
		[Serializable]
		private class AudioSourceWrapper
		{
			// Token: 0x0400C10D RID: 49421
			public string spatialName;

			// Token: 0x0400C10E RID: 49422
			public float spatialBlend;

			// Token: 0x0400C10F RID: 49423
			public float reverbZoneMix;

			// Token: 0x0400C110 RID: 49424
			public float dopplerLevel;

			// Token: 0x0400C111 RID: 49425
			public int spread;

			// Token: 0x0400C112 RID: 49426
			public AudioRolloffMode rolloffMode;

			// Token: 0x0400C113 RID: 49427
			public float minDistance;

			// Token: 0x0400C114 RID: 49428
			public float maxDistance;

			// Token: 0x0400C115 RID: 49429
			public AnimationCurve customRolloffCurve;

			// Token: 0x0400C116 RID: 49430
			public AnimationCurve spatialBlendCurve;

			// Token: 0x0400C117 RID: 49431
			public AnimationCurve reverbZoneMixCurve;

			// Token: 0x0400C118 RID: 49432
			public AnimationCurve spreadCurve;
		}

		// Token: 0x0200118D RID: 4493
		[Serializable]
		public class AudioMasterSettings
		{
			// Token: 0x0600863F RID: 34367 RVA: 0x0000216D File Offset: 0x0000036D
			public void CopySettings(AudioManager.AudioMasterSettings src)
			{
			}

			// Token: 0x06008640 RID: 34368 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetCurrentVolume()
			{
				return 0f;
			}

			// Token: 0x06008641 RID: 34369 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetVolumeFactor()
			{
				return 0f;
			}

			// Token: 0x06008642 RID: 34370 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetVolumeUpdater(float start, float target, float time)
			{
			}

			// Token: 0x06008643 RID: 34371 RVA: 0x0000216D File Offset: 0x0000036D
			public void ClearVolumeUpdater()
			{
			}

			// Token: 0x06008644 RID: 34372 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool UpdateVolume()
			{
				return false;
			}

			// Token: 0x06008645 RID: 34373 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetMute(bool onMute)
			{
			}

			// Token: 0x06008646 RID: 34374 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetMannerMode(bool onMute)
			{
			}

			// Token: 0x0400C119 RID: 49433
			public string masterName;

			// Token: 0x0400C11A RID: 49434
			public float volume;

			// Token: 0x0400C11B RID: 49435
			private AudioManager.AudioParamUpdater volumeUpdater;

			// Token: 0x0400C11C RID: 49436
			public float programVolume;

			// Token: 0x0400C11D RID: 49437
			public float mute;

			// Token: 0x0400C11E RID: 49438
			private float manner;
		}

		// Token: 0x0200118E RID: 4494
		private class AudioMixerSettings
		{
			// Token: 0x06008648 RID: 34376 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetAudioMixer(AudioMixer _mixer)
			{
			}

			// Token: 0x06008649 RID: 34377 RVA: 0x0000216A File Offset: 0x0000036A
			public AudioMixerGroup[] FindGroup(string groupName)
			{
				return null;
			}

			// Token: 0x0600864A RID: 34378 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetSnapshot(string snapName, float time)
			{
			}

			// Token: 0x0600864B RID: 34379 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetFloat(string paramName, float value)
			{
			}

			// Token: 0x0400C11F RID: 49439
			public AudioMixer mixer;
		}

		// Token: 0x0200118F RID: 4495
		private class AudioParamUpdater
		{
			// Token: 0x1700110F RID: 4367
			// (get) Token: 0x0600864D RID: 34381 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x0600864E RID: 34382 RVA: 0x0000216D File Offset: 0x0000036D
			public bool active
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x0600864F RID: 34383 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetParam(float _start, float _target, float moveTime, bool isLow)
			{
			}

			// Token: 0x06008650 RID: 34384 RVA: 0x0000216D File Offset: 0x0000036D
			public void UpdateStart()
			{
			}

			// Token: 0x06008651 RID: 34385 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float Update()
			{
				return 0f;
			}

			// Token: 0x06008652 RID: 34386 RVA: 0x0000216D File Offset: 0x0000036D
			public void Clear()
			{
			}

			// Token: 0x0400C120 RID: 49440
			private float target;

			// Token: 0x0400C121 RID: 49441
			private float current;

			// Token: 0x0400C122 RID: 49442
			private float unit;

			// Token: 0x0400C123 RID: 49443
			private float prevTime;

			// Token: 0x0400C124 RID: 49444
			private bool move;

			// Token: 0x0400C125 RID: 49445
			private bool _active;
		}

		// Token: 0x02001190 RID: 4496
		private class AudioPlayer
		{
			// Token: 0x17001110 RID: 4368
			// (get) Token: 0x06008654 RID: 34388 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06008655 RID: 34389 RVA: 0x0000216D File Offset: 0x0000036D
			public string PlayerName
			{
				get
				{
					return null;
				}
				set
				{
				}
			}

			// Token: 0x06008656 RID: 34390 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetAudioMixerGroup(AudioMixerGroup _mixer)
			{
			}

			// Token: 0x06008657 RID: 34391 RVA: 0x0000216D File Offset: 0x0000036D
			public void UpdateRandomSourceInfo(Dictionary<string, AudioManager.AudioPlayer> dict)
			{
			}

			// Token: 0x06008658 RID: 34392 RVA: 0x0000216A File Offset: 0x0000036A
			public AudioClip GetPlayClip()
			{
				return null;
			}

			// Token: 0x06008659 RID: 34393 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsSetPlayClip()
			{
				return false;
			}

			// Token: 0x0600865A RID: 34394 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetPlayClip(AudioClip clip)
			{
			}

			// Token: 0x0600865B RID: 34395 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetClipLength()
			{
				return 0f;
			}

			// Token: 0x0600865C RID: 34396 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetClipSamples()
			{
				return 0;
			}

			// Token: 0x0600865D RID: 34397 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool Init(AudioClip clip, string name, AudioManager.AudioLabelSettings label, Dictionary<string, AudioManager.AudioPlayer> dict)
			{
				return false;
			}

			// Token: 0x0600865E RID: 34398 RVA: 0x0000216D File Offset: 0x0000036D
			private void initRandomSettins()
			{
			}

			// Token: 0x0600865F RID: 34399 RVA: 0x0000216D File Offset: 0x0000036D
			public void ResetPlayClip()
			{
			}

			// Token: 0x06008660 RID: 34400 RVA: 0x0000216D File Offset: 0x0000036D
			public void Reset()
			{
			}

			// Token: 0x06008661 RID: 34401 RVA: 0x0000216D File Offset: 0x0000036D
			public void LoadAudioData()
			{
			}

			// Token: 0x06008662 RID: 34402 RVA: 0x0000216D File Offset: 0x0000036D
			public void UnloadAudioData()
			{
			}

			// Token: 0x06008663 RID: 34403 RVA: 0x000029C5 File Offset: 0x00000BC5
			private float getRandomValue(float min, float max, float unit, bool isconsecutive, float prevValue)
			{
				return 0f;
			}

			// Token: 0x06008664 RID: 34404 RVA: 0x0000216D File Offset: 0x0000036D
			public void ResetPlayPosition()
			{
			}

			// Token: 0x06008665 RID: 34405 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetPlayingNum()
			{
				return 0;
			}

			// Token: 0x06008666 RID: 34406 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetPlayingTrueNum()
			{
				return 0;
			}

			// Token: 0x06008667 RID: 34407 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetMaxPlaybacksNum()
			{
				return 0;
			}

			// Token: 0x06008668 RID: 34408 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsStealOldest()
			{
				return false;
			}

			// Token: 0x06008669 RID: 34409 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetCategoryName()
			{
				return null;
			}

			// Token: 0x0600866A RID: 34410 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetCategoryMaxPlaybacksNum()
			{
				return 0;
			}

			// Token: 0x0600866B RID: 34411 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetPriority()
			{
				return 0;
			}

			// Token: 0x0600866C RID: 34412 RVA: 0x000029CC File Offset: 0x00000BCC
			public AudioManager.AudioLabelSettings.BEHAVIOR GetMaxPlaybacksBehavior()
			{
				return AudioManager.AudioLabelSettings.BEHAVIOR.STEAL_OLDEST;
			}

			// Token: 0x0600866D RID: 34413 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetFadeOutTime()
			{
				return 0f;
			}

			// Token: 0x0600866E RID: 34414 RVA: 0x0000216A File Offset: 0x0000036A
			public AudioManager.AudioCategorySettings GetCategorySettings()
			{
				return null;
			}

			// Token: 0x0600866F RID: 34415 RVA: 0x0000216A File Offset: 0x0000036A
			public AudioManager.AudioLabelSettings GetLabelSettings()
			{
				return null;
			}

			// Token: 0x06008670 RID: 34416 RVA: 0x000029CC File Offset: 0x00000BCC
			public AudioDefine.INSTANCE_STATUS GetInstanceStatus(int instanceId)
			{
				return AudioDefine.INSTANCE_STATUS.STOP;
			}

			// Token: 0x06008671 RID: 34417 RVA: 0x0000216D File Offset: 0x0000036D
			public void StopOldInstance()
			{
			}

			// Token: 0x06008672 RID: 34418 RVA: 0x000029CC File Offset: 0x00000BCC
			private int prepareImpl(float volume, float fadeTime, float pan, int pitch, float delay, bool isStart, bool isForce2D)
			{
				return 0;
			}

			// Token: 0x06008673 RID: 34419 RVA: 0x000029CC File Offset: 0x00000BCC
			public int Prepare(float volume, float fadeTime, float pan, int pitch, bool isForce2D)
			{
				return 0;
			}

			// Token: 0x06008674 RID: 34420 RVA: 0x000029CC File Offset: 0x00000BCC
			public int Play(float volume, float fadeTime, float pan, int pitch, float delay, bool isForce2D)
			{
				return 0;
			}

			// Token: 0x06008675 RID: 34421 RVA: 0x0000216D File Offset: 0x0000036D
			public void PlayInstance(int instanceId, float delay = 0f)
			{
			}

			// Token: 0x06008676 RID: 34422 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetTrackingObject(int instanceId, GameObject target)
			{
			}

			// Token: 0x06008677 RID: 34423 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetTrackingObject(int instanceId, Transform target)
			{
			}

			// Token: 0x06008678 RID: 34424 RVA: 0x0000216D File Offset: 0x0000036D
			public void Stop(int instanceId, float fadeTime = -1f)
			{
			}

			// Token: 0x06008679 RID: 34425 RVA: 0x0000216D File Offset: 0x0000036D
			public void StopAll(float fadeTime = -1f)
			{
			}

			// Token: 0x0600867A RID: 34426 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnPause(int instanceId, float fadeTime = -1f)
			{
			}

			// Token: 0x0600867B RID: 34427 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnPauseAll(float fadeTime = -1f)
			{
			}

			// Token: 0x0600867C RID: 34428 RVA: 0x0000216D File Offset: 0x0000036D
			public void OffPause(int instanceId, float fadeTime = -1f)
			{
			}

			// Token: 0x0600867D RID: 34429 RVA: 0x0000216D File Offset: 0x0000036D
			public void OffPauseAll(float fadeTime = -1f)
			{
			}

			// Token: 0x0600867E RID: 34430 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetVolume(int instanceId, float newVolume, float moveTime)
			{
			}

			// Token: 0x0600867F RID: 34431 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetCurrentVolume(int instanceId)
			{
				return 0f;
			}

			// Token: 0x06008680 RID: 34432 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetCalcVolume(int instanceId)
			{
				return 0f;
			}

			// Token: 0x06008681 RID: 34433 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetVolumeAll(float newVolume, float moveTime)
			{
			}

			// Token: 0x06008682 RID: 34434 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetPitch(int instanceId, int newPitch, float moveTime)
			{
			}

			// Token: 0x06008683 RID: 34435 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetPitchAll(int newPitch, float moveTime)
			{
			}

			// Token: 0x06008684 RID: 34436 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetPan(int instanceId, float newPan, float moveTime)
			{
			}

			// Token: 0x06008685 RID: 34437 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetPanAll(float newPan, float moveTime)
			{
			}

			// Token: 0x06008686 RID: 34438 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetPosition(int instanceId, Vector3 position)
			{
			}

			// Token: 0x06008687 RID: 34439 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetPositionAll(Vector3 position)
			{
			}

			// Token: 0x06008688 RID: 34440 RVA: 0x0000216D File Offset: 0x0000036D
			public void UpdateVolumeFactor(float volumeFactor)
			{
			}

			// Token: 0x06008689 RID: 34441 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetPlayTime(int instanceId)
			{
				return 0f;
			}

			// Token: 0x0600868A RID: 34442 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetPlayTime()
			{
				return 0f;
			}

			// Token: 0x0600868B RID: 34443 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetPlaySamples(int instanceId)
			{
				return 0;
			}

			// Token: 0x0600868C RID: 34444 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetTime(int instanceId, float time)
			{
			}

			// Token: 0x0600868D RID: 34445 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetTimeSamples(int instanceId, int samples)
			{
			}

			// Token: 0x0600868E RID: 34446 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool GetSpectrumData(int instanceId, float[] sample, int channel, FFTWindow window)
			{
				return false;
			}

			// Token: 0x0600868F RID: 34447 RVA: 0x0000216D File Offset: 0x0000036D
			public void Update()
			{
			}

			// Token: 0x06008690 RID: 34448 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetSpatialGroup()
			{
				return null;
			}

			// Token: 0x06008691 RID: 34449 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsSetSpatialGroup()
			{
				return false;
			}

			// Token: 0x06008692 RID: 34450 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetAudio3DSettings(Audio3DSettings setting)
			{
			}

			// Token: 0x06008693 RID: 34451 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsPlayInterval()
			{
				return false;
			}

			// Token: 0x06008694 RID: 34452 RVA: 0x0000216D File Offset: 0x0000036D
			private void setInterval()
			{
			}

			// Token: 0x06008695 RID: 34453 RVA: 0x0000216D File Offset: 0x0000036D
			public void UpdateAudio3DSettings(Audio3DSettings settings)
			{
			}

			// Token: 0x0400C126 RID: 49446
			private List<AudioManager.AudioInstance> playInstance;

			// Token: 0x0400C127 RID: 49447
			private List<AudioManager.AudioPlayer.PlayData> playSource;

			// Token: 0x0400C128 RID: 49448
			private AudioManager.AudioLabelSettings playSettings;

			// Token: 0x0400C129 RID: 49449
			private AudioClip playClip;

			// Token: 0x0400C12A RID: 49450
			private bool isSetClip;

			// Token: 0x0400C12B RID: 49451
			private int prevPlayIndex;

			// Token: 0x0400C12C RID: 49452
			private float prevVolumeRandom;

			// Token: 0x0400C12D RID: 49453
			private float prevPitchRandom;

			// Token: 0x0400C12E RID: 49454
			private float prevPanRandom;

			// Token: 0x0400C12F RID: 49455
			private List<int> prevPlaySamplesList;

			// Token: 0x0400C130 RID: 49456
			private string playerName;

			// Token: 0x0400C131 RID: 49457
			private AudioMixerGroup mixer;

			// Token: 0x0400C132 RID: 49458
			private Audio3DSettings spatialSettings;

			// Token: 0x0400C133 RID: 49459
			private float nextInterval;

			// Token: 0x0400C134 RID: 49460
			private float prevPlayTime;

			// Token: 0x0400C135 RID: 49461
			private bool force2D;

			// Token: 0x02001191 RID: 4497
			private class PlayData
			{
				// Token: 0x0400C136 RID: 49462
				public AudioClip clip;

				// Token: 0x0400C137 RID: 49463
				public AudioManager.AudioLabelSettings info;
			}
		}

		// Token: 0x02001192 RID: 4498
		private class AudioXmlLoad
		{
			// Token: 0x06008698 RID: 34456 RVA: 0x0000216A File Offset: 0x0000036A
			public static XmlDocument Load(Stream xml)
			{
				return null;
			}

			// Token: 0x06008699 RID: 34457 RVA: 0x0000216A File Offset: 0x0000036A
			public static XmlDocument Load(Stream xsd, Stream xml)
			{
				return null;
			}

			// Token: 0x0600869A RID: 34458 RVA: 0x0000216D File Offset: 0x0000036D
			private static void DebugParse(XmlDocument xmlDoc)
			{
			}

			// Token: 0x0600869B RID: 34459 RVA: 0x0000216D File Offset: 0x0000036D
			private static void OutputXml(XmlDocument xmlDoc)
			{
			}

			// Token: 0x0600869C RID: 34460 RVA: 0x0000216D File Offset: 0x0000036D
			private static void ValidationCallback(object sender, ValidationEventArgs args)
			{
			}
		}
	}
}
