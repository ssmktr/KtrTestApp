<?php
	include "../DBConnect.php";

	$id = $_REQUEST["platform_id"];
	$pw = $_REQUEST["platform_pass"];

	$data = array();
	$data["ecode"] = "success";

	$conn = mysqli_connect($host, $dbId, $dbPw, $dbname);

	if(!$conn)
	{
		$data["ecode"] = "empty_db";
	}
	else
	{
		// 계정 정보 가져오기
		$sql = sprintf("SELECT * FROM AccountInfo WHERE platform_id='%s' && platform_pass='%s'", $id, $pw);
		$result = mysqli_query($conn, $sql);
		if(mysqli_num_rows($result) > 0)
		{
			$row = $result->fetch_assoc();
			$AccountInfo = array();
			$AccountInfo["account_gsn"] = $row["account_gsn"];
			$AccountInfo["platform_id"] = $row["platform_id"];
			$AccountInfo["platform_pass"] = $row["platform_pass"];
			$AccountInfo["nickname"] = $row["nickname"];
			$AccountInfo["join_dt"] = $row["join_dt"];
			$AccountInfo["block_type"] = $row["block_type"];
			$AccountInfo["block_expire_dt"] = $row["block_expire_dt"];
			$data["AccountInfo"] = $AccountInfo;
		}
		else
		{
			$data["ecode"] = "notfound_account";
		}
	}

	mysqli_close($conn);
	echo json_encode($data);
	exit();
?>