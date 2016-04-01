
<?php
require 'vendor/autoload.php';


//$shop = $body1->{"shop"}; // if we want to define specifc shops
//$epwd = $body1->{"password"};


$servername = "localhost";
$username = "root";
$password = "260562995";
$dbname = "hansa361";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

$sql = "SELECT * FROM shop ";
$result = $conn->query($sql);
$success="...";
$id; 
$enumtype;
$res = array();





if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
    	global $id, $enumtype;
    	
    	$id = $row["ID"];
    	$enumtype = $row["TABLE"];
    	findItem($id, $enumtype);
    }
} 
else {
    $success = "0 results";
}


echo json_encode($res);



function findItem($ID, $TABLE){
	global $conn, $res;
	$sql2 = "SELECT * FROM $TABLE WHERE ID = $ID";
	$result2 = $conn->query($sql2);

	if ($result2->num_rows > 0) {
	    // output data of each row
	    while($row = $result2->fetch_assoc()) {
	    	
	    	array_push($res, $row);
	    }
	}

}




$conn->close();
?>

