﻿using System;
using System.Collections.Generic;
using Dialogues;
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

        public static void AddWithId(this List<Dialogue> dialogues, Dialogue dialogue)
        {
            if (!dialogues.Exists(x => x.id == dialogue.id))
            {
                dialogues.Add(dialogue);
            }
            else
            {
                throw new ArgumentException($"Dialogue with id {dialogue.id} already exists in the list.");
            }
        }
    }
}