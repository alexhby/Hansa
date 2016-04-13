
<?php


$servername = "hansa361.c8rnmv72to33.us-east-1.rds.amazonaws.com:3306:3306";
$username = "alexhe";
$password = "hansa361team";
$dbname = "hansa361";



// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 
$world = "000001";
//$world = $_POST["world"];
//$area = $_POST["area"];
//$alliance = $_POST["enemy"];


$sql = "SELECT * FROM worlds WHERE world_ID = $world";
$res = $conn->query($sql);
$row = $res->fetch_assoc()["CurrentTime"];
echo $row;
date_default_timezone_set("EST"); 
echo "\n";
echo date("Y-m-d h:m:s");

$datetime1 = new DateTime($row);
$datetime2 = new DateTime(date("Y-m-d h:m:s"));
echo "\n";

$interval = $datetime1->diff($datetime2);
echo $interval->format('%R%a days');
//$world = "000001";
//$area = "000005";
//$alliance = "000003";

/*$sql = "UPDATE areas SET enemy_kingdom_ID =  '$alliance' WHERE area_ID =  ".$area." AND world_ID = ".$world;
		    	
$result = $conn->query($sql);*/
echo "\n";

echo "0";


$conn->close();
?>

