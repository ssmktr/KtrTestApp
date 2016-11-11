<?php
	include "../DBConnect.php";

function GetAllUnitData()
{
	$account_gsn = $_REQUEST["account_gsn"];

	$data = array();
	$data["ecode"] = "success";

	$conn = mysqli_connect($host, $dbId, $dbPw, $dbname);

	if(!$conn)
	{
		$data["ecode"] = "empty_db";
	}
	else
	{
		$sql = sprintf("SELECT * FROM UnitData WHERE account_gsn='%d'", $account_gsn);
		$result = mysqli_query($conn, $sql);

		if(mysqli_num_rows($result) > 0)
		{
			$idx = 0;
			$UnitList = array();
			while($row = $result->fetch_assoc())
			{
				$UnitData = array();
				$UnitData["pc_gsn"] = $row["pc_gsn"];
				$UnitData["pc_id"] = $row["pc_id"];
				$UnitData["star_grade"] = $row["star_grade"];
				$UnitData["pc_position"] = $row["pc_position"];
				$UnitData["pc_reg_dt"] = $row["pc_reg_dt"];
				$UnitData["pc_state"] = $row["pc_state"];
				$UnitData["skill_level_arr"] = $row["skill_level_arr"];

				$UnitList[$idx] = $UnitData;
				$idx++;
			}
			$data["haveUnit"] = $UnitList;
		}
	}

	$conn->close();
	echo json_encode($data);
	exit();
}
	
?>