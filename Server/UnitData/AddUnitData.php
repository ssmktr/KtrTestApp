<?php
	include "../DBConnect.php";

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
	}

	$conn->close();
	echo json_encode($data);
	exit();
?>