using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000F00 RID: 3840
	public class SelectingCursorManager : MonoBehaviour
	{
		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06007140 RID: 28992 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007141 RID: 28993 RVA: 0x0000216A File Offset: 0x0000036A
		public static SelectingCursorManager Create(RunEffectWorker worker)
		{
			return null;
		}

		// Token: 0x06007142 RID: 28994 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadCursorPrefab()
		{
		}

		// Token: 0x06007143 RID: 28995 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadAsset(string path, Action<GameObject> loadedCallback)
		{
		}

		// Token: 0x06007144 RID: 28996 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartDisp(int team, int position, int index)
		{
		}

		// Token: 0x06007145 RID: 28997 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartDispImpl(Vector3 pos, bool isForce)
		{
		}

		// Token: 0x06007146 RID: 28998 RVA: 0x0000216D File Offset: 0x0000036D
		public void EndDisp()
		{
		}

		// Token: 0x06007147 RID: 28999 RVA: 0x0000216D File Offset: 0x0000036D
		public void RefreshDisp()
		{
		}

		// Token: 0x06007148 RID: 29000 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06007149 RID: 29001 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFocusHighlight(int team, int position, int viewIndex)
		{
		}

		// Token: 0x0600714A RID: 29002 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowHighlightEffect(int team, int position, int viewIndex)
		{
		}

		// Token: 0x0600714B RID: 29003 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowHighlightEffect(Vector3 pos, Quaternion rot, DuelEffectPool.Type effectType)
		{
		}

		// Token: 0x0600714C RID: 29004 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideHighlightEffect()
		{
		}

		// Token: 0x0600714D RID: 29005 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectCurrentTarget()
		{
		}

		// Token: 0x0600714E RID: 29006 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Select(int player, int position, int viewIndex)
		{
			return false;
		}

		// Token: 0x0600714F RID: 29007 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowPositionPadCursor(int player, int position, int viewIndex)
		{
		}

		// Token: 0x06007150 RID: 29008 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowPhasePadCursor()
		{
		}

		// Token: 0x06007151 RID: 29009 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowPadCursor(GameObject cursorObject)
		{
		}

		// Token: 0x06007152 RID: 29010 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject CreatePositionPadCursor(int position, Transform parent)
		{
			return null;
		}

		// Token: 0x06007153 RID: 29011 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject CreatePhasePadCursor()
		{
			return null;
		}

		// Token: 0x06007154 RID: 29012 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject CreatePadCursor(GameObject prefab, Transform parent)
		{
			return null;
		}

		// Token: 0x06007155 RID: 29013 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject GetPadCursorPrefab(int position)
		{
			return null;
		}

		// Token: 0x06007156 RID: 29014 RVA: 0x0000216D File Offset: 0x0000036D
		public void HidePadCursor(bool resetInfo = true)
		{
		}

		// Token: 0x06007157 RID: 29015 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReshowPadCursor()
		{
		}

		// Token: 0x06007158 RID: 29016 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdatePadCursor()
		{
		}

		// Token: 0x0400AAE5 RID: 43749
		private RunEffectWorker worker;

		// Token: 0x0400AAE6 RID: 43750
		private SimpleEffect effArrow;

		// Token: 0x0400AAE7 RID: 43751
		private SimpleEffect highlightEff;

		// Token: 0x0400AAE8 RID: 43752
		private int currentCursorPlayer;

		// Token: 0x0400AAE9 RID: 43753
		private int currentCursorPosition;

		// Token: 0x0400AAEA RID: 43754
		private int currentCursorViewIndex;

		// Token: 0x0400AAEB RID: 43755
		private List<GameObject> cursorEffect;

		// Token: 0x0400AAEC RID: 43756
		private int cursorEffectPlayer;

		// Token: 0x0400AAED RID: 43757
		private int cursorEffectPosition;

		// Token: 0x0400AAEE RID: 43758
		private int cursorEffectViewIndex;

		// Token: 0x0400AAEF RID: 43759
		private bool cursorEffectPhaseButton;

		// Token: 0x0400AAF0 RID: 43760
		private Dictionary<string, Queue<GameObject>> cursorEffectPool;

		// Token: 0x0400AAF1 RID: 43761
		private Transform poolParent;

		// Token: 0x0400AAF2 RID: 43762
		private const string prefabPathBase = "Duel/Models/FieldPadCursor/FieldPadCursor";

		// Token: 0x0400AAF3 RID: 43763
		private const string prefabPathCursorExclude = "Duel/Models/FieldPadCursor/FieldPadCursorExclude";

		// Token: 0x0400AAF4 RID: 43764
		private const string prefabPathCursorField = "Duel/Models/FieldPadCursor/FieldPadCursorField";

		// Token: 0x0400AAF5 RID: 43765
		private const string prefabPathCursorGrave = "Duel/Models/FieldPadCursor/FieldPadCursorGrave";

		// Token: 0x0400AAF6 RID: 43766
		private const string prefabPathCursorMagic = "Duel/Models/FieldPadCursor/FieldPadCursorMagic";

		// Token: 0x0400AAF7 RID: 43767
		private const string prefabPathCursorMonster = "Duel/Models/FieldPadCursor/FieldPadCursorMonster";

		// Token: 0x0400AAF8 RID: 43768
		private const string prefabPathCursorPhase = "Duel/Models/FieldPadCursor/FieldPadCursorPhase";

		// Token: 0x0400AAF9 RID: 43769
		private const string prefabPathCursorCard = "Duel/Models/FieldPadCursor/FieldPadCursorCard";

		// Token: 0x0400AAFA RID: 43770
		private GameObject prefabCursorExclude;

		// Token: 0x0400AAFB RID: 43771
		private GameObject prefabCursorField;

		// Token: 0x0400AAFC RID: 43772
		private GameObject prefabCursorGrave;

		// Token: 0x0400AAFD RID: 43773
		private GameObject prefabCursorMagic;

		// Token: 0x0400AAFE RID: 43774
		private GameObject prefabCursorMonster;

		// Token: 0x0400AAFF RID: 43775
		private GameObject prefabCursorPhase;

		// Token: 0x0400AB00 RID: 43776
		private GameObject prefabCursorCard;

		// Token: 0x0400AB01 RID: 43777
		private Vector3 curPos;

		// Token: 0x0400AB02 RID: 43778
		private int setPlayer;

		// Token: 0x0400AB03 RID: 43779
		private int setPosition;

		// Token: 0x0400AB04 RID: 43780
		private int setViewIndex;
	}
}
