
<?php


$servername = "hansa361.c8rnmv72to33.us-east-1.rds.amazonaws.com:3306:3306";
$username = "alexhe";
$password = "hansa361team";
$dbname = "hansa361";

//THREE POST VARIABLES --- "world" is the world ID ------- "area" is the area ID --------- "attackerSuccess" is a bool for if it was an attacker or defender that just won a battle


// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

$sql = "SELECT * FROM areas WHERE world_ID = ".$_POST["world"]." AND area_ID = ".$_POST["area"];
$result = $conn->query($sql);
$success="...";
$battleSuccessCount;
$oppositionID;




//0 means the war was won -- 1 means the war was already over! 	


if ($result->num_rows > 0) {
    // output data of each row
    //Make sure war hasn't already been won
    if($row["enemy_kingdom_ID"] != "000000"){
	    $row = $result->fetch_assoc();
	    if($_POST["attackerSuccess"]){
	    	$oppositionID = $row["enemy_kingdom_ID"];
		    $battleSuccessCount = $row["takeOverCount"] + 1;
		    

		    if($battleSuccessCount <= 3){
		    	$sql = "UPDATE areas SET takeOverCount =  '$battleSuccessCount' WHERE  area_ID =  ".$_POST["area"]." AND world_ID = ".$_POST["world"];
		    }
		    else{
		    	$sql = "UPDATE  areas SET enemy_kingdom_ID = '000000',owner_kingdom_ID =  '$oppositionID' WHERE area_ID =  ".$_POST["area"]." AND world_ID = ".$_POST["world"];
		    	echo "0";
		    }
		}
		else{
			$battleSuccessCount = $row["DefendCount"] + 1;
		    if($battleSuccessCount <= 3){
		    	$sql = "UPDATE areas SET DefendCount =  '$battleSuccessCount' WHERE  area_ID =  ".$_POST["area"]." AND world_ID = ".$_POST["world"];
		    }
		    else{
		    	$sql = "UPDATE  areas SET enemy_kingdom_ID =  '000000' WHERE area_ID =  ".$_POST["area"]." AND world_ID = ".$_POST["world"];
		    	echo "0";
		    }
		}
	   	$conn->query($sql);
	}
	else{
		echo "1";
	}

    
} 
else {
    $success = "0 results";
}







$conn->close();
?>

