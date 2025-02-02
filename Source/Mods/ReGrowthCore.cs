﻿using Verse;

namespace Multiplayer.Compat
{
    /// <summary>ReGrowth: Core by Helixien, Taranchuk</summary>
    /// <see href="https://github.com/Helixien/ReGrowth-Core"/>
    /// <see href="https://steamcommunity.com/sharedfiles/filedetails/?id=2260097569"/>
    [MpCompatFor("ReGrowth.BOTR.Core")]
    public class ReGrowthCore
    {
        public ReGrowthCore(ModContentPack mod)
        {
            LongEventHandler.ExecuteWhenFinished(LatePatch);

            // (Dev) spawn leaves
            MpCompat.RegisterLambdaMethod("ReGrowthCore.CompLeavesSpawnerBase", "CompGetGizmosExtra", 0).SetDebugOnly();
        }

        private static void LatePatch() => PatchingUtilities.PatchPushPopRand("ReGrowthCore.DevilDust_Tornado:ThrowDevilDustPuff");
    }
}