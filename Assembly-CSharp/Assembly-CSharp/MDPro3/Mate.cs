using System;
using MDPro3.Servant;
using MDPro3.UI;
using UnityEngine;
using UnityEngine.Playables;
using Willow;

namespace MDPro3
{
	// Token: 0x02001288 RID: 4744
	public class Mate : MonoBehaviour
	{
		// Token: 0x06008B4E RID: 35662 RVA: 0x0011A574 File Offset: 0x00118774
		private bool Playing()
		{
			return (this.directorA != null && this.directorA.state == PlayState.Playing) || (this.directorB != null && this.directorB.state == PlayState.Playing) || (this.directorD != null && this.directorD.state == PlayState.Playing) || (this.directorE != null && this.directorE.state == PlayState.Playing) || (this.directorI != null && this.directorI.state == PlayState.Playing) || (this.directorI != null && this.directorI.state == PlayState.Playing) || (this.directorJ != null && this.directorJ.state == PlayState.Playing) || (this.directorK != null && this.directorK.state == PlayState.Playing) || (this.directorM != null && this.directorM.state == PlayState.Playing) || (this.directorN != null && this.directorN.state == PlayState.Playing) || (this.directorO != null && this.directorO.state == PlayState.Playing);
		}

		// Token: 0x06008B4F RID: 35663 RVA: 0x0011A6CC File Offset: 0x001188CC
		private void Start()
		{
			if (this.type == Mate.MateType.MasterDuel)
			{
				this.m_collider = base.gameObject.AddComponent<BoxCollider>();
				this.m_collider.size = new Vector3(10f, 10f, 10f);
				this.m_collider.center = new Vector3(0f, 5f, 0f);
				base.transform.GetChild(0).gameObject.AddComponent<EventSEPlayer>();
				Animator animator = base.transform.GetChild(0).GetComponent<Animator>();
				if (animator != null)
				{
					animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
					return;
				}
			}
			else
			{
				Tools.SetPlayableDirectorUnscaledGameTime(base.transform);
				base.transform.localEulerAngles = Vector3.zero;
				this.mesh = base.transform.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
				this.mesh.updateWhenOffscreen = false;
				Bounds bounds = this.mesh.bounds;
				bounds.extents = new Vector3(1000f, 1000f, 1000f);
				bounds.center = Vector3.zero;
				this.m_collider = base.gameObject.AddComponent<BoxCollider>();
				this.m_collider.size = new Vector3(2f, 2f, 2f);
				this.m_collider.center = new Vector3(0f, 1f, 0f);
				if (Program.instance.currentServant == Program.instance.ocgcore)
				{
					Tools.ChangeLayer(base.gameObject, "Default", false);
				}
				for (int i = 0; i < base.transform.childCount; i++)
				{
					CustomTimelineController controller;
					if ((controller = base.transform.GetChild(i).GetComponent<CustomTimelineController>()) != null)
					{
						PlayableDirector director = controller.currentDirector;
						if (director.name.ToLower().Contains("_a_"))
						{
							this.directorA = director;
						}
						if (director.name.ToLower().Contains("_b_"))
						{
							this.directorB = director;
						}
						if (director.name.ToLower().Contains("_d_"))
						{
							this.directorD = director;
						}
						if (director.name.ToLower().Contains("_e_"))
						{
							this.directorE = director;
						}
						if (director.name.ToLower().Contains("_i_"))
						{
							this.directorI = director;
						}
						if (director.name.ToLower().Contains("_j_"))
						{
							this.directorJ = director;
						}
						if (director.name.ToLower().Contains("_k_"))
						{
							this.directorK = director;
						}
						if (director.name.ToLower().Contains("_m_"))
						{
							this.directorM = director;
							this.huge = true;
						}
						if (director.name.ToLower().Contains("_n_"))
						{
							this.directorN = director;
						}
						if (director.name.ToLower().Contains("_o_"))
						{
							this.directorO = director;
						}
					}
				}
				base.transform.localScale = Vector3.one * 5f;
				if (this.huge)
				{
					base.transform.localScale = Vector3.one * 4f;
				}
				Animator animator2 = base.GetComponent<Animator>();
				if (animator2 != null)
				{
					animator2.cullingMode = AnimatorCullingMode.AlwaysAnimate;
				}
			}
		}

