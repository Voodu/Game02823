using System;
using System.Collections;
using System.Collections.Generic;
using Quests;
using Quests.Enums;
using Quests.Items;

public class Objective
{
    public string Id { get; set; }
    public string ConnectedQuestId { get; set; }
    public string Description { get; set; }
    public ObjectiveType Type { get; set; } = ObjectiveType.None;
    public ObjectiveStatus Status { get; set; } = ObjectiveStatus.NotStarted;
    public (int current, int full) Progress { get; set; }

    public void RecordProgress(ObjectiveItem item)
    {
        switch (item.ObjectiveType)
        {
            case ObjectiveType.Kill:
                Progress = (Progress.current + 1, Progress.full);
                break;
            case ObjectiveType.Gather:
                Progress = (Progress.current + 1, Progress.full);
                break;
            case ObjectiveType.Deliver:
                Progress = (Progress.current + 1, Progress.full);
                break;
            case ObjectiveType.Escort:
                Progress = (Progress.current + 1, Progress.full);
                break;
            case ObjectiveType.Visit:
                Progress = (Progress.current + 1, Progress.full);
                break;
            case ObjectiveType.Talk:
                Progress = (Progress.current + 1, Progress.full);
                break;
            case ObjectiveType.Other:
                Progress = (Progress.current + 1, Progress.full);
                break;
            case ObjectiveType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (Progress.current == Progress.full)
        {
            Status = ObjectiveStatus.Completed;
            QuestManager.Instance[ConnectedQuestId].Continue();
        }
    }
}
