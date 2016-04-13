
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



$sql = "SELECT * FROM worlds";
$result = $conn->query($sql);
$success="...";


$res = array();





if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
    	array_push($res, $row);
    }
} 
else {
    $success = "0 results";
}


echo json_encode($res);




$conn->close();
?>

