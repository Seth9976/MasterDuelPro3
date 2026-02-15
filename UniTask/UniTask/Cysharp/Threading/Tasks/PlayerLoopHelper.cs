using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000062 RID: 98
	public static class PlayerLoopHelper
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00004790 File Offset: 0x00002990
		public static SynchronizationContext UnitySynchronizationContext
		{
			get
			{
				return PlayerLoopHelper.unitySynchronizationContext;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004797 File Offset: 0x00002997
		public static int MainThreadId
		{
			get
			{
				return PlayerLoopHelper.mainThreadId;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000479E File Offset: 0x0000299E
		internal static string ApplicationDataPath
		{
			get
			{
				return PlayerLoopHelper.applicationDataPath;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000047A5 File Offset: 0x000029A5
		public static bool IsMainThread
		{
			get
			{
				return Thread.CurrentThread.ManagedThreadId == PlayerLoopHelper.mainThreadId;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000047B8 File Offset: 0x000029B8
		// (set) Token: 0x0600013E RID: 318 RVA: 0x000047BF File Offset: 0x000029BF
		internal static bool IsEditorApplicationQuitting { get; private set; }

		// Token: 0x0600013F RID: 319 RVA: 0x000047C8 File Offset: 0x000029C8
		private static PlayerLoopSystem[] InsertRunner(PlayerLoopSystem loopSystem, bool injectOnFirst, Type loopRunnerYieldType, ContinuationQueue cq, Type loopRunnerType, PlayerLoopRunner runner)
		{
			PlayerLoopSystem yieldLoop = new PlayerLoopSystem
			{
				type = loopRunnerYieldType,
				updateDelegate = new PlayerLoopSystem.UpdateFunction(cq.Run)
			};
			PlayerLoopSystem runnerLoop = new PlayerLoopSystem
			{
				type = loopRunnerType,
				updateDelegate = new PlayerLoopSystem.UpdateFunction(runner.Run)
			};
			PlayerLoopSystem[] source = PlayerLoopHelper.RemoveRunner(loopSystem, loopRunnerYieldType, loopRunnerType);
			PlayerLoopSystem[] dest = new PlayerLoopSystem[source.Length + 2];
			Array.Copy(source, 0, dest, injectOnFirst ? 2 : 0, source.Length);
			if (injectOnFirst)
			{
				dest[0] = yieldLoop;
				dest[1] = runnerLoop;
			}
			else
			{
				dest[dest.Length - 2] = yieldLoop;
				dest[dest.Length - 1] = runnerLoop;
			}
			return dest;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004878 File Offset: 0x00002A78
		private static PlayerLoopSystem[] RemoveRunner(PlayerLoopSystem loopSystem, Type loopRunnerYieldType, Type loopRunnerType)
		{
			return loopSystem.subSystemList.Where((PlayerLoopSystem ls) => ls.type != loopRunnerYieldType && ls.type != loopRunnerType).ToArray<PlayerLoopSystem>();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000048B8 File Offset: 0x00002AB8
		private static PlayerLoopSystem[] InsertUniTaskSynchronizationContext(PlayerLoopSystem loopSystem)
		{
			PlayerLoopSystem loop = new PlayerLoopSystem
			{
				type = typeof(UniTaskSynchronizationContext),
				updateDelegate = new PlayerLoopSystem.UpdateFunction(UniTaskSynchronizationContext.Run)
			};
			List<PlayerLoopSystem> dest = new List<PlayerLoopSystem>(loopSystem.subSystemList.Where((PlayerLoopSystem ls) => ls.type != typeof(UniTaskSynchronizationContext)).ToArray<PlayerLoopSystem>());
			int index = dest.FindIndex((PlayerLoopSystem x) => x.type.Name == "ScriptRunDelayedTasks");
			if (index == -1)
			{
				index = dest.FindIndex((PlayerLoopSystem x) => x.type.Name == "UniTaskLoopRunnerUpdate");
			}
			dest.Insert(index + 1, loop);
			return dest.ToArray();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004988 File Offset: 0x00002B88
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		private static void Init()
		{
			PlayerLoopHelper.unitySynchronizationContext = SynchronizationContext.Current;
			PlayerLoopHelper.mainThreadId = Thread.CurrentThread.ManagedThreadId;
			try
			{
				PlayerLoopHelper.applicationDataPath = Application.dataPath;
			}
			catch
			{
			}
			if (PlayerLoopHelper.runners != null)
			{
				return;
			}
			PlayerLoopSystem playerLoop = PlayerLoop.GetCurrentPlayerLoop();
			PlayerLoopHelper.Initialize(ref playerLoop, InjectPlayerLoopTimings.All);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000049E8 File Offset: 0x00002BE8
		private static int FindLoopSystemIndex(PlayerLoopSystem[] playerLoopList, Type systemType)
		{
			for (int i = 0; i < playerLoopList.Length; i++)
			{
				if (playerLoopList[i].type == systemType)
				{
					return i;
				}
			}
			throw new Exception("Target PlayerLoopSystem does not found. Type:" + systemType.FullName);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004A30 File Offset: 0x00002C30
		private static void InsertLoop(PlayerLoopSystem[] copyList, InjectPlayerLoopTimings injectTimings, Type loopType, InjectPlayerLoopTimings targetTimings, int index, bool injectOnFirst, Type loopRunnerYieldType, Type loopRunnerType, PlayerLoopTiming playerLoopTiming)
		{
			int i = PlayerLoopHelper.FindLoopSystemIndex(copyList, loopType);
			if ((injectTimings & targetTimings) == targetTimings)
			{
				copyList[i].subSystemList = PlayerLoopHelper.InsertRunner(copyList[i], injectOnFirst, loopRunnerYieldType, PlayerLoopHelper.yielders[index] = new ContinuationQueue(playerLoopTiming), loopRunnerType, PlayerLoopHelper.runners[index] = new PlayerLoopRunner(playerLoopTiming));
				return;
			}
			copyList[i].subSystemList = PlayerLoopHelper.RemoveRunner(copyList[i], loopRunnerYieldType, loopRunnerType);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004AAC File Offset: 0x00002CAC
		public static void Initialize(ref PlayerLoopSystem playerLoop, InjectPlayerLoopTimings injectTimings = InjectPlayerLoopTimings.All)
		{
			PlayerLoopHelper.yielders = new ContinuationQueue[16];
			PlayerLoopHelper.runners = new PlayerLoopRunner[16];
			PlayerLoopSystem[] copyList = playerLoop.subSystemList.ToArray<PlayerLoopSystem>();
			PlayerLoopHelper.InsertLoop(copyList, injectTimings, typeof(Initialization), InjectPlayerLoopTimings.Initialization, 0, true, typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldInitialization), typeof(UniTaskLoopRunners.UniTaskLoopRunnerInitialization), PlayerLoopTiming.Initialization);
			PlayerLoopHelper.InsertLoop(copyList, injectTimings, typeof(Initialization), InjectPlayerLoopTimings.LastInitialization, 1, false, typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldInitialization), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastInitialization), PlayerLoopTiming.LastInitialization);
			PlayerLoopHelper.InsertLoop(copyList, injectTimings, typeof(EarlyUpdate), InjectPlayerLoopTimings.EarlyUpdate, 2, true, typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldEarlyUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerEarlyUpdate), PlayerLoopTiming.EarlyUpdate);
			PlayerLoopHelper.InsertLoop(copyList, injectTimings, typeof(EarlyUpdate), InjectPlayerLoopTimings.LastEarlyUpdate, 3, false, typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldEarlyUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastEarlyUpdate), PlayerLoopTiming.LastEarlyUpdate);
			PlayerLoopHelper.InsertLoop(copyList, injectTimings, typeof(FixedUpdate), InjectPlayerLoopTimings.FixedUpdate, 4, true, typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldFixedUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerFixedUpdate), PlayerLoopTiming.FixedUpdate);
			PlayerLoopHelper.InsertLoop(copyList, injectTimings, typeof(FixedUpdate), InjectPlayerLoopTimings.LastFixedUpdate, 5, false, typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldFixedUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastFixedUpdate), PlayerLoopTiming.LastFixedUpdate);
			PlayerLoopHelper.InsertLoop(copyList, injectTimings, typeof(PreUpdate), InjectPlayerLoopTimings.PreUpdate, 6, true, typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldPreUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerPreUpdate), PlayerLoopTiming.PreUpdate);
			PlayerLoopHelper.InsertLoop(copyList, injectTimings, typeof(PreUpdate), InjectPlayerLoopTimings.LastPreUpdate, 7, false, typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldPreUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastPreUpdate), PlayerLoopTiming.LastPreUpdate);
			PlayerLoopHelper.InsertLoop(copyList, injectTimings, typeof(Update), InjectPlayerLoopTimings.Update, 8, true, typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerUpdate), PlayerLoopTiming.Update);
			PlayerLoopHelper.InsertLoop(copyList, injectTimings, typeof(Update), InjectPlayerLoopTimings.LastUpdate, 9, false, typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastUpdate), PlayerLoopTiming.LastUpdate);
			PlayerLoopHelper.InsertLoop(copyList, injectTimings, typeof(PreLateUpdate), InjectPlayerLoopTimings.PreLateUpdate, 10, true, typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldPreLateUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerPreLateUpdate), PlayerLoopTiming.PreLateUpdate);
			PlayerLoopHelper.InsertLoop(copyList, injectTimings, typeof(PreLateUpdate), InjectPlayerLoopTimings.LastPreLateUpdate, 11, false, typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldPreLateUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastPreLateUpdate), PlayerLoopTiming.LastPreLateUpdate);
			PlayerLoopHelper.InsertLoop(copyList, injectTimings, typeof(PostLateUpdate), InjectPlayerLoopTimings.PostLateUpdate, 12, true, typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldPostLateUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerPostLateUpdate), PlayerLoopTiming.PostLateUpdate);
			PlayerLoopHelper.InsertLoop(copyList, injectTimings, typeof(PostLateUpdate), InjectPlayerLoopTimings.LastPostLateUpdate, 13, false, typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldPostLateUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastPostLateUpdate), PlayerLoopTiming.LastPostLateUpdate);
			PlayerLoopHelper.InsertLoop(copyList, injectTimings, typeof(TimeUpdate), InjectPlayerLoopTimings.TimeUpdate, 14, true, typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldTimeUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerTimeUpdate), PlayerLoopTiming.TimeUpdate);
			PlayerLoopHelper.InsertLoop(copyList, injectTimings, typeof(TimeUpdate), InjectPlayerLoopTimings.LastTimeUpdate, 15, false, typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldTimeUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastTimeUpdate), PlayerLoopTiming.LastTimeUpdate);
			int i = PlayerLoopHelper.FindLoopSystemIndex(copyList, typeof(Update));
			copyList[i].subSystemList = PlayerLoopHelper.InsertUniTaskSynchronizationContext(copyList[i]);
			playerLoop.subSystemList = copyList;
			PlayerLoop.SetPlayerLoop(playerLoop);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004DDD File Offset: 0x00002FDD
		public static void AddAction(PlayerLoopTiming timing, IPlayerLoopItem action)
		{
			PlayerLoopRunner playerLoopRunner = PlayerLoopHelper.runners[(int)timing];
			if (playerLoopRunner == null)
			{
				PlayerLoopHelper.ThrowInvalidLoopTiming(timing);
			}
			playerLoopRunner.AddAction(action);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004DF5 File Offset: 0x00002FF5
		private static void ThrowInvalidLoopTiming(PlayerLoopTiming playerLoopTiming)
		{
			throw new InvalidOperationException("Target playerLoopTiming is not injected. Please check PlayerLoopHelper.Initialize. PlayerLoopTiming:" + playerLoopTiming.ToString());
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004E13 File Offset: 0x00003013
		public static void AddContinuation(PlayerLoopTiming timing, Action continuation)
		{
			ContinuationQueue continuationQueue = PlayerLoopHelper.yielders[(int)timing];
			if (continuationQueue == null)
			{
				PlayerLoopHelper.ThrowInvalidLoopTiming(timing);
			}
			continuationQueue.Enqueue(continuation);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004E2C File Offset: 0x0000302C
		public static void DumpCurrentPlayerLoop()
		{
			ref PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("PlayerLoop List");
			foreach (PlayerLoopSystem header in currentPlayerLoop.subSystemList)
			{
				sb.AppendFormat("------{0}------", header.type.Name);
				sb.AppendLine();
				if (header.subSystemList == null)
				{
					sb.AppendFormat("{0} has no subsystems!", header.ToString());
					sb.AppendLine();
				}
				else
				{
					foreach (PlayerLoopSystem subSystem in header.subSystemList)
					{
						sb.AppendFormat("{0}", subSystem.type.Name);
						sb.AppendLine();
						if (subSystem.subSystemList != null)
						{
							Debug.LogWarning("More Subsystem:" + subSystem.subSystemList.Length.ToString());
						}
					}
				}
			}
			Debug.Log(sb.ToString());
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004F34 File Offset: 0x00003134
		public static bool IsInjectedUniTaskPlayerLoop()
		{
			foreach (PlayerLoopSystem header in PlayerLoop.GetCurrentPlayerLoop().subSystemList)
			{
				if (header.subSystemList != null)
				{
					PlayerLoopSystem[] subSystemList2 = header.subSystemList;
					for (int j = 0; j < subSystemList2.Length; j++)
					{
						if (subSystemList2[j].type == typeof(UniTaskLoopRunners.UniTaskLoopRunnerInitialization))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x040000C2 RID: 194
		private static readonly ContinuationQueue ThrowMarkerContinuationQueue = new ContinuationQueue(PlayerLoopTiming.Initialization);

		// Token: 0x040000C3 RID: 195
		private static readonly PlayerLoopRunner ThrowMarkerPlayerLoopRunner = new PlayerLoopRunner(PlayerLoopTiming.Initialization);

		// Token: 0x040000C4 RID: 196
		private static int mainThreadId;

		// Token: 0x040000C5 RID: 197
		private static string applicationDataPath;

		// Token: 0x040000C6 RID: 198
		private static SynchronizationContext unitySynchronizationContext;

		// Token: 0x040000C7 RID: 199
		private static ContinuationQueue[] yielders;

		// Token: 0x040000C8 RID: 200
		private static PlayerLoopRunner[] runners;
	}
}
