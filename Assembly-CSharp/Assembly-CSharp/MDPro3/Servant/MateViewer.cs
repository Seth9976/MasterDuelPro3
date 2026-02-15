using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MDPro3.UI;
using MDPro3.UI.PropertyOverride;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MDPro3.Servant
{
	// Token: 0x020012DE RID: 4830
	public class MateViewer : Servant
	{
		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x06008D26 RID: 36134 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool ShowLine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x06008D27 RID: 36135 RVA: 0x0000763C File Offset: 0x0000583C
		public override int Depth
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06008D28 RID: 36136 RVA: 0x001290D3 File Offset: 0x001272D3
		public override void Initialize()
		{
			this.returnServant = Program.instance.menu;
			base.Initialize();
			this.targetCamera = Program.instance.camera_.cameraDuelOverlay2D;
			this.LoadSeData();
		}

		// Token: 0x06008D29 RID: 36137 RVA: 0x00129108 File Offset: 0x00127308
		protected override void ApplyShowArrangement(int preDepth)
		{
			base.ApplyShowArrangement(preDepth);
			Program.instance.camera_.light.gameObject.SetActive(true);
			Program.instance.camera_.light.transform.GetChild(0).localEulerAngles = new Vector3(123f, -28f, -40f);
			Program.instance.camera_.light.transform.GetChild(1).localEulerAngles = new Vector3(-80f, -140f, 0f);
			CameraManager.DuelOverlay2DPlus();
			this.CameraReset();
			AudioManager.PlayBGM("BGM_OUT_TUTORIAL_2", 0.5f);
			UserInput.SetMoveRepeatRate(0.05f);
		}

		// Token: 0x06008D2A RID: 36138 RVA: 0x001291C0 File Offset: 0x001273C0
		protected override void ApplyHideArrangement(int preDepth)
		{
			base.ApplyHideArrangement(preDepth);
			CameraManager.DuelOverlay2DMinus();
			Program.instance.camera_.light.gameObject.SetActive(false);
			Program.instance.camera_.light.transform.GetChild(0).localEulerAngles = new Vector3(96f, -28f, -40f);
			Program.instance.camera_.light.transform.GetChild(1).localEulerAngles = new Vector3(-15f, -45f, 0f);
			if (this.mate != null)
			{
				global::UnityEngine.Object.Destroy(this.mate.gameObject);
			}
			AudioManager.ResetSESource();
			AudioManager.PlaySE("SE_MENU_CANCEL", 1f);
			AudioManager.PlayBGM("BGM_MENU_01", 1f);
			UserInput.SetMoveRepeatRate(0.1f);
		}

		// Token: 0x06008D2B RID: 36139 RVA: 0x001292A4 File Offset: 0x001274A4
		public override void PerFrameFunction()
		{
			if (!this.showing)
			{
				return;
			}
			if (this.NeedResponseInput())
			{
				if (UserInput.MouseRightDown || UserInput.WasCancelPressed)
				{
					this.OnReturn();
				}
				if (UserInput.WasGamepadButtonWestPressed)
				{
					this.GetUI<MateViewerUI>().FocusOnInputField();
				}
				if (this.mate == null)
				{
					return;
				}
				if (UserInput.WasGamepadButtonNorthPressed)
				{
					this.GetUI<MateViewerUI>().OnMateTap();
				}
				float leftOffset = (PropertyOverrider.NeedMobileLayout() ? 532f : 432f) * (float)Screen.height / 1080f;
				if (UserInput.MouseLeftDown && UserInput.MousePos.x > leftOffset)
				{
					float widthOffset = (float)Screen.width - leftOffset;
					if (UserInput.MousePos.x > leftOffset + widthOffset / 2f)
					{
						this.clickInRight = true;
						this.clickInTime = Time.time;
						this.mateAngel = this.mate.transform.eulerAngles;
						this.clickInPosition = UserInput.MousePos;
						this.oSize = this.targetCamera.orthographicSize;
					}
					else
					{
						this.clickInLeft = true;
						this.clickInTime = Time.time;
						this.clickInPosition = UserInput.MousePos;
						this.matePosition = this.mate.transform.position;
					}
				}
				if (UserInput.MouseLeftPressing && this.clickInLeft)
				{
					float x = this.matePosition.x + (this.clickInPosition.x - UserInput.MousePos.x) * 0.01f;
					float y = this.matePosition.y + (UserInput.MousePos.y - this.clickInPosition.y) * 0.02f;
					if (x > 10f)
					{
						x = 10f;
					}
					if (x < -10f)
					{
						x = -10f;
					}
					if (y > 0f)
					{
						y = 0f;
					}
					if (y < -20f)
					{
						y = -20f;
					}
					this.mate.transform.position = new Vector3(x, y, this.matePosition.z);
				}
				if (UserInput.MouseLeftPressing && this.clickInRight)
				{
					this.mate.transform.eulerAngles = this.mateAngel + new Vector3(0f, (this.clickInPosition.x - UserInput.MousePos.x) * 0.2f, 0f);
					float size = this.oSize + (this.clickInPosition.y - UserInput.MousePos.y) * 0.01f;
					if (size < 5f)
					{
						size = 5f;
					}
					if (size > 20f)
					{
						size = 20f;
					}
					this.targetCamera.orthographicSize = size;
					this.targetCamera.transform.localPosition = new Vector3(0f, size * 0.95f, 200f);
				}
				if (UserInput.MouseLeftUp && (this.clickInLeft || this.clickInRight))
				{
					this.clickInLeft = false;
					this.clickInRight = false;
					if (Time.time - this.clickInTime < 0.2f)
					{
						this.mate.Play(Mate.MateAction.Tap);
						this.mate.ActiveCamera(Mate.MateAction.Tap, this.targetCamera.gameObject.layer);
					}
				}
				if (UserInput.LeftScrollWheel != Vector2.zero)
				{
					float x2 = this.mate.transform.position.x - UserInput.LeftScrollWheel.x * Time.unscaledDeltaTime * 50f;
					if (x2 > 10f)
					{
						x2 = 10f;
					}
					if (x2 < -10f)
					{
						x2 = -10f;
					}
					float y2 = this.mate.transform.position.y + UserInput.LeftScrollWheel.y * Time.unscaledDeltaTime * 50f;
					if (y2 > 0f)
					{
						y2 = 0f;
					}
					if (y2 < -20f)
					{
						y2 = -20f;
					}
					this.mate.transform.position = new Vector3(x2, y2, this.mate.transform.position.z);
				}
				if (UserInput.RightScrollWheel != Vector2.zero)
				{
					float x3 = UserInput.RightScrollWheel.x * Time.unscaledDeltaTime * 200f;
					this.mate.transform.Rotate(Vector3.up, -x3);
					if (Mathf.Abs(UserInput.RightScrollWheel.y) > 0.3f)
					{
						float size2 = this.targetCamera.orthographicSize - UserInput.RightScrollWheel.y * Time.unscaledDeltaTime * 30f;
						if (size2 < 5f)
						{
							size2 = 5f;
						}
						if (size2 > 20f)
						{
							size2 = 20f;
						}
						this.targetCamera.orthographicSize = size2;
						this.targetCamera.transform.localPosition = new Vector3(0f, size2 * 0.95f, 200f);
					}
				}
			}
		}

		// Token: 0x06008D2C RID: 36140 RVA: 0x00129783 File Offset: 0x00127983
		public override void Select(bool forced = false)
		{
			if (!forced && !UserInput.NeedDefaultSelect())
			{
				return;
			}
			if (this.lastSelectedMateItem == null)
			{
				return;
			}
			this.lastSelectedMateItem.GetSelectable().Select();
		}

		// Token: 0x06008D2D RID: 36141 RVA: 0x001297AF File Offset: 0x001279AF
		public void LoadMates()
		{
			if (this.servantUI == null)
			{
				return;
			}
			this.GetUI<MateViewerUI>().Load();
		}

		// Token: 0x06008D2E RID: 36142 RVA: 0x00127555 File Offset: 0x00125755
		public void SelectLastMateItem()
		{
			UserInput.NextSelectionIsAxis = true;
			this.Select(false);
		}

		// Token: 0x06008D2F RID: 36143 RVA: 0x001297CC File Offset: 0x001279CC
		private void CameraReset()
		{
			this.targetCamera.transform.localPosition = new Vector3(0f, 19f, 200f);
			this.targetCamera.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
			this.targetCamera.orthographicSize = 20f;
		}

		// Token: 0x06008D30 RID: 36144 RVA: 0x00129831 File Offset: 0x00127A31
		public void ViewMate(int code)
		{
			if (this.mate != null)
			{
				if (this.mate.code == code)
				{
					return;
				}
				global::UnityEngine.Object.Destroy(this.mate.gameObject);
			}
			this.LoadMateAsync(code);
		}

		// Token: 0x06008D31 RID: 36145 RVA: 0x00129868 File Offset: 0x00127A68
		private async UniTask LoadMateAsync(int code)
		{
			Mate mate = await ABLoader.LoadMateAsync(code);
			this.mate = mate;
			Tools.ChangeLayer(this.mate.gameObject, this.targetCamera.gameObject.layer, false);
			await UniTask.WaitForSeconds(0.1f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			AudioManager.ResetSESource();
			this.mate.gameObject.SetActive(true);
			this.mate.Play(Mate.MateAction.Entry);
			this.mate.ActiveCamera(Mate.MateAction.Entry, this.targetCamera.gameObject.layer);
			this.CameraReset();
		}

		// Token: 0x06008D32 RID: 36146 RVA: 0x001298B4 File Offset: 0x00127AB4
		private void LoadSeData()
		{
			AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>("EffectSeLabelData");
			handle.Completed += delegate(AsyncOperationHandle<TextAsset> result)
			{
				string[] lines = Regex.Split(handle.Result.text, "\r\n");
				for (int i = 1; i < lines.Length; i++)
				{
					string[] splits = lines[i].Split(',', StringSplitOptions.None);
					if (splits.Length == 7)
					{
						MateViewer.CrossDuelSeLabelData seData = default(MateViewer.CrossDuelSeLabelData);
						seData.name = splits[0];
						seData.label1 = splits[1];
						if (string.IsNullOrEmpty(splits[2]))
						{
							seData.start1 = 0f;
						}
						else
						{
							seData.start1 = float.Parse(splits[2]);
						}
						seData.label2 = splits[3];
						if (string.IsNullOrEmpty(splits[4]))
						{
							seData.start2 = 0f;
						}
						else
						{
							seData.start2 = float.Parse(splits[4]);
						}
						seData.label3 = splits[5];
						if (string.IsNullOrEmpty(splits[6]))
						{
							seData.start3 = 0f;
						}
						else
						{
							seData.start3 = float.Parse(splits[6]);
						}
						MateViewer.cdSeData.Add(seData);
					}
				}
			};
		}

		// Token: 0x06008D33 RID: 36147 RVA: 0x001298F0 File Offset: 0x00127AF0
		public static void PlayCrossDuelSe(string name)
		{
			MateViewer.CrossDuelSeLabelData data = default(MateViewer.CrossDuelSeLabelData);
			bool found = false;
			foreach (MateViewer.CrossDuelSeLabelData seData in MateViewer.cdSeData)
			{
				if (seData.name == name)
				{
					data = seData;
					found = true;
				}
			}
			if (!found)
			{
				return;
			}
			if (!string.IsNullOrEmpty(data.label1))
			{
				DOTween.To(delegate(float v)
				{
				}, 0f, 0f, data.start1).OnComplete(delegate
				{
					AudioManager.PlaySE(data.label1.ToUpper(), 1f);
				});
			}
			if (!string.IsNullOrEmpty(data.label2))
			{
				DOTween.To(delegate(float v)
				{
				}, 0f, 0f, data.start2).OnComplete(delegate
				{
					AudioManager.PlaySE(data.label2.ToUpper(), 1f);
				});
			}
			if (!string.IsNullOrEmpty(data.label3))
			{
				DOTween.To(delegate(float v)
				{
				}, 0f, 0f, data.start3).OnComplete(delegate
				{
					AudioManager.PlaySE(data.label3.ToUpper(), 1f);
				});
			}
		}

		// Token: 0x0400CB21 RID: 52001
		[HideInInspector]
		public SelectionToggle_Mate lastSelectedMateItem;

		// Token: 0x0400CB22 RID: 52002
		[HideInInspector]
		public Mate mate;

		// Token: 0x0400CB23 RID: 52003
		private Camera targetCamera;

		// Token: 0x0400CB24 RID: 52004
		private Vector2 clickInPosition;

		// Token: 0x0400CB25 RID: 52005
		private Vector3 mateAngel;

		// Token: 0x0400CB26 RID: 52006
		private Vector3 matePosition;

		// Token: 0x0400CB27 RID: 52007
		private float oSize;

		// Token: 0x0400CB28 RID: 52008
		private float clickInTime;

		// Token: 0x0400CB29 RID: 52009
		private bool clickInLeft;

		// Token: 0x0400CB2A RID: 52010
		private bool clickInRight;

		// Token: 0x0400CB2B RID: 52011
		private static readonly List<MateViewer.CrossDuelSeLabelData> cdSeData = new List<MateViewer.CrossDuelSeLabelData>();

		// Token: 0x020012DF RID: 4831
		public struct CrossDuelSeLabelData
		{
			// Token: 0x0400CB2C RID: 52012
			public string name;

			// Token: 0x0400CB2D RID: 52013
			public string label1;

			// Token: 0x0400CB2E RID: 52014
			public float start1;

			// Token: 0x0400CB2F RID: 52015
			public string label2;

			// Token: 0x0400CB30 RID: 52016
			public float start2;

			// Token: 0x0400CB31 RID: 52017
			public string label3;

			// Token: 0x0400CB32 RID: 52018
			public float start3;
		}
	}
}
