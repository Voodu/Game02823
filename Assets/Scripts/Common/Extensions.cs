using System;
using System.Collections.Generic;
using Quests;

namespace Common
{
    public static class Extensions
    {
        public static void AddWithId(this List<Objective> objectives, Objective objective)
        {
            if (!objectives.Exists(x => x.id == objective.id))
            {
                objectives.Add(objective);
            }
            else
            {
                throw new ArgumentException($"Objective with id {objective.id} already exists in the list.");
            }
        }

        public static void AddWithId(this List<Quest> quests, Quest quest)
        {
            if (!quests.Exists(x => x.id == quest.id))
            {
                quests.Add(quest);
            }
            else
            {
                throw new ArgumentException($"Quest with id {quest.id} already exists in the list.");
            }
        }
    }
}