		// Token: 0x06008B50 RID: 35664 RVA: 0x0011AA40 File Offset: 0x00118C40
		public void Play(Mate.MateAction action)
		{
			Mate.MateType mateType = this.type;
			if (mateType != Mate.MateType.MasterDuel)
			{
				if (mateType != Mate.MateType.CrossDuel)
				{
					return;
				}
				Animator animator = base.GetComponent<Animator>();
				if (!animator.GetBool("IsKnockDown"))
				{
					switch (action)
					{
					case Mate.MateAction.Initial:
						animator.SetBool("IsVisible", true);
						animator.SetBool("IsFaceUp", true);
						animator.SetBool("IsAttackPosition", true);
						animator.SetTrigger("Update");
						return;
					case Mate.MateAction.Entry:
						base.transform.SetParent(this.parent, false);
						animator.SetBool("IsVisible", true);
						animator.SetBool("IsFaceUp", true);
						animator.SetBool("IsAttackPosition", true);
						animator.SetTrigger("Update");
						if (this.directorI != null)
						{
							this.directorI.Play();
							MateViewer.PlayCrossDuelSe(this.directorI.name.Replace("(Clone)", string.Empty));
							this.mesh.updateWhenOffscreen = true;
							return;
						}
						if (this.directorA != null)
						{
							this.directorA.Play();
							MateViewer.PlayCrossDuelSe(this.directorA.name.Replace("(Clone)", string.Empty));
							return;
						}
						break;
					case Mate.MateAction.Tap:
						if (!this.Playing())
						{
							if (this.directorM != null && this.directorN != null && this.directorO != null)
							{
								switch (global::UnityEngine.Random.Range(0, 3))
								{
								case 0:
									this.directorM.Play();
									MateViewer.PlayCrossDuelSe(this.directorM.name.Replace("(Clone)", string.Empty));
									return;
								case 1:
									this.directorN.Play();
									MateViewer.PlayCrossDuelSe(this.directorN.name.Replace("(Clone)", string.Empty));
									return;
								case 2:
									this.directorO.Play();
									MateViewer.PlayCrossDuelSe(this.directorO.name.Replace("(Clone)", string.Empty));
									return;
								default:
									return;
								}
							}
							else
							{
								if (this.directorM != null)
								{
									this.directorM.Play();
									MateViewer.PlayCrossDuelSe(this.directorM.name.Replace("(Clone)", string.Empty));
									return;
								}
								if (this.directorD != null)
								{
									this.directorD.Play();
									MateViewer.PlayCrossDuelSe(this.directorD.name.Replace("(Clone)", string.Empty));
									return;
								}
							}
						}
						break;
					case Mate.MateAction.BattlePhase:
					case Mate.MateAction.GetDamage:
					case Mate.MateAction.Victory:
						break;
					case Mate.MateAction.Attack:
						if (this.directorE != null)
						{
							this.directorE.Play();
							MateViewer.PlayCrossDuelSe(this.directorE.name.Replace("(Clone)", string.Empty));
							return;
						}
						break;
					case Mate.MateAction.Defeat:
						if (this.directorB != null)
						{
							this.directorB.Play();
							MateViewer.PlayCrossDuelSe(this.directorB.name.Replace("(Clone)", string.Empty));
							animator.SetBool("IsKnockDown", true);
							animator.SetBool("IsVisible", false);
							return;
						}
						animator.SetBool("IsAttackPosition", false);
						break;
					default:
						return;
					}
				}
			}
			else
			{
				switch (action)
				{
				case Mate.MateAction.Entry:
					base.transform.SetParent(this.parent, false);
					Tools.PlayAnimation(base.transform, "Entry");
					return;
				case Mate.MateAction.Tap:
					switch (global::UnityEngine.Random.Range(0, 3))
					{
					case 0:
						Tools.PlayAnimation(base.transform, "Tap");
						return;
					case 1:
						Tools.PlayAnimation(base.transform, "Tap1");
						return;
					case 2:
						Tools.PlayAnimation(base.transform, "Tap2");
						return;
					default:
						return;
					}
					break;
				case Mate.MateAction.BattlePhase:
					break;
				case Mate.MateAction.Attack:
					Tools.PlayAnimation(base.transform, "Attack");
					return;
				case Mate.MateAction.GetDamage:
					Tools.PlayAnimation(base.transform, "Damage");
					return;
				case Mate.MateAction.Victory:
					Tools.PlayAnimation(base.transform, "Victory");
					return;
				case Mate.MateAction.Defeat:
					Tools.PlayAnimation(base.transform, "Defeat");
					return;
				case Mate.MateAction.Random:
					if ((float)global::UnityEngine.Random.Range(0, 2) > 0.5f)
					{
						Tools.PlayAnimation(base.transform, "Random1");
						return;
					}
					Tools.PlayAnimation(base.transform, "Random2");
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x06008B51 RID: 35665 RVA: 0x0011AE88 File Offset: 0x00119088
		public void ActiveCamera(Mate.MateAction action, int layerMask)
		{
			if (action == Mate.MateAction.Entry && this.directorI != null)
			{
				foreach (Camera camera in this.directorI.transform.parent.GetComponentsInChildren<Camera>(true))
				{
					if (!(camera.name == "UIEffect_Camera"))
					{
						camera.enabled = true;
						camera.cullingMask = 1 << layerMask;
						CameraManager.DuelOverlay2DMinus();
						camera.gameObject.AddComponent<DoWhenDisabled>().action = delegate
						{
							CameraManager.DuelOverlay2DPlus();
						};
					}
				}
				foreach (Light light in this.directorI.transform.parent.GetComponentsInChildren<Light>(true))
				{
					light.enabled = true;
					light.cullingMask = 1 << layerMask;
				}
			}
			if (action == Mate.MateAction.Tap && this.directorM != null)
			{
				foreach (Camera camera2 in this.directorM.transform.parent.GetComponentsInChildren<Camera>(true))
				{
					if (!(camera2.name == "UIEffect_Camera"))
					{
						camera2.enabled = true;
						camera2.cullingMask = 1 << layerMask;
					}
				}
				foreach (Light light2 in this.directorM.transform.parent.GetComponentsInChildren<Light>(true))
				{
					light2.enabled = true;
					light2.cullingMask = 1 << layerMask;
				}
			}
		}

		// Token: 0x06008B52 RID: 35666 RVA: 0x0011B00D File Offset: 0x0011920D
		public void SetTimeScale(float timeScale)
		{
			if (this.type == Mate.MateType.MasterDuel)
			{
				Tools.SetAnimatorTimescale(base.transform, timeScale);
			}
		}

		// Token: 0x0400C6D6 RID: 50902
		public Mate.MateType type;

		// Token: 0x0400C6D7 RID: 50903
		public bool huge;

		// Token: 0x0400C6D8 RID: 50904
		public Transform parent;

		// Token: 0x0400C6D9 RID: 50905
		public int code;

		// Token: 0x0400C6DA RID: 50906
		public PlayableDirector directorA;

		// Token: 0x0400C6DB RID: 50907
		public PlayableDirector directorB;

		// Token: 0x0400C6DC RID: 50908
		public PlayableDirector directorD;

		// Token: 0x0400C6DD RID: 50909
		public PlayableDirector directorE;

		// Token: 0x0400C6DE RID: 50910
		public PlayableDirector directorI;

		// Token: 0x0400C6DF RID: 50911
		public PlayableDirector directorJ;

		// Token: 0x0400C6E0 RID: 50912
		public PlayableDirector directorK;

		// Token: 0x0400C6E1 RID: 50913
		public PlayableDirector directorM;

		// Token: 0x0400C6E2 RID: 50914
		public PlayableDirector directorN;

		// Token: 0x0400C6E3 RID: 50915
		public PlayableDirector directorO;

		// Token: 0x0400C6E4 RID: 50916
		private BoxCollider m_collider;

		// Token: 0x0400C6E5 RID: 50917
		private SkinnedMeshRenderer mesh;

		// Token: 0x02001289 RID: 4745
		public enum MateType
		{
			// Token: 0x0400C6E7 RID: 50919
			MasterDuel,
			// Token: 0x0400C6E8 RID: 50920
			CrossDuel
		}

		// Token: 0x0200128A RID: 4746
		public enum MateAction
		{
			// Token: 0x0400C6EA RID: 50922
			Initial,
			// Token: 0x0400C6EB RID: 50923
			Entry,
			// Token: 0x0400C6EC RID: 50924
			Tap,
			// Token: 0x0400C6ED RID: 50925
			BattlePhase,
			// Token: 0x0400C6EE RID: 50926
			Attack,
			// Token: 0x0400C6EF RID: 50927
			GetDamage,
			// Token: 0x0400C6F0 RID: 50928
			Victory,
			// Token: 0x0400C6F1 RID: 50929
			Defeat,
			// Token: 0x0400C6F2 RID: 50930
			Random
		}
	}
}
