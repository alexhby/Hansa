
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

$id=0;
$uname=$_POST["username"];
$pword=$_POST["password"];




$sql = "SELECT * FROM users WHERE username='$user' AND password='$pword'";
$result = $conn->query($sql);
$success="...";




if ($result->num_rows > 0) {
    	logsuccess();    
} else {
    $success = "0 results";
    logfail();
}



function logsuccess(){

	//CONNECT TO DB AND GET PLAYER INFO
	echo "0";
	
	

}

function logfail(){
	//RETURN ERROR!!!!

	echo "1";
	
}




$conn->close();
?>

