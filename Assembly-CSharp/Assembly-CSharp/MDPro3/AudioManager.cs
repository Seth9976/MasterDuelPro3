using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MDPro3
{
	// Token: 0x02001259 RID: 4697
	public class AudioManager : Manager
	{
		// Token: 0x06008A55 RID: 35413 RVA: 0x001129BC File Offset: 0x00110BBC
		public override void Initialize()
		{
			base.Initialize();
			AudioManager.se = this.seR;
			AudioManager.bgm = this.bgmR;
			AudioManager.voice = this.voiceR;
			AudioSettings.OnAudioConfigurationChanged += this.OnAudioConfigurationChanged;
			AudioManager.PlayBGM("BGM_MENU_01", 1f);
		}

		// Token: 0x06008A56 RID: 35414 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnAudioConfigurationChanged(bool deviceWasChanged)
		{
		}

		// Token: 0x06008A57 RID: 35415 RVA: 0x00112A10 File Offset: 0x00110C10
		public static void SetSeVol(float vol)
		{
			AudioManager.se.volume = vol;
		}

		// Token: 0x06008A58 RID: 35416 RVA: 0x00112A1D File Offset: 0x00110C1D
		public static void SetBGMVol(float vol)
		{
			AudioManager.bgm.volume = vol * AudioManager.currentBGMScale;
		}

		// Token: 0x06008A59 RID: 35417 RVA: 0x00112A30 File Offset: 0x00110C30
		public static void SetVoiceVol(float vol)
		{
			AudioManager.voice.volume = vol;
		}

		// Token: 0x06008A5A RID: 35418 RVA: 0x00112A3D File Offset: 0x00110C3D
		public static IEnumerator<AudioClip> LoadAudioFileAsync(string path, AudioType audioType)
		{
			AudioManager.<LoadAudioFileAsync>d__12 <LoadAudioFileAsync>d__ = new AudioManager.<LoadAudioFileAsync>d__12(0);
			<LoadAudioFileAsync>d__.path = path;
			<LoadAudioFileAsync>d__.audioType = audioType;
			return <LoadAudioFileAsync>d__;
		}

		// Token: 0x06008A5B RID: 35419 RVA: 0x00112A54 File Offset: 0x00110C54
		public static async UniTask<AudioClip> LoadAudioFileUniAsync(string path, AudioType audioType)
		{
			string fullPath = Path.Combine(Environment.CurrentDirectory, path);
			AudioClip audioClip;
			using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(fullPath, audioType))
			{
				await request.SendWebRequest();
				if (request.result == UnityWebRequest.Result.Success)
				{
					audioClip = DownloadHandlerAudioClip.GetContent(request);
				}
				else
				{
					audioClip = null;
				}
			}
			return audioClip;
		}

		// Token: 0x06008A5C RID: 35420 RVA: 0x00112AA0 File Offset: 0x00110CA0
		public static void PlaySE(string path, float volumeScale = 1f)
		{
			if (string.IsNullOrEmpty(path))
			{
				return;
			}
			if (AudioManager.se == null)
			{
				return;
			}
			if (path == AudioManager.nextMuteSE)
			{
				AudioManager.nextMuteSE = string.Empty;
				return;
			}
			if (AudioManager.lastSE.time > 0f && AudioManager.lastSE.seName == path && Time.time - AudioManager.lastSE.time < 0.1f)
			{
				return;
			}
			AudioManager.lastSE.time = Time.time;
			AudioManager.lastSE.seName = path;
			Addressables.LoadAssetAsync<AudioClip>(path).Completed += delegate(AsyncOperationHandle<AudioClip> result)
			{
				if (result.Result != null)
				{
					AudioManager.se.PlayOneShot(result.Result, volumeScale);
				}
			};
		}

		// Token: 0x06008A5D RID: 35421 RVA: 0x00112B58 File Offset: 0x00110D58
		public void PlayShuffleSE()
		{
			List<string> ses = new List<string> { "SE_CARD_MOVE_01", "SE_CARD_MOVE_02", "SE_CARD_MOVE_03", "SE_CARD_MOVE_04" };
			base.StartCoroutine(AudioManager.PlaySEGroup(ses, 1f));
		}

		// Token: 0x06008A5E RID: 35422 RVA: 0x00112BA9 File Offset: 0x00110DA9
		private static IEnumerator PlaySEGroup(List<string> ses, float volumeScale = 1f)
		{
			foreach (string s in ses)
			{
				AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>(s);
				while (!handle.IsDone)
				{
					yield return null;
				}
				AudioManager.se.PlayOneShot(handle.Result, volumeScale);
				yield return new WaitForSeconds(handle.Result.length * 0.5f);
				handle = default(AsyncOperationHandle<AudioClip>);
			}
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06008A5F RID: 35423 RVA: 0x00112BBF File Offset: 0x00110DBF
		public static void PlaySEClip(AudioClip clip, float volumeScale = 1f)
		{
			AudioManager.se.PlayOneShot(clip, volumeScale);
		}

		// Token: 0x06008A60 RID: 35424 RVA: 0x00112BCD File Offset: 0x00110DCD
		public static void ResetSESource()
		{
			AudioManager.se.gameObject.SetActive(false);
			AudioManager.se.gameObject.SetActive(true);
		}

		// Token: 0x06008A61 RID: 35425 RVA: 0x00112BF0 File Offset: 0x00110DF0
		public static void PlayVoiceByResourcePath(string path)
		{
			AudioClip clip = Resources.Load<AudioClip>(path);
			if (clip != null)
			{
				AudioManager.voice.PlayOneShot(clip);
			}
		}

		// Token: 0x06008A62 RID: 35426 RVA: 0x00112C18 File Offset: 0x00110E18
		public static void PlayVoice(AudioClip clip)
		{
			AudioManager.voice.PlayOneShot(clip);
		}

		// Token: 0x06008A63 RID: 35427 RVA: 0x00112C25 File Offset: 0x00110E25
		private static int GetFieldIdByFieldName(string fieldName)
		{
			return int.Parse(fieldName.Substring(4, 3));
		}

		// Token: 0x06008A64 RID: 35428 RVA: 0x00112C34 File Offset: 0x00110E34
		private static List<string> GetBgmsByFieldId(int fieldId)
		{
			if (global::UnityEngine.Random.value < 0.05f)
			{
				return AudioManager.rateBgms;
			}
			foreach (KeyValuePair<int, List<int>> pair in AudioManager.fieldBGMs)
			{
				if (pair.Value.Contains(fieldId))
				{
					return new List<string>
					{
						string.Format("BGM_DUEL_NORMAL_{0:D2}", pair.Key),
						string.Format("BGM_DUEL_KEYCARD_{0:D2}", pair.Key),
						string.Format("BGM_DUEL_CLIMAX_{0:D2}", pair.Key)
					};
				}
			}
			if (fieldId == 8)
			{
				int index = AudioManager.colosseumBgms[global::UnityEngine.Random.Range(0, AudioManager.colosseumBgms.Count)];
				List<string> colBgmList;
				if (AudioManager.fieldColBGMs.TryGetValue(index, out colBgmList))
				{
					return colBgmList;
				}
			}
			if (fieldId == 18)
			{
				int index2 = AudioManager.cyberseBgms[global::UnityEngine.Random.Range(0, AudioManager.cyberseBgms.Count)];
				List<string> cybBgmList;
				if (AudioManager.fieldCybBGMs.TryGetValue(index2, out cybBgmList))
				{
					return cybBgmList;
				}
			}
			foreach (KeyValuePair<List<string>, List<int>> pair2 in AudioManager.specialFieldBGMs)
			{
				if (pair2.Value.Contains(fieldId))
				{
					return pair2.Key;
				}
			}
			int undefined = AudioManager.commonBgms[global::UnityEngine.Random.Range(0, AudioManager.commonBgms.Count)];
			return new List<string>
			{
				string.Format("BGM_DUEL_NORMAL_{0:D2}", undefined),
				string.Format("BGM_DUEL_KEYCARD_{0:D2}", undefined),
				string.Format("BGM_DUEL_CLIMAX_{0:D2}", undefined)
			};
		}

		// Token: 0x06008A65 RID: 35429 RVA: 0x00112E20 File Offset: 0x00111020
		public static void PlayBgmNormal(string filedName)
		{
			AudioManager.bgmState = 0;
			AudioManager.currentBGMs = AudioManager.GetBgmsByFieldId(AudioManager.GetFieldIdByFieldName(filedName));
			if (AudioManager.currentBGMs.Count > 0)
			{
				AudioManager.PlayBGM(AudioManager.currentBGMs[0], 1f);
			}
		}

		// Token: 0x06008A66 RID: 35430 RVA: 0x00112E5A File Offset: 0x0011105A
		public static void PlayBgmKeyCard()
		{
			if (AudioManager.bgmState > 0)
			{
				return;
			}
			AudioManager.bgmState = 1;
			if (AudioManager.currentBGMs.Count > 1)
			{
				AudioManager.PlayBGM(AudioManager.currentBGMs[1], 1f);
			}
		}

		// Token: 0x06008A67 RID: 35431 RVA: 0x00112E8D File Offset: 0x0011108D
		public static void PlayBgmClimax()
		{
			if (AudioManager.bgmState == 2)
			{
				return;
			}
			AudioManager.bgmState = 2;
			if (AudioManager.currentBGMs.Count > 2)
			{
				AudioManager.PlayBGM(AudioManager.currentBGMs[2], 1f);
			}
		}

		// Token: 0x06008A68 RID: 35432 RVA: 0x00112EC0 File Offset: 0x001110C0
		public static void PlayBGM(string path, float volumeScale = 1f)
		{
			AudioManager.currentBGMScale = volumeScale;
			Addressables.LoadAssetAsync<AudioClip>(path).Completed += delegate(AsyncOperationHandle<AudioClip> result)
			{
				float volume = Program.instance.setting.GetBGMVolum() * AudioManager.currentBGMScale;
				DOTween.To(() => volume, delegate(float x)
				{
					AudioManager.bgm.volume = x;
				}, 0f, 0.2f).OnComplete(delegate
				{
					AudioManager.SetCurrentBGM(path, result.Result.length);
					AudioManager.bgm.volume = volume;
					AudioManager.bgm.clip = result.Result;
					AudioManager.bgm.time = 0f;
					AudioManager.bgm.Play();
				});
			};
		}

		// Token: 0x06008A69 RID: 35433 RVA: 0x00112F00 File Offset: 0x00111100
		public static void StopBGM()
		{
			float volume = AudioManager.bgm.volume;
			DOTween.To(() => volume, delegate(float x)
			{
				AudioManager.bgm.volume = x;
			}, 0f, 0.5f).OnComplete(delegate
			{
				AudioManager.bgm.Stop();
				AudioManager.bgm.volume = volume;
			});
		}

		// Token: 0x06008A6A RID: 35434 RVA: 0x00112F70 File Offset: 0x00111170
		public static void PlayRandomKeyCardBGM()
		{
			int randomID = AudioManager.bgms[global::UnityEngine.Random.Range(0, AudioManager.bgms.Count)];
			AudioManager.PlayBGM(string.Format("BGM_DUEL_KEYCARD_{0:D2}", randomID), 1f);
		}

		// Token: 0x06008A6B RID: 35435 RVA: 0x00112FB4 File Offset: 0x001111B4
		private static void SetCurrentBGM(string bgm, float bgmLength)
		{
			AudioManager.currentBGM = bgm;
			bool found = false;
			foreach (AudioManager.BgmLoop loop in AudioManager.loops)
			{
				if (loop.name == AudioManager.currentBGM)
				{
					found = true;
					AudioManager.loopStart = loop.startTime;
					AudioManager.loopEnd = loop.endTime;
					break;
				}
			}
			if (!found)
			{
				AudioManager.loopStart = 0f;
				AudioManager.loopEnd = bgmLength - 1f;
			}
		}

		// Token: 0x06008A6C RID: 35436 RVA: 0x0011304C File Offset: 0x0011124C
		private void Update()
		{
			if (AudioManager.bgm == null)
			{
				return;
			}
			if (AudioManager.bgm.time > AudioManager.loopEnd)
			{
				AudioManager.bgm.time = AudioManager.loopStart;
			}
		}

		// Token: 0x0400C5E8 RID: 50664
		public AudioSource seR;

		// Token: 0x0400C5E9 RID: 50665
		public AudioSource bgmR;

		// Token: 0x0400C5EA RID: 50666
		public AudioSource voiceR;

		// Token: 0x0400C5EB RID: 50667
		private static AudioSource se;

		// Token: 0x0400C5EC RID: 50668
		private static AudioSource bgm;

		// Token: 0x0400C5ED RID: 50669
		private static AudioSource voice;

		// Token: 0x0400C5EE RID: 50670
		public const string BGM_MENU_MAIN = "BGM_MENU_01";

		// Token: 0x0400C5EF RID: 50671
		private static AudioManager.LastSE lastSE = default(AudioManager.LastSE);

		// Token: 0x0400C5F0 RID: 50672
		public static string nextMuteSE;

		// Token: 0x0400C5F1 RID: 50673
		private static List<string> currentBGMs = new List<string>();

		// Token: 0x0400C5F2 RID: 50674
		private static string currentBGM = string.Empty;

		// Token: 0x0400C5F3 RID: 50675
		private static int bgmState = 0;

		// Token: 0x0400C5F4 RID: 50676
		private static float loopStart = 0f;

		// Token: 0x0400C5F5 RID: 50677
		private static float loopEnd = 10f;

		// Token: 0x0400C5F6 RID: 50678
		private static readonly List<AudioManager.BgmLoop> loops = new List<AudioManager.BgmLoop>
		{
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_NORMAL_01",
				startTime = 9.6f,
				endTime = 115.2f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_NORMAL_02",
				startTime = 16.5f,
				endTime = 108.5f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_NORMAL_03",
				startTime = 5.727f,
				endTime = 131.444f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_NORMAL_04",
				startTime = 13.518f,
				endTime = 117.3f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_NORMAL_05",
				startTime = 11.208f,
				endTime = 142.875f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_NORMAL_06",
				startTime = 9.527f,
				endTime = 101.906f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_NORMAL_07",
				startTime = 17.456f,
				endTime = 129.247f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_NORMAL_08",
				startTime = 18.4f,
				endTime = 132.4f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_NORMAL_09",
				startTime = 6.2f,
				endTime = 111.4f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_NORMAL_10",
				startTime = 9.989f,
				endTime = 111.636f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_NORMAL_11",
				startTime = 2.378f,
				endTime = 89.65f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_NORMAL_12",
				startTime = 7.5f,
				endTime = 107.8f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_NORMAL_13",
				startTime = 7.433f,
				endTime = 114.741f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_NORMAL_14",
				startTime = 5.538f,
				endTime = 94.142f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_NORMAL_15",
				startTime = 8.455f,
				endTime = 94.854996f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_NORMAL_16",
				startTime = 14.44f,
				endTime = 104.44f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_KEYCARD_01",
				startTime = 11.744f,
				endTime = 109.39f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_KEYCARD_02",
				startTime = 10.5f,
				endTime = 106.5f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_KEYCARD_03",
				startTime = 13.697f,
				endTime = 98.15f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_KEYCARD_04",
				startTime = 7.032f,
				endTime = 109.888f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_KEYCARD_05",
				startTime = 12.495f,
				endTime = 83.079f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_KEYCARD_06",
				startTime = 11.4f,
				endTime = 98.4f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_KEYCARD_07",
				startTime = 6.518f,
				endTime = 84.928f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_KEYCARD_08",
				startTime = 13.783f,
				endTime = 117.727005f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_KEYCARD_09",
				startTime = 3.8f,
				endTime = 80.3f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_KEYCARD_10",
				startTime = 17.599f,
				endTime = 100.507996f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_KEYCARD_11",
				startTime = 11.738f,
				endTime = 117.104004f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_KEYCARD_12",
				startTime = 13.63f,
				endTime = 105.684f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_KEYCARD_13",
				startTime = 18.519f,
				endTime = 115.734f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_KEYCARD_14",
				startTime = 2.269f,
				endTime = 95.83f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_KEYCARD_15",
				startTime = 11.369f,
				endTime = 101.369f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_KEYCARD_16",
				startTime = 6.348f,
				endTime = 96.151f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_CLIMAX_01",
				startTime = 6.3f,
				endTime = 97.8f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_CLIMAX_02",
				startTime = 12.883f,
				endTime = 113.958f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_CLIMAX_03",
				startTime = 12.579f,
				endTime = 127.444f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_CLIMAX_04",
				startTime = 3.325f,
				endTime = 91.047f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_CLIMAX_05",
				startTime = 5.424f,
				endTime = 97.188f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_CLIMAX_06",
				startTime = 5.896f,
				endTime = 86.184f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_CLIMAX_07",
				startTime = 11.5f,
				endTime = 91.5f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_CLIMAX_08",
				startTime = 15.547f,
				endTime = 108.505005f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_CLIMAX_09",
				startTime = 6.3f,
				endTime = 88.8f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_CLIMAX_10",
				startTime = 2.5f,
				endTime = 94.5f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_CLIMAX_11",
				startTime = 13.223f,
				endTime = 103.955f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_CLIMAX_12",
				startTime = 6.448f,
				endTime = 94.252f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_CLIMAX_13",
				startTime = 5.637f,
				endTime = 110.429f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_CLIMAX_14",
				startTime = 12.169f,
				endTime = 108.165f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_CLIMAX_15",
				startTime = 7.056f,
				endTime = 99.847f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_CLIMAX_16",
				startTime = 9.606f,
				endTime = 88.067f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_DC01_NORMAL",
				startTime = 3.83f,
				endTime = 121.649f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_DC01_KEYCARD",
				startTime = 3.128f,
				endTime = 124.974f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_DC01_CLIMAX",
				startTime = 7.65f,
				endTime = 122.62f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_DC02_NORMAL",
				startTime = 6.892f,
				endTime = 124.404f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_DC02_KEYCARD",
				startTime = 21.349f,
				endTime = 120.908f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_DC02_CLIMAX",
				startTime = 1.858f,
				endTime = 115.380005f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_01",
				startTime = 21.014f,
				endTime = 117.026f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_02_NORMAL",
				startTime = 2.466f,
				endTime = 107.193f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_02_KEYCARD",
				startTime = 6.941f,
				endTime = 106.766f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_02_CLIMAX",
				startTime = 2.346f,
				endTime = 103.21f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_03_NORMAL",
				startTime = 11.478f,
				endTime = 116.473f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_03_KEYCARD",
				startTime = 12.463f,
				endTime = 106.098f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_03_CLIMAX",
				startTime = 1.815f,
				endTime = 128.79199f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_04_NORMAL",
				startTime = 3.391f,
				endTime = 106.25f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_04_KEYCARD",
				startTime = 3.139f,
				endTime = 90.436005f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_04_CLIMAX",
				startTime = 3.063f,
				endTime = 112.417f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_05_NORMAL",
				startTime = 8.796f,
				endTime = 125.869f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_05_KEYCARD",
				startTime = 7.832f,
				endTime = 122.615f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_05_CLIMAX",
				startTime = 2.039f,
				endTime = 126.835f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_06_ALL",
				startTime = 4.95f,
				endTime = 187.141f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_07_PHASE_A",
				startTime = 20.499f,
				endTime = 145.909f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_07_PHASE_B",
				startTime = 7.757f,
				endTime = 147.756f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_08_ALL",
				startTime = 21.667f,
				endTime = 197.979f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_09_PHASE_A",
				startTime = 5.488f,
				endTime = 123.416f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_09_PHASE_B",
				startTime = 0.802f,
				endTime = 122.501f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_10_NORMAL",
				startTime = 0f,
				endTime = 87.832f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_10_KEYCARD",
				startTime = 9.512f,
				endTime = 90.265f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_EX_10_CLIMAX",
				startTime = 1.516f,
				endTime = 91.898f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F01_ALL",
				startTime = 24.219f,
				endTime = 162.886f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F02_PHASE_A",
				startTime = 13.603f,
				endTime = 103.324005f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F02_PHASE_B",
				startTime = 14.818f,
				endTime = 122.645f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F03_PHASE_A",
				startTime = 2.905f,
				endTime = 102.119995f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F03_PHASE_B",
				startTime = 9.52f,
				endTime = 90.959f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F03_PHASE_C",
				startTime = 28.667f,
				endTime = 109.794f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F04_PHASE_A",
				startTime = 32.952f,
				endTime = 116.522995f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F04_PHASE_B",
				startTime = 19.02f,
				endTime = 146.755f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F05_PHASE_A",
				startTime = 13.323f,
				endTime = 135.155f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F05_PHASE_B",
				startTime = 7.291f,
				endTime = 133.283f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F06_PHASE_A",
				startTime = 3.831f,
				endTime = 129.429f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F06_PHASE_B",
				startTime = 0.739f,
				endTime = 132.236f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F07_PHASE_A",
				startTime = 24.566f,
				endTime = 128.425f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F07_PHASE_B",
				startTime = 0.552f,
				endTime = 84.069f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F08_PHASE_A",
				startTime = 0f,
				endTime = 130.439f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F08_PHASE_B",
				startTime = 8.569f,
				endTime = 128.047f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F09_PHASE_A",
				startTime = 0.757f,
				endTime = 138.982f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F09_PHASE_B",
				startTime = 39.995f,
				endTime = 127.306f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F10_PHASE_A",
				startTime = 0.507f,
				endTime = 105.979996f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F10_PHASE_B",
				startTime = 9.092f,
				endTime = 115.03f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_F10_PHASE_C",
				startTime = 1.353f,
				endTime = 142.772f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_RATE01_NORMAL",
				startTime = 1.119f,
				endTime = 125.607f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_RATE01_KEYCARD",
				startTime = 1.761f,
				endTime = 115.984f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DUEL_RATE01_CLIMAX",
				startTime = 0.822f,
				endTime = 113.925f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_MENU_01",
				startTime = 12.433f,
				endTime = 151.1f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_MENU_02",
				startTime = 15.687f,
				endTime = 122.354004f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_MENU_RETRO",
				startTime = 0f,
				endTime = 138.666f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_DICERALLY",
				startTime = 17.72f,
				endTime = 72.578f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_MD_TEST_QUIZ",
				startTime = 0.226f,
				endTime = 49.559f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_SOLO_GATE",
				startTime = 10.053f,
				endTime = 96.453995f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_TUTORIAL_01",
				startTime = 13.992f,
				endTime = 43.326f
			},
			new AudioManager.BgmLoop
			{
				name = "BGM_OUT_TUTORIAL_2",
				startTime = 7.48f,
				endTime = 82.479996f
			}
		};

		// Token: 0x0400C5F7 RID: 50679
		private static readonly List<int> bgms = new List<int>
		{
			1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
			11, 12, 13, 14, 15, 16
		};

		// Token: 0x0400C5F8 RID: 50680
		private static readonly List<int> commonBgms = new List<int> { 1, 3, 7 };

		// Token: 0x0400C5F9 RID: 50681
		private static readonly List<int> colosseumBgms = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };

		// Token: 0x0400C5FA RID: 50682
		private static readonly List<int> cyberseBgms = new List<int> { 1, 2 };

		// Token: 0x0400C5FB RID: 50683
		private static readonly Dictionary<int, List<int>> fieldBGMs = new Dictionary<int, List<int>>
		{
			{
				1,
				new List<int> { 2, 12, 29, 31 }
			},
			{
				2,
				new List<int> { 4, 3, 21 }
			},
			{
				3,
				new List<int> { 1, 11 }
			},
			{
				4,
				new List<int> { 5, 6 }
			},
			{
				5,
				new List<int> { 7 }
			},
			{
				6,
				new List<int> { 15, 24 }
			},
			{
				7,
				new List<int> { 9 }
			},
			{
				8,
				new List<int>()
			},
			{
				9,
				new List<int> { 16, 17 }
			},
			{
				10,
				new List<int>()
			},
			{
				11,
				new List<int> { 26, 50 }
			},
			{
				12,
				new List<int> { 19, 25, 36 }
			},
			{
				13,
				new List<int> { 14 }
			},
			{
				14,
				new List<int> { 22 }
			},
			{
				15,
				new List<int> { 10, 35 }
			},
			{
				16,
				new List<int> { 20, 27, 28 }
			}
		};

		// Token: 0x0400C5FC RID: 50684
		private static readonly Dictionary<int, List<string>> fieldColBGMs = new Dictionary<int, List<string>>
		{
			{
				1,
				new List<string> { "BGM_DUEL_EX_02_NORMAL", "BGM_DUEL_EX_02_KEYCARD", "BGM_DUEL_EX_02_CLIMAX" }
			},
			{
				2,
				new List<string> { "BGM_DUEL_DC01_NORMAL", "BGM_DUEL_DC01_KEYCARD", "BGM_DUEL_DC01_CLIMAX" }
			},
			{
				3,
				new List<string> { "BGM_DUEL_DC02_NORMAL", "BGM_DUEL_DC02_KEYCARD", "BGM_DUEL_DC02_CLIMAX" }
			},
			{
				4,
				new List<string> { "BGM_DUEL_EX_04_NORMAL", "BGM_DUEL_EX_04_KEYCARD", "BGM_DUEL_EX_04_CLIMAX" }
			},
			{
				5,
				new List<string> { "BGM_DUEL_EX_05_NORMAL", "BGM_DUEL_EX_05_KEYCARD", "BGM_DUEL_EX_05_CLIMAX" }
			},
			{
				6,
				new List<string> { "BGM_DUEL_EX_06_ALL" }
			},
			{
				7,
				new List<string> { "BGM_DUEL_EX_07_PHASE_A", "BGM_DUEL_EX_07_PHASE_B" }
			},
			{
				8,
				new List<string> { "BGM_DUEL_EX_10_NORMAL", "BGM_DUEL_EX_10_KEYCARD", "BGM_DUEL_EX_10_CLIMAX" }
			}
		};

		// Token: 0x0400C5FD RID: 50685
		private static readonly Dictionary<int, List<string>> fieldCybBGMs = new Dictionary<int, List<string>>
		{
			{
				1,
				new List<string> { "BGM_DUEL_NORMAL_12", "BGM_DUEL_KEYCARD_12", "BGM_DUEL_CLIMAX_12" }
			},
			{
				2,
				new List<string> { "BGM_DUEL_EX_01" }
			}
		};

		// Token: 0x0400C5FE RID: 50686
		private static readonly Dictionary<List<string>, List<int>> specialFieldBGMs = new Dictionary<List<string>, List<int>>
		{
			{
				new List<string> { "BGM_DUEL_EX_03_NORMAL", "BGM_DUEL_EX_03_KEYCARD", "BGM_DUEL_EX_03_CLIMAX" },
				new List<int> { 13 }
			},
			{
				new List<string> { "BGM_DUEL_EX_08_ALL" },
				new List<int> { 45 }
			},
			{
				new List<string> { "BGM_DUEL_EX_09_PHASE_A", "BGM_DUEL_EX_09_PHASE_B" },
				new List<int> { 504 }
			},
			{
				new List<string> { "BGM_DUEL_F01_ALL" },
				new List<int> { 38 }
			},
			{
				new List<string> { "BGM_DUEL_F02_PHASE_A", "BGM_DUEL_F02_PHASE_B" },
				new List<int> { 34 }
			},
			{
				new List<string> { "BGM_DUEL_F03_PHASE_A", "BGM_DUEL_F03_PHASE_B", "BGM_DUEL_F03_PHASE_C" },
				new List<int> { 40 }
			},
			{
				new List<string> { "BGM_DUEL_F04_PHASE_A", "BGM_DUEL_F04_PHASE_B" },
				new List<int> { 42 }
			},
			{
				new List<string> { "BGM_DUEL_F05_PHASE_A", "BGM_DUEL_F05_PHASE_B" },
				new List<int> { 41 }
			},
			{
				new List<string> { "BGM_DUEL_F06_PHASE_A", "BGM_DUEL_F06_PHASE_B" },
				new List<int> { 47 }
			},
			{
				new List<string> { "BGM_DUEL_F07_PHASE_A", "BGM_DUEL_F07_PHASE_B" },
				new List<int> { 46 }
			},
			{
				new List<string> { "BGM_DUEL_F08_PHASE_A", "BGM_DUEL_F08_PHASE_B" },
				new List<int> { 39 }
			},
			{
				new List<string> { "BGM_DUEL_F09_PHASE_A", "BGM_DUEL_F09_PHASE_B" },
				new List<int> { 48 }
			},
			{
				new List<string> { "BGM_DUEL_F10_PHASE_A", "BGM_DUEL_F10_PHASE_B", "BGM_DUEL_F10_PHASE_C" },
				new List<int> { 49 }
			}
		};

		// Token: 0x0400C5FF RID: 50687
		private static readonly List<string> rateBgms = new List<string> { "BGM_DUEL_RATE01_NORMAL", "BGM_DUEL_RATE01_KEYCARD", "BGM_DUEL_RATE01_CLIMAX" };

		// Token: 0x0400C600 RID: 50688
		private const float RateBgmChance = 0.05f;

		// Token: 0x0400C601 RID: 50689
		private static float currentBGMScale = 1f;

		// Token: 0x0200125A RID: 4698
		private struct LastSE
		{
			// Token: 0x0400C602 RID: 50690
			public float time;

			// Token: 0x0400C603 RID: 50691
			public string seName;
		}

		// Token: 0x0200125B RID: 4699
		private enum BgmType
		{
			// Token: 0x0400C605 RID: 50693
			NORMAL,
			// Token: 0x0400C606 RID: 50694
			KEYCARD,
			// Token: 0x0400C607 RID: 50695
			CLIMAX
		}

		// Token: 0x0200125C RID: 4700
		private struct BgmLoop
		{
			// Token: 0x0400C608 RID: 50696
			public string name;

			// Token: 0x0400C609 RID: 50697
			public float startTime;

			// Token: 0x0400C60A RID: 50698
			public float endTime;
		}
	}
}
