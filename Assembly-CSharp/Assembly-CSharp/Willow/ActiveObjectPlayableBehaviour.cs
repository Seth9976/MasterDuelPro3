using System;
using System.Collections.Generic;
using Ara;
using MDPro3;
using UnityEngine;
using UnityEngine.Playables;

namespace Willow
{
	// Token: 0x02001544 RID: 5444
	public class ActiveObjectPlayableBehaviour : PlayableBehaviour
	{
		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x06009DC6 RID: 40390 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isFullLifespan
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06009DC7 RID: 40391 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnGraphStart(Playable playable)
		{
		}

		// Token: 0x06009DC8 RID: 40392 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnGraphStop(Playable playable)
		{
		}

		// Token: 0x06009DC9 RID: 40393 RVA: 0x0019B07C File Offset: 0x0019927C
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			if (this.m_prefab == null)
			{
				return;
			}
			if (this.m_object == null)
			{
				this.m_object = global::UnityEngine.Object.Instantiate<GameObject>(this.m_prefab);
				if (Program.instance.currentServant == Program.instance.mate)
				{
					Tools.ChangeLayer(this.m_object, "DuelOverlay2D", false);
				}
				if (this.needRotate != null)
				{
					this.m_object.transform.SetParent(this.needRotate, false);
					return;
				}
				if (this.m_parent != null)
				{
					this.m_object.transform.SetParent(this.m_parent, false);
					return;
				}
				if (this.m_relayObject != null)
				{
					if (this.m_relayObject.transform.GetChild(0).childCount > 0)
					{
						this.m_object.transform.SetParent(this.needRotate, false);
					}
					else
					{
						this.m_object.transform.SetParent(this.m_relayObject.transform.GetChild(0), false);
					}
				}
				else
				{
					foreach (CustomTimelineObject t in this.controller.transform.GetComponentsInChildren<CustomTimelineObject>(true))
					{
						if (t.name.ToLower().Contains(this.m_prefab.name.ToLower()) && t.name.ToLower().Contains("relay_"))
						{
							this.m_relayObject = t;
							break;
						}
					}
				}
				if (this.m_relayObject != null)
				{
					if (this.m_relayObject.transform.GetChild(0).childCount > 0)
					{
						this.m_object.transform.SetParent(this.m_relayObject.transform.GetChild(0).GetChild(0), false);
					}
					else
					{
						this.m_object.transform.SetParent(this.m_relayObject.transform.GetChild(0), false);
					}
				}
				else if (this.m_parentObject != null)
				{
					this.m_object.transform.SetParent(this.m_parentObject.transform, false);
				}
				else
				{
					Transform parent = null;
					foreach (Transform t2 in this.controller.transform.parent.GetComponentsInChildren<Transform>(true))
					{
						if (t2.name.ToLower() == this.m_name.ToLower())
						{
							parent = t2;
						}
					}
					if (parent != null)
					{
						this.m_object.transform.SetParent(parent, false);
					}
					else
					{
						this.m_object.transform.SetParent(this.controller.transform.parent, false);
					}
				}
				this.m_parent = this.m_object.transform.parent;
				if (this.m_prefab.name.Contains("04766_01J_dattack_01"))
				{
					this.needRotate = this.m_parent;
					return;
				}
			}
			else
			{
				this.m_object.SetActive(true);
			}
		}

		// Token: 0x06009DCA RID: 40394 RVA: 0x0019B37D File Offset: 0x0019957D
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (this.m_object != null && this.m_isFullLifespan)
			{
				this.m_object.SetActive(false);
				return;
			}
			global::UnityEngine.Object.Destroy(this.m_object);
		}

		// Token: 0x06009DCB RID: 40395 RVA: 0x0019B3B0 File Offset: 0x001995B0
		public override void PrepareFrame(Playable playable, FrameData info)
		{
			if (this.needRotate != null)
			{
				if (Program.instance.currentServant == Program.instance.ocgcore)
				{
					this.needRotate.LookAt(Program.instance.camera_.cameraMain.transform);
					return;
				}
				this.needRotate.LookAt(Program.instance.camera_.cameraDuelOverlay2D.transform);
			}
		}

		// Token: 0x06009DCC RID: 40396 RVA: 0x0000216D File Offset: 0x0000036D
		private static void GetComponentRoots<T>(Transform t, ICollection<T> roots) where T : Component
		{
		}

		// Token: 0x0400DD8A RID: 56714
		public string m_name;

		// Token: 0x0400DD8B RID: 56715
		public Playable m_root;

		// Token: 0x0400DD8C RID: 56716
		public GameObject m_owner;

		// Token: 0x0400DD8D RID: 56717
		public GameObject m_parentObject;

		// Token: 0x0400DD8E RID: 56718
		public CustomTimelineObject m_relayObject;

		// Token: 0x0400DD8F RID: 56719
		public GameObject m_prefab;

		// Token: 0x0400DD90 RID: 56720
		public bool m_isFullLifespan;

		// Token: 0x0400DD91 RID: 56721
		public CustomTimelineController controller;

		// Token: 0x0400DD92 RID: 56722
		private CustomTimelineController m_controller;

		// Token: 0x0400DD93 RID: 56723
		private GameObject m_object;

		// Token: 0x0400DD94 RID: 56724
		private List<ParticleSystem> m_listParticle;

		// Token: 0x0400DD95 RID: 56725
		private List<AraTrail> m_listTrail;

		// Token: 0x0400DD96 RID: 56726
		private double m_prevTime;

		// Token: 0x0400DD97 RID: 56727
		private double m_duration;

		// Token: 0x0400DD98 RID: 56728
		private bool m_isPlay;

		// Token: 0x0400DD99 RID: 56729
		private bool m_isKeepObject;

		// Token: 0x0400DD9A RID: 56730
		public const string kPrefixTempPrefab = "_____tmp_playing_";

		// Token: 0x0400DD9B RID: 56731
		public bool m_isLookAtCamera;

		// Token: 0x0400DD9C RID: 56732
		private Transform needRotate;

		// Token: 0x0400DD9D RID: 56733
		private Transform m_parent;
	}
}
