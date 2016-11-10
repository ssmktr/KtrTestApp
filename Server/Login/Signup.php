<?php
	include "../DBConnect.php";

	$id = $_REQUEST["platform_id"];
	$pw = $_REQUEST["platform_pass"];
	$nickname = $_REQUEST["nickname"];

	$data = array();
	$data["ecode"] = "success";

	$conn = new mysqli($host, $dbId, $dbPw, $dbname);

	if(!$conn)
	{
		$data["ecode"] = "empty_db";
	}
	else
	{
		$sql = sprintf("SELECT * FROM AccountInfo WHERE platform_id='%s'", $id);
		$query = $conn->query($sql);
		if(mysqli_num_rows($query) > 0)
		{
			$data["ecode"] = "have_account";
		}
		else
		{
			$sql = sprintf("INSERT INTO AccountInfo 
				(platform_id, platform_pass, nickname, join_dt, block_type, block_expire_dt) 
				VALUES 
				('%s', '%s', '%s', '%lf', '%b', '%lf')", $id, $pw, $nickname, time(), 0, 1000);
			$result = mysqli_query($conn, $sql);

			// 정상적으로 계정이 추가됐으면 데이터 불러온다
			$sql = sprintf("SELECT * FROM AccountInfo WHERE platform_id='%s' && platform_pass='%s'", $id, $pw);
			$result = mysqli_query($conn, $sql);
			if(mysqli_num_rows($result) > 0)
			{
				$row = $result->fetch_assoc();
				$AccountInfoData = array();
				$AccountInfoData["account_gsn"] = $row["account_gsn"];
				$AccountInfoData["platform_id"] = $row["platform_id"];
				$AccountInfoData["platform_pass"] = $row["platform_pass"];
				$AccountInfoData["nickname"] = $row["nickname"];
				$AccountInfoData["join_dt"] = $row["join_dt"];
				$AccountInfoData["block_type"] = $row["block_type"];
				$AccountInfoData["block_expire_dt"] = $row["block_expire_dt"];
				$data["AccountInfo"] = $AccountInfoData;

				// 유저정보 추가
				$UserInfoSql = sprintf("INSERT INTO UserInfo
					(account_gsn)
					VALUES
					('%d')", (int)$row["account_gsn"]);
				$UserInfoResult = mysqli_query($conn, $UserInfoSql);

				// 재료데이터 추가
				$MaterialSql = sprintf("INSERT INTO MaterialItemData
					(account_gsn, material_id, cnt)
					VALUES
					('%d', '%d', '%d')", (int)$row["account_gsn"], 101, 1);
				$MaterialResult = mysqli_query($conn, $MaterialSql);

				// 유닛정보 추가 (3마리)
				$UnitDataSql = sprintf("INSERT INTO UnitData
					(account_gsn, pc_id)
					VALUES
					('%d', '%d')", (int)$row["account_gsn"], 101);
				$UnitDataResult = mysqli_query($conn, $UnitDataSql);

				$UnitDataSql = sprintf("INSERT INTO UnitData
					(account_gsn, pc_id)
					VALUES
					('%d', '%d')", (int)$row["account_gsn"], 102);
				$UnitDataResult = mysqli_query($conn, $UnitDataSql);

				$UnitDataSql = sprintf("INSERT INTO UnitData
					(account_gsn, pc_id)
					VALUES
					('%d', '%d')", (int)$row["account_gsn"], 103);
				$UnitDataResult = mysqli_query($conn, $UnitDataSql);	

				// 아이템정보 추가
				$UserInfoSql = sprintf("INSERT INTO ItemData
					(account_gsn)
					VALUES
					('%d')", (int)$row["account_gsn"]);
				$UserInfoResult = mysqli_query($conn, $UserInfoSql);			
			}
			else
			{
				$data['ecode'] = "bad data";
			}
		}
	}

	$conn->close();
	echo json_encode($data);
	exit();
?>