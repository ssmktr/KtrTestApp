<?php
	include "../DBConnect.php";
	include "GetAllUnitData.php";

function AddUnitData()
{
	$account_gsn = $_REQUEST["account_gsn"];
	$pc_id = $_REQUEST["pc_id"];

	$data = array();
	$data["ecode"] = "success";

	$conn = mysqli_connect($host, $dbId, $dbPw, $dbname);

	if(!$conn)
	{
		$data["ecode"] = "empty_db";
	}
	else
	{
		$sql = sprintf("INSERT INTO UnitData 
			(account_gsn, pc_id)
			VALUES
			('%d', '%d')", $account_gsn, $pc_id);
		$result = mysqli_query($conn, $sql);

		// 추가된 데이터 출력
		$AddUnitSql = sprintf("SELECT * FROM UnitData WHERE ");
	}

	$conn->close();
	echo json_encode($data);
	exit();
}
	
?>