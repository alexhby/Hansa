
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



$sql = "SELECT * FROM areas ORDER BY Global_Area_ID DESC LIMIT 1";
$result = $conn->query($sql);
$success="...";

$kingdomCount = $_POST["kingdomCount"];
$UserID = $_POST["userID"];
$WorldName = $_POST["name"];
$date = date("Y-m-d h:m:s");
//$kingdomCount = 9;




//FOR NOW ONLY CAN CREATE 9 worlds (1-9)
//cities are 1,7,14,16,19,23,28,29,32

$row = $result->fetch_assoc();
$worldNum = $row["Global_Area_ID"]/34 + 1;
echo $worldNum;
$worldID = "00000".$worldNum;
$sql = "INSERT INTO areas (Global_Area_ID, area_ID, world_ID, enemy_kingdom_ID, owner_kingdom_ID, takeOverCount, DefendCount) VALUES (NULL, '000001', '$worldID', '000000', '000001', '0', '0')";

for($i = 2; $i <= 34; $i++){
    $ownerid = "000000";
    if(($i == 1 || $i == 2 || $i == 3 || $i == 4) && $kingdomCount > 0){
        $ownerid = "000001";
        
    }
    if(($i == 7 || $i == 6 || $i == 5) && $kingdomCount > 1){
        $ownerid = "000002";
        
    }
    if(($i == 14 || $i == 13 || $i==10 || $i==15) && $kingdomCount > 2){
        $ownerid = "000003";
        
    }
    if(($i == 16 || $i ==30 || $i == 17 || $i == 31 )&& $kingdomCount > 3){
        $ownerid = "000004";
        
    }
    if(($i == 19 || $i==18 || $i == 20 || $i == 33)&& $kingdomCount > 4){
        $ownerid = "000005";
        
    }
    if(($i == 23 || $i == 24 || $i == 25 || $i == 34) && $kingdomCount > 5){
        $ownerid = "000006";
        
    }
    if(($i == 28 || $i == 27 || $i == 26 || $i == 8) && $kingdomCount > 6){
        $ownerid = "000007";
        
    }
    if(($i == 29 || $i == 11 || $i == 12 || $i == 9) && $kingdomCount > 7){
        $ownerid = "000008";
        
    }
    if(($i == 32 || $i == 22 || $i ==21) && $kingdomCount > 8){
        $ownerid = "000009";
        
    }


    if($i < 10){
        $sql = $sql.",(NULL, '00000".$i."', '$worldID', '000000', '$ownerid', '0', '0')";
    }
    else{
        $sql = $sql.",(NULL, '0000".$i."', '$worldID', '000000', '$ownerid', '0', '0')";
    }
}
//echo $sql;
$res = $conn->query($sql);
/*$UserID = "000055";
$WorldName = "TommyWorld";
$date = date("Y-m-d h:m:s");*/

$sql = "INSERT INTO worlds (world_ID, owner_ID, name, CurrentTime) VALUES ('$worldID','$UserID','$WorldName','$date');";
echo $sql;
$res = $conn->query($sql);
echo "END SUCCESS!";





$conn->close();
?>

