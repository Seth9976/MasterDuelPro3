using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000F0C RID: 3852
	public class SharedMisc
	{
		// Token: 0x0600719F RID: 29087 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardRoot GetCardRoot(DuelGameObjectManager goManager, int team, int position, int index)
		{
			return null;
		}

		// Token: 0x060071A0 RID: 29088 RVA: 0x000F62A8 File Offset: 0x000F44A8
		public static Vector3 Bezier(Vector3 P0, Vector3 P1, Vector3 P2, float t)
		{
			return default(Vector3);
		}

		// Token: 0x060071A1 RID: 29089 RVA: 0x000F62C0 File Offset: 0x000F44C0
		public static Vector3 BezierVec(Vector3 P0, Vector3 P1, Vector3 P2, float t)
		{
			return default(Vector3);
		}

		// Token: 0x060071A2 RID: 29090 RVA: 0x000F62D8 File Offset: 0x000F44D8
		public static Vector3 BezierVec2(Vector3 P0, Vector3 P1, Vector3 P2, Vector3 P3, float t)
		{
			return default(Vector3);
		}

		// Token: 0x060071A3 RID: 29091 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float Bezier2(float P1, float P2, float t)
		{
			return 0f;
		}

		// Token: 0x060071A4 RID: 29092 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float Bezier3(float P1, float P2, float P3, float t)
		{
			return 0f;
		}

		// Token: 0x060071A5 RID: 29093 RVA: 0x000F62F0 File Offset: 0x000F44F0
		public static Quaternion QuatBezier(Quaternion P0, Quaternion P1, Quaternion P2, float t)
		{
			return default(Quaternion);
		}

		// Token: 0x060071A6 RID: 29094 RVA: 0x000F6308 File Offset: 0x000F4508
		public static Vector3 PerlinShake(float cycle, Vector3 pow)
		{
			return default(Vector3);
		}

		// Token: 0x060071A7 RID: 29095 RVA: 0x000F631E File Offset: 0x000F451E
		public static bool LinePlaneIntersection(out Vector3 intersection, Vector3 linePoint, Vector3 lineVec, Vector3 planeNormal, Vector3 planePoint)
		{
			intersection = default(Vector3);
			return false;
		}

		// Token: 0x060071A8 RID: 29096 RVA: 0x000F631E File Offset: 0x000F451E
		public static bool LineLineIntersection(out Vector3 intersection, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
		{
			intersection = default(Vector3);
			return false;
		}

		// Token: 0x060071A9 RID: 29097 RVA: 0x000F6328 File Offset: 0x000F4528
		public static Vector3 SetVectorLength(Vector3 vector, float size)
		{
			return default(Vector3);
		}

		// Token: 0x060071AA RID: 29098 RVA: 0x000F6340 File Offset: 0x000F4540
		public static Vector3 GetIntersectionZXPlane(Ray ray)
		{
			return default(Vector3);
		}

		// Token: 0x060071AB RID: 29099 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetBillboardTexPath(int cardId)
		{
			return null;
		}

		// Token: 0x060071AC RID: 29100 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetMeshColor(MeshFilter meshFilter, Color col)
		{
		}

		// Token: 0x060071AD RID: 29101 RVA: 0x000F6358 File Offset: 0x000F4558
		public static Color GetMeshColor(MeshFilter meshFilter)
		{
			return default(Color);
		}

		// Token: 0x060071AE RID: 29102 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<CardPlace, Engine.AffectType> GetAffectCardPlaces(DuelFieldBase duelField, int team, int position)
		{
			return null;
		}

		// Token: 0x060071AF RID: 29103 RVA: 0x0000216A File Offset: 0x0000036A
		public static string DebugGetCallMethod(int numFrames = 1)
		{
			return null;
		}

		// Token: 0x060071B0 RID: 29104 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DebugPrintCallStack(string message, uint numStacks = 3U)
		{
		}

		// Token: 0x060071B1 RID: 29105 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DelayedFunction(float delay, Action func)
		{
		}

		// Token: 0x060071B2 RID: 29106 RVA: 0x0000216A File Offset: 0x0000036A
		private static IEnumerator DelayedFunctionImpl(float delay, Action func)
		{
			return null;
		}

		// Token: 0x060071B3 RID: 29107 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int BinarySearch<T>(List<T> list, Func<T, int> comparer)
		{
			return 0;
		}

		// Token: 0x060071B4 RID: 29108 RVA: 0x0000216A File Offset: 0x0000036A
		public static TweenSet ApplyTweenTR(GameObject go, Vector3 srcPos, Quaternion srcRot, Vector3 dstPos, Quaternion dstRot, float duration, Tween.Easing easing, UnityAction onFinished)
		{
			return null;
		}

		// Token: 0x060071B5 RID: 29109 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PlayTween(GameObject target, string label, Action onFinished)
		{
		}

		// Token: 0x060071B6 RID: 29110 RVA: 0x0000216A File Offset: 0x0000036A
		public static IEnumerator WaitLimitedTime(float timeLimit, Func<bool> isFinishedFunc)
		{
			return null;
		}

		// Token: 0x060071B7 RID: 29111 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDistanceLength(Vector2 screenPointA, Vector2 screenPointB, float screenRatio)
		{
			return false;
		}
	}
}
