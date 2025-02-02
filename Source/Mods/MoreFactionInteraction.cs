﻿using System;
using System.Linq;
using HarmonyLib;
using Multiplayer.API;
using Verse;

namespace Multiplayer.Compat
{
    /// <summary>More Faction Interaction by Mehni</summary>
    /// <see href="https://github.com/emipa606/MoreFactionInteraction"/>
    /// <see href="https://steamcommunity.com/sharedfiles/filedetails/?id=2379076640"/>
    [MpCompatFor("Mlie.MoreFactionInteraction")]
    internal class MoreFactionInteraction
    {
        public MoreFactionInteraction(ModContentPack mod)
        {
            Type type = AccessTools.TypeByName("Multiplayer.Client.Patches.CloseDialogsForExpiredLetters");
            // We should probably add this to the API the next time we update it
            // TODO: Expose in API
            var registerAction = AccessTools.Method(type, "RegisterDefaultLetterChoice");

            type = AccessTools.TypeByName("MoreFactionInteraction.ChoiceLetter_ReverseTradeRequest");
            var methods = MpMethodUtil.GetLambda(type, "Choices", MethodType.Getter, null, 0, 3).ToArray();
            MP.RegisterSyncDelegate(type, methods[0].DeclaringType.Name, methods[0].Name);
            MP.RegisterSyncMethod(methods[1]);
            registerAction.Invoke(null, new object[] {methods[1], type});

            var typeNames = new[]
            {
                "MoreFactionInteraction.ChoiceLetter_DiplomaticMarriage",
                "MoreFactionInteraction.ChoiceLetter_ExtortionDemand",
                "MoreFactionInteraction.ChoiceLetter_MysticalShaman",
            };

            foreach (var typeName in typeNames)
            {
                type = AccessTools.TypeByName(typeName);
                methods = MpMethodUtil.GetLambda(type, "Choices", MethodType.Getter, null, 0, 1).ToArray();
                MP.RegisterSyncMethod(methods[0]);
                MP.RegisterSyncMethod(methods[1]);
                registerAction.Invoke(null, new object[] {methods[1], type});
            }

            typeNames = new[]
            {
                "MoreFactionInteraction.FactionWarPeaceTalks",
                "MoreFactionInteraction.More_Flavour.AnnualExpo",
            };

            foreach (var typeName in typeNames)
            {
                type = AccessTools.TypeByName(typeName);
                MP.RegisterSyncDialogNodeTree(type, "Notify_CaravanArrived");
            }

            type = AccessTools.TypeByName("MoreFactionInteraction.IncidentWorker_MysticalShaman");
            MP.RegisterSyncDialogNodeTree(type, "TryExecuteWorker");

            typeNames = new[]
            {
                "MoreFactionInteraction.IncidentWorker_MysticalShaman",
                "MoreFactionInteraction.IncidentWorker_ReverseTradeRequest",
                "MoreFactionInteraction.IncidentWorker_RoadWorks",
            };

            foreach (var typeName in typeNames)
            {
                type = AccessTools.TypeByName(typeName);
                MP.RegisterSyncDialogNodeTree(type, "TryExecuteWorker");
            }
        }
    }
}
