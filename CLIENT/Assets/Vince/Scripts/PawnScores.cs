﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PawnScores : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText;
    public TextMeshProUGUI scoreText;

    public Image leaderboardImg;
    public Image scoreImg;

    Dictionary<int, Pawn> pawns = new Dictionary<int, Pawn>();

    private static PawnScores _singleton;
    public static PawnScores singleton
    {
        get { return _singleton; }
        private set { _singleton = value; }
    }

    void DrawLeaderboard()
    {
        List<Pawn> pawnList = pawns.OrderByDescending(p => p.Value.score).Select(p => p.Value).ToList();
        if (pawnList.Count != 0)
        {
            scoreImg.gameObject.SetActive(true);
            leaderboardImg.gameObject.SetActive(true);
        }
        else
        {
            scoreImg.gameObject.SetActive(false);
            scoreImg.gameObject.SetActive(false);
            return;
        }
        string text = "";
        int i = 0;
        foreach (var pawn in pawnList)
        {
            text += $"{++i}. {pawn.username}\n";

            if (pawn.canPlayerControl)
            {
                scoreText.text = $"Score: {pawn.score}";
            }
        }

        leaderboardText.text = text;
    }

    public void AddPawn(Pawn pawn)
    {
        if (!pawns.ContainsKey(pawn.networkID))
        {
            pawns.Add(pawn.networkID, pawn);
        }
    }

    public void RemovePawn(int networkID)
    {
        if (pawns.ContainsKey(networkID))
        {
            pawns.Remove(networkID);
        }
    }


    private void Update()
    {
        DrawLeaderboard();
    }

    void Start()
    {
        leaderboardImg.gameObject.SetActive(false);
        scoreImg.gameObject.SetActive(false);
        singleton = this;
    }



}
