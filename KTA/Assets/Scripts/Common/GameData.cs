﻿using UnityEngine;
using System.Collections.Generic;


// 유닛 데이터
public class UnitData
{
    public uint pc_gsn;         // 유저-NPC 고유번호
    public uint pc_id;
    public int star_grade;
    public uint pc_position;
    public long pc_reg_dt;
    public uint pc_state;
    public List<int> skill_level_arr = new List<int>();
}

// 장비 데이터
public class ItemData
{
    public uint item_gsn;
    public uint item_id;
    public int star_grade;
    public int enchant_grade;
    public uint equip_target_gsn;
    public uint item_state;
    public ulong item_reg_dt;
    public uint item_lock_state;
}

// 재료 데이터
public class MaterialItemData
{
    public int material_id;
    public int cnt;
}

// 계정 정보
public class AccountInfo
{
    public int account_gsn;
    public string platform_id;
    public string platform_pass;
    public string nickname = "None";
    public ulong join_dt;
    public bool block_type;
    public ulong block_expire_dt;
}

// 유저 정보
public class UserInfo
{
    public uint account_level = 1;      //< 계정 레벨
    public uint account_exp = 0;        //< 계정 경험치
    public uint cash = 10000;               //< 루비
    public uint sapphire = 10000;           //< 사파이어
    public uint gold = 10000;               //< 골드개수
    public uint item_slot_cnt = 100;      //< 아이템 인벤갯수

    public void Set(UserInfo info, bool first = false)
    {
        if (info == null)
            return;

        account_level = info.account_level;
        account_exp = info.account_exp;
        cash = info.cash;
        gold = info.gold;
        sapphire = info.sapphire;
        item_slot_cnt = info.item_slot_cnt;
    }
}

// 유닛 데이터 참고
/* 
     유닛 그레이드 1
     유닛 데이터에 1, 2, 3 넣기
*